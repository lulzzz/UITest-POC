using MOF.Etimad.Monafasat.ViewModel;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.TendersAPI.Services
{
    public interface IContractAppService
    {
        Task<ContractResponseViewModel> CheckTenderCanBeLinkedToContract(string referenceNumber, string agencyCode);
    }
}
