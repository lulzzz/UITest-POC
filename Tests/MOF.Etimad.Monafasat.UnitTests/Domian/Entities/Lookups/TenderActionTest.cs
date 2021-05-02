using MOF.Etimad.Monafasat.Core.Entities;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Lookups
{
    public class TenderActionTest
    {
        [Fact]
        public void Should_Construct_TenderAction()
        {
            TenderAction tenderAction = new TenderAction();
            _ = tenderAction.TenderActionId;
            _ = tenderAction.NameAr;
            _ = tenderAction.NameEn;

            tenderAction.ShouldNotBeNull();
        }
    }
}
