using MOF.Etimad.Monafasat.Core.Entities;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Lookups
{
    public class TenderPlaintRequestProcedureTest
    {
        [Fact]
        public void Should_Construct_TenderPlaintRequestProcedure()
        {
            TenderPlaintRequestProcedure tenderPlaintRequestProcedure = new TenderPlaintRequestProcedure();
            _ = tenderPlaintRequestProcedure.Id;
            _ = tenderPlaintRequestProcedure.Name;

            tenderPlaintRequestProcedure.ShouldNotBeNull();
        }
    }
}
