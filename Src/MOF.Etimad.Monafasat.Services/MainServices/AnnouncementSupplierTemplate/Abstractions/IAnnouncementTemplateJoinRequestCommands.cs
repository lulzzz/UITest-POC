using MOF.Etimad.Monafasat.Core.Entities;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public interface IAnnouncementTemplateJoinRequestCommands
    {
        Task<AnnouncementJoinRequestSupplierTemplate> UpdateAnnouncementJoinRequestAsync(AnnouncementJoinRequestSupplierTemplate request);
        Task<AnnouncementJoinRequestSupplierTemplate> CreateAnnouncementJoinRequestAsync(AnnouncementJoinRequestSupplierTemplate request);
        Task UpdateAnnouncementJoinRequest(AnnouncementJoinRequestSupplierTemplate request);
        Task SaveChangesAsync();
    }
}
