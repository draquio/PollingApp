using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(PollingDbContext dbContext) : base(dbContext)
        {
        }
    }
}
