using MOF.Etimad.Monafasat.SharedKernel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("AnnouncementRequestSupplierTemplate", Schema = "AnnouncementTemplate")]

    public class AnnouncementJoinRequestSupplierTemplate : AuditableEntity
    {
        [Key]
        public int Id { get; private set; }
        public int StatusId { get; private set; }

        [ForeignKey(nameof(StatusId))]
        public AnnouncementJoinRequestStatusSupplierTemplate JoinRequestStatus { get; private set; }

        public int AnnouncementId { get; private set; }
        [ForeignKey(nameof(AnnouncementId))]
        public AnnouncementSupplierTemplate AnnouncementSupplierTemplate { get; private set; }

        [StringLength(20)]
        public string Cr { get; set; }
        [ForeignKey(nameof(Cr))]
        public Supplier Supplier { get; private set; }

        [StringLength(2000)]
        public string RejectionReason { get; set; }

        [StringLength(2000)]
        public string DeleteReason { get; set; }

        [StringLength(2000)]
        public string Notes { get; set; }

        public List<AnnouncementTemplateJoinRequestAttachment> Attachments { get; private set; } = new List<AnnouncementTemplateJoinRequestAttachment>();
        public List<AnnouncementTemplateJoinRequestHistory> JoinRequestHistories { get; private set; } = new List<AnnouncementTemplateJoinRequestHistory>();

        public AnnouncementJoinRequestSupplierTemplate()
        {

        }
        public AnnouncementJoinRequestSupplierTemplate(List<AnnouncementTemplateJoinRequestAttachment> attachments, int announcementId, string cr, int statusId)
        {

            this.AnnouncementId = announcementId;
            this.Cr = cr;
            this.StatusId = statusId;
            if (attachments.Count > 0)
            {
                this.UpdateSuppliersTemplateJoinRequestAttachments(attachments, cr);
            }

            EntityCreated();

        }

        public AnnouncementJoinRequestSupplierTemplate UpdateSuppliersTemplateJoinRequestAttachments(List<AnnouncementTemplateJoinRequestAttachment> attachments, string userId, bool addHistory = true)
        {
            foreach (var item in Attachments)
            {
                item.Delete();
            }

            foreach (var attachment in attachments)
            {
                Attachments.Add(attachment);
            }
            EntityUpdated();
            return this;
        }

        public void UpdateAnnouncementJoinRequest(int userId, int joinRequestId, int requestStatusId, string rejectionRaason = "", string notes = "")
        {

            StatusId = requestStatusId;
            RejectionReason = rejectionRaason;
            Notes = notes;
            if (requestStatusId == (int)Enums.AnnouncementTemplateJoinRequestStatus.Rejected)
            {
                Attachments.ForEach(a => a.DeActive());
            }
            JoinRequestHistories.Add(new AnnouncementTemplateJoinRequestHistory(userId, joinRequestId, requestStatusId, rejectionRaason));

            EntityUpdated();

        }


        public AnnouncementJoinRequestSupplierTemplate SetAnnouncementTemplate(AnnouncementSupplierTemplate announcementTemplate)
        {
            AnnouncementSupplierTemplate = announcementTemplate;
            EntityUpdated();
            return this;
        }

        public void setJoinRequestStatusForTest(List<AnnouncementTemplateJoinRequestHistory> joinRequestHistories)
        {
            JoinRequestStatus = new AnnouncementJoinRequestStatusSupplierTemplate();
           JoinRequestHistories = joinRequestHistories;
            EntityUpdated();
        }



        public void Withdraw(int userId, int joinRequestId)
        {
            StatusId = (int)Enums.AnnouncementTemplateJoinRequestStatus.Withdrawn;
            Attachments.ForEach(a => a.DeActive());
            JoinRequestHistories.Add(new AnnouncementTemplateJoinRequestHistory(userId, joinRequestId, (int)Enums.AnnouncementTemplateJoinRequestStatus.Withdrawn));
            EntityUpdated();
        }
        public void DeleteSupplierJoinRequest(int userId, int joinRequestId, string deleteReason)
        {
            StatusId = (int)Enums.AnnouncementTemplateJoinRequestStatus.Deleted;
            DeleteReason = deleteReason;
            Attachments.ForEach(a => a.DeActive());
            JoinRequestHistories.Add(new AnnouncementTemplateJoinRequestHistory(userId, joinRequestId, (int)Enums.AnnouncementTemplateJoinRequestStatus.Deleted));
            EntityUpdated();

        }
    }
}
