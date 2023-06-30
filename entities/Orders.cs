using DaoDbNorthwind.entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static DaoDbNorthwind.entities.OrderDetails;

namespace DaoDbNorthwind.contract.enities
{
    [Table("Orders")]
    public class Orders
    {
        [Key]
        public int OrderId { get; set; }

        [MaxLength(5)]
        [ForeignKey(nameof(Customers.CustomerId))]
        [Column("CustomerID")]
        public string? CustomerId { get; set; } //assumo che non sia FK

        [Column("EmployeeID")]
        [ForeignKey(nameof(Employees.EmployeeId))]
        public int? EmployeeId { get; set; } //FK

        [Column("OrderDate")]
        public DateTime? OrderDate { get; set; }

        [Column("RequiredDate")]
        public DateTime? RequiredDate { get; set; }

        [Column("ShippedDate")]
        public DateTime? ShippedDate { get; set; }

        [Column("ShipVia")]
        [ForeignKey(nameof(Supliers.SupplierId))]
        public int? ShipVia { get; set; } //assumo che non sia FK 

        [Column("Freight")]
        public decimal? Freight { get; set; }

        [MaxLength(40)]
        public string? ShipName { get; set; }

        [MaxLength(60)]
        public string? ShipAddress { get; set; }

        [MaxLength(15)]
        public string? ShipCity { get; set; }

        [MaxLength(15)]
        public string? ShipRegion { get; set; }

        [MaxLength(10)]
        public string? ShipPostalCode { get; set; }

        [MaxLength(15)]
        public string? ShipCountry { get; set; }

        //nav. prop.
        public Employees? Employee { get; set; }
        public virtual Shippers? ShipViaNavigation { get; set; }
        public virtual ICollection<OrderDetails> OrderDetails { get; set; } = new List<OrderDetails>();
        public Customers? Customer { get; set; }
    }
}
