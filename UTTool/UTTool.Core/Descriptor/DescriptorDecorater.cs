using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTTool.Core.Filter;

namespace UTTool.Core.Descriptor
{
    internal class DescriptorDecorater
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public DescriptorDecorater(DecoraterContext context)
        {
            this.DecoraterContext = context;
        }
        public DecoraterContext DecoraterContext { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="assemblyDescriptor"></param>
        public void DecoraterProcess()
        {
            this.DecoraterContext.AssemblyDescriptor.Load(this.DecoraterContext);
            this.DecoraterContext.AssemblyDescriptor.FIlteringChildren(this.DecoraterContext);
        }
    }
    /// <summary>
    /// 
    /// </summary>
    internal class DecoraterContext
    {
        public string? Path {
            get; set;
        }
        public AssemblyDescriptor AssemblyDescriptor {
            get; set;
        }
    }
}
