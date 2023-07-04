using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTTool.Core.Descriptor;
using UTTool.Core.Extension;
using UTTool.Core.Generate;

namespace UTTool.Core.Constructor.Initiallization
{
    internal class InitializerSelector
    {
        public InitializerSelector() { }
        public IInitiallizer Select(ReferenceParameterMapping referenceParameterMapping)
        {
            if (referenceParameterMapping.IsSystemType)
            {
                return null;
            }
            var node = referenceParameterMapping.Context.DescripterNode.GetRoot().Find(d => d.NodeType == NodeType.Member && ((MemberDescripter)d).BaseType == referenceParameterMapping.Parameter.ParameterType);

            if (node != null)
            {
                if (!referenceParameterMapping.Context.GetSetupNodes().Exists(d => d.NodeType == NodeType.Member && ((MemberDescripter)d).BaseType == referenceParameterMapping.Parameter.ParameterType))
                {
                    return new UnInjectObjectInitializer();
                }
                return null;
            }
            else
            {
                return new ExternalObjectInitializer();
            }
        }
    }
}
