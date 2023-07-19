using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTTool.Core.Descriptor;

namespace UTTool.Core.Generate.Batch
{
    /// <summary>
    /// 
    /// </summary>
    public class FullGenerateContext : GenerateContext
    {
        public bool IsReadForBatch { get; set; }
        internal MethodScope AttachMethodForBatch(DescripterNode node)
        {
            var scope = new MethodScope(node);
            this.Scopes.Add(scope);
            return scope;
        }
        internal new void Generate(List<string> namespaces)
        {
            this.Text.Clear();
            string[] defaultNamespaces = { "System", "System.Collections.Generic", "System.Linq" , "System.Text", "System.Threading.Tasks", "NUnit.Framework" };
            for (int i = 0; i < defaultNamespaces.Length; i++)
            {
                this.Text.Append($"using {defaultNamespaces[i]};");
                this.Text.Append(Environment.NewLine);
            }
            namespaces.ForEach(n =>
            {
                this.Text.Append($"using {n};");
                this.Text.Append(Environment.NewLine);
            });
            this.Text.Append($"public class {this.DescripterNode.Name + "_Test"}{Environment.NewLine}");
            this.Text.Append("{");
            this.Scopes.ToList().ForEach(scope =>
            {
                scope.Generate(this);
            });
            this.Text.Append("}");
        }
    }
}
