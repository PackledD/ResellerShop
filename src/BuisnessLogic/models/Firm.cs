using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLogic.models
{
    public class Firm
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
        private string email;
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
            }
        }
        private string phone;
        public string Phone
        {
            get { return phone; }
            set
            {
                phone = value;
            }
        }
        private string physAddr;
        public string PhysAddr
        {
            get { return physAddr; }
            set
            {
                physAddr = value;
            }
        }
        private string lawAddr;
        public string LawAddr
        {
            get { return lawAddr; }
            set
            {
                lawAddr = value;
            }
        }

        public Firm(int id, string name, string email, string phone, string physAddr, string lawAddr)
        {
            this.id = id;
            this.name = name;
            this.email = email;
            this.phone = phone;
            this.physAddr = physAddr;
            this.lawAddr = lawAddr;
        }

        public override string ToString()
        {
            return string.Format("{0}, '{1}', '{2}', '{3}', '{4}', '{5}'", id, name, phone, email, physAddr, lawAddr);
        }
    }
}
