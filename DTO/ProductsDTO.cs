using DaoDbNorthwind.contract.enities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DaoDbNorthwind.DTO
{
    public class ProductsDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public int? SupplierId { get; set; }
        public int? CategoryId { get; set; }
        public string? QuantityPerUnit { get; set; }
        public decimal? UnitPrice { get; set; }
        public short? UnitsInStock { get; set; }
        public short? UnitsOnOrder { get; set; }
        public short? ReorderLevel { get; set; }
        public bool Discontinued { get; set; }

        //nav. prop.
        public virtual Supliers? Supplier { get; set; }
    }
}
