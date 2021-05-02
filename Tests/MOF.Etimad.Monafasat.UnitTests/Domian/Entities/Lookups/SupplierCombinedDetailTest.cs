using MOF.Etimad.Monafasat.Core.Entities;
using Shouldly;
using System;
using System.Collections.Generic;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Lookups
{
    public class SupplierCombinedDetailTest
    {
        private const int combinedDetailId = 1;
        private const int combinedId = 1;
        private const bool isChamberJoiningAttached = true;
        private const bool isChamberJoiningValid = true;
        private const bool isCommercialRegisterAttached = true;
        private const bool isCommercialRegisterValid = true;
        private const bool isSaudizationAttached = true;
        private const bool isSaudizationValidDate = true;
        private const bool isSocialInsuranceAttached = true;
        private const bool isSocialInsuranceValidDate = true;
        private const bool isZakatAttached = true;
        private const bool isZakatValidDate = true;
        private const bool isSpecificationAttached = true;
        private const bool isSpecificationValidDate = true;
        private const bool isOfferCopyAttached = true;
        private const bool isOfferLetterAttached = true;
        private const string offerLetterNumber = "222";




        [Fact]
        public void Should_Construct_SupplierCombinedDetail()
        {
            SupplierCombinedDetail supplierCombinedDetail = new SupplierCombinedDetail();
            supplierCombinedDetail.ShouldNotBeNull();
        }

        [Fact]
        public void Should_UpdateAttachmentDetails()
        {
            SupplierCombinedDetail supplierCombinedDetail = new SupplierCombinedDetail();
            supplierCombinedDetail.UpdateAttachmentDetails(combinedDetailId, combinedId, isChamberJoiningAttached, isChamberJoiningValid, isCommercialRegisterAttached
                , isCommercialRegisterValid, isSaudizationAttached, isSaudizationValidDate, isSocialInsuranceAttached, isSocialInsuranceValidDate, isZakatAttached, isZakatValidDate, isSpecificationAttached, isSpecificationValidDate);

            supplierCombinedDetail.UpdateAttachmentDetails(0, combinedId, isChamberJoiningAttached, isChamberJoiningValid, isCommercialRegisterAttached
                , isCommercialRegisterValid, isSaudizationAttached, isSaudizationValidDate, isSocialInsuranceAttached, isSocialInsuranceValidDate, isZakatAttached, isZakatValidDate, isSpecificationAttached, isSpecificationValidDate);

            supplierCombinedDetail.ShouldNotBeNull();
        }

        [Fact]
        public void Should_UpdateTechnicalAttachmentDetails()
        {
            SupplierCombinedDetail supplierCombinedDetail = new SupplierCombinedDetail();
            supplierCombinedDetail.UpdateTechnicalAttachmentDetails(combinedDetailId, combinedId, isChamberJoiningAttached, isChamberJoiningValid, isCommercialRegisterAttached, isCommercialRegisterValid,
                isOfferCopyAttached, isOfferLetterAttached, offerLetterNumber, DateTime.Now.Date, isSaudizationAttached, isSaudizationValidDate, isSocialInsuranceAttached, isSocialInsuranceValidDate, isSpecificationAttached,
                isZakatAttached, isZakatValidDate, isSpecificationAttached, isSpecificationValidDate);

            supplierCombinedDetail.UpdateTechnicalAttachmentDetails(0, combinedId, isChamberJoiningAttached, isChamberJoiningValid, isCommercialRegisterAttached, isCommercialRegisterValid,
                isOfferCopyAttached, isOfferLetterAttached, offerLetterNumber, DateTime.Now.Date, isSaudizationAttached, isSaudizationValidDate, isSocialInsuranceAttached, isSocialInsuranceValidDate, isSpecificationAttached,
                isZakatAttached, isZakatValidDate, isSpecificationAttached, isSpecificationValidDate);
            supplierCombinedDetail.ShouldNotBeNull();
        }

        [Fact]
        public void Should_updateAttachmentsList()
        {
            SupplierCombinedDetail supplierCombinedDetail = new SupplierCombinedDetail();

            supplierCombinedDetail.updateAttachmentsList(new List<SupplierSpecificationDetail>() { new SupplierSpecificationDetail() });

            supplierCombinedDetail.ShouldNotBeNull();
        }

        [Fact]
        public void Should_AddCalssificationAttachments()
        {
            SupplierCombinedDetail supplierCombinedDetail = new SupplierCombinedDetail();

            supplierCombinedDetail.AddCalssificationAttachments(new List<SupplierSpecificationDetail>() { new SupplierSpecificationDetail() });

            supplierCombinedDetail.ShouldNotBeNull();
        }

        [Fact]
        public void Should_AddSpecification()
        {
            SupplierCombinedDetail supplierCombinedDetail = new SupplierCombinedDetail();

            supplierCombinedDetail.AddSpecification(new List<SupplierSpecificationDetail>() { new SupplierSpecificationDetail() });

            Assert.NotEmpty(supplierCombinedDetail.SpecificationDetails);
        }
    }
}
