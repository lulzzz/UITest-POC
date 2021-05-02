using MOF.Etimad.Monafasat.Core.Entities;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Lookups
{
    public class ConstructionWorkTest
    {
        [Fact]
        public void Should_Construct_ConstructionWork()
        {
            ConstructionWork constructionWork = new ConstructionWork();
            _ = constructionWork.ParentID;
            _ = constructionWork.NameAr;
            _ = constructionWork.NameEn;
            _ = constructionWork.ConstructionWorkId;
            _ = constructionWork.ParentWork;
            _ = constructionWork.SubWorks;
            _ = constructionWork.TenderConstructionWorks;

            constructionWork.ShouldNotBeNull();
        }
    }
}
