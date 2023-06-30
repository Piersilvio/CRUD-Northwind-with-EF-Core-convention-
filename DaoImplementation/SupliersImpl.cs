using DaoDbNorthwind.contract.dao;
using DaoDbNorthwind.contract.enities;
using DaoDbNorthwind.data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Net;

namespace DaoDbNorthwind.DaoImplementation
{
    public class SupliersImpl : IDaoSupliers
    {
        private readonly NorthwindContext context;

        //iniettato per DI 
        public SupliersImpl(NorthwindContext dbContext)
        {
            context = dbContext;
        }

        public async Task<List<Supliers>> GetSuppliersByCity(string city)
        {
            return await context.Supliers.Where(e => e.City == city).ToListAsync();
        }

        public async Task<int> Create(Supliers entity)
        {
            int id = -1;

            context.Supliers.Add(entity);
            if (await context.SaveChangesAsync() > 0) //restituisce il #(righe) modificate
            {
                id = entity.SupplierId;
            }

            return id;
        }

        public async Task<Supliers> Get(int id)
        {
            Supliers? suplier = await context.Supliers.Where(e => e.SupplierId == id).FirstOrDefaultAsync(e => e.SupplierId == id);
            return suplier;
        }

        public async Task<bool> Update(int id, string field1, string field2)
        {
            Boolean update;
            int rowsAffected = 0;

            //ricerco il recod da aggiornare...
            var suplier = await context.Supliers.FindAsync(id);

            if (suplier != null)
            {
                suplier.City = field1;
                suplier.Address = field2;

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

            var suplier = await context.Supliers.FindAsync(id);

            if (suplier != null)
            {
                context.Supliers.Remove(suplier);

                rowsAffected = await context.SaveChangesAsync();
            }

            if (rowsAffected > 0) { update = true; }
            else { update = false; }

            return update;
        }
    }
}
