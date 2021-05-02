using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernal.Exceptions;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.SharedKernel.Exceptions;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.SharedKernel;
using System.Threading.Tasks;
using static MOF.Etimad.Monafasat.SharedKernel.Enums;

namespace MOF.Etimad.Monafasat.Services
{
    public partial class TenderAppService
    {
        public async Task<QueryResult<VROTenderCheckingAndOpeningModel>> FindTendersForVROCheckingAndOpeningStageBySearchCriteria(TenderSearchCriteria criteria)
        {
            criteria.TenderStatusIds.Add((int)Enums.TenderStatus.Approved);
            criteria.TenderStatusIds.Add((int)Enums.TenderStatus.VROOffersTechnicalChecking);
            criteria.TenderStatusIds.Add((int)Enums.TenderStatus.VROOffersCheckingAndTechnicalEval);
            criteria.TenderStatusIds.Add((int)Enums.TenderStatus.VROOffersTechnicalCheckingApproved);
            criteria.TenderStatusIds.Add((int)Enums.TenderStatus.VROOffersTechnicalCheckingPending);
            criteria.TenderStatusIds.Add((int)Enums.TenderStatus.VROOffersTechnicalCheckingRejected);

            criteria.TenderStatusIds.Add((int)Enums.TenderStatus.VROOffersFinancialChecking);
            criteria.TenderStatusIds.Add((int)Enums.TenderStatus.VROFinancialCheckingOpening);
            criteria.TenderStatusIds.Add((int)Enums.TenderStatus.VROOffersFinancialCheckingPending);
            criteria.TenderStatusIds.Add((int)Enums.TenderStatus.VROOffersFinancialCheckingApproved);
            criteria.TenderStatusIds.Add((int)Enums.TenderStatus.VROOffersFinancialCheckingRejected);


            if (string.IsNullOrEmpty(criteria.Sort))
            {
                criteria.Sort = "SubmitionDate";
                criteria.SortDirection = "DESC";
            }
            return await _tenderQueries.FindTendersForVROCheckingAndOpeningStageBySearchCriteria(criteria);
        }

        public async Task<VROTenderOffersModel> FindVROTenderOfferDetailsByTenderIdForCheckStage(string tenderIdString, int userId)
        {
            int tenderId = Util.Decrypt(tenderIdString);
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            Check.ArgumentNotNullOrEmpty(nameof(userId), userId.ToString());
            var tender = await _tenderQueries.FindVROTenderOfferDetailsByTenderIdForCheckStage(tenderId, userId);
            if (tender == null)
                throw new UnHandledAccessException(Resources.SharedResources.ErrorMessages.YouHaveNoAccess);
            return tender;
        }
        public async Task StartVROTenderOffersCheckingAsync(string tenderIdString)
        {
            int tenderId = Util.Decrypt(tenderIdString);
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            Tender tender = await _tenderQueries.FindTenderForOpenCheckStageByTenderId(tenderId);
            IsValidStartVROTenderOffersCheckingAsync(tender);
            tender.UpdateTenderStatus(Enums.TenderStatus.VROOffersTechnicalChecking, "", _httpContextAccessor.HttpContext.User.UserId(), TenderActions.StartVROOffersTechnicalChecking);
            await _tenderCommands.UpdateAsync(tender);
        }

        private void IsValidStartVROTenderOffersCheckingAsync(Tender tender)
        {
            if (tender == null)
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.TenderNotExist);
            if (tender.TenderStatusId != (int)Enums.TenderStatus.Approved)
            {
                var tenderStatus = Resources.TenderResources.Messages.ResourceManager.GetString(nameof(Enums.TenderStatus.Approved));
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.CanNotProceedStatusIs + tenderStatus);
            }
        }

        #region Technical

        public async Task SendVROTenderOffersTechnicalCheckingToApproveAsync(string tenderIdString)
        {
            int tenderId = Util.Decrypt(tenderIdString);
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            Tender tender = await _tenderQueries.FindTenderForOpenCheckStageByTenderId(tenderId);
            IsValidToSendVROTenderOffersTechnicalCheckingToApproveAsync(tender);
            tender.UpdateTenderStatus(Enums.TenderStatus.VROOffersTechnicalCheckingPending, "", _httpContextAccessor.HttpContext.User.UserId(), TenderActions.SendVROTenderOffersTechnicalCheckingToApproveAsync);
            await _tenderCommands.UpdateAsync(tender);

            NotificationArguments NotificationArgument = new NotificationArguments
            {
                BodyEmailArgs = new object[] { tender.TenderNumber },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { tender.TenderNumber },
                SMSArgs = new object[] { tender.TenderNumber }
            };

            MainNotificationTemplateModel mainNotificationTemplate = new MainNotificationTemplateModel(
                NotificationArgument,
                $"Tender/VROTenderChecking?tenderIdString={Util.Encrypt(tender.TenderId)}",
                NotificationEntityType.Tender,
                tender.TenderId.ToString(),
                null,
                tender.VROCommitteeId);

            await _notificationAppService.SendNotificationForCommitteeUsers(
                NotificationOperations.VROCheckManager.OperationsOnTheTender.sendTechnicalEvaluationOfCompetitionToApprove,
                tender.VROCommitteeId,
                mainNotificationTemplate);
        }
        private void IsValidToSendVROTenderOffersTechnicalCheckingToApproveAsync(Tender tender)
        {
            if (tender == null)
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.TenderNotExist);
            if (tender.TenderStatusId != (int)Enums.TenderStatus.VROOffersCheckingAndTechnicalEval)
            {
                var tenderStatus = Resources.TenderResources.Messages.ResourceManager.GetString(nameof(Enums.TenderStatus.VROOffersCheckingAndTechnicalEval));
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.CanNotProceedStatusIs + tenderStatus);
            }
        }
        public async Task ReopenVROTenderOffersTechnicalCheckingAsync(string tenderIdString)
        {
            int tenderId = Util.Decrypt(tenderIdString);
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            Tender tender = await _tenderQueries.FindTenderForOpenCheckStageByTenderId(tenderId);
            IsValidToReopenVROTenderOffersTechnicalCheckingAsync(tender);
            tender.UpdateTenderStatus(Enums.TenderStatus.VROOffersCheckingAndTechnicalEval, "", _httpContextAccessor.HttpContext.User.UserId(), TenderActions.ReopenVROTenderOffersTechnicalCheckingAsync); await _tenderCommands.UpdateAsync(tender);
        }

        private void IsValidToReopenVROTenderOffersTechnicalCheckingAsync(Tender tender)
        {
            if (tender == null)
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.TenderNotExist);
            if (tender.TenderStatusId != (int)Enums.TenderStatus.VROOffersTechnicalCheckingRejected)
            {
                var tenderStatus = Resources.TenderResources.Messages.ResourceManager.GetString(nameof(Enums.TenderStatus.VROOffersTechnicalCheckingRejected));
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.CanNotProceedStatusIs + tenderStatus);
            }
        }
        public async Task ApproveVROTenderOffersTechnicalCheckingAsync(string tenderIdString)
        {
            int tenderId = Util.Decrypt(tenderIdString);
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            Tender tender = await _tenderQueries.GetTenderByIdWithoutAnyIncluds(tenderId);
            IsValidToApproveOrRejectVROTenderOffersTechnicalCheckingAsync(tender);
            await RejectTenderCancelRequestWhileTenderApproval(tender, RoleNames.OffersOpeningAndCheckSecretary);
            tender.UpdateTenderStatus(Enums.TenderStatus.VROOffersTechnicalCheckingApproved, "", _httpContextAccessor.HttpContext.User.UserId(), TenderActions.ApproveVROTenderOffersTechnicalCheckingAsync);

            NotificationArguments NotificationArgument = new NotificationArguments
            {
                BodyEmailArgs = new object[] { tender.TenderNumber },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { tender.TenderNumber },
                SMSArgs = new object[] { tender.TenderNumber }
            };

            MainNotificationTemplateModel mainNotificationTemplate = new MainNotificationTemplateModel(
                NotificationArgument,
                $"Tender/VROTenderChecking?tenderIdString={Util.Encrypt(tender.TenderId)}",
                NotificationEntityType.Tender,
                tender.TenderId.ToString(),
                null,
                tender.VROCommitteeId);

            await _notificationAppService.SendNotificationForCommitteeUsers(
                NotificationOperations.VROCheckSecretary.OperationsOnTheTender.TenderOffersTechnicalCheckingApproved,
                tender.VROCommitteeId,
                mainNotificationTemplate);

            _genericCommandRepository.Update(tender);
            await _genericCommandRepository.SaveAsync();

        }

      
        public async Task RejectVROTenderOffersTechnicalCheckingAsync(string tenderIdString, string rejectionReason)
        {
            int tenderId = Util.Decrypt(tenderIdString);
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            Tender tender = await _tenderQueries.FindTenderForOpenCheckStageByTenderId(tenderId);
            IsValidToApproveOrRejectVROTenderOffersTechnicalCheckingAsync(tender);
            tender.UpdateTenderStatus(Enums.TenderStatus.VROOffersTechnicalCheckingRejected, rejectionReason, _httpContextAccessor.HttpContext.User.UserId(), TenderActions.RejectVROTenderOffersTechnicalCheckingAsync);
            await _tenderCommands.UpdateAsync(tender);

            NotificationArguments NotificationArgument = new NotificationArguments
            {
                BodyEmailArgs = new object[] { tender.TenderNumber },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { tender.TenderNumber },
                SMSArgs = new object[] { tender.TenderNumber }
            };

            MainNotificationTemplateModel mainNotificationTemplate = new MainNotificationTemplateModel(
                NotificationArgument,
                $"Tender/VROTenderChecking?tenderIdString={Util.Encrypt(tender.TenderId)}",
                NotificationEntityType.Tender,
                tender.TenderId.ToString(),
                null,
                tender.VROCommitteeId);

            await _notificationAppService.SendNotificationForCommitteeUsers(
                NotificationOperations.VROCheckSecretary.OperationsOnTheTender.TenderOffersTechnicalCheckingRejected,
                tender.VROCommitteeId,
                mainNotificationTemplate);
        }


        private void IsValidToApproveOrRejectVROTenderOffersTechnicalCheckingAsync(Tender tender)
        {
            if (tender == null)
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.TenderNotExist);
            if (tender.TenderStatusId != (int)Enums.TenderStatus.VROOffersTechnicalCheckingPending)
            {
                var tenderStatus = Resources.TenderResources.Messages.ResourceManager.GetString(nameof(Enums.TenderStatus.VROOffersTechnicalCheckingPending));
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.CanNotProceedStatusIs + tenderStatus);
            }
        }
        #endregion Technical

        #region Financial

        public async Task StartVROTenderOffersFinancialCheckingAsync(string tenderIdString, decimal estimatedValue)
        {
            int tenderId = Util.Decrypt(tenderIdString);
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            Tender tender = await _tenderQueries.FindTenderForOpenCheckStageByTenderId(tenderId);
            IsValidStartVROTenderOffersFinancialCheckingAsync(tender);
            if (tender.VRORelatedBranchId == null)
            {
                tender.UpdateEstimatedValue(estimatedValue);
            }
            tender.UpdateTenderStatus(Enums.TenderStatus.VROFinancialCheckingOpening, "", _httpContextAccessor.HttpContext.User.UserId(), TenderActions.StartVROOffersTechnicalChecking);
            await _tenderCommands.UpdateAsync(tender);
        }

        private void IsValidStartVROTenderOffersFinancialCheckingAsync(Tender tender)
        {
            if (tender == null)
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.TenderNotExist);
            if (tender.TenderStatusId != (int)Enums.TenderStatus.VROOffersTechnicalCheckingApproved)
            {
                var tenderStatus = Resources.TenderResources.Messages.ResourceManager.GetString(nameof(Enums.TenderStatus.VROOffersTechnicalCheckingApproved));
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.CanNotProceedStatusIs + tenderStatus);
            }
        }

        public async Task SendVROTenderOffersFinanceCheckingToApproveAsync(string tenderIdString)
        {
            int tenderId = Util.Decrypt(tenderIdString);
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            Tender tender = await _tenderQueries.FindTenderForOpenCheckStageByTenderId(tenderId);
            IsValidToSendVROTenderOffersFinanceCheckingToApproveAsync(tender);
            tender.UpdateTenderStatus(Enums.TenderStatus.VROOffersFinancialCheckingPending, "", _httpContextAccessor.HttpContext.User.UserId(), TenderActions.SendVROTenderOffersFinanceCheckingToApproveAsync);
            await _tenderCommands.UpdateAsync(tender);

            NotificationArguments NotificationArgument = new NotificationArguments
            {
                BodyEmailArgs = new object[] { tender.TenderNumber },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { tender.TenderNumber },
                SMSArgs = new object[] { tender.TenderNumber }
            };

            MainNotificationTemplateModel mainNotificationTemplate = new MainNotificationTemplateModel(
                NotificationArgument,
                $"Tender/VROTenderChecking?tenderIdString={Util.Encrypt(tender.TenderId)}",
                NotificationEntityType.Tender,
                tender.TenderId.ToString(),
                null,
                tender.VROCommitteeId);

            await _notificationAppService.SendNotificationForCommitteeUsers(
                NotificationOperations.VROCheckManager.OperationsOnTheTender.sendFinancialEvaluationOfCompetitionToApprove,
                tender.VROCommitteeId,
                mainNotificationTemplate);
        }

        private void IsValidToSendVROTenderOffersFinanceCheckingToApproveAsync(Tender tender)
        {
            if (tender == null)
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.TenderNotExist);
            if (tender.TenderStatusId != (int)Enums.TenderStatus.VROOffersFinancialChecking)
            {
                var tenderStatus = Resources.TenderResources.Messages.ResourceManager.GetString(nameof(Enums.TenderStatus.VROOffersFinancialChecking));
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.CanNotProceedStatusIs + tenderStatus);
            }
        }

        public async Task ReopenVROTenderOffersFinancialCheckingAsync(string tenderIdString)
        {
            int tenderId = Util.Decrypt(tenderIdString);
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            Tender tender = await _tenderQueries.FindTenderForOpenCheckStageByTenderId(tenderId);
            IsValidToReopenVROTenderOffersFinancialCheckingAsync(tender);
            tender.UpdateTenderStatus(Enums.TenderStatus.VROOffersFinancialChecking, "", _httpContextAccessor.HttpContext.User.UserId(), TenderActions.ReopenVROTenderOffersFinancialCheckingAsync); 
            await _tenderCommands.UpdateAsync(tender);
        }

        private void IsValidToReopenVROTenderOffersFinancialCheckingAsync(Tender tender)
        {
            if (tender == null)
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.TenderNotExist);
            if (tender.TenderStatusId != (int)Enums.TenderStatus.VROOffersFinancialCheckingRejected)
            {
                var tenderStatus = Resources.TenderResources.Messages.ResourceManager.GetString(nameof(Enums.TenderStatus.VROOffersFinancialCheckingRejected));
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.CanNotProceedStatusIs + tenderStatus);
            }
        }

        public async Task ApproveVROTenderOffersFinanceCheckingAsync(string tenderIdString)
        {
            int tenderId = Util.Decrypt(tenderIdString);
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            Tender tender = await _tenderQueries.GetTenderByIdWithoutAnyIncluds(tenderId);
            IsValidToApproveOrRejectVROTenderOffersFinanceCheckingAsync(tender);

            await RejectTenderCancelRequestWhileTenderApproval(tender, RoleNames.OffersOpeningAndCheckSecretary);

            tender.UpdateTenderStatus(Enums.TenderStatus.VROOffersFinancialCheckingApproved, "", _httpContextAccessor.HttpContext.User.UserId(), TenderActions.ApproveVROTenderOffersFinanceCheckingAsync);

            NotificationArguments NotificationArgument = new NotificationArguments
            {
                BodyEmailArgs = new object[] { tender.TenderNumber },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { tender.TenderNumber },
                SMSArgs = new object[] { tender.TenderNumber }
            };

            MainNotificationTemplateModel mainNotificationTemplate = new MainNotificationTemplateModel(
                NotificationArgument,
                $"Tender/VROTenderChecking?tenderIdString={Util.Encrypt(tender.TenderId)}",
                NotificationEntityType.Tender,
                tender.TenderId.ToString(),
                null,
                tender.VROCommitteeId);

            await _notificationAppService.SendNotificationForCommitteeUsers(
                NotificationOperations.VROCheckSecretary.OperationsOnTheTender.TenderOffersFinancialCheckingApproved,
                tender.VROCommitteeId,
                mainNotificationTemplate);

            _genericCommandRepository.Update(tender);
            await _genericCommandRepository.SaveAsync();
        }
 

        public async Task RejectVROTenderOffersFinanceCheckingAsync(string tenderIdString, string rejectionReason)
        {
            int tenderId = Util.Decrypt(tenderIdString);
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            Tender tender = await _tenderQueries.FindTenderForOpenCheckStageByTenderId(tenderId);
            IsValidToApproveOrRejectVROTenderOffersFinanceCheckingAsync(tender);
            tender.UpdateTenderStatus(Enums.TenderStatus.VROOffersFinancialCheckingRejected, rejectionReason, _httpContextAccessor.HttpContext.User.UserId(), TenderActions.RejectVROTenderOffersFinanceCheckingAsync);
            await _tenderCommands.UpdateAsync(tender);

            NotificationArguments NotificationArgument = new NotificationArguments
            {
                BodyEmailArgs = new object[] { tender.TenderNumber },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { tender.TenderNumber },
                SMSArgs = new object[] { tender.TenderNumber }
            };

            MainNotificationTemplateModel mainNotificationTemplate = new MainNotificationTemplateModel(
                NotificationArgument,
                $"Tender/VROTenderChecking?tenderIdString={Util.Encrypt(tender.TenderId)}",
                NotificationEntityType.Tender,
                tender.TenderId.ToString(),
                null,
                tender.VROCommitteeId);

            await _notificationAppService.SendNotificationForCommitteeUsers(
                NotificationOperations.VROCheckSecretary.OperationsOnTheTender.TenderOffersFinancialCheckingRejected,
                tender.VROCommitteeId,
                mainNotificationTemplate);
        }

        public void IsValidToApproveOrRejectVROTenderOffersFinanceCheckingAsync(Tender tender)
        {
            if (tender == null)
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.TenderNotExist);
            if (tender.TenderStatusId != (int)Enums.TenderStatus.VROOffersFinancialCheckingPending)
            {
                var tenderStatus = Resources.TenderResources.Messages.ResourceManager.GetString(nameof(Enums.TenderStatus.VROOffersFinancialCheckingPending));
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.CanNotProceedStatusIs + tenderStatus);
            }
        }
        #endregion Financial

    }
}
