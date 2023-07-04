using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using UTTool.Core.Constructor.Render;
using UTTool.Core.Descriptor;
using UTTool.Core.Generate;

namespace UTTool.Core.Constructor
{
    internal class ValueTypeParameterMapping : ParameterMapping
    {
        public ValueTypeParameterMapping(ParameterInfo parameter, DescripterNode descripterNode, GenerateContext context) : base(parameter, descripterNode, context)
        {
        }

        public override void Initialize()
        {

        }

        public override void Render()
        {
            new ValueTypeRender().Render(this);
        }
    }
}
