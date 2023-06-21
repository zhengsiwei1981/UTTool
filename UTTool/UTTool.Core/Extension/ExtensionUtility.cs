using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace UTTool.Core.Extension
{
    internal static class ExtensionUtility
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetLimitName(this string str)
        {
            if (str == null)
            {
                return "";
            }
            var arry = str.Split(',');
            if (arry == null || arry.Length == 0)
            {
                return "";
            }
            return arry[0];
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetFirstLowerString(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return "";
            }
            return str.First().ToString().ToLower() + str.Substring(1);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        public static string GetPropertyDefaultValue(this object obj, Type type)
        {
            var str = typeof(ExtensionUtility).GetMethod("GetDefaultValue").MakeGenericMethod(new Type[] { type }).Invoke(null, null);
            return str.ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string GetDefaultValue<T>()
        {
            Type realType = null;
            if (typeof(T).IsGenericType && typeof(T).GetGenericTypeDefinition().IsAssignableFrom(typeof(Nullable<>)))
            {
                realType = typeof(T).GetGenericArguments()[0];
            }
            var val = default(T);
            if (val == null)
            {
                if (typeof(T) == typeof(string))
                {
                    return "\"\"";
                }
                else
                {
                    if (realType != null)
                    {
                        if (realType == typeof(int))
                        {
                            return default(int).ToString();
                        }
                        if (realType == typeof(decimal))
                        {
                            return default(decimal).ToString();
                        }
                        if (realType == typeof(float))
                        {
                            return default(float).ToString();
                        }
                        if (realType == typeof(DateTime))
                        {
                            return $"\"{DateTime.Now.ToShortDateString()}\"";
                        }
                    }
                    return "null";
                }
            }
            return val.ToString();
        }
    }
}
