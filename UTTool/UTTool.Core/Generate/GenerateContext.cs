using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTTool.Core.Descriptor;

namespace UTTool.Core.Generate
{
    public class GenerateContext
    {
        /// <summary>
        /// 
        /// </summary>
        public StringBuilder Text {
            get; set;
        }
        public DescripterNode DescripterNode {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        internal List<TextScope> Scopes { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public GenerateContext()
        {
            this.Text = new StringBuilder();
            this.Scopes = new List<TextScope>();
        }
        /// <summary>
        /// 
        /// </summary>
        public void Generate()
        {
            this.Text.Clear();
            this.Scopes.ToList().ForEach(scope =>
            {
                scope.Generate(this);
            });
        }
        /// <summary>
        /// 
        /// </summary>
        public void ClearScope()
        {
            this.Scopes.Clear();
        }
        public List<DescripterNode> GetSetupNodes()
        {
            var setupScope = this.GetOrCreateSetUpScope();
            return setupScope.GenerateItems.Select(g => g.DescripterNode).ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="descripterNode"></param>
        internal void AttachMethodScope(DescripterNode node)
        {
            //if (!this.Scopes.Exists(s => s.ScopeItem == this.DescripterNode))
            //{
            //    this.Scopes.Add(new MethodScope(this.DescripterNode));
            //}
            this.Scopes.Clear();
            this.Scopes.Add(new MethodScope(node));
        }

        internal MethodScope GetOrCreateMethodScope(DescripterNode node)
        {
            if (this.Scopes.Exists(s => s.GetType() == typeof(MethodScope)) && this.Scopes.FirstOrDefault(s => s.ScopeItem == node) != null)
            {
            }
            else
            {
                this.Scopes.RemoveAll(s => s.GetType() == typeof(MethodScope));
                this.Scopes.Add(new MethodScope(node));
            }
            return (MethodScope)this.Scopes.Where(s => s.GetType() == typeof(MethodScope)).FirstOrDefault();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        internal MethodScope GetMethodScope()
        {
            return (MethodScope)this.Scopes.Where(s => s.GetType() == typeof(MethodScope)).FirstOrDefault();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal MethodScope GetCurrentMethodScope()
        {
            return (MethodScope)this.Scopes.Where(s => s.GetType() == typeof(MethodScope)).FirstOrDefault();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal SetUpScope GetOrCreateSetUpScope()
        {
            if (this.Scopes.Count == 0 || !this.Scopes.Exists(s => s.GetType() == typeof(SetUpScope)))
            {
                if (this.Scopes.Exists(s => s.GetType() == typeof(BlankScope)))
                {
                    this.Scopes.Insert(1, new SetUpScope(null));
                }
                else
                {
                    this.Scopes.Insert(0, new SetUpScope(null));
                }
            }
            return (SetUpScope)this.Scopes.Where(s => s.GetType() == typeof(SetUpScope)).First();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal BlankScope GetOrCreateBlankScope()
        {
            if (this.Scopes.Count == 0 || !this.Scopes.Exists(s => s.GetType() == typeof(BlankScope)))
            {
                this.Scopes.Insert(0, new BlankScope(null));
            }
            return (BlankScope)this.Scopes.Where(s => s.GetType() == typeof(BlankScope)).First();
        }
    }
}
