using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTTool.Core.Descriptor;

namespace UTTool.Core.Filter.Object
{
    internal class MethodFilterBaseClass : IMethodFilter
    {
        public void Filter(DecoraterContext context,MemberDescripter memberDescripter)
        {
            var available = memberDescripter.Children.Where(m => ((MethodDescipter)m)?.MethodInfo?.DeclaringType?.Name == memberDescripter.Name).ToList();
            memberDescripter.Children.Clear();
            memberDescripter.Children.AddRange(available);
        }
    }
}
