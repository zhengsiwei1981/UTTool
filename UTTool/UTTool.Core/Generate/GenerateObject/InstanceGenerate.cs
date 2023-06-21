using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using UTTool.Core.Descriptor;
using UTTool.Core.Extension;

namespace UTTool.Core.Generate.GenerateObject
{
    internal class InstanceGenerate : IGenerate
    {
        public InstanceGenerate(DescripterNode descripterNode)
        {
            this.DescripterNode = descripterNode;
        }
        /// <summary>
        /// 
        /// </summary>
        public DescripterNode DescripterNode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="generateContext"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Generate(GenerateContext generateContext)
        {
            var root = this.DescripterNode.GetRoot();
            var member = this.DescripterNode as MemberDescripter;
            var constructors = member!.BaseType.GetConstructors();
            if (constructors.Length == 0)
            {
                generateContext.Text.Append($"      _{this.DescripterNode.Name.Substring(0).GetFirstLowerString()} = new {this.DescripterNode.Name}();\r\n");
            }
            //default no arguments constructor
            else if (constructors.Length == 1 && constructors[0].GetParameters().Count() == 0)
            {
                generateContext.Text.Append($"      _{this.DescripterNode.Name.Substring(0).GetFirstLowerString()} = new {this.DescripterNode.Name}();\r\n");
            }
            else
            {
                var targetConstructor = constructors.Where(c => c.GetParameters().Count() != 0).FirstOrDefault();
                var parameters = targetConstructor?.GetParameters().ToList();
                var injectedObjets = generateContext.GetOrCreateSetUpScope().GenerateItems.Where(g => g.DescripterNode.NodeType == NodeType.Member).ToList();
                var mappingObjectList = new List<TextMappingObject>();

                parameters!.ForEach(p =>
                {
                    if (!p.ParameterType.IsValueType && p.ParameterType != typeof(string))
                    {
                        var injectObject = injectedObjets.Where(io => (io.DescripterNode as MemberDescripter)!.BaseType == p.ParameterType).FirstOrDefault();
                        if (injectObject != null)
                        {
                            mappingObjectList.Add(new TextMappingObject() { IsInject = true, IsInterface = p.ParameterType.IsInterface, IsValueType = false, Name = injectObject.DescripterNode.Name });
                        }
                        else
                        {
                            mappingObjectList.Add(new TextMappingObject() { IsInject = false, IsInterface = p.ParameterType.IsInterface, IsValueType = false, Name = p.ParameterType.Name });
                        }
                    }
                    else
                    {
                        mappingObjectList.Add(new TextMappingObject() { IsInject = false, IsInterface = false, IsValueType = true, Name = p.Name!, DefaultValue = this.GetPropertyDefaultValue(p.ParameterType) });
                    }
                });

                generateContext.Text.Append($"      _{this.DescripterNode.Name.Substring(0).GetFirstLowerString()} = new {this.DescripterNode.Name}");
                generateContext.Text.Append("(");

                var rowIndex = 0;
                mappingObjectList.ForEach(obj =>
                {
                    if (rowIndex != 0)
                    {
                        generateContext.Text.Append("      ");
                    }
                    if (obj.IsInject)
                    {
                        generateContext.Text.Append($"_{obj.Name.Substring(obj.IsInterface == true ? 1 : 0).GetFirstLowerString()}.Object,");
                    }
                    else
                    {
                        if (obj.IsValueType)
                        {
                            generateContext.Text.Append(obj.DefaultValue + ",");
                        }
                        else
                        {
                            generateContext.Text.Append($"new {obj.Name}(),");
                        }
                    }
                    if (rowIndex < mappingObjectList.Count - 1)
                    {
                        generateContext.Text.Append("\r\n");
                    }
                    rowIndex++;
                });
                generateContext.Text = generateContext.Text.Remove(generateContext.Text.Length - 1, 1);
                generateContext.Text.Append(");");
                generateContext.Text.Append("\r\n");
            }
        }

        private class TextMappingObject
        {
            public bool IsValueType {
                get; set;
            }
            public bool IsInject {
                get; set;
            }
            public bool IsInterface {
                get; set;
            }
            public string Name {
                get; set;
            }
            public string DefaultValue {
                get; set;
            }
        }
    }
}
