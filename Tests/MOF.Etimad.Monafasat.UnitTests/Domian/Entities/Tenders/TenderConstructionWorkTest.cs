using MOF.Etimad.Monafasat.Core.Entities;
using Shouldly;
using Xunit;


namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Tenders
{
    public class TenderConstructionWorkTest
    {
        private const int constructionWorkId = 1;

        [Fact]
        public void Should_Construct_TenderConstructionWork()
        {
            TenderConstructionWork tenderConstructionWork = new TenderConstructionWork(constructionWorkId);
            _ = new TenderConstructionWork();
            _ = tenderConstructionWork.ConstructionWork;
            _ = tenderConstructionWork.TenderId;
            _ = tenderConstructionWork.Tender;

            tenderConstructionWork.ShouldNotBeNull();
        }

        [Fact]
        public void Should_Update()
        {
            TenderConstructionWork tenderConstructionWork = new TenderConstructionWork(constructionWorkId);
            tenderConstructionWork.Update();
            tenderConstructionWork.ShouldNotBeNull();
        }

        [Fact]
        public void Should_Deactive()
        {
            TenderConstructionWork tenderConstructionWork = new TenderConstructionWork(constructionWorkId);
            tenderConstructionWork.DeActive();
            Assert.False(tenderConstructionWork.IsActive);
        }

        [Fact]
        public void Should_SetActive()
        {
            TenderConstructionWork tenderConstructionWork = new TenderConstructionWork(constructionWorkId);
            tenderConstructionWork.SetActive();
            Assert.True(tenderConstructionWork.IsActive);
        }

        [Fact]
        public void Should_SetAddedState()
        {
            TenderConstructionWork tenderConstructionWork = new TenderConstructionWork(constructionWorkId);
            tenderConstructionWork.SetAddedState();
            Assert.Equal(0, tenderConstructionWork.Id);
        }

    }
}
