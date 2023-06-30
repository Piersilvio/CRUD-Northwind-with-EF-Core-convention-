using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DaoDbNorthwind.contract.enities
{
    [Table("Suppliers")]
    public class Supliers
    {
        [Key]
        [Required]
        public int SupplierId { get; set; }

        [MaxLength(40)]
        public string CompanyName { get; set; } = null!;

        [MaxLength(30)]
        public string? ContactName { get; set; }

        [MaxLength(30)]
        public string? ContactTitle { get; set; }

        [MaxLength(60)]
        public string? Address { get; set; }

        [MaxLength(15)]
        public string? City { get; set; }

        [MaxLength(15)]
        public string? Region { get; set; }

        [MaxLength(10)]
        public string? PostalCode { get; set; }

        [MaxLength(15)]
        public string? Country { get; set; }

        [MaxLength(24)]
        public string? Phone { get; set; }

        [MaxLength(24)]
        public string? Fax { get; set; }

        [Column("HomePage")]
        public string? HomePage { get; set; }

        //nav. prop.
        public virtual List<Products> Products { get; set; } = new List<Products>();
    }
}
