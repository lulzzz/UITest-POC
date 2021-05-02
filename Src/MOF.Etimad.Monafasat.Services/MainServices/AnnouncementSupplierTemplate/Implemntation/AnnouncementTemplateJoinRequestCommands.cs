using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Data;
using MOF.Etimad.SharedKernel;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{

    public class AnnouncementTemplateJoinRequestCommands : IAnnouncementTemplateJoinRequestCommands
    {
        private IAppDbContext _context { get; }

        public AnnouncementTemplateJoinRequestCommands(IAppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public async Task<AnnouncementJoinRequestSupplierTemplate> CreateAnnouncementJoinRequestAsync(AnnouncementJoinRequestSupplierTemplate request)
        {
            Check.ArgumentNotNull(nameof(request), request);

            await _context.AnnouncementRequestSupplierTemplate.AddAsync(request);
            return request;
        }
        public async Task UpdateAnnouncementJoinRequest(AnnouncementJoinRequestSupplierTemplate request)
        {
            Check.ArgumentNotNull(nameof(request), request);
            _context.AnnouncementRequestSupplierTemplate.Update(request);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<AnnouncementJoinRequestSupplierTemplate> UpdateAnnouncementJoinRequestAsync(AnnouncementJoinRequestSupplierTemplate request)
        {
            _context.AnnouncementRequestSupplierTemplate.Update(request);
            await _context.SaveChangesAsync();
            return request;
        }

    }
}
