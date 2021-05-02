using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public interface ITenderJobSerivce
    {
        Task<bool> CheckBiddingTenderRounds();
        Task<bool> GetTenderOffersForOpening(int id);
        Task<bool> GetTenderOffersForChecking();
    }
}
