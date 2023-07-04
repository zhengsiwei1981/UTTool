using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTTool.Core.Descriptor;
using UTTool.Core.Extension;

namespace UTTool.Core.Generate.GenerateObject
{
    /// <summary>
    /// 
    /// </summary>
    internal class InjectionGenerate : IGenerate
    {
        public InjectionGenerate(DescripterNode descripterNode)
        {
            this.DescripterNode = descripterNode;
        }
        public DescripterNode DescripterNode {
            get; set;
        }
        public void Generate(GenerateContext generateContext)
        {
            generateContext.Text.Append($"      _{this.DescripterNode.Name.Substring((this.DescripterNode as MemberDescripter).BaseType.IsInterface == true ? 1 : 0).GetFirstLowerString()} = new Mock<{this.DescripterNode.Name}>(MockBehavior.Loose);\r\n");
        }
    }
}
