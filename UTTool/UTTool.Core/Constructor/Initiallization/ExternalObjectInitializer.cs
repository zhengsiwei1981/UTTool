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
    internal class ExternalObjectInitializer : IInitiallizer
    {
        public void Initiallize(ReferenceParameterMapping referenceParameterMapping)
        {
            var setupScope = referenceParameterMapping.Context.GetOrCreateSetUpScope();
            var blankScope = referenceParameterMapping.Context.GetOrCreateBlankScope();

            var dcontext = new DecoraterContext() { AssemblyDescriptor = referenceParameterMapping.Context.DescripterNode.GetRoot() };
            var memberDescripter = new MemberDescripter(referenceParameterMapping.Parameter.ParameterType) { NodeType = NodeType.Member, IsLoadFromExtenal = true, IsMock = true };

            memberDescripter.Load(dcontext);
            memberDescripter.FIlteringChildren(dcontext);

            blankScope.AttachGenerateItem(new BlankMoqInjectionGenerate(memberDescripter));
            setupScope.AttachGenerateItem(new InjectionGenerate(memberDescripter));
        }
    }
}
