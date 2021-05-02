using MOF.Etimad.Monafasat.Core.Entities;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public interface IAnnouncementCommands
    {

        Task<Announcement> UpdateAnnouncementAsync(Announcement announcement);
        Task<Announcement> CreateAnnouncement(Announcement announcement);
        Task UpdateAnnouncement(Announcement announcement);
        Task<string> UpdateReferenceNumber();
        Task SaveChangesAsync();
    }
}
