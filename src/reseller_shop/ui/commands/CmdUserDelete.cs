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
    public class CmdUserDelete : ICommand
    {
        private int user_id;
        private ControllerManager manager;
        public CmdUserDelete(int user_id, ControllerManager manager)
        {
            this.user_id = user_id;
            this.manager = manager;
        }
        public void exec()
        {
            manager.UserController.Delete(user_id);
        }
    }
}
