using MOF.Etimad.Monafasat.Core.Entities;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.TendersAPI.Services
{
    public interface IContractQueries
    {
        Task<Tender> FindTenderByAgencyAndReferenceNumberForContractLinking(string referenceNumber);
    }
}
