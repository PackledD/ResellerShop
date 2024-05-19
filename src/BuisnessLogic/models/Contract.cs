using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLogic.models
{
    public class Contract
    {
        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        private Firm firm;
        public Firm Firm
        {
            get { return firm; }
            set { firm = value; }
        }
        private User director1;
        public User Director1
        {
            get { return director1; }
            set
            {
                director1 = value;
            }
        }
        private User director2;
        public User Director2
        {
            get { return director2; }
            set
            {
                director2 = value;
            }
        }
        private User manager1;
        public User Manager1
        {
            get { return manager1; }
            set
            {
                manager1 = value;
            }
        }
        private User manager2;
        public User Manager2
        {
            get { return manager2; }
            set
            {
                manager2 = value;
            }
        }
        private DateTime conclusionDate;
        public DateTime ConclusionDate
        {
            get { return conclusionDate; }
            set
            {
                conclusionDate = value;
            }
        }
        private DateTime expirationDate;
        public DateTime ExpirationDate
        {
            get { return expirationDate; }
            set
            {
                expirationDate = value;
            }
        }
        private string document;
        public string Document
        {
            get { return document; }
            set
            {
                document = value;
            }
        }

        public Contract(int id, Firm firm, User director1, User director2, User manager1, User manager2, DateTime conclusionDate, DateTime expirationDate, string document)
        {
            this.id = id;
            this.firm = firm;
            this.director1 = director1;
            this.director2 = director2;
            this.manager1 = manager1;
            this.manager2 = manager2;
            this.conclusionDate = conclusionDate;
            this.expirationDate = expirationDate;
            this.document = document;
        }

        public bool IsExpired(DateTime date)
        {
            return date > expirationDate;
        }

        public bool IsConcluded(DateTime date)
        {
            return date >= conclusionDate;
        }

        public bool WithFirm(Firm firm)
        {
            return firm.Id == this.firm.Id || firm.Id == 0; // 0 is Id of our firm, contract with it is always valid
        }

        public bool CanApprove(User manager)
        {
            return manager.Firm.Id == 0 ? manager1 == null : manager2 == null;
        }

        public void Approve(User manager)
        {
            if (manager.Firm.Id == 0)
            {
                manager1 = manager;
            }
            else
            {
                manager2 = manager;
            }
        }

        public bool CanSign(User director)
        {
            return (director.Firm.Id == 0 ? director1 == null : director2 == null) &&
                manager1 != null && manager2 != null;
        }

        public void Sign(User director)
        {
            if (director.Firm.Id == 0)
            {
                director1 = director;
            }
            else
            {
                director2 = director;
            }
        }

        public override string ToString()
        {
            return string.Format("{0}, {1}, {2}, {3}, {4}, {5}, '{6}', '{7}', '{8}'", id, firm?.Id, director1?.Id, director2?.Id, manager1?.Id,
                                                       manager2?.Id, conclusionDate.ToString(), expirationDate.ToString(), document);
        }

        public string ToShortString()
        {
            return string.Format("{0}, {1}, '{2}', '{3}', '{4}'", id, firm?.Id,
                                                       conclusionDate.ToString(), expirationDate.ToString(), document);
        }

        public bool IsComplete()
        {
            return director1 != null && director2 != null && manager1 != null && manager2 != null;
        }
    }
}
