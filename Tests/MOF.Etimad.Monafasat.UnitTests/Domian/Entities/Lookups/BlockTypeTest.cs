using MOF.Etimad.Monafasat.Core;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Lookups
{
    public class BlockTypeTest
    {
        private const int blockTypeId = 1;
        private const string nameAr = "Ar";
        private const string nameEn = "En";
        private const int blockStatusId = 1;

        [Fact]
        public void Should_Construct_BlockType()
        {
            BlockType blockType = new BlockType(blockTypeId, nameEn, nameAr);
            blockType.ShouldNotBeNull();
        }

        [Fact]
        public void Should_Construct_BlockStatus()
        {
            BlockStatus blockStatus = new BlockStatus(blockStatusId, nameEn, nameAr);
            blockStatus.ShouldNotBeNull();
        }
    }
}
