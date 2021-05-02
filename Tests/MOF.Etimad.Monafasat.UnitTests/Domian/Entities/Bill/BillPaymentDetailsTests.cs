using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernal;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Bill
{
    public class BillPaymentDetailsTests
    {
        [Fact]
        public void Should_Empty_Construct_BillPaymentDetails()
        {
            var billPaymentDetails = new BillPaymentDetails();

            billPaymentDetails.ShouldNotBeNull();
            billPaymentDetails.Id.ShouldBe(0);
            billPaymentDetails.tenderFeesType.ShouldBeNull();
        }

        [Fact]
        public void Should_Construct_BillPaymentDetails()
        {
            const decimal amount = 1;
            const string billInvoiceNumber = "billInvoiceNumber";
            const string gFSCode = "gFSCode";
            const int feesTypeId = 1;

            var billPaymentDetails = new BillPaymentDetails(amount, billInvoiceNumber, gFSCode, feesTypeId);

            billPaymentDetails.ShouldNotBeNull();
            billPaymentDetails.AmountDue.ShouldBe(amount);
            billPaymentDetails.BillInvoiceNumber.ShouldBe(billInvoiceNumber);
            billPaymentDetails.GFSCode.ShouldBe(gFSCode);
            billPaymentDetails.FeesTypeId.ShouldBe(feesTypeId);

        }

        [Fact]
        public void Should_Delete_BillPaymentDetails()
        {
            var activityId = 1;
            const decimal amount = 1;
            const string billInvoiceNumber = "billInvoiceNumber";
            const string gFSCode = "gFSCode";
            const int feesTypeId = 1;

            var billPaymentDetails = new BillPaymentDetails(amount, billInvoiceNumber, gFSCode, feesTypeId);

            billPaymentDetails.Delete();

            billPaymentDetails.ShouldNotBeNull();
            billPaymentDetails.State.ShouldBe(ObjectState.Deleted);
        }
    }
}