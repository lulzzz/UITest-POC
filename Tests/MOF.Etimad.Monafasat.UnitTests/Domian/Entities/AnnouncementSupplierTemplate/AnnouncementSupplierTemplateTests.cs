using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernal.Exceptions;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.TestsBuilders.AnnouncementTemplateDefaults;
using MOF.Etimad.Monafasat.ViewModel;
using Shouldly;
using System;
using System.Collections.Generic;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.AnnouncementSupplierTemplate
{
    public class AnnouncementSupplierTemplateTests
    {
        private const string Name = "test announcement";
        private const string TemplateExtendMechanism = "TemplateExtendMechanism";

        private const string IntroAboutAnnouncementTemplate = "intro";
        private const bool IsInsideKsa = true;
        private const string Details = "test details";
        private const string Descriptions = "descriptions";
        private const string ActivityDescription = "activity description";
        private const int BranchId = 1;
        private const int StatusId = 1;

        private const string TenderTypId = "2";

        private const string AgencyCode = "agency code";
        private const string AgencyAddress = "agency address";
        private const string AgencyFax = "agency fax";
        private const string AgencyPhone = "agency phone";
        private const string AgencyEmail = "agency email";

        private const int Cr = 1;

        private readonly List<int> _activitiesIds = new List<int> { 1, 2, 3 };
        private readonly List<int> _areasIds = new List<int> { 1, 2, 3 };
        private readonly List<int> _constructionsWorkIds = new List<int> { 1, 2, 3 };
        private readonly List<int> _countriesIds = new List<int> { 1, 2, 3 };
        private readonly List<int> _maintenanceWorkIds = new List<int> { 1, 2, 3 };

        private readonly AnnouncementTemplateDefaults announcementTemplateDefaults = new AnnouncementTemplateDefaults();


        [Fact]
        public void Should_Empty_Construct_AnnouncementSupplierTemplate()
        {
            var announcementSupplierTemplate = new Core.Entities.AnnouncementSupplierTemplate();

            announcementSupplierTemplate.ShouldNotBeNull();
        }

        [Fact]
        public void Should_CreateAnnouncementSupplierTemplate()
        {
            var announcement = CreateAnnouncement();

            announcement.ShouldNotBeNull();
            announcement.AnnouncementName.ShouldBe(Name);
            announcement.TenderTypesId.ShouldBe(TenderTypId);
            announcement.IntroAboutAnnouncementTemplate.ShouldBe(IntroAboutAnnouncementTemplate);
            announcement.Descriptions.ShouldBe(Descriptions);
            announcement.Details.ShouldBe(Details);
            announcement.ActivityDescription.ShouldBe(ActivityDescription);
            announcement.InsidKsa.ShouldBe(IsInsideKsa);
            announcement.BranchId.ShouldBe(BranchId);
            announcement.AgencyCode.ShouldBe(AgencyCode);
            announcement.AgencyAddress.ShouldBe(AgencyAddress);
            announcement.AgencyFax.ShouldBe(AgencyFax);
            announcement.AgencyPhone.ShouldBe(AgencyPhone);
            announcement.AgencyEmail.ShouldBe(AgencyEmail);

            announcement.IsEffectiveList.ShouldBe(true);
            announcement.RequirementConditionsToJoinList.ShouldBeNull();
            announcement.IsRequiredAttachmentToJoinList.ShouldBe(false);
            announcement.RequiredAttachment.ShouldBeNull();

            announcement.AnnouncementActivities.Count.ShouldBe(3);
            announcement.AnnouncementActivities[0].ActivityId.ShouldBe(1);
            announcement.AnnouncementActivities[1].ActivityId.ShouldBe(2);
            announcement.AnnouncementActivities[2].ActivityId.ShouldBe(3);
            announcement.AnnouncementConstructionWorks.Count.ShouldBe(3);
            announcement.AnnouncementConstructionWorks[0].ConstructionWorkId.ShouldBe(1);
            announcement.AnnouncementConstructionWorks[1].ConstructionWorkId.ShouldBe(2);
            announcement.AnnouncementConstructionWorks[2].ConstructionWorkId.ShouldBe(3);
            announcement.AnnouncementMaintenanceRunnigWorks.Count.ShouldBe(3);
            announcement.AnnouncementMaintenanceRunnigWorks[0].MaintenanceRunningWorkId.ShouldBe(1);
            announcement.AnnouncementMaintenanceRunnigWorks[1].MaintenanceRunningWorkId.ShouldBe(2);
            announcement.AnnouncementMaintenanceRunnigWorks[2].MaintenanceRunningWorkId.ShouldBe(3);
            announcement.AnnouncementAreas.Count.ShouldBe(3);
            announcement.AnnouncementAreas[0].AreaId.ShouldBe(1);
            announcement.AnnouncementAreas[1].AreaId.ShouldBe(2);
            announcement.AnnouncementAreas[2].AreaId.ShouldBe(3);
            announcement.AnnouncementCountries.Count.ShouldBe(3);
            announcement.AnnouncementCountries[0].CountryId.ShouldBe(1);
            announcement.AnnouncementCountries[1].CountryId.ShouldBe(2);
            announcement.AnnouncementCountries[2].CountryId.ShouldBe(3);

            announcement.Attachments.Count.ShouldBeGreaterThanOrEqualTo(1);
            announcement.Attachments[0].Name.ShouldBe("name");

            announcement.StatusId.ShouldBe((int)Enums.AnnouncementStatus.UnderCreation);

            announcement.CreatedAt.ShouldBeLessThanOrEqualTo(DateTime.Now);
            announcement.State.ShouldBe(ObjectState.Added);
            announcement.IsActive.ShouldBe(true);

            announcement.CreatedById.ShouldBe(Cr);

            announcement.AnnouncementTemplateListType.ShouldBeNull();
            announcement.Agency.ShouldBeNull();
            announcement.Branch.ShouldBeNull();
            announcement.AnnouncementTemplateSuppliersListTypeId.ShouldBeNull();
            announcement.AnnouncementId.ShouldBe(0);
        }

        [Fact]
        public void Should_UpdateAnnouncementSupplierTemplateRelations()
        {
            var announcement = CreateAnnouncement();

            announcement = announcement.UpdateAnnouncementSupplierTemplateRelations(_areasIds, _activitiesIds,
                _constructionsWorkIds, _maintenanceWorkIds, false, "det", "Desc", _countriesIds);

            announcement.ShouldNotBeNull();
            announcement.InsidKsa.ShouldBe(false);
            announcement.Details.ShouldBe("det");
            announcement.ActivityDescription.ShouldBe("Desc");

            announcement.AnnouncementAreas.Count.ShouldBe(3);
            announcement.AnnouncementAreas[0].AreaId.ShouldBe(1);
            announcement.AnnouncementAreas[1].AreaId.ShouldBe(2);
            announcement.AnnouncementAreas[2].AreaId.ShouldBe(3);
            announcement.AnnouncementActivities.Count.ShouldBe(6);
            announcement.AnnouncementActivities[0].ActivityId.ShouldBe(1);
            announcement.AnnouncementActivities[1].ActivityId.ShouldBe(2);
            announcement.AnnouncementActivities[2].ActivityId.ShouldBe(3);
            announcement.AnnouncementConstructionWorks.Count.ShouldBe(6);
            announcement.AnnouncementConstructionWorks[0].ConstructionWorkId.ShouldBe(1);
            announcement.AnnouncementConstructionWorks[1].ConstructionWorkId.ShouldBe(2);
            announcement.AnnouncementConstructionWorks[2].ConstructionWorkId.ShouldBe(3);
            announcement.AnnouncementMaintenanceRunnigWorks.Count.ShouldBe(6);
            announcement.AnnouncementMaintenanceRunnigWorks[0].MaintenanceRunningWorkId.ShouldBe(1);
            announcement.AnnouncementMaintenanceRunnigWorks[1].MaintenanceRunningWorkId.ShouldBe(2);
            announcement.AnnouncementMaintenanceRunnigWorks[2].MaintenanceRunningWorkId.ShouldBe(3);
            announcement.AnnouncementCountries.Count.ShouldBe(6);
            announcement.AnnouncementCountries[0].CountryId.ShouldBe(1);
            announcement.AnnouncementCountries[1].CountryId.ShouldBe(2);
            announcement.AnnouncementCountries[2].CountryId.ShouldBe(3);
        }

        [Fact]
        public void Should_UpdateSuppliersTemplateAttachments()
        {
            var announcement = CreateAnnouncement();

            var name = "name";
            var fileNetReferenceId = "123";
            var attachmentType = 1;

            var atta = new AnnouncementSuppliersTemplateAttachment(name, fileNetReferenceId, attachmentType);

            announcement.UpdateSuppliersTemplateAttachments(new List<AnnouncementSuppliersTemplateAttachment> { atta }, Cr);

            announcement.ShouldNotBeNull();
            announcement.Attachments.Count.ShouldBe(2);
            announcement.Attachments[0].Name.ShouldBe(name);
            announcement.Attachments[1].Name.ShouldBe(name);
        }

        [Fact]
        public void Should_UpdateAnnouncementSupplierTemplateData()
        {
            var announcement = CreateAnnouncement();

            var model = new CreateAnnouncementSupplierTemplateModel
            {
                AnnouncementSupplierTemplateName = Name,
                TenderTypesId = TenderTypId,
                IntroAboutAnnouncementTemplate = IntroAboutAnnouncementTemplate,
                Descriptions = Descriptions,
                Details = Details,
                ActivityDescription = ActivityDescription,
                InsideKsa = IsInsideKsa,
                BranchId = BranchId,
                AgencyCode = AgencyCode,
                AgencyAddress = AgencyAddress,
                AgencyFax = AgencyFax,
                AgencyPhone = AgencyPhone,
                AgencyEmail = AgencyEmail,

                IsRequiredAttachmentToJoinList = false,

                AreasIds = _areasIds,
                ActivityIds = _activitiesIds,
                ConstructionWorkIds = _constructionsWorkIds,
                MaintenanceRunnigWorkIds = _maintenanceWorkIds,
                CountriesIds = _countriesIds
            };

            var name = "name";
            var fileNetReferenceId = "123";
            var attachmentType = 1;

            var updateAtta = new AnnouncementSuppliersTemplateAttachment("NewName", fileNetReferenceId, attachmentType);

            announcement = announcement.UpdateAnnouncementSupplierTemplateData(
                model, new List<AnnouncementSuppliersTemplateAttachment>
                {
                    updateAtta
                },
                Cr);

            announcement.ShouldNotBeNull();
            announcement.AnnouncementName.ShouldBe(Name);
            announcement.TenderTypesId.ShouldBe(TenderTypId);
            announcement.IntroAboutAnnouncementTemplate.ShouldBe(IntroAboutAnnouncementTemplate);
            announcement.Descriptions.ShouldBe(Descriptions);
            announcement.Details.ShouldBe(Details);
            announcement.ActivityDescription.ShouldBe(ActivityDescription);
            announcement.InsidKsa.ShouldBe(IsInsideKsa);
            announcement.BranchId.ShouldBe(BranchId);
            announcement.AgencyCode.ShouldBe(AgencyCode);
            announcement.AgencyAddress.ShouldBe(AgencyAddress);
            announcement.AgencyFax.ShouldBe(AgencyFax);
            announcement.AgencyPhone.ShouldBe(AgencyPhone);
            announcement.AgencyEmail.ShouldBe(AgencyEmail);

            announcement.IsEffectiveList.ShouldBe(true);
            announcement.RequirementConditionsToJoinList.ShouldBeNull();
            announcement.IsRequiredAttachmentToJoinList.ShouldBe(false);
            announcement.RequiredAttachment.ShouldBeNull();

            announcement.AnnouncementActivities.Count.ShouldBe(6);
            announcement.AnnouncementActivities[0].ActivityId.ShouldBe(1);
            announcement.AnnouncementActivities[1].ActivityId.ShouldBe(2);
            announcement.AnnouncementActivities[2].ActivityId.ShouldBe(3);
            announcement.AnnouncementConstructionWorks.Count.ShouldBe(6);
            announcement.AnnouncementConstructionWorks[0].ConstructionWorkId.ShouldBe(1);
            announcement.AnnouncementConstructionWorks[1].ConstructionWorkId.ShouldBe(2);
            announcement.AnnouncementConstructionWorks[2].ConstructionWorkId.ShouldBe(3);
            announcement.AnnouncementMaintenanceRunnigWorks.Count.ShouldBe(6);
            announcement.AnnouncementMaintenanceRunnigWorks[0].MaintenanceRunningWorkId.ShouldBe(1);
            announcement.AnnouncementMaintenanceRunnigWorks[1].MaintenanceRunningWorkId.ShouldBe(2);
            announcement.AnnouncementMaintenanceRunnigWorks[2].MaintenanceRunningWorkId.ShouldBe(3);
            announcement.AnnouncementAreas.Count.ShouldBe(3);
            announcement.AnnouncementAreas[0].AreaId.ShouldBe(1);
            announcement.AnnouncementAreas[1].AreaId.ShouldBe(2);
            announcement.AnnouncementAreas[2].AreaId.ShouldBe(3);
            announcement.AnnouncementCountries.Count.ShouldBe(6);
            announcement.AnnouncementCountries[0].CountryId.ShouldBe(1);
            announcement.AnnouncementCountries[1].CountryId.ShouldBe(2);
            announcement.AnnouncementCountries[2].CountryId.ShouldBe(3);

            announcement.Attachments.Count.ShouldBeGreaterThanOrEqualTo(2);
            announcement.Attachments[0].Name.ShouldBe(name);
            announcement.Attachments[1].Name.ShouldBe("NewName");

            announcement.StatusId.ShouldBe((int)Enums.AnnouncementStatus.UnderCreation);

            announcement.CreatedAt.ShouldBeLessThanOrEqualTo(DateTime.Now);
            announcement.IsActive.ShouldBe(true);

            announcement.CreatedById.ShouldBe(Cr);
        }

        [Fact]
        public void Should_UpdateAnnouncementSupplierTemplateListData()
        {
            var announcement = CreateAnnouncement();

            var model = new UpdateAnnouncementSupplierTemplateModel
            {
                AnnouncementTemplateSuppliersListTypeId = 1,
                BranchId = BranchId,
                AgencyCode = AgencyCode,
                AgencyAddress = AgencyAddress,
                AgencyFax = AgencyFax,
                AgencyPhone = AgencyPhone,
                AgencyEmail = AgencyEmail
            };

            var name = "name";
            var fileNetReferenceId = "123";
            var attachmentType = 1;

            var atta = new AnnouncementSuppliersTemplateAttachment(name, fileNetReferenceId, attachmentType);

            announcement.UpdateAnnouncementSupplierTemplateListData(
                model, new List<AnnouncementSuppliersTemplateAttachment>
                {
                    atta
                },
                Cr);

            announcement.ShouldNotBeNull();
            announcement.IsEffectiveList.ShouldBe(true);
            announcement.EffectiveListDate.ShouldBeNull();
            announcement.BranchId.ShouldBe(BranchId);
            announcement.AgencyCode.ShouldBe(AgencyCode);
            announcement.AgencyAddress.ShouldBe(AgencyAddress);
            announcement.AgencyFax.ShouldBe(AgencyFax);
            announcement.AgencyPhone.ShouldBe(AgencyPhone);
            announcement.AgencyEmail.ShouldBe(AgencyEmail);

            announcement.Attachments.Count.ShouldBeGreaterThanOrEqualTo(1);
            announcement.Attachments[0].Name.ShouldBe(name);
        }

        [Fact]
        public void Should_Delete()
        {
            var announcement = CreateAnnouncement();

            announcement.Delete();

            announcement.ShouldNotBeNull();
            announcement.State.ShouldBe(ObjectState.Deleted);
        }

        [Fact]
        public void Should_UpdateStatus()
        {
            var announcement = CreateAnnouncement();

            announcement.Test_UpdateStatus(3);

            announcement.ShouldNotBeNull();
            announcement.StatusId.ShouldBe(3);
        }

        [Fact]
        public void Should_UpdateAnnouncementStatus_Returns_Approved()
        {
            var announcement = CreateAnnouncement();

            announcement.UpdateAnnouncementStatus(Enums.AnnouncementSupplierTemplateStatus.Approved, "reason", Cr);

            announcement.ShouldNotBeNull();
            announcement.StatusId.ShouldBe(3);
            announcement.CancelationReason.ShouldBe("reason");
        }

        [Fact]
        public void Should_Test_UpdatePublishDatetooldDate()
        {
            var date = DateTime.Now;
            var announcement = CreateAnnouncement();

            announcement.Test_UpdatePublishDatetooldDate(date);
            // Assert
            announcement.ShouldNotBeNull();
            announcement.PublishedDate.HasValue.ShouldBeTrue();
            announcement.PublishedDate.Value.ShouldBeGreaterThanOrEqualTo(date);
        }

        [Fact]
        public void Should_DeActive()
        {
            var announcement = CreateAnnouncement();

            announcement.DeActive();

            announcement.IsActive.ShouldBe(false);
        }

        [Fact]
        public void Should_Active()
        {
            var announcement = CreateAnnouncement();

            announcement.SetActive();

            announcement.IsActive.ShouldBe(true);
        }

        [Fact]
        public void Should_SetApprovalStatus()
        {
            var announcement = CreateAnnouncement();

            announcement.SetApprovalStatus(Enums.AnnouncementSupplierTemplateStatus.Approved);

            announcement.ReadyForApproval.ShouldBe(3);
        }

        [Fact]
        public void Should_RemoveApprovalStatus()
        {
            var announcement = CreateAnnouncement();

            announcement.RemoveApprovalStatus();

            announcement.ReadyForApproval.ShouldBe(0);
        }

        [Fact]
        public void Should_AddJoinRequest()
        {
            var announcement = CreateAnnouncement();

            var name = "name";
            var fileNetReferenceId = "123";

            var atta = new AnnouncementTemplateJoinRequestAttachment(name, fileNetReferenceId);

            announcement.AddJoinRequest(new List<AnnouncementTemplateJoinRequestAttachment> { atta }, 123, Cr.ToString());

            announcement.AnnouncementJoinRequests.Count.ShouldBe(1);
        }



        [Fact]
        public void Should_Set_PublishDate()
        {
            var announcement = CreateAnnouncement();

            announcement.SetPublishedDate();


            announcement.ShouldNotBeNull();
            announcement.PublishedDate.HasValue.ShouldBeTrue();
            announcement.PublishedDate.Value.ShouldBeLessThanOrEqualTo(DateTime.Now);
        }

        [Fact]
        public void Should_SetApprovedBy()
        {
            var announcement = CreateAnnouncement();

            announcement.SetApprovedBy("256254");


            announcement.ShouldNotBeNull();
            announcement.ApprovedBy.ShouldBe("256254");
        }

        [Fact]
        public void Should_SendAnnouncementForApproval_Throw_Exception_When_Announcement_Status_Is_Not_UnderConstruction()
        {
            var announcement = CreateAnnouncement();

            announcement.Test_UpdateStatus((int)Enums.AnnouncementStatus.Pending);

            announcement.ShouldNotBeNull();
            Action action = () => { announcement.SendAnnouncementForApproval(); };
            var exception = action.ShouldThrow(typeof(BusinessRuleException));
            exception.Message.ShouldBe("يجب ان يكون الإعلان تحت الإنشاء");
        }

        [Fact]
        public void Should_SendAnnouncementForApproval_Throw_Exception_When_Announcement_EffectiveListDate_GreaterThan_Now()
        {
            var announcement = CreateAnnouncement();
            announcement.Test_EditEffectiveListDate(DateTime.Now.AddDays(30));

            announcement.ShouldNotBeNull();
            Action action = () => { announcement.SendAnnouncementForApproval(); };
            var exception = action.ShouldThrow(typeof(BusinessRuleException));
            exception.Message.ShouldBe("يجب ان لا يكون الإعلان منتهي ");
        }


        [Fact]
        public void Should_SendAnnouncementForApproval()
        {
            var announcement = CreateAnnouncement();

            announcement.Test_UpdateStatus((int)Enums.AnnouncementStatus.UnderCreation);

            announcement.SendAnnouncementForApproval();

            announcement.ShouldNotBeNull();
            announcement.StatusId.ShouldBe((int)Enums.AnnouncementStatus.Pending);
        }

        [Fact]
        public void Should_SetReferenceNumber()
        {
            var announcement = CreateAnnouncement();

            announcement.SetReferenceNumber("256254");

            announcement.ShouldNotBeNull();
            announcement.ReferenceNumber.ShouldBe("256254");
        }


        [Fact]
        public void Should_ApproveAnnouncement_Throw_Exception_When_Announcement_EffectiveListDate_LessThan_Now()
        {
            var announcement = CreateAnnouncement();
            announcement.Test_EditEffectiveListDate(DateTime.Now.AddDays(-30));

            announcement.ShouldNotBeNull();
            Action action = () => { announcement.ApproveAnnouncement(); };
            var exception = action.ShouldThrow(typeof(BusinessRuleException));
            exception.Message.ShouldBe("يجب ان لا يكون الإعلان منتهي ");
        }


        [Fact]
        public void Should_ApproveAnnouncement()
        {
            var announcement = CreateAnnouncement();

            announcement.ApproveAnnouncement();

            announcement.ShouldNotBeNull();
            announcement.StatusId.ShouldBe((int)Enums.AnnouncementStatus.Approved);
        }

        [Fact]
        public void Should_DeleteAnnouncement()
        {
            var announcement = CreateAnnouncement();

            announcement.DeleteAnnouncement();

            announcement.ShouldNotBeNull();
            announcement.IsActive.ShouldNotBeNull();
            announcement.IsActive.HasValue.ShouldBeTrue();
            announcement.IsActive.Value.ShouldBeFalse();
        }

        [Fact]
        public void Should_DeleteAnnouncement_Throw_Exception_When_Announcement_Status_Is_Not_UnderCreation()
        {
            var announcement = CreateAnnouncement();

            announcement.Test_UpdateStatus((int)Enums.AnnouncementStatus.Pending);

            announcement.ShouldNotBeNull();
            Action action = () => { announcement.DeleteAnnouncement(); };
            var exception = action.ShouldThrow(typeof(BusinessRuleException));
            exception.Message.ShouldBe("يجب ان تكون حالة الإعلان تحت الإنشاء");
        }

        [Fact]
        public void Should_Add_Announcement_History_With_UserId()
        {
            var announcement = CreateAnnouncement();

            announcement.AddAnnouncementHistoy(123);

            announcement.ShouldNotBeNull();
            announcement.AnnouncementHistories.Count.ShouldBe(1);
        }

        [Fact]
        public void Should_Add_Announcement_History_With_UserId_And_RejectionReason()
        {
            var announcement = CreateAnnouncement();

            announcement.AddAnnouncementHistoy(123, "reason");

            announcement.ShouldNotBeNull();
            announcement.AnnouncementHistories.Count.ShouldBe(1);
        }

        [Fact]
        public void Should_UpdateTemplate()
        {
            var announcement = CreateAnnouncement();

            announcement.UpdateTemplate();

            announcement.ShouldNotBeNull();
        }

        #region Creation Helpers

        private Core.Entities.AnnouncementSupplierTemplate CreateAnnouncement()
        {
            var announcement = new Core.Entities.AnnouncementSupplierTemplate();
            var model = new CreateAnnouncementSupplierTemplateModel
            {
                AnnouncementSupplierTemplateName = Name,
                TenderTypesId = TenderTypId,
                IntroAboutAnnouncementTemplate = IntroAboutAnnouncementTemplate,
                Descriptions = Descriptions,
                Details = Details,
                ActivityDescription = ActivityDescription,
                InsideKsa = IsInsideKsa,
                BranchId = BranchId,
                AgencyCode = AgencyCode,
                AgencyAddress = AgencyAddress,
                AgencyFax = AgencyFax,
                AgencyPhone = AgencyPhone,
                AgencyEmail = AgencyEmail,

                IsRequiredAttachmentToJoinList = false,

                AreasIds = _areasIds,
                ActivityIds = _activitiesIds,
                ConstructionWorkIds = _constructionsWorkIds,
                MaintenanceRunnigWorkIds = _maintenanceWorkIds,
                CountriesIds = _countriesIds
            };

            var name = "name";
            var fileNetReferenceId = "123";
            var attachmentType = 1;

            var atta = new AnnouncementSuppliersTemplateAttachment(name, fileNetReferenceId, attachmentType);

            announcement.CreateAnnouncementSupplierTemplate(
                model, new List<AnnouncementSuppliersTemplateAttachment>
                {
                    atta
                },
                Cr);

            return announcement;
        }


        [Fact]
        public void Should_ExtendAnnouncementSupplierTemplate()
        {
            var announcement = announcementTemplateDefaults.ReturnAnnouncementSupplierTemplateDefaults();
            var announcementTemplateData = announcementTemplateDefaults.GetExtendAnnouncementSupplierTemplateModelData();
            announcement.ExtendAnnouncementSupplierTemplateData(announcementTemplateDefaults.GetExtendAnnouncementSupplierTemplateModelData(), new List<AnnouncementSuppliersTemplateAttachment>(), 1);
            announcement.ShouldNotBeNull();
            announcement.TemplateExtendMechanism.ShouldBe(announcementTemplateData.TemplateExtendMechanism);
            announcement.AnnouncementName.ShouldBe(announcementTemplateData.AnnouncementSupplierTemplateName);
            announcement.IntroAboutAnnouncementTemplate.ShouldBe(announcementTemplateData.IntroAboutAnnouncementTemplate);
            announcement.Descriptions.ShouldBe(announcementTemplateData.Descriptions);
            announcement.Details.ShouldBe(announcementTemplateData.Details);
            announcement.ActivityDescription.ShouldBe(announcementTemplateData.ActivityDescription);
            announcement.InsidKsa.ShouldBe(announcementTemplateData.InsideKsa);
            announcement.BranchId.ShouldBe(announcementTemplateData.BranchId);
            announcement.AgencyCode.ShouldBe(announcementTemplateData.AgencyCode);
            announcement.AgencyAddress.ShouldBe(announcementTemplateData.AgencyAddress);
            announcement.AgencyFax.ShouldBe(announcementTemplateData.AgencyFax);
            announcement.AgencyPhone.ShouldBe(announcementTemplateData.AgencyPhone);
            announcement.AgencyEmail.ShouldBe(announcementTemplateData.AgencyEmail);
            announcement.StatusId.ShouldBe((int)Enums.AnnouncementStatus.Approved);
            announcement.CreatedAt.ShouldBeLessThanOrEqualTo(DateTime.Now);
            announcement.State.ShouldBe(ObjectState.Added);
            announcement.IsActive.ShouldBe(true);
        }

        #endregion

        [Fact]
        public void Should_AddPublicAnnouncementListToAgency()
        {
            var announcement = CreateAnnouncement();
            announcement.AddPublicAnnouncementListToAgency(AgencyCode);
            announcement.LinkedAgenciesAnnouncements.ShouldNotBeNull();
        }

        [Fact]
        public void Should_RemovePublicAnnouncementListFromAgency()
        {
            var announcement = CreateAnnouncement();
            announcement.RemovePublicAnnouncementListFromAgency();
            announcement.LinkedAgenciesAnnouncements.ShouldNotBeNull();
        }

        [Fact]
        public void Should_SetLinkedAgencies()
        {
            var announcement = CreateAnnouncement();
            announcement.SetLinkedAgencies(new LinkedAgenciesAnnouncementTemplate(AgencyCode));
            announcement.LinkedAgenciesAnnouncements.ShouldNotBeNull();
        }
    }
}