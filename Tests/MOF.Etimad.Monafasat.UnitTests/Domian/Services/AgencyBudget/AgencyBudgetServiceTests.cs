using MOF.Etimad.Monafasat.Integration;
using MOF.Etimad.Monafasat.Integration.Models;
using MOF.Etimad.Monafasat.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Services.AgencyBudget
{
    public class AgencyBudgetServiceTests
    {
        private readonly AgencyBudgetService _sut;
        private readonly Mock<IAgencyBudgetProxy> _agencyBudgettProxy;

        public AgencyBudgetServiceTests()
        {
            _agencyBudgettProxy = new Mock<IAgencyBudgetProxy>();
            _sut = new AgencyBudgetService(_agencyBudgettProxy.Object);
        }

        // [Fact]
        // public void Should_GetAgencyProjectBudget()
        // {
        //     ViewModel.AgencyBudget.AgencyBudgetModel agencyBudgetModel = new ViewModel.AgencyBudget.AgencyBudgetModel
        //     {
        //         Cash = 1
        //     };
        //     _agencyBudgettProxy.Setup(x => x.GetAgencyProjectBudget(It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<string>()))
        //                     .Returns(() =>
        //                     {
        //                         return Task.FromResult<ViewModel.AgencyBudget.AgencyBudgetModel>(agencyBudgetModel);
        //                     });

        //     var result = _sut.GetAgencyProjectBudget("1", true, "123");

        //     Assert.NotNull(result);
        //     Assert.IsType<ViewModel.AgencyBudget.AgencyBudgetModel>(result.Result);
        // }

    }

}
