using Microsoft.AspNetCore.Http;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Core.Interfaces;
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
    public class PrePlanningAppService : IPrePlanningAppService
    {
        private readonly IPrePlanningDomainService _prePlanningDomainService;
        private readonly IGenericCommandRepository _genericCommandRepository;
        private readonly IPrePlanningQueries _prePlanningQueries;
        private readonly INotificationAppService _notificationAppService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITenderQueries _tenderQueries;
        private readonly IVerificationService _verification;
        public PrePlanningAppService(ITenderQueries tenderQueries, IPrePlanningDomainService prePlanningDomainService, IGenericCommandRepository genericCommandRepository, IPrePlanningQueries prePlanningQueries,  /*IIDMAppService iDMAppService,*/ INotificationAppService notificationAppService, IHttpContextAccessor httpContextAccessor, IVerificationService verification)
        {
            _tenderQueries = tenderQueries;
            _httpContextAccessor = httpContextAccessor;
            _prePlanningDomainService = prePlanningDomainService;
            _prePlanningQueries = prePlanningQueries;
            _genericCommandRepository = genericCommandRepository;
            _notificationAppService = notificationAppService;
            _verification = verification;
        }

        #region Service Queries ======================================================

        public async Task<QueryResult<PrePlanningModel>> FindPrePlanningBySearchCriteria(PrePlanningSearchCriteria criteria)
        {
            return await _prePlanningQueries.FindPrePlanningBySearchCriteria(criteria);
        }
        public async Task<PrePlanningModel> FindPrePlanningById(int id)
        {
            PrePlanningModel preplanning = await _prePlanningQueries.FindPrePlanningById(id);

            if (preplanning == null)
            {
                throw new UnHandledAccessException();
            }
            return preplanning;
        }
        public async Task<PrePlanningModel> GetPrePlanningDetailsById(int id)
        {
            PrePlanningModel preplanning = await _prePlanningQueries.GetPrePlanningDetailsById(id);
            if (preplanning == null)
            {
                throw new UnHandledAccessException();
            }
            return preplanning;
        }

        public async Task<PrePlanningModel> SetPrePlanningLookUps()
        {
            PrePlanningModel preplanning = await _prePlanningQueries.SetPrePlanningLookUps();
            if (preplanning == null)
            {
                throw new UnHandledAccessException();
            }
            return preplanning;
        }
        public async Task<int> Deactivate(int id, bool userRole)
        {

            return await _prePlanningQueries.Deactivate(id, userRole);
        }
        #endregion

        #region  Service Commands =====================================================

        public async Task<PrePlanningModel> CreateAsync(PrePlanningModel model)
        {
            Check.ArgumentNotNull(nameof(model), model);
            PrePlanning planning;
            await _prePlanningDomainService.CheckNameExist(model.ProjectName, model.BranchId, model.YearQuarterId, model.AgencyCode, model.PrePlanningId);
            if (model.PrePlanningId == 0)
            {
                planning = new PrePlanning(model.PrePlanningId, model.AgencyCode, model.BranchId, model.ProjectName, model.ProjectNature, model.ProjectTypeId, model.ProjectDescription, model.InsideKSA, (int)Enums.PrePlanningStatus.UnderUpdate, model.Duration, model.DurationInDays, model.DurationInMonths, model.DurationInYears, model.YearQuarterId);
                planning.IsDurationValid();
                planning.AddtAreasCountriesTypes(model.PrePlanningAreaIDs, model.InsideKSA, model.PrePlanningCountriesIDs, model.ProjectTypesIDs);
                planning = await _genericCommandRepository.CreateAsync<PrePlanning>(planning);
            }
            else
            {
                planning = await _prePlanningQueries.FindByIdForEdit(model.PrePlanningId);
                if (planning.StatusId == (int)Enums.PrePlanningStatus.Pending || planning.StatusId == (int)Enums.PrePlanningStatus.Rejected)
                {
                    throw new UnHandledAccessException();
                }
                planning.Update(model.AgencyCode, model.ProjectName, model.ProjectNature, model.ProjectDescription, model.InsideKSA, (int)Enums.PrePlanningStatus.UnderUpdate, model.Duration, model.DurationInDays, model.DurationInMonths, model.DurationInYears, model.YearQuarterId);
                planning.IsDurationValid();
                planning = planning.UpdatetAreasCountriesTypes(model.PrePlanningAreaIDs, model.InsideKSA, model.PrePlanningCountriesIDs, model.ProjectTypesIDs);
                _genericCommandRepository.Update<PrePlanning>(planning);
            }
            await _genericCommandRepository.SaveAsync();
            model.PrePlanningId = planning.PrePlanningId;
            return model;
        }


        public async Task ReOpen(int planningId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(planningId), planningId.ToString());
            PrePlanning planning = await _prePlanningQueries.FindById(planningId);

            if (planning.StatusId != (int)Enums.PrePlanningStatus.Rejected)
            {
                throw new UnHandledAccessException();
            }
            planning.UpdateStatus(Enums.PrePlanningStatus.UnderUpdate, "");

            _genericCommandRepository.Update<PrePlanning>(planning);
            await _genericCommandRepository.SaveAsync();
        }

        public async Task ReOpenForCancelation(int planningId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(planningId), planningId.ToString());
            PrePlanning planning = await _prePlanningQueries.FindById(planningId);

            if (planning.StatusId != (int)Enums.PrePlanningStatus.Approved && planning.IsDeleteRequested && !string.IsNullOrEmpty(planning.DeleteRejectionReason))
            {
                throw new UnHandledAccessException();
            }
            planning.RemoveCancelationRejectReason();

            _genericCommandRepository.Update<PrePlanning>(planning);
            await _genericCommandRepository.SaveAsync();
        }




        public async Task SendToApprove(int planningId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(planningId), planningId.ToString());
            PrePlanning planning = await _prePlanningQueries.FindById(planningId);
            if (planning.StatusId != (int)Enums.PrePlanningStatus.UnderUpdate && planning.StatusId != (int)Enums.PrePlanningStatus.Rejected)
            {
                throw new UnHandledAccessException();
            }
            planning.UpdateStatus(Enums.PrePlanningStatus.Pending, "");


            NotificationArguments NotificationArguments = new NotificationArguments
            {
                BodyEmailArgs = new object[] { "", planning.ProjectName },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { planning.ProjectName },
                SMSArgs = new object[] { "", planning.ProjectName }
            };
            MainNotificationTemplateModel approveTender = new MainNotificationTemplateModel(NotificationArguments, $"PrePlanning/Details?id={Util.Encrypt(planning.PrePlanningId)}", NotificationEntityType.prePlanning, planning.PrePlanningId.ToString(), planning.BranchId);
            await _notificationAppService.SendNotificationForBranchUsers(NotificationOperations.PrePlaningAuditor.OperationsOnPrePlaning.SendToApproval, planning.BranchId, approveTender);
            _genericCommandRepository.Update<PrePlanning>(planning);
            await _genericCommandRepository.SaveAsync();
        }

        public async Task Reject(int planningId, string rejectionReason)
        {
            Check.ArgumentNotNullOrEmpty(nameof(planningId), planningId.ToString());
            PrePlanning planning = await _prePlanningQueries.FindById(planningId);
            if (rejectionReason.Length > 500)
            {
                throw new BusinessRuleException("سبب الرفض لا يمكن ان يزيد عن 500 حرف.");
            }
            if (planning.StatusId == (int)Enums.PrePlanningStatus.Approved && planning.IsDeleteRequested)
            {
                planning.SetDeActiveRequest(false);
                planning.SetDeleteRejectionReason(rejectionReason);
            }
            else if (planning.StatusId != (int)Enums.PrePlanningStatus.Pending)
            {
                throw new UnHandledAccessException();
            }
            else
                planning.UpdateStatus(Enums.PrePlanningStatus.Rejected, rejectionReason);
            NotificationArguments NotificationArguments = new NotificationArguments
            {
                BodyEmailArgs = new object[]{ "", planning.ProjectName,
                                              },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { planning.ProjectName },
                SMSArgs = new object[] { planning.ProjectName }
            };
            MainNotificationTemplateModel approveTender = new MainNotificationTemplateModel(NotificationArguments, $"PrePlanning/Details?id={Util.Encrypt(planning.PrePlanningId)}", NotificationEntityType.prePlanning, planning.PrePlanningId.ToString(), planning.BranchId);
            await _notificationAppService.SendNotificationForBranchUsers(NotificationOperations.PrePlaningEmployee.OperationsOnPrePlaning.RejectPreplaning, planning.BranchId, approveTender);
            _genericCommandRepository.Update<PrePlanning>(planning);
            await _genericCommandRepository.SaveAsync();
        }

        public async Task<bool> CheckForVerificationCode(int tenderId, string verificationCodeString, int typeId)
        {
            int userId = _httpContextAccessor.HttpContext.User.UserId();
            await _tenderQueries.FindVerificationCode(userId, typeId);
            await _verification.CheckForVerificationCode(tenderId, verificationCodeString, typeId);
            return true;
        }
        public async Task Approve(int planningId, string verificationCode)
        {
            Check.ArgumentNotNullOrEmpty(nameof(planningId), planningId.ToString());
            PrePlanning planning = await _prePlanningQueries.FindById(planningId);
            if (planning.StatusId != (int)Enums.PrePlanningStatus.Pending)
            {
                throw new UnHandledAccessException();
            }
            await CheckForVerificationCode(planningId, verificationCode, (int)Enums.VerificationType.PrePlanning);
            planning.UpdateStatus(Enums.PrePlanningStatus.Approved, "");
            NotificationArguments NotificationArguments = new NotificationArguments
            {
                BodyEmailArgs = new object[] { "", planning.ProjectName },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { planning.ProjectName },
                SMSArgs = new object[] { "", planning.ProjectName }
            };
            MainNotificationTemplateModel approveTender = new MainNotificationTemplateModel(NotificationArguments, $"PrePlanning/Details?id={Util.Encrypt(planning.PrePlanningId)}", NotificationEntityType.prePlanning, planning.PrePlanningId.ToString(), planning.BranchId);
            await _notificationAppService.SendNotificationForBranchUsers(NotificationOperations.PrePlaningEmployee.OperationsOnPrePlaning.ApprovePreplaning, planning.BranchId, approveTender);
            _genericCommandRepository.Update<PrePlanning>(planning);
            await _genericCommandRepository.SaveAsync();
        }
        #endregion
    }
}
