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
    public class CmdProductGet : ICommand
    {
        private int prod_id;
        private Product res = null;
        public Product Result { get { return res; } }
        private ControllerManager manager;
        public CmdProductGet(int prod_id, ControllerManager manager)
        {
            this.prod_id = prod_id;
            this.manager = manager;
        }
        public void exec()
        {
            res = manager.ProductController.Get(prod_id);
        }
    }
}
