using Microsoft.Net.Http.Headers;
using MOF.Etimad.Monafasat.FunctionalTests.Base;
using MOF.Etimad.Monafasat.FunctionalTests.Helpers;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.Web;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace MOF.Etimad.Monafasat.FunctionalTests.Web.Actions
{
    public class QualificationControllerTest : BaseTestController
    {
        public QualificationControllerTest(CustomWebApplicationFactory<Startup> factory) : base(factory)
        {
        }

        [Fact]
        public async Task IndexPagingAsyncTest()
        {
            // arranges
            var requestModel = new PreQualificationSearchCriteriaModel() { };
            var queryString = requestModel.ToDictionary().ToQueryString();

            // act
            var response = await _client.GetAsync($"/Qualification/IndexPagingAsync?" + queryString);
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response);
        }

        [Fact]
        public async Task IndexPagingForSupplierProjectTest()
        {
            // arranges
            var requestModel = new PreQualificationSearchCriteriaModel() { };
            var queryString = requestModel.ToDictionary().ToQueryString();

            // act
            var response = await _client.GetAsync($"/Qualification/IndexPagingForSupplierProject?" + queryString);
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response);
        }

        [Fact]
        public async Task GetStatusAsyncTest()
        {
            // arranges
            // act
            var response = await _client.GetAsync($"/Qualification/GetStatusAsync");
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response);
        }

        [Fact]
        public async Task GetAreasAsyncTest()
        {
            // arranges
            // act
            var response = await _client.GetAsync($"/Qualification/GetAreasAsync");
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response);
        }

        [Fact]
        public async Task GetAllDataEntryUsersAsyncTest()
        {
            // arranges
            // act
            var response = await _client.GetAsync($"/Qualification/GetAllDataEntryUsersAsync");
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response);
        }

        [Fact]
        public async Task GetAllAgenciesAsyncTest()
        {
            // arranges
            // act
            var response = await _client.GetAsync($"/Qualification/GetAllAgenciesAsync");
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response);
        }

        [Fact]
        public async Task GetAllAuditorUsersAsyncTest()
        {
            // arranges
            // act
            var response = await _client.GetAsync($"/Qualification/GetAllAuditorUsersAsync");
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response);
        }

        [Fact]
        public async Task GetFinancialYearTest()
        {
            // arranges
            var requestModel = new PreQualificationSearchCriteriaModel() { };
            var queryString = requestModel.ToDictionary().ToQueryString();
            // act
            var response = await _client.GetAsync($"/Qualification/GetFinancialYear?" + queryString);
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response);
        }

        [Fact]
        public async Task IndexTest()
        {
            // arranges

            // act
            var response = await _client.GetAsync($"/Qualification/Index");
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response);
        }

        [Fact]
        public async Task PreQualificationDetailsTest()
        {
            // arranges
            // act
            var response = await _client.GetAsync($"/Qualification/PreQualificationDetails?QualificationId=S5w4nQxXBKMJK*@@**UCTg3n0A==");
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response);
        }

        [Fact]
        public async Task GetTenderTypesTest()
        {
            // arranges
            // act
            var response = await _client.GetAsync($"/Qualification/GetTenderTypes");
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response);
        }

        [Fact]
        public async Task AllPreQualificationsAsyncTest()
        {
            // arranges
            var requestModel = new PreQualificationSearchCriteriaModel() { };
            var queryString = requestModel.ToDictionary().ToQueryString();
            // act
            var response = await _client.GetAsync($"/Qualification/AllPreQualificationsAsync?" + queryString);
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response);
        }

        [Fact]
        public async Task QualificationsForVisitorTest()
        {
            // arranges
            // act
            var response = await _client.GetAsync($"/Qualification/QualificationsForVisitor");
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response);
        }

        [Fact]
        public async Task PreQualificationVisitorDetailsTest()
        {
            // arranges
            // act
            var response = await _client.GetAsync($"/Qualification/PreQualificationVisitorDetails/S5w4nQxXBKMJK*@@**UCTg3n0A==");
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response);
        }

        //The view 'QualificationStageIndex' was not found
        [Fact]
        public async Task QualificationStageIndexTest()
        {
            // arranges
            // act
            var response = await _client.GetAsync($"/Qualification/QualificationStageIndex?Type=DataEntry");
            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.NotNull(response);
        }

        [Fact]
        public async Task PreQualificationIndexUnderOperationsStageTest()
        {
            // arranges
            // act
            var response = await _client.GetAsync($"/Qualification/PreQualificationIndexUnderOperationsStage?Type=DataEntry");
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response);
        }

        [Fact]
        public async Task PreQualificationIndexCheckingStageTest()
        {
            // arranges
            // act
            var response = await _client.GetAsync($"/Qualification/PreQualificationIndexCheckingStage?Type=DataEntry");
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response);
        }

        [Fact]
        public async Task GetPreQualificationsForCheckingStageIndexAsyncTest()
        {
            // arranges
            var requestModel = new PreQualificationSearchCriteriaModel() { };
            var queryString = requestModel.ToDictionary().ToQueryString();
            // act
            var response = await _client.GetAsync($"/Qualification/GetPreQualificationsForCheckingStageIndexAsync?" + queryString);
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response);
        }

        [Fact]
        public async Task GetPreQualificationForUnderOperationsStageIndexAsyncTest()
        {
            // arranges
            var requestModel = new PreQualificationSearchCriteriaModel() { };
            var queryString = requestModel.ToDictionary().ToQueryString();
            // act
            var response = await _client.GetAsync($"/Qualification/GetPreQualificationForUnderOperationsStageIndexAsync?" + queryString);
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response);
        }

        //NotFound
        [Fact]
        public async Task CheckPreQualificationTest()
        {
            // arranges
            // act
            var response = await _client.GetAsync($"/Qualification/CheckPreQualification?tenderIdString=S5w4nQxXBKMJK*@@**UCTg3n0A==");
            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            Assert.NotNull(response);
        }

        [Fact]
        public async Task GetPreQualificationsRequestsForCheckingAsyncTest()
        {
            // arranges
            var requestModel = new QualificationIdWithSearchCriteria() { };
            var queryString = requestModel.ToDictionary().ToQueryString();
            // act
            var response = await _client.GetAsync($"/Qualification/GetPreQualificationsRequestsForCheckingAsync?" + queryString);
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response);
        }


        [Fact]
        public async Task checkQualificationDateTest()
        {
            // arranges
            HttpResponseMessage initialResponse = await _client.GetAsync("http://localhost:44386/Qualification/SavePreQualification");

            var antiForgeryValue = await ExtractAntiForgeryValue(initialResponse);


            HttpRequestMessage postRequest = new HttpRequestMessage(HttpMethod.Post, "http://localhost:44386/Qualification/checkQualificationDate");
            postRequest.Headers.Add("Cookie", new CookieHeaderValue(AntiForgeryCookieName, antiForgeryValue.cookieValue).ToString());

            var formData = new Dictionary<string, string>
                {
                    { AntiForgeryFieldName,antiForgeryValue.fieldValue},
                    { "PreQualificationCommitteeId","7"},
                    { "TechnicalOrganizationId","122"}
                };


            postRequest.Content = new FormUrlEncodedContent(formData);

            HttpResponseMessage postResponse = await _client.SendAsync(postRequest);
            // Assert
            Assert.Equal(HttpStatusCode.OK, postResponse.StatusCode);
            Assert.NotNull(postResponse);
        }



        [Fact]
        public async Task SaveDraftTest()
        {
            // arranges
            HttpResponseMessage initialResponse = await _client.GetAsync("http://localhost:44386/Qualification/SavePreQualification");

            var antiForgeryValue = await ExtractAntiForgeryValue(initialResponse);


            HttpRequestMessage postRequest = new HttpRequestMessage(HttpMethod.Post, "http://localhost:44386/Qualification/SaveDraft");
            postRequest.Headers.Add("Cookie", new CookieHeaderValue(AntiForgeryCookieName, antiForgeryValue.cookieValue).ToString());

            var formData = new Dictionary<string, string>
                {
                    { AntiForgeryFieldName,antiForgeryValue.fieldValue},
                    { "PreQualificationCommitteeId","7"},
                    { "TechnicalOrganizationId","122"},
                { "TenderName" , "test22"}
                };


            postRequest.Content = new FormUrlEncodedContent(formData);

            HttpResponseMessage postResponse = await _client.SendAsync(postRequest);
            // Assert
            Assert.Equal(HttpStatusCode.OK, postResponse.StatusCode);
            Assert.NotNull(postResponse);
        }

        [Fact]
        public async Task SavePreQualificationTest()
        {
            // arranges
            HttpResponseMessage initialResponse = await _client.GetAsync("http://localhost:44386/Qualification/SavePreQualification");

            var antiForgeryValue = await ExtractAntiForgeryValue(initialResponse);


            HttpRequestMessage postRequest = new HttpRequestMessage(HttpMethod.Post, "http://localhost:44386/Qualification/SavePreQualification");
            postRequest.Headers.Add("Cookie", new CookieHeaderValue(AntiForgeryCookieName, antiForgeryValue.cookieValue).ToString());

            var formData = new Dictionary<string, string>
                {
                    { AntiForgeryFieldName,antiForgeryValue.fieldValue},
                    { "PreQualificationCommitteeId","7"},
                    { "TechnicalOrganizationId","122"},
                    { "TenderName" , "test23"},
                    { "Details" , "details"},
                    { "IsAgancyHasTechnicalCommittee" , "true"},
                    {"LastEnqueriesDate" , DateTime.Now.AddDays(10).ToShortDateString() },
                    {"OffersCheckingDate" , DateTime.Now.AddDays(25).ToShortDateString() },
                    {"LastOfferPresentationTime" , DateTime.Now.TimeOfDay.ToString() },
                    {"OffersCheckingTime" , DateTime.Now.TimeOfDay.ToString() },


                };


            postRequest.Content = new FormUrlEncodedContent(formData);

            HttpResponseMessage postResponse = await _client.SendAsync(postRequest);
            // Assert
            Assert.Equal(HttpStatusCode.OK, postResponse.StatusCode);
            Assert.NotNull(postResponse);
        }

        // not found Tender/Delete 
        [Fact]
        public async Task DeleteTest()
        {
            // arranges
            // act
            var response = await _client.GetAsync($"/Qualification/Delete?tenderIdString=S5w4nQxXBKMJK*@@**UCTg3n0A==");
            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task DeletePostQualificationTest()
        {
            // arranges
            // act
            var response = await _client.GetAsync($"/Qualification/DeletePostQualification?tenderIdString=S5w4nQxXBKMJK*@@**UCTg3n0A==");
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response);
        }

        // Forbidden must be supplier
        [Fact]
        public async Task AddQualificationToSupplierFavouriteListAsyncTest()
        {
            // arranges
            // act
            var response = await _client.GetAsync($"/Qualification/AddQualificationToSupplierFavouriteListAsync?tenderIdString=S5w4nQxXBKMJK*@@**UCTg3n0A==");
            // Assert
            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
            Assert.NotNull(response);
        }

        [Fact]
        public async Task PreQualificationApprovalTest()
        {
            // arranges
            // act
            var response = await _client.GetAsync($"/Qualification/PreQualificationApproval?qualificationIdString=S5w4nQxXBKMJK*@@**UCTg3n0A==");
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response);
        }


        //[Fact]
        //public async Task SendPreQualificationToApproveAsyncTest()
        //{
        //    // arranges
        //    HttpResponseMessage initialResponse = await _client.GetAsync("http://localhost:44386/Qualification/Index");

        //    var antiForgeryValue = await ExtractAntiForgeryValue(initialResponse);


        //    HttpRequestMessage postRequest = new HttpRequestMessage(HttpMethod.Post, "http://localhost:44386/Qualification/SendPreQualificationToApproveAsync");
        //    postRequest.Headers.Add("Cookie", new CookieHeaderValue(AntiForgeryCookieName, antiForgeryValue.cookieValue).ToString());

        //    var formData = new Dictionary<string, string>
        //        {
        //            { AntiForgeryFieldName,antiForgeryValue.fieldValue},
        //            { "tenderIdString","S5w4nQxXBKMJK*@@**UCTg3n0A=="}
        //        };


        //    postRequest.Content = new FormUrlEncodedContent(formData);

        //    HttpResponseMessage postResponse = await _client.SendAsync(postRequest);
        //    // Assert
        //    Assert.Equal(HttpStatusCode.OK, postResponse.StatusCode);
        //    Assert.NotNull(postResponse);
        //}

        //[Fact]
        //public async Task SendQualificationToCommitteeApproveAsyncTest()
        //{
        //    // arranges
        //    HttpResponseMessage initialResponse = await _client.GetAsync("http://localhost:44386/Qualification/SavePreQualification");

        //    var antiForgeryValue = await ExtractAntiForgeryValue(initialResponse);


        //    HttpRequestMessage postRequest = new HttpRequestMessage(HttpMethod.Post, "http://localhost:44386/Qualification/SendQualificationToCommitteeApproveAsync");
        //    postRequest.Headers.Add("Cookie", new CookieHeaderValue(AntiForgeryCookieName, antiForgeryValue.cookieValue).ToString());

        //    var formData = new Dictionary<string, string>
        //        {
        //            { AntiForgeryFieldName,antiForgeryValue.fieldValue},
        //            { "tenderIdString","S5w4nQxXBKMJK*@@**UCTg3n0A=="}
        //        };


        //    postRequest.Content = new FormUrlEncodedContent(formData);

        //    HttpResponseMessage postResponse = await _client.SendAsync(postRequest);
        //    // Assert
        //    Assert.Equal(HttpStatusCode.OK, postResponse.StatusCode);
        //    Assert.NotNull(postResponse);
        //}

        //[Fact]
        //public async Task ApproveQualificationFromQualificationSecritaryAsyncTest()
        //{
        //    // arranges
        //    HttpResponseMessage initialResponse = await _client.GetAsync("http://localhost:44386/Qualification/PreQualificationApproval?qualificationIdString=qAUjdO5t2TcdNezYviJMQg==");

        //    var antiForgeryValue = await ExtractAntiForgeryValue(initialResponse);


        //    HttpRequestMessage postRequest = new HttpRequestMessage(HttpMethod.Post, "http://localhost:44386/Qualification/ApproveQualificationFromQualificationSecritaryAsync");
        //    postRequest.Headers.Add("Cookie", new CookieHeaderValue(AntiForgeryCookieName, antiForgeryValue.cookieValue).ToString());

        //    var formData = new Dictionary<string, string>
        //        {
        //            { AntiForgeryFieldName,antiForgeryValue.fieldValue},
        //            { "tenderIdString","S5w4nQxXBKMJK*@@**UCTg3n0A=="}
        //        };


        //    postRequest.Content = new FormUrlEncodedContent(formData);

        //    HttpResponseMessage postResponse = await _client.SendAsync(postRequest);
        //    // Assert
        //    Assert.Equal(HttpStatusCode.OK, postResponse.StatusCode);
        //    Assert.NotNull(postResponse);
        //}

        // must change tender status to 3 first
        [Fact]
        public async Task RejectApprvePreQualificationAsyncTest()
        {
            // arranges
            HttpResponseMessage initialResponse = await _client.GetAsync("http://localhost:44386/Qualification/PreQualificationApproval?qualificationIdString=qAUjdO5t2TcdNezYviJMQg==");

            var antiForgeryValue = await ExtractAntiForgeryValue(initialResponse);


            HttpRequestMessage postRequest = new HttpRequestMessage(HttpMethod.Post, "http://localhost:44386/Qualification/RejectApprvePreQualificationAsync");
            postRequest.Headers.Add("Cookie", new CookieHeaderValue(AntiForgeryCookieName, antiForgeryValue.cookieValue).ToString());

            string id = Util.Encrypt(295);

            var formData = new Dictionary<string, string>
                {
                    { AntiForgeryFieldName,antiForgeryValue.fieldValue},
                    { "tenderIdString",id},
                    { "RejectionReason","reason"}

                };


            postRequest.Content = new FormUrlEncodedContent(formData);

            HttpResponseMessage postResponse = await _client.SendAsync(postRequest);
            // Assert
            Assert.Equal(HttpStatusCode.OK, postResponse.StatusCode);
            Assert.NotNull(postResponse);
        }

        [Fact]
        public async Task ReopenPreQualificationAsyncTest()
        {
            // arranges
            string id = Util.Encrypt(342);

            HttpResponseMessage initialResponse = await _client.GetAsync("http://localhost:44386/Qualification/PreQualificationApproval?qualificationIdString=" + id);

            var antiForgeryValue = await ExtractAntiForgeryValue(initialResponse);


            HttpRequestMessage postRequest = new HttpRequestMessage(HttpMethod.Post, "http://localhost:44386/Qualification/ReopenPreQualificationAsync");
            postRequest.Headers.Add("Cookie", new CookieHeaderValue(AntiForgeryCookieName, antiForgeryValue.cookieValue).ToString());


            var formData = new Dictionary<string, string>
                {
                    { AntiForgeryFieldName,antiForgeryValue.fieldValue},
                    { "tenderIdString",id}

                };


            postRequest.Content = new FormUrlEncodedContent(formData);

            HttpResponseMessage postResponse = await _client.SendAsync(postRequest);
            // Assert
            Assert.Equal(HttpStatusCode.OK, postResponse.StatusCode);
            Assert.NotNull(postResponse);
        }

        //NotFound int api controller !!
        [Fact]
        public async Task StartPreQualificationCheckingOffersAsyncTest()
        {
            // arranges
            HttpResponseMessage initialResponse = await _client.GetAsync("http://localhost:44386/Qualification/PreQualificationApproval?qualificationIdString=qAUjdO5t2TcdNezYviJMQg==");

            var antiForgeryValue = await ExtractAntiForgeryValue(initialResponse);


            HttpRequestMessage postRequest = new HttpRequestMessage(HttpMethod.Post, "http://localhost:44386/Qualification/StartPreQualificationCheckingOffersAsync");
            postRequest.Headers.Add("Cookie", new CookieHeaderValue(AntiForgeryCookieName, antiForgeryValue.cookieValue).ToString());

            var formData = new Dictionary<string, string>
                {
                    { AntiForgeryFieldName,antiForgeryValue.fieldValue},
                    { "tenderIdString","qAUjdO5t2TcdNezYviJMQg=="}
                };


            postRequest.Content = new FormUrlEncodedContent(formData);

            HttpResponseMessage postResponse = await _client.SendAsync(postRequest);
            // Assert
            Assert.Equal(HttpStatusCode.NotFound, postResponse.StatusCode);
        }

        //NotFound int api controller !!
        [Fact]
        public async Task SendPreQualificationToApproveCheckingAsyncTest()
        {
            // arranges
            HttpResponseMessage initialResponse = await _client.GetAsync("http://localhost:44386/Qualification/PreQualificationApproval?qualificationIdString=qAUjdO5t2TcdNezYviJMQg==");

            var antiForgeryValue = await ExtractAntiForgeryValue(initialResponse);


            HttpRequestMessage postRequest = new HttpRequestMessage(HttpMethod.Post, "http://localhost:44386/Qualification/SendPreQualificationToApproveCheckingAsync");
            postRequest.Headers.Add("Cookie", new CookieHeaderValue(AntiForgeryCookieName, antiForgeryValue.cookieValue).ToString());

            var formData = new Dictionary<string, string>
                {
                    { AntiForgeryFieldName,antiForgeryValue.fieldValue},
                    { "tenderIdString","qAUjdO5t2TcdNezYviJMQg=="}
                };


            postRequest.Content = new FormUrlEncodedContent(formData);

            HttpResponseMessage postResponse = await _client.SendAsync(postRequest);
            // Assert
            Assert.Equal(HttpStatusCode.NotFound, postResponse.StatusCode);
        }

        //-----------------------------------------------------------------------------------------------------

        [Fact]
        public async Task CreateVerificationCodeTest()
        {
            // arranges
            HttpResponseMessage initialResponse = await _client.GetAsync("http://localhost:44386/Qualification/PreQualificationApproval?qualificationIdString=qAUjdO5t2TcdNezYviJMQg==");

            var antiForgeryValue = await ExtractAntiForgeryValue(initialResponse);


            HttpRequestMessage postRequest = new HttpRequestMessage(HttpMethod.Post, "http://localhost:44386/Qualification/CreateVerificationCode");
            postRequest.Headers.Add("Cookie", new CookieHeaderValue(AntiForgeryCookieName, antiForgeryValue.cookieValue).ToString());

            var formData = new Dictionary<string, string>
                {
                    { AntiForgeryFieldName,antiForgeryValue.fieldValue},
                    { "tenderIdString","qAUjdO5t2TcdNezYviJMQg=="}
                };


            postRequest.Content = new FormUrlEncodedContent(formData);

            HttpResponseMessage postResponse = await _client.SendAsync(postRequest);
            // Assert
            Assert.Equal(HttpStatusCode.OK, postResponse.StatusCode);
        }

        [Fact]
        public async Task EditCommitteesAsyncTest()
        {
            // arranges
            // act
            var response = await _client.GetAsync($"/Qualification/EditCommitteesAsync?id=S5w4nQxXBKMJK*@@**UCTg3n0A==");
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response);
        }


        [Fact]
        public async Task QualificationAttachmentsChangesApprovementTest()
        {
            // arranges
            // act
            var response = await _client.GetAsync($"/Qualification/QualificationAttachmentsChangesApprovement?tenderIdString=S5w4nQxXBKMJK*@@**UCTg3n0A==");
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response);
        }

        [Fact]
        public async Task EditAreasAsyncTest()
        {
            // arranges
            // act
            var response = await _client.GetAsync($"/Qualification/EditAreasAsync?id=S5w4nQxXBKMJK*@@**UCTg3n0A==");
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response);
        }

        [Fact]
        public async Task QualificationAttachmentsUpdatesTest()
        {
            // arranges
            // act
            var response = await _client.GetAsync($"/Qualification/QualificationAttachmentsUpdates?id=S5w4nQxXBKMJK*@@**UCTg3n0A==");
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response);
        }

        [Fact]
        public async Task ExtendQualificationDatesAsyncTest()
        {
            // arranges
            // act
            var response = await _client.GetAsync($"/Qualification/ExtendQualificationDatesAsync?tenderIdString=S5w4nQxXBKMJK*@@**UCTg3n0A==");
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response);
        }

        [Fact]
        public async Task GetAllSuppliersHasPrequalificationsTest()
        {
            // arranges
            // act
            var response = await _client.GetAsync($"/Qualification/GetAllSuppliersHasPrequalifications?tenderIdString=S5w4nQxXBKMJK*@@**UCTg3n0A==");
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response);
        }

        //Not Found !!
        [Fact]
        public async Task RejectCheckPreQualificationOffersReportAsyncTest()
        {
            // arranges
            var queryString = new Dictionary<string, object>
                {
                    { "tenderIdString","S5w4nQxXBKMJK*@@**UCTg3n0A=="},
                    { "RejectionReason","reason"}
            }.ToQueryString();
            // act
            var response = await _client.GetAsync($"/Qualification/RejectCheckPreQualificationOffersReportAsync?" + queryString);
            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            Assert.NotNull(response);
        }

        [Fact]
        public async Task QualificationExtendDateApprovementTest()
        {
            // arranges
            // act
            var response = await _client.GetAsync($"/Qualification/QualificationExtendDateApprovement?tenderIdString=S5w4nQxXBKMJK*@@**UCTg3n0A==");
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response);
        }

        [Fact]
        public async Task OpenQualificationDetailsReportTest()
        {
            // arranges
            // act
            var response = await _client.GetAsync($"/Qualification/OpenQualificationDetailsReport?tenderIdString=S5w4nQxXBKMJK*@@**UCTg3n0A==");
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response);
        }

        [Fact]
        public async Task QualificationOffersRegistryReportTest()
        {
            // arranges
            // act
            var response = await _client.GetAsync($"/Qualification/QualificationOffersRegistryReport?qualificationIdString=S5w4nQxXBKMJK*@@**UCTg3n0A==");
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response);
        }


    }
}
