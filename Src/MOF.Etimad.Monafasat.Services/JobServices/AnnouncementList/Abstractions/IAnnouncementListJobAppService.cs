using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public interface IAnnouncementListJobAppService
    {
        Task<bool> updateAnnouncementListStatus();
    }
}
