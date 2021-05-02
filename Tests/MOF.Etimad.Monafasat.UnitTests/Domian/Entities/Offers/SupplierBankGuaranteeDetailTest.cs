using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernal;
using Shouldly;
using System;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Offers
{
    public class SupplierBankGuaranteeDetailTest
    {
        private const int attachmentId = 1;
        private const int offerId = 1;
        private const bool isBankGuaranteeValid = true;
        private const string guaranteeNumber = "22";
        private const int bankId = 1;
        private const decimal amount = 1;
        private const int guaranteeDays = 1;


        [Fact]
        public void Should_Construct_SupplierBankGuaranteeDetail()
        {
            _ = new SupplierBankGuaranteeDetail();
            SupplierBankGuaranteeDetail supplierBankGuaranteeDetail = new SupplierBankGuaranteeDetail(attachmentId, offerId, isBankGuaranteeValid, guaranteeNumber, bankId, amount,
                DateTime.Now.Date, DateTime.Now.Date, guaranteeDays);
            _ = supplierBankGuaranteeDetail.Bank;
            _ = supplierBankGuaranteeDetail.Offer;

            supplierBankGuaranteeDetail.ShouldNotBeNull();

        }

        [Fact]
        public void Should_Construct_SupplierBankGuaranteeDetail_create()
        {
            SupplierBankGuaranteeDetail supplierBankGuaranteeDetail = new SupplierBankGuaranteeDetail(0, offerId, isBankGuaranteeValid, guaranteeNumber, bankId, amount,
                DateTime.Now.Date, DateTime.Now.Date, guaranteeDays);

            supplierBankGuaranteeDetail.ShouldNotBeNull();
        }

        [Fact]
        public void Should_Delete()
        {
            SupplierBankGuaranteeDetail supplierBankGuaranteeDetail = new SupplierBankGuaranteeDetail(0, offerId, isBankGuaranteeValid, guaranteeNumber, bankId, amount,
                DateTime.Now.Date, DateTime.Now.Date, guaranteeDays);
            supplierBankGuaranteeDetail.Delete();
            Assert.Equal(ObjectState.Deleted, supplierBankGuaranteeDetail.State);
        }

        [Fact]
        public void Should_Deactive()
        {
            SupplierBankGuaranteeDetail supplierBankGuaranteeDetail = new SupplierBankGuaranteeDetail(0, offerId, isBankGuaranteeValid, guaranteeNumber, bankId, amount,
                DateTime.Now.Date, DateTime.Now.Date, guaranteeDays);
            supplierBankGuaranteeDetail.DeActive();
            Assert.False(supplierBankGuaranteeDetail.IsActive);
        }
    }
}
