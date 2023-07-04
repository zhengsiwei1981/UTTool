using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTTool.Core.Constructor.Render
{
    internal interface IRender
    {
        void Render(in ParameterMapping parameterMapping);
    }
}
