using MOF.Etimad.Monafasat.Core.Interfaces;
using MOF.Etimad.Monafasat.SharedKernal;
using System.Linq;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Data.GenericRepository
{
    public class GenericQueryRepository : IGenericQueryRepository

    {
        private readonly IAppDbContext _dbContext;
        public GenericQueryRepository(IAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IQueryable> GetAllAsync<TEntity>() where TEntity : BaseEntity
        {
            var query = (_dbContext.Set<TEntity>());
            return query;
        }
    }
}
