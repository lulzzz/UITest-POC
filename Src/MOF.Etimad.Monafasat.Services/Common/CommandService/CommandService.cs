using MOF.Etimad.Monafasat.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public class CommandService : ICommandService
    {
        private readonly IAppDbContext _context;

        public CommandService(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<TEntity> CreateAsync<TEntity>(TEntity entity) where TEntity : class
        {
            var dbSet = _context.Set<TEntity>();
            await dbSet.AddAsync(entity);
            return entity;
        }

        public TEntity UpdateAsync<TEntity>(TEntity entity) where TEntity : class
        {
            var dbSet = _context.Set<TEntity>();
            dbSet.Update(entity);
            return entity;
        }

        public void UpdateRangeAsync<TEntity>(List<TEntity> entityList) where TEntity : class
        {
            var dbSet = _context.Set<TEntity>();
            dbSet.UpdateRange(entityList);
        }

        public void DeleteAsync<TEntity>(TEntity entity) where TEntity : class
        {
            var dbSet = _context.Set<TEntity>();
            dbSet.Remove(entity);
        }

        public void DeleteRangeAsync<TEntity>(List<TEntity> entityList) where TEntity : class
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
