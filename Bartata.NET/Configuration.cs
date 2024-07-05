using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Bartata.NET
{
    public class Configuration : ConfigurationSection
    {
        public static Configuration Config
        {
            get
            {
                return ConfigurationManager.GetSection("pcgClientConfig") as Configuration;
            }
        }

        [ConfigurationProperty("loginUrl", IsRequired = false, DefaultValue = "~/")]
        public string LoginUrl
        {
            get
            {
                return base["loginUrl"] as string;
            }
        }

        [ConfigurationProperty("expireThreshold", IsRequired = false, DefaultValue = 5)]
        public int ExpireThreshold
        {
            get
            {
                return Convert.ToInt32(base["expireThreshold"]);
            }
        }

        [ConfigurationProperty("urlFilterPattern", IsRequired = false, DefaultValue = "")]
        public string UrlFilterPattern
        {
            get
            {
                return base["urlFilterPattern"] as string;
            }
        }
    }

}
