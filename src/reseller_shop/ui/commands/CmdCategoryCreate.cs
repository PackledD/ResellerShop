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
    public class CmdCategoryCreate : ICommand
    {
        private Category cat;
        private ControllerManager manager;
        public CmdCategoryCreate(Category cat, ControllerManager manager)
        {
            this.cat = cat;
            this.manager = manager;
        }
        public void exec()
        {
            manager.CategoryController.Create(cat);
        }
    }
}
