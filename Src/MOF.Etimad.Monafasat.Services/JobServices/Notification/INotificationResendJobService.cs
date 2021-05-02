using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public interface INotificationResendJobService
    {
        Task ResendFailNotification();
    }
}
