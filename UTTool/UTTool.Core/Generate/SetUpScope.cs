using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTTool.Core.Descriptor;

namespace UTTool.Core.Generate
{
    internal class SetUpScope : TextScope
    {
        public SetUpScope(DescripterNode item) : base(item)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <exception cref="NotImplementedException"></exception>
        public override void Generate(GenerateContext generateContext)
        {
            generateContext.Text.Append("[SetUp]\r\n");
            generateContext.Text.Append("public void Setup()\r\n");
            generateContext.Text.Append("{\r\n");
            this.GenerateItems.ForEach(item =>
            {
                item.Generate(generateContext);
            });
            generateContext.Text.Append("}\r\n");
        }
    }
}
