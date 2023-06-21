using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTTool.Core.Descriptor
{
    public abstract class DescripterNode
    {
        public DescripterNode Parent {
            get; set;
        }
        public List<DescripterNode> Children { get; set; }
        public NodeType NodeType { get; set; }
        public string Name {
            get;
            set;
        }
        internal abstract void FIlteringChildren(DecoraterContext decoraterContext);
        internal abstract void Load(DecoraterContext decoraterContext);
    }
    public enum NodeType
    {
        None,
        Member,
        Method,
        Parameter,
        Director
    }
}
