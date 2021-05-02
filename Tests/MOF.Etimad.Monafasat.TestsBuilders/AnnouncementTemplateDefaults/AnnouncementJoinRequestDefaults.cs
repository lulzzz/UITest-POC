using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using System.Collections.Generic;

namespace MOF.Etimad.Monafasat.TestsBuilders.AnnouncementTemplateDefaults
{
    public class AnnouncementJoinRequestDefaults
    {
          

        private const string REJECTION_REASON = "Rejection Reason";
        private const string NOTES = "notes";
        private const string CR = "123";
        private const int JOINREQUEST_ID = 1;
        private const int ANNOUNCEMENT_ID = 1;
        private const int JOIN_REQUEST_STATUS_ID = (int)Enums.AnnouncementTemplateJoinRequestStatus.Sent;

        private readonly List<AnnouncementTemplateJoinRequestAttachment> ATTACHMENTS = new List<AnnouncementTemplateJoinRequestAttachment>
            {
                new AnnouncementTemplateJoinRequestAttachment("name", "123")
            };
        public AnnouncementSuppliersTemplateJoinRequestsDetailsModel GetJoinRequestsDetailsModel()
        {
            var model = new AnnouncementSuppliersTemplateJoinRequestsDetailsModel
            {
                JoinRequestId = JOINREQUEST_ID,
                AnnouncementId = ANNOUNCEMENT_ID,
                RequestResultId = (int)Enums.AnnouncementTemplateJoinRequestStatus.PendingRejection,
                RejectionReason = REJECTION_REASON,
                Notes = NOTES
            };
            return model;
        }

        public AnnouncementJoinRequestSupplierTemplate GetJoinRequestDefaultData()
        {
            AnnouncementJoinRequestSupplierTemplate joinRequest = new AnnouncementJoinRequestSupplierTemplate(ATTACHMENTS, ANNOUNCEMENT_ID, CR, JOIN_REQUEST_STATUS_ID);
            return joinRequest;
        }
        public AnnouncementJoinRequestSupplierTemplate GetJoinRequestWithAnnouncementDefaultData()
        {
            AnnouncementJoinRequestSupplierTemplate joinRequest = new AnnouncementJoinRequestSupplierTemplate(ATTACHMENTS, ANNOUNCEMENT_ID, CR, JOIN_REQUEST_STATUS_ID);
           
            AnnouncementSupplierTemplate announcementTemplate = new AnnouncementSupplierTemplate();
            announcementTemplate.CreateAnnouncementSupplierTemplate(new AnnouncementTemplateDefaults().GetAllAnnouncementSupplierTemplateModelData(), new List<AnnouncementSuppliersTemplateAttachment>(), 1);

            joinRequest.SetAnnouncementTemplate(announcementTemplate);

            var lst = new List<AnnouncementTemplateJoinRequestHistory>();
            lst.Add(new AnnouncementTemplateJoinRequestHistory(1, 1, 6, "",""));

            joinRequest.setJoinRequestStatusForTest(lst);
            return joinRequest;
        }
    }
}
