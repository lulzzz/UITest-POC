using Microsoft.EntityFrameworkCore;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.TendersAPI.Services
{
    public class ContractQueries:IContractQueries
    {
        private readonly IAppDbContext _dbContext;
        public ContractQueries(IAppDbContext appDbContext)
        {
            _dbContext = appDbContext;
        }

        public async Task<Tender> FindTenderByAgencyAndReferenceNumberForContractLinking(string referenceNumber)
        {
            var result = await _dbContext.Tenders.Include(d => d.TenderHistories).Include(e => e.AgencyCommunicationRequests).ThenInclude(ee => ee.PlaintRequests)
                         .Where(t => t.ReferenceNumber == referenceNumber && t.IsActive == true).FirstOrDefaultAsync();
            return result;
        }
    }
}
