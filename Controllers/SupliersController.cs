using DaoDbNorthwind.contract.dao;
using DaoDbNorthwind.contract.enities;
using DaoDbNorthwind.DTO;
using JWTAuthAPi.models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DaoDbNorthwind.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    [ApiController]
    [Route("api/[controller]")]
    public class SupliersController : ControllerBase
    {
        private readonly IDaoSupliers repoSupliers;
        private readonly IMapperConfig mapper;
        private readonly ILogger<OrdersController> _logger;

        public SupliersController(ILogger<OrdersController> logger, IDaoSupliers daoSup, IMapperConfig MapperConfig)
        {
            _logger = logger;
            this.repoSupliers = daoSup;
            this.mapper = MapperConfig;
        }

        [HttpGet("GetSuppliersById")]
        public async Task<ActionResult<SupliersDTO>> GetSuppliersById(int suppliersId)
        {
            var map = mapper.InitializeAutomapper();
            SupliersDTO suplierById;
            try
            {
                var e = await repoSupliers.Get(suppliersId);
                if (e is null)
                {
                    return StatusCode(404, "supplier not found");
                }
                else
                {
                    suplierById = map.Map<SupliersDTO>(e);
                    return StatusCode(200, suplierById);
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "server broken");
            }
        }

        [HttpGet("GetSuppliersByCity")]
        public async Task<ActionResult<List<SupliersDTO>>> GetSuppliersByCity(string city)
        {
            var map = mapper.InitializeAutomapper();
            List<SupliersDTO> supliersByCity;
            try
            {
                var e = await repoSupliers.GetSuppliersByCity(city);
                if (e is null)
                {
                    return StatusCode(404, "row affected: 0");
                }
                else
                {
                    supliersByCity = map.Map<List<SupliersDTO>>(e);
                    return StatusCode(200, supliersByCity);
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "server broken");
            }
        }

        [HttpPost("CreateSupplier")]
        public async Task<IActionResult> CreateSupplier(SupliersDTO entity)
        {
            var map = mapper.InitializeAutomapper();
            var supplier = map.Map<Supliers>(entity);

            int suplierId = await repoSupliers.Create(supplier);
            if (suplierId > 0)
            {
                return Ok();
            }
            else
            {
                return StatusCode(422, "failed to create attempt");
            }
        }

        [HttpPut("UpdateSupplier")]
        public async Task<IActionResult> UpdateSupplier(int SupplierID, string SupplierCity, string SupplierAddress)
        {
            bool update = await repoSupliers.Update(SupplierID, SupplierCity, SupplierAddress);
            if (update)
            {
                return Ok();
            }
            else
            {
                return StatusCode(422, "failed to update attempt");
            }
        }

        [HttpDelete("DeleteSupplier")]
        public async Task<IActionResult> DeleteSupplier(SupliersDTO entity)
        {
            var map = mapper.InitializeAutomapper();
            var supplier = map.Map<Supliers>(entity);

            bool delete = await repoSupliers.Delete(supplier.SupplierId);
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
