using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTTool.Core.Descriptor;

namespace UTTool.Core.Generate
{
    internal abstract class TextScope
    {
        /// <summary>
        /// 
        /// </summary>
        public TextScope(DescripterNode item)
        {
            this.GenerateItems = new List<IGenerate>();
            this.ScopeItem = item;
        }
        /// <summary>
        /// 
        /// </summary>
        public DescripterNode ScopeItem { get; set; }
        /// <summary>
        /// 
        /// </summary>
        internal List<IGenerate> GenerateItems { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public abstract void Generate(GenerateContext generateContext);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="generate"></param>
        internal virtual void AttachGenerateItem(IGenerate generate)
        {
            if (!this.GenerateItems.Exists(g => g.DescripterNode.Equals(generate.DescripterNode)))
            {
                this.GenerateItems.Add(generate);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="descripterNode"></param>
        public virtual void RemoveItem(DescripterNode descripterNode)
        {
            var removeItem = this.GenerateItems.FirstOrDefault(g => g.DescripterNode == descripterNode);
            if (removeItem != null)
            {
                this.GenerateItems.Remove(removeItem);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal bool IsExists(DescripterNode node)
        {
            return this.GenerateItems.Exists(g => g.DescripterNode == node);
        }
    }
}
