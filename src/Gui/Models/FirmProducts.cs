using BuisnessLogic.models;

namespace Gui.Models
{
    public class FirmProducts
    {
        public Firm Firm { get; set; }
        public List<Product> Products { get; set; } = new();
    }
}
