using MOF.Etimad.Monafasat.SharedKernal.Exceptions;
using MOF.Etimad.Monafasat.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("MandatoryList", Schema = "MandatoryList")]
    public class MandatoryList : AuditableEntity
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
        public MandatoryListStatus Status { get; private set; }

        public List<MandatoryListProduct> Products { get; set; }

        public List<MandatoryListChangeRequest> ChangeRequests { get; set; } = new List<MandatoryListChangeRequest>();

        public MandatoryList()
        {

        }


        public void Add()
        {
            if (!Products.Any())
                throw new BusinessRuleException(Resources.MandatoryListResources.ErrorMessages.YouHaveToAddOneProductAtleast);

            StatusId = (int)Enums.MandatoryListStatus.UnderEstablishing;
            Products.ForEach(a => a.Add());
            EntityCreated();
        }

        public void AddChangeRequest(MandatoryListChangeRequest changeRequest)
        {
            if (ChangeRequests.Any(a => a.StatusId == (int)Enums.MandatoryListChangeRequestStatus.WaitingApproval)
                && ChangeRequests.Any(a => a.StatusId == (int)Enums.MandatoryListChangeRequestStatus.Rejected))
                throw new BusinessRuleException(Resources.MandatoryListResources.ErrorMessages.ThereIsActiveChangeRequest);

            changeRequest.Add();
            ChangeRequests.Add(changeRequest);
            EntityUpdated();
        }

        public void Update(MandatoryList entity)
        {
            if (StatusId != (int)Enums.MandatoryListStatus.UnderEstablishing && StatusId != (int)Enums.MandatoryListStatus.Approved)
                throw new BusinessRuleException(Resources.SharedResources.ErrorMessages.NotAddedAuthorized);

            if (!entity.Products.Any())
                throw new BusinessRuleException(Resources.MandatoryListResources.ErrorMessages.YouHaveToAddOneProductAtleast);

            DivisionNameAr = entity.DivisionNameAr;
            DivisionNameEn = entity.DivisionNameEn;
            DivisionCode = entity.DivisionCode;

            UpdateProducts(entity.Products);
            EntityUpdated();
        }

        private void UpdateProducts(List<MandatoryListProduct> updatedProducts)
        {
            if (Products == null)
                Products = new List<MandatoryListProduct>();

            if (updatedProducts == null)
            {
                Products.Clear();
                return;
            }

            // Deleting not found products
            var deletedProducts = Products.Where(a => !updatedProducts.Any(p => p.Id == a.Id)).ToList();
            foreach (var product in deletedProducts)
            {
                product.SetInActive();
            }

            // Adding new products or update existing products.
            foreach (var item in updatedProducts)
            {
                if (item.Id == 0)
                {
                    item.Add();
                    Products.Add(item);
                }
                else
                {
                    var original = Products.FirstOrDefault(a => a.Id == item.Id);

                    if (original != null)
                        original.Update(item);
                }
            }
        }

        public void SendToApproval()
        {
            if (StatusId != (int)Enums.MandatoryListStatus.UnderEstablishing)
                throw new BusinessRuleException(Resources.SharedResources.ErrorMessages.NotAddedAuthorized);

            StatusId = (int)Enums.MandatoryListStatus.WaitingApproval;
            EntityUpdated();
        }

        public void Approve()
        {
            if (StatusId != (int)Enums.MandatoryListStatus.WaitingApproval)
                throw new BusinessRuleException(Resources.SharedResources.ErrorMessages.NotAddedAuthorized);

            StatusId = (int)Enums.MandatoryListStatus.Approved;
            EntityUpdated();

        }

        public void Reject(string rejectionReason)
        {
            if (StatusId != (int)Enums.MandatoryListStatus.WaitingApproval)
                throw new BusinessRuleException(Resources.SharedResources.ErrorMessages.NotAddedAuthorized);

            if (String.IsNullOrEmpty(rejectionReason))
                throw new BusinessRuleException(Resources.MandatoryListResources.ErrorMessages.RejectionReasonCannotBeEmpty);

            RejectionReason = rejectionReason;
            StatusId = (int)Enums.MandatoryListStatus.Rejected;
            EntityUpdated();
        }

        public void Delete()
        {
            if (StatusId != (int)Enums.MandatoryListStatus.UnderEstablishing)
                throw new BusinessRuleException(Resources.SharedResources.ErrorMessages.NotAddedAuthorized);

            IsActive = false;
            EntityUpdated();
        }

        public void RequestDelete()
        {
            if (StatusId != (int)Enums.MandatoryListStatus.Approved)
                throw new BusinessRuleException(Resources.SharedResources.ErrorMessages.NotAddedAuthorized);

            if (ChangeRequests.Any(a => a.StatusId == (int)Enums.MandatoryListChangeRequestStatus.WaitingApproval)
                && ChangeRequests.Any(a => a.StatusId == (int)Enums.MandatoryListChangeRequestStatus.Rejected))
                throw new BusinessRuleException(Resources.MandatoryListResources.ErrorMessages.ThereIsActiveChangeRequest);

            StatusId = (int)Enums.MandatoryListStatus.WaitingCancelApproval;
            EntityUpdated();
        }

        public void ApproveDelete()
        {
            if (StatusId != (int)Enums.MandatoryListStatus.WaitingCancelApproval)
                throw new BusinessRuleException(Resources.SharedResources.ErrorMessages.NotAddedAuthorized);

            StatusId = (int)Enums.MandatoryListStatus.Canceled;
            EntityUpdated();
        }

        public MandatoryListChangeRequest ApproveEdit()
        {
            if (StatusId != (int)Enums.MandatoryListStatus.Approved)
                throw new BusinessRuleException(Resources.SharedResources.ErrorMessages.NotAddedAuthorized);

            var changeRequestEntity = ChangeRequests.FirstOrDefault(a => a.StatusId == (int)Enums.MandatoryListChangeRequestStatus.WaitingApproval);

            if (changeRequestEntity == null)
                throw new BusinessRuleException(Resources.SharedResources.ErrorMessages.NotAddedAuthorized);

            changeRequestEntity.Approve();

            return changeRequestEntity;
        }

        public void RejectEdit(string rejectionReason)
        {
            if (StatusId != (int)Enums.MandatoryListStatus.Approved)
                throw new BusinessRuleException(Resources.SharedResources.ErrorMessages.NotAddedAuthorized);

            var changeRequestEntity = ChangeRequests.FirstOrDefault(a => a.StatusId == (int)Enums.MandatoryListChangeRequestStatus.WaitingApproval);

            if (changeRequestEntity == null)
                throw new BusinessRuleException(Resources.SharedResources.ErrorMessages.NotAddedAuthorized);

            changeRequestEntity.Reject(rejectionReason);
        }

        public void CloseEdit()
        {
            if (StatusId != (int)Enums.MandatoryListStatus.Approved)
                throw new BusinessRuleException(Resources.SharedResources.ErrorMessages.NotAddedAuthorized);

            var changeRequestEntity = ChangeRequests.FirstOrDefault(a => a.StatusId == (int)Enums.MandatoryListChangeRequestStatus.Rejected);

            if (changeRequestEntity == null)
                throw new BusinessRuleException(Resources.SharedResources.ErrorMessages.NotAddedAuthorized);

            changeRequestEntity.Close();
        }

        public void RejectDelete(string rejectionReason)
        {
            if (StatusId != (int)Enums.MandatoryListStatus.WaitingCancelApproval)
                throw new BusinessRuleException(Resources.SharedResources.ErrorMessages.NotAddedAuthorized);

            if (String.IsNullOrEmpty(rejectionReason))
                throw new BusinessRuleException(Resources.MandatoryListResources.ErrorMessages.RejectionReasonCannotBeEmpty);

            StatusId = (int)Enums.MandatoryListStatus.CancelRejected;
            RejectionReason = rejectionReason;
            EntityUpdated();
        }

        public void Reopen()
        {
            if (StatusId != (int)Enums.MandatoryListStatus.Rejected && StatusId != (int)Enums.MandatoryListStatus.CancelRejected)
                throw new BusinessRuleException(Resources.SharedResources.ErrorMessages.NotAddedAuthorized);

            if (StatusId == (int)Enums.MandatoryListStatus.CancelRejected)
                StatusId = (int)Enums.MandatoryListStatus.Approved;
            else
                StatusId = (int)Enums.MandatoryListStatus.UnderEstablishing;

            EntityUpdated();
        }
    }
}
