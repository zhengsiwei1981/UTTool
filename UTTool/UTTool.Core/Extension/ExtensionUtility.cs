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
        //public static string GetGenericType(Type type)
        //{ }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string GetDefaultValue<T>()
        {
            if (typeof(T).IsValueType || typeof(T) == typeof(string) || (typeof(T).IsGenericType && typeof(T).GetGenericTypeDefinition().IsAssignableFrom(typeof(Nullable<>))))
            {
                return StringFormat(typeof(T));
            }
            return "null";
        }
        private static string StringFormat(Type realType)
        {
            //是nullable类型
            if (realType.IsGenericType)
            {
                realType = realType.GetGenericArguments()[0];
            }
            if (realType != null)
            {
                if (realType == typeof(bool))
                {
                    return default(bool).ToString().ToLower();
                }
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
                    //return $"\"{DateTime.Now.ToShortDateString()}\"";
                    return "DateTime.Now";
                }
                if (realType == typeof(Guid))
                {
                    return $"Guid.NewGuid()";
                }
                if (realType == typeof(string))
                {
                    return "\"\"";
                }
                if (realType.IsEnum)
                {
                    var field = realType.GetFields().Where(f => f.FieldType == realType).FirstOrDefault();
                    if (field != null)
                    {
                        return $"{realType.Name}.{field.Name}";
                    }
                }
            }
            return "null";
        }
    }
}
