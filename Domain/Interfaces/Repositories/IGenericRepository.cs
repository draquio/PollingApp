using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<List<T>> GetAll(int page, int pageSize);
        Task<T> GetById(int id);
        Task<T> Create(T model);
        Task<bool> Update(T model);
        Task<bool> Delete(T model);
    }
}
