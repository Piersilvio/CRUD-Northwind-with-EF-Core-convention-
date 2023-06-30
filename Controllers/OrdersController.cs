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
    public class OrdersController : ControllerBase
    {
        //un DTO per ogni API
        private readonly IDaoOrders repoOrder;
        private readonly IMapperConfig mapper;
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(ILogger<OrdersController> logger, IDaoOrders daoOrder, IMapperConfig MapperConfig)
        {
            _logger = logger;
            this.repoOrder = daoOrder;
            this.mapper = MapperConfig;
        }
        
        [HttpGet("GetOrderById")]
        public async Task<ActionResult<OrdersDTO>> GetOrderById(int OrderID)
        {
            var map = mapper.InitializeAutomapper();
            OrdersDTO orderById;
            try
            {
                var e = await repoOrder.Get(OrderID);
                if(e is null)
                {
                    return StatusCode(404, "order not found");
                }
                else
                {
                    orderById = map.Map<OrdersDTO>(e);
                    return StatusCode(200, orderById);
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "server broken");
            }
        }

        [HttpGet("GetOrderByCustomerID")]
        public async Task<ActionResult<List<OrdersDTO>>> GetOrdersByCustomerID(string CustomerID)
        {
            var map = mapper.InitializeAutomapper();
            List<OrdersDTO> ordersByCustomerId;
            try
            {
                var e = await repoOrder.GetOrdersByCustomerID(CustomerID);
                if( e is null)
                {
                    return StatusCode(404, "row affected: 0");
                }
                else
                {
                    ordersByCustomerId = map.Map<List<OrdersDTO>>(e);
                    return StatusCode(200, ordersByCustomerId);
                }
                
            }
            catch (Exception)
            {
                return StatusCode(500, "server broken");
            }
        }

        [HttpGet("GetOrdersByCity")]
        public async Task<ActionResult<List<OrdersDTO>>> GetOrdersByCity(string city)
        {
            var map = mapper.InitializeAutomapper();
            List<OrdersDTO> orderByCity;
            try
            {
                var e = await repoOrder.GetOrdersByCity(city);
                if( e is null)
                {
                    return StatusCode(404, "row affected: 0");
                }
                else
                {
                    orderByCity = map.Map<List<OrdersDTO>>(e);
                    return StatusCode(200, orderByCity);
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "server broken");
            }
        }

        [HttpPost("CreateOrder")]
        public async Task<IActionResult> CreateOrder(OrdersDTO entity)
        {
            var map = mapper.InitializeAutomapper();
            var order = map.Map<Orders>(entity);

            int orderID = await repoOrder.Create(order);
            if (orderID > 0)
            {
                return Ok(orderID);
            }
            else
            {
                return StatusCode(422, "failed to create attempt");
            }
        }

        [HttpPut("UpdateOrder")]
        public async Task<IActionResult> UpdateOrder(int OrderID, string OrderShipName, string OrderShipCity)
        {
            bool update = await repoOrder.Update(OrderID, OrderShipName, OrderShipCity);
            if (update)
            {
                return Ok();
            }
            else
            {
                return StatusCode(422, "failed to update attempt");
            }
        }

        [HttpDelete("DeleteOrder")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            bool delete = await repoOrder.Delete(id);
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
