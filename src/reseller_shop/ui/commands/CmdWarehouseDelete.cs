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
    public class CmdWarehouseDelete : ICommand
    {
        private int wh_id;
        private ControllerManager manager;
        public CmdWarehouseDelete(int wh_id, ControllerManager manager)
        {
            this.wh_id = wh_id;
            this.manager = manager;
        }
        public void exec()
        {
            manager.WarehouseController.Delete(wh_id);
        }
    }
}
