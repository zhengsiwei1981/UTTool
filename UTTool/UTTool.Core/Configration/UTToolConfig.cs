using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTTool.Core.Configration
{
    internal class UTToolConfig : System.Configuration.ConfigurationSection
    {
        [ConfigurationProperty("collection", IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(UTToolConfigElementCollection),
      AddItemName = "add",
      ClearItemsName = "clear",
      RemoveItemName = "remove")]
        public UTToolConfigElementCollection Collection {
            get
            {
                UTToolConfigElementCollection collection =
                    (UTToolConfigElementCollection)base["collection"];

                return collection;
            }

            set
            {
                UTToolConfigElementCollection collection = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public UTToolConfig()
        {

        }
    }
}
