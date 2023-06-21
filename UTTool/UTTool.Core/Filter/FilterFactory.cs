using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTTool.Core.Filter.Object;

namespace UTTool.Core.Filter
{
    internal class FilterFactory
    {
        private static List<IMethodFilter> MethodFilter = null;
        private static List<IMemberFilter> MemberFilters = null;
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<IMemberFilter> GetMemberFilter()
        {
            if (MemberFilters == null)
            {
                MemberFilters = new List<IMemberFilter>()
                {
                    new MemberFilterObjectWithCompilerGeneratedAttribute()
                };
            }
            return MemberFilters;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<IMethodFilter> GetMethodFilter()
        {
            if (MethodFilter == null)
            {
                MethodFilter = new List<IMethodFilter>()
                {
                    new MethodFilterProperty(),
                    new MethodFilterBaseClass()
                };
            }
            return MethodFilter;
        }
    }
}
