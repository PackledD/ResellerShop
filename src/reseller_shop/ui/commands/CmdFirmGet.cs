using BuisnessLogic.models;
using General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResellerShop.ui.commands
{
    public class CmdFirmGet : ICommand
    {
        private int firm_id;
        private Firm res = null;
        public Firm Result { get { return res; } }
        private ControllerManager manager;
        public CmdFirmGet(int firm_id, ControllerManager manager)
        {
            this.firm_id = firm_id;
            this.manager = manager;
        }
        public void exec()
        {
            res = manager.FirmController.Get(firm_id);
        }
    }
}
