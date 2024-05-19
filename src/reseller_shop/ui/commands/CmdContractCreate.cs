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
    public class CmdContractCreate : ICommand
    {
        private Contract contract;
        private ControllerManager manager;
        public CmdContractCreate(Contract contract, ControllerManager manager)
        {
            this.contract = contract;
            this.manager = manager;
        }
        public void exec()
        {
            manager.ContractController.Create(contract);
        }
    }
}
