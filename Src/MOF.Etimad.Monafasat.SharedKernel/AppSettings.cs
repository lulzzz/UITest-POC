namespace MOF.Etimad.Monafasat.SharedKernal
{
    public class SendInvitationByEmailConfiguration
    {
        public string subject { get; set; }
        public string body { get; set; }
    }

    public class InvitationSolidarityEmailConfiguration
    {
        public string SendInvitationBySms { get; set; }
        public SendInvitationByEmailConfiguration SendInvitationByEmail { get; set; }
    }

    public class SendInvitationByEmail2Configuration
    {
        public string subject { get; set; }
        public string body { get; set; }
    }

    public class InvitationEmailConfiguration
    {
        public string SendInvitationBySms { get; set; }
        public SendInvitationByEmail2Configuration SendInvitationByEmail { get; set; }
    }

    public class APIConfiguration
    {
        public string Etimad { get; set; }
        public string IDM { get; set; }
        public string OldMonafsat { get; set; }
        public string QuantityTemplates { get; set; }
        public string LocalContent { get; set; }
    }

    public class EtimadAgencyBudgetConfiguration
    {
        public string EtimadAddress { get; set; }
        public string clientid { get; set; }
        public string ClientSecret { get; set; }
    }

    public class BillingConfiguration
    {
        public string URL { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string GFSCODEForConditionalBooklet { get; set; }
        public string GFSCODEForInvitations { get; set; }
        public string GFSCODEForAddedValue { get; set; }
        public string ClientKey { get; set; }
    }

    public class WorkingTimeConfiguration
    {
        public string StatringTime { get; set; }
        public string EndTime { get; set; }
    }

    public class PHPMonafasatConfiguration
    {
        public string URL { get; set; }
        public string AccessToken { get; set; }
    }

    public class AuthorityConfiguration
    {
        public string AuthorityURL { get; set; }
        public string ApiSecret { get; set; }
        public string ApiName { get; set; }
        public string RequireHttpsMetadata { get; set; }
        public string clientid { get; set; }
        public string clientsecret { get; set; }
        public string OldMonafasatSyncAPIToken { get; set; }
    }

    public class SendMessageConfiguration
    {
        public string subject { get; set; }
        public string body { get; set; }
    }

    public class SendNotificationForSuppliersForCreatNewRelatedTenderConfiguration
    {
        public SendMessageConfiguration SendMessage { get; set; }
    }

    public class NewTableTemplateConfiguration
    {
        public string Value { get; set; }
    }

    public class ConditionsTemplateConfiguration
    {
        public string RegulationsDate { get; set; }
    }

    public class ServicesConfiguration
    {
        public string FileNetUploadService { get; set; }
        public string FileNetDownloadService { get; set; }
        public string FileNetCheckInService { get; set; }
        public string FileNetCheckOutService { get; set; }
        public string FileNetPropertiesService { get; set; }
        public string FileNetDeleteService { get; set; }
        public string BillsInquiryService { get; set; }
        public string BillUploadService { get; set; }
        public string PaymentsInquiryService { get; set; }
        public string MCICRValidationService { get; set; }
        public string COCSubscriptionInquiryService { get; set; }
        public string ZakatCertificateInquiryService { get; set; }
        public string GOSICertificateInquiryService { get; set; }
        public string NitaqatInformationInquiryService { get; set; }
        public string MOMRAContractorClassificationService { get; set; }
        public string LicenseDetailsInquiryService { get; set; }
        public string CheckFundService { get; set; }
        public string WathiqDetailsInquiry { get; set; }
        public string SubscriptionService { get; set; }
        public string SRMFrameworkAgreementManageService { get; set; }

        public string SMEASizeInquiryService { get; set; }

        public string LocalContentService { get; set; }
    }

    public class EsbSettingsConfiguration
    {
        public bool IsProduction { get; set; }
        public string ClientCertificateFindValue { get; set; }
        public string PortValue { get; set; }
        public string RepositoryID { get; set; }
        public string FolderID { get; set; }
        public string Class { get; set; }
        public string BillRefIdForBillingForSaddad { get; set; }
        public string BillAgencyCodeentifierForSaddad { get; set; }
        public string BillAgencyCodeForSaddad { get; set; }
        public string PayemntNotificationUserName { get; set; }
        public string PayemntNotificationUserPassword { get; set; }
        public string PayemntNotificationUserClaim { get; set; }
        public string BenAgencyCodeForInvitations { get; set; }
        public string BenAgencyCodeForConditionsBooklet { get; set; }
    }

    public class BillingServiceIDConfiguration
    {
        public string MonafasatBillInquiryServiceID { get; set; }
        public string MonafasatPaymentInquiryServiceID { get; set; }
        public string BillManageServiceID { get; set; }
    }

    public class BillingFunctionIDConfiguration
    {
        public string MonafasatBillInquiryFunctionID { get; set; }
        public string MonafasatPaymentInquiryFunctionID { get; set; }
        public string BillManageFunctionID { get; set; }
        public string BillManageFunctionIDForTahaseel { get; set; }
        public string BillManageBulkFunctionIDForTahaseel { get; set; }
    }

    public class ChachingConfiguration
    {
        public string CachingDays { get; set; }
        public string CachingHours { get; set; }
        public string CachingSeconds { get; set; }
        public string CachingMinutes { get; set; }
    }

    public class AwardingConfiguration
    {
        public string QualifationsValidityDays { get; set; }
    }

    public class PlaintSettingConfiguration
    {
        public string PlaintPeriod { get; set; }
        public string PlaintReviewingPeriod { get; set; }
        public string EscalationPeriod { get; set; }
        public string EscalationReviewingPeriod { get; set; }
    }

    public class QualificationSettingConfiguration
    {
        public string DefaultValue { get; set; }
    }

    public class YesserChannelIDConfiguration
    {
        public string YesserMainChannelID { get; set; }
        public string MCIValidationChannelID { get; set; }
        public string COCSubscriptionChannelID { get; set; }
        public string ZakatCertificateInquiryChannelID { get; set; }
        public string GOSICertificateInquiryChannelID { get; set; }
        public string ContractorDetailsInquiryChannelID { get; set; }
        public string NitaqatInformationInquiryChannelID { get; set; }
        public string LicenseDetailsInquiryChannelID { get; set; }
    }

    public class YesserServiceIDConfiguration
    {
        public string MCICRValidationCRServiceID { get; set; }
        public string COCSubscriptionInquiryServiceID { get; set; }
        public string ZakatCertificateInquiryServiceID { get; set; }
        public string GOSICertificateInquiryServiceID { get; set; }
        public string ContractorDetailsInquiryServiceID { get; set; }
        public string NitaqatInformationInquiryServiceID { get; set; }
        public string LicenseDetailsInquiryServiceID { get; set; }
    }

    public class YesserFunctionIDConfiguration
    {
        public string MCICRValidationCRFunctionID { get; set; }
        public string MCICRValidationCROwnersFunctionID { get; set; }
        public string COCSubscriptionInquiryByMemberIdFunctionID { get; set; }
        public string COCSubscriptionInquiryBySijilNumberFunctionID { get; set; }
        public string ZakatCertificateInquiryByCrFunctionID { get; set; }
        public string ZakatCertificateInquiryBySevenHandredCodeFunctionID { get; set; }
        public string GOSICertificateInquiryFunctionID { get; set; }
        public string ContractorDetailsInquiryFunctionID { get; set; }
        public string NitaqatInformationInquiryFunctionID { get; set; }
        public string LicenseDetailsInquiryFunctionID { get; set; }
        public string LicenseStatusInquiryFunctionID { get; set; }
    }

    public class FileNetCredintialConfiguration
    {
        public string FileNetUserID { get; set; }
        public string FileNetPassword { get; set; }
    }

    public class FileNetServiceIDConfiguration
    {
        public string FileNetCheckIn { get; set; }
        public string FileNetCheckOut { get; set; }
        public string FileNetUpload { get; set; }
        public string FileNetDownload { get; set; }
        public string FileNetDelete { get; set; }
        public string FileNetPropertiesInquiry { get; set; }
    }

    public class SRMFrameworkAgreementManageServiceIDConfiguration
    {
        public string SRMFrameworkAgreementMngServiceID { get; set; }
    }

    public class SRMFrameworkAgreementManageFunctionIDConfiguration
    {
        public string SRMFrameworkAgreementMngFunctionID { get; set; }
    }


    public class SMEASizeInquiryServiceIDConfiguration
    {
        public string SMEASizeInquiryServiceID { get; set; }
    }

    public class SMEASizeInquiryFunctionIDConfiguration
    {
        public string SMEASizeInquiryFunctionID { get; set; }
    }

    public class FileNetFunctionIDConfiguration
    {
        public string CheckInFunctionID { get; set; }
        public string CheckOutFunctionID { get; set; }
        public string UploadFunctionID { get; set; }
        public string DownloadFunctionID { get; set; }
        public string DeleteFunctionID { get; set; }
        public string UserInquiryFunctionID { get; set; }
    }

    public class RFPFieldsConfiguration
    {
        public string RegulationDate { get; set; }
        public string SecondRegulationDate { get; set; }
    }

    public class SupplierSubscriptionTypeConfiguration
    {
        public string Type { get; set; }
    }

    public class IsFileNetWorkConfiguration
    {
        public bool isFileNetWork { get; set; }
        public int TimeOutMinutes { get; set; }
    }

    public class IsSRMFrameworkAgreementWorkConfiguration
    {
        public bool isSRMFrameworkAgreementWork { get; set; }
    }

    public class IsSMEASizeInquiryWorkConfiguration
    {
        public bool isSMEASizeInquiryWork { get; set; }
    }

    public class IsLicenseDetailsConfiguration
    {
        public bool isLicenseDetailsWork { get; set; }
    }

    public class IsNitaqatInformationConfiguration
    {
        public bool isNitaqatInformationWork { get; set; }
    }

    public class IsMOMRAContractorClassificationConfiguration
    {
        public bool isMOMRAContractorClassificationWork { get; set; }
    }

    public class IsGOSICertificateConfiguration
    {
        public bool isGOSICertificateWork { get; set; }
    }

    public class IsZakatCertificateConfiguration
    {
        public bool isZakatCertificateWork { get; set; }
    }


    public class IsCOCSubscriptionConfiguration
    {
        public bool isCOCSubscriptionWork { get; set; }
    }

    public class IsMCICRValidationConfiguration
    {
        public bool isMCICRValidationWork { get; set; }
    }
    public class IsWathiqDetailsConfiguration
    {
        public bool isWathiqDetailsWork { get; set; }
    }

    public class IsNotificationConfiguration
    {
        public bool isNotificationWork { get; set; }
    }

    public class IsBillingServiceConfiguration
    {
        public bool isBillingService { get; set; }
    }

    public class UnitAgencyCodeConfiguration
    {
        public string UnitAgencyCode { get; set; }
    }

    public class IsFileScanConfiguration
    {
        public bool Value { get; set; }
        public string ServerUrl { get; set; }
        public string Port { get; set; }
    }

    public class IsOldSystemConfiguration
    {
        public bool Value { get; set; }
    }

    public class IsFileEncryptionConfiguration
    {
        public bool Value { get; set; }
    }

    public class IsAgencyBudgetConfiguration
    {
        public bool Value { get; set; }
    }

    public class VerificationCodeConfiguration
    {
        public bool Value { get; set; }
    }

    public class FileNetCertificationConfiguration
    {
        public string FileNetCertification { get; set; }
    }

    public class NotificationEndPointConfiguration
    {
        public string Value { get; set; }
    }

    public class BulkNotificationEndPointConfiguration
    {
        public string Value { get; set; }
    }

    public class MonafasatURLConfiguration
    {
        public string MonafasatURL { get; set; }
    }

    public class MonafasatLogoURLConfiguration
    {
        public string MonafasatLogoURL { get; set; }
    }

    public class BayanFormIdConfiguration
    {
        public string BayanFormId { get; set; }
    }
    public class OldFlow
    {
        public string EndDate { get; set; }
        public bool isApplied { get; set; }
    }

    public class NewAwarding
    {
        public string ReleaseDate { get; set; }
    }

    public class IsSubscriptionConfiguration
    {
        public bool isSubscription { get; set; }
    }

    public class OfferTimesConfiguration
    {
        public int StartOfferTime { get; set; }
        public int EndOfferTime { get; set; }
        public int StartOfferVacationTime { get; set; }
        public int EndOfferVacationTime { get; set; }
    }



    public class BulkNotification
    {
        public string SCId { get; set; }
        public string ServiceId { get; set; }
        public string FuncId { get; set; }
        public string EmailFrom { get; set; }
        public string BodyHeaderFuncId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }


    }

    public class RootConfigurations
    {
        public InvitationSolidarityEmailConfiguration InvitationSolidarityEmailConfiguration { get; set; }
        public InvitationEmailConfiguration InvitationEmailConfiguration { get; set; }
        public APIConfiguration APIConfiguration { get; set; }
        public EtimadAgencyBudgetConfiguration EtimadAgencyBudgetConfiguration { get; set; }
        public BillingConfiguration BillingConfiguration { get; set; }
        public WorkingTimeConfiguration WorkingTimeConfiguration { get; set; }
        public PHPMonafasatConfiguration PHPMonafasatConfiguration { get; set; }
        public AuthorityConfiguration AuthorityConfiguration { get; set; }
        public SendNotificationForSuppliersForCreatNewRelatedTenderConfiguration SendNotificationForSuppliersForCreatNewRelatedTenderConfiguration { get; set; }
        public NewTableTemplateConfiguration NewTableTemplateConfiguration { get; set; }
        public ConditionsTemplateConfiguration ConditionsTemplateConfiguration { get; set; }
        public ServicesConfiguration ServicesConfiguration { get; set; }
        public EsbSettingsConfiguration EsbSettingsConfiguration { get; set; }
        public BillingServiceIDConfiguration BillingServiceIDConfiguration { get; set; }
        public BillingFunctionIDConfiguration BillingFunctionIDConfiguration { get; set; }
        public ChachingConfiguration ChachingConfiguration { get; set; }
        public AwardingConfiguration AwardingConfiguration { get; set; }
        public PlaintSettingConfiguration PlaintSettingConfiguration { get; set; }
        public QualificationSettingConfiguration QualificationSettingConfiguration { get; set; }
        public YesserChannelIDConfiguration YesserChannelIDConfiguration { get; set; }
        public YesserServiceIDConfiguration YesserServiceIDConfiguration { get; set; }
        public YesserFunctionIDConfiguration YesserFunctionIDConfiguration { get; set; }
        public FileNetCredintialConfiguration FileNetCredintialConfiguration { get; set; }
        public FileNetServiceIDConfiguration FileNetServiceIDConfiguration { get; set; }
        public FileNetFunctionIDConfiguration FileNetFunctionIDConfiguration { get; set; }
        public RFPFieldsConfiguration RFPFieldsConfiguration { get; set; }
        public SupplierSubscriptionTypeConfiguration SupplierSubscriptionTypeConfiguration { get; set; }
        public IsFileNetWorkConfiguration isFileNetWorkConfiguration { get; set; }
        public IsLocalContentServiceConfiguration IsLocalContentServiceConfiguration { get; set; }
        public IsSRMFrameworkAgreementWorkConfiguration isSRMFrameworkAgreementWorkConfiguration { get; set; }
        public IsSMEASizeInquiryWorkConfiguration isSMEASizeInquiryWorkConfiguration { get; set; }

        public IsNotificationConfiguration isNotificationConfiguration { get; set; }
        public IsLicenseDetailsConfiguration isLicenseDetailsConfiguration { get; set; }

        public IsNitaqatInformationConfiguration isNitaqatInformationConfiguration { get; set; }

        public IsMOMRAContractorClassificationConfiguration isMOMRAContractorClassificationConfiguration { get; set; }
        public IsZakatCertificateConfiguration isZakatCertificateConfiguration { get; set; }
        public IsGOSICertificateConfiguration isGOSICertificateConfiguration { get; set; }


        public IsCOCSubscriptionConfiguration isCOCSubscriptionConfiguration { get; set; }

        public IsMCICRValidationConfiguration isMCICRValidationConfiguration { get; set; }

        public IsWathiqDetailsConfiguration isWathiqDetailsConfiguration { get; set; }

        public IsBillingServiceConfiguration isBillingServiceConfiguration { get; set; }
        public UnitAgencyCodeConfiguration UnitAgencyCodeConfiguration { get; set; }
        public IsFileScanConfiguration isFileScanConfiguration { get; set; }
        public IsOldSystemConfiguration isOldSystemConfiguration { get; set; }
        public IsFileEncryptionConfiguration isFileEncryptionConfiguration { get; set; }
        public IsAgencyBudgetConfiguration isAgencyBudgetConfiguration { get; set; }
        public VerificationCodeConfiguration VerificationCodeConfiguration { get; set; }
        public FileNetCertificationConfiguration FileNetCertificationConfiguration { get; set; }
        public NotificationEndPointConfiguration NotificationEndPointConfiguration { get; set; }
        public BulkNotificationEndPointConfiguration BulkNotificationEndPointConfiguration { get; set; }

        public MonafasatURLConfiguration MonafasatURLConfiguration { get; set; }
        public MonafasatLogoURLConfiguration MonafasatLogoURLConfiguration { get; set; }
        public BayanFormIdConfiguration BayanFormIdConfiguration { get; set; }
        public OldFlow OldFlow { get; set; }
        public NewAwarding NewAwarding { get; set; }
        public IsSubscriptionConfiguration isSubscriptionConfiguration { get; set; }

        public SRMFrameworkAgreementManageServiceIDConfiguration SRMFrameworkAgreementManageServiceIDConfiguration { get; set; }
        public SRMFrameworkAgreementManageFunctionIDConfiguration SRMFrameworkAgreementManageFunctionIDConfiguration
        {
            get; set;
        }

        public SMEASizeInquiryServiceIDConfiguration SMEASizeInquiryServiceIDConfiguration { get; set; }
        public SMEASizeInquiryFunctionIDConfiguration SMEASizeInquiryFunctionIDConfiguration
        {
            get; set;
        }

        public BulkNotification bulkNotification { get; set; }
        public OfferTimesConfiguration OfferTimesConfiguration { get; set; }

        public LocalContentServiceIDConfiguration LocalContentServiceIDConfiguration { get; set; }
        public LocalContentFunctionIDConfiguration LocalContentFunctionIDConfiguration { get; set; }
    }

    public class LocalContentServiceIDConfiguration
    {
        public string LocalContentServiceID { get; set; }
    }

    public class LocalContentFunctionIDConfiguration
    {
        public string BaselineFunctionId { get; set; }
        public string TargetPlanFunctionId { get; set; }
    }
    public class IsLocalContentServiceConfiguration
    {
        public bool isBaseLineService { get; set; }
        public bool isTargetPlanService { get; set; }
    }
}

