using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTTool.Core.Extension
{
    public static class NameSpaceExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nameSpace"></param>
        /// <returns></returns>
        public static string GetLastSpace(this string nameSpace)
        {
            if (!nameSpace.Contains("."))
            {
                return nameSpace;
            }
            return nameSpace.Split(new char[] { '.' }).LastOrDefault()!;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nameSpace"></param>
        /// <returns></returns>
        public static string GetFirstSpace(this string nameSpace)
        {
            if (!nameSpace.Contains("."))
            {
                return nameSpace;
            }
            return nameSpace.Split(new char[] { '.' }).FirstOrDefault()!;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nameSpace"></param>
        /// <returns></returns>
        public static string[] GetSplitSpaces(this string nameSpace)
        {
            return nameSpace.Split(new char[] { '.' });
        }
    }
}
