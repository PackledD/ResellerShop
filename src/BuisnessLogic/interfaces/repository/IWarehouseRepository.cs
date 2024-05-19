using BuisnessLogic.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLogic.interfaces.repository
{
    public interface IWarehouseRepository
    {
        void Add(Warehouse wareh);
        Warehouse Get(int id);
        List<Warehouse> GetAll();
        List<WareProd> GetProducts(Warehouse wareh);
        void Delete(int id);
        void Update(Warehouse wareh);
    }
}
