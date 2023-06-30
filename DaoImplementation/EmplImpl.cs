using DaoDbNorthwind.contract;
using DaoDbNorthwind.contract.dao;
using DaoDbNorthwind.contract.enities;
using DaoDbNorthwind.data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Net;

namespace DaoDbNorthwind.DaoImplementation
{
    public class EmplImpl : IDaoEmployees
    {
        private readonly NorthwindContext context;

        //iniettato per DI 
        public EmplImpl(NorthwindContext dbContext)
        {
            context = dbContext;
        }

        public async Task<List<Employees>> GetEmpByCity(string city)
        {
            return await context.Employees.Where(e => e.City == city).ToListAsync();
        }

        public async Task<int> Create(Employees entity)
        {
            int id = -1;

            context.Employees.Add(entity);
            if(await context.SaveChangesAsync() > 0) //restituisce il #(righe) modificate
            {
                id = entity.EmployeeId;
            }

            return id;
        }

        public async Task<Employees> Get(int id)
        {
            Employees? employees = await context.Employees.Where(e => e.EmployeeId == id).FirstOrDefaultAsync(e => e.EmployeeId == id);
            return employees;
        }

        public async Task<bool> Update(int id, string field1, string field2)
        {
            Boolean update;
            int rowsAffected = 0;

            //ricerco il recod da aggiornare...
            var employee = await context.Employees.FindAsync(id);

            if (employee != null)
            {
                employee.LastName = field1;
                employee.City = field2;

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

            var employee = await context.Employees.FindAsync(id);

            if (employee != null)
            {
                context.Employees.Remove(employee);

                rowsAffected = await context.SaveChangesAsync();
            }

            if (rowsAffected > 0) { update = true; }
            else { update = false; }

            return update;
        }
    }
}
