using BuisnessLogic.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLogic.interfaces.repository
{
    public interface IProducerRepository
    {
        void Add(Producer prod);
        Producer Get(int id);
        List<Producer> GetAll();
        void Delete(int id);
        void Update(Producer prod);
    }
}
