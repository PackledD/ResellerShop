using BuisnessLogic.models;
using General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResellerShop.ui.commands
{
    public class CmdFirmUpdate : ICommand
    {
        private Firm firm;
        private ControllerManager manager;
        public CmdFirmUpdate(Firm firm, ControllerManager manager)
        {
            this.firm = firm;
            this.manager = manager;
        }
        public void exec()
        {
            manager.FirmController.Update(firm);
        }
    }
}
