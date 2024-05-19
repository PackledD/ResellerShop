using BuisnessLogic.interfaces.repository;
using BuisnessLogic.models;
using Exceptions;
using Exceptions.logic;


namespace BuisnessLogic.controllers_inner
{
    public class CategoryController
    {
        private readonly IProductCategoryRepository rep;

        public CategoryController(IProductCategoryRepository rep)
        {
            this.rep = rep ?? throw new ControllerCreationException("Empty repository in constructor of " + GetType().ToString());
        }

        public void Create(Category cat)
        {
            try
            {
                rep.Add(cat);
                Logger.Logger.Info("New category was added");
            }
            catch (DbException e)
            {
                throw new CreationException("Error while create new category with id " + cat.Id.ToString());
            }
        }

        public Category Get(int id)
        {
            try
            {
                var res = rep.Get(id);
                Logger.Logger.Info("Category was got");
                return res;
            }
            catch (DbException e)
            {
                throw new GettingException("Error while get category with id " + id.ToString());
            }
        }

        public List<Category> GetAll()
        {
            try
            {
                var res = rep.GetAll();
                Logger.Logger.Info("All categories were got");
                return res;
            }
            catch (DbException e)
            {
                throw new GettingException("Error while get all categories");
            }
        }

        public void Delete(int id)
        {
            try
            {
                rep.Delete(id);
                Logger.Logger.Info("Category was deleted");
            }
            catch (DbException e)
            {
                throw new DeletingException("Error while delete category with id " + id.ToString());
            }
        }

        public void Update(Category cat)
        {
            try
            {
                rep.Update(cat);
                Logger.Logger.Info("Category was updated");
            }
            catch (DbException e)
            {
                throw new UpdatingException("Error while update category with id " + cat.Id.ToString());
            }
        }
    }
}

