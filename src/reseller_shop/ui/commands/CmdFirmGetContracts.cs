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
    public class CmdFirmGetContracts : ICommand
    {
        private Firm firm;
        private List<Contract> res = null;
        public List<Contract> Result { get { return res; } }
        private ControllerManager manager;
        public CmdFirmGetContracts(Firm firm, ControllerManager manager)
        {
            this.firm = firm;
            this.manager = manager;
        }
        public void exec()
        {
            res = manager.FirmController.GetContracts(firm);
        }
    }
}
