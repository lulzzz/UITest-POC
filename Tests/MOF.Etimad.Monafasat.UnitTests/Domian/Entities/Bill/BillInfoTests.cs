using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.TestsBuilders.BillDefaults;
using Shouldly;
using System;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Bill
{
    public class BillInfoTests
    {

        private readonly BillDefaults billDefaults = new BillDefaults();

        [Fact]
        public void ConstructBillInfo()
        {
            BillInfo bill = billDefaults.returnBillInfoDefaults();

            bill.ShouldNotBeNull();
            bill.AmountDue.ShouldBe(billDefaults._amountDue);
            bill.DueDate.ShouldBe(billDefaults._dueDt);
            bill.ExpiryDate.ShouldBe(billDefaults._expiryDate);
            bill.AgencyCode.ShouldBe(billDefaults._agencyCode);
            bill.CreatedAt.Date.ShouldBe(DateTime.Now.Date);
            bill.IsActive.ShouldBe(true);
            bill.State.ShouldBe(ObjectState.Added);

            bill.BillReferenceInfo.ShouldBeNull();
            bill.BenChapterNumber.ShouldBeNull();
            bill.BenSectionNumber.ShouldBeNull();
            bill.BenSequenceNumber.ShouldBeNull();
            bill.BenSubDepartmentsCount.ShouldBeNull();
            bill.BenSubSectionsCount.ShouldBeNull();
            bill.SadadBatchId.ShouldBeNull();
            bill.DisplayLabelAr.ShouldBeNull();
            bill.DisplayLabelEn.ShouldBeNull();
            bill.ActionStatus.ShouldBeNull();
            bill.ActionReason.ShouldBeNull();
            bill.POINumber.ShouldBeNull();
            bill.POIType.ShouldBeNull();
            bill.BankPaymentId.ShouldBeNull();
            bill.BankId.ShouldBeNull();
            bill.BankBranchId.ShouldBeNull();
            bill.PaymentChannel.ShouldBeNull();
            bill.BankReversalTransactionID.ShouldBeNull();
            bill.IntermediatePaymentId.ShouldBeNull();
            bill.SadadPaymentTranactionId.ShouldBeNull();
            bill.PaymentStatusCode.ShouldBeNull();
            bill.CurrnetAmount.ShouldBeNull();
            bill.PurchaseDate.ShouldBeNull();
            bill.BillingAccount.ShouldBeNull();
            bill.PaymentMethod.ShouldBeNull();
            bill.PaymentReferencefInfo.ShouldBeNull();
            bill.ConditionsBookletID.ShouldBeNull();
            bill.InvitationId.ShouldBeNull();
            bill.BillPaymentDetails.ShouldNotBeNull();
            bill.BillPaymentDetails.Count.ShouldBe(0);
        }

        [Fact]
        public void UpdateBillStatusTest()
        {
            BillInfo bill = billDefaults.returnBillInfoDefaults();

            bill.UpdateBillStatus(Enums.BillStatus.Rejected);

            bill.ShouldNotBeNull();
            bill.AmountDue.ShouldBe(billDefaults._amountDue);
            bill.DueDate.ShouldBe(billDefaults._dueDt);
            bill.ExpiryDate.ShouldBe(billDefaults._expiryDate);
            bill.AgencyCode.ShouldBe(billDefaults._agencyCode);
            bill.CreatedAt.Date.ShouldBe(DateTime.Now.Date);
            bill.IsActive.ShouldBe(true);
            bill.State.ShouldBe(ObjectState.Added);
            bill.BillStatusId.ShouldBe((int)Enums.BillStatus.Rejected);
        }

        [Fact]
        public void UpdateActionStatusTest()
        {
            BillInfo bill = billDefaults.returnBillInfoDefaults();

            bill.UpdateActionStatus(Enums.BillActionStatus.SuccessUpdateBill);

            bill.ShouldNotBeNull();
            bill.AmountDue.ShouldBe(billDefaults._amountDue);
            bill.DueDate.ShouldBe(billDefaults._dueDt);
            bill.ExpiryDate.ShouldBe(billDefaults._expiryDate);
            bill.AgencyCode.ShouldBe(billDefaults._agencyCode);
            bill.CreatedAt.Date.ShouldBe(DateTime.Now.Date);
            bill.IsActive.ShouldBe(true);
            bill.State.ShouldBe(ObjectState.Added);
            bill.ActionStatus.ShouldBe((int)Enums.BillActionStatus.SuccessUpdateBill);
        }

        [Fact]
        public void UpdateActionStatusAndExpiryDateTest()
        {
            BillInfo bill = billDefaults.returnBillInfoDefaults();

            var expiryDate = DateTime.Today;

            bill.UpdateActionStatusAndExpiryDate(Enums.BillActionStatus.SuccessUpdateBill, expiryDate.Date);

            bill.ShouldNotBeNull();
            bill.AmountDue.ShouldBe(billDefaults._amountDue);
            bill.DueDate.ShouldBe(billDefaults._dueDt);
            bill.AgencyCode.ShouldBe(billDefaults._agencyCode);
            bill.CreatedAt.Date.ShouldBe(DateTime.Now.Date);
            bill.IsActive.ShouldBe(true);
            bill.State.ShouldBe(ObjectState.Added);
            bill.ActionStatus.ShouldBe((int)Enums.BillActionStatus.SuccessUpdateBill);
            bill.ExpiryDate.Date.ShouldBe(expiryDate.Date);
        }

        [Fact]
        public void UpdateExpiryDateWithoutChangeStateTest()
        {
            BillInfo bill = billDefaults.returnBillInfoDefaults();

            var expiryDate = DateTime.Today;

            bill.UpdateExpiryDateWithoutChangeState(expiryDate.Date);

            bill.ShouldNotBeNull();
            bill.AmountDue.ShouldBe(billDefaults._amountDue);
            bill.DueDate.ShouldBe(billDefaults._dueDt);
            bill.AgencyCode.ShouldBe(billDefaults._agencyCode);
            bill.CreatedAt.Date.ShouldBe(DateTime.Now.Date);
            bill.IsActive.ShouldBe(true);
            bill.State.ShouldBe(ObjectState.Added);
            bill.ActionStatus.ShouldBeNull();
            bill.ExpiryDate.Date.ShouldBe(expiryDate.Date);
        }

        [Fact]
        public void UpdateActionReasonTest()
        {
            BillInfo bill = billDefaults.returnBillInfoDefaults();

            bill.UpdateActionReason("reason 1");

            Assert.Equal(billDefaults._amountDue, bill.AmountDue);
            Assert.Equal(billDefaults._dueDt, bill.DueDate);
            Assert.Equal(billDefaults._expiryDate, bill.ExpiryDate);
            Assert.Equal(billDefaults._agencyCode, bill.AgencyCode);
            Assert.Equal(DateTime.Now.Date, bill.CreatedAt.Date);
            Assert.True(bill.IsActive);
            Assert.Equal("reason 1", bill.ActionReason);
        }

        [Fact]
        public void UpdateBillInfoTest()
        {
            BillInfo bill = billDefaults.returnBillInfoDefaults();

            var billRefInfo = "123";
            var benChapterNo = "chap 123";
            var benBranchNo = "branch 123";
            var benSectionNo = "sec 123";
            var benSequenceNo = "sec 123";
            var benSubDepartmentsCount = "123";
            var benSubSectionsCount = "section 123";
            var agencyCode = "456";
            var displayLabelAr = "disp Ar";
            var displayLabelEn = "disp En";
            var sadadBatchId = "sadad 123";


            bill.UpdateBillInfo(billRefInfo, benChapterNo, benBranchNo, benSectionNo, benSequenceNo,
                benSubDepartmentsCount, benSubSectionsCount, agencyCode, displayLabelAr, displayLabelEn, sadadBatchId);

            bill.ShouldNotBeNull();
            bill.BillInvoiceNumber.ShouldBe(billRefInfo);
            bill.BillReferenceInfo.ShouldBe(billRefInfo);
            bill.BenChapterNumber.ShouldBe(benChapterNo);
            bill.BankBranchId.ShouldBe(benBranchNo);
            bill.BenSectionNumber.ShouldBe(benSectionNo);
            bill.BenSequenceNumber.ShouldBe(benSequenceNo);
            bill.BenSubDepartmentsCount.ShouldBe(benSubDepartmentsCount);
            bill.BenSubSectionsCount.ShouldBe(benSubSectionsCount);
            bill.AgencyCode.ShouldBe(agencyCode);
            bill.DisplayLabelAr.ShouldBe(displayLabelAr);
            bill.DisplayLabelEn.ShouldBeNull();
            bill.SadadBatchId.ShouldBe(sadadBatchId);
            bill.BillStatusId.ShouldBe((int)Enums.BillStatus.SuccessUploaded);
        }

        [Fact]
        public void UpdatePaymentDetailsTest()
        {
            BillInfo bill = billDefaults.returnBillInfoDefaults();

            var intermediatePaymentId = "123";
            var sadadPaymentTranactionId = "pay 123";
            var bankReversalTransactionID = "rev 123";
            var paymentStatusCode = "code 123";
            var billingAccount = "account 123";
            var currnetAmount = (decimal)123.2;
            var purchaseDate = DateTime.Now.Date;
            var BankId = "456";
            var accessChannel = "chan";
            var paymentMethod = "Visa";
            var paymentReferencefInfo = "PRef 123";
            var bankPaymentID = "bank 123";
            var poiNumber = "256";
            var poiType = "254";


            bill.UpdatePaymentDetails(intermediatePaymentId, sadadPaymentTranactionId, bankReversalTransactionID, paymentStatusCode, billingAccount,
                currnetAmount, purchaseDate, BankId, accessChannel, paymentMethod, paymentReferencefInfo, bankPaymentID, poiNumber, poiType);

            bill.ShouldNotBeNull();
            bill.IntermediatePaymentId.ShouldBe(intermediatePaymentId);
            bill.SadadPaymentTranactionId.ShouldBe(sadadPaymentTranactionId);
            bill.BankReversalTransactionID.ShouldBe(bankReversalTransactionID);
            bill.PaymentStatusCode.ShouldBe(paymentStatusCode);
            bill.BillingAccount.ShouldBe(billingAccount);
            bill.CurrnetAmount.ShouldBe(currnetAmount);
            bill.PurchaseDate.ShouldBe(purchaseDate);
            bill.BankId.ShouldBe(BankId);
            bill.PaymentChannel.ShouldBe(accessChannel);
            bill.PaymentMethod.ShouldBe(paymentMethod);
            bill.PaymentReferencefInfo.ShouldBe(paymentReferencefInfo);
            bill.BankPaymentId.ShouldBe(bankPaymentID);
            bill.POINumber.ShouldBe(poiNumber);
            bill.POIType.ShouldBe(poiType);
            bill.BillStatusId.ShouldBe((int)Enums.BillStatus.Paid);
        }

        [Fact]
        public void UpdateFreeTenderPurchaseDetailsTest()
        {
            BillInfo bill = billDefaults.returnBillInfoDefaults();


            var purchaseDate = DateTime.Now.Date;


            bill.UpdateFreeTenderPurchaseDetails(123, purchaseDate);

            bill.ShouldNotBeNull();
            bill.CurrnetAmount.ShouldBe(123);
            bill.PurchaseDate.ShouldBe(purchaseDate);
            bill.BillStatusId.ShouldBe((int)Enums.BillStatus.Paid);
        }

        [Fact]
        public void DeleteBooklet_Test()
        {
            BillInfo bill = billDefaults.returnBillInfoDefaults();

            bill.Test_AddBooklet(new ConditionsBooklet());
            bill.DeleteBooklet();

            bill.ShouldNotBeNull();
            bill.ConditionsBooklet.State.ShouldBe(ObjectState.Deleted);
            bill.State.ShouldBe(ObjectState.Deleted);
        }

        [Fact]
        public void DeleteInvitation_Test()
        {
            BillInfo bill = billDefaults.returnBillInfoDefaults();

            bill.Test_AddInvitation(new Invitation());
            bill.DeleteInvitation();

            bill.ShouldNotBeNull();
            bill.Invitation.StatusId.ShouldBe((int)Enums.InvitationStatus.New);
            bill.IsActive.ShouldBe(false);
        }

        [Fact]
        public void DeleteBillInWithdrawAndRejectFromInvitaion_Test()
        {
            BillInfo bill = billDefaults.returnBillInfoDefaults();

            bill.DeleteBillInWithdrawAndRejectFromInvitaion();

            bill.ShouldNotBeNull();
            bill.State.ShouldBe(ObjectState.Deleted);
        }

        [Fact]
        public void UpdateBillStatusForRollOutTeam_Test()
        {
            BillInfo bill = billDefaults.returnBillInfoDefaults();

            bill.UpdateBillStatusForRollOutTeam();

            bill.ShouldNotBeNull();
            bill.BillStatusId.ShouldBe((int)Enums.BillStatus.Paid);
        }
    }
}
