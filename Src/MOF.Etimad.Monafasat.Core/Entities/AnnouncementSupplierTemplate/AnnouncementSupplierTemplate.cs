using MOF.Etimad.Monafasat.SharedKernal.Exceptions;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("AnnouncementSupplierTemplate", Schema = "AnnouncementTemplate")]
    public class AnnouncementSupplierTemplate : AuditableEntity
    {
        [Key]
        public int AnnouncementId { get; set; }

        [StringLength(1024)]
        public string AnnouncementName { get; private set; }

        public AnnouncementTemplateListType AnnouncementTemplateListType { get; private set; }

        [ForeignKey(nameof(AnnouncementTemplateListType))]
        public int? AnnouncementTemplateSuppliersListTypeId { get; private set; }

        [StringLength(5000)]
        public string IntroAboutAnnouncementTemplate { get; private set; }

        public bool InsidKsa { get; set; }

        [StringLength(5000)]
        public string Descriptions { get; private set; }

        [StringLength(5000)]
        public string Details { get; private set; }

        [StringLength(2000)]
        public string ActivityDescription { get; private set; }

        public DateTime? PublishedDate { get; private set; }

        public string TenderTypesId { get; private set; }

        [StringLength(20)]
        [ForeignKey(nameof(Agency))]
        public string AgencyCode { get; private set; }

        [ForeignKey(nameof(Branch))]
        public int? BranchId { get; private set; }

        [StringLength(200)]
        public string ApprovedBy { get; private set; }
        [StringLength(100)]
        public string ReferenceNumber { get; private set; }

        public bool? IsEffectiveList { get; private set; }
        public DateTime? EffectiveListDate { get; private set; }
        [ForeignKey(nameof(AnnouncementStatus))]
        public int StatusId { get; set; }

        public AnnouncementStatusSupplierTemplate AnnouncementStatus { get; private set; }
        public GovAgency Agency { get; private set; }
        public Branch Branch { get; private set; }
        public List<AnnouncementMaintenanceRunnigWorkSupplierTemplate> AnnouncementMaintenanceRunnigWorks { private set; get; } = new List<AnnouncementMaintenanceRunnigWorkSupplierTemplate>();
        public List<AnnouncementConstructionWorkSupplierTemplate> AnnouncementConstructionWorks { private set; get; } = new List<AnnouncementConstructionWorkSupplierTemplate>();
        public List<AnnouncementActivitySupplierTemplate> AnnouncementActivities { private set; get; } = new List<AnnouncementActivitySupplierTemplate>();
        public List<AnnouncementAreaSupplierTemplate> AnnouncementAreas { private set; get; } = new List<AnnouncementAreaSupplierTemplate>();
        public List<AnnouncementCountrySupplierTemplate> AnnouncementCountries { private set; get; } = new List<AnnouncementCountrySupplierTemplate>();
        public List<AnnouncementHistorySupplierTemplate> AnnouncementHistories { get; private set; } = new List<AnnouncementHistorySupplierTemplate>();
        public List<AnnouncementJoinRequestSupplierTemplate> AnnouncementJoinRequests { get; private set; } = new List<AnnouncementJoinRequestSupplierTemplate>();
        public List<AnnouncementSuppliersTemplateAttachment> Attachments { private set; get; } = new List<AnnouncementSuppliersTemplateAttachment>();
        public List<LinkedAgenciesAnnouncementTemplate> LinkedAgenciesAnnouncements { private set; get; } = new List<LinkedAgenciesAnnouncementTemplate>();

        public string RequirementConditionsToJoinList { get; private set; }
        public string AgencyAddress { get; private set; }
        public string AgencyPhone { get; private set; }
        public string AgencyFax { get; private set; }
        public string AgencyEmail { get; private set; }
        public bool IsRequiredAttachmentToJoinList { get; private set; }
        public string RequiredAttachment { get; private set; }
        public int? CreatedById { get; private set; }
        public int? ReadyForApproval { get; private set; }

        public string CancelationReason { get; set; }
        public string TemplateExtendMechanism { get; private set; }


        public AnnouncementSupplierTemplate()
        {
        }

        public AnnouncementSupplierTemplate UpdateAnnouncementSupplierTemplateRelations(IList<int> AreaIds, IList<int> ActivityIds, IList<int> ConstructionWorksIds, IList<int> MentainanceRunnigWorksIds, bool insideKsa, string details, string activityDescription, IList<int> CountriesIds)
        {
            InsidKsa = insideKsa;
            Details = details;
            UpdateAreas(AreaIds, insideKsa);
            UpdateCountries(CountriesIds, insideKsa);
            UpdateActivities(ActivityIds);
            UpdateConstructionWorks(ConstructionWorksIds);
            UpdateRunningMentainanceWorks(MentainanceRunnigWorksIds);
            ActivityDescription = activityDescription;
            EntityUpdated();
            return this;
        }

        public AnnouncementSupplierTemplate UpdateSuppliersTemplateAttachments(List<AnnouncementSuppliersTemplateAttachment> attachments, int userId, bool addHistory = true)
        {
            foreach (var item in Attachments)
            {
                item.Delete();
            }

            foreach (var attachment in attachments)
            {
                Attachments.Add(attachment);
            }
            EntityUpdated();
            return this;
        }
        public AnnouncementSupplierTemplate UpdateAnnouncementSupplierTemplateData(CreateAnnouncementSupplierTemplateModel model, List<AnnouncementSuppliersTemplateAttachment> attachments, int userId)
        {
            AnnouncementName = model.AnnouncementSupplierTemplateName;
            AnnouncementTemplateSuppliersListTypeId = model.AnnouncementTemplateSuppliersListTypeId;
            TenderTypesId = model.TenderTypesId;
            IntroAboutAnnouncementTemplate = model.IntroAboutAnnouncementTemplate;
            Descriptions = model.Descriptions;
            Details = model.Details;
            IsEffectiveList = model.IsEffectiveList;
            EffectiveListDate = model.EffectiveListDate;
            ActivityDescription = model.ActivityDescription;
            InsidKsa = model.InsideKsa;
            StatusId = (int)Enums.AnnouncementSupplierTemplateStatus.UnderCreation;
            BranchId = model.BranchId;
            RequirementConditionsToJoinList = model.RequirementConditionsToJoinList;
            IsRequiredAttachmentToJoinList = model.IsRequiredAttachmentToJoinList;
            RequiredAttachment = model.RequiredAttachment;
            AgencyCode = model.AgencyCode;
            AgencyAddress = model.AgencyAddress;
            AgencyFax = model.AgencyFax;
            AgencyPhone = model.AgencyPhone;
            AgencyEmail = model.AgencyEmail;
            UpdateAnnouncementSupplierTemplateRelations(model.AreasIds, model.ActivityIds, model.ConstructionWorkIds, model.MaintenanceRunnigWorkIds, model.InsideKsa, model.Details, model.ActivityDescription, model.CountriesIds);
            UpdateSuppliersTemplateAttachments(attachments, userId, false);
            EntityUpdated();
            return this;
        }

        public void CreateAnnouncementSupplierTemplate(CreateAnnouncementSupplierTemplateModel model, List<AnnouncementSuppliersTemplateAttachment> attachments, int userId)
        {
            AnnouncementName = model.AnnouncementSupplierTemplateName;
            AnnouncementTemplateSuppliersListTypeId = model.AnnouncementTemplateSuppliersListTypeId;
            TenderTypesId = model.TenderTypesId;
            IntroAboutAnnouncementTemplate = model.IntroAboutAnnouncementTemplate;
            Descriptions = model.Descriptions;
            Details = model.Details;
            IsEffectiveList = model.IsEffectiveList;
            EffectiveListDate = model.EffectiveListDate;
            ActivityDescription = model.ActivityDescription;
            InsidKsa = model.InsideKsa;
            StatusId = (int)Enums.AnnouncementSupplierTemplateStatus.UnderCreation;
            BranchId = model.BranchId;
            RequirementConditionsToJoinList = model.RequirementConditionsToJoinList;
            IsRequiredAttachmentToJoinList = model.IsRequiredAttachmentToJoinList;
            RequiredAttachment = model.RequiredAttachment;
            AgencyCode = model.AgencyCode;
            AgencyAddress = model.AgencyAddress;
            AgencyFax = model.AgencyFax;
            AgencyPhone = model.AgencyPhone;
            AgencyEmail = model.AgencyEmail;
            CreatedById = userId;
            UpdateAnnouncementSupplierTemplateRelations(model.AreasIds, model.ActivityIds, model.ConstructionWorkIds, model.MaintenanceRunnigWorkIds, model.InsideKsa, model.Details, model.ActivityDescription, model.CountriesIds);
            UpdateSuppliersTemplateAttachments(attachments, userId, false);
            EntityCreated();
        }


        public AnnouncementSupplierTemplate UpdateAnnouncementSupplierTemplateListData(UpdateAnnouncementSupplierTemplateModel model, List<AnnouncementSuppliersTemplateAttachment> attachments, int userId)
        {
            AnnouncementTemplateSuppliersListTypeId = model.AnnouncementTemplateSuppliersListTypeId;
            IsEffectiveList = model.IsEffectiveList;
            EffectiveListDate = model.EffectiveListDate;
            BranchId = model.BranchId;
            AgencyCode = model.AgencyCode;
            AgencyAddress = model.AgencyAddress;
            AgencyFax = model.AgencyFax;
            AgencyPhone = model.AgencyPhone;
            AgencyEmail = model.AgencyEmail;
            UpdateSuppliersTemplateAttachments(attachments, userId, false);
            EntityUpdated();
            return this;
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


        public void UpdateAnnouncementStatus(Enums.AnnouncementSupplierTemplateStatus announcementStatus, string cancelReason, int userId, bool isActive = true)
        {
            this.StatusId = (int)announcementStatus;
            this.AnnouncementStatus = null;
            IsActive = isActive;
            if (!string.IsNullOrEmpty(cancelReason))
                CancelationReason = cancelReason;
            EntityUpdated();
        }

        public void UpdateAnnouncementStatus_ForTest()
        {
            this.AnnouncementStatus = new AnnouncementStatusSupplierTemplate ();
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

        public void SetApprovalStatus(Enums.AnnouncementSupplierTemplateStatus readyForApproval)
        {
            ReadyForApproval = (int)readyForApproval;
            EntityUpdated();
        }

        public void RemoveApprovalStatus()
        {
            ReadyForApproval = 0;
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
                AnnouncementActivities.Add(new AnnouncementActivitySupplierTemplate(item));
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
                    AnnouncementConstructionWorks.Add(new AnnouncementConstructionWorkSupplierTemplate(item));
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
                    AnnouncementMaintenanceRunnigWorks.Add(new AnnouncementMaintenanceRunnigWorkSupplierTemplate(item));
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
                    AnnouncementAreas.Add(new AnnouncementAreaSupplierTemplate(item));
                }
            }

        }
        private void UpdateCountries(IList<int> countriesIds, bool insideKSA)
        {
            if (countriesIds != null)
            {
                if (!insideKSA && AnnouncementCountries != null)
                {
                    foreach (AnnouncementCountrySupplierTemplate item in AnnouncementCountries)
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
                    AnnouncementCountries.Add(new AnnouncementCountrySupplierTemplate(item));
                }
            }
        }
        public AnnouncementSupplierTemplate AddJoinRequest(List<AnnouncementTemplateJoinRequestAttachment> attachments, int announcementId, string cr)
        {
            AnnouncementJoinRequests.Add(new AnnouncementJoinRequestSupplierTemplate(attachments, announcementId, cr, (int)Enums.AnnouncementTemplateJoinRequestStatus.Sent));
            EntityUpdated();
            return this;
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
            if (StatusId != (int)Enums.AnnouncementStatus.UnderCreation)
                throw new BusinessRuleException("يجب ان يكون الإعلان تحت الإنشاء");
            if (EffectiveListDate > DateTime.Now)
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
            if (EffectiveListDate < DateTime.Now)
                throw new BusinessRuleException("يجب ان لا يكون الإعلان منتهي ");

            StatusId = (int)Enums.AnnouncementStatus.Approved;
            EntityUpdated();
        }
        public void DeleteAnnouncement()
        {
            if (StatusId != (int)Enums.AnnouncementStatus.UnderCreation)
                throw new BusinessRuleException("يجب ان تكون حالة الإعلان تحت الإنشاء");
            IsActive = false;
            EntityUpdated();
        }

        public AnnouncementSupplierTemplate SetLinkedAgencies(LinkedAgenciesAnnouncementTemplate linkedAgencies)
        {
            LinkedAgenciesAnnouncements.Add(linkedAgencies);
            EntityUpdated();
            return this;
        }
        public void AddPublicAnnouncementListToAgency(string agencyCode)
        {
            LinkedAgenciesAnnouncements.Add(new LinkedAgenciesAnnouncementTemplate(agencyCode));
            EntityUpdated();
        }
        public void RemovePublicAnnouncementListFromAgency()
        {
            LinkedAgenciesAnnouncements.ForEach(x => x.DeActivate());
            EntityUpdated();
        }

        public void RemovePublicAnnouncementListFromAgency(string agencyCode)
        {
            var linkedAgenciesAnnouncements = LinkedAgenciesAnnouncements.Where(x => x.AgencyCode == agencyCode).ToList();
            foreach (var item in linkedAgenciesAnnouncements)
            {
                item.DeActivate();
            }
            EntityUpdated();
        }
        public void AddAnnouncementHistoy(int userId)
        {
            AnnouncementHistories.Add(new AnnouncementHistorySupplierTemplate(userId, StatusId));
            EntityUpdated();
        }
        public void AddAnnouncementHistoy(int userId, string rejctionReason)
        {
            AnnouncementHistories.Add(new AnnouncementHistorySupplierTemplate(userId, StatusId, rejctionReason));
            EntityUpdated();
        }

        public void UpdateTemplate()
        {
            EntityUpdated();
        }

        public void Test_EditEffectiveListDate(DateTime date)
        {
            EffectiveListDate = date;
        }
        public void Test_AddedCreatedByID()
        {
            CreatedById = 1;
        }

        public AnnouncementSupplierTemplate ExtendAnnouncementSupplierTemplateData(ExtendAnnouncementSupplierTemplateModel model, List<AnnouncementSuppliersTemplateAttachment> attachments, int userId)
        {
            TemplateExtendMechanism = model.TemplateExtendMechanism;
            AnnouncementName = model.AnnouncementSupplierTemplateName;
            AnnouncementTemplateSuppliersListTypeId = model.AnnouncementTemplateSuppliersListTypeId;
            TenderTypesId = model.TenderTypesId;
            IntroAboutAnnouncementTemplate = model.IntroAboutAnnouncementTemplate;
            Descriptions = model.Descriptions;
            Details = model.Details;
            IsEffectiveList = model.IsEffectiveList;
            EffectiveListDate = model.EffectiveListDate;
            ActivityDescription = model.ActivityDescription;
            InsidKsa = model.InsideKsa;
            StatusId = (int)Enums.AnnouncementSupplierTemplateStatus.Approved;
            BranchId = model.BranchId;
            RequirementConditionsToJoinList = model.RequirementConditionsToJoinList;
            IsRequiredAttachmentToJoinList = model.IsRequiredAttachmentToJoinList;
            RequiredAttachment = model.RequiredAttachment;
            AgencyCode = model.AgencyCode;
            AgencyAddress = model.AgencyAddress;
            AgencyFax = model.AgencyFax;
            AgencyPhone = model.AgencyPhone;
            AgencyEmail = model.AgencyEmail;
            UpdateAnnouncementSupplierTemplateRelations(model.AreasIds, model.ActivityIds, model.ConstructionWorkIds, model.MaintenanceRunnigWorkIds, model.InsideKsa, model.Details, model.ActivityDescription, model.CountriesIds);
            UpdateSuppliersTemplateAttachments(attachments, userId, false);
            EntityUpdated();
            return this;
        }

    }
}
