using BuisnessLogic.models;
using General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResellerShop.ui.commands
{
    public class CmdProductGetAll : ICommand
    {
        private List<Product> res = null;
        public List<Product> Result { get { return res; } }
        private ControllerManager manager;
        public CmdProductGetAll(ControllerManager manager)
        {
            this.manager = manager;
        }
        public void exec()
        {
            res = manager.ProductController.GetAll();
        }
    }
}
