namespace MOF.Etimad.Monafasat.ViewModel
{
    public class NotificationByModel
    {
        public NotificationByModel()
        {

        }
        public NotificationByModel(bool mobile, bool email)
        {
            Mobile = mobile;
            Email = email;
        }
        public bool Mobile { get; set; }
        public bool Email { get; set; }
    }
}
