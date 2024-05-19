using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLogic.models
{
    public class Product
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
        private Category category;
        public Category Category
        {
            get { return category; }
            set { category = value; }
        }
        private Firm distributor;
        public Firm Distributor
        {
            get { return distributor; }
            set { distributor = value; }
        }
        private int cost;
        public int Cost
        {
            get { return cost; }
            set { cost = value; }
        }
        private Producer producer;
        public Producer Producer
        {
            get { return producer; }
            set { producer = value; }
        }

        public Product(int id, string name, Category category, Firm distributor, int cost, Producer producer)
        {
            this.id = id;
            this.name = name;
            this.category = category;
            this.distributor = distributor;
            this.cost = cost;
            this.producer = producer;
        }

        public override string ToString()
        {
            return string.Format("{0}, '{1}', {2}, {3}, {4}, '{5}'", id, name, category?.Id, distributor?.Id, cost, producer?.Id);
        }
    }
}
