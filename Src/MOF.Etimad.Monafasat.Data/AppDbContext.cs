using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;
using MOF.Etimad.Monafasat.Core;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Core.Entities.Lookups;
using MOF.Etimad.Monafasat.Core.Entities.Negotiations;
using MOF.Etimad.Monafasat.Core.Entities.QualificationEvaluation;
using MOF.Etimad.Monafasat.Core.Entities.Settings;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.SharedKernel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Data
{
    public class SP_GenerateReferenceNumber
    {
        public string ReferenceNumberSequence { get; set; }
    }
    public class AppDbContext : DbContext, IAppDbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AppDbContext(DbContextOptions<AppDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        #region
        public DbSet<LocalContentSetting> LocalContentSettings { get; set; }
        public DbSet<ConfigurationSetting> ConfigurationSettings { get; set; }
        #endregion
        #region Mandatory List

        public DbSet<MandatoryList> MandatoryLists { get; set; }
        public DbSet<MandatoryListProduct> MandatoryListProducts { get; set; }
        public DbSet<MandatoryListStatus> MandatoryListStatuses { get; set; }
        public DbSet<MandatoryListChangeRequest> MandatoryListsChangeRequests { get; set; }
        public DbSet<MandatoryListProductChangeRequest> MandatoryListProductsChangeRequests { get; set; }
        public DbSet<MandatoryListChangeRequestStatus> MandatoryListChangeRequestStatuses { get; set; }

        #endregion

        #region Bill
        public DbSet<BillInfo> BillInfos { get; set; }
        public DbSet<BillArchive> BillArchives { get; set; }

        #endregion

        #region Tender
        public DbSet<Tender> Tenders { get; set; }
        public DbSet<VacationsDate> VacationsDates { get; set; }
        public DbSet<TenderHistory> TenderHistories { get; set; }
        public DbSet<TenderArea> TenderAreas { get; set; }
        public DbSet<TenderChangeRequest> TenderChangeRequests { get; set; }
        public DbSet<TenderConstructionWork> TenderConstructionWorks { get; set; }

        public void Seed()
        {
            throw new NotImplementedException();
        }

        public DbSet<TenderMaintenanceRunnigWork> TenderMentainanceRunnigWorks { get; set; }
        public DbSet<FavouriteSupplierTender> FavouriteSupplierTenders { get; set; }
        public DbSet<FavouriteSupplierList> FavouriteSupplierLists { get; set; }
        public DbSet<FavouriteSupplier> FavouriteSuppliers { get; set; }
        public DbSet<TenderCountry> TenderCountries { get; set; }
        public DbSet<TenderAttachmentChanges> AttachmentChanges { get; set; }
        public DbSet<SyncDataWithOldMonafasat> SyncDataWithOldMonafasat { get; set; }
        public DbSet<PostQualificationSuppliersInvitations> PostQualificationSuppliersInvitations { get; set; }
        public DbSet<SupplierExtendOfferDatesRequest> SupplierExtendOfferDatesRequests { get; set; }
        public DbSet<TenderQuantityTableChanges> TenderQuantityTableChanges { get; set; }

        #endregion

        #region Offers

        public DbSet<SupplierTenderQuantityTable> SupplierTenderQuantityTables { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<OfferStatus> OfferStatuses { get; set; }
        public DbSet<OfferHistory> OfferHistories { get; set; }

        #endregion

        #region Notifications

        public DbSet<UserNotificationSetting> UserNotificationSettings { get; set; }
        public DbSet<NotificationCategory> NotificationCategories { get; set; }
        public DbSet<NotificationOperationCode> NotificationOperationCodes { get; set; }
        public DbSet<BaseNotification> Notifications { get; set; }
        public DbSet<NotificationPanel> PanelNotifications { get; set; }

        public DbSet<NotifayTypeEntity> NotifayTypeEntities { get; set; }
        public DbSet<NotifacationStatusEntity> NotifacationStatusEntities { get; set; }
        #endregion

        #region Enquiries 
        public DbSet<Enquiry> Enquiries { get; set; }
        public DbSet<EnquiryReply> EnquiryReplies { get; set; }
        public DbSet<EnquiryReplyStatus> EnquiryReplyStatuses { get; set; }
        public DbSet<JoinTechnicalCommittee> JoinTechnicalCommittees { get; set; }
        #endregion

        #region Attachments 
        public DbSet<SupplierAttachment> SupplierAttachmentes { get; set; }
        public DbSet<SupplierSpecificationAttachment> SupplierSpecificationAttachment { get; set; }
        public DbSet<SupplierBankGuaranteeAttachment> SupplierBankGuaranteeAttachment { get; set; }
        public DbSet<TenderAttachment> TenderAttachments { get; set; }
        #endregion

        #region Users
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<UserAudit> UserAuditEvents { get; set; }
        public DbSet<GovAgency> GovAgencies { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<SupplierUserProfile> SupplierUserProfiles { get; set; }
        #endregion
        #region ConditionTemplate
        public DbSet<TenderConditionsTemplate> TenderConditionsTemplates { get; set; }
        public DbSet<TenderConditionsTemplateTechnicalDeclration> TenderConditionsTemplateTechnicalDeclrations { get; set; }
        public DbSet<TenderConditionsTemplateTechnicalOutput> TenderConditionsTemplateTechnicalOutputs { get; set; }
        public DbSet<ConditionsTemplateCertificate> ConditionsTemplateCertificates { get; set; }
        public DbSet<VendorCertificates> VendorCertificates { get; set; }
        #endregion


        #region Qualification 

        public virtual DbSet<QualificationYear> QualificationYear { get; set; }
        public virtual DbSet<QualificationSupplierDataYearly> QualificationSupplierDataYearly { get; set; }
        public virtual DbSet<Point> Point { get; set; }
        public virtual DbSet<QualificationConfiguration> QualificationConfiguration { get; set; }
        public virtual DbSet<QualificationItem> QualificationItem { get; set; }
        public virtual DbSet<QualificationItemCategory> QualificationItemCategory { get; set; }
        public virtual DbSet<QualificationItemType> QualificationItemType { get; set; }
        public virtual DbSet<QualificationLookup> QualificationLookup { get; set; }
        public virtual DbSet<QuantityTableRowType> QuantityTableRowType { get; set; }
        public virtual DbSet<QualificationLookupsName> QualificationLookupsName { get; set; }
        public virtual DbSet<QualificationCategoryResult> QualificationCategoryResult { get; set; }
        public virtual DbSet<QualificationFinalResult> QualificationFinalResult { get; set; }
        public virtual DbSet<QualificationConfigurationAttachment> QualificationConfigurationAttachment { get; set; }
        public virtual DbSet<QualificationSubCategory> QualificationSubCategory { get; set; }
        public virtual DbSet<QualificationSubCategoryResult> QualificationSubCategoryResult { get; set; }
        public virtual DbSet<QualificationSupplierData> QualificationSupplierData { get; set; }
        public virtual DbSet<QualificationType> QualificationType { get; set; }
        public virtual DbSet<QualificationTypeCategory> QualificationTypeCategory { get; set; }
        public virtual DbSet<QualificationSupplierProject> QualificationSupplierProject { get; set; }
        public virtual DbSet<QualificationSubCategoryConfiguration> QualificationSubCategoryConfiguration { get; set; }
        #endregion
        public async Task<string> GenerateReferenceNumber(int ProcessID)
        {
            //40 Qualification , 39 monafasat 
            string sqlQuery = "EXEC [dbo].[SP_GenerateReferenceNumber] " + ProcessID;
            var configuration = ConfigurationHelper.GetConfiguration(Directory.GetCurrentDirectory());
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection connection = new SqlConnection { ConnectionString = connectionString })
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand
                {
                    Connection = connection,
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@ProcessId", SqlDbType.VarChar) { Value = ProcessID });
                cmd.CommandText = "SP_GenerateReferenceNumber";
                var result = (await cmd.ExecuteScalarAsync()).ToString();
                cmd.Connection.Close();
                return result;
            }
        }
        public DbSet<TenderDatesChange> TenderRevisionDate { get; set; }
        public DbSet<Invitation> Invitations { get; set; }
        public DbSet<PlaintRequestNotification> PlaintRequestNotifications { get; set; }
        public DbSet<TechnicianReportAttachment> TechnicianReportAttachments { get; set; }
        public DbSet<Committee> Committees { get; set; }
        public DbSet<CommitteeUser> CommitteeUsers { get; set; }
        public DbSet<UnRegisteredSuppliersInvitation> UnRegisteredSuppliersInvitation { get; set; }
        public DbSet<ConditionsBooklet> ConditionsBooklets { get; set; }
        public DbSet<ConditionsTemplateSection> ConditionsTemplateSections { get; set; }
        public DbSet<ConditionTemplateActivities> ConditionTemplateActivities { get; set; }
        public DbSet<VerificationCode> VerificationCode { get; set; }
        public DbSet<SupplierBlock> SupplierBlock { get; set; }
        public DbSet<SupplierViolator> supplierViolators { get; set; }
        public DbSet<AgencyCommunicationRequest> AgencyCommunicationRequests { get; set; }
        public DbSet<PlaintRequest> PlaintRequests { get; set; }
        public DbSet<EscalationRequest> EscalationRequests { get; set; }
        public DbSet<PlaintRequestAttachment> AgencyCommunicationAttachments { get; set; }
        public DbSet<ExtendOffersValidity> ExtendOffersValiditys { get; set; }
        public DbSet<ExtendOffersValiditySupplier> ExtendOffersValiditySuppliers { get; set; }
        public DbSet<ExtendOffersValidityAttachment> ExtendOffersValidityAttachments { get; set; }
        public DbSet<BiddingRound> BiddingRounds { get; set; }
        public DbSet<BiddingRoundOffer> BiddingRoundOffers { get; set; }
        public DbSet<TenderUnitAssign> TenderUnitAssigns { get; set; }
        public DbSet<TenderUnitUpdateType> TenderUnitUpdateTypes { get; set; }
        public DbSet<TenderAwardingHistory> TenderAwardingHistories { get; set; }
        public DbSet<DeviceToken> DeviceTokens { get; set; }
        public DbSet<DeviceTokenNotification> DeviceTokenNotifications { get; set; }
        public DbSet<MobileAlert> MobileAlerts { get; set; }
        public DbSet<AddressType> AddressTypes { get; set; }
        public DbSet<VerificationType> VerificationTypes { get; set; }
        public DbSet<SupplierTenderQuantityTableItem> SupplierTenderQuantityTableItems { get; set; }
        public DbSet<SupplierTenderQuantityTableItemJson> SupplierTenderQuantityTableItemJsons { get; set; }
        public DbSet<TenderQuantityTable> TenderQuantityTables { get; set; }
        public DbSet<RejectionHistory> RejectionHistory { get; set; }
        public DbSet<OfferlocalContentDetails> OfferlocalContentDetails { get; set; }
        public DbSet<MoneyMarketSuppliers> MoneyMarketSuppliers { get; set; }
        #region Announcement
        public DbSet<Announcement> Announcements { get; set; }

        public DbSet<AnnouncementStatus> AnnouncementStatus { get; set; }
        public DbSet<AnnouncementMaintenanceRunnigWork> AnnouncementMaintenanceRunnigWork { get; set; }
        public DbSet<AnnouncementConstructionWork> AnnouncementConstructionWork { get; set; }
        public DbSet<AnnouncementActivity> AnnouncementActivity { get; set; }
        public DbSet<AnnouncementArea> AnnouncementArea { get; set; }
        public DbSet<AnnouncementCountry> AnnouncementCountry { get; set; }
        public DbSet<AnnouncementHistory> AnnouncementHistory { get; set; }
        public DbSet<AnnouncementJoinRequest> AnnouncementJoinRequests { get; set; }
        public DbSet<AnnouncementJoinRequestStatus> AnnouncementJoinRequestStatuses { get; set; }
        #endregion Announcement

        #region  AnnouncementSuppliersTemplateList

        public DbSet<AnnouncementSupplierTemplate> AnnouncementSupplierTemplate { get; set; }
        public DbSet<AnnouncementStatusSupplierTemplate> AnnouncementStatusSupplierTemplate { get; set; }
        public DbSet<AnnouncementMaintenanceRunnigWorkSupplierTemplate> AnnouncementMaintenanceRunnigWorkSupplierTemplate { get; set; }
        public DbSet<AnnouncementConstructionWorkSupplierTemplate> AnnouncementConstructionWorkSupplierTemplate { get; set; }
        public DbSet<AnnouncementActivitySupplierTemplate> AnnouncementActivitySupplierTemplate { get; set; }
        public DbSet<AnnouncementAreaSupplierTemplate> AnnouncementAreaSupplierTemplate { get; set; }
        public DbSet<AnnouncementCountrySupplierTemplate> AnnouncementCountrySupplierTemplate { get; set; }
        public DbSet<AnnouncementHistorySupplierTemplate> AnnouncementHistorySupplierTemplate { get; set; }
        public DbSet<AnnouncementJoinRequestSupplierTemplate> AnnouncementRequestSupplierTemplate { get; set; }
        public DbSet<AnnouncementJoinRequestStatusSupplierTemplate> AnnouncementJoinRequestStatusSupplierTemplate { get; set; }
        public DbSet<AnnouncementSuppliersTemplateAttachment> AnnouncementSuppliersTemplateAttachment { get; set; }
        public DbSet<AnnouncementTemplateJoinRequestAttachment> AnnouncementTemplateJoinRequestAttachment { get; set; }
        public DbSet<AnnouncementTemplateJoinRequestHistory> AnnouncementTemplateJoinRequestHistory { get; set; }
        public DbSet<LinkedAgenciesAnnouncementTemplate> LinkedAgenciesAnnouncementTemplate { get; set; }

        #endregion

        #region Branch
        public DbSet<Branch> Branch { get; set; }
        public DbSet<BranchAddress> BranchAddress { get; set; }
        public DbSet<BranchCommittee> BranchCommittees { get; set; }
        public DbSet<BranchUser> BranchUsers { get; set; }
        #endregion 

        #region Lookups 
        public DbSet<TenderCreatedByType> TenderCreatedByTypes { get; set; }
        public DbSet<NegotiationType> NegotiationTypes { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<Address> addresses { get; set; }
        public DbSet<Bank> Banks { get; set; }
        public DbSet<TenderFeesType> TenderFeesTypes { get; set; }
        public DbSet<PrePlanningStatus> PrePlanningStatuses { get; set; }
        public DbSet<PrePlanning> PrePlannings { get; set; }
        public DbSet<PrePlanningArea> PrePlanningAreas { get; set; }
        public DbSet<PrePlanningProjectType> PrePlanningProjectTypes { get; set; }
        public DbSet<PrePlanningCountry> PrePlanningCountries { get; set; }
        public DbSet<MaintenanceRunningWork> MaintenanceRunningWorks { get; set; }
        public DbSet<ConstructionWork> ConstructionWorks { get; set; }
        public DbSet<TenderStatus> TenderStatus { get; set; }
        public DbSet<TenderType> TenderTypes { get; set; }
        public DbSet<RequestsRejectionType> RequestsRejectionTypes { get; set; }
        public DbSet<SpendingCategory> SpendingCategories { get; set; }
        public DbSet<CancelationReason> CancelationReasons { get; set; }
        public DbSet<OfferPresentationWay> OfferPresentationWays { get; set; }
        public DbSet<ReasonForLimitedTenderType> ReasonForLimitedTenderTypes { get; set; }
        public DbSet<ReasonForPurchaseTenderType> ReasonForPurchaseTenderTypes { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Template> ActivityTemplates { get; set; }
        public DbSet<TenderActivity> TenderActivities { get; set; }
        public DbSet<CommitteeType> CommitteeTypes { get; set; }
        public DbSet<InvitationStatus> InvitationStatuses { get; set; }
        public DbSet<BlockType> BlockTypes { get; set; }
        public DbSet<OfferSolidarity> OfferSolidarities { get; set; }
        public DbSet<OfferSolidarityStatus> SolidarityStatus { get; set; }
        public DbSet<SupplierCombinedDetail> SupplierCombinedDetails { get; set; }
        public DbSet<BlockStatus> BlockStatus { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<ChangeRequestType> ChangeRequestTypes { get; set; }
        public DbSet<ChangeRequestStatus> ChangeRequestStatuses { get; set; }
        public DbSet<BillStatus> BillStatus { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<YearQuarter> YearQuarters { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<TenderUnitStatus> TenderUnitStatuses { get; set; }
        public DbSet<TenderUnitStatusesHistory> TenderUnitStatusesHistories { get; set; }
        public DbSet<NegotiationReason> NegotiationReasons { get; set; }
        public DbSet<NegotiationStatus> NegotiationStatuses { get; set; }
        public DbSet<SupplierSecondNegotiationStatus> SupplierNegotiationStatuses { get; set; }
        public DbSet<AgencyCommunicationRequestStatus> AgencyCommunicationRequestStatuses { get; set; }
        public DbSet<AgencyCommunicationRequestType> AgencyCommunicationRequestTypes { get; set; }
        public DbSet<AgencyCommunicationPlaintStatus> PlaintStatus { get; set; }
        public DbSet<TenderPlaintRequestProcedure> TenderPlaintRequestProcedures { get; set; }

        public DbSet<BiddingRoundStatus> BiddingRoundStatuses { get; set; }
        public DbSet<SupplierExtendOffersValidityStatus> SupplierExtendOffersValidityStatuses { get; set; }
        public DbSet<AgreementType> AgreementTypes { get; set; }
        public DbSet<TenderAgreementAgency> TenderAgreementAgencies { get; set; }

        public DbSet<EstablishmentType> EstablishmentTypes { get; set; }
        public DbSet<SP_QuantityTableCellCount> SP_QuantityTableCellCount { get; set; }
        public DbSet<SP_TenderQuantityTableCellCount> SP_TendrQuantityTableCellCount { get; set; }
        public DbSet<SP_ConvertOfferQTItemsToJson> SP_ConvertOfferQTItemsToJson { get; set; }
        public DbSet<SP_AddQuantityItemsToTenderQT> SP_AddQuantityItemsToTenderQT { get; set; }
        public DbSet<SP_DeleteTenderQuantityTableWithItems> SP_DeleteTenderQuantityTableWithItem { get; set; }
        #endregion

        #region Log And Audit 
        public DbSet<AuditLog> TransactionLogs { get; set; }
        #endregion

        #region [Negotiation]

        public DbSet<NegotiationSupplierQuantityTableItem> NegotiationSupplierQuantityTableItems { get; set; }
        public DbSet<NegotiationSupplierQuantityTable> NegotiationSupplierQuantityTables { get; set; }
        public DbSet<Negotiation> Negotiations { get; set; }
        public DbSet<NegotiationFirstStage> NegotiationFirstStages { get; set; }
        public DbSet<NegotiationSecondStage> NegotiationSecondStages { get; set; }
        public DbSet<NegotiationFirstStageSupplier> NegotiationFirstStageSuppliers { get; set; }

        #endregion

        #region Views

        public DbSet<SupplierQuantityTableItemView> SupplierQuantityTableItemsView { get; set; }
        public DbSet<TenderQuantityTableItemView> TenderQuantityTableItemsView { get; set; }
        public DbSet<NegotiationQuantityTableItemView> NegotiationQuantityTableItemsView { get; set; }
        #endregion

        #region Log And Audit

        #endregion

        #region [Qualification]
        public DbSet<SupplierPreQualificationDocument> SupplierPreQualificationDocument { get; set; }
        public DbSet<SupplierPreQualificationAttachment> SupplierPreQualificationAttachment { get; set; }
        #endregion



        #region Version
        public virtual DbSet<TenderDates> TenderDates { get; set; }
        public virtual DbSet<TenderAddress> TenderAddresses { get; set; }
        public virtual DbSet<TenderLocalContent> TenderLocalContents { get; set; }
        public virtual DbSet<VersionHistory> Versions { get; set; }
        public virtual DbSet<VersionType> VersionTypes { get; set; }
        public virtual DbSet<TenderVersion> TenderVersions { get; set; }
        public virtual DbSet<ActivityTemplateVersion> ActivityVersions { get; set; }

        public DbSet<LocalContentMechanismPreference> LocalContentMechanismPreference { get; set; }
        public DbSet<LocalContentMechanism> LocalContentMechanism { get; set; }



        #endregion

        #region methods ========================================================================================

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            FixState();
            this.PreserveAuditData(_httpContextAccessor);
            var execludeTypes = new List<Type>()
            {
             typeof(UserAudit),
             typeof(AuditLog),
             typeof(BaseNotification),
             typeof(TenderHistory),
             typeof(UserNotificationSetting)
            };
            this.LogTransaction(_httpContextAccessor, execludeTypes);
            var result = base.SaveChangesAsync(cancellationToken);
            FlushObjectState();
            return result;
        }

        public override int SaveChanges()
        {
            var result = base.SaveChanges();
            return result;
        }

        private void FlushObjectState()
        {
            foreach (var entry in ChangeTracker.Entries<IStateObject>().ToList())
            {
                ChangeIStateObjectState(entry.Entity, ObjectState.Unchanged);
            }
        }

        /// <summary>
        /// Change the state of IStateObject entity
        /// </summary>
        /// <param name="entity">IStateObject entity <typeparamref name="IStateObject"/></param>
        /// <param name="state">the ObjectState <typeparamref name="ObjectState"/></param>
        private static void ChangeIStateObjectState(object entity, ObjectState state)
        {
            if (entity is IStateObject stateObject)
            {
                var objectType = entity.GetType();
                var prop = objectType.GetProperty("State");
                prop.SetValue(entity, state);
            }
        }

        private void FixState()
        {
            foreach (var entry in ChangeTracker.Entries<IStateObject>())
            {
                IStateObject stateInfo = entry.Entity;
                entry.State = ConvertState(stateInfo.State);
            }
        }

        private EntityState ConvertState(ObjectState state)
        {
            switch (state)
            {
                case ObjectState.Added:
                    return EntityState.Added;
                case ObjectState.Modified:
                    return EntityState.Modified;
                case ObjectState.Deleted:
                    return EntityState.Deleted;
                default:
                    return EntityState.Unchanged;
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            var settings = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Error = (sender, args) =>
                {
                    args.ErrorContext.Handled = true;
                },
            };

            builder.Query<SP_GenerateReferenceNumber>();
            builder.Query<SP_QuantityTableCellCount>();
            builder.Query<SP_TenderQuantityTableCellCount>();
            builder.Query<SP_ConvertOfferQTItemsToJson>();
            builder.Query<SP_AddQuantityItemsToTenderQT>();
            builder.Query<SP_DeleteTenderQuantityTableWithItems>();

            BaseEntityMap(builder);

            builder.Entity<BillInfo>()
                                .Property(b => b.Id)
                                .ValueGeneratedOnAdd();
            var cascadeFKs = builder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);
            foreach (var fk in cascadeFKs)
                fk.DeleteBehavior = DeleteBehavior.Restrict;

            builder.Entity<Tender>().HasOne(e => e.PreQualification)
                                    .WithMany()
                                    .HasForeignKey(e => e.PreQualificationId);
            builder.Entity<Tender>().HasOne(e => e.TenderFirstStage)
                                    .WithMany()
                                    .HasForeignKey(e => e.TenderFirstStageId);
            builder.Entity<Tender>().HasOne(e => e.PostQualificationTender)
                                    .WithMany(q => q.PostQualifications)
                                    .HasForeignKey(e => e.PostQualificationTenderId);

            builder.Entity<BiddingRoundOffer>().HasOne(e => e.BiddingRound)
                                   .WithMany(q => q.BiddingRoundOffers)
                                   .HasForeignKey(e => e.BiddingRoundId);

            builder.Entity<Offer>().HasOne(o => o.OfferlocalContentDetails)
                .WithOne(l => l.Offer)
                .OnDelete(DeleteBehavior.Cascade);


            builder.Entity<TenderConditionsTemplate>()
             .Property(f => f.TenderConditionsTemplateId)
             .UseSqlServerIdentityColumn();
            builder.Entity<TenderQuantitiyItemsJson>(entity =>
            {
                entity.Property(e => e.TenderQuantityTableItems).HasConversion(
                    v => JsonConvert.SerializeObject(v),
                    v => JsonConvert.DeserializeObject<List<TenderQuantityTableItem>>(v));
            });

            builder.Entity<SupplierTenderQuantityTableItemJson>(entity =>
            {
                entity.Property(e => e.SupplierTenderQuantityTableItems).HasConversion(
                    v => JsonConvert.SerializeObject(v),
                    v => JsonConvert.DeserializeObject<List<SupplierTenderQuantityTableItem>>(v)).HasComment("Define the Quantity Table Items");
            });

            builder.Entity<TenderQuantitiyItemsChangeJson>(entity =>
            {
                entity.Property(e => e.TenderQuantityTableItemChanges).HasConversion(
                    v => JsonConvert.SerializeObject(v),
                    v => JsonConvert.DeserializeObject<List<TenderQuantityTableItemChanges>>(v));
            });
            builder.Entity<NegotiationQuantityItemJson>(entity =>
            {
                entity.Property(e => e.NegotiationSupplierQuantityTableItems).HasConversion(
                    v => JsonConvert.SerializeObject(v),
                    v => JsonConvert.DeserializeObject<List<NegotiationSupplierQuantityTableItem>>(v));
            });
            builder.Entity<SupplierUserProfile>()
            .HasKey(c => new { c.SelectedCr, c.UserProfileId });

            builder.Entity<SupplierQuantityTableItemView>().HasNoKey().ToView("VW_QTJsonItems", "Offer");
            builder.Entity<TenderQuantityTableItemView>().HasNoKey().ToView("VW_TendersQTJsonItems", "Tender");
            builder.Entity<NegotiationQuantityTableItemView>().HasNoKey().ToView("VW_NegotiationQTJsonItems", "CommunicationRequest");
            #region Entity Metadata
            InitializeHasComments(builder);

            #endregion

            InitializeCommitteeType(builder);
            InitializeAgencyCommunicationRequestStatus(builder);
            InitializeBranchAddressType(builder);
            InitializeTenderApplyType(builder);
            InitializePrePlanningStatus(builder);
            InitializeBillStatus(builder);
            InitializeTenderFeesTypes(builder);
            InitializeMandatoryListStatus(builder);
            InitializeMandatoryListChangeRequestStatus(builder);
            InitializeOfferStatus(builder);
            InitializeNotifacationStatusEntity(builder);
            InitializeNotifayTypeEntity(builder);
            InitializeBlockType(builder);
            InitializeBlockStatus(builder);
            InitializeArea(builder);
            InitializeAttachmentType(builder);
            InitializeBank(builder); InitializeVerificationType(builder);
            InitializeChangeRequestStatus(builder);
            InitializeChangeRequestType(builder);
            InitializeConstructionWork(builder);
            InitializeEnquiryReplyStatus(builder);
            InitializeInvitationStatus(builder);
            InitializeCombinedStatus(builder);
            InitializeInvitationType(builder);
            InitializeYearQuerters(builder);
            InitializeNegotiationRequestStages(builder);
            InitializeMaintenanceRunningWork(builder);
            InitializeTenderStatus(builder);
            InitializeTenderUntiStatus(builder);
            InitializeTenderType(builder);
            InitializeTenderConditionTemplateStatus(builder);
            InitializeTenderConditionTemplateSections(builder);
            InitializeRoleName(builder);
            InitializeLimitedTenderReasons(builder);
            InitializeDirectPurchaseTenderReasons(builder);
            InitializeOfferPrewsntationWay(builder);
            InitializeCommunicationRequestTypes(builder);
            InitializeSpendingCategories(builder);
            InitializeCancelationReasons(builder);
            InitializePlaintStatus(builder);
            InitializePlaintProcedures(builder);
            InitializeBiddingRoundStatus(builder);
            InitializeSupplierExtendOffersValidityStatuses(builder);
            InitializeTenderUnitUpdateType(builder);
            InitializeFirstStageNegotiationReasons(builder);
            InitializeAgreementType(builder);
            InitializeNegotiationStatus(builder);
            InitializeVendorCertificates(builder);
            InitializeSupplierNegotiationStatus(builder);
            InitializeActivityTemplates(builder);
            InitializeRequestsRejectionTypes(builder);
            InitializeQualificationItemCategory(builder);
            InitializeQualificationItemType(builder);
            InitializeQualificationSubCategory(builder);
            InitializeQualificationEvaluationItems(builder);
            InitializeQualificationLookupNames(builder);
            InitializeQuantityTableRowType(builder);
            InitializeQualificationQualityGuarantee(builder);
            InitializeQualificationEnvironmentStandardsLookup(builder);
            InitializeQualificationAvailibleNotAvailibleLookup(builder);
            InitializeQualificationYesOrNoLookup(builder);
            InitializeQualificationProfitDirAverageLookup(builder);
            InitializeQualificationResultLookup(builder);
            InitializePreQualificationType(builder);
            InitializeQualificationYear(builder);
            InitializeQualificationTypeCategory(builder);
            base.OnModelCreating(builder);
            InitializeTenderCreatedByType(builder);
            InitializeAnnouncementStatus(builder);
            InitializeAnnouncementJoinRequestStatus(builder);
            InitializeAnnouncementTemplateSupplierListType(builder);
            InitializeAnnouncementSupplierTemplateStatus(builder);
            InitializeAnnouncementSupplierTemplateJoinRequestStatus(builder);



            #region Negotioation


            #endregion

            InitializeLocalContentMechanism(builder);

        }

        #region Negotioation Meta Data   

        private void InitializeHasComments(ModelBuilder builder)
        {
            #region hamedwork
            IntializeMetaDataForAnnouncementMaintenanceRunnigWorkSupplierTemplate(builder);
            IntializeMetaDataForAnnouncementMaintenanceRunnigWork(builder);
            IntializeMetaDataForAnnouncementSupplierTemplate(builder);
            IntializeMetaDataForAnnouncementStatusSupplierTemplate(builder);
            IntializeMetaDataForAnnouncementStatus(builder);
            IntializeMetaDataForAnnouncementSuppliersTemplateAttachment(builder);
            IntializeMetaDataForAnnouncementTemplateJoinRequestAttachment(builder);
            IntializeMetaDataForAddress(builder);
            IntializeMetaDataForAddressType(builder);
            IntializeMetaDataForAreas(builder);
            IntializeMetaDataForCountries(builder);
            IntializeMetaDataForCommittees(builder);
            IntializeMetaDataForCommitteeTypes(builder);
            IntializeMetaDataForCommitteeUser(builder);
            IntializeMetaDataForBank(builder);
            IntializeMetaDataForConditionsBooklets(builder);
            IntializeMetaDataForActivityTemplate(builder);
            IntializeMetaDataForTenderFeesType(builder);
            IntializeMetaDataForConstructionWork(builder);
            IntializeMetaDataForInvitationStatus(builder);
            IntializeMetaDataForPrePlanningStatus(builder);
            IntializeMetaDataForPrePlanning(builder);
            IntializeMetaDataForPrePlanningArea(builder);
            IntializeMetaDataForPrePlanningCountry(builder);
            IntializeMetaDataForPrePlanningProjectType(builder);
            IntializeMetaDataForActivities(builder);
            IntializeMetaDataForYearQuarters(builder);
            IntializeMetaDataForVerificationTypes(builder);
            IntializeMetaDataForVerificationCode(builder);
            IntializeMetaDataForVendorCertificates(builder);
            IntializeMetaDataForVacationsDate(builder);
            IntializeMetaDataForUserRole(builder);
            IntializeMetaDataForUserProfile(builder);
            IntializeMetaDataForInvitation(builder);


            #endregion

            #region reda 
            IntializeMetaDataForTender(builder);
            IntializeMetaDataForTenderRelations(builder);
            AddMetaDataForAgencyCommunicationRequest(builder);
            AddMetaDataForPlaintAndEsclationRequest(builder);
            AddMetaDataForExtendOfferValidityRequest(builder);
            AddMetaDataForExtendOffersValiditySupplierRequest(builder);
            AddMetaDataForAgencyCommunicationRequestLookups(builder);
            #endregion

            #region Ibrahem
            // AddMetaDataAuditableEntity(builder);
            AddMetaDataNegotiation(builder);
            AddMetaDataNegotiationType(builder);
            AddMetaDataNegotiationReason(builder);
            AddMetaDataNegotiationAttachment(builder);
            AddMetaDataNegotiationFirstSatge(builder);
            AddMetaDataNegotiationFirstStageSupplier(builder);
            AddMetaDataNegotiationSecondStage(builder);
            AddMetaDataOffer(builder);
            AddMetaDataOfferPresentationWay(builder);
            AddMetaDataOfferStatus(builder);
            AddMetaDataOfferSolidarity(builder);
            AddMetaDataOfferSolidarityStatus(builder);
            AddMetaDataOfferHistory(builder);
            AddMetaDataNotifacationStatusEntity(builder);
            AddMetaDataNotificationCategory(builder);
            AddMetaDataNegotiationSupplierQuantityTable(builder);
            AddMetaDataNotificationOperationCode(builder);
            AddMetaDataSupplierTenderQuantityTableItemJson(builder);
            AddMetaDataSupplierSecondNegotiationStatus(builder);
            AddMetaDataUnRegisteredSuppliersInvitation(builder);
            AddMetaDataUserNotificationSetting(builder);
            AddMetaDataNotification(builder);
            AddMetaDataUserAudit(builder);
            AddMetaDataCity(builder);
            AddMetaDataBranchUser(builder);
            AddMetaDataBranchCommittee(builder);
            AddMetaDataBranch(builder);
            AddMetaDataDeviceToken(builder);
            AddMetaDataBranchAddress(builder);
            AddMetaDataDeviceTokenNotification(builder);
            AddMetaDataMobileAlert(builder);
            AddMetaDataTenderActivity(builder);
            AddMetaDataTenderAgreementAgency(builder);
            AddMetaDataTenderArea(builder);
            AddMetaDataTenderAttachment(builder);
            AddMetaDataTenderChangeRequest(builder);
            AddMetaDataTenderCountry(builder);
            AddMetaDataTenderMaintenanceRunnigWork(builder);
            #endregion


            #region Nawaf 
            IntializeMetaDataForSupplierPreQualificationAttachment(builder);
            IntializeMetaDataForSupplierPreQualificationDocument(builder);
            IntializeMetaDataForSupplierTenderQuantityTable(builder);
            //IntializeMetaDataForSupplierTenderQuantityTableItem(builder);
            IntializeMetaDataForBillArchive( builder);
            IntializeMetaDataForSupplierCombinedDetail( builder);


            #endregion



        }
        #region Nawaf
        private void IntializeMetaDataForSupplierCombinedDetail(ModelBuilder builder)
        { 
            builder.Entity<SupplierCombinedDetail>()
         .HasComment("Descripe the Details for suppliers [Offer owner and Combined]");

            builder.Entity<SupplierCombinedDetail>()
         .Property(e => e.CombinedDetailId)
         .HasComment("Define  the Identity  for the table ");


            builder.Entity<SupplierCombinedDetail>()
         .Property(e => e.CombinedId)
         .HasComment("Define  the supplier who applied for the tender");


            builder.Entity<SupplierCombinedDetail>()
         .Property(e => e.IsChamberJoiningAttached)
         .HasComment("Define  if the Chamber Join Request is Attached with offer papers");


            builder.Entity<SupplierCombinedDetail>()
         .Property(e => e.IsChamberJoiningValid)
         .HasComment("Define   if the Chamber Join  Is Still valid");


            builder.Entity<SupplierCombinedDetail>()
         .Property(e => e.IsCommercialRegisterAttached)
         .HasComment("Define  if the Commercial Registeration number Copy is Attached with offer papers");
            
            builder.Entity<SupplierCombinedDetail>()
         .Property(e => e.IsCommercialRegisterValid)
         .HasComment("Define   if the Commercial Registeration    is Valid  ");
             
            
            builder.Entity<SupplierCombinedDetail>()
         .Property(e => e.IsSaudizationAttached)
         .HasComment("Define  if the Saudization Copy is Attached with offer papers");
            builder.Entity<SupplierCombinedDetail>()
         .Property(e => e.IsSaudizationValidDate)
         .HasComment("Define   if the Saudization Copy   Is Still valid");
            builder.Entity<SupplierCombinedDetail>()
         .Property(e => e.IsSpecificationValidDate)
         .HasComment("Define   if the Specification Certificate  is Valid ");
            builder.Entity<SupplierCombinedDetail>()
         .Property(e => e.IsSpecificationAttached)
         .HasComment("Define  if the Specification Copy is Attached with offer papers");
            builder.Entity<SupplierCombinedDetail>()
         .Property(e => e.IsSocialInsuranceAttached)
         .HasComment("Define  if the Specification Insurance Copy  is Attached with offer papers ");
          
            builder.Entity<SupplierCombinedDetail>()
         .Property(e => e.IsZakatAttached)
         .HasComment("Define  if the Zakat Certificate Copy  is Attached with offer papers ");
            builder.Entity<SupplierCombinedDetail>()
         .Property(e => e.IsZakatValidDate)
         .HasComment("Define   if the Zakat Certificate  is Valid ");



        }
        private void IntializeMetaDataForBillArchive(ModelBuilder builder)
        { 
            builder.Entity<BillArchive>()
         .HasComment("Descripe the payment history for Conditional Booklet and Invitation");

            builder.Entity<BillArchive>()
                .Property(e => e.Id)
                .HasComment("Define  the Identity  for the table ");
            builder.Entity<BillArchive>()
                .Property(e => e.ConditionsBookletID)
                .HasComment("Define the reference to Conditions Booklet");
            builder.Entity<BillArchive>()
                .Property(e => e.InvitationId)
                .HasComment("This refere the invitation ");
            builder.Entity<BillArchive>()
                .Property(e => e.TenderId)
                .HasComment("Define the related Tender");
            builder.Entity<BillArchive>()
                .Property(e => e.TenderReferenceNumber)
                .HasComment("Related Tender Reference Number");
            builder.Entity<BillArchive>()
                .Property(e => e.BillInvoiceNumber)
                .HasComment("Define the number of Bill");
            builder.Entity<BillArchive>()
                .Property(e => e.AgencyCode)
                .HasComment("Define the Code for the agency that own the Tender");
            builder.Entity<BillArchive>()
                .Property(e => e.SupplierNumber)
                .HasComment("Define Supplier Commercial Number");




        }
        private void IntializeMetaDataForSupplierTenderQuantityTableItem(ModelBuilder builder)
        {
           
            builder.Entity<SupplierTenderQuantityTableItem>()
                .HasComment("Not Used");

            builder.Entity<SupplierTenderQuantityTableItem>()
                .Property(e => e.ColumnId)
                .HasComment("Not Used");

            builder.Entity<SupplierTenderQuantityTableItem>()
                .Property(e => e.TenderFormHeaderId)
                .HasComment("Not Used");

            builder.Entity<SupplierTenderQuantityTableItem>()
                .Property(e => e.ActivityTemplateId)
                .HasComment("Not Used");

            builder.Entity<SupplierTenderQuantityTableItem>()
                .Property(e => e.ItemNumber)
                .HasComment("Not Used");

            builder.Entity<SupplierTenderQuantityTableItem>()
                .Property(e => e.Value)
                .HasComment("Not Used");
            builder.Entity<SupplierTenderQuantityTableItem>()
                .Property(e => e.AlternativeValue)
                .HasComment("Not Used");

            builder.Entity<SupplierTenderQuantityTableItem>()
                .Property(e => e.IsDefault)
                .HasComment("Not Used");
        }
        private void IntializeMetaDataForSupplierTenderQuantityTable(ModelBuilder builder)
        {
            builder.Entity<SupplierTenderQuantityTable>()
                .HasComment("Define the Quantity Table for Supplier to Apply his offer");

            builder.Entity<SupplierTenderQuantityTable>()
                .Property(e => e.Id)
                .HasComment("Define  the Identity  for the table");
            builder.Entity<SupplierTenderQuantityTable>()
               .Property(e => e.Name)
               .HasComment("Define  Name of  the table");
            builder.Entity<SupplierTenderQuantityTable>()
               .Property(e => e.TenederQuantityId)
               .HasComment("Define releated Tender Quantity table");
            builder.Entity<SupplierTenderQuantityTable>()
               .Property(e => e.AdjustedTotalPrice)
               .HasComment("Not Used");
            builder.Entity<SupplierTenderQuantityTable>()
               .Property(e => e.AdjustedTotalVAT)
               .HasComment("Not Used");
            builder.Entity<SupplierTenderQuantityTable>()
               .Property(e => e.AdjustedTotalDiscount)
               .HasComment("Not Used");


        }
        private void IntializeMetaDataForSupplierPreQualificationAttachment(ModelBuilder builder)
        {
            builder.Entity<SupplierPreQualificationAttachment>()
                .HasComment("Define the Attachments needed for the Qualification");



            builder.Entity<SupplierPreQualificationAttachment>()
                .Property(e => e.AttachmentId)
                .HasComment("Define  the Identity  for the table");

            builder.Entity<SupplierPreQualificationAttachment>()
                .Property(e => e.FileName)
                .HasComment("Define name of the attachment");

            builder.Entity<SupplierPreQualificationAttachment>()
                .Property(e => e.FileNetReferenceId)
                .HasComment("Define the reference id from file net system");
        }

        private void IntializeMetaDataForSupplierPreQualificationDocument(ModelBuilder builder)
        {
            /** public int SupplierPreQualificationDocumentId { get; private set; }
        public string SupplierId { get; private set; }
        public int PreQualificationId { get; private set; }
        public int? StatusId { get; private set; }
        public int? PreQualificationResult { get; private set; }
        public string RejectionReason { get; private set; }*/
            builder.Entity<SupplierPreQualificationDocument>()
                .HasComment("Define the Documents applied from the supplier  Qualification");

            builder.Entity<SupplierPreQualificationDocument>()
                .Property(e => e.SupplierId)
                .HasComment("The supplier Commercial Registeration Number ");

            builder.Entity<SupplierPreQualificationDocument>()
                .Property(e => e.PreQualificationId)
                .HasComment("Define the related Qualification");

            builder.Entity<SupplierPreQualificationDocument>()
                .Property(e => e.StatusId)
                .HasComment("Define the status of the request");
            builder.Entity<SupplierPreQualificationDocument>()
                .Property(e => e.PreQualificationResult)
                .HasComment("Define result of evaluation the supplier files ");
            builder.Entity<SupplierPreQualificationDocument>()
                .Property(e => e.RejectionReason)
                .HasComment("Define the Rejection Reason if the request was rejected");
        }
        #endregion
        #region hamedWork

        private void IntializeMetaDataForAnnouncementMaintenanceRunnigWorkSupplierTemplate(ModelBuilder builder)
        {
            builder.Entity<AnnouncementMaintenanceRunnigWorkSupplierTemplate>()
                .Property(e => e.Id)
                .HasComment("Define Identity of announcement maintenance runnig work supplier template");

            builder.Entity<AnnouncementMaintenanceRunnigWorkSupplierTemplate>()
                .Property(e => e.AnnouncementId)
                .HasComment("Define forigne key  of announcement supplier template");

            builder.Entity<AnnouncementMaintenanceRunnigWorkSupplierTemplate>()
                .Property(e => e.MaintenanceRunningWorkId)
                .HasComment("Define forigne key of maintenance running work");

            builder.Entity<AnnouncementMaintenanceRunnigWorkSupplierTemplate>()
                 .Property(e => e.CreatedBy)
                 .HasComment("Determine who cretead announcement maintenance runnig work");

            builder.Entity<AnnouncementMaintenanceRunnigWorkSupplierTemplate>()
                .Property(e => e.CreatedAt)
                .HasComment("Define created date for announcement maintenance runnig work");

            builder.Entity<AnnouncementMaintenanceRunnigWorkSupplierTemplate>()
                .Property(e => e.UpdatedAt)
                .HasComment("Define updated date for announcement maintenance runnig work");

            builder.Entity<AnnouncementMaintenanceRunnigWorkSupplierTemplate>()
                 .Property(e => e.UpdatedBy)
                 .HasComment("Determine who updated announcement maintenance runnig work");

            builder.Entity<AnnouncementMaintenanceRunnigWorkSupplierTemplate>()
                 .Property(e => e.IsActive)
                 .HasComment("Define announcement maintenance runnig work is active or not");
        }

        private void IntializeMetaDataForAnnouncementMaintenanceRunnigWork(ModelBuilder builder)
        {
            builder.Entity<AnnouncementMaintenanceRunnigWork>()
                .Property(e => e.Id)
                .HasComment("Define Identity of announcement maintenance runnig work");

            builder.Entity<AnnouncementMaintenanceRunnigWork>()
                .Property(e => e.AnnouncementId)
                .HasComment("Define forigne key of announcement");

            builder.Entity<AnnouncementMaintenanceRunnigWork>()
                .Property(e => e.MaintenanceRunningWorkId)
                .HasComment("Define forigne key of maintenance running work");

            builder.Entity<AnnouncementMaintenanceRunnigWork>()
                 .Property(e => e.CreatedBy)
                 .HasComment("Determine who cretead announcement maintenance runnig work");

            builder.Entity<AnnouncementMaintenanceRunnigWork>()
                .Property(e => e.CreatedAt)
                .HasComment("Define created date for announcement maintenance runnig work");

            builder.Entity<AnnouncementMaintenanceRunnigWork>()
                .Property(e => e.UpdatedAt)
                .HasComment("Define updated date for announcement maintenance runnig work");

            builder.Entity<AnnouncementMaintenanceRunnigWork>()
                 .Property(e => e.UpdatedBy)
                 .HasComment("Determine who updated announcement maintenance runnig work");

            builder.Entity<AnnouncementMaintenanceRunnigWork>()
                 .Property(e => e.IsActive)
                 .HasComment("Define announcement maintenance runnig work is active or not");
        }

        private void IntializeMetaDataForAnnouncementSupplierTemplate(ModelBuilder builder)
        {
            builder.Entity<AnnouncementSupplierTemplate>()
                    .Property(e => e.AnnouncementId)
                    .HasComment("Define Identity Of Announcement Supplier Template");

            builder.Entity<AnnouncementSupplierTemplate>()
                    .Property(e => e.AnnouncementName)
                    .HasComment("Define Name Of Announcement Supplier Template");

            builder.Entity<AnnouncementSupplierTemplate>()
                    .Property(e => e.AnnouncementTemplateSuppliersListTypeId)
                    .HasComment("Define the  type of announcement supplier template list that public or private ");

            builder.Entity<AnnouncementSupplierTemplate>()
                    .Property(e => e.IntroAboutAnnouncementTemplate)
                    .HasComment("Define Intro about  Announcement Supplier Template");

            builder.Entity<AnnouncementSupplierTemplate>()
                    .Property(e => e.InsidKsa)
                    .HasComment("Define excution place  of  Announcement Supplier Template");

            builder.Entity<AnnouncementSupplierTemplate>()
                    .Property(e => e.Descriptions)
                    .HasComment("Define descriptions of  Announcement Supplier Template");

            builder.Entity<AnnouncementSupplierTemplate>()
                    .Property(e => e.Details)
                    .HasComment("Define details of  Announcement Supplier Template");

            builder.Entity<AnnouncementSupplierTemplate>()
                    .Property(e => e.ActivityDescription)
                    .HasComment("Define  description of activity for announcement supplier template");

            builder.Entity<AnnouncementSupplierTemplate>()
                    .Property(e => e.PublishedDate)
                    .HasComment("Define published date for announcement supplier template");


            builder.Entity<AnnouncementSupplierTemplate>()
                    .Property(e => e.AgencyCode)
                    .HasComment("Define agency code for announcement supplier template");

            builder.Entity<AnnouncementSupplierTemplate>()
                   .Property(e => e.ReferenceNumber)
                   .HasComment("Define reference number of announcement supplier template, its a unique identifier like announcement identity");

            builder.Entity<AnnouncementSupplierTemplate>()
                    .Property(e => e.TenderTypesId)
                    .HasComment("Define type of Tender");

            builder.Entity<AnnouncementSupplierTemplate>()
                   .Property(e => e.StatusId)
                   .HasComment("Define status of announcement supplier template");


            builder.Entity<AnnouncementSupplierTemplate>()
                   .Property(e => e.BranchId)
                   .HasComment("Define branch of announcement supplier template");

            builder.Entity<AnnouncementSupplierTemplate>()
                 .Property(e => e.CreatedById)
                 .HasComment("Determine who cretead announcement supplier template");


            builder.Entity<AnnouncementSupplierTemplate>()
                .Property(e => e.ApprovedBy)
                .HasComment("Determine who approved announcement supplier template");

            builder.Entity<AnnouncementSupplierTemplate>()
                .Property(e => e.IsEffectiveList)
                .HasComment("Define effective date of announcement list  is required or not");

            builder.Entity<AnnouncementSupplierTemplate>()
                .Property(e => e.EffectiveListDate)
                .HasComment("Define Effective date for announcement list");


            builder.Entity<AnnouncementSupplierTemplate>()
                .Property(e => e.AgencyAddress)
                .HasComment("Define agency address  for announcement supplier template");


            builder.Entity<AnnouncementSupplierTemplate>()
                .Property(e => e.AgencyPhone)
                .HasComment("Define agency phone  for announcement supplier template");

            builder.Entity<AnnouncementSupplierTemplate>()
                .Property(e => e.AgencyFax)
                .HasComment("Define agency fax  for announcement supplier template");


            builder.Entity<AnnouncementSupplierTemplate>()
                .Property(e => e.AgencyEmail)
                .HasComment("Define agency email  for announcement supplier template");


            builder.Entity<AnnouncementSupplierTemplate>()
                .Property(e => e.RequirementConditionsToJoinList)
                .HasComment("Define conditions necessary to join the list");


            builder.Entity<AnnouncementSupplierTemplate>()
                .Property(e => e.TemplateExtendMechanism)
                .HasComment("Define mechanism to extend the list");


            builder.Entity<AnnouncementSupplierTemplate>()
                .Property(e => e.IsRequiredAttachmentToJoinList)
                .HasComment("Define required attachment of announcement list  is required or not");



            builder.Entity<AnnouncementSupplierTemplate>()
                .Property(e => e.RequiredAttachment)
                .HasComment("Define required attachment of announcement list");


            builder.Entity<AnnouncementSupplierTemplate>()
                .Property(e => e.CancelationReason)
                .HasComment("Define cancelation reason  of announcement list");


            builder.Entity<AnnouncementSupplierTemplate>()
                .Property(e => e.ReadyForApproval)
                .HasComment("Define status for announcement list is ready for approval or not ");


            builder.Entity<AnnouncementSupplierTemplate>()
                .Property(e => e.CreatedBy)
                .HasComment("Determine who cretead announcement supplier template");

            builder.Entity<AnnouncementSupplierTemplate>()
                .Property(e => e.CreatedAt)
                .HasComment("Define created date for announcement list");

            builder.Entity<AnnouncementSupplierTemplate>()
                .Property(e => e.UpdatedAt)
                .HasComment("Define updated date for announcement list");

            builder.Entity<AnnouncementSupplierTemplate>()
                 .Property(e => e.UpdatedBy)
                 .HasComment("Determine who updated announcement supplier templat");

            builder.Entity<AnnouncementSupplierTemplate>()
                 .Property(e => e.IsActive)
                 .HasComment("Define announcement template list is active or not ");
        }

        private void IntializeMetaDataForAnnouncementStatusSupplierTemplate(ModelBuilder builder)
        {
            builder.Entity<AnnouncementStatusSupplierTemplate>()
            .Property(e => e.Id)
            .HasComment("Define identity of announcement status supplier template");

            builder.Entity<AnnouncementStatusSupplierTemplate>()
           .Property(e => e.Name)
              .HasComment("Define name of status supplier template");
        }

        private void IntializeMetaDataForAnnouncementStatus(ModelBuilder builder)
        {
            builder.Entity<AnnouncementStatus>()
            .Property(e => e.Id)
            .HasComment("Define identity of announcement status");

            builder.Entity<AnnouncementStatus>()
           .Property(e => e.Name)
              .HasComment("Define name of anouncement status");
        }

        private void IntializeMetaDataForAnnouncementSuppliersTemplateAttachment(ModelBuilder builder)
        {
            builder.Entity<AnnouncementSuppliersTemplateAttachment>()
                .Property(e => e.AnnouncementSuppliersTemplateAttachmentId)
                .HasComment("Define identity of announcement supplier template attachment");

            builder.Entity<AnnouncementSuppliersTemplateAttachment>()
                .Property(e => e.Name)
                .HasComment("Define name of announcement supplier template attachment");

            builder.Entity<AnnouncementSuppliersTemplateAttachment>()
                .Property(e => e.AttachmentTypeId)
                .HasComment("Define type of  attachment");

            builder.Entity<AnnouncementSuppliersTemplateAttachment>()
                .Property(e => e.FileNetReferenceId)
                .HasComment("Define file net id");

            builder.Entity<AnnouncementSuppliersTemplateAttachment>()
                .Property(e => e.AnnouncementId)
                .HasComment("Define forigne key of announcement supplier template");

            builder.Entity<AnnouncementSuppliersTemplateAttachment>()
                .Property(e => e.RejectionReason)
                .HasComment("Define rejection reason of attachment for announcement supplier template");

            builder.Entity<AnnouncementSuppliersTemplateAttachment>()
                .Property(e => e.ReviewStatusId)
                .HasComment("Define review status id of attachment for announcement supplier template");

            builder.Entity<AnnouncementSuppliersTemplateAttachment>()
                .Property(e => e.ChangeStatusId)
                .HasComment("Define change status id of attachment for announcement supplier template");

            builder.Entity<AnnouncementSuppliersTemplateAttachment>()
                .Property(e => e.CreatedBy)
                .HasComment("Determine who cretead attachment for announcement supplier template");

            builder.Entity<AnnouncementSuppliersTemplateAttachment>()
                .Property(e => e.CreatedAt)
                .HasComment("Define created date of attachment for announcement supplier template");

            builder.Entity<AnnouncementSuppliersTemplateAttachment>()
                .Property(e => e.UpdatedAt)
                .HasComment("Define updated date of attachment for announcement supplier template");

            builder.Entity<AnnouncementSuppliersTemplateAttachment>()
                 .Property(e => e.UpdatedBy)
                 .HasComment("Determine who updated attachment for announcement supplier template");

            builder.Entity<AnnouncementSuppliersTemplateAttachment>()
                 .Property(e => e.IsActive)
                 .HasComment("Define attachment is active or not");

        }

        private void IntializeMetaDataForAnnouncementTemplateJoinRequestAttachment(ModelBuilder builder)
        {
            builder.Entity<AnnouncementTemplateJoinRequestAttachment>()
                .Property(e => e.Id)
                .HasComment("Define identity of announcement supplier template attachment");

            builder.Entity<AnnouncementTemplateJoinRequestAttachment>()
                .Property(e => e.Name)
                .HasComment("Define name of announcement supplier template attachment");

            builder.Entity<AnnouncementTemplateJoinRequestAttachment>()
                .Property(e => e.FileNetReferenceId)
                .HasComment("Define file net id");

            builder.Entity<AnnouncementTemplateJoinRequestAttachment>()
                .Property(e => e.JoinRequestSupplierTemplateId)
                .HasComment("Define forigne key of join request announcement supplier template");

            builder.Entity<AnnouncementTemplateJoinRequestAttachment>()
                .Property(e => e.CreatedBy)
                .HasComment("Determine who cretead attachment for announcement supplier template");

            builder.Entity<AnnouncementTemplateJoinRequestAttachment>()
                .Property(e => e.CreatedAt)
                .HasComment("Define created date of attachment for announcement supplier template");

            builder.Entity<AnnouncementTemplateJoinRequestAttachment>()
                .Property(e => e.UpdatedAt)
                .HasComment("Define updated date of attachment for announcement supplier template");

            builder.Entity<AnnouncementTemplateJoinRequestAttachment>()
                 .Property(e => e.UpdatedBy)
                 .HasComment("Determine who updated attachment for announcement supplier template");

            builder.Entity<AnnouncementTemplateJoinRequestAttachment>()
                 .Property(e => e.IsActive)
                 .HasComment("Define attachment is active or not");

        }

        private void IntializeMetaDataForAddress(ModelBuilder builder)
        {
            builder.Entity<Address>()
                .Property(e => e.AddressId)
                .HasComment("Define identity of Address");

            builder.Entity<Address>()
                .Property(e => e.AddressName)
                .HasComment("Define name of Address");

            builder.Entity<Address>()
                .Property(e => e.AddressTypeId)
                .HasComment("Define type of Address");

            builder.Entity<Address>()
                .Property(e => e.BranchId)
                .HasComment("Define forigne key of branch");
        }

        private void IntializeMetaDataForAddressType(ModelBuilder builder)
        {
            builder.Entity<AddressType>()
                .Property(e => e.AddressTypeId)
                .HasComment("Define identity of Address");

            builder.Entity<AddressType>()
            .Property(e => e.AddressTypeName)
            .HasComment("Define name of Address type");
        }

        private void IntializeMetaDataForAreas(ModelBuilder builder)
        {
            builder.Entity<Area>()
                .Property(e => e.AreaId)
                .HasComment("Define identity of area");

            builder.Entity<Area>()
                .Property(e => e.NameAr)
                .HasComment("Define arabic name of Area");

            builder.Entity<Area>()
                .Property(e => e.NameEn)
                .HasComment("Define english name of Area");
        }

        private void IntializeMetaDataForCountries(ModelBuilder builder)
        {
            builder.Entity<Country>()
                .Property(e => e.CountryId)
                .HasComment("Define identity of country");

            builder.Entity<Country>()
                .Property(e => e.NameAr)
                .HasComment("Define arabic name of country");

            builder.Entity<Country>()
                .Property(e => e.NameEn)
                .HasComment("Define english name of country");


            builder.Entity<Country>()
                .Property(e => e.IsGolf)
                .HasComment("Describe that is country affiliated with the gulf cooperation council or not");
        }

        private void IntializeMetaDataForCommittees(ModelBuilder builder)
        {
            builder.Entity<Committee>()
                .Property(e => e.CommitteeId)
                .HasComment("Define identity of committee");

            builder.Entity<Committee>()
                .Property(e => e.CommitteeName)
                .HasComment("Define  name of committee");

            builder.Entity<Committee>()
                .Property(e => e.Address)
                .HasComment("Define address of committee");


            builder.Entity<Committee>()
             .Property(e => e.Phone)
             .HasComment("Define phone of committee");

            builder.Entity<Committee>()
             .Property(e => e.Fax)
             .HasComment("Define fax of committee");

            builder.Entity<Committee>()
             .Property(e => e.Email)
             .HasComment("Define e-mail of committee");


            builder.Entity<Committee>()
             .Property(e => e.PostalCode)
             .HasComment("Define postal code of committee");


            builder.Entity<Committee>()
             .Property(e => e.AgencyCode)
             .HasComment("Define agency code of committee");

            builder.Entity<Committee>()
              .Property(e => e.ZipCode)
              .HasComment("Define zip code of committee");

            builder.Entity<Committee>()
                .Property(e => e.CommitteeTypeId)
                .HasComment("it's a forigne key  that describe committe type");

            builder.Entity<Committee>()
           .Property(e => e.CreatedBy)
           .HasComment("Determine who cretead committee");

            builder.Entity<Committee>()
                .Property(e => e.CreatedAt)
                .HasComment("Define created date of committee");

            builder.Entity<Committee>()
                .Property(e => e.UpdatedAt)
                .HasComment("Define updated date of committee");

            builder.Entity<Committee>()
                 .Property(e => e.UpdatedBy)
                 .HasComment("Determine who updated committee");

            builder.Entity<Committee>()
                 .Property(e => e.IsActive)
                 .HasComment("Define committee is active or not");
        }



        private void IntializeMetaDataForCommitteeUser(ModelBuilder builder)
        {
            builder.Entity<CommitteeUser>()
              .Property(e => e.CommitteeUserId)
              .HasComment("Define identity of committee user");

            builder.Entity<CommitteeUser>()
                .Property(e => e.CommitteeId)
                .HasComment("it's a forigne key that described related committe");

            builder.Entity<CommitteeUser>()
                  .Property(e => e.RelatedAgencyCode)
                  .HasComment("Define identity related agencycode");

            builder.Entity<CommitteeUser>()
                   .Property(e => e.UserProfileId)
                   .HasComment("it's a forigne key that described user profile");

            builder.Entity<CommitteeUser>()
                   .Property(e => e.UserRoleId)
                   .HasComment("it's a forigne key that described user role");

            builder.Entity<CommitteeUser>()
                    .Property(e => e.CreatedBy)
                    .HasComment("Determine who cretead committee user");

            builder.Entity<CommitteeUser>()
                    .Property(e => e.CreatedAt)
                    .HasComment("Define created date of committee user");

            builder.Entity<CommitteeUser>()
                    .Property(e => e.UpdatedAt)
                    .HasComment("Define updated date of committee user");

            builder.Entity<CommitteeUser>()
                     .Property(e => e.UpdatedBy)
                     .HasComment("Determine who updated committee user");

            builder.Entity<CommitteeUser>()
                     .Property(e => e.IsActive)
                     .HasComment("Define committee user is active or not");

        }

        private void IntializeMetaDataForCommitteeTypes(ModelBuilder builder)
        {
            builder.Entity<CommitteeType>()
              .Property(e => e.CommitteeTypeId)
              .HasComment("Define identity of committee type");

            builder.Entity<CommitteeType>()
                .Property(e => e.NameAr)
                .HasComment("Define arabic name of committee type");

            builder.Entity<CommitteeType>()
                .Property(e => e.NameEn)
                .HasComment("Define english name of committee type");
        }



        private void IntializeMetaDataForBank(ModelBuilder builder)
        {
            builder.Entity<Bank>()
              .Property(e => e.BankId)
              .HasComment("Define identity of bank");

            builder.Entity<Bank>()
                .Property(e => e.NameAr)
                .HasComment("Define arabic name of bank");

            builder.Entity<Bank>()
                .Property(e => e.NameEn)
                .HasComment("Define english name of bank");
        }

        private void IntializeMetaDataForConditionsBooklets(ModelBuilder builder)
        {
            builder.Entity<ConditionsBooklet>()
               .Property(e => e.ConditionsBookletId)
               .HasComment("Define identity of conditions booklet");

            builder.Entity<ConditionsBooklet>()
                       .Property(e => e.CommericalRegisterNo)
                       .HasComment("it's described supplier commerical number that who purchased the condition booklet");

            builder.Entity<ConditionsBooklet>()
                     .Property(e => e.TenderId)
                     .HasComment("it's a forigne key that described Tender related to condtion booklet");

            builder.Entity<ConditionsBooklet>()
                  .Property(e => e.CreatedBy)
                  .HasComment("Determine who cretead request for purchased the condition booklet");

            builder.Entity<ConditionsBooklet>()
                    .Property(e => e.CreatedAt)
                    .HasComment("Define created date of purchased the condition booklet");

            builder.Entity<ConditionsBooklet>()
                    .Property(e => e.UpdatedAt)
                    .HasComment("Define updated date of purchased the condition booklet");

            builder.Entity<ConditionsBooklet>()
                     .Property(e => e.UpdatedBy)
                     .HasComment("Determine who updated request for purchased the condition booklet");

            builder.Entity<ConditionsBooklet>()
                     .Property(e => e.IsActive)
                     .HasComment("Define condition booklet user is active or not");
        }

        private void IntializeMetaDataForActivityTemplate(ModelBuilder builder)
        {
            builder.Entity<Template>()
                  .Property(e => e.ActivitytemplatId)
                  .HasComment("Define identity of activity template");

            builder.Entity<Template>()
                .Property(e => e.NameAr)
                .HasComment("Define arabic name of activity template");

            builder.Entity<Template>()
                .Property(e => e.NameEn)
                .HasComment("Define english name of activity template");

            builder.Entity<Template>()
               .Property(e => e.HasYears)
               .HasComment("Define activity template has years or not");
        }

        private void IntializeMetaDataForTenderFeesType(ModelBuilder builder)
        {
            builder.Entity<TenderFeesType>()
              .Property(e => e.TenderFeesTypeId)
              .HasComment("Define identity of Tender fees type");

            builder.Entity<TenderFeesType>()
                .Property(e => e.NameArabic)
                .HasComment("Define arabic name of Tender fees type");

            builder.Entity<TenderFeesType>()
                .Property(e => e.NameEnglish)
                .HasComment("Define english name of Tender fees type");
        }

        private void IntializeMetaDataForConstructionWork(ModelBuilder builder)
        {
            builder.Entity<ConstructionWork>()
              .Property(e => e.ConstructionWorkId)
              .HasComment("define identity of construction work");

            builder.Entity<ConstructionWork>()
                .Property(e => e.NameAr)
                .HasComment("define arabic name of  construction work");

            builder.Entity<ConstructionWork>()
                .Property(e => e.NameEn)
                .HasComment("define english name of  construction work");

            builder.Entity<ConstructionWork>()
                  .Property(e => e.ParentID)
                  .HasComment("define parent id of construction work");
        }

        private void IntializeMetaDataForInvitationStatus(ModelBuilder builder)
        {
            builder.Entity<InvitationStatus>()
              .Property(e => e.InvitationStatusId)
              .HasComment("define identity of invitation status");

            builder.Entity<InvitationStatus>()
                .Property(e => e.NameAr)
                .HasComment("define arabic name of invitation status");

            builder.Entity<InvitationStatus>()
                .Property(e => e.NameEn)
                .HasComment("define english name of invitation status");
        }

        private void IntializeMetaDataForPrePlanningStatus(ModelBuilder builder)
        {
            builder.Entity<PrePlanningStatus>()
              .Property(e => e.StatusId)
              .HasComment("define identity of preplanning status");

            builder.Entity<PrePlanningStatus>()
                .Property(e => e.NameAr)
                .HasComment("define arabic name of preplanning status");

            builder.Entity<PrePlanningStatus>()
                .Property(e => e.NameEn)
                .HasComment("define english name of preplanning status");
        }




        private void IntializeMetaDataForPrePlanning(ModelBuilder builder)
        {
            builder.Entity<PrePlanning>()
              .Property(e => e.PrePlanningId)
              .HasComment("define identity of pre-planning");

            builder.Entity<PrePlanning>()
                .Property(e => e.AgencyCode)
                .HasComment("describe of the pre-planning government agency");

            builder.Entity<PrePlanning>()
                .Property(e => e.BranchId)
                .HasComment("it's a foreign  key that describe of the pre-planning branch");

            builder.Entity<PrePlanning>()
              .Property(e => e.ProjectName)
              .HasComment("define project name of pre-planning");


            builder.Entity<PrePlanning>()
              .Property(e => e.ProjectNature)
              .HasComment("define project nature of pre-planning");


            builder.Entity<PrePlanning>()
              .Property(e => e.ProjectDescription)
              .HasComment("define project description of pre-planning");

            builder.Entity<PrePlanning>()
                  .Property(e => e.RejectionReason)
                  .HasComment("define reject reason of pre-planning");

            builder.Entity<PrePlanning>()
                .Property(e => e.DeleteRejectionReason)
                .HasComment("describe of the reason for denying the deletion request of pre-planning");

            builder.Entity<PrePlanning>()
             .Property(e => e.InsideKSA)
             .HasComment("define pre-planning project is inside ksa or not");

            builder.Entity<PrePlanning>()
        .Property(e => e.StatusId)
        .HasComment("define pre-planning status");

            builder.Entity<PrePlanning>()
                .Property(e => e.YearQuarterId)
                .HasComment("define pre-planning year quarter");


            builder.Entity<PrePlanning>()
                .Property(e => e.Duration)
                .HasComment("define pre-planning duration");

            builder.Entity<PrePlanning>()
                    .Property(e => e.DurationInDays)
                    .HasComment("define pre-planning duration in days");

            builder.Entity<PrePlanning>()
                    .Property(e => e.DurationInMonths)
                    .HasComment("define pre-planning duration in month");

            builder.Entity<PrePlanning>()
                    .Property(e => e.DurationInYears)
                    .HasComment("define pre-planning duration in years");

            builder.Entity<PrePlanning>()
                  .Property(e => e.IsDeleteRequested)
                  .HasComment("define pre-planning has deleted request or not");

            builder.Entity<PrePlanning>()
                   .Property(e => e.CreatedBy)
                   .HasComment("Determine who cretead pre-planning");

            builder.Entity<PrePlanning>()
                    .Property(e => e.CreatedAt)
                    .HasComment("Define created date of pre-planning");

            builder.Entity<PrePlanning>()
                    .Property(e => e.UpdatedAt)
                    .HasComment("Define updated date of pre-planning");

            builder.Entity<PrePlanning>()
                     .Property(e => e.UpdatedBy)
                     .HasComment("Determine who updated pre-planning");

            builder.Entity<PrePlanning>()
                     .Property(e => e.IsActive)
                     .HasComment("Define pre-planning is active or not");


        }


        private void IntializeMetaDataForPrePlanningArea(ModelBuilder builder)
        {

            builder.Entity<PrePlanningArea>()
         .Property(e => e.Id)
         .HasComment("define identity of pre-planning area");


            builder.Entity<PrePlanningArea>()
        .Property(e => e.AreaId)
                .HasComment("it's a foreign  key that described area related to pre-planning");

            builder.Entity<PrePlanningArea>()
        .Property(e => e.PrePlanningId)
                .HasComment("it's a foreign  key that described  pre-planning ");


            builder.Entity<PrePlanningArea>()
              .Property(e => e.CreatedBy)
              .HasComment("Determine who cretead pre-planning area");

            builder.Entity<PrePlanningArea>()
                    .Property(e => e.CreatedAt)
                    .HasComment("Define created date of pre-planning area");

            builder.Entity<PrePlanningArea>()
                    .Property(e => e.UpdatedAt)
                    .HasComment("Define updated date of pre-planning area");

            builder.Entity<PrePlanningArea>()
                     .Property(e => e.UpdatedBy)
                     .HasComment("Determine who updated pre-planning area");

            builder.Entity<PrePlanningArea>()
                     .Property(e => e.IsActive)
                     .HasComment("Define pre-planning area is active or not");

        }




        private void IntializeMetaDataForPrePlanningCountry(ModelBuilder builder)
        {

            builder.Entity<PrePlanningCountry>()
         .Property(e => e.Id)
         .HasComment("define identity of pre-planning country");


            builder.Entity<PrePlanningCountry>()
        .Property(e => e.CountryId)
                .HasComment("it's a foreign  key that described country related to pre-planning");

            builder.Entity<PrePlanningCountry>()
        .Property(e => e.PrePlanningId)
                .HasComment("it's a foreign  key that described pre-planning ");


            builder.Entity<PrePlanningCountry>()
              .Property(e => e.CreatedBy)
              .HasComment("Determine who cretead pre-planning country");

            builder.Entity<PrePlanningCountry>()
                    .Property(e => e.CreatedAt)
                    .HasComment("Define created date of pre-planning country");

            builder.Entity<PrePlanningCountry>()
                    .Property(e => e.UpdatedAt)
                    .HasComment("Define updated date of pre-planning country");

            builder.Entity<PrePlanningCountry>()
                     .Property(e => e.UpdatedBy)
                     .HasComment("Determine who updated pre-planning country");

            builder.Entity<PrePlanningCountry>()
                     .Property(e => e.IsActive)
                     .HasComment("Define pre-planning country is active or not");

        }


        private void IntializeMetaDataForPrePlanningProjectType(ModelBuilder builder)
        {

            builder.Entity<PrePlanningProjectType>()
         .Property(e => e.Id)
         .HasComment("define identity of pre-planning project type");

            builder.Entity<PrePlanningProjectType>()
        .Property(e => e.PrePlanningId)
                .HasComment("it's a foreign  key that described pre-planning ");

            builder.Entity<PrePlanningProjectType>()
                  .Property(e => e.ActivityId)
                  .HasComment("it's a foreign  key that described activity of pre-planning project type");


            builder.Entity<PrePlanningProjectType>()
              .Property(e => e.CreatedBy)
              .HasComment("Determine who cretead pre-planning project type");

            builder.Entity<PrePlanningProjectType>()
                    .Property(e => e.CreatedAt)
                    .HasComment("Define created date of pre-planning project type");

            builder.Entity<PrePlanningProjectType>()
                    .Property(e => e.UpdatedAt)
                    .HasComment("Define updated date of pre-planning project type");

            builder.Entity<PrePlanningProjectType>()
                     .Property(e => e.UpdatedBy)
                     .HasComment("Determine who updated pre-planning project type");

            builder.Entity<PrePlanningProjectType>()
                     .Property(e => e.IsActive)
                     .HasComment("Define pre-planning project type is active or not");

        }



        private void IntializeMetaDataForActivities(ModelBuilder builder)
        {

            builder.Entity<Activity>()
                 .Property(e => e.ActivityId)
                 .HasComment("define identity of activity");

            builder.Entity<Activity>()
                   .Property(e => e.NameAr)
                   .HasComment("define arabic name of activity");

            builder.Entity<Activity>()
                .Property(e => e.NameEn)
                .HasComment("define english name of activity");

            builder.Entity<Activity>()
                .Property(e => e.ParentID)
                .HasComment("define parent id of activity");
        }


        private void IntializeMetaDataForYearQuarters(ModelBuilder builder)
        {

            builder.Entity<YearQuarter>()
                 .Property(e => e.YearQuarterId)
                 .HasComment("define identity of year quarter");

            builder.Entity<YearQuarter>()
                   .Property(e => e.NameAr)
                   .HasComment("define arabic name of year quarter");

            builder.Entity<YearQuarter>()
                .Property(e => e.NameEn)
                .HasComment("define english name of year quarter");

        }

        private void IntializeMetaDataForVerificationTypes(ModelBuilder builder)
        {

            builder.Entity<VerificationType>()
                 .Property(e => e.VerificationTypeId)
                 .HasComment("define identity of verification type");

            builder.Entity<VerificationType>()
                   .Property(e => e.VerificationTypeName)
                   .HasComment("define name of verification type");
        }



        private void IntializeMetaDataForVerificationCode(ModelBuilder builder)
        {

            builder.Entity<VerificationCode>()
                 .Property(e => e.VerificationCodeId)
                 .HasComment("define identity of verification code");

            builder.Entity<VerificationCode>()
                   .Property(e => e.VerificationCodeNo)
                   .HasComment("describe verfication code number");

            builder.Entity<VerificationCode>()
                 .Property(e => e.VerificationTypeId)
                 .HasComment("it's a foreign  key that described type of verification code");

            builder.Entity<VerificationCode>()
                .Property(e => e.UserId)
                .HasComment("refer to who received verfication code");


            builder.Entity<VerificationCode>()
                    .Property(e => e.ExpiredDate)
                    .HasComment("define expiry date of verfication code");


            builder.Entity<VerificationCode>()
              .Property(e => e.CreatedBy)
              .HasComment("Determine who cretead verification code");

            builder.Entity<VerificationCode>()
                    .Property(e => e.CreatedAt)
                    .HasComment("Define created date of  verification code");

            builder.Entity<VerificationCode>()
                    .Property(e => e.UpdatedAt)
                    .HasComment("Define updated date of  verification code");

            builder.Entity<VerificationCode>()
                     .Property(e => e.UpdatedBy)
                     .HasComment("Determine who updated  verification code");

            builder.Entity<VerificationCode>()
                     .Property(e => e.IsActive)
                     .HasComment("Define  verification code  is active or not");
        }



        private void IntializeMetaDataForVendorCertificates(ModelBuilder builder)
        {

            builder.Entity<VendorCertificates>()
                 .Property(e => e.VendorCertificateId)
                 .HasComment("define identity of vendor certificate");

            builder.Entity<VendorCertificates>()
                   .Property(e => e.NameAr)
                   .HasComment("define arabic name of vendor certificate");

            builder.Entity<VendorCertificates>()
                .Property(e => e.NameEn)
                .HasComment("define english name of vendor certificate");

        }

        private void IntializeMetaDataForVacationsDate(ModelBuilder builder)
        {

            builder.Entity<VacationsDate>()
                 .Property(e => e.VacationId)
                 .HasComment("define identity of vacation");

            builder.Entity<VacationsDate>()
                   .Property(e => e.VacationName)
                   .HasComment("define name of vacation ");

            builder.Entity<VacationsDate>()
                   .Property(e => e.VacationDate)
                   .HasComment("define date of vacation ");
        }


        private void IntializeMetaDataForUserRole(ModelBuilder builder)
        {
            builder.Entity<UserRole>()
                 .Property(e => e.UserRoleId)
                 .HasComment("define identity of user role");

            builder.Entity<UserRole>()
                 .Property(e => e.Name)
                 .HasComment("define name of role");

            builder.Entity<UserRole>()
                   .Property(e => e.DisplayNameAr)
                   .HasComment("define arabic name of role");

            builder.Entity<UserRole>()
                    .Property(e => e.DisplayNameEn)
                   .HasComment("define english name of role");

            builder.Entity<UserRole>()
                 .Property(e => e.IsCombinedRole)
                .HasComment("describe notification for low budget module");
        }

        private void IntializeMetaDataForUserProfile(ModelBuilder builder)
        {

            builder.Entity<UserProfile>()
                 .Property(e => e.Id)
                 .HasComment("define identity of user profile");

            builder.Entity<UserProfile>()
                 .Property(e => e.FullName)
                 .HasComment("define full name of user profile");

            builder.Entity<UserProfile>()
                   .Property(e => e.Mobile)
                   .HasComment("define mobile of user profile");

            builder.Entity<UserProfile>()
                   .Property(e => e.Email)
                   .HasComment("define email of user profile");


            builder.Entity<UserProfile>()
                   .Property(e => e.UserName)
                   .HasComment("define user name of user profile");

            builder.Entity<UserProfile>()
                 .Property(e => e.DefaultUserRole)
                 .HasComment("define default user role of user profile");

            builder.Entity<UserProfile>()
                 .Property(e => e.RowVersion)
                 .HasComment("define row version of user profile");

            builder.Entity<UserProfile>()
                .Property(e => e.CreatedBy)
                .HasComment("Determine who cretead user profile");

            builder.Entity<UserProfile>()
                    .Property(e => e.CreatedAt)
                    .HasComment("Define created date of user profile");

            builder.Entity<UserProfile>()
                    .Property(e => e.UpdatedAt)
                    .HasComment("Define updated date of user profile");

            builder.Entity<UserProfile>()
                     .Property(e => e.UpdatedBy)
                     .HasComment("Determine who updated user profile");

            builder.Entity<UserProfile>()
                     .Property(e => e.IsActive)
                     .HasComment("Define user profile is active or not");
        }

        private void IntializeMetaDataForInvitation(ModelBuilder builder)
        {

            builder.Entity<Invitation>()
             .Property(e => e.InvitationId)
             .HasComment("define identity of invitation");

            builder.Entity<Invitation>()
                 .Property(e => e.WithdrawalDate)
                 .HasComment("define withdrawal date of invitation");

            builder.Entity<Invitation>()
             .Property(e => e.SubmissionDate)
             .HasComment("define submission date of invitation");


            builder.Entity<Invitation>()
                 .Property(e => e.SendingDate)
                 .HasComment("define sending date of invitation");

            builder.Entity<Invitation>()
              .Property(e => e.SupplierType)
              .HasComment("define supplier type  of invitation");

            builder.Entity<Invitation>()
           .Property(e => e.InvitedByQualification)
           .HasComment("define supplier invited by qualification or not");


            builder.Entity<Invitation>()
               .Property(e => e.RejectionReason)
               .HasComment("define reject reason of invitation");


            builder.Entity<Invitation>()
               .Property(e => e.CommericalRegisterNo)
               .HasComment("define supplier commerical register number");

            builder.Entity<Invitation>()
                .Property(e => e.TenderId)
                .HasComment("it's a foreign key described Tender that related to  invitation");

            builder.Entity<Invitation>()
      .Property(e => e.InvitationTypeId)
                 .HasComment("it's a foreign  key that described type of invitation");

            builder.Entity<Invitation>()
    .Property(e => e.StatusId)
               .HasComment("it's a foreign  key that described status of invitation");

            builder.Entity<Invitation>()
                .Property(e => e.CreatedBy)
                .HasComment("Determine who cretead invitation");

            builder.Entity<Invitation>()
                    .Property(e => e.CreatedAt)
                    .HasComment("Define created date of invitation");

            builder.Entity<Invitation>()
                    .Property(e => e.UpdatedAt)
                    .HasComment("Define updated date of invitation");

            builder.Entity<Invitation>()
                     .Property(e => e.UpdatedBy)
                     .HasComment("Determine who updated invitation");

            builder.Entity<Invitation>()
                     .Property(e => e.IsActive)
                     .HasComment("Define invitation is active or not");
        }

        #endregion
        #region reda 
        private void IntializeMetaDataForTender(ModelBuilder builder)
        {


            builder.Entity<Tender>()
                   .Property(e => e.TenderId)
                   .HasComment("Define Identity Of Tender");

            builder.Entity<Tender>()
               .Property(e => e.TenderName)
               .HasComment("Define Name Of Tender");

            builder.Entity<Tender>()
                   .Property(e => e.ReferenceNumber)
                   .HasComment("Define Reference Number Of Tender, its a unique Identifier Like Tender Identity");

            builder.Entity<Tender>()
                   .Property(e => e.TenderNumber)
                   .HasComment("Define Number Of Tender");
            builder.Entity<Tender>()
                   .Property(e => e.Purpose)
                   .HasComment("Define Purpose Of Tender");

            builder.Entity<Tender>()
                   .Property(e => e.ConditionsBookletPrice)
                   .HasComment("Define the price of conditions booklet Of Tender, it is determined by the government agency and supplier can buy it");
            builder.Entity<Tender>()
                   .Property(e => e.EstimatedValue)
                   .HasComment("Define the estimation value of Tender");
            builder.Entity<Tender>()
                   .Property(e => e.SupplierNeedSample)
                   .HasComment("Boolean detrmine tf the supplier need samples of the Tender");
            builder.Entity<Tender>()
                   .Property(e => e.SamplesDeliveryAddress)
                   .HasComment("Define the address of samples deleviry of the Tender");
            builder.Entity<Tender>()
                   .Property(e => e.InitialGuaranteePercentage)
                   .HasComment("Define the percentage of initial gurantee for suppliers if agency require it");
            builder.Entity<Tender>()
                   .Property(e => e.InitialGuaranteeAddress)
                   .HasComment("Define the address of initial gurantee that suppliers apply it");
            builder.Entity<Tender>()
                   .Property(e => e.LastEnqueriesDate)
                   .HasComment("Define the last date that supplier can enquiry or ask questions for Tender");
            builder.Entity<Tender>()
                   .Property(e => e.LastOfferPresentationDate)
                   .HasComment("Define the last date that supplier can apply offers on Tender");
            builder.Entity<Tender>()
                  .Property(e => e.OffersOpeningDate)
                  .HasComment("Define the opening date for opening the suppliers offers");

            builder.Entity<Tender>()
                   .Property(e => e.InsideKSA)
                   .HasComment("Define the location of Tender");
            builder.Entity<Tender>()
                   .Property(e => e.Details)
                   .HasComment("Define more information about Tender");

            builder.Entity<Tender>()
                    .Property(e => e.SubmitionDate)
                    .HasComment("Define the publish/approval date of Tender");

            builder.Entity<Tender>()
                   .Property(e => e.ActivityDescription)
                   .HasComment("Define the description of Tender activiites");

            builder.Entity<Tender>()
                   .Property(e => e.TenderAwardingType)
                   .HasComment("Define the awarding type of Tender partial or total awarding");

            builder.Entity<Tender>()
                   .Property(e => e.OffersCheckingDate)
                   .HasComment("Define the checking date for checking the suppliers offers");

            builder.Entity<Tender>()
                   .Property(e => e.TenderTypeOtherSelectionReason)
                   .HasComment("Define the reason of selecting direct purchase or limited Tender that not exist in reasons list");

            builder.Entity<Tender>()
                   .Property(e => e.AwardingStoppingPeriod)
                   .HasComment("Define the period that suppliers can add plaint on Tender after awarding between 5 and 10 days");

            builder.Entity<Tender>()
                   .Property(e => e.AwardingReportFileName)
                   .HasComment("Define the name of awarding report that entered while entering awarding data in awarding stage");

            builder.Entity<Tender>()
                   .Property(e => e.AwardingReportFileNameId)
                   .HasComment("Define the identity of awarding report that entered while entering awarding data in awarding stage");

            builder.Entity<Tender>()
                   .Property(e => e.AgreementDays)
                   .HasComment("Number of days for framework agreement");

            builder.Entity<Tender>()
                   .Property(e => e.AgreementMonthes)
                   .HasComment("Number of months for framework agreement");

            builder.Entity<Tender>()
                   .Property(e => e.AgreementYears)
                   .HasComment("Number of years for framework agreement");

            builder.Entity<Tender>()
                   .Property(e => e.NumberOfWinners)
                   .HasComment("Number of winners in the competition");

            builder.Entity<Tender>()
                   .Property(e => e.AwardingDiscountPercentage)
                   .HasComment("Value of the final guarantee");

            builder.Entity<Tender>()
                   .Property(e => e.AwardingMonths)
                   .HasComment("Final guarantee duration in months");

            builder.Entity<Tender>()
                   .Property(e => e.FinalGuaranteeDeliveryAddress)
                   .HasComment("The address of deleviry the final guarantee");

            builder.Entity<Tender>()
                   .Property(e => e.HasGuarantee)
                   .HasComment("Boolean determine if the Tender requires a final guarantee or not");

            builder.Entity<Tender>()
                   .Property(e => e.HasAlternativeOffer)
                   .HasComment("determine if the supliers can apply an alternative offers");
            builder.Entity<Tender>()
                   .Property(e => e.TenderPointsToPass)
                   .HasComment("The points needed to pass a prequalification");

            builder.Entity<Tender>()
                   .Property(e => e.TemplateYears)
                   .HasComment("number of years in quantities tables");

            builder.Entity<Tender>()
                   .Property(e => e.BuildingName)
                   .HasComment("The location of building name for samples delivery");

            builder.Entity<Tender>()
                   .Property(e => e.FloarNumber)
                   .HasComment("Floar number at buliding for samples delivery");

            builder.Entity<Tender>()
                   .Property(e => e.DepartmentName)
                   .HasComment("Department name at buliding for samples delivery");

            builder.Entity<Tender>()
                   .Property(e => e.DeliveryDate)
                   .HasComment("The date of samples delivey to supplier");

            builder.Entity<Tender>()
                   .Property(e => e.CheckingDateSet)
                   .HasComment("Is a date set when starting the checking stage");
            builder.Entity<Tender>()
                   .Property(e => e.FinancialCheckingDateSet)
                   .HasComment("Date set by the checking secretary at the start of the financial checking");

            builder.Entity<Tender>()
                   .Property(e => e.OpeningNotificationSent)
                   .HasComment("Flag that states that a notification is sent to the opening committee when the opening date is passed");

            builder.Entity<Tender>()
                   .Property(e => e.CheckingNotificationSent)
                   .HasComment("Flag that states that a notification is sent to the direct purchase committee when the checking date is tomorrow");

            builder.Entity<Tender>()
                   .Property(e => e.IsSendToEmarketPlace)
                   .HasComment("Flag determine if the awarded Tender is sent to e-market place or not");

            builder.Entity<Tender>()
                   .Property(e => e.IsNotificationSentForStoppingPeriod)
                   .HasComment("Flag determine if a notification sent after finishing the stoping period of Tender");

            builder.Entity<Tender>()
                   .Property(e => e.IsLowBudgetDirectPurchase)
                   .HasComment("determine if the Tender is low budget or not if the estimatinn value less than 30000 sar");

            builder.Entity<Tender>()
                   .Property(e => e.QualificationTypeId)
                   .HasComment("determine the type of qualification post or prequalification");

            builder.Entity<Tender>()
                   .Property(e => e.CreatedByTypeId)
                   .HasComment("determine the Tender created by vro or agency or agency related by vro");

            builder.Entity<Tender>()
                       .Property(e => e.TenderStatusId)
                       .HasComment("Status of Tender");

            builder.Entity<Tender>()
                       .Property(e => e.TenderTypeId)
                       .HasComment("Type of Tender");

            builder.Entity<Tender>()
                       .Property(e => e.InvitationTypeId)
                       .HasComment("Type of invitation on Tender public or private");

            builder.Entity<Tender>()
                       .Property(e => e.TechnicalOrganizationId)
                       .HasComment("The technical committee for reply on suppliers enquireis");

            builder.Entity<Tender>()
                       .Property(e => e.OffersOpeningCommitteeId)
                       .HasComment("The opening committee for opening offers");

            builder.Entity<Tender>()
                       .Property(e => e.OffersCheckingCommitteeId)
                       .HasComment("The Checking committee for checking and awarding Tender");

            builder.Entity<Tender>()
                   .Property(e => e.DirectPurchaseCommitteeId)
                   .HasComment("The direct purchase committee for checking and awarding Tender from type direct purchase");

            builder.Entity<Tender>()
                   .Property(e => e.VROCommitteeId)
                   .HasComment("The committe of VRO for opening, checking and awarding Tender");
            builder.Entity<Tender>()
                       .Property(e => e.PreQualificationCommitteeId)
                       .HasComment("Define PreQualification Committee");

            builder.Entity<Tender>()
                   .Property(e => e.QuantityTableVersionId)
                   .HasComment("The version of quantity table");

            builder.Entity<Tender>()
                   .Property(e => e.NationalProductPercentage)
                   .HasComment("The percentage of National Product");

            builder.Entity<Tender>()
                   .Property(e => e.UnitSpacialistWouldLikeToAttendTheCommitte)
                   .HasComment("Determine if the unit manger want to attend th committee");

            builder.Entity<Tender>()
                   .Property(e => e.TechnicalAdministrativeCapacity)
                   .HasComment("Technical administrative capacity");

            builder.Entity<Tender>()
                   .Property(e => e.FinancialCapacity)
                   .HasComment("Financial Capacity");

            builder.Entity<Tender>()
                   .Property(e => e.BonusValue)
                   .HasComment("Bonus value of competition");

            builder.Entity<Tender>()
                .Property(e => e.SpendingCategoryId)
                .HasComment("Define the spending Category of the Tender");

            builder.Entity<Tender>()
                       .Property(e => e.ConditionTemplateStageStatusId)
                       .HasComment("The Status of condition template stage");

            builder.Entity<Tender>()
                       .Property(e => e.OfferPresentationWayId)
                       .HasComment("method of apply offers by suppliers one file or two files");

            builder.Entity<Tender>()
                       .Property(e => e.ReasonForPurchaseTenderTypeId)
                       .HasComment("The reason of selecting direct purchase Tender");

            builder.Entity<Tender>()
                       .Property(e => e.ReasonForLimitedTenderTypeId)
                       .HasComment("The reason of selecting limited Tender");

            builder.Entity<Tender>()
                       .Property(e => e.PreQualificationId)
                       .HasComment("The identity that identify pre qualification on the Tender");

            builder.Entity<Tender>()
                       .Property(e => e.PreAnnouncementId)
                       .HasComment("Tha pre-announcement related to Tender");

            builder.Entity<Tender>()
                       .Property(e => e.AnnouncementTemplateId)
                       .HasComment("Tha announcement list of suppliers related to Tender");

            builder.Entity<Tender>()
                       .Property(e => e.AgreementTypeId)
                       .HasComment("The identity that identify the type framework agreament opened or closed");

            builder.Entity<Tender>()
                       .Property(e => e.PreviousFramWorkId)
                       .HasComment("The identity that identify the previous framework agreament");

            builder.Entity<Tender>()
                       .Property(e => e.PostQualificationTenderId)
                       .HasComment("The identity that identify post qualification on the Tender");

            builder.Entity<Tender>()
                       .Property(e => e.TenderFirstStageId)
                       .HasComment("The identity of Tender of type first stage");

            builder.Entity<Tender>()
                       .Property(e => e.OffersOpeningAddressId)
                       .HasComment("The Identity that identify address of open Tender offers");

            builder.Entity<Tender>()
                       .Property(e => e.TenderConditionsTemplateId)
                       .HasComment("The Activity template of Tender");

            builder.Entity<Tender>()
                       .Property(e => e.AgencyCode)
                       .HasComment("The Agency code of agency that create a Tender");

            builder.Entity<Tender>()
                       .Property(e => e.BranchId)
                       .HasComment("The branch that create a Tender");

            builder.Entity<Tender>()
                       .Property(e => e.VRORelatedBranchId)
                       .HasComment("Branch assigned to vro");

            builder.Entity<Tender>()
                       .Property(e => e.TenderUnitStatusId)
                       .HasComment("The status of Tender at unit review");

            builder.Entity<Tender>()
                       .Property(e => e.DirectPurchaseMemberId)
                       .HasComment("The user responsible for low budget direct purchase");
        }
        private void IntializeMetaDataForTenderRelations(ModelBuilder builder)
        {
            builder.Entity<TenderType>()
                .Property(e => e.TenderTypeId)
                .HasComment("Define identity of Tender type");

            builder.Entity<TenderType>()
                 .Property(e => e.NameAr)
                 .HasComment("Define the arabic name of Tender type");

            builder.Entity<TenderType>()
                 .Property(e => e.NameEn)
                 .HasComment("Define the english name of Tender type");

            builder.Entity<TenderType>()
                 .Property(e => e.BuyingCost)
                 .HasComment("Define the cost of buying  of Tender type");

            builder.Entity<TenderType>()
                 .Property(e => e.InvitationCost)
                 .HasComment("Define the Invitation Cost of Tender type");

            builder.Entity<AgreementType>()
              .Property(e => e.AgreementTypeId)
              .HasComment("Define the identity of agreement type");

            builder.Entity<AgreementType>()
              .Property(e => e.NameAr)
              .HasComment("Define the arabic name of agreement type");

            builder.Entity<AgreementType>()
              .Property(e => e.NameEn)
              .HasComment("Define the arabic name of agreement type");


            builder.Entity<TenderStatus>()
              .Property(e => e.TenderStatusId)
              .HasComment("Define identity of Tender status");

            builder.Entity<TenderStatus>()
                 .Property(e => e.NameAr)
                 .HasComment("Define the arabic name of Tender status");

            builder.Entity<TenderStatus>()
                 .Property(e => e.NameEn)
                 .HasComment("Define the english name of Tender status");

            builder.Entity<TenderUnitStatus>()
              .Property(e => e.TenderUnitStatusId)
              .HasComment("Define identity of Tender unit status");

            builder.Entity<TenderUnitStatus>()
                 .Property(e => e.Name)
                 .HasComment("Define the name of Tender unit status");

            builder.Entity<TenderUnitUpdateType>()
              .Property(e => e.TenderUnitUpdateTypeId)
              .HasComment("Define identity of Tender unit type");

            builder.Entity<TenderUnitUpdateType>()
                 .Property(e => e.Name)
                 .HasComment("Define the name of Tender unit type");
        }


        private void AddMetaDataForAgencyCommunicationRequest(ModelBuilder builder)
        {
            builder.Entity<AgencyCommunicationRequest>()
                  .Property(e => e.AgencyRequestId)
                    .HasComment("Define a unique identifer of agency communication request");
            builder.Entity<AgencyCommunicationRequest>()
                  .Property(e => e.TenderId)
                    .HasComment("Define the related Tender of agency communication request");
            builder.Entity<AgencyCommunicationRequest>()
                  .Property(e => e.AgencyRequestTypeId)
                    .HasComment("Define the Id of agency communication request type");
            builder.Entity<AgencyCommunicationRequest>()
                  .Property(e => e.RejectionReason)
                    .HasComment("Define the rejection reason for any request");
            builder.Entity<AgencyCommunicationRequest>()
                  .Property(e => e.EscalationRejectionReason)
                    .HasComment("Define the rejection reason for the esclation request");
            builder.Entity<AgencyCommunicationRequest>()
                  .Property(e => e.TenderPlaintRequestProcedureId)
                    .HasComment("Define the action caused by accepting plaint or esclation");
            builder.Entity<AgencyCommunicationRequest>()
                     .Property(e => e.StatusId)
                       .HasComment("Define the status of agency communication request");
            builder.Entity<AgencyCommunicationRequest>()
                     .Property(e => e.EscalationStatusId)
                       .HasComment("Define the esclation status set by manager of esclation request");
            builder.Entity<AgencyCommunicationRequest>()
                  .Property(e => e.EscalationAcceptanceStatusId)
                    .HasComment("Define the esclation status set by secretary of esclation request");
            builder.Entity<AgencyCommunicationRequest>()
                  .Property(e => e.PlaintAcceptanceStatusId)
                    .HasComment("Define the plaint status set by secretary of plaint request");
            builder.Entity<AgencyCommunicationRequest>()
                  .Property(e => e.RequestedByRoleName)
                    .HasComment("Define the role who create the agency communication request");

            builder.Entity<AgencyCommunicationRequest>()
                     .Property(e => e.IsReported)
                       .HasComment("Define if the requeste is reported or not");

            builder.Entity<AgencyCommunicationRequest>()
                 .Property(e => e.SupplierExtendOfferDatesRequestId)
                   .HasComment("Define the Supplier extend offer dates request Id of agency communication request");
        }


        private void AddMetaDataForExtendOfferValidityRequest(ModelBuilder builder)
        {
            builder.Entity<ExtendOffersValidity>()
                 .Property(e => e.ExtendOffersValidityId)
                   .HasComment("Define a unique identifer of extend offers validity request");

            builder.Entity<ExtendOffersValidity>()
                 .Property(e => e.AgencyCommunicationRequestId)
                   .HasComment("Define the parent commmunication request for extend offers validity request");
            builder.Entity<ExtendOffersValidity>()
                 .Property(e => e.OffersDuration)
                   .HasComment("Define the duration in days to end expire the extend offers validity request");
            builder.Entity<ExtendOffersValidity>()
                 .Property(e => e.NewOffersExpiryDate)
                   .HasComment("Define the expiration date of extend offers validity request");
            builder.Entity<ExtendOffersValidity>()
                 .Property(e => e.ExtendOffersReason)
                 .HasComment("Define the reason of extend offers validity request");
            builder.Entity<ExtendOffersValidity>()
                .Property(e => e.ReplyReceivingDurationDays)
                  .HasComment("Define number of days to allow suppliers to reply the extend offers validity request");
            builder.Entity<ExtendOffersValidity>()
                .Property(e => e.ReplyReceivingDurationTime)
                  .HasComment("Define the time to reply the extend offers validity request");
        }

        private void AddMetaDataForExtendOffersValiditySupplierRequest(ModelBuilder builder)
        {
            builder.Entity<ExtendOffersValiditySupplier>()
               .Property(e => e.Id)
                 .HasComment("Define a unique identifer of extend offers validity supplier");

            builder.Entity<ExtendOffersValiditySupplier>()
                 .Property(e => e.ExtendOffersValidityId)
                   .HasComment("Define the parent extend offers validity for extend offers validity supplier request");
            builder.Entity<ExtendOffersValiditySupplier>()
                    .Property(e => e.OfferId)
                      .HasComment("Define the related  supplier offer for extend offers validity");
            builder.Entity<ExtendOffersValiditySupplier>()
                    .Property(e => e.SupplierCR)
                      .HasComment("Define the supplier cr that extend offers validity sent to");
            builder.Entity<ExtendOffersValiditySupplier>()
                    .Property(e => e.SupplierExtendOfferValidityStatusId)
                      .HasComment("Define the status of extend offers validity supplier based on supplier action on request");
            builder.Entity<ExtendOffersValiditySupplier>()
                    .Property(e => e.PeriodStartDateTime)
                      .HasComment("Define the the start date of extend offers validity period");
            builder.Entity<ExtendOffersValiditySupplier>()
                     .Property(e => e.IsReported)
                       .HasComment("Define if the request is reported or not");

            builder.Entity<ExtendOffersValidityAttachment>()
                 .Property(e => e.AttachmentId)
                   .HasComment("Define the parent extend offers validity for extend offers validity supplier request");
            builder.Entity<ExtendOffersValidityAttachment>()
                 .Property(e => e.ExtendOffersValiditySupplierId)
                   .HasComment("Define the parent extend offers validity for extend offers validity supplier request");
            builder.Entity<ExtendOffersValidityAttachment>()
                 .Property(e => e.AttachmentTypeId)
                .HasComment("the type of attachment file like [supplier attachment or agency attachment]");
            builder.Entity<ExtendOffersValidityAttachment>()
                 .Property(e => e.FileNetReferenceId)
                  .HasComment("Define The reference number from file Net");
            builder.Entity<ExtendOffersValidityAttachment>()
                 .Property(e => e.Name)
                   .HasComment("Define the name of file attached");

        }

        private void AddMetaDataForPlaintAndEsclationRequest(ModelBuilder builder)
        {
            builder.Entity<EscalationRequest>()
                 .Property(e => e.EscalationRequestId)
                   .HasComment("Define a unique identifer of escalation request");
            builder.Entity<EscalationRequest>()
                 .Property(e => e.PlaintRequestId)
                   .HasComment("Define the related plaint request for the escalation request");
            builder.Entity<EscalationRequest>()
               .Property(e => e.EscalationNotes)
                 .HasComment("Define notes for more details about the escalation request");

            builder.Entity<PlaintRequest>()
                 .Property(e => e.PlainRequestId)
                   .HasComment("Define a unique identifer of plaint request");
            builder.Entity<PlaintRequest>()
                 .Property(e => e.AgencyCommunicationRequestId)
               .HasComment("Define the related agency communication request for plaint request");
            builder.Entity<PlaintRequest>()
                 .Property(e => e.OfferId)
               .HasComment("Define the related Offer for plaint request");
            builder.Entity<PlaintRequest>()
                 .Property(e => e.IsEscalation)
                   .HasComment("Flag determine if the plaint request has an escalation request or not");
            builder.Entity<PlaintRequest>()
                 .Property(e => e.Notes)
                   .HasComment("Define notes for more details about the plaint request");
            builder.Entity<PlaintRequest>()
                 .Property(e => e.PlaintReason)
                   .HasComment("Define the reason of plaint request");
        }

        private void AddMetaDataForAgencyCommunicationRequestLookups(ModelBuilder builder)
        {
            builder.Entity<AgencyCommunicationRequestStatus>()
                 .Property(e => e.Id)
                   .HasComment("Define a unique identifer of agency communication request status");
            builder.Entity<AgencyCommunicationRequestStatus>()
                 .Property(e => e.Name)
                   .HasComment("Define status name of agency communication request");

            builder.Entity<AgencyCommunicationPlaintStatus>()
               .Property(e => e.Id)
                 .HasComment("Define a unique identifer of plaint request status");
            builder.Entity<AgencyCommunicationPlaintStatus>()
                 .Property(e => e.Name)
                   .HasComment("Define the plaint status name of plaint request");

            builder.Entity<AgencyCommunicationRequestType>()
                 .Property(e => e.Id)
                   .HasComment("Define a unique identifer of agency communication request type");
            builder.Entity<AgencyCommunicationRequestType>()
                 .Property(e => e.Name)
                   .HasComment("Define type name of agency communication request");

        }

        #endregion

        #region Ibrahem
        private void AddMetaDataTenderAgreementAgency(ModelBuilder builder)
        {
            builder.Entity<TenderAgreementAgency>()
                    .HasComment("Describe the relation between Tender and Agencies");

            builder.Entity<TenderAgreementAgency>()
                .Property(d => d.TenderAgreementAgencyId)
                       .HasComment("Define the unique udentifier for Tender Agreement Agency");
            builder.Entity<TenderAgreementAgency>()
             .Property(d => d.TenderId)
                    .HasComment("Define the related Tender");
            builder.Entity<TenderAgreementAgency>()
             .Property(d => d.AgencyCode)
                    .HasComment("Define the related Agency");

        }
        private void AddMetaDataTenderMaintenanceRunnigWork(ModelBuilder builder)
        {
            builder.Entity<TenderMaintenanceRunnigWork>()
                    .HasComment("Describe the relation between Tender and Maintenance Runnig Work");

            builder.Entity<TenderMaintenanceRunnigWork>()
                .Property(d => d.Id)
                       .HasComment("Define the unique udentifier for Tender Maintenance Runnig Work");
            builder.Entity<TenderMaintenanceRunnigWork>()
             .Property(d => d.TenderId)
                    .HasComment("Define the related Tender");
            builder.Entity<TenderMaintenanceRunnigWork>()
             .Property(d => d.MaintenanceRunningWorkId)
                    .HasComment("Define the related Maintenance Runnig Work");

        }
        private void AddMetaDataTenderCountry(ModelBuilder builder)
        {
            builder.Entity<TenderCountry>()
                    .HasComment("Describe the relation between Tender and Country");

            builder.Entity<TenderCountry>()
                .Property(d => d.Id)
                       .HasComment("Define the unique udentifier for Tender Country");
            builder.Entity<TenderCountry>()
             .Property(d => d.TenderId)
                    .HasComment("Define the related Tender");
            builder.Entity<TenderCountry>()
             .Property(d => d.CountryId)
                    .HasComment("Define the related Country");

        }
        private void AddMetaDataTenderChangeRequest(ModelBuilder builder)
        {
            builder.Entity<TenderChangeRequest>()
                    .HasComment("Describe the Change request on Tender");

            builder.Entity<TenderChangeRequest>()
                .Property(d => d.TenderChangeRequestId)
                       .HasComment("Define the unique udentifier for Tender Change Request");

            builder.Entity<TenderChangeRequest>()
                .Property(d => d.TenderId)
                       .HasComment("Define the Related Tender");

            builder.Entity<TenderChangeRequest>()
                .Property(d => d.ChangeRequestStatusId)
                       .HasComment("Define the status of the Request");

            builder.Entity<TenderChangeRequest>()
                .Property(d => d.ChangeRequestTypeId)
                       .HasComment("Define the type of change if it was [Change Dates, Change in files ,  Cancelation Request ....ETC]");

            builder.Entity<TenderChangeRequest>()
                .Property(d => d.RequestedByRoleName)
                       .HasComment("Define the user role who created the Request");

            builder.Entity<TenderChangeRequest>()
                .Property(d => d.RejectionReason)
                       .HasComment("Define the Rejection Reason if the request was rejected");


            builder.Entity<TenderChangeRequest>()
                .Property(d => d.HasAlternativeOffer)
                       .HasComment("Define if the Tender allow alternative offer, used if the request was Change in Quantity Table");


            builder.Entity<TenderChangeRequest>()
                .Property(d => d.CancelationReasonDescription)
                       .HasComment("Define the Cancelation Request Reason Description");


            builder.Entity<TenderChangeRequest>()
                .Property(d => d.CancelationReasonId)
                       .HasComment("Define the reason why the  Cancelation Request created");




        }
        private void AddMetaDataTenderAttachment(ModelBuilder builder)
        {

            builder.Entity<TenderAttachment>()
                    .HasComment("Describe the Atachment for tender");

            builder.Entity<TenderAttachment>()
                .Property(d => d.TenderAttachmentId)
                       .HasComment("Define the unique udentifier for Tender Attachment");
            builder.Entity<TenderAttachment>()
             .Property(d => d.TenderId)
                    .HasComment("Define the related Tender");
            builder.Entity<TenderAttachment>()
             .Property(d => d.Name)
                    .HasComment("The name of the file attached");
            builder.Entity<TenderAttachment>()
        .Property(d => d.AttachmentTypeId)
               .HasComment("The category of this attachment [Condition bocklet  ETC.... ]");
            builder.Entity<TenderAttachment>()
        .Property(d => d.FileNetReferenceId)
               .HasComment("Define the file net reference Id ");
            builder.Entity<TenderAttachment>()
        .Property(d => d.RejectionReason)
               .HasComment("Rejection reason if there were change request on the attachment");
            builder.Entity<TenderAttachment>()
        .Property(d => d.ReviewStatusId)
               .HasComment("Define status of review ");

            builder.Entity<TenderAttachment>()
       .Property(d => d.ChangeStatusId)
              .HasComment("Define the the status of change request");

        }
        private void AddMetaDataTenderArea(ModelBuilder builder)
        {
            builder.Entity<TenderArea>()
                    .HasComment("Describe the relation between Tender and Areas");

            builder.Entity<TenderArea>()
                .Property(d => d.Id)
                       .HasComment("Define the unique udentifier for Tender area");
            builder.Entity<TenderArea>()
             .Property(d => d.TenderId)
                    .HasComment("Define the related Tender");
            builder.Entity<TenderArea>()
             .Property(d => d.AreaId)
                    .HasComment("Define the related Area");

        }

        #region offer
        private void AddMetaDataOfferHistory(ModelBuilder builder)
        {
            builder.Entity<OfferHistory>()
               .Property(e => e.OfferHistoryId)
               .HasComment("Define Unique identifer Of Offer Presentation Way lookup");

            builder.Entity<OfferHistory>()
                    .Property(e => e.OfferId)
                    .HasComment("Define Unique identifer Of Offer");

            builder.Entity<OfferHistory>()
               .Property(e => e.TenderId)
               .HasComment("Define the Id of related Tender");
            builder.Entity<OfferHistory>()
               .Property(e => e.OfferStatusId)
               .HasComment("Define the status of offer like (under establishment,Sent,cancelled ...etc )");

            builder.Entity<OfferHistory>()
               .Property(e => e.OfferAcceptanceStatusId)
               .HasComment("Define the financial evaluation result [Accepted or Rejected]");
            builder.Entity<OfferHistory>()
               .Property(e => e.OfferTechnicalEvaluationStatusId)
               .HasComment("Define the Technical evaluation result [Accepted or Rejected]");

            builder.Entity<OfferHistory>()
                 .Property(e => e.CommericalRegisterNo)
                 .HasComment("Define the Commerical Register Number for the owner supplier for the offer ");

            builder.Entity<OfferHistory>()
                .Property(e => e.RejectionReason)
                .HasComment("The Rejection reason in awrding stage");

            builder.Entity<OfferHistory>()
                        .Property(e => e.ActionId)
                        .HasComment("The type of action on the offer");

            builder.Entity<OfferHistory>()
                        .Property(e => e.OfferId)
                        .HasComment("Define the related offer");


            builder.Entity<OfferHistory>()
                  .HasComment("Represent  Offer Data ");
        }
        private void AddMetaDataNotification(ModelBuilder builder)
        {
            builder.Entity<NotificationEmail>()
               .HasComment("Contains the Data of Notifications");

            builder.Entity<NotificationEmail>()
               .Property(e => e.Id)
               .HasComment("Define Unique identifer Of Notification");
            builder.Entity<NotificationEmail>()
              .Property(e => e.UserId)
              .HasComment("Define the user id who recieve the notification");
            builder.Entity<NotificationEmail>()
              .Property(e => e.CR)
              .HasComment("Define the Supplier id who recieve the notification");
            builder.Entity<NotificationEmail>()
              .Property(e => e.NotifacationStatusId)
              .HasComment("Define the status of Notification if sent or not or Faild");

            builder.Entity<NotificationEmail>()
              .Property(e => e.sendAt)
              .HasComment("the date of sending the notification");

            builder.Entity<NotificationEmail>()
              .Property(e => e.Link)
              .HasComment("link for reciever to access direct the related page of monafasat");

            builder.Entity<NotificationEmail>()
              .Property(e => e.Key)
              .HasComment("compination of string \'Tender\' and the Id of Tender ");

            builder.Entity<NotificationEmail>()
              .Property(e => e.NotificationSettingId)
              .HasComment("the related setting of the setting");

        }

        private void AddMetaDataUserAudit(ModelBuilder builder)
        {
            builder.Entity<UserAudit>()
               .Property(e => e.Id)
               .HasComment("Define Unique identifer Of  User Action Auditting");
            builder.Entity<UserAudit>()
              .Property(e => e.UserId)
              .HasComment("Define the user id who making the action");
            builder.Entity<UserAudit>()
              .Property(e => e.Timestamp)
              .HasComment("Define time that action was done on ");
            builder.Entity<UserAudit>()
              .Property(e => e.UserName)
              .HasComment("Define User Name of the user who take the action ");
            builder.Entity<UserAudit>()
              .Property(e => e.FullName)
              .HasComment("Define User Full Name of the user who take the action");

            builder.Entity<UserAudit>()
              .Property(e => e.Process)
              .HasComment("Define the Process status Success or Fail");



            builder.Entity<UserAudit>()
              .Property(e => e.IpAddress)
              .HasComment("Define ip Address of Device that the user using ");



            builder.Entity<UserAudit>()
              .Property(e => e.AuditEvent)
              .HasComment("Define the type of Action if Edit or Add or Delete");


            builder.Entity<UserAudit>()
              .Property(e => e.ProcessId)
              .HasComment("Unique Number to the process");
            builder.Entity<UserAudit>()
              .HasComment("Descripes the Auditing Data to track users Actions");
        }

        private void AddMetaDataUserNotificationSetting(ModelBuilder builder)
        {
            builder.Entity<UserNotificationSetting>()
               .Property(e => e.Id)
               .HasComment("Define Unique identifer Of  User Notification Setting"); builder.Entity<UserNotificationSetting>()
                .Property(e => e.UserProfileId)
                .HasComment("Define the related user if he was Governate user ");
            builder.Entity<UserNotificationSetting>()
            .Property(e => e.IsArabic)
            .HasComment("Define if the user configured Notification to be in arabic Language ");
            builder.Entity<UserNotificationSetting>()
            .Property(e => e.OperationCode)
            .HasComment("Define the Notification Code");

            builder.Entity<UserNotificationSetting>()
               .Property(e => e.NotificationCodeId)
               .HasComment("Define the Notification Code ID");

            builder.Entity<UserNotificationSetting>()
            .Property(e => e.Cr)
            .HasComment("Refere to the Supplier Commercial Registeration Number who will recieve the Notification");

            builder.Entity<UserNotificationSetting>()
           .Property(e => e.Sms)
           .HasComment("Define if the user needs to recieve SMS or Not");

            builder.Entity<UserNotificationSetting>()
           .Property(e => e.Email)
             .HasComment("Define if the user needs to recieve EMAIL or Not");

            builder.Entity<UserNotificationSetting>()
               .Property(e => e.UserRoleId)
               .HasComment("Define the role of the user who will recieve the Notification");


            builder.Entity<UserNotificationSetting>()
               .HasComment("Contains the user settings for every notification");
        }
        private void AddMetaDataUnRegisteredSuppliersInvitation(ModelBuilder builder)
        {
            builder.Entity<UnRegisteredSuppliersInvitation>()
               .Property(e => e.Id)
               .HasComment("Define Unique identifer Of UnRegistered Suppliers Invitation");

            builder.Entity<UnRegisteredSuppliersInvitation>()
               .Property(e => e.TenderId)
               .HasComment("Define the related Tender for the invitations");

            builder.Entity<UnRegisteredSuppliersInvitation>()
               .Property(e => e.Email)
               .HasComment("Define the supplier email that he will recieve the invitaion on it ");

            builder.Entity<UnRegisteredSuppliersInvitation>()
               .Property(e => e.MobileNo)
               .HasComment("Define the supplier Mobile Number");

            builder.Entity<UnRegisteredSuppliersInvitation>()
               .Property(e => e.InvitationStatusId)
               .HasComment("Define the status of invitation id sent aor not and if accepted by supplier or Not");

            builder.Entity<UnRegisteredSuppliersInvitation>()
               .Property(e => e.CrNumber)
               .HasComment("Define the supplier Commercial Registeration Number");

            builder.Entity<UnRegisteredSuppliersInvitation>()
               .Property(e => e.InvitationTypeId)
               .HasComment("Define the invitaion Type if it was by email or mobile ETC...");

            builder.Entity<UnRegisteredSuppliersInvitation>()
               .Property(e => e.Description)
               .HasComment("Define the description written with the invitaion  ");

            builder.Entity<UnRegisteredSuppliersInvitation>()
              .HasComment("Contains the Invitations for uregistered suppliers");
        }
        private void AddMetaDataOfferPresentationWay(ModelBuilder builder)
        {
            builder.Entity<OfferPresentationWay>()
               .Property(e => e.Id)
               .HasComment("Define Unique identifer Of Offer Presentation Way lookup");

            builder.Entity<OfferPresentationWay>()
               .Property(e => e.Name)
               .HasComment("Define the Name Of Offer Presentation Way lookup");

            builder.Entity<OfferPresentationWay>()
              .HasComment("Define the Offer Presentation Way lookup");
        }
        private void AddMetaDataNotificationCategory(ModelBuilder builder)
        {
            builder.Entity<NotificationCategory>()
               .Property(e => e.Id)
               .HasComment("Define Unique identifer Of  Notification Category");
            builder.Entity<NotificationCategory>()
               .Property(e => e.ArabicName)
               .HasComment("Define the arabic Name Of Notification Category");
            builder.Entity<NotificationCategory>()
                      .Property(e => e.EnglishName)
                      .HasComment("Define the English Name Of Notification Category");

            builder.Entity<NotificationCategory>()
                   .HasComment("Define the diffrent categories of notification ");

        }

        private void AddMetaDataSupplierSecondNegotiationStatus(ModelBuilder builder)
        {
            builder.Entity<SupplierSecondNegotiationStatus>()
               .Property(e => e.SupplierNegotiaitionStatusId)
               .HasComment("Define Unique identifer Of  Second Stage Negotiation Status");

            builder.Entity<SupplierSecondNegotiationStatus>()
                      .Property(e => e.Name)
                      .HasComment("Define the  Name Of Second Stage Negotiation Status");

            builder.Entity<SupplierSecondNegotiationStatus>()
                   .HasComment("Define the Status of Second Stage Negotiation like [Approved , Supplier Agree , ETC..]");

        }

        private void AddMetaDataNegotiationSupplierQuantityTable(ModelBuilder builder)
        {
            builder.Entity<NegotiationSupplierQuantityTable>()
               .Property(e => e.Id)
               .HasComment("Define Unique identifer Of Quantity Table ");
            builder.Entity<NegotiationSupplierQuantityTable>()
               .Property(e => e.Name)
               .HasComment("Define the Name Of Quantity Table ");
            builder.Entity<NegotiationSupplierQuantityTable>()
                      .Property(e => e.refNegotiationSecondStage)
                      .HasComment("Refere to the related Negotioation Request ");
            builder.Entity<NegotiationSupplierQuantityTable>()
                      .Property(e => e.SupplierQuantityTableId)
                      .HasComment("Refer to the Original Supplier Quantity Table that Filled by supplier");

            builder.Entity<NegotiationSupplierQuantityTable>()
                   .HasComment("Define the Quantity Table for the Negotiation");

        }
        private void AddMetaDataNotifacationStatusEntity(ModelBuilder builder)
        {
            builder.Entity<NotifacationStatusEntity>()
               .Property(e => e.NotifacationStatusEntityId)
               .HasComment("Define Unique identifer Of Notification Status lookup [مرسل,لم يتم الارسال , فشل فى الارسال , مقروءه , غير مقروءه]");
            builder.Entity<OfferPresentationWay>()
               .Property(e => e.Name)
               .HasComment("Define the Name Of Notification Status");
            builder.Entity<OfferPresentationWay>()
                 .HasComment("Define the way that supplier can apply offer if it was on one file or two files");

        }

        private void AddMetaDataSupplierTenderQuantityTableItemJson(ModelBuilder builder)
        {
            builder.Entity<SupplierTenderQuantityTableItemJson>()
               .Property(e => e.Id)
               .HasComment("Define Unique identifer Of Supplier  Quantity Table items");
            builder.Entity<SupplierTenderQuantityTableItemJson>()
               .Property(e => e.SupplierTenderQuantityTableId)
               .HasComment("Define the related Quatity Table");
            builder.Entity<SupplierTenderQuantityTableItemJson>()
                 .HasComment("Contains Quantity table Items ");

        }
        private void AddMetaDataNotificationOperationCode(ModelBuilder builder)
        {
            builder.Entity<NotificationOperationCode>()
               .Property(e => e.NotificationOperationCodeId)
               .HasComment("Define Unique identifer Of Notification Operation Code");

            builder.Entity<NotificationOperationCode>()
               .Property(e => e.OperationCode)
               .HasComment("its  unique Text the represent the notification template");

            builder.Entity<NotificationOperationCode>()
               .Property(e => e.ArabicName)
               .HasComment("Define notification template arabic Name");
            builder.Entity<NotificationOperationCode>()
               .Property(e => e.EnglishName)
               .HasComment("Define notification template english Name");
            builder.Entity<NotificationOperationCode>()
            .Property(e => e.UserRoleId)
            .HasComment("Define the related user role  that will recieve the notification");

            builder.Entity<NotificationOperationCode>()
            .Property(e => e.DefaultSMS)
            .HasComment("Define if the reciever role will recieve SMS by default  or not");


            builder.Entity<NotificationOperationCode>()
            .Property(e => e.DefaultEmail)
            .HasComment("Define if the reciever role will recieve Email by default  or not");

            builder.Entity<NotificationOperationCode>()
            .Property(e => e.CanEditEmail)
            .HasComment("Define if the reciever role can change Default setting for recieving Email or not");

            builder.Entity<NotificationOperationCode>()
            .Property(e => e.CanEditSMS)
            .HasComment("Define if the reciever role can change Default setting for recieving SMS or not");

            builder.Entity<NotificationOperationCode>()
            .Property(e => e.NotificationCategoryId)
            .HasComment("Define if the Category of notificatopn item like [operations on Tender , negotiation ETC..]");

            builder.Entity<NotificationOperationCode>()
            .Property(e => e.SmsTemplateAr)
            .HasComment("The SMS arabic Template");

            builder.Entity<NotificationOperationCode>()
            .Property(e => e.SmsTemplateEn)
            .HasComment("The SMS English Template");

            builder.Entity<NotificationOperationCode>()
            .Property(e => e.EmailSubjectTemplateAr)
            .HasComment("The Arabic EMail Subject for noification ");

            builder.Entity<NotificationOperationCode>()
            .Property(e => e.EmailSubjectTemplateEn)
            .HasComment("The English EMail Subject for noification ");

            builder.Entity<NotificationOperationCode>()
             .Property(e => e.EmailBodyTemplateAr)
             .HasComment("The Arabic EMail Subject for noification ");

            builder.Entity<NotificationOperationCode>()
             .Property(e => e.EmailSubjectTemplateEn)
             .HasComment("The English EMail Body for noification ");

            builder.Entity<NotificationOperationCode>()
             .Property(e => e.PanelTemplateAr)
             .HasComment("The Arabic Panel Subject for noification ");
            builder.Entity<NotificationOperationCode>()
           .Property(e => e.PanelTemplateEn)
           .HasComment("The English Panel Subject for noification ");


            builder.Entity<NotificationOperationCode>()
               .HasComment("Describe the Notifications Templates and involved roles");

        }
        private void AddMetaDataOfferSolidarity(ModelBuilder builder)
        {

            builder.Entity<OfferSolidarity>()
              .HasComment("Describe the Data related to offer solidarity ");

            builder.Entity<OfferSolidarity>()
               .Property(e => e.Id)
               .HasComment("Define Unique identifer Of Offer Solidarity Table");

            builder.Entity<OfferSolidarity>()
               .Property(e => e.OfferId)
               .HasComment("Define the related Offer");


            builder.Entity<OfferSolidarity>()
               .Property(e => e.RejectionReason)
               .HasComment("Define the reason if the supplier rejected the request");
            builder.Entity<OfferSolidarity>()
                       .Property(e => e.Email)
                       .HasComment("The email for the supplierwho will recieve the request to be partner of the offer");
            builder.Entity<OfferSolidarity>()
                       .Property(e => e.Mobile)
                       .HasComment("The Mobile number for the supplierwho will recieve the request to be partner of the offer");

            builder.Entity<OfferSolidarity>()
                       .Property(e => e.StatusId)
                       .HasComment("Define the status of the request (Not sent , Rejected  or accepted)");

            builder.Entity<OfferSolidarity>()
                       .Property(e => e.SubmissionDate)
                       .HasComment("The date of Accepting the solidarity Request");


            builder.Entity<OfferSolidarity>()
                       .Property(e => e.SolidarityTypeId)
                       .HasComment("The type of Request between Registered Supplier or Forign ");
            builder.Entity<OfferSolidarity>()
                         .Property(e => e.CRNumber)
                         .HasComment("Define the supplier Commercial Number if he was Registered on Monafasat");
        }
        private void AddMetaDataOfferStatus(ModelBuilder builder)
        {
            builder.Entity<OfferStatus>()
        .HasComment("Define the Offer status lookup");

            builder.Entity<OfferStatus>()
               .Property(e => e.OfferStatusId)
               .HasComment("Define Unique identifer Of Offer Status lookup");

            builder.Entity<OfferStatus>()
               .Property(e => e.NameAr)
               .HasComment("Define the Arabic Name Of  Offer Status");

            builder.Entity<OfferStatus>()
               .Property(e => e.NameEn)
               .HasComment("Define the English Name Of  Offer Status");
        }
        private void AddMetaDataOfferSolidarityStatus(ModelBuilder builder)
        {
            builder.Entity<OfferSolidarityStatus>()
        .HasComment("Define the Offer Solidarity  status lookup");

            builder.Entity<OfferSolidarityStatus>()
               .Property(e => e.CombinedStatusId)
               .HasComment("Define Unique identifer Of Offer Solidarity  Status lookup");

            builder.Entity<OfferSolidarityStatus>()
               .Property(e => e.Name)
               .HasComment("Define the  Name Of  Offer Solidarity Status");


        }

        private void AddMetaDataOffer(ModelBuilder builder)
        {
            //   builder.Entity<Negotiation>().HasComment("Descripes all Negotiation Requests [First Stage and Second Stage]");

            builder.Entity<Offer>()
               .Property(e => e.OfferId)
               .HasComment("Define Unique identifer Of Offer");
            builder.Entity<Offer>()
               .Property(e => e.SuplierId)
               .HasComment("Not Used");
            builder.Entity<Offer>()
               .Property(e => e.TenderId)
               .HasComment("Define the Id of related Tender");
            builder.Entity<Offer>()
               .Property(e => e.OfferStatusId)
               .HasComment("Define the status of offer like (under establishment,Sent,cancelled ...etc )");
            builder.Entity<Offer>()
               .Property(e => e.OfferPresentationWayId)
               .HasComment("Define if the offer is presented in one file or two files ");
            builder.Entity<Offer>()
               .Property(e => e.OfferAcceptanceStatusId)
               .HasComment("Define the financial evaluation result [Accepted or Rejected]");
            builder.Entity<Offer>()
               .Property(e => e.OfferTechnicalEvaluationStatusId)
               .HasComment("Define the Technical evaluation result [Accepted or Rejected]");
            builder.Entity<Offer>()
                 .Property(e => e.IsManuallyApplied)
                 .HasComment("Define if the offer was applied out of the system or by the system");
            builder.Entity<Offer>()
                 .Property(e => e.CommericalRegisterNo)
                 .HasComment("Define the Commerical Register Number for the owner supplier for the offer ");
            builder.Entity<Offer>()
                 .Property(e => e.Notes)
                 .HasComment("Represent the notes entered on Checkng Stage");
            builder.Entity<Offer>()
                 .Property(e => e.FinantialNotes)
                 .HasComment("Represent the notes entered on Checkng Stage");
            builder.Entity<Offer>()
                 .Property(e => e.DiscountNotes)
                 .HasComment("Represent notes entered while adding  discount , used in awarding stage");
            builder.Entity<Offer>()
                 .Property(e => e.Discount)
                 .HasComment("Represent the Discount , used in awarding stage");
            builder.Entity<Offer>()
                 .Property(e => e.IsOfferCopyAttached)
                 .HasComment("Define if hard copy of offer Applied");
            builder.Entity<Offer>()
                 .Property(e => e.IsSolidarity)
                 .HasComment("Define if More than one supplier in one offer ");
            builder.Entity<Offer>()
                .Property(e => e.IsOfferLetterAttached)
                .HasComment("Define if Offer Letter Attached ");
            builder.Entity<Offer>()
                .Property(e => e.OfferLetterNumber)
                .HasComment("Define  the Offer Letter Number");
            builder.Entity<Offer>()
                .Property(e => e.OfferLetterDate)
                .HasComment("Define the Offer Letter Expiry Date");
            builder.Entity<Offer>()
                .Property(e => e.IsPurchaseBillAttached)
                .HasComment("Define if Purchase Bill hard Copy applied to the agency ");
            builder.Entity<Offer>()
                .Property(e => e.IsBankGuaranteeAttached)
                .HasComment("Define if Bank Guarantee hard Copy appied to the agency ");
            builder.Entity<Offer>()
                 .Property(e => e.IsVisitationAttached)
                 .HasComment("Define if Visitation hard Copy appied to the agency ");
            builder.Entity<Offer>()
                 .Property(e => e.JustificationOfRecommendation)
                 .HasComment("Define Justification Of Recommendation");
            builder.Entity<Offer>()
                 .Property(e => e.IsOpened)
                 .HasComment("Is the offer opended and all needed Data filled in oppenning stage");
            builder.Entity<Offer>()
                 .Property(e => e.TotalOfferAwardingValue)
                 .HasComment("Define the total value of awarding ");
            builder.Entity<Offer>()
                 .Property(e => e.PartialOfferAwardingValue)
                 .HasComment("Define the partial value of awarding if the Tender partialy awarded");
            builder.Entity<Offer>()
                 .Property(e => e.FinalPriceAfterDiscount)
                 .HasComment("The final financial value of the offer after appling VAT and discount");
            builder.Entity<Offer>()
                .Property(e => e.FinalPriceBeforeDiscount)
                .HasComment("The final financial value of the offer before appling VAT and discount");
            builder.Entity<Offer>()
                .Property(e => e.OfferWeightAfterCalcNPA)
                .HasComment("The final financial value of the offer after appling VAT and discount and Calculation of National Product Equation");
            builder.Entity<Offer>()
                .Property(e => e.RejectionReason)
                .HasComment("The Rejection reason in awrding stage");
            builder.Entity<Offer>()
                .Property(e => e.FinantialRejectionReason)
                .HasComment("The reason for financial rejection in checking stage");
            builder.Entity<Offer>()
              .Property(e => e.IsFinaintialOfferLetterAttached)
              .HasComment("Represent if the  Finaintial Offer Letter Attached");
            builder.Entity<Offer>()
              .Property(e => e.FinantialOfferLetterNumber)
              .HasComment("The number of Finantial Offer Letter");
            builder.Entity<Offer>()
              .Property(e => e.IsFinantialOfferLetterCopyAttached)
              .HasComment("Represent if acopy of Finaintial Offer Letter manually Applied");
            builder.Entity<Offer>()
              .Property(e => e.IsOfferFinancialDetailsEntered)
              .HasComment("Define if all financial Details entired or Not");
            builder.Entity<Offer>()
              .Property(e => e.TechnicalEvaluationLevel)
              .HasComment("Define the Technical Evaluation Level of the offer ");
            builder.Entity<Offer>()
                .Property(e => e.FinancialEvaluationLevel)
                .HasComment("Define the Financial Evaluation Level of the offer ");
            builder.Entity<Offer>()
                .Property(e => e.ExclusionReason)
                .HasComment("Represent the Reason of excluding the offer ");
            builder.Entity<Offer>()
                .Property(e => e.TechnicalEvaluationLevel)
                .HasComment("Define the Technical Evaluation Level of the offer ");

            builder.Entity<Offer>()
                  .HasComment("Represent  Offer Data ");
        }

        private void AddMetaDataCity(ModelBuilder builder)
        {
            builder.Entity<City>()
                   .HasComment("look up for all cities");

            builder.Entity<City>()
                 .Property(e => e.CityID)
                   .HasComment("Define a unique identifer of City");
            builder.Entity<City>()
                 .Property(e => e.NameArabic)
                   .HasComment("Define arabic name of City");
            builder.Entity<City>()
           .Property(e => e.NameEnglish)
                 .HasComment("Define English name of City");
        }
        private void AddMetaDataBranchUser(ModelBuilder builder)
        {
            builder.Entity<BranchUser>()
                   .HasComment("Contain Data for Users for each Branch");

            builder.Entity<BranchUser>()
                 .Property(e => e.BranchUserId)
                   .HasComment("Define a unique identifer of Branch User");



            builder.Entity<BranchUser>()
                 .Property(e => e.BranchId)
                   .HasComment("Refere to the Branch");


            builder.Entity<BranchUser>()
                 .Property(e => e.EstimatedValueFrom)
                   .HasComment("Define the smallest estimated value");



            builder.Entity<BranchUser>()
                 .Property(e => e.EstimatedValueTo)
                   .HasComment("Define the biggest estimated value");


            builder.Entity<BranchUser>()
                 .Property(e => e.RelatedAgencyCode)
                   .HasComment("Define the agency which the branch is related to");


            builder.Entity<BranchUser>()
                 .Property(e => e.UserProfileId)
                   .HasComment("reference the user full profile");


            builder.Entity<BranchUser>()
                 .Property(e => e.UserRoleId)
                   .HasComment("Define user role inside the branch");



        }
        private void AddMetaDataMobileAlert(ModelBuilder builder)
        {
            builder.Entity<MobileAlert>()
                    .HasComment("Not Used");

            builder.Entity<MobileAlert>()
                .Property(d => d.MobileAlertId)
                       .HasComment("Not Used");

            builder.Entity<MobileAlert>()
                .Property(d => d.DeviceId)
                       .HasComment("Not Used");

            builder.Entity<MobileAlert>()
                .Property(d => d.Message)
                       .HasComment("Not Used");

            builder.Entity<MobileAlert>()
                .Property(d => d.MessageId)
                       .HasComment("Not Used");
            builder.Entity<MobileAlert>()
                .Property(d => d.MessageStatusId)
       .HasComment("Not Used");
            builder.Entity<MobileAlert>()
                .Property(d => d.GroupCode)
                       .HasComment("Not Used");
        }
        private void AddMetaDataTenderActivity(ModelBuilder builder)
        {
            builder.Entity<TenderActivity>()
                    .HasComment("Describe the Tender activities");

            builder.Entity<TenderActivity>()
                .Property(d => d.TenderActivityId)
                       .HasComment("Define the unique udentifier for Tender Activity");
            builder.Entity<TenderActivity>()
             .Property(d => d.TenderId)
                    .HasComment("Define the related Tender");
            builder.Entity<TenderActivity>()
             .Property(d => d.ActivityId)
                    .HasComment("Define the related Activity");

        }

        private void AddMetaDataDeviceTokenNotification(ModelBuilder builder)
        {
            builder.Entity<DeviceTokenNotification>()
                    .HasComment("Not Used");
            builder.Entity<DeviceTokenNotification>()
                .Property(d => d.DeviceTokenNotificationId)
                       .HasComment("Not Used");

            builder.Entity<DeviceTokenNotification>()
                .Property(d => d.DeviceId)
                       .HasComment("Not Used");

            builder.Entity<DeviceTokenNotification>()
                .Property(d => d.ActivityId)
                       .HasComment("Not Used");

            builder.Entity<DeviceTokenNotification>()
                .Property(d => d.Status)
                       .HasComment("Not Used");
        }


        private void AddMetaDataDeviceToken(ModelBuilder builder)
        {
            builder.Entity<DeviceToken>()
                    .HasComment("Not Used");
            builder.Entity<DeviceToken>()
                .Property(d => d.DeviceId)
                       .HasComment("Not Used");

            builder.Entity<DeviceToken>()
                .Property(d => d.SourceIP)
                      .HasComment("Not Used");

            builder.Entity<DeviceToken>()
                .Property(d => d.TimeStamp)
                     .HasComment("Not Used");

            builder.Entity<DeviceToken>()
                .Property(d => d.UserDeviceId)
                   .HasComment("Not Used");

            builder.Entity<DeviceToken>()
                .Property(d => d.Model)
                     .HasComment("Not Used");

            builder.Entity<DeviceToken>()
                .Property(d => d.DeviceName)
                   .HasComment("Not Used");
            builder.Entity<DeviceToken>()
                .Property(d => d.DeviceVersion)
                     .HasComment("Not Used");

            builder.Entity<DeviceToken>()
                .Property(d => d.DeviceTokenValue)
                   .HasComment("Not Used");

            builder.Entity<DeviceToken>()
                .Property(d => d.UserProfileId)
                    .HasComment("Not Used");

        }
        private void AddMetaDataBranch(ModelBuilder builder)
        {

            builder.Entity<Branch>()
                    .HasComment("Contain the Brnach main data");
            builder.Entity<Branch>()
                .Property(d => d.BranchId)
                    .HasComment("Define a unique identifer of Branch");
            builder.Entity<Branch>()
                .Property(d => d.BranchName)
                    .HasComment("Define the branch name");
            builder.Entity<Branch>()
                .Property(d => d.AgencyCode)
                    .HasComment("Define the related Agency");
        }
        private void AddMetaDataBranchAddress(ModelBuilder builder)
        {

            builder.Entity<BranchAddress>()
                    .HasComment("Contain the Branch address data");
            builder.Entity<BranchAddress>()
                .Property(d => d.BranchAddressId)
                    .HasComment("Define a unique identifer of Branch Address");
            builder.Entity<BranchAddress>()
                .Property(d => d.AddressName)
                    .HasComment("Define the branch Address name");
            builder.Entity<BranchAddress>()
                .Property(d => d.AddressTypeId)
                    .HasComment("Define the type of address");
            builder.Entity<BranchAddress>()
              .Property(d => d.Position)
                  .HasComment("Define the Position of the address");
            builder.Entity<BranchAddress>()
              .Property(d => d.Phone)
                  .HasComment("Define phone number of the address");
            builder.Entity<BranchAddress>()
              .Property(d => d.Fax)
                  .HasComment("Define FAX number of the address");

            builder.Entity<BranchAddress>()
              .Property(d => d.Phone2)
                  .HasComment("Define second phone number of the address");
            builder.Entity<BranchAddress>()
              .Property(d => d.Fax2)
                  .HasComment("Define second FAX number of the address");


            builder.Entity<BranchAddress>()
              .Property(d => d.ZipCode)
                  .HasComment("Define Zip Code of the address");
            builder.Entity<BranchAddress>()
              .Property(d => d.CityCode)
                  .HasComment("Define City Code of the address");
            builder.Entity<BranchAddress>()
              .Property(d => d.PostalCode)
                  .HasComment("Define Postal Code of the address");
            builder.Entity<BranchAddress>()
              .Property(d => d.Description)
                  .HasComment("Define Description of the address");

            builder.Entity<BranchAddress>()
              .Property(d => d.Address)
                  .HasComment("Define The detailed loction  of the Branch Address");
        }
        private void AddMetaDataBranchCommittee(ModelBuilder builder)
        {

            builder.Entity<BranchCommittee>()
                    .HasComment("Contain the relation between Branches and Committees");
            builder.Entity<BranchCommittee>()
                .Property(d => d.BranchCommitteeId)
                    .HasComment("Define a unique identifer of Branch Committee");

            builder.Entity<BranchCommittee>().Property(d => d.BranchId)
                    .HasComment("Refere to the Branch");

            builder.Entity<BranchCommittee>().Property(d => d.CommitteeId)
                    .HasComment("Refere to the Committie");


        }
        #endregion


        private void AddMetaDataAuditableEntity(ModelBuilder builder)
        {
            //   builder.Entity<Negotiation>().HasComment("Descripes all Negotiation Requests [First Stage and Second Stage]");
            builder.Entity<AuditableEntity>()
                    .Property(e => e.UpdatedAt)
                    .HasComment("Define The last Update Date");
            builder.Entity<AuditableEntity>()
                    .Property(e => e.UpdatedBy)
                    .HasComment("Define the Last user who Updated any Field");
            builder.Entity<AuditableEntity>()
                    .Property(e => e.CreatedAt)
                    .HasComment("Define The creation Date");
            builder.Entity<AuditableEntity>()
                    .Property(e => e.CreatedBy)
                    .HasComment("Define the Last user who Created the Row");
            builder.Entity<AuditableEntity>()
                    .Property(e => e.IsActive)
                    .HasComment("Define if the row is Active and can  be used");


        }
        private void AddMetaDataNegotiation(ModelBuilder builder)
        {
            //   builder.Entity<Negotiation>().HasComment("Descripes all Negotiation Requests [First Stage and Second Stage]");
            builder.Entity<Negotiation>()
                    .Property(e => e.AgencyRequestId)
                    .HasComment("Communication Request that the negotiation is related To");
            builder.Entity<Negotiation>()
                  .Property(e => e.NegotiationId)
                  .HasComment("Define Identity Of Negotiation Request");
            builder.Entity<Negotiation>()
                           .Property(e => e.SupplierReplyPeriodHours)
                           .HasComment("the period that the defined for supplier to reply on the negotiation Request ");
            builder.Entity<Negotiation>()
                             .Property(e => e.NegotiationReasonId)
                             .HasComment("the reason for negotiation Note: this column refer to Negotiations Reasons Table ");
            builder.Entity<Negotiation>()
                             .Property(e => e.NegotiationTypeId)
                             .HasComment("the type of negotiation [first stage or second stage] Note: this column refer to Negotiations types Table ");
            builder.Entity<Negotiation>()
                             .Property(e => e.RejectionReason)
                             .HasComment("Define the Note written by the high level employee if he rejected the request ");
            builder.Entity<Negotiation>()
                             .Property(e => e.StatusId)
                             .HasComment("Define the status of Negotiation ");


        }
        private void AddMetaDataNegotiationType(ModelBuilder builder)
        {

            builder.Entity<NegotiationType>().HasComment("Describe the negotiation types");
            builder.Entity<NegotiationType>()
                             .Property(e => e.NegotiationTypeId)
                             .HasComment("Define the unique  identifier for negotiation types");
            builder.Entity<NegotiationType>()
                             .Property(e => e.Name)
                             .HasComment("Define The name of Negotiation Type Name");



        }
        private void AddMetaDataNegotiationReason(ModelBuilder builder)
        {

            builder.Entity<NegotiationReason>().HasComment("Describe the negotiation reason");
            builder.Entity<NegotiationReason>()
                             .Property(e => e.NegotiationReasonId)
                             .HasComment("Define the unique  identifier for negotiation reason");
            builder.Entity<NegotiationReason>()
                             .Property(e => e.Name)
                             .HasComment("Define The name of Negotiation reason Name");



        }
        private void AddMetaDataNegotiationAttachment(ModelBuilder builder)
        {

            builder.Entity<NegotiationAttachment>().HasComment("Describe the Negotiation Attachment");
            builder.Entity<NegotiationAttachment>()
                             .Property(e => e.AttachmentId)
                             .HasComment("Define the unique  identifier for negotiation Attachment");
            builder.Entity<NegotiationAttachment>()
                             .Property(e => e.NegotiationId)
                             .HasComment("Define The related of Negotiation request");

            builder.Entity<NegotiationAttachment>()
                                    .Property(e => e.Name)
                                    .HasComment("Define The name of the file");

            builder.Entity<NegotiationAttachment>()
                                    .Property(e => e.AttachmentTypeId)
                                    .HasComment("the type of attachment file like [supplier attachment or agency attachment]");

            builder.Entity<NegotiationAttachment>()
                                    .Property(e => e.FileNetReferenceId)
                                    .HasComment("Define The reference number from file Net");

        }
        private void AddMetaDataNegotiationFirstSatge(ModelBuilder builder)
        {
            builder.Entity<NegotiationFirstStage>().HasComment("Describe first Stage Negotiation");

            builder.Entity<NegotiationFirstStage>()
                             .Property(e => e.DiscountLetterRefID)
                             .HasComment("Define the Referance for the Letter File of Negotiation");
            builder.Entity<NegotiationFirstStage>()
                             .Property(e => e.ProjectNumber)
                             .HasComment("Define The project number from Etimad Budget");
            builder.Entity<NegotiationFirstStage>()
                             .Property(e => e.DiscountAmount)
                             .HasComment("Define The amount which the agency needs to deduct from supplier offer");
            builder.Entity<NegotiationFirstStage>()
                             .Property(e => e.IsNewNegotiation)
                             .HasComment("define if the negotiation request should take the new flow or old flow");
            builder.Entity<NegotiationFirstStage>()
                         .Property(e => e.ExtraDiscountValue)
                         .HasComment("Define The Amount of the offer after supplier deducted axtra amount from his offer ");



        }
        private void AddMetaDataNegotiationSecondStage(ModelBuilder builder)
        {
            //  builder.Entity<NegotiationSecondStage>().HasComment("Describe first Stage Negotiation");

            builder.Entity<NegotiationSecondStage>()
                             .Property(e => e.NegotiationFirstStageId)
                             .HasComment("Define the Related First Stage Negotiation Request");
            builder.Entity<NegotiationSecondStage>()
                             .Property(e => e.OfferId)
                             .HasComment("Define The project number from Etimad Budget");
            builder.Entity<NegotiationSecondStage>()
                           .Property(e => e.PeriodStartDate)
                           .HasComment("Define The start date that the supplier recieved the request ");

            builder.Entity<NegotiationFirstStageSupplier>()
                                 .Property(e => e.IsReported)
                                 .HasComment("Define if the supplier notified that he has Negotiation request");


        }

        private void AddMetaDataNegotiationFirstStageSupplier(ModelBuilder builder)
        {
            builder.Entity<NegotiationFirstStageSupplier>().HasComment("Describe first Stage Negotiation Suppliers List");

            builder.Entity<NegotiationFirstStageSupplier>()
                             .Property(e => e.Id)
                             .HasComment("Define the Uniqee Identifier for  Negotiation First stage Suppliers List Table");
            builder.Entity<NegotiationFirstStageSupplier>()
                             .Property(e => e.OfferId)
                             .HasComment("Define The related offer");
            builder.Entity<NegotiationFirstStageSupplier>()
                             .Property(e => e.NegotiationId)
                             .HasComment("Define The Negotiation Table ");
            builder.Entity<NegotiationFirstStageSupplier>()
                             .Property(e => e.SupplierCR)
                             .HasComment("Define the supplier who will recieved the negotiation request  ");
            builder.Entity<NegotiationFirstStageSupplier>()
                         .Property(e => e.offerOriginalAmount)
                         .HasComment("Define The Amount of the offer before deduction");
            builder.Entity<NegotiationFirstStageSupplier>()
                  .Property(e => e.NegotiationSupplierStatusId)
                  .HasComment("The status of Request for the supplier is accepted from supplier or rejected or still not sent to the supplier");
            builder.Entity<NegotiationFirstStageSupplier>()
                  .Property(e => e.PeriodStartDateTime)
                  .HasComment("Define The start date that the supplier recieved the request ");

            builder.Entity<NegotiationFirstStageSupplier>()
                                 .Property(e => e.IsReported)
                                 .HasComment("Define if the supplier notified that he has Negotiation request");



        }
        #endregion


        #region Nawaf 
        #endregion

        #endregion
        private void InitializeCombinedStatus(ModelBuilder builder)
        {
            builder.Entity<OfferSolidarityStatus>().HasData(
                 new { CombinedStatusId = 1, Name = "جديدة" },
                 new { CombinedStatusId = 2, Name = "تم القبول" },
                 new { CombinedStatusId = 3, Name = "تم الرفض" },
                 new { CombinedStatusId = 4, Name = "بإنتظار الرد" }
                  );
        }

        private void InitializeNegotiationRequestStages(ModelBuilder builder)
        {
            builder.Entity<NegotiationType>().HasData(
                new { NegotiationTypeId = (int)Enums.enNegotiationType.FirstStage, Name = "التفاوض المرحلة الاولى" },
              new { NegotiationTypeId = (int)Enums.enNegotiationType.SecondStage, Name = "التفاوض المرحلة الثانية" }

              );
        }

        private void BaseEntityMap(ModelBuilder builder)
        {
            builder.Entity<SupplierAttachment>()
                           .HasDiscriminator<Enums.AttachmentType>("AttachType")
                           .HasValue<SupplierSpecificationAttachment>(Enums.AttachmentType.ClassificationCertificate)
                           .HasValue<SupplierBankGuaranteeAttachment>(Enums.AttachmentType.GuaranteeLetter)
                           .HasValue<SupplierOriginalAttachment>(Enums.AttachmentType.SupplierOriginalAttachment)
                           .HasValue<SupplierOriginalAttachment>(Enums.AttachmentType.SupplierOriginalAttachment)
                           .HasValue<SupplierCombinedAttachment>(Enums.AttachmentType.SupplierCombinedAttachment)
                           .HasValue<SupplierFinancialProposalAttachment>(Enums.AttachmentType.SupplierFinancialProposalAttachment)
                           .HasValue<SupplierTechnicalProposalAttachment>(Enums.AttachmentType.SupplierTechnicalProposalAttachment)
                           .HasValue<SupplierFinancialandTechnicalProposalAttachment>(Enums.AttachmentType.SupplierTechnicalAndFinancialProposalAttachment);

            builder.Entity<Negotiation>().ToTable("Negotiation", "CommunicationRequest")
                                    .HasDiscriminator<int>("NegotiationTypeId")
                                     .HasValue<NegotiationFirstStage>((int)Enums.NegotiationType.FirstStage)
                                    .HasValue<NegotiationSecondStage>((int)Enums.NegotiationType.SecondStage);


            builder.Entity<UserRole>()
        .Property(b => b.IsCombinedRole)
        .HasDefaultValue(0);

            builder.Entity<BaseNotification>().ToTable("Notification", "Notification")
                          .HasDiscriminator<int>("NotifayByType")
                          .HasValue<NotificationEmail>(1)
                          .HasValue<NotificationSMS>(2)
                          .HasValue<NotificationPanel>(3);
        }

        #region Seeds =======================================================
        private void InitializeSupplierNegotiationStatus(ModelBuilder builder)
        {
            builder.Entity<NegotiationSupplierStatus>().HasData(
                new { NegotiationSupplierStatusId = (int)Enums.enNegotiationSupplierStatus.Agree, Name = "تمت الموافقة" },
                new { NegotiationSupplierStatusId = (int)Enums.enNegotiationSupplierStatus.DisAgree, Name = "تم الرفض" },
                new { NegotiationSupplierStatusId = (int)Enums.enNegotiationSupplierStatus.NoReply, Name = "لا يوجد رد" },
                new { NegotiationSupplierStatusId = (int)Enums.enNegotiationSupplierStatus.NotSent, Name = "لم يتم ارسال" },
                new { NegotiationSupplierStatusId = (int)Enums.enNegotiationSupplierStatus.PendeingSupplierReply, Name = "فى انتظار رد المورد" },
                new { NegotiationSupplierStatusId = (int)Enums.enNegotiationSupplierStatus.AgreeWithExtraDiscount, Name = "تم الرد بالموافقة مع تخفيض إضافي " }
               );
        }
        private void InitializeAgencyCommunicationRequestStatus(ModelBuilder builder)
        {
            builder.Entity<AgencyCommunicationRequestStatus>().HasData(
                new { Id = 1, Name = "تحت التحديث" },
                new { Id = 2, Name = "بإنتظار إعتماد الطلب" },
                new { Id = 3, Name = "تم الإعتماد" },
                new { Id = 4, Name = "تم رفض إعتماد الطلب " },
                new { Id = 5, Name = "بإنتظار اعتماد مدير التظلم " },
                new { Id = 6, Name = "معتمدة من مدير التظلم " },
                new { Id = 7, Name = " مرفوضة من مدير التظلم" },
                new { Id = 8, Name = "تم إرسال الطلب" },
                new { Id = 9, Name = "تم إرسال إستفسار وبإنتظار الرد" },
                new { Id = 10, Name = "تم الرد على الإستفسار" },
                new { Id = (int)Enums.AgencyCommunicationRequestStatus.Ended, Name = "منتهية" },
                new { Id = (int)Enums.AgencyCommunicationRequestStatus.UnderRevision, Name = "تحت المراجعة" },
                new { Id = (int)Enums.AgencyCommunicationRequestStatus.TenderExtendedDates, Name = "تم تمديد تواريخ المنافسة" }
            );
        }

        private void InitializeActivityTemplates(ModelBuilder builder)
        {
            builder.Entity<Template>().HasData(
                new { ActivitytemplatId = 1, NameAr = "عام" },
                new { ActivitytemplatId = 2, NameAr = "إنشاء الطرق " },
                new { ActivitytemplatId = 3, NameAr = "انشاء مباني" },
                new { ActivitytemplatId = 4, NameAr = "الصيانة والتشغيل" },
                new { ActivitytemplatId = 5, NameAr = "الصيانة الطبية " },
                new { ActivitytemplatId = 6, NameAr = "المستلزمات الطبية" },
                new { ActivitytemplatId = 7, NameAr = "التغذية " },
                new { ActivitytemplatId = 8, NameAr = " الادوية" },
                new { ActivitytemplatId = 9, NameAr = "تقنية المعلومات" },
                new { ActivitytemplatId = 10, NameAr = "الخدمات الهندسية (تصميم)" },
                new { ActivitytemplatId = 11, NameAr = "نظافة المدن" },
                new { ActivitytemplatId = 12, NameAr = "الخدمات الاستشارية" },
                new { ActivitytemplatId = 13, NameAr = "المستلزمات العامة(التوريد)" },
                new { ActivitytemplatId = 14, NameAr = "الخدمات الهندسية (اشراف, )" },
                new { ActivitytemplatId = 15, NameAr = "جداول البيانات" }
            );
        }
        private void InitializeRoleName(ModelBuilder builder)
        {
            builder.Entity<UserRole>().HasData(
                new { UserRoleId = 1, Name = "NewMonafasat_DataEntry", DisplayNameAr = "مدخل بيانات المنافسة", DisplayNameEn = "DataEntry", IsCombinedRole = false },
                new { UserRoleId = 2, Name = "NewMonafasat_CustomerService", DisplayNameAr = "خدمة عملاء", DisplayNameEn = "Customer Service", IsCombinedRole = false },
                new { UserRoleId = 3, Name = "NewMonafasat_Auditer", DisplayNameAr = "مدقق بيانات المنافسة", DisplayNameEn = "Auditor", IsCombinedRole = false },
                new { UserRoleId = 4, Name = "NewMonafasat_Supplier", DisplayNameAr = "مورد", DisplayNameEn = "Supplier", IsCombinedRole = false },
                new { UserRoleId = 5, Name = "NewMonafasat_OffersOpeningManager", DisplayNameAr = "رئيس لجنة فتح العروض", DisplayNameEn = "Open Manager", IsCombinedRole = false },
                new { UserRoleId = 6, Name = "NewMonafasat_OffersOpeningSecretary", DisplayNameAr = "سكرتير لجنة فتح العروض", DisplayNameEn = "Open Secretary", IsCombinedRole = false },
                new { UserRoleId = 7, Name = "NewMonafasat_OffersCheckManager", DisplayNameAr = "رئيس لجنة فحص العروض", DisplayNameEn = "Check Manager", IsCombinedRole = false },
                new { UserRoleId = 8, Name = "NewMonafasat_OffersCheckSecretary", DisplayNameAr = "سكرتير لجنة فحص العروض", DisplayNameEn = "Check Secretary", IsCombinedRole = false },
                new { UserRoleId = 9, Name = "NewMonafasat_TechnicalCommitteeUser", DisplayNameAr = "مسؤول الجهة الفنية", DisplayNameEn = "Technical Committee User", IsCombinedRole = false },
                new { UserRoleId = 10, Name = "NewMonafasat_AccountManager", DisplayNameAr = "مدير حساب منافسات بإعتماد", DisplayNameEn = "Account Manager", IsCombinedRole = false },
                new { UserRoleId = 11, Name = "NewMonafasat_ManagerBlockCommittee", DisplayNameAr = "لجنة المنع (منافسات)", DisplayNameEn = "Block Specialist", IsCombinedRole = false },
                new { UserRoleId = 12, Name = "NewMonafasat_Admin", DisplayNameAr = "مدير منافسات", DisplayNameEn = "Monafasat Admin", IsCombinedRole = false },
                new { UserRoleId = 20, Name = "NewMonafasat_UnitManager", DisplayNameAr = "رئيس مركز تحقيق كفاءة الإنفاق", DisplayNameEn = "Achieving Efficient Spending Center Manager", IsCombinedRole = false },
                new { UserRoleId = 21, Name = "NewMonafasat_UnitSpecialistLevel1", DisplayNameAr = "مختص مستوى أول مركز تحقيق كفاءة الإنفاق", DisplayNameEn = "Achieving Efficient Spending Center Specialist 1", IsCombinedRole = false },
                new { UserRoleId = 22, Name = "NewMonafasat_UnitSpecialistLevel2", DisplayNameAr = "مختص مستوى ثاني مركز تحقيق كفاءة الإنفاق", DisplayNameEn = "Achieving Efficient Spending Center Specialist 2", IsCombinedRole = false },
                new { UserRoleId = 23, Name = "NewMonafasat_ManagerDirtectPurshasingCommittee", DisplayNameAr = "رئيس لجنة الشراء المباشر", DisplayNameEn = "Direct Purchase Check Manager", IsCombinedRole = false },
                new { UserRoleId = 24, Name = "NewMonafasat_SecretaryDirtectPurshasingCommittee", DisplayNameAr = "سكرتير لجنة الشراء المباشر", DisplayNameEn = "Direct Purchase Check Secretary", IsCombinedRole = false },
                new { UserRoleId = 25, Name = "NewMonafasat_ApproveTenderAward", DisplayNameAr = "اعتماد ترسية المنافسة لصاحب الصلاحية", DisplayNameEn = "Approve Tender Awarding", IsCombinedRole = false },
                new { UserRoleId = 26, Name = "NewMonafasat_SecretaryBlockCommittee", DisplayNameAr = "سكرتير منع", DisplayNameEn = "Block Secretary", IsCombinedRole = false },
                new { UserRoleId = 27, Name = "NewMonafasat_ManagerGrievanceCommittee", DisplayNameAr = "رئيس لجنة النظر فى التظلم", DisplayNameEn = "Manager Grievance Committee", IsCombinedRole = false },
                new { UserRoleId = 28, Name = "NewMonafasat_SecretaryGrievanceCommittee", DisplayNameAr = "سكرتير لجنة النظر فى التظلم", DisplayNameEn = "Secretary Grievance Committee", IsCombinedRole = false },
                new { UserRoleId = 29, Name = "NewMonafasat_PreQualificationCommitteeManager", DisplayNameAr = "رئيس لجنة التأهيل", DisplayNameEn = "PreQualification Check  Manager", IsCombinedRole = false },
                new { UserRoleId = 30, Name = "NewMonafasat_PreQualificationCommitteeSecretary", DisplayNameAr = "سكرتير لجنة التأهيل", DisplayNameEn = "PreQualification Check Secretary", IsCombinedRole = false },
                new { UserRoleId = 31, Name = "NewMonafasat_PlanningOfficer", DisplayNameAr = "مسئول التخطبيط المسبق", DisplayNameEn = "Planning Officer", IsCombinedRole = false },
                new { UserRoleId = 32, Name = "NewMonafasat_PlanningApprover", DisplayNameAr = "مدقق التخطيط المسبق", DisplayNameEn = "Planning Approver", IsCombinedRole = false },
                new { UserRoleId = 33, Name = "NewMonafasat_UnitBusinessManagement", DisplayNameAr = "صاحب صلاحية ادارة أعمال  مركز تحقيق كفاءة الإنفاق", DisplayNameEn = "Unit Business Management", IsCombinedRole = false },
                new { UserRoleId = 34, Name = "NewMonafasat_OffersOpeningAndCheckManager", DisplayNameAr = "رئيس لجنة فتح و فحص العروض", DisplayNameEn = "Offers Opening And Check Manager", IsCombinedRole = false },
                new { UserRoleId = 35, Name = "NewMonafasat_OffersOpeningAndCheckSecretary", DisplayNameAr = "سكرتير لجنة فتح و فحص العروض", DisplayNameEn = "Offers Opening And Check Secretary", IsCombinedRole = false },
                new { UserRoleId = 36, Name = "NewMonafasat_EtimadOfficer", DisplayNameAr = "مسؤول اعتماد", DisplayNameEn = "Etimad Officer", IsCombinedRole = false },
                new { UserRoleId = 37, Name = "NewMonafasat_PurshaseSpecialist", DisplayNameAr = "أخصائي مشتريات", DisplayNameEn = "Purshase Specialist", IsCombinedRole = false },
                new { UserRoleId = 38, Name = "NewMonafasat_AccountsManagementSpecialist", DisplayNameAr = "مختص ادارة الحسابات", DisplayNameEn = "Accounts Management Specialist", IsCombinedRole = false },
                new { UserRoleId = 39, Name = "NewMonafasat_ProductionManager", DisplayNameAr = "مدير إدارة المنتجات - تعديل", DisplayNameEn = "Product Manager - Edit", IsCombinedRole = false },
                new { UserRoleId = 40, Name = "NewMonafasat_ProductionManagerDisplay", DisplayNameAr = "مدير إدارة المنتجات - عرض", DisplayNameEn = "Product Manager - Display", IsCombinedRole = false },
                new { UserRoleId = 41, Name = "LC_ProductsOfficer", DisplayNameAr = "مسؤول منتجات القائمة الإلزامية", DisplayNameEn = "Mandatory List Officer", IsCombinedRole = false },
                new { UserRoleId = 42, Name = "LC_ProductsApprover", DisplayNameAr = "معتمد منتجات القائمة الإلزامية", DisplayNameEn = "Mandatory List Approver", IsCombinedRole = false },
                new { UserRoleId = 43, Name = "NewMonafasat_LocalContentOfficer", DisplayNameAr = "مسؤول متطلبات المحتوى المحلي", DisplayNameEn = "Local Content Officer", IsCombinedRole = false },
                new { UserRoleId = (int)Enums.UserRole.NewMonafasat_FinancialSupervisor, Name = "NewMonafasat_FinancialSupervisor", DisplayNameAr = "المراقب المالى ", DisplayNameEn = "Financial Supervisor", IsCombinedRole = false },
                new { UserRoleId = (int)Enums.UserRole.CR_DirectPurchaseMember, Name = "CR_DirectPurchaseMember", DisplayNameAr = "عضولجنة الشراء المباشر", DisplayNameEn = "Direct Purchase Member", IsCombinedRole = true }
          );
        }

        private void InitializeCommitteeType(ModelBuilder builder)
        {
            builder.Entity<CommitteeType>().HasData(
                new { CommitteeTypeId = 1, NameAr = "لجنة فنية" },
                new { CommitteeTypeId = 2, NameAr = "لجنة فتح العروض" },
                new { CommitteeTypeId = 3, NameAr = "لجنة فحص العروض" },
                new { CommitteeTypeId = 4, NameAr = "لجنة المنع" },
                new { CommitteeTypeId = 5, NameAr = "لجنة الشراء" },
                new { CommitteeTypeId = 6, NameAr = "لجنة التأهيل المسبق" },
                new { CommitteeTypeId = 7, NameAr = "لجنة فتح وفحص العروض(مكتب تحقيق الرؤية)" }
            );
        }

        private void InitializeTenderCreatedByType(ModelBuilder builder)
        {
            builder.Entity<TenderCreatedByType>().HasData(
                new { TenderCreatedByTypeId = (int)Enums.TenderCreatedByType.VRO, NameAr = "مكتب تحقيق الرؤية" },
                new { TenderCreatedByTypeId = (int)Enums.TenderCreatedByType.AgenciesRelatedByVRO, NameAr = "جهة مرتبطة بمكتب تحقيق الرؤية" },
                new { TenderCreatedByTypeId = (int)Enums.TenderCreatedByType.ExceptionalAgencies, NameAr = "جهات مستثناة" }
            );
        }
        private void InitializeBranchAddressType(ModelBuilder builder)
        {
            builder.Entity<AddressType>().HasData(
                new { AddressTypeId = (int)Enums.BranchAddressType.MainBranchAddress, AddressTypeName = "العنوان الرئيسي" },
                //new { AddressTypeId = (int)Enums.BranchAddressType.DeliverSpecificationBookOfferAddress, AddressTypeName = "عناوين استالم كراسة الشروط والمواصفات يدويا" },
                new { AddressTypeId = (int)Enums.BranchAddressType.DeliverOfferAddress, AddressTypeName = "عناوين تسليم العينات" },
                new { AddressTypeId = (int)Enums.BranchAddressType.OpenOfferAddress, AddressTypeName = "عناوين فتح العروض" }
                );
        }

        private void InitializeBlockType(ModelBuilder builder)
        {
            builder.Entity<BlockType>().HasData(
                new
                {
                    BlockTypeId = (int)Enums.BlockType.Permenant,
                    Name = Convert.ToString(EnumFactory.ToDisplay(Enums.BlockType.Permenant))
                },
                new
                {
                    BlockTypeId = (int)Enums.BlockType.Tamporary,
                    Name = Convert.ToString(EnumFactory.ToDisplay(Enums.BlockType.Tamporary))
                }
             );
        }
        private void InitializeBlockStatus(ModelBuilder builder)
        {
            builder.Entity<BlockStatus>().HasData(
                new
                {
                    BlockStatusId = (int)Enums.BlockStatus.NewAdmin,
                    BlockStatusName = Convert.ToString(EnumFactory.ToDisplay(Enums.BlockStatus.NewAdmin)),
                    BlockStatusNamear = "بانتظار اعتماد سكرتير لجنه المنع"
                },
                new
                {
                    BlockStatusId = (int)Enums.BlockStatus.NewSecretary,
                    BlockStatusName = Convert.ToString(EnumFactory.ToDisplay(Enums.BlockStatus.NewSecretary)),
                    BlockStatusNameAr = "بانتظار الارسال الى اعتماد مدير لجنه المنع"
                },
                new
                {
                    BlockStatusId = (int)Enums.BlockStatus.ApprovedSecertary,
                    BlockStatusName = Convert.ToString(EnumFactory.ToDisplay(Enums.BlockStatus.ApprovedSecertary)),
                    BlockStatusNameAr = "بانتظار اعتماد مدير لجنه المنع"
                },
                new
                {
                    BlockStatusId = (int)Enums.BlockStatus.RejectedSecertary,
                    BlockStatusName = Convert.ToString(EnumFactory.ToDisplay(Enums.BlockStatus.RejectedSecertary)),
                    BlockStatusNameAr = "تم الرفض من قبل سكرتير لجنه المنع"
                },
                new
                {
                    BlockStatusId = (int)Enums.BlockStatus.ApprovedManager,
                    BlockStatusName = Convert.ToString(EnumFactory.ToDisplay(Enums.BlockStatus.ApprovedManager)),
                    BlockStatusNameAr = "تم الاعتماد من قبل مدير لجنه المنع"
                },
                new
                {
                    BlockStatusId = (int)Enums.BlockStatus.RejectedManager,
                    BlockStatusName = Convert.ToString(EnumFactory.ToDisplay(Enums.BlockStatus.RejectedManager)),
                    BlockStatusNameAr = "تم الرفض من قبل مدير لجنه المنع"
                },
                  new
                  {
                      BlockStatusId = (int)Enums.BlockStatus.RemoveBlock,
                      BlockStatusName = Convert.ToString(EnumFactory.ToDisplay(Enums.BlockStatus.RemoveBlock)),
                      BlockStatusNameAr = "تم إزالة المنع"
                  }
             );
        }

        private void InitializeTenderApplyType(ModelBuilder builder)
        {
            builder.Entity<FavouriteSupplierTender>()
                .HasKey(k => new { Tender = k.TenderId, Supplier = k.SupplierCRNumber });
        }
        private void InitializePrePlanningStatus(ModelBuilder builder)
        {
            builder.Entity<PrePlanningStatus>().HasData(
               new { StatusId = (int)Enums.PrePlanningStatus.UnderUpdate, NameAr = "تحت التحديث" },
               new { StatusId = (int)Enums.PrePlanningStatus.Pending, NameAr = "بانتظار اعتماد الطلب" },
               new { StatusId = (int)Enums.PrePlanningStatus.Approved, NameAr = "معتمد" },
               new { StatusId = (int)Enums.PrePlanningStatus.Rejected, NameAr = "تم رفض اعتماد الطلب" }
           );
        }

        private void InitializeBillStatus(ModelBuilder builder)
        {
            builder.Entity<BillStatus>().HasData(
                new { BillStatusId = (int)Enums.BillStatus.New, BillStatusNameAr = "جديد" },
                new { BillStatusId = (int)Enums.BillStatus.UnderProcess, BillStatusNameAr = "تحت الإجراء" },
                new { BillStatusId = (int)Enums.BillStatus.SuccessUploaded, BillStatusNameAr = "تم اصدار رقم سداد" },
                new { BillStatusId = (int)Enums.BillStatus.Paid, BillStatusNameAr = "تم الشراء" },
                new { BillStatusId = (int)Enums.BillStatus.Rejected, BillStatusNameAr = "تم الرفض" }
            );
        }
        private void InitializeTenderFeesTypes(ModelBuilder builder)
        {
            builder.Entity<TenderFeesType>().HasData(
                new { TenderFeesTypeId = (int)Enums.TenderFeesType.FinantialCostForInvitation, NameArabic = "المقابل المالى للدعوة" },
                new { TenderFeesTypeId = (int)Enums.TenderFeesType.FinantialCostForConditionalBooklet, NameArabic = "المقابل المالى لكراسة الشروط" },
                new { TenderFeesTypeId = (int)Enums.TenderFeesType.ConditionalBookletPrice, NameArabic = "قيمة كراسة الشروط" }
            );
        }

        private void InitializeMandatoryListStatus(ModelBuilder builder)
        {
            builder.Entity<MandatoryListStatus>().HasData(
                new { StatusId = (int)Enums.MandatoryListStatus.UnderEstablishing, NameAr = "تحت الإنشاء" },
                new { StatusId = (int)Enums.MandatoryListStatus.WaitingApproval, NameAr = "بانتظار الاعتماد" },
                new { StatusId = (int)Enums.MandatoryListStatus.Rejected, NameAr = "تم الرفض" },
                new { StatusId = (int)Enums.MandatoryListStatus.Approved, NameAr = "معتمدة" },
                new { StatusId = (int)Enums.MandatoryListStatus.WaitingCancelApproval, NameAr = "بانتظار اعتماد الإلغاء" },
                new { StatusId = (int)Enums.MandatoryListStatus.CancelRejected, NameAr = "تم رفض الالغاء" },
                new { StatusId = (int)Enums.MandatoryListStatus.Canceled, NameAr = "تم الالغاء" }
            );
        }

        private void InitializeMandatoryListChangeRequestStatus(ModelBuilder builder)
        {
            builder.Entity<MandatoryListChangeRequestStatus>().HasData(
                new { StatusId = (int)Enums.MandatoryListChangeRequestStatus.WaitingApproval, NameAr = "بانتظار الاعتماد" },
                new { StatusId = (int)Enums.MandatoryListChangeRequestStatus.Rejected, NameAr = "تم الرفض" },
                new { StatusId = (int)Enums.MandatoryListChangeRequestStatus.Approved, NameAr = "معتمدة" },
                new { StatusId = (int)Enums.MandatoryListChangeRequestStatus.Closed, NameAr = "مغلقة" }
            );
        }

        private void InitializeOfferStatus(ModelBuilder builder)
        {
            builder.Entity<OfferStatus>().HasData(
                new { OfferStatusId = (int)Enums.OfferStatus.UnderEstablishing, NameAr = "تحت الانشاء" },
                new { OfferStatusId = (int)Enums.OfferStatus.Established, NameAr = "مكتمل فى انتظار الارسال" },
                new { OfferStatusId = (int)Enums.OfferStatus.Received, NameAr = "مستلم" },
                new { OfferStatusId = (int)Enums.OfferStatus.Canceled, NameAr = "ملغى" },
                 new { OfferStatusId = (int)Enums.OfferStatus.Accepted, Name = "مطابق " },
                new { OfferStatusId = (int)Enums.OfferStatus.Rejected, Name = "غير مطابق" },
                new { OfferStatusId = (int)Enums.OfferStatus.Excluded, Name = "مستبعد" },
                new { OfferStatusId = (int)Enums.OfferStatus.ChangeByQT, Name = "معاد بعد تعديل جداول الكميات" });
        }

        private void InitializeNotifacationStatusEntity(ModelBuilder builder)
        {
            builder.Entity<NotifacationStatusEntity>().HasData(
                new { NotifacationStatusEntityId = 1, Name = "مرسل" },
                new { NotifacationStatusEntityId = 2, Name = "لم يتم الارسال" },
                new { NotifacationStatusEntityId = 3, Name = "فشل فى الارسال" },
                new { NotifacationStatusEntityId = 4, Name = "مقروءه" },
                new { NotifacationStatusEntityId = 5, Name = "غير مقروءه" }
            );
        }

        private void InitializeNotifayTypeEntity(ModelBuilder builder)
        {
            builder.Entity<NotifayTypeEntity>().HasData(
                new { NotifayTypeId = 1, NameAr = "حاله العرض" },
                new { NotifayTypeId = 2, NameAr = "استفسارات حول منافسه" },
                new { NotifayTypeId = 3, NameAr = "عمليات حدثت على حسابك" },
                new { NotifayTypeId = 4, NameAr = "عمليات حدثة على المنافسة" },
                new { NotifayTypeId = 5, NameAr = "رقم فاتوره سداد" },
                new { NotifayTypeId = 6, NameAr = "تغير كلمة المرور" },
                new { NotifayTypeId = 7, NameAr = "نسيت كلمة المرور" },
                new { NotifayTypeId = 8, NameAr = "منافسة لها علاقه بنشاطكم" },
                new { NotifayTypeId = 9, NameAr = "استلام مبلغ كراسة الشروط" },
                new { NotifayTypeId = 10, NameAr = "كود الدخول الى النظام" },
                new { NotifayTypeId = 11, NameAr = "عمليات تحتاج الى الاعتماد" },
                new { NotifayTypeId = 13, NameAr = "عمليات عامة " }
            );
        }

        private void InitializeArea(ModelBuilder builder)
        {
            builder.Entity<Area>().HasData(
                new { AreaId = 1, NameEn = "Riyadh", NameAr = "منطقة الرياض" },
                new { AreaId = 2, NameEn = "Mekkah", NameAr = "منطقة مكة المكرمة" },
                new { AreaId = 3, NameEn = "Maddinah", NameAr = "منطقة المدينة المنورة " },
                new { AreaId = 4, NameEn = "Quaseem", NameAr = "منطقة القصيم " },
                new { AreaId = 5, NameEn = "Sharkia", NameAr = "المنطقة الشرقية" },
                new { AreaId = 6, NameEn = "Asir", NameAr = "منطقة عسير" },
                new { AreaId = 7, NameEn = "Tabuk", NameAr = "منطقة تبوك" },
                new { AreaId = 8, NameEn = "Hail", NameAr = "منطقة حائل" },
                new { AreaId = 9, NameEn = "North Borders", NameAr = "منطقة الحدود الشمالية" },
                new { AreaId = 10, NameEn = "Jazan", NameAr = "منطقة جازان" },
                new { AreaId = 11, NameEn = "Najran", NameAr = "منطقة نجران" },
                new { AreaId = 12, NameEn = "Bahah", NameAr = "منطقة الباحة" },
                new { AreaId = 13, NameEn = "Jof", NameAr = "منطقة الجوف" }
                );
        }


        private void InitializeLocalContentMechanism(ModelBuilder builder)
        {
            builder.Entity<LocalContentMechanismPreference>().HasData(
                new { Id = 1, NameEn = "آلية وزن المحتوى المحلي في التقييم المالي", NameAr = "آلية وزن المحتوى المحلي في التقييم المالي" },
                new { Id = 2, NameEn = "آلية الحد الأدنى المطلوب للمحتوى المحلي", NameAr = "آلية الحد الأدنى المطلوب للمحتوى المحلي" },
                new { Id = 3, NameEn = "آلية التفضيل السعري للمنتج", NameAr = "آلية التفضيل السعري للمنتج " }
                );
        }

        private void InitializeAttachmentType(ModelBuilder builder)
        {
            builder.Entity<AttachmentType>().HasData(
                new { AttachmentTypeId = 1, NameAr = "Tender Booklet" },
                new { AttachmentTypeId = 2, NameAr = "QuantityTableAttachment" },
                new { AttachmentTypeId = 3, NameAr = "Offer File" },
                new { AttachmentTypeId = 4, NameAr = "Guarantee Letter" },
                new { AttachmentTypeId = 5, NameAr = "Tender Purchase Envoice" },
                new { AttachmentTypeId = 6, NameAr = "Certificate Of Visitation" },
                new { AttachmentTypeId = 7, NameAr = "Classification Certificate" },
                new { AttachmentTypeId = 8, NameAr = "VAT Certificate" },
                new { AttachmentTypeId = 9, NameAr = "Social insurance certificate" },
                new { AttachmentTypeId = 10, NameAr = "Saudization Certificate" },
                new { AttachmentTypeId = 11, NameAr = "Commercial Register" },
                new { AttachmentTypeId = 12, NameAr = "Chamber of Commerce Certificate" },
                new { AttachmentTypeId = 13, NameAr = "Offer Letter" },
                new { AttachmentTypeId = 14, NameAr = "Offer Copy" },
                new { AttachmentTypeId = 15, NameAr = "SupplierOriginalAttachment" },
                new { AttachmentTypeId = 16, NameAr = "Tender File" },
                new { AttachmentTypeId = 17, NameAr = "Supplier Combined Attachment" },
                new { AttachmentTypeId = 18, NameAr = "Supplier Financial Proposal Attachment" },
                new { AttachmentTypeId = 19, NameAr = "Supplier Technical Proposal Attachment" },
                new { AttachmentTypeId = 20, NameAr = "Plain Request" },
                new { AttachmentTypeId = 21, NameAr = "First Stage Negotiation Letter" },
                new { AttachmentTypeId = 22, NameAr = "Initial Guarantee" },
                new { AttachmentTypeId = 23, NameAr = "Unit Modifications Attachments To Data Entry" },
                new { AttachmentTypeId = 24, NameAr = "Negotiation" },
                new { AttachmentTypeId = 25, NameAr = "Escalation" },
                new { AttachmentTypeId = 26, NameAr = "TechnicianReport" },
                new { AttachmentTypeId = 28, NameAr = "LocalContent" }

                );
        }

        private void InitializeBank(ModelBuilder builder)
        {
            builder.Entity<Bank>().HasData(
                 new { BankId = 1, NameEn = "The National Commercial Bank", NameAr = "البنك الأهلي التجاري" },
                 new { BankId = 2, NameEn = "The Saudi British Bank", NameAr = "البنك السعودي البريطاني" },
                 new { BankId = 3, NameEn = "Banque Saudi Fransi", NameAr = "البنك السعودي الفرنسي" },
                 new { BankId = 4, NameEn = "Alawwal Bank", NameAr = "البنك الأول" },
                 new { BankId = 5, NameEn = "Saudi Investment Bank", NameAr = "البنك السعودي للاستثمار" },
                 new { BankId = 6, NameEn = "Arab National Bank", NameAr = "البنك العربي الوطني" },
                 new { BankId = 7, NameEn = "Bank AlBilad", NameAr = "بنك البلاد" },
                 new { BankId = 8, NameEn = "Bank AlJazira", NameAr = "بنك الجزيرة" },
                 new { BankId = 9, NameEn = "Riyad Bank", NameAr = "بنك الرياض" },
                 new { BankId = 10, NameEn = "Samba Financial Group (Samba)", NameAr = "مجموعة سامبا المالية (سامبا)" },
                 new { BankId = 11, NameEn = "Al Rajhi Bank", NameAr = "مصرف الراجحي" },
                 new { BankId = 12, NameEn = "alinma bank", NameAr = "مصرف الإنماء" },
                 new { BankId = 13, NameEn = "Gulf International Bank", NameAr = "بنك الخليج الدولي" },
                 new { BankId = 14, NameEn = "Emirates NBD", NameAr = "بنك الإمارات دبي الوطني" },
                 new { BankId = 15, NameEn = "National Bank of Bahrain", NameAr = "بنك البحرين الوطني" },
                 new { BankId = 16, NameEn = "National Bank of Kuwait", NameAr = "بنك الكويت الوطني" },
                 new { BankId = 17, NameEn = "BankMuscat", NameAr = "بنك مسقط" },
                 new { BankId = 18, NameEn = "Deutsche Bank", NameAr = "دويتشه بنك" },
                 new { BankId = 19, NameEn = "BNP Paribas", NameAr = "بي إن بي باريبا" },
                 new { BankId = 20, NameEn = "JPMorgan Chase & Co", NameAr = "جي بي مورقان تشيز إن أيه" },
                 new { BankId = 21, NameEn = "National Bank of Pakistan", NameAr = "بنك باكستان الوطني" },
                 new { BankId = 22, NameEn = "State Bank of India Bahrain", NameAr = "ستيت بنك أوف إنديا" },
                 new { BankId = 23, NameEn = "Ziraat Bankası", NameAr = "بنك تي سي زراعات بانكاسي" },
                 new { BankId = 24, NameEn = "ICBC China", NameAr = "بنك الصين للصناعة والتجارة" }
                   );
        }

        private void InitializeChangeRequestStatus(ModelBuilder builder)
        {
            builder.Entity<ChangeRequestStatus>().HasData(
                new { Id = 1, NameAr = "جديدة" },
                new { Id = 2, NameAr = "بانتظار الاعتماد" },
                new { Id = 3, NameAr = "معتمدة" },
                new { Id = 4, NameAr = "مرفوضة" }
                 );
        }

        private void InitializeMaintenanceRunningWork(ModelBuilder builder)
        {
            builder.Entity<MaintenanceRunningWork>().HasData(
                new { MaintenanceRunningWorkId = 1, NameAr = " صيانة وتشغيل المسالخ" },
                new { MaintenanceRunningWorkId = 2, NameAr = "تقنية الإتصالات" },
                new { MaintenanceRunningWorkId = 3, NameAr = "صيانة وتشغيل أعمال المياه والصرف الصحي" },
                new { MaintenanceRunningWorkId = 4, NameAr = "حفر االأبار" },
                new { MaintenanceRunningWorkId = 5, NameAr = "صيانة وتشغيل الأعمال الصناعية" },
                new { MaintenanceRunningWorkId = 6, NameAr = "تخديم وتأمين التغذية للمراكز الطبية" },
                new { MaintenanceRunningWorkId = 7, NameAr = " نظافة المدن والتخلص من النفايات مقالب وردم النفايات " },
                new { MaintenanceRunningWorkId = 8, NameAr = " صيانة السدود" },
                new { MaintenanceRunningWorkId = 9, NameAr = "صيانة وتشغيل الأعمال البحرية" },
                new { MaintenanceRunningWorkId = 10, NameAr = "صيانة تقنية الإتصالات" },
                new { MaintenanceRunningWorkId = 11, NameAr = "صيــانة الحدائق والمنتزهـات" },
                new { MaintenanceRunningWorkId = 12, NameAr = "صيانة الطرق" },
                new { MaintenanceRunningWorkId = 13, NameAr = "صيانة وتشغيل الأعمال الميكانيكيـة" },
                new { MaintenanceRunningWorkId = 14, NameAr = "صيانة المباني" },
                new { MaintenanceRunningWorkId = 15, NameAr = "تخديم و تأمين الإعاشه لألفراد" },
                new { MaintenanceRunningWorkId = 16, NameAr = "صيانة المـراكـز الطبيــة" },
                new { MaintenanceRunningWorkId = 17, NameAr = "صيانة وتشغيل الأعمال اإللكترونيــة" },
                new { MaintenanceRunningWorkId = 18, NameAr = "صيانة وتشغيل الأعمال الكهربائية" },
                new { MaintenanceRunningWorkId = 19, NameAr = " صيانة وتشغيل تقنية الإتصالات" }
               );
        }

        private void InitializeChangeRequestType(ModelBuilder builder)
        {
            builder.Entity<ChangeRequestType>().HasData(
                new { Id = 1, NameAr = "تمديد التواريخ" },
                new { Id = 2, NameAr = "جداول الكميات" },
                new { Id = 3, NameAr = "الملحقات" },
                new { Id = 4, NameAr = "إلغاء" }
                 );
        }

        private void InitializeConstructionWork(ModelBuilder builder)
        {
            builder.Entity<ConstructionWork>().HasData(
                new { ConstructionWorkId = 1, NameAr = " حفر االبار " },
                new { ConstructionWorkId = 2, NameAr = "تشجيــر الحدائق وتنظيم المواقـع" },
                new { ConstructionWorkId = 3, NameAr = "الأعمال اإللكترونيــة" },
                new { ConstructionWorkId = 4, NameAr = " الســـــدود" },
                new { ConstructionWorkId = 5, NameAr = "المســـالـخ" },
                new { ConstructionWorkId = 6, NameAr = "الأعمال البحــريـة" },
                new { ConstructionWorkId = 7, NameAr = " الطــــرق" },
                new { ConstructionWorkId = 8, NameAr = " األعمــال الميكانيكيـة" },
                new { ConstructionWorkId = 9, NameAr = " الأعمال الصنــاعية" },
                new { ConstructionWorkId = 10, NameAr = " المبـــاني" },
                new { ConstructionWorkId = 11, NameAr = " الأعمال الكهربائيـة" },
                new { ConstructionWorkId = 12, NameAr = " أعمال المياه والصرف الصحي" },
                new { ConstructionWorkId = 13, NameAr = "تقنية الإتصالات" },
                new { ConstructionWorkId = 14, NameAr = "آبار أنبوبية", ParentID = 1 },
                new { ConstructionWorkId = 15, NameAr = "حفــر اآلبـــار", ParentID = 1 },
                new { ConstructionWorkId = 16, NameAr = "آبار يدوية", ParentID = 1 },
                new { ConstructionWorkId = 17, NameAr = "الحـــدائق", ParentID = 2 },
                new { ConstructionWorkId = 18, NameAr = "تنظيــم المواقــع", ParentID = 2 },
                new { ConstructionWorkId = 19, NameAr = "شبكات الري للحدائق", ParentID = 2 },
                new { ConstructionWorkId = 20, NameAr = "تشجيــر الحدائق وتنظيم المواقـع", ParentID = 2 },
                new { ConstructionWorkId = 21, NameAr = "المنتزهـات العـامة", ParentID = 2 },
                new { ConstructionWorkId = 22, NameAr = "تشجير الشــوارع", ParentID = 2 },
                new { ConstructionWorkId = 23, NameAr = "توريد وتركيب أجهزة ومعدات الحاسب اآللي (حاسبات وشبكات وملحقات},", ParentID = 3 },
                new { ConstructionWorkId = 24, NameAr = "المعدات اإللكترونية الطبية / المختبرية", ParentID = 3 },
                new { ConstructionWorkId = 25, NameAr = " نظام التحكم بسير اإلنتاج", ParentID = 3 },
                new { ConstructionWorkId = 26, NameAr = "نظام اإلرسال المرئي والصوتي", ParentID = 3 },
                new { ConstructionWorkId = 27, NameAr = "الأعمال اإللكترونيــة", ParentID = 3 },
                new { ConstructionWorkId = 28, NameAr = "معدات استوديوهات المرئيات والسمعيات", ParentID = 3 },
                new { ConstructionWorkId = 29, NameAr = "نظام المراقبة واألمن والسالمة", ParentID = 3 },
                new { ConstructionWorkId = 30, NameAr = "الســـــدود", ParentID = 4 },
                new { ConstructionWorkId = 31, NameAr = "الســــدود الخرسانية", ParentID = 4 },
                new { ConstructionWorkId = 32, NameAr = "الســــدود الترابية", ParentID = 4 },
                new { ConstructionWorkId = 33, NameAr = "المسالخ", ParentID = 5 },
                new { ConstructionWorkId = 34, NameAr = " مســالخ عـاديـة", ParentID = 5 },
                new { ConstructionWorkId = 35, NameAr = "مســالخ آليـة", ParentID = 5 },
                new { ConstructionWorkId = 36, NameAr = "حفر ودق الركائز", ParentID = 6 },
                new { ConstructionWorkId = 37, NameAr = " الحواجـز / الجسور / الطرق البحرية", ParentID = 6 },
                new { ConstructionWorkId = 38, NameAr = " أحواض إصالح السفن", ParentID = 6 },
                new { ConstructionWorkId = 39, NameAr = " الأعمال البحــريـة", ParentID = 6 },
                new { ConstructionWorkId = 40, NameAr = "األنفاق تحت المـاء" },
                new { ConstructionWorkId = 41, NameAr = "األرصفة البحـرية", ParentID = 6 },
                new { ConstructionWorkId = 42, NameAr = "التمديدات تحت المـاء" },
                new { ConstructionWorkId = 43, NameAr = "المـراسي", ParentID = 6 },
                new { ConstructionWorkId = 44, NameAr = " أعمال التعميق والتنظيف", ParentID = 6 },
                new { ConstructionWorkId = 45, NameAr = "الطــــرق", ParentID = 7 },
                new { ConstructionWorkId = 46, NameAr = "السكك الحديدية", ParentID = 7 },
                new { ConstructionWorkId = 47, NameAr = "السفلته", ParentID = 7 },
                new { ConstructionWorkId = 48, NameAr = " الأعمال الترابية إلنشاء الطرق وتشمل },الردم والتسوية والدك(", ParentID = 7 },
                new { ConstructionWorkId = 49, NameAr = "مدارج الطائرات", ParentID = 7 },
                new { ConstructionWorkId = 50, NameAr = "الجسور", ParentID = 7 },
                new { ConstructionWorkId = 51, NameAr = "األنفاق", ParentID = 7 },
                new { ConstructionWorkId = 52, NameAr = " صـوامـع الغــالل", ParentID = 8 },
                new { ConstructionWorkId = 53, NameAr = "نظـام التسخين الميكانيكي", ParentID = 8 },
                new { ConstructionWorkId = 54, NameAr = "محطات تنقية المياه محطات تنقية الصرف الصحي", ParentID = 8 },
                new { ConstructionWorkId = 55, NameAr = " التوربينات / الغاليات البخـارية", ParentID = 8 },
                new { ConstructionWorkId = 56, NameAr = "محطات ضخ المياه والصرف الصحي", ParentID = 8 },
                new { ConstructionWorkId = 57, NameAr = "األعمــال الميكانيكيـة", ParentID = 8 },
                new { ConstructionWorkId = 58, NameAr = "محطـات توليد الطاقـة", ParentID = 8 },
                new { ConstructionWorkId = 59, NameAr = "نظــام التهـوية", ParentID = 8 },
                new { ConstructionWorkId = 60, NameAr = " نظام التكييف المركزي", ParentID = 8 },
                new { ConstructionWorkId = 61, NameAr = "شبكات النقل بالهواء المضغوط" },
                new { ConstructionWorkId = 62, NameAr = "شبكات مداولة الشحنات السـائبة", ParentID = 8 },
                new { ConstructionWorkId = 63, NameAr = "المصـاعد / السـاللم المتحركة", ParentID = 8 },
                new { ConstructionWorkId = 64, NameAr = " أرصفة الحاويات", ParentID = 8 },
                new { ConstructionWorkId = 65, NameAr = "نظمة مكافحة الحرق", ParentID = 8 },
                new { ConstructionWorkId = 66, NameAr = " نظام السيور الناقلــة", ParentID = 8 },
                new { ConstructionWorkId = 67, NameAr = " نظام التــبريد", ParentID = 8 },
                new { ConstructionWorkId = 69, NameAr = "حاويات الغاز", ParentID = 9 },
                new { ConstructionWorkId = 70, NameAr = "أحزاض بناء وإصالح السفن", ParentID = 9 },
                new { ConstructionWorkId = 71, NameAr = "الصوامع", ParentID = 9 },
                new { ConstructionWorkId = 72, NameAr = "المصانع", ParentID = 9 },
                new { ConstructionWorkId = 73, NameAr = "محارق النفايات", ParentID = 9 },
                new { ConstructionWorkId = 74, NameAr = "الأعمال الصنــاعية", ParentID = 9 },
                new { ConstructionWorkId = 75, NameAr = "مطاحن الدقيق", ParentID = 9 },
                new { ConstructionWorkId = 76, NameAr = " شبكات أنابيب البترول والغاز", ParentID = 9 },
                new { ConstructionWorkId = 77, NameAr = "المناجم والصناعات التعدينية", ParentID = 9 },
                new { ConstructionWorkId = 78, NameAr = " محطات تحلية المياه", ParentID = 9 },
                new { ConstructionWorkId = 79, NameAr = " المباني الفوالذية }, الحديدية (", ParentID = 10 },
                new { ConstructionWorkId = 80, NameAr = "المباني سابقة الصب", ParentID = 10 },
                new { ConstructionWorkId = 81, NameAr = "المباني الخرسانية", ParentID = 10 },
                new { ConstructionWorkId = 82, NameAr = "المبـــاني", ParentID = 10 },
                new { ConstructionWorkId = 83, NameAr = "محطات توليد الطــاقة", ParentID = 11 },
                new { ConstructionWorkId = 84, NameAr = "إدارة الشوارع", ParentID = 11 },
                new { ConstructionWorkId = 85, NameAr = "محطات الطاقة الشمسية", ParentID = 11 },
                new { ConstructionWorkId = 86, NameAr = " الحماية الكاثودية", ParentID = 11 },
                new { ConstructionWorkId = 87, NameAr = "وحدات الطاقة غير المنقطعة", ParentID = 11 },
                new { ConstructionWorkId = 88, NameAr = "نقل وتوزيع الطاقة", ParentID = 11 },
                new { ConstructionWorkId = 89, NameAr = "محطات تحويل الطاقة", ParentID = 11 },
                new { ConstructionWorkId = 92, NameAr = "إشارات المرور الضوئية", ParentID = 11 },
                new { ConstructionWorkId = 93, NameAr = " محطات ضخ المياه والصرف الصحي", ParentID = 12 },
                new { ConstructionWorkId = 94, NameAr = "محطات تنقية الميـاه والصرف الصحي", ParentID = 12 },
                new { ConstructionWorkId = 95, NameAr = "خزانات المياه", ParentID = 12 },
                new { ConstructionWorkId = 96, NameAr = "أعمال المياه والصرف الصحي", ParentID = 12 },
                new { ConstructionWorkId = 97, NameAr = "شبكات تصريف السيول", ParentID = 12 },
                new { ConstructionWorkId = 98, NameAr = "شبكات المياه والصرف الصحي", ParentID = 12 },
                new { ConstructionWorkId = 99, NameAr = "مشاريع الري والصرف", ParentID = 12 },
                new { ConstructionWorkId = 100, NameAr = "تقنية الاتصاالت", ParentID = 13 },
                new { ConstructionWorkId = 101, NameAr = "قدرات الشبكة الأساسية", ParentID = 13 },
                new { ConstructionWorkId = 102, NameAr = "خدمات اتصالات معززة", ParentID = 13 },
                new { ConstructionWorkId = 103, NameAr = "خدمات الإنترنت", ParentID = 13 },
                new { ConstructionWorkId = 104, NameAr = "اتصالات هاتفية محلية وبعيدة", ParentID = 13 },
                new { ConstructionWorkId = 105, NameAr = " خدمات اتصالات باستخدام الألياف", ParentID = 13 },
                new { ConstructionWorkId = 106, NameAr = "تطوير النظم والتطبيقات وقواعد البيانات", ParentID = 13 },
                new { ConstructionWorkId = 107, NameAr = "خدمات وصول العملاء", ParentID = 13 },
                new { ConstructionWorkId = 108, NameAr = "خدمات اتصالات باستخدام دوائر الطلب الهاتفي والخطوط المخصصة", ParentID = 13 },
                new { ConstructionWorkId = 109, NameAr = "خدمات اتصالات خلوية", ParentID = 13 }
                );
        }

        private void InitializeEnquiryReplyStatus(ModelBuilder builder)
        {
            builder.Entity<EnquiryReplyStatus>().HasData(
                new { Id = 1, NameAr = "بإنتظار الإعتماد" },
                new { Id = 2, NameAr = "معتمد" }
                 );
        }

        private void InitializeInvitationStatus(ModelBuilder builder)
        {
            builder.Entity<InvitationStatus>().HasData(
                new { InvitationStatusId = 1, NameAr = "جديدة" },
                new { InvitationStatusId = 2, NameAr = "تم القبول" },
                new { InvitationStatusId = 3, NameAr = "تم الرفض" },
                new { InvitationStatusId = 4, NameAr = "تم الانسحاب" },
                new { InvitationStatusId = 5, Name = "بإنتظار الاعتماد" },
                new { InvitationStatusId = 6, Name = "تم اصدار رقم سداد" }
                 );
        }
        private void InitializeYearQuerters(ModelBuilder builder)
        {
            builder.Entity<YearQuarter>().HasData(
                new { YearQuarterId = (int)Enums.YearQuarters.FirstQuarters, NameAr = "الربع الأول", NameEn = "First Quarter" },
                new { YearQuarterId = (int)Enums.YearQuarters.SecondQuarters, NameAr = "الربع الثانى", NameEn = "Second Quarter" },
                new { YearQuarterId = (int)Enums.YearQuarters.ThirdQuarters, NameAr = "الربع الثالث", NameEn = "Third Quarter" },
                new { YearQuarterId = (int)Enums.YearQuarters.FourthQuarters, NameAr = "الربع الرابع", NameEn = "Fourth Quarter" }
               );
        }
        private void InitializeInvitationType(ModelBuilder builder)
        {
            builder.Entity<InvitationType>().HasData(
                new { InvitationTypeId = 1, NameAr = "عامه" },
                new { InvitationTypeId = 2, NameAr = "خاصة" }
               );
        }

        private void InitializeAnnouncementTemplateSupplierListType(ModelBuilder builder)
        {
            builder.Entity<AnnouncementTemplateListType>().HasData(
                new { AnnouncementTemplateSuppliersListTypeId = 1, NameAr = "عامه" },
                new { AnnouncementTemplateSuppliersListTypeId = 2, NameAr = "خاصة" }
               );
        }

        private void InitializeTenderStatus(ModelBuilder builder)
        {
            builder.Entity<TenderStatus>().HasData(
                        new { TenderStatusId = 1, NameAr = "تحت الانشاء" },
                        new { TenderStatusId = 2, NameAr = "تحت التحديث" },
                        new { TenderStatusId = 3, NameAr = "يانتظار الاعتماد" },
                        new { TenderStatusId = 4, NameAr = "معتمده" },
                        new { TenderStatusId = 5, NameAr = "مرفوضة" },
                        new { TenderStatusId = 6, NameAr = "مرحلة فتح العروض" },
                        new { TenderStatusId = 7, NameAr = "بإنتظار إعتماد تقرير فتح العروض" },
                        new { TenderStatusId = 8, NameAr = "تم فتح العروض" },
                        new { TenderStatusId = 9, NameAr = "تم رفض تقرير فتح العروض" },
                        new { TenderStatusId = 10, NameAr = "بانتظار اعتماد تقييم العروض" },
                        new { TenderStatusId = 11, NameAr = "تم إعتماد تقييم العروض" },
                        new { TenderStatusId = 12, NameAr = "تم رفض تقييم العروض" },
                        new { TenderStatusId = 13, NameAr = "مرحلة الترسية" },
                        new { TenderStatusId = 14, NameAr = "بإنتظار إعتماد الترسية" },
                        new { TenderStatusId = 15, NameAr = "تم إعتماد الترسية" },
                        new { TenderStatusId = 16, NameAr = "تم رفض الترسية" },
                        new { TenderStatusId = 17, NameAr = "تم الإلغاء" },
                        new { TenderStatusId = 18, NameAr = "مرحلة فحص العروض" },
                        new { TenderStatusId = 19, NameAr = " مرحلة تقييم وثائق التأهيل" },
                        new { TenderStatusId = 20, NameAr = "بإنتظار اعتماد تقييم وثائق التأهيل" },
                        new { TenderStatusId = 21, NameAr = " تم إعتماد تقييم وثائق التأهيل " },
                        new { TenderStatusId = 22, NameAr = "تم رفض تقييم وثائق التأهيل " },
                        new { TenderStatusId = 23, NameAr = "بانتظار الاعتماد من مركز تحقيق كفاءة الإنفاق" },
                        new { TenderStatusId = 24, NameAr = "تم رفض طرح المنافسة من مركز تحقيق كفاءة الإنفاق " },
                        new { TenderStatusId = 25, NameAr = "بانتظار تأكيد مرحلة فحص عروض الشراء المباشر" },
                        new { TenderStatusId = 26, NameAr = "مرحلة فحص عروض الشراء المباشر" },
                        new { TenderStatusId = 27, NameAr = "بانتظار اعتماد فحص عروض الشراء المباشر" },
                        new { TenderStatusId = 28, NameAr = "بإنتظار اعتماد تقييم العروض الفنية" },
                        new { TenderStatusId = 29, NameAr = " تم إعتماد تقييم العروض الفنية" },
                        new { TenderStatusId = 30, NameAr = "تم رفض تقييم العروض الفنية" },
                        new { TenderStatusId = 31, NameAr = "مرحلة التقييم المالي" },
                        new { TenderStatusId = 32, NameAr = "بإنتظار اعتماد تقييم العروض المالية" },
                        new { TenderStatusId = 33, NameAr = "تم إعتماد تقييم العروض المالية" },
                        new { TenderStatusId = 34, NameAr = "تم رفض تقييم العروض المالية" },
                        new { TenderStatusId = (int)Enums.TenderStatus.OffersOpenFinancialStage, NameAr = "مرحلة فتح العرض المالي" },
                        new { TenderStatusId = 35, NameAr = "تم إعتماد فحص العروض للشراء المباشر" },
                        new { TenderStatusId = 36, NameAr = "تم رفض فحص العروض للشراء المباشر" },
                        new { TenderStatusId = 38, NameAr = "بإنتظار اعتماد الترسية المبدئي " },
                        new { TenderStatusId = 39, NameAr = "تم اعتماد الترسية المبدئي " },
                        new { TenderStatusId = 40, NameAr = "تم رفض الترسية المبدئي " },
                        new { TenderStatusId = 47, NameAr = "معادة للجهة للتعديل " },
                        new { TenderStatusId = 48, NameAr = "مرحلة المزايدة المباشرة" },
                        new { TenderStatusId = 49, NameAr = "انتهاء المزايدة المباشرة" },
                        new { TenderStatusId = (int)Enums.TenderStatus.PendingVROAuditerApprove, NameAr = "بانتظار اعتماد مكتب تحقيق الرؤية" },
                        new { TenderStatusId = (int)Enums.TenderStatus.VROOffersTechnicalChecking, NameAr = "مرحلة فتح العروض الفنية" },
                        new { TenderStatusId = (int)Enums.TenderStatus.VROOffersTechnicalCheckingPending, NameAr = "بإنتظار اعتماد التقييم الفني" },
                        new { TenderStatusId = (int)Enums.TenderStatus.VROOffersTechnicalCheckingRejected, NameAr = "تم رفض التقييم الفني" },
                        new { TenderStatusId = (int)Enums.TenderStatus.VROOffersFinancialChecking, NameAr = "مرحلة فحص العروض والتقييم المالي" },
                        new { TenderStatusId = (int)Enums.TenderStatus.VROOffersFinancialCheckingPending, NameAr = "بانتظار إعتماد التقييم المالي" },
                        new { TenderStatusId = (int)Enums.TenderStatus.VROOffersFinancialCheckingApproved, NameAr = "تم إعتماد التقييم المالي" },
                        new { TenderStatusId = (int)Enums.TenderStatus.VROOffersFinancialCheckingRejected, NameAr = "تم رفض التقييم المالي" },
                        new { TenderStatusId = (int)Enums.TenderStatus.VROOffersTechnicalCheckingApproved, NameAr = "تم إعتماد التقييم الفني" },
                        new { TenderStatusId = (int)Enums.TenderStatus.VROFinancialCheckingOpening, NameAr = "مرحلة فتح العروض المالية" },
                        new { TenderStatusId = (int)Enums.TenderStatus.VROOffersCheckingAndTechnicalEval, NameAr = "مرحلة فحص العروض والتقييم الفني" },
                        new { TenderStatusId = (int)Enums.TenderStatus.QualificationUnderEstablishingFromCommittee, NameAr = "تحت الإنشاء" },
                        new { TenderStatusId = (int)Enums.TenderStatus.QualificationCommitteeApproval, NameAr = "بإنتظار موافقة اللجنة" },
                        new { TenderStatusId = (int)Enums.TenderStatus.PendingQualificationCommitteeManagerApproval, NameAr = "بإنتظار الأعتماد" },
                        new { TenderStatusId = (int)Enums.TenderStatus.RejectedQualificationApprovalByCommitteeManager, NameAr = "مرفوضة" },
                        new { TenderStatusId = (int)Enums.TenderStatus.OffersOpenFinancialStagePending, NameAr = "بانتظار اعتماد تقرير فتح العروض المالية" },
                        new { TenderStatusId = (int)Enums.TenderStatus.OffersOpenFinancialStageApproved, NameAr = "تم فتح العروض المالية" },
                        new { TenderStatusId = (int)Enums.TenderStatus.OffersOpenFinancialStageRejected, NameAr = "تم رفض تقرير فتح العروض المالية" },
                        new { TenderStatusId = (int)Enums.TenderStatus.OffersTechnicalOppening, NameAr = "مرحلة فتح العروض الفنية" },
                        new { TenderStatusId = (int)Enums.TenderStatus.OffersTechnicalOppeningPending, NameAr = "بإنتظار إعتماد تقرير فتح العروض الفنية" },
                        new { TenderStatusId = (int)Enums.TenderStatus.OffersTechnicalOppeningConfirmed, NameAr = "تم فتح العروض الفنية" },
                        new { TenderStatusId = (int)Enums.TenderStatus.OffersTechnicalOppeningRejected, NameAr = "تم رفض تقرير فتح العروض الفنية" },
                        new { TenderStatusId = (int)Enums.TenderStatus.OffersTechnicalChecking, NameAr = "مرحلة فحص العروض الفنية" },
                        new { TenderStatusId = (int)Enums.TenderStatus.BackForAwardingFromPlaint, NameAr = "معادة للترسية بسبب قبول طلب التظلم" }
                        );
        }

        private void InitializeTenderUntiStatus(ModelBuilder builder)
        {
            builder.Entity<TenderUnitStatus>().HasData(
                     new { TenderUnitStatusId = (int)Enums.TenderUnitStatus.WaitingUnitSecretaryReview, Name = "بإنتظار مراجعة مختص  مركز تحقيق كفاءة الإنفاق" },
                     new { TenderUnitStatusId = (int)Enums.TenderUnitStatus.UnderUnitReviewLevelOne, Name = "  تحت المراجعة المستوى الأول" },
                     new { TenderUnitStatusId = (int)Enums.TenderUnitStatus.ReturnedToAgencyForEdit, Name = " معادة للجهة الحكومية للتعديل" },
                     new { TenderUnitStatusId = (int)Enums.TenderUnitStatus.TenderTransferdToLevelTwo, Name = " تم تحويلها للمستوى الثاني" },
                     new { TenderUnitStatusId = (int)Enums.TenderUnitStatus.UnderUnitReviewLevelTwo, Name = " تحت المراجعة المستوى الثاني" },
                     new { TenderUnitStatusId = (int)Enums.TenderUnitStatus.UnderReviewing, Name = "تحت المراجعة" },
                     new { TenderUnitStatusId = (int)Enums.TenderUnitStatus.WaitingManagerApprove, Name = "بإنتظار إعتماد رئيس مركز تحقيق كفاءة الإنفاق" },
                     new { TenderUnitStatusId = (int)Enums.TenderUnitStatus.ApprovedByManager, Name = "تم قبول الإعتماد" },
                     new { TenderUnitStatusId = (int)Enums.TenderUnitStatus.RejectedByManager, Name = "تم رفض اعتماد القرار" },
                     new { TenderUnitStatusId = (int)Enums.TenderUnitStatus.UnderManagerReviewing, Name = "تحت مراجعة رئيس مركز تحقيق كفاءة الإنفاق" }
                     );
        }

        private void InitializeTenderType(ModelBuilder builder)
        {
            builder.Entity<TenderType>().HasData(
                new { TenderTypeId = 1, NameAr = " منافسة عامة (جديد)", BuyingCost = 500.0m, InvitationCost = 200.0m, CreatedAt = DateTime.Now },
                new { TenderTypeId = 2, NameAr = "شراء مباشر (جديد)", BuyingCost = 0.0m, InvitationCost = 200.0m, CreatedAt = DateTime.Now },
                new { TenderTypeId = 3, NameAr = "تأهيل مسبق", BuyingCost = 0.0m, InvitationCost = 0.0m, CreatedAt = DateTime.Now },
                new { TenderTypeId = 4, NameAr = "منافسة محدودة", BuyingCost = 0.0m, InvitationCost = 200.0m, CreatedAt = DateTime.Now },
                new { TenderTypeId = 5, NameAr = "المزايدة العكسية الإلكترونية", BuyingCost = 500.0m, InvitationCost = 200.0m, CreatedAt = DateTime.Now },
                new { TenderTypeId = 6, NameAr = "المنافسة على مرحلتين(المرحلة الاولى)", BuyingCost = 500.0m, InvitationCost = 200.0m, CreatedAt = DateTime.Now },
                new { TenderTypeId = 7, NameAr = "المنافسة على مرحلتين(المرحلة الثانية)", BuyingCost = 0.0m, InvitationCost = 0.0m, CreatedAt = DateTime.Now },
                new { TenderTypeId = 8, NameAr = "تأهيل لاحق", BuyingCost = 0.0m, InvitationCost = 0.0m, CreatedAt = DateTime.Now },
                new { TenderTypeId = 9, NameAr = "منافسة عامة (النظام القديم)", BuyingCost = 500.0m, InvitationCost = 200.0m, CreatedAt = DateTime.Now },
                new { TenderTypeId = 10, NameAr = "شراء مباشر (النظام القديم)", BuyingCost = 0.0m, InvitationCost = 200.0m, CreatedAt = DateTime.Now },
                new { TenderTypeId = 11, NameAr = "منافسة اتفاقية اطارية", BuyingCost = 500.0m, InvitationCost = 0.0m, CreatedAt = DateTime.Now },
                new { TenderTypeId = 12, NameAr = "مسابقة", BuyingCost = 200.0m, InvitationCost = 200.0m, CreatedAt = DateTime.Now },
                new { TenderTypeId = 13, NameAr = "مشاريع التحول الوطني", BuyingCost = 0.0m, InvitationCost = 0.0m, CreatedAt = DateTime.Now }
               );
        }

        private void InitializeTenderConditionTemplateStatus(ModelBuilder builder)
        {
            builder.Entity<TenderConditoinsStatus>().HasData(
                new { TenderConditoinsStatusId = 1, NameAr = "الأحكام العامة" },
                new { TenderConditoinsStatusId = 2, NameAr = "إعداد العروض" },
                new { TenderConditoinsStatusId = 3, NameAr = "تسليم العروض" },
                new { TenderConditoinsStatusId = 4, NameAr = "تقييم العروض" },
                new { TenderConditoinsStatusId = 5, NameAr = "متطلبات التعاقد" },
                new { TenderConditoinsStatusId = 6, NameAr = "التعريفات الفنية" },
                new { TenderConditoinsStatusId = 7, NameAr = "المواصفات والشروط المطلوبة" },
                new { TenderConditoinsStatusId = 8, NameAr = "المحتوى المحلي" }

               );
        }
        private void InitializeTenderConditionTemplateSections(ModelBuilder builder)
        {
            builder.Entity<ConditionsTemplateSection>().HasData(
                new { ConditionsTemplateSectionId = (int)Enums.ConditionsTemplateSections.WorkForce, NameAr = "القوى العاملة" },
                new { ConditionsTemplateSectionId = (int)Enums.ConditionsTemplateSections.Materials, NameAr = "المواد" },
                new { ConditionsTemplateSectionId = (int)Enums.ConditionsTemplateSections.Equipments, NameAr = "المعدات" },
                new { ConditionsTemplateSectionId = (int)Enums.ConditionsTemplateSections.MaterialsAdvanced, NameAr = "المواد متقدم" },
                new { ConditionsTemplateSectionId = (int)Enums.ConditionsTemplateSections.EquipmentAdvanced, NameAr = "المعدات متقدم" },
                new { ConditionsTemplateSectionId = (int)Enums.ConditionsTemplateSections.ImplementaionMethod, NameAr = "طريقة التنفيذ" },
                new { ConditionsTemplateSectionId = (int)Enums.ConditionsTemplateSections.SafteyDescription, NameAr = "تفاصيل الامات" },
                new { ConditionsTemplateSectionId = (int)Enums.ConditionsTemplateSections.QualityDescription, NameAr = "تفاصيل الجودة" },
                new { ConditionsTemplateSectionId = (int)Enums.ConditionsTemplateSections.ContractBasedOnPerformance, NameAr = "العقود بناءا على الاداء" },
                new { ConditionsTemplateSectionId = (int)Enums.ConditionsTemplateSections.TechnicalDeclerations, NameAr = "التعريفات الفنية" },
                new { ConditionsTemplateSectionId = (int)Enums.ConditionsTemplateSections.ProjectWorkScope, NameAr = "نطاق عمل المشروع" },
                new { ConditionsTemplateSectionId = (int)Enums.ConditionsTemplateSections.WorkProgram, NameAr = "برنامج العمل" },
                new { ConditionsTemplateSectionId = (int)Enums.ConditionsTemplateSections.WorkLocation, NameAr = " موقع العمل" },
                new { ConditionsTemplateSectionId = (int)Enums.ConditionsTemplateSections.Outputs, NameAr = "المخرجات" }
               );
        }

        private void InitializeLimitedTenderReasons(ModelBuilder builder)
        {
            builder.Entity<ReasonForLimitedTenderType>().HasData(
                new { Id = 1, Name = " اعمال مشتريات التي  لا تتوفر إلا لدى عدد محدود من المقاولين أو الموردين أو المتعهدين " },
                new { Id = 2, Name = " أعمال ومشتريات التي  تبلغ قيمتها التقديرية خمسمائة ألف ريال فأق" },
                new { Id = 3, Name = "الأعمال والمشتريات  المطلوبة عاجلا " },
                new { Id = 4, Name = "أعمال ومشتريات تقدم  من قبل الكيانات الغير ربحية " },
                new { Id = 5, Name = "أخرى )" }
               );
        }
        private void InitializeDirectPurchaseTenderReasons(ModelBuilder builder)
        {
            builder.Entity<ReasonForPurchaseTenderType>().HasData(
                new { Id = 1, Name = " اعمال ومشتريات الأسلحة والمعدات العسكرية وقطع غيارها " },
                new { Id = 2, Name = " الأعمال والمشتريات متوافرة لدى متعهد أو مقاول أو مورد واحد، ولم يكن لها بديل مقبول " },
                new { Id = 3, Name = " التكلفة التقديرية للأعمال والمشتريات لا تتجاوز مبلغ مائة ألف ريال  " },
                new { Id = 4, Name = " استخدام هذا  الأسلوب ضروريا لحماية مصالح الأمن الوطني  " },
                new { Id = 5, Name = " أعمال ومشتريات  متوفرة لدى كيان غير ربحي واحد " },
                new { Id = 6, Name = "حالة طارئة " },
                new { Id = 7, Name = "أخرى )" }
               );
        }
        private void InitializeOfferPrewsntationWay(ModelBuilder builder)
        {
            builder.Entity<OfferPresentationWay>().HasData(
                new { Id = 1, Name = "ملف واحد للعرض الفني والمالي معا  " },
                new { Id = 2, Name = "  ملفين منفصلين ( فني ومالي) " }

               );
        }
        private void InitializeBiddingRoundStatus(ModelBuilder builder)
        {
            builder.Entity<BiddingRoundStatus>().HasData(
                new { BiddingRoundStatusId = 1, Name = "بدأت الجولة" },
                new { BiddingRoundStatusId = 2, Name = "توقفت الجولة" },
                new { BiddingRoundStatusId = 3, Name = "تم اعتماد الجولة" },
                new { BiddingRoundStatusId = 4, Name = "جولة جديدة" }
               );
        }
        private void InitializeNegotiationStatus(ModelBuilder builder)
        {
            builder.Entity<NegotiationStatus>().HasData(
             new { NegotiationStatusId = (int)Enums.enNegotiationStatus.New, Name = " تحت التحديث " },
             new { NegotiationStatusId = (int)Enums.enNegotiationStatus.UnderUpdate, Name = " تحت التحديث " },
             new { NegotiationStatusId = (int)Enums.enNegotiationStatus.CheckManagerPendingApprove, Name = " بإنتظار اعتماد الطلب" },
             new { NegotiationStatusId = (int)Enums.enNegotiationStatus.CheckManagerReject, Name = "تم رفض الاعتماد " },
             new { NegotiationStatusId = (int)Enums.enNegotiationStatus.UnitSpecialestPendingApproved, Name = "بإنتظار اعتماد مركز تحقيق كفاءة الإنفاق " },
             // new { NegotiationStatusId = (int)Enums.enNegotiationStatus.UnitSpecialistApproved, Name = "معتمد من مختص الوحدة " },
             new { NegotiationStatusId = (int)Enums.enNegotiationStatus.UnitSpecialistReject, Name = "مرفوض من مختص مركز تحقيق كفاءة الإنفاق " },
             new { NegotiationStatusId = (int)Enums.enNegotiationStatus.SupplierNotAgreed, Name = "تم الرد (بالرفض)  " },
             new { NegotiationStatusId = (int)Enums.enNegotiationStatus.SupplierAgreed, Name = " تم الرد (بالموافقة) " },
             new { NegotiationStatusId = (int)Enums.enNegotiationStatus.SentToSuppliers, Name = " تم ارسال الطلب" },
             new { NegotiationStatusId = (int)Enums.enNegotiationStatus.SupplierAgreedWithExtraDiscount, Name = " تم الرد بالموافقة مع تخفيض إضافي " }
            );
        }
        private void InitializeSupplierExtendOffersValidityStatuses(ModelBuilder builder)
        {
            builder.Entity<SupplierExtendOffersValidityStatus>().HasData(
             new { SupplierExtendOffersValidityStatusId = (int)Enums.SupplierExtendOffersValidityStatus.Sent, Name = "تم إرسال الطلب" },
             new { SupplierExtendOffersValidityStatusId = (int)Enums.SupplierExtendOffersValidityStatus.UnderProcessing, Name = "تحت المراجعة" },
             new { SupplierExtendOffersValidityStatusId = (int)Enums.SupplierExtendOffersValidityStatus.Accepted, Name = "تم الرد (بالموافقة)" },
             new { SupplierExtendOffersValidityStatusId = (int)Enums.SupplierExtendOffersValidityStatus.Rejected, Name = "تم الرد (بالرفض)" },
             new { SupplierExtendOffersValidityStatusId = (int)Enums.SupplierExtendOffersValidityStatus.AcceptedInitially, Name = "تم الرد (بالموافقةالمبدئية)" }
            );
        }
        private void InitializeFirstStageNegotiationReasons(ModelBuilder builder)
        {
            builder.Entity<NegotiationReason>().HasData(
             new { NegotiationReasonId = (int)Enums.NegotiationFirstStageRejectionReasons.HighPriceThanBudget, Name = "ارتفاع اسعار العروض عن القيمة المعتمدة للمشروع" },
             new { NegotiationReasonId = (int)Enums.NegotiationFirstStageRejectionReasons.HighPriceThanMarket, Name = "ارتفاع اسعار العروض عن اسعار السوق بشكل ظاهر" }
            );
        }
        private void InitializeTenderUnitUpdateType(ModelBuilder builder)
        {
            builder.Entity<TenderUnitUpdateType>().HasData(
             new { TenderUnitUpdateTypeId = (int)Enums.TenderUnitUpdateType.MainInformation, Name = "المعلومات الأساسية" },
             new { TenderUnitUpdateTypeId = (int)Enums.TenderUnitUpdateType.Dates, Name = "التواريخ" },
             new { TenderUnitUpdateTypeId = (int)Enums.TenderUnitUpdateType.RelationStep, Name = "مجال التصنيف وموقع التنفيذ" },
             new { TenderUnitUpdateTypeId = (int)Enums.TenderUnitUpdateType.TenderActivities, Name = "نشاط المنافسة" },
             new { TenderUnitUpdateTypeId = (int)Enums.TenderUnitUpdateType.QuantityTables, Name = "جدول الكميات" },
             new { TenderUnitUpdateTypeId = (int)Enums.TenderUnitUpdateType.Attachments, Name = "كراسة الشروط والمواصفات" },
             new { TenderUnitUpdateTypeId = (int)Enums.TenderUnitUpdateType.LocalContent, Name = "متطلبات المحتوى المحلي" }

            );
        }
        private void InitializeAgreementType(ModelBuilder builder)
        {
            builder.Entity<AgreementType>().HasData(
             new { AgreementTypeId = (int)Enums.AgreementType.Closed, NameAr = "مغلقة" },
             new { AgreementTypeId = (int)Enums.AgreementType.Opened, NameAr = "مفتوحة" }
            );
        }


        #region QualificationSeed

        private void InitializeQualificationItemCategory(ModelBuilder builder)
        {
            builder.Entity<QualificationItemCategory>().HasData(
             new { ID = (int)Enums.QualificationItemCategory.Technical, Name = "القدرات الفنية والإدارية" },
             new { ID = (int)Enums.QualificationItemCategory.Financial, Name = "القدرات المالية" }
            );
        }

        private void InitializeQualificationItemType(ModelBuilder builder)
        {
            builder.Entity<QualificationItemType>().HasData(
             new { ID = (int)Enums.QualificationItemType.Percentage, Name = "Percentage" },
             new { ID = (int)Enums.QualificationItemType.Range, Name = "Range" },
             new { ID = (int)Enums.QualificationItemType.Select, Name = "Select" },
             new { ID = (int)Enums.QualificationItemType.Value, Name = "Value" }
            );
        }

        private void InitializeQualificationSubCategory(ModelBuilder builder)
        {
            builder.Entity<QualificationSubCategory>().HasData(
             new { ID = (int)Enums.QualificationSubCategory.PreviousExperienceYear, QualificationCategoryId = (int)Enums.QualificationItemCategory.Technical, Name = "الخبرات السابقة", IsConfigure = true },
             new { ID = (int)Enums.QualificationSubCategory.ExistingContractualObligations, QualificationCategoryId = (int)Enums.QualificationItemCategory.Technical, Name = "الالتزامات التعاقدية القائمة", IsConfigure = true },
             new { ID = (int)Enums.QualificationSubCategory.HumanResource, QualificationCategoryId = (int)Enums.QualificationItemCategory.Technical, Name = "الموارد البشرية", IsConfigure = true },
             new { ID = (int)Enums.QualificationSubCategory.Quality, Name = "الجودة", QualificationCategoryId = (int)Enums.QualificationItemCategory.Technical, IsConfigure = true },
             new { ID = (int)Enums.QualificationSubCategory.EnviromentAndHealthy, Name = "البيئة والصحة والسلامة", QualificationCategoryId = (int)Enums.QualificationItemCategory.Technical, IsConfigure = true },
             new { ID = (int)Enums.QualificationSubCategory.Insurance, Name = "التأمين", QualificationCategoryId = (int)Enums.QualificationItemCategory.Technical, IsConfigure = true },
             new { ID = (int)Enums.QualificationSubCategory.FinancialStatements, Name = "البيانات الماليه", QualificationCategoryId = (int)Enums.QualificationItemCategory.Financial, IsConfigure = true },
             new { ID = (int)Enums.QualificationSubCategory.FinancialPerformanceIndicators, Name = "بيان الميزانية العمومية", QualificationCategoryId = (int)Enums.QualificationItemCategory.Financial, IsConfigure = false }
            );
        }

        private void InitializeQualificationEvaluationItems(ModelBuilder builder)
        {
            builder.Entity<QualificationItem>().HasData(
             new { Code = 1, SubCategoryId = (int)Enums.QualificationSubCategory.PreviousExperienceYear, QualificationItemTypeId = (int)Enums.QualificationItemType.Range, IsDeleted = false, ID = (int)Enums.QualificationEvaluationItems.NumberOfYearsOfExperience, Name = "عدد سنوات الخبرة في مجال طلب التأهيل", IsConfigure = true },
             new { Code = 2, SubCategoryId = (int)Enums.QualificationSubCategory.PreviousExperienceYear, QualificationItemTypeId = (int)Enums.QualificationItemType.Range, IsDeleted = false, ID = (int)Enums.QualificationEvaluationItems.NumberOfProjectsImplementedLastThreeYears, Name = "عدد المشاريع المنفذة خلال الثلاث سنوات الأخيرة في مجال طلب التأهيل", IsConfigure = true },
             new { Code = 3, SubCategoryId = (int)Enums.QualificationSubCategory.PreviousExperienceYear, QualificationItemTypeId = (int)Enums.QualificationItemType.Range, IsDeleted = false, ID = (int)Enums.QualificationEvaluationItems.TotalValueProjectsLastThreeYears, Name = "إجمالي قيمة المشاريع خلال الثلاث سنوات الأخيرة في مجال طلب التأهيل", IsConfigure = true },
             new { Code = 4, SubCategoryId = (int)Enums.QualificationSubCategory.Quality, QualificationItemTypeId = (int)Enums.QualificationItemType.Select, IsDeleted = false, ID = (int)Enums.QualificationEvaluationItems.QualityAssuranceStandards, Name = "ما هي معايير ضمان الجودة", IsConfigure = true },
             new { Code = 5, SubCategoryId = (int)Enums.QualificationSubCategory.EnviromentAndHealthy, QualificationItemTypeId = (int)Enums.QualificationItemType.Select, IsDeleted = false, ID = (int)Enums.QualificationEvaluationItems.EnvironmentalHealthSafetyStandards, Name = "ما هي معايير ضمان البيئة والصحة والسلامة", IsConfigure = true },
             new { Code = 6, SubCategoryId = (int)Enums.QualificationSubCategory.ExistingContractualObligations, QualificationItemTypeId = (int)Enums.QualificationItemType.Range, IsDeleted = false, ID = (int)Enums.QualificationEvaluationItems.NumberOfExistingProjects, Name = "عدد المشاريع القائمة", IsConfigure = true },
             new { Code = 7, SubCategoryId = (int)Enums.QualificationSubCategory.ExistingContractualObligations, QualificationItemTypeId = (int)Enums.QualificationItemType.Range, IsDeleted = false, ID = (int)Enums.QualificationEvaluationItems.ValueOfExistingProjects, Name = "قيمة المشاريع القائمة", IsConfigure = true },
             new { Code = 8, SubCategoryId = (int)Enums.QualificationSubCategory.HumanResource, QualificationItemTypeId = (int)Enums.QualificationItemType.Range, IsDeleted = false, ID = (int)Enums.QualificationEvaluationItems.NumberOfEmployees, Name = "عدد الموظفين", IsConfigure = true },
             new { Code = 9, SubCategoryId = (int)Enums.QualificationSubCategory.HumanResource, QualificationItemTypeId = (int)Enums.QualificationItemType.Range, IsDeleted = false, ID = (int)Enums.QualificationEvaluationItems.NumberOfSaudiEmployees, Name = "عدد الموظفين السعوديين", IsConfigure = false },
             new { Code = 10, SubCategoryId = (int)Enums.QualificationSubCategory.HumanResource, QualificationItemTypeId = (int)Enums.QualificationItemType.Percentage, IsDeleted = false, ID = (int)Enums.QualificationEvaluationItems.PercentageOfSaudiEmployees, Name = "نسبة الموظفين السعوديين", IsConfigure = true },
             new { Code = 11, SubCategoryId = (int)Enums.QualificationSubCategory.Insurance, QualificationItemTypeId = (int)Enums.QualificationItemType.Select, IsDeleted = true, ID = (int)Enums.QualificationEvaluationItems.InsuranceOfProfessionalCompensation, Name = "تأمين التعويض المهني", IsConfigure = true },
             new { Code = 12, SubCategoryId = (int)Enums.QualificationSubCategory.Insurance, QualificationItemTypeId = (int)Enums.QualificationItemType.Select, IsDeleted = true, ID = (int)Enums.QualificationEvaluationItems.LiabilityInsurance, Name = "تأمين المسؤولية ضد الغير", IsConfigure = true },
             new { Code = 13, SubCategoryId = (int)Enums.QualificationSubCategory.Insurance, QualificationItemTypeId = (int)Enums.QualificationItemType.Select, IsDeleted = true, ID = (int)Enums.QualificationEvaluationItems.InsuranceOfGeneralCommercialResponsibility, Name = "تأمين المسؤولية التجارية العامة", IsConfigure = true },
             new { Code = 11, SubCategoryId = (int)Enums.QualificationSubCategory.Insurance, QualificationItemTypeId = (int)Enums.QualificationItemType.Select, IsDeleted = false, ID = (int)Enums.QualificationEvaluationItems.Insurance, Name = "يرجى تأكيد القدرة على تقديم التأمين اللازم", IsConfigure = true },
             new { Code = 15, SubCategoryId = (int)Enums.QualificationSubCategory.FinancialStatements, QualificationItemTypeId = (int)Enums.QualificationItemType.Value, IsDeleted = false, ID = (int)Enums.QualificationEvaluationItems.CashEquivalents, Name = "النقدية ومكافئات النقدية", IsConfigure = true },
             new { Code = 16, SubCategoryId = (int)Enums.QualificationSubCategory.FinancialStatements, QualificationItemTypeId = (int)Enums.QualificationItemType.Value, IsDeleted = false, ID = (int)Enums.QualificationEvaluationItems.AccountsReceivable, Name = "الحسابات مستحقة القبض", IsConfigure = true },
             new { Code = 18, SubCategoryId = (int)Enums.QualificationSubCategory.FinancialStatements, QualificationItemTypeId = (int)Enums.QualificationItemType.Value, IsDeleted = false, ID = (int)Enums.QualificationEvaluationItems.CurrentLiabilities, Name = "الالتزامات المتداولة", IsConfigure = true },
             new { Code = 0, SubCategoryId = (int)Enums.QualificationSubCategory.FinancialStatements, QualificationItemTypeId = (int)Enums.QualificationItemType.Range, IsDeleted = false, ID = (int)Enums.QualificationEvaluationItems.CashRate, Name = "نسبة النقدية (النقدية ومكافئات النقدية \\ الالتزامات المتداولة)", IsConfigure = true },
             new { Code = 0, SubCategoryId = (int)Enums.QualificationSubCategory.FinancialStatements, QualificationItemTypeId = (int)Enums.QualificationItemType.Range, IsDeleted = false, ID = (int)Enums.QualificationEvaluationItems.LiquidityRatio, Name = "نسبة السيولة السريعة ((النقدية ومكافئات النقدية+الحسابات المستحقة القبض)/الالتزامات المتداولة)", IsConfigure = true },
             new { Code = 0, SubCategoryId = (int)Enums.QualificationSubCategory.FinancialStatements, QualificationItemTypeId = (int)Enums.QualificationItemType.Range, IsDeleted = false, ID = (int)Enums.QualificationEvaluationItems.TradingRatio, Name = "نسبة التداول (الأصول المتداولة \\ الالتزامات المتداولة)", IsConfigure = true },
             new { Code = 0, SubCategoryId = (int)Enums.QualificationSubCategory.FinancialStatements, QualificationItemTypeId = (int)Enums.QualificationItemType.Select, IsDeleted = false, ID = (int)Enums.QualificationEvaluationItems.ConfirmAbilityToSubmitLastThreeyearsAuditedFinancialStatements, Name = "يرجى تأكيد القدرة على تقديم آخر 3 سنوات من البيانات المالية المدققة، إذا طلب من الجهة", IsConfigure = true },
             new { Code = 14, SubCategoryId = (int)Enums.QualificationSubCategory.FinancialStatements, QualificationItemTypeId = (int)Enums.QualificationItemType.Value, IsDeleted = false, ID = (int)Enums.QualificationEvaluationItems.CurrentAssets, Name = "   الأصول المتداولة", IsConfigure = true },
             new { Code = 17, SubCategoryId = (int)Enums.QualificationSubCategory.FinancialStatements, QualificationItemTypeId = (int)Enums.QualificationItemType.Value, IsDeleted = false, ID = (int)Enums.QualificationEvaluationItems.TotalAssets, Name = "مجموع الموجودات", IsConfigure = true },
             new { Code = 19, SubCategoryId = (int)Enums.QualificationSubCategory.FinancialStatements, QualificationItemTypeId = (int)Enums.QualificationItemType.Value, IsDeleted = false, ID = (int)Enums.QualificationEvaluationItems.TotalLiabilities, Name = "مجموع المطلوبات", IsConfigure = true },
             new { Code = 20, SubCategoryId = (int)Enums.QualificationSubCategory.FinancialStatements, QualificationItemTypeId = (int)Enums.QualificationItemType.Value, IsDeleted = false, ID = (int)Enums.QualificationEvaluationItems.TotalRevenue, Name = "مجموع الإيرادات", IsConfigure = true },
             new { Code = 21, SubCategoryId = (int)Enums.QualificationSubCategory.FinancialStatements, QualificationItemTypeId = (int)Enums.QualificationItemType.Value, IsDeleted = false, ID = (int)Enums.QualificationEvaluationItems.NetProfit, Name = "صافي الأرباح", IsConfigure = true },
             new { Code = 0, SubCategoryId = (int)Enums.QualificationSubCategory.FinancialStatements, QualificationItemTypeId = (int)Enums.QualificationItemType.Range, IsDeleted = false, ID = (int)Enums.QualificationEvaluationItems.RatioOfObligations, Name = "نسبة الالتزامات (مجموع المطلوبات\\مجموع الموجودات)", IsConfigure = true },
             new { Code = 0, SubCategoryId = (int)Enums.QualificationSubCategory.FinancialStatements, QualificationItemTypeId = (int)Enums.QualificationItemType.Range, IsDeleted = false, ID = (int)Enums.QualificationEvaluationItems.RateOfProfitability, Name = "معدل التغيير التراكمي لمعدل الربحية", IsConfigure = true }
            );
        }

        private void InitializeQualificationLookupNames(ModelBuilder builder)
        {
            builder.Entity<QualificationLookupsName>().HasData(
             new { ID = (int)Enums.QualificationLookupNames.AcceptAndRejectQuestion, Name = "ضمان الجوده" },
             new { ID = (int)Enums.QualificationLookupNames.AvaiableQuestion, Name = "معاير ضمان البيئه والصحه" },
             new { ID = (int)Enums.QualificationLookupNames.EnvironmentalAndHealthInsurance, Name = "سؤال يوجد // لا يوجد" },
             new { ID = (int)Enums.QualificationLookupNames.QualityGuarantie, Name = "سؤال نعم // لا" },
             new { ID = (int)Enums.QualificationLookupNames.RateOfProfitability, Name = "اتجاه معدل الربحية" },
             new { ID = (int)Enums.QualificationLookupNames.ResultOfRehabilitation, Name = "نتيجه التاهيل" }
            );
        }

        private void InitializeQuantityTableRowType(ModelBuilder builder)
        {
            builder.Entity<QuantityTableRowType>().HasData(
             new { QuantityTableRowTypeId = (int)Enums.QuantityTableRowType.Available, NameAr = "متوفر", NameEn = "Available" },
             new { QuantityTableRowTypeId = (int)Enums.QuantityTableRowType.NotAvailable, NameAr = "غير متوفر", NameEn = "Not Available" },
             new { QuantityTableRowTypeId = (int)Enums.QuantityTableRowType.Free, NameAr = "مجانى", NameEn = "Free" }
            );
        }

        private void InitializeQualificationQualityGuarantee(ModelBuilder builder)
        {
            builder.Entity<QualificationLookup>().HasData(
             new { ID = (int)Enums.QualificationQualityGuaranteeLookup.IsoCertificate, Name = "شهادة ايزو", QualificationLookupId = (int)Enums.QualificationLookupNames.QualityGuarantie },
             new { ID = (int)Enums.QualificationQualityGuaranteeLookup.QualityGuarantee, Name = "دليل جودة", QualificationLookupId = (int)Enums.QualificationLookupNames.QualityGuarantie },
             new { ID = (int)Enums.QualificationQualityGuaranteeLookup.GuaranteeNotAvailible, Name = "لا يوجد", QualificationLookupId = (int)Enums.QualificationLookupNames.QualityGuarantie }
            );
        }

        private void InitializeQualificationEnvironmentStandardsLookup(ModelBuilder builder)
        {
            builder.Entity<QualificationLookup>().HasData(
             new { ID = (int)Enums.QualificationEnvironmentStandardsLookup.OSHA, Name = "شهادة OSHA أو OHSAS", QualificationLookupId = (int)Enums.QualificationLookupNames.EnvironmentalAndHealthInsurance },
             new { ID = (int)Enums.QualificationEnvironmentStandardsLookup.EnvironmentGuide, Name = "دليل البيئة والصحة والسلامة", QualificationLookupId = (int)Enums.QualificationLookupNames.EnvironmentalAndHealthInsurance },
             new { ID = (int)Enums.QualificationEnvironmentStandardsLookup.EnvironmentGuideNotAvailible, Name = "لا يوجد", QualificationLookupId = (int)Enums.QualificationLookupNames.EnvironmentalAndHealthInsurance }
            );
        }

        private void InitializeQualificationAvailibleNotAvailibleLookup(ModelBuilder builder)
        {
            builder.Entity<QualificationLookup>().HasData(
             new { ID = (int)Enums.QualificationAvailibleNotAvailibleLookup.Availible, Name = "يوجد", QualificationLookupId = (int)Enums.QualificationLookupNames.AvaiableQuestion },
             new { ID = (int)Enums.QualificationAvailibleNotAvailibleLookup.NotAvailible, Name = "لا يوجد", QualificationLookupId = (int)Enums.QualificationLookupNames.AvaiableQuestion }
            );
        }

        private void InitializeQualificationYesOrNoLookup(ModelBuilder builder)
        {
            builder.Entity<QualificationLookup>().HasData(
             new { ID = (int)Enums.QualificationYesOrNoLookup.Yes, Name = "نعم", QualificationLookupId = (int)Enums.QualificationLookupNames.AcceptAndRejectQuestion },
             new { ID = (int)Enums.QualificationYesOrNoLookup.No, Name = "لا", QualificationLookupId = (int)Enums.QualificationLookupNames.AcceptAndRejectQuestion }
            );
        }

        private void InitializeQualificationProfitDirAverageLookup(ModelBuilder builder)
        {
            builder.Entity<QualificationLookup>().HasData(
             new { ID = (int)Enums.QualificationProfitDirAverageLookup.High, Name = "مرتفع", QualificationLookupId = (int)Enums.QualificationLookupNames.RateOfProfitability },
             new { ID = (int)Enums.QualificationProfitDirAverageLookup.Low, Name = "منخفض", QualificationLookupId = (int)Enums.QualificationLookupNames.RateOfProfitability },
             new { ID = (int)Enums.QualificationProfitDirAverageLookup.Stable, Name = "ثابت", QualificationLookupId = (int)Enums.QualificationLookupNames.RateOfProfitability }
            );
        }

        private void InitializeQualificationResultLookup(ModelBuilder builder)
        {
            builder.Entity<QualificationLookup>().HasData(
             new { ID = (int)Enums.QualificationResultLookup.Succeeded, Name = " ناجح", QualificationLookupId = (int)Enums.QualificationLookupNames.ResultOfRehabilitation },
             new { ID = (int)Enums.QualificationResultLookup.Failed, Name = "لم يجتاز الاختبار", QualificationLookupId = (int)Enums.QualificationLookupNames.ResultOfRehabilitation }
            );
        }

        private void InitializePreQualificationType(ModelBuilder builder)
        {
            builder.Entity<QualificationType>().HasData(
             new { ID = (int)Enums.PreQualificationType.Small, Name = "صغير" },
             new { ID = (int)Enums.PreQualificationType.Medium, Name = "وسط" },
             new { ID = (int)Enums.PreQualificationType.Large, Name = "كبير" }
            );
        }

        private void InitializeQualificationYear(ModelBuilder builder)
        {
            builder.Entity<QualificationYear>().HasData(
             new { ID = (int)Enums.QualificationYear.CurrentYear, Name = "0" },
             new { ID = (int)Enums.QualificationYear.SecondYear, Name = "-1" },
             new { ID = (int)Enums.QualificationYear.ThirdYear, Name = "-2" }
            );
        }

        private void InitializeQualificationTypeCategory(ModelBuilder builder)
        {
            builder.Entity<QualificationTypeCategory>().HasData(
             new { ID = 1, QualificationTypeId = 1, QualificationSubCategoryId = 1 },
             new { ID = 2, QualificationTypeId = 1, QualificationSubCategoryId = 2 },
             new { ID = 3, QualificationTypeId = 1, QualificationSubCategoryId = 3 },
             new { ID = 6, QualificationTypeId = 2, QualificationSubCategoryId = 1 },
             new { ID = 7, QualificationTypeId = 2, QualificationSubCategoryId = 2 },
             new { ID = 8, QualificationTypeId = 2, QualificationSubCategoryId = 3 },
             new { ID = 9, QualificationTypeId = 2, QualificationSubCategoryId = 4 },
             new { ID = 10, QualificationTypeId = 2, QualificationSubCategoryId = 5 },
             new { ID = 13, QualificationTypeId = 3, QualificationSubCategoryId = 1 },
             new { ID = 14, QualificationTypeId = 3, QualificationSubCategoryId = 4 },
             new { ID = 15, QualificationTypeId = 3, QualificationSubCategoryId = 5 },
             new { ID = 16, QualificationTypeId = 3, QualificationSubCategoryId = 2 },
             new { ID = 17, QualificationTypeId = 3, QualificationSubCategoryId = 3 },
             new { ID = 18, QualificationTypeId = 3, QualificationSubCategoryId = 6 },
             new { ID = 19, QualificationTypeId = 3, QualificationSubCategoryId = 7 },
             new { ID = 23, QualificationTypeId = 1, QualificationSubCategoryId = 7 },
             new { ID = 24, QualificationTypeId = 2, QualificationSubCategoryId = 7 }
            );
        }



        #endregion


        #endregion

        #endregion

        #region [Communication Request Seeds]

        private void InitializeCommunicationRequestTypes(ModelBuilder builder)
        {
            builder.Entity<AgencyCommunicationRequestType>().HasData(
                new { Id = (int)Enums.AgencyCommunicationRequestType.Plaint, Name = "تظلم" },
                new { Id = (int)Enums.AgencyCommunicationRequestType.Negotiation, Name = "تفاوض" },
                new { Id = (int)Enums.AgencyCommunicationRequestType.SupplierOfferExtendDates, Name = "طلب تأجيل تقديم العروض" },
                new { Id = (int)Enums.AgencyCommunicationRequestType.ExtendOfferValidtiy, Name = "طلب تمديد سريان العروض" },
                new { Id = (int)Enums.AgencyCommunicationRequestType.Enquiry, Name = "إستفسار" });
        }

        private void InitializeVendorCertificates(ModelBuilder builder)
        {
            builder.Entity<VendorCertificates>().HasData(
               new { VendorCertificateId = 1, NameAr = "السجل التجاري", NameEn = "Commercial Registery" },
               new { VendorCertificateId = 2, NameAr = "تصنيف المقاولين", NameEn = "contractor Classification" },
               new { VendorCertificateId = 3, NameAr = "شهادة الزكاة", NameEn = "Zakat Certificate" },
               new { VendorCertificateId = 4, NameAr = "شهادة الضريبة", NameEn = "taxes Certificate" },
               new { VendorCertificateId = 5, NameAr = "التأمينات الإجتماعية", NameEn = "Social Insurance" },
               new { VendorCertificateId = 6, NameAr = "شهادة اشتراك الغرفة التجارية", NameEn = "Commercial Chamber Participation" },
               new { VendorCertificateId = 7, NameAr = "رخصة الإستثمار", NameEn = "Investigation licence" },
               new { VendorCertificateId = 8, NameAr = "شهادة السعودة", NameEn = "Saudization Certificate" },
               new { VendorCertificateId = 9, NameAr = "رخصة البلدية", NameEn = "Muncipality Registery" });
        }


        private void InitializeSpendingCategories(ModelBuilder builder)
        {
            builder.Entity<SpendingCategory>().HasData(
                new { SpendingCategoryId = 1, NameAr = "إنشاء المبانى", NameEn = "إنشاء المبانى" },
                new { SpendingCategoryId = 2, NameAr = "إنشاء الطرق", NameEn = "إنشاء الطرق" },
                new { SpendingCategoryId = 3, NameAr = "الخدمات الهندسية(تصميم/إشراف)", NameEn = "الخدمات الهندسية(تصميم/إشراف)" },
                new { SpendingCategoryId = 4, NameAr = "الصيانة والتشغيل", NameEn = "الصيانة والتشغيل" },
                new { SpendingCategoryId = 5, NameAr = "صيانة الطرق", NameEn = "صيانة الطرق" },
                new { SpendingCategoryId = 6, NameAr = "الإستئجار", NameEn = "الإستئجار" },
                new { SpendingCategoryId = 7, NameAr = "الصيانة الطبية", NameEn = "الصيانة الطبية" },
                new { SpendingCategoryId = 8, NameAr = "نظافة المدن", NameEn = "نظافة المدن" },
                new { SpendingCategoryId = 9, NameAr = "التغذية", NameEn = "التغذية" },
                new { SpendingCategoryId = 10, NameAr = "المستلزمات الطبية", NameEn = "المستلزمات الطبية" },
                new { SpendingCategoryId = 11, NameAr = "الأدوية", NameEn = "الأدوية" },
                new { SpendingCategoryId = 12, NameAr = "الخدمات الإستشارية", NameEn = "الخدمات الإستشارية" },
                new { SpendingCategoryId = 13, NameAr = "تقنية المعلومات", NameEn = "تقنية المعلومات" },
                new { SpendingCategoryId = 14, NameAr = "توريد", NameEn = "توريد" }
            );
        }
        private void InitializePlaintStatus(ModelBuilder builder)
        {
            builder.Entity<AgencyCommunicationPlaintStatus>().HasData(
                new { Id = (int)Enums.AgencyPlaintStatus.New, Name = "تم الإرسال" },
                new { Id = (int)Enums.AgencyPlaintStatus.Pending, Name = "بانتظار الاعتماد" },
                new { Id = (int)Enums.AgencyPlaintStatus.Accepted, Name = "مقبول" },
                new { Id = (int)Enums.AgencyPlaintStatus.Rejected, Name = "مرفوض" }
                );
        }
        private void InitializeRequestsRejectionTypes(ModelBuilder builder)
        {
            builder.Entity<RequestsRejectionType>().HasData(
                new { RequestTypeId = (int)Enums.RequestRejectionType.Plaint, NameAr = "تظلم", NameEn = "Plaint" },
                new { RequestTypeId = (int)Enums.RequestRejectionType.Escalation, NameAr = "تصعيد تظلم", NameEn = "Plaint Escalation" }
                );
        }
        private void InitializeVerificationType(ModelBuilder builder)
        {
            builder.Entity<VerificationType>().HasData(
                new { VerificationTypeId = (int)Enums.VerificationType.Tender, VerificationTypeName = "منافسه" },
                new { VerificationTypeId = (int)Enums.VerificationType.Block, VerificationTypeName = "منع" },
                new { VerificationTypeId = (int)Enums.VerificationType.PrePlanning, VerificationTypeName = "قبل التخطيط" },
                new { VerificationTypeId = (int)Enums.VerificationType.AgencyCommunication, VerificationTypeName = "تواصل الجهه" },
                new { VerificationTypeId = (int)Enums.VerificationType.Negotiation, VerificationTypeName = "التفاوض" },
                new { VerificationTypeId = (int)Enums.VerificationType.MandatoryList, VerificationTypeName = "القائمة الالزامية" },
                new { VerificationTypeId = (int)Enums.VerificationType.Announcement, VerificationTypeName = "الإعلان" }
              );
        }
        private void InitializePlaintProcedures(ModelBuilder builder)
        {
            builder.Entity<TenderPlaintRequestProcedure>().HasData(
                new { Id = (int)Enums.TenderPlaintRequestProcedure.ReOpenTenderChecking, Name = "إعادة فحص العروض" },
                new { Id = (int)Enums.TenderPlaintRequestProcedure.ReOpenTenderAwarding, Name = "إعادة فتح الترسية" },
                new { Id = (int)Enums.TenderPlaintRequestProcedure.Other, Name = "أخرى" }
                );
        }

        private void InitializeCancelationReasons(ModelBuilder builder)
        {
            builder.Entity<CancelationReason>().HasData(
                new { CancelationReasonId = 1, NameAr = "وجود أخطاء جوهرية في وثائق المنافسة", NameEn = "وجود أخطاء جوهرية في وثائق المنافسة" },
                new { CancelationReasonId = 2, NameAr = "مخالفة إجراءات المنافسة لنظام المنافسات والمشتريات الحكومية", NameEn = "مخالفة إجراءات المنافسة لنظام المنافسات والمشتريات الحكومية" },
                new { CancelationReasonId = 3, NameAr = "تحقيق المصلحة العامة", NameEn = "تحقيق المصلحة العامة" },
                new { CancelationReasonId = 4, NameAr = "ارتكاب أي من المتنافسين مخالفات", NameEn = "ارتكاب أي من المتنافسين مخالفات" },
                new { CancelationReasonId = 5, NameAr = "ارتفاع أسعار العروض عن المبالغ المعتمدة", NameEn = "ارتفاع أسعار العروض عن المبالغ المعتمدة" }


                 );
        }

        #endregion

        private void InitializeAnnouncementStatus(ModelBuilder builder)
        {
            builder.Entity<AnnouncementStatus>().HasData(
                new { Id = (int)Enums.AnnouncementStatus.UnderCreation, Name = "تحت الإنشاء" },
                new { Id = (int)Enums.AnnouncementStatus.Pending, Name = "بإنتظار الإعتماد" },
                new { Id = (int)Enums.AnnouncementStatus.Approved, Name = "معتمد" },
                new { Id = (int)Enums.AnnouncementStatus.Rejected, Name = "مرفوض" },
                new { Id = (int)Enums.AnnouncementStatus.Ended, Name = "منهي" }
                );
        }

        private void InitializeAnnouncementSupplierTemplateStatus(ModelBuilder builder)
        {
            builder.Entity<AnnouncementStatusSupplierTemplate>().HasData(
                new { Id = (int)Enums.AnnouncementSupplierTemplateStatus.UnderCreation, Name = "تحت الإنشاء" },
                new { Id = (int)Enums.AnnouncementSupplierTemplateStatus.Pending, Name = "بإنتظار الإعتماد" },
                new { Id = (int)Enums.AnnouncementSupplierTemplateStatus.Approved, Name = "معتمد" },
                new { Id = (int)Enums.AnnouncementSupplierTemplateStatus.Rejected, Name = "مرفوض" },
                new { Id = (int)Enums.AnnouncementSupplierTemplateStatus.Ended, Name = "منهي" },
                new { Id = (int)Enums.AnnouncementSupplierTemplateStatus.ReadyForApproval, Name = "بانتظار الاعتماد" },
                new { Id = (int)Enums.AnnouncementSupplierTemplateStatus.Canceled, Name = "ملغي" }

                );
        }
        private void InitializeAnnouncementJoinRequestStatus(ModelBuilder builder)
        {
            builder.Entity<AnnouncementJoinRequestStatus>().HasData(
                new { Id = (int)Enums.AnnouncementJoinRequestStatus.Sent, NameEn = "Request Sent", NameAr = " تم ارسال طلب الانضمام" },
                new { Id = (int)Enums.AnnouncementJoinRequestStatus.WithDraw, NameEn = "Request WithDrawn", NameAr = "تم سحب الطلب" }

                );
        }
        private void InitializeAnnouncementSupplierTemplateJoinRequestStatus(ModelBuilder builder)
        {
            builder.Entity<AnnouncementJoinRequestStatusSupplierTemplate>().HasData(
                new { Id = (int)Enums.AnnouncementTemplateJoinRequestStatus.Sent, NameEn = "Request Sent", NameAr = "تم الارسال" },
                new { Id = (int)Enums.AnnouncementTemplateJoinRequestStatus.PendingAcceptance, NameEn = "Pending Acceptance", NameAr = "بانتظار اعنماد القبول" },
                new { Id = (int)Enums.AnnouncementTemplateJoinRequestStatus.PendingRejection, NameEn = "Pending Rejection", NameAr = "بانتظار اعتماد الرفض" },
                new { Id = (int)Enums.AnnouncementTemplateJoinRequestStatus.Accepted, NameEn = "Accepted", NameAr = "تم القبول" },
                new { Id = (int)Enums.AnnouncementTemplateJoinRequestStatus.Rejected, NameEn = "Rejected", NameAr = "تم الرفض" },
                new { Id = (int)Enums.AnnouncementTemplateJoinRequestStatus.Withdrawn, NameEn = "Withdrawn", NameAr = "تم الانسحاب" },
                new { Id = (int)Enums.AnnouncementTemplateJoinRequestStatus.Deleted, NameEn = "Deleted", NameAr = "تم الحذف" }

                );
        }
    }
}
