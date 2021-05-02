namespace MOF.Etimad.Monafasat.ViewModel
{
    public class ApproveAnnouncemntSupplierTemplateModel
    {
        public int AnnouncementId { get; set; }
        public string AnnouncementIdString { get; set; }
        public string LastAnnouncementJoinDate { get; set; }
        public int StatusId { set; get; }

        public int CreatedById { set; get; }

        public int UserProfileId { get; set; }
    }
}
