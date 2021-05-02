namespace MOF.Etimad.Monafasat.Services
{
    public interface IPushNotificationService
    {
        string SendNotification(string NotificationMessage, string deviceToken);
    }
}
