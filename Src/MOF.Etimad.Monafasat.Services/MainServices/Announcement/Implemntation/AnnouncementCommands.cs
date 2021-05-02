using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Data;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.SharedKernel;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public class AnnouncementCommands : IAnnouncementCommands
    {
        public IAppDbContext _context { get; }

        public AnnouncementCommands(IAppDbContext appDbContext)
        {
            _context = appDbContext;
        }


        public async Task<Announcement> CreateAnnouncement(Announcement announcement)
        {
            Check.ArgumentNotNull(nameof(announcement), announcement);

            await _context.Announcements.AddAsync(announcement);
            return announcement;
        }
        public async Task UpdateAnnouncement(Announcement announcement)
        {
            Check.ArgumentNotNull(nameof(announcement), announcement);

            _context.Announcements.Update(announcement);
        }
        public async Task<string> UpdateReferenceNumber()
        {
            return await _context.GenerateReferenceNumber((int)Enums.ReferenceNumberType.Announcement);
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<Announcement> UpdateAnnouncementAsync(Announcement announcement)
        {
            _context.Announcements.Update(announcement);
            await _context.SaveChangesAsync();
            return announcement;
        }
    }
}
