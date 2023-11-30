using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UTTool.Core.Descriptor;

namespace UTTool.Core.Constructor
{
    internal class ConstructorSelector
    {
        private MemberDescripter MemberDescripter;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="memberDescripter"></param>
        /// <param name="enableArgument"></param>
        public ConstructorSelector(MemberDescripter memberDescripter, bool enableArgument = true)
        {
            this.MemberDescripter = memberDescripter;
            this.EnableArgument = enableArgument;
        }
        public bool EnableArgument {
            get; set;
        }
        public ConstructorInfo[] Constructors {
            get
            {
                return this.MemberDescripter.BaseType.GetConstructors();
            }
        }
        public ConstructorInfo Preferential()
        {
            if (EnableArgument)
            {
                var con = this.Constructors.Where(c => c.GetParameters().Count() > 0).FirstOrDefault();
                if (con == null)
                {
                    return this.Constructors.FirstOrDefault();
                }
            }
            return this.Constructors.FirstOrDefault();
        }
    }
}
