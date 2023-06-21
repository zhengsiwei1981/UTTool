using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTTool.Core.Extension;

namespace UTTool.Core.Descriptor
{
    public class DirectorDescripter : DescripterNode
    {
        public string Namspace { get; set; }
        public DirectorDescripter(string _namespace)
        {
            this.Namspace = _namespace;
            this.NodeType = NodeType.Director;
            this.Children = new List<DescripterNode>();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="decoraterContext"></param>
        internal override void FIlteringChildren(DecoraterContext decoraterContext)
        {
            this.Children.ForEach(child =>
            {
                child.FIlteringChildren(decoraterContext);
            });
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
