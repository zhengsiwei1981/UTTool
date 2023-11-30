using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UTTool.Core.Extension;
using UTTool.Core.Filter;

namespace UTTool.Core.Descriptor
{
    public class AssemblyDescriptor : DescripterNode
    {
        /// <summary>
        /// 
        /// </summary>
        public Assembly Assembly { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="assembly"></param>
        public AssemblyDescriptor(Assembly assembly)
        {
            Assembly = assembly;
            Name = Assembly?.GetName().Name;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
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
            this.Children = new List<DescripterNode>();
            decoraterContext.AssemblyDescriptor.Assembly.ExportedTypes.ToList().ForEach(t =>
            {
                var member = new MemberDescripter(t) { NodeType = NodeType.Member, Parent = this };
                member.Load(decoraterContext);
                var dir = this.GetDirectorDescripter(t, decoraterContext);
                if (dir != null)
                {
                    dir.Children.Add(member);
                }
                else
                {
                    this.Children.Add(member);
                }
            });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="director"></param>
        /// <returns></returns>
        private DescripterNode GetDirectorDescripter(Type t, DecoraterContext decoraterContext)
        {
            if (t.Namespace == null)
            {
                return null;
            }
            if (t.Namespace.Equals(this.Name))
            {
                return null;
            }
            var _namespace = t.Namespace.Replace(this.Name + ".", "");
            if (string.IsNullOrEmpty(_namespace))
            {
                return null;
            }

            return this.FindOrCreateDirector(_namespace);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="descripterNode"></param>
        /// <returns></returns>
        private DescripterNode FindOrCreateDirector(string _namespace)
        {
            var ss = _namespace.GetSplitSpaces();
            DescripterNode dir = null;
            for (int i = 0; i < ss.Length; i++)
            {
                dir = this.FindDirector(dir, ss[i], _namespace);
            }
            return dir;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_name"></param>
        /// <returns></returns>
        private DescripterNode FindDirector(DescripterNode parent, string _name, string _namespace)
        {
            if (parent == null)
            {
                return this.InnerFind(this, _name, _namespace);
            }
            else
            {
                return this.InnerFind(parent, _name, _namespace);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="_name"></param>
        /// <param name="_namespace"></param>
        /// <returns></returns>
        private DescripterNode InnerFind(DescripterNode parent, string _name, string _namespace)
        {
            var dir = parent.Children.Where(c => c.NodeType == NodeType.Director && c.Name == _name).FirstOrDefault();
            if (dir != null)
            {
                return dir;
            }
            else
            {
                dir = new DirectorDescripter(_name) { Name = _name, Parent = this };
                parent.Children.Add(dir);
                return dir;
            }
        }
    }
}
