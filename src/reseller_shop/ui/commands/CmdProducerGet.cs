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
    public class CmdProducerGet : ICommand
    {
        private int prod_id;
        private Producer res = null;
        public Producer Result { get { return res; } }
        private ControllerManager manager;
        public CmdProducerGet(int prod_id, ControllerManager manager)
        {
            this.prod_id = prod_id;
            this.manager = manager;
        }
        public void exec()
        {
            res = manager.ProducerController.Get(prod_id);
        }
    }
}
