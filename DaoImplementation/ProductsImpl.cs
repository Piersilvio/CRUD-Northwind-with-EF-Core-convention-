using DaoDbNorthwind.contract.dao;
using DaoDbNorthwind.contract.enities;
using DaoDbNorthwind.data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace DaoDbNorthwind.DaoImplementation
{
    public class ProductsImpl : IDaoProducts
    {
        private readonly NorthwindContext context;

        //iniettato per DI 
        public ProductsImpl(NorthwindContext dbContext)
        {
            context = dbContext;
        }

        public async Task<List<Products>> GetProductsByName(string productName)
        {
            return await context.Products.Where(e => e.ProductName == productName).ToListAsync();
        }

        public async Task<int> Create(Products entity)
        {
            int id = -1;

            context.Products.Add(entity);
            if (await context.SaveChangesAsync() > 0) //restituisce il #(righe) modificate
            {
                id = entity.ProductId;
            }

            return id;
        }

        public async Task<Products> Get(int id)
        {
            Products? product = await context.Products.Where(e => e.ProductId == id).FirstOrDefaultAsync(e => e.ProductId == id);
            return product;
        }

        public async Task<bool> Update(int id, string field1, string field2)
        {
            Boolean update;
            int rowsAffected = 0;

            //ricerco il recod da aggiornare...
            var product = await context.Products.FindAsync(id);

            var castingField1 = decimal.Parse(field1);
            var castingField2 = short.Parse(field2);

            if (product != null)
            {
                product.UnitPrice = castingField1;
                product.UnitsInStock = castingField2;

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

            var product = await context.Products.FindAsync(id);

            if (product != null)
            {
                context.Products.Remove(product);

                rowsAffected = await context.SaveChangesAsync();
            }

            if (rowsAffected > 0) { update = true; }
            else { update = false; }

            return update;
        }
    }
}
