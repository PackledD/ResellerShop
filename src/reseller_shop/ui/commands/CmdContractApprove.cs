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
    public class CmdContractApprove : ICommand
    {
        private Contract contract;
        private User manager;
        private ControllerManager ctrl_manager;
        public CmdContractApprove(Contract contract, User manager, ControllerManager ctrl_manager)
        {
            this.contract = contract;
            this.manager = manager;
            this.ctrl_manager = ctrl_manager;
        }
        public void exec()
        {
            ctrl_manager.ContractController.Approve(contract, manager);
        }
    }
}
