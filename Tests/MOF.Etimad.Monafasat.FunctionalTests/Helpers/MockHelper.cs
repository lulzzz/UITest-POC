using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.Web.Infrastructure;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.FunctionalTests.Helpers
{
    public static class MockHelper
    {
        public static IOptionsSnapshot<T> CreateIOptionSnapshotMock<T>(T value) where T : class, new()
        {
            var mock = new Mock<IOptionsSnapshot<T>>();
            mock.Setup(m => m.Value).Returns(value);
            return mock.Object;
        }
        public static IApiClient CreateApiClientMock<T>(T value) where T : class, new()
        {
            var _mockApiClient = new Mock<IApiClient>();

            _mockApiClient.Setup(x => x.GetQueryResultAsync<T>(It.IsAny<string>(), It.IsAny<Dictionary<string, object>>()))
            .Returns(QueryResultModel<T>());

            _mockApiClient.Setup(x => x.GetAsync<T>(It.IsAny<string>(), It.IsAny<Dictionary<string, object>>()))
            .Returns(GetAsyncModel<T>());

            _mockApiClient.Setup(x => x.GetListAsync<T>(It.IsAny<string>(), It.IsAny<Dictionary<string, object>>()))
                .Returns(GetListAsyncModel<T>());

            return _mockApiClient.Object;
        }

        private static async Task<QueryResult<T>> QueryResultModel<T>() where T : class, new()
        {
            List<T> items = new List<T>();
            items.Add(new T());
            var result = new QueryResult<T>(items, 1, 1, 1);
            return result;
        }

        private static async Task<T> GetAsyncModel<T>() where T : class, new()
        {
            var result = new T();
            return result;
        }

        private static async Task<List<T>> GetListAsyncModel<T>() where T : class, new()
        {
            List<T> result = new List<T>();
            result.Add(new T());
            return result;
        }
    }
}
