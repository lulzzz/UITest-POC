//using MOF.Etimad.Monafasat.Integration;
//using MOF.Etimad.Monafasat.Integration.Enums;
//using System.Threading.Tasks;
//using Xunit;

//namespace MOF.Etimad.Monafasat.UnitTests.Services.CheckFundService
//{
//    public class CheckFundProxyTest
//    {
//        //private readonly ICheckFundProxy _checkFundProxy= ProxyResolver.Resolve<ICheckFundProxy>();
//        private readonly CheckFundProxy _checkFundProxy;

//        public CheckFundProxyTest()
//        {

//            _checkFundProxy = new CheckFundProxy();

//        }

//        [Fact]
//        public async Task GetProjectBudgetTypeAsync()
//        {
//            var agencyCode = "027001000000000";
//            var projectNumber = "211111";
//            var result = await _checkFundProxy.GetProjectBudgetByType(agencyCode, projectNumber, BudgetType.Cost, true);
//            Assert.IsType<decimal>(result);

//        }
//    }
//}
