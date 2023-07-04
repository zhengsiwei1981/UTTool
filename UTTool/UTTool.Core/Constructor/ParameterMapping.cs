using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using UTTool.Core.Descriptor;
using UTTool.Core.Generate;

namespace UTTool.Core.Constructor
{
    internal abstract class ParameterMapping
    {
        public ParameterMapping(ParameterInfo parameter, DescripterNode descripterNode, GenerateContext context)
        {
            this.Parameter = parameter;
            this.Node = descripterNode;
            this.Context = context;
        }
        public GenerateContext Context { get; set; }
        public ParameterInfo Parameter { get; set; }
        public DescripterNode Node { get; set; }
        public abstract void Initialize();
        public abstract void Render();
    }

    internal class ParameterMappingList : List<ParameterMapping>
    {
        public ParameterMappingList()
        {

        }
        public void Add(ParameterInfo parameterInfo, DescripterNode descripterNode, GenerateContext generateContext)
        {
            if (parameterInfo.ParameterType.IsValueType || parameterInfo.ParameterType == typeof(string) || parameterInfo.ParameterType == typeof(Nullable<>))
            {
                this.Add(new ValueTypeParameterMapping(parameterInfo, descripterNode, generateContext));
            }
            else
            {
                this.Add(new ReferenceParameterMapping(parameterInfo, descripterNode, generateContext));
            }
        }
        public ParameterMappingList Initialize()
        {
            this.ForEach(x => x.Initialize());
            return this;
        }
        public ParameterMappingList Render()
        {
            this.ForEach(x => x.Render());
            return this;
        }
        public static ParameterMappingList CreateParameterMappingList(ConstructorInfo constructorInfo, GenerateContext generateContext)
        {
            var pList = new ParameterMappingList();
            if (constructorInfo == null)
                return pList;
            var parameters = constructorInfo?.GetParameters().ToList();
            parameters!.ForEach(p =>
            {
                pList.Add(p, generateContext.DescripterNode, generateContext);
            });
            return pList;
        }
    }
}
