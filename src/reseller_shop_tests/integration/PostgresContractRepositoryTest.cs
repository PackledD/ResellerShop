using DataAccess.repository;
using BuisnessLogic.enums;
using Exceptions;
using Exceptions.logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using General.config;

namespace reseller_shop_tests.integration
{
    [TestClass]
    public class PostgresContractRepositoryTest
    {
        IContractRepository rep;
        public PostgresContractRepositoryTest()
        {
            rep = new PostgresContractRepository();
        }

        [TestMethod]
        public void ContractApproveAndSign()
        {
            // Подпись контракта всеми представителями
            PostgresConfigCreator.Create("resell_data", "localhost", new("admin", "pg_admin"));
            IFirmRepository firm_rep = new PostgresFirmRepository();
            Firm firm1 = new(0, "test1", "test1@mail.ru", "1234", "test1", "test1");
            firm_rep.Add(firm1);
            Firm firm0 = firm_rep.Get(0);
            IUserRepository user_rep = new PostgresUserRepository();
            User u1 = new(0, "test1", firm0, "test1", "1234", UsersEnum.Manager);
            User u2 = new(0, "test2", firm0, "test2", "1234", UsersEnum.Director);
            User u3 = new(0, "test3", firm1, "test3", "1234", UsersEnum.Manager);
            User u4 = new(0, "test4", firm1, "test4", "1234", UsersEnum.Director);
            user_rep.Add(u1);
            user_rep.Add(u2);
            user_rep.Add(u3);
            user_rep.Add(u4);
            Contract contract = new Contract(0, firm_rep.Get(firm1.Id), null, null, null, null, DateTime.UtcNow, DateTime.Now, "./test_contract.pdf");
            rep.Add(contract);
            var lst0 = firm_rep.GetStaff(firm_rep.Get(firm0.Id)).Where(x => x.Id == u1.Id || x.Id == u2.Id).ToList();
            var lst1 = firm_rep.GetStaff(firm_rep.Get(firm1.Id)).Where(x => x.Id == u3.Id || x.Id == u4.Id).ToList();
            User manager1 = lst0[0];
            User director1 = lst0[1];
            User manager2 = lst1[0];
            User director2 = lst1[1];
            try
            {
                rep.Approve(contract, manager1);
                rep.Approve(contract, manager2);
                rep.Sign(contract, director2);
                rep.Sign(contract, director1);
            }
            catch (Exception)
            {
                rep.Delete(contract.Id);
                user_rep.Delete(u1.Id);
                user_rep.Delete(u2.Id);
                user_rep.Delete(u3.Id);
                user_rep.Delete(u4.Id);
                firm_rep.Delete(firm1.Id);
                throw;
            }
            rep.Delete(contract.Id);
            user_rep.Delete(u1.Id);
            user_rep.Delete(u2.Id);
            user_rep.Delete(u3.Id);
            user_rep.Delete(u4.Id);
            firm_rep.Delete(firm1.Id);
        }
    }
}
