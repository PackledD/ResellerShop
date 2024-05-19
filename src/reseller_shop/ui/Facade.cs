using General.config;
using BuisnessLogic.interfaces.repository;
using BuisnessLogic.controllers_inner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResellerShop.ui
{
    public class Facade
    {
        public Facade()
        {

        }

        public void exec(ICommand cmd)
        {
            cmd.exec();
        }
    }
}
