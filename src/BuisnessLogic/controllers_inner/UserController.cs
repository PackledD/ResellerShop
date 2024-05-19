using BuisnessLogic.interfaces.repository;
using BuisnessLogic.models;
using Exceptions;
using Exceptions.logic;


namespace BuisnessLogic.controllers_inner
{
    public class UserController
    {
        private readonly IUserRepository rep;

        public UserController(IUserRepository rep)
        {
            this.rep = rep ?? throw new ControllerCreationException("Empty repository in constructor of " + GetType().ToString());
        }

        public void Create(User user)
        {
            try
            {
                rep.Add(user);
                Logger.Logger.Info("User was added");
            }
            catch (DbException e)
            {
                throw new CreationException("Error while create new user with id " + user.Id.ToString());
            }
        }

        public User Get(int id)
        {
            try
            {
                var res = rep.Get(id);
                Logger.Logger.Info("User was got");
                return res;
            }
            catch (DbException e)
            {
                throw new GettingException("Error while get user with id " + id.ToString());
            }
        }

        public List<User> GetAll()
        {
            try
            {
                var res = rep.GetAll();
                Logger.Logger.Info("All users were got");
                return res;
            }
            catch (DbException e)
            {
                throw new GettingException("Error while get all users");
            }
        }

        public void Delete(int id)
        {
            try
            {
                rep.Delete(id);
                Logger.Logger.Info("User was deleted");
            }
            catch (DbException e)
            {
                throw new DeletingException("Error while delete user with id " + id.ToString());
            }
        }

        public void Update(User user)
        {
            try
            {
                rep.Update(user);
                Logger.Logger.Info("User was updated");
            }
            catch (DbException e)
            {
                throw new UpdatingException("Error while update user with id " + user.Id.ToString());
            }
        }

        public User Auth(string login, string hash)
        {
            try
            {
                var res = rep.Auth(login, hash);
                Logger.Logger.Info("User was authenticate");
                return res;
            }
            catch (DbException e)
            {
                throw new AuthException("Error while authenticate user with login " + login);
            }
        }

        public User Register(User user, string hash)
        {
            try
            {
                var res = rep.Register(user, hash);
                Logger.Logger.Info("User was registered");
                return res;
            }
            catch (DbException e)
            {
                throw new AuthException("Error while register new user");
            }
        }
    }
}
