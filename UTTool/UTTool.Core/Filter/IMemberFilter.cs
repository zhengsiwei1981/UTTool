using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTTool.Core.Descriptor;

namespace UTTool.Core.Filter
{
    internal interface IMemberFilter
    {
        void Filter(DecoraterContext context,List<MemberDescripter> members);
    }
}
