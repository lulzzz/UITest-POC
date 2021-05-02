using MOF.Etimad.Monafasat.Core.Interfaces;
using MOF.Etimad.Monafasat.SharedKernel;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public class AnnouncementListJobAppService : IAnnouncementListJobAppService
    {
        private readonly IGenericCommandRepository _genericCommandRepository;
        private readonly IAnnouncementListJobQueries _announcementQueires;

        public AnnouncementListJobAppService(IAnnouncementListJobQueries announcementQueires, IGenericCommandRepository genericCommandRepository)
        {
            _announcementQueires = announcementQueires;
            _genericCommandRepository = genericCommandRepository;
        }
        public async Task<bool> updateAnnouncementListStatus()
        {
            var announcements = await _announcementQueires.getAllEndedTemplates();
            foreach (var announcement in announcements)
            {
                announcement.UpdateAnnouncementStatus(Enums.AnnouncementSupplierTemplateStatus.Ended, "", 0);
                _genericCommandRepository.Update(announcement);
            }
            await _genericCommandRepository.SaveAsync();
            return true;
        }
    }
}
