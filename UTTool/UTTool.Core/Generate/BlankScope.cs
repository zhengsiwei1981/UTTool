using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTTool.Core.Descriptor;

namespace UTTool.Core.Generate
{
    internal class BlankScope : TextScope
    {
        public BlankScope(DescripterNode item) : base(item)
        {
        }

        public override void Generate(GenerateContext generateContext)
        {
            this.GenerateItems.ForEach(item =>
            {
                item.Generate(generateContext);
            });
            generateContext.Text.Append("\r\n");
        }
    }
}
