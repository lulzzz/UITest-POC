using MOF.Etimad.Monafasat.Core.Entities;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Bill
{
    public class BillArchiveTests
    {
        [Fact]
        public void Should_Empty_Construct_BillArchive()
        {
            var billArchive = new BillArchive();

            billArchive.ShouldNotBeNull();
            billArchive.Id.ShouldBe(0);
        }

        [Fact]
        public void Should_Construct_BillArchive()
        {
            int? conditionsBookletId = 1;
            int? invitationId = 1;
            const int tenderId = 1;
            const string tenderReferenceNumber = "tenderReferenceNumber";
            const string billInvoiceNumber = "billInvoiceNumber";
            const string agencyCode = "agencyCode";
            const string supplierNumber = "supplierNumber";

            var billArchive = new BillArchive(conditionsBookletId, invitationId, tenderId,
                tenderReferenceNumber, billInvoiceNumber, agencyCode, supplierNumber);

            billArchive.ShouldNotBeNull();
            billArchive.ConditionsBookletID.ShouldBe(conditionsBookletId);
            billArchive.InvitationId.ShouldBe(invitationId);
            billArchive.TenderId.ShouldBe(tenderId);
            billArchive.TenderReferenceNumber.ShouldBe(tenderReferenceNumber);
            billArchive.BillInvoiceNumber.ShouldBe(billInvoiceNumber);
            billArchive.AgencyCode.ShouldBe(agencyCode);
            billArchive.SupplierNumber.ShouldBe(supplierNumber);
        }
    }
}
