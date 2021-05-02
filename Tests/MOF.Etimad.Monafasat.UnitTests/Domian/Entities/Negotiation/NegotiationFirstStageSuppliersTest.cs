using MOF.Etimad.Monafasat.Core.Entities.Negotiations;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernel;
using System;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Negotiation
{
    public class NegotiationFirstStageSuppliersTest
    {


        [Fact]
        public void ShouldCreateNewNegotiationSupplier()
        {
            NegotiationFirstStageSupplier negotiationFirstStageSupplier = new NegotiationFirstStageSupplier((int)Enums.enNegotiationSupplierStatus.Agree, DateTime.Now, 1, "101000154", 1, 50);

            Assert.Equal(ObjectState.Added, negotiationFirstStageSupplier.State);
        }
        [Theory]
        [InlineData((int)Enums.enNegotiationSupplierStatus.Agree)]
        public void ShouldUpdateNegotiationFirstStageSupplierStatusAndPeriod(int negotiationStatusId)
        {
            NegotiationFirstStageSupplier negotiationFirstStageSupplier = new NegotiationFirstStageSupplier();

            negotiationFirstStageSupplier.UpdateNegotiationFirstStageSupplier(negotiationStatusId, DateTime.Now.Date);

            Assert.Equal(negotiationStatusId, negotiationFirstStageSupplier.NegotiationSupplierStatusId);
            Assert.Equal(DateTime.Now.Date, negotiationFirstStageSupplier.PeriodStartDateTime);
            Assert.Equal(ObjectState.Modified, negotiationFirstStageSupplier.State);

        }

        [Fact]
        public void ShouldUpdateSupplierStatusAndStartNegotiationPeriod()
        {
            NegotiationFirstStageSupplier negotiationFirstStageSupplier = new NegotiationFirstStageSupplier();

            negotiationFirstStageSupplier.StartSupplierPeriodService();

            Assert.Equal((int)Enums.enNegotiationSupplierStatus.PendeingSupplierReply, negotiationFirstStageSupplier.NegotiationSupplierStatusId);
            Assert.Equal(DateTime.Now.Date, negotiationFirstStageSupplier.PeriodStartDateTime.Value.Date);
            Assert.Equal(ObjectState.Modified, negotiationFirstStageSupplier.State);
        }

        [Fact]
        public void ShouldNegotiationAsReportedToSupplier()
        {
            NegotiationFirstStageSupplier negotiationFirstStageSupplier = new NegotiationFirstStageSupplier();

            negotiationFirstStageSupplier.SetAsReported(true);

            Assert.True(negotiationFirstStageSupplier.IsReported);
            Assert.Equal(ObjectState.Modified, negotiationFirstStageSupplier.State);
        }

        [Theory]
        [InlineData(1)]
        public void ShouldUpdateNegotiationId(int negotiationId)
        {
            NegotiationFirstStageSupplier negotiationFirstStageSupplier = new NegotiationFirstStageSupplier();

            negotiationFirstStageSupplier.UpdateNegotiationId(negotiationId);

            Assert.Equal(negotiationId, negotiationFirstStageSupplier.NegotiationId);
            Assert.Equal(ObjectState.Modified, negotiationFirstStageSupplier.State);
        }
    }
}
