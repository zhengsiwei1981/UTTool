using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTTool.Core.Descriptor;
using UTTool.Core.Extension;

namespace UTTool.Core.Generate.GenerateObject
{
    internal class ParameterInitializationGenerate : IGenerate
    {
        public ParameterInitializationGenerate(DescripterNode descripterNode)
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
        public void Generate(GenerateContext generateContext)
        {
            var param = this.DescripterNode as ParameterDescripter;
            generateContext.Text.Append($"      var {param.ParameterName.GetFirstLowerString()} = {this.GetPropertyDefaultValue(param.Type)};");
        }
    }
}
