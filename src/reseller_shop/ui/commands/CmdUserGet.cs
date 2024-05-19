using BuisnessLogic.models;
using General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResellerShop.ui.commands
{
    public class CmdUserGet : ICommand
    {
        private int user_id;
        private User res = null;
        public User Result { get { return res; } }
        private ControllerManager manager;
        public CmdUserGet(int user_id, ControllerManager manager)
        {
            this.user_id = user_id;
            this.manager = manager;
        }
        public void exec()
        {
            res = manager.UserController.Get(user_id);
        }
    }
}
