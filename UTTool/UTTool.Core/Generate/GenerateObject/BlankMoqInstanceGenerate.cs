using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTTool.Core.Descriptor;
using UTTool.Core.Extension;

namespace UTTool.Core.Generate.GenerateObject
{
    internal class BlankMoqInstanceGenerate : IGenerate
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="descripterNode"></param>
        public BlankMoqInstanceGenerate(DescripterNode descripterNode)
        {
            this.DescripterNode = descripterNode;
        }
        public DescripterNode DescripterNode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="generateContext"></param>
        public void Generate(GenerateContext generateContext)
        {
            generateContext.Text.Append($" private {this.DescripterNode.Name} _{this.DescripterNode.Name.Substring(0).GetFirstLowerString()};\r\n");
        }
    }
}
