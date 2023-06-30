using DaoDbNorthwind.entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static DaoDbNorthwind.entities.OrderDetails;

namespace DaoDbNorthwind.contract.enities
{
    [Table("Products")]
    public class Products
    {
        [Key]
        [Required]
        public int ProductId { get; set; }

        [Required]
        [MaxLength(40)]
        public string ProductName { get; set; } = null!;

        [Column("SupplierID")]
        [ForeignKey(nameof(Supliers))]
        public int? SupplierId { get; set; }

        [Column("CategoryID")]
        public int? CategoryId { get; set; }

        [MaxLength(20)]
        public string? QuantityPerUnit { get; set; }

        [Column("UnitPrice")]
        public decimal? UnitPrice { get; set; }

        [Column("UnitsInStock")]
        public short? UnitsInStock { get; set; }

        [Column("UnitsOnOrder")]
        public short? UnitsOnOrder { get; set; }

        [Column("ReorderLevel")]
        public short? ReorderLevel { get; set; }

        [Column("Discontinued")]
        public bool Discontinued { get; set; }

        //nav. prop.
        public virtual Supliers? Supplier { get; set; }
        public virtual Category? Category { get; set; }
        public virtual ICollection<OrderDetails> OrderDetails { get; set; } = new List<OrderDetails>();
    }
}
