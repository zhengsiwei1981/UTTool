using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace UTTool.Core.Configration
{
    internal class UTToolConfigElementCollection : System.Configuration.ConfigurationElementCollection
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override UTToolConfigElement CreateNewElement()
        {

            return new UTToolConfigElement();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((UTToolConfigElement)element).Name;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        public void Add(UTToolConfigElement url)
        {
            BaseAdd(url);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public UTToolConfigElement? GetElement(string name)
        {
            return BaseGet(name) as UTToolConfigElement;
        }
    }
}
