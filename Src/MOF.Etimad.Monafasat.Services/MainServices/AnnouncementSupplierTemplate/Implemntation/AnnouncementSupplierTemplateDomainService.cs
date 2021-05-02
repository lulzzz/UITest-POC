using Microsoft.AspNetCore.Http;
using MOF.Etimad.Monafasat.SharedKernel;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{

    public class AnnouncementSupplierTemplateDomainService : IAnnouncementSupplierTemplateDomainService
    {
        public IHttpContextAccessor _httpContextAccessor { get; }
        public IVerificationService _verification { get; }
        public INotificationAppService _notificationAppService { get; }

        public AnnouncementSupplierTemplateDomainService(IHttpContextAccessor httpContextAccessor, IVerificationService verification, INotificationAppService notificationAppService)
        {
            _httpContextAccessor = httpContextAccessor;
            _verification = verification;
            _notificationAppService = notificationAppService;
        }

        public int GetUserId()
        {
            var userId = _httpContextAccessor.HttpContext.User.UserId();
            return userId;
        }
        public string GetUserFullName()
        {
            var name = _httpContextAccessor.HttpContext.User.FullName();
            return name;
        }

        public async Task CreateVerificationCode(int id)
        {
            var typeId = (int)Enums.VerificationType.Announcement;
            var userId = _httpContextAccessor.HttpContext.User.UserId();
            var email = _httpContextAccessor.HttpContext.User.Email();
            var mobile = _httpContextAccessor.HttpContext.User.Mobile();
            await _verification.CreateVerificationCode(id, mobile, email, userId, typeId);
        }
        public async Task<bool> CheckVerificationCode(int id, string verificationCode, int typeId)
        {
            bool check = await _verification.CheckForVerificationCode(id, verificationCode, typeId);
            return check;
        }
    }
}
