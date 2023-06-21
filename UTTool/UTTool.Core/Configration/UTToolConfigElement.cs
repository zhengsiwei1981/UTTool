using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTTool.Core.Configration
{
    internal class UTToolConfigElement : ConfigurationElement
    {
        public UTToolConfigElement(string name, string map)
        {
            this.Name = name;
            this.Map = map;
        }

        public UTToolConfigElement()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        [ConfigurationProperty("name", IsRequired = true, IsKey = true)]
        public string Name {
            get
            {
                return (string)this["name"];
            }
            set
            {
                this["name"] = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [ConfigurationProperty("map", IsRequired = true)]
        public string Map {
            get
            {
                return (string)this["map"];
            }
            set
            {
                this["map"] = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [ConfigurationProperty("preInstall", IsRequired = true)]
        public bool PreInstall {
            get
            {
                return (bool)this["preInstall"];
            }
            set
            {
                this["preInstall"] = value;
            }
        }
    }
}
