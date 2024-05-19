using BuisnessLogic.models;

namespace Gui.Models
{
    public class ContractWithPositions
    {
        public Contract Contract { get; set; }
        public User User { get; set; }
        public List<ContractPos> Positions { get; set; } = new();
    }
}
