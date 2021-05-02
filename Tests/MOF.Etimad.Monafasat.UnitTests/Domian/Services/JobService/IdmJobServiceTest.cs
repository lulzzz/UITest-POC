using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Services;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Services.JobService
{
    public class IdmJobServiceTest
    {
        private readonly Mock<IDMJobQueries> _moqiDMQueries;
        private readonly IDMJobQueries _sut;
        public IdmJobServiceTest()
        {
            _moqiDMQueries = new Mock<IDMJobQueries>();
            _sut = new IDMJobQueries(InitialData.context);
        }

        [Fact]
        public async Task ShouldFindUsersProfileByIdAsyncSucess()
        {

            List<int?> userIds = new List<int?>() { 1, 2, 3 };
            var result = await _sut.FindUsersProfileByIdAsync(userIds);
            Assert.NotNull(result);
            Assert.IsType<List<UserProfile>>(result);
        }
    }
}
