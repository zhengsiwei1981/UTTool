using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTTool.Core.Extension;
using UTTool.Core.Generate;

namespace UTTool.Core.Constructor.Render
{
    internal class NoSystemReferenceRender : IRender
    {
        public void Render(in ParameterMapping parameterMapping)
        {
            var p = parameterMapping as ReferenceParameterMapping;
            if (p != null && !p.IsSystemType)
            {
                p.Context.Text.Append($"           _{p.Parameter.ParameterType.Name!.Substring(p.Parameter.ParameterType.IsInterface == true ? 1 : 0).GetFirstLowerString()}.Object,");
                p.Context.Text.Append(Environment.NewLine);
            }
        }
    }
}
