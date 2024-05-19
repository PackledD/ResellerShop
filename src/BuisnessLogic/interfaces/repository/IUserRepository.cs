using BuisnessLogic.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLogic.interfaces.repository
{
    public interface IUserRepository
    {
        void Add(User user);
        User Get(int id);
        List<User> GetAll();
        void Delete(int id);
        void Update(User user);
        User Auth(string login, string hash);
        User Register(User user, string hash);
    }
}
