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
    public class CmdContractDelete : ICommand
    {
        private int contract_id;
        private ControllerManager manager;
        public CmdContractDelete(int contract_id, ControllerManager manager)
        {
            this.contract_id = contract_id;
            this.manager = manager;
        }
        public void exec()
        {
            manager.ContractController.Delete(contract_id);
        }
    }
}
