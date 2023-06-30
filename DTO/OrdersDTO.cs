using DaoDbNorthwind.contract.enities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DaoDbNorthwind.DTO
{
    public class OrdersDTO
    {
        public int OrderId { get; set; }
        public string? CustomerId { get; set; } //assumo che non sia FK
        public int? EmployeeId { get; set; } //FK
        public DateTime? OrderDate { get; set; }
        public DateTime? RequiredDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public int? ShipVia { get; set; } //assumo che non sia FK 
        public decimal? Freight { get; set; }
        public string? ShipName { get; set; }
        public string? ShipAddress { get; set; }
        public string? ShipCity { get; set; }
        public string? ShipRegion { get; set; }
        public string? ShipPostalCode { get; set; }
        public string? ShipCountry { get; set; }

        //nav. prop.
        public Employees? Employee { get; set; }
    }
}
