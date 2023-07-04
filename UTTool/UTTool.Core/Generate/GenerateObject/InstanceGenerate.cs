using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using UTTool.Core.Constructor;
using UTTool.Core.Descriptor;
using UTTool.Core.Extension;

namespace UTTool.Core.Generate.GenerateObject
{
    internal class InstanceGenerate : IGenerate
    {
        public InstanceGenerate(DescripterNode descripterNode, ParameterMappingList parameterMappings, ConstructorSelector constructorSelector)
        {
            this.DescripterNode = descripterNode;
            this.ParameterMappings = parameterMappings;
            this.constructorSelector = constructorSelector;
        }
        private ParameterMappingList ParameterMappings { get; set; }
        private ConstructorSelector constructorSelector { get; set; }
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
            var constructor = constructorSelector.Preferential();
            if (constructor.GetParameters().Count() > 0)
            {
                generateContext.Text.Append($"      _{this.DescripterNode.Name.Substring(0).GetFirstLowerString()} = new {this.DescripterNode.Name}");
                generateContext.Text.Append("(");
                generateContext.Text.Append(Environment.NewLine);

                this.ParameterMappings.Render();

                var last = generateContext.Text.ToString().LastIndexOf(",");
                generateContext.Text = generateContext.Text.Remove(last, 2);

                generateContext.Text.Append(");");
                generateContext.Text.Append(Environment.NewLine);
            }
            else
            {
                generateContext.Text.Append($"      _{this.DescripterNode.Name.Substring(0).GetFirstLowerString()} = new {this.DescripterNode.Name}();\r\n");
            }
        }
    }
}
