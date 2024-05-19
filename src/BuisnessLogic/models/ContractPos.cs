using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLogic.models
{
    public class ContractPos
    {
        private Contract contract;
        public Contract Contract
        {
            get { return contract; }
        }
        private Product product;
        public Product Product
        {
            get { return product; }
        }
        private Warehouse warehouse;
        public Warehouse Warehouse
        {
            get { return warehouse; }
        }
        private int amount;
        public int Amount
        {
            get { return amount; }
            set
            {
                amount = value;
            }
        }

        public ContractPos(Contract contract, Warehouse warehouse, Product product, int amount)
        {
            this.contract = contract;
            this.warehouse = warehouse;
            this.product = product;
            this.amount = amount;
        }

        public override string ToString()
        {
            return string.Format("{0}, {1}, {2}, {3}", contract?.Id, product?.Id, warehouse?.Id, amount);
        }
    }
}
