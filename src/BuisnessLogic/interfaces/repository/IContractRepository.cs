using BuisnessLogic.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLogic.interfaces.repository
{
    public interface IContractRepository
    {
        void Add(Contract contr);
        Contract Get(int id);
        List<Contract> GetAll();
        List<ContractPos> GetContent(Contract contr);
        void Delete(int id);
        void Update(Contract contr);
        void Approve(Contract contr, User manager);
        void Sign(Contract contr, User director);
    }
}
