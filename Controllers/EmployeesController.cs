using AutoMapper;
using AutoMapper.Configuration.Annotations;
using DaoDbNorthwind.config;
using DaoDbNorthwind.contract;
using DaoDbNorthwind.contract.dao;
using DaoDbNorthwind.contract.DaoImplementation;
using DaoDbNorthwind.contract.enities;
using DaoDbNorthwind.DaoImplementation;
using DaoDbNorthwind.DTO;
using DaoDbNorthwind.entities;
using JWTAuthAPi.models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DaoDbNorthwind.Controllers
{
    //[Authorize(Roles = UserRoles.Admin)]
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IDaoEmployees repoEmployees;
        private readonly IMapperConfig mapper;
        private readonly ILogger<OrdersController> _logger;

        public EmployeesController(ILogger<OrdersController> logger, IDaoEmployees daoEmp, IMapperConfig MapperConfig)
        {
            _logger = logger;
            this.repoEmployees = daoEmp;
            this.mapper = MapperConfig;
        }

        [HttpGet("GetEmployeeById")]
        public async Task<ActionResult<EmployeesDTO>> GetEmployeeById(int empId)
        {
            var map = mapper.InitializeAutomapper();
            EmployeesDTO employeeById;
            try
            {
                var e = await repoEmployees.Get(empId);
                if (e is null)
                {
                    return StatusCode(404, "employee not found");
                }
                else
                {
                    employeeById = map.Map<EmployeesDTO>(e);
                    return StatusCode(200, employeeById);
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "server broken");
            }
        }

        [HttpGet("GetEmployeesByCity")]
        public async Task<ActionResult<List<EmployeesDTO>>> GetEmployeesByCity(string city)
        {
            var map = mapper.InitializeAutomapper();
            var EmployeeByCity = new List<EmployeesDTO>();
            try
            {
                var e = await repoEmployees.GetEmpByCity(city);
                if (e is null)
                {
                    return StatusCode(404, "row affected: 0");
                }
                else
                {
                    EmployeeByCity = map.Map<List<EmployeesDTO>>(e);
                    return StatusCode(200, EmployeeByCity);
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "server broken");
            }
        }

        [HttpPost("CreateEmployee")]
        public async Task<IActionResult> CreateEmployee(EmployeesDTO entity)
        {
            var map = mapper.InitializeAutomapper();
            var employee = map.Map<Employees>(entity);

            int empID = await repoEmployees.Create(employee);
            if (empID > 0)
            {
                return Ok(empID);
            }
            else
            {
                return StatusCode(422, "failed to create attempt");
            }
        }

        [HttpPut("UpdateEmployee")]
        public async Task<IActionResult> UpdateEmployee(int EmployeeID, string EmployeeLastName, string EmployeeCity)
        {
            bool update = await repoEmployees.Update(EmployeeID, EmployeeLastName, EmployeeCity);
            if (update)
            {
                return Ok();
            }
            else
            {
                return StatusCode(422, "failed to update attempt");
            }
        }

        [HttpDelete("DeleteEmployee")]
        public async Task<IActionResult> DeleteEmployee(int EmployeeID)
        {
            bool delete = await repoEmployees.Delete(EmployeeID);
            if (delete)
            {
                return Ok();
            }
            else
            {
                return StatusCode(422, "failed to delete attempt");
            }
        }
    }
}

