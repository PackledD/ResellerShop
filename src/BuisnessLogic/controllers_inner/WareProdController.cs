using BuisnessLogic.interfaces.repository;
using BuisnessLogic.models;
using Exceptions;
using Exceptions.logic;


namespace BuisnessLogic.controllers_inner
{
    public class WareProdController
    {
        private readonly IWareProdRepository rep;

        public WareProdController(IWareProdRepository rep)
        {
            this.rep = rep ?? throw new ControllerCreationException("Empty repository in constructor of " + GetType().ToString());
        }

        public void Create(WareProd wp)
        {
            try
            {
                rep.Add(wp);
                Logger.Logger.Info("Warehouse product was added");
            }
            catch (DbException e)
            {
                throw new CreationException("Error while create new warehouse product");
            }
        }

        public WareProd Get(int wareId, int prodId)
        {
            try
            {
                var res = rep.Get(wareId, prodId);
                Logger.Logger.Info("Warehouse product was got");
                return res;
            }
            catch (DbException e)
            {
                throw new GettingException("Error while get warehouse product with wareId "
                    + wareId.ToString() + " and prodId " + prodId.ToString());
            }
        }

        public List<WareProd> GetAll()
        {
            try
            {
                var res = rep.GetAll();
                Logger.Logger.Info("All warehouse products were got");
                return res;
            }
            catch (DbException e)
            {
                throw new GettingException("Error while get all warehouse products");
            }
        }

        public void Delete(int wareId, int prodId)
        {
            try
            {
                rep.Delete(wareId, prodId);
                Logger.Logger.Info("Warehouse product was deleted");
            }
            catch (DbException e)
            {
                throw new DeletingException("Error while delete warehouse product with wareId "
                    + wareId.ToString() + " and prodId " + prodId.ToString());
            }
        }

        public void Update(WareProd wp)
        {
            try
            {
                rep.Update(wp);
                Logger.Logger.Info("Warehouse product was updated");
            }
            catch (DbException e)
            {
                throw new UpdatingException("Error while update warehouse product");
            }
        }
    }
}
