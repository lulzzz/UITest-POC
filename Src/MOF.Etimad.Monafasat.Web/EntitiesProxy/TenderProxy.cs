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
   public class TenderProxy
   {
      private static ApiRequest<BasicTenderModel> apiRequest;
      private static ApiRequest<object> apiRequestObj;
      internal static Task<TenderOffersStepModel> GetOfferDetailsByIdAsync(object tenderId)
      {
         throw new NotImplementedException();
      }
      private static ApiRequest<List<QuantityTableModel>> quantityApiRequest;
      private static ApiRequest<TenderRelationsModel> relationsRequest;
      private static ApiRequest<TenderOffersStepModel> detailsRequest;
      private static ApiRequest<List<TenderAttachmentModel>> AttachmentsApiRequest;
      private static ApiRequest<TestUserModel> apiRequestIDP;
      private static ApiRequest<List<SupplierInvitationModel>> ApiSupplierInvitation;
      private static ApiRequest<PurshaseModel> PurshaseRequest;
      private static ApiRequest<EnquiryModel> enquiryRequest;
      private static ApiRequest<TenderCountsModel> tenderCountApi;
      private static ApiRequest<EnquiryReplyModel> enquiryReplyRequest;
      private static ApiRequest<JoinTechnicalCommitteeModel> joinCommitteeRequest;
      private static ApiRequest<EnquiryInternalCommentModel> joinCommentRequest;
      private static ApiRequest<List<EnquiryInternalCommentModel>> joinCommentListRequest;
      private static QueryResult<EnquiryReplyModel> enquiryList;
      private static ApiRequest<VacationsDateModel> vacationsRequest;
      private static ControllerContext _CurrentContext;

      public static void Initialize(ControllerContext currentContext)
      {
         // storing controller context allowing to inistalize ApiRequest object instance only when needed !
         _CurrentContext = currentContext;
         apiRequest = new ApiRequest<BasicTenderModel>(currentContext);
         quantityApiRequest = new ApiRequest<List<QuantityTableModel>>(currentContext);
         relationsRequest = new ApiRequest<TenderRelationsModel>(currentContext);
         detailsRequest = new ApiRequest<TenderOffersStepModel>(currentContext);
         AttachmentsApiRequest = new ApiRequest<List<TenderAttachmentModel>>(currentContext);
         apiRequestIDP = new ApiRequest<TestUserModel>(currentContext);
         ApiSupplierInvitation = new ApiRequest<List<SupplierInvitationModel>>(currentContext);
         apiRequestObj = new ApiRequest<object>(currentContext);
         PurshaseRequest = new ApiRequest<PurshaseModel>(currentContext);
         enquiryRequest = new ApiRequest<EnquiryModel>(currentContext);
         enquiryReplyRequest = new ApiRequest<EnquiryReplyModel>(currentContext);
         joinCommitteeRequest = new ApiRequest<JoinTechnicalCommitteeModel>(currentContext);
         joinCommentRequest = new ApiRequest<EnquiryInternalCommentModel>(currentContext);
         joinCommentListRequest = new ApiRequest<List<EnquiryInternalCommentModel>>(currentContext);
         vacationsRequest = new ApiRequest<VacationsDateModel>(currentContext);
         tenderCountApi = new ApiRequest<TenderCountsModel>(currentContext);
      }
      public static async Task<QueryResult<BasicTenderModel>> GetAsync(TenderSearchCriteriaModel criteria)
      {
         //Deserializing the response recieved from web api and storing into the Tender List  
         var tenderList = JsonConvert.DeserializeObject<QueryResult<BasicTenderModel>>(await apiRequest.GetAsync("Tender/GetTendersBySearchCriteria?" + UrlHelper.ToQueryString(criteria)));
         return tenderList;
      }

      public static async Task<QueryResult<BasicTenderModel>> FindAwardedTenderBySearchCriteria(TenderSearchCriteriaModel criteria)
      {
         //Deserializing the response recieved from web api and storing into the Tender List  
         var tenderList = JsonConvert.DeserializeObject<QueryResult<BasicTenderModel>>(
            await apiRequest.GetAsync("Tender/FindAwardedTenderBySearchCriteria?" + UrlHelper.ToQueryString(criteria)));
         return tenderList;
      }



      public static async Task<QueryResult<BasicTenderModel>> GetMyTenderAsync()
      {
         var tenderList = JsonConvert.DeserializeObject<QueryResult<BasicTenderModel>>(await apiRequest.GetAsync("Tender/GetTendersByLogedUser"));
         return tenderList;
      }
      public static async Task<List<VacationsDateModel>> FindAllVacationDates()
      {
         //Deserializing the response recieved from web api and storing into the Tender List  
         var addressList = JsonConvert.DeserializeObject<List<VacationsDateModel>>(await vacationsRequest.GetAsync("Tender/FindAllVacationDates"));
         return addressList;
      }
      public static async Task<bool> SendRequestToApplayForTender(int tenderId)
      {

         var result = JsonConvert.DeserializeObject<bool>(await apiRequest.PostAsync("Tender/SendRequestToApplayForTender",
             new BasicTenderModel { TenderId = tenderId }));

         return result;
      }

      public static async Task<bool> IsTenderPurchasedBySupplier(int tenderId)
      {
         var result = JsonConvert.DeserializeObject<bool>(await apiRequest.GetAsync("Tender/IsTenderPurchasedBySupplier/" + tenderId));

         return result;
      }

      #region Invitation Proxy
      public static async Task<TenderInvitationDetailsModel> GetByIdInvitationDetialsAsync(int Id)
      {
         var tenderObject = JsonConvert.DeserializeObject<TenderInvitationDetailsModel>(await apiRequest.GetAsync("Tender/GetByTenderIdInvitationDetails/" + Id));
         return tenderObject;
      }

      public static async Task<List<string>> GetFinancialYear()
      {

         var years = JsonConvert.DeserializeObject<List<string>>(await apiRequest.GetAsync("Tender/GetFinancialYear"));
         return years;

      }

      public static async Task<TenderInvitationDetailsModel> UpdateInvitationStatus(int invitationId, int statusId)
      {
         var invitationObject = JsonConvert.DeserializeObject<TenderInvitationDetailsModel>(await apiRequest.GetAsync("Invitation/UpdateInvitationStatus?invitationId=" + invitationId + "&statusId=" + statusId));
         return invitationObject;
      }
      public static async Task<TenderInvitationDetailsModel> RejectJoiningRequestStatus(int invitationId, int statusId, string rejectionReason)
      {
         var invitationObject = JsonConvert.DeserializeObject<TenderInvitationDetailsModel>(await apiRequest.GetAsync("Invitation/RejectJoiningRequestStatus?invitationId=" + invitationId + "&statusId=" + statusId + "&rejectionReason=" + rejectionReason));
         return invitationObject;
      }
      //public static async Task<QueryResult<TenderOffersStepModel>> SendInvitationsAsync(int tenderId, List<int> checkedCommericalRegisterNo, List<int> unCheckedCommericalRegisterNo)
      //{
      //   var tenderList = JsonConvert.DeserializeObject<QueryResult<TenderOffersStepModel>>(await apiRequest.GetAsync("Tender/SendInvitations?tenderId=" + tenderId + UrlHelper.ListToQuerstring(checkedCommericalRegisterNo, "checkedCommericalRegisterNo") + UrlHelper.ListToQuerstring(unCheckedCommericalRegisterNo, "unCheckedCommericalRegisterNo")));
      //   return tenderList;
      //}
      public static async Task<string> SendInvitationsAsync(List<SupplierInvitationModel> suppliers)
      {
         return await ApiSupplierInvitation.PostAsync("Tender/SendInvitations", suppliers);
      }

      public static async Task<List<SupplierInvitationModel>> GetInvitationsAsync(int tenderId, List<string> commericalRegisterNos)
      {
         var tenderList = JsonConvert.DeserializeObject<List<SupplierInvitationModel>>(await apiRequest.GetAsync("Invitation/GetInvitation?tenderId=" + tenderId + UrlHelper.ListToQuerstring(commericalRegisterNos, "commericalRegisterNos")));
         return tenderList;
      }
      public static async Task<QueryResult<SupplierInvitationModel>> GetAllInvitedSuppliersAsync(SupplierSearchCretriaModel supplierSearchCretria)
      {
         var tenderList = JsonConvert.DeserializeObject<QueryResult<SupplierInvitationModel>>(await apiRequest.GetAsync("Invitation/GetAllInvitedSuppliers?" + UrlHelper.ToQueryString(supplierSearchCretria)));
         return tenderList;
      }
      public static async Task<QueryResult<SupplierInvitationModel>> GetAllSuppliersAsync(SupplierSearchCretriaModel supplierSearchCretria)
      {
         var tenderList = JsonConvert.DeserializeObject<QueryResult<SupplierInvitationModel>>(await apiRequest.GetAsync("Invitation/GetAllSuppliers?" + UrlHelper.ToQueryString(supplierSearchCretria)));
         return tenderList;
      }
      public static async Task<QueryResult<TenderInvitationDetailsModel>> PostEmailsAsync(int tenderId, List<string> emails)
      {
         var tenderList = JsonConvert.DeserializeObject<QueryResult<TenderInvitationDetailsModel>>(await apiRequest.GetAsync("Invitation/SendInvitationByEmail?tenderId=" + tenderId + UrlHelper.ListToQuerstring(emails, "emails")));
         return tenderList;
      }

      public static async Task<QueryResult<TenderInvitationDetailsModel>> PostSmsAsync(int tenderId, List<string> mobilNoList)
      {
         var tenderList = JsonConvert.DeserializeObject<QueryResult<TenderInvitationDetailsModel>>(await apiRequest.GetAsync("Invitation/SendInvitationBySms?tenderId=" + tenderId + UrlHelper.ListToQuerstring(mobilNoList, "mobilNoList")));
         return tenderList;
      }
      public static async Task<List<TestUserModel>> GetAllSuppliersAsync()
      {
         var _data = await apiRequestIDP.GetIDPAsync("IDP/Users/Suppliers");
         List<TestUserModel> tenderObject = JsonConvert.DeserializeObject<List<TestUserModel>>(_data);

         return tenderObject;
      }

      public static async Task<List<SupplierModel>> GetAllSuppliersDataAsync()
      {
         var suppliersList = JsonConvert.DeserializeObject<List<SupplierModel>>(await apiRequestObj.GetAsync("Invitation/GetAllSuppliersData"));
         return suppliersList;
      }
      public static async Task<List<TestUserModel>> GetSuppliersSearchAsync(ViewModel.SupplierSearchCretriaModel supplierSearchCretria)
      {
         List<TestUserModel> tenderObject = JsonConvert.DeserializeObject<List<TestUserModel>>(await apiRequestIDP.GetIDPAsync("IDP/Users/GetSuppliersSearch?" + UrlHelper.ToQueryString(supplierSearchCretria)));

         return tenderObject;
      }
      //public static async Task<List<TestUser>> GetSuppliersInvitationDataAsync(TestUser users , SupplierInvitationModel supplierInvitationModel)
      //{
      //   List<TestUser> tenderObject = JsonConvert.DeserializeObject<List<TestUser>>(await apiRequestIDP.GetIDPAsync("IDP/Users/GetSuppliersSearch?" + UrlHelper.ToQueryString(supplierSearchCretria)));

      //   return tenderObject;
      //}

      public static async Task<QueryResult<TenderInvitationDetailsModel>> GetNewInvitationsByCRNoAsync(int commercialRegisterNo, int? statusId)
      {

         //Deserializing the response recieved from web api and storing into the Tender List  
         var tenderList = JsonConvert.DeserializeObject<QueryResult<TenderInvitationDetailsModel>>(await apiRequest.GetAsync("Invitation/GetNewInvitationsByCRNo?commericalRegisterNo=" + commercialRegisterNo + "&statusId=" + statusId));

         return tenderList;
      }
      public static async Task<QueryResult<TenderInvitationDetailsModel>> GetSupplierTenders(TenderSearchCriteria tenderSearch)
      {
         var tenderList = JsonConvert.DeserializeObject<QueryResult<TenderInvitationDetailsModel>>(await apiRequest.GetAsync("Invitation/GetSupplierTenders?" + UrlHelper.ToQueryString(tenderSearch)));

         return tenderList;
      }

      public static async Task<QueryResult<TenderInvitationDetailsModel>> GetAllTendersAsync()
      {
         var tenderList = JsonConvert.DeserializeObject<QueryResult<TenderInvitationDetailsModel>>(await apiRequest.GetAsync("Invitation/GetAllTenders"));
         return tenderList;
      }

      public static async Task<QueryResult<TenderInvitationDetailsModel>> GetAllSupplierTendersAsync(TenderSearchCriteriaModel tenderSearch)
      {
         var tenderList = JsonConvert.DeserializeObject<QueryResult<TenderInvitationDetailsModel>>(await apiRequest.GetAsync("Invitation/GetAllSupplierTenders?" + UrlHelper.ToQueryString(tenderSearch)));
         return tenderList;
      }
      public static async Task<QueryResult<SupplierInvitationModel>> GetSuppliersJoiningRequestsByTenderIdAsync(int tenderId)
      {
         var joiningRequestObject = JsonConvert.DeserializeObject<QueryResult<SupplierInvitationModel>>(await apiRequest.GetAsync("Invitation/GetSuppliersJoiningRequestsByTenderId/" + tenderId));

         return joiningRequestObject;
      }

      public static async Task<QueryResult<TenderOpenOfferModel>> GetOffersForOpenByTenderIdAsync(int tenderId)
      {
         var invitationsOffers = JsonConvert.DeserializeObject<QueryResult<TenderOpenOfferModel>>(await apiRequest.GetAsync("Tender/GetOffersForOpenByTenderIdAsync/" + tenderId));

         return invitationsOffers;
      }

      public static async Task<QueryResult<TenderCheckOfferModel>> GetOffersForCheckByTenderIdAsync(int tenderId)
      {
         var invitationsOffers = JsonConvert.DeserializeObject<QueryResult<TenderCheckOfferModel>>(await apiRequest.GetAsync("Tender/GetOffersForCheckByTenderIdAsync/" + tenderId));

         return invitationsOffers;
      }
      public static async Task<QueryResult<TenderCheckOfferModel>> GetOffersForAwardingByTenderIdAsync(int tenderId)
      {
         var invitationsOffers = JsonConvert.DeserializeObject<QueryResult<TenderCheckOfferModel>>(await apiRequest.GetAsync("Tender/GetOffersForAwardingByTenderId/" + tenderId));

         return invitationsOffers;
      }
      public static async Task<QueryResult<TenderCheckOfferModel>> GetAwardedOffersByTenderIdAsync(int tenderId)
      {
         var invitationsOffers = JsonConvert.DeserializeObject<QueryResult<TenderCheckOfferModel>>(await apiRequest.GetAsync("Tender/GetAwardedOffersByTenderId/" + tenderId));

         return invitationsOffers;
      }

      public static async Task<TenderInvitationDetailsModel> GetJoiningRequestByInvitationIdAsync(int invitationId)
      {
         var joiningRequestObject = JsonConvert.DeserializeObject<TenderInvitationDetailsModel>(await apiRequest.GetAsync("Invitation/GetJoiningRequestByInvitationId/" + invitationId));

         return joiningRequestObject;
      }
      #endregion
      public static async Task CreateVerificationCode(int tenderId)
      {
         JsonConvert.DeserializeObject(await apiRequest.PostAsync("Tender/CreateVerificationCode", new BasicTenderModel { TenderId = tenderId }));
      }
      public static async Task<TenderOffersStepModel> GetDetailsByIdAsync(int Id)
      {
         var tenderObject = JsonConvert.DeserializeObject<TenderOffersStepModel>(await apiRequest.GetAsync("Tender/GetDetailsById/" + Id));

         return tenderObject;
      }
      public static async Task<BasicTenderModel> GetBasicByIdAsync(int Id)
      {
         var tenderObject = JsonConvert.DeserializeObject<BasicTenderModel>(await apiRequest.GetAsync("Tender/GetBasicById/" + Id));

         return tenderObject;
      }

      public static async Task<PurshaseModel> GetBasicByIdAndSupplier(int Id)
      {
         var tenderObject = JsonConvert.DeserializeObject<PurshaseModel>(await PurshaseRequest.GetAsync("Tender/GetBasicByIdAndSupplier/" + Id));

         return tenderObject;
      }
      public static async Task<TenderOffersModel> GetTenderOffersByIdAsync(int Id)
      {
         var tenderObject = JsonConvert.DeserializeObject<TenderOffersModel>(await apiRequest.GetAsync("Tender/GetTenderOffersByIdAsync/" + Id));

         return tenderObject;
      }
      public static async Task<PurshaseModel> PurshaseTender(int Id)
      {
         var purshase = JsonConvert.DeserializeObject<PurshaseModel>(await apiRequestObj.PostAsync("Tender/PurshaseTender", Id));

         return purshase;
      }

      #region Enquiry

      public static async Task<List<LookupModel>> GetTechnicalCommitteeList()
      {
         var technicalCommitteeList = JsonConvert.DeserializeObject<List<LookupModel>>(await apiRequest.GetAsync("Lookup/GetTechnicalCommitteeList"));

         return technicalCommitteeList;
      }
      public static async Task<QueryResult<LookupModel>> GetInvitedCommitesByEnquiryId(SimpleSearchCretriaModel criteria)
      {
         var technicalCommitteeList = JsonConvert.DeserializeObject<QueryResult<LookupModel>>(await apiRequest.GetAsync("Enquiry/GetInvitedCommitesByEnquiryId?" + UrlHelper.ToQueryString(criteria)));

         return technicalCommitteeList;
      }
  
      public static async Task<JoinTechnicalCommitteeModel> SendInvitationsToJoinCommitteeAsync(JoinTechnicalCommitteeModel joinTechnicalCommitteeModel)
      {
         string response = await joinCommitteeRequest.PostAsync("Enquiry/SendInvitationsToJoinCommittee", joinTechnicalCommitteeModel);
         var result = JsonConvert.DeserializeObject<JoinTechnicalCommitteeModel>(response);
         return result;

      }

      public static async Task<JoinTechnicalCommitteeModel> GetJoinTechnicalCommitteeByEnquiryId(int enquiryId)
      {
         var technicalCommittee = JsonConvert.DeserializeObject<JoinTechnicalCommitteeModel>(await joinCommitteeRequest.GetAsync("Enquiry/GetJoinTechnicalCommitteeByEnquiryId/" + enquiryId));

         return technicalCommittee;
      }
        
      public static async Task<EnquiryModel> SendEnquiry(EnquiryModel enquiryModel)
      {

         string response = await enquiryRequest.PostAsync("Enquiry/SendEnquiry", enquiryModel);
         var result = JsonConvert.DeserializeObject<EnquiryModel>(response);
         return result;
      }

      public static async Task<EnquiryReplyModel> AddEnquiryReply(EnquiryReplyModel enquiryReplyModel)
      {

         string response = await enquiryReplyRequest.PostAsync("Enquiry/AddEnquiryReply", enquiryReplyModel);
         var result = JsonConvert.DeserializeObject<EnquiryReplyModel>(response);
         return result;
      }

      public static async Task<EnquiryReplyModel> AddEnquiryCommentAsync(EnquiryReplyModel enquiryReplyModel)
      {
         string response = await enquiryReplyRequest.PostAsync("Enquiry/AddEnquiryComment", enquiryReplyModel);
         var result = JsonConvert.DeserializeObject<EnquiryReplyModel>(response);
         return result;
      }
 
      public static async Task<string> RemoveInvitedCommitteeAsync(int committeeId)
      {
         return await joinCommentListRequest.PostAsync("Enquiry/RemoveInvitedCommittee?committeeId=" + committeeId, null);
      }

      public static async Task<EnquiryReplyModel> EditEnquiryReply(EnquiryReplyModel enquiryReplyModel)
      {

         string response = await enquiryReplyRequest.PostAsync("Enquiry/EditEnquiryReply", enquiryReplyModel);
         var result = JsonConvert.DeserializeObject<EnquiryReplyModel>(response);
         return result;
      }

      public static async Task<EnquiryReplyModel> ApproveEnquiryReply(int enquiryReplyId)
      {

         string response = await enquiryReplyRequest.GetAsync("Enquiry/ApproveEnquiryReply/" + enquiryReplyId);
         var result = JsonConvert.DeserializeObject<EnquiryReplyModel>(response);
         return result;
      }

      public static async Task<QueryResult<EnquiryModel>> GetAllSuppliersEnquiriesByTenderId(int tenderId)
      {
         var enquiryList = JsonConvert.DeserializeObject<QueryResult<EnquiryModel>>(await enquiryRequest.GetAsync("Enquiry/GetAllSuppliersEnquiriesByTenderId/" + tenderId));

         return enquiryList;
      }

      public static async Task<QueryResult<EnquiryReplyModel>> GetAllEnquiryRepliesByEnquiryId(int enquiryId)
      {
         var enquiryRepliesList = JsonConvert.DeserializeObject<QueryResult<EnquiryReplyModel>>(await enquiryReplyRequest.GetAsync("Enquiry/GetAllEnquiryRepliesByEnquiryId/" + enquiryId));

         return enquiryRepliesList;
      }

      public static async Task<List<EnquiryModel>> GetAllEnquiryRepliesByTenderId(int tenderId)
      {
         var enquiryList = JsonConvert.DeserializeObject<List<EnquiryModel>>(await enquiryRequest.GetAsync("Enquiry/GetAllEnquiryRepliesByTenderId/" + tenderId));

         return enquiryList;
      }

      public static async Task<QueryResult<EnquiryModel>> GetAllEnquiryRepliesByTenderIdAsync(SimpleSearchCretriaModel criteria)
      {
         var enquiryList = JsonConvert.DeserializeObject<QueryResult<EnquiryModel>>(await enquiryRequest.GetAsync("Enquiry/GetAllEnquiryRepliesByTenderId?" + UrlHelper.ToQueryString(criteria)));

         return enquiryList;
      }

      public static async Task<QueryResult<BasicTenderModel>> GetAllTendersHasEnquiryAsync(TenderSearchCriteriaModel criteria)
      {
         var tenderEnquiryList = JsonConvert.DeserializeObject<QueryResult<BasicTenderModel>>(await apiRequest.GetAsync("Tender/GetAllTendersHasEnquiry?" + UrlHelper.ToQueryString(criteria)));

         return tenderEnquiryList;
      }
       
      public static async Task<EnquiryModel> GetEnquiryById(int enquiryId)
      {
         var enquiry = JsonConvert.DeserializeObject<EnquiryModel>(await enquiryRequest.GetAsync("Enquiry/GetEnquiryById/" + enquiryId));

         return enquiry;
      }

      public static async Task<EnquiryReplyModel> GetEnquiryReplyByReplyId(int enquiryReplyId)
      {
         var enquiryReply = JsonConvert.DeserializeObject<EnquiryReplyModel>(await enquiryRequest.GetAsync("Enquiry/GetEnquiryReplyByReplyId/" + enquiryReplyId));

         return enquiryReply;
      }

      public static async Task DeleteEquiryReplyAsync(int id)
      {
         await enquiryReplyRequest.GetAsync("Enquiry/Delete/" + id);
      }
       
      #endregion

      #region Open Offers
      public static async Task<BasicTenderModel> OpenTenderOffersAsync(int id)
      {
         var tender = JsonConvert.DeserializeObject<BasicTenderModel>(await apiRequest.PostAsync("Tender/OpenTenderOffersAsync/" + id, null));
         return tender;
      }
      public static async Task<BasicTenderModel> PreConfirmOpenTenderOffersAsync(int id)
      {
         var tender = JsonConvert.DeserializeObject<BasicTenderModel>(await apiRequest.GetAsync("Tender/PreConfirmOpenTenderOffersAsync/" + id));
         return tender;
      }

      public static async Task<BasicTenderModel> AcceptOpenTenderOffersAsync(int id)
      {
         var tender = JsonConvert.DeserializeObject<BasicTenderModel>(await apiRequest.GetAsync("Tender/AcceptOpenTenderOffersAsync/" + id));
         return tender;
      }

      public static async Task<BasicTenderModel> RejectOpenTenderOffersReportAsync(int id, string rejectionReason)
      {
         var tender = JsonConvert.DeserializeObject<BasicTenderModel>(await apiRequest.PostAsync("Tender/RejectOpenTenderOffersReportAsync/" + id + "/" + rejectionReason, null));
         return tender;
      }
      #endregion Open Offers

      #region Check Offers
      public static async Task<BasicTenderModel> StartTenderCheckingOffersAsync(int id)
      {
         var tender = JsonConvert.DeserializeObject<BasicTenderModel>(await apiRequest.PostAsync("Tender/StartTenderCheckingOffersAsync/" + id, null));
         return tender;
      }
      public static async Task<BasicTenderModel> AcceptCheckTenderOffersAsync(int id)
      {
         var tender = JsonConvert.DeserializeObject<BasicTenderModel>(await apiRequest.GetAsync("Tender/AcceptCheckTenderOffersAsync/" + id));
         return tender;
      }
      public static async Task<BasicTenderModel> RejectCheckTenderOffersReportAsync(int id, string rejectionReason)
      {
         var tender = JsonConvert.DeserializeObject<BasicTenderModel>(await apiRequest.GetAsync("Tender/RejectCheckTenderOffersReportAsync/" + id + "/" + rejectionReason));
         return tender;
      }
      #endregion Check Offers

      #region Award Offers
      public static async Task<BasicTenderModel> AwardTenderOffersAsync(int id)
      {
         var tender = JsonConvert.DeserializeObject<BasicTenderModel>(await apiRequest.PostAsync("Tender/AwardTenderOffersAsync/" + id, null));
         return tender;
      }

      public static async Task<BasicTenderModel> SendAwardTenderToApproveAsync(int id)
      {
         var tender = JsonConvert.DeserializeObject<BasicTenderModel>(await apiRequest.PostAsync("Tender/SendAwardTenderToApprove/" + id, null));
         return tender;
      }
      public static async Task<BasicTenderModel> AcceptAwardTenderOffersAsync(int id)
      {
         var tender = JsonConvert.DeserializeObject<BasicTenderModel>(await apiRequest.PostAsync("Tender/AcceptAwardTenderOffersAsync/" + id, null));
         return tender;
      }
      public static async Task<BasicTenderModel> RejectAwardTenderOffersReportAsync(int id, string rejectionReason)
      {
         var tender = JsonConvert.DeserializeObject<BasicTenderModel>(await apiRequest.PostAsync("Tender/RejectAwardTenderOffersReportAsync/" + id + "/" + rejectionReason, null));
         return tender;
      }
      #endregion Award Offers

      public static async Task<TenderRelationsModel> GetRelationsByIdAsync(int Id)
      {
         var tenderObject = JsonConvert.DeserializeObject<TenderRelationsModel>(await apiRequest.GetAsync("Tender/GetRelationsById/" + Id));

         return tenderObject;
      }

      public static async Task<QuantitiesTablesModel> GetTenderQuantityTablesById(int Id)
      {
         var tenderObject = JsonConvert.DeserializeObject<QuantitiesTablesModel>(await apiRequest.GetAsync("Tender/GetTenderQuantityTablesById/" + Id));

         return tenderObject;
      }

      public static async Task<QuantitiesTablesModel> GetTenderQuantityTablesStepById(int Id)
      {
         var tenderObject = JsonConvert.DeserializeObject<QuantitiesTablesModel>(await apiRequest.GetAsync("Tender/GetTenderQuantityTablesStepById/" + Id));

         return tenderObject;
      }

      public static async Task<QuantitiesTablesModel> GetApprovedTenderQuantityTablesById(int Id)
      {
         var tenderObject = JsonConvert.DeserializeObject<QuantitiesTablesModel>(await apiRequest.GetAsync("Tender/GetApprovedTenderQuantityTablesById/" + Id));

         return tenderObject;
      }

      public static async Task<string> SaveQuantitiesTables(List<QuantityTableModel> tables, int id)
      {
         tables[0].TenderId = id;
         return await quantityApiRequest.PostAsync("Tender/SaveQuantitiesTables", tables);
      }
      public static async Task<string> SaveQuantitiesTablesUpdates(List<QuantityTableModel> tables, int id)
      {
         tables[0].TenderId = id;
         return await quantityApiRequest.PostAsync("Tender/SaveQuantitiesTablesUpdates", tables);
      }
      public static async Task<string> SaveQuantitiesTables(List<TenderAttachmentModel> attachments, int id)
      {
         attachments[0].TenderId = id;
         return await AttachmentsApiRequest.PostAsync("Tender/SaveTenderAttachments", attachments);
      }
      public static async Task<TenderHistoryModel> GetTenderHistoryByUserIdAndTenderIdAndStatusId(int userId, int tenderId, int tenderStatusId)
      {
         var tenderHistory = JsonConvert.DeserializeObject<TenderHistoryModel>(await apiRequest.GetAsync("Tender/GetTenderHistoryByUserIdAndTenderIdAndStatusId/" + userId + "/" + tenderId + "/" + tenderStatusId));
         return tenderHistory;
      }

      public static async Task<TenderRevisionDateModel> GetRevisionDateByTenderId(int tenderId)
      {
         var tenderRevisionDate = JsonConvert.DeserializeObject<
            TenderRevisionDateModel>(await apiRequest.GetAsync("Tender/GetRevisionDateByTenderId/" + tenderId));
         return tenderRevisionDate;
      }

      public static async Task<TenderRevisionDateModel> GetRejectedRevisionDateByTenderId(int tenderId)
      {
         var tenderRevisionDate = JsonConvert.DeserializeObject<
            TenderRevisionDateModel>(await apiRequest.GetAsync("Tender/GetRejectedRevisionDateByTenderId/" + tenderId));
         return tenderRevisionDate;
      }

      public static async Task<QueryResult<TenderRevisionDateModel>> GetTenderRevisionDateBySearchCriteria(TenderRevisionSearchCriteria criteria)
      {

         var tenderRevisionDate = JsonConvert.DeserializeObject<QueryResult<TenderRevisionDateModel>>(
            await apiRequest.GetAsync("Tender/GetTenderRevisionDateBySearchCriteria?" + UrlHelper.ToQueryString(criteria)));

         return tenderRevisionDate;
      }

      public static async Task<QueryResult<TenderRevisionCancelModel>> GetTenderRevisionCancelBySearchCriteria(TenderRevisionSearchCriteria tenderRevisionSearchCriteria)
      {

         var tenderRevisionDate = JsonConvert.DeserializeObject<QueryResult<TenderRevisionCancelModel>>(
            await apiRequest.GetAsync("Tender/GetTenderRevisionCancelBySearchCriteria?" + UrlHelper.ToQueryString(tenderRevisionSearchCriteria)));

         return tenderRevisionDate;
      }

      // Update Tender Status
      //public static async Task<BasicTenderModel> UpdateTenderStatus(int id, int statusId)
      //{
      //   var tender = JsonConvert.DeserializeObject<BasicTenderModel>(await apiRequest.GetAsync("Tender/UpdateTenderStatus/" + id + "/" + statusId));
      //   return tender;
      //}
      public static async Task<BasicTenderModel> ReopenTenderCheckingAsync(int id)
      {
         var tender = JsonConvert.DeserializeObject<BasicTenderModel>(await apiRequest.PostAsync("Tender/ReopenTenderCheckingAsync/" + id, null));
         return tender;
      }
      public static async Task<BasicTenderModel> ReopenTenderAsync(int id)
      {
         var tender = JsonConvert.DeserializeObject<BasicTenderModel>(await apiRequest.PostAsync("Tender/ReopenTenderAsync/" + id, null));
         return tender;
      }

      public static async Task<BasicTenderModel> ReopenTenderAfterCancelAsync(int id)
      {
         var tender = JsonConvert.DeserializeObject<BasicTenderModel>(await apiRequest.PostAsync("Tender/ReopenTenderAfterCancelAsync/" + id, null));
         return tender;
      }
      public static async Task<BasicTenderModel> ReopenTenderUnderUpdateForDatesAsync(int id)
      {
         var tender = JsonConvert.DeserializeObject<BasicTenderModel>(await apiRequest.PostAsync("Tender/ReopenTenderUnderUpdateForDatesAsync/" + id, null));
         return tender;
      }
      public static async Task<BasicTenderModel> SendTenderToApproveAsync(int id)
      {
         var tender = JsonConvert.DeserializeObject<BasicTenderModel>(await apiRequest.PostAsync("Tender/SendTenderToApproveAsync/" + id, null));
         return tender;
      }
      public static async Task<BasicTenderModel> ApproveTenderAsync(int id)
      {
         var tender = JsonConvert.DeserializeObject<BasicTenderModel>(await apiRequest.PostAsync("Tender/ApproveTenderAsync/" + id, null));
         return tender;
      }
      public static async Task<BasicTenderModel> SendTenderToApproveOppenningAsync(int id)
      {
         var tender = JsonConvert.DeserializeObject<BasicTenderModel>(await apiRequest.PostAsync("Tender/SendTenderToApproveOppenningAsync/" + id, null));
         return tender;
      }
      public static async Task<BasicTenderModel> SendTenderToApproveCheckingAsync(int id)
      {
         var tender = JsonConvert.DeserializeObject<BasicTenderModel>(await apiRequest.PostAsync("Tender/SendTenderToApproveCheckingAsync/" + id, null));
         return tender;
      }
      public static async Task<BasicTenderModel> ReopenTenderAwardingAsync(int id)
      {
         var tender = JsonConvert.DeserializeObject<BasicTenderModel>(await apiRequest.PostAsync("Tender/ReopenTenderAwardingAsync/" + id, null));
         return tender;
      }
      public static async Task<BasicTenderModel> ApproveTenderCheckingAsync(int id)
      {
         var tender = JsonConvert.DeserializeObject<BasicTenderModel>(await apiRequest.PostAsync("Tender/ApproveTenderCheckingAsync/" + id, null));
         return tender;
      }

      public static async Task<BasicTenderModel> CancelUpdatesInTenderAsync(int id)
      {
         var tender = JsonConvert.DeserializeObject<BasicTenderModel>(await apiRequest.PostAsync("Tender/CancelUpdatesInTenderAsync/" + id, null));
         return tender;
      }

      //public static async Task<BasicTenderModel> UpdateTenderStatusWithVerification(int id, int statusId, string verificationCode)
      //{
      //   var tender = JsonConvert.DeserializeObject<BasicTenderModel>(await apiRequest.GetAsync("Tender/UpdateTenderStatusWithVerification/" + id + "/" + statusId + "/" + verificationCode));
      //   return tender;
      //}

      public static async Task<BasicTenderModel> ApproveTenderAwardingWithVerificationAsync(int id, string verificationCode)
      {
         var tender = JsonConvert.DeserializeObject<BasicTenderModel>(await apiRequest.PostAsync("Tender/ApproveTenderAwardingWithVerificationAsync/" + id + "/" + verificationCode, null));
         return tender;
      }

      public static async Task<BasicTenderModel> ApproveTenderOppeningWithVerificationAsync(int id, string verificationCode)
      {
         var tender = JsonConvert.DeserializeObject<BasicTenderModel>(await apiRequest.PostAsync("Tender/ApproveTenderOppeningWithVerificationAsync/" + id + "/" + verificationCode, null));
         return tender;
      }

      public static async Task<BasicTenderModel> ApproveTenderCheckingWithVerificationAsync(int id, string verificationCode)
      {
         var tender = JsonConvert.DeserializeObject<BasicTenderModel>(await apiRequest.PostAsync("Tender/ApproveTenderCheckingWithVerificationAsync/" + id + "/" + verificationCode, null));
         return tender;
      }


      public static async Task<BasicTenderModel> ApproveTenderWithInbudgetAsync(int id, string verificationCode, decimal? estimatedValue, bool iagree, bool competitionIsBudgeted)
      {
         var tender = JsonConvert.DeserializeObject<BasicTenderModel>(await apiRequest.PostAsync("Tender/ApproveTenderWithInbudgetAsync/" + id + "/" + verificationCode + "/" + estimatedValue + "/" + iagree + "/" + competitionIsBudgeted, null));
         return tender;
      }
      //public static async Task<BasicTenderModel> UpdateTenderAfterUpdate(int id)
      //{
      //   var tender = JsonConvert.DeserializeObject<BasicTenderModel>(await apiRequest.GetAsync("Tender/UpdateTenderAfterUpdate/" + id + "/" + statusId));
      //   return tender;
      //}

      public static async Task<BasicTenderModel> SendUpdateQuantityTableToApproveAsync(int id)
      {
         var tender = JsonConvert.DeserializeObject<BasicTenderModel>(await apiRequest.PostAsync("Tender/SendUpdateQuantityTableToApproveAsync/" + id, null));
         return tender;
      }
      public static async Task<BasicTenderModel> SendUpdateAttachmentsToApproveAsync(int id)
      {
         var tender = JsonConvert.DeserializeObject<BasicTenderModel>(await apiRequest.PostAsync("Tender/SendUpdateAttachmentsToApproveAsync/" + id, null));
         return tender;
      }
      public static async Task<BasicTenderModel> SendUpdateDatesToApproveAsync(int id)
      {
         var tender = JsonConvert.DeserializeObject<BasicTenderModel>(await apiRequest.PostAsync("Tender/SendUpdateDatesToApproveAsync/" + id, null));
         return tender;
      }
      public static async Task<BasicTenderModel> ApproveTenderUpdateWithVerificationAsync(int id, string verificationCode)
      {
         var tender = JsonConvert.DeserializeObject<BasicTenderModel>(await apiRequest.PostAsync("Tender/ApproveTenderUpdateWithVerificationAsync/" + id + "/" + verificationCode, null));
         return tender;
      }


      // Update Tender Estimated Value
      public static async Task<BasicTenderModel> UpdateTenderEstimatedValueAsync(int tenderId, Decimal? estimatedValue)
      {
         var tender = JsonConvert.DeserializeObject<BasicTenderModel>(await apiRequest.GetAsync("Tender/UpdateTenderEstimatedValue/" + tenderId + "/" + estimatedValue));
         return tender;
      }

      // REject Tender 
      public static async Task<BasicTenderModel> RejectTenderAsync(int id, string rejectionReason)
      {
         var tender = JsonConvert.DeserializeObject<BasicTenderModel>(await apiRequest.PostAsync("Tender/RejectTender/" + id + "/" + rejectionReason, null));
         return tender;
      }

      public static async Task<BasicTenderModel> RejectTenderExtendDateAsync(int id, string verificationCode, string rejectionReason)
      {
         var tender = JsonConvert.DeserializeObject<BasicTenderModel>(await apiRequest.PostAsync("Tender/RejectTenderExtendDateAsync/" + id + "/" + verificationCode + "/" + rejectionReason, null));
         return tender;
      }
      public static async Task<BasicTenderModel> RejectTenderUpdateQuantityTableAsync(int id, string verificationCode, string rejectionReason)
      {
         var tender = JsonConvert.DeserializeObject<BasicTenderModel>(await apiRequest.PostAsync("Tender/RejectTenderUpdateQuantityTableAsync/" + id + "/" + verificationCode + "/" + rejectionReason, null));
         return tender;
      }
      public static async Task<BasicTenderModel> RejectTenderUpdateAttachmentAsync(int id, string verificationCode, string rejectionReason)
      {
         var tender = JsonConvert.DeserializeObject<BasicTenderModel>(await apiRequest.PostAsync("Tender/RejectTenderUpdateAttachmentAsync/" + id + "/" + verificationCode + "/" + rejectionReason, null));
         return tender;
      }
      public static async Task<BasicTenderModel> CreateAsync(BasicTenderModel basicTenderModel)
      {
         string response = await apiRequest.PostAsync("Tender/Add", basicTenderModel);
         var result = JsonConvert.DeserializeObject<BasicTenderModel>(response);
         return result;
      }
      //public static async Task<TenderOffersStepModel> UpdateTenderDetailsAsync(TenderOffersStepModel tenderOffersStepModel)
      //{
      //  string response = await apiRequest.PostAsync("Tender/UpdateTenderDetails", tenderOffersStepModel);
      //  var result = JsonConvert.DeserializeObject<TenderOffersStepModel>(response);
      //   return result;
      //}
      public static async Task UpdateTenderDetailsAsync(TenderOffersStepModel tenderOffersStepModel)
      {
         await detailsRequest.PostAsync("Tender/UpdateTenderDetails", tenderOffersStepModel);
         //var result = JsonConvert.DeserializeObject<TenderOffersStepModel>(response);

      }
      public static async Task UpdateAsync(int id, BasicTenderModel basicTenderModel)
      {
         await apiRequest.PostAsync("Tender/Update/" + id, basicTenderModel);
      }

      public static async Task UpdateRelationsAsync(TenderRelationsModel Model)
      {
         await relationsRequest.PostAsync("Tender/UpdateTenderRelations", Model);
      }

      public static async Task<List<LookupModel>> GetStatusAsync()
      {
         var statusList = JsonConvert.DeserializeObject<List<LookupModel>>(await apiRequest.GetAsync("Tender/GetStatus"));

         return statusList;
      }


      public static async Task<BasicTenderModel> GetByIdAsync(int Id)
      {
         var tenderObject = JsonConvert.DeserializeObject<BasicTenderModel>(await apiRequest.GetAsync("Tender/GetById/" + Id));

         return tenderObject;
      }
      public static async Task<BasicTenderModel> GetTenderByAgencyIdAsync(int AgencyId)
      {
         var tenderObject = JsonConvert.DeserializeObject<BasicTenderModel>(await apiRequest.GetAsync("Tender/GetTenderByAgencyId/" + AgencyId));

         return tenderObject;
      }

      // Delete Tender
      public static async Task DeleteAsync(int id)
      {
         await apiRequest.GetAsync("Tender/Delete/" + id);
      }

      public static async Task<List<AddressModel>> GetAddressesAsync()
      {
         //Deserializing the response recieved from web api and storing into the Tender List  
         var addressList = JsonConvert.DeserializeObject<List<AddressModel>>(await detailsRequest.GetAsync("Tender/GetAllAddresses"));
         return addressList;
      }
      #region details 

      public static async Task<TenderOffersStepModel> GetOfferDetailsByIdAsync(int Id)
      {
         var tenderObject = JsonConvert.DeserializeObject<TenderOffersStepModel>(await apiRequest.GetAsync("Tender/GetOfferDetailsById/" + Id));

         return tenderObject;
      }

      internal static async Task<List<TenderAttachmentModel>> GetAttachmentsByTenderIdAsync(int id)
      {
         var tenderObject = JsonConvert.DeserializeObject<List<TenderAttachmentModel>>(await apiRequest.GetAsync("Tender/GetAttachmentsByTenderId/" + id));

         return tenderObject;
      }
      internal static async Task<List<TenderAttachmentModel>> GetAttachmentsStepByTenderIdAsync(int id)
      {
         var tenderObject = JsonConvert.DeserializeObject<List<TenderAttachmentModel>>(await apiRequest.GetAsync("Tender/GetAttachmentsStepByTenderId/" + id));

         return tenderObject;
      }
      public static async Task<TenderRelationsModel> GetRelationsDetailsByTenderIdAsync(int Id)
      {
         var tenderObject = JsonConvert.DeserializeObject<TenderRelationsModel>(await apiRequest.GetAsync("Tender/GetRelationsDetailsByTenderId/" + Id));

         return tenderObject;
      }

      public static async Task SaveTenderAttachments(List<TenderAttachmentModel> tenderAttachments, int tenderID)
      {
         tenderAttachments[0].TenderId = tenderID;
         await AttachmentsApiRequest.PostAsync("Tender/SaveTenderAttachments", tenderAttachments);
      }
      public static async Task SaveTenderAttachmentsUpdates(List<TenderAttachmentModel> tenderAttachments, int tenderID)
      {
         tenderAttachments[0].TenderId = tenderID;
         await AttachmentsApiRequest.PostAsync("Tender/SaveTenderAttachmentsUpdates", tenderAttachments);
      }

      public static async Task<QueryResult<TenderInvitationDetailsModel>> GetSupplierFavouritTendersListAsync(TenderSearchCriteriaModel tenderSearchCriteria)
      {
         var tenders = JsonConvert.DeserializeObject<QueryResult<TenderInvitationDetailsModel>>(
            await apiRequest.GetAsync("Tender/GetSupplierFavouritTendersList?" + UrlHelper.ToQueryString(tenderSearchCriteria)));

         return tenders;
      }
      public static async Task<QueryResult<BasicTenderModel>> GetTendersReportList(TenderSearchCriteriaModel tenderSearchCriteria)
      {
         var tenders = JsonConvert.DeserializeObject<QueryResult<BasicTenderModel>>(
            await apiRequest.GetAsync("Reports/GetTendersReportList?" + UrlHelper.ToQueryString(tenderSearchCriteria)));

         return tenders;
      }
      public static async Task<QueryResult<TenderCountsModel>> GetTendersCountReportList(TenderSearchCriteriaModel tenderSearchCriteria)
      {
         var tenders = JsonConvert.DeserializeObject<QueryResult<TenderCountsModel>>(
            await apiRequest.GetAsync("Reports/GetTendersCountReportList?" + UrlHelper.ToQueryString(tenderSearchCriteria)));

         return tenders;
      }
      public static async Task<QueryResult<PurshaseModel>> GetTendersPurshasesReportList(TenderSearchCriteriaModel tenderSearchCriteria)
      {
         var tenders = JsonConvert.DeserializeObject<QueryResult<PurshaseModel>>(
            await PurshaseRequest.GetAsync("Reports/GetTendersPurshasesReportList?" + UrlHelper.ToQueryString(tenderSearchCriteria)));

         return tenders;
      }
      public static async Task<QueryResult<BasicTenderModel>> FindSuppliersPurshaseReport(TenderSearchCriteriaModel tenderSearchCriteria)
      {
         var tenders = JsonConvert.DeserializeObject<QueryResult<BasicTenderModel>>(
            await apiRequest.GetAsync("Reports/FindSuppliersPurshaseReport?" + UrlHelper.ToQueryString(tenderSearchCriteria)));

         return tenders;
      }

      public static async Task<AgencyTenderStatisticsModel> FindAgencyStatisticsReport(TenderValueToTypeSearchCriteria tenderSearchCriteria)
      {
         var tenders = JsonConvert.DeserializeObject<AgencyTenderStatisticsModel>(
            await apiRequest.GetAsync("Reports/ReportGetCountsStatistics?" + UrlHelper.ToQueryString(tenderSearchCriteria)));

         return tenders;
      }

      public static async Task<QueryResult<BasicTenderModel>> FindSupplierFavouritTendersListAsync(TenderSearchCriteriaModel criteria)
      {
         //Deserializing the response recieved from web api and storing into the Tender List  
         var tenderList = JsonConvert.DeserializeObject<QueryResult<BasicTenderModel>>(
            await apiRequest.GetAsync("Tender/FindSupplierFavouritTendersBySearchCriteria?" + UrlHelper.ToQueryString(criteria)));
         return tenderList;
      }

      public static async Task AddTenderToSupplierTendersFavouriteList(int tenderId)
      {
         await apiRequest.GetAsync("Tender/AddTenderToSupplierTendersFavouriteList?tenderId=" + tenderId);
      }

      public static async Task DeleteTenderFromSupplierTendersFavouriteList(int tenderId)
      {
         await apiRequest.GetAsync("Tender/DeleteTenderFromSupplierTendersFavouriteList?tenderId=" + tenderId);
      }
      #endregion

      #region User Favourite Suppliers

      public static async Task<QueryResult<SupplierInvitationModel>> GetAllSuppliers(SupplierSearchCretriaModel cretria)
      {
         var result = JsonConvert.DeserializeObject<QueryResult<SupplierInvitationModel>>(
            await apiRequest.GetAsync("Tender/GetAllSuppliers?" + UrlHelper.ToQueryString(cretria)));

         return result;
      }

      public static async Task<List<FavouriteSupplierListModel>> GetFavouriteLists(FavouriteSupplierListModel cretria)
      {
         var result = JsonConvert.DeserializeObject<List<FavouriteSupplierListModel>>(
            await apiRequest.GetAsync("Tender/GetFavouriteListsByAgencyId?" + UrlHelper.ToQueryString(cretria)));

         return result;
      }

      public static async Task<FavouriteSupplierListModel> AddFavouriteSupplierList(FavouriteSupplierListModel cretria)
      {
         var tenders = JsonConvert.DeserializeObject<FavouriteSupplierListModel>(
            await apiRequest.GetAsync("Tender/AddFavouriteSupplierList?" + UrlHelper.ToQueryString(cretria)));

         return tenders;
      }

      public static async Task<bool> DeleteFavouriteSupplierList(FavouriteSupplierListModel cretria)
      {
         var result = JsonConvert.DeserializeObject<bool>(
            await apiRequest.GetAsync("Tender/DeleteFavouriteSupplierList?" + UrlHelper.ToQueryString(cretria)));

         return result;
      }

      public static async Task<QueryResult<SupplierInvitationModel>> GetFavouriteSuppliersByListId(SupplierSearchCretriaModel cretria)
      {
         var result = JsonConvert.DeserializeObject<QueryResult<SupplierInvitationModel>>(
            await apiRequest.GetAsync("Tender/GetFavouriteSuppliersByListId?" + UrlHelper.ToQueryString(cretria)));

         return result;
      }

      public static async Task<bool> AddSupplierToFavouriteSuppliersList(SupplierSearchCretriaModel cretria)
      {
         var result = JsonConvert.DeserializeObject<bool>(
            await apiRequest.PostModelAsync<SupplierSearchCretriaModel>("Tender/AddSupplierToFavouriteSuppliersList", cretria));

         return result;
      }

      public static async Task<bool> DeleteSupplierFromFavouriteList(SupplierSearchCretriaModel cretria)
      {
         var result = JsonConvert.DeserializeObject<bool>(
            await apiRequest.GetAsync("Tender/DeleteSupplierFromFavouriteList?" + UrlHelper.ToQueryString(cretria)));

         return result;
      }

      #endregion

      public static async Task<List<TenderValueToTypesModel>> TenderValueToTypesReport(TenderValueToTypeSearchCriteria searchCriteria)
      {
         var tenders = JsonConvert.DeserializeObject<List<TenderValueToTypesModel>>(await apiRequest.GetAsync("Reports/TenderValueToTypesReport?" + UrlHelper.ToQueryString(searchCriteria)));

         return tenders;
      }
      public static async Task<List<TenderValueToTypesModel>> TenderCountToTypesReport(TenderValueToTypeSearchCriteria searchCriteria)
      {
         var tenders = JsonConvert.DeserializeObject<List<TenderValueToTypesModel>>(await apiRequest.GetAsync("Reports/TenderCountToTypesReport?" + UrlHelper.ToQueryString(searchCriteria)));

         return tenders;
      }
      public static async Task<List<TenderValueToTypesModel>> TendersToAwardedSuppliersReport(TenderValueToTypeSearchCriteria searchCriteria)
      {
         var tenders = JsonConvert.DeserializeObject<List<TenderValueToTypesModel>>(await apiRequest.GetAsync("Reports/TendersToAwardedSuppliersReport?" + UrlHelper.ToQueryString(searchCriteria)));

         return tenders;
      }
      public static async Task<TenderValueToTypesModel> ReportTendersToActivity(TenderValueToTypeSearchCriteria searchCriteria)
      {
         var tenders = JsonConvert.DeserializeObject<TenderValueToTypesModel>(await apiRequest.GetAsync("Reports/ReportTendersToActivity?" + UrlHelper.ToQueryString(searchCriteria)));

         return tenders;
      }
      public static async Task<QueryResult<MostSuppliersHaveTendersModel>> MostSuppliersHaveTenderReport(TenderValueToTypeSearchCriteria searchCriteria)
      {
         var tenders = JsonConvert.DeserializeObject<QueryResult<MostSuppliersHaveTendersModel>>(await apiRequest.GetAsync("Reports/Report_GetMostSuppliersHaveTendersAsync?" + UrlHelper.ToQueryString(searchCriteria)));

         return tenders;
      }
      public static async Task<QueryResult<NotifyModel>> DailyNotificationsList(NotifySearchCriteria searchCriteria)
      {
         var tenders = JsonConvert.DeserializeObject<QueryResult<NotifyModel>>(await apiRequest.GetAsync("Reports/DailyNotificationsList?" + UrlHelper.ToQueryString(searchCriteria)));

         return tenders;
      }

      public static async Task<List<MostTendersActivitiesModel>> ReportGetMostTendersActivitiesAsync(TenderValueToTypeSearchCriteria searchCriteria)
      {
         var tenders = JsonConvert.DeserializeObject<List<MostTendersActivitiesModel>>(await apiRequest.GetAsync("Reports/ReportGetMostTendersActivitiesAsync?" + UrlHelper.ToQueryString(searchCriteria)));

         return tenders;
      }

      public static async Task<List<MostTendersActivitiesModel>> ReportGetMostSuppliersActivitiesAsync(TenderValueToTypeSearchCriteria searchCriteria)
      {
         var tenders = JsonConvert.DeserializeObject<List<MostTendersActivitiesModel>>(await apiRequest.GetAsync("Reports/ReportGetMostSuppliersActivitiesAsync?" + UrlHelper.ToQueryString(searchCriteria)));

         return tenders;
      }
      public static async Task<List<MostSuppliersHaveTendersModel>> ReportGetMostSuppliersHaveTendersAsync(TenderValueToTypeSearchCriteria searchCriteria)
      {
         var tenders = JsonConvert.DeserializeObject<List<MostSuppliersHaveTendersModel>>(await apiRequest.GetAsync("Reports/ReportGetMostSuppliersHaveTendersAsync?" + UrlHelper.ToQueryString(searchCriteria)));

         return tenders;
      }
      public static async Task<List<TendersSalesModel>> ReportGetTendersSalesAsync(TenderValueToTypeSearchCriteria searchCriteria)
      {
         var tenders = JsonConvert.DeserializeObject<List<TendersSalesModel>>(await apiRequest.GetAsync("Reports/ReportGetTendersSalesAsync?" + UrlHelper.ToQueryString(searchCriteria)));

         return tenders;
      }
      public static async Task<List<TendersPublishingModel>> ReportGetPublishedTendersAsync(TenderValueToTypeSearchCriteria searchCriteria)
      {
         var tenders = JsonConvert.DeserializeObject<List<TendersPublishingModel>>(await apiRequest.GetAsync("Reports/ReportGetPublishedTendersAsync?" + UrlHelper.ToQueryString(searchCriteria)));

         return tenders;
      }
      public static async Task<List<TenderCountsModel>> ReportGetTendersCountReportAsync(TenderSearchCriteriaModel tenderSearchCriteria)
      {
         var tenders = JsonConvert.DeserializeObject<List<TenderCountsModel>>(
            await apiRequest.GetAsync("Reports/ReportGetTendersCountReportAsync?" + UrlHelper.ToQueryString(tenderSearchCriteria)));

         return tenders;
      }
      public static async Task<TenderDetialsReportModel> OpenTenderDetailsReport(int tenderId)
      {
         var result = JsonConvert.DeserializeObject<TenderDetialsReportModel>(
            await apiRequest.GetAsync("Tender/OpenTenderDetailsReport/" + tenderId));
         return result;
      }
      public static async Task<TenderDetialsReportModel> OpenTenderAdvDetailsReport(int tenderId)
      {
         var result = JsonConvert.DeserializeObject<TenderDetialsReportModel>(
            await apiRequest.GetAsync("Tender/OpenTenderAdvDetailsReport/" + tenderId));
         return result;
      }
      public static async Task<AwardingReportModel> AwardingReport(int tenderId)
      {
         var result = JsonConvert.DeserializeObject<AwardingReportModel>(
            await apiRequest.GetAsync("Tender/AwardingReport/" + tenderId));
         return result;
      }
      public static async Task<AwardingReportModel> PrintTenderReceiptReport(int tenderId, int SupplierId)
      {
         var result = JsonConvert.DeserializeObject<AwardingReportModel>(
            await apiRequest.GetAsync("Tender/PrintTenderReceiptReport?tenderId=" + tenderId + "&SupplierId=" + SupplierId));
         return result;
      }

      public static async Task<OfferReportModel> OffersReport(int tenderId)
      {
         var result = JsonConvert.DeserializeObject<OfferReportModel>(
            await apiRequest.GetAsync("Tender/OffersReport/" + tenderId));
         return result;
      }

      public static async Task<CountAndCloseAppliedOffersModel> CountAndCloseAppliedOffers(int tenderId)
      {
         var result = JsonConvert.DeserializeObject<CountAndCloseAppliedOffersModel>(
            await apiRequest.GetAsync("Tender/CountAndCloseAppliedOffers/" + tenderId));
         return result;
      }
      public static async Task<List<DirectInvitationsReportModel>> DirectInvitationsReport(TenderValueToTypeSearchCriteria searchCriteria)
      {
         var tenders = JsonConvert.DeserializeObject<List<DirectInvitationsReportModel>>(await apiRequest.GetAsync("Reports/DirectInvitationsReport?" + UrlHelper.ToQueryString(searchCriteria)));

         return tenders;
      }

      public static async Task<TenderRevisionCancelModel> CreateTenderCancelRequestAsync(String tenderIdString)
      {
         ApiRequest<TenderRevisionCancelModel> iAPIRequest = new ApiRequest<TenderRevisionCancelModel>(_CurrentContext);
         var result = JsonConvert.DeserializeObject<TenderRevisionCancelModel>(await iAPIRequest.PostAsync("Tender/CreateTenderCancelRequestAsync", new TenderRevisionCancelModel { TenderIDString = tenderIdString }));
         return result;
      }

      public static async Task<TenderRevisionCancelModel> GetTenderRevisionCancelByTenderId(int tenderId)
      {
         var tenderRevisionCancelModel = JsonConvert.DeserializeObject<
            TenderRevisionCancelModel>(await apiRequest.GetAsync("Tender/GetTenderRevisionCancelByTenderId/" + tenderId));
         return tenderRevisionCancelModel;
      }

      public static async Task<bool> ApproveTenderCancelRequestAsync(int id, string verificationCode)
      {
         var result = JsonConvert.DeserializeObject<bool>(await apiRequest.PostAsync("Tender/ApproveTenderCancelRequestAsync/" + id + "/" + verificationCode, null));
         return result;
      }

      public static async Task<bool> RejectTenderCancelRequestAsync(int id, string rejectionReason)
      {
         var result = JsonConvert.DeserializeObject<bool>(await apiRequest.PostAsync("Tender/RejectTenderCancelRequestAsync/" + id + "/" + rejectionReason, null));
         return result;
      }

   }
}
