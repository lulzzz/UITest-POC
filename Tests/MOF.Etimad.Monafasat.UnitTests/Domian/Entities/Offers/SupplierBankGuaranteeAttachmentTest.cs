using MOF.Etimad.Monafasat.Core.Entities;
using Shouldly;
using System;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Offers
{
    public class SupplierBankGuaranteeAttachmentTest
    {
        private const int attachmentId = 1;
        private const int offerId = 1;
        private const bool isBankGuaranteeValid = true;
        private const string guaranteeNumber = "22";
        private const int bankId = 1;
        private const decimal amount = 1;
        private const int guaranteeDays = 1;


        [Fact]
        public void Should_Construct_SupplierBankGuaranteeAttachment()
        {
            _ = new SupplierBankGuaranteeAttachment();
            SupplierBankGuaranteeAttachment supplierBankGuaranteeAttachment = new SupplierBankGuaranteeAttachment(attachmentId, offerId, isBankGuaranteeValid, guaranteeNumber, bankId, amount,
                DateTime.Now.Date, DateTime.Now.Date, guaranteeDays);
            _ = supplierBankGuaranteeAttachment.Bank;
            supplierBankGuaranteeAttachment.ShouldNotBeNull();

        }

        [Fact]
        public void Should_Construct_SupplierBankGuaranteeAttachment_Created()
        {
            _ = new SupplierBankGuaranteeAttachment();
            SupplierBankGuaranteeAttachment supplierBankGuaranteeAttachment = new SupplierBankGuaranteeAttachment(0, offerId, isBankGuaranteeValid, guaranteeNumber, bankId, amount,
                DateTime.Now.Date, DateTime.Now.Date, guaranteeDays);
            _ = supplierBankGuaranteeAttachment.Bank;
            supplierBankGuaranteeAttachment.ShouldNotBeNull();

        }
    }
}
