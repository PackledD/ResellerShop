using BuisnessLogic.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLogic.interfaces.repository
{
    public interface IContractPosRepository
    {
        void Add(ContractPos pos);
        ContractPos Get(int contractId, int wareId, int prodId);
        List<ContractPos> GetAll();
        void Delete(int contractId, int wareId, int prodId);
    }
}
