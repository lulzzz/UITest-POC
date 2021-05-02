namespace MOF.Etimad.Monafasat.ViewModel
{
    public class UserNotificationModel
    {
        public int UserId { get; set; }
        public int userRoleId { get; set; }

    }
    public class CRNotificationModel
    {
        public string CR { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Name { get; set; }
    }
    public class UserNotificationSettingModel
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }

    }




}
