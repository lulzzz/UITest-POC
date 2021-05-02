using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Data;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public class MandatoryListCommands : IMandatoryListCommands
    {
        private readonly IAppDbContext _dbContext;

        public MandatoryListCommands(IAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<MandatoryList> Add(MandatoryList entity)
        {
            var addedEntity = _dbContext.MandatoryLists.Add(entity);
            await _dbContext.SaveChangesAsync();
            return addedEntity.Entity;
        }

        public async Task AddChangeRequest(MandatoryListChangeRequest mandatoryListUpdates)
        {
            _dbContext.MandatoryListsChangeRequests.Add(mandatoryListUpdates);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(MandatoryList entity)
        {
            _dbContext.MandatoryLists.Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateChangeRequest(MandatoryListChangeRequest mandatoryListUpdates)
        {
            _dbContext.MandatoryListsChangeRequests.Update(mandatoryListUpdates);
            await _dbContext.SaveChangesAsync();
        }
    }
}
