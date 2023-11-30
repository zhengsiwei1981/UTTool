using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTTool.Core.Descriptor;
using UTTool.Core.Extension;
using UTTool.Core.Generate;
using UTTool.Core.Generate.GenerateObject;

namespace UTTool.Core.Constructor.Initiallization
{
    internal class UnInjectObjectInitializer : IInitiallizer
    {
        public void Initiallize(ReferenceParameterMapping referenceParameterMapping)
        {
            var setupScope = referenceParameterMapping.Context.GetOrCreateSetUpScope();
            var blankScope = referenceParameterMapping.Context.GetOrCreateBlankScope();

            var node = referenceParameterMapping.Context.DescripterNode.GetRoot().Find(d => d.NodeType == NodeType.Member && ((MemberDescripter)d).BaseType == referenceParameterMapping.Parameter.ParameterType);

            (node as MemberDescripter).IsMock = true;
            blankScope.AttachGenerateItem(new BlankMoqInjectionGenerate(node));
            setupScope.AttachGenerateItem(new InjectionGenerate(node));
        }
    }
}
