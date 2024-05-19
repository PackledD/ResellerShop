using BuisnessLogic.interfaces.repository;
using BuisnessLogic.models;
using Exceptions;
using Exceptions.logic;


namespace BuisnessLogic.controllers_inner
{
    public class ProducerController
    {
        private readonly IProducerRepository rep;

        public ProducerController(IProducerRepository rep)
        {
            this.rep = rep ?? throw new ControllerCreationException("Empty repository in constructor of " + GetType().ToString());
        }

        public void Create(Producer prod)
        {
            try
            {
                rep.Add(prod);
                Logger.Logger.Info("New producer was added");
            }
            catch (DbException e)
            {
                throw new CreationException("Error while create new producer with id " + prod.Id.ToString());
            }
        }

        public Producer Get(int id)
        {
            try
            {
                var res = rep.Get(id);
                Logger.Logger.Info("Producer was got");
                return res;
            }
            catch (DbException e)
            {
                throw new GettingException("Error while get producer with id " + id.ToString());
            }
        }

        public List<Producer> GetAll()
        {
            try
            {
                var res = rep.GetAll();
                Logger.Logger.Info("All producers were got");
                return res;
            }
            catch (DbException e)
            {
                throw new GettingException("Error while get all producers");
            }
        }

        public void Delete(int id)
        {
            try
            {
                rep.Delete(id);
                Logger.Logger.Info("Producer was deleted");
            }
            catch (DbException e)
            {
                throw new DeletingException("Error while delete producer with id " + id.ToString());
            }
        }

        public void Update(Producer prod)
        {
            try
            {
                rep.Update(prod);
                Logger.Logger.Info("Producer was updated");
            }
            catch (DbException e)
            {
                throw new UpdatingException("Error while update producer with id " + prod.Id.ToString());
            }
        }
    }
}

