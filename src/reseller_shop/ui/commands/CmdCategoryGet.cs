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
    public class CmdCategoryGet : ICommand
    {
        private int cat_id;
        private Category res = null;
        public Category Result { get { return res; } }
        private ControllerManager manager;
        public CmdCategoryGet(int cat_id, ControllerManager manager)
        {
            this.cat_id = cat_id;
            this.manager = manager;
        }
        public void exec()
        {
            res = manager.CategoryController.Get(cat_id);
        }
    }
}
