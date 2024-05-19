using BuisnessLogic.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLogic.interfaces.repository
{
    public interface IFirmRepository
    {
        void Add(Firm firm);
        Firm Get(int id);
        List<Firm> GetAll();
        List<User> GetStaff(Firm firm);
        List<Product> GetProducts(Firm firm);
        List<Contract> GetContracts(Firm firm);
        void Delete(int id);
        void Update(Firm firm);
    }
}
