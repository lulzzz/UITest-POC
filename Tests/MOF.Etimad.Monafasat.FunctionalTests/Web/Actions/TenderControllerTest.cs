
using Microsoft.AspNetCore.Mvc.Rendering;
using MOF.Etimad.Monafasat.FunctionalTests.Base;
using MOF.Etimad.Monafasat.FunctionalTests.Helpers;
using MOF.Etimad.Monafasat.Integration.Models;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.Web;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xunit;

namespace MOF.Etimad.Monafasat.FunctionalTests.Web.Actions
{
    public class TenderControllerTest : BaseTestController
    {
        private readonly string _tenderIdString = Util.Encrypt(3);
        public TenderControllerTest(CustomWebApplicationFactory<Startup> factory) : base(factory)
        {

        }

        [Fact]
        public async Task Index_Returns_ViewResult()
        {
            // arranges
            // act
            var response = await _client.GetAsync($"/Tender/Index");
            var responseContent = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(responseContent);
            Assert.Contains("!DOCTYPE html", responseContent);
        }

        [Fact]
        public async Task AllSupplierTendersAsync_Returns_JsonResult()
        {
            // arranges
            var requestModel = new TenderSearchCriteriaModel() { };
            var queryString = requestModel.ToDictionary().ToQueryString();
            // act
            var response = await _client.GetAsync($"/Tender/AllSupplierTendersAsync?" + queryString);
            var responseContent = ContentHelper.GetResponseContent<PaginationModel>(response);
            var data = responseContent.Result;
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response);
            Assert.NotNull(data);
            Assert.True(data.totalCount > 0);
        }

        [Fact]
        public async Task DetailsForSupplier_ViewResult()
        {
            // arranges
            var _sTenderId = "faetQ%20nrGIQJL*@@***@@**R8s8AOQ%3D%3D";
            // act
            var response = await _client.GetAsync($"/Tender/DetailsForSupplier?STenderId=" + _sTenderId);
            var responseContent = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(responseContent);
            Assert.Contains("200939022106", responseContent);
        }

        [Fact]
        public async Task OpenTenderDetailsReport_ViewResult()
        {
            // arranges
            var _sTenderId = "faetQ%20nrGIQJL*@@***@@**R8s8AOQ%3D%3D";
            // act
            var response = await _client.GetAsync($"/Tender/OpenTenderDetailsReport?STenderId=" + _sTenderId);
            var responseContent = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(responseContent);
            Assert.Contains("200939022106", responseContent);
        }

        [Fact]
        public async Task AllSuppliersTenders_ViewResult()
        {
            // arranges

            // act
            var response = await _client.GetAsync($"/Tender/AllSuppliersTenders");
            var responseContent = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(responseContent);
            Assert.Contains("200939022106", responseContent);
        }

        [Fact]
        public async Task IndexPagingAsync_Returns_JsonResult()
        {
            // arranges
            var requestModel = new TenderSearchCriteriaModel() { };
            var queryString = requestModel.ToDictionary().ToQueryString();
            // act
            var response = await _client.GetAsync($"/Tender/IndexPagingAsync?" + queryString);
            var responseContent = ContentHelper.GetResponseContent<PaginationModel>(response);
            var paginationModel = responseContent.Result;
            var tenders = JsonConvert.DeserializeObject(paginationModel.data.ToString(), typeof(List<BasicTenderModel>)) as List<BasicTenderModel>;
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response);
            Assert.NotNull(paginationModel);
            Assert.NotEmpty(tenders);
        }

        [Fact]
        public async Task AppliedSuppliersList_Returns_ViewResult()
        {
            // arrange
            // act
            var response = await _client.GetAsync($"/Tender/AppliedSuppliersList");
            var responseContent = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(responseContent);
            Assert.Contains("!DOCTYPE html", responseContent);
        }

        [Fact]
        public async Task AppliedSuppliersListPaging_Returns_JsonResult()
        {
            // arranges
            var requestModel = new TenderSearchCriteriaModel() { };
            var queryString = requestModel.ToDictionary().ToQueryString();
            // act
            var response = await _client.GetAsync($"/Tender/AppliedSuppliersListPaging?" + queryString);
            var responseContent = ContentHelper.GetResponseContent<PaginationModel>(response);
            var paginationModel = responseContent.Result;
            var tenders = JsonConvert.DeserializeObject(paginationModel.data.ToString(), typeof(List<BasicTenderModel>)) as List<BasicTenderModel>;
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response);
            Assert.NotNull(paginationModel);
            Assert.NotEmpty(tenders);
        }

        [Fact]
        public async Task GetTendersToJoinCommittees_Returns_JsonResult()
        {
            // arranges
            var requestModel = new LinkTendersToCommitteeSearchCriteriaModel() { CommitteeTypeId = (int)Enums.CommitteeType.PurchaseCommittee };
            var queryString = requestModel.ToDictionary().ToQueryString();
            // act
            var response = await _client.GetAsync($"/Tender/GetTendersToJoinCommittees?" + queryString);
            var responseContent = ContentHelper.GetResponseContent<PaginationModel>(response);
            var paginationModel = responseContent.Result;
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response);
            Assert.NotNull(paginationModel);
        }

        [Fact]
        public async Task GetSupplierInfoByCR_Returns_SupplierInfoPartialView()
        {
            // arrange
            string supplierCR = "1010000154";
            // act
            var response = await _client.GetAsync($"/Tender/GetSupplierInfoByCR/" + supplierCR);
            var responseContent = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(responseContent);
            Assert.Contains(supplierCR, responseContent);
        }

        [Fact]
        public async Task TenderAwardedIndexPagingAsync_Returns_JsonResult()
        {
            // arranges
            var requestModel = new TenderSearchCriteriaModel() { };
            var queryString = requestModel.ToDictionary().ToQueryString();
            // act
            var response = await _client.GetAsync($"/Tender/TenderAwardedIndexPagingAsync?" + queryString);
            var responseContent = ContentHelper.GetResponseContent<PaginationModel>(response);
            var paginationModel = responseContent.Result;
            var tenders = JsonConvert.DeserializeObject(paginationModel.data.ToString(), typeof(List<TenderCheckingAndAwardingModel>)) as List<TenderCheckingAndAwardingModel>;
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response);
            Assert.NotNull(paginationModel);
            Assert.NotEmpty(tenders);
        }

        [Fact]
        public async Task TenderIndexUnderOperationsStage_Returns_ViewResult()
        {
            // arranges

            // act
            var response = await _client.GetAsync($"/Tender/TenderIndexUnderOperationsStage");
            var responseContent = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(responseContent);
            Assert.Contains("!DOCTYPE html", responseContent);
        }

        [Fact]
        public async Task GetTendersForUnderOperationsStageIndexAsync_Returns_JsonResult()
        {
            // arranges
            var requestModel = new TenderSearchCriteriaModel() { };
            var queryString = requestModel.ToDictionary().ToQueryString();
            // act
            var response = await _client.GetAsync($"/Tender/GetTendersForUnderOperationsStageIndexAsync?" + queryString);
            var responseContent = ContentHelper.GetResponseContent<PaginationModel>(response);
            var paginationModel = responseContent.Result;
            var tenders = JsonConvert.DeserializeObject(paginationModel.data.ToString(), typeof(List<TenderCheckingAndAwardingModel>)) as List<TenderCheckingAndAwardingModel>;
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response);
            Assert.NotNull(paginationModel);
            Assert.NotEmpty(tenders);
        }

        [Fact]
        public async Task TenderIndexOpeningStage_Returns_ViewResult()
        {
            // arranges

            // act
            var response = await _client.GetAsync($"/Tender/TenderIndexOpeningStage");
            var responseContent = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(responseContent);
            Assert.Contains("!DOCTYPE html", responseContent);
        }

        [Fact]
        public async Task GetTendersForOpeningStageIndexAsync_Returns_JsonResult()
        {
            // arranges
            string listType = "DataEntry";
            var requestModel = new TenderSearchCriteriaModel() { TenderTypeString = listType };
            var queryString = requestModel.ToDictionary().ToQueryString();
            // act
            var response = await _client.GetAsync($"/Tender/GetTendersForOpeningStageIndexAsync?" + queryString);
            var responseContent = ContentHelper.GetResponseContent<PaginationModel>(response);
            var paginationModel = responseContent.Result;
            var tenders = JsonConvert.DeserializeObject(paginationModel.data.ToString(), typeof(List<TenderCheckingAndAwardingModel>)) as List<TenderCheckingAndAwardingModel>;
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response);
            Assert.NotNull(paginationModel);
        }

        [Fact]
        public async Task VROTendersCreatedByGovAgency_ViewResult()
        {
            // arranges

            // act
            var response = await _client.GetAsync($"/Tender/VROTendersCreatedByGovAgency");
            var responseContent = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(responseContent);
            Assert.Contains("!DOCTYPE html", responseContent);
        }

        [Fact]
        public async Task GetVROTendersCreatedByGovAgency_Returns_JsonResult()
        {
            // arranges
            var requestModel = new TenderSearchCriteria() { TenderTypeId = (int)Enums.TenderType.NationalTransformationProjects, IsActive = true, AgencyCode = "001001003000" };
            var queryString = requestModel.ToDictionary().ToQueryString();
            // act
            var response = await _client.GetAsync($"/Tender/GetVROTendersCreatedByGovAgency?" + queryString);
            var responseContent = ContentHelper.GetResponseContent<PaginationModel>(response);
            var paginationModel = responseContent.Result;
            var tenders = JsonConvert.DeserializeObject(paginationModel.data.ToString(), typeof(List<VROTendersCreatedByGovAgencyModel>)) as List<VROTendersCreatedByGovAgencyModel>;
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response);
            Assert.NotNull(paginationModel);
        }

        [Fact]
        public async Task TenderIndexCheckingStage_ViewResult()
        {
            // arranges

            // act
            var response = await _client.GetAsync($"/Tender/TenderIndexCheckingStage");
            var responseContent = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(responseContent);
            Assert.Contains("!DOCTYPE html", responseContent);
        }

        [Fact]
        public async Task GetTendersForCheckingStageIndexAsync_Returns_JsonResult()
        {
            // arranges
            var requestModel = new TenderSearchCriteriaModel() { TenderNumber = "1234321" };
            var queryString = requestModel.ToDictionary().ToQueryString();
            // act
            var response = await _client.GetAsync($"/Tender/GetTendersForCheckingStageIndexAsync?" + queryString);
            var responseContent = ContentHelper.GetResponseContent<PaginationModel>(response);
            var paginationModel = responseContent.Result;
            var tenders = JsonConvert.DeserializeObject(paginationModel.data.ToString(), typeof(List<TenderCheckingAndAwardingModel>)) as List<TenderCheckingAndAwardingModel>;
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response);
            Assert.NotNull(paginationModel);
            //Assert.NotEmpty(tenders);
        }

        [Fact]
        public async Task TenderIndexAwardingStage_ViewResult()
        {
            // arranges

            // act
            var response = await _client.GetAsync($"/Tender/TenderIndexAwardingStage");
            var responseContent = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(responseContent);
            Assert.Contains("!DOCTYPE html", responseContent);
        }

        [Fact]
        public async Task GetTendersForAwardingStageIndexAsync_Returns_JsonResult()
        {
            // arranges
            var requestModel = new TenderSearchCriteriaModel() { TenderNumber = "1234321" };
            var queryString = requestModel.ToDictionary().ToQueryString();
            // act
            var response = await _client.GetAsync($"/Tender/GetTendersForAwardingStageIndexAsync?" + queryString);
            var responseContent = ContentHelper.GetResponseContent<PaginationModel>(response);
            var paginationModel = responseContent.Result;
            var tenders = JsonConvert.DeserializeObject(paginationModel.data.ToString(), typeof(List<TenderCheckingAndAwardingModel>)) as List<TenderCheckingAndAwardingModel>;
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response);
            Assert.NotNull(paginationModel);
            //Assert.NotEmpty(tenders);
        }

        [Fact]
        public async Task RejectInitialAwardTenderOffer_Returns_Ok()
        {
            // arrange
            HttpResponseMessage initialResponse = await _client.GetAsync($"/Tender/Index");
            var antiForgeryValue = await ExtractAntiForgeryValue(initialResponse);
            HttpRequestMessage postRequest = new HttpRequestMessage(HttpMethod.Post, $"/Tender/RejectInitialAwardTenderOffer");
            postRequest.Headers.Add("Cookie", new CookieHeaderValue(AntiForgeryCookieName, antiForgeryValue.cookieValue).ToString());
            var formData = new Dictionary<string, string>
            {
               { AntiForgeryFieldName,antiForgeryValue.fieldValue},
                { "TenderIdString",_tenderIdString}
            };

            postRequest.Content = new FormUrlEncodedContent(formData);
            // act
            HttpResponseMessage response = await _client.SendAsync(postRequest);
            var responseContent = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.NotNull(response);
            Assert.NotNull(responseContent);
        }

        [Fact]
        public async Task RejectInitialAwardTenderOffer_Returns_Error()
        {
            // arranges
            // act
            var response = await _client.GetAsync($"/Tender/RejectInitialAwardTenderOffer?TenderIdString" + _tenderIdString);
            var responseContent = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.NotNull(response);
            Assert.Equal("{ message = Unexpected Error }", responseContent);
        }

        [Fact]
        public async Task SendRequestToApplayForTenderPost_Returns_Ok()
        {
            // arrange
            HttpResponseMessage initialResponse = await _client.GetAsync($"/Tender/Index");
            var antiForgeryValue = await ExtractAntiForgeryValue(initialResponse);
            HttpRequestMessage postRequest = new HttpRequestMessage(HttpMethod.Post, $"/Tender/SendRequestToApplayForTender");
            postRequest.Headers.Add("Cookie", new CookieHeaderValue(AntiForgeryCookieName, antiForgeryValue.cookieValue).ToString());
            var formData = new Dictionary<string, string>
            {
               { AntiForgeryFieldName,antiForgeryValue.fieldValue},
                { "tenderId",_tenderIdString}
            };

            postRequest.Content = new FormUrlEncodedContent(formData);
            // act
            HttpResponseMessage response = await _client.SendAsync(postRequest);
            var responseContent = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response);
            Assert.NotNull(responseContent);
        }

        [Fact]
        public async Task SendRequestToApplayForTenderPost_Returns_Error()
        {
            // arrange
            HttpResponseMessage initialResponse = await _client.GetAsync($"/Tender/Index");
            var antiForgeryValue = await ExtractAntiForgeryValue(initialResponse);
            HttpRequestMessage postRequest = new HttpRequestMessage(HttpMethod.Post, $"/Tender/SendRequestToApplayForTender");
            postRequest.Headers.Add("Cookie", new CookieHeaderValue(AntiForgeryCookieName, antiForgeryValue.cookieValue).ToString());
            var formData = new Dictionary<string, string>
            {
               { AntiForgeryFieldName,antiForgeryValue.fieldValue},
                { "tenderId","2"}
            };

            postRequest.Content = new FormUrlEncodedContent(formData);
            // act
            HttpResponseMessage response = await _client.SendAsync(postRequest);
            var responseContent = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.NotNull(response);
            Assert.Contains("message", responseContent);
        }

        [Fact]
        public async Task SendRequestToApplayForTenderGet_ViewResult()
        {
            // arranges

            // act
            var response = await _client.GetAsync($"/Tender/SendRequestToApplayForTender/" + _tenderIdString);
            var responseContent = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(responseContent);
            Assert.Contains("!DOCTYPE html", responseContent);
        }

        [Fact]
        public async Task Create_ViewResult()
        {
            // arranges

            // act
            var response = await _client.GetAsync($"/Tender/Create?TenderIdString=" + _tenderIdString);
            var responseContent = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(responseContent);
            Assert.Contains("!DOCTYPE html", responseContent);
        }

        [Fact]
        public async Task CreateVerificationCode_Returns_Ok()
        {
            // arrange
            HttpResponseMessage initialResponse = await _client.GetAsync($"/Tender/Index");
            var antiForgeryValue = await ExtractAntiForgeryValue(initialResponse);
            HttpRequestMessage postRequest = new HttpRequestMessage(HttpMethod.Post, $"/Tender/CreateVerificationCode");
            postRequest.Headers.Add("Cookie", new CookieHeaderValue(AntiForgeryCookieName, antiForgeryValue.cookieValue).ToString());
            var formData = new Dictionary<string, string>
            {
               { AntiForgeryFieldName,antiForgeryValue.fieldValue},
                { "tenderIdString",_tenderIdString}
            };
            postRequest.Content = new FormUrlEncodedContent(formData);

            // act
            HttpResponseMessage response = await _client.SendAsync(postRequest);
            var responseContent = await response.Content.ReadAsStringAsync();

            // Assert

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response);
            Assert.NotNull(responseContent);
        }

        [Fact]
        public async Task CreateVerificationCode_Returns_Error()
        {
            // arrange
            HttpResponseMessage initialResponse = await _client.GetAsync($"/Tender/Index");
            var antiForgeryValue = await ExtractAntiForgeryValue(initialResponse);
            HttpRequestMessage postRequest = new HttpRequestMessage(HttpMethod.Post, $"/Tender/CreateVerificationCode");
            postRequest.Headers.Add("Cookie", new CookieHeaderValue(AntiForgeryCookieName, antiForgeryValue.cookieValue).ToString());
            var formData = new Dictionary<string, string>
            {
               { AntiForgeryFieldName,antiForgeryValue.fieldValue},
               { "tenderIdString","60"}
            };

            postRequest.Content = new FormUrlEncodedContent(formData);
            // act
            HttpResponseMessage response = await _client.SendAsync(postRequest);
            var responseContent = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.NotNull(response);
            Assert.NotNull(responseContent);
            Assert.Contains("message", responseContent);
        }

        [Fact]
        public async Task ConditionsBookletTemplate_ViewResult()
        {
            // arranges

            // act
            var response = await _client.GetAsync($"/Tender/ConditionsBookletTemplat");
            var responseContent = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(responseContent);
            Assert.Contains("!DOCTYPE html", responseContent);
        }

        [Fact]
        public async Task SendRequestToApplayForTenderGet_Returns_Error()
        {
            // arranges
            // act
            var response = await _client.GetAsync($"/Tender/SendRequestToApplayForTenderGet?TenderIdString=" + _tenderIdString);
            var responseContent = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.NotNull(response);
            Assert.Equal("{ message = Unexpected Error }", responseContent);
        }

        [Fact]
        public async Task GetStatusIDs_Returns_AuditerTenderStatusIds()
        {
            // arrange
            string listType = "DataEntry";
            var tenderSearchCriteriaModel = new TenderSearchCriteriaModel() { TenderTypeString = listType };
            var queryString = tenderSearchCriteriaModel.ToDictionary().ToQueryString();
            var expectedIDs = GetExpectedtenderStatusIds(RoleNames.Auditer, listType);
            // act 
            var response = await _client.GetAsync($"/Tender/GetStatusIDs?" + queryString + "&&listType=" + listType);
            var responseContent = await ContentHelper.GetResponseContent<List<int>>(response);
            // assert

            // assert
            Assert.NotNull(response);
            Assert.NotEmpty(responseContent);
            Assert.Equal(expectedIDs.Count, responseContent.Count);
            Assert.Equal(expectedIDs[0].ToString(), responseContent[0].ToString());
            Assert.Equal(expectedIDs[1].ToString(), responseContent[1].ToString());
            Assert.Equal(expectedIDs[2].ToString(), responseContent[2].ToString());
            Assert.Equal(expectedIDs[3].ToString(), responseContent[3].ToString());
        }

        [Fact]
        public async Task GetStatusIDs_Returns_DataEntryTenderStatusIds()
        {
            // arrange
            var tenderSearchCriteriaModel = new TenderSearchCriteriaModel() { TenderTypeString = "DataEntry" };
            var expectedIDs = GetExpectedtenderStatusIds(RoleNames.DataEntry, "DataEntry");
            var queryString = tenderSearchCriteriaModel.ToDictionary().ToQueryString();
            // act 
            var response = await _client.GetAsync($"/Tender/GetStatusIDs?" + queryString);
            var responseContent = await ContentHelper.GetResponseContent<List<int>>(response);
            // assert
            Assert.NotNull(response);
            Assert.NotEmpty(responseContent);
            Assert.Equal(expectedIDs.Count, responseContent.Count);
            Assert.Equal(expectedIDs[0].ToString(), responseContent[0].ToString());
            Assert.Equal(expectedIDs[1].ToString(), responseContent[1].ToString());
            Assert.Equal(expectedIDs[2].ToString(), responseContent[2].ToString());
            Assert.Equal(expectedIDs[3].ToString(), responseContent[3].ToString());
        }

        [Fact]
        public async Task SupplierInvitationTenders_ViewResult()
        {
            // arranges

            // act
            var response = await _client.GetAsync($"/Tender/SupplierInvitationTenders");
            var responseContent = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(responseContent);
            Assert.Contains("!DOCTYPE html", responseContent);
        }

        [Fact]
        public async Task SupplierInvitationTendersPagingAsync_Returns_JsonResult()
        {
            // arranges
            var requestModel = new TenderSearchCriteria() { TenderNumber = "1234321" };
            var queryString = requestModel.ToDictionary().ToQueryString();
            // act
            var response = await _client.GetAsync($"/Tender/SupplierInvitationTendersPagingAsyn?" + queryString);
            var responseContent = ContentHelper.GetResponseContent<PaginationModel>(response);
            var paginationModel = responseContent.Result;
            var tenders = JsonConvert.DeserializeObject(paginationModel.data.ToString(), typeof(List<TenderCheckingAndAwardingModel>)) as List<TenderCheckingAndAwardingModel>;
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response);
            Assert.NotNull(paginationModel);
            //Assert.NotEmpty(tenders);
        }

        [Fact]
        public async Task AllSupplierTendersForVisitorAsync_Returns_JsonResult()
        {
            // arranges
            var requestModel = new TenderSearchCriteriaModel() { TenderNumber = "1234321" };
            var queryString = requestModel.ToDictionary().ToQueryString();
            // act
            var response = await _client.GetAsync($"/Tender/AllSupplierTendersForVisitorAsync?" + queryString);
            var responseContent = ContentHelper.GetResponseContent<PaginationModel>(response);
            var paginationModel = responseContent.Result;
            var tenders = JsonConvert.DeserializeObject(paginationModel.data.ToString(), typeof(List<AllTendersForVisitorModel>)) as List<AllTendersForVisitorModel>;
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response);
            Assert.NotNull(paginationModel);
            Assert.NotEmpty(tenders);
        }

        [Fact]
        public async Task UnSubscribedSuppliers_ViewResult()
        {
            // arranges

            // act
            var response = await _client.GetAsync($"/Tender/UnSubscribedSuppliers");
            var responseContent = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(responseContent);
            Assert.Contains("!DOCTYPE html", responseContent);
        }

        [Fact]
        public async Task SupplierTenders_ViewResult()
        {
            // arranges

            // act
            var response = await _client.GetAsync($"/Tender/SupplierTenders");
            var responseContent = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(responseContent);
            Assert.Contains("!DOCTYPE html", responseContent);
        }

        [Fact]
        public async Task AllTendersForVisitor_ViewResult()
        {
            // arranges

            // act
            var response = await _client.GetAsync($"/Tender/AllTendersForVisito");
            var responseContent = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(responseContent);
            Assert.Contains("!DOCTYPE html", responseContent);
        }

        [Fact]
        public async Task AgencyProjectBudget_Returns_JsonResult()
        {
            // arranges
            string _projectNumber = "";
            bool _isGfs = true;
            // act
            var response = await _client.GetAsync($"/Tender/AgencyProjectBudget?ProjectNumber=" + _projectNumber + "&IsGfs=" + _isGfs);
            var responseContent = ContentHelper.GetResponseContent<PaginationModel>(response);
            var paginationModel = responseContent.Result;
            var agencyProjectBudget = JsonConvert.DeserializeObject(paginationModel.data.ToString(), typeof(List<AgencyBudgetModel>)) as List<AgencyBudgetModel>;
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response);
            Assert.NotNull(paginationModel);
            //Assert.NotEmpty(tenders);
        }

        [Fact]
        public async Task AllTendersAsync_Returns_JsonResult()
        {
            // arranges
            // act
            var response = await _client.GetAsync($"/Tender/AllTendersAsync");
            var responseContent = ContentHelper.GetResponseContent<PaginationModel>(response);
            var paginationModel = responseContent.Result;
            var tenderInvitationDetailsModel = JsonConvert.DeserializeObject(paginationModel.data.ToString(), typeof(List<TenderInvitationDetailsModel>)) as List<TenderInvitationDetailsModel>;
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response);
            Assert.NotNull(paginationModel);
            Assert.NotEmpty(tenderInvitationDetailsModel);
        }

        [Fact]
        public async Task AllSubscribedSupplierTendersAsync_Returns_JsonResult()
        {
            // arranges
            var requestModel = new TenderSearchCriteriaModel() { TenderNumber = "1234321" };
            var queryString = requestModel.ToDictionary().ToQueryString();
            // act
            var response = await _client.GetAsync($"/Tender/AllSubscribedSupplierTendersAsync?" + queryString);
            var responseContent = ContentHelper.GetResponseContent<PaginationModel>(response);
            var paginationModel = responseContent.Result;
            var tenders = JsonConvert.DeserializeObject(paginationModel.data.ToString(), typeof(List<TenderInvitationDetailsModel>)) as List<TenderInvitationDetailsModel>;
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response);
            Assert.NotNull(paginationModel);
            Assert.NotEmpty(tenders);
        }

        [Fact]
        public async Task SupplierTendersAsync_Returns_JsonResult()
        {
            // arranges
            var requestModel = new TenderSearchCriteria() { TenderNumber = "1234321" };
            var queryString = requestModel.ToDictionary().ToQueryString();
            // act
            var response = await _client.GetAsync($"/Tender/SupplierTendersAsync?" + queryString);
            var responseContent = ContentHelper.GetResponseContent<PaginationModel>(response);
            var paginationModel = responseContent.Result;
            var tenders = JsonConvert.DeserializeObject(paginationModel.data.ToString(), typeof(List<TenderInvitationDetailsModel>)) as List<TenderInvitationDetailsModel>;
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response);
            Assert.NotNull(paginationModel);
            Assert.NotEmpty(tenders);
        }

        [Fact]
        public async Task SuppliersJoiningRequest_Returns_ViewResult()
        {
            // arranges
            // act
            var response = await _client.GetAsync($"/Tender/SuppliersJoiningRequest?tenderIdString" + _tenderIdString);
            var responseContent = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(responseContent);
            Assert.Contains("!DOCTYPE html", responseContent);
        }

        [Fact]
        public async Task TenderIndexUnitStage_Returns_ViewResult()
        {
            // arranges
            // act
            var response = await _client.GetAsync($"/Tender/SuppliersJoiningRequest?TenderIndexUnitStage?Type=1");
            var responseContent = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(responseContent);
            Assert.Contains("!DOCTYPE html", responseContent);
        }

        [Fact]
        public async Task TenderExtendDateApprovement_Returns_ViewResult()
        {
            // arranges
            // act
            var response = await _client.GetAsync($"/Tender/SuppliersJoiningRequest?TenderExtendDateApprovement?tenderIdString=" + Util.Encrypt(4));
            var responseContent = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(responseContent);
            Assert.Contains("!DOCTYPE html", responseContent);
        }

        [Fact]
        public async Task TenderQuantityTableChangesApprovement_Returns_ViewResult()
        {
            // arranges
            // act
            var response = await _client.GetAsync($"/Tender/SuppliersJoiningRequest?TenderQuantityTableChangesApprovement?tenderIdString=" + Util.Encrypt(4));
            var responseContent = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(responseContent);
            Assert.Contains("!DOCTYPE html", responseContent);
        }

        [Fact]
        public async Task TenderAttachmentsChangesApprovement_Returns_ViewResult()
        {
            // arranges
            // act
            var response = await _client.GetAsync($"/Tender/SuppliersJoiningRequest?TenderAttachmentsChangesApprovement?tenderIdString=" + Util.Encrypt(4));
            var responseContent = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(responseContent);
            Assert.Contains("!DOCTYPE html", responseContent);
        }

        [Fact]
        public async Task SuppliersJoininqRequestPagingAsync_Returns_JsonResult()
        {
            // arranges
            // act
            var response = await _client.GetAsync($"/Tender/SuppliersJoininqRequestPagingAsync?tenderIdString=" + _tenderIdString);
            var responseContent = ContentHelper.GetResponseContent<PaginationModel>(response);
            var paginationModel = responseContent.Result;
            var tenders = JsonConvert.DeserializeObject(paginationModel.data.ToString(), typeof(List<SupplierInvitationModel>)) as List<SupplierInvitationModel>;
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response);
            Assert.NotNull(paginationModel);
            Assert.NotEmpty(tenders);
        }

        [Fact]
        public async Task GetAllCities_Returns_JsonResult()
        {
            // arranges
            // act
            var response = await _client.GetAsync($"/Tender/GetAllCities");
            var cities = await ContentHelper.GetResponseContent<List<LookupModel>>(response) as List<LookupModel>;
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response);
            Assert.NotEmpty(cities);
        }

        [Fact]
        public async Task TenderInvitationDetails_Returns_ViewResult()
        {
            // arranges
            // act
            var response = await _client.GetAsync($"/Tender/TenderInvitationDetails?tenderIdString" + _tenderIdString);
            var responseContent = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(responseContent);
            Assert.Contains("!DOCTYPE html", responseContent);
        }

        [Fact]
        public async Task UpdateInvitationStatus_Returns_Ok()
        {
            // arrange
            string _invitationId = "0J4PeaOHdn0*@@**2z1YClk0Ng==";
            string _statusId = "1";
            HttpResponseMessage initialResponse = await _client.GetAsync($"/Tender/Index");
            var antiForgeryValue = await ExtractAntiForgeryValue(initialResponse);
            HttpRequestMessage postRequest = new HttpRequestMessage(HttpMethod.Post, $"/Tender/UpdateInvitationStatus");
            postRequest.Headers.Add("Cookie", new CookieHeaderValue(AntiForgeryCookieName, antiForgeryValue.cookieValue).ToString());
            var formData = new Dictionary<string, string>
            {
               { AntiForgeryFieldName,antiForgeryValue.fieldValue},
               {"invitationId", _invitationId},
               {"statusId", _statusId}
            };

            postRequest.Content = new FormUrlEncodedContent(formData);

            // act
            HttpResponseMessage response = await _client.SendAsync(postRequest);
            var responseContent = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response);
            Assert.NotNull(responseContent);
        }

        [Fact]
        public async Task AcceptInvitationWithFeesAsync_Returns_Ok()
        {
            // arrange
            var _tenderFinantialCostModel = new TenderFinantialCostModel() { };
            HttpResponseMessage initialResponse = await _client.GetAsync($"/Tender/Index");
            var antiForgeryValue = await ExtractAntiForgeryValue(initialResponse);
            HttpRequestMessage postRequest = new HttpRequestMessage(HttpMethod.Post, $"/Tender/AcceptInvitationWithFeesAsync");
            postRequest.Headers.Add("Cookie", new CookieHeaderValue(AntiForgeryCookieName, antiForgeryValue.cookieValue).ToString());
            var formData = new Dictionary<string, string>
            {
               { AntiForgeryFieldName,antiForgeryValue.fieldValue},
               { "Id","1"}
            };
            postRequest.Content = new FormUrlEncodedContent(formData);

            // act
            HttpResponseMessage response = await _client.SendAsync(postRequest);
            var responseContent = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response);
            Assert.NotNull(responseContent);
        }

        [Fact]
        public async Task JoinDirectPurchaseTenderAsync_Returns_Ok()
        {
            // arrange
            HttpResponseMessage initialResponse = await _client.GetAsync($"/Tender/Index");
            var antiForgeryValue = await ExtractAntiForgeryValue(initialResponse);
            HttpRequestMessage postRequest = new HttpRequestMessage(HttpMethod.Post, $"/Tender/JoinDirectPurchaseTenderAsync");
            postRequest.Headers.Add("Cookie", new CookieHeaderValue(AntiForgeryCookieName, antiForgeryValue.cookieValue).ToString());
            var formData = new Dictionary<string, string>
            {
               { AntiForgeryFieldName,antiForgeryValue.fieldValue},
               {"tenderIdString", _tenderIdString}
            };
            postRequest.Content = new FormUrlEncodedContent(formData);

            // act
            HttpResponseMessage response = await _client.SendAsync(postRequest);
            var responseContent = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.NotNull(response);
            Assert.NotNull(responseContent);
        }

        [Fact]
        public async Task ApproveJoiningRequestStatus_Returns_Ok()
        {
            // arrange
            string _invitationId = "0J4PeaOHdn0*@@**2z1YClk0Ng==";
            string _statusId = "1";
            HttpResponseMessage initialResponse = await _client.GetAsync($"/Tender/Index");
            var antiForgeryValue = await ExtractAntiForgeryValue(initialResponse);
            HttpRequestMessage postRequest = new HttpRequestMessage(HttpMethod.Post, $"/Tender/ApproveJoiningRequestStatus");
            postRequest.Headers.Add("Cookie", new CookieHeaderValue(AntiForgeryCookieName, antiForgeryValue.cookieValue).ToString());
            var formData = new Dictionary<string, string>
            {
               { AntiForgeryFieldName,antiForgeryValue.fieldValue},
                { "invitationId",_invitationId},
                { "statusId",_statusId}
            };

            postRequest.Content = new FormUrlEncodedContent(formData);
            // act
            HttpResponseMessage response = await _client.SendAsync(postRequest);
            var responseContent = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response);
            Assert.NotNull(responseContent);
        }

        [Fact]
        public async Task ApproveJoiningRequestStatus_Returns_Error()
        {
            // arrange
            string _invitationId = "15";
            string _statusId = "1";
            HttpResponseMessage initialResponse = await _client.GetAsync($"/Tender/Index");
            var antiForgeryValue = await ExtractAntiForgeryValue(initialResponse);
            HttpRequestMessage postRequest = new HttpRequestMessage(HttpMethod.Post, $"/Tender/ApproveJoiningRequestStatus");
            postRequest.Headers.Add("Cookie", new CookieHeaderValue(AntiForgeryCookieName, antiForgeryValue.cookieValue).ToString());
            var formData = new Dictionary<string, string>
            {
               { AntiForgeryFieldName,antiForgeryValue.fieldValue},
                { "invitationId",_invitationId},
                { "statusId",_statusId}
            };

            postRequest.Content = new FormUrlEncodedContent(formData);
            // act
            HttpResponseMessage response = await _client.SendAsync(postRequest);
            var responseContent = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.NotNull(response);
            Assert.Contains("message", responseContent);
        }


        [Fact]
        public async Task RejectJoiningRequestStatus_Returns_Ok()
        {
            // arrange
            string _invitationId = "8";
            string _rejectionReason = "reject reason";
            HttpResponseMessage initialResponse = await _client.GetAsync($"/Tender/Index");
            var antiForgeryValue = await ExtractAntiForgeryValue(initialResponse);
            HttpRequestMessage postRequest = new HttpRequestMessage(HttpMethod.Post, $"/Tender/RejectJoiningRequestStatus");
            postRequest.Headers.Add("Cookie", new CookieHeaderValue(AntiForgeryCookieName, antiForgeryValue.cookieValue).ToString());
            var formData = new Dictionary<string, string>
            {
               { AntiForgeryFieldName,antiForgeryValue.fieldValue},
               {"sInvitationId", _invitationId},
               {"rejectionReason", _rejectionReason}
            };
            postRequest.Content = new FormUrlEncodedContent(formData);

            // act
            HttpResponseMessage response = await _client.SendAsync(postRequest);
            var responseContent = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response);
            Assert.NotNull(responseContent);
        }

        [Fact]
        public async Task SendTenderInvitations_Returns_ViewResult()
        {
            // arranges
            // act
            var response = await _client.GetAsync($"/Tender/RejectJoiningRequestStatus?tenderIdString=" + _tenderIdString);
            var responseContent = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(responseContent);
            Assert.Contains("!DOCTYPE html", responseContent);
        }

        [Fact]
        public async Task SendInvitationsAsync_Returns_Ok()
        {
            // arrange
            HttpResponseMessage initialResponse = await _client.GetAsync($"/Tender/Index");
            var antiForgeryValue = await ExtractAntiForgeryValue(initialResponse);
            HttpRequestMessage postRequest = new HttpRequestMessage(HttpMethod.Post, $"/Tender/SendInvitationsAsync");
            postRequest.Headers.Add("Cookie", new CookieHeaderValue(AntiForgeryCookieName, antiForgeryValue.cookieValue).ToString());
            var formData = new Dictionary<string, string>
            {
               { AntiForgeryFieldName,antiForgeryValue.fieldValue},
            };

            postRequest.Content = new FormUrlEncodedContent(formData);
            // act
            HttpResponseMessage response = await _client.SendAsync(postRequest);
            var responseContent = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.NotNull(response);
            Assert.NotNull(responseContent);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task SendInvitationsByEmailAsync_Returns_Ok()
        {
            // arrange
            HttpResponseMessage initialResponse = await _client.GetAsync($"/Tender/Index");
            var antiForgeryValue = await ExtractAntiForgeryValue(initialResponse);
            HttpRequestMessage postRequest = new HttpRequestMessage(HttpMethod.Post, $"/Tender/SendInvitationsByEmailAsync");
            postRequest.Headers.Add("Cookie", new CookieHeaderValue(AntiForgeryCookieName, antiForgeryValue.cookieValue).ToString());
            var formData = new Dictionary<string, string>
            {
               { AntiForgeryFieldName,antiForgeryValue.fieldValue},
               {"tenderId",_tenderIdString },
               {"emails","mail address" }
            };

            postRequest.Content = new FormUrlEncodedContent(formData);
            // act
            HttpResponseMessage response = await _client.SendAsync(postRequest);
            var responseContent = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.NotNull(response);
            Assert.NotNull(responseContent);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task SendInvitationsBySmsAsync_Returns_Ok()
        {
            // arrange
            HttpResponseMessage initialResponse = await _client.GetAsync($"/Tender/Index");
            var antiForgeryValue = await ExtractAntiForgeryValue(initialResponse);
            HttpRequestMessage postRequest = new HttpRequestMessage(HttpMethod.Post, $"/Tender/SendInvitationsBySmsAsync");
            postRequest.Headers.Add("Cookie", new CookieHeaderValue(AntiForgeryCookieName, antiForgeryValue.cookieValue).ToString());
            var formData = new Dictionary<string, string>
            {
               { AntiForgeryFieldName,antiForgeryValue.fieldValue},
               {"tenderId",_tenderIdString },
               {"mobileNoList","mobileNoList" }
            };

            postRequest.Content = new FormUrlEncodedContent(formData);
            // act
            HttpResponseMessage response = await _client.SendAsync(postRequest);
            var responseContent = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.NotNull(response);
            Assert.NotNull(responseContent);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetSuppliersBySearchCretria_Returns_JsonResult()
        {
            // arranges
            var requestModel = new SupplierSearchCretriaModel() { };
            var queryString = requestModel.ToDictionary().ToQueryString();
            // act
            var response = await _client.GetAsync($"/Tender/GetSuppliersBySearchCretria?" + queryString);
            var responseContent = ContentHelper.GetResponseContent<PaginationModel>(response);
            var paginationModel = responseContent.Result;
            var tenders = JsonConvert.DeserializeObject(paginationModel.data.ToString(), typeof(List<SupplierInvitationModel>)) as List<SupplierInvitationModel>;
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response);
            Assert.NotNull(paginationModel);
            Assert.NotEmpty(tenders);
        }

        [Fact]
        public async Task GetInvitedSuppliers_Returns_JsonResult()
        {
            // arranges
            var requestModel = new SupplierSearchCretriaModel() { };
            var queryString = requestModel.ToDictionary().ToQueryString();
            // act
            var response = await _client.GetAsync($"/Tender/GetInvitedSuppliers?" + queryString);
            var responseContent = ContentHelper.GetResponseContent<PaginationModel>(response);
            var paginationModel = responseContent.Result;
            var supplier = JsonConvert.DeserializeObject(paginationModel.data.ToString(), typeof(InvitationCrModel)) as InvitationCrModel;
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response);
            Assert.NotNull(paginationModel);
            Assert.NotNull(supplier);
        }

        [Fact]
        public async Task GetTechincalAndOpenAndCheckCommittees_Returns_JsonResult()
        {
            // arranges

            // act
            var response = await _client.GetAsync($"/Tender/GetTechincalAndOpenAndCheckCommittees");
            var editeCommitteesModel = ContentHelper.GetResponseContent<EditeCommitteesModel>(response).Result;
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response);
            Assert.NotNull(editeCommitteesModel);
            Assert.NotEmpty(editeCommitteesModel.TechnicalCommittees);
            Assert.NotEmpty(editeCommitteesModel.OfferOpenningCommittees);
            Assert.NotEmpty(editeCommitteesModel.OfferCheckingCommittees);
        }

        [Fact]
        public async Task ConvertNumberToText_Returns_JsonResult()
        {
            // arranges
            decimal _estimatedValue = 1;
            string _expectedResult = "ريال سعودي لا غير.";
            // act
            var response = await _client.GetAsync($"/Tender/ConvertNumberToText?estimatedValue=" + _estimatedValue);
            var _actualResult = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response);
            Assert.Equal(_expectedResult, _actualResult);
        }

        [Fact]
        public async Task GetInvitedUnRegisterSuppliers_Returns_JsonResult()
        {
            // arranges
            var requestModel = new SupplierSearchCretriaModel() { };
            var queryString = requestModel.ToDictionary().ToQueryString();
            // act
            var response = await _client.GetAsync($"/Tender/GetInvitedUnRegisterSuppliers?" + queryString);
            var responseContent = ContentHelper.GetResponseContent<PaginationModel>(response);
            var paginationModel = responseContent.Result;
            var supplier = JsonConvert.DeserializeObject(paginationModel.data.ToString(), typeof(InvitationCrModel)) as InvitationCrModel;
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response);
            Assert.NotNull(paginationModel);
            Assert.NotNull(supplier);
        }

        [Fact]
        public async Task GetInvitedUnRegisterSuppliersForCreation_Returns_JsonResult()
        {
            // arranges
            var requestModel = new SupplierSearchCretriaModel() { };
            var queryString = requestModel.ToDictionary().ToQueryString();
            // act
            var response = await _client.GetAsync($"/Tender/GetInvitedUnRegisterSuppliersForCreation?" + queryString);
            var responseContent = ContentHelper.GetResponseContent<PaginationModel>(response);
            var paginationModel = responseContent.Result;
            var supplier = JsonConvert.DeserializeObject(paginationModel.data.ToString(), typeof(InvitationCrModel)) as InvitationCrModel;
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response);
            Assert.NotNull(paginationModel);
            Assert.NotNull(supplier);
        }

        [Fact]
        public async Task GetAllInvitedSuppliersAndSendInvitationAgain_Returns_JsonResult()
        {
            // arranges
            var requestModel = new SupplierSearchCretriaModel() { InvitationTenderIdString = _tenderIdString };
            var queryString = requestModel.ToDictionary().ToQueryString();
            // act
            var response = await _client.GetAsync($"/Tender/GetAllInvitedSuppliersAndSendInvitationAgain?" + queryString);
            var responseContent = ContentHelper.GetResponseContent<PaginationModel>(response);
            var paginationModel = responseContent.Result;
            var supplier = JsonConvert.DeserializeObject(paginationModel.data.ToString(), typeof(InvitationCrModel)) as InvitationCrModel;
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response);
            Assert.NotNull(paginationModel);
            Assert.NotNull(supplier);
        }

        [Fact]
        public async Task JoiningRequestDetails_Returns_ViewResult()
        {
            // arranges
            // act
            var response = await _client.GetAsync($"/Tender/JoiningRequestDetails?invitationIdString=" + _tenderIdString);
            var responseContent = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(responseContent);
            Assert.Contains("!DOCTYPE html", responseContent);
        }

        [Fact]
        public async Task BasicSecondStageData_Returns_ViewResultWithModel()
        {
            // arranges
            var _id = "";
            // act
            var response = await _client.GetAsync($"/Tender/BasicSecondStageData?id=" + _id);
            var responseContent = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(responseContent);
            Assert.Contains("!DOCTYPE html", responseContent);
        }

        [Fact]
        public async Task BasicSecondStageData_Returns_ViewResult()
        {
            // arranges
            string _id = "";
            // act
            var response = await _client.GetAsync($"/Tender/BasicSecondStageData?id=" + _id);
            var responseContent = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(responseContent);
            Assert.Contains("!DOCTYPE html", responseContent);
        }

        [Fact]
        public async Task GetStatusIDs_Returns_OffersOppeningManagerTenderStatusIds()
        {
            // arrange
            string listType = "Open";
            var tenderSearchCriteriaModel = new TenderSearchCriteriaModel() { TenderTypeString = listType };
            var queryString = tenderSearchCriteriaModel.ToDictionary().ToQueryString();
            var expectedIDs = GetExpectedtenderStatusIds(RoleNames.OffersOppeningManager, listType);
            // act 
            var response = await _client.GetAsync($"/Tender/GetStatusIDs?" + queryString + "&&listType=" + listType);
            var responseContent = await ContentHelper.GetResponseContent<List<int>>(response);
            // assert
            Assert.NotNull(response);
            Assert.NotEmpty(responseContent);
            Assert.Equal(expectedIDs.Count, responseContent.Count);
            Assert.Equal(expectedIDs[0].ToString(), responseContent[0].ToString());
            Assert.Equal(expectedIDs[1].ToString(), responseContent[1].ToString());
            Assert.Equal(expectedIDs[2].ToString(), responseContent[2].ToString());
            Assert.Equal(expectedIDs[3].ToString(), responseContent[3].ToString());
        }

        [Fact]
        public async Task GetStatusIDs_Returns_OffersOppeningSecretaryTenderStatusIds()
        {
            // arrange
            string listType = "Open";
            var tenderSearchCriteriaModel = new TenderSearchCriteriaModel() { TenderTypeString = listType };
            var queryString = tenderSearchCriteriaModel.ToDictionary().ToQueryString();
            var expectedIDs = GetExpectedtenderStatusIds(RoleNames.OffersOppeningSecretary, listType);
            // act 
            var response = await _client.GetAsync($"/Tender/GetStatusIDs?" + queryString + "&&listType=" + listType);
            var responseContent = await ContentHelper.GetResponseContent<List<int>>(response);
            // assert
            Assert.NotNull(response);
            Assert.NotEmpty(responseContent);
            Assert.Equal(expectedIDs.Count, responseContent.Count);
            Assert.Equal(expectedIDs[0].ToString(), responseContent[0].ToString());
            Assert.Equal(expectedIDs[1].ToString(), responseContent[1].ToString());
            Assert.Equal(expectedIDs[2].ToString(), responseContent[2].ToString());
            Assert.Equal(expectedIDs[3].ToString(), responseContent[3].ToString());
        }

        [Fact]
        public async Task GetStatusIDs_Returns_OffersCheckManagerTenderStatusIds_Check()
        {
            // arrange
            string listType = "Check";
            var tenderSearchCriteriaModel = new TenderSearchCriteriaModel() { TenderTypeString = listType };
            var expectedIDs = GetExpectedtenderStatusIds(RoleNames.OffersCheckManager, listType);
            var queryString = tenderSearchCriteriaModel.ToDictionary().ToQueryString();
            // act 
            var response = await _client.GetAsync($"/Tender/GetStatusIDs?" + queryString + "&&listType=" + listType);
            var responseContent = await ContentHelper.GetResponseContent<List<int>>(response);
            // assert
            Assert.NotNull(response);
            Assert.NotEmpty(responseContent);
            Assert.Equal(expectedIDs.Count, responseContent.Count);
            Assert.Equal(expectedIDs[0].ToString(), responseContent[0].ToString());
            Assert.Equal(expectedIDs[1].ToString(), responseContent[1].ToString());
            Assert.Equal(expectedIDs[2].ToString(), responseContent[2].ToString());
            Assert.Equal(expectedIDs[3].ToString(), responseContent[3].ToString());
        }

        [Fact]
        public async Task GetStatusIDs_Returns_OffersCheckManagerTenderStatusIds_Award()
        {
            // arrange
            string listType = "Award";
            var tenderSearchCriteriaModel = new TenderSearchCriteriaModel() { TenderTypeString = listType };
            var expectedIDs = GetExpectedtenderStatusIds(RoleNames.OffersCheckManager, listType);
            var queryString = tenderSearchCriteriaModel.ToDictionary().ToQueryString();

            // act 
            var response = await _client.GetAsync($"/Tender/GetStatusIDs?" + queryString + "&&listType=" + listType);
            var responseContent = await ContentHelper.GetResponseContent<List<int>>(response);
            // assert
            Assert.NotNull(response);
            Assert.NotEmpty(responseContent);
            Assert.Equal(expectedIDs.Count, responseContent.Count);
            Assert.Equal(expectedIDs[0].ToString(), responseContent[0].ToString());
            Assert.Equal(expectedIDs[1].ToString(), responseContent[1].ToString());
            Assert.Equal(expectedIDs[2].ToString(), responseContent[2].ToString());
            Assert.Equal(expectedIDs[3].ToString(), responseContent[3].ToString());
        }

        [Fact]
        public async Task GetStatusIDs_Returns_OffersCheckSecretaryTenderStatusIds_Check()
        {
            // arrange
            string listType = "Check";
            var tenderSearchCriteriaModel = new TenderSearchCriteriaModel() { TenderTypeString = listType };
            var expectedIDs = GetExpectedtenderStatusIds(RoleNames.OffersCheckSecretary, listType);
            var queryString = tenderSearchCriteriaModel.ToDictionary().ToQueryString();

            // act 
            var response = await _client.GetAsync($"/Tender/GetStatusIDs?" + queryString + "&&listType=" + listType);
            var responseContent = await ContentHelper.GetResponseContent<List<int>>(response);
            // assert
            Assert.NotNull(response);
            Assert.NotEmpty(responseContent);
            Assert.Equal(expectedIDs.Count, responseContent.Count);
            Assert.Equal(expectedIDs[0].ToString(), responseContent[0].ToString());
            Assert.Equal(expectedIDs[1].ToString(), responseContent[1].ToString());
            Assert.Equal(expectedIDs[2].ToString(), responseContent[2].ToString());
            Assert.Equal(expectedIDs[3].ToString(), responseContent[3].ToString());
        }

        [Fact]
        public async Task GetStatusIDs_Returns_OffersCheckSecretaryTenderStatusIds_Award()
        {
            // arrange
            string listType = "Award";
            var tenderSearchCriteriaModel = new TenderSearchCriteriaModel() { TenderTypeString = listType };
            var expectedIDs = GetExpectedtenderStatusIds(RoleNames.OffersCheckSecretary, listType);
            var queryString = tenderSearchCriteriaModel.ToDictionary().ToQueryString();

            // act 
            var response = await _client.GetAsync($"/Tender/GetStatusIDs?" + queryString + "&&listType=" + listType);
            var responseContent = await ContentHelper.GetResponseContent<List<int>>(response);
            // assert
            Assert.NotNull(response);
            Assert.NotEmpty(responseContent);
            Assert.Equal(expectedIDs.Count, responseContent.Count);
            Assert.Equal(expectedIDs[0].ToString(), responseContent[0].ToString());
            Assert.Equal(expectedIDs[1].ToString(), responseContent[1].ToString());
            Assert.Equal(expectedIDs[2].ToString(), responseContent[2].ToString());
            Assert.Equal(expectedIDs[3].ToString(), responseContent[3].ToString());
        }

        [Fact]
        public async Task DetailsForVisitor_Returns_ViewResult()
        {
            // arranges
            string _sTenderId = Util.Encrypt(12);
            string _refNum = "4101082374792911000001";
            // act
            var response = await _client.GetAsync($"/Tender/DetailsForVisitor?STenderId=" + _sTenderId);
            var responseContent = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(responseContent);
            Assert.Contains(_refNum, responseContent);
        }

        [Fact]
        public async Task GetTenderDatesViewComponenet_Returns_ViewResult()
        {
            // arranges
            string _sTenderId = Util.Encrypt(12);
            string _expDate = "12/02/2020";
            // act
            var response = await _client.GetAsync($"/Tender/GetTenderDatesViewComponenet?tenderIdStr=" + _sTenderId);
            var responseContent = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(responseContent);
            Assert.Contains(_expDate, responseContent);
        }

        [Fact]
        public async Task GetQuantitiesTableViewComponenet_Returns_ViewResult()
        {
            // arranges
            // act
            var response = await _client.GetAsync($"/Tender/GetQuantitiesTableViewComponenet?tenderIdStr=" + _tenderIdString);
            var responseContent = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(responseContent);
        }

        [Fact]
        public async Task GetRelationsDetailsViewComponenet_Returns_ViewResult()
        {
            // arranges
            // act
            var response = await _client.GetAsync($"/Tender/GetRelationsDetailsViewComponenet?tenderIdStr=" + _tenderIdString);
            var responseContent = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(responseContent);
            Assert.Contains("supplierModal", responseContent);
        }

        [Fact]
        public async Task GetPurchaseBookViewComponenet_Returns_ViewResult()
        {
            // arranges
            string _tenderIdStr = "aG 9xWcoWhvaIO b6jEzDA==";
            string _tenderStatusIdString = "6qPa1DWumsGG0KjPQhbCXA==";
            string _expecRSLT = "200639020793";
            // act
            var response = await _client.GetAsync($"/Tender/GetPurchaseBookViewComponenet?tenderIdStr=" + _tenderIdStr + "&tenderStatusIdString=" + _tenderStatusIdString);
            var responseContent = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(responseContent);
            Assert.Contains(_expecRSLT, responseContent);
        }

        [Fact]
        public async Task Details_Returns_ViewResult()
        {
            // arranges
            string _sTenderId = "aG 9xWcoWhvaIO b6jEzDA==";
            string _refNum = "191239000002";
            // act
            var response = await _client.GetAsync($"/Tender/Details?STenderId=" + _sTenderId);
            var responseContent = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(responseContent);
            Assert.Contains(_refNum, responseContent);
        }

        [Fact]
        public async Task GetAttachmentsViewComponenet_Returns_ViewResult()
        {
            // arranges
            string _tenderIdStr = "304Zme7AE5jMXFkjqDKI2g==";
            // act
            var response = await _client.GetAsync($"/Tender/GetAttachmentsViewComponenet?tenderIdStr=" + _tenderIdStr);
            var responseContent = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(responseContent);
        }

        [Fact]
        public async Task GetTenderNewsViewComponenet_Returns_ViewResult()
        {
            // arranges
            string _tenderIdStr = _tenderIdString;
            string _exp = "08:14:33";
            // act
            var response = await _client.GetAsync($"/Tender/GetTenderNewsViewComponenet?tenderIdStr=" + _tenderIdStr);
            var responseContent = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(responseContent);
            Assert.Contains(_exp, responseContent);
        }

        [Fact]
        public async Task PurshaseTender_Returns_JsonResult()
        {
            // arranges
            var _tenderIdString = "aG 9xWcoWhvaIO b6jEzDA==";
            // act
            var response = await _client.GetAsync($"/Tender/PurshaseTender?tenderIdString=" + _tenderIdString);
            var responseContent = ContentHelper.GetResponseContent<PurchaseModel>(response);
            var model = responseContent.Result;
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response);
            Assert.NotNull(responseContent);
            Assert.NotNull(model);
            Assert.True(!string.IsNullOrEmpty(model.BillInvoiceNumber));
            Assert.True(model.Price > 0);
            Assert.True(!string.IsNullOrEmpty(model.Email));
            Assert.True(!string.IsNullOrEmpty(model.Mobile));
        }

        private List<int> GetExpectedtenderStatusIds(string roleName, string listType)
        {
            List<int> tenderStatusIds = new List<int>();
            if ((roleName == RoleNames.DataEntry || roleName == RoleNames.Auditer) && listType == "DataEntry")
            {
                tenderStatusIds.Add((int)Enums.TenderStatus.UnderEstablishing);
                tenderStatusIds.Add((int)Enums.TenderStatus.Pending);
                tenderStatusIds.Add((int)Enums.TenderStatus.Established);
                tenderStatusIds.Add((int)Enums.TenderStatus.Approved);
            }
            else if ((roleName == RoleNames.OffersOppeningManager || roleName == RoleNames.OffersOppeningSecretary) && listType == "Open")
            {
                tenderStatusIds.Add((int)Enums.TenderStatus.Approved);
                tenderStatusIds.Add((int)Enums.TenderStatus.OffersOppening);
                tenderStatusIds.Add((int)Enums.TenderStatus.OffersOppenedPending);
                tenderStatusIds.Add((int)Enums.TenderStatus.OffersOppenedConfirmed);
                tenderStatusIds.Add((int)Enums.TenderStatus.OffersOppenedRejected);
            }
            else if ((roleName == RoleNames.OffersCheckManager || roleName == RoleNames.OffersCheckSecretary) && (listType == "Check" || listType == "Award"))
            {
                if (listType.Equals("Check"))
                {
                    tenderStatusIds.Add((int)Enums.TenderStatus.OffersOppenedConfirmed);
                    tenderStatusIds.Add((int)Enums.TenderStatus.OffersCheckedPending);
                    tenderStatusIds.Add((int)Enums.TenderStatus.OffersCheckedConfirmed);
                    tenderStatusIds.Add((int)Enums.TenderStatus.OffersCheckedRejected);
                }
                else if (listType.Equals("Award"))
                {
                    tenderStatusIds.Add((int)Enums.TenderStatus.OffersCheckedConfirmed);
                    tenderStatusIds.Add((int)Enums.TenderStatus.OffersAwarding);
                    tenderStatusIds.Add((int)Enums.TenderStatus.OffersAwardedPending);
                    tenderStatusIds.Add((int)Enums.TenderStatus.OffersAwardedConfirmed);
                    tenderStatusIds.Add((int)Enums.TenderStatus.OffersAwardedRejected);
                }
                else
                    return null;
            }
            return tenderStatusIds;
        }

        [Fact]
        public async Task GetAllAgenciesAsync_ReturnDate()
        {
            // arranges

            // act
            var response = await _client.GetAsync($"/Tender/GetAllAgenciesAsync");
            var data = await ContentHelper.GetResponseContent<List<GovAgencyModel>>(response);
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(data);
            Assert.True(data.Count > 0);
        }

        [Fact]
        public async Task TaskGetStatusAsync_ReturnDate()
        {
            // arranges

            // act
            var response = await _client.GetAsync($"/Tender/GetAllBranchesByAgencyCode");
            var data = await ContentHelper.GetResponseContent<List<LookupModel>>(response);
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(data);
            Assert.True(data.Count > 0);
        }

        [Fact]
        public async Task GetStatusAsync_ReturnDate()
        {
            // arranges

            // act
            var response = await _client.GetAsync($"/Tender/GetStatusAsync");
            var data = await ContentHelper.GetResponseContent<List<LookupModel>>(response);
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(data);
            Assert.True(data.Count > 0);
        }

        [Fact]
        public async Task GetApprovedStatuses_ReturnDate()
        {
            // arranges

            // act
            var response = await _client.GetAsync($"/Tender/GetApprovedStatuses");
            var data = await ContentHelper.GetResponseContent<List<LookupModel>>(response);
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(data);
            Assert.True(data.Count > 0);
        }

        [Fact]
        public async Task GetAreasAsync_ReturnDate()
        {
            // arranges

            // act
            var response = await _client.GetAsync($"/Tender/GetAreasAsync");
            var data = await ContentHelper.GetResponseContent<List<LookupModel>>(response);
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(data);
            Assert.True(data.Count > 0);
        }

        [Fact]
        public async Task GetMainTenderActivitiesAsync_ReturnDate()
        {
            // arranges

            // act
            var response = await _client.GetAsync($"/Tender/GetMainTenderActivitiesAsync");
            var data = await ContentHelper.GetResponseContent<List<ActivityModel>>(response);
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(data);
            Assert.True(data.Count > 0);
        }

        [Fact]
        public async Task GetAllDataEntryUsersAsync_ReturnDate()
        {
            // arranges

            // act
            var response = await _client.GetAsync($"/Tender/GetAllDataEntryUsersAsync");
            var data = await ContentHelper.GetResponseContent<List<UserLookupModel>>(response);
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(data);
            Assert.True(data.Count > 0);
        }

        [Fact]
        public async Task GetAllAuditorUsersAsync_ReturnDate()
        {
            // arranges

            // act
            var response = await _client.GetAsync($"/Tender/GetAllAuditorUsersAsync");
            var data = await ContentHelper.GetResponseContent<List<UserLookupModel>>(response);
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(data);
            Assert.True(data.Count > 0);
        }

        [Fact]
        public async Task GetCountriesync_ReturnDate()
        {
            // arranges

            // act
            var response = await _client.GetAsync($"/Tender/GetCountriesync");
            var data = await ContentHelper.GetResponseContent<List<CountryModel>>(response);
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(data);
            Assert.True(data.Count > 0);
        }

        [Fact]
        public async Task GetActivitiesAsync_ReturnDate()
        {
            // arranges

            // act
            var response = await _client.GetAsync($"/Tender/GetActivitiesAsync");
            var data = await ContentHelper.GetResponseContent<List<SelectListItem>>(response);
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(data);
            Assert.True(data.Count > 0);
        }

        [Fact]
        public async Task GetAllSpendingCategories_ReturnDate()
        {
            // arranges

            // act
            var response = await _client.GetAsync($"/Tender/GetAllSpendingCategories");
            var data = await ContentHelper.GetResponseContent<List<SelectListItem>>(response);
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(data);
            Assert.True(data.Count > 0);
        }

        [Fact]
        public async Task GetMainActivitiesAsync_ReturnDate()
        {
            // arranges

            // act
            var response = await _client.GetAsync($"/Tender/GetMainActivitiesAsync");
            var data = await ContentHelper.GetResponseContent<List<SelectListItem>>(response);
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(data);
            Assert.True(data.Count > 0);
        }

        [Fact]
        public async Task BasicTenderData_Returns_ViewResult()
        {
            // arranges
            string _id = null;
            int? _isCommittee = null;
            string _tenderTypeIdString = null;
            string _announcementIdString = null;
            // act
            var response = await _client.GetAsync($"/Tender/BasicTenderData?id=" + _id + "&IsCommittee=" + _isCommittee + "&tenderTypeIdString=" + _tenderTypeIdString + "&announcementIdString=" + _announcementIdString);
            var responseContent = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(responseContent);
            Assert.Contains("!DOCTYPE html", responseContent);

        }

        [Fact]
        public async Task TenderNegotiation_Returns_ViewResult()
        {
            // arranges
            // act
            var response = await _client.GetAsync($"/Tender/TenderNegotiation");
            var responseContent = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(responseContent);
            Assert.Contains("!DOCTYPE html", responseContent);

        }

        [Fact]
        public async Task BasicTenderData_ReturnsViewWhenThrowsException()
        {
            // arranges
            string _id = null;
            int? _isCommittee = null;
            string _tenderTypeIdString = "50";
            string _announcementIdString = null;
            // act
            var response = await _client.GetAsync($"/Tender/BasicTenderData?id=" + _id + "&IsCommittee=" + _isCommittee + "&tenderTypeIdString=" + _tenderTypeIdString + "&announcementIdString=" + _announcementIdString);
            var responseContent = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(responseContent);
            Assert.Contains("!DOCTYPE html", responseContent);
        }

        [Fact]
        public async Task CheckForCrNumberExistAsync_ReturnsJson()
        {
            // arranges
            string _cr = "1010000154";
            // act
            var response = await _client.GetAsync($"/Tender/CheckForCrNumberExistAsync?crNumber=" + _cr);
            var responseContent = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.NotNull(response);
            Assert.NotNull(responseContent);
        }

        [Fact]
        public async Task RoundBiddingPagingAsync_ReturnsJson()
        {
            // arranges
            string _cr = "1010000154";
            // act
            var response = await _client.GetAsync($"/Tender/CheckForCrNumberExistAsync?tenderIdString=" + _tenderIdString);
            var responseContent = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.NotNull(response);
            Assert.NotNull(responseContent);
        }

        [Fact]
        public async Task VROTendersIndexCheckingAndOpeningStagee_Returns_ViewResult()
        {
            // arranges
            // act
            var response = await _client.GetAsync($"/Tender/VROTendersIndexCheckingAndOpeningStagee/1");
            var responseContent = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(responseContent);
        }

        [Fact]
        public async Task UpdateConditionsTemplate_Returns_ViewResult()
        {
            // arranges
            // act
            var response = await _client.GetAsync($"/Tender/UpdateConditionsTemplate/" + _tenderIdString);
            var responseContent = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(responseContent);
        }

        [Fact]
        public async Task VROTenderChecking_Returns_ViewResult()
        {
            // arranges
            // act
            var response = await _client.GetAsync($"/Tender/VROTenderChecking/" + _tenderIdString);
            var responseContent = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(responseContent);
        }

        [Fact]
        public async Task TenderBiddingViewAsyncReturns_ViewResult()
        {
            // arranges
            // act
            var response = await _client.GetAsync($"/Tender/TenderBiddingViewAsync?tenderIdString=" + _tenderIdString);
            var responseContent = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(responseContent);
        }

        [Fact]
        public async Task BasicTenderData_Returns_View()
        {
            // arrange
            HttpResponseMessage initialResponse = await _client.GetAsync($"/Tender/Index");
            var antiForgeryValue = await ExtractAntiForgeryValue(initialResponse);
            HttpRequestMessage postRequest = new HttpRequestMessage(HttpMethod.Post, $"/Tender/basictenderdata");
            postRequest.Headers.Add("Cookie", new CookieHeaderValue(AntiForgeryCookieName, antiForgeryValue.cookieValue).ToString());
            var formData = new Dictionary<string, string>
            {
               { AntiForgeryFieldName,antiForgeryValue.fieldValue},
            };

            postRequest.Content = new FormUrlEncodedContent(formData);
            // act
            HttpResponseMessage response = await _client.SendAsync(postRequest);
            var responseContent = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(responseContent);
            Assert.Contains("<!DOCTYPE html>", responseContent);
        }


        [Fact]
        public async Task ReopenDirectPurchaseTenderOffersTechnicalCheckingAsync_Returns_Ok()
        {
            // arrange
            HttpResponseMessage initialResponse = await _client.GetAsync($"/Tender/Index");
            var antiForgeryValue = await ExtractAntiForgeryValue(initialResponse);
            HttpRequestMessage postRequest = new HttpRequestMessage(HttpMethod.Post, $"/Tender/ReopenDirectPurchaseTenderOffersTechnicalCheckingAsync");
            postRequest.Headers.Add("Cookie", new CookieHeaderValue(AntiForgeryCookieName, antiForgeryValue.cookieValue).ToString());
            var formData = new Dictionary<string, string>
            {
               { AntiForgeryFieldName,antiForgeryValue.fieldValue},
               {"tenderIdString",Util.Encrypt(20) }
            };

            postRequest.Content = new FormUrlEncodedContent(formData);
            // act
            HttpResponseMessage response = await _client.SendAsync(postRequest);
            var responseContent = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response);
            Assert.NotNull(responseContent);
        }

        [Fact]
        public async Task SendDirectPurchaseTenderOffersFinanceCheckingToApproveAsync_Returns_Ok()
        {
            // arrange
            HttpResponseMessage initialResponse = await _client.GetAsync($"/Tender/Index");
            var antiForgeryValue = await ExtractAntiForgeryValue(initialResponse);
            HttpRequestMessage postRequest = new HttpRequestMessage(HttpMethod.Post, $"/Tender/SendDirectPurchaseTenderOffersFinanceCheckingToApproveAsync");
            postRequest.Headers.Add("Cookie", new CookieHeaderValue(AntiForgeryCookieName, antiForgeryValue.cookieValue).ToString());
            var formData = new Dictionary<string, string>
            {
               { AntiForgeryFieldName,antiForgeryValue.fieldValue},
               {"tenderIdString",Util.Encrypt(20) }
            };

            postRequest.Content = new FormUrlEncodedContent(formData);
            // act
            HttpResponseMessage response = await _client.SendAsync(postRequest);
            var responseContent = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response);
            Assert.NotNull(responseContent);
        }

        [Fact]
        public async Task CheckDirectPurchaseOffers_Returns_View()
        {
            // arrange
            HttpResponseMessage initialResponse = await _client.GetAsync($"/Tender/Index");
            var antiForgeryValue = await ExtractAntiForgeryValue(initialResponse);
            HttpRequestMessage postRequest = new HttpRequestMessage(HttpMethod.Post, $"/Tender/CheckDirectPurchaseOffers");
            postRequest.Headers.Add("Cookie", new CookieHeaderValue(AntiForgeryCookieName, antiForgeryValue.cookieValue).ToString());
            var formData = new Dictionary<string, string>
            {
               { AntiForgeryFieldName,antiForgeryValue.fieldValue},
               {"tenderIdString",Util.Encrypt(11) }
            };

            postRequest.Content = new FormUrlEncodedContent(formData);
            // act
            HttpResponseMessage response = await _client.SendAsync(postRequest);
            var responseContent = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response);
            Assert.NotNull(responseContent);
        }

        [Fact]
        public async Task StartTenderDirectPurchaseOffersCheckingAsync_Returns_Ok()
        {
            // arrange
            HttpResponseMessage initialResponse = await _client.GetAsync($"/Tender/Index");
            var antiForgeryValue = await ExtractAntiForgeryValue(initialResponse);
            HttpRequestMessage postRequest = new HttpRequestMessage(HttpMethod.Post, $"/Tender/StartTenderDirectPurchaseOffersCheckingAsync");
            postRequest.Headers.Add("Cookie", new CookieHeaderValue(AntiForgeryCookieName, antiForgeryValue.cookieValue).ToString());
            var formData = new Dictionary<string, string>
            {
               { AntiForgeryFieldName,antiForgeryValue.fieldValue},
               {"tenderIdString",Util.Encrypt(11) }
            };

            postRequest.Content = new FormUrlEncodedContent(formData);
            // act
            HttpResponseMessage response = await _client.SendAsync(postRequest);
            var responseContent = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response);
            Assert.NotNull(responseContent);
        }

        [Fact]
        public async Task SendDirectPurchaseTenderOffersCheckingToApproveAsync_Returns_Ok()
        {
            // arrange
            HttpResponseMessage initialResponse = await _client.GetAsync($"/Tender/Index");
            var antiForgeryValue = await ExtractAntiForgeryValue(initialResponse);
            HttpRequestMessage postRequest = new HttpRequestMessage(HttpMethod.Post, $"/Tender/SendDirectPurchaseTenderOffersCheckingToApproveAsync");
            postRequest.Headers.Add("Cookie", new CookieHeaderValue(AntiForgeryCookieName, antiForgeryValue.cookieValue).ToString());
            var formData = new Dictionary<string, string>
            {
               { AntiForgeryFieldName,antiForgeryValue.fieldValue},
               {"tenderIdString",Util.Encrypt(11) }
            };

            postRequest.Content = new FormUrlEncodedContent(formData);
            // act
            HttpResponseMessage response = await _client.SendAsync(postRequest);
            var responseContent = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response);
            Assert.NotNull(responseContent);
        }

        [Fact]
        public async Task ReopenDirectPurchaseTenderOffersCheckingAsync_Returns_Ok()
        {
            // arrange
            HttpResponseMessage initialResponse = await _client.GetAsync($"/Tender/Index");
            var antiForgeryValue = await ExtractAntiForgeryValue(initialResponse);
            HttpRequestMessage postRequest = new HttpRequestMessage(HttpMethod.Post, $"/Tender/ReopenDirectPurchaseTenderOffersCheckingAsync");
            postRequest.Headers.Add("Cookie", new CookieHeaderValue(AntiForgeryCookieName, antiForgeryValue.cookieValue).ToString());
            var formData = new Dictionary<string, string>
            {
               { AntiForgeryFieldName,antiForgeryValue.fieldValue},
               {"tenderIdString",Util.Encrypt(11) }
            };

            postRequest.Content = new FormUrlEncodedContent(formData);
            // act
            HttpResponseMessage response = await _client.SendAsync(postRequest);
            var responseContent = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response);
            Assert.NotNull(responseContent);
        }

        [Fact]
        public async Task SendDirectPurchaseTenderOffersTechnicalCheckingToApproveAsync_Returns_Ok()
        {
            // arrange
            HttpResponseMessage initialResponse = await _client.GetAsync($"/Tender/Index");
            var antiForgeryValue = await ExtractAntiForgeryValue(initialResponse);
            HttpRequestMessage postRequest = new HttpRequestMessage(HttpMethod.Post, $"/Tender/SendDirectPurchaseTenderOffersTechnicalCheckingToApproveAsync");
            postRequest.Headers.Add("Cookie", new CookieHeaderValue(AntiForgeryCookieName, antiForgeryValue.cookieValue).ToString());
            var formData = new Dictionary<string, string>
            {
               { AntiForgeryFieldName,antiForgeryValue.fieldValue},
               {"tenderIdString",Util.Encrypt(16) }
            };

            postRequest.Content = new FormUrlEncodedContent(formData);
            // act
            HttpResponseMessage response = await _client.SendAsync(postRequest);
            var responseContent = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response);
            Assert.NotNull(responseContent);
        }

        [Fact]
        public async Task ApproveDirectPurchaseTenderOffersTechnicalCheckingAsync_Returns_Ok()
        {
            // arrange
            HttpResponseMessage initialResponse = await _client.GetAsync($"/Tender/Index");
            var antiForgeryValue = await ExtractAntiForgeryValue(initialResponse);
            HttpRequestMessage postRequest = new HttpRequestMessage(HttpMethod.Post, $"/Tender/ApproveDirectPurchaseTenderOffersTechnicalCheckingAsync");
            postRequest.Headers.Add("Cookie", new CookieHeaderValue(AntiForgeryCookieName, antiForgeryValue.cookieValue).ToString());
            var formData = new Dictionary<string, string>
            {
               { AntiForgeryFieldName,antiForgeryValue.fieldValue},
               {"tenderIdString",Util.Encrypt(16) },
               {"verificationCode","1760"}
            };

            postRequest.Content = new FormUrlEncodedContent(formData);
            // act
            HttpResponseMessage response = await _client.SendAsync(postRequest);
            var responseContent = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response);
            Assert.NotNull(responseContent);
        }

        [Fact]
        public async Task ApproveDirectPurchaseTenderOffersFinanceCheckingAsync_Returns_Ok()
        {
            // arrange
            HttpResponseMessage initialResponse = await _client.GetAsync($"/Tender/Index");
            var antiForgeryValue = await ExtractAntiForgeryValue(initialResponse);
            HttpRequestMessage postRequest = new HttpRequestMessage(HttpMethod.Post, $"/Tender/ApproveDirectPurchaseTenderOffersFinanceCheckingAsync");
            postRequest.Headers.Add("Cookie", new CookieHeaderValue(AntiForgeryCookieName, antiForgeryValue.cookieValue).ToString());
            var formData = new Dictionary<string, string>
            {
               { AntiForgeryFieldName,antiForgeryValue.fieldValue},
               {"tenderIdString",Util.Encrypt(16) },
               {"verificationCode","1760"}
            };

            postRequest.Content = new FormUrlEncodedContent(formData);
            // act
            HttpResponseMessage response = await _client.SendAsync(postRequest);
            var responseContent = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response);
            Assert.NotNull(responseContent);
        }

        [Fact]
        public async Task ReopenDirectPurchaseTenderOffersFinancialCheckingAsync_Returns_Ok()
        {
            // arrange
            HttpResponseMessage initialResponse = await _client.GetAsync($"/Tender/Index");
            var antiForgeryValue = await ExtractAntiForgeryValue(initialResponse);
            HttpRequestMessage postRequest = new HttpRequestMessage(HttpMethod.Post, $"/Tender/ReopenDirectPurchaseTenderOffersFinancialCheckingAsync");
            postRequest.Headers.Add("Cookie", new CookieHeaderValue(AntiForgeryCookieName, antiForgeryValue.cookieValue).ToString());
            var formData = new Dictionary<string, string>
            {
               { AntiForgeryFieldName,antiForgeryValue.fieldValue},
               {"tenderIdString",Util.Encrypt(16) }
            };

            postRequest.Content = new FormUrlEncodedContent(formData);
            // act
            HttpResponseMessage response = await _client.SendAsync(postRequest);
            var responseContent = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response);
            Assert.NotNull(responseContent);
        }

        [Fact]
        public async Task ReopenVROTenderOffersFinancialCheckingAsync_Returns_Ok()
        {
            // arrange
            HttpResponseMessage initialResponse = await _client.GetAsync($"/Tender/Index");
            var antiForgeryValue = await ExtractAntiForgeryValue(initialResponse);
            HttpRequestMessage postRequest = new HttpRequestMessage(HttpMethod.Post, $"/Tender/ReopenVROTenderOffersFinancialCheckingAsync");
            postRequest.Headers.Add("Cookie", new CookieHeaderValue(AntiForgeryCookieName, antiForgeryValue.cookieValue).ToString());
            var formData = new Dictionary<string, string>
            {
               { AntiForgeryFieldName,antiForgeryValue.fieldValue},
               {"tenderIdString",Util.Encrypt(12) }
            };

            postRequest.Content = new FormUrlEncodedContent(formData);
            // act
            HttpResponseMessage response = await _client.SendAsync(postRequest);
            var responseContent = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response);
            Assert.NotNull(responseContent);
        }

        [Fact]
        public async Task StartVROTenderOffersFinancialCheckingAsync_Returns_Ok()
        {
            // arrange
            HttpResponseMessage initialResponse = await _client.GetAsync($"/Tender/Index");
            var antiForgeryValue = await ExtractAntiForgeryValue(initialResponse);
            HttpRequestMessage postRequest = new HttpRequestMessage(HttpMethod.Post, $"/Tender/StartVROTenderOffersFinancialCheckingAsync");
            postRequest.Headers.Add("Cookie", new CookieHeaderValue(AntiForgeryCookieName, antiForgeryValue.cookieValue).ToString());
            var formData = new Dictionary<string, string>
            {
               { AntiForgeryFieldName,antiForgeryValue.fieldValue},
               {"tenderIdString",Util.Encrypt(12) }
            };

            postRequest.Content = new FormUrlEncodedContent(formData);
            // act
            HttpResponseMessage response = await _client.SendAsync(postRequest);
            var responseContent = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response);
            Assert.NotNull(responseContent);
        }

        [Fact]
        public async Task StartVROTenderOffersCheckingAsync_Returns_Ok()
        {
            // arrange
            HttpResponseMessage initialResponse = await _client.GetAsync($"/Tender/Index");
            var antiForgeryValue = await ExtractAntiForgeryValue(initialResponse);
            HttpRequestMessage postRequest = new HttpRequestMessage(HttpMethod.Post, $"/Tender/StartVROTenderOffersCheckingAsync");
            postRequest.Headers.Add("Cookie", new CookieHeaderValue(AntiForgeryCookieName, antiForgeryValue.cookieValue).ToString());
            var formData = new Dictionary<string, string>
            {
               { AntiForgeryFieldName,antiForgeryValue.fieldValue},
               {"tenderIdString",Util.Encrypt(7) }
            };

            postRequest.Content = new FormUrlEncodedContent(formData);
            // act
            HttpResponseMessage response = await _client.SendAsync(postRequest);
            var responseContent = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response);
            Assert.NotNull(responseContent);

        }
        [Fact]
        public async Task SendVROTenderOffersTechnicalCheckingToApproveAsync_Returns_Ok()
        {
            // arrange
            HttpResponseMessage initialResponse = await _client.GetAsync($"/Tender/Index");
            var antiForgeryValue = await ExtractAntiForgeryValue(initialResponse);
            HttpRequestMessage postRequest = new HttpRequestMessage(HttpMethod.Post, $"/Tender/SendVROTenderOffersTechnicalCheckingToApproveAsync");
            postRequest.Headers.Add("Cookie", new CookieHeaderValue(AntiForgeryCookieName, antiForgeryValue.cookieValue).ToString());
            var formData = new Dictionary<string, string>
            {
               { AntiForgeryFieldName,antiForgeryValue.fieldValue},
               {"tenderIdString",Util.Encrypt(6) }
            };

            postRequest.Content = new FormUrlEncodedContent(formData);
            // act
            HttpResponseMessage response = await _client.SendAsync(postRequest);
            var responseContent = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response);
            Assert.NotNull(responseContent);
        }

        [Fact]
        public async Task ReopenVROTenderOffersTechnicalCheckingAsync_Returns_Ok()
        {
            // arrange
            HttpResponseMessage initialResponse = await _client.GetAsync($"/Tender/Index");
            var antiForgeryValue = await ExtractAntiForgeryValue(initialResponse);
            HttpRequestMessage postRequest = new HttpRequestMessage(HttpMethod.Post, $"/Tender/ReopenVROTenderOffersTechnicalCheckingAsync");
            postRequest.Headers.Add("Cookie", new CookieHeaderValue(AntiForgeryCookieName, antiForgeryValue.cookieValue).ToString());
            var formData = new Dictionary<string, string>
            {
               { AntiForgeryFieldName,antiForgeryValue.fieldValue},
               {"tenderIdString",Util.Encrypt(6) }
            };

            postRequest.Content = new FormUrlEncodedContent(formData);
            // act
            HttpResponseMessage response = await _client.SendAsync(postRequest);
            var responseContent = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response);
            Assert.NotNull(responseContent);
        }

        [Fact]
        public async Task ApproveVROTenderOffersTechnicalCheckingAsync_Returns_Ok()
        {
            // arrange
            HttpResponseMessage initialResponse = await _client.GetAsync($"/Tender/Index");
            var antiForgeryValue = await ExtractAntiForgeryValue(initialResponse);
            HttpRequestMessage postRequest = new HttpRequestMessage(HttpMethod.Post, $"/Tender/ApproveVROTenderOffersTechnicalCheckingAsync");
            postRequest.Headers.Add("Cookie", new CookieHeaderValue(AntiForgeryCookieName, antiForgeryValue.cookieValue).ToString());
            var formData = new Dictionary<string, string>
            {
               { AntiForgeryFieldName,antiForgeryValue.fieldValue},
               {"tenderIdString",Util.Encrypt(14) },
               {"verificationCode","5435" }
            };

            postRequest.Content = new FormUrlEncodedContent(formData);
            // act
            HttpResponseMessage response = await _client.SendAsync(postRequest);
            var responseContent = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response);
            Assert.NotNull(responseContent);
        }

        [Fact]
        public async Task ReviewTenderByUnitManagerAsync_Returns_Ok()
        {
            // arrange
            HttpResponseMessage initialResponse = await _client.GetAsync($"/Tender/Index");
            var antiForgeryValue = await ExtractAntiForgeryValue(initialResponse);
            HttpRequestMessage postRequest = new HttpRequestMessage(HttpMethod.Post, $"/Tender/ReviewTenderByUnitManagerAsync");
            postRequest.Headers.Add("Cookie", new CookieHeaderValue(AntiForgeryCookieName, antiForgeryValue.cookieValue).ToString());
            var formData = new Dictionary<string, string>
            {
               { AntiForgeryFieldName,antiForgeryValue.fieldValue},
               {"tenderIdString",Util.Encrypt(12) }
            };

            postRequest.Content = new FormUrlEncodedContent(formData);
            // act
            HttpResponseMessage response = await _client.SendAsync(postRequest);
            var responseContent = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response);
            Assert.NotNull(responseContent);
        }

        [Fact]
        public async Task ApproveTenderByUnitManagerAsync_Returns_Ok()
        {
            // arrange
            HttpResponseMessage initialResponse = await _client.GetAsync($"/Tender/Index");
            var antiForgeryValue = await ExtractAntiForgeryValue(initialResponse);
            HttpRequestMessage postRequest = new HttpRequestMessage(HttpMethod.Post, $"/Tender/ApproveTenderByUnitManagerAsync");
            postRequest.Headers.Add("Cookie", new CookieHeaderValue(AntiForgeryCookieName, antiForgeryValue.cookieValue).ToString());
            var formData = new Dictionary<string, string>
            {
               { AntiForgeryFieldName,antiForgeryValue.fieldValue},
               {"tenderIdString",Util.Encrypt(12) },
               { "verificationCode","7080"}
            };

            postRequest.Content = new FormUrlEncodedContent(formData);
            // act
            HttpResponseMessage response = await _client.SendAsync(postRequest);
            var responseContent = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response);
            Assert.NotNull(responseContent);
        }

        [Fact]
        public async Task ReOpenTenderByUnitSecertaryLevelAsync_Returns_Ok()
        {
            // arrange
            HttpResponseMessage initialResponse = await _client.GetAsync($"/Tender/Index");
            var antiForgeryValue = await ExtractAntiForgeryValue(initialResponse);
            HttpRequestMessage postRequest = new HttpRequestMessage(HttpMethod.Post, $"/Tender/ReOpenTenderByUnitSecertaryLevelAsync");
            postRequest.Headers.Add("Cookie", new CookieHeaderValue(AntiForgeryCookieName, antiForgeryValue.cookieValue).ToString());
            var formData = new Dictionary<string, string>
            {
               { AntiForgeryFieldName,antiForgeryValue.fieldValue},
               {"tenderIdString",Util.Encrypt(12) }
            };

            postRequest.Content = new FormUrlEncodedContent(formData);
            // act
            HttpResponseMessage response = await _client.SendAsync(postRequest);
            var responseContent = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response);
            Assert.NotNull(responseContent);
        }

        [Fact]
        public async Task ApproveTenderByUnitSecretaryLevelTwoAsync_Returns_Ok()
        {
            // arrange
            HttpResponseMessage initialResponse = await _client.GetAsync($"/Tender/Index");
            var antiForgeryValue = await ExtractAntiForgeryValue(initialResponse);
            HttpRequestMessage postRequest = new HttpRequestMessage(HttpMethod.Post, $"/Tender/ApproveTenderByUnitSecretaryLevelTwoAsync");
            postRequest.Headers.Add("Cookie", new CookieHeaderValue(AntiForgeryCookieName, antiForgeryValue.cookieValue).ToString());
            var formData = new Dictionary<string, string>
            {
               { AntiForgeryFieldName,antiForgeryValue.fieldValue},
               {"tenderIdString",Util.Encrypt(12) }
            };

            postRequest.Content = new FormUrlEncodedContent(formData);
            // act
            HttpResponseMessage response = await _client.SendAsync(postRequest);
            var responseContent = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response);
            Assert.NotNull(responseContent);
        }

        [Fact]
        public async Task SendToApproveFromUnitSecretaryToUnitMangerAsync_Returns_Ok()
        {
            // arrange
            HttpResponseMessage initialResponse = await _client.GetAsync($"/Tender/Index");
            var antiForgeryValue = await ExtractAntiForgeryValue(initialResponse);
            HttpRequestMessage postRequest = new HttpRequestMessage(HttpMethod.Post, $"/Tender/SendToApproveFromUnitSecretaryToUnitMangerAsync");
            postRequest.Headers.Add("Cookie", new CookieHeaderValue(AntiForgeryCookieName, antiForgeryValue.cookieValue).ToString());
            var formData = new Dictionary<string, string>
            {
               { AntiForgeryFieldName,antiForgeryValue.fieldValue},
               {"tenderIdString",Util.Encrypt(12) }
            };

            postRequest.Content = new FormUrlEncodedContent(formData);
            // act
            HttpResponseMessage response = await _client.SendAsync(postRequest);
            var responseContent = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response);
            Assert.NotNull(responseContent);
        }

        [Fact]
        public async Task ReOpenTenderByUnitSecertaryAsync_Returns_Ok()
        {
            // arrange
            HttpResponseMessage initialResponse = await _client.GetAsync($"/Tender/Index");
            var antiForgeryValue = await ExtractAntiForgeryValue(initialResponse);
            HttpRequestMessage postRequest = new HttpRequestMessage(HttpMethod.Post, $"/Tender/ReOpenTenderByUnitSecertaryAsync");
            postRequest.Headers.Add("Cookie", new CookieHeaderValue(AntiForgeryCookieName, antiForgeryValue.cookieValue).ToString());
            var formData = new Dictionary<string, string>
            {
               { AntiForgeryFieldName,antiForgeryValue.fieldValue},
               {"tenderIdString",Util.Encrypt(12) }
            };

            postRequest.Content = new FormUrlEncodedContent(formData);
            // act
            HttpResponseMessage response = await _client.SendAsync(postRequest);
            var responseContent = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response);
            Assert.NotNull(responseContent);
        }

        [Fact]
        public async Task OpenTenderByUnitSecretaryLevelTwoAsync_Returns_Ok()
        {
            // arrange
            HttpResponseMessage initialResponse = await _client.GetAsync($"/Tender/Index");
            var antiForgeryValue = await ExtractAntiForgeryValue(initialResponse);
            HttpRequestMessage postRequest = new HttpRequestMessage(HttpMethod.Post, $"/Tender/OpenTenderByUnitSecretaryLevelTwoAsync");
            postRequest.Headers.Add("Cookie", new CookieHeaderValue(AntiForgeryCookieName, antiForgeryValue.cookieValue).ToString());
            var formData = new Dictionary<string, string>
            {
               { AntiForgeryFieldName,antiForgeryValue.fieldValue},
               {"tenderIdString",Util.Encrypt(12) }
            };

            postRequest.Content = new FormUrlEncodedContent(formData);
            // act
            HttpResponseMessage response = await _client.SendAsync(postRequest);
            var responseContent = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response);
            Assert.NotNull(responseContent);
        }

        [Fact]
        public async Task SendToApproveFromUnitSecretaryLevelTwoToUnitMangerAsync_Returns_Ok()
        {
            // arrange
            HttpResponseMessage initialResponse = await _client.GetAsync($"/Tender/Index");
            var antiForgeryValue = await ExtractAntiForgeryValue(initialResponse);
            HttpRequestMessage postRequest = new HttpRequestMessage(HttpMethod.Post, $"/Tender/SendToApproveFromUnitSecretaryLevelTwoToUnitMangerAsync");
            postRequest.Headers.Add("Cookie", new CookieHeaderValue(AntiForgeryCookieName, antiForgeryValue.cookieValue).ToString());
            var formData = new Dictionary<string, string>
            {
               { AntiForgeryFieldName,antiForgeryValue.fieldValue},
               {"tenderIdString",Util.Encrypt(12) }
            };

            postRequest.Content = new FormUrlEncodedContent(formData);
            // act
            HttpResponseMessage response = await _client.SendAsync(postRequest);
            var responseContent = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response);
            Assert.NotNull(responseContent);
        }

        [Fact]
        public async Task RejectTenderByUnitSecretaryLevelTwoAsync_Returns_Ok()
        {
            // arrange
            HttpResponseMessage initialResponse = await _client.GetAsync($"/Tender/Index");
            var antiForgeryValue = await ExtractAntiForgeryValue(initialResponse);
            HttpRequestMessage postRequest = new HttpRequestMessage(HttpMethod.Post, $"/Tender/RejectTenderByUnitSecretaryLevelTwoAsync");
            postRequest.Headers.Add("Cookie", new CookieHeaderValue(AntiForgeryCookieName, antiForgeryValue.cookieValue).ToString());
            var formData = new Dictionary<string, string>
            {
               { AntiForgeryFieldName,antiForgeryValue.fieldValue},
               {"tenderIdString",Util.Encrypt(12) },
               { "comment","like"}
            };

            postRequest.Content = new FormUrlEncodedContent(formData);
            // act
            HttpResponseMessage response = await _client.SendAsync(postRequest);
            var responseContent = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response);
            Assert.NotNull(responseContent);
        }

        [Fact]
        public async Task ApproveTenderByUnitSecretaryAsync_Returns_Ok()
        {
            // arrange
            HttpResponseMessage initialResponse = await _client.GetAsync($"/Tender/Index");
            var antiForgeryValue = await ExtractAntiForgeryValue(initialResponse);
            HttpRequestMessage postRequest = new HttpRequestMessage(HttpMethod.Post, $"/Tender/ApproveTenderByUnitSecretaryAsync");
            postRequest.Headers.Add("Cookie", new CookieHeaderValue(AntiForgeryCookieName, antiForgeryValue.cookieValue).ToString());
            var formData = new Dictionary<string, string>
            {
               { AntiForgeryFieldName,antiForgeryValue.fieldValue},
               {"tenderIdString",Util.Encrypt(12) },
               { "IWouldLikeToAttendTheommitte","false"}
            };

            postRequest.Content = new FormUrlEncodedContent(formData);
            // act
            HttpResponseMessage response = await _client.SendAsync(postRequest);
            var responseContent = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response);
            Assert.NotNull(responseContent);
        }

        [Fact]
        public async Task TenderDetailsForUnitSecretary_Returns_Ok()
        {
            // arrange
            HttpResponseMessage initialResponse = await _client.GetAsync($"/Tender/Index");
            var antiForgeryValue = await ExtractAntiForgeryValue(initialResponse);
            HttpRequestMessage postRequest = new HttpRequestMessage(HttpMethod.Post, $"/Tender/TenderDetailsForUnitSecretary");
            postRequest.Headers.Add("Cookie", new CookieHeaderValue(AntiForgeryCookieName, antiForgeryValue.cookieValue).ToString());
            var formData = new Dictionary<string, string>
            {
               { AntiForgeryFieldName,antiForgeryValue.fieldValue},
               {"tenderIdString",Util.Encrypt(12) }
            };

            postRequest.Content = new FormUrlEncodedContent(formData);
            // act
            HttpResponseMessage response = await _client.SendAsync(postRequest);
            var responseContent = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response);
            Assert.NotNull(responseContent);
        }

        [Fact]
        public async Task OpenTenderByUnitSecretaryAsync_Returns_Ok()
        {
            // arrange
            HttpResponseMessage initialResponse = await _client.GetAsync($"/Tender/Index");
            var antiForgeryValue = await ExtractAntiForgeryValue(initialResponse);
            HttpRequestMessage postRequest = new HttpRequestMessage(HttpMethod.Post, $"/Tender/OpenTenderByUnitSecretaryAsync");
            postRequest.Headers.Add("Cookie", new CookieHeaderValue(AntiForgeryCookieName, antiForgeryValue.cookieValue).ToString());
            var formData = new Dictionary<string, string>
            {
               { AntiForgeryFieldName,antiForgeryValue.fieldValue},
               {"tenderIdString",Util.Encrypt(12) }
            };

            postRequest.Content = new FormUrlEncodedContent(formData);
            // act
            HttpResponseMessage response = await _client.SendAsync(postRequest);
            var responseContent = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response);
            Assert.NotNull(responseContent);
        }

        [Fact]
        public async Task RejectTenderByUnitSecretaryAsync_Returns_Ok()
        {
            // arrange
            HttpResponseMessage initialResponse = await _client.GetAsync($"/Tender/Index");
            var antiForgeryValue = await ExtractAntiForgeryValue(initialResponse);
            HttpRequestMessage postRequest = new HttpRequestMessage(HttpMethod.Post, $"/Tender/RejectTenderByUnitSecretaryAsync");
            postRequest.Headers.Add("Cookie", new CookieHeaderValue(AntiForgeryCookieName, antiForgeryValue.cookieValue).ToString());
            var formData = new Dictionary<string, string>
            {
               { AntiForgeryFieldName,antiForgeryValue.fieldValue},
               {"tenderIdString",Util.Encrypt(12) },
               { "comment","like"}
            };

            postRequest.Content = new FormUrlEncodedContent(formData);
            // act
            HttpResponseMessage response = await _client.SendAsync(postRequest);
            var responseContent = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response);
            Assert.NotNull(responseContent);
        }

        [Fact]
        public async Task RejectTenderByUnitManagerAsync_Returns_Ok()
        {
            // arrange
            HttpResponseMessage initialResponse = await _client.GetAsync($"/Tender/Index");
            var antiForgeryValue = await ExtractAntiForgeryValue(initialResponse);
            HttpRequestMessage postRequest = new HttpRequestMessage(HttpMethod.Post, $"/Tender/RejectTenderByUnitManagerAsync");
            postRequest.Headers.Add("Cookie", new CookieHeaderValue(AntiForgeryCookieName, antiForgeryValue.cookieValue).ToString());
            var formData = new Dictionary<string, string>
            {
               { AntiForgeryFieldName,antiForgeryValue.fieldValue},
               {"tenderIdString",Util.Encrypt(12) },
               { "comment","like"}
            };

            postRequest.Content = new FormUrlEncodedContent(formData);
            // act
            HttpResponseMessage response = await _client.SendAsync(postRequest);
            var responseContent = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response);
            Assert.NotNull(responseContent);
        }

        [Fact]
        public async Task SendVROTenderOffersFinanceCheckingToApproveAsync_Returns_Ok()
        {
            // arrange
            HttpResponseMessage initialResponse = await _client.GetAsync($"/Tender/Index");
            var antiForgeryValue = await ExtractAntiForgeryValue(initialResponse);
            HttpRequestMessage postRequest = new HttpRequestMessage(HttpMethod.Post, $"/Tender/SendVROTenderOffersFinanceCheckingToApproveAsync");
            postRequest.Headers.Add("Cookie", new CookieHeaderValue(AntiForgeryCookieName, antiForgeryValue.cookieValue).ToString());
            var formData = new Dictionary<string, string>
            {
               { AntiForgeryFieldName,antiForgeryValue.fieldValue},
               {"tenderIdString",Util.Encrypt(12) }
            };

            postRequest.Content = new FormUrlEncodedContent(formData);
            // act
            HttpResponseMessage response = await _client.SendAsync(postRequest);
            var responseContent = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response);
            Assert.NotNull(responseContent);
        }

        [Fact]
        public async Task ApproveVROTenderOffersFinanceCheckingAsync_Returns_View()
        {
            // arrange
            HttpResponseMessage initialResponse = await _client.GetAsync($"/Tender/Index");
            var antiForgeryValue = await ExtractAntiForgeryValue(initialResponse);
            HttpRequestMessage postRequest = new HttpRequestMessage(HttpMethod.Post, $"/Tender/ApproveVROTenderOffersFinanceCheckingAsync");
            postRequest.Headers.Add("Cookie", new CookieHeaderValue(AntiForgeryCookieName, antiForgeryValue.cookieValue).ToString());
            var formData = new Dictionary<string, string>
            {
               { AntiForgeryFieldName,antiForgeryValue.fieldValue},
               {"tenderIdString",Util.Encrypt(12) },
               {"verificationCode","73511"}
            };

            postRequest.Content = new FormUrlEncodedContent(formData);
            // act
            HttpResponseMessage response = await _client.SendAsync(postRequest);
            var responseContent = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.NotNull(response);
            Assert.NotNull(responseContent);
        }

        [Fact]
        public async Task TenderIndexAwardingStageForDirectPurchase_Returns_ViewResult()
        {
            // arranges
            // act
            var response = await _client.GetAsync($"/Tender/TenderIndexAwardingStageForDirectPurchase");
            var responseContent = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(responseContent);
            Assert.Contains("<!DOCTYPE html>", responseContent);
        }

        [Fact]
        public async Task TenderDates_Returns_ViewResult()
        {
            // arranges
            // act
            var response = await _client.GetAsync($"/Tender/TenderDates?tenderId=" + _tenderIdString);
            var responseContent = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(responseContent);
            Assert.Contains("<!DOCTYPE html>", responseContent);
        }

        [Fact]
        public async Task RelationsStep_Returns_ViewResult()
        {
            // arranges
            // act
            var response = await _client.GetAsync($"/Tender/RelationsStep?tenderId=" + _tenderIdString);
            var responseContent = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(responseContent);
            Assert.Contains("<!DOCTYPE html>", responseContent);
        }

        [Fact]
        public async Task TenderDates_Returns_Redirect()
        {

            // arrange
            HttpResponseMessage initialResponse = await _client.GetAsync($"/Tender/Index");
            var antiForgeryValue = await ExtractAntiForgeryValue(initialResponse);
            HttpRequestMessage postRequest = new HttpRequestMessage(HttpMethod.Post, $"/Tender/TenderDates");
            postRequest.Headers.Add("Cookie", new CookieHeaderValue(AntiForgeryCookieName, antiForgeryValue.cookieValue).ToString());

            var formData = new Dictionary<string, string>
                {
                    { AntiForgeryFieldName,antiForgeryValue.fieldValue},
                    { "LastEnqueriesDate",System.DateTime.Now.AddDays(30).ToString()},
                    { "LastOfferPresentationDate",System.DateTime.Now.AddDays(30).ToString()},
                    { "LastOfferPresentationTime","10:40 PM"},
                    { "NeedInitialGuarantee","true"},
                    { "OffersCheckingTime","11:40 PM"},
                    { "InitialGuaranteePercentage","12"},
                    { "InitialGuaranteeAddress","Guarantee Address"},
                    { "OffersOpeningAddressId","2"},
                    { "TenderTypeId","1"},
                    { "TenderIdString",_tenderIdString},
                    { "TenderStatusId","1"},
                    { "SupplierNeedSample","false"},
                };

            postRequest.Content = new FormUrlEncodedContent(formData);
            // act
            HttpResponseMessage response = await _client.SendAsync(postRequest);
            var responseContent = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(responseContent);
            Assert.Contains("!DOCTYPE html", responseContent);
        }

        [Fact]
        public async Task ExportTenderTableQuantityItemsAsync_Returns_File()
        {
            // arranges

            // act
            var response = await _client.GetAsync($"/Tender/ExportTenderTableQuantityItemsAsync?tableId=1&isNewChange=true");
            var responseContent = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.NotNull(response);
            Assert.NotNull(responseContent);
        }

        [Fact]
        public async Task AddNewTableChanges_Returns_Content()
        {
            // arranges

            // act
            var response = await _client.GetAsync($"/Tender/AddNewTableChanges?formid=1&tenderId=1&templateYears=2&tableName=tableName");
            var responseContent = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response);
            Assert.NotNull(responseContent);
        }

        [Fact]
        public async Task ConditionsTemplateHtml_Returns_ViewResult()
        {
            // arranges
            // act
            var response = await _client.GetAsync($"/Tender/ConditionsTemplateHtml?STenderId=" + _tenderIdString);
            var responseContent = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(responseContent);
            Assert.Contains("<!DOCTYPE html>", responseContent);
        }

        [Fact]
        public async Task PrintConditionsTemplateHtml_Returns_ViewResult()
        {
            // arranges
            // act
            var response = await _client.GetAsync($"/Tender/PrintConditionsTemplateHtml?STenderId=" + _tenderIdString);
            var responseContent = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(responseContent);
            Assert.Contains("<!DOCTYPE html>", responseContent);
        }

        [Fact]
        public async Task GetTenderQuantityTableViewComponent_Returns_ViewComponent()
        {
            // arrange
            HttpResponseMessage initialResponse = await _client.GetAsync($"/Tender/Index");
            var antiForgeryValue = await ExtractAntiForgeryValue(initialResponse);
            HttpRequestMessage postRequest = new HttpRequestMessage(HttpMethod.Post, $"/Tender/GetTenderQuantityTableViewComponent");
            postRequest.Headers.Add("Cookie", new CookieHeaderValue(AntiForgeryCookieName, antiForgeryValue.cookieValue).ToString());
            var formData = new Dictionary<string, string>
            {
               { AntiForgeryFieldName,antiForgeryValue.fieldValue},
                {"tenderId",_tenderIdString},
                {"tableId","1"},
                {"formId","1"},
                {"isReadOnly","true"}
            };
            postRequest.Content = new FormUrlEncodedContent(formData);

            // act
            HttpResponseMessage response = await _client.SendAsync(postRequest);
            var responseContent = await response.Content.ReadAsStringAsync();

            // Assert

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response);
            Assert.NotNull(responseContent);
            Assert.Contains("tableContainer", responseContent);
        }

        [Fact]
        public async Task GetTenderTableQuantityItemsAsync_Returns_JsonResult()
        {
            // arranges
            var requestModel = new QuantityTableSearchCretriaModel() { TenderId = 1 };
            var queryString = requestModel.ToDictionary().ToQueryString();
            // act
            var response = await _client.GetAsync($"/Tender/GetTenderTableQuantityItemsAsync?" + queryString);
            var responseContent = ContentHelper.GetResponseContent<PaginationModel>(response);
            var data = responseContent.Result;
            // Assert
            Assert.NotNull(response);
            Assert.NotNull(responseContent);
            Assert.NotNull(data);
        }

        [Fact]
        public async Task GetTenderTableQuantityItemsChangesAsync_Returns_JsonResult()
        {
            // arranges
            var requestModel = new QuantityTableSearchCretriaModel() { TenderId = 1 };
            var queryString = requestModel.ToDictionary().ToQueryString();
            // act
            var response = await _client.GetAsync($"/Tender/GetTenderTableQuantityItemsChangesAsync?" + queryString);
            var responseContent = ContentHelper.GetResponseContent<PaginationModel>(response);
            var data = responseContent.Result;
            // Assert
            Assert.NotNull(response);
            Assert.NotNull(responseContent);
            Assert.NotNull(data);
        }

        [Fact]
        public async Task AddEditDescriptionAndConditionsTemplateAsync_Returns_JsonResult()
        {
            // arrange
            HttpResponseMessage initialResponse = await _client.GetAsync($"/Tender/Index");
            var antiForgeryValue = await ExtractAntiForgeryValue(initialResponse);
            HttpRequestMessage postRequest = new HttpRequestMessage(HttpMethod.Post, $"/Tender/AddEditDescriptionAndConditionsTemplateAsync");
            postRequest.Headers.Add("Cookie", new CookieHeaderValue(AntiForgeryCookieName, antiForgeryValue.cookieValue).ToString());
            var formData = new Dictionary<string, string>
            {
               { AntiForgeryFieldName,antiForgeryValue.fieldValue},
               { "AttachmentReference","2,4,5"},
               { "Attachments","Attachments"},
               { "BasicInformation","Basic Information"},
               { "ContractBasedOnPerformanceDetails","Contract Details"},
               { "EncryptedTenderId",Util.Encrypt(6)},
               { "EquipmentsSpecifications","Equipments Specifications"},
               { "Guarantee","G u a r a n t e e"},
               { "IntilizationAndStartWork","IntilizationAndStartWork"},
               { "InvitationTypeId","1"},
               { "IsTemplateHasOutput","true"},
               { "ListOfSections"," "},
               { "MachineGuarantee"," "},
               { "MachineMaintanance"," "},
               { "Maintanance"," "},
               { "MaterialsSpecifications"," "},
               { "OfferPresentationWayId","2"},
               { "PreQualificationId","3"},
               { "QualitySpecifications","  "},
               { "ReferenceNumber","  "},
            };

            postRequest.Content = new FormUrlEncodedContent(formData);
            // act
            HttpResponseMessage response = await _client.SendAsync(postRequest);
            var responseContent = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.NotNull(response);
            Assert.NotNull(responseContent);
        }

        [Fact]
        public async Task GetTenderQuantityTableChangesViewComponent_Returns_ViewComponent()
        {
            // arrange
            HttpResponseMessage initialResponse = await _client.GetAsync($"/Tender/Index");
            var antiForgeryValue = await ExtractAntiForgeryValue(initialResponse);
            HttpRequestMessage postRequest = new HttpRequestMessage(HttpMethod.Post, $"/Tender/GetTenderQuantityTableChangesViewComponent");
            postRequest.Headers.Add("Cookie", new CookieHeaderValue(AntiForgeryCookieName, antiForgeryValue.cookieValue).ToString());
            var formData = new Dictionary<string, string>
            {
               { AntiForgeryFieldName,antiForgeryValue.fieldValue},
                {"tenderId",_tenderIdString},
                {"tableId","1"},
                {"formId","1"},
                {"isReadOnly","true"},
                {"isNewTable","false"},
                {"isTableDeleted","false"},
            };
            postRequest.Content = new FormUrlEncodedContent(formData);

            // act
            HttpResponseMessage response = await _client.SendAsync(postRequest);
            var responseContent = await response.Content.ReadAsStringAsync();

            // Assert

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response);
            Assert.NotNull(responseContent);
            Assert.Contains("tableContainer", responseContent);
        }

        [Fact]
        public async Task EditAddedValue_Returns_ViewWithtenderTypes()
        {
            // arranges
            var _expeBuyingCost = "123.00";
            var _expInvitaionCost = "200.00";
            // act
            var response = await _client.GetAsync($"/Tender/EditAddedValue");
            var responseContent = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(responseContent);
            Assert.Contains("!DOCTYPE html", responseContent);
            Assert.Contains(_expeBuyingCost, responseContent);
            Assert.Contains(_expInvitaionCost, responseContent);
        }

        [Fact]
        public async Task EditAddedValue_Returns_View()
        {
            // arrange
            HttpResponseMessage initialResponse = await _client.GetAsync($"/Tender/Index");
            var antiForgeryValue = await ExtractAntiForgeryValue(initialResponse);
            HttpRequestMessage postRequest = new HttpRequestMessage(HttpMethod.Post, $"/Tender/EditAddedValue");
            postRequest.Headers.Add("Cookie", new CookieHeaderValue(AntiForgeryCookieName, antiForgeryValue.cookieValue).ToString());
            var formData = new Dictionary<string, string>
            {
               { AntiForgeryFieldName,antiForgeryValue.fieldValue},
            };

            postRequest.Content = new FormUrlEncodedContent(formData);
            // act
            HttpResponseMessage response = await _client.SendAsync(postRequest);
            var responseContent = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(responseContent);
            Assert.Contains("!DOCTYPE html", responseContent);
        }

        [Fact]
        public async Task GetAllMandatoryListProductsForExport_Returns_File()
        {
            // arranges

            // act
            var response = await _client.GetAsync($"/Tender/GetAllMandatoryListProductsForExport");
            var responseContent = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(responseContent);
            Assert.Contains("worksheets", responseContent);
        }

    }
}
