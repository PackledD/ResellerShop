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
    public class CmdRegister : ICommand
    {
        private User user;
        private string hash;
        private User res = null;
        public User Result { get { return res; } }
        private ControllerManager manager;
        public CmdRegister(User user, string hash, ControllerManager manager)
        {
            this.user = user;
            this.hash = hash;
            this.manager = manager;
        }
        public void exec()
        {
            res = manager.UserController.Register(user, hash);
        }
    }
}
