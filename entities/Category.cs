using DaoDbNorthwind.contract.enities;

namespace DaoDbNorthwind.entities
{
    public class Category
    {
        public int CategoryId { get; set; }

        public string CategoryName { get; set; } = null!;

        public string? Description { get; set; }

        public byte[]? Picture { get; set; }

        public virtual ICollection<Products> Products { get; set; } = new List<Products>();
    }
}
