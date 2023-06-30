using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DaoDbNorthwind.contract.enities
{
    public class Employees
    {
        [Key]
        [Required]
        public int EmployeeId { get; set; }

        [Required]
        [MaxLength(20)]
        public string? LastName { get; set; }

        [Required]
        [MaxLength(10)]
        public string? FirstName { get; set; }

        [MaxLength(30)]
        public string? Title { get; set; }

        [MaxLength(25)]
        public string? TitleOfCourtesy { get; set; }

        [Column("BirthDate")]
        public DateTime BirthDate { get; set; }

        [Column("HireDate")]
        public DateTime HireDate { get; set; }

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

        public ICollection<Orders> Orders { get; set; } = new List<Orders>();
    }
}
