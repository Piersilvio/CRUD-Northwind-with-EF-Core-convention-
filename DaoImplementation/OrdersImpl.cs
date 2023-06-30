using DaoDbNorthwind.contract.dao;
using DaoDbNorthwind.contract.enities;
using DaoDbNorthwind.data;
using DaoDbNorthwind.entities;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace DaoDbNorthwind.contract.DaoImplementation
{
    public class OrdersImpl : IDaoOrders
    {
        private readonly NorthwindContext context;

        //iniettato per DI 
        public OrdersImpl(NorthwindContext dbContext)
        {
            context = dbContext;
        }

        public async Task<List<Orders>> GetOrdersByCustomerID(string CustomerID)
        {
            var orders = await context.Orders
                        .Join(context.Customers, o => o.CustomerId, c => c.CustomerId, (o, c) => o)
                        .Where(o => o.CustomerId == CustomerID)
                        .ToListAsync();

            return orders;
        }

        public async Task<List<Orders>> GetOrdersByCity(string city)
        {
            var orders = await context.Orders
                        .Join(context.Customers, o => o.CustomerId, c => c.CustomerId, (o, c) => new { Order = o, Customer = c })
                        .Where(x => x.Customer.City == city)
                        .Select(x => x.Order)
                        .ToListAsync();

            return orders;
        }

        public async Task<int> Create(Orders entity)
        {
            int id = -1;

            context.Orders.Add(entity);
            if (await context.SaveChangesAsync() > 0) //restituisce il #(righe) modificate
            {
                id = entity.OrderId;
            }

            return id;
        }

        public async Task<Orders> Get(int id)
        {
            Orders? order = await context.Orders.Where(e => e.OrderId == id).FirstOrDefaultAsync(e => e.OrderId == id);
            return order;
        }

        public async Task<bool> Update(int id, string field1, string field2)
        {
            Boolean update;
            int rowsAffected = 0;

            //ricerco il recod da aggiornare...
            var order = await context.Orders.FindAsync(id);

            if (order != null)
            {
                order.ShipName = field1;
                order.ShipCity = field2;

                rowsAffected = await context.SaveChangesAsync();
            }

            if (rowsAffected > 0) { update = true; }
            else { update = false; }

            return update;
        }

        public async Task<bool> Delete(int id)
        {
            Boolean update;
            int rowsAffected = 0;

            var order = await context.Orders.FindAsync(id);

            if (order != null)
            {
                context.Orders.Remove(order);

                rowsAffected = await context.SaveChangesAsync();
            }

            if (rowsAffected > 0) { update = true; }
            else { update = false; }

            return update;
        }
    }
}
