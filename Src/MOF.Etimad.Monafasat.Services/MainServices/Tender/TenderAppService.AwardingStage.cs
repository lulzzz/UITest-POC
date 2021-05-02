using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernal.Exceptions;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.SharedKernel.Exceptions;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static MOF.Etimad.Monafasat.SharedKernel.Enums;

namespace MOF.Etimad.Monafasat.Services
{
    public partial class TenderAppService
    {
        public async Task AwardTenderOffers(int tenderId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            Tender tender = await _tenderQueries.GetTenderByIdWithoutAnyIncluds(tenderId);
            IsValidToStartAwardTender(tender);
            tender.UpdateTenderStatus(Enums.TenderStatus.OffersAwarding, "", _httpContextAccessor.HttpContext.User.UserId(), TenderActions.UpdateTender);
            await _tenderCommands.UpdateAsync(tender);
        }

        private void IsValidToStartAwardTender(Tender tender)
        {
            if (tender == null)
                throw new UnHandledAccessException(Resources.SharedResources.ErrorMessages.YouHaveNoAccess);
            if (tender.TenderStatusId != (int)Enums.TenderStatus.OffersCheckedConfirmed
                && tender.TenderStatusId != (int)Enums.TenderStatus.OffersAwardedRejected && tender.TenderStatusId != (int)Enums.TenderStatus.DirectPurchaseOffersCheckingApproved
                && tender.TenderStatusId != (int)Enums.TenderStatus.BiddingApproved && tender.TenderStatusId != (int)Enums.TenderStatus.OffersFinancialOfferCheckingApproved
                && tender.TenderStatusId != (int)Enums.TenderStatus.VROOffersFinancialCheckingApproved && tender.TenderStatusId != (int)Enums.TenderStatus.OffersTechnicalCheckingConfirmed)
            {
                var tenderStatus = Resources.TenderResources.Messages.ResourceManager.GetString(nameof(Enums.TenderStatus.OffersCheckedConfirmed));
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.CanNotProceedStatusIs + tenderStatus);
            }
        }

        public async Task SendAwardTenderToApprove(int tenderId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            Tender tender = await _tenderQueries.GetTenderByIdWithoutAnyIncluds(tenderId);
            await IsValidToSendAwardTenderToApprove(tender);
            tender.UpdateTenderStatus(Enums.TenderStatus.OffersAwardedPending, "", _httpContextAccessor.HttpContext.User.UserId(), TenderActions.sendTenderForAwarding);
            NotificationArguments NotificationArguments = new NotificationArguments
            {
                BodyEmailArgs = new object[] { "", tender.ReferenceNumber, tender.Purpose,
                        tender.LastEnqueriesDate?.ToString("dd/MM/yyyy hh:mm tt"), tender.LastOfferPresentationDate?.ToString("dd/MM/yyyy hh:mm tt"),
                       tender.OffersOpeningDate == null ? Resources.SharedResources.DisplayInputs.NotFound : tender.OffersOpeningDate?.ToString("dd/MM/yyyy"), tender.OffersOpeningDate == null ? Resources.SharedResources.DisplayInputs.NotFound : tender.OffersOpeningDate?.ToString("hh:mm tt")  },

                PanelArgs = new object[] { tender.ReferenceNumber },
                SMSArgs = new object[] { tender.ReferenceNumber }
            };
            MainNotificationTemplateModel templetModel = new MainNotificationTemplateModel(NotificationArguments, $"Tender/TenderAwarding?tenderIdString={Util.Encrypt(tender.TenderId)}", NotificationEntityType.Tender, tender.TenderId.ToString(), null, tender.OffersCheckingCommitteeId);
            await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.OffersCheckManager.OperationsNeedToBeAccredited.tenderawardingwaitingapproval, tender.OffersCheckingCommitteeId, templetModel);
            await _tenderCommands.UpdateAsync(tender);
        }

        public async Task SendAwardTenderToInitialApprove(int tenderId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            Tender tender = await _tenderQueries.FindTenderByIdForAwarding(tenderId);
            await IsValidToSendInitailAwardTenderToApprove(tender);
            if (tender.TenderTypeId != (int)Enums.TenderType.CurrentTender && tender.TenderTypeId != (int)Enums.TenderType.CurrentDirectPurchase && tender.TenderTypeId != (int)Enums.TenderType.NationalTransformationProjects)
            {
                tender.UpdateTenderStatus(Enums.TenderStatus.OffersInitialAwardedPending, "", _httpContextAccessor.HttpContext.User.UserId(), TenderActions.SendAwardTenderToInitialApprove);
            }
            else
            {
                tender.UpdateTenderStatus(Enums.TenderStatus.OffersAwardedPending, "", _httpContextAccessor.HttpContext.User.UserId(), TenderActions.sendTenderForAwarding);
            }
            await NotificationAfterSendAwardingToApproval(tender);
            await _tenderCommands.UpdateAsync(tender);
        }
        public async Task IsTenderHasPendingRequestsOrNoExeclusionReasons(int tenderId)
        {
            await CheckForActiveNegotiationOrExtendOfferValidityRequsts(tenderId);
            Tender tender = await _tenderQueries.FindTenderByIdForAwarding(tenderId);
            await IsAllExecludedSupplierHasNoExclussionReason(tender);
        }

        public async Task<int> IsSupplierPassedForTenderAwarding(int tenderId, string cr, string agencyCode)
        {
            var result = (int)AwardingReturnType.HasValidPreQualification;
            Tender tender = await _tenderQueries.FindTenderForAwarding(tenderId);
            List<Tender> postQualifications = tender.PostQualifications.Where(p => p.IsActive == true).ToList();

            if (tender.AgencyCode != agencyCode)
                throw new UnHandledAccessException();
            var user = _httpContextAccessor.HttpContext.User;
            if (!(user.IsInRole(RoleNames.OffersOpeningAndCheckSecretary) || user.IsInRole(RoleNames.OffersCheckSecretary) || user.IsInRole(RoleNames.OffersPurchaseSecretary) || (user.IsInRole(RoleNames.OffersPurchaseManager) && user.UserId() == tender.DirectPurchaseMemberId)))
                throw new UnHandledAccessException();
            if (tender.TenderTypeId != (int)Enums.TenderType.CurrentTender && tender.TenderTypeId != (int)Enums.TenderType.CurrentDirectPurchase && tender.TenderTypeId != (int)Enums.TenderType.NationalTransformationProjects)
            {
                var isOptionalReasonForDirectpurchase = tender.ReasonForPurchaseTenderTypeId == (int)Enums.ReasonForPurchaseTenderType.BusinessesAndPurchasesLessThanOneHundredThousand || tender.ReasonForPurchaseTenderTypeId == (int)Enums.ReasonForPurchaseTenderType.Emergency;

                if ((tender.TenderTypeId == (int)Enums.TenderType.NewDirectPurchase && isOptionalReasonForDirectpurchase) || tender.TenderTypeId == (int)Enums.TenderType.Competition)
                {
                    result = CheckNewDirectPurchaseAndCompetition(tender, postQualifications, cr);
                }
                else
                {
                    result = CheckOtherTendersForAwarding(tender, postQualifications, cr);
                }
            }
            return result;
        }

        private int CheckOtherTendersForAwarding(Tender tender, List<Tender> postQualifications, string cr)
        {
            var days = int.Parse(_rootConfiguration.AwardingConfiguration.QualifationsValidityDays);

            var result = (int)AwardingReturnType.HasValidPreQualification;

            if ((tender.PreQualification == null || !tender.PreQualification.QualificationFinalResults.Any(q => q.QualificationLookupId == (int)Enums.QualificationResultLookup.Succeeded && q.IsActive == true)) && !postQualifications.Any(x => x.QualificationFinalResults.Any(c => c.SupplierSelectedCr == cr && c.IsActive == true)))
            {
                result = (int)AwardingReturnType.NoPreQualificationNoPostQualificatoin;
                return result;
            }
            if (tender.PreQualification != null && !postQualifications.Any())
            {
                if ((DateTime.Now - tender.PreQualification.TenderHistories.FirstOrDefault(h => h.StatusId == (int)Enums.TenderStatus.DocumentCheckConfirmed).CreatedAt).TotalDays > days)
                {
                    result = (int)AwardingReturnType.HasExpiredPreQualification;
                    return result;
                }
                else if (tender.PreQualification.QualificationFinalResults.Any(q => q.QualificationLookupId != (int)QualificationResultLookup.Succeeded && q.SupplierSelectedCr == cr && q.IsActive == true))
                {
                    result = (int)AwardingReturnType.SupplierFailedInPreQualification;
                }
            }
            if (postQualifications.Any() && !postQualifications.Any(p => p.TenderHistories.Any(h => h.StatusId == (int)Enums.TenderStatus.Approved)))
            {
                result = (int)AwardingReturnType.HasPendingPostQualification;
            }
            if (postQualifications.Any(p => p.TenderStatusId != (int)Enums.TenderStatus.DocumentCheckConfirmed && p.TenderHistories.Any(h => h.StatusId == (int)Enums.TenderStatus.Approved)))
            {
                result = (int)AwardingReturnType.HasApprovedPostQualification;
            }
            if (postQualifications.Any(p => p.TenderStatusId == (int)Enums.TenderStatus.DocumentCheckConfirmed))
            {
                if (postQualifications.Any(p => p.QualificationFinalResults.Any(q => q.QualificationLookupId == (int)QualificationResultLookup.Succeeded && q.SupplierSelectedCr == cr && q.IsActive == true)))
                {
                    result = (int)AwardingReturnType.SupplierPassedInPostQualification;
                }
                else if (postQualifications.Any(p => p.QualificationFinalResults.Any(q => q.QualificationLookupId == (int)Enums.QualificationResultLookup.Failed && q.SupplierSelectedCr == cr && q.IsActive == true)))
                {
                    result = (int)AwardingReturnType.SupplierFailedInPostQualification;
                }
                else if (tender.PreQualification == null && !postQualifications.Any(x => x.QualificationFinalResults.Any(c => c.SupplierSelectedCr == cr && c.IsActive == true)))
                {
                    result = (int)AwardingReturnType.NoPreQualificationNoPostQualificatoin;
                }
            }

            return result;
        }
        private int CheckNewDirectPurchaseAndCompetition(Tender tender, List<Tender> postQualifications, string cr)
        {
            var result = (int)AwardingReturnType.NoPreQualificationNoPostQualificatoin;
            if (postQualifications.Any() && !postQualifications.Any(p => p.TenderHistories.Any(h => h.StatusId == (int)Enums.TenderStatus.Approved)))
            {
                result = (int)AwardingReturnType.HasPendingPostQualification;
            }
            if (postQualifications.Any(p => !p.QualificationFinalResults.Any(f => f.SupplierSelectedCr == cr && f.IsActive == true) && p.TenderStatusId != (int)Enums.TenderStatus.DocumentCheckConfirmed && p.TenderHistories.Any(h => h.StatusId == (int)Enums.TenderStatus.Approved)))
            {
                throw new BusinessRuleException("يوجد تأهيل لاحق معتمد ويجب تأهيل المورد المراد الترسية عليه");
            }
            if (postQualifications.Any(p => p.TenderStatusId == (int)Enums.TenderStatus.DocumentCheckConfirmed))
            {
                if (postQualifications.Any(p => p.QualificationFinalResults.Any(q => q.QualificationLookupId == (int)Enums.QualificationResultLookup.Succeeded && q.SupplierSelectedCr == cr && q.IsActive == true)))
                {
                    result = (int)AwardingReturnType.SupplierPassedInPostQualification;
                }
                else if (postQualifications.Any(p => p.QualificationFinalResults.Any(q => q.QualificationLookupId == (int)Enums.QualificationResultLookup.Failed && q.SupplierSelectedCr == cr && q.IsActive == true)))
                {
                    result = (int)AwardingReturnType.SupplierFailedInPostQualification;
                }
                else if (tender.PreQualification == null && !postQualifications.Any(x => x.QualificationFinalResults.Any(c => c.SupplierSelectedCr == cr && c.IsActive == true)))
                {
                    result = (int)AwardingReturnType.NoPreQualificationNoPostQualificatoin;
                }
            }
            return result;
        }

        public async Task<int> CheckSendTenderToApproveAwarding(int tenderId, List<string> crs, string agencyCode)
        {

            var result = (int)AwardingReturnType.SupplierHasNoPostQualification;
            Tender tender = await _tenderQueries.FindTenderForAwarding(tenderId);
            List<Tender> postQualifications = tender.PostQualifications.Where(p => p.IsActive == true).ToList();

            if (tender.PreQualification != null && !postQualifications.Any())
            {
                var days = int.Parse(_rootConfiguration.AwardingConfiguration.QualifationsValidityDays);
                if ((DateTime.Now - tender.PreQualification.TenderHistories.FirstOrDefault(h => h.StatusId == (int)Enums.TenderStatus.DocumentCheckConfirmed).CreatedAt).TotalDays > days)
                {
                    result = (int)AwardingReturnType.HasExpiredPreQualification;
                    return result;
                }
            }
            if (!postQualifications.Any())
            {
                result = (int)AwardingReturnType.SupplierHasNoPostQualification; // سوف يتم الترسية على المورد دون اجراء تأهيل لاحق
            }
            if (postQualifications.Any() && !postQualifications.Any(p => p.TenderHistories.Any(h => h.StatusId == (int)Enums.TenderStatus.Approved)))
            {
                result = (int)AwardingReturnType.HasPendingPostQualification; // سوف يتم الترسية على المورد دون اجراء تأهيل لاحق -- يوجد تاهيل لاحق تحت الانشاء 
            }
            if (postQualifications.Any(p => p.TenderHistories.Any(h => h.StatusId == (int)Enums.TenderStatus.Approved)))
            {
                result = (int)AwardingReturnType.HasApprovedPostQualification;
            }
            if (postQualifications.Any(p => p.TenderStatusId == (int)Enums.TenderStatus.DocumentCheckConfirmed))
            {
                foreach (var cr in crs)
                    if (!postQualifications.SelectMany(a => a.QualificationFinalResults).Any(a => a.QualificationLookupId == (int)Enums.QualificationResultLookup.Succeeded && a.SupplierSelectedCr == cr && a.IsActive == true))
                        result = (int)AwardingReturnType.SupplierFailedInPostQualification;
                if (result != (int)AwardingReturnType.SupplierFailedInPostQualification)
                    result = (int)AwardingReturnType.SupplierPassedInPostQualification;
            }
            return result;
        }

        private async Task IsValidToSendInitailAwardTenderToApprove(Tender tender)
        {

            if (tender.TenderStatusId != (int)Enums.TenderStatus.OffersAwarding)
            {
                var tenderStatus = Resources.TenderResources.Messages.ResourceManager.GetString(nameof(Enums.TenderStatus.OffersAwarding));
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.CanNotProceedStatusIs + tenderStatus);
            }
            if (!(tender.TenderTypeId == (int)Enums.TenderType.CurrentTender || tender.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase || tender.TenderTypeId == (int)Enums.TenderType.NationalTransformationProjects))
            {
                await CheckForActiveNegotiationOrExtendOfferValidityRequsts(tender.TenderId);
                await IsAllExecludedSupplierHasNoExclussionReason(tender);
            }
        }

        private async Task IsAllExecludedSupplierHasNoExclussionReason(Tender tender)
        {
            var postqualifiedSuppliers = await _tenderDomainService.GetPostqualificationOnTender(tender.TenderId);

            DateTime newAwardingReleaseDate = _rootConfiguration.NewAwarding.ReleaseDate.ToDateTime();
            var isSupplierHasNoExclussionReason = false;
            var offers = new List<Offer>();

            if (tender.IsNewAwarding(newAwardingReleaseDate))
            {
                offers = tender.Offers.Where(o => o.OfferStatusId == (int)Enums.OfferStatus.Received && o.OfferTechnicalEvaluationStatusId == (int)Enums.OfferTechnicalEvaluationStatusId.IdenticalOffer && o.OfferAcceptanceStatusId == (int)Enums.OfferAcceptanceStatusId.AcceptedOffer && o.PartialOfferAwardingValue == null && o.TotalOfferAwardingValue == null && string.IsNullOrEmpty(o.ExclusionReason)).ToList();
            }
            else
            {
                offers = tender.Offers.Where(o => o.OfferStatusId == (int)Enums.OfferStatus.Received && o.OfferTechnicalEvaluationStatusId == (int)Enums.OfferTechnicalEvaluationStatusId.IdenticalOffer && o.PartialOfferAwardingValue == null && o.TotalOfferAwardingValue == null && string.IsNullOrEmpty(o.ExclusionReason)).ToList();
            }

            foreach (var item in offers)
            {
                if (!postqualifiedSuppliers.Any(s => s.PostQualification.QualificationFinalResults.Any(f => f.SupplierSelectedCr == item.CommericalRegisterNo && f.QualificationLookupId == (int)Enums.QualificationResultLookup.Failed && f.IsActive == true)))
                {
                    isSupplierHasNoExclussionReason = true;
                    break;
                }
            }

            if (offers.Any() && isSupplierHasNoExclussionReason)
            {
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.ExcludedMessageNotInserted);
            }
        }
        private async Task CheckForActiveNegotiationOrExtendOfferValidityRequsts(int tenderId)
        {
            var isTenderHasActiveNegotiationRequests = await _tenderDomainService.IsTenderHasActiveNegotiationRequests(tenderId);
            if (isTenderHasActiveNegotiationRequests)
            {
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.ThereAreActiveNegotiationsOnThisTender);
            }

            var isTenderHasActiveExtendOfferValidityRequests = await _tenderDomainService.IsTenderHasActiveExtendOfferValidityRequests(tenderId);
            if (isTenderHasActiveExtendOfferValidityRequests)
            {
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.ThereAreActiveActiveExtendDateOnThisTender);
            }
        }
        private async Task NotificationAfterSendAwardingToApproval(Tender tender)
        {
            NotificationArguments NotificationArguments = new NotificationArguments
            {
                BodyEmailArgs = new object[] { "", tender.ReferenceNumber, tender.Purpose,
                        tender.LastEnqueriesDate?.ToString("dd/MM/yyyy hh:mm tt"), tender.LastOfferPresentationDate?.ToString("dd/MM/yyyy hh:mm tt"),
                       tender.OffersOpeningDate == null ? Resources.SharedResources.DisplayInputs.NotFound : tender.OffersOpeningDate?.ToString("dd/MM/yyyy"), tender.OffersOpeningDate == null ? Resources.SharedResources.DisplayInputs.NotFound : tender.OffersOpeningDate?.ToString("hh:mm tt") },
                PanelArgs = new object[] { tender.ReferenceNumber },
                SMSArgs = new object[] { tender.ReferenceNumber }
            };
            if (tender.TenderTypeId == (int)Enums.TenderType.NationalTransformationProjects)
            {
                MainNotificationTemplateModel templetModelVRO = new MainNotificationTemplateModel(NotificationArguments,
                    $"Tender/TenderAwarding?tenderIdString={Util.Encrypt(tender.TenderId)}", NotificationEntityType.Tender,
                    tender.TenderId.ToString(), tender.BranchId, tender.VROCommitteeId);
                await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.VROCheckManager.OperationsOnTheTender.vroAwardingWaitingApproval, tender.VROCommitteeId, templetModelVRO);
            }
            else if (tender.TenderTypeId == (int)Enums.TenderType.CurrentTender || tender.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase)
            {
                MainNotificationTemplateModel templetModel = new MainNotificationTemplateModel(NotificationArguments,
                 $"Tender/TenderAwarding?tenderIdString={Util.Encrypt(tender.TenderId)}", NotificationEntityType.Tender,
                 tender.TenderId.ToString(), tender.BranchId, tender.OffersCheckingCommitteeId);
                await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.OffersCheckManager.OperationsNeedToBeAccredited.tenderawardingwaitingapproval, tender.OffersCheckingCommitteeId, templetModel);
            }
            else
            {
                if (tender.TenderTypeId == (int)Enums.TenderType.NewDirectPurchase)
                {
                    MainNotificationTemplateModel templetModelDirectPurchase = new MainNotificationTemplateModel(NotificationArguments, $"Tender/TenderAwarding?tenderIdString={Util.Encrypt(tender.TenderId)}",
                          NotificationEntityType.Tender, tender.TenderId.ToString(), tender.BranchId, tender.DirectPurchaseCommitteeId);
                    await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.DirectPurchaseManager.OperationsOnTheTender.TenderAwardingNeedFirstApprove, tender.DirectPurchaseCommitteeId, templetModelDirectPurchase);
                }
                else
                {
                    MainNotificationTemplateModel templetModel = new MainNotificationTemplateModel(NotificationArguments, $"Tender/TenderAwarding?tenderIdString={Util.Encrypt(tender.TenderId)}",
                         NotificationEntityType.Tender, tender.TenderId.ToString(), tender.BranchId, tender.OffersCheckingCommitteeId);
                    await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.OffersCheckManager.OperationsOnTheTender.TenderAwardingNeedFirstApprove, tender.OffersCheckingCommitteeId, templetModel);
                }
            }
        }

        public async Task ApproveTenderInitialAwarding(int tenderId, string agencyCode, string verificationCode)
        {
            await _verification.CheckForVerificationCode(tenderId, verificationCode, (int)Enums.VerificationType.Tender);
            Tender tender = await _tenderQueries.FindTenderByIdForAwarding(tenderId);
            if (tender.TenderTypeId == (int)Enums.TenderType.CurrentTender || tender.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase || tender.TenderTypeId == (int)Enums.TenderType.NationalTransformationProjects)
                IsValidToAcceptAwardTender(tender);
            else
                await IsValidToApproveTenderInitialAwarding(tender, agencyCode);

            await RejectTenderCancelRequest(tender);
            UpdateAwardingStatus(tender);
            await SendNotificationAfterInitialAwardingApproval(tender);
            _genericCommandRepository.Update(tender);
            await _genericCommandRepository.SaveAsync();
        }

        private void UpdateAwardingStatus(Tender tender)
        {
            if (tender.TenderTypeId == (int)Enums.TenderType.CurrentTender || tender.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase || tender.TenderTypeId == (int)Enums.TenderType.NationalTransformationProjects)
            {
                tender.UpdateTenderStatus(Enums.TenderStatus.OffersAwardedConfirmed, "", _httpContextAccessor.HttpContext.User.UserId(), TenderActions.ApproveAwarding);
            }
            else
            {
                tender.UpdateTenderStatus(Enums.TenderStatus.OffersInitialAwardedConfirmed, "", _httpContextAccessor.HttpContext.User.UserId(), TenderActions.ApproveTenderInitialAwarding);
            }
        }
        private async Task RejectTenderCancelRequest(Tender tender)
        {

            if (tender.TenderTypeId == (int)Enums.TenderType.NationalTransformationProjects)
            {
                await RejectTenderCancelRequestWhileTenderApproval(tender, RoleNames.OffersOpeningAndCheckSecretary);
            }
            else if (tender.TenderTypeId == (int)Enums.TenderType.NewDirectPurchase && tender.IsLowBudgetDirectPurchase != true)
            {
                await RejectTenderCancelRequestWhileTenderApproval(tender, RoleNames.OffersPurchaseSecretary);
            }
            else
            {
                await RejectTenderCancelRequestWhileTenderApproval(tender, RoleNames.OffersCheckSecretary);
            }
        }
        private async Task SendNotificationAfterInitialAwardingApproval(Tender tender)
        {
            var suppliersOffers = await _tenderQueries.FindSuppliersOffersInAwardingStageByTenderId(tender.TenderId);
            List<string> awardedsuppliers = suppliersOffers.Where(x => x.TotalOfferAwardingValue != null || x.PartialOfferAwardingValue != null).Select(a => a.CommericalRegisterNo).ToList();
            NotificationArguments NotificationArguments = new NotificationArguments
            {
                BodyEmailArgs = new object[] { "", tender.ReferenceNumber, tender.Purpose,
                        tender.LastEnqueriesDate?.ToString("dd/MM/yyyy hh:mm tt"), tender.LastOfferPresentationDate?.ToString("dd/MM/yyyy hh:mm tt"),
                       tender.OffersOpeningDate == null ? Resources.SharedResources.DisplayInputs.NotFound : tender.OffersOpeningDate?.ToString("dd/MM/yyyy"), tender.OffersOpeningDate == null ? Resources.SharedResources.DisplayInputs.NotFound : tender.OffersOpeningDate?.ToString("hh:mm tt") },
                PanelArgs = new object[] { tender.ReferenceNumber },
                SMSArgs = new object[] { tender.ReferenceNumber }
            };
            MainNotificationTemplateModel templetModelSupplier = new MainNotificationTemplateModel(NotificationArguments, $"Tender/DetailsForSupplier?STenderId={Util.Encrypt(tender.TenderId)}", NotificationEntityType.Tender, tender.TenderId.ToString(), tender.BranchId, tender.OffersCheckingCommitteeId);
            if (tender.TenderTypeId == (int)Enums.TenderType.NationalTransformationProjects)
            {
                MainNotificationTemplateModel templetModelVRO = new MainNotificationTemplateModel(
                    NotificationArguments, $"Tender/TenderAwarding?tenderIdString={Util.Encrypt(tender.TenderId)}",
                    NotificationEntityType.Tender, tender.TenderId.ToString(), tender.BranchId, tender.VROCommitteeId);

                await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.VROCheckSecretary.OperationsOnTheTender.vroApproveTenderAwarding, tender.VROCommitteeId, templetModelVRO);
                await _notificationAppService.SendNotificationForSuppliers(NotificationOperations.Supplier.OperationsOnTheTender.vendorawardingtender, awardedsuppliers, templetModelSupplier);
            }
            else if (tender.TenderTypeId == (int)Enums.TenderType.CurrentTender || tender.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase)
            {
                MainNotificationTemplateModel templetModel = new MainNotificationTemplateModel(NotificationArguments, $"Tender/TenderAwarding?tenderIdString={Util.Encrypt(tender.TenderId)}", NotificationEntityType.Tender, tender.TenderId.ToString(), tender.BranchId, tender.OffersCheckingCommitteeId);
                await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.OffersCheckSecretary.OperationsOnTheTender.approvetenderawarding, tender.OffersCheckingCommitteeId, templetModel);
                await _notificationAppService.SendNotificationForSuppliers(NotificationOperations.Supplier.OperationsOnTheTender.vendorawardingtender, awardedsuppliers, templetModelSupplier);
            }
            else if (tender.TenderStatusId == (int)Enums.TenderStatus.OffersInitialAwardedConfirmed)
            {
                //to do filter with estimated value
                MainNotificationTemplateModel templetModel = new MainNotificationTemplateModel(NotificationArguments, $"Tender/TenderAwarding?tenderIdString={Util.Encrypt(tender.TenderId)}", NotificationEntityType.Tender, tender.TenderId.ToString(), tender.BranchId, tender.OffersCheckingCommitteeId);
                await _notificationAppService.SendNotificationForBranchUsers(NotificationOperations.ApproveTenderAwarding.OperationsOnTheTender.sendAwardingToApproveAfterInitialAwarding, tender.BranchId, templetModel);
                if (tender.TenderTypeId == (int)Enums.TenderType.NewDirectPurchase)
                {
                    if (tender.IsLowBudgetDirectPurchase != true)
                    {
                        MainNotificationTemplateModel templetModelDirectPurchase = new MainNotificationTemplateModel(NotificationArguments, $"Tender/TenderAwarding?tenderIdString={Util.Encrypt(tender.TenderId)}", NotificationEntityType.Tender, tender.TenderId.ToString(), tender.BranchId, tender.DirectPurchaseCommitteeId);
                        await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.DirectPurchaseSecretary.OperationsOnTheTender.approveInitialAwarding, tender.DirectPurchaseCommitteeId, templetModelDirectPurchase);
                    }
                }
                else
                {
                    await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.OffersCheckSecretary.OperationsOnTheTender.approveInitialAwarding, tender.OffersCheckingCommitteeId, templetModel);
                }
            }
        }

        public async Task RejectInitialAwardTenderOffer(int tenderId, string rejectionReason, string agencyCode)
        {
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            Tender tender = await _tenderQueries.FindTenderFoAwardingStageByTenderId(tenderId);
            IsValidToRejectTenderInitialAwarding(tender, agencyCode);
            if (tender.TenderTypeId == (int)Enums.TenderType.NationalTransformationProjects || tender.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase || tender.TenderTypeId == (int)Enums.TenderType.CurrentTender)
            {
                tender.UpdateTenderStatus(Enums.TenderStatus.OffersAwardedRejected, rejectionReason, _httpContextAccessor.HttpContext.User.UserId(), TenderActions.RejectInitialAwardTenderOffer);
            }
            else
            {
                tender.UpdateTenderStatus(Enums.TenderStatus.OffersInitialAwardedRejected, rejectionReason, _httpContextAccessor.HttpContext.User.UserId(), TenderActions.RejectInitialAwardTenderOffer);
            }
            await SendNotificationAfterRejectAwarding(tender);
            await _tenderCommands.UpdateAsync(tender);
        }

        private async Task SendNotificationAfterRejectAwarding(Tender tender)
        {
            NotificationArguments NotificationArguments = new NotificationArguments
            {
                BodyEmailArgs = new object[] { "", tender.ReferenceNumber, tender.Purpose,
                        tender.LastEnqueriesDate?.ToString("dd/MM/yyyy hh:mm tt"), tender.LastOfferPresentationDate?.ToString("dd/MM/yyyy hh:mm tt"),
                       tender.OffersOpeningDate == null  ?Resources.SharedResources.DisplayInputs.NotFound : tender.OffersOpeningDate?.ToString("dd/MM/yyyy"), tender.OffersOpeningDate == null ? Resources.SharedResources.DisplayInputs.NotFound : tender.OffersOpeningDate?.ToString("hh:mm tt") },
                PanelArgs = new object[] { tender.ReferenceNumber },
                SMSArgs = new object[] { tender.ReferenceNumber }
            };
            if (tender.TenderTypeId == (int)Enums.TenderType.NationalTransformationProjects)
            {
                MainNotificationTemplateModel templetModelVRO = new MainNotificationTemplateModel(
                    NotificationArguments,
                    $"Tender/TenderAwarding?tenderIdString={Util.Encrypt(tender.TenderId)}",
                    NotificationEntityType.Tender,
                    tender.TenderId.ToString(),
                    tender.BranchId,
                    tender.VROCommitteeId);
                await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.VROCheckSecretary.OperationsOnTheTender.vroRejectTenderAwarding, tender.VROCommitteeId, templetModelVRO);
            }
            else if (tender.TenderTypeId == (int)Enums.TenderType.CurrentTender || tender.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase)
            {
                MainNotificationTemplateModel templetModel = new MainNotificationTemplateModel(NotificationArguments, $"Tender/TenderAwarding?tenderIdString={Util.Encrypt(tender.TenderId)}", NotificationEntityType.Tender, tender.TenderId.ToString(), tender.BranchId, tender.OffersCheckingCommitteeId);
                await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.OffersCheckSecretary.OperationsOnTheTender.rejecttenderawarding, tender.OffersCheckingCommitteeId, templetModel);
            }
            else if (tender.TenderTypeId == (int)Enums.TenderType.NewDirectPurchase)
            {
                MainNotificationTemplateModel templetModelDirectPurchase = new MainNotificationTemplateModel(NotificationArguments, $"Tender/TenderAwarding?tenderIdString={Util.Encrypt(tender.TenderId)}", NotificationEntityType.Tender, tender.TenderId.ToString(), tender.BranchId, tender.DirectPurchaseCommitteeId);
                await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.DirectPurchaseSecretary.OperationsOnTheTender.rejectInitialAwarding, tender.DirectPurchaseCommitteeId, templetModelDirectPurchase);
            }
            else
            {
                MainNotificationTemplateModel templetModel = new MainNotificationTemplateModel(NotificationArguments, $"Tender/TenderAwarding?tenderIdString={Util.Encrypt(tender.TenderId)}", NotificationEntityType.Tender, tender.TenderId.ToString(), tender.BranchId, tender.OffersCheckingCommitteeId);
                await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.OffersCheckSecretary.OperationsOnTheTender.rejectInitialAwarding, tender.OffersCheckingCommitteeId, templetModel);
            }

        }

        public async Task ApproveTenderAwarding(int tenderId, string verificationCode)
        {
            await _verification.CheckForVerificationCode(tenderId, verificationCode, (int)Enums.VerificationType.Tender);
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            Tender tender = await _tenderQueries.GetTenderByIdWithAwardingHistoy(tenderId);
            Tuple<decimal, decimal> managerApprovalLimnits = new Tuple<decimal, decimal>(0, int.MaxValue);
            IsValidToApproveTenderAwarding(tender, managerApprovalLimnits);
            tender.UpdateTenderStatus(Enums.TenderStatus.OffersAwardedConfirmed, "", _httpContextAccessor.HttpContext.User.UserId(), TenderActions.ApproveAwarding);
            await SendNotificationAfterFinalAwardingApprove(tender);
            await VendorFinalAwardingNotifications(tender);
            await _tenderCommands.UpdateAsync(tender);
        }

        private async Task SendNotificationAfterFinalAwardingApprove(Tender tender)
        {
            NotificationArguments NotificationArguments = new NotificationArguments
            {
                BodyEmailArgs = new object[] { "", tender.ReferenceNumber, tender.Purpose,
                        tender.LastEnqueriesDate?.ToString("dd/MM/yyyy hh:mm tt"), tender.LastOfferPresentationDate?.ToString("dd/MM/yyyy hh:mm tt"),
                       tender.OffersOpeningDate == null ? Resources.SharedResources.DisplayInputs.NotFound : tender.OffersOpeningDate?.ToString("dd/MM/yyyy"), tender.OffersOpeningDate == null ? Resources.SharedResources.DisplayInputs.NotFound : tender.OffersOpeningDate?.ToString("hh:mm tt")  },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { tender.ReferenceNumber },
                SMSArgs = new object[] { tender.ReferenceNumber }
            };
            if (tender.TenderTypeId == (int)Enums.TenderType.NewDirectPurchase)
            {
                if (tender.IsLowBudgetDirectPurchase.HasValue && tender.IsLowBudgetDirectPurchase.Value)
                {
                    UserProfile userProfile = await _iDMQueries.FindUserProfileByIdWithoutIncludesAsync(tender.DirectPurchaseMemberId.Value);
                    await _notificationAppService.AddNotificationSettingByUserId(NotificationOperations.DirectPurchaseMember.OperationsOnTheTender.ApproveTenderAwardingDirectPurchaseMember, userProfile, (int)Enums.UserRole.CR_DirectPurchaseMember);
                    MainNotificationTemplateModel templetModelDirectPurchaseForLowBudget = new MainNotificationTemplateModel(NotificationArguments, $"Tender/Details?STenderId={Util.Encrypt(tender.TenderId)}", NotificationEntityType.Tender, tender.TenderId.ToString(), tender.BranchId);
                    await _notificationAppService.SendNotificationByUserId(NotificationOperations.DirectPurchaseMember.OperationsOnTheTender.ApproveTenderAwardingDirectPurchaseMember, tender.DirectPurchaseMemberId.Value, userProfile.UserName, templetModelDirectPurchaseForLowBudget);
                }
                else
                {
                    MainNotificationTemplateModel templetModelDirectPurchase = new MainNotificationTemplateModel(NotificationArguments, $"Tender/Details?STenderId={Util.Encrypt(tender.TenderId)}", NotificationEntityType.Tender, tender.TenderId.ToString(), tender.BranchId, tender.DirectPurchaseCommitteeId);
                    await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.DirectPurchaseSecretary.OperationsOnTheTender.approvetenderawarding, tender.DirectPurchaseCommitteeId, templetModelDirectPurchase);
                    await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.DirectPurchaseManager.OperationsOnTheTender.approvetenderawarding, tender.DirectPurchaseCommitteeId, templetModelDirectPurchase);
                }
            }
            else
            {
                MainNotificationTemplateModel templetModel = new MainNotificationTemplateModel(NotificationArguments, $"Tender/Details?STenderId={Util.Encrypt(tender.TenderId)}", NotificationEntityType.Tender, tender.TenderId.ToString(), tender.BranchId, tender.OffersCheckingCommitteeId);
                await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.OffersCheckSecretary.OperationsOnTheTender.approvetenderawarding, tender.OffersCheckingCommitteeId, templetModel);
                await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.OffersCheckManager.OperationsOnTheTender.approvetenderawarding, tender.OffersCheckingCommitteeId, templetModel);
            }
        }

        private async Task VendorFinalAwardingNotifications(Tender tender)
        {
            string awarderdSuppliersName = "";
            var suppliersOffers = await _tenderQueries.FindSuppliersOffersInAwardingStageByTenderId(tender.TenderId);
            var awardedTenderOffers = suppliersOffers.Where(x => x.TotalOfferAwardingValue != null || x.PartialOfferAwardingValue != null).ToList();
            List<string> awardedsuppliers = suppliersOffers.Where(x => x.TotalOfferAwardingValue != null || x.PartialOfferAwardingValue != null).Select(a => a.CommericalRegisterNo).ToList();
            int awardingIndex = tender.TenderAwardingHistories.Any() ? tender.TenderAwardingHistories.Max(x => x.AwardingIndex) + 1 : 1;
            foreach (Offer offer in awardedTenderOffers)
            {
                tender.AddTenderAwardingHistory(offer.CommericalRegisterNo, tender.TenderId, tender.TenderAwardingType,
                tender.TenderAwardingType.HasValue && tender.TenderAwardingType.Value ? offer.TotalOfferAwardingValue : offer.PartialOfferAwardingValue, awardingIndex);
                awarderdSuppliersName = offer.Supplier.SelectedCrName + ",";
            }
            await SendNotificationToExcludedSuppliers(tender, awarderdSuppliersName.Substring(0, awarderdSuppliersName.Length - 1));
            NotificationArguments NotificationAwardingArguments = new NotificationArguments
            {
                BodyEmailArgs = new object[] { "", "", "",  tender.TenderName, tender.ReferenceNumber, _httpContextAccessor.HttpContext.User.UserAgencyName(),"", DateTime.Now.ToString("dd/MM/yyyy"),
                       tender.AwardingDiscountPercentage, new ConvertNumberToText(tender.AwardingDiscountPercentage??0, new CurrencyInfo(CurrencyInfo.Currencies.SaudiArabia), "", "", "", "").ConvertToArabicWithoutSuffix(),
                       tender.AwardingMonths,_httpContextAccessor.HttpContext.User.UserAgencyName()
                },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { tender.ReferenceNumber },
                SMSArgs = new object[] { tender.ReferenceNumber }
            };
            NotificationArguments NotificationAwardingWithoutGuaranteeArguments = new NotificationArguments
            {
                BodyEmailArgs = new object[] { "", "", "", tender.TenderName, tender.ReferenceNumber, _httpContextAccessor.HttpContext.User.UserAgencyName(), "", DateTime.Now.ToString("dd/MM/yyyy"),
                       tender.AwardingDiscountPercentage, new ConvertNumberToText(tender.AwardingDiscountPercentage??0, new CurrencyInfo(CurrencyInfo.Currencies.SaudiArabia)).ConvertToArabicWithoutSuffix()
                },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { tender.ReferenceNumber },
                SMSArgs = new object[] { tender.ReferenceNumber }
            };

            MainNotificationTemplateModel awardingTempletModel = new MainNotificationTemplateModel(NotificationAwardingArguments, $"Tender/Details?STenderId={Util.Encrypt(tender.TenderId)}", NotificationEntityType.Tender,
                tender.TenderId.ToString(), tender.BranchId, tender.OffersCheckingCommitteeId);
            MainNotificationTemplateModel awardingWithoutGuaranteeTempletModel = new MainNotificationTemplateModel(NotificationAwardingWithoutGuaranteeArguments, $"Tender/Details?STenderId={Util.Encrypt(tender.TenderId)}", NotificationEntityType.Tender,
                tender.TenderId.ToString(), tender.BranchId, tender.OffersCheckingCommitteeId);

            Dictionary<string, Dictionary<int, string>> newawardedParameters = new Dictionary<string, Dictionary<int, string>> { };
            int index = 1;
            foreach (var offer in awardedTenderOffers)
            {
                Dictionary<int, string> newParameters = new Dictionary<int, string>
            {
                { 1, "" },
                { 2, "" },
                { 6, "" }
            };
                newParameters[1] = index + " / " + awardedTenderOffers.Count;
                newParameters[2] = offer.OffersHistory.LastOrDefault(o => o.OfferStatusId == (int)OfferStatus.Received).CreatedAt.ToString("dd/MM/yyyy");
                newParameters[6] = offer.TotalOfferAwardingValue.HasValue && offer.TotalOfferAwardingValue.Value > 0 ? offer.TotalOfferAwardingValue.Value.ToString() : offer.PartialOfferAwardingValue.Value.ToString();
                newawardedParameters.Add(offer.CommericalRegisterNo, newParameters);
                index++;
            }
            if (tender.HasGuarantee.HasValue && tender.HasGuarantee == true)
                await _notificationAppService.SendNotificationForSuppliers(NotificationOperations.Supplier.OperationsOnTheTender.SupplierAwardedNotification, awardedsuppliers, awardingTempletModel, newawardedParameters);
            else
                await _notificationAppService.SendNotificationForSuppliers(NotificationOperations.Supplier.OperationsOnTheTender.SupplierAwardedNotificationNoGurantee, awardedsuppliers, awardingWithoutGuaranteeTempletModel, newawardedParameters);
        }

        private async Task SendNotificationToExcludedSuppliers(Tender tender, string awarderdSuppliersName)
        {
            Dictionary<string, Dictionary<int, string>> ExcludedParamaters = new Dictionary<string, Dictionary<int, string>> { };
            List<string> ExcludedSuppliers = new List<string>();
            var lstSuppliersHaveExcludedReason = await _tenderQueries.FindNotAwardedSuppliersCauseExcludedReason(tender.TenderId);

            var offersFailedInTechnicalEvaluation = await _tenderQueries.GetFailedSuppliersOnTechnicalEvaluation(tender.TenderId);
            var offersFailedInFinancialEvaluation = await _tenderQueries.GetFailedSuppliersOnFinancialEvaluation(tender.TenderId);

            var lstSuppliersNotUplodedBankGuarantee = await _communicationQueries.FindSuppliersThatAcceptInitialyExtendOfferValidity(tender.TenderId);
            var lstSuppliresThatFailInPostQualification = await _offerQueries.GetFailedSuppliersOnPostQualification(tender.TenderId);
            var lstSuppliersThatRejectExtendOfferValidity = await _communicationQueries.FindSuppliersThatRejectExtendOfferValidity(tender.TenderId);
            var lstSuppliersThatNotResponseToExtendOfferValidity = await _communicationQueries.FindSuppliersThatNotResponseToExtendOfferValidity(tender.TenderId);
            DateTime newAwardingReleaseDate = _rootConfiguration.NewAwarding.ReleaseDate.ToDateTime();

            if (lstSuppliersHaveExcludedReason.Any())
            {
                Dictionary<int, string> newExcludedParameters = new Dictionary<int, string> { { 2, "" } };
                foreach (var itemExcludedReason in lstSuppliersHaveExcludedReason)
                {
                    ExcludedSuppliers.Add(itemExcludedReason.CommericalRegisterNo);
                    newExcludedParameters[2] = itemExcludedReason.ExclusionReason;
                    ExcludedParamaters.Add(itemExcludedReason.CommericalRegisterNo, newExcludedParameters);
                }
            }

            if (lstSuppliresThatFailInPostQualification.Any())
            {
                Dictionary<int, string> newExcludedParameters = new Dictionary<int, string> { { 2, "" } };
                foreach (var itemFailInPostQualification in lstSuppliresThatFailInPostQualification)
                {
                    ExcludedSuppliers.Add(itemFailInPostQualification);
                    newExcludedParameters[2] = Resources.TenderResources.Messages.FailInPostQualification;
                    ExcludedParamaters.Add(itemFailInPostQualification, newExcludedParameters);
                }
            }
            var localContentSetting = await _offerQueries.GetLocalContentSettingsDate();
            var isValidToApplyOfferLocalContent = tender.CreatedAt > localContentSetting.Date;

            var offersFailInTechnicalAndFinancialEvaluation = offersFailedInTechnicalEvaluation.Where(f => offersFailedInFinancialEvaluation.Contains(f));

            if (offersFailedInTechnicalEvaluation.Any())
            {
                Dictionary<int, string> newExcludedParameters = new Dictionary<int, string> { { 2, "" } };
                foreach (var item in offersFailedInTechnicalEvaluation.Where(y => !offersFailInTechnicalAndFinancialEvaluation.Contains(y)))
                {

                    if (isValidToApplyOfferLocalContent)
                    {
                        var isBindedToTheLowestBaseLine = item.OfferlocalContentDetails.IsBindedToTheLowestBaseLine;
                        var isBindedToTheLowestLocalContent = item.OfferlocalContentDetails.IsBindedToTheLowestLocalContent;
                        var rejectionReason = "";
                        if (!isBindedToTheLowestBaseLine)
                        {
                            rejectionReason += "عدم تحقيق المورد للحد الادني لخط الاساس";
                        }
                        if (!isBindedToTheLowestLocalContent)
                        {
                            rejectionReason += "@ عدم تحقيق الحد الادني المطلوب للمحتوي المحلي ";
                        }
                        rejectionReason = rejectionReason.Replace("@", Environment.NewLine);

                        ExcludedSuppliers.Add(item.CommericalRegisterNo);
                        newExcludedParameters[2] = Resources.TenderResources.Messages.FailInTechnicalEvaluation + rejectionReason;
                        ExcludedParamaters.Add(item.CommericalRegisterNo, newExcludedParameters);

                    }
                    else
                    {
                        ExcludedSuppliers.Add(item.CommericalRegisterNo);
                        newExcludedParameters[2] = Resources.TenderResources.Messages.FailInTechnicalEvaluation;
                        ExcludedParamaters.Add(item.CommericalRegisterNo, newExcludedParameters);
                    }
                }
            }

            bool isnewAwarding = tender.IsNewAwarding(newAwardingReleaseDate);
            if (isnewAwarding && offersFailedInFinancialEvaluation.Any())
            {
                Dictionary<int, string> newExcludedParameters = new Dictionary<int, string> { { 2, "" } };
                foreach (var item in offersFailedInFinancialEvaluation.Where(y => !offersFailInTechnicalAndFinancialEvaluation.Contains(y)))
                {

                    if (isValidToApplyOfferLocalContent)
                    {
                        var isBindedToMandatoryList = item.OfferlocalContentDetails.IsBindedToMandatoryList;
                        var rejectionReason = "";
                        if (!isBindedToMandatoryList)
                        {
                            rejectionReason += "عدم التزام المورد بالقائمة الالزامية";
                        }

                        ExcludedSuppliers.Add(item.CommericalRegisterNo);
                        newExcludedParameters[2] = Resources.TenderResources.Messages.FailInFinancialEvaluation + rejectionReason;
                        ExcludedParamaters.Add(item.CommericalRegisterNo, newExcludedParameters);

                    }
                    else
                    {
                        ExcludedSuppliers.Add(item.CommericalRegisterNo);
                        newExcludedParameters[2] = Resources.TenderResources.Messages.FailInFinancialEvaluation;
                        ExcludedParamaters.Add(item.CommericalRegisterNo, newExcludedParameters);
                    }
                }
            }

            if (offersFailInTechnicalAndFinancialEvaluation.Any())
            {
                Dictionary<int, string> newExcludedParameters = new Dictionary<int, string> { { 2, "" } };
                foreach (var item in offersFailInTechnicalAndFinancialEvaluation)
                {
                    if (isValidToApplyOfferLocalContent)
                    {
                        var isBindedToTheLowestBaseLine = item.OfferlocalContentDetails.IsBindedToTheLowestBaseLine;
                        var isBindedToTheLowestLocalContent = item.OfferlocalContentDetails.IsBindedToTheLowestLocalContent;
                        var isBindedToMandatoryList = item.OfferlocalContentDetails.IsBindedToMandatoryList;
                        var rejectionReason = "";
                        if (!isBindedToMandatoryList)
                        {
                            rejectionReason += "عدم التزام المورد بالقائمة الالزامية";
                        }
                        if (!isBindedToTheLowestBaseLine)
                        {
                            rejectionReason += "@ عدم تحقيق المورد للحد الادني لخط الاساس";
                        }
                        if (!isBindedToTheLowestLocalContent)
                        {
                            rejectionReason += "@ عدم تحقيق الحد الادني المطلوب للمحتوي المحلي ";
                        }
                        rejectionReason = rejectionReason.Replace("@", Environment.NewLine);

                        ExcludedSuppliers.Add(item.CommericalRegisterNo);
                        newExcludedParameters[2] = Resources.TenderResources.Messages.FailInTechnicalAndFinancialEvaluation + rejectionReason;
                        ExcludedParamaters.Add(item.CommericalRegisterNo, newExcludedParameters);
                    }
                    else
                    {
                        ExcludedSuppliers.Add(item.CommericalRegisterNo);
                        newExcludedParameters[2] = Resources.TenderResources.Messages.FailInTechnicalAndFinancialEvaluation;
                        ExcludedParamaters.Add(item.CommericalRegisterNo, newExcludedParameters);

                    }
                }
            }

            if (lstSuppliersNotUplodedBankGuarantee.Any())
            {
                Dictionary<int, string> newExcludedParameters = new Dictionary<int, string> { { 2, "" } };
                foreach (var itemBankGuarantee in lstSuppliersNotUplodedBankGuarantee)
                {
                    if (!ExcludedSuppliers.Contains(itemBankGuarantee))
                    {
                        ExcludedSuppliers.Add(itemBankGuarantee);
                        newExcludedParameters[2] = Resources.TenderResources.Messages.NotUplodedBankGuarantee;
                        ExcludedParamaters.Add(itemBankGuarantee, newExcludedParameters);
                    }
                }
            }

            if (lstSuppliersThatRejectExtendOfferValidity.Any())
            {
                Dictionary<int, string> newExcludedParameters = new Dictionary<int, string> { { 2, "" } };
                foreach (var itemRejectExtendOfferValidity in lstSuppliersThatRejectExtendOfferValidity)
                {
                    if (!ExcludedSuppliers.Contains(itemRejectExtendOfferValidity))
                    {
                        ExcludedSuppliers.Add(itemRejectExtendOfferValidity);
                        newExcludedParameters[2] = Resources.TenderResources.Messages.RejectExtendOfferValidity;
                        ExcludedParamaters.Add(itemRejectExtendOfferValidity, newExcludedParameters);
                    }
                }
            }

            if (lstSuppliersThatNotResponseToExtendOfferValidity.Any())
            {
                Dictionary<int, string> newExcludedParameters = new Dictionary<int, string> { { 2, "" } };
                foreach (var itemNotResponseToExtendOfferValidity in lstSuppliersThatNotResponseToExtendOfferValidity)
                {
                    if (!ExcludedSuppliers.Contains(itemNotResponseToExtendOfferValidity))
                    {
                        ExcludedSuppliers.Add(itemNotResponseToExtendOfferValidity);
                        newExcludedParameters[2] = Resources.TenderResources.Messages.NotResponseToExtendOfferValidity;
                        ExcludedParamaters.Add(itemNotResponseToExtendOfferValidity, newExcludedParameters);
                    }
                }
            }

            NotificationArguments NotificationAwardingArgumentsExcludedSuppliers = new NotificationArguments
            {
                BodyEmailArgs = new object[] { tender.ReferenceNumber, awarderdSuppliersName, "" },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { tender.ReferenceNumber },
                SMSArgs = new object[] { tender.ReferenceNumber }
            };
            MainNotificationTemplateModel ExcludedReasonTempletModel = new MainNotificationTemplateModel(NotificationAwardingArgumentsExcludedSuppliers, $"Tender/Details?STenderId={Util.Encrypt(tender.TenderId)}", NotificationEntityType.Tender,
            tender.TenderId.ToString(), tender.BranchId);
            await _notificationAppService.SendNotificationForSuppliers(NotificationOperations.Supplier.OperationsOnTheTender.ExclusionSupplierFromAwarding, ExcludedSuppliers, ExcludedReasonTempletModel, ExcludedParamaters);
        }


        private void TechnicalEvaluationRejectionReasons(List<Offer> offerFailedInTechnicalEvaluation, IEnumerable<Offer> offersFailInTechnicalAndFinancialEvaluation, bool isValidToApplyOfferLocalContent)
        {
            Dictionary<string, Dictionary<int, string>> ExcludedParamaters = new Dictionary<string, Dictionary<int, string>> { };
            List<string> ExcludedSuppliers = new List<string>();


        }
        private void FinancialEvaluationRejectionReasons(List<Offer> offersFailedInFinancialEvaluation, IEnumerable<Offer> offersFailInTechnicalAndFinancialEvaluation, bool isValidToApplyOfferLocalContent)
        {
            Dictionary<string, Dictionary<int, string>> ExcludedParamaters = new Dictionary<string, Dictionary<int, string>> { };
            List<string> ExcludedSuppliers = new List<string>();

        }

        public async Task RejectAwardTenderOffer(int tenderId, string rejectionReason)
        {
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            Tender tender = await _tenderQueries.GetTenderByIdWithoutAnyIncluds(tenderId);
            Tuple<decimal, decimal> managerApprovalLimnits = new Tuple<decimal, decimal>(0, int.MaxValue);
            IsValidToRejectAwardTender(tender, managerApprovalLimnits);
            tender.UpdateTenderStatus(Enums.TenderStatus.OffersAwardedRejected, rejectionReason, _httpContextAccessor.HttpContext.User.UserId(), TenderActions.TenderAwardingWasRejected);
            await SendNotificationAfterRejectAwardTenderOffer(tender);
            await _tenderCommands.UpdateAsync(tender);
        }

        private async Task SendNotificationAfterRejectAwardTenderOffer(Tender tender)
        {
            NotificationArguments NotificationArguments = new NotificationArguments
            {
                BodyEmailArgs = new object[] { "", tender.ReferenceNumber, tender.Purpose,
                        tender.LastEnqueriesDate?.ToString("dd/MM/yyyy hh:mm tt"), tender.LastOfferPresentationDate?.ToString("dd/MM/yyyy hh:mm tt"),
                       tender.OffersOpeningDate == null  ?Resources.SharedResources.DisplayInputs.NotFound : tender.OffersOpeningDate?.ToString("dd/MM/yyyy"), tender.OffersOpeningDate == null ? Resources.SharedResources.DisplayInputs.NotFound : tender.OffersOpeningDate?.ToString("hh:mm tt") },
                PanelArgs = new object[] { tender.ReferenceNumber },
                SMSArgs = new object[] { tender.ReferenceNumber }
            };
            if (tender.TenderTypeId == (int)Enums.TenderType.NewDirectPurchase)
            {
                if (tender.IsLowBudgetDirectPurchase.HasValue && tender.IsLowBudgetDirectPurchase.Value)
                {
                    UserProfile userProfile = await _iDMQueries.FindUserProfileByIdWithoutIncludesAsync(tender.DirectPurchaseMemberId.Value);
                    await _notificationAppService.AddNotificationSettingByUserId(NotificationOperations.DirectPurchaseMember.OperationsOnTheTender.RejectTenderAwardingDirectPurchaseMember, userProfile, (int)Enums.UserRole.CR_DirectPurchaseMember);
                    MainNotificationTemplateModel templetDirectPurchaseForLowBudget = new MainNotificationTemplateModel(NotificationArguments, $"Tender/TenderAwarding?tenderIdString={Util.Encrypt(tender.TenderId)}", NotificationEntityType.Tender, tender.TenderId.ToString(), tender.BranchId);
                    await _notificationAppService.SendNotificationByUserId(NotificationOperations.DirectPurchaseMember.OperationsOnTheTender.RejectTenderAwardingDirectPurchaseMember, tender.DirectPurchaseMemberId.Value, userProfile.UserName, templetDirectPurchaseForLowBudget);
                }
                else
                {
                    MainNotificationTemplateModel templetDirectPurchase = new MainNotificationTemplateModel(NotificationArguments, $"Tender/TenderAwarding?tenderIdString={Util.Encrypt(tender.TenderId)}", NotificationEntityType.Tender, tender.TenderId.ToString(), tender.BranchId, tender.DirectPurchaseCommitteeId);
                    await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.DirectPurchaseSecretary.OperationsOnTheTender.rejecttenderawarding, tender.DirectPurchaseCommitteeId, templetDirectPurchase);
                    await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.DirectPurchaseManager.OperationsOnTheTender.rejecttenderawarding, tender.DirectPurchaseCommitteeId, templetDirectPurchase);
                }
            }
            else
            {
                MainNotificationTemplateModel templetModel = new MainNotificationTemplateModel(NotificationArguments, $"Tender/TenderAwarding?tenderIdString={Util.Encrypt(tender.TenderId)}", NotificationEntityType.Tender, tender.TenderId.ToString(), tender.BranchId, tender.OffersCheckingCommitteeId);
                await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.OffersCheckSecretary.OperationsOnTheTender.rejecttenderawarding, tender.OffersCheckingCommitteeId, templetModel);
                await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.OffersCheckManager.OperationsOnTheTender.rejecttenderawarding, tender.OffersCheckingCommitteeId, templetModel);
            }
        }

        public async Task ReopenTenderAwarding(int tenderId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            Tender tender = await _tenderQueries.GetTenderByIdWithoutAnyIncluds(tenderId);
            IsValidToReopenTenderAwarding(tender);
            tender.UpdateTenderStatus(Enums.TenderStatus.OffersAwarding, "", _httpContextAccessor.HttpContext.User.UserId(), TenderActions.TenderReopenedForAwarding); await _tenderCommands.UpdateAsync(tender);
        }


        public async Task<bool> EmarketPlace(List<int> offerIds)
        {
            var columns = await _tenderQueries.GetAwardedSupplierQuantityTableItemsValue(offerIds);
            ApiResponse<List<EmarketPlaceResponse>> list = await _templatesProxy.GetEmarketPlace(columns);
            if (list.Data != null)
            {
                var emarketPlaceModel = await _tenderQueries.FillVendorProducts(offerIds, list.Data);
                var result = await _sRMFrameworkAgreementManageProxy.SRMFrameworkAgreementManage(emarketPlaceModel);
                if (!result)
                {
                    return false;
                }
                return result;
            }
            else
            {
                return false;
            }
        }

        private void IsValidToReopenTenderAwarding(Tender tender)
        {
            if (tender == null)
                throw new UnHandledAccessException(Resources.SharedResources.ErrorMessages.YouHaveNoAccess);
            if (tender.TenderStatusId != (int)Enums.TenderStatus.OffersAwardedRejected && tender.TenderStatusId != (int)Enums.TenderStatus.OffersInitialAwardedRejected && tender.TenderStatusId != (int)Enums.TenderStatus.BackForAwardingFromPlaint)
            {
                typeof(Enums.TenderStatus).GetProperty(nameof(Enums.TenderStatus.OffersAwardedRejected));
                var tenderStatus = Resources.TenderResources.Messages.ResourceManager.GetString(nameof(Enums.TenderStatus.OffersAwardedRejected));
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.CanNotProceedStatusIs + tenderStatus);
            }
        }

        private void IsValidToAcceptAwardTender(Tender tender)
        {
            if (tender == null)
                throw new UnHandledAccessException(Resources.SharedResources.ErrorMessages.YouHaveNoAccess);

            if (tender.TenderStatusId != (int)Enums.TenderStatus.OffersAwardedPending)
            {
                typeof(Enums.TenderStatus).GetProperty(nameof(Enums.TenderStatus.OffersAwardedPending));
                var tenderStatus = Resources.TenderResources.Messages.ResourceManager.GetString(nameof(Enums.TenderStatus.OffersAwardedPending));
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.CanNotProceedStatusIs + tenderStatus);
            }
        }

        private void IsValidToApproveTenderAwarding(Tender tender, Tuple<decimal, decimal> managerApprovalLimnits)
        {
            if (tender == null)
                throw new UnHandledAccessException(Resources.SharedResources.ErrorMessages.YouHaveNoAccess);
            if (tender.TenderStatusId != (int)Enums.TenderStatus.OffersInitialAwardedConfirmed && !(tender.TenderStatusId == (int)Enums.TenderStatus.OffersAwardedPending && (tender.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase || tender.TenderTypeId == (int)Enums.TenderType.CurrentTender)))
            {
                typeof(Enums.TenderStatus).GetProperty(nameof(Enums.TenderStatus.OffersAwardedPending));
                var tenderStatus = Resources.TenderResources.Messages.ResourceManager.GetString(nameof(Enums.TenderStatus.OffersAwardedPending));
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.CanNotProceedStatusIs + tenderStatus);
            }
            if (!(tender.EstimatedValue >= managerApprovalLimnits.Item1 && tender.EstimatedValue <= managerApprovalLimnits.Item2))
            {
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.TenderEstimationValueIsNotWithinTheManagerApprovalRange);
            }
        }

        private async Task IsValidToSendAwardTenderToApprove(Tender tender)
        {
            if (tender == null)
                throw new UnHandledAccessException(Resources.SharedResources.ErrorMessages.YouHaveNoAccess);
            if (tender.TenderStatusId != (int)Enums.TenderStatus.OffersAwarding)
            {
                typeof(Enums.TenderStatus).GetProperty(nameof(Enums.TenderStatus.OffersAwarding));
                var tenderStatus = Resources.TenderResources.Messages.ResourceManager.GetString(nameof(Enums.TenderStatus.OffersAwarding));
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.CanNotProceedStatusIs + tenderStatus);
            }
            await CheckForActiveNegotiationOrExtendOfferValidityRequsts(tender.TenderId);
        }

        private void IsValidToRejectAwardTender(Tender tender, Tuple<decimal, decimal> managerApprovalLimnits)
        {
            if (tender == null)
                throw new UnHandledAccessException(Resources.SharedResources.ErrorMessages.YouHaveNoAccess);
            if (tender.TenderStatusId != (int)Enums.TenderStatus.OffersAwardedPending && tender.TenderStatusId != (int)Enums.TenderStatus.OffersInitialAwardedConfirmed)
            {
                typeof(Enums.TenderStatus).GetProperty(nameof(Enums.TenderStatus.OffersAwardedPending));
                var tenderStatus = Resources.TenderResources.Messages.ResourceManager.GetString(nameof(Enums.TenderStatus.OffersAwardedPending));
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.CanNotProceedStatusIs + tenderStatus);
            }
            if (!(tender.EstimatedValue >= managerApprovalLimnits.Item1 && tender.EstimatedValue <= managerApprovalLimnits.Item2))
            {
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.TenderEstimationValueIsNotWithinTheManagerApprovalRange);
            }
        }

        private async Task IsValidToApproveTenderInitialAwarding(Tender tender, string agencyCode)
        {
            if (tender == null)
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.TenderNotExist);
            if (tender.AgencyCode != agencyCode)
                throw new UnHandledAccessException();

            var isLowBudgetFlow = tender.TenderTypeId == (int)Enums.TenderType.NewDirectPurchase && tender.IsLowBudgetDirectPurchase == true && _httpContextAccessor.HttpContext.User.UserId() == tender.DirectPurchaseMemberId;

            if (isLowBudgetFlow && tender.TenderStatusId != (int)Enums.TenderStatus.OffersAwarding)
            {
                var tenderStatus = Resources.TenderResources.Messages.ResourceManager.GetString(nameof(Enums.TenderStatus.OffersAwarding));
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.CanNotProceedStatusIs + tenderStatus);
            }
            else if (!isLowBudgetFlow && tender.TenderStatusId != (int)Enums.TenderStatus.OffersInitialAwardedPending && tender.TenderStatusId != (int)Enums.TenderStatus.OffersAwardedPending)
            {
                var tenderStatus = Resources.TenderResources.Messages.ResourceManager.GetString(nameof(Enums.TenderStatus.OffersAwardedPending));
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.CanNotProceedStatusIs + tenderStatus);
            }
            //may be removed
            await CheckForActiveNegotiationOrExtendOfferValidityRequsts(tender.TenderId);
            await IsAllExecludedSupplierHasNoExclussionReason(tender);
        }
        private void IsValidToRejectTenderInitialAwarding(Tender tender, string agencyCode)
        {
            if (tender == null)
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.TenderNotExist);
            if (tender.AgencyCode != agencyCode)
                throw new UnHandledAccessException();

            else if (tender.TenderStatusId != (int)Enums.TenderStatus.OffersInitialAwardedPending && tender.TenderStatusId != (int)Enums.TenderStatus.OffersAwardedPending)
            {
                var tenderStatus = Resources.TenderResources.Messages.ResourceManager.GetString(nameof(Enums.TenderStatus.OffersAwardedPending));
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.CanNotProceedStatusIs + tenderStatus);
            }
        }
    }
}
