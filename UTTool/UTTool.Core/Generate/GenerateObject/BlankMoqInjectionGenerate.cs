using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTTool.Core.Descriptor;
using UTTool.Core.Extension;

namespace UTTool.Core.Generate.GenerateObject
{
    internal class BlankMoqInjectionGenerate : IGenerate
    {
        public BlankMoqInjectionGenerate(DescripterNode descripterNode)
        {
            DescripterNode = descripterNode;
        }
        public DescripterNode DescripterNode { get; set; }
        public void Generate(GenerateContext generateContext)
        {
            generateContext.Text.Append($" private Mock<{this.DescripterNode.Name}> _{this.DescripterNode.Name.Substring(1).GetFirstLowerString()};\r\n");
        }
    }
}
