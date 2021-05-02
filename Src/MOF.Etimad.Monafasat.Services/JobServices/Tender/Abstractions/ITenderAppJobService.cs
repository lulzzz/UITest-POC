using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public interface ITenderAppJobService
    {
        Task SendFinalAwardedTendersToEmarketPlace();
        Task SendNotificatoinAfterFinishTendersStoppingPeriod();
        Task<bool> CheckBiddingTenderRounds();
        Task<bool> GetTenderOffersForOpening(int id);
        Task<bool> GetTenderOffersForChecking();
    }
}
