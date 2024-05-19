using BuisnessLogic.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLogic.models
{
    public class User
    {

        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        private string fullname;
        public string FullName
        {
            get { return fullname; }
            set { fullname = value; }
        }
        private Firm firm;
        public Firm Firm
        {
            get { return firm; }
            set
            {
                firm = value;
            }
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
        private UsersEnum userKind;
        public UsersEnum UserKind
        {
            get { return userKind; }
            set
            {
                userKind = value;
            }
        }

        public User(int id, string fullname, Firm firm, string email, string phone, UsersEnum userKind)
        {
            this.id = id;
            this.fullname = fullname;
            this.firm = firm;
            this.email = email;
            this.phone = phone;
            this.userKind = userKind;
        }

        public bool IsAdmin()
        {
            return userKind == UsersEnum.Admin;
        }

        public bool IsManager()
        {
            return userKind == UsersEnum.Manager;
        }

        public bool IsDirector()
        {
            return userKind == UsersEnum.Director;
        }

        public override string ToString()
        {
            return string.Format("{0}, '{1}', {2}, '{3}', '{4}', {5}", id, fullname, firm.Id, email, phone, (int)userKind);
        }
    }
}
