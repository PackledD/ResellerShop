using BuisnessLogic.models;
using General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResellerShop.ui.commands
{
    public class CmdUserUpdate : ICommand
    {
        private User user;
        private ControllerManager manager;
        public CmdUserUpdate(User user, ControllerManager manager)
        {
            this.user = user;
            this.manager = manager;
        }
        public void exec()
        {
            manager.UserController.Update(user);
        }
    }
}
