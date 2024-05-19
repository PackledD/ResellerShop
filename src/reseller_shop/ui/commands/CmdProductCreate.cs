using BuisnessLogic.models;
using General;
using ResellerShop.ui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResellerShop.ui.commands
{
    public class CmdProductCreate : ICommand
    {
        private Product prod;
        private ControllerManager manager;
        public CmdProductCreate(Product prod, ControllerManager manager)
        {
            this.prod = prod;
            this.manager = manager;
        }
        public void exec()
        {
            manager.ProductController.Create(prod);
        }
    }
}
