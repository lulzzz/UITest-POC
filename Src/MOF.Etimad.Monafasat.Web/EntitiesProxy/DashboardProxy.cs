using IdentityServer4.Test;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MOF.Etimad.Monafasat.Core;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.ViewModel; 
using MOF.Etimad.Monafasat.Web.Infrastructure;
using MOF.Etimad.Monafasat.Web.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.EntitiesProxy
{
   public class DashboardProxy
   {
      private static ApiRequest<DashboardIndexModel> apiRequest;
      private static ApiRequest<object> apiRequestObj;
      private static ApiRequest<TestUserModel> apiRequestIDP;
      public static void Initialize(ControllerContext currentContext)
      {
         apiRequest = new ApiRequest<DashboardIndexModel>(currentContext);
         apiRequestObj = new ApiRequest<object>(currentContext);
         apiRequestIDP = new ApiRequest<TestUserModel>(currentContext);

      }
      public static async Task<QueryResult<RejectTenderViewModel>> RejectedTendersPagingAsync(SearchCriteria criteria)
      {
         //Deserializing the response recieved from web api and storing into the Tender List  
         var tenderList = JsonConvert.DeserializeObject<QueryResult<RejectTenderViewModel>>(await apiRequest.GetAsync("Dashboard/RejectedTendersPagingAsync?" + UrlHelper.ToQueryString(criteria)));
         return tenderList;
      }

      public static async Task<QueryResult<RejectTenderViewModel>> OppenedRejectedTendersPagingAsync(SearchCriteria criteria)
      {
         //Deserializing the response recieved from web api and storing into the Tender List  
         var tenderList = JsonConvert.DeserializeObject<QueryResult<RejectTenderViewModel>>(await apiRequest.GetAsync("Dashboard/OppenedRejectedTendersPagingAsync?" + UrlHelper.ToQueryString(criteria)));
         return tenderList;
      }

      public static async Task<QueryResult<RejectTenderViewModel>> CheckedRejectedTendersPagingAsync(SearchCriteria criteria)
      {
         //Deserializing the response recieved from web api and storing into the Tender List  
         var tenderList = JsonConvert.DeserializeObject<QueryResult<RejectTenderViewModel>>(await apiRequest.GetAsync("Dashboard/CheckedRejectedTendersPagingAsync?" + UrlHelper.ToQueryString(criteria)));
         return tenderList;
      }

      public static async Task<QueryResult<RejectTenderViewModel>> AwardedRejectedTendersPagingAsync(SearchCriteria criteria)
      {
         //Deserializing the response recieved from web api and storing into the Tender List  
         var tenderList = JsonConvert.DeserializeObject<QueryResult<RejectTenderViewModel>>(await apiRequest.GetAsync("Dashboard/AwardedRejectedTendersPagingAsync?" + UrlHelper.ToQueryString(criteria)));
         return tenderList;
      }

      public static async Task<QueryResult<JoiningRequestInvitationsViewModel>> JoiningRequestInvitationsPagingAsync(SearchCriteria criteria)
      {
         //Deserializing the response recieved from web api and storing into the Tender List  
         var tenderList = JsonConvert.DeserializeObject<QueryResult<JoiningRequestInvitationsViewModel>>(await apiRequest.GetAsync("Dashboard/JoiningRequestInvitationsPagingAsync?" + UrlHelper.ToQueryString(criteria)));
         return tenderList;
      }

      public static async Task<QueryResult<OpeningNotificationsViewModel>> OpeningNotificationsPagingAsync(SearchCriteria criteria)
      {
         //Deserializing the response recieved from web api and storing into the Tender List  
         var tenderList = JsonConvert.DeserializeObject<QueryResult<OpeningNotificationsViewModel>>(await apiRequest.GetAsync("Dashboard/OpeningNotificationsPagingAsync?" + UrlHelper.ToQueryString(criteria)));
         return tenderList;
      }

      public static async Task<QueryResult<ProcessNeedsActionViewModel>> GetAuditingProcessNeedsActionPagingAsync(SearchCriteria criteria)
      {
         //Deserializing the response recieved from web api and storing into the Tender List  
         var tenderList = JsonConvert.DeserializeObject<QueryResult<ProcessNeedsActionViewModel>>(await apiRequest.GetAsync("Dashboard/GetAuditingProcessNeedsActionPagingAsync?" + UrlHelper.ToQueryString(criteria)));
         return tenderList;
      }
      public static async Task<QueryResult<ProcessNeedsActionViewModel>> GetDataEnteryProcessNeedsActionPagingAsync(SearchCriteria criteria)
      {
         //Deserializing the response recieved from web api and storing into the Tender List  
         var tenderList = JsonConvert.DeserializeObject<QueryResult<ProcessNeedsActionViewModel>>(await apiRequest.GetAsync("Dashboard/GetDataEntrygProcessNeedsActionPagingAsync?" + UrlHelper.ToQueryString(criteria)));
         return tenderList;
      }

      public static async Task<QueryResult<ProcessNeedsActionViewModel>> GetConfirmOpeningProcessNeedsActionPagingAsync(SearchCriteria criteria)
      {
         //Deserializing the response recieved from web api and storing into the Tender List  
         var tenderList = JsonConvert.DeserializeObject<QueryResult<ProcessNeedsActionViewModel>>(await apiRequest.GetAsync("Dashboard/GetConfirmOpeningProcessNeedsActionPagingAsync?" + UrlHelper.ToQueryString(criteria)));
         return tenderList;
      }

      public static async Task<QueryResult<ProcessNeedsActionViewModel>> GetCheckingStageProcessNeedsActionPagingAsync(SearchCriteria criteria)
      {
         //Deserializing the response recieved from web api and storing into the Tender List  
         var tenderList = JsonConvert.DeserializeObject<QueryResult<ProcessNeedsActionViewModel>>(await apiRequest.GetAsync("Dashboard/GetOpeningStageProcessNeedsActionPagingAsync?" + UrlHelper.ToQueryString(criteria)));
         return tenderList;
      }

      public static async Task<QueryResult<ProcessNeedsActionViewModel>> GetAwardingProcessNeedsActionPagingAsync(SearchCriteria criteria)
      {
         //Deserializing the response recieved from web api and storing into the Tender List  
         var tenderList = JsonConvert.DeserializeObject<QueryResult<ProcessNeedsActionViewModel>>(await apiRequest.GetAsync("Dashboard/OpeningNotificationsPagingAsync?" + UrlHelper.ToQueryString(criteria)));
         return tenderList;
      }

      public static async Task<DashboardIndexModel> GetEntryStageIndexCounts()
      {
         var dashboardModel = JsonConvert.DeserializeObject<DashboardIndexModel>(await apiRequest.GetAsync("Dashboard/GetEntryStageIndexCounts"));
         return dashboardModel;
      }

      public static async Task<DashboardIndexModel> GetAuditingStageIndexCounts()
      {
         var dashboardModel = JsonConvert.DeserializeObject<DashboardIndexModel>(await apiRequest.GetAsync("Dashboard/GetAuditingStageIndexCounts"));
         return dashboardModel;
      }


      public static async Task<DashboardIndexModel> GetOpeningStageIndexCounts()
      {
         var dashboardModel = JsonConvert.DeserializeObject<DashboardIndexModel>(await apiRequest.GetAsync("Dashboard/GetOpeningStageIndexCounts"));
         return dashboardModel;
      }

      public static async Task<DashboardIndexModel> GetConfirmOpeningStageIndexCounts()
      {
         var dashboardModel = JsonConvert.DeserializeObject<DashboardIndexModel>(await apiRequest.GetAsync("Dashboard/GetConfirmOpeningStageIndexCounts"));
         return dashboardModel;
      }


      public static async Task<DashboardIndexModel> GetCheckingStageIndexCounts()
      {
         var dashboardModel = JsonConvert.DeserializeObject<DashboardIndexModel>(await apiRequest.GetAsync("Dashboard/GetCheckingStageIndexCounts"));
         return dashboardModel;
      }
      public static async Task<DashboardIndexModel> GetConfirmCheckingStageIndexCounts()
      {
         var dashboardModel = JsonConvert.DeserializeObject<DashboardIndexModel>(await apiRequest.GetAsync("Dashboard/GetConfirmCheckingStageIndexCounts"));
         return dashboardModel;
      }


      public static async Task<DashboardIndexModel> GetAwardingStageIndexCounts()
      {
         var dashboardModel = JsonConvert.DeserializeObject<DashboardIndexModel>(await apiRequest.GetAsync("Dashboard/GetAwardingStageIndexCounts"));
         return dashboardModel;
      }

      public static async Task<DashboardIndexModel> GetConfirmAwardingStageIndexCounts()
      {
         var dashboardModel = JsonConvert.DeserializeObject<DashboardIndexModel>(await apiRequest.GetAsync("Dashboard/GetConfirmAwardingStageIndexCounts"));
         return dashboardModel;
      }

      public static async Task<SalesSummaryViewModel> SalesSummaryPagingAsync(DashboardSearchCriteria criteria)
      {
         //Deserializing the response recieved from web api and storing into the Tender List  
         var resultList = JsonConvert.DeserializeObject<SalesSummaryViewModel>(await apiRequest.GetAsync("Dashboard/SalesSummaryPagingAsync?" + UrlHelper.ToQueryString(criteria)));
         return resultList;
      }
      public static async Task<TendersSummaryViewModel> TendersSummaryPagingAsync(DashboardSearchCriteria criteria)
      {
         //Deserializing the response recieved from web api and storing into the Tender List  
         var resultList = JsonConvert.DeserializeObject<TendersSummaryViewModel>(await apiRequest.GetAsync("Dashboard/TendersSummaryPagingAsync?" + UrlHelper.ToQueryString(criteria)));
         return resultList;
      }
      public static async Task<DirectInvitationsSummaryViewModel> DirectInvitationsSummaryPagingAsync(DashboardSearchCriteria criteria)
      {
         //Deserializing the response recieved from web api and storing into the Tender List  
         var resultList = JsonConvert.DeserializeObject<DirectInvitationsSummaryViewModel>(await apiRequest.GetAsync("Dashboard/DirectInvitationsSummaryPagingAsync?" + UrlHelper.ToQueryString(criteria)));
         return resultList;
      }
      public static async Task<string> SuppliersCountPagingAsync(DashboardSearchCriteria criteria)
      {
         //Deserializing the response recieved from web api and storing into the Tender List  
         var resultList = JsonConvert.DeserializeObject<string>(await apiRequest.GetAsync("Dashboard/SuppliersCountPagingAsync?" + UrlHelper.ToQueryString(criteria)));
         return resultList;
      }
      public static async Task<List<LastTenPurshaseModel>> GetLastTenPurshase()
      {
         var tenPurshaseModel = JsonConvert.DeserializeObject<List<LastTenPurshaseModel>>(await apiRequest.GetAsync("Dashboard/GetLastTenPurshase"));
         return tenPurshaseModel;
      }
      public static async Task<QueryResult<SupplierEnquiryModel>> GetSuppliersEnquiryAsync(DashboardSearchCriteria criteria)
      {
         var tenPurshaseModel = JsonConvert.DeserializeObject<QueryResult<SupplierEnquiryModel>>(await apiRequest.GetAsync("Dashboard/GetSuppliersEnquiry?" + UrlHelper.ToQueryString(criteria)));
         return tenPurshaseModel;
      }
   }
}
