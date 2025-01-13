using Domain.Interfaces.Repositories;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly PollingDbContext _dbContext;

        public GenericRepository(PollingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task<List<T>> GetAll(int page, int pageSize)
        {
            try
            {
                List<T> model = await _dbContext.Set<T>()
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
                return model;
            }
            catch
            {
                throw;
            }
        }
        public virtual async Task<T> GetById(int id)
        {
            try
            {
                T? model = await _dbContext.Set<T>().FindAsync(id);
                return model;
            }
            catch
            {
                throw;
            }
        }
        public virtual async Task<T> Create(T model)
        {
            try
            {
                _dbContext.Set<T>().Add(model);
                await _dbContext.SaveChangesAsync();
                return model;
            }
            catch
            {
                throw;
            }
        }
        public virtual async Task<bool> Update(T model)
        {
            try
            {
                _dbContext.Set<T>().Update(model);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch 
            {
                throw;
            }
        }
        public virtual async Task<bool> Delete(T model)
        {
            try
            {
                _dbContext.Set<T>().Remove(model);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;
            }
        }
    }
}
