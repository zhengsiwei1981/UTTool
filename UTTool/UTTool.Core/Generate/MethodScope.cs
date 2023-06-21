using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTTool.Core.Descriptor;

namespace UTTool.Core.Generate
{
    internal class MethodScope : TextScope
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        public MethodScope(DescripterNode item) : base(item)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        public override void Generate(GenerateContext generateContext)
        {
            if (ScopeItem != null)
            {
                generateContext.Text.Append("[Test]\r\n");
                generateContext.Text.Append($"public void Test_{ScopeItem.Name}()\r\n");
                generateContext.Text.Append("{\r\n");
                this.GenerateItems.ForEach(item =>
                {
                    item.Generate(generateContext);
                    generateContext.Text.Append("\r\n");
                });
                generateContext.Text.Append("}\r\n");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="generate"></param>
        internal override void AttachGenerateItem(IGenerate generate)
        {
            if (generate.DescripterNode.Parent is MemberDescripter)
            {
                if ((generate.DescripterNode.Parent as MemberDescripter).IsInterface == true)
                {
                    this.GenerateItems.Add(generate);
                    return;
                }
            }
            if (!this.GenerateItems.Exists(g => g.DescripterNode.Equals(generate.DescripterNode)))
            {
                this.GenerateItems.Add(generate);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="generate"></param>
        public void AttachGenerateItem(IGenerate generate,int index)
        {
            if (generate.DescripterNode.Parent is MemberDescripter)
            {
                if ((generate.DescripterNode.Parent as MemberDescripter).IsInterface == true)
                {
                    this.GenerateItems.Insert(index,generate);
                    return;
                }
            }
            if (!this.GenerateItems.Exists(g => g.DescripterNode.Equals(generate.DescripterNode)))
            {
                this.GenerateItems.Insert(index,generate);
            }
        }
    }
}
