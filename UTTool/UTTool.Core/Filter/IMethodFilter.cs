using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UTTool.Core.Descriptor;

namespace UTTool.Core.Filter
{
    internal interface IMethodFilter
    {
        void Filter(DecoraterContext context,MemberDescripter memberDescriptor);
    }
}
