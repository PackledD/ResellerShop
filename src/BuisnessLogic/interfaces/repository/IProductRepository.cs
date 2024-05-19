using BuisnessLogic.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLogic.interfaces.repository
{
    public interface IProductRepository
    {
        void Add(Product prod);
        Product Get(int id);
        List<Product> GetAll();
        List<Product> GetByCategory(Category categ);
        void Delete(int id);
        void Update(Product prod);
    }
}
