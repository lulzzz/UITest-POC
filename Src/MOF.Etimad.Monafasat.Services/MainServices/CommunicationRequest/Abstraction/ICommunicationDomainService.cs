using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public interface ICommunicationDomainService
    {
        Task<List<Tender>> GetPostqualificationOnTenderForCancel(int tenderId);

        void IsValidToCreatePlain(Tender tender, string selectedCR);
        Task IsValidToCreateRequest(SupplierExtendOfferDatesRequest supplierExtendOfferDatesRequest, DateTime requestedDate, Tender tender);
        Task IsValidToGetSupplierPlain(TenderPlaintCommunicationRequestModel tender);
        Task IsValidToCheckPlain(PlaintRequestModel plaint);
        Task IsValidToSendPlaintDecission(TenderPLaintCommunicationModel model);
        Task IsValidToManagerToChecklPlaint(TenderPLaintCommunicationModel model);
        Task IsValidToManagerToChecklEscalation(TenderEscalatedPLaintModel model);
        void IsValidToAcceptExtendOffersValidityBySupplier(ExtendOffersValiditySupplier supplierExtendOffersValidity);
        void IsValidToRejectExtendOffersValidityBySupplier(ExtendOffersValiditySupplier supplierExtendOffersValidity);
        void IsValidToUpdateModel(ExtendOfferPresentationDatesModel tenderDatesModel, decimal? estimatedValue);

        #region ExtendOfersValidity
        void IsValidToRejectExtendOffersRequest(AgencyCommunicationRequest request);
        void IsValidToDeleteExtendOffersRequest(AgencyCommunicationRequest request);
        #endregion

        #region Extend offer presentation dates
        void IsValidToApproveSupplierExtendOfferDatesRequest(AgencyCommunicationRequest request);
        void IsValidToRejectSupplierExtendOfferDatesRequest(AgencyCommunicationRequest request);
        #endregion Extend offers presentation dates


        Task<bool> IsSupplierFailedInPostqualification(int tenderId, string cr);
        Task<bool> IsTenderHasActivePostqualification(int tenderId);
    }
}
