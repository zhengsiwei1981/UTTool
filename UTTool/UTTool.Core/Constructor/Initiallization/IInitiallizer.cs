using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTTool.Core.Generate;

namespace UTTool.Core.Constructor.Initiallization
{
    internal interface IInitiallizer
    {
        void Initiallize(ReferenceParameterMapping referenceParameterMapping);
    }
}
