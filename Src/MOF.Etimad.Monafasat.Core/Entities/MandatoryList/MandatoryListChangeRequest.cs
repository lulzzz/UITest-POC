using MOF.Etimad.Monafasat.SharedKernal.Exceptions;
using MOF.Etimad.Monafasat.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("MandatoryListChangeRequest", Schema = "MandatoryList")]
    public class MandatoryListChangeRequest : AuditableEntity
    {
        [Key]
        public int Id { get; set; }

        [StringLength(400)]
        public string DivisionNameAr { get; set; }

        [StringLength(400)]
        public string DivisionNameEn { get; set; }

        [StringLength(400)]
        public string DivisionCode { get; set; }

        public string RejectionReason { get; set; }

        [ForeignKey(nameof(Status))]
        public int StatusId { get; private set; }
        public MandatoryListChangeRequestStatus Status { get; private set; }

        [ForeignKey(nameof(OriginalEntity))]
        public int OriginalEntityId { get; private set; }
        public MandatoryList OriginalEntity { get; private set; }

        public List<MandatoryListProductChangeRequest> Products { get; set; }

        public MandatoryListChangeRequest()
        {

        }


        public void Add()
        {
            if (!Products.Any())
                throw new BusinessRuleException(Resources.MandatoryListResources.ErrorMessages.YouHaveToAddOneProductAtleast);

            StatusId = (int)Enums.MandatoryListChangeRequestStatus.WaitingApproval;
            Products.ForEach(a => a.Add());
            EntityCreated();
        }

        public void Approve()
        {
            if (StatusId != (int)Enums.MandatoryListChangeRequestStatus.WaitingApproval)
                throw new BusinessRuleException(Resources.SharedResources.ErrorMessages.NotAddedAuthorized);

            StatusId = (int)Enums.MandatoryListChangeRequestStatus.Approved;
            EntityUpdated();
        }

        public void Reject(string rejectionReason)
        {
            if (StatusId != (int)Enums.MandatoryListChangeRequestStatus.WaitingApproval)
                throw new BusinessRuleException(Resources.SharedResources.ErrorMessages.NotAddedAuthorized);

            if (String.IsNullOrEmpty(rejectionReason))
                throw new BusinessRuleException(Resources.MandatoryListResources.ErrorMessages.RejectionReasonCannotBeEmpty);

            RejectionReason = rejectionReason;
            StatusId = (int)Enums.MandatoryListChangeRequestStatus.Rejected;
            EntityUpdated();
        }

        public void Close()
        {
            if (StatusId != (int)Enums.MandatoryListChangeRequestStatus.Rejected)
                throw new BusinessRuleException(Resources.SharedResources.ErrorMessages.NotAddedAuthorized);

            StatusId = (int)Enums.MandatoryListChangeRequestStatus.Closed;
            EntityUpdated();
        }
    }
}
