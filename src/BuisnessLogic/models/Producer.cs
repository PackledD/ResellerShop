using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLogic.models
{
    public class Producer
    {
        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public Producer(int id, string name)
        {
            this.id = id;
            this.name = name;
        }

        public override string ToString()
        {
            return string.Format("{0}, '{1}'", id, name);
        }
    }
}
