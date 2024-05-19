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
    public class CmdWarehouseGet : ICommand
    {
        private int wh_id;
        private Warehouse res = null;
        public Warehouse Result { get { return res; } }
        private ControllerManager manager;
        public CmdWarehouseGet(int wh_id, ControllerManager manager)
        {
            this.wh_id = wh_id;
            this.manager = manager;
        }
        public void exec()
        {
            res = manager.WarehouseController.Get(wh_id);
        }
    }
}
