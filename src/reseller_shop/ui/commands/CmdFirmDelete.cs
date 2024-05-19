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
    public class CmdFirmDelete : ICommand
    {
        private int firm_id;
        private ControllerManager manager;
        public CmdFirmDelete(int firm_id, ControllerManager manager)
        {
            this.firm_id = firm_id;
            this.manager = manager;
        }
        public void exec()
        {
            manager.FirmController.Delete(firm_id);
        }
    }
}
