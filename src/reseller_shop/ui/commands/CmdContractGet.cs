using BuisnessLogic.models;
using General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResellerShop.ui.commands
{
    public class CmdContractGet : ICommand
    {
        private int contract_id;
        private Contract res = null;
        public Contract Result { get { return res; } }
        private ControllerManager manager;
        public CmdContractGet(int contract_id, ControllerManager manager)
        {
            this.contract_id = contract_id;
            this.manager = manager;
        }
        public void exec()
        {
            res = manager.ContractController.Get(contract_id);
        }
    }
}
