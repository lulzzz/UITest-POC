using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Core.Interfaces
{
    public interface IGenericCommandRepository
    {
        Task<TEntity> CreateAsync<TEntity>(TEntity entity) where TEntity : class;
        Task CreateRangeAsync<TEntity>(List<TEntity> entities) where TEntity : class;
        TEntity Update<TEntity>(TEntity entity) where TEntity : class;
        void UpdateRange<TEntity>(List<TEntity> entities) where TEntity : class;
        void Delete<TEntity>(TEntity entity) where TEntity : class;
        void DeleteRange<TEntity>(List<TEntity> entityList) where TEntity : class;
        Task SaveAsync();
    }
}
