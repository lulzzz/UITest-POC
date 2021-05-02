using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Core.Entities.Negotiations;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.TestsBuilders;
using MOF.Etimad.Monafasat.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Negotiation
{
    public class NegotiationSecondStageEntityTest
    {


        [Theory]
        [InlineData(1, 1, 1, 1)]
        public void ShouldCreateNewNegotiationSecondStage(int agencyRequestId, int? reasonId, int firstStageId, int offerId)
        {
            NegotiationSecondStage negotiationSecondStage = new NegotiationSecondStage(agencyRequestId, reasonId, firstStageId, offerId);

            Assert.Equal((int)Enums.enNegotiationStatus.UnderUpdate, negotiationSecondStage.StatusId);
            Assert.Equal(offerId, negotiationSecondStage.OfferId);
            Assert.Equal(ObjectState.Added, negotiationSecondStage.State);
        }

        [Fact]
        public void ShouldUpdateStartNegotiationPeriod()
        {
            NegotiationSecondStage negotiationSecondStage = new NegotiationSecondStage();

            negotiationSecondStage.StartSuppierPeriod();

            Assert.Equal(DateTime.Now.Date, negotiationSecondStage.PeriodStartDate.Value.Date);
        }

        [Theory]
        [InlineData(1, (int)Enums.enNegotiationStatus.CheckManagerPendingApprove)]
        public void ShouldUpdateNegotiationReasonAndStatus(int reasonId, int statusId)
        {
            NegotiationSecondStage negotiationSecondStage = new NegotiationSecondStage();

            negotiationSecondStage.UpdateNegotiationSecondStage(reasonId, statusId);

            Assert.Equal(statusId, negotiationSecondStage.StatusId);
            Assert.Equal(reasonId, negotiationSecondStage.NegotiationReasonId);
            Assert.Equal(ObjectState.Modified, negotiationSecondStage.State);
        }

        [Theory]
        [InlineData((int)Enums.enNegotiationStatus.New)]
        public void ShouldUpdateSecondNegotiationStatus(int statusId)
        {
            NegotiationSecondStage negotiationSecondStage = new NegotiationSecondStage();

            negotiationSecondStage.UpdateSecondNegotiationStatus(statusId);

            Assert.Equal(statusId, negotiationSecondStage.StatusId);
            Assert.Equal(ObjectState.Modified, negotiationSecondStage.State);
        }


        [Theory]
        [InlineData((int)Enums.enNegotiationStatus.SentToSuppliers)]
        public void ShouldUpdateSecondNegotiationPeriodWhenSendingItToSuppliers(int statusId)
        {
            NegotiationSecondStage negotiationSecondStage = new NegotiationSecondStage();

            negotiationSecondStage.UpdateSecondNegotiationStatus(statusId);

            Assert.Equal(statusId, negotiationSecondStage.StatusId);
            Assert.Equal(DateTime.Now.Date, negotiationSecondStage.PeriodStartDate.Value.Date);
            Assert.Equal(ObjectState.Modified, negotiationSecondStage.State);
        }

        [Fact]
        public void ShouldAddSupplierQuantityTables()
        {
            NegotiationSecondStage negotiationSecondStage = new NegotiationSecondStage();
            SupplierTenderQuantityTable supplierTenderQuantityTable = new NegotiationDefaults().GetSupplierTenderQuantityTable();
            List<SupplierTenderQuantityTable> tenderTablelist = new List<SupplierTenderQuantityTable>() { supplierTenderQuantityTable };

            negotiationSecondStage.AddSupplierQuantityTables(tenderTablelist);

            Assert.Single(negotiationSecondStage.negotiationSupplierQuantitiestable);
            Assert.Equal(ObjectState.Added, negotiationSecondStage.negotiationSupplierQuantitiestable.FirstOrDefault().State);
        }

        [Fact]
        public void ShouldUpdatNegotiationQuantityTablesItems()
        {
            NegotiationSecondStage negotiationSecondStage = new NegotiationDefaults().GetNegotiationSecondStageWithQTables();
            TenderQuantityItemDTO tableDto = new NegotiationDefaults().GetTenderQuantityItemDTO();

            negotiationSecondStage.UpdateNegotiationQItems(new List<TenderQuantityItemDTO>() { tableDto });

            Assert.Single(negotiationSecondStage.negotiationSupplierQuantitiestable);
            Assert.Equal(ObjectState.Added, negotiationSecondStage.negotiationSupplierQuantitiestable.FirstOrDefault().State);
        }

        [Fact]
        public void ShouldDeleteNegotiationQuantityTablesItems()
        {
            NegotiationSecondStage negotiationSecondStage = new NegotiationDefaults().GetNegotiationSecondStageWithQTables();
            TenderQuantityItemDTO tableDto = new NegotiationDefaults().GetTenderQuantityItemDTO();

            negotiationSecondStage.DeleteNegotiationQItems(new List<TenderQuantityItemDTO>() { tableDto });

            Assert.Empty(negotiationSecondStage.negotiationSupplierQuantitiestable.FirstOrDefault().NegotiationQuantityItemJson.NegotiationSupplierQuantityTableItems);
        }
    }
}
