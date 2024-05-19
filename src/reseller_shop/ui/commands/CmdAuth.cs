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
    public class CmdAuth : ICommand
    {
        private string login;
        private string hash;
        private User res = null;
        public User Result { get { return res; } }
        private ControllerManager manager;
        public CmdAuth(string login, string hash, ControllerManager manager)
        {
            this.login = login;
            this.hash = hash;
            this.manager = manager;
        }
        public void exec()
        {
            res = manager.UserController.Auth(login, hash);
        }
    }
}
