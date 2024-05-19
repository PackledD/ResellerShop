using BuisnessLogic.interfaces.repository;
using BuisnessLogic.models;
using Exceptions;
using Exceptions.logic;


namespace BuisnessLogic.controllers_inner
{
    public class WarehouseController
    {
        private readonly IWarehouseRepository rep;

        public WarehouseController(IWarehouseRepository rep)
        {
            this.rep = rep ?? throw new ControllerCreationException("Empty repository in constructor of " + GetType().ToString());
        }

        public void Create(Warehouse wh)
        {
            try
            {
                rep.Add(wh);
                Logger.Logger.Info("Warehouse was added");
            }
            catch (DbException e)
            {
                throw new CreationException("Error while create new warehouse with id " + wh.Id.ToString());
            }
        }

        public Warehouse Get(int id)
        {
            try
            {
                var res = rep.Get(id);
                Logger.Logger.Info("Warehouse was got");
                return res;
            }
            catch (DbException e)
            {
                throw new GettingException("Error while get warehouse with id " + id.ToString());
            }
        }

        public List<Warehouse> GetAll()
        {
            try
            {
                var res = rep.GetAll();
                Logger.Logger.Info("All warehouses were got");
                return res;
            }
            catch (DbException e)
            {
                throw new GettingException("Error while get all warehouses");
            }
        }

        public List<WareProd> GetProducts(Warehouse wh)
        {
            try
            {
                var res = rep.GetProducts(wh);
                Logger.Logger.Info("All warehouse's products were got");
                return res;
            }
            catch (DbException e)
            {
                throw new GettingException("Error while get products from warehouse with id " + wh.Id.ToString());
            }
        }

        public void Delete(int id)
        {
            try
            {
                rep.Delete(id);
                Logger.Logger.Info("Warehouse was deleted");
            }
            catch (DbException e)
            {
                throw new DeletingException("Error while delete warehouse with id " + id.ToString());
            }
        }

        public void Update(Warehouse wh)
        {
            try
            {
                rep.Update(wh);
                Logger.Logger.Info("Warehouse was updated");
            }
            catch (DbException e)
            {
                throw new UpdatingException("Error while update warehouse with id " + wh.Id.ToString());
            }
        }
    }
}

