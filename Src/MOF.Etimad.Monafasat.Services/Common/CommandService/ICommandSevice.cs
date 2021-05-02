using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public interface ICommandService
    {
        Task<TEntity> CreateAsync<TEntity>(TEntity entity) where TEntity : class;
        TEntity UpdateAsync<TEntity>(TEntity entity) where TEntity : class;
        void UpdateRangeAsync<TEntity>(List<TEntity> entityList) where TEntity : class;
        void DeleteAsync<TEntity>(TEntity entity) where TEntity : class;
        void DeleteRangeAsync<TEntity>(List<TEntity> entityList) where TEntity : class;
        Task SaveAsync();
    }
}
