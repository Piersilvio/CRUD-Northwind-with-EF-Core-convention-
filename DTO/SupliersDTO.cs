using DaoDbNorthwind.contract.enities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DaoDbNorthwind.DTO
{
    public class SupliersDTO
    {
        public int SupplierId { get; set; }
        public string CompanyName { get; set; } = null!;
        public string? ContactName { get; set; }
        public string? ContactTitle { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Region { get; set; }
        public string? PostalCode { get; set; }
        public string? Country { get; set; }
        public string? Phone { get; set; }
        public string? Fax { get; set; }
        public string? HomePage { get; set; }

        //nav. prop.
        public virtual List<Products> Products { get; set; } = new List<Products>();
    }
}
