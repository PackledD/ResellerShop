using BuisnessLogic.interfaces.repository;
using BuisnessLogic.models;
using Exceptions;
using Exceptions.logic;
using System.IO;


namespace BuisnessLogic.controllers_inner
{
    public class ContractController
    {
        private readonly IContractRepository rep;

        public ContractController(IContractRepository rep)
        {
            this.rep = rep ?? throw new ControllerCreationException("Empty repository in constructor of " + GetType().ToString());
        }

        public void Create(Contract contr)
        {
            try
            {
                rep.Add(contr);
                Logger.Logger.Info("New contract was added");
            }
            catch (DbException e)
            {
                throw new CreationException("Error while create new contract with id " + contr.Id.ToString());
            }
        }

        public Contract Get(int id)
        {
            try
            {
                var res = rep.Get(id);
                Logger.Logger.Info("Contract was got");
                return res;
            }
            catch (DbException e)
            {
                throw new GettingException("Error while get contract with id " + id.ToString());
            }
        }

        public List<Contract> GetAll()
        {
            try
            {
                var res = rep.GetAll();
                Logger.Logger.Info("All contracts were got");
                return res;
            }
            catch (DbException e)
            {
                throw new GettingException("Error while get all contracts");
            }
        }

        public List<ContractPos> GetContent(Contract contr)
        {
            try
            {
                var res = rep.GetContent(contr);
                Logger.Logger.Info("Contract content was got");
                return res;
            }
            catch (DbException e)
            {
                throw new GettingException("Error while get content of contract with id " + contr.Id.ToString());
            }
        }

        public void Delete(int id)
        {
            try
            {
                rep.Delete(id);
                Logger.Logger.Info("Contract was deleted");
            }
            catch (DbException e)
            {
                throw new DeletingException("Error while delete contract with id " + id.ToString());
            }
        }

        public void Update(Contract contr)
        {
            try
            {
                rep.Update(contr);
                Logger.Logger.Info("Contract was updated");
            }
            catch (DbException e)
            {
                throw new UpdatingException("Error while update contract with id " + contr.Id.ToString());
            }
        }

        public void Approve(Contract contr, User manager)
        {
            if (!manager.IsManager())
            {
                throw new InvalidUserException("User with id " + manager.Id.ToString() + " isn't manager, can't approve contract");
            }
            if (!contr.WithFirm(manager.Firm))
            {
                throw new InvalidUserException("User with id " + manager.Id.ToString() +
                                               " isn't attach to firm with id " + contr.Firm.Id.ToString());
            }
            if (!contr.CanApprove(manager))
            {
                throw new AlreadyProcessedContractException("Contract with id " + contr.Id.ToString() + " has already approved");
            }
            try
            {
                rep.Approve(contr, manager);
                Logger.Logger.Info("Contract was approved");
            }
            catch (DbException e)
            {
                throw new UpdatingException("Error while approving contract with id " + contr.Id.ToString());
            }
        }

        public void Sign(Contract contr, User director)
        {
            if (!director.IsDirector())
            {
                throw new InvalidUserException("User with id " + director.Id.ToString() + " isn't director, can't sign contract");
            }
            if (!contr.WithFirm(director.Firm))
            {
                throw new InvalidUserException("User with id " + director.Id.ToString() +
                                               " isn't attach to firm with id " + contr.Firm.Id.ToString());
            }
            if (!contr.CanSign(director))
            {
                throw new AlreadyProcessedContractException("Contract with id " + contr.Id.ToString() + " can't be signed");
            }
            try
            {
                rep.Sign(contr, director);
                Logger.Logger.Info("Contract was signed");
            }
            catch (DbException e)
            {
                throw new UpdatingException("Error while signing contract with id " + contr.Id.ToString());
            }
        }
    }
}

