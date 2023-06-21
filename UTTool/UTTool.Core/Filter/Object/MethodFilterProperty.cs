using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UTTool.Core.Descriptor;

namespace UTTool.Core.Filter.Object
{
    internal class MethodFilterProperty : IMethodFilter
    {
        public void Filter(DecoraterContext context,MemberDescripter memberDescripter)
        {
            var properties = memberDescripter.BaseType.GetProperties().ToList();
            var propertyMethodDefinition = new List<string>();
            foreach (var property in properties)
            {
                propertyMethodDefinition.Add("set_" + property.Name);
                propertyMethodDefinition.Add("get_" + property.Name);
            }

            var availables = memberDescripter.Children.Where(child => !propertyMethodDefinition.Contains(child.Name)).ToList();
            memberDescripter.Children.Clear();
            memberDescripter.Children.AddRange(availables);
        }
    }
}
