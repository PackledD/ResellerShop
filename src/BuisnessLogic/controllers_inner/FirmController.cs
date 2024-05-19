using BuisnessLogic.interfaces.repository;
using BuisnessLogic.models;
using Exceptions;
using Exceptions.logic;


namespace BuisnessLogic.controllers_inner
{
    public class FirmController
    {
        private readonly IFirmRepository rep;

        public FirmController(IFirmRepository rep)
        {
            this.rep = rep ?? throw new ControllerCreationException("Empty repository in constructor of " + GetType().ToString());
        }

        public void Create(Firm firm)
        {
            try
            {
                rep.Add(firm);
                Logger.Logger.Info("Firm was added");
            }
            catch (DbException e)
            {
                throw new CreationException("Error while create new firm with id " + firm.Id.ToString());
            }
        }

        public Firm Get(int id)
        {
            try
            {
                var res = rep.Get(id);
                Logger.Logger.Info("Firm was got");
                return res;
            }
            catch (DbException e)
            {
                throw new GettingException("Error while get firm with id " + id.ToString());
            }
        }

        public List<Firm> GetAll()
        {
            try
            {
                var res = rep.GetAll();
                Logger.Logger.Info("All firms were got");
                return res;
            }
            catch (DbException e)
            {
                throw new GettingException("Error while get all firms");
            }
        }

        public List<Product> GetProducts(Firm firm)
        {
            try
            {
                var res = rep.GetProducts(firm);
                Logger.Logger.Info("All firm products were got");
                return res;
            }
            catch (DbException e)
            {
                throw new GettingException("Error while get products of firm with id " + firm.Id.ToString());
            }
        }

        public List<Contract> GetContracts(Firm firm)
        {
            try
            {
                var res = rep.GetContracts(firm);
                Logger.Logger.Info("All firm contracts were got");
                return res;
            }
            catch (DbException e)
            {
                throw new GettingException("Error while get contracts of firm with id " + firm.Id.ToString());
            }
        }

        public List<User> GetStaff(Firm firm)
        {
            try
            {
                var res = rep.GetStaff(firm);
                Logger.Logger.Info("All firm staff were got");
                return res;
            }
            catch (DbException e)
            {
                throw new GettingException("Error while get staff of firm with id " + firm.Id.ToString());
            }
        }

        public void Delete(int id)
        {
            try
            {
                rep.Delete(id);
                Logger.Logger.Info("Firm was deleted");
            }
            catch (DbException e)
            {
                throw new DeletingException("Error while delete firm with id " + id.ToString());
            }
        }

        public void Update(Firm firm)
        {
            try
            {
                rep.Update(firm);
                Logger.Logger.Info("Firm was updated");
            }
            catch (DbException e)
            {
                throw new UpdatingException("Error while update firm with id " + firm.Id.ToString());
            }
        }
    }
}

