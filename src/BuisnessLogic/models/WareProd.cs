using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLogic.models
{
    public class WareProd
    {
        private Warehouse warehouse;
        public Warehouse Warehouse
        {
            get { return warehouse; }
        }
        private Product product;
        public Product Product
        {
            get { return product; }
        }
        private int amount;
        public int Amount
        {
            get { return amount; }
            set { amount = value; }
        }

        public WareProd(Warehouse warehouse, Product product, int amount)
        {
            this.warehouse = warehouse;
            this.product = product;
            this.amount = amount;
        }

        public override string ToString()
        {
            return string.Format("{0}, {1}, {2}", product?.Id, warehouse?.Id, amount);
        }
    }
}
