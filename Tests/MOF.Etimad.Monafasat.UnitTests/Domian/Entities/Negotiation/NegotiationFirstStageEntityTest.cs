using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Core.Entities.Negotiations;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernal.Exceptions;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.TestsBuilders;
using MOF.Etimad.Monafasat.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Negotiation
{
    public class NegotiationFirstStageEntityTest
    {
        [Theory]
        [InlineData(2000)]
        [InlineData(0)]
        public void AcceptNegotiationWithExtraDiscountShouldFaildIfGreaterThanTargetedAmount(decimal discountAmount)
        {
            NegotiationFirstStage negotiationFirstStage = new NegotiationDefaults().GetNegotiationFirstStage();

            var e = Assert.ThrowsAsync<BusinessRuleException>(async () => negotiationFirstStage.AgreeWithExtraDiscount(discountAmount));

            Assert.Equal(Resources.CommunicationRequest.ErrorMessages.NegotiationFirstStageExtraDiscountValueValidation, e.Result.Message);
        }

        [Theory]
        [InlineData(1000)]
        public void AcceptNegotiationWithExtraDiscountShould(decimal discountAmount)
        {
            NegotiationFirstStage negotiationFirstStage = new NegotiationDefaults().GetNegotiationFirstStage();

            negotiationFirstStage.AgreeWithExtraDiscount(discountAmount);

            Assert.Equal((int)Enums.enNegotiationStatus.SupplierAgreedWithExtraDiscount, negotiationFirstStage.StatusId);
            Assert.Equal(discountAmount, negotiationFirstStage.ExtraDiscountValue);
        }


        [Fact]
        public void ShouldAddNewNegotiationFirstStage()
        {
            NegotiationFirstStage negotiationFirstStage = new NegotiationFirstStage(2, "ref323", 1200, 1, 2, new List<ViewModel.NegotiationAttachmentViewModel>(), (int)Enums.enNegotiationStatus.New, "33");

            Assert.Equal((int)Enums.enNegotiationStatus.New, negotiationFirstStage.StatusId);
            Assert.Equal(ObjectState.Added, negotiationFirstStage.State);
        }

        [Fact]
        public void ShouldUpdateNegotiationFirstStageData()
        {
            NegotiationFirstStage negotiationFirstStage = new NegotiationFirstStage();

            negotiationFirstStage.UpdateNegotiationFirstStage(2, "ref323", 1200, 1, (int)Enums.enNegotiationStatus.UnderUpdate, new List<ViewModel.NegotiationAttachmentViewModel>(), "33");

            Assert.Equal((int)Enums.enNegotiationStatus.UnderUpdate, negotiationFirstStage.StatusId);
            Assert.Equal(ObjectState.Modified, negotiationFirstStage.State);
        }

        [Theory]
        [InlineData((int)Enums.enNegotiationStatus.SentToSuppliers, "Test Reject")]
        public void ShouldUpdateNegotiationFirstStageStatusWithRejectionReason(int statusId, string rejectionReason)
        {
            NegotiationFirstStage negotiationFirstStage = new NegotiationFirstStage();

            negotiationFirstStage.UpdateNegotiationStatus(statusId, rejectionReason);

            Assert.Equal((int)Enums.enNegotiationStatus.SentToSuppliers, negotiationFirstStage.StatusId);
            Assert.Equal(rejectionReason, negotiationFirstStage.RejectionReason);
            Assert.Equal(ObjectState.Modified, negotiationFirstStage.State);
        }

        [Theory]
        [InlineData(4)]
        public void ShouldUpdateReplayPeriodHours(int replyHours)
        {
            NegotiationFirstStage negotiationFirstStage = new NegotiationDefaults().GetNegotiationFirstStage();

            negotiationFirstStage.UpdateReplyPeriod(replyHours);

            Assert.Equal(replyHours, negotiationFirstStage.SupplierReplyPeriodHours);
        }

        [Theory]
        [InlineData(4, "1010000154")]
        public void ShouldStartNegotiation(int offerId, string cr)
        {
            NegotiationFirstStage negotiationFirstStage = new NegotiationDefaults().GetNegotiationFirstStage();
            NegotiationFirstStageSupplier negotiationFirstStageSupplier = new NegotiationDefaults().GetNegotiationFirstStageSupplier(offerId, cr);

            negotiationFirstStage.AddSupplier(negotiationFirstStageSupplier);

            Assert.Equal(offerId, negotiationFirstStage.NegotiationFirstStageSuppliers.FirstOrDefault(nego => nego.OfferId == offerId).OfferId);
            Assert.Equal(cr, negotiationFirstStage.NegotiationFirstStageSuppliers.FirstOrDefault(nego => nego.OfferId == offerId).SupplierCR);
        }

        [Fact]
        public void ShouldUpdateNegotiationAttachments()
        {
            NegotiationFirstStage negotiationFirstStage = new NegotiationDefaults().GetNegotiationFirstStage();
            List<NegotiationAttachmentViewModel> attachments = new NegotiationDefaults().GetNegotiationAttachments();

            negotiationFirstStage.UpdateAttachments(attachments);

            Assert.Equal(ObjectState.Added, negotiationFirstStage.Attachments.FirstOrDefault().State);
        }

        [Fact]
        public void ShouldNegotiationCommunicationRequest()
        {
            AgencyCommunicationRequest agencyRequest = new AgencyCommunicationRequest();
            NegotiationFirstStage negotiationFirstStage = new NegotiationDefaults().GetNegotiationFirstStage();

            negotiationFirstStage.SetNegotiationCommunicationRequest(agencyRequest);

            Assert.Equal(negotiationFirstStage.AgencyCommunicationRequest, agencyRequest);
        }

        [Fact]
        public void ShouldDeactivateNegotiation()
        {
            NegotiationFirstStage negotiationFirstStage = new NegotiationDefaults().GetNegotiationFirstStage();

            negotiationFirstStage.DeActive();

            Assert.Equal(false, negotiationFirstStage.IsActive);
        }

        [Fact]
        public void ShouldUpdateNegotiationFirstStageStatus()
        {
            NegotiationFirstStage negotiationFirstStage = new NegotiationDefaults().GetNegotiationFirstStage();

            negotiationFirstStage.UpdateNegotiationFirstStageStatus(1, "Test Reason");

            Assert.Equal(1, negotiationFirstStage.StatusId);
            Assert.Equal("Test Reason", negotiationFirstStage.RejectionReason);
        }

        [Theory]
        [InlineData(1)]
        public void ShouldStartNegotiationFirstStageStatus(int offerId)
        {
            NegotiationFirstStage negotiationFirstStage = new NegotiationDefaults().GetNegotiationFirstStage();
            var supplierNegotiation = negotiationFirstStage.NegotiationFirstStageSuppliers.FirstOrDefault(x => x.OfferId == offerId);

            negotiationFirstStage.StartNegotiation(offerId);

            Assert.Equal(DateTime.Now.Date, supplierNegotiation.PeriodStartDateTime.Value.Date);
            Assert.Equal((int)Enums.enNegotiationSupplierStatus.PendeingSupplierReply, supplierNegotiation.NegotiationSupplierStatusId);
        }

        [Theory]
        [InlineData(1)]
        public void ShouldUpdateSupplierNegotiationStatus(int offerId)
        {
            NegotiationFirstStage negotiationFirstStage = new NegotiationDefaults().GetNegotiationFirstStage();
            var supplierNegotiation = negotiationFirstStage.NegotiationFirstStageSuppliers.FirstOrDefault(x => x.OfferId == offerId);

            negotiationFirstStage.UpdateSupplierStatus(0, (int)Enums.enNegotiationSupplierStatus.NoReply, DateTime.Now.Date);

            Assert.Equal(DateTime.Now.Date, supplierNegotiation.PeriodStartDateTime.Value.Date);
            Assert.Equal((int)Enums.enNegotiationSupplierStatus.NoReply, supplierNegotiation.NegotiationSupplierStatusId);
        }


    }
}
