using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General.config.stored
{
    public class StoredDbData
    {
        public string Type { get; set; } = string.Empty;
        public string DbHost { get; set; } = string.Empty;
        public string DbName { get; set; } = string.Empty;
        public string UserLogin { get; set; } = string.Empty;
        public string UserPassword { get; set; } = string.Empty;

    }
}
