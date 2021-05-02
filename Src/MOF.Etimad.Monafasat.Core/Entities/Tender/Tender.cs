using MOF.Etimad.Monafasat.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("Tender", Schema = "Tender")]

    public partial class Tender : AuditableEntity
    {
        #region Fields====================================================
        [Key]
        public int TenderId { get; private set; }
        //[Required]
        //public int test111111 { get; set; }
        public int TenderTypeId { get; private set; }
        [ForeignKey("TenderTypeId")]
        public TenderType TenderType { get; private set; } //= new TenderType();
        public int? InvitationTypeId { get; private set; } //In Case Of Direct purchase
        //[Required]
        [ForeignKey("InvitationTypeId")]
        public InvitationType InvitationType { get; private set; } //= new InvitationType();
        public string InitiatorName { get; private set; }
        //[Required]

        public int? TechnicalOrganizationId { get; private set; }
        [ForeignKey("TechnicalOrganizationId")]
        public Committee TechnicalOrganization { get; private set; } //= new Committee();

        public int? OffersOpeningCommitteeId { get; private set; }
        [ForeignKey("OffersOpeningCommitteeId")]
        public Committee OffersOpeningCommittee { get; private set; } //= new Committee();


        public int? OffersCheckingCommitteeId { get; private set; }
        [ForeignKey("OffersCheckingCommitteeId")]
        public Committee OffersCheckingCommittee { get; private set; } //= new Committee();

        //[Required]
        [StringLength(500)]
        public string TenderName { get; private set; }
        //[Required]
        [StringLength(100)]
        public string TenderNumber { get; private set; }
        [StringLength(1024)]
        public string Purpose { get; private set; }
        public Decimal? ConditionsBookletPrice { get; private set; }
        public Decimal? EstimatedValue { get; private set; }
        //[Required]
        public int? OfferPresentationWayId { get; private set; }
        [ForeignKey("OfferPresentationWayId")]
        public TenderApplyType ApplyType { get; private set; }
        [ForeignKey("OfferPresentationPlaceId")]
        public int? OfferPresentationPlaceId { get; private set; }
        public Address OfferPresentationPlace { get; private set; }
        //[Required]
        [ForeignKey("OffersOpeningAddressId")]
        public int? OffersOpeningAddressId { get; private set; }
        public Address OffersOpeningAddress { get; private set; }
        [ForeignKey("ConditionsBookletDeliveryAddressId")]
        public int? ConditionsBookletDeliveryAddressId { get; private set; }
        public Address ConditionsBookletDeliveryAddress { get; private set; }
        [StringLength(1024)]
        public bool? SupplierNeedSample { get; set; }
        [StringLength(1024)]
        public string SamplesDeliveryAddress { get; set; }
        //[Required]
        public DateTime? LastEnqueriesDate { get; private set; }
        //[Required]
        public DateTime? LastOfferPresentationDate { get; private set; }
        //[Required]
        public TimeSpan? LastOfferPresentationTime { get; private set; }
        //Required if there is offer opening Place
        public DateTime? OffersOpeningDate { get; private set; }
        //Required if there is offer opening Place
        public TimeSpan? OffersOpeningTime { get; private set; }
        //[Required]
        public bool? InsideKSA { get; private set; }
        //  Tender Place
        public string Details { get; private set; }

        public DateTime? SubmitionDate { get; private set; }

        //[Required]
        public List<TenderArea> TenderAreas { private set; get; } = new List<TenderArea>();
        //List Booklet and attachment Required

        //List Quantities Tables //In electronic offer presentation
        //[Required]
        public List<QuantitiesTable> QuantitiesTables { get; private set; } = new List<QuantitiesTable>();
        //List Conditions and attachment 
        //[Required]
        public List<TenderHistory> TenderHistories { get; private set; } = new List<TenderHistory>();
        //[Required]
        //[ForeignKey("Status")]
        public int TenderStatusId { get; private set; }
        public TenderStatus Status { get; private set; }
        //[Required]
        public List<TenderActivity> TenderActivities { private set; get; } = new List<TenderActivity>();
        //  Tender Speciality Field

        [StringLength(2000)]
        public string ActivityDescription { get; private set; }

        public List<TenderMaintenanceRunnigWork> TenderMaintenanceRunnigWorks { private set; get; } = new List<TenderMaintenanceRunnigWork>();
        public List<TenderConstructionWork> TenderConstructionWorks { private set; get; } = new List<TenderConstructionWork>();

        public List<Offer> Offers { get; set; }
        //Tender Attachments PDF Or Zip And Maximum 350 MB
        public List<TenderAttachment> Attachments { private set; get; } = new List<TenderAttachment>();
        public List<Invitation> Invitations { get; private set; } = new List<Invitation>();
        public List<InvitationEmails> InvitationEmails { get; private set; } = new List<InvitationEmails>();

        /// <summary>
        /// tO BE SPLITTED
        /// </summary>

        //public int? TenderDetailId { get; private set; }
        //[ForeignKey("TenderDetailId")]
        //public TenderDetails TenderDetail { get; private set; } = new TenderDetails();

        //public int? TenderRelationlId { get; private set; }
        //[ForeignKey("TenderRelationlId")]
        //public TenderRelationDetails TenderRelation { get; private set; } = new TenderRelationDetails();

        public int AgencyId { get; private set; }
        #endregion

        #region NotMapped

        #endregion

        #region Constructors====================================================

        public Tender()
        {

        }
        public Tender(int tenderID, int tenderType, int? invitationType, string tenderName, string tenderNumber, string initiatorName, string purpose, int? technicalOrganizationId, int? offersCheckingCommitteeId, int? offersOpeningCommitteeId)
        {
            TenderId= tenderID;
            TenderTypeId = tenderType;
            InvitationTypeId = invitationType;
            TenderName = tenderName;
            InitiatorName = initiatorName;
            TenderNumber = tenderNumber;
            Purpose = purpose;
            TechnicalOrganizationId = technicalOrganizationId;
            OffersCheckingCommitteeId = offersCheckingCommitteeId;
            OffersOpeningCommitteeId = offersOpeningCommitteeId;
            if (tenderID==0)
                EntityCreated();
            else
                EntityUpdated(); 
        }
        public Tender(int tenderType, int invitationType, string tenderName, string tenderNumber, string initiatorName, string purpose, bool? insideKSA, string details)
        {
            //Description = description;
            TenderTypeId = tenderType;
            InvitationTypeId = invitationType;
            TenderName = tenderName;
            InitiatorName = initiatorName;
            TenderNumber = tenderNumber;
            Purpose = purpose;
            InsideKSA = insideKSA;
            Details = details;
            EntityCreated();
        }
        #endregion

        #region Methods====================================================
        public Tender UpdateBasicData(int tenderType, int? invitationType, string tenderName, string tenderNumber, string purpose, Enums.TenderStatus tenderstatusId, int? technicalOrganizationId, int? offersOpeningCommitteeId, int? offersCheckingCommitteeId, decimal? conditionsBookletPrice)
        {
            TenderTypeId = tenderType;
            InvitationTypeId = invitationType;
            TenderName = tenderName;
            TenderNumber = tenderNumber;
            TenderStatusId = (int)tenderstatusId;
            Purpose = purpose;
            TechnicalOrganizationId = TechnicalOrganizationId;
            OffersOpeningCommitteeId = offersOpeningCommitteeId;
            OffersCheckingCommitteeId = offersCheckingCommitteeId;
            AddActionHistory((int)Enums.TenderStatus.UnderEstablishing, "");
            ConditionsBookletPrice = conditionsBookletPrice;
            EntityUpdated();
            return this;
        }
        public Tender UpdateOffersData(int offerPresentationWayId, int? offerPresentationPlaceId, int? offersOpeningAddressId,
            int? conditionsBookletDeliveryAddressId, DateTime? lastEnqueriesDate,
            DateTime? lastOfferPresentationDate, TimeSpan? lastOfferPresentationTime, DateTime? offersOpeningDate, TimeSpan? offersOpeningTime)
        {
            OfferPresentationPlaceId = offerPresentationPlaceId;
            OffersOpeningAddressId = offersOpeningAddressId;
            ConditionsBookletDeliveryAddressId = conditionsBookletDeliveryAddressId;
            OfferPresentationWayId = offerPresentationWayId;
            LastEnqueriesDate = lastEnqueriesDate;
            LastOfferPresentationDate = lastOfferPresentationDate;
            LastOfferPresentationTime = lastOfferPresentationTime;
            OffersOpeningDate = offersOpeningDate;
            OffersOpeningTime = offersOpeningTime;
            AddActionHistory((int)Enums.TenderStatus.UnderEstablishing, "");

            EntityUpdated();
            return this;
        }
        public Tender UpdateTenderRelations(IList<int> AreaIds, IList<int> ActivityIds, IList<int> ConstructionWorksIds, IList<int> MentainanceRunnigWorksIds, bool? insideKSA, string details)
        {
            InsideKSA = insideKSA;
            Details = details;
            UpdateAreas(AreaIds);
            UpdateActivities(ActivityIds);
            UpdateConstructionWorks(ConstructionWorksIds);
            UpdateRunningMentainanceWorks(MentainanceRunnigWorksIds);

            AddActionHistory((int)Enums.TenderStatus.UnderEstablishing,"");

            EntityUpdated();
            return this;
        }
        public Tender UpdateQuantitiesTables(List<QuantitiesTable> tables)
        {
            //delete quantity tables and its items the add the new ones
            foreach (var item in QuantitiesTables)
            {
                item.DeleteQuantitiesTableAndItems();
            }
            foreach (var table in tables)
            {
                //table.AddQuantityItems(table.QuantitiesTableItems);
                QuantitiesTables.Add(new QuantitiesTable(table.Name, table.QuantitiesTableItems));
            }
            AddActionHistory((int)Enums.TenderStatus.UnderEstablishing,"");
            EntityUpdated();
            return this;
        }


        public Tender UpdateEstimatedValue(int tenderId, Decimal? estimatedValue)
        {
            EstimatedValue = estimatedValue;
            AddActionHistory((int)Enums.TenderStatus.Approved, "");
            EntityUpdated();
            return this;
        }

        public Tender UpdateAttachments(List<TenderAttachment> attachments)
        {
            //delete quantity tables and its items the add the new ones
            foreach (var item in Attachments)
            {
                item.Delete();
            }
            foreach (var attachment in attachments)
            {
                //table.AddQuantityItems(table.QuantitiesTableItems);
                Attachments.Add(attachment);
            }

            AddActionHistory((int)Enums.TenderStatus.Established, "");
            EntityUpdated();
            return this;
        }

        public void UpdateAreas(IList<int> AreaIds)
        {
            if (AreaIds != null)
            {
                //will not cahnge
                var mutualAreas = TenderAreas.Where(x => AreaIds.Contains(x.AreaId)).ToList();
                var mutualIds = mutualAreas.Select(a => a.AreaId).ToList<int>();
                //Will be deleted
                var AreasToBeDeleted = TenderAreas.Where(x => !AreaIds.Contains(x.AreaId)).ToList();
                //Will be added
                List<int> AreasAddedIDs = AreaIds.Where(x => !mutualIds.Contains(x)).ToList<int>();
                if (TenderAreas != null)
                {
                    foreach (var item in AreasToBeDeleted)
                    {
                        item.Delete();
                    }
                }
                if (AreaIds != null)
                {
                    foreach (var item in AreasAddedIDs)
                    {
                        TenderAreas.Add(new TenderArea(item));
                    }
                }
            }
        }
        public void UpdateActivities(IList<int> ActivityIds)
        {
            if (ActivityIds != null)
            {
                //will not cahnge
                var mutualActivities = TenderActivities.Where(x => ActivityIds.Contains(x.ActivityId)).ToList();
                var mutualActivitiesIds = mutualActivities.Select(a => a.ActivityId).ToList<int>();
                //Will be deleted
                var ActivitiesToBeDeleted = TenderActivities.Where(x => !ActivityIds.Contains(x.ActivityId)).ToList();

                //Will be added
                List<int> ActivitiesAddedIDs = ActivityIds.Where(x => !mutualActivitiesIds.Contains(x)).ToList<int>();
                if (TenderActivities != null)
                {
                    foreach (var item in ActivitiesToBeDeleted)
                    {
                        item.Delete();
                    }
                }
                if (ActivityIds != null)
                {
                    foreach (var item in ActivitiesAddedIDs)
                    {
                        TenderActivities.Add(new TenderActivity(item));
                    }
                }
            }
        }
        public void UpdateConstructionWorks(IList<int> ConstructionWorksIds)
        {
            if (ConstructionWorksIds != null)
            {
                //Construction
                //will not cahnge
                var mutualConstruction = TenderConstructionWorks.Where(x => ConstructionWorksIds.Contains(x.ConstructionWorkId)).ToList();
                var mutualConstructionIds = mutualConstruction.Select(a => a.ConstructionWorkId).ToList<int>();
                //Will be deleted
                var ConstructionToBeDeleted = TenderConstructionWorks.Where(x => !ConstructionWorksIds.Contains(x.ConstructionWorkId)).ToList();

                //Will be added
                List<int> ConstructionAddedIDs = ConstructionWorksIds.Where(x => !mutualConstructionIds.Contains(x)).ToList<int>();

                if (TenderConstructionWorks != null)
                {
                    foreach (var item in ConstructionToBeDeleted)
                    {
                        item.Delete();
                    }
                }

                if (ConstructionWorksIds != null)
                {
                    foreach (var item in ConstructionAddedIDs)
                    {
                        TenderConstructionWorks.Add(new TenderConstructionWork(item));
                    }
                }
            }
        }
        public void UpdateRunningMentainanceWorks(IList<int> MentainanceRunnigWorksIds)
        {
            if (MentainanceRunnigWorksIds != null)
            {
                //will not cahnge
                var mutualMentainance = TenderMaintenanceRunnigWorks.Where(x => MentainanceRunnigWorksIds.Contains(x.MaintenanceRunningWorkId)).ToList();
                var mutualMentainanceIds = mutualMentainance.Select(a => a.MaintenanceRunningWorkId).ToList<int>();
                //Will be deleted
                var MentainanceToBeDeleted = TenderMaintenanceRunnigWorks.Where(x => !MentainanceRunnigWorksIds.Contains(x.MaintenanceRunningWorkId)).ToList();

                //Will be added
                List<int> MentainanceAddedIDs = MentainanceRunnigWorksIds.Where(x => !mutualMentainanceIds.Contains(x)).ToList<int>();

                if (TenderMaintenanceRunnigWorks != null)
                {
                    foreach (var item in MentainanceToBeDeleted)
                    {
                        item.Delete();
                    }
                }

                if (MentainanceRunnigWorksIds != null)
                {
                    foreach (var item in MentainanceAddedIDs)
                    {
                        TenderMaintenanceRunnigWorks.Add(new TenderMaintenanceRunnigWork(item));
                    }
                }
            }
        }
        public void AddSupplierInvitation(List<int> checkedCommericalRegesterNo, List<int> unCheckedcommericalRegesterNo)
        {
            List<int> commericalNos = new List<int>();
            foreach (var item in Invitations.Where(x => x.InvitationTypeId == 1 && x.StatusId != (int)Enums.InvitationStatus.Approved && x.IsActive == true))
            {
                commericalNos.Add(item.CommericalRegisterNo);
            }
            foreach (var item in checkedCommericalRegesterNo)
            {
                if (!commericalNos.Contains(item))
                {
                    Invitations.Add(new Invitation(item, Enums.InvitationStatus.New, Enums.InvitationRequestType.Invitation));
                }

            }
            foreach (var item in unCheckedcommericalRegesterNo)
            {
                Invitation invitation = Invitations.Where(x => x.CommericalRegisterNo == item && x.IsActive ==true).FirstOrDefault();
                if (invitation != null)
                {
                    invitation.DeActive();
                }
               
            }
            EntityUpdated();
        }
        public void RegisterSupplierEmails(List<string> emails)
        {
            foreach (var item in InvitationEmails.Where(x => x.SentTypeID == 1))
            {
                item.DeActive();
            }
            foreach (var item in emails)
            {
                InvitationEmails.Add(new InvitationEmails(item, 1));
            }
            EntityUpdated();
        }
        public void RegisterSupplierMobilNo(List<string> mobilNoList)
        {
            foreach (var item in InvitationEmails.Where(x => x.SentTypeID == 2))
            {
                item.DeActive();
            }
            foreach (var item in mobilNoList)
            {
                InvitationEmails.Add(new InvitationEmails(item, 2));
            }
            EntityUpdated();
        }
        public void AddActionHistory(int statusId, string rejectionReason)
        {
            var currentUserId = Thread.CurrentPrincipal.Identity.UserId();

            TenderHistories.Add(new TenderHistory(currentUserId, statusId, rejectionReason));

            EntityUpdated();
        }
        public void SendRequestForTender(int supplierId)
        {
            Invitations.Add(new Invitation(supplierId, Enums.InvitationStatus.New, Enums.InvitationRequestType.Request));
            EntityUpdated();
        }

        //reda
        public void SendTenderForApproving()
        {
            this.TenderStatusId = (int)Enums.TenderStatus.Established;

            EntityUpdated();
        }

        public void UpdateTenderStatus(Enums.TenderStatus tenderStatus, string RejectionReason)
        {
            this.TenderStatusId = (int)tenderStatus;
            
            AddActionHistory(TenderStatusId, RejectionReason);

            EntityUpdated();
        }
        public void UpdateTenderStatus(Enums.TenderStatus tenderStatus)
        { 
            this.TenderStatusId = (int)tenderStatus; 
            EntityUpdated();
        }

        public void UpdateSubmitionDate()
        {
            this.SubmitionDate = DateTime.Now;
        }
        public void UpdateAttachments(string description)
        {
            //Description = description;
            AddActionHistory((int)Enums.TenderStatus.Established, "");
            EntityUpdated();
        }

        public Tender Update()
        {
            EntityUpdated();
            return this;
        }

        public void DeActive()
        {
            IsActive = false;
            EntityUpdated();
        }
        public void SetActive()
        {
            IsActive = true;
            EntityUpdated();
        }
        #endregion
    }
}
