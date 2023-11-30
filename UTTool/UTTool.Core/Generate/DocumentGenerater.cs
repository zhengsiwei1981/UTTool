using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTTool.Core.Descriptor;
using UTTool.Core.Generate.GenerateObject;
using UTTool.Core.Extension;
using UTTool.Core.Constructor;
using UTTool.Core.Generate.Batch;

namespace UTTool.Core.Generate
{
    public class DocumentGenerater
    {
        internal Dictionary<Func<DescripterNode, bool>, List<DocumentObject>> DocumentObjectMaps = new Dictionary<Func<DescripterNode, bool>, List<DocumentObject>>();
        public Action<GenerateContext> BasicAction;
        public Action<object, EventArgs, Color> TreeNodeChangeAction;
        public GenerateContext GenerateContext {
            get; set;
        }
        public DescripterNode CurrentNode {
            get; set;
        }
        public void SetUp(DescripterNode descripterNode)
        {
            this.CurrentNode = descripterNode;
            this.GenerateContext.DescripterNode = this.CurrentNode;
        }
        /// <summary>
        /// 
        /// </summary>
        public DocumentGenerater()
        {
            this.GenerateContext = new GenerateContext();

            //Method map
            //this.DocumentObjectMaps.Add(node => node.NodeType == NodeType.Method && (node.Parent as MemberDescripter).IsInterface == false, new List<DocumentObject>()
            //{
            //    new DocumentObject("生成方法体",(obj,e)=>{
            //         if (this.CurrentNode == null)
            //         {
            //             return;
            //         }
            //         this.GenerateContext.GetOrCreateMethodScope(this.CurrentNode);
            //         this.GenerateContext.Generate();
            //         this.BasicAction(this.GenerateContext);
            //    })
            //});
            this.DocumentObjectMaps.Add(node => node.NodeType == NodeType.Director, new List<DocumentObject>()
            {
                new DocumentObject("批量生成",(obj,e) =>
                {
                    var context = new FullGenerateContext(){ DescripterNode = this.CurrentNode, IsReadForBatch = true};
                    BasicAction(context);
                })
            });

            this.DocumentObjectMaps.Add(node => node.NodeType == NodeType.Method && ((node.Parent as MemberDescripter).IsInterface || (node.Parent as MemberDescripter).IsMock) && this.GenerateContext.GetOrCreateSetUpScope().IsExists(node.Parent), new List<DocumentObject>()
            {
                new DocumentObject("注入方法",(obj,e)=>{
                    if (this.CurrentNode == null)
                    {
                        return;
                    }
                    var method = this.GenerateContext.GetCurrentMethodScope();
                    if (method != null)
                    {
                        method.AttachGenerateItem(new InjectionMethodGenerate(this.CurrentNode,false),0);
                        this.GenerateContext.Generate();
                        this.BasicAction(this.GenerateContext);
                    }
                }),
                new DocumentObject("验证",(obj,e)=>{
                     if (this.CurrentNode == null)
                    {
                        return;
                    }
                    var method = this.GenerateContext.GetCurrentMethodScope();
                    if (method != null)
                    {
                        method.AttachGenerateItem(new InjectionMethodGenerate(this.CurrentNode,true));
                        this.GenerateContext.Generate();
                        this.BasicAction(this.GenerateContext);
                    }
                })
            });
            this.DocumentObjectMaps.Add(node => node.NodeType == NodeType.Method && ((node.Parent as MemberDescripter).IsInterface == false &&
            (node.Parent as MemberDescripter).IsMock == false) && this.GenerateContext.GetOrCreateSetUpScope().IsExists(node.Parent), new List<DocumentObject>()
            {
                new DocumentObject("生成测试方法",(obj,e)=>{
                    var method = this.GenerateContext.GetOrCreateMethodScope(this.CurrentNode);
                    this.CurrentNode.Children.ForEach(c =>{
                        var param = c as ParameterDescripter;
                        if (!param.Type.IsValueType && param.Type != typeof(string))
                        {
                            if (!method.IsExists(c))
                            {
                                method.AttachGenerateItem(new ParameterInstanceGenerate(c));
                            }
                        }
                        else
                        {
                            if (!method.IsExists(c))
                            {
                                method.AttachGenerateItem(new ParameterInitializationGenerate(c));
                            }
                        }
                    });
                    if (method != null)
                    {
                        method.AttachGenerateItem(new ExecuteMethodGenerate(this.CurrentNode));
                    }
                    this.GenerateContext.Generate();
                    this.BasicAction(this.GenerateContext);
                }),
                new DocumentObject("生成调用方法",(obj,e) =>{
                    if (this.CurrentNode == null)
                    {
                        return;
                    }
                    var method = this.GenerateContext.GetCurrentMethodScope();
                    this.CurrentNode.Children.ForEach(c =>{
                        var param = c as ParameterDescripter;
                        if (!param.Type.IsValueType && param.Type != typeof(string))
                        {
                            if (!method.IsExists(c))
                            {
                                method.AttachGenerateItem(new ParameterInstanceGenerate(c));
                            }
                        }
                        else
                        {
                            if (!method.IsExists(c))
                            {
                                method.AttachGenerateItem(new ParameterInitializationGenerate(c));
                            }
                        }
                    });
                    if (method != null)
                    {
                        method.AttachGenerateItem(new ExecuteMethodGenerate(this.CurrentNode));
                    }
                    this.GenerateContext.Generate();
                    this.BasicAction(this.GenerateContext);
                })
            });
            //Member map
            this.DocumentObjectMaps.Add(node => node.NodeType == NodeType.Member && (node as MemberDescripter).IsInterface, new List<DocumentObject>()
            {
                new DocumentObject("接口注入",(obj,e) =>{
                    if (this.CurrentNode == null)
                    {
                        return;
                    }
                    var blankScope = this.GenerateContext.GetOrCreateBlankScope();
                    blankScope.AttachGenerateItem(new BlankMoqInjectionGenerate(this.CurrentNode));

                    var scope = this.GenerateContext.GetOrCreateSetUpScope();
                    scope.AttachGenerateItem(new InjectionGenerate(this.CurrentNode));

                    this.GenerateContext.Generate();
                    this.TreeNodeChangeAction(obj,e,Color.Blue);
                    this.BasicAction(this.GenerateContext);
                })
            });
            this.DocumentObjectMaps.Add(node => node.NodeType == NodeType.Member && (node as MemberDescripter).IsInterface == false && (node as MemberDescripter).BaseType.IsAbstract == false, new List<DocumentObject>()
            {
                new DocumentObject("实例化",(obj,e) =>{
                    if (this.CurrentNode == null)
                    {
                         return;
                    }
                    if (this.GenerateContext.GetOrCreateSetUpScope().GenerateItems.Exists(g => g.DescripterNode.Equals(this.CurrentNode)))
                    {
                        return;
                    }
                    var constructorSelector = new ConstructorSelector(this.GenerateContext.DescripterNode as MemberDescripter);
                    var pList = ParameterMappingList.CreateParameterMappingList(constructorSelector.Preferential(),this.GenerateContext);
                    pList.Initialize();
                    this.GenerateContext.Generate();

                    var blankScope = this.GenerateContext.GetOrCreateBlankScope();
                    blankScope.AttachGenerateItem(new BlankMoqInstanceGenerate(this.CurrentNode));

                    var scope = this.GenerateContext.GetOrCreateSetUpScope();
                    scope.AttachGenerateItem(new InstanceGenerate(this.CurrentNode,pList,constructorSelector));

                    this.GenerateContext.Generate();
                    this.BasicAction(this.GenerateContext);
                }),
                new DocumentObject("生成文件",(obj,e)=>{
                    if (this.CurrentNode == null)
                    {
                         return;
                    }
                    FullGenerater fullGenerater = new FullGenerater(this.CurrentNode);
                    fullGenerater.Generate();
                     this.BasicAction(fullGenerater.GenerateContext);
                })
            });
            this.DocumentObjectMaps.Add(node => node.NodeType == NodeType.Member && this.GenerateContext.GetOrCreateSetUpScope().IsExists(node), new List<DocumentObject>()
            {
                new DocumentObject("清除",(obj,e) =>{
                    if (this.CurrentNode == null)
                    {
                         return;
                    }
                    var blankScope = this.GenerateContext.GetOrCreateBlankScope();
                    blankScope.RemoveItem(this.CurrentNode);

                    var scope = this.GenerateContext.GetOrCreateSetUpScope();
                    scope.RemoveItem(this.CurrentNode);

                    var method = this.GenerateContext.GetCurrentMethodScope();
                    method?.GenerateItems.RemoveAll(g => g.DescripterNode.Parent == this.CurrentNode || g.DescripterNode.Parent.Parent == this.CurrentNode);

                    this.GenerateContext.Generate();
                    this.TreeNodeChangeAction(obj,e,Color.Black);
                    this.BasicAction(this.GenerateContext);
                })
            });
            //Parameter map
            this.DocumentObjectMaps.Add(node => node.NodeType == NodeType.Parameter && (node as ParameterDescripter).Type.IsClass && (node as ParameterDescripter).Type != typeof(string), new List<DocumentObject>()
            {
                new DocumentObject("实例化参数对象",(obj,e) =>
                {
                        var method = this.GenerateContext.GetOrCreateMethodScope(this.CurrentNode.Parent);
                        method.AttachGenerateItem(new ParameterInstanceGenerate(this.CurrentNode));
                        this.GenerateContext.Generate();
                        this.BasicAction(this.GenerateContext);
                })
            });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<DescripterNode> GetAllNode()
        {
            if (this.GenerateContext.Scopes.Count > 0)
            {
                return this.GenerateContext.Scopes.Select(s => s.GenerateItems).SelectMany(g => g.ToList()).Select(g => g.DescripterNode).ToList();
            }
            return new List<DescripterNode>();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nodeType"></param>
        /// <returns></returns>
        public List<DocumentObject> GetDocumentObjects()
        {
            if (this.CurrentNode == null)
            {
                return null;
            }
            var map = this.DocumentObjectMaps.ToList().Where(kv => kv.Key(this.CurrentNode)).ToList();
            return map.SelectMany(m => m.Value).ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        public void Clear()
        {
            this.GenerateContext.ClearScope();
            this.GenerateContext.DescripterNode = null;
            this.GenerateContext.Text.Clear();
            this.BasicAction(this.GenerateContext);
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public class DocumentObject
    {
        public DocumentObject(string eventname, EventHandler eventHandler)
        {
            this.EventName = eventname;
            this.EventHandler = eventHandler;
        }
        public string EventName {
            get; set;
        }
        public EventHandler EventHandler {
            get; set;
        }
    }
}
