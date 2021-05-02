using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MOF.Etimad.Monafasat.Core.Interfaces;
using MOF.Etimad.Monafasat.Data.GenericRepository;
using MOF.Etimad.Monafasat.Integration;
using MOF.Etimad.Monafasat.Integration.Proxies;
using MOF.Etimad.Monafasat.Services;
using MOF.Etimad.Monafasat.Services.MainServices.SupplierQualificationDocument.Abstract;

namespace MOF.Etimad.Monafasat.TendersAPI.Infrastructure
{
    public static class IServiceCollectionExtension
    {

        public static void SetAllConfiguration(this IServiceCollection services, IConfiguration Configuration)
        {

            services.AddScoped<ITenderAppService, TenderAppService>();
            services.AddScoped<IVerificationService, VerificationService>();
            services.AddScoped<IVerificationQueries, VerificationQueries>();
            services.AddScoped<ITenderQueries, TenderQueries>();
            services.AddScoped<IFileNetService, FileNetService>();
            services.AddScoped<ITenderCommands, TenderCommands>();
            services.AddScoped<IOfferAppService, OfferAppService>();
            services.AddScoped<IOfferQueries, OfferQueries>();
            services.AddScoped<IOfferCommands, OfferCommands>();
            services.AddScoped<IOfferDomainService, OfferDomainService>();
            services.AddScoped<ITenderDomainService, TenderDomainService>();
            services.AddScoped<IVerificationCodeService, VerificationCodeService>();
            services.AddScoped<ICommitteeAppService, CommitteeAppService>();
            services.AddScoped<ICommitteeCommands, CommitteeCommands>();
            services.AddScoped<ICommitteeDomainService, CommitteeDomainService>();
            services.AddScoped<ICommitteeQueries, CommitteeQueries>();
            services.AddScoped<IPrePlanningAppService, PrePlanningAppService>();
            services.AddScoped<IPrePlanningCommands, PrePlanningCommands>();
            services.AddScoped<IPrePlanningDomainService, PrePlanningDomainService>();
            services.AddScoped<IPrePlanningQueries, PrePlanningQueries>();
            services.AddScoped<IVerificationCodeService, VerificationCodeService>();
            services.AddScoped<IBlockAppService, BlockAppService>();
            services.AddScoped<IBlockQueries, BlockQueries>();
            services.AddScoped<IBlockCommands, BlockCommands>();
            services.AddScoped<IEnquiryAppService, EnquiryAppService>();
            services.AddScoped<IYasserProxy, YasserProxy>();
            services.AddScoped<IEnquiryCommands, EnquiryCommands>();
            services.AddScoped<IEnquiryQueries, EnquiryQueries>();
            services.AddScoped<IEnquiryDomainService, EnquiryDomainService>();
            services.AddScoped<IReportsService, ReportsService>();
            services.AddScoped<IIDMAppService, IDMAppService>();
            services.AddScoped<IIDMQueries, IDMQueries>();
            services.AddScoped<ICommandService, CommandService>();
            services.AddScoped<IAuthorizationService, AuthorizationService>();
            services.AddScoped<ISupplierService, SupplierService>();
            services.AddScoped<IQualificationDomainService, QualificationDomainService>();
            services.AddScoped<IQualificationCommands, QualificationCommands>();
            services.AddScoped<IQualificationAppService, QualificationAppService>();
            services.AddScoped<ISupplierQualificationDocumentDomainService, SupplierQualificationDocumentDomainService>();
            services.AddScoped<IQualificationQueries, QualificationQueries>();
            services.AddScoped<IGenericCommandRepository, GenericCommandRepository>();
            services.AddScoped<ISupplierCommands, SupplierCommands>();
            services.AddScoped<ISupplierQueries, SupplierQueries>();
            services.AddScoped<IBillQueries, BillQueries>();
            services.AddScoped<IBillCommand, BillCommand>();
            services.AddScoped<IBillArchiveCommand, BillArchiveCommand>();
            services.AddScoped<IBillAppService, BillAppService>();
            services.AddScoped<IBranchAppService, BranchAppService>();
            services.AddScoped<IBranchServiceCommand, BranchServiceCommand>();
            services.AddScoped<IBranchServiceQueries, BranchServiceQueries>();
            services.AddScoped<IBranchServiceDomain, BranchServiceDomain>();
            services.AddScoped<IManageUsersAssignationAppService, ManageUsersAssignationAppService>();
            services.AddScoped<IPushNotificationService, PushNotificationService>();
            services.AddScoped<INotificationAppService, NotificationAppService>();
            services.AddScoped<INotificationQueries, NotificationQueries>();
            services.AddScoped<IINotificationCommands, NotificationCommands>();
            services.AddScoped<IFileNetScan, FileNetScan>();
            services.AddScoped<ICommunicationAppService, CommunicationAppService>();
            services.AddScoped<ICommunicationCommands, CommunicationCommands>();
            services.AddScoped<ICommunicationDomainService, CommunicationDomainService>();
            services.AddScoped<ICommunicationQueries, CommunicationQueries>();
            services.AddScoped<INegotiationAppService, NegotiationAppService>();
            services.AddScoped<INegotiationCommands, NegotiationCommands>();
            services.AddScoped<INegotiationQueries, NegotiationQueries>();
            services.AddScoped<INegotiationDomainService, NegotiationDomainService>();
            services.AddScoped<ICheckFundProxy, CheckFundProxy>();
            services.AddScoped<IQuantityTemplatesProxy, QuantityTemplatesProxy>();
            services.AddScoped<IAgencyBudgetService, AgencyBudgetService>();
            services.AddScoped<ISubscriptionService, SubscriptionService>();
            services.AddScoped<IWathiqService, WathiqServices>();
            services.AddScoped<IMandatoryListAppService, MandatoryListAppService>();
            services.AddScoped<IMandatoryListCommands, MandatoryListCommands>();
            services.AddScoped<IMandatoryListQueries, MandatoryListQueries>();
            services.AddTransient<INationalProductSettingsAppService, NationalProductSettingsAppService>();
            services.AddTransient<INationalProductSettingsCommandService, NationalProductSettingsCommandService>();
            services.AddTransient<INationalProductSettingsQueryService, NationalProductSettingsQueryService>();

            #region [Pre-Qualification]

            services.AddScoped<ISupplierQualificationDocumentAppService, SupplierQualificationDocumentAppService>();
            services.AddScoped<ISupplierQualificationDocumentCommands, SupplierQualificationDocumentCommands>();
            services.AddScoped<ISupplierQualificationDocumentQueries, SupplierQualificationDocumentQueries>();
            services.AddScoped<ISupplierQualificationDomainService, SupplierQualificationDomainService>();
            services.AddScoped<ISupplierQualificationDocumentDomainService, SupplierQualificationDocumentDomainService>();

            #endregion

            #region[ Proxies ]
            services.AddScoped<INotificationProxy, NotificationProxy>();
            services.AddScoped<IFileNetProxy, FileNetProxy>();
            services.AddScoped<IBillingProxy, BillingProxy>();
            services.AddScoped<IIDMProxy, IDMProxy>();
            services.AddScoped<IYasserProxy, YasserProxy>();
            services.AddScoped<IContentEncryptionManger, ContentEncryptionManger>();
            services.AddScoped<ICertificateEncryption, CertificateEncryption>();
            services.AddScoped<IQuantityTemplatesProxy, QuantityTemplatesProxy>();
            services.AddScoped<IAgencyBudgetProxy, AgencyBudgetProxy>();
            services.AddScoped<ISubscriptionProxy, SubscriptionProxy>();
            services.AddScoped<IWathiqProxy, WathiqProxy>();
            services.AddScoped<ISRMFrameworkAgreementManageProxy, SRMFrameworkAgreementManageProxy>();
            services.AddScoped<ISMEASizeInquiryProxy, SMEASizeInquiryProxy>();
            #endregion

            #region Announcement
            services.AddScoped<IAnnouncementAppService, AnnouncementAppService>();
            services.AddScoped<IAnnouncementQueries, AnnouncementQueries>();
            services.AddScoped<IAnnouncementCommands, AnnouncementCommands>();
            services.AddScoped<IAnnouncementDomainService, AnnouncementDomainService>();
            #endregion
        }
    }
}
