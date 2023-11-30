using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTTool.Core.Constructor;
using UTTool.Core.Descriptor;
using UTTool.Core.Generate.GenerateObject;

namespace UTTool.Core.Generate.Batch
{
    public static class FullPublicGenerate
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nodes"></param>
        /// <returns></returns>
        public  static List<GenerateContext> BatchGenerate(List<DescripterNode> nodes)
        {
            var contextList = new List<GenerateContext>();
            nodes.ForEach(node =>
            {
                var generater = new FullGenerater(node);
                generater.Generate();
                contextList.Add(generater.GenerateContext);
            });
            return contextList;
        }
    }
    internal class FullGenerater
    {
        public DescripterNode CurrentNode { get; set; }
        public FullGenerateContext GenerateContext { get; set; }
        private List<string> Namespaces { get; set; }

        public FullGenerater(DescripterNode descripterNode)
        {
            CurrentNode = descripterNode;
            GenerateContext = new FullGenerateContext() { DescripterNode = CurrentNode };
            Namespaces = new List<string>();
        }
        /// <summary>
        /// 
        /// </summary>
        public void Generate()
        {
            this.AttachTextWithoutMethds();
            this.AttachMethods();
            this.GenerateContext.Generate(this.Namespaces);
        }
        /// <summary>
        /// 
        /// </summary>
        private void AttachTextWithoutMethds()
        {
            var constructorSelector = new ConstructorSelector(this.CurrentNode as MemberDescripter);
            var pList = ParameterMappingList.CreateParameterMappingList(constructorSelector.Preferential(), this.GenerateContext);
            pList.Initialize();

            this.AttachNamespaces(pList);
            //this.GenerateContext.Generate();

            var blankScope = this.GenerateContext.GetOrCreateBlankScope();
            blankScope.AttachGenerateItem(new BlankMoqInstanceGenerate(this.CurrentNode));

            var scope = this.GenerateContext.GetOrCreateSetUpScope();
            scope.AttachGenerateItem(new InstanceGenerate(this.CurrentNode, pList, constructorSelector));

        }
        /// <summary>
        /// 
        /// </summary>
        private void AttachMethods()
        {
            this.CurrentNode.Children.ForEach(child =>
            {
                if (child.NodeType == NodeType.Method)
                {
                    var method = this.GenerateContext.AttachMethodForBatch(child);
                    child.Children.ForEach(c =>
                    {
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
                    method.AttachGenerateItem(new ExecuteMethodGenerate(child));
                }
            });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pmList"></param>
        private void AttachNamespaces(ParameterMappingList pmList)
        {
            pmList.Where(p => p is ReferenceParameterMapping).ToList().ForEach(p =>
            {
                Namespaces.Add(p.Parameter.ParameterType.Namespace);
            });
        }
    }
}
