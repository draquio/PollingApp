using Domain.Entities;

namespace Domain.Interfaces.Repositories
{
    public interface IPollRepository : IGenericRepository<Poll>
    {
        Task<List<Poll>> GetAll(int page, int pageSize);
    }
}
