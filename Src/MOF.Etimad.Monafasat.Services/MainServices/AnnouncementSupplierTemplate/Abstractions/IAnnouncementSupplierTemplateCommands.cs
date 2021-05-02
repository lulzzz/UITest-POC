using MOF.Etimad.Monafasat.Core.Entities;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public interface IAnnouncementSupplierTemplateCommands
    {
        Task<AnnouncementSupplierTemplate> UpdateAnnouncementSupplierTemplateAsync(AnnouncementSupplierTemplate announcement);
        Task<AnnouncementSupplierTemplate> CreateAnnouncementSupplierTemplate(AnnouncementSupplierTemplate announcement);
        Task UpdateAnnouncementSupplierTemplate(AnnouncementSupplierTemplate announcement);
        Task<string> UpdateReferenceNumber();
        Task SaveChangesAsync();
    }
}
