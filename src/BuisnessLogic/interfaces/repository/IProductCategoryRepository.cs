using BuisnessLogic.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLogic.interfaces.repository
{
    public interface IProductCategoryRepository
    {
        void Add(Category categ);
        Category Get(int id);
        List<Category> GetAll();
        void Delete(int id);
        void Update(Category categ);
    }
}
