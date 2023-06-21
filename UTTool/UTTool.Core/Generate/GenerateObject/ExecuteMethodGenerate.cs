using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTTool.Core.Descriptor;
using UTTool.Core.Extension;

namespace UTTool.Core.Generate.GenerateObject
{
    internal class ExecuteMethodGenerate : IGenerate
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="descripterNode"></param>
        public ExecuteMethodGenerate(DescripterNode descripterNode)
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
            generateContext.Text.Append($"      var result =_{this.DescripterNode.Parent.Name.GetFirstLowerString()}.{this.DescripterNode.Name}({this.GetMethodParameterDefinition()});\r\n");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string GetMethodParameterDefinition()
        {
            var sb = new StringBuilder();
            this.DescripterNode.Children.ForEach(c =>
            {
                var param = c as ParameterDescripter;
                sb.Append(param.ParameterName.GetLimitName());
                sb.Append(",");
            });
            if (sb.Length > 0)
            {
                sb = sb.Remove(sb.Length - 1, 1);
            }
            return sb.ToString();   
        }
    }
}
