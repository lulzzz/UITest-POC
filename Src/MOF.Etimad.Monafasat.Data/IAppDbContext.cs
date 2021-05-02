using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MOF.Etimad.Monafasat.Core;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Core.Entities.Lookups;
using MOF.Etimad.Monafasat.Core.Entities.Negotiations;
using MOF.Etimad.Monafasat.Core.Entities.QualificationEvaluation;
using MOF.Etimad.Monafasat.Core.Entities.Settings;
using System.Threading;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Data
{
    public interface IAppDbContext
    {
        Task<string> GenerateReferenceNumber(int ProcessID);
        DbSet<LocalContentSetting> LocalContentSettings { get; set; }
        DbSet<ConfigurationSetting> ConfigurationSettings { get; set; }
        DbSet<MandatoryList> MandatoryLists { get; set; }
        DbSet<MandatoryListProduct> MandatoryListProducts { get; set; }
        DbSet<MandatoryListChangeRequest> MandatoryListsChangeRequests { get; set; }
        DbSet<MandatoryListProductChangeRequest> MandatoryListProductsChangeRequests { get; set; }
        DbSet<PrePlanning> PrePlannings { get; set; }
        DbSet<PrePlanningArea> PrePlanningAreas { get; set; }
        DbSet<PrePlanningProjectType> PrePlanningProjectTypes { get; set; }
        DbSet<PrePlanningCountry> PrePlanningCountries { get; set; }
        DbSet<NotificationOperationCode> NotificationOperationCodes { get; set; }
        DbSet<NotificationCategory> NotificationCategories { get; set; }
        DbSet<NotifacationStatusEntity> NotifacationStatusEntities { get; set; }
        DbSet<UserProfile> UserProfiles { get; set; }
        DbSet<UserNotificationSetting> UserNotificationSettings { get; set; }
        DbSet<RequestsRejectionType> RequestsRejectionTypes { get; set; }
        DbSet<BaseNotification> Notifications { get; set; }
        DbSet<NotificationPanel> PanelNotifications { get; set; }
        DbSet<UserAudit> UserAuditEvents { get; set; }
        DbSet<Tender> Tenders { get; set; }
        DbSet<VacationsDate> VacationsDates { get; set; }
        DbSet<TenderHistory> TenderHistories { get; set; }
        DbSet<TenderArea> TenderAreas { get; set; }
        DbSet<TenderConstructionWork> TenderConstructionWorks { get; set; }
        DbSet<TenderMaintenanceRunnigWork> TenderMentainanceRunnigWorks { get; set; }
        DbSet<FavouriteSupplierTender> FavouriteSupplierTenders { get; set; }
        DbSet<FavouriteSupplierList> FavouriteSupplierLists { get; set; }
        DbSet<FavouriteSupplier> FavouriteSuppliers { get; set; }
        DbSet<TenderActivity> TenderActivities { get; set; }
        DbSet<OfferSolidarity> OfferSolidarities { get; set; }
        DbSet<OfferSolidarityStatus> SolidarityStatus { get; set; }
        DbSet<SupplierCombinedDetail> SupplierCombinedDetails { get; set; }
        DbSet<TenderAttachment> TenderAttachments { get; set; }
        DbSet<RejectionHistory> RejectionHistory { get; set; }
        DbSet<TenderDatesChange> TenderRevisionDate { get; set; }
        DbSet<Committee> Committees { get; set; }
        DbSet<CommitteeUser> CommitteeUsers { get; set; }
        DbSet<Invitation> Invitations { get; set; }
        DbSet<SupplierTenderQuantityTableItemJson> SupplierTenderQuantityTableItemJsons { get; set; }
        DbSet<UnRegisteredSuppliersInvitation> UnRegisteredSuppliersInvitation { get; set; }
        DbSet<Address> addresses { get; set; }
        DbSet<Bank> Banks { get; set; }
        DbSet<TenderFeesType> TenderFeesTypes { get; set; }
        DbSet<VerificationCode> VerificationCode { get; set; }
        DbSet<SupplierViolator> supplierViolators { get; set; }
        DbSet<GovAgency> GovAgencies { get; set; }
        DbSet<Supplier> Suppliers { get; set; }
        DbSet<SupplierUserProfile> SupplierUserProfiles { get; set; }
        DbSet<TenderCountry> TenderCountries { get; set; }
        DbSet<TenderChangeRequest> TenderChangeRequests { get; set; }
        DbSet<TenderAttachmentChanges> AttachmentChanges { get; set; }
        DbSet<TenderUnitStatus> TenderUnitStatuses { get; set; }
        DbSet<TenderUnitStatusesHistory> TenderUnitStatusesHistories { get; set; }
        DbSet<PostQualificationSuppliersInvitations> PostQualificationSuppliersInvitations { get; set; }
        DbSet<SupplierExtendOfferDatesRequest> SupplierExtendOfferDatesRequests { get; set; }
        DbSet<AgencyCommunicationRequest> AgencyCommunicationRequests { get; set; }
        DbSet<PlaintRequest> PlaintRequests { get; set; }
        DbSet<EscalationRequest> EscalationRequests { get; set; }
        DbSet<PlaintRequestAttachment> AgencyCommunicationAttachments { get; set; }
        DbSet<TechnicianReportAttachment> TechnicianReportAttachments { get; set; }
        DbSet<ExtendOffersValidity> ExtendOffersValiditys { get; set; }
        DbSet<ExtendOffersValidityAttachment> ExtendOffersValidityAttachments { get; set; }
        DbSet<ExtendOffersValiditySupplier> ExtendOffersValiditySuppliers { get; set; }
        DbSet<TenderUnitAssign> TenderUnitAssigns { get; set; }
        DbSet<TenderUnitUpdateType> TenderUnitUpdateTypes { get; set; }
        DbSet<TenderAwardingHistory> TenderAwardingHistories { get; set; }
        DbSet<DeviceToken> DeviceTokens { get; set; }
        DbSet<DeviceTokenNotification> DeviceTokenNotifications { get; set; }
        DbSet<MobileAlert> MobileAlerts { get; set; }
        DbSet<AddressType> AddressTypes { get; set; }
        DbSet<SyncDataWithOldMonafasat> SyncDataWithOldMonafasat { get; set; }
        DbSet<AgreementType> AgreementTypes { get; set; }
        DbSet<TenderAgreementAgency> TenderAgreementAgencies { get; set; }
        DbSet<Template> ActivityTemplates { get; set; }
        DbSet<SupplierTenderQuantityTable> SupplierTenderQuantityTables { get; set; }
        DbSet<SupplierTenderQuantityTableItem> SupplierTenderQuantityTableItems { get; set; }
        DbSet<TenderQuantityTableChanges> TenderQuantityTableChanges { get; set; }
        DbSet<OfferlocalContentDetails> OfferlocalContentDetails { get; set; }
        DbSet<MoneyMarketSuppliers> MoneyMarketSuppliers { get; set; }

        #region Qualification

        DbSet<QualificationYear> QualificationYear { get; set; }
        DbSet<QualificationSupplierDataYearly> QualificationSupplierDataYearly { get; set; }
        DbSet<Point> Point { get; set; }
        DbSet<QualificationConfiguration> QualificationConfiguration { get; set; }
        DbSet<QualificationItem> QualificationItem { get; set; }
        DbSet<QualificationItemCategory> QualificationItemCategory { get; set; }
        DbSet<QualificationItemType> QualificationItemType { get; set; }
        DbSet<QualificationLookup> QualificationLookup { get; set; }
        DbSet<QuantityTableRowType> QuantityTableRowType { get; set; }
        DbSet<QualificationLookupsName> QualificationLookupsName { get; set; }
        DbSet<QualificationCategoryResult> QualificationCategoryResult { get; set; }
        DbSet<QualificationSubCategory> QualificationSubCategory { get; set; }
        DbSet<QualificationSubCategoryResult> QualificationSubCategoryResult { get; set; }
        DbSet<QualificationSupplierData> QualificationSupplierData { get; set; }
        DbSet<QualificationType> QualificationType { get; set; }
        DbSet<QualificationTypeCategory> QualificationTypeCategory { get; set; }
        DbSet<QualificationSupplierProject> QualificationSupplierProject { get; set; }
        DbSet<QualificationFinalResult> QualificationFinalResult { get; set; }
        DbSet<QualificationSubCategoryConfiguration> QualificationSubCategoryConfiguration { get; set; }

        DbSet<QualificationConfigurationAttachment> QualificationConfigurationAttachment { get; set; }
        #endregion

        #region Bill
        DbSet<BillInfo> BillInfos { get; set; }
        DbSet<BillArchive> BillArchives { get; set; }
        #endregion

        #region [Negotiation]
        DbSet<Negotiation> Negotiations { get; set; }
        DbSet<NegotiationFirstStage> NegotiationFirstStages { get; set; }
        DbSet<NegotiationSecondStage> NegotiationSecondStages { get; set; }
        DbSet<NegotiationFirstStageSupplier> NegotiationFirstStageSuppliers { get; set; }
        DbSet<TenderQuantityTable> TenderQuantityTables { get; set; }
        DbSet<NegotiationSupplierQuantityTable> NegotiationSupplierQuantityTables { get; set; }
        #endregion

        #region Enquiry
        DbSet<Enquiry> Enquiries { get; set; }
        DbSet<EnquiryReply> EnquiryReplies { get; set; }
        DbSet<JoinTechnicalCommittee> JoinTechnicalCommittees { get; set; }


        #endregion

        #region Blocks
        DbSet<SupplierBlock> SupplierBlock { get; set; }

        #endregion

        #region Offer Entities
        DbSet<Offer> Offers { get; set; }
        DbSet<OfferStatus> OfferStatuses { get; set; }
        DbSet<OfferHistory> OfferHistories { get; set; }
        DbSet<SupplierAttachment> SupplierAttachmentes { get; set; }
        DbSet<SupplierSpecificationAttachment> SupplierSpecificationAttachment { get; set; }
        DbSet<SupplierBankGuaranteeAttachment> SupplierBankGuaranteeAttachment { get; set; }
        DbSet<ConditionsBooklet> ConditionsBooklets { get; set; }

        #endregion

        #region Lookups
        DbSet<Area> Areas { get; set; }
        DbSet<Country> Countries { get; set; }
        DbSet<MaintenanceRunningWork> MaintenanceRunningWorks { get; set; }
        DbSet<ConstructionWork> ConstructionWorks { get; set; }
        DbSet<TenderStatus> TenderStatus { get; set; }
        DbSet<TenderType> TenderTypes { get; set; }
        DbSet<PrePlanningStatus> PrePlanningStatuses { get; set; }
        DbSet<OfferPresentationWay> OfferPresentationWays { get; set; }
        DbSet<ReasonForLimitedTenderType> ReasonForLimitedTenderTypes { get; set; }
        DbSet<ReasonForPurchaseTenderType> ReasonForPurchaseTenderTypes { get; set; }
        DbSet<Activity> Activities { get; set; }
        DbSet<InvitationStatus> InvitationStatuses { get; set; }
        DbSet<CommitteeType> CommitteeTypes { get; set; }
        DbSet<BlockType> BlockTypes { get; set; }
        DbSet<EnquiryReplyStatus> EnquiryReplyStatuses { get; set; }
        DbSet<ChangeRequestType> ChangeRequestTypes { get; set; }
        DbSet<ChangeRequestStatus> ChangeRequestStatuses { get; set; }
        DbSet<BillStatus> BillStatus { get; set; }
        DbSet<SpendingCategory> SpendingCategories { get; set; }
        DbSet<CancelationReason> CancelationReasons { get; set; }
        DbSet<AgencyCommunicationRequestStatus> AgencyCommunicationRequestStatuses { get; set; }
        DbSet<AgencyCommunicationRequestType> AgencyCommunicationRequestTypes { get; set; }
        DbSet<AgencyCommunicationPlaintStatus> PlaintStatus { get; set; }
        DbSet<TenderPlaintRequestProcedure> TenderPlaintRequestProcedures { get; set; }
        DbSet<NegotiationReason> NegotiationReasons { get; set; }
        DbSet<NegotiationStatus> NegotiationStatuses { get; set; }
        DbSet<SupplierSecondNegotiationStatus> SupplierNegotiationStatuses { get; set; }
        DbSet<NegotiationType> NegotiationTypes { get; set; }
        DbSet<TenderCreatedByType> TenderCreatedByTypes { get; set; }
        DbSet<City> Cities { get; set; }
        DbSet<YearQuarter> YearQuarters { get; set; }
        DbSet<UserRole> UserRoles { get; set; }

        DbSet<EstablishmentType> EstablishmentTypes { get; set; }
        DbSet<SP_QuantityTableCellCount> SP_QuantityTableCellCount { get; set; }
        DbSet<SP_TenderQuantityTableCellCount> SP_TendrQuantityTableCellCount { get; set; }
        DbSet<SP_ConvertOfferQTItemsToJson> SP_ConvertOfferQTItemsToJson { get; set; }
        DbSet<SP_AddQuantityItemsToTenderQT> SP_AddQuantityItemsToTenderQT { get; set; }
        DbSet<SP_DeleteTenderQuantityTableWithItems> SP_DeleteTenderQuantityTableWithItem { get; set; }

        #endregion

        #region [Qualification]
        DbSet<SupplierPreQualificationDocument> SupplierPreQualificationDocument { get; set; }
        DbSet<SupplierPreQualificationAttachment> SupplierPreQualificationAttachment { get; set; }

        #endregion

        #region Bidding Round

        DbSet<BiddingRound> BiddingRounds { get; set; }
        DbSet<BiddingRoundOffer> BiddingRoundOffers { get; set; }
        DbSet<BiddingRoundStatus> BiddingRoundStatuses { get; set; }

        #endregion

        #region Breanch
        DbSet<Branch> Branch { get; set; }
        DbSet<BranchAddress> BranchAddress { get; set; }
        DbSet<BranchCommittee> BranchCommittees { get; set; }
        DbSet<BranchUser> BranchUsers { get; set; }
        #endregion

        #region ConditionTemplate
        DbSet<TenderConditionsTemplate> TenderConditionsTemplates { get; set; }
        DbSet<ConditionsTemplateCertificate> ConditionsTemplateCertificates { get; set; }
        DbSet<VendorCertificates> VendorCertificates { get; set; }

        #endregion

        #region Announcement
        DbSet<Announcement> Announcements { get; set; }
        DbSet<AnnouncementJoinRequest> AnnouncementJoinRequests { get; set; }

        DbSet<AnnouncementStatus> AnnouncementStatus { get; set; }
        DbSet<AnnouncementMaintenanceRunnigWork> AnnouncementMaintenanceRunnigWork { get; set; }
        DbSet<AnnouncementConstructionWork> AnnouncementConstructionWork { get; set; }
        DbSet<AnnouncementActivity> AnnouncementActivity { get; set; }
        DbSet<AnnouncementArea> AnnouncementArea { get; set; }
        DbSet<AnnouncementCountry> AnnouncementCountry { get; set; }
        DbSet<AnnouncementHistory> AnnouncementHistory { get; set; }

        #endregion Announcement


        #region AnnouncementSupplierList
        DbSet<AnnouncementSupplierTemplate> AnnouncementSupplierTemplate { get; set; }
        DbSet<AnnouncementStatusSupplierTemplate> AnnouncementStatusSupplierTemplate { get; set; }
        DbSet<AnnouncementMaintenanceRunnigWorkSupplierTemplate> AnnouncementMaintenanceRunnigWorkSupplierTemplate { get; set; }
        DbSet<AnnouncementConstructionWorkSupplierTemplate> AnnouncementConstructionWorkSupplierTemplate { get; set; }
        DbSet<AnnouncementActivitySupplierTemplate> AnnouncementActivitySupplierTemplate { get; set; }
        DbSet<AnnouncementAreaSupplierTemplate> AnnouncementAreaSupplierTemplate { get; set; }
        DbSet<AnnouncementCountrySupplierTemplate> AnnouncementCountrySupplierTemplate { get; set; }
        DbSet<AnnouncementHistorySupplierTemplate> AnnouncementHistorySupplierTemplate { get; set; }
        DbSet<AnnouncementJoinRequestSupplierTemplate> AnnouncementRequestSupplierTemplate { get; set; }
        DbSet<AnnouncementJoinRequestStatusSupplierTemplate> AnnouncementJoinRequestStatusSupplierTemplate { get; set; }
        DbSet<AnnouncementSuppliersTemplateAttachment> AnnouncementSuppliersTemplateAttachment { get; set; }
        DbSet<AnnouncementTemplateJoinRequestAttachment> AnnouncementTemplateJoinRequestAttachment { get; set; }
        DbSet<AnnouncementTemplateJoinRequestHistory> AnnouncementTemplateJoinRequestHistory { get; set; }
        DbSet<LinkedAgenciesAnnouncementTemplate> LinkedAgenciesAnnouncementTemplate { get; set; }

        #endregion
        #region Views
        public DbSet<SupplierQuantityTableItemView> SupplierQuantityTableItemsView { get; set; }
        public DbSet<TenderQuantityTableItemView> TenderQuantityTableItemsView { get; set; }
        public DbSet<NegotiationQuantityTableItemView> NegotiationQuantityTableItemsView { get; set; }
        #endregion


        #region Version

        DbSet<TenderDates> TenderDates { get; set; }
        DbSet<TenderAddress> TenderAddresses { get; set; }
        DbSet<TenderLocalContent> TenderLocalContents { get; set; }
        DbSet<VersionHistory> Versions { get; set; }
        DbSet<VersionType> VersionTypes { get; set; }
        DbSet<TenderVersion> TenderVersions { get; set; }
        DbSet<ActivityTemplateVersion> ActivityVersions { get; set; }

        #endregion
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
        int SaveChanges();
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        DbSet<VerificationType> VerificationTypes { get; set; }
        DbSet<PlaintRequestNotification> PlaintRequestNotifications { get; set; }

         DbSet<LocalContentMechanismPreference> LocalContentMechanismPreference { get; set; }
         DbSet<LocalContentMechanism> LocalContentMechanism { get; set; }
    }
}
