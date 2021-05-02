using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Data;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.SharedKernel;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public class AnnouncementSupplierTemplateCommands : IAnnouncementSupplierTemplateCommands
    {
        private IAppDbContext _context { get; }

        public AnnouncementSupplierTemplateCommands(IAppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public async Task<AnnouncementSupplierTemplate> CreateAnnouncementSupplierTemplate(AnnouncementSupplierTemplate announcement)
        {
            Check.ArgumentNotNull(nameof(announcement), announcement);

            await _context.AnnouncementSupplierTemplate.AddAsync(announcement);
            return announcement;
        }
        public async Task UpdateAnnouncementSupplierTemplate(AnnouncementSupplierTemplate announcement)
        {
            Check.ArgumentNotNull(nameof(announcement), announcement);
            _context.AnnouncementSupplierTemplate.Update(announcement);
        }
        public async Task<string> UpdateReferenceNumber()
        {
            return await _context.GenerateReferenceNumber((int)Enums.ReferenceNumberType.AnnouncementTemplate);
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<AnnouncementSupplierTemplate> UpdateAnnouncementSupplierTemplateAsync(AnnouncementSupplierTemplate announcement)
        {
            _context.AnnouncementSupplierTemplate.Update(announcement);
            await _context.SaveChangesAsync();
            return announcement;
        }
    }
}
