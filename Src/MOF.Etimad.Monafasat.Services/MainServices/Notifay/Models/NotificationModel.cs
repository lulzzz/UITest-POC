namespace MOF.Etimad.Monafasat.Services.MainServices.Notifay.Models
{
    public class NotificationDataModel
    {
        public MessageModel Email { get; set; } = new
            MessageModel();
        public MessageModel SMS { get; set; } = new
            MessageModel();
        public string PanelMessage { get; set; }
    }
}
