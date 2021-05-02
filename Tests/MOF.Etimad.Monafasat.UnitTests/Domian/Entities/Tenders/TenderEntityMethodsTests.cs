using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Core.Entities.QualificationEvaluation;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernal.Exceptions;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.TestsBuilders.TenderDefaults;
using MOF.Etimad.Monafasat.TestsBuilders.TenderDefaults.ChangeRequestDefault;
using MOF.Etimad.Monafasat.ViewModel;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using static MOF.Etimad.Monafasat.SharedKernel.Enums;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Tenders
{
    public class TenderEntityMethodsTests
    {
        [Fact]
        public void CreatePreQualificationTest()
        {
            Tender tender = new Tender();

            tender.CreatePreQualification(0, (int)Enums.TenderType.NewDirectPurchase, "Tender 1", 1, 1, new DateTime(2020, 9, 15), new DateTime(2020, 10, 15), new DateTime(2020, 10, 16), (int)Enums.TenderStatus.UnderEstablishing, 1000, 100, 10000000, 1, "123456789", 1);

            Assert.Equal("Tender 1", tender.TenderName);
            Assert.Equal((int)Enums.TenderStatus.UnderEstablishing, tender.TenderStatusId);
            Assert.Equal((int)Enums.TenderType.NewDirectPurchase, tender.TenderTypeId);
            Assert.Equal(1, tender.TechnicalOrganizationId);
            Assert.Equal(1, tender.PreQualificationCommitteeId);
            Assert.Equal(ObjectState.Added, tender.State);
        }

        [Fact]
        public void ModifyPreQualificationTest()
        {
            Tender tender = new Tender();

            tender.CreatePreQualification(1, (int)Enums.TenderType.NewDirectPurchase, "Tender 1", 1, 1, new DateTime(2020, 9, 15), new DateTime(2020, 10, 15), new DateTime(2020, 10, 16), (int)Enums.TenderStatus.UnderEstablishing, 1000, 100, 10000000, 1, "123456789", 1);

            Assert.Equal("Tender 1", tender.TenderName);
            Assert.Equal((int)Enums.TenderStatus.UnderEstablishing, tender.TenderStatusId);
            Assert.Equal((int)Enums.TenderType.NewDirectPurchase, tender.TenderTypeId);
            Assert.Equal(1, tender.TechnicalOrganizationId);
            Assert.Equal(1, tender.PreQualificationCommitteeId);
            Assert.Equal(ObjectState.Modified, tender.State);
        }

        [Fact]
        public void UpdateQualificationMasterDataTest()
        {
            Tender tender = new Tender();
            tender.UpdateQualificationMasterData(0, 1000, 100, 10000000, (int)Enums.QualificationItemType.Range);

            Assert.Equal(1000, tender.TenderPointsToPass);
            Assert.Equal(100, tender.TechnicalAdministrativeCapacity);
            Assert.Equal(10000000, tender.FinancialCapacity);
            Assert.Equal((int)Enums.QualificationItemType.Range, tender.QualificationTypeId);
            Assert.Equal(ObjectState.Added, tender.State);
        }

        [Fact]
        public void UpdateQualificationMasterDataModificaitonTest()
        {
            Tender tender = new Tender();
            tender.UpdateQualificationMasterData(1, 1000, 100, 10000000, (int)Enums.QualificationItemType.Range);

            Assert.Equal(1000, tender.TenderPointsToPass);
            Assert.Equal(100, tender.TechnicalAdministrativeCapacity);
            Assert.Equal(10000000, tender.FinancialCapacity);
            Assert.Equal((int)Enums.QualificationItemType.Range, tender.QualificationTypeId);
            Assert.Equal(ObjectState.Modified, tender.State);
        }

        [Fact]
        public void UpdateConditionsTemplateSeventhStepTest()
        {
            Tender tender = new Tender();
            tender.CreateConditionsTemplate();
            tender.UpdateConditionsTemplateSeventhStep(new List<TenderConditionsTemplateTechnicalOutput>() { new TenderConditionsTemplateTechnicalOutput() }
            , new List<TenderConditionsTemplateTechnicalDeclration>() { new TenderConditionsTemplateTechnicalDeclration() },
                "project scope", "works program", "work location details", "servicesAndWorkImplementationsMethod");

            Assert.Equal(ObjectState.Modified, tender.State);
        }

        [Fact]
        public void UpdateBasicDataTest()
        {

            Tender tender = new Tender();

            Tender _tender = tender.UpdateBasicData((int)Enums.TenderType.Competition, (int)Enums.InvitationType.Public, "Tender 1", "1234", "General Tender", null, 1, 1, 1, 1, null, null, null, "tender type",
                10, 1, 10000000, 10000, 123, 4, 1, 20, 1, new List<string>() { "123456789" }, 1, 10, 1, false, null, "", 1, 1, new List<AgencyBudgetNumber>());

            Assert.Equal("Tender 1", _tender.TenderName);
            Assert.Equal((int)Enums.TenderStatus.UnderEstablishing, _tender.TenderStatusId);
            Assert.Single(_tender.TenderHistories);
            Assert.Equal((int)Enums.TenderType.Competition, _tender.TenderTypeId);
            Assert.Equal(1, _tender.QuantityTableVersionId);
            Assert.Null(_tender.SupplierNeedSample);
            Assert.Equal(ObjectState.Modified, _tender.State);
        }

        [Fact]
        public void UpdateBasicDataTestWithSamples()
        {

            Tender tender = new Tender();

            Tender _tender = tender.UpdateBasicData((int)Enums.TenderType.Competition, (int)Enums.InvitationType.Public, "Tender 1", "1234", "General Tender", null, 1, 1, 1, 1, 12, null, null, "tender type",
                10, 1, 10000000, 10000, 123, 4, 1, 20, 1, new List<string>() { "123456789" }, 1, 10, 1, true, true, "Oliya, Riyadh", 1, 1, new List<AgencyBudgetNumber>());

            Assert.Equal("Tender 1", _tender.TenderName);
            Assert.Equal((int)Enums.TenderStatus.UnderEstablishing, _tender.TenderStatusId);
            Assert.Single(_tender.TenderHistories);
            Assert.Equal((int)Enums.TenderType.Competition, _tender.TenderTypeId);
            Assert.Equal(1, _tender.QuantityTableVersionId);
            Assert.True(_tender.SupplierNeedSample);
            Assert.Equal("Oliya, Riyadh", tender.SamplesDeliveryAddress);
            Assert.Equal(ObjectState.Modified, _tender.State);
        }

        [Fact]
        public void SetUnitStatusTest()
        {
            Tender tender = new Tender();
            tender.SetUnitStatus(Enums.TenderUnitStatus.ApprovedByManager);

            Assert.Equal((int)Enums.TenderUnitStatus.ApprovedByManager, tender.TenderUnitStatusId);
            Assert.Equal(ObjectState.Modified, tender.State);

        }

        [Fact]
        public void SetUnitSpacialistWouldLikeToAttendTheCommitteTest()
        {
            Tender tender = new Tender();

            tender.SetUnitSpacialistWouldLikeToAttendTheCommitte(true);

            Assert.True(tender.UnitSpacialistWouldLikeToAttendTheCommitte);
            Assert.Equal(ObjectState.Modified, tender.State);
        }

        [Fact]
        public void UpdateOfferOpeningNotificationStatusTest()
        {
            Tender tender = new Tender();

            tender.UpdateOfferOpeningNotificationStatus();

            Assert.True(tender.OpeningNotificationSent);
            Assert.Equal(ObjectState.Modified, tender.State);
        }

        [Fact]
        public void UpdateOfferCheckingNotificationStatusTest()
        {
            Tender tender = new Tender();

            tender.UpdateOfferCheckingNotificationStatus();

            Assert.True(tender.CheckingNotificationSent);
            Assert.Equal(ObjectState.Modified, tender.State);
        }

        [Fact]
        public void GetCommunicationRequestsTest()
        {

            Tender tender = new Tender();
            AgencyCommunicationRequest newRequest = new AgencyCommunicationRequest(1, (int)Enums.AgencyCommunicationRequestType.Enquiry);

            tender.AddAgencyCommunicationRequest(newRequest);

            AgencyCommunicationRequest request = tender.GetCommunicationRequests(Enums.AgencyCommunicationRequestType.Enquiry);

            Assert.Equal(1, request.TenderId);

        }

        [Fact]
        public void AddInvitationForQualifiedSuppliersTest()
        {
            Tender tender = new Tender();

            tender.AddInvitationForQualifiedSuppliers(new List<string>() { "123456789", "987654321" });

            Assert.Equal(2, tender.Invitations.Count);
            Assert.True(tender.Invitations[0].IsActive);
        }

        [Fact]
        public void AddInvitationForQualifiedSuppliers_DeactiveOldTest()
        {
            Tender tender = new Tender();

            tender.AddInvitationForQualifiedSuppliers(new List<string>() { "123456789" });
            tender.AddInvitationForQualifiedSuppliers(new List<string>() { "987654321" });

            Assert.Equal(2, tender.Invitations.Count);
            Assert.Equal(ObjectState.Deleted, tender.Invitations[0].State);
            Assert.True(tender.Invitations[1].IsActive);
        }

        [Fact]
        public void UpdatePreQualificationDataTest()
        {
            Tender tender = new Tender();
            PreQualificationSavingModel _preQualification = new PreQualificationSavingModel();

            var updated = tender.UpdatePreQualificationData(_preQualification, new List<TenderAttachment>(), 1);

            Assert.IsType<Tender>(updated);
            Assert.Equal(ObjectState.Modified, tender.State);
        }

        [Fact]
        public void UpdateQualificationDataFromSecraryTest()
        {
            Tender tender = new Tender();
            PreQualificationSavingModel _preQualification = new PreQualificationSavingModel();

            var updated = tender.UpdateQualificationDataFromSecrary(_preQualification, new List<TenderAttachment>(), 1);

            Assert.IsType<Tender>(updated);
            Assert.Equal(ObjectState.Modified, tender.State);
        }

        [Fact]
        public void UpdateTenderDatesTest()
        {
            Tender tender = new Tender();

            tender.UpdateTenderDates(DateTime.Now.AddDays(7), DateTime.Now.AddDays(9), DateTime.Now.AddDays(10), DateTime.Now.AddDays(15), "", 10, false, "", 1, "Oliya", "2", "dep 1", DateTime.Now.AddMonths(2));

            Assert.Equal((int)Enums.TenderStatus.UnderEstablishing, tender.TenderStatusId);
            Assert.Null(tender.OffersOpeningAddress);
            Assert.Equal(ObjectState.Modified, tender.State);
        }
        [Fact]
        public void AddEditTenderDatesTest()
        {
            Tender tender = new Tender();

            tender.AddEditTenderDates(DateTime.Now.AddDays(7), DateTime.Now.AddDays(9), DateTime.Now.AddDays(10), DateTime.Now.AddDays(15), false, "", 1, "Oliya", "2", "dep 1", DateTime.Now.AddMonths(2), 5);

            Assert.Equal((int)Enums.TenderStatus.UnderEstablishing, tender.TenderStatusId);
            Assert.Equal(5, tender.AwardingStoppingPeriod);
            Assert.Null(tender.OffersOpeningAddress);
            Assert.Equal(ObjectState.Modified, tender.State);
        }
        
        [Fact]
        public void UpdateFinalAndInitialGuaranteeTest()
        {
            Tender tender = new Tender();

            tender.UpdateFinalAndInitialGuarantee(5,2,"initial guarantee address");

            Assert.Equal(5, tender.AwardingDiscountPercentage);
            Assert.Equal(ObjectState.Modified, tender.State);
        }
        [Fact]
        public void UpdateTenderInApproveVROTest()
        {
            Tender tender = new Tender();

            var updated = tender.UpdateTenderInApproveVRO(DateTime.Now.AddDays(7), DateTime.Now.AddDays(9), DateTime.Now.AddDays(10), 1, 1, 1);

            Assert.IsType<Tender>(updated);

            Assert.Equal(1, tender.VROCommitteeId);
            Assert.Null(tender.OffersOpeningAddress);
            Assert.Equal(ObjectState.Modified, tender.State);
        }

        [Fact]
        public void UpdateTenderBranchAndAgencyTest()
        {
            Tender tender = new Tender();

            var updated = tender.UpdateTenderBranchAndAgency(1, "123456");

            Assert.IsType<Tender>(updated);
            Assert.Null(updated.Branch);
            Assert.Null(updated.Agency);
            Assert.Equal(ObjectState.Modified, tender.State);
        }

        [Fact]
        public void UpdateDatesTest()
        {
            Tender tender = new Tender();
            var updated = tender.UpdateDates(new Core.TenderDatesChange(), 123);

            Assert.Equal((int)Enums.TenderStatus.Approved, updated.TenderStatusId);
            Assert.Equal(ObjectState.Modified, tender.State);
        }

        [Fact]
        public void UpdateTenderRelationsTest()
        {
            Tender tender = new Tender();
            var updated = tender.UpdateTenderRelations(new List<int>() { 1, 2, 3 }, new List<int>() { 1, 2, 3 }, new List<int>() { 1, 2, 3 }, new List<int>() { 1, 2, 3 }, true, "", "", new List<int>() { 1, 2, 3 }, true, 2, (int)Enums.TenderStatus.UnderEstablishing);

            Assert.Equal((int)Enums.TenderStatus.UnderEstablishing, updated.TenderStatusId);
            Assert.Equal(ObjectState.Modified, tender.State);
        }

        [Fact]
        public void UpdateTenderQuantityTablesItemsTest()
        {
            Tender tender = new Tender();
            var updated = tender.UpdateTenderQuantityTablesItems(3);

            Assert.IsType<Tender>(updated);
            Assert.Equal(ObjectState.Modified, tender.State);
        }

        [Fact]
        public void UpdateTenderRelationsWithoutQuantityTablesTest()
        {
            Tender tender = new Tender();
            var updated = tender.UpdateTenderRelationsWithoutQuantityTables(new List<int>() { 1, 2, 3 }, new List<int>() { 1, 2, 3 }, new List<int>() { 1, 2, 3 }, new List<int>() { 1, 2, 3 }, true, "", "", new List<int>() { 1, 2, 3 }, true,true, (int)Enums.TenderStatus.UnderEstablishing);

            Assert.IsType<Tender>(updated);
            Assert.Equal((int)Enums.TenderStatus.UnderEstablishing, updated.TenderStatusId);
            Assert.Equal(ObjectState.Modified, tender.State);
        }

        [Fact]
        public void SetIsUnitSecreteryAcceptedTest()
        {
            Tender tender = new Tender();
            tender.SetIsUnitSecreteryAccepted(false);

            Assert.False(tender.IsUnitSecreteryAccepted);
            Assert.Equal(ObjectState.Modified, tender.State);
        }

        [Fact]
        public void SaveTenderNewQuantityTablesTest()
        {
            Tender tender = new Tender();
            long itemId = 1;
            tender.SaveTenderQuantityTables(new List<TenderQuantityItemDTO>() { new TenderQuantityItemDTO() { Id = 1234 } }, "TableName", 1, out itemId, 1);

            Assert.Equal(0, itemId);
            Assert.Equal(ObjectState.Modified, tender.State);
        }


        [Fact]
        public void UpdateQuantityTableNameTest()
        {
            Tender tender = new Tender();
            tender.TenderQuantityTables.Add(new TenderQuantityTable() { Id = 1234, Name = "Table Name" });

            tender.UpdateQuantityTableName(1234, "new Table Name");

            Assert.Equal("new Table Name", tender.TenderQuantityTables[0].Name);
            Assert.Equal(ObjectState.Modified, tender.State);
        }

        [Fact]
        public void UpdateTenderTemplateYearsTest()
        {
            Tender tender = new Tender();
            var _rslt = tender.UpdateTenderTemplateYears(15);
            Assert.NotNull(_rslt);
            Assert.IsType<Tender>(_rslt);
            Assert.Equal(15, tender.TemplateYears);
            Assert.Equal(ObjectState.Modified, tender.State);
        }

        [Fact]
        public void DeleteTenderQuantityTablesTest()
        {
            Tender tender = new Tender();
            tender.TenderQuantityTables.Add(new TenderQuantityTable());
            tender.DeleteTenderQuantityTables();
            Assert.Equal(ObjectState.Deleted, tender.TenderQuantityTables[0].State);
        }

        [Fact]
        public void DeleteTenderQuantityTablesItemsTest()
        {
            Tender tender = new Tender();
            var tenderQuantityTable = new TenderQuantityTable() { QuantitiyItemsJson = new TenderQuantitiyItemsJson() };
            tender.TenderQuantityTables.Add(tenderQuantityTable);
            tender.DeleteTenderQuantityTablesItems();
            Assert.Equal(ObjectState.Deleted, tender.TenderQuantityTables[0].QuantitiyItemsJson.State);
        }

        [Fact]
        public void UpdateQuantityTableChangesNameTest()
        {
            Tender tender = new Tender();
            TenderChangeRequest tenderChangeRequest = new TenderChangeRequest(105, 1, 100, "100", true);
            tender.ChangeRequests.Add(tenderChangeRequest);
            tender.CreateTenderQuantityTablesChanges(0, "", 2);
            var _rslt = tender.UpdateQuantityTableChangesName(0, 0, "QName");
            Assert.NotNull(_rslt);
            Assert.IsType<Tender>(_rslt);
            Assert.Equal(ObjectState.Modified, tender.State);
            Assert.Equal(ObjectState.Added, tender.ChangeRequests[0].State);
        }

        [Fact]
        public void UpdateHasAlternativeTest()
        {
            Tender tender = new Tender();
            var _rslt = tender.UpdateHasAlternative(true);
            Assert.NotNull(_rslt);
            Assert.IsType<Tender>(_rslt);
            Assert.True(_rslt.HasAlternativeOffer);
            Assert.Equal(ObjectState.Modified, tender.State);
        }

        [Fact]
        public void UpdateEstimatedValueTest()
        {
            Tender tender = new Tender();
            tender.UpdateEstimatedValue(1000);
            Assert.Equal(1000, tender.EstimatedValue);
            Assert.Equal(ObjectState.Modified, tender.State);
        }

        [Fact]
        public void UpdateTenderAwardingTypeTest()
        {
            Tender tender = new Tender();
            tender.UpdateTenderAwardingType(1000, true);
            Assert.True(tender.TenderAwardingType);
            Assert.Equal(ObjectState.Modified, tender.State);
        }

        [Fact]
        public void UpdateTenderAwardingNotificationTest()
        {
            Tender tender = new Tender();
            tender.UpdateTenderAwardingNotification(true, 90, 10, "address test");
            Assert.True(tender.HasGuarantee);
            Assert.Equal(90, tender.AwardingDiscountPercentage);
            Assert.Equal(10, tender.AwardingMonths);
            Assert.Equal("address test", tender.FinalGuaranteeDeliveryAddress);
            Assert.Equal(ObjectState.Modified, tender.State);
        }

        [Fact]
        public void SetTenderRelationsTest()
        {
            Tender tender = new Tender();
            tender.SetTenderRelations(new List<TenderActivity>(), new List<TenderArea>(), new List<TenderCountry>(), new List<TenderConstructionWork>(), new List<TenderMaintenanceRunnigWork>());
            Assert.NotNull(tender.TenderActivities);
            Assert.NotNull(tender.TenderAreas);
            Assert.NotNull(tender.TenderCountries);
            Assert.NotNull(tender.TenderConstructionWorks);
            Assert.NotNull(tender.TenderMaintenanceRunnigWorks);
            Assert.Equal(ObjectState.Modified, tender.State);
        }

        [Fact]
        public void SetPreviousFramWorkTest()
        {
            Tender tender = new Tender();
            tender.TenderQuantityTables.Add(new TenderQuantityTable() { QuantitiyItemsJson = new TenderQuantitiyItemsJson() });
            tender.Attachments.Add(new TenderAttachment());
            tender.TenderActivities.Add(new TenderActivity());
            tender.TenderAreas.Add(new TenderArea());
            tender.TenderCountries.Add(new TenderCountry());
            tender.TenderConstructionWorks.Add(new TenderConstructionWork());
            tender.TenderMaintenanceRunnigWorks.Add(new TenderMaintenanceRunnigWork());
            tender.TenderAgreementAgencies.Add(new TenderAgreementAgency());
            tender.SetPreviousFramWork(100);
            Assert.Equal(ObjectState.Added, tender.State);
        }

        [Fact]
        public void UpdateAwardingStoppingPeriodTest()
        {
            Tender tender = new Tender();
            tender.UpdateAwardingStoppingPeriod(100);
            Assert.Equal(100, tender.AwardingStoppingPeriod);
            Assert.Equal(ObjectState.Modified, tender.State);
        }

        [Fact]
        public void UpdateAwardingReportTest()
        {
            Tender tender = new Tender();
            tender.UpdateAwardingReport("FName10", "Id10");
            Assert.Equal("FName10", tender.AwardingReportFileName);
            Assert.Equal("Id10", tender.AwardingReportFileNameId);
            Assert.Equal(ObjectState.Modified, tender.State);
        }

        [Fact]
        public void DeleteAwardingReportTest()
        {
            Tender tender = new Tender();
            tender.UpdateAwardingReport("FName10", "Id10");
            tender.DeleteAwardingReport();
            Assert.Null(tender.AwardingReportFileName);
            Assert.Null(tender.AwardingReportFileNameId);
            Assert.Equal(ObjectState.Modified, tender.State);
        }

        [Fact]
        public void ShouldUpdateTenderAttachments()
        {
            Tender tender = new Tender();
            tender.Attachments.Add(new TenderAttachment("Attachment Name", "1212", 1, null, null));
            List<TenderAttachment> attachments = new List<TenderAttachment>() { new TenderAttachment("Attachment Name", "1212", 1, null, null) };

            tender.UpdateAttachments(attachments, 1, false);

            Assert.Equal(ObjectState.Modified, tender.State);
        }

        [Fact]
        public void ShouldUpdateTenderAttachmentsForLocalContent()
        {
            Tender tender = new Tender();
            tender.Attachments.Add(new TenderAttachment("Attachment Name", "1212", (int)Enums.AttachmentType.LocalContent, null, null));
            List<TenderAttachment> attachments = new List<TenderAttachment>() { new TenderAttachment("Attachment Name", "1212", (int)Enums.AttachmentType.LocalContent, null, null) };

            tender.UpdateLocalContentAttachments(attachments, 1, false);

            Assert.Equal(ObjectState.Modified, tender.State);
        }

        [Fact]
        public void ShouldUpdateTenderAttachmentsAndAddTenderHistory()
        {
            Tender tender = new Tender();
            tender.Attachments.Add(new TenderAttachment("Attachment Name", "1212", 1, null, null));
            List<TenderAttachment> attachments = new List<TenderAttachment>() { new TenderAttachment("Attachment Name", "1212", 1, null, null) };

            tender.UpdateAttachments(attachments, 1, true);

            Assert.Single(tender.TenderHistories);
            Assert.Equal(ObjectState.Modified, tender.State);
        }

        [Fact]
        public void ShouldResetUnitSecretaryAction()
        {
            Tender tender = new Tender();

            tender.ResetUnitSecretaryAction();

            Assert.Null(tender.IsUnitSecreteryAccepted);
            Assert.Equal(ObjectState.Modified, tender.State);
        }

        [Fact]
        public void ShouldUpdateBeneficiaryAgencies()
        {
            Tender tender = new Tender();
            tender.TenderAgreementAgencies.Add(new TenderAgreementAgency("30202020"));
            List<string> agencys = new List<string>() { "101012020" };

            tender.UpdateBeneficiaryAgencies(agencys);

            var deletedAgency = tender.TenderAgreementAgencies.FirstOrDefault(s => s.AgencyCode == "30202020");
            var addedAgency = tender.TenderAgreementAgencies.FirstOrDefault(s => s.AgencyCode == "101012020");
            Assert.Equal(ObjectState.Deleted, deletedAgency.State);
            Assert.Equal(ObjectState.Added, addedAgency.State);
        }

        [Theory]
        [InlineData(1)]
        public void ShouldResetTechnicalCommittee(int committeeId)
        {
            Tender tender = new Tender();

            tender.ResetTechnicalCommittee(committeeId);

            Assert.Null(tender.TechnicalOrganizationId);
            Assert.Equal(ObjectState.Modified, tender.State);
        }

        [Theory]
        [InlineData(1)]
        public void ShouldResetCheckCommittee(int committeeId)
        {
            Tender tender = new Tender();

            tender.ResetCheckCommittee(committeeId);

            Assert.Null(tender.OffersCheckingCommitteeId);
            Assert.Equal(ObjectState.Modified, tender.State);
        }

        [Theory]
        [InlineData(4, (int)Enums.CommitteeType.TechincalCommittee, 1, 1)]
        [InlineData(4, (int)Enums.CommitteeType.OpenOfferCommittee, 1, 1)]
        [InlineData(4, (int)Enums.CommitteeType.CheckOfferCommittee, 1, 1)]
        [InlineData(4, (int)Enums.CommitteeType.PurchaseCommittee, 1, 1)]
        [InlineData(4, (int)Enums.CommitteeType.PreQualificationCommittee, 1, 1)]
        [InlineData(4, (int)Enums.CommitteeType.VROCommittee, 1, 1)]
        public void ShouldUpdateTenderCommittee(int tenderStatusId, int CommitteeTypeId, int committeeId, int userId)
        {
            Tender tender = new Tender();

            tender.UpdateTenderCommittee(tenderStatusId, CommitteeTypeId, committeeId, userId);

            Assert.Equal(ObjectState.Modified, tender.State);
        }

        [Theory]
        [InlineData(4, (int)Enums.CommitteeType.TechincalCommittee, 1, 1)]
        [InlineData(4, (int)Enums.CommitteeType.OpenOfferCommittee, 1, 1)]
        [InlineData(4, (int)Enums.CommitteeType.CheckOfferCommittee, 1, 1)]
        [InlineData(4, (int)Enums.CommitteeType.PurchaseCommittee, 1, 1)]
        [InlineData(4, (int)Enums.CommitteeType.PreQualificationCommittee, 1, 1)]
        [InlineData(4, (int)Enums.CommitteeType.VROCommittee, 1, 1)]
        public void ShouldReturnBusinessExceptionWhenCommitteeAlreadyLinkedWithTender(int tenderStatusId, int CommitteeTypeId, int committeeId, int userId)
        {
            Tender tender = new TenderDefault().GetGeneralTenderWithCommittees();

            var e = Assert.Throws<BusinessRuleException>(() => tender.UpdateTenderCommittee(tenderStatusId, CommitteeTypeId, committeeId, userId));

            Assert.Equal("هذه المنافسة مربوطه باللجنه من قبل ولا يمكن ربطها مره أخرى", e.Message);
        }

        [Fact]
        public void ShouldUpdateTenderInvitationType()
        {
            Tender tender = new Tender();

            tender.UpdateTenderInvitationType();

            Assert.Equal((int)Enums.InvitationType.Public, tender.InvitationTypeId);
            Assert.Equal(ObjectState.Modified, tender.State);
        }

        [Theory]
        [InlineData(1)]
        public void ShouldResetOpenCommittee(int committeeId)
        {
            Tender tender = new Tender();

            tender.ResetOpenCommittee(committeeId);

            Assert.Null(tender.OffersOpeningCommitteeId);
            Assert.Equal(ObjectState.Modified, tender.State);
        }

        [Theory]
        [InlineData(4, 1, 1, 1, 1, 1, 1, 1121)]
        public void ShouldUpdateCommittees(int tenderStatusId, int? openingCommitteeId, int? CheckingCommitteeId, int? technicalCommitteeId, int? vROCommitteeId, int? directPurchaseCommitteeId, int? directPurchaseCommitteeMemberId, int userId)
        {
            Tender tender = new Tender();

            tender.UpdateCommittees(tenderStatusId, openingCommitteeId, CheckingCommitteeId, technicalCommitteeId, vROCommitteeId, directPurchaseCommitteeId, directPurchaseCommitteeMemberId, userId);

            Assert.Equal(openingCommitteeId, tender.OffersOpeningCommitteeId);
            Assert.NotNull(tender.TenderHistories);
            Assert.Equal(ObjectState.Modified, tender.State);
        }


        [Theory]
        [InlineData(4, 1, 1, 1121)]
        public void ShouldUpdatePreQualificationCommittees(int tenderStatusId, int? qualificationCommitteedId, int? technicalCommitteeId, int userId)
        {
            Tender tender = new Tender();

            tender.UpdatePreQualificationCommittees(tenderStatusId, qualificationCommitteedId, technicalCommitteeId, userId);

            Assert.Equal(qualificationCommitteedId, tender.PreQualificationCommitteeId);
            Assert.Equal(technicalCommitteeId, tender.TechnicalOrganizationId);
            Assert.NotNull(tender.TenderHistories);
            Assert.Equal(ObjectState.Modified, tender.State);
        }

        [Theory]
        [InlineData("Test Address")]
        public void ShouldUpdateSamplesDeliveryAddress(string address)
        {
            Tender tender = new Tender();

            tender.UpdateSamplesDeliveryAddress(address);

            Assert.Equal(address, tender.SamplesDeliveryAddress);
            Assert.Equal(ObjectState.Modified, tender.State);
        }

        [Theory]
        [InlineData(true)]
        public void ShouldUpdateAreas(bool? inSideKsa)
        {
            Tender tender = new Tender();
            tender.TenderCountries.Add(new TenderCountry(1));
            tender.TenderAreas.Add(new TenderArea(1));
            List<int> areas = new List<int>() { 2, 3 };

            tender.UpdateAreas(areas, inSideKsa);

            Assert.All(tender.TenderCountries, t => Assert.Equal(ObjectState.Deleted, t.State));
            Assert.NotEmpty(tender.TenderAreas);
        }

        [Theory]
        [InlineData(false)]
        public void ShouldUpdateContries(bool? inSideKsa)
        {
            Tender tender = new Tender();
            tender.TenderCountries.Add(new TenderCountry(1));
            List<int> countries = new List<int>() { 2, 3 };

            tender.UpdateCountries(countries, inSideKsa);

            Assert.NotEmpty(tender.TenderCountries);
        }

        [Fact]
        public void ShouldUpdateActivities()
        {
            Tender tender = new Tender();
            tender.TenderActivities.Add(new TenderActivity(1));
            List<int> activites = new List<int>() { 2, 3 };

            tender.UpdateActivities(activites);

            Assert.NotEmpty(tender.TenderActivities);
        }

        [Fact]
        public void ShouldUpdateConstructionWorks()
        {
            Tender tender = new Tender();
            tender.TenderConstructionWorks.Add(new TenderConstructionWork(1));
            List<int> constructionWorkIds = new List<int>() { 2, 3 };

            tender.UpdateConstructionWorks(constructionWorkIds);

            Assert.NotEmpty(tender.TenderConstructionWorks);
        }

        [Fact]
        public void ShouldUpdateRunningMentainanceWorks()
        {
            Tender tender = new Tender();
            tender.TenderMaintenanceRunnigWorks.Add(new TenderMaintenanceRunnigWork(1));
            List<int> runningMentainanceWorkIds = new List<int>() { 2, 3 };

            tender.UpdateRunningMentainanceWorks(runningMentainanceWorkIds);

            Assert.NotEmpty(tender.TenderMaintenanceRunnigWorks);
        }

        [Fact]
        public void ShouldUpdateUnregesteredInvitations()
        {
            Tender tender = new Tender();
            tender.UnRegisteredSuppliersInvitation.Add(new UnRegisteredSuppliersInvitation("101010", 1, "WW@ww.com", "010202020", Enums.InvitationStatus.New, "Test"));
            tender.UnRegisteredSuppliersInvitation.Add(new UnRegisteredSuppliersInvitation("101010", 1, "WW@ww.com", "010202020", Enums.InvitationStatus.Approved, "Test"));

            tender.UpdateUnregesteredInvitations();

            Assert.All(tender.UnRegisteredSuppliersInvitation, c => Assert.Equal((int)Enums.InvitationStatus.New, c.InvitationStatusId));
        }

        [Fact]
        public void ShouldSendInvitations()
        {
            Tender tender = new Tender();
            tender.Invitations.Add(new Invitation("10202020", Enums.InvitationStatus.New, Enums.InvitationRequestType.Invitation, false));
            tender.Invitations.Add(new Invitation("293999939", Enums.InvitationStatus.New, Enums.InvitationRequestType.Invitation, false));
            List<string> suppliers = new List<string>() { "10202020", "293999939" };

            tender.SendInvitations(suppliers);

            Assert.All(tender.Invitations, c => Assert.Equal((int)Enums.InvitationStatus.New, c.StatusId));
            Assert.All(tender.Invitations, c => Assert.Equal(DateTime.Now.Date, c.SendingDate));
            Assert.Equal(ObjectState.Modified, tender.State);
        }

        [Fact]
        public void ShouldAddSupplierInvitation()
        {
            Tender tender = new Tender();
            tender.Invitations.Add(new Invitation("10202020", Enums.InvitationStatus.New, Enums.InvitationRequestType.Invitation, false));
            tender.Invitations.Add(new Invitation("293999939", Enums.InvitationStatus.Withdrawal, Enums.InvitationRequestType.Invitation, false));
            tender.Invitations.Add(new Invitation("29399993933", Enums.InvitationStatus.New, Enums.InvitationRequestType.Invitation, false));
            List<string> checkedSupplier = new List<string>() { "10202020", "293999939", "293999939111" };
            List<string> unCheckedSupplier = new List<string>() { "29399993933" };

            tender.AddSupplierInvitation(checkedSupplier, unCheckedSupplier);

            Assert.All(tender.Invitations, c => Assert.Equal((int)Enums.InvitationStatus.New, c.StatusId));
            Assert.Equal(ObjectState.Modified, tender.State);
        }

        [Fact]
        public void ShouldRegisterSupplierEmails()
        {
            Tender tender = new Tender();
            tender.UnRegisteredSuppliersInvitation.Add(new UnRegisteredSuppliersInvitation("101010", 1, "WW@ww.com", "010202020", Enums.InvitationStatus.New, "Test"));
            List<string> emails = new List<string>() { "hh@hha.com" };

            tender.RegisterSupplierEmails(emails);

            Assert.Equal(ObjectState.Modified, tender.State);
        }

        [Fact]
        public void ShouldRegisterSupplierMobilNo()
        {
            Tender tender = new Tender();
            tender.UnRegisteredSuppliersInvitation.Add(new UnRegisteredSuppliersInvitation("101010", 1, "WW@ww.com", "010202020", Enums.InvitationStatus.New, "Test"));
            List<string> mobilNoList = new List<string>() { "012020020" };

            tender.RegisterSupplierMobilNo(mobilNoList);

            Assert.Equal(ObjectState.Modified, tender.State);
        }

        [Theory]
        [InlineData(1, Enums.TenderActions.CreateTender, "", 1121)]
        public void ShouldAddActionHistory(int statusId, TenderActions action, string rejectionReason, int currentUserId)
        {
            Tender tender = new Tender();

            tender.AddActionHistory(statusId, action, rejectionReason, currentUserId);

            Assert.Single(tender.TenderHistories);
        }

        [Theory]
        [InlineData("1001000154", 1, true, 1000, 1)]
        public void ShouldAddTenderAwardingHistory(string commercialRegisterationNumber, int tenderId, bool? tenderAwardingType, decimal awardingValue, int awardingIndex)
        {
            Tender tender = new Tender();
            tender.TenderAwardingHistories.Add(new TenderAwardingHistory());
            tender.AddTenderAwardingHistory(commercialRegisterationNumber, tenderId, tenderAwardingType, awardingValue, awardingIndex);

            Assert.Equal(ObjectState.Modified,tender.State);
        }

        [Theory]
        [InlineData("1001000154")]
        public void ShouldSendRequestForTender(string commercialRegisterationNumber)
        {
            Tender tender = new Tender();

            tender.SendRequestForTender(commercialRegisterationNumber);

            Assert.Single(tender.Invitations);
            Assert.All(tender.Invitations, i => Assert.Equal((int)InvitationRequestType.Request, i.InvitationTypeId));
            Assert.All(tender.Invitations, i => Assert.Equal((int)InvitationStatus.New, i.StatusId));
        }

        [Theory]
        [InlineData("1001000154")]
        public void ShouldAddInvitationBySupplier(string commercialRegisterationNumber)
        {
            Tender tender = new Tender();

            tender.AddInvitationBySupplier(commercialRegisterationNumber);

            Assert.Single(tender.Invitations);
            Assert.All(tender.Invitations, i => Assert.Equal((int)InvitationRequestType.Invitation, i.InvitationTypeId));
            Assert.All(tender.Invitations, i => Assert.Equal((int)InvitationStatus.New, i.StatusId));
        }

        [Theory]
        [InlineData(Enums.TenderStatus.Approved, "", 1121, TenderActions.Approve)]
        public void ShouldUpdateTenderStatus(Enums.TenderStatus tenderStatus, string rejectionReason, int userId, TenderActions tenderAction, bool isActive = true)
        {
            Tender tender = new Tender();

            tender.UpdateTenderStatus(tenderStatus, rejectionReason, userId, tenderAction);

            Assert.Single(tender.TenderHistories);
            Assert.Equal((int)Enums.TenderStatus.Approved, tender.TenderStatusId);
            Assert.Equal(DateTime.Now.Date, tender.SubmitionDate.Value.Date);
        }

        [Theory]
        [InlineData(Enums.TenderConditoinsStatus.EvaluateOffers)]
        public void ShouldUpdateTenderConditoinsStatus(Enums.TenderConditoinsStatus status)
        {
            Tender tender = new Tender();

            tender.UpdateTenderConditoinsStatus(status);

            Assert.Equal((int)status, tender.ConditionTemplateStageStatusId);
            Assert.Equal(ObjectState.Modified, tender.State);
        }

        [Fact]
        public void ShouldUpdateSubmitionDate()
        {
            Tender tender = new Tender();

            tender.UpdateSubmitionDate();

            Assert.Equal(DateTime.Now.Date, tender.SubmitionDate.Value.Date);
        }

        [Fact]
        public void ShouldDeActivateTender()
        {
            Tender tender = new Tender();

            tender.DeActive();

            Assert.False(tender.IsActive);
            Assert.Equal(ObjectState.Modified, tender.State);
        }

        [Fact]
        public void ShouldDeleteTender()
        {
            Tender tender = new Tender();

            tender.Delete();

            Assert.Equal(ObjectState.Deleted, tender.State);
        }

        [Fact]
        public void ShouldResetPostQualifications()
        {
            Tender tender = new Tender();

            tender.ResetPostQualifications();

            Assert.False(tender.IsActive);
            Assert.Equal((int)Enums.TenderStatus.Canceled, tender.TenderStatusId);
            Assert.Equal(ObjectState.Modified, tender.State);
        }

        [Fact]
        public void ShouldRUpdateEmarketPlaceStatus()
        {
            Tender tender = new Tender();

            tender.UpdateEmarketPlaceStatus();

            Assert.True(tender.IsSendToEmarketPlace);
            Assert.Equal(ObjectState.Modified, tender.State);
        }

        [Fact]
        public void ShouldUpdateFinishedStoppingAwardingPeriodTendersStatus()
        {
            Tender tender = new Tender();

            tender.UpdateFinishedStoppingAwardingPeriodTendersStatus();

            Assert.True(tender.IsNotificationSentForStoppingPeriod);
            Assert.Equal(ObjectState.Modified, tender.State);
        }

        [Fact]
        public void ShouldSetAuditorAgree()
        {
            Tender tender = new Tender();

            tender.SetAuditorAgree(true);

            Assert.True(tender.AuditorAgree);
            Assert.Equal(ObjectState.Modified, tender.State);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void ShouldSetCompetitionIsBudgetedOrNot(bool inBudget)
        {
            Tender tender = new Tender();

            tender.SetCompetitionIsBudgetedOrNot(inBudget);

            Assert.Equal(DateTime.Now.Date, tender.SubmitionDate.Value.Date);
            Assert.Equal(ObjectState.Modified, tender.State);
        }

        [Theory]
        [InlineData("102120121021210")]
        public void ShouldSetReferenceNumber(string referenceNumber)
        {
            Tender tender = new Tender();

            tender.SetReferenceNumber(referenceNumber);

            Assert.Equal(referenceNumber, tender.ReferenceNumber);
        }

        [Fact]
        public void ShouldAddBiddingRounds()
        {
            Tender tender = new Tender();
            BiddingRound biddingRound = new BiddingRound(DateTime.Now.Date, DateTime.Now.Date.AddDays(2), 1, 1);

            tender.AddBiddingRounds(biddingRound);

            Assert.Single(tender.BiddingRounds);
        }

        [Theory]
        [InlineData(0, 1, 11)]
        public void ShouldAddBiddingRoundOfffer(int biddingRoundId, int offerId, decimal biddingValue)
        {
            Tender tender = new Tender();
            BiddingRound biddingRound = new BiddingRound(DateTime.Now.Date, DateTime.Now.Date.AddDays(2), 1, 1);
            biddingRound.BiddingRoundOffers = new List<BiddingRoundOffer>();

            tender.BiddingRounds.Add(biddingRound);
            tender.AddBiddingRoundOfffer(biddingRoundId, offerId, biddingValue);

            Assert.Single(tender.BiddingRounds);
            Assert.Equal(ObjectState.Modified, tender.State);
        }

        [Theory]
        [InlineData("Purpise", 1, 1, 1, 1121)]
        public void ShouldUpdateSecondStageBasicData(string purpose, int? technicalOrganizationId, int? offersOpeningCommitteeId,
        int offerPresentationWayId, int userId)
        {
            Tender tender = new Tender();

            tender.UpdateSecondStageBasicData(purpose, technicalOrganizationId, offersOpeningCommitteeId, offerPresentationWayId, userId, null);

            Assert.Single(tender.TenderHistories);
            Assert.Equal(purpose, tender.Purpose);
            Assert.Equal(ObjectState.Modified, tender.State);
        }

        [Fact]
        public void ShouldAddAgencyPlaintRequest()
        {
            Tender tender = new Tender();
            tender.AgencyCommunicationRequests.Add(new AgencyCommunicationRequest(1, (int)Enums.AgencyCommunicationRequestType.Plaint, (int)Enums.AgencyCommunicationRequestStatus.RequestSent, "DataEntry"));
            TenderPlaintCommunicationRequestModel requestModel = new TenderPlaintCommunicationRequestModel() { TenderId = 5, EncryptedTenderId = Util.Encrypt(1) };

            tender.AddAgencyPlaintRequest(requestModel);

            Assert.All(tender.AgencyCommunicationRequests, c => Assert.Equal(ObjectState.Added, c.State));
            Assert.Equal(ObjectState.Modified, tender.State);
        }

        [Fact]
        public void ShouldUpdateAgencyPlaintRequest()
        {
            Tender tender = new Tender();
            tender.AgencyCommunicationRequests.Add(new AgencyCommunicationRequest(1, (int)Enums.AgencyCommunicationRequestType.Plaint, (int)Enums.AgencyCommunicationRequestStatus.RequestSent, "DataEntry"));
            TenderPlaintCommunicationRequestModel requestModel = new TenderPlaintCommunicationRequestModel() { TenderId = 1, EncryptedTenderId = Util.Encrypt(1) };

            tender.AddAgencyPlaintRequest(requestModel);

            Assert.Equal(ObjectState.Modified, tender.State);
        }

        [Fact]
        public void ShouldAddInvitationForAcceptedSuppliersInFirstStage()
        {
            Tender tender = new Tender();
            List<string> suppliers = new List<string>() { "1010000154", "10101010200" };

            tender.AddInvitationForAcceptedSuppliersInFirstStage(suppliers);

            Assert.All(tender.Invitations, c => Assert.Equal(ObjectState.Added, c.State));
            Assert.All(tender.Invitations, c => Assert.Equal((int)Enums.InvitationStatus.ToBeSent, c.StatusId));
            Assert.Equal(ObjectState.Modified, tender.State);
        }

        [Fact]
        public void ShouldAddSupplierInvitationWhileCreation()
        {
            Tender tender = new Tender();
            tender.Invitations.Add(new Invitation("10101000154", Enums.InvitationStatus.ToBeSent, Enums.InvitationRequestType.Invitation, false));
            List<string> suppliers = new List<string>() { "1010000154", "10101010200" };
            var userId = 1121;

            tender.AddSupplierInvitationWhileCreation(suppliers, userId);

            Assert.Single(tender.TenderHistories);
            Assert.Equal((int)Enums.TenderStatus.Established, tender.TenderStatusId);
            Assert.Equal(ObjectState.Modified, tender.State);
        }


        [Theory]
        [InlineData(0, "10101000154")]
        public void ShouldRemoveInvitationsForUnregisteredSupplier(int tenderId, string crnumber)
        {
            Tender tender = new Tender();
            tender.UnRegisteredSuppliersInvitation.Add(new UnRegisteredSuppliersInvitation("10101000154", 1, "WW@ww.com", "010202020", Enums.InvitationStatus.New, "Test"));

            tender.RemoveInvitationsForUnregisteredSupplierByCrAndTenderId(tenderId, crnumber);

            Assert.All(tender.UnRegisteredSuppliersInvitation, a => Assert.Equal(ObjectState.Deleted, a.State));
            Assert.Equal(ObjectState.Modified, tender.State);
        }

        [Theory]
        [InlineData("10101000154", (int)UnRegisteredSuppliersInvitationType.Existed, "aa@aa.com", "0100002020", Enums.InvitationStatus.Approved, "Description")]
        public void ShouldAddInvitationsForUnRegisteredSupplier(string crNumber, int invitationType, string email, string mobile, InvitationStatus invitationStatus, string description)
        {
            Tender tender = new Tender();

            tender.AddInvitationsForUnRegisteredSupplier(crNumber, invitationType, email, mobile, invitationStatus, description);

            Assert.All(tender.UnRegisteredSuppliersInvitation, a => Assert.Equal(ObjectState.Added, a.State));
            Assert.All(tender.Invitations, a => Assert.Equal(ObjectState.Added, a.State));
        }

        [Fact]
        public void ShouldAddPostQualificationInvitations()
        {
            Tender tender = new Tender();
            tender.PostQualificationInvitations.Add(new PostQualificationSuppliersInvitations("1010000154"));
            List<string> suppliers = new List<string>() { "10101010200" };

            tender.AddPostQualificationInvitations(suppliers);

            Assert.NotEmpty(tender.PostQualificationInvitations);
        }

        [Fact]
        public void ShouldUpdatePostQualificationData()
        {
            Tender tender = new Tender();
            PreQualificationSavingModel qualificationSavingModel = new TenderDefault().GetPreQualificationSavingModel();
            List<TenderAttachment> tenderAttachments = new List<TenderAttachment>();
            var userId = 1121;

            tender.UpdatePostQualificationData(qualificationSavingModel, tenderAttachments, userId);

            Assert.Single(tender.TenderHistories);
            Assert.Equal(ObjectState.Modified, tender.State);
        }

        [Fact]
        public void ShouldSetCheckingDateFlagForUnitNotification()
        {
            Tender tender = new Tender();

            tender.SetCheckingDateFlagForUnitNotification(true);

            Assert.True(tender.CheckingDateSet);
        }

        [Fact]
        public void ShouldSetFinancialCheckingDateFlagForUnitNotification()
        {
            Tender tender = new Tender();

            tender.SetFinancialCheckingDateFlagForUnitNotification(true);

            Assert.True(tender.FinancialCheckingDateSet);
        } 
        [Fact]
        public void ShouldSetTenderLocalContent()
        {
            Tender tender = new Tender();
            TenderLocalContent tenderLocalContent = new TenderLocalContent();
            tender.SetTenderLocalContent(tenderLocalContent);

            Assert.Equal(tender.TenderLocalContent, tenderLocalContent);
        }

        [Theory]
        [InlineData(1, "Table 1", 1)]
        public void ShouldCreateTenderQuantityTables(int tableId, string name, int formId)
        {
            Tender tender = new Tender();

            tender.CreateTenderQuantityTables(tableId, name, formId);

            Assert.Single(tender.TenderQuantityTables);
            Assert.Equal(ObjectState.Modified, tender.State);
        }

        [Theory]
        [InlineData(11123, "DataEntry")]
        public void ShouldCreateTenderQuantityTableChangeRequest(int userId, string userRole)
        {
            Tender tender = new Tender();

            tender.CreateTenderQuantityTableChangeRequest(userId, userRole);

            Assert.Single(tender.ChangeRequests);
            Assert.Equal(ObjectState.Modified, tender.State);
        }

        [Theory]
        [InlineData(0, 1)]
        public void ShouldDeleteQuantityTableItems(long tableId, int itemNumber)
        {
            Tender tender = new Tender();
            tender.TenderQuantityTables.Add(new TenderDefault().GetTenderQuantityTable());

            tender.DeleteQuantityTableItems(tableId, itemNumber);

            Assert.Empty(tender.TenderQuantityTables.FirstOrDefault().QuantitiyItemsJson.TenderQuantityTableItems);
            Assert.Equal(ObjectState.Modified, tender.State);
        }

        [Fact]
        public void ShouldDeleteQualificationConfigurations()
        {
            Tender tender = new Tender();
            tender.QualificationConfigurations.Add(new QualificationConfiguration(1, 1, 1, 12, 20, 20));

            tender.DeleteQualificationConfigurations();

            Assert.All(tender.QualificationConfigurations, t => Assert.Equal(ObjectState.Deleted, t.State));
        }

        [Fact]
        public void ShouldDeleteQualificationSubCategoryConfigurations()
        {
            Tender tender = new Tender();
            tender.QualificationSubCategoryConfigurations.Add(new QualificationSubCategoryConfiguration(1, 1, 10));

            tender.DeleteQualificationSubCategoryConfigurations();

            Assert.All(tender.QualificationSubCategoryConfigurations, t => Assert.Equal(ObjectState.Deleted, t.State));
        }

        [Theory]
        [InlineData(1)]
        public void ShouldDeleteCancelRequestedByDataEntry(int qualificationId)
        {
            Tender tender = new Tender();
            tender.ChangeRequests.Add(new TenderChangeRequest(1, (int)Enums.ChangeRequestType.Canceling, (int)Enums.ChangeStatusType.Pending, "NewMonafasat_DataEntry", null));

            tender.DeleteCancelRequestedByDataEntry(qualificationId);

            Assert.All(tender.ChangeRequests, t => Assert.False(t.IsActive));
        }

        [Theory]
        [InlineData(1)]
        public void ShouldDeletePendingCancelRequests(int qualificationId)
        {
            Tender tender = new Tender();
            tender.ChangeRequests.Add(new TenderChangeRequest(1, (int)Enums.ChangeRequestType.Canceling, (int)Enums.ChangeStatusType.Pending, "NewMonafasat_DataEntry", null));

            tender.DeletePendingCancelRequests(qualificationId);

            Assert.All(tender.ChangeRequests, t => Assert.False(t.IsActive));
        }

        [Fact]
        public void ShouldDeleteAllCancelRequest()
        {
            Tender tender = new Tender();
            tender.ChangeRequests.Add(new TenderChangeRequest(1, (int)Enums.ChangeRequestType.Canceling, (int)Enums.ChangeStatusType.Pending, "NewMonafasat_DataEntry", null));

            tender.DeleteAllCancelRequest();

            Assert.All(tender.ChangeRequests, t => Assert.False(t.IsActive));
        }

        [Theory]
        [InlineData("Table Name")]
        public void ShouldSaveTenderQuantityTableItems(string tableName)
        {
            Tender tender = new Tender();
            tender.TenderQuantityTables.Add(new TenderQuantityTable(1,"Table Name",1));
            List<TenderQuantityItemDTO> table = new List<TenderQuantityItemDTO>()
                {new TenderQuantityItemDTO() {TableId = 0}};
            tender.SaveTenderQuantityTableItems(table,tableName);

            Assert.Equal(ObjectState.Modified, tender.State);

        }

        [Theory]
        [InlineData(0,0,1)]
        public void ShouldDeleteQuantityTableItemsChanges(int changeRequestId, long tableId, int itemNumber)
        {
            Tender tender = new Tender();
            tender.ChangeRequests.Add(new TenderChangeRequest(1, (int)Enums.ChangeRequestType.Canceling, (int)Enums.ChangeStatusType.Pending, "NewMonafasat_DataEntry", null));
            tender.ChangeRequests.FirstOrDefault().TenderQuantityTableChanges.Add(new ChangeRequestDefault().GeTenderQuantityTableChanges());
            tender.DeleteQuantityTableItemsChanges(changeRequestId,tableId,itemNumber);

            Assert.Equal(ObjectState.Modified, tender.State);

        }

        [Theory]
        [InlineData(1, 1)]
        public void ShouldCheckisTenderFinalAwarded(int esclationPeriod, int plaintReviewingPeriod)
        {
            Tender tender = new Tender();
            tender.AddActionHistory((int)Enums.TenderStatus.OffersAwardedConfirmed,Enums.TenderActions.ApproveAwarding,"",1);
            tender.AgencyCommunicationRequests.Add(new AgencyCommunicationRequest(1,1));
            tender.UpdateAwardingStoppingPeriod(10);

            tender.isTenderFinalAwarded(esclationPeriod, plaintReviewingPeriod);

            Assert.Equal(ObjectState.Modified, tender.State);

        }



        [Theory]
        [InlineData(3)]
        public void AddActivityVersion_WithActivityVersionId_NewAddedEntityInTenderVersions(int versionId)
        {
            var tender = new Tender();
            
            tender.AddActivityVersion(versionId);

            Assert.Single(tender.TenderVersions);
        }
      
        [Fact]
        public void SetTenderDateTest()
        {
            var tender = new Tender();
            TenderDates tenderDates = new TenderDates();
            tender.SetTenderDates(tenderDates);
            Assert.Equal(ObjectState.Modified, tender.State);
        }

        [Fact]
        public void SetTenderAddressTest()
        {
            var tender = new Tender();

            TenderAddress tenderAddress = new TenderAddress();

            tender.SetTenderAddress(tenderAddress);
            Assert.Equal(ObjectState.Modified, tender.State);

        }

        [Fact]
        public void ShouldSetNationalProductPercentage()
        {    
            var tender = new Tender();
            tender.AddIsTenderContainsTawreedTables_ForTest(true);
            tender.SetNationalProductPercentage(10);
            tender.NationalProductPercentage.ShouldBe(10);
        }
        [Fact]
        public void ShouldGetTenderQuantityTableLastIndexInEdit()
        {
            Tender tender = new Tender();
            TenderChangeRequest tenderChangeRequest = new TenderChangeRequest(1, (int)Enums.ChangeRequestType.Canceling, (int)Enums.ChangeStatusType.Pending, "NewMonafasat_DataEntry", null);
            typeof(TenderChangeRequest).GetProperty(nameof(TenderChangeRequest.TenderChangeRequestId)).SetValue(tenderChangeRequest, 1);
            tender.ChangeRequests.Add(tenderChangeRequest);
            var changeRequestItems = new ChangeRequestDefault().GeTenderQuantityTableChanges();
            typeof(TenderQuantityTableChanges).GetProperty(nameof(TenderQuantityTableChanges.Id)).SetValue(changeRequestItems, 1);

            tender.ChangeRequests.FirstOrDefault().TenderQuantityTableChanges.Add(changeRequestItems);


            var lastIndex = tender.GetTenderQuantityTableLastIndexInEdit(1, 1);

            Assert.Equal(0, lastIndex);
        }
    }
}
