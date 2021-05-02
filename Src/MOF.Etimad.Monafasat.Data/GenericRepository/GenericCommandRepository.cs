using MOF.Etimad.Monafasat.Core.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Data.GenericRepository
{
    public class GenericCommandRepository : IGenericCommandRepository
    {
        private readonly IAppDbContext _context;

        public GenericCommandRepository(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<TEntity> CreateAsync<TEntity>(TEntity entity) where TEntity : class
        {
            var dbSet = _context.Set<TEntity>();
            await dbSet.AddAsync(entity);
            return entity;
        }

        public async Task CreateRangeAsync<TEntity>(List<TEntity> entities) where TEntity : class
        {
            var dbSet = _context.Set<TEntity>();
            await dbSet.AddRangeAsync(entities);
        }

        public TEntity Update<TEntity>(TEntity entity) where TEntity : class
        {
            var dbSet = _context.Set<TEntity>();
            dbSet.Update(entity);
            return entity;
        }

        public void UpdateRange<TEntity>(List<TEntity> entities) where TEntity : class
        {
            var dbSet = _context.Set<TEntity>();
            dbSet.UpdateRange(entities);
        }

        public void Delete<TEntity>(TEntity entity) where TEntity : class
        {
            var dbSet = _context.Set<TEntity>();
            dbSet.Remove(entity);
        }

        public void DeleteRange<TEntity>(List<TEntity> entityList) where TEntity : class
        {
            var dbSet = _context.Set<TEntity>();
            dbSet.RemoveRange(entityList);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}
