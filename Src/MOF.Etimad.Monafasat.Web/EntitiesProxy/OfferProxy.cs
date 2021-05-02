using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.ViewModel.Offer;
using MOF.Etimad.Monafasat.Web.Infrastructure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.EntitiesProxy
{
   public class OfferProxy
   {
      private static ApiRequest<BasicTenderModel> apiRequest;
      private static ApiRequest<OfferModel> offerRequest;
      private static ApiRequest<object> offerObjectRequest;
      private static ApiRequest<OfferCheckingModel> offercheck;
      private static ApiRequest<OfferAwardingModel> offerAwarding;
      private static ApiRequest<List<TenderCheckOfferModel>> tenderCheckOfferModelRequest;
      private static ApiRequest<SaveTableModel> offerRequestSaveTable;
      private static ApiRequest<SupplierAttachmentModel> offerRequestSupplierAttachment;
      private static ApiRequest<OfferAttachmentsModel> offerSuppAttachment;
      private static ApiRequest<SupplierBankGuaranteeModel> offerBankAttachment;
      private static ApiRequest<SupplierSpecificationModel> offerspecificationAttachment;
      public static void Initialize(ControllerContext currentContext)
      {
         apiRequest = new ApiRequest<BasicTenderModel>(currentContext);
         offerRequest = new ApiRequest<OfferModel>(currentContext);
         offerRequestSaveTable = new ApiRequest<SaveTableModel>(currentContext);
         offercheck = new ApiRequest<OfferCheckingModel>(currentContext);
         offerBankAttachment = new ApiRequest<SupplierBankGuaranteeModel>(currentContext);
         offerspecificationAttachment = new ApiRequest<SupplierSpecificationModel>(currentContext);
         offerRequestSupplierAttachment = new ApiRequest<SupplierAttachmentModel>(currentContext);
         offerSuppAttachment = new ApiRequest<OfferAttachmentsModel>(currentContext);
         tenderCheckOfferModelRequest = new ApiRequest<List<TenderCheckOfferModel>>(currentContext);
         offerObjectRequest = new ApiRequest<object>(currentContext);
         offerAwarding = new ApiRequest<OfferAwardingModel>(currentContext);

      }

      public static async Task<QueryResult<BasicTenderModel>> GetAsync(TenderSearchCriteriaModel criteria)
      {
         //Deserializing the response recieved from web api and storing into the Tender List  
         var tenderList = JsonConvert.DeserializeObject<QueryResult<BasicTenderModel>>(await apiRequest.GetAsync("Tender/GetTendersBySearchCriteria?" + UrlHelper.ToQueryString(criteria)));
         return tenderList;
      }
      public static async Task<OfferModel> GetQuantityTablesByTenderId(int tenderId)
      {
         //Deserializing the response recieved from web api and storing into the Tender List  
         var tenderList = JsonConvert.DeserializeObject<OfferModel>(await apiRequest.GetAsync("Offer/GetQuantityTablesByTenderId/" + tenderId));
         return tenderList;
      }
      public static async Task<OfferModel> GetQuantityTablesByTenderAndOffer(int tenderId, int offerId)
      {
         //Deserializing the response recieved from web api and storing into the Tender List  
         var tenderList = JsonConvert.DeserializeObject<OfferModel>(await apiRequest.GetAsync("Offer/GetQuantityTablesByTenderAndOffer/" + tenderId + "/" + offerId));
         return tenderList;
      }
      public static async Task<QueryResult<BasicTenderModel>> GetSuppliersReportByTenderIdAsync(int tenderId)
      {
         var tenderObject = JsonConvert.DeserializeObject<QueryResult<BasicTenderModel>>(await offerRequest.GetAsync("Offer/GetSuppliersReportByTenderId/" + tenderId));
         return tenderObject;//
      }

      public static async Task<OfferModel> SaveTable(SaveTableModel table)
      {
         var result = JsonConvert.DeserializeObject < OfferModel> (await offerRequestSaveTable.PostAsync("offer/SaveTable", table));
         return result;
      }
      public static async Task<OfferModel> AddSupplierOriginalAttachment(List<SupplierAttachmentModel> model, params string[] paramters)
      {
         var result = JsonConvert.DeserializeObject<OfferModel>(await offerRequestSupplierAttachment.PostListAsyncWithPramAsync("offer/AddSupplierOriginalAttachment", model, paramters));
         return result;
      }
      public static async Task<OfferModel> DeleteSupplierOriginalAttachment(SupplierAttachmentModel model)
      {
         var result = JsonConvert.DeserializeObject<OfferModel>(await offerRequestSupplierAttachment.PostAsync("offer/DeleteSupplierOriginalAttachment", model));
         return result;
      }
      public static async Task<bool> SendOffer(int offerId)
      {
         var result = await offerObjectRequest.PostAsync("offer/SendOffer", offerId);
         return true;
      }


      public static async Task<OfferFinancialDetailsModel> OfferFinancialDetails(int OfferId)
      {
         var result = JsonConvert.DeserializeObject<OfferFinancialDetailsModel>(await offerRequestSaveTable.GetAsync("offer/OfferFinancialDetails/" + OfferId));
         return result;
      }

      public static async Task<OfferCheckingModel> UpdateOfferStatus(OfferCheckingModel offerModel)
      {
         var offer = JsonConvert.DeserializeObject<OfferCheckingModel>(await offercheck.PostAsync("Offer/OfferCheckingStatus/", offerModel));
         return offer;
      }

      public static async Task<OfferCheckingModel> UpdateOfferAwardingStatusAsync(OfferCheckingModel offerModel)
      {
         var offer = JsonConvert.DeserializeObject<OfferCheckingModel>(await offercheck.PostAsync("Offer/UpdateOfferAwardingStatus/", offerModel));
         return offer;
      }
      public static async Task<OfferModel> GetOfferById(int offerId)
      {
         var offer = JsonConvert.DeserializeObject<OfferModel>(await offerRequest.GetAsync("Offer/GetOfferById/" + offerId));
         return offer;
      }

      public static async Task<OfferAttachmentsModel> GetOfferAttachmentsDetails(int offerId)
      {
         var offer = JsonConvert.DeserializeObject<OfferAttachmentsModel>(await offerRequest.GetAsync("Offer/GetOfferAttachmentsDetails/" + offerId));
         return offer;
      }
      public static async Task<bool> AddBankGuaranrtee(SupplierBankGuaranteeModel att)
      {
         var result = await offerBankAttachment.PostAsync("Offer/AddBankGuaranrtee", att);
         return true;
      }

      public static async Task<bool> AddOfferAttachmentsDetails(OfferAttachmentsModel model)
      {
         var result = await offerSuppAttachment.PostAsync("Offer/AddOfferAttachmentsDetails", model);
         return true;
      }
      public static async Task<bool> AddClassification(SupplierSpecificationModel att)
      {
         var result = await offerspecificationAttachment.PostAsync("Offer/AddClassification", att);
         return true;
      }

      public static async Task<QueryResult<OfferFinancialDetailsModel>> GetOffersForAwardingByTenderIdAsync(int tenderId)
      {
         var invitationsOffers = JsonConvert.DeserializeObject<QueryResult<OfferFinancialDetailsModel>>(await apiRequest.GetAsync("Offer/GetOffersForAwardingByTenderId/" + tenderId));

         return invitationsOffers;
      }
      public static async Task<QueryResult<OfferFinancialDetailsModel>> OffersOpeningReport(int tenderId)
      {
         var offers = JsonConvert.DeserializeObject<QueryResult<OfferFinancialDetailsModel>>(await apiRequest.GetAsync("Offer/OffersOpeningReport/" + tenderId));

         return offers;
      }
      public static async Task<QueryResult<OfferFinancialDetailsModel>> GetAwardedOffersByTenderIdAsync(int tenderId)
      {
         var invitationsOffers = JsonConvert.DeserializeObject<QueryResult<OfferFinancialDetailsModel>>(await apiRequest.GetAsync("Offer/GetAwardedOffersByTenderId/" + tenderId));

         return invitationsOffers;
      }

      public static async Task SaveOfferAwardingValuesAsync(OfferAwardingModel offerAwardingObj)
      {
         await offerAwarding.PostAsync("Offer/SaveOfferAwardingValues", offerAwardingObj);

      }

      public static async Task<List<OfferDeatilsReportForTenderModel>> OpenOffersReport(int tenderId)
      {
         var result = JsonConvert.DeserializeObject<List<OfferDeatilsReportForTenderModel>>(
            await offerAwarding.GetAsync("Offer/OfferDeatilsReportForTender/" + tenderId));
         return result;
      }

   }
}
