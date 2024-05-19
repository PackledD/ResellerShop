using BuisnessLogic.models;
using General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResellerShop.ui.commands
{
    public class CmdContractSign : ICommand
    {
        private Contract contract;
        private User director;
        private ControllerManager manager;
        public CmdContractSign(Contract contract, User director, ControllerManager manager)
        {
            this.contract = contract;
            this.director = director;
            this.manager = manager;
        }
        public void exec()
        {
            manager.ContractController.Sign(contract, director);
        }
    }
}
