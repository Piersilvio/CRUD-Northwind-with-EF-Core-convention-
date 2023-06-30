using DaoDbNorthwind.contract.enities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DaoDbNorthwind.DTO
{
    public class EmployeesDTO
    {
        public int EmployeeId { get; set; }
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public string? Title { get; set; }
        public string? TitleOfCourtesy { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime HireDate { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Region { get; set; }
        public string? PostalCode { get; set; }
        public string? Country { get; set; }

        public ICollection<Orders> Orders { get; set; } = new List<Orders>();
    }
}
