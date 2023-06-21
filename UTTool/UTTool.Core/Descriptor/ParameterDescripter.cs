using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTTool.Core.Descriptor
{
    public class ParameterDescripter : DescripterNode
    {
        /// <summary>
        /// 
        /// </summary>
        public Type? Type { get; set; }
        public string? TypeName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ParameterName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        internal override void FIlteringChildren(DecoraterContext decoraterContext)
        {
            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="decoraterContext"></param>
        /// <exception cref="NotImplementedException"></exception>
        internal override void Load(DecoraterContext decoraterContext)
        {
           
        }
    }
}
