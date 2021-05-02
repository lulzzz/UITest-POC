using Microsoft.AspNetCore.Http;
using MOF.Etimad.Monafasat.Services;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using Moq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Services.DashboardQueriesTest
{
    public class DashboardQueriesTest
    {
        private readonly Mock<IHttpContextAccessor> _moqhttpContextAccessor;
        private readonly DashboardQueries _sut;
        private readonly string _agencyCode = "";
        public DashboardQueriesTest()
        {
            _moqhttpContextAccessor = new Mock<IHttpContextAccessor>();
            _sut = new DashboardQueries(InitialData.context, _moqhttpContextAccessor.Object);
        }

        [Fact]
        public async Task ShouldGetRejectedPreplaningSuccess()
        {
            var result = await _sut.GetRejectedPreplaning(_agencyCode, new SharedKernal.SearchCriteria ());
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldGetRejectedTendersOfGrievenceSuccess()
        {
            var result = await _sut.GetRejectedTendersOfGrievence(_agencyCode, new SharedKernal.SearchCriteria());
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldGetTendersNeedActionOfGrievenceSuccess()
        {
            var result = await _sut.GetTendersNeedActionOfGrievence(_agencyCode, new SharedKernal.SearchCriteria());
            Assert.NotNull(result);
        }

        
        [Fact]
        public async Task ShouldGetPrePlanningNeedApprovalSuccess()
        {
            var result = await _sut.GetPrePlanningNeedApproval(_agencyCode, new SharedKernal.SearchCriteria());
            Assert.NotNull(result);
        }


        
        [Fact]
        public async Task ShouldGetBlockNeedManagerApprovalSuccess()
        {
            var result = await _sut.GetBlockNeedManagerApproval(new SharedKernal.SearchCriteria());
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldGetBlockNeedSecretaryApprovalSuccess()
        {
            var result = await _sut.GetBlockNeedSecretaryApproval(new SharedKernal.SearchCriteria());
            Assert.NotNull(result);
        }


        [Fact]
        public async Task ShouldGetBlockSecretaryRejectedSuccess()
        {
            var result = await _sut.GetBlockSecretaryRejected(new SharedKernal.SearchCriteria());
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldRejectedTendersOfCheckQualificationStageSuccess()
        {
            var result = await _sut.GetRejectedTendersOfCheckQualificationStage(1, new SharedKernal.SearchCriteria());
            Assert.NotNull(result);
        }


        [Fact]
        public async Task ShouldGetTendersNeedApprovalForPreQualificationCommitteeManagerAsyncSuccess()
        {
            var result = await _sut.GetTendersNeedApprovalForPreQualificationCommitteeManagerAsync(1, new SharedKernal.SearchCriteria(),_agencyCode);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldGetTendersWaitingForApproveOppeiningReportSuccess()
        {
            var result = await _sut.GetTendersWaitingForApproveOppeiningReport(1, new SharedKernal.SearchCriteria());
            Assert.NotNull(result);
        }


        [Fact]
        public async Task ShouldGetProcessNeedsApprovalByStatusPagingSuccess()
        {
            var result = await _sut.GetProcessNeedsApprovalByStatusPaging(1,_agencyCode);
            Assert.IsType<int>(result);
        }

        
        [Fact]
        public async Task ShouldTendersSummaryPagingAsyncSuccess()
        {
            var result = await _sut.TendersSummaryPagingAsync( new DashboardSearchCriteria());
            Assert.NotNull(result);
        }

        

        [Fact]
        public async Task ShouldSalesSummaryPagingAsyncSuccess()
        {
            var result = await _sut.SalesSummaryPagingAsync(new DashboardSearchCriteria());
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldGetRejectedTendersByStatusIdPagingAsyncSuccess()
        {
            var result = await _sut.GetRejectedTendersByStatusIdPagingAsync(1,1,new DashboardSearchCriteria());
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldGetTendersNeedActionOfGrievenceCountSuccess()
        {
            var result = await _sut.GetTendersNeedActionOfGrievenceCount(_agencyCode);
            Assert.IsType<int>(result);
        }


        [Fact]
        public async Task ShouldGetRejectedTendersOfDirectPurchaseSuccess()
        {
            MoqUser();
            var result = await _sut.GetRejectedTendersOfDirectPurchase(1, new DashboardSearchCriteria());
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldGetFinalAwardingStageProcessNeedsActionCountSuccess()
        {
            MoqUser();
            var result = await _sut.GetFinalAwardingStageProcessNeedsActionCount();
            Assert.IsType<int>(result);
        }


        [Fact]
        public async Task ShouldGetVROAwardStageProcessNeedsActionSuccess()
        {
            var result = await _sut.GetVROAwardStageProcessNeedsAction(1, new DashboardSearchCriteria());
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldGetTendersNeedApprovalForDirectPurchasePaginAsyncSuccess()
        {
            var result = await _sut.GetTendersNeedApprovalForDirectPurchasePaginAsync(1, new DashboardSearchCriteria());
            Assert.NotNull(result);
        }


        [Fact]
        public async Task ShouldGetTendersNeedApprovalForPreQualificationCommitteeManagerCountSuccess()
        {
            var result = await _sut.GetTendersNeedApprovalForPreQualificationCommitteeManagerCount(1, _agencyCode);
            Assert.IsType<int>(result);
        }

        [Fact]
        public async Task ShouldGetTendersNeedApprovalForDirectPurchaseSecretaryPaginAsyncSuccess()
        {
            var result = await _sut.GetTendersNeedApprovalForDirectPurchaseSecretaryPaginAsync(1, new SharedKernal.SearchCriteria());
            Assert.NotNull(result);
        }


        [Fact]
        public async Task ShouldGetTendersNeedApprovalForOfferCheckSecretaryPaginAsyncSuccess()
        {
            var result = await _sut.GetTendersNeedApprovalForOfferCheckSecretaryPaginAsync(1, new SharedKernal.SearchCriteria());
            Assert.NotNull(result);
        }


        [Fact]
        public async Task ShouldGetRejectedTendersForDataEntrySuccess()
        {
            var result = await _sut.GetRejectedTendersForDataEntry(1, new SharedKernal.SearchCriteria());
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldGetTendersInvitationsPagingAsyncSuccess()
        {
            var result = await _sut.GetTendersInvitationsPagingAsync(1, new SharedKernal.SearchCriteria());
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldOpeningNotificationsPagingAsyncSuccess()
        {
            var result = await _sut.OpeningNotificationsPagingAsync(1, new SharedKernal.SearchCriteria());
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldGetPendingEnquiriesSuccess()
        {
            var result = await _sut.GetPendingEnquiries(1,1, new SharedKernal.SearchCriteria());
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldGetRejectedTendersForOpeningStageSuccess()
        {
            var result = await _sut.GetRejectedTendersForOpeningStage(1, new SharedKernal.SearchCriteria());
            Assert.NotNull(result);
        }



        [Fact]
        public async Task ShouldGetTendersWaitingForApproveOpeningReportCountSuccess()
        {
            var result = await _sut.GetTendersWaitingForApproveOpeningReportCount(1);
            Assert.IsType<int>(result);
        }


        [Fact]
        public async Task ShouldGetRejectedTendersCountForOpeningStageSuccess()
        {
            var result = await _sut.GetRejectedTendersCountForOpeningStage(1);
            Assert.IsType<int>(result);
        }

        [Fact]
        public async Task ShouldGetTendersWaitingForApproveCheckingReportSuccess()
        {
            var result = await _sut.GetTendersWaitingForApproveCheckingReport(1,new SharedKernal.SearchCriteria ());
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldGetTendersWaitingForApproveAwardingReportSuccess()
        {
            var result = await _sut.GetTendersWaitingForApproveAwardingReport(1, new SharedKernal.SearchCriteria());
            Assert.NotNull(result);
        }


        [Fact]
        public async Task ShouldGetProcessNeedsApprovalForEtimadOfficcerSuccess()
        {
            var result = await _sut.GetProcessNeedsApprovalForEtimadOfficcer(1,_agencyCode);
            Assert.IsType<int>(result);
        }

        [Fact]
        public async Task ShouldGetQualificationProcessNeedsApprovalByStatusPagingAsyncSuccess()
        {
            var result = await _sut.GetQualificationProcessNeedsApprovalByStatusPagingAsync(1,new SharedKernal.SearchCriteria () );
            Assert.NotNull(result);
        }


        [Fact]
        public async Task ShouldGetUnderEstablishedTendersAsyncSuccess()
        {
            var result = await _sut.GetUnderEstablishedTendersAsync(1, new SharedKernal.SearchCriteria());
            Assert.NotNull(result);
        }


        [Fact]
        public async Task ShouldDirectInvitationsSummaryPagingAsyncSuccess()
        {
            var result = await _sut.DirectInvitationsSummaryPagingAsync(new DashboardSearchCriteria ());
            Assert.NotNull(result);
        }



        [Fact]
        public async Task ShouldSuppliersCountPagingAsyncSuccess()
        {
            var result = await _sut.SuppliersCountPagingAsync(new DashboardSearchCriteria());
            Assert.IsType<string>(result);
        }

        [Fact]
        public async Task ShouldGetLastTenPurshaseSuccess()
        {
            var result = await _sut.GetLastTenPurshase(1,_agencyCode);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldGetSuppliersEnquirySuccess()
        {
            var result = await _sut.GetSuppliersEnquiry(1, new SharedKernal.SearchCriteria ());
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldGetOpeningStageProcessNeedsActionPagingAsyncSuccess()
        {
            var result = await _sut.GetOpeningStageProcessNeedsActionPagingAsync(1, new SharedKernal.SearchCriteria());
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldGetRejectedTendersCountForDataEntrySuccess()
        {
            var result = await _sut.GetRejectedTendersCountForDataEntry(1);
            Assert.IsType<int>(result);
        }


        [Fact]
        public async Task ShouldGetTendersInvitationsCountSuccess()
        {
            var result = await _sut.GetTendersInvitationsCount(1);
            Assert.IsType<int>(result);
        }

        [Fact]
        public async Task ShouldGetLastTenPurshaseCountSuccess()
        {
            var result = await _sut.GetLastTenPurshaseCount(1,_agencyCode);
            Assert.IsType<int>(result);
        }

        [Fact]
        public async Task ShouldGetUnderEstablishedTendersCountSuccess()
        {
            var result = await _sut.GetUnderEstablishedTendersCount(1);
            Assert.IsType<int>(result);
        }

        [Fact]
        public async Task ShouldGetRejectedEscalationsPagingSuccess()
        {
            var result = await _sut.GetRejectedEscalationsPaging(_agencyCode);
            Assert.IsType<int>(result);
        }

        [Fact]
        public async Task ShouldGetCheckingAndAwardingStageProcessNeedsActionCountSuccess()
        {
            var result = await _sut.GetCheckingAndAwardingStageProcessNeedsActionCount(1);
            Assert.IsType<int>(result);
        }


        [Fact]
        public async Task ShouldGetCheckingAndAwardingStageProcessNeedsActionSuccess()
        {
            var result = await _sut.GetCheckingAndAwardingStageProcessNeedsAction(1,new SharedKernal.SearchCriteria ());
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldGetRejectedTendersCountOfCheckAndAwardingStageSuccess()
        {
            var result = await _sut.GetRejectedTendersCountOfCheckAndAwardingStage(1);
            Assert.IsType<int>(result);
        }

        [Fact]
        public async Task ShouldGetRejectedTendersOfCheckAndAwardingStageSuccess()
        {
            var result = await _sut.GetRejectedTendersOfCheckAndAwardingStage(1, new SharedKernal.SearchCriteria());
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldGetRejectedTendersCountOfDirectPurchaseCheckAndAwardingStageSuccess()
        {
            MoqUser();
            var result = await _sut.GetRejectedTendersCountOfDirectPurchaseCheckAndAwardingStage(1);
            Assert.IsType<int>(result);
        }

        [Fact]
        public async Task ShouldGetTendersNeedApprovalForDirectPurchaseCountAsyncSuccess()
        {
            var result = await _sut.GetTendersNeedApprovalForDirectPurchaseCountAsync(1);
            Assert.IsType<int>(result);
        }


        [Fact]
        public async Task ShouldGetPrePlaningNeedApprovalCountSuccess()
        {
            var result = await _sut.GetPrePlaningNeedApprovalCount(_agencyCode);
            Assert.IsType<int>(result);
        }

        [Fact]
        public async Task ShouldGetPrePlaningRejectedCountSuccess()
        {
            var result = await _sut.GetPrePlaningRejectedCount(_agencyCode);
            Assert.IsType<int>(result);
        }

        [Fact]
        public async Task ShouldGetManagerBlockNeedApprovalCountSuccess()
        {
            var result = await _sut.GetManagerBlockNeedApprovalCount();
            Assert.IsType<int>(result);
        }

        private void MoqUser()
        {
            var claim = new Claim("selectedAgency", "selectedAgency,022001000000");
            var usernum = new Claim("sub", "12345");
            var agencyCode = new Claim(IdentityConfigs.Claims.Agency, "022001000000");
            var branchId = new Claim(IdentityConfigs.Claims.BranchId, "1");
            var idintity = new GenericIdentity(IdentityConfigs.Claims.SelectedGovAgency, "selectedAgency");
            idintity.AddClaim(usernum);
            idintity.AddClaim(claim);
            idintity.AddClaim(agencyCode);
            idintity.AddClaim(branchId);
            _moqhttpContextAccessor.SetupGet(x => x.HttpContext.User.Identity).Returns(() => { return idintity; });
            _moqhttpContextAccessor.SetupGet(x => x.HttpContext.User.Claims).Returns(() => { return idintity.Claims; });
        }
    }
}
