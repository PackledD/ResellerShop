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
    public class CmdWarehouseUpdate : ICommand
    {
        private Warehouse wh;
        private ControllerManager manager;
        public CmdWarehouseUpdate(Warehouse wh, ControllerManager manager)
        {
            this.wh = wh;
            this.manager = manager;
        }
        public void exec()
        {
            manager.WarehouseController.Update(wh);
        }
    }
}
