using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTTool.Core.Descriptor;

namespace UTTool.Core.Generate
{
    internal interface IGenerate
    {
        public DescripterNode DescripterNode {
            get;set;
        }
        void Generate(GenerateContext generateContext);
    }
}
