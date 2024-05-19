using General.config;
using ResellerShop.enums;
using BuisnessLogic.enums;
using Exceptions;
using BuisnessLogic.models;
using ResellerShop.ui.commands;
using General.utils;
using General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ResellerShop.ui
{
    public class ConsoleApp
    {
        private ControllerManager mng;
        private Facade facade;
        private User? curUser;
        private bool workFlag = true;

        private ConsoleUiStateEnum state;
        public ConsoleApp(IConfig cfg)
        {
            mng = new(cfg);
            facade = new();
            state = ConsoleUiStateEnum.Main;
        }

        public void Mainloop()
        {
            while (workFlag)
            {
                try
                {
                    PrintMenu();
                    Console.Write("What to do? ");
                    int action = Convert.ToInt32(Console.ReadLine());
                    Apply(action);
                    Console.WriteLine("\n");
                }
                catch (LogicException) { }
                catch (Exception ex)
                {
                    Console.WriteLine("Unknown exception!");
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        private void PrintMenu()
        {
            switch (state)
            {
                case ConsoleUiStateEnum.Main:
                    Console.Write(
                        "0. Exit\n" +
                        "1. Login\n" +
                        "2. Register\n" +
                        "3. Logout\n" +
                        "4. Open catalog\n" +
                        "5. Open contracts\n" +
                        "6. Open admin panel\n" +
                        "7. Open personal page\n"
                    );
                    break;
                case ConsoleUiStateEnum.Catalog:
                    Console.Write(
                        "0. Back to main\n" +
                        "1. View list of products\n"
                    );
                    break;
                case ConsoleUiStateEnum.Contracts:
                    Console.Write(
                        "0. Back to main\n" +
                        "1. View list of contracts\n" +
                        "2. Create new contract\n" +
                        "3. Approve contract\n" +
                        "4. Sign contract\n"
                    );
                    break;
                case ConsoleUiStateEnum.Admin:
                    Console.Write(
                        "0. Back to main\n" +
                        "1. Control firms\n" +
                        "2. Control warehouses\n" +
                        "3. Control products\n" +
                        "4. Control users\n" +
                        "5. Control categories\n"
                    );
                    break;
                case ConsoleUiStateEnum.Personal:
                    Console.Write(
                        "0. Back to main\n" +
                        "1. Update personal data\n"
                    );
                    break;
                default:
                    Console.WriteLine("Impossible");
                    break;
            }
        }

        private void Apply(int action)
        {
            switch (state)
            {
                case ConsoleUiStateEnum.Main:
                    switch (action)
                    {
                        case 0:
                            workFlag = false; break;
                        case 1:
                            Login(); break;
                        case 2:
                            Register(); break;
                        case 3:
                            Logout(); break;
                        case 4:
                            OpenCatalogPage(); break;
                        case 5:
                            OpenContractPage(); break;
                        case 6:
                            OpenAdminPage(); break;
                        case 7:
                            OpenPersonalPage(); break;
                        default:
                            Console.WriteLine("Unknown option"); break;
                    }
                    break;
                case ConsoleUiStateEnum.Catalog:
                    switch (action)
                    {
                        case 0:
                            OpenMainPage(); break;
                        case 1:
                            ViewProductList(); break;
                        default:
                            Console.WriteLine("Unknown option"); break;
                    }
                    break;
                case ConsoleUiStateEnum.Contracts:
                    switch (action)
                    {
                        case 0:
                            OpenMainPage(); break;
                        case 1:
                            ViewContractList(); break;
                        case 2:
                            AddContract(); break;
                        case 3:
                            ApproveContract(); break;
                        case 4:
                            SignContract(); break;
                        default:
                            Console.WriteLine("Unknown option"); break;
                    }
                    break;
                case ConsoleUiStateEnum.Admin:
                    switch (action)
                    {
                        case 0:
                            OpenMainPage(); break;
                        case 1:
                            ControlFirm(); break;
                        case 2:
                            ControlWarehouse(); break;
                        case 3:
                            ControlProduct(); break;
                        case 4:
                            ControlUser(); break;
                        case 5:
                            ControlCategory(); break;
                        default:
                            Console.WriteLine("Unknown option"); break;
                    }
                    break;
                case ConsoleUiStateEnum.Personal:
                    switch (action)
                    {
                        case 0:
                            OpenMainPage(); break;
                        case 1:
                            UpdateUser(); break;
                        default:
                            Console.WriteLine("Unknown option"); break;
                    }
                    break;
                default:
                    Console.WriteLine("Impossible");
                    break;
            }
        }

        private void Login()
        {
            if (curUser != null)
            {
                Console.WriteLine("You are already login.");
                return;
            }
            Console.Write("Enter login: ");
            string? login = Console.ReadLine();
            Console.Write("Enter password: ");
            string? hash = Hasher.GetHash(Console.ReadLine());
            CmdAuth cmd = new(login, hash, mng);
            try
            {
                facade.exec(cmd);
                Console.WriteLine("Login successful.");
                curUser = cmd.Result;
            }
            catch (LogicException)
            {
                Console.WriteLine("Can't login.");
            }
        }

        private void Register()
        {
            if (curUser != null)
            {
                Console.WriteLine("You are already login.");
                return;
            }
            Console.Write("Enter your fullname: ");
            string? fullname = Console.ReadLine();
            Firm firm = SelectFirm();
            Console.Write("Enter your email: ");
            string? email = Console.ReadLine();
            Console.Write("Enter your phone: ");
            string? phone = Console.ReadLine();
            Console.Write("Enter password: ");
            string? hash = Hasher.GetHash(Console.ReadLine());
            UsersEnum kind = UsersEnum.NotSet;
            User user = new(-1, fullname, firm, email, phone, kind);
            CmdRegister cmd = new(user, hash, mng);
            try
            {
                facade.exec(cmd);
                Console.WriteLine("Register successful.");
                curUser = cmd.Result;
            }
            catch (LogicException)
            {
                Console.WriteLine("Can't register.");
            }
        }

        private void Logout()
        {
            if (curUser != null)
            {
                curUser = null;
                Console.WriteLine("Logout successful.");
            }
            else
            {
                Console.WriteLine("You are already logout.");
            }
        }

        private void OpenCatalogPage()
        {
            if (curUser != null)
            {
                state = ConsoleUiStateEnum.Catalog;
                Console.WriteLine("Catalog was opened.");
            }
            else
            {
                Console.WriteLine("Can't open catalog. Login firstly.");
            }
        }

        private void OpenContractPage()
        {
            if (curUser != null)
            {
                state = ConsoleUiStateEnum.Contracts;
                Console.WriteLine("Contract page was opened.");
            }
            else
            {
                Console.WriteLine("Can't open contract page. Login firstly.");
            }
        }

        private void OpenPersonalPage()
        {
            if (curUser != null)
            {
                state = ConsoleUiStateEnum.Personal;
                Console.WriteLine("Personal page was opened.");
            }
            else
            {
                Console.WriteLine("Can't open personal page. Login firstly.");
            }
        }

        private void OpenAdminPage()
        {
            if (curUser != null)
            {
                if (curUser.IsAdmin())
                {
                    state = ConsoleUiStateEnum.Admin;
                    Console.WriteLine("Admin page was opened.");
                }
                else
                {
                    Console.WriteLine("You aren't admin.");
                }
            }
            else
            {
                Console.WriteLine("Can't open admin page. Login firstly.");
            }
        }

        private void OpenMainPage()
        {
            state = ConsoleUiStateEnum.Main;
            Console.WriteLine("Main page was opened.");
        }

        private Contract SelectContract()
        {
            Console.Write("Enter contract id: ");
            int id = Convert.ToInt32(Console.ReadLine());
            CmdContractGet cmd = new(id, mng);
            try
            {
                facade.exec(cmd);
                Console.WriteLine("Get contract.");
            }
            catch (LogicException ex)
            {
                Console.WriteLine("Can't get contract with given id.");
                throw ex;
            }
            return cmd.Result;
        }

        private Firm SelectFirm()
        {
            Console.Write("Enter firm id: ");
            int id = Convert.ToInt32(Console.ReadLine());
            CmdFirmGet cmd = new(id, mng);
            try
            {
                facade.exec(cmd);
                Console.WriteLine("Get firm.");
            }
            catch (LogicException ex)
            {
                Console.WriteLine("Can't get firm with given id.");
                throw ex;
            }
            return cmd.Result;
        }

        private Product SelectProduct()
        {
            Console.Write("Enter product id: ");
            int id = Convert.ToInt32(Console.ReadLine());
            CmdProductGet cmd = new(id, mng);
            try
            {
                facade.exec(cmd);
                Console.WriteLine("Get product.");
            }
            catch (LogicException ex)
            {
                Console.WriteLine("Can't get product with given id.");
                throw ex;
            }
            return cmd.Result;
        }

        private Category SelectCategory()
        {
            Console.Write("Enter category id: ");
            int id = Convert.ToInt32(Console.ReadLine());
            CmdCategoryGet cmd = new(id, mng);
            try
            {
                facade.exec(cmd);
                Console.WriteLine("Get category.");
            }
            catch (LogicException ex)
            {
                Console.WriteLine("Can't get category with given id.");
                throw ex;
            }
            return cmd.Result;
        }

        private Producer SelectProducer()
        {
            Console.Write("Enter producer id: ");
            int id = Convert.ToInt32(Console.ReadLine());
            CmdProducerGet cmd = new(id, mng);
            try
            {
                facade.exec(cmd);
                Console.WriteLine("Get producer.");
            }
            catch (LogicException ex)
            {
                Console.WriteLine("Can't get producer with given id.");
                throw ex;
            }
            return cmd.Result;
        }

        private User SelectUser()
        {
            Console.Write("Enter user id: ");
            int id = Convert.ToInt32(Console.ReadLine());
            CmdUserGet cmd = new(id, mng);
            try
            {
                facade.exec(cmd);
                Console.WriteLine("Get user.");
            }
            catch (LogicException ex)
            {
                Console.WriteLine("Can't get user with given id.");
                throw ex;
            }
            return cmd.Result;
        }

        private Warehouse SelectWarehouse()
        {
            Console.Write("Enter warehouse id: ");
            int id = Convert.ToInt32(Console.ReadLine());
            CmdWarehouseGet cmd = new(id, mng);
            try
            {
                facade.exec(cmd);
                Console.WriteLine("Get warehouse.");
            }
            catch (LogicException ex)
            {
                Console.WriteLine("Can't get warehouse with given id.");
                throw ex;
            }
            return cmd.Result;
        }

        private void AddContract()
        {
            if (curUser == null)
            {
                Console.WriteLine("Firstly must login.");
                return;
            }
            Console.Write("Enter conclusion date: ");
            DateTime conclusion = Convert.ToDateTime(Console.ReadLine());
            Console.Write("Enter expiration date: ");
            DateTime expiration = Convert.ToDateTime(Console.ReadLine());
            Console.Write("Enter document name: ");
            string? document = Console.ReadLine();
            Contract contract = new(-1, curUser.Firm, null, null, null, null, conclusion, expiration, document);
            try
            {
                CmdContractCreate cmd = new(contract, mng);
                facade.exec(cmd);
            }
            catch (LogicException ex)
            {
                Console.WriteLine("Error while create contract.");
                return;
            }
            try
            {
                CmdContractApprove cmd = new(contract, curUser, mng);
                facade.exec(cmd);
                Console.WriteLine("Contract was successful created.");
            }
            catch (LogicException ex)
            {
                Console.WriteLine("Contract can be created only by manager.");
                CmdContractDelete cmd = new(contract.Id, mng);
                facade.exec(cmd);
            }
        }

        private void SignContract()
        {
            Contract contract = SelectContract();
            CmdContractSign cmd = new(contract, curUser, mng);
            try
            {
                facade.exec(cmd);
                Console.WriteLine("You successful sign a contract.");
            }
            catch (LogicException ex)
            {
                Console.WriteLine("You can't sign this contract.");
            }
        }

        private void ApproveContract()
        {
            Contract contract = SelectContract();
            CmdContractApprove cmd = new(contract, curUser, mng);
            try
            {
                facade.exec(cmd);
                Console.WriteLine("You successful approve a contract.");
            }
            catch (LogicException ex)
            {
                Console.WriteLine("You can't approve this contract.");
            }
        }

        private void AddFirm()
        {
            Console.Write("Enter firm name: ");
            string? name = Console.ReadLine();
            Console.Write("Enter firm phone: ");
            string? phone = Console.ReadLine();
            Console.Write("Enter firm email: ");
            string? email = Console.ReadLine();
            Console.Write("Enter firm physical address: ");
            string? phys_addr = Console.ReadLine();
            Console.Write("Enter firm legal address: ");
            string? law_addr = Console.ReadLine();
            Firm firm = new(-1, name, email, phone, phys_addr, law_addr);
            try
            {
                CmdFirmCreate cmd = new(firm, mng);
                facade.exec(cmd);
                Console.WriteLine("Firm was successful created.");
            }
            catch (LogicException ex)
            {
                Console.WriteLine("Error while create firm.");
            }
        }

        private void AddWarehouse()
        {
            Console.Write("Enter warehouse address: ");
            string? addr = Console.ReadLine();
            Warehouse wh = new(-1, addr);
            try
            {
                CmdWarehouseCreate cmd = new(wh, mng);
                facade.exec(cmd);
                Console.WriteLine("Warehouse was successful created.");
            }
            catch (LogicException ex)
            {
                Console.WriteLine("Error while create warehouse.");
            }
        }

        private void AddProduct()
        {
            Console.Write("Enter product name: ");
            string? name = Console.ReadLine();
            Category category = SelectCategory();
            Firm provider = state == ConsoleUiStateEnum.Admin ? SelectFirm() : curUser.Firm;
            Console.Write("Enter product cost: ");
            int cost = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter producer name: ");
            Producer producer = SelectProducer();
            Product prod = new(-1, name, category, provider, cost, producer);
            try
            {
                CmdProductCreate cmd = new(prod, mng);
                facade.exec(cmd);
                Console.WriteLine("Product was successful created.");
            }
            catch (LogicException ex)
            {
                Console.WriteLine("Error while create product.");
            }
        }

        private void AddUser()
        {
            Console.Write("Enter user fullname: ");
            string? name = Console.ReadLine();
            Firm firm = SelectFirm();
            Console.Write("Enter user phone: ");
            string? phone = Console.ReadLine();
            Console.Write("Enter user email: ");
            string? email = Console.ReadLine();
            Console.Write("Enter user kind (0 = manager, 1 = director, 2 = admin): ");
            UsersEnum kind = (UsersEnum)Convert.ToInt32(Console.ReadLine());
            User user = new(-1, name, firm, email, phone, kind);
            try
            {
                CmdUserCreate cmd = new(user, mng);
                facade.exec(cmd);
                Console.WriteLine("User was successful created.");
            }
            catch (LogicException ex)
            {
                Console.WriteLine("Error while create user.");
            }
        }

        private void AddCategory()
        {
            Console.Write("Enter category name: ");
            string? name = Console.ReadLine();
            Category cat = new(-1, name);
            try
            {
                CmdCategoryCreate cmd = new(cat, mng);
                facade.exec(cmd);
                Console.WriteLine("Category was successful created.");
            }
            catch (LogicException ex)
            {
                Console.WriteLine("Error while create category.");
            }
        }

        private void DeleteContract()
        {
            Console.Write("Enter contract id: ");
            int id = Convert.ToInt32(Console.ReadLine());
            CmdContractDelete cmd = new(id, mng);
            try
            {
                facade.exec(cmd);
                Console.WriteLine("Delete contract.");
            }
            catch (LogicException ex)
            {
                Console.WriteLine("Can't delete contract with given id.");
            }
        }

        private void DeleteFirm()
        {
            Console.Write("Enter firm id: ");
            int id = Convert.ToInt32(Console.ReadLine());
            CmdFirmDelete cmd = new(id, mng);
            try
            {
                facade.exec(cmd);
                Console.WriteLine("Delete firm.");
            }
            catch (LogicException ex)
            {
                Console.WriteLine("Can't delete firm with given id.");
            }
        }

        private void DeleteProduct()
        {
            Console.Write("Enter product id: ");
            int id = Convert.ToInt32(Console.ReadLine());
            CmdProductGet cmd1 = new(id, mng);
            CmdProductDelete cmd2 = new(id, mng);
            try
            {
                facade.exec(cmd1);
                if (cmd1.Result.Distributor.Id != curUser.Firm.Id && state != ConsoleUiStateEnum.Admin)
                {
                    Console.WriteLine("Can't delete product of another firm.");
                    return;
                }
                facade.exec(cmd2);
                Console.WriteLine("Delete product.");
            }
            catch (LogicException ex)
            {
                Console.WriteLine("Can't delete product with given id.");
            }
        }

        private void DeleteCategory()
        {
            Console.Write("Enter category id: ");
            int id = Convert.ToInt32(Console.ReadLine());
            CmdCategoryDelete cmd = new(id, mng);
            try
            {
                facade.exec(cmd);
                Console.WriteLine("Delete category.");
            }
            catch (LogicException ex)
            {
                Console.WriteLine("Can't delete category with given id.");
            }
        }

        private void DeleteUser()
        {
            Console.Write("Enter user id: ");
            int id = Convert.ToInt32(Console.ReadLine());
            if (id == curUser.Id)
            {
                Console.WriteLine("Can't delete yourself.");
                return;
            }
            CmdUserDelete cmd = new(id, mng);
            try
            {
                facade.exec(cmd);
                Console.WriteLine("Delete user.");
            }
            catch (LogicException ex)
            {
                Console.WriteLine("Can't delete user with given id.");
            }
        }

        private void DeleteWarehouse()
        {
            Console.Write("Enter warehouse id: ");
            int id = Convert.ToInt32(Console.ReadLine());
            CmdWarehouseDelete cmd = new(id, mng);
            try
            {
                facade.exec(cmd);
                Console.WriteLine("Delete warehouse.");
            }
            catch (LogicException ex)
            {
                Console.WriteLine("Can't delete warehouse with given id.");
            }
        }

        private void UpdateFirm()
        {
            Firm firm = SelectFirm();
            Console.Write("Enter new firm name: ");
            string? name = Console.ReadLine();
            Console.Write("Enter new firm phone: ");
            string? phone = Console.ReadLine();
            Console.Write("Enter new firm email: ");
            string? email = Console.ReadLine();
            Console.Write("Enter new firm physical address: ");
            string? phys_addr = Console.ReadLine();
            Console.Write("Enter new firm legal address: ");
            string? law_addr = Console.ReadLine();
            if (name != null && name.Length > 0) firm.Name = name;
            if (email != null && email.Length > 0) firm.Email = email;
            if (phone != null && phone.Length > 0) firm.Phone = phone;
            if (phys_addr != null && phys_addr.Length > 0) firm.PhysAddr = phys_addr;
            if (law_addr != null && law_addr.Length > 0) firm.LawAddr = law_addr;
            try
            {
                CmdFirmUpdate cmd = new(firm, mng);
                facade.exec(cmd);
                Console.WriteLine("Firm was successful updated.");
            }
            catch (LogicException ex)
            {
                Console.WriteLine("Error while update firm.");
            }
        }

        private void UpdateWarehouse()
        {
            Warehouse wh = SelectWarehouse();
            Console.Write("Enter new warehouse address: ");
            string? addr = Console.ReadLine();
            if (addr != null && addr.Length > 0) wh.Address = addr;
            try
            {
                CmdWarehouseUpdate cmd = new(wh, mng);
                facade.exec(cmd);
                Console.WriteLine("Warehouse was successful updated.");
            }
            catch (LogicException ex)
            {
                Console.WriteLine("Error while update warehouse.");
            }
        }

        private void UpdateProduct()
        {
            Product prod = SelectProduct();
            Console.Write("Enter new product name: ");
            string? name = Console.ReadLine();
            Category category = SelectCategory();
            Firm provider = curUser.IsAdmin() ? SelectFirm() : curUser.Firm;
            Console.Write("Enter new product cost: ");
            int? cost = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter new producer name: ");
            Producer producer = SelectProducer();
            if (name != null && name.Length > 0) prod.Name = name;
            if (category != null) prod.Category = category;
            if (provider != null) prod.Distributor = provider;
            if (cost != null) prod.Cost = (int)cost;
            if (producer != null) prod.Producer = producer;
            try
            {
                CmdProductUpdate cmd = new(prod, mng);
                facade.exec(cmd);
                Console.WriteLine("Product was successful updated.");
            }
            catch (LogicException ex)
            {
                Console.WriteLine("Error while update product.");
            }
        }

        private void UpdateUser()
        {
            User user = state == ConsoleUiStateEnum.Admin ? SelectUser() : curUser;
            Console.Write("Enter new user fullname: ");
            string? name = Console.ReadLine();
            Firm firm = state == ConsoleUiStateEnum.Admin ? SelectFirm() : user.Firm;
            Console.Write("Enter new user phone: ");
            string? phone = Console.ReadLine();
            Console.Write("Enter new user email: ");
            string? email = Console.ReadLine();
            UsersEnum? kind = user.UserKind;
            if (state == ConsoleUiStateEnum.Admin)
            {
                Console.Write("Enter new user kind (0 = manager, 1 = director, 2 = admin): ");
                kind = (UsersEnum)Convert.ToInt32(Console.ReadLine());
            }
            if (name != null && name.Length > 0) user.FullName = name;
            if (firm != null) user.Firm = firm;
            if (phone != null && phone.Length > 0) user.Phone = phone;
            if (email != null && email.Length > 0) user.Email = email;
            if (kind != null) user.UserKind = (UsersEnum)kind;
            try
            {
                CmdUserUpdate cmd = new(user, mng);
                facade.exec(cmd);
                Console.WriteLine("User was successful updated.");
            }
            catch (LogicException ex)
            {
                Console.WriteLine("Error while update user.");
            }
        }

        private void UpdateCategory()
        {
            Category cat = SelectCategory();
            Console.Write("Enter new category name: ");
            string? name = Console.ReadLine();
            if (name != null && name.Length > 0) cat.Name = name;
            try
            {
                CmdCategoryUpdate cmd = new(cat, mng);
                facade.exec(cmd);
                Console.WriteLine("Category was successful updated.");
            }
            catch (LogicException ex)
            {
                Console.WriteLine("Error while update category.");
            }
        }

        private void ViewProductList()
        {
            CmdProductGetAll cmd = new(mng);
            try
            {
                facade.exec(cmd);
                Console.WriteLine("List of products: ");
                foreach (Product prod in cmd.Result)
                {
                    Console.WriteLine(string.Format("Id = {0}, Name = {1}, Category = {2} ({3}), Distributor = {4} ({5}), " +
                                                    "Cost = {6}, Producer = {7};",
                                                    prod.Id, prod.Name, prod.Category.Id, prod.Category.Name,
                                                    prod.Distributor.Id, prod.Distributor.Name,
                                                    prod.Cost, prod.Producer));
                }
            }
            catch (LogicException ex)
            {
                Console.WriteLine("Can't get product list.");
            }
        }

        private void ViewContractList()
        {
            CmdFirmGetContracts cmd = new(curUser.Firm, mng);
            try
            {
                facade.exec(cmd);
                Console.WriteLine("List of products: ");
                foreach (Contract ctr in cmd.Result)
                {
                    Console.WriteLine(string.Format("Id = {0}, Firm = {1} ({2}), Director1 = {3} ({4}), " +
                                                    "Director2 = {5} ({6}), Manager1 = {7} ({8}), Manager2 = {9} ({10}), " +
                                                    "ConclusionDate = {11}, ExpirationDate = {12}, Document = {13};",
                                                    ctr.Id, ctr.Firm.Id, ctr.Firm.Name,
                                                    ctr.Director1?.Id, ctr.Director1?.FullName,
                                                    ctr.Director2?.Id, ctr.Director2?.FullName,
                                                    ctr.Manager1?.Id, ctr.Manager1?.FullName,
                                                    ctr.Manager2?.Id, ctr.Manager2?.FullName,
                                                    ctr.ConclusionDate, ctr.ExpirationDate,
                                                    ctr.Document));
                }
            }
            catch (LogicException ex)
            {
                Console.WriteLine("Can't get product list.");
            }
        }

        private void ControlMenu()
        {
            Console.Write(
                "1. Create\n" +
                "2. Update\n" +
                "3. Delete\n"
            );
        }

        private void ControlFirm()
        {
            ControlMenu();
            Console.Write("What to do? ");
            int action = Convert.ToInt32(Console.ReadLine());
            switch (action)
            {
                case 1:
                    AddFirm(); break;
                case 2:
                    UpdateFirm(); break;
                case 3:
                    DeleteFirm(); break;
                default:
                    Console.WriteLine("Unknown option."); break;
            }
        }

        private void ControlWarehouse()
        {
            ControlMenu();
            Console.Write("What to do? ");
            int action = Convert.ToInt32(Console.ReadLine());
            switch (action)
            {
                case 1:
                    AddWarehouse(); break;
                case 2:
                    UpdateWarehouse(); break;
                case 3:
                    DeleteWarehouse(); break;
                default:
                    Console.WriteLine("Unknown option."); break;
            }
        }

        private void ControlProduct()
        {
            ControlMenu();
            Console.Write("What to do? ");
            int action = Convert.ToInt32(Console.ReadLine());
            switch (action)
            {
                case 1:
                    AddProduct(); break;
                case 2:
                    UpdateProduct(); break;
                case 3:
                    DeleteProduct(); break;
                default:
                    Console.WriteLine("Unknown option."); break;
            }
        }

        private void ControlUser()
        {
            ControlMenu();
            Console.Write("What to do? ");
            int action = Convert.ToInt32(Console.ReadLine());
            switch (action)
            {
                case 1:
                    AddUser(); break;
                case 2:
                    UpdateUser(); break;
                case 3:
                    DeleteUser(); break;
                default:
                    Console.WriteLine("Unknown option."); break;
            }
        }

        private void ControlCategory()
        {
            ControlMenu();
            Console.Write("What to do? ");
            int action = Convert.ToInt32(Console.ReadLine());
            switch (action)
            {
                case 1:
                    AddCategory(); break;
                case 2:
                    UpdateCategory(); break;
                case 3:
                    DeleteCategory(); break;
                default:
                    Console.WriteLine("Unknown option."); break;
            }
        }
    }
}
