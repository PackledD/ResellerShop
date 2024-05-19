using BuisnessLogic.interfaces.repository;
using BuisnessLogic.models;
using Exceptions;
using Exceptions.logic;


namespace BuisnessLogic.controllers_inner
{
    public class ProductController
    {
        private readonly IProductRepository rep;

        public ProductController(IProductRepository rep)
        {
            this.rep = rep ?? throw new ControllerCreationException("Empty repository in constructor of " + GetType().ToString());
        }

        public void Create(Product prod)
        {
            try
            {
                rep.Add(prod);
                Logger.Logger.Info("Product was added");
            }
            catch (DbException e)
            {
                throw new CreationException("Error while create new product with id " + prod.Id.ToString());
            }
        }

        public Product Get(int id)
        {
            try
            {
                var res = rep.Get(id);
                Logger.Logger.Info("Product was got");
                return res;
            }
            catch (DbException e)
            {
                throw new GettingException("Error while get product with id " + id.ToString());
            }
        }

        public List<Product> GetAll()
        {
            try
            {
                var res = rep.GetAll();
                Logger.Logger.Info("All products were got");
                return res;
            }
            catch (DbException e)
            {
                throw new GettingException("Error while get all products");
            }
        }

        public List<Product> GetByCategory(Category cat)
        {
            try
            {
                var res = rep.GetByCategory(cat);
                Logger.Logger.Info("All products by category were got");
                return res;
            }
            catch (DbException e)
            {
                throw new GettingException("Error while get products of category with id " + cat.Id.ToString());
            }
        }

        public void Delete(int id)
        {
            try
            {
                rep.Delete(id);
                Logger.Logger.Info("Product was deleted");
            }
            catch (DbException e)
            {
                throw new DeletingException("Error while delete product with id " + id.ToString());
            }
        }

        public void Update(Product prod)
        {
            try
            {
                rep.Update(prod);
                Logger.Logger.Info("Product was updated");
            }
            catch (DbException e)
            {
                throw new UpdatingException("Error while update product with id " + prod.Id.ToString());
            }
        }
    }
}

