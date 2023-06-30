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
    public class ProductsController : ControllerBase
    {
        private readonly IDaoProducts repoProducts;
        private readonly IMapperConfig mapper;
        private readonly ILogger<OrdersController> _logger;

        public ProductsController(ILogger<OrdersController> logger, IDaoProducts daoProduct, IMapperConfig MapperConfig)
        {
            _logger = logger;
            this.repoProducts = daoProduct;
            this.mapper = MapperConfig;
        }

        [HttpGet("GetProductsById")]
        [Authorize]
        public async Task<ActionResult<ProductsDTO>> GetProductsById(int ProductID)
        {
            var map = mapper.InitializeAutomapper();
            ProductsDTO productById;
            try
            {
                var e = await repoProducts.Get(ProductID);
                if (e is null)
                {
                    return StatusCode(404, "product not found");
                }
                else
                {
                    productById = map.Map<ProductsDTO>(e);
                    return StatusCode(200, productById);
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "server broken");
            }
        }

        [HttpGet("GetProductByNamer")]
        [Authorize]
        public async Task<ActionResult<List<ProductsDTO>>> GetProductsByNamer(string productName)
        {
            var map = mapper.InitializeAutomapper();
            List<ProductsDTO> productByName;
            try
            {
                var e = await repoProducts.GetProductsByName(productName);
                if (e is null)
                {
                    return StatusCode(404, "row affected: 0");
                }
                else
                {
                    productByName = map.Map<List<ProductsDTO>>(e);
                    return StatusCode(200, productByName);
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "server broken");
            }
        }

        [Authorize]
        [HttpPost("CreateProduct")]
        public async Task<IActionResult> CreateProduct(ProductsDTO entity)
        {
            var map = mapper.InitializeAutomapper();
            var product = map.Map<Products>(entity);

            int productId = await repoProducts.Create(product);
            if (productId > 0)
            {
                return Ok(productId);
            }
            else
            {
                return StatusCode(422, "failed to create attempt");
            }
        }

        [Authorize]
        [HttpPut("UpdateProducts")]
        public async Task<IActionResult> UpdateProducts(int ProductID, string ProductUnitPrice, string ProductUnitsInStock)
        {
            bool update = await repoProducts.Update(ProductID, ProductUnitPrice, ProductUnitsInStock);
            if (update)
            {
                return Ok();
            }
            else
            {
                return StatusCode(422, "failed to update attempt");
            }
        }

        [Authorize]
        [HttpDelete("DeleteProducts")]
        public async Task<IActionResult> DeleteProducts(ProductsDTO entity)
        {
            var map = mapper.InitializeAutomapper();
            var product = map.Map<Products>(entity);

            bool delete = await repoProducts.Delete(product.ProductId);
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
