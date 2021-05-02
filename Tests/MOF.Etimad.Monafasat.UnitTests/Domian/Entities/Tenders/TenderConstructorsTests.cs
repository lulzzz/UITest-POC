using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernel;
using System;
using System.Collections.Generic;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Tenders
{
    public class TenderConstructorsTests
    {
        [Fact]
        public void ConstructDefaultTender()
        {
            Tender tender = new Tender();

            Assert.NotNull(tender);
        }

        [Fact]
        public void ConstructGeneralTender()
        {
            Tender tender = new Tender("123456789", 1, (int)Enums.TenderType.Competition, 1, "Tender 1", "1234", "General Tender", null, 12, 12, 13, 14, 14, 0, 0, "reason", 1, 1, 1, 1000000, 100, 123, 2, 1, 5, 1, new List<string>() { "987654321", "567894321" }, 1, 10000, 1, "Riyadh", false, false, "", 1, new List<AgencyBudgetNumber>());

            Assert.Equal("Tender 1", tender.TenderName);
            Assert.Equal("123456789", tender.AgencyCode);
            Assert.Equal((int)Enums.TenderStatus.UnderEstablishing, tender.TenderStatusId);
            Assert.Single(tender.TenderHistories);
            Assert.Equal((int)Enums.TenderType.Competition, tender.TenderTypeId);
            Assert.Equal(0, tender.ConditionsBookletPrice);
            Assert.Equal(1, tender.QuantityTableVersionId);
            Assert.Null(tender.SupplierNeedSample);
            Assert.Equal(ObjectState.Added, tender.State);
        }

        [Fact]
        public void ConstructGeneralTenderWithSamples()
        {
            Tender tender = new Tender("123456789", 1, (int)Enums.TenderType.Competition, null, "Tender 1", "1234", "General Tender", 1, 12, 12, 13, 14, 14, 0, 0, "reason", 1, 1, 1, 1000000, 100, 123, 2, 1, 5, 1, new List<string>() { "987654321", "567894321" }, 1, 10000, 1, "Riyadh", true, true, "Olyia, Riyadh", 1, new List<AgencyBudgetNumber>());

            Assert.Equal("Tender 1", tender.TenderName);
            Assert.Equal("123456789", tender.AgencyCode);
            Assert.Equal((int)Enums.TenderStatus.UnderEstablishing, tender.TenderStatusId);
            Assert.Single(tender.TenderHistories);
            Assert.Equal((int)Enums.TenderType.Competition, tender.TenderTypeId);
            Assert.Equal(0, tender.ConditionsBookletPrice);
            Assert.Equal(1, tender.QuantityTableVersionId);
            Assert.True(tender.SupplierNeedSample);
            Assert.Equal("Olyia, Riyadh", tender.SamplesDeliveryAddress);
            Assert.Equal(ObjectState.Added, tender.State);
        }

        [Fact]
        public void ConstructTenderForQualification()
        {
            Tender tender = new Tender(0, (int)Enums.TenderType.NewDirectPurchase, "Tender 1", 1, 1, null, new DateTime(2020, 9, 15), new DateTime(2020, 10, 15), new DateTime(2020, 10, 16), new DateTime(2020, 10, 17), (int)Enums.TenderStatus.UnderEstablishing, "123456789", 1, 1, null);

            Assert.Equal("Tender 1", tender.TenderName);
            Assert.Equal((int)Enums.TenderStatus.UnderEstablishing, tender.TenderStatusId);
            Assert.Equal((int)Enums.TenderType.NewDirectPurchase, tender.TenderTypeId);
            Assert.Equal(1, tender.TechnicalOrganizationId);
            Assert.Equal(1, tender.OffersCheckingCommitteeId);
            Assert.Equal(ObjectState.Added, tender.State);
        }

        [Fact]
        public void ConstructTenderForQualificationUpdate()
        {
            Tender tender = new Tender(1, (int)Enums.TenderType.NewDirectPurchase, "Tender 1", 1, 1, null, new DateTime(2020, 9, 15), new DateTime(2020, 10, 15), new DateTime(2020, 10, 16), new DateTime(2020, 10, 17), (int)Enums.TenderStatus.UnderEstablishing, "123456789", 1, 1, null);

            Assert.Equal("Tender 1", tender.TenderName);
            Assert.Equal((int)Enums.TenderStatus.UnderEstablishing, tender.TenderStatusId);
            Assert.Equal((int)Enums.TenderType.NewDirectPurchase, tender.TenderTypeId);
            Assert.Equal(1, tender.TechnicalOrganizationId);
            Assert.Equal(1, tender.OffersCheckingCommitteeId);
            Assert.Equal("123456789", tender.AgencyCode);
            Assert.Equal(1, tender.BranchId);
            Assert.Equal(1, tender.PostQualificationTenderId);
            Assert.Null(tender.PreQualificationCommitteeId);
            Assert.Equal(ObjectState.Modified, tender.State);
        }

        [Fact]
        public void ConstructTenderSecondStage()
        {
            Tender tender = new Tender((int)Enums.TenderType.SecondStageTender, "Tender 1", "123456789", "Two Stages Tender", 1, 2, 3, (int)Enums.OfferPresentationWayId.OneFile, 12, "987654321", 1, 1000000, 1, "Olyia", 10, new List<AgencyBudgetNumber>() { new AgencyBudgetNumber() });

            Assert.Equal((int)Enums.TenderType.SecondStageTender, tender.TenderTypeId);
            Assert.Equal("Tender 1", tender.TenderName);
            Assert.Equal("987654321", tender.AgencyCode);
            Assert.Equal((int)Enums.TenderStatus.UnderEstablishing, tender.TenderStatusId);
            Assert.Equal(ObjectState.Added, tender.State);
        }

        [Fact]
        public void SetPointToPassToQualificationTest()
        {
            Tender tender = new Tender();

            tender.SetPointToPassToQualification(1000, 100, 10000000, (int)Enums.QualificationItemType.Range);

            Assert.Equal(1000, tender.TenderPointsToPass);
            Assert.Equal(100, tender.TechnicalAdministrativeCapacity);
            Assert.Equal(10000000, tender.FinancialCapacity);
            Assert.Equal((int)Enums.QualificationItemType.Range, tender.QualificationTypeId);
        }

        [Fact]
        public void SetPointToPassToPostQualificationTest()
        {
            Tender tender = new Tender();

            tender.SetPointToPassToPostQualification(1000, 100, 10000000, (int)Enums.QualificationItemType.Range);

            Assert.Equal(1000, tender.TenderPointsToPass);
            Assert.Equal(100, tender.TechnicalAdministrativeCapacity);
            Assert.Equal(10000000, tender.FinancialCapacity);
            Assert.Equal((int)Enums.QualificationItemType.Range, tender.QualificationTypeId);
            Assert.Equal(ObjectState.Modified, tender.State);
        }

        [Fact]
        public void ConstructDirectPurchaseTenderBasicWay()
        {
            Tender tender = new Tender("123456789", 1, (int)Enums.TenderType.NewDirectPurchase, 1, "direct purchase Tender 1", "1234", "direct purchase Tender", 12, null, null, 14, null, 2, null, null, "", 1, 1, 1, 1000000, 100, 123, 2, 1, 5, 1, new List<string>() { "987654321", "567894321" }, 1, 10000, 1, "Riyadh", false, false, null, 7000, new List<AgencyBudgetNumber>(), false, null);

            Assert.Equal("direct purchase Tender 1", tender.TenderName);
            Assert.False(tender.IsLowBudgetDirectPurchase);
            Assert.Null(tender.DirectPurchaseMemberId);
        }

        [Fact]
        public void ConstructDirectPurchaseTenderLowBudget()
        {
            Tender tender = new Tender("123456789", 1, (int)Enums.TenderType.NewDirectPurchase, 1, "direct purchase Tender 1", "1234", "direct purchase Tender", 12, null, null, 14, null, 2, null, null, "", 1, 1, 1, 20000, 100, 123, 2, 1, 5, 1, new List<string>() { "987654321", "567894321" }, 1, 10000, 1, "Riyadh", false, false, null, 7000, new List<AgencyBudgetNumber>(), true, 2);

            Assert.Equal("direct purchase Tender 1", tender.TenderName);
            Assert.True(tender.IsLowBudgetDirectPurchase);
            Assert.NotNull(tender.DirectPurchaseMemberId);
        }
    }
}
