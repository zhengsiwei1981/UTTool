using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTTool.Core.Extension;

namespace UTTool.Core.Constructor.Render
{
    internal class ValueTypeRender : IRender
    {
        public void Render(in ParameterMapping parameterMapping)
        {
            var p = parameterMapping as ValueTypeParameterMapping;
            if (p != null)
            {
                parameterMapping.Context.Text.Append("            " + this.GetPropertyDefaultValue(p.Parameter.ParameterType) + ",");
                p.Context.Text.Append(Environment.NewLine);
            }
        }
    }
}
