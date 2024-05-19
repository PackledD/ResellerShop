using General.config.stored;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General.utils
{
    public class JsonReader
    {
        public static T ReadJson<T>(StreamReader f)
        {
            string json = f.ReadToEnd();
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
