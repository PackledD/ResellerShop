using BuisnessLogic.models;

namespace Gui.Models
{
    public class FirmContracts
    {
        public Firm Firm { get; set; }
        public User User { get; set; }
        public List<Contract> Contracts { get; set; } = new();
    }
}
