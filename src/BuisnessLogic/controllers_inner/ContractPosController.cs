using BuisnessLogic.interfaces.repository;
using BuisnessLogic.models;
using Exceptions;
using Exceptions.logic;
using System.Diagnostics.Contracts;


namespace BuisnessLogic.controllers_inner
{
    public class ContractPosController
    {
        private readonly IContractPosRepository rep;

        public ContractPosController(IContractPosRepository rep)
        {
            this.rep = rep ?? throw new ControllerCreationException("Empty repository in constructor of " + GetType().ToString());
        }

        public void Create(ContractPos cp)
        {
            try
            {
                rep.Add(cp);
                Logger.Logger.Info("Contract position was added");
            }
            catch (DbException e)
            {
                throw new CreationException("Error while create new contract position");
            }
        }

        public ContractPos Get(int contractId, int wareId, int prodId)
        {
            try
            {
                var res = rep.Get(contractId, wareId, prodId);
                Logger.Logger.Info("Contract position was got");
                return res;
            }
            catch (DbException e)
            {
                throw new GettingException("Error while get contract position");
            }
        }

        public List<ContractPos> GetAll()
        {
            try
            {
                var res = rep.GetAll();
                Logger.Logger.Info("All contract positions were added");
                return res;
            }
            catch (DbException e)
            {
                throw new GettingException("Error while get all contract positions");
            }
        }

        public void Delete(int contractId, int wareId, int prodId)
        {
            try
            {
                rep.Delete(contractId, wareId, prodId);
                Logger.Logger.Info("Contract position was deleted");

            }
            catch (DbException e)
            {
                throw new DeletingException("Error while delete contract position");
            }
        }
    }
}

