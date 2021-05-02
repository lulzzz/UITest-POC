using MOF.Etimad.Monafasat.Data;
using MOF.Etimad.Monafasat.Services;

namespace MOF.Etimad.Monafasat.UnitTests.Services.Block
{
    public class BlockCommandServiceTest
    {
        private readonly BlockCommands _sut;
        private readonly AppDbContext context;

        public BlockCommandServiceTest()
        {
            _sut = new BlockCommands(
                context);
        }

    }
}
