using BuisnessLogic.models;

namespace Gui.Models
{
    public class ProductWithWareProd
    {
        public Product Product { get; set; }
        public List<WareProd> Data { get; set; } = new();
    }
}
