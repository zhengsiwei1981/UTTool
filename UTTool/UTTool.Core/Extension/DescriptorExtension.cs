using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UTTool.Core.Descriptor;

namespace UTTool.Core.Extension
{
    internal static class DescriptorExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="descripter"></param>
        /// <returns></returns>
        public static AssemblyDescriptor GetRoot(this DescripterNode descripter)
        {
            if (descripter.GetType() == typeof(AssemblyDescriptor))
            {
                return (AssemblyDescriptor)descripter;
            }
            return (AssemblyDescriptor)GetParent(descripter.Parent);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public static DescripterNode Find(this AssemblyDescriptor root, Func<DescripterNode, bool> predicate)
        {
            if (root.Children == null)
            {
                return null;
            }
            var result = root.Children.Where(predicate).FirstOrDefault();
            if (result != null)
            {
                return result;
            }
            return GetResult(root.Children, predicate);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="children"></param>
        /// <returns></returns>
        private static DescripterNode GetResult(List<DescripterNode> children, Func<DescripterNode, bool> predicate)
        {
            foreach (var child in children)
            {
                if (child.Children != null)
                {
                    var result = child.Children.Where(predicate).FirstOrDefault();
                    if (result != null)
                    {
                        return result;
                    }
                    else
                    {
                        var result2 = GetResult(child.Children, predicate);
                        if (result2 != null)
                        {
                            return result2;
                        }
                    }
                }
            }
            return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="root"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static List<DescripterNode> FindList(this AssemblyDescriptor root, Func<DescripterNode, bool> predicate)
        {
            if (root.Children == null)
            {
                return null;
            }
            var queryResultList = new List<DescripterNode>();
            SetResultList(root.Children, predicate, queryResultList);
            return queryResultList;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="children"></param>
        /// <param name="predicate"></param>
        /// <param name="queryResultList"></param>
        private static void SetResultList(List<DescripterNode> children, Func<DescripterNode, bool> predicate, List<DescripterNode> queryResultList)
        {
            foreach (var child in children)
            {
                if (child.Children != null)
                {
                    var results = child.Children.Where(predicate);
                    if (results != null)
                    {
                        queryResultList.AddRange(results);
                    }
                    SetResultList(child.Children, predicate, queryResultList);
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static DescripterNode GetParent(DescripterNode descripter)
        {
            if (descripter.GetType() == typeof(AssemblyDescriptor))
            {
                return (AssemblyDescriptor)descripter;
            }
            return GetParent(descripter.Parent);
        }
    }
}
