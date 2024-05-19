using BuisnessLogic.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLogic.interfaces.repository
{
    public interface IWareProdRepository
    {
        void Add(WareProd wareProd);
        WareProd Get(int wareId, int prodId);
        List<WareProd> GetAll();
        void Delete(int wareId, int prodId);
        void Update(WareProd wareProd);
    }
}
