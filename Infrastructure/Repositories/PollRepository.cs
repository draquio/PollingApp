using Domain.Entities;
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
    public class PollRepository : GenericRepository<Poll>, IPollRepository
    {
        public PollRepository(PollingDbContext dbContext) : base(dbContext)
        {

        }
        public override async Task<List<Poll>> GetAll(int page, int pageSize)
        {
            try
            {
                List<Poll> model = await _dbContext.Set<Poll>()
                    .Include(options => options.Options)
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
        public override async Task<Poll> GetById(int id)
        {
            try
            {
                Poll? model = await _dbContext.Set<Poll>()
                    .Include(options => options.Options)
                    .FirstOrDefaultAsync(p => p.Id == id);
                return model;
            }
            catch
            {
                throw;
            }
        }
    }
}
