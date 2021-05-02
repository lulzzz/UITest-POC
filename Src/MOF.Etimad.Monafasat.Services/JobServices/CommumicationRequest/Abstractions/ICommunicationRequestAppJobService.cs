using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public interface ICommunicationRequestAppJobService
    {
        Task FindTendersWithPlaintsAfterStoppingPeriodJob();

        Task ExcludeSupplierOffer();
        Task UpdateAllNegotiationWaitingSupplierResponse();
        Task UpdateAllSecondNegotiationWaitingSupplierResponse();
    }
}
