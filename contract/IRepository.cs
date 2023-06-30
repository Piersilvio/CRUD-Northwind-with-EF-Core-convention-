using Microsoft.EntityFrameworkCore;

namespace DaoDbNorthwind.contract
{
    public interface IRepository<T> where T : class
    {
        //CRUD
        public Task<int> Create (T entity);
        public Task<T> Get(int id);
        public Task<bool> Update (int id, string field1, string field2);
        public Task<bool> Delete(int id);
    }
}
