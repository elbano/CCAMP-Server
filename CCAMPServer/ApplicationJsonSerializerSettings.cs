using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CCAMPServer
{
    public class ApplicationJsonSerializerSettings
    {
        private static JsonSerializerSettings m_settings;
        private static JsonSerializerSettings m_settingsEnumAsString;

        public static JsonSerializerSettings Settings
        {
            get
            {
                if (m_settings == null)
                {
                    m_settings = new JsonSerializerSettings()
                    {
                        MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                        Formatting = Formatting.Indented
                    };
                }
                return m_settings;
            }
        }

        public static JsonSerializerSettings SettingsDBSeader
        {
            get
            {
                if (m_settingsEnumAsString == null)
                {
                    m_settingsEnumAsString = new JsonSerializerSettings()
                    {
                        MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                        Formatting = Formatting.Indented
                    };
                    m_settingsEnumAsString.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
                }
                return m_settings;
            }
        }
    }
}
