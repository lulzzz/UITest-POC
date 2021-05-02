using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernal;
using Shouldly;
using System;
using System.Collections.Generic;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Offers
{
    public class OfferTest
    {
        private const string commericalRegisterNo = "4030059098";
        private const int tenderId = 1;
        private const int offerPresentationWayid = 1;
        private const int tableId = 1;
        private const string discount = "10";
        private const string discountNotes = "discount notes";
        private const bool isOfferCopyAttached = true;
        private const bool isOfferLetterAttached = true;
        private const string offerLetterNumber = "10";
        private const bool isVisitationAttached = true;
        private const bool isPurchaseBillAttached = true;
        private const bool isBankGuaranteeAttached = true;
        private const bool isFinaintialOfferLetterAttached = true;
        private const bool iIsFinantialOfferLetterCopyAttached = true;
        private const string exclusionReason = "rason";
        private const decimal price = 800;
        private const decimal priceNP = 600;
        private const decimal discountAmount = 20;
        private const int technicalEvaluationLevel = 20;
        private const int financialWeight = 8;
        private const int acceptanceStatusId = 1;
        private const bool isBindedToMandatoryList = true;
        private const int technicalEvaluationStatusId = 1;
        private const string note = "note";
        private const string rejectionReason = "reason";
        private const string financialnotes = "note";
        private const string finantialOfferLetterNumber = "6";
        private const int offerId = 8;
        private const decimal totalOfferAwardingValue = 200;
        private const decimal partialOfferAwardingValue = 200;
        private const string justificationOfRecommendation = "recommendation";
        private long itemId = 0;
        private const int itemNumber = 1;


        [Fact]
        public void Should_Construct_Offer()
        {
            Offer offer = new Offer(tenderId, commericalRegisterNo, new List<SupplierTenderQuantityTable> { new SupplierTenderQuantityTable() }, offerPresentationWayid);
            _ = new Offer();
            _ = offer.SuplierId;
            _ = offer.OfferPresentationWay;
            _ = offer.Status;
            _ = offer.negotiationFirstStageSuppliers;
            _ = offer.ExtendOffersValiditySupplier;

            offer.ShouldNotBeNull();
        }

        [Fact]
        public void Should_Construct_Offer_Second()
        {
            Offer offer = new Offer(commericalRegisterNo, tenderId, true);
            _ = new Offer();

            offer.ShouldNotBeNull();
        }

        [Fact]
        public void Should_UpdateOfferSupplierQItems()
        {
            Offer offer = new Offer(tenderId, commericalRegisterNo, new List<SupplierTenderQuantityTable> { new SupplierTenderQuantityTable() { Id = 1, QuantitiyItemsJson = new SupplierTenderQuantityTableItemJson(new List<SupplierTenderQuantityTableItem>() { new SupplierTenderQuantityTableItem() { Id = 1 } }) } }, offerPresentationWayid);
            offer.UpdateOfferSupplierQItems(new List<SupplierTenderQuantityTableItem>() { new SupplierTenderQuantityTableItem() { Id = 1, Value = " new val" } }, tableId);
            offer.ShouldNotBeNull();
        }

        [Fact]
        public void Should_UpdateOfferSupplierQItemsDefault()
        {
            Offer offer = new Offer(tenderId, commericalRegisterNo, new List<SupplierTenderQuantityTable> { new SupplierTenderQuantityTable() { Id = 1, QuantitiyItemsJson = new SupplierTenderQuantityTableItemJson(new List<SupplierTenderQuantityTableItem>() { new SupplierTenderQuantityTableItem() { Id = 1 } }) } }, offerPresentationWayid);
            offer.UpdateOfferSupplierQItemsDefault(new Dictionary<long, bool>() { { 1, true } }, tableId);
            offer.ShouldNotBeNull();
        }

        [Fact]
        public void Should_UpadteSupplierQTableItemsAlternativeValue()
        {
            Offer offer = new Offer(tenderId, commericalRegisterNo, new List<SupplierTenderQuantityTable> { new SupplierTenderQuantityTable() { Id = 1, QuantitiyItemsJson = new SupplierTenderQuantityTableItemJson(new List<SupplierTenderQuantityTableItem>() { new SupplierTenderQuantityTableItem() { Id = 1 } }) } }, offerPresentationWayid);
            offer.UpadteSupplierQTableItemsAlternativeValue(new List<SupplierTenderQuantityTableItem>() { new SupplierTenderQuantityTableItem() { Id = 1 } }, tableId);
            offer.ShouldNotBeNull();
        }

        [Fact]
        public void Should_UpdateOpeningDiscountNotes()
        {
            Offer offer = new Offer(tenderId, commericalRegisterNo, new List<SupplierTenderQuantityTable> { new SupplierTenderQuantityTable() { Id = 1, QuantitiyItemsJson = new SupplierTenderQuantityTableItemJson(new List<SupplierTenderQuantityTableItem>() { new SupplierTenderQuantityTableItem() { Id = 1 } }) } }, offerPresentationWayid);
            offer.UpdateOpeningDiscountNotes(discount, discountNotes);
            Assert.Equal(discount, offer.Discount);
        }

        [Fact]
        public void Should_SetAttachmentsValuesToTrue()
        {
            Offer offer = new Offer(tenderId, commericalRegisterNo, new List<SupplierTenderQuantityTable> { new SupplierTenderQuantityTable() { Id = 1, QuantitiyItemsJson = new SupplierTenderQuantityTableItemJson(new List<SupplierTenderQuantityTableItem>() { new SupplierTenderQuantityTableItem() { Id = 1 } }) } }, offerPresentationWayid);
            offer.SetAttachmentsValuesToTrue();
            Assert.False(offer.IsOpened);
        }

        [Fact]
        public void Should_AddRegisteredSolidaritySupplier()
        {
            Offer offer = new Offer(tenderId, commericalRegisterNo, new List<SupplierTenderQuantityTable> { new SupplierTenderQuantityTable() { Id = 1, QuantitiyItemsJson = new SupplierTenderQuantityTableItemJson(new List<SupplierTenderQuantityTableItem>() { new SupplierTenderQuantityTableItem() { Id = 1 } }) } }, offerPresentationWayid);
            offer.AddRegisteredSolidaritySupplier(new List<OfferSolidarity>() { new OfferSolidarity() });
            Assert.NotEmpty(offer.Combined);
        }

        [Fact]
        public void Should_AddRegisteredCombinedSupplier()
        {
            Offer offer = new Offer(tenderId, commericalRegisterNo, new List<SupplierTenderQuantityTable> { new SupplierTenderQuantityTable() { Id = 1, QuantitiyItemsJson = new SupplierTenderQuantityTableItemJson(new List<SupplierTenderQuantityTableItem>() { new SupplierTenderQuantityTableItem() { Id = 1 } }) } }, offerPresentationWayid);
            offer.AddRegisteredCombinedSupplier(new OfferSolidarity());
            Assert.NotEmpty(offer.Combined);
        }

        [Fact]
        public void Should_AddUnRegisteredCombinedSupplier()
        {
            Offer offer = new Offer(tenderId, commericalRegisterNo, new List<SupplierTenderQuantityTable> { new SupplierTenderQuantityTable() { Id = 1, QuantitiyItemsJson = new SupplierTenderQuantityTableItemJson(new List<SupplierTenderQuantityTableItem>() { new SupplierTenderQuantityTableItem() { Id = 1 } }) } }, offerPresentationWayid);
            offer.AddUnRegisteredCombinedSupplier(new OfferSolidarity());
            Assert.NotEmpty(offer.Combined);
        }

        [Fact]
        public void Should_UpdateOfferAttachments()
        {
            Offer offer = new Offer(tenderId, commericalRegisterNo, new List<SupplierTenderQuantityTable> { new SupplierTenderQuantityTable() { Id = 1, QuantitiyItemsJson = new SupplierTenderQuantityTableItemJson(new List<SupplierTenderQuantityTableItem>() { new SupplierTenderQuantityTableItem() { Id = 1 } }) } }, offerPresentationWayid);
            offer.UpdateOfferAttachments(isOfferCopyAttached, isOfferLetterAttached, offerLetterNumber, DateTime.Now.Date, isVisitationAttached, isPurchaseBillAttached, isBankGuaranteeAttached);
            Assert.Equal(offerLetterNumber, offer.OfferLetterNumber);
        }

        [Fact]
        public void Should_UpdateOfferFinancialAttachments()
        {
            Offer offer = new Offer(tenderId, commericalRegisterNo, new List<SupplierTenderQuantityTable> { new SupplierTenderQuantityTable() { Id = 1, QuantitiyItemsJson = new SupplierTenderQuantityTableItemJson(new List<SupplierTenderQuantityTableItem>() { new SupplierTenderQuantityTableItem() { Id = 1 } }) } }, offerPresentationWayid);
            offer.UpdateOfferFinancialAttachments(isFinaintialOfferLetterAttached, iIsFinantialOfferLetterCopyAttached);
            Assert.Equal(isFinaintialOfferLetterAttached, offer.IsFinaintialOfferLetterAttached);
        }

        [Fact]
        public void Should_UpdateTechnicianReportAttachments()
        {
            Offer offer = new Offer(tenderId, commericalRegisterNo, new List<SupplierTenderQuantityTable> { new SupplierTenderQuantityTable() { Id = 1, QuantitiyItemsJson = new SupplierTenderQuantityTableItemJson(new List<SupplierTenderQuantityTableItem>() { new SupplierTenderQuantityTableItem() { Id = 1 } }) } }, offerPresentationWayid);
            offer.UpdateTechnicianReportAttachments(new List<TechnicianReportAttachment>() { new TechnicianReportAttachment() { } });
            offer.UpdateTechnicianReportAttachments(new List<TechnicianReportAttachment>() { new TechnicianReportAttachment() { } });
            Assert.NotEmpty(offer.TechnicianReportAttachments);
        }

        [Fact]
        public void Should_AddExclusionReasonForSuppliers()
        {
            Offer offer = new Offer(tenderId, commericalRegisterNo, new List<SupplierTenderQuantityTable> { new SupplierTenderQuantityTable() { Id = 1, QuantitiyItemsJson = new SupplierTenderQuantityTableItemJson(new List<SupplierTenderQuantityTableItem>() { new SupplierTenderQuantityTableItem() { Id = 1 } }) } }, offerPresentationWayid);
            offer.AddExclusionReasonForSuppliers(exclusionReason);
            Assert.Equal(exclusionReason, offer.ExclusionReason);
        }

        [Fact]
        public void Should_RemoveExclusionReasonForSuppliers()
        {
            Offer offer = new Offer(tenderId, commericalRegisterNo, new List<SupplierTenderQuantityTable> { new SupplierTenderQuantityTable() { Id = 1, QuantitiyItemsJson = new SupplierTenderQuantityTableItemJson(new List<SupplierTenderQuantityTableItem>() { new SupplierTenderQuantityTableItem() { Id = 1 } }) } }, offerPresentationWayid);
            offer.RemoveExclusionReasonForSuppliers();
            Assert.True(string.IsNullOrEmpty(offer.ExclusionReason));
        }

        [Fact]
        public void Should_ResetAwardingValue()
        {
            Offer offer = new Offer(tenderId, commericalRegisterNo, new List<SupplierTenderQuantityTable> { new SupplierTenderQuantityTable() { Id = 1, QuantitiyItemsJson = new SupplierTenderQuantityTableItemJson(new List<SupplierTenderQuantityTableItem>() { new SupplierTenderQuantityTableItem() { Id = 1 } }) } }, offerPresentationWayid);
            offer.ResetAwardingValue();
            Assert.Null(offer.PartialOfferAwardingValue);
        }

        [Fact]
        public void Should_ResetOfferToAwarding()
        {
            Offer offer = new Offer(tenderId, commericalRegisterNo, new List<SupplierTenderQuantityTable> { new SupplierTenderQuantityTable() { Id = 1, QuantitiyItemsJson = new SupplierTenderQuantityTableItemJson(new List<SupplierTenderQuantityTableItem>() { new SupplierTenderQuantityTableItem() { Id = 1 } }) } }, offerPresentationWayid);
            offer.UpdateStatus(SharedKernel.Enums.OfferStatus.Excluded);
            offer.ResetOfferToAwarding();
            Assert.Null(offer.PartialOfferAwardingValue);
        }

        [Fact]
        public void Should_ResetOfferToCheck()
        {
            Offer offer = new Offer(tenderId, commericalRegisterNo, new List<SupplierTenderQuantityTable> { new SupplierTenderQuantityTable() { Id = 1, QuantitiyItemsJson = new SupplierTenderQuantityTableItemJson(new List<SupplierTenderQuantityTableItem>() { new SupplierTenderQuantityTableItem() { Id = 1 } }) } }, offerPresentationWayid);
            offer.UpdateStatus(SharedKernel.Enums.OfferStatus.Excluded);
            offer.ResetOfferToCheck();
            Assert.Null(offer.PartialOfferAwardingValue);
        }

        [Fact]
        public void Should_UpadatePriceAfterNegotiation()
        {
            Offer offer = new Offer(tenderId, commericalRegisterNo, new List<SupplierTenderQuantityTable> { new SupplierTenderQuantityTable() { Id = 1, QuantitiyItemsJson = new SupplierTenderQuantityTableItemJson(new List<SupplierTenderQuantityTableItem>() { new SupplierTenderQuantityTableItem() { Id = 1 } }) } }, offerPresentationWayid);
            offer.UpadatePriceAfterNegotiation(price);
            Assert.Equal(price, offer.FinalPriceAfterDiscount);
        }

        [Fact]
        public void Should_UpadatePriceNPAfterNegotiation()
        {
            Offer offer = new Offer(tenderId, commericalRegisterNo, new List<SupplierTenderQuantityTable> { new SupplierTenderQuantityTable() { Id = 1, QuantitiyItemsJson = new SupplierTenderQuantityTableItemJson(new List<SupplierTenderQuantityTableItem>() { new SupplierTenderQuantityTableItem() { Id = 1 } }) } }, offerPresentationWayid);
            offer.UpadatePriceNPAfterNegotiation(priceNP);
            Assert.Equal(priceNP, offer.OfferWeightAfterCalcNPA);
        }

        [Fact]
        public void Should_DeleteAttachment()
        {
            Offer offer = new Offer(tenderId, commericalRegisterNo, new List<SupplierTenderQuantityTable> { new SupplierTenderQuantityTable() { Id = 1, QuantitiyItemsJson = new SupplierTenderQuantityTableItemJson(new List<SupplierTenderQuantityTableItem>() { new SupplierTenderQuantityTableItem() { Id = 1 } }) } }, offerPresentationWayid);
            offer.AddAttachment(new List<SupplierAttachment>() { new SupplierTechnicalProposalAttachment() { } });
            offer.DeleteAttachment();
            Assert.Equal(ObjectState.Deleted, offer.Attachment[0].State);
        }

        [Fact]
        public void Should_AddAttachment()
        {
            Offer offer = new Offer(tenderId, commericalRegisterNo, new List<SupplierTenderQuantityTable> { new SupplierTenderQuantityTable() { Id = 1, QuantitiyItemsJson = new SupplierTenderQuantityTableItemJson(new List<SupplierTenderQuantityTableItem>() { new SupplierTenderQuantityTableItem() { Id = 1 } }) } }, offerPresentationWayid);
            offer.AddAttachment(new List<SupplierAttachment>() { new SupplierTechnicalProposalAttachment() { } });
            Assert.NotEmpty(offer.Attachment);
        }

        [Fact]
        public void Should_AddAttachment_Second()
        {
            Offer offer = new Offer(tenderId, commericalRegisterNo, new List<SupplierTenderQuantityTable> { new SupplierTenderQuantityTable() { Id = 1, QuantitiyItemsJson = new SupplierTenderQuantityTableItemJson(new List<SupplierTenderQuantityTableItem>() { new SupplierTenderQuantityTableItem() { Id = 1 } }) } }, offerPresentationWayid);
            offer.AddAttachment(new SupplierTechnicalProposalAttachment() { });
            Assert.NotEmpty(offer.Attachment);
        }

        [Fact]
        public void Should_AddBankGuarantee()
        {
            Offer offer = new Offer(tenderId, commericalRegisterNo, new List<SupplierTenderQuantityTable> { new SupplierTenderQuantityTable() { Id = 1, QuantitiyItemsJson = new SupplierTenderQuantityTableItemJson(new List<SupplierTenderQuantityTableItem>() { new SupplierTenderQuantityTableItem() { Id = 1 } }) } }, offerPresentationWayid);
            offer.AddBankGuarantee(new List<SupplierBankGuaranteeDetail>() { new SupplierBankGuaranteeDetail() { } });
            Assert.NotEmpty(offer.BankGuaranteeDetails);
        }

        //[Fact]
        //public void Should_RemoveAttachment()
        //{
        //    Offer offer = new Offer(tenderId, commericalRegisterNo, new List<SupplierTenderQuantityTable> { new SupplierTenderQuantityTable() { Id = 1, QuantitiyItemsJson = new SupplierTenderQuantityTableItemJson(new List<SupplierTenderQuantityTableItem>() { new SupplierTenderQuantityTableItem() { Id = 1 } }) } }, offerPresentationWayid);
        //    offer.AddAttachment(new SupplierTechnicalProposalAttachment() { AttachmentId = 1 });
        //    offer.RemoveAttachment();
        //    Assert.NotEmpty(offer.Attachment);
        //}

        [Fact]
        public void Should_AddActionToOfferHistory()
        {
            Offer offer = new Offer(tenderId, commericalRegisterNo, new List<SupplierTenderQuantityTable> { new SupplierTenderQuantityTable() { Id = 1, QuantitiyItemsJson = new SupplierTenderQuantityTableItemJson(new List<SupplierTenderQuantityTableItem>() { new SupplierTenderQuantityTableItem() { Id = 1 } }) } }, offerPresentationWayid);
            offer.AddActionToOfferHistory(1, 1, SharedKernel.Enums.TenderActions.AcceptInvitation, 1, commericalRegisterNo);
            Assert.NotEmpty(offer.OffersHistory);
        }

        [Fact]
        public void Should_UpdateStatus()
        {
            Offer offer = new Offer(tenderId, commericalRegisterNo, new List<SupplierTenderQuantityTable> { new SupplierTenderQuantityTable() { Id = 1, QuantitiyItemsJson = new SupplierTenderQuantityTableItemJson(new List<SupplierTenderQuantityTableItem>() { new SupplierTenderQuantityTableItem() { Id = 1 } }) } }, offerPresentationWayid);
            offer.UpdateStatus(SharedKernel.Enums.OfferStatus.Accepted);
            Assert.Equal((int)SharedKernel.Enums.OfferStatus.Accepted, offer.OfferStatusId);
        }

        [Fact]
        public void Should_UpdateStatus_Second()
        {
            Offer offer = new Offer(tenderId, commericalRegisterNo, new List<SupplierTenderQuantityTable> { new SupplierTenderQuantityTable() { Id = 1, QuantitiyItemsJson = new SupplierTenderQuantityTableItemJson(new List<SupplierTenderQuantityTableItem>() { new SupplierTenderQuantityTableItem() { Id = 1 } }) } }, offerPresentationWayid);
            offer.UpdateStatus(SharedKernel.Enums.OfferStatus.Canceled);
            Assert.Equal((int)SharedKernel.Enums.OfferStatus.Canceled, offer.OfferStatusId);
        }

        [Fact]
        public void Should_UpdateFinalPriceAfterDiscount()
        {
            Offer offer = new Offer(tenderId, commericalRegisterNo, new List<SupplierTenderQuantityTable> { new SupplierTenderQuantityTable() { Id = 1, QuantitiyItemsJson = new SupplierTenderQuantityTableItemJson(new List<SupplierTenderQuantityTableItem>() { new SupplierTenderQuantityTableItem() { Id = 1 } }) } }, offerPresentationWayid);
            offer.UpdateFinalPriceAfterDiscount(price);
            Assert.Equal(price, offer.FinalPriceAfterDiscount);
        }

        [Fact]
        public void Should_UpdateOfferWeightAfterCalcNPA()
        {
            Offer offer = new Offer(tenderId, commericalRegisterNo, new List<SupplierTenderQuantityTable> { new SupplierTenderQuantityTable() { Id = 1, QuantitiyItemsJson = new SupplierTenderQuantityTableItemJson(new List<SupplierTenderQuantityTableItem>() { new SupplierTenderQuantityTableItem() { Id = 1 } }) } }, offerPresentationWayid);
            offer.UpdateOfferWeightAfterCalcNPA(price);
            Assert.Equal(price, offer.OfferWeightAfterCalcNPA);
        }

        [Fact]
        public void Should_UpdateFinalPriceBeforeDiscount()
        {
            Offer offer = new Offer(tenderId, commericalRegisterNo, new List<SupplierTenderQuantityTable> { new SupplierTenderQuantityTable() { Id = 1, QuantitiyItemsJson = new SupplierTenderQuantityTableItemJson(new List<SupplierTenderQuantityTableItem>() { new SupplierTenderQuantityTableItem() { Id = 1 } }) } }, offerPresentationWayid);
            offer.UpdateFinalPriceBeforeDiscount(price);
            Assert.Equal(price, offer.FinalPriceBeforeDiscount);
        }

        [Fact]
        public void Should_SetFinalPrice()
        {
            Offer offer = new Offer(tenderId, commericalRegisterNo, new List<SupplierTenderQuantityTable> { new SupplierTenderQuantityTable() { Id = 1, QuantitiyItemsJson = new SupplierTenderQuantityTableItemJson(new List<SupplierTenderQuantityTableItem>() { new SupplierTenderQuantityTableItem() { Id = 1 } }) } }, offerPresentationWayid);
            offer.SetFinalPrice(price);
            Assert.Equal(price, offer.FinalPriceAfterDiscount);
        }

        [Fact]
        public void Should_UpdateFinalPriceForNegotiation()
        {
            Offer offer = new Offer(tenderId, commericalRegisterNo, new List<SupplierTenderQuantityTable> { new SupplierTenderQuantityTable() { Id = 1, QuantitiyItemsJson = new SupplierTenderQuantityTableItemJson(new List<SupplierTenderQuantityTableItem>() { new SupplierTenderQuantityTableItem() { Id = 1 } }) } }, offerPresentationWayid);
            offer.UpdateFinalPriceForNegotiation(discountAmount);
            Assert.Equal((offer.OfferWeightAfterCalcNPA - (discountAmount / 100 * offer.OfferWeightAfterCalcNPA)), offer.OfferWeightAfterCalcNPA);
        }

        [Fact]
        public void Should_UpdateFinalPriceForNegotiationFirstStage()
        {
            Offer offer = new Offer(tenderId, commericalRegisterNo, new List<SupplierTenderQuantityTable> { new SupplierTenderQuantityTable() { Id = 1, QuantitiyItemsJson = new SupplierTenderQuantityTableItemJson(new List<SupplierTenderQuantityTableItem>() { new SupplierTenderQuantityTableItem() { Id = 1 } }) } }, offerPresentationWayid);
            offer.UpdateFinalPriceForNegotiationFirstStage(discountAmount);
            Assert.Equal(discountAmount, offer.OfferWeightAfterCalcNPA);
        }

        [Fact]
        public void Should_DeActive()
        {
            Offer offer = new Offer(tenderId, commericalRegisterNo, new List<SupplierTenderQuantityTable> { new SupplierTenderQuantityTable() { Id = 1, QuantitiyItemsJson = new SupplierTenderQuantityTableItemJson(new List<SupplierTenderQuantityTableItem>() { new SupplierTenderQuantityTableItem() { Id = 1 } }) } }, offerPresentationWayid);
            offer.DeActive();
            Assert.False(offer.IsActive);
        }

        [Fact]
        public void Should_Delete()
        {
            Offer offer = new Offer(tenderId, commericalRegisterNo, new List<SupplierTenderQuantityTable> { new SupplierTenderQuantityTable() { Id = 1, QuantitiyItemsJson = new SupplierTenderQuantityTableItemJson(new List<SupplierTenderQuantityTableItem>() { new SupplierTenderQuantityTableItem() { Id = 1 } }) } }, offerPresentationWayid);
            offer.Delete();
            Assert.NotNull(offer);
        }

        [Fact]
        public void Should_UpdateOfferTechnicalWeight()
        {
            Offer offer = new Offer(tenderId, commericalRegisterNo, new List<SupplierTenderQuantityTable> { new SupplierTenderQuantityTable() { Id = 1, QuantitiyItemsJson = new SupplierTenderQuantityTableItemJson(new List<SupplierTenderQuantityTableItem>() { new SupplierTenderQuantityTableItem() { Id = 1 } }) } }, offerPresentationWayid);
            offer.UpdateOfferTechnicalWeight(technicalEvaluationLevel);
            Assert.Equal(technicalEvaluationLevel, offer.TechnicalEvaluationLevel);
        }

        [Fact]
        public void Should_UpdateOfferFinancialWeight()
        {
            Offer offer = new Offer(tenderId, commericalRegisterNo, new List<SupplierTenderQuantityTable> { new SupplierTenderQuantityTable() { Id = 1, QuantitiyItemsJson = new SupplierTenderQuantityTableItemJson(new List<SupplierTenderQuantityTableItem>() { new SupplierTenderQuantityTableItem() { Id = 1 } }) } }, offerPresentationWayid);
            offer.UpdateOfferFinancialWeight(financialWeight);
            Assert.Equal(financialWeight, offer.FinancialEvaluationLevel);
        }

        [Fact]
        public void Should_UpdateVROOfferFinancialWeight()
        {
            Offer offer = new Offer(tenderId, commericalRegisterNo, new List<SupplierTenderQuantityTable> { new SupplierTenderQuantityTable() { Id = 1, QuantitiyItemsJson = new SupplierTenderQuantityTableItemJson(new List<SupplierTenderQuantityTableItem>() { new SupplierTenderQuantityTableItem() { Id = 1 } }) } }, offerPresentationWayid);
            offer.UpdateVROOfferFinancialWeight(financialWeight);
            Assert.Equal(financialWeight, offer.FinancialEvaluationLevel);
        }

        [Fact]
        public void Should_UpdateOfferCheckingStatus()
        {
            Offer offer = new Offer(tenderId, commericalRegisterNo, new List<SupplierTenderQuantityTable> { new SupplierTenderQuantityTable() { Id = 1, QuantitiyItemsJson = new SupplierTenderQuantityTableItemJson(new List<SupplierTenderQuantityTableItem>() { new SupplierTenderQuantityTableItem() { Id = 1 } }) } }, offerPresentationWayid);
            offer.UpdateOfferCheckingStatus(acceptanceStatusId, technicalEvaluationStatusId, note, rejectionReason);
            Assert.Equal(acceptanceStatusId, offer.OfferAcceptanceStatusId);
        }

        [Fact]
        public void Should_UpdateOfferTecknicalCheckingStatus()
        {
            Offer offer = new Offer(tenderId, commericalRegisterNo, new List<SupplierTenderQuantityTable> { new SupplierTenderQuantityTable() { Id = 1, QuantitiyItemsJson = new SupplierTenderQuantityTableItemJson(new List<SupplierTenderQuantityTableItem>() { new SupplierTenderQuantityTableItem() { Id = 1 } }) } }, offerPresentationWayid);
            offer.UpdateOfferTecknicalCheckingStatus(technicalEvaluationStatusId, rejectionReason, note);
            Assert.Equal(technicalEvaluationStatusId, offer.OfferTechnicalEvaluationStatusId);
        }

        [Fact]
        public void Should_UpdateSolidarity()
        {
            Offer offer = new Offer(tenderId, commericalRegisterNo, new List<SupplierTenderQuantityTable> { new SupplierTenderQuantityTable() { Id = 1, QuantitiyItemsJson = new SupplierTenderQuantityTableItemJson(new List<SupplierTenderQuantityTableItem>() { new SupplierTenderQuantityTableItem() { Id = 1 } }) } }, offerPresentationWayid);
            offer.UpdateSolidarity(true);
            Assert.True(offer.IsSolidarity);
        }

        [Fact]
        public void Should_sendSolidarityInvitations()
        {
            Offer offer = new Offer(tenderId, commericalRegisterNo, new List<SupplierTenderQuantityTable> { new SupplierTenderQuantityTable() { Id = 1, QuantitiyItemsJson = new SupplierTenderQuantityTableItemJson(new List<SupplierTenderQuantityTableItem>() { new SupplierTenderQuantityTableItem() { Id = 1 } }) } }, offerPresentationWayid);
            OfferSolidarity solidarity = new OfferSolidarity(commericalRegisterNo, SharedKernel.Enums.SupplierSolidarityStatus.Approved);
            offer.AddRegisteredSolidaritySupplier(new List<OfferSolidarity>() { solidarity });
            offer.sendSolidarityInvitations(SharedKernel.Enums.SupplierSolidarityStatus.Approved);
            Assert.NotEmpty(offer.Combined);
        }

        [Fact]
        public void Should_UpdateOfferFinantialCheckingStatus()
        {
            Offer offer = new Offer(tenderId, commericalRegisterNo, new List<SupplierTenderQuantityTable> { new SupplierTenderQuantityTable() { Id = 1, QuantitiyItemsJson = new SupplierTenderQuantityTableItemJson(new List<SupplierTenderQuantityTableItem>() { new SupplierTenderQuantityTableItem() { Id = 1 } }) } }, offerPresentationWayid);
            offer.OfferlocalContentDetails = new OfferlocalContentDetails();
            offer.UpdateOfferFinantialCheckingStatus(acceptanceStatusId, rejectionReason, financialnotes);
            offer.OfferlocalContentDetails.SetIsBindedToMandatoryList(isBindedToMandatoryList);
            Assert.Equal(acceptanceStatusId, offer.OfferAcceptanceStatusId);
        }

        [Fact]
        public void Should_UpdateOfferFinantialCheckingDetails()
        {
            Offer offer = new Offer(tenderId, commericalRegisterNo, new List<SupplierTenderQuantityTable> { new SupplierTenderQuantityTable() { Id = 1, QuantitiyItemsJson = new SupplierTenderQuantityTableItemJson(new List<SupplierTenderQuantityTableItem>() { new SupplierTenderQuantityTableItem() { Id = 1 } }) } }, offerPresentationWayid);
            offer.UpdateOfferFinantialCheckingDetails(isFinaintialOfferLetterAttached, finantialOfferLetterNumber, DateTime.Now.Date, iIsFinantialOfferLetterCopyAttached, isBankGuaranteeAttached);
            Assert.Equal(finantialOfferLetterNumber, offer.FinantialOfferLetterNumber);
        }

        [Fact]
        public void Should_UpdateBankGurnteesDetails()
        {
            Offer offer = new Offer(tenderId, commericalRegisterNo, new List<SupplierTenderQuantityTable> { new SupplierTenderQuantityTable() { Id = 1, QuantitiyItemsJson = new SupplierTenderQuantityTableItemJson(new List<SupplierTenderQuantityTableItem>() { new SupplierTenderQuantityTableItem() { Id = 1 } }) } }, offerPresentationWayid);
            offer.UpdateBankGurnteesDetails(new List<SupplierBankGuaranteeDetail>() { new SupplierBankGuaranteeDetail() { } });
            offer.UpdateBankGurnteesDetails(new List<SupplierBankGuaranteeDetail>() { new SupplierBankGuaranteeDetail() { } });
            Assert.NotEmpty(offer.BankGuaranteeDetails);
        }

        [Fact]
        public void Should_updateBankGurnteeList()
        {
            Offer offer = new Offer(tenderId, commericalRegisterNo, new List<SupplierTenderQuantityTable> { new SupplierTenderQuantityTable() { Id = 1, QuantitiyItemsJson = new SupplierTenderQuantityTableItemJson(new List<SupplierTenderQuantityTableItem>() { new SupplierTenderQuantityTableItem() { Id = 1 } }) } }, offerPresentationWayid);
            offer.updateBankGurnteeList(new List<SupplierBankGuaranteeDetail>() { new SupplierBankGuaranteeDetail() { } });
            Assert.NotEmpty(offer.BankGuaranteeDetails);
        }

        [Fact]
        public void Should_SaveOfferAwardingValues()
        {
            Offer offer = new Offer(tenderId, commericalRegisterNo, new List<SupplierTenderQuantityTable> { new SupplierTenderQuantityTable() { Id = 1, QuantitiyItemsJson = new SupplierTenderQuantityTableItemJson(new List<SupplierTenderQuantityTableItem>() { new SupplierTenderQuantityTableItem() { Id = 1 } }) } }, offerPresentationWayid);
            offer.SaveOfferAwardingValues(offerId, totalOfferAwardingValue, partialOfferAwardingValue, justificationOfRecommendation);
            Assert.Equal(totalOfferAwardingValue, offer.TotalOfferAwardingValue);
        }

        [Fact]
        public void Should_RemoveOfferAwardingValue()
        {
            Offer offer = new Offer(tenderId, commericalRegisterNo, new List<SupplierTenderQuantityTable> { new SupplierTenderQuantityTable() { Id = 1, QuantitiyItemsJson = new SupplierTenderQuantityTableItemJson(new List<SupplierTenderQuantityTableItem>() { new SupplierTenderQuantityTableItem() { Id = 1 } }) } }, offerPresentationWayid);
            offer.RemoveOfferAwardingValue(offerId);
            Assert.Null(offer.TotalOfferAwardingValue);
        }

        [Fact]
        public void Should_DeleteOfferAwardingValues()
        {
            Offer offer = new Offer(tenderId, commericalRegisterNo, new List<SupplierTenderQuantityTable> { new SupplierTenderQuantityTable() { Id = 1, QuantitiyItemsJson = new SupplierTenderQuantityTableItemJson(new List<SupplierTenderQuantityTableItem>() { new SupplierTenderQuantityTableItem() { Id = 1 } }) } }, offerPresentationWayid);
            offer.DeleteOfferAwardingValues(offerId);
            Assert.Null(offer.TotalOfferAwardingValue);
        }

        [Fact]
        public void Should_RemoveRegisteredSupplierCombined()
        {
            Offer offer = new Offer(tenderId, commericalRegisterNo, new List<SupplierTenderQuantityTable> { new SupplierTenderQuantityTable() { Id = 1, QuantitiyItemsJson = new SupplierTenderQuantityTableItemJson(new List<SupplierTenderQuantityTableItem>() { new SupplierTenderQuantityTableItem() { Id = 1 } }) } }, offerPresentationWayid);
            OfferSolidarity solidarity = new OfferSolidarity(commericalRegisterNo, SharedKernel.Enums.SupplierSolidarityStatus.Approved);
            offer.AddRegisteredSolidaritySupplier(new List<OfferSolidarity>() { solidarity });
            offer.RemoveRegisteredSupplierCombined(new OfferSolidarity());
            Assert.Equal(commericalRegisterNo, offer.CommericalRegisterNo);
        }

        [Fact]
        public void Should_CreateOfferQuantityTables()
        {
            Offer offer = new Offer(tenderId, commericalRegisterNo, new List<SupplierTenderQuantityTable> { new SupplierTenderQuantityTable() { Id = 1, QuantitiyItemsJson = new SupplierTenderQuantityTableItemJson(new List<SupplierTenderQuantityTableItem>() { new SupplierTenderQuantityTableItem() { Id = 1 } }) } }, offerPresentationWayid);
            offer.CreateOfferQuantityTables(tableId);
            Assert.NotEmpty(offer.SupplierTenderQuantityTables);
        }

        [Fact]
        public void Should_CreateVROOfferQuantityTables()
        {
            Offer offer = new Offer(tenderId, commericalRegisterNo, new List<SupplierTenderQuantityTable> { new SupplierTenderQuantityTable() { Id = 1, QuantitiyItemsJson = new SupplierTenderQuantityTableItemJson(new List<SupplierTenderQuantityTableItem>() { new SupplierTenderQuantityTableItem() { Id = 1 } }) } }, offerPresentationWayid);
            offer.CreateVROOfferQuantityTables(tableId);
            Assert.NotEmpty(offer.SupplierTenderQuantityTables);
        }

        [Fact]
        public void Should_SaveOfferQuantityTables()
        {
            Offer offer = new Offer(tenderId, commericalRegisterNo, new List<SupplierTenderQuantityTable> { new SupplierTenderQuantityTable() { Id = 1, QuantitiyItemsJson = new SupplierTenderQuantityTableItemJson(new List<SupplierTenderQuantityTableItem>() { new SupplierTenderQuantityTableItem() { Id = 1 } }) } }, offerPresentationWayid);
            offer.SaveOfferQuantityTables(tableId, new List<ViewModel.TenderQuantityItemDTO>() { new ViewModel.TenderQuantityItemDTO() { Id = 1 } });
            Assert.NotEmpty(offer.SupplierTenderQuantityTables);
        }

        [Fact]
        public void Should_SaveSupplierTenderQuantityTables()
        {
            Offer offer = new Offer(tenderId, commericalRegisterNo, new List<SupplierTenderQuantityTable> { new SupplierTenderQuantityTable() { Id = 1, QuantitiyItemsJson = new SupplierTenderQuantityTableItemJson(new List<SupplierTenderQuantityTableItem>() { new SupplierTenderQuantityTableItem() { Id = 1 } }) } }, offerPresentationWayid);
            offer.SaveSupplierTenderQuantityTables(new List<ViewModel.TenderQuantityItemDTO>() { new ViewModel.TenderQuantityItemDTO() { Id = 1, TableId = 1 } }, "table name", 1, out itemId);
            Assert.NotEmpty(offer.SupplierTenderQuantityTables);
        }

        [Fact]
        public void Should_DeleteQuantityTableItems()
        {
            var supplierTenderQuantityTable = new SupplierTenderQuantityTable(offerId, "name");
            supplierTenderQuantityTable.Id = 1;
            supplierTenderQuantityTable.QuantitiyItemsJson = new SupplierTenderQuantityTableItemJson(new List<SupplierTenderQuantityTableItem>() { new SupplierTenderQuantityTableItem() { Id = 1 } });
            Offer offer = new Offer(tenderId, commericalRegisterNo, new List<SupplierTenderQuantityTable> { supplierTenderQuantityTable }, offerPresentationWayid);
            var result = offer.DeleteQuantityTableItems(tableId, itemNumber);
            Assert.NotNull(result);
        }

        [Fact]
        public void Should_ResetOfferLocalContentPreference()
        {
            Offer offer = new Offer(tenderId, commericalRegisterNo, new List<SupplierTenderQuantityTable> { new SupplierTenderQuantityTable() { Id = 1, QuantitiyItemsJson = new SupplierTenderQuantityTableItemJson(new List<SupplierTenderQuantityTableItem>() { new SupplierTenderQuantityTableItem() { Id = 1 } }) } }, offerPresentationWayid);
            offer.OfferlocalContentDetails = new OfferlocalContentDetails();
            offer.OfferlocalContentDetails.SetOfferFinancialWeight(20);
            offer.ResetOfferLocalContentPreference();
            Assert.Null(offer.OfferlocalContentDetails.OfferPriceAfterLocalContent);
        }
        [Fact]
        public void Should_SetOfferLocalContentDetails()
        {
            Offer offer = new Offer();
            offer.SetOfferLocalContentDetails();
            Assert.NotNull(offer.OfferlocalContentDetails);
        }

    }
}
