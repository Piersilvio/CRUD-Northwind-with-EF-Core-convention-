using DaoDbNorthwind.contract.enities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DaoDbNorthwind.entities
{
    public class OrderDetails
    {
            [Key]
            [Required]
            [Column("OrderID")]
            [ForeignKey(nameof(Orders.OrderId))]
            public int OrderId { get; set; }

            [Key]
            [Required]
            [Column("ProductID")]
            [ForeignKey(nameof(Products.ProductId))]
            public int ProductId { get; set; }

            [Column("UnitPrice")]
            [Required]
            public decimal UnitPrice { get; set; }

            [Column("Quantity")]
            [Required]
            public short Quantity { get; set; }

            [Column("Discount")]
            [Required]
            public float Discount { get; set; }

            public virtual Orders Order { get; set; } = null!;

            public virtual Products Product { get; set; } = null!;
    }
}
