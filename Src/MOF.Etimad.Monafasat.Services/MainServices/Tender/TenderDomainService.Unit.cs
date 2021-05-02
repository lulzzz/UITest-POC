using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernal.Exceptions;
using MOF.Etimad.Monafasat.SharedKernel;
using System;
using System.Linq;

namespace MOF.Etimad.Monafasat.Services
{
    public partial class TenderDomainService
    {
        public void IsValidToOpenUnitStageByUnitSecretary(Tender tender)
        {
            if (tender == null)
                throw new AuthorizationException();
            if (tender.TenderStatusId != (int)Enums.TenderStatus.UnitStaging)
            {
                var tenderStatus = Resources.TenderResources.Messages.ResourceManager.GetString(nameof(Enums.TenderStatus.UnitStaging));
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.CanNotProceedStatusIs + tenderStatus);
            }
            if (tender.TenderUnitStatusId != (int)Enums.TenderUnitStatus.WaitingUnitSecretaryReview)
            {
                var tenderStatus = Resources.TenderResources.Messages.ResourceManager.GetString(nameof(Enums.TenderUnitStatus.UnderUnitReviewLevelOne));
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.CanNotProceedStatusIs + tenderStatus);
            }

        }

        public void IsValidToOpenUnitStageByUnitSecretaryLevelTwo(Tender tender)
        {
            if (tender == null)
                throw new AuthorizationException();
            if (tender.TenderStatusId != (int)Enums.TenderStatus.UnitStaging)
            {
                var tenderStatus = Resources.TenderResources.Messages.ResourceManager.GetString(nameof(Enums.TenderStatus.UnitStaging));
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.CanNotProceedStatusIs + tenderStatus);
            }
            if (tender.TenderUnitStatusId != (int)Enums.TenderUnitStatus.TenderTransferdToLevelTwo)
            {
                var tenderStatus = Resources.TenderResources.Messages.ResourceManager.GetString(nameof(Enums.TenderUnitStatus.TenderTransferdToLevelTwo));
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.CanNotProceedStatusIs + tenderStatus);
            }
        }

        public void IsValidToSendToLevelTwoFromUnitSecretaryLevelOne(Tender tender, string userName)
        {
            if (tender == null)
                throw new AuthorizationException();
            if (tender.TenderStatusId != (int)Enums.TenderStatus.UnitStaging)
            {
                var tenderStatus = Resources.TenderResources.Messages.ResourceManager.GetString(nameof(Enums.TenderStatus.UnitStaging));
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.CanNotProceedStatusIs + tenderStatus);
            }
            if (!(tender.TenderUnitStatusId == (int)Enums.TenderUnitStatus.WaitingUnitSecretaryReview || tender.TenderUnitStatusId == (int)Enums.TenderUnitStatus.UnderUnitReviewLevelOne))
            {
                var tenderStatus = Resources.TenderResources.Messages.ResourceManager.GetString(nameof(Enums.TenderUnitStatus.UnderUnitReviewLevelOne));
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.CanNotProceedStatusIs + tenderStatus);
            }
            if (string.IsNullOrEmpty(userName))
            {
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.PleaseSelectLevel2Spacialist);
            }
        }

        public void IsValidToReturnTenderToDataEntryForEdit(Tender tender)
        {
            if (tender == null)
                throw new AuthorizationException();
            if (tender.TenderStatusId != (int)Enums.TenderStatus.UnitStaging)
            {
                var tenderStatus = Resources.TenderResources.Messages.ResourceManager.GetString(nameof(Enums.TenderStatus.UnitStaging));
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.CanNotProceedStatusIs + tenderStatus);
            }
            if (tender.TenderUnitStatusId != (int)Enums.TenderUnitStatus.UnderUnitReviewLevelOne)
            {
                var tenderStatus = Resources.TenderResources.Messages.ResourceManager.GetString(nameof(Enums.TenderUnitStatus.UnderUnitReviewLevelOne));
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.CanNotProceedStatusIs + tenderStatus);
            }
        }

        public void IsValidToReturnTenderToDataEntryForEditLevelTwo(Tender tender)
        {
            if (tender == null)
                throw new AuthorizationException();
            if (tender.TenderStatusId != (int)Enums.TenderStatus.UnitStaging)
            {
                var tenderStatus = Resources.TenderResources.Messages.ResourceManager.GetString(nameof(Enums.TenderStatus.UnitStaging));
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.CanNotProceedStatusIs + tenderStatus);
            }
            if (tender.TenderUnitStatusId != (int)Enums.TenderUnitStatus.UnderUnitReviewLevelTwo)
            {
                var tenderStatus = Resources.TenderResources.Messages.ResourceManager.GetString(nameof(Enums.TenderUnitStatus.UnderUnitReviewLevelTwo));
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.CanNotProceedStatusIs + tenderStatus);
            }
        }

        public void IsValidToUpdateRejectTenderByUnitSecretary(Tender tender, string comment)
        {
            if (tender == null)
                throw new AuthorizationException();
            if (tender.TenderStatusId != (int)Enums.TenderStatus.UnitStaging)
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.MustBeInUnitStage);
            if (tender.TenderUnitStatusId != (int)Enums.TenderUnitStatus.UnderUnitReviewLevelOne)
            {
                var tenderStatus = Resources.TenderResources.Messages.ResourceManager.GetString(nameof(Enums.TenderUnitStatus.UnderUnitReviewLevelOne));
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.CanNotProceedStatusIs + tenderStatus);
            }
            if (string.IsNullOrEmpty(comment))
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.RejectReasonMustHaveValue);
        }

        public void IsValidToUpdateRejectTenderByUnitSecretaryLevelTwo(Tender tender, string comment)
        {
            if (tender == null)
                throw new AuthorizationException();
            if (tender.TenderStatusId != (int)Enums.TenderStatus.UnitStaging)
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.MustBeInUnitStage);
            if (tender.TenderUnitStatusId != (int)Enums.TenderUnitStatus.UnderUnitReviewLevelTwo)
            {
                var tenderStatus = Resources.TenderResources.Messages.ResourceManager.GetString(nameof(Enums.TenderUnitStatus.UnderUnitReviewLevelTwo));
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.CanNotProceedStatusIs + tenderStatus);
            }
            if (string.IsNullOrEmpty(comment))
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.RejectReasonMustHaveValue);
        }

        public void IsValidToUpdateApproveTenderByUnitSecretary(Tender tender)
        {
            if (tender == null)
                throw new AuthorizationException();
            if (tender.TenderStatusId != (int)Enums.TenderStatus.UnitStaging)
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.MustBeInUnitStage);
            if (tender.TenderUnitStatusId != (int)Enums.TenderUnitStatus.UnderUnitReviewLevelOne)
            {
                var tenderStatus = Resources.TenderResources.Messages.ResourceManager.GetString(nameof(Enums.TenderUnitStatus.UnderUnitReviewLevelOne));
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.CanNotProceedStatusIs + tenderStatus);
            }
        }

        public void IsValidToUpdateApproveTenderByUnitSecretaryLevelTwo(Tender tender)
        {
            if (tender == null)
                throw new AuthorizationException();
            if (tender.TenderStatusId != (int)Enums.TenderStatus.UnitStaging)
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.MustBeInUnitStage);
            if (tender.TenderUnitStatusId != (int)Enums.TenderUnitStatus.UnderUnitReviewLevelTwo)
            {
                var tenderStatus = Resources.TenderResources.Messages.ResourceManager.GetString(nameof(Enums.TenderUnitStatus.UnderUnitReviewLevelTwo));
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.CanNotProceedStatusIs + tenderStatus);
            }
        }

        public void IsValidToSendToApproveByUnitManager(Tender tender)
        {
            if (tender == null)
                throw new AuthorizationException();
            if (tender.TenderStatusId != (int)Enums.TenderStatus.UnitStaging)
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.MustBeInUnitStage);
            if (tender.TenderUnitStatusId != (int)Enums.TenderUnitStatus.UnderUnitReviewLevelOne)
            {
                var tenderStatus = Resources.TenderResources.Messages.ResourceManager.GetString(nameof(Enums.TenderUnitStatus.UnderUnitReviewLevelOne));
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.CanNotProceedStatusIs + tenderStatus);
            }
            if (tender.IsUnitSecreteryAccepted.Value && !tender.isValidToApproveByLastOfferPresentationDate())
            {
                throw new BusinessRuleException(Resources.TenderResources.Messages.CantSendToApprovePresentaionDatePassed);
            }
        }

        public void IsValidToSendToApproveFromLevelToByUnitManager(Tender tender)
        {
            if (tender == null)
                throw new AuthorizationException();
            if (tender.TenderStatusId != (int)Enums.TenderStatus.UnitStaging)
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.MustBeInUnitStage);
            if (tender.TenderUnitStatusId != (int)Enums.TenderUnitStatus.UnderUnitReviewLevelTwo)
            {
                var tenderStatus = Resources.TenderResources.Messages.ResourceManager.GetString(nameof(Enums.TenderUnitStatus.UnderUnitReviewLevelTwo));
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.CanNotProceedStatusIs + tenderStatus);
            }
            if (tender.IsUnitSecreteryAccepted.Value && !tender.isValidToApproveByLastOfferPresentationDate())
            {
                throw new BusinessRuleException(Resources.TenderResources.Messages.CantSendToApprovePresentaionDatePassed);
            }
        }

        public void IsValidToReviewTenderByUnitManager(Tender tender)
        {
            if (tender == null)
                throw new AuthorizationException();
            if (tender.TenderStatusId != (int)Enums.TenderStatus.UnitStaging)
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.MustBeInUnitStage);

            if (tender.TenderUnitStatusId != (int)Enums.TenderUnitStatus.WaitingManagerApprove)
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.TenderUnitStatusMustBeWaitingManagerApprove);
        }

        public void IsValidToUpdateApproveTenderByUnitManager(Tender tender)
        {
            if (tender == null)
                throw new AuthorizationException();
            if (tender.TenderStatusId != (int)Enums.TenderStatus.UnitStaging)
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.MustBeInUnitStage);

            if (tender.TenderUnitStatusId != (int)Enums.TenderUnitStatus.UnderManagerReviewing)
            {
                var tenderStatus = Resources.TenderResources.Messages.ResourceManager.GetString(nameof(Enums.TenderUnitStatus.UnderManagerReviewing));
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.CanNotProceedStatusIs + tenderStatus);
            };
            if (tender.IsUnitSecreteryAccepted.Value && !tender.isValidToApproveByLastOfferPresentationDate())
            {
                throw new BusinessRuleException(Resources.TenderResources.Messages.CantApprovePresentaionDatePassed);
            }
        }

        public void IsValidToUpdateRejectTenderByUnitManager(Tender tender, string comment)
        {
            if (tender == null)
                throw new AuthorizationException();
            if (tender.TenderStatusId != (int)Enums.TenderStatus.UnitStaging)
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.MustBeInUnitStage);

            if (tender.TenderUnitStatusId != (int)Enums.TenderUnitStatus.UnderManagerReviewing)
            {
                var tenderStatus = Resources.TenderResources.Messages.ResourceManager.GetString(nameof(Enums.TenderUnitStatus.UnderManagerReviewing));
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.CanNotProceedStatusIs + tenderStatus);
            };
            if (string.IsNullOrEmpty(comment))
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.RejectReasonMustHaveValue);
        }

        public void IsValidToReOpenTenderByUnitSecretary(Tender tender)
        {
            if (tender == null)
                throw new AuthorizationException();
            if (tender.TenderStatusId != (int)Enums.TenderStatus.UnitStaging)
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.MustBeInUnitStage);
            if (tender.TenderUnitStatusId != (int)Enums.TenderUnitStatus.RejectedByManager)
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.TenderUnitStatusMustBeRejectedByManager);

            if (!(tender.TenderUnitAssigns.Where(a => a.UnitSpecialistLevel == (int)Enums.UnitSpecialistLevel.UnitSpecialistLevelOne || a.UnitSpecialistLevel == (int)Enums.UnitSpecialistLevel.UnitSpecialistLevelTwo).OrderByDescending(a =>
              a.TenderUnitAssignId).FirstOrDefault(a => a.IsActive == true && a.IsCurrentlyAssigned == true).UserProfileId == Convert.ToInt32(_httpContextAccessor.HttpContext.User.UserId())))
            {
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.TenderCanOnlyReOpenedByLatestAssignedSecretary);
            }
        }

        public void IsValidToSendToNewUserUnitBusinessManagement(Tender tender, UserProfile user)
        {
            if (tender == null)
                throw new AuthorizationException();
            if (user == null)
                throw new AuthorizationException();
        }
    }
}
