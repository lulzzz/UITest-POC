using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernal.Exceptions;
using MOF.Etimad.Monafasat.SharedKernel;
using Shouldly;
using System;
using System.Collections.Generic;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Announcements
{
    public class AnnouncementTests
    {
        private const string Name = "test announcement";
        private const int Period = 30;
        private const int TenderTypId = 2;
        private const int TenderReasonId = 2;
        private const string IntroAboutTender = "intro";
        private const bool IsInsideKsa = true;
        private const string Details = "test details";
        private const string ActivityDescription = "activity description";
        private const int BranchId = 1;
        private const string AgencyCode = "agency code";
        private readonly List<int> _activitiesIds = new List<int> { 1, 2, 3 };
        private readonly List<int> _areasIds = new List<int> { 1, 2, 3 };
        private readonly List<int> _constructionsWorkIds = new List<int> { 1, 2, 3 };
        private readonly List<int> _countriesIds = new List<int> { 1, 2, 3 };
        private readonly List<int> _maintenanceWorkIds = new List<int> { 1, 2, 3 };

        [Fact]
        public void Should_Construct_Announcement()
        {
            // Act
            var announcement = new Announcement(Name, Period, TenderTypId, TenderReasonId, IntroAboutTender, IsInsideKsa, Details,
                ActivityDescription, BranchId, AgencyCode, _activitiesIds,
                _constructionsWorkIds, _maintenanceWorkIds, _areasIds, _countriesIds);
            // Assert
            announcement.ShouldNotBeNull();
            announcement.AnnouncementName.ShouldBe(Name);
            announcement.AnnouncementPeriod.ShouldBe(Period);
            announcement.TenderTypeId.ShouldBe(TenderTypId);
            announcement.ReasonForSelectingTenderTypeId.ShouldBe(TenderReasonId);

            announcement.IntroAboutTender.ShouldBe(IntroAboutTender);
            announcement.InsidKsa.ShouldBe(IsInsideKsa);
            announcement.Details.ShouldBe(Details);
            announcement.ActivityDescription.ShouldBe(ActivityDescription);
            announcement.StatusId.ShouldBe((int)Enums.AnnouncementStatus.UnderCreation);
            announcement.BranchId.ShouldBe(BranchId);
            announcement.AgencyCode.ShouldBe(AgencyCode);
            announcement.TenderTypeId.ShouldBe((int)Enums.TenderType.NewDirectPurchase);
            announcement.ReasonForSelectingTenderTypeId
                .ShouldBe((int)Enums.ReasonForPurchaseTenderType
                    .BusinessAndProcurementAreAvailableToOneContractorCSupplierAndHaveNoAcceptableAlternative);

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

            announcement.AnnouncementStatusId.ShouldBe((int)Enums.AnnouncementStatus.UnderCreation);

            announcement.CreatedAt.ShouldBeLessThanOrEqualTo(DateTime.Now);
            announcement.State.ShouldBe(ObjectState.Added);
            announcement.IsActive.ShouldBe(true);
        }

        [Fact]
        public void Should_Update_Announcement()
        {
            // Arrange
            var announcement = new Announcement(Name, Period, TenderTypId, TenderReasonId, IntroAboutTender, IsInsideKsa, Details,
                ActivityDescription, BranchId, AgencyCode, _activitiesIds,
                _constructionsWorkIds, _maintenanceWorkIds, _areasIds, _countriesIds);
            var updatedName = "updated name";
            var updatedPeriod = 50;
            var updatedTenderTypeId = 2;
            var updatedPReason = 2;
            var updatedIntro = "updated intro";
            var updatedIsInsideKsa = false;
            var updatedDetails = "updated details";
            var updatedDescription = "updated description";
            var updatedBranch = 2;
            var updatedAgencyCode = "updated code";
            var updatedActivitiesIds = new List<int> { 4, 5, 6 };
            var updatedConstructionsWorkIds = new List<int> { 4, 5, 6 };
            var updatedMaintenanceWorkIds = new List<int> { 4, 5, 6 };
            var updatedAreasIds = new List<int> { 4, 5, 6 };
            var updatedCountriesIds = new List<int> { 4, 5, 6 };

            // Act
            announcement.UpdateAnnouncement(updatedName, updatedPeriod, updatedTenderTypeId, updatedPReason, updatedIntro, updatedIsInsideKsa,
                updatedDetails, updatedDescription
                , updatedBranch, updatedAgencyCode, updatedActivitiesIds, updatedConstructionsWorkIds,
                updatedMaintenanceWorkIds, updatedAreasIds, updatedCountriesIds);
            // Assert
            announcement.ShouldNotBeNull();
            announcement.AnnouncementName.ShouldBe(updatedName);
            announcement.AnnouncementPeriod.ShouldBe(50);
            announcement.IntroAboutTender.ShouldBe(updatedIntro);
            announcement.InsidKsa.ShouldBe(updatedIsInsideKsa);
            announcement.Details.ShouldBe(updatedDetails);
            announcement.ActivityDescription.ShouldBe(updatedDescription);
            announcement.StatusId.ShouldBe((int)Enums.AnnouncementStatus.UnderCreation);
            announcement.BranchId.ShouldBe(updatedBranch);
            announcement.AgencyCode.ShouldBe(updatedAgencyCode);
            announcement.TenderTypeId.ShouldBe((int)Enums.TenderType.NewDirectPurchase);
            announcement.ReasonForSelectingTenderTypeId
                .ShouldBe((int)Enums.ReasonForPurchaseTenderType
                    .BusinessAndProcurementAreAvailableToOneContractorCSupplierAndHaveNoAcceptableAlternative);
            announcement.AnnouncementActivities.Count.ShouldBe(6);
            announcement.AnnouncementActivities[0].State.ShouldBe(ObjectState.Deleted);
            announcement.AnnouncementActivities[1].State.ShouldBe(ObjectState.Deleted);
            announcement.AnnouncementActivities[2].State.ShouldBe(ObjectState.Deleted);
            announcement.AnnouncementActivities[3].ActivityId.ShouldBe(4);
            announcement.AnnouncementActivities[4].ActivityId.ShouldBe(5);
            announcement.AnnouncementActivities[5].ActivityId.ShouldBe(6);
            announcement.AnnouncementConstructionWorks.Count.ShouldBe(6);
            announcement.AnnouncementConstructionWorks[0].State.ShouldBe(ObjectState.Deleted);
            announcement.AnnouncementConstructionWorks[1].State.ShouldBe(ObjectState.Deleted);
            announcement.AnnouncementConstructionWorks[2].State.ShouldBe(ObjectState.Deleted);
            announcement.AnnouncementConstructionWorks[3].ConstructionWorkId.ShouldBe(4);
            announcement.AnnouncementConstructionWorks[4].ConstructionWorkId.ShouldBe(5);
            announcement.AnnouncementConstructionWorks[5].ConstructionWorkId.ShouldBe(6);
            announcement.AnnouncementMaintenanceRunnigWorks.Count.ShouldBe(6);
            announcement.AnnouncementMaintenanceRunnigWorks[0].State.ShouldBe(ObjectState.Deleted);
            announcement.AnnouncementMaintenanceRunnigWorks[1].State.ShouldBe(ObjectState.Deleted);
            announcement.AnnouncementMaintenanceRunnigWorks[2].State.ShouldBe(ObjectState.Deleted);
            announcement.AnnouncementMaintenanceRunnigWorks[3].MaintenanceRunningWorkId.ShouldBe(4);
            announcement.AnnouncementMaintenanceRunnigWorks[4].MaintenanceRunningWorkId.ShouldBe(5);
            announcement.AnnouncementMaintenanceRunnigWorks[5].MaintenanceRunningWorkId.ShouldBe(6);
            announcement.AnnouncementAreas.Count.ShouldBe(6);
            announcement.AnnouncementAreas[0].State.ShouldBe(ObjectState.Deleted);
            announcement.AnnouncementAreas[1].State.ShouldBe(ObjectState.Deleted);
            announcement.AnnouncementAreas[2].State.ShouldBe(ObjectState.Deleted);
            announcement.AnnouncementAreas[3].AreaId.ShouldBe(4);
            announcement.AnnouncementAreas[4].AreaId.ShouldBe(5);
            announcement.AnnouncementAreas[5].AreaId.ShouldBe(6);
            announcement.AnnouncementCountries.Count.ShouldBe(6);
            announcement.AnnouncementCountries[0].State.ShouldBe(ObjectState.Deleted);
            announcement.AnnouncementCountries[1].State.ShouldBe(ObjectState.Deleted);
            announcement.AnnouncementCountries[2].State.ShouldBe(ObjectState.Deleted);
            announcement.AnnouncementCountries[3].CountryId.ShouldBe(4);
            announcement.AnnouncementCountries[4].CountryId.ShouldBe(5);
            announcement.AnnouncementCountries[5].CountryId.ShouldBe(6);
            announcement.IsActive.ShouldBe(true);
        }

        [Fact]
        public void Should_Delete_Announcement()
        {
            // Arrange
            var announcement = new Announcement(Name, Period, TenderTypId, TenderReasonId, IntroAboutTender, IsInsideKsa, Details,
                ActivityDescription, BranchId, AgencyCode, _activitiesIds,
                _constructionsWorkIds, _maintenanceWorkIds, _areasIds, _countriesIds);
            // Act
            announcement.Delete();
            // Assert
            announcement.State.ShouldBe(ObjectState.Deleted);
        }

        [Fact]
        public void Should_DeActive_Announcement()
        {
            // Arrange
            var announcement = new Announcement(Name, Period, TenderTypId, TenderReasonId, IntroAboutTender, IsInsideKsa, Details,
                ActivityDescription, BranchId, AgencyCode, _activitiesIds,
                _constructionsWorkIds, _maintenanceWorkIds, _areasIds, _countriesIds);
            // Act
            announcement.DeActive();
            // Assert
            announcement.IsActive.ShouldBe(false);
        }

        [Fact]
        public void Should_Active_Announcement()
        {
            // Arrange
            var announcement = new Announcement(Name, Period, TenderTypId, TenderReasonId, IntroAboutTender, IsInsideKsa, Details,
                ActivityDescription, BranchId, AgencyCode, _activitiesIds,
                _constructionsWorkIds, _maintenanceWorkIds, _areasIds, _countriesIds);
            announcement.DeActive();
            // Act
            announcement.SetActive();
            // Assert
            announcement.IsActive.ShouldBe(true);
        }

        [Fact]
        public void Should_Add_JoinRequest()
        {
            // Arrange
            var announcement = new Announcement(Name, Period, TenderTypId, TenderReasonId, IntroAboutTender, IsInsideKsa, Details,
                ActivityDescription, BranchId, AgencyCode, _activitiesIds,
                _constructionsWorkIds, _maintenanceWorkIds, _areasIds, _countriesIds);
            var joinRequest = new AnnouncementJoinRequest();
            // Act
            announcement.AddJoinRequest(joinRequest);
            // Assert
            announcement.ShouldNotBeNull();
            announcement.AnnouncementJoinRequests.Count.ShouldBe(1);
        }

        [Fact]
        public void Should_Withdraw_JoinRequest()
        {
            // Arrange
            var announcement = new Announcement(Name, Period, TenderTypId, TenderReasonId, IntroAboutTender, IsInsideKsa, Details,
                ActivityDescription, BranchId, AgencyCode, _activitiesIds,
                _constructionsWorkIds, _maintenanceWorkIds, _areasIds, _countriesIds);
            var joinRequest = new AnnouncementJoinRequest(1, "123", 1);
            announcement.AddJoinRequest(joinRequest);
            // Act
            announcement.WithdroawJoinRequest("123");
            // Assert
            announcement.ShouldNotBeNull();
            announcement.AnnouncementJoinRequests.Count.ShouldBe(1);
            announcement.AnnouncementJoinRequests[0].StatusId
                .ShouldBe((int)Enums.AnnouncementJoinRequestStatus.WithDraw);
        }

        [Fact]
        public void Should_Set_PublishDate_To_Announcement()
        {
            // Arrange
            var announcement = new Announcement(Name, Period, TenderTypId, TenderReasonId, IntroAboutTender, IsInsideKsa, Details,
                ActivityDescription, BranchId, AgencyCode, _activitiesIds,
                _constructionsWorkIds, _maintenanceWorkIds, _areasIds, _countriesIds);
            // Act
            announcement.SetPublishedDate();
            // Assert
            announcement.ShouldNotBeNull();
            announcement.PublishedDate.HasValue.ShouldBeTrue();
            announcement.PublishedDate.Value.ShouldBeLessThanOrEqualTo(DateTime.Now);
        }

        [Fact]
        public void Should_Set_ApprovedBy_To_Announcement()
        {
            // Arrange
            var announcement = new Announcement(Name, Period, TenderTypId, TenderReasonId, IntroAboutTender, IsInsideKsa, Details,
                ActivityDescription, BranchId, AgencyCode, _activitiesIds,
                _constructionsWorkIds, _maintenanceWorkIds, _areasIds, _countriesIds);
            // Act
            announcement.SetApprovedBy("user");
            // Assert
            announcement.ShouldNotBeNull();
            announcement.ApprovedBy.ShouldBe("user");
        }

        [Fact]
        public void Should_SendAnnouncementForApproval()
        {
            // Arrange
            var announcement = new Announcement(Name, Period, TenderTypId, TenderReasonId, IntroAboutTender, IsInsideKsa, Details,
                ActivityDescription, BranchId, AgencyCode, _activitiesIds,
                _constructionsWorkIds, _maintenanceWorkIds, _areasIds, _countriesIds);
            // Act
            announcement.SendAnnouncementForApproval();
            // Assert
            announcement.ShouldNotBeNull();
            announcement.StatusId.ShouldBe((int)Enums.AnnouncementStatus.Pending);
        }

        [Fact]
        public void
            Should_SendAnnouncementForApproval_Throw_Exception_When_Announcement_Status_Is_Not_UnderConstruction()
        {
            // Arrange
            var announcement = new Announcement(Name, Period, TenderTypId, TenderReasonId, IntroAboutTender, IsInsideKsa, Details,
                ActivityDescription, BranchId, AgencyCode, _activitiesIds,
                _constructionsWorkIds, _maintenanceWorkIds, _areasIds, _countriesIds)
            {
                StatusId = (int)Enums.AnnouncementStatus.Pending
            };

            // Assert
            announcement.ShouldNotBeNull();
            Action action = () => { announcement.SendAnnouncementForApproval(); };
            var exception = action.ShouldThrow(typeof(BusinessRuleException));
            exception.Message.ShouldBe("يجب ان يكون الإعلان تحت الإنشاء");
        }

        [Fact]
        public void
            Should_SendAnnouncementForApproval_Throw_Exception_When_Announcement_LastApplyingRequestsDate_LessThan_Now()
        {
            // Arrange
            var announcement = new Announcement(Name, Period, TenderTypId, TenderReasonId, IntroAboutTender, IsInsideKsa, Details,
                ActivityDescription, BranchId, AgencyCode, _activitiesIds,
                _constructionsWorkIds, _maintenanceWorkIds, _areasIds, _countriesIds);
            //todo: need to set the publish date to a past date, this can not be done due to DateTime.Now() needs to be an injected service
            // Assert
            // announcement.Test_UpdateStatus((int)Enums.AnnouncementStatus.Pending);
            announcement.Test_UpdatePublishDatetooldDate(DateTime.Now);
            announcement.ShouldNotBeNull();
            Action action = () => { announcement.SendAnnouncementForApproval(); };
            var exception = action.ShouldThrow(typeof(BusinessRuleException));
            exception.Message.ShouldBe("يجب ان لا يكون الإعلان منتهي ");
        }

        [Fact]
        public void Should_Set_Reference_Number()
        {
            // Arrange
            var announcement = new Announcement(Name, Period, TenderTypId, TenderReasonId, IntroAboutTender, IsInsideKsa, Details,
                ActivityDescription, BranchId, AgencyCode, _activitiesIds,
                _constructionsWorkIds, _maintenanceWorkIds, _areasIds, _countriesIds);
            // Act
            announcement.SetReferenceNumber("123");
            // Assert
            announcement.ShouldNotBeNull();
            announcement.ReferenceNumber.ShouldBe("123");
        }

        [Fact]
        public void Should_ApproveAnnouncement()
        {
            // Arrange
            var announcement = new Announcement(Name, Period, TenderTypId, TenderReasonId, IntroAboutTender, IsInsideKsa, Details,
                ActivityDescription, BranchId, AgencyCode, _activitiesIds,
                _constructionsWorkIds, _maintenanceWorkIds, _areasIds, _countriesIds)
            {
                StatusId = (int)Enums.AnnouncementStatus.Pending
            };
            // Act
            announcement.ApproveAnnouncement();
            // Assert
            announcement.ShouldNotBeNull();
            announcement.StatusId.ShouldBe((int)Enums.AnnouncementStatus.Approved);
        }

        [Fact]
        public void Should_ApproveAnnouncement_Throw_Exception_When_Announcement_Status_Is_Not_Pending()
        {
            // Arrange
            var announcement = new Announcement(Name, Period, TenderTypId, TenderReasonId, IntroAboutTender, IsInsideKsa, Details,
                ActivityDescription, BranchId, AgencyCode, _activitiesIds,
                _constructionsWorkIds, _maintenanceWorkIds, _areasIds, _countriesIds);

            // Assert
            announcement.ShouldNotBeNull();
            Action action = () => { announcement.ApproveAnnouncement(); };
            var exception = action.ShouldThrow(typeof(BusinessRuleException));
            exception.Message.ShouldBe("يجب ان يكون الإعلان بإنتظار الإعتماد");
        }



        [Fact]
        public void Should_ApproveAnnouncement_Throw_Exception_When_Announcement_LastApplyingRequestsDate_LessThan_Now()
        {
            // Arrange
            var announcement = new Announcement(Name, Period, TenderTypId, TenderReasonId, IntroAboutTender, IsInsideKsa, Details,
                ActivityDescription, BranchId, AgencyCode, _activitiesIds,
                _constructionsWorkIds, _maintenanceWorkIds, _areasIds, _countriesIds);
            //todo: need to set the publish date to a past date, this can not be done due to DateTime.Now() needs to be an injected service
            // Assert

            announcement.Test_UpdateStatus((int)Enums.AnnouncementStatus.Pending);
            announcement.Test_UpdatePublishDatetooldDate(DateTime.Now.AddDays(-33));
            announcement.ShouldNotBeNull();
            Action action = () => { announcement.Test_ApproveAnnouncementDate(); };
            var exception = action.ShouldThrow(typeof(BusinessRuleException));
            exception.Message.ShouldBe("يجب ان لا يكون الإعلان منتهي ");
        }

        [Fact]
        public void Should_RejectAnnouncement()
        {
            // Arrange
            var announcement = new Announcement(Name, Period, TenderTypId, TenderReasonId, IntroAboutTender, IsInsideKsa, Details,
                ActivityDescription, BranchId, AgencyCode, _activitiesIds,
                _constructionsWorkIds, _maintenanceWorkIds, _areasIds, _countriesIds)
            {
                StatusId = (int)Enums.AnnouncementStatus.Pending
            };
            // Act
            announcement.RejectApproveAnnouncement("reject reason");
            // Assert
            announcement.ShouldNotBeNull();
            announcement.StatusId.ShouldBe((int)Enums.AnnouncementStatus.Rejected);
        }

        [Fact]
        public void Should_RejectAnnouncement_Throw_Exception_When_Announcement_Status_Is_Not_Pending()
        {
            // Arrange
            var announcement = new Announcement(Name, Period, TenderTypId, TenderReasonId, IntroAboutTender, IsInsideKsa, Details,
                ActivityDescription, BranchId, AgencyCode, _activitiesIds,
                _constructionsWorkIds, _maintenanceWorkIds, _areasIds, _countriesIds);

            // Assert
            announcement.ShouldNotBeNull();
            Action action = () => { announcement.RejectApproveAnnouncement("reject reason"); };
            var exception = action.ShouldThrow(typeof(BusinessRuleException));
            exception.Message.ShouldBe("يجب ان يكون الإعلان بإنتظار الإعتماد");
        }

        [Fact]
        public void Should_RejectAnnouncement_Throw_Exception_When_Announcement_LastApplyingRequestsDate_LessThan_Now()
        {
            // Arrange
            var announcement = new Announcement(Name, Period, TenderTypId, TenderReasonId, IntroAboutTender, IsInsideKsa, Details,
                ActivityDescription, BranchId, AgencyCode, _activitiesIds,
                _constructionsWorkIds, _maintenanceWorkIds, _areasIds, _countriesIds);
            //todo: need to set the publish date to a past date, this can not be done due to DateTime.Now() needs to be an injected service
            // Assert
            announcement.ShouldNotBeNull();
            announcement.Test_UpdateStatus((int)Enums.AnnouncementStatus.Pending);
            announcement.Test_UpdatePublishDatetooldDate(DateTime.Now);
            Action action = () => { announcement.RejectApproveAnnouncement("reject reason"); };
            var exception = action.ShouldThrow(typeof(BusinessRuleException));
            exception.Message.ShouldBe("يجب ان لا يكون الإعلان منتهي ");
        }

        [Fact]
        public void Should_ReOpenAnnouncement()
        {
            // Arrange
            var announcement = new Announcement(Name, Period, TenderTypId, TenderReasonId, IntroAboutTender, IsInsideKsa, Details,
                ActivityDescription, BranchId, AgencyCode, _activitiesIds,
                _constructionsWorkIds, _maintenanceWorkIds, _areasIds, _countriesIds)
            {
                StatusId = (int)Enums.AnnouncementStatus.Rejected
            };
            // Act
            announcement.ReOpenAnnouncement();
            // Assert
            announcement.ShouldNotBeNull();
            announcement.StatusId.ShouldBe((int)Enums.AnnouncementStatus.UnderCreation);
        }

        [Fact]
        public void Should_ReOpenAnnouncement_Throw_Exception_When_Announcement_Status_Is_Not_Rejected()
        {
            // Arrange
            var announcement = new Announcement(Name, Period, TenderTypId, TenderReasonId, IntroAboutTender, IsInsideKsa, Details,
                ActivityDescription, BranchId, AgencyCode, _activitiesIds,
                _constructionsWorkIds, _maintenanceWorkIds, _areasIds, _countriesIds);

            // Assert
            announcement.ShouldNotBeNull();
            Action action = () => { announcement.ReOpenAnnouncement(); };
            var exception = action.ShouldThrow(typeof(BusinessRuleException));
            exception.Message.ShouldBe("يجب ان يكون الإعلان مرفوض");
        }

        [Fact]
        public void Should_DeleteAnnouncement()
        {
            // Arrange
            var announcement = new Announcement(Name, Period, TenderTypId, TenderReasonId, IntroAboutTender, IsInsideKsa, Details,
                ActivityDescription, BranchId, AgencyCode, _activitiesIds,
                _constructionsWorkIds, _maintenanceWorkIds, _areasIds, _countriesIds);
            // Act
            announcement.DeleteAnnouncement();
            // Assert
            announcement.ShouldNotBeNull();
            announcement.IsActive.ShouldNotBeNull();
            announcement.IsActive.HasValue.ShouldBeTrue();
            announcement.IsActive.Value.ShouldBeFalse();
        }

        [Fact]
        public void Should_DeleteAnnouncement_Throw_Exception_When_Announcement_Status_Is_Not_UnderCreation()
        {
            // Arrange
            var announcement = new Announcement(Name, Period, TenderTypId, TenderReasonId, IntroAboutTender, IsInsideKsa, Details,
                ActivityDescription, BranchId, AgencyCode, _activitiesIds,
                _constructionsWorkIds, _maintenanceWorkIds, _areasIds, _countriesIds)
            {
                StatusId = (int)Enums.AnnouncementStatus.Pending
            };

            // Assert
            announcement.ShouldNotBeNull();
            Action action = () => { announcement.DeleteAnnouncement(); };
            var exception = action.ShouldThrow(typeof(BusinessRuleException));
            exception.Message.ShouldBe("يجب ان تكون حالة الإعلان تحت الإنشاء");
        }

        [Fact]
        public void Should_Add_Announcement_History_With_UserId()
        {
            // Arrange
            var announcement = new Announcement(Name, Period, TenderTypId, TenderReasonId, IntroAboutTender, IsInsideKsa, Details,
                ActivityDescription, BranchId, AgencyCode, _activitiesIds,
                _constructionsWorkIds, _maintenanceWorkIds, _areasIds, _countriesIds);

            // Act
            announcement.AddAnnouncementHistoy(123);
            // Assert
            announcement.ShouldNotBeNull();
            announcement.AnnouncementHistories.Count.ShouldBe(1);
        }

        [Fact]
        public void Should_Add_Announcement_History_With_UserId_And_RejectionReason()
        {
            // Arrange
            var announcement = new Announcement(Name, Period, TenderTypId, TenderReasonId, IntroAboutTender, IsInsideKsa, Details,
                ActivityDescription, BranchId, AgencyCode, _activitiesIds,
                _constructionsWorkIds, _maintenanceWorkIds, _areasIds, _countriesIds);

            // Act
            announcement.AddAnnouncementHistoy(123, "reason");
            // Assert
            announcement.ShouldNotBeNull();
            announcement.AnnouncementHistories.Count.ShouldBe(1);
        }

        [Fact]
        public void Should_Add_LastApplyingRequestsDate_AddDaysBasedOnPeriod()
        {
            // Arrange
            var announcement = new Announcement(Name, Period, TenderTypId, TenderReasonId, IntroAboutTender, IsInsideKsa, Details,
                ActivityDescription, BranchId, AgencyCode, _activitiesIds,
                _constructionsWorkIds, _maintenanceWorkIds, _areasIds, _countriesIds);
            // Act
            announcement.SetPublishedDate();
            // Assert
            announcement.ShouldNotBeNull();
            announcement.LastApplyingRequestsDate.HasValue.ShouldBeTrue();
            announcement.LastApplyingRequestsDate.Value.ShouldBeGreaterThan(DateTime.Now);
        }

        [Fact]
        public void Should_LastApplyingRequestsDate_Return_PublishDate_Null()
        {
            // Arrange
            var announcement = new Announcement(Name, Period, TenderTypId, TenderReasonId, IntroAboutTender, IsInsideKsa, Details,
                ActivityDescription, BranchId, AgencyCode, _activitiesIds,
                _constructionsWorkIds, _maintenanceWorkIds, _areasIds, _countriesIds);

            // Assert
            announcement.ShouldNotBeNull();
            announcement.LastApplyingRequestsDate.HasValue.ShouldBeFalse();
        }

        [Fact]
        public void Should_AnnouncementStatusId_Return_Ended()
        {
            // Arrange
            var announcement = new Announcement(Name, Period, TenderTypId, TenderReasonId, IntroAboutTender, IsInsideKsa, Details,
                ActivityDescription, BranchId, AgencyCode, _activitiesIds,
                _constructionsWorkIds, _maintenanceWorkIds, _areasIds, _countriesIds);

            // Act
            announcement.Test_UpdatePublishDatetooldDate(DateTime.Now.AddDays(-30));

            // Assert
            announcement.ShouldNotBeNull();
            announcement.AnnouncementStatusId.ShouldBe((int)Enums.AnnouncementStatus.Ended);

        }
    }
}