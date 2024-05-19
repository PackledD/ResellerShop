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
    public class CmdContractUpdate : ICommand
    {
        private Contract contract;
        private ControllerManager manager;
        public CmdContractUpdate(Contract contract, ControllerManager manager)
        {
            this.contract = contract;
            this.manager = manager;
        }
        public void exec()
        {
            manager.ContractController.Update(contract);
        }
    }
}
