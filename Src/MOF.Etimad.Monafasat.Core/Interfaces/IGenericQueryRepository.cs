
using MOF.Etimad.Monafasat.SharedKernal;
using System.Linq;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Core.Interfaces
{
    public interface IGenericQueryRepository
    {
        Task<IQueryable> GetAllAsync<TEntity>() where TEntity : BaseEntity;
    }
}
