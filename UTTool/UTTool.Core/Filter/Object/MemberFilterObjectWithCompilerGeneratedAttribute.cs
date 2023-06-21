using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UTTool.Core.Descriptor;

namespace UTTool.Core.Filter.Object
{
    internal class MemberFilterObjectWithCompilerGeneratedAttribute : IMemberFilter
    {
        public void Filter(DecoraterContext context,List<MemberDescripter> members)
        {
            var availables = members.Where(t => context.AssemblyDescriptor.Assembly.ExportedTypes.Contains(t.BaseType)).ToList();
            members.Clear();
            members.AddRange(availables);
        }
    }
}
