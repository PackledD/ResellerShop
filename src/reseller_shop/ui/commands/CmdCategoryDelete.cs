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
    public class CmdCategoryDelete : ICommand
    {
        private int cat_id;
        private ControllerManager manager;
        public CmdCategoryDelete(int cat_id, ControllerManager manager)
        {
            this.cat_id = cat_id;
            this.manager = manager;
        }
        public void exec()
        {
            manager.CategoryController.Delete(cat_id);
        }
    }
}
