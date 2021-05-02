using MOF.Etimad.Monafasat.SharedKernal.Exceptions;
using MOF.Etimad.Monafasat.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("Announcement", Schema = "Announcement")]

    public class Announcement : AuditableEntity
    {
        [Key]
        public int AnnouncementId { get; private set; }

        [StringLength(1024)]
        public string AnnouncementName { get; private set; }

        [StringLength(5000)]
        public string IntroAboutTender { get; private set; }

        public int AnnouncementPeriod { get; private set; }

        public bool InsidKsa { get; set; }

        [StringLength(5000)]
        public string Details { get; private set; }

        [StringLength(2000)]
        public string ActivityDescription { get; private set; }

        public DateTime? PublishedDate { get; private set; }

        [ForeignKey(nameof(TenderType))]
        public int TenderTypeId { get; private set; }

        public TenderType TenderType { get; private set; }

        [ForeignKey(nameof(ReasonForPurchaseTenderType))]
        public int ReasonForSelectingTenderTypeId { get; private set; }

        public ReasonForPurchaseTenderType ReasonForPurchaseTenderType { get; private set; }

        [StringLength(20)]
        [ForeignKey(nameof(Agency))]
        public string AgencyCode { get; private set; }

        [ForeignKey(nameof(Branch))]
        public int? BranchId { get; private set; }

        [StringLength(200)]
        public string ApprovedBy { get; private set; }
        [StringLength(100)]
        public string ReferenceNumber { get; private set; }

        [NotMapped]
        public DateTime? LastApplyingRequestsDate
        {
            get
            {
                return !this.PublishedDate.HasValue ? this.PublishedDate : this.PublishedDate.Value.AddDays(this.AnnouncementPeriod);
            }
        }

        [ForeignKey(nameof(AnnouncementStatus))]
        public int StatusId { get; set; }

        [NotMapped]
        public int AnnouncementStatusId => this.LastApplyingRequestsDate < DateTime.Now ? (int)Enums.AnnouncementStatus.Ended : this.StatusId;

        public AnnouncementStatus AnnouncementStatus { get; private set; }
        public GovAgency Agency { get; private set; }
        public Branch Branch { get; private set; }
        public List<AnnouncementMaintenanceRunnigWork> AnnouncementMaintenanceRunnigWorks { private set; get; } = new List<AnnouncementMaintenanceRunnigWork>();
        public List<AnnouncementConstructionWork> AnnouncementConstructionWorks { private set; get; } = new List<AnnouncementConstructionWork>();
        public List<AnnouncementActivity> AnnouncementActivities { private set; get; } = new List<AnnouncementActivity>();
        public List<AnnouncementArea> AnnouncementAreas { private set; get; } = new List<AnnouncementArea>();
        public List<AnnouncementCountry> AnnouncementCountries { private set; get; } = new List<AnnouncementCountry>();
        public List<AnnouncementHistory> AnnouncementHistories { get; private set; } = new List<AnnouncementHistory>();
        public List<AnnouncementJoinRequest> AnnouncementJoinRequests { get; private set; } = new List<AnnouncementJoinRequest>();


        public Announcement()
        {

        }
        public Announcement(string announcementName, int announcementPeriod, int tenderTypeId, int tenderReasonId, string introAboutTender, bool insideKsa, string detials, string activityDescription,
            int? branchId, string agencyCode, List<int> activitiesIds, List<int> constructionsWorkIds, List<int> mainteananceWorkIds, List<int> areasIds, List<int> countriesIds)
        {
            AnnouncementName = announcementName;
            AnnouncementPeriod = announcementPeriod;
            IntroAboutTender = introAboutTender;
            InsidKsa = insideKsa;
            Details = detials;
            ActivityDescription = activityDescription;
            StatusId = (int)Enums.AnnouncementStatus.UnderCreation;
            BranchId = branchId;
            AgencyCode = agencyCode;
            TenderTypeId = tenderTypeId;
            this.ReasonForSelectingTenderTypeId = tenderTypeId == (int)Enums.TenderType.NewDirectPurchase ? (int)Enums.ReasonForPurchaseTenderType.BusinessAndProcurementAreAvailableToOneContractorCSupplierAndHaveNoAcceptableAlternative : (int)Enums.ReasonForLimitedTenderType.PurchasesThatAvailableOnlyToLimitedNumberOfContractOrSuppliers;
            UpdateActivities(activitiesIds);
            UpdateConstructionWorks(constructionsWorkIds);
            UpdateRunningMentainanceWorks(mainteananceWorkIds);
            UpdateAreas(areasIds, insideKsa);
            UpdateCountries(countriesIds, insideKsa);
            EntityCreated();
        }

        public void UpdateAnnouncement(string announcementName, int announcementPeriod, int tenderTypeId, int tenderReasonId, string introAboutTender, bool insideKsa, string detials, string activityDescription,
           int? branchId, string agencyCode, List<int> activitiesIds, List<int> constructionsWorkIds, List<int> mainteananceWorkIds, List<int> areasIds, List<int> countriesIds)
        {
            this.AnnouncementName = announcementName;
            this.AnnouncementPeriod = announcementPeriod;
            this.IntroAboutTender = introAboutTender;
            this.InsidKsa = insideKsa;
            this.Details = detials;
            this.ActivityDescription = activityDescription;
            this.StatusId = (int)Enums.AnnouncementStatus.UnderCreation;
            this.BranchId = branchId;
            this.AgencyCode = agencyCode;
            this.TenderTypeId = tenderTypeId;
            this.ReasonForSelectingTenderTypeId = tenderTypeId == (int)Enums.TenderType.NewDirectPurchase ? (int)Enums.ReasonForPurchaseTenderType.BusinessAndProcurementAreAvailableToOneContractorCSupplierAndHaveNoAcceptableAlternative : (int)Enums.ReasonForLimitedTenderType.PurchasesThatAvailableOnlyToLimitedNumberOfContractOrSuppliers;

            UpdateActivities(activitiesIds);
            UpdateConstructionWorks(constructionsWorkIds);
            UpdateRunningMentainanceWorks(mainteananceWorkIds);
            UpdateAreas(areasIds, insideKsa);
            UpdateCountries(countriesIds, insideKsa);
            EntityUpdated();
        }
        public void Delete()
        {
            EntityDeleted();
        }
        public void Test_UpdateStatus(int newStatusId)
        {
            this.StatusId = newStatusId;
            EntityUpdated();
        }

        public void Test_UpdatePublishDatetooldDate(DateTime newPublishDate)
        {
            this.PublishedDate = newPublishDate;
            EntityUpdated();
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
        private void UpdateActivities(IList<int> ActivityIds)
        {
            if (ActivityIds == null) return;
            var activitiesToBeDeleted = AnnouncementActivities.ToList();
            var activitiesAddedIDs = ActivityIds.ToList();
            if (AnnouncementActivities != null)
            {
                foreach (var item in activitiesToBeDeleted)
                {
                    item.Delete();
                }
            }

            foreach (var item in activitiesAddedIDs)
            {
                AnnouncementActivities.Add(new AnnouncementActivity(item));
            }
        }
        private void UpdateConstructionWorks(IList<int> ConstructionWorksIds)
        {
            if (ConstructionWorksIds != null)
            {
                var ConstructionToBeDeleted = AnnouncementConstructionWorks.ToList();
                var ConstructionAddedIDs = ConstructionWorksIds.ToList();
                if (AnnouncementConstructionWorks != null)
                {
                    foreach (var item in ConstructionToBeDeleted)
                    {
                        item.Delete();
                    }
                }
                foreach (var item in ConstructionAddedIDs)
                {
                    AnnouncementConstructionWorks.Add(new AnnouncementConstructionWork(item));
                }
            }
        }
        private void UpdateRunningMentainanceWorks(IList<int> MentainanceRunnigWorksIds)
        {
            if (MentainanceRunnigWorksIds != null)
            {
                var MentainanceToBeDeleted = AnnouncementMaintenanceRunnigWorks.ToList();
                var MentainanceAddedIDs = MentainanceRunnigWorksIds.ToList();
                if (AnnouncementMaintenanceRunnigWorks != null)
                {
                    foreach (var item in MentainanceToBeDeleted)
                    {
                        item.Delete();
                    }
                }
                foreach (var item in MentainanceAddedIDs)
                {
                    AnnouncementMaintenanceRunnigWorks.Add(new AnnouncementMaintenanceRunnigWork(item));
                }
            }
        }
        private void UpdateAreas(IList<int> AreaIds, bool insideKSA)
        {

            if (AreaIds != null)
            {
                var mutualAreas = AnnouncementAreas.Where(x => AreaIds.Contains(x.AreaId)).ToList();
                var mutualIds = mutualAreas.Select(a => a.AreaId).ToList();
                var AreasToBeDeleted = AnnouncementAreas.Where(x => !AreaIds.Contains(x.AreaId)).ToList();
                var AreasAddedIDs = AreaIds.Where(x => !mutualIds.Contains(x)).ToList<int>();
                if (insideKSA && AnnouncementCountries != null)
                {
                    foreach (var item in AnnouncementCountries)
                    {
                        item.Delete();
                    }
                }
                if (AnnouncementAreas != null)
                {
                    foreach (var item in AreasToBeDeleted)
                    {
                        item.Delete();
                    }
                }

                foreach (var item in AreasAddedIDs)
                {
                    AnnouncementAreas.Add(new AnnouncementArea(item));
                }
            }

        }
        private void UpdateCountries(IList<int> countriesIds, bool insideKSA)
        {
            if (countriesIds != null)
            {
                if (!insideKSA && AnnouncementCountries != null)
                {
                    foreach (AnnouncementCountry item in AnnouncementCountries)
                        {
                            item.Delete();
                        }
                    
                }

                var CountriesToBeDeleted = AnnouncementCountries.ToList();
                var CountriesAddedIDs = countriesIds.ToList();
                if (AnnouncementCountries != null)
                {
                    foreach (var item in CountriesToBeDeleted)
                    {
                        item.Delete();
                    }
                }

                foreach (var item in CountriesAddedIDs)
                {
                    AnnouncementCountries.Add(new AnnouncementCountry(item));
                }
            }
        }
        public void AddJoinRequest(AnnouncementJoinRequest joinRequest)
        {
            this.AnnouncementJoinRequests.Add(joinRequest);
            EntityUpdated();
        }
        public void WithdroawJoinRequest(string _Cr)
        {
            var requests = this.AnnouncementJoinRequests.Where(w => w.Cr == _Cr);
            foreach (var request in requests)
            {
                request.WithDraw();
            }
            EntityUpdated();
        }
        public void SetPublishedDate()
        {
            PublishedDate = DateTime.Now;
        }
        public void SetApprovedBy(string user)
        {
            ApprovedBy = user;
        }
        public void SendAnnouncementForApproval()
        {
            if (AnnouncementStatusId != (int)Enums.AnnouncementStatus.UnderCreation)
                throw new BusinessRuleException("يجب ان يكون الإعلان تحت الإنشاء");
            if (LastApplyingRequestsDate > DateTime.Now)
                throw new BusinessRuleException("يجب ان لا يكون الإعلان منتهي ");

            StatusId = (int)Enums.AnnouncementStatus.Pending;
            EntityUpdated();
        }
        public void SetReferenceNumber(string referenceNumber)
        {
            ReferenceNumber = referenceNumber;
        }
        public void ApproveAnnouncement()
        {
            if (AnnouncementStatusId != (int)Enums.AnnouncementStatus.Pending)
                throw new BusinessRuleException("يجب ان يكون الإعلان بإنتظار الإعتماد");
            if (LastApplyingRequestsDate < DateTime.Now)
                throw new BusinessRuleException("يجب ان لا يكون الإعلان منتهي ");

            StatusId = (int)Enums.AnnouncementStatus.Approved;
            EntityUpdated();
        }


        public void Test_ApproveAnnouncementDate()
        {
            if (LastApplyingRequestsDate < DateTime.Now)
                throw new BusinessRuleException("يجب ان لا يكون الإعلان منتهي ");

            StatusId = (int)Enums.AnnouncementStatus.Approved;
            EntityUpdated();
        }
        public void RejectApproveAnnouncement(string rejctionReason)
        {
            if (AnnouncementStatusId != (int)Enums.AnnouncementStatus.Pending)
                throw new BusinessRuleException("يجب ان يكون الإعلان بإنتظار الإعتماد");
            if (LastApplyingRequestsDate > DateTime.Now)
                throw new BusinessRuleException("يجب ان لا يكون الإعلان منتهي ");

            StatusId = (int)Enums.AnnouncementStatus.Rejected;
            EntityUpdated();
        }
        public void ReOpenAnnouncement()
        {
            if (AnnouncementStatusId != (int)Enums.AnnouncementStatus.Rejected)
                throw new BusinessRuleException("يجب ان يكون الإعلان مرفوض");

            StatusId = (int)Enums.AnnouncementStatus.UnderCreation;
            EntityUpdated();
        }
        public void DeleteAnnouncement()
        {
            if (StatusId != (int)Enums.AnnouncementStatus.UnderCreation)
                throw new BusinessRuleException("يجب ان تكون حالة الإعلان تحت الإنشاء");

            IsActive = false;
            EntityUpdated();
        }
        public void AddAnnouncementHistoy(int userId)
        {
            AnnouncementHistories.Add(new AnnouncementHistory(userId, StatusId));
            EntityUpdated();
        }
        public void AddAnnouncementHistoy(int userId, string rejctionReason)
        {
            AnnouncementHistories.Add(new AnnouncementHistory(userId, StatusId, rejctionReason));
            EntityUpdated();
        }
    }
}
