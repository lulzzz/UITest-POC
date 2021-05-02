using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace MOF.Etimad.Monafasat.ViewModel.Notification
{
    public class OperationCodeViewModel
    {
        public string IdString { get; set; }
        public string OperationCode { get; set; }
        public string ArabicName { get; set; }
        public string PanelTemplateEn { get; set; }
        public string PanelTemplateAr { get; set; }
        public string EmailBodyTemplateEn { get; set; }
        public string EmailBodyTemplateAr { get; set; }
        public string SmsTemplateEn { get; set; }
        public string SmsTemplateAr { get; set; }
        public string EnglishName { get; set; }
        public string roleName { get; set; }
        public string groupName { get; set; }
        public int UserRoleId { get; set; }
        public List<SelectListItem> UserRoles { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> Categories { get; set; } = new List<SelectListItem>();
        public int NotificationCategoryId { get; set; }
    }
}
