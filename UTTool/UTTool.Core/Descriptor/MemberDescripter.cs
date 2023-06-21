using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTTool.Core.Filter;

namespace UTTool.Core.Descriptor
{
    public class MemberDescripter : DescripterNode
    {
        /// <summary>
        /// 
        /// </summary>
        public Type BaseType {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        public MemberDescripter(Type type)
        {
            this.Name = type.Name;
            this.BaseType = type;
        }
        /// <summary>
        /// 
        /// </summary>
        public bool IsInterface {
            get
            {
                return this.BaseType.IsInterface;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        internal override void FIlteringChildren(DecoraterContext decoraterContext)
        {
            foreach (var filter in FilterFactory.GetMethodFilter())
            {
                filter.Filter(decoraterContext, this);
            }
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
            this.Children = new List<DescripterNode>();
            foreach (var method in this.BaseType.GetMethods())
            {
                var methodDescripter = new MethodDescipter(method) { Parent = this, NodeType = NodeType.Method };
                methodDescripter.Load(decoraterContext);
                this.Children.Add(methodDescripter);
            }
        }
    }
}
