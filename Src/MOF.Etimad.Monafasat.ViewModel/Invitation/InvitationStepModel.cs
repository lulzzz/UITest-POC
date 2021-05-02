using System;
using System.Collections.Generic;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class InvitationStepModel
    {
        public int TenderId { get; set; }
        public string TenderIdString { get; set; }
        public DateTime? LastOfferPresentationDate { get; set; }
        public DateTime? LastEnqueriesDate { get; set; }
        public DateTime? OffersOpeningDate { get; set; }
        public DateTime? OffersCkeckingDate { get; set; }
        public string TenderNumber { get; set; }
        public string ReferenceNumber { get; set; }
        public string TenderName { get; set; }
        public string AgencyName { get; set; }

        public string TenderTypeName { get; set; }
        public bool? InsideKSA { get; set; }
        public int TenderStatusId { get; set; }
        public int TenderTypeId { get; set; }
        public int? TenderCreatedByTypeId { get; set; }
        public bool HasQualification { get; set; }
        public int? PreQualificationId { get; set; }
        public int? FirstStageId { get; set; }
        public List<AreaModel> TenderAreas { set; get; }

        public bool HasAnnouncementTemplateWithLimitedNumberReason { get; set; }
        public bool HasAnnouncementTemplate { get; set; }
        public int? AnnouncementTemplateId { get; set; }

    }

}
