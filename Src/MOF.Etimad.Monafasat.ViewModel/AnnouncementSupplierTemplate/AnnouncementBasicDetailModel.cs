using MOF.Etimad.Monafasat.SharedKernel;
using System;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class AnnouncementDescriptionModel
    {
        public string RequirementConditionsToJoinList { get; set; }
        public bool IsRequiredAttachmentToJoinList { get; set; }
        public string RequiredAttachment { get; set; }
    }
    public class AnnouncementBasicDetailModel
    {
        public string CreatedBy { get; set; }
        public string IntroAboutAnnouncementTemplate { get; set; }
        public string ReferenceNumber { get; set; }

        public string AnnouncementName { get; set; }
        public string PublishDateHijriString
        {
            get
            {
                return PublishedDate.HasValue ? PublishedDate.Value.ToHijriDateWithFormat("dd/MM/yyyy") : "";
            }
        }
        public string PublishDateString
        {
            get
            {
                return PublishedDate.HasValue ? PublishedDate.Value.ToGregorianDate("dd/MM/yyyy") : "";
            }
        }
        public string AgencyName { get; set; }
        public string AgencyCode { get; set; }
        public string BranchName { get; set; }
        public string Details { get; set; }
        public string StatusName { get; set; }
        public string TenderTypeName { get; set; }
        public string AnnouncementListTypeName { get; set; }
        public DateTime? PublishedDate { get; set; }
        public bool IsEffectiveList { get; set; } = true;
        public string EffectiveListDate { get; set; }
        public string Descriptions { get; set; }
        public bool IsCreatedByAgncy { get; set; }

    }


    public class AnnouncementTemplateListDetailsModel
    {
        public int JoinRequestCount { get; set; }

        public int AcceptedSuppliersCount { get; set; }

        public int UsingListCount { get; set; }

        public int UsingListCountInternally { get; set; }

    }

}
