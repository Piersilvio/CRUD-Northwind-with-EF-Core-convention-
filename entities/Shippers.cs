using DaoDbNorthwind.contract.enities;
using System.ComponentModel.DataAnnotations;

namespace DaoDbNorthwind.entities
{
    public class Shippers
    {
        [Key]
        public int ShipperId { get; set; }

        public string CompanyName { get; set; } = null!;
        public string? Phone { get; set; }

        public virtual ICollection<Orders> Orders { get; set; } = new List<Orders>();
    }
}
