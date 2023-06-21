using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTTool.Core.Descriptor;
using UTTool.Core.Generate.GenerateObject;
using UTTool.Core.Extension;
using static System.Formats.Asn1.AsnWriter;

namespace UTTool.Core.Generate
{
    public class DocumentGenerater
    {
        internal Dictionary<Func<DescripterNode, bool>, List<DocumentObject>> DocumentObjectMaps = new();
        public Action<GenerateContext>? BasicAction;
        public Action<object, EventArgs, Color>? TreeNodeChangeAction;
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
            this.DocumentObjectMaps.Add(node => node.NodeType == NodeType.Method && (node.Parent as MemberDescripter).IsInterface && this.GenerateContext.GetOrCreateSetUpScope().IsExists(node.Parent), new List<DocumentObject>()
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
            this.DocumentObjectMaps.Add(node => node.NodeType == NodeType.Method && (node.Parent as MemberDescripter).IsInterface == false && this.GenerateContext.GetOrCreateSetUpScope().IsExists(node.Parent), new List<DocumentObject>()
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
                   this.PreLoad();
                   var blankScope = this.GenerateContext.GetOrCreateBlankScope();
                   blankScope.AttachGenerateItem(new BlankMoqInstanceGenerate(this.CurrentNode));

                    var scope = this.GenerateContext.GetOrCreateSetUpScope();
                    scope.AttachGenerateItem(new InstanceGenerate(this.CurrentNode));

                    this.GenerateContext.Generate();

                    this.TreeNodeChangeAction(obj,e,Color.Blue);
                    this.BasicAction(this.GenerateContext);
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
                    method.GenerateItems.RemoveAll(g => g.DescripterNode.Parent == this.CurrentNode || g.DescripterNode.Parent.Parent == this.CurrentNode);

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
        private void PreLoad()
        {
            var member = this.CurrentNode as MemberDescripter;
            var targetConstructor = member?.BaseType.GetConstructors().Where(c => c.GetParameters().Count() != 0).FirstOrDefault();

            if (targetConstructor != null)
            {
                var root = this.CurrentNode.GetRoot();

                var setupScope = this.GenerateContext.GetOrCreateSetUpScope();
                var blankScope = this.GenerateContext.GetOrCreateBlankScope();
                var injectedObjets = setupScope.GenerateItems.Where(g => g.DescripterNode.NodeType == NodeType.Member).ToList();

                var parameters = targetConstructor.GetParameters().ToList();
                parameters.ForEach(p =>
                {
                    if (!p.ParameterType.IsValueType)
                    {
                        if (!injectedObjets.Exists(io => (io.DescripterNode as MemberDescripter).BaseType == p.ParameterType))
                        {
                            var desc = root.Find(d => d.NodeType == NodeType.Member && ((MemberDescripter)d).BaseType == p.ParameterType);
                            if (desc != null)
                            {
                                blankScope.AttachGenerateItem(new BlankMoqInjectionGenerate(desc));
                                setupScope.AttachGenerateItem(new InjectionGenerate(desc));
                            }
                            else
                            {
                                if (p.ParameterType.IsInterface)
                                {
                                    var memberDescripter = new MemberDescripter(p.ParameterType) { NodeType = NodeType.Member };
                                    memberDescripter.Load(new DecoraterContext() { AssemblyDescriptor = root });

                                    blankScope.AttachGenerateItem(new BlankMoqInjectionGenerate(memberDescripter));
                                    setupScope.AttachGenerateItem(new InjectionGenerate(memberDescripter));
                                }
                            }
                        }
                    }
                });
            }
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
