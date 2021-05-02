using MOF.Etimad.Monafasat.ViewModel;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Integration
{
    public interface IOldSystemIntegration
    {
        Task<bool> SyncTenderDetails(int tenderId, TenderSyncDataModel tenderSyncDataModel, string uRL, string token);
        Task<bool> UpdateTenderSyncDetails(int tenderId, string requestContent, bool tenderUpdateStatus, string uRL);
    }
}
