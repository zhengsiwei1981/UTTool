using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using UTTool.Core.Constructor.Initiallization;
using UTTool.Core.Constructor.Render;
using UTTool.Core.Descriptor;
using UTTool.Core.Extension;
using UTTool.Core.Generate;

namespace UTTool.Core.Constructor
{
    internal class ReferenceParameterMapping : ParameterMapping
    {
        public ReferenceParameterMapping(ParameterInfo parameter, DescripterNode descripterNode, GenerateContext context) : base(parameter, descripterNode, context)
        {
            this.Initiallizer = new InitializerSelector().Select(this);
        }
        public override void Initialize()
        {
            this.Initiallizer?.Initiallize(this);
        }

        public override void Render()
        {
            if (!IsSystemType)
            {
                new NoSystemReferenceRender().Render(this);
            }
            else
            {
                new SystemObjectRender().Render(this);
            }
        }

        private Initiallization.IInitiallizer Initiallizer { get; set; }

        public bool HasInitializer {
            get
            {
                return this.Initiallizer != null;
            }
        }
        /// <summary>
        /// 是否系统类型
        /// </summary>
        public bool IsSystemType {
            get
            {
                var typenamespace = this.Parameter.ParameterType.Namespace;
                return typenamespace.StartsWith("System") || typenamespace.StartsWith("Microsoft");
            }
        }
        /// <summary>
        /// 是否泛型类型
        /// </summary>
        public bool IsGenericeType {
            get
            {
                return this.Parameter.ParameterType.IsGenericType;
            }
        }
        /// <summary>
        /// 是否已注入
        /// </summary>
        public bool IsInjected {
            get
            {
                if (this.IsIncludeStructure)
                {
                    return this.Context.GetSetupNodes().Exists(d => d.NodeType == NodeType.Member && ((MemberDescripter)d).BaseType == this.Parameter.ParameterType);
                }
                return false;
            }
        }
        /// <summary>
        /// 是否包含在当前结构中
        /// </summary>
        public bool IsIncludeStructure {
            get
            {
                if (this.IsSystemType)
                    return false;
                var node = this.Context.DescripterNode.GetRoot().Find(d => d.NodeType == NodeType.Member && (d as MemberDescripter).BaseType == this.Parameter.ParameterType);
                return node != null;
            }
        }
    }
}
