using static MOF.Etimad.Monafasat.SharedKernel.Enums;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class MainNotificationTemplateModel
    {
        public MainNotificationTemplateModel() { }
        public MainNotificationTemplateModel(NotificationArguments prmtrs, string link, NotificationEntityType entityType, string entityValue, int? branchId = null, int? committeeId = null)
        {
            Args = prmtrs;
            Link = link;
            EntityType = entityType;
            EntityValue = entityValue;
            BranchId = branchId;
            CommitteeId = committeeId;
        }
        public MainNotificationTemplateModel(NotificationArguments prmtrs)
        {
            Args = prmtrs;
        }
        public string RecipientnName { get; set; }
        public string Link { get; set; }
        public string TemplateName { get; set; } // assigned From Notification Service
        public NotificationEntityType EntityType { get; set; }
        public string EntityValue { get; set; }
        public int? BranchId { get; set; }
        public int? CommitteeId { get; set; }
        public NotificationArguments Args { get; set; }

    }

    public class NotificationArguments
    {
        public object[] SubjectEmailArgs { get; set; }
        public object[] BodyEmailArgs { get; set; }
        public object[] PanelArgs { get; set; }
        public object[] SMSArgs { get; set; }


    }
}
