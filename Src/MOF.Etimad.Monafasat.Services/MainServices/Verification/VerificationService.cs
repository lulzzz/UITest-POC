using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Core.Interfaces;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public class VerificationService : IVerificationService
    {
        private readonly IGenericCommandRepository _genericCommandRepository;
        private readonly IVerificationQueries _verificationQueries;
        private readonly RootConfigurations _configuration;
        private readonly INotificationAppService _notificationAppService;
        private readonly ITenderDomainService _tenderDomainService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public VerificationService(IGenericCommandRepository genericCommandRepository, IVerificationQueries verificationQueries, INotificationAppService notificationAppService, IHttpContextAccessor httpContextAccessor, ITenderDomainService tenderDomainService, IOptionsSnapshot<RootConfigurations> optionsSnapshot)
        {
            _verificationQueries = verificationQueries;
            _genericCommandRepository = genericCommandRepository;
            _httpContextAccessor = httpContextAccessor; _tenderDomainService = tenderDomainService;
            _notificationAppService = notificationAppService;
            _configuration = optionsSnapshot.Value;
        }

        public async Task<bool> CheckForVerificationCode(int tenderId, string verificationCodeString, int typeId)
        {
            int userId = _httpContextAccessor.HttpContext.User.UserId();
            var verificationCode = await _verificationQueries.FindVerificationCode(tenderId, userId, typeId);
            if (_configuration.VerificationCodeConfiguration.Value.ToString().ToLower() == "false")
                verificationCodeString = verificationCode.VerificationCodeNo;
            _tenderDomainService.IsValidVerificationCode(verificationCode.ExpiredDate, verificationCode.VerificationCodeNo, verificationCodeString);
            return true;
        }

        public async Task CreateVerificationCode(int codeTypeId, string userMobile, string userEmail, int userID, int typeId)
        {
            var verificationCode = Util.RandomString(5);
            VerificationCode vCode = new VerificationCode(verificationCode, userID, typeId, codeTypeId);
            NotificationArguments NotificationArgument = new NotificationArguments
            {
                BodyEmailArgs = new object[] { "", verificationCode },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { },
                SMSArgs = new object[] { verificationCode }
            };
            MainNotificationTemplateModel verficationCodeModel = new MainNotificationTemplateModel(NotificationArgument);
            int operationCode = 0;
            if (_httpContextAccessor.HttpContext.User.UserRole() == RoleNames.Auditer)
                operationCode = NotificationOperations.Auditor.OperationsNeedApprove.approvaloperation;
            else if (_httpContextAccessor.HttpContext.User.UserRole() == RoleNames.DataEntry)
                operationCode = NotificationOperations.DataEntry.OperationsNeedApprove.approvaloperation;
            else if (_httpContextAccessor.HttpContext.User.UserRole() == RoleNames.OffersOppeningManager)
                operationCode = NotificationOperations.OffersOppeningManager.TransactionsNeedApproval.approvaloperation;
            else if (_httpContextAccessor.HttpContext.User.UserRole() == RoleNames.OffersCheckManager)
                operationCode = NotificationOperations.OffersCheckManager.OperationsNeedToBeAccredited.approvaloperation;
            else if (_httpContextAccessor.HttpContext.User.UserRole() == RoleNames.MonafasatBlackListCommittee)
                operationCode = NotificationOperations.BlockManager.OperationsToBeApproved.ApproveVerificationBlockManager;
            else if (_httpContextAccessor.HttpContext.User.UserRole() == RoleNames.OffersPurchaseManager)
                operationCode = NotificationOperations.DirectPurchaseManager.OperationsOnTheTender.approvaloperation;
            else if (_httpContextAccessor.HttpContext.User.UserRole() == RoleNames.PrePlanningAuditor)
                operationCode = NotificationOperations.PrePlaningAuditor.OperationsOnPrePlaning.approvaloperation;
            else if (_httpContextAccessor.HttpContext.User.UserRole() == RoleNames.PreQualificationCommitteeManager)
                operationCode = NotificationOperations.PreQualificationManager.OperationsOnPreQualification.QualificationVerficationCodeApproval;
            else if (_httpContextAccessor.HttpContext.User.UserRole() == RoleNames.OffersOpeningAndCheckManager)
                operationCode = NotificationOperations.VROCheckManager.OperationsOnTheTender.approvaloperation;
            else if (_httpContextAccessor.HttpContext.User.UserRole() == RoleNames.EtimadOfficer)
                operationCode = NotificationOperations.EtimadOfficer.OperationsNeedApprove.approvaloperation;
            else if (_httpContextAccessor.HttpContext.User.UserRole() == RoleNames.ApproveTenderAward)
                operationCode = NotificationOperations.ApproveTenderAwarding.OperationsOnTheTender.approvaloperation;
            else if (_httpContextAccessor.HttpContext.User.UserRole() == RoleNames.OffersCheckSecretary)
                operationCode = NotificationOperations.OffersCheckSecretary.OperationsOnTheTender.BiddingVerficationCode;
            else if (_httpContextAccessor.HttpContext.User.UserRole() == RoleNames.MandatoryListApprover)
                operationCode = NotificationOperations.MandatoryListApprover.MandatoryListProducts.ApprovalOperation;
            else if (_httpContextAccessor.HttpContext.User.UserRole() == RoleNames.UnitManagerUser)
                operationCode = NotificationOperations.UnitManager.OperationsOnTheTender.SendVerificationCode;
            else if (_httpContextAccessor.HttpContext.User.UserRole() == RoleNames.UnitSpecialistLevel1)
                operationCode = NotificationOperations.UnitSecrtaryLevel1.OperationsOnTheTender.SendVerificationCode;
            else if (_httpContextAccessor.HttpContext.User.UserRole() == RoleNames.UnitSpecialistLevel2)
                operationCode = NotificationOperations.UnitSecrtaryLevel2.OperationsOnTheTender.SendVerificationCode;
            else if (_httpContextAccessor.HttpContext.User.UserRole() == RoleNames.OffersPurchaseSecretary)
                operationCode = NotificationOperations.DirectPurchaseSecretary.OperationsOnTheTender.SendVerificationCode;
            await _notificationAppService.SendNotificationDirectByUserId(operationCode, userID, verficationCodeModel);
            await _genericCommandRepository.CreateAsync(vCode);
            await _genericCommandRepository.SaveAsync();
        }
    }
}