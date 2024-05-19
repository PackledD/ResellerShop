using Exceptions;
using General.config.stored;
using General.db_connect;
using General.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General.config
{
    public class ConfigLoader
    {
        public static StoredDbData Load(string path, string cfg_name)
        {
            StreamReader f = new(path);
            if (f != null)
            {
                var cfg_data = JsonReader.ReadJson<StoredConfig>(f);
                foreach (var item in cfg_data.DbData)
                {
                    if (item.Type == cfg_name)
                    {
                        return item;
                    }
                }
                throw new ConfigException("Unknown DB type");
            }
            else
            {
                throw new ConfigException("Can't load config file");
            }
        }
    }
}
