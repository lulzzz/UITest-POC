using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Integration;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernel;
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
        #region Unit

        public async Task<List<LookupModel>> GetAllUnitUsersByRoleName(string roleName)
        {
            List<EmployeeIntegrationModel> employeeList = await _iDMAppService.GetEmployeeDetailsByRoleName(roleName);
            List<LookupModel> userLookupModels = new List<LookupModel>();
            foreach (EmployeeIntegrationModel employee in employeeList)
            {
                userLookupModels.Add(new LookupModel() { Name = employee.fullName + " - " + employee.mobileNumber + " - " + employee.email, Value = employee.nationalId });
            }
            return userLookupModels;
        }

        #region LevelOne

        public async Task<QueryResult<BasicTenderModel>> FindTendersForUnitStageBySearchCriteria(TenderSearchCriteria criteria)
        {
            criteria.TenderStatusIds.Add((int)Enums.TenderStatus.UnitStaging);
            if (string.IsNullOrEmpty(criteria.Sort))
            {
                criteria.Sort = "CreatedAt";
                criteria.SortDirection = "DESC";
            }
            return await _tenderQueries.FindTenderBySearchCriteriaForUnitStage(criteria);
        }

        public async Task OpenTenderByUnitSecretaryAsync(string tenderIdString)
        {
            var tenderId = Util.Decrypt(tenderIdString);
            Tender tender = await _tenderQueries.FindTenderWithUnitHistoryById(tenderId);
            _tenderDomainService.IsValidToOpenUnitStageByUnitSecretary(tender);
            TenderUnitStatusesHistory tenderUnitStatusesHistory = new TenderUnitStatusesHistory(comment: "", tenderUnitStatusId: (int)Enums.TenderUnitStatus.UnderUnitReviewLevelOne, estimatedValue: tender.EstimatedValue);
            TenderUnitAssign tenderUnitAssign = new TenderUnitAssign(_httpContextAccessor.HttpContext.User.UserId(), tenderId, true, (int)Enums.UnitSpecialistLevel.UnitSpecialistLevelOne);
            tender.TenderUnitStatusesHistories.Add(tenderUnitStatusesHistory);
            tender.SetUnitStatus(Enums.TenderUnitStatus.UnderUnitReviewLevelOne);
            _genericCommandRepository.Update(tender);
            await _genericCommandRepository.CreateAsync(tenderUnitAssign);
            await _genericCommandRepository.SaveAsync();
        }

        public async Task SendTenderByUnitSecretaryForModificationAsync(ReturnTenderToDataEntryFromUnitFormodificationsModel returnTenderToDataEntryFromUnitFormodificationsModel)
        {
            var tenderId = Util.Decrypt(returnTenderToDataEntryFromUnitFormodificationsModel.tenderIdString);
            Tender tender = await _tenderQueries.FindTenderWithUnitHistoryById(tenderId);
            _tenderDomainService.IsValidToReturnTenderToDataEntryForEdit(tender);
            tender.DeactiveAllAssingments();
            tender.SetUnitStatus(Enums.TenderUnitStatus.ReturnedToAgencyForEdit);
            tender.DeleteUnitModificationAttachments();
            TenderUnitStatusesHistory tenderUnitStatusesHistory = new TenderUnitStatusesHistory(comment: returnTenderToDataEntryFromUnitFormodificationsModel.notes, tenderUnitStatusId: (int)Enums.TenderUnitStatus.ReturnedToAgencyForEdit, updateTypeId: returnTenderToDataEntryFromUnitFormodificationsModel.modificationTypeId, estimatedValue: tender.EstimatedValue);
            tender.TenderUnitStatusesHistories.Add(tenderUnitStatusesHistory);
            tender.UpdateTenderStatus(Enums.TenderStatus.ReturnedFromUnitToAgencyForEdit,
                returnTenderToDataEntryFromUnitFormodificationsModel.notes,
                _httpContextAccessor.HttpContext.User.UserId(),
                TenderActions.SendTenderByUnitSecretaryForModification);
            List<TenderAttachment> tenderAttachments = new List<TenderAttachment>();
            foreach (var file in returnTenderToDataEntryFromUnitFormodificationsModel.files)
            {
                tenderAttachments.Add(new TenderAttachment(file.Name, file.FileNetReferenceId, (int)Enums.AttachmentType.UnitModificationsAttachmentsToDataEntry));
            }
            tender.Attachments.AddRange(tenderAttachments);
            #region [Send Notification]
            NotificationArguments NotificationArguments = new NotificationArguments
            {
                BodyEmailArgs = new object[] { tender.ReferenceNumber },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { tender.ReferenceNumber },
                SMSArgs = new object[] { tender.ReferenceNumber }
            };
            MainNotificationTemplateModel mainNotificationTemplateModel = new MainNotificationTemplateModel(NotificationArguments,
                          $"Tender/Details?STenderId={Util.Encrypt(tender.TenderId)}",
                          NotificationEntityType.Tender,
                          tender.TenderId.ToString(),
                          tender.BranchId);

            await _notificationAppService.SendNotificationForBranchUsers(NotificationOperations.Auditor.OperationsOnTheTender.editTenderFromUnit, tender.BranchId, mainNotificationTemplateModel);
            await _notificationAppService.SendNotificationForBranchUsers(NotificationOperations.DataEntry.OperationsOnTheTender.editTenderFromUnit, tender.BranchId, mainNotificationTemplateModel);
            #endregion
            _genericCommandRepository.Update(tender);
            await _genericCommandRepository.SaveAsync();
        }

        public async Task SendToLevelTwoFromUnitSecretaryLevelOneAsync(string tenderIdString, string userName, string notes)
        {
            var tenderId = Util.Decrypt(tenderIdString);
            Tender tender = await _tenderQueries.FindTenderWithUnitHistoryById(tenderId);
            _tenderDomainService.IsValidToSendToLevelTwoFromUnitSecretaryLevelOne(tender, userName);
            var user = await _iDMAppService.FindUserProfileByUserNameAsync(userName);
            if (user == null)
            {
                Enums.UserRole userType = (Enums.UserRole)Enum.Parse(typeof(Enums.UserRole), RoleNames.UnitSpecialistLevel2, true);
                user = await _iDMAppService.GetUserProfileByEmployeeId(userName, null, userType);
                Check.ArgumentNotNull(nameof(user), user);
                await _genericCommandRepository.CreateAsync(user);
            }
            TenderUnitAssign tenderUnitAssign = new TenderUnitAssign(user.Id, tenderId, true, (int)UnitSpecialistLevel.UnitSpecialistLevelTwo);
            tender.SetUnitStatus(Enums.TenderUnitStatus.TenderTransferdToLevelTwo);
            TenderUnitStatusesHistory tenderUnitStatusesHistory = new TenderUnitStatusesHistory(comment: notes, tenderUnitStatusId: (int)Enums.TenderUnitStatus.TenderTransferdToLevelTwo, estimatedValue: tender.EstimatedValue);
            tender.TenderUnitStatusesHistories.Add(tenderUnitStatusesHistory);
            #region [Send Notification]
            NotificationArguments NotificationArguments = new NotificationArguments
            {
                BodyEmailArgs = new object[] { tender.ReferenceNumber },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { tender.ReferenceNumber },
                SMSArgs = new object[] { tender.ReferenceNumber }
            };
            MainNotificationTemplateModel mainNotificationTemplateModel = new MainNotificationTemplateModel(NotificationArguments,
                          $"Tender/TenderDetailsForUnitSecretary?tenderIdString={Util.Encrypt(tender.TenderId)}",
                          NotificationEntityType.Tender,
                          tender.TenderId.ToString(),
                          tender.BranchId);
            await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.UnitSecrtaryLevel2.OperationsOnTheTender.reviewTender, null, mainNotificationTemplateModel);
            #endregion
            await _tenderCommands.UpdateAsync(tender);
            await _genericCommandRepository.CreateAsync(tenderUnitAssign);
            await _genericCommandRepository.SaveAsync();
        }

        public async Task ApproveTenderByUnitSecretaryAsync(string tenderIdString, bool IWouldLikeToAttendTheommitte)
        {
            int tenderId = Util.Decrypt(tenderIdString);
            Tender tender = await _tenderQueries.FindTenderWithUnitHistoryById(tenderId);
            _tenderDomainService.IsValidToUpdateApproveTenderByUnitSecretary(tender);
            TenderUnitStatusesHistory tenderUnitStatusesHistory = new TenderUnitStatusesHistory(comment: "", tenderUnitStatusId: (int)Enums.TenderUnitStatus.UnderUnitReviewLevelOne, estimatedValue: tender.EstimatedValue);
            tender.SetUnitStatus(Enums.TenderUnitStatus.UnderUnitReviewLevelOne);
            tender.SetUnitSpacialistWouldLikeToAttendTheCommitte(IWouldLikeToAttendTheommitte);
            tender.SetIsUnitSecreteryAccepted(true);
            tender.TenderUnitStatusesHistories.Add(tenderUnitStatusesHistory);
            await _tenderCommands.UpdateAsync(tender);
        }

        public async Task RejectTenderByUnitSecretaryAsync(string tenderIdString, string comment)
        {
            int tenderId = Util.Decrypt(tenderIdString);
            Tender tender = await _tenderQueries.FindTenderWithUnitHistoryById(tenderId);
            _tenderDomainService.IsValidToUpdateRejectTenderByUnitSecretary(tender, comment);
            TenderUnitStatusesHistory tenderUnitStatusesHistory = new TenderUnitStatusesHistory(comment: comment, tenderUnitStatusId: (int)Enums.TenderUnitStatus.UnderUnitReviewLevelOne, estimatedValue: tender.EstimatedValue);
            tender.SetUnitStatus(Enums.TenderUnitStatus.UnderUnitReviewLevelOne);
            tender.SetIsUnitSecreteryAccepted(false);
            tender.TenderUnitStatusesHistories.Add(tenderUnitStatusesHistory);
            await _tenderCommands.UpdateAsync(tender);
        }

        public async Task SendToApproveFromUnitSecretaryToUnitMangerAsync(string tenderIdString)
        {
            int tenderId = Util.Decrypt(tenderIdString);
            Tender tender = await _tenderQueries.FindTenderWithUnitHistoryById(tenderId);
            _tenderDomainService.IsValidToSendToApproveByUnitManager(tender);
            TenderUnitStatusesHistory tenderUnitStatusesHistory = new TenderUnitStatusesHistory(comment: "", tenderUnitStatusId: (int)Enums.TenderUnitStatus.WaitingManagerApprove, estimatedValue: tender.EstimatedValue);
            tender.TenderUnitStatusesHistories.Add(tenderUnitStatusesHistory);
            tender.SetUnitStatus(Enums.TenderUnitStatus.WaitingManagerApprove);
            #region [Send Notification] 
            NotificationArguments NotificationArguments = new NotificationArguments
            {
                BodyEmailArgs = new object[] { tender.ReferenceNumber },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { tender.ReferenceNumber },
                SMSArgs = new object[] { tender.ReferenceNumber }
            };
            MainNotificationTemplateModel mainNotificationTemplateModel = new MainNotificationTemplateModel(NotificationArguments,
                          $"Tender/TenderDetailsForUnitSecretary?tenderIdString={Util.Encrypt(tender.TenderId)}",
                          NotificationEntityType.Tender,
                          tender.TenderId.ToString(),
                          tender.BranchId);
            await _notificationAppService.SendNotificationForCommitteeUsers(
                NotificationOperations.UnitManager.OperationsOnTheTender.sendToUnitManagerToApprove,
                null,
                mainNotificationTemplateModel);
            #endregion
            await _tenderCommands.UpdateAsync(tender);
        }

        public async Task ReOpenTenderByUnitSecertaryAsync(string tenderIdString)
        {
            int tenderId = Util.Decrypt(tenderIdString);
            Tender tender = await _tenderQueries.FindTenderWithUnitHistoryById(tenderId);
            _tenderDomainService.IsValidToReOpenTenderByUnitSecretary(tender);
            TenderUnitStatusesHistory tenderUnitStatusesHistory = new TenderUnitStatusesHistory(comment: "", tenderUnitStatusId: (int)Enums.TenderUnitStatus.UnderUnitReviewLevelOne, estimatedValue: tender.EstimatedValue);
            tender.TenderUnitStatusesHistories.Add(tenderUnitStatusesHistory);
            tender.SetUnitStatus(Enums.TenderUnitStatus.UnderUnitReviewLevelOne);
            TenderUnitAssign tenderUnitAssign = new TenderUnitAssign(_httpContextAccessor.HttpContext.User.UserId(), tenderId, true, (int)Enums.UnitSpecialistLevel.UnitSpecialistLevelOne);
            tender.TenderUnitStatusesHistories.Add(tenderUnitStatusesHistory);
            tender.ResetUnitSecretaryAction();
            _genericCommandRepository.Update(tender);
            await _genericCommandRepository.CreateAsync(tenderUnitAssign);
            await _genericCommandRepository.SaveAsync();
        }

        #endregion LevelOne

        #region LevelTwo

        public async Task OpenTenderByUnitSecretaryLevelTwoAsync(string tenderIdString)
        {
            int tenderId = Util.Decrypt(tenderIdString);
            Tender tender = await _tenderQueries.FindTenderWithUnitHistoryById(tenderId);
            _tenderDomainService.IsValidToOpenUnitStageByUnitSecretaryLevelTwo(tender);
            TenderUnitStatusesHistory tenderUnitStatusesHistory = new TenderUnitStatusesHistory(comment: "", tenderUnitStatusId: (int)Enums.TenderUnitStatus.UnderUnitReviewLevelTwo, estimatedValue: tender.EstimatedValue);
            tender.TenderUnitStatusesHistories.Add(tenderUnitStatusesHistory);
            tender.SetUnitStatus(Enums.TenderUnitStatus.UnderUnitReviewLevelTwo);
            _genericCommandRepository.Update(tender);
            await _genericCommandRepository.SaveAsync();
        }

        public async Task SendTenderByUnitSecretaryLevelTwoForModificationAsync(ReturnTenderToDataEntryFromUnitFormodificationsModel returnTenderToDataEntryFromUnitFormodificationsModel)
        {
            var tenderId = Util.Decrypt(returnTenderToDataEntryFromUnitFormodificationsModel.tenderIdString);
            Tender tender = await _tenderQueries.FindTenderWithUnitHistoryById(tenderId);
            _tenderDomainService.IsValidToReturnTenderToDataEntryForEditLevelTwo(tender);
            tender.DeactiveAllAssingments();
            tender.SetUnitStatus(Enums.TenderUnitStatus.ReturnedToAgencyForEdit);
            TenderUnitStatusesHistory tenderUnitStatusesHistory = new TenderUnitStatusesHistory(comment: returnTenderToDataEntryFromUnitFormodificationsModel.notes, tenderUnitStatusId: (int)Enums.TenderUnitStatus.ReturnedToAgencyForEdit, updateTypeId: returnTenderToDataEntryFromUnitFormodificationsModel.modificationTypeId, estimatedValue: tender.EstimatedValue);
            tender.TenderUnitStatusesHistories.Add(tenderUnitStatusesHistory);
            tender.UpdateTenderStatus(Enums.TenderStatus.ReturnedFromUnitToAgencyForEdit,
                returnTenderToDataEntryFromUnitFormodificationsModel.notes,
                _httpContextAccessor.HttpContext.User.UserId(),
                TenderActions.SendTenderByUnitSecretaryLevelTwoForModification);
            List<TenderAttachment> tenderAttachments = new List<TenderAttachment>();
            foreach (var file in returnTenderToDataEntryFromUnitFormodificationsModel.files)
            {
                tenderAttachments.Add(new TenderAttachment(file.Name, file.FileNetReferenceId, (int)Enums.AttachmentType.UnitModificationsAttachmentsToDataEntry));
            }
            tender.Attachments.AddRange(tenderAttachments);
            #region [Send Notification]
            NotificationArguments NotificationArguments = new NotificationArguments
            {
                BodyEmailArgs = new object[] { tender.ReferenceNumber },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { tender.ReferenceNumber },
                SMSArgs = new object[] { tender.ReferenceNumber }
            };
            MainNotificationTemplateModel mainNotificationTemplateModel = new MainNotificationTemplateModel(NotificationArguments,
                          $"Tender/Details?STenderId={Util.Encrypt(tender.TenderId)}",
                          NotificationEntityType.Tender,
                          tender.TenderId.ToString(),
                          tender.BranchId);
            await _notificationAppService.SendNotificationForBranchUsers(NotificationOperations.Auditor.OperationsOnTheTender.editTenderFromUnit, tender.BranchId, mainNotificationTemplateModel);
            await _notificationAppService.SendNotificationForBranchUsers(NotificationOperations.DataEntry.OperationsOnTheTender.editTenderFromUnit, tender.BranchId, mainNotificationTemplateModel);
            #endregion
            _genericCommandRepository.Update(tender);
            await _genericCommandRepository.SaveAsync();
        }

        public async Task RejectTenderByUnitSecretaryLevelTwoAsync(string tenderIdString, string comment)
        {
            int tenderId = Util.Decrypt(tenderIdString);
            Tender tender = await _tenderQueries.FindTenderWithUnitHistoryById(tenderId);
            _tenderDomainService.IsValidToUpdateRejectTenderByUnitSecretaryLevelTwo(tender, comment);
            TenderUnitStatusesHistory tenderUnitStatusesHistory = new TenderUnitStatusesHistory(comment: comment, tenderUnitStatusId: (int)Enums.TenderUnitStatus.UnderUnitReviewLevelTwo, estimatedValue: tender.EstimatedValue);
            tender.SetUnitStatus(Enums.TenderUnitStatus.UnderUnitReviewLevelTwo);
            tender.SetIsUnitSecreteryAccepted(false);
            tender.TenderUnitStatusesHistories.Add(tenderUnitStatusesHistory);
            await _tenderCommands.UpdateAsync(tender);
        }

        public async Task ApproveTenderByUnitSecretaryLevelTwoAsync(string tenderIdString)
        {
            int tenderId = Util.Decrypt(tenderIdString);
            Tender tender = await _tenderQueries.FindTenderWithUnitHistoryById(tenderId);
            _tenderDomainService.IsValidToUpdateApproveTenderByUnitSecretaryLevelTwo(tender);
            TenderUnitStatusesHistory tenderUnitStatusesHistory = new TenderUnitStatusesHistory(comment: "", tenderUnitStatusId: (int)Enums.TenderUnitStatus.UnderUnitReviewLevelTwo, estimatedValue: tender.EstimatedValue);
            tender.SetUnitStatus(Enums.TenderUnitStatus.UnderUnitReviewLevelTwo);
            tender.SetIsUnitSecreteryAccepted(true);
            tender.TenderUnitStatusesHistories.Add(tenderUnitStatusesHistory);
            await _tenderCommands.UpdateAsync(tender);
        }

        public async Task SendToApproveFromUnitSecretaryLevelTwoToUnitMangerAsync(string tenderIdString)
        {
            int tenderId = Util.Decrypt(tenderIdString);
            Tender tender = await _tenderQueries.FindTenderWithUnitHistoryById(tenderId);
            _tenderDomainService.IsValidToSendToApproveFromLevelToByUnitManager(tender);
            TenderUnitStatusesHistory tenderUnitStatusesHistory = new TenderUnitStatusesHistory(comment: "", tenderUnitStatusId: (int)Enums.TenderUnitStatus.WaitingManagerApprove, estimatedValue: tender.EstimatedValue);
            tender.TenderUnitStatusesHistories.Add(tenderUnitStatusesHistory);
            tender.SetUnitStatus(Enums.TenderUnitStatus.WaitingManagerApprove);
            #region [Send Notification] 
            NotificationArguments NotificationArguments = new NotificationArguments
            {
                BodyEmailArgs = new object[] { tender.ReferenceNumber },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { tender.ReferenceNumber },
                SMSArgs = new object[] { tender.ReferenceNumber }
            };
            MainNotificationTemplateModel mainNotificationTemplateModel = new MainNotificationTemplateModel(NotificationArguments,
                          $"Tender/TenderDetailsForUnitSecretary?tenderIdString={Util.Encrypt(tender.TenderId)}",
                          NotificationEntityType.Tender,
                          tender.TenderId.ToString(),
                          tender.BranchId);
            await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.UnitManager.OperationsOnTheTender.sendToUnitManagerToApprove, null, mainNotificationTemplateModel);
            #endregion
            await _tenderCommands.UpdateAsync(tender);
        }

        public async Task ReOpenTenderByUnitSecertaryLevelAsync(string tenderIdString)
        {
            int tenderId = Util.Decrypt(tenderIdString);
            Tender tender = await _tenderQueries.FindTenderWithUnitHistoryById(tenderId);
            _tenderDomainService.IsValidToReOpenTenderByUnitSecretary(tender);
            TenderUnitStatusesHistory tenderUnitStatusesHistory = new TenderUnitStatusesHistory(comment: "", tenderUnitStatusId: (int)Enums.TenderUnitStatus.UnderUnitReviewLevelTwo, estimatedValue: tender.EstimatedValue);
            tender.TenderUnitStatusesHistories.Add(tenderUnitStatusesHistory);
            tender.SetUnitStatus(Enums.TenderUnitStatus.UnderUnitReviewLevelTwo);
            TenderUnitAssign tenderUnitAssign = new TenderUnitAssign(_httpContextAccessor.HttpContext.User.UserId(), tenderId, true, (int)UnitSpecialistLevel.UnitSpecialistLevelTwo);
            tender.TenderUnitStatusesHistories.Add(tenderUnitStatusesHistory);
            tender.ResetUnitSecretaryAction();
            _genericCommandRepository.Update(tender);
            await _genericCommandRepository.CreateAsync(tenderUnitAssign);
            await _genericCommandRepository.SaveAsync();
        }

        #endregion LevelTwo

        public async Task ReviewTenderByUnitManager(string tenderIdString)
        {
            var tenderId = Util.Decrypt(tenderIdString);
            Tender tender = await _tenderQueries.FindTenderWithUnitHistoryById(tenderId);
            _tenderDomainService.IsValidToReviewTenderByUnitManager(tender);
            TenderUnitStatusesHistory tenderUnitStatusesHistory = new TenderUnitStatusesHistory(comment: "", tenderUnitStatusId: (int)Enums.TenderUnitStatus.UnderManagerReviewing, estimatedValue: tender.EstimatedValue);
            TenderUnitAssign tenderUnitAssign = new TenderUnitAssign(_httpContextAccessor.HttpContext.User.UserId(), tenderId, true, (int)UnitSpecialistLevel.UnitManager);
            tender.TenderUnitStatusesHistories.Add(tenderUnitStatusesHistory);
            tender.SetUnitStatus(Enums.TenderUnitStatus.UnderManagerReviewing);
            _genericCommandRepository.Update(tender);
            await _genericCommandRepository.CreateAsync(tenderUnitAssign);
            await _genericCommandRepository.SaveAsync();
        }

        public async Task ApproveTenderByUnitManagerAsync(string tenderIdString, string verificationCode)
        {
            int tenderId = Util.Decrypt(tenderIdString);
            await _verification.CheckForVerificationCode(tenderId, verificationCode, (int)Enums.VerificationType.Tender);
            Tender tender = await _tenderQueries.FindTenderWithUnitHistoryById(tenderId);
            _tenderDomainService.IsValidToUpdateApproveTenderByUnitManager(tender);
            TenderUnitStatusesHistory tenderUnitStatusesHistory = new TenderUnitStatusesHistory(comment: "", tenderUnitStatusId: (int)Enums.TenderUnitStatus.ApprovedByManager, estimatedValue: tender.EstimatedValue);
            tender.TenderUnitStatusesHistories.Add(tenderUnitStatusesHistory);
            tender.SetUnitStatus(Enums.TenderUnitStatus.ApprovedByManager);
            #region [Send Notification] 
            NotificationArguments NotificationArguments = new NotificationArguments
            {
                BodyEmailArgs = new object[] { tender.ReferenceNumber },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { tender.ReferenceNumber },
                SMSArgs = new object[] { tender.ReferenceNumber }
            };
            MainNotificationTemplateModel mainNotificationTemplateModel = new MainNotificationTemplateModel(NotificationArguments,
                          $"Tender/Details?STenderId={Util.Encrypt(tender.TenderId)}",
                          NotificationEntityType.Tender,
                          tender.TenderId.ToString(),
                          tender.BranchId);
            #endregion
            if (tender.IsUnitSecreteryAccepted.Value)
            {
                tender.UpdateSubmitionDate();
                tender.UpdateTenderStatus(Enums.TenderStatus.Approved, "", _httpContextAccessor.HttpContext.User.UserId(), TenderActions.ApproveTenderByUnitManager);
                #region [Send Notification] 
                await _notificationAppService.SendNotificationForBranchUsers(NotificationOperations.DataEntry.OperationsOnTheTender.approveTenderByUnit, tender.BranchId, mainNotificationTemplateModel);
                await _notificationAppService.SendNotificationForBranchUsers(NotificationOperations.Auditor.OperationsOnTheTender.approveTenderByUnit, tender.BranchId, mainNotificationTemplateModel);
                #endregion
                if (tender.Invitations.Any(c => c.IsActive == true && c.StatusId == (int)InvitationStatus.ToBeSent))
                {
                    var suppliers = tender.Invitations.Select(t => new SupplierInvitationModel { CommericalRegisterNo = t.CommericalRegisterNo, TenderId = tender.TenderId }).ToList();
                    await SendInvitationsToUnBlockedSuppliers(suppliers, tender);
                    var UnRegesteredsuppliers = tender.UnRegisteredSuppliersInvitation.Where(x => x.IsActive == true && x.InvitationStatusId == (int)InvitationStatus.ToBeSent).ToList();
                    if (UnRegesteredsuppliers.Count > 0)
                    {
                        tender.UpdateUnregesteredInvitations();
                        var emails = UnRegesteredsuppliers.Where(t => t.Email != null).Select(x => x.Email).ToList();
                        var mobileNos = UnRegesteredsuppliers.Where(t => t.MobileNo != null).Select(x => x.MobileNo).ToList();
                        await _notificationAppService.SendInvitationByEmail(emails, tender);
                        await _notificationAppService.SendInvitationBySms(mobileNos, tender);
                    }
                }
            }
            else
            {
                var tenderUnitHisories = await _tenderQueries.GetUnitStatusesHistoryByTenderId(tenderId);
                tender.UpdateTenderStatus(
                    Enums.TenderStatus.RejectedFromUnit,
                    tenderUnitHisories.OrderByDescending(t => t.TenderUnitStatusesHistoryId).FirstOrDefault().Comment,
                    _httpContextAccessor.HttpContext.User.UserId(),
                    TenderActions.RejectedFromUnit);
                #region [Send Notification] 
                await _notificationAppService.SendNotificationForBranchUsers(NotificationOperations.DataEntry.OperationsOnTheTender.rejectTenderByUnit, tender.BranchId, mainNotificationTemplateModel);
                await _notificationAppService.SendNotificationForBranchUsers(NotificationOperations.Auditor.OperationsOnTheTender.rejectTenderByUnit, tender.BranchId, mainNotificationTemplateModel);
                #endregion
            }
            await _tenderCommands.UpdateAsync(tender);
        }


        public async Task RejectTenderByUnitManagerAsync(string tenderIdString, string comment)
        {
            int tenderId = Util.Decrypt(tenderIdString);
            Tender tender = await _tenderQueries.FindTenderWithUnitHistoryById(tenderId);
            _tenderDomainService.IsValidToUpdateRejectTenderByUnitManager(tender, comment);
            TenderUnitStatusesHistory tenderUnitStatusesHistory = new TenderUnitStatusesHistory(comment: comment, tenderUnitStatusId: (int)Enums.TenderUnitStatus.RejectedByManager, estimatedValue: tender.EstimatedValue);
            tender.TenderUnitStatusesHistories.Add(tenderUnitStatusesHistory);
            tender.SetUnitStatus(Enums.TenderUnitStatus.RejectedByManager);
            tender.ResetUnitSecretaryAction();
            #region [Send Notification] 
            NotificationArguments NotificationArguments = new NotificationArguments
            {
                BodyEmailArgs = new object[] { tender.ReferenceNumber },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { tender.ReferenceNumber },
                SMSArgs = new object[] { tender.ReferenceNumber }
            };
            MainNotificationTemplateModel mainNotificationTemplateModel = new MainNotificationTemplateModel(NotificationArguments,
                $"Tender/TenderDetailsForUnitSecretary?tenderIdString={Util.Encrypt(tender.TenderId)}",
                NotificationEntityType.Tender, tender.TenderId.ToString(), tender.BranchId);
            if (tender.TenderUnitAssigns.OrderByDescending(a => a.TenderUnitAssignId).Last(a => a.IsCurrentlyAssigned).UnitSpecialistLevel == (int)UnitSpecialistLevel.UnitSpecialistLevelOne)
                await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.UnitSecrtaryLevel1.OperationsOnTheTender.rejectTenderFromUnitManager, null, mainNotificationTemplateModel);
            else
                await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.UnitSecrtaryLevel2.OperationsOnTheTender.rejectTenderFromUnitManager, null, mainNotificationTemplateModel);
            #endregion
            await _tenderCommands.UpdateAsync(tender);
        }

        public async Task SendToNewUserUnitBusinessManagementAsync(string tenderIdString, string userName)
        {
            var tenderId = Util.Decrypt(tenderIdString);
            Tender tender = await _tenderQueries.FindTenderWithUnitHistoryById(tenderId);
            Enums.UserRole userType;
            UnitSpecialistLevel userLevel;
            if (tender.TenderUnitStatusId == (int)Enums.TenderUnitStatus.UnderUnitReviewLevelOne || tender.TenderUnitStatusId == (int)Enums.TenderUnitStatus.WaitingUnitSecretaryReview)
            {
                userType = (Enums.UserRole)Enum.Parse(typeof(Enums.UserRole), RoleNames.UnitSpecialistLevel1, true);
                userLevel = (UnitSpecialistLevel)Enum.Parse(typeof(UnitSpecialistLevel), UnitSpecialistLevel.UnitSpecialistLevelOne.ToString(), true);
                tender.SetUnitStatus(Enums.TenderUnitStatus.UnderUnitReviewLevelOne);
            }
            else if (tender.TenderUnitStatusId == (int)Enums.TenderUnitStatus.UnderUnitReviewLevelTwo || tender.TenderUnitStatusId == (int)Enums.TenderUnitStatus.TenderTransferdToLevelTwo)
            {
                userType = (Enums.UserRole)Enum.Parse(typeof(Enums.UserRole), RoleNames.UnitSpecialistLevel2, true);
                userLevel = (UnitSpecialistLevel)Enum.Parse(typeof(UnitSpecialistLevel), UnitSpecialistLevel.UnitSpecialistLevelTwo.ToString(), true);
                tender.SetUnitStatus(Enums.TenderUnitStatus.UnderUnitReviewLevelTwo);
            }
            else if (tender.TenderUnitStatusId == (int)Enums.TenderUnitStatus.WaitingManagerApprove || tender.TenderUnitStatusId == (int)Enums.TenderUnitStatus.UnderManagerReviewing)
            {
                userType = (Enums.UserRole)Enum.Parse(typeof(Enums.UserRole), RoleNames.UnitManagerUser, true);
                userLevel = (UnitSpecialistLevel)Enum.Parse(typeof(UnitSpecialistLevel), UnitSpecialistLevel.UnitManager.ToString(), true);
                tender.SetUnitStatus(Enums.TenderUnitStatus.WaitingManagerApprove);
            }
            else
                throw new SharedKernal.Exceptions.BusinessRuleException("لابد من اختيار مستخدم بنفس المستوى"); var user = await _iDMAppService.FindUserProfileByUserNameAsync(userName);
            if (user == null)
            {
                user = await _iDMAppService.GetUserProfileByEmployeeId(userName, null, userType);
                Check.ArgumentNotNull(nameof(user), user);
                await _genericCommandRepository.CreateAsync(user);
            }
            else
                user = await _iDMAppService.GetUserProfileByEmployeeId(userName, null, userType);
            _tenderDomainService.IsValidToSendToNewUserUnitBusinessManagement(tender, user);
            TenderUnitAssign tenderUnitAssign = new TenderUnitAssign(user.Id, tenderId, true, (int)userLevel);
            TenderUnitStatusesHistory tenderUnitStatusesHistory = new TenderUnitStatusesHistory(comment: "", tenderUnitStatusId: tender.TenderUnitStatusId.Value, estimatedValue: tender.EstimatedValue);
            tender.TenderUnitStatusesHistories.Add(tenderUnitStatusesHistory);
            await _tenderCommands.UpdateAsync(tender);
            await _genericCommandRepository.CreateAsync(tenderUnitAssign);
            await _genericCommandRepository.SaveAsync();
        }

        #endregion
    }
}
