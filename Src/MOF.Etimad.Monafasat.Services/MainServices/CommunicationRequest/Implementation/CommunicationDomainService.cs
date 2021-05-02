using Microsoft.AspNetCore.Http;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernal.Exceptions;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.SharedKernel.Exceptions;
using MOF.Etimad.Monafasat.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public class CommunicationDomainService : ICommunicationDomainService
    {
        private readonly IOfferQueries _offerQueries;
        private readonly ITenderQueries _tenderQueries;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CommunicationDomainService(IHttpContextAccessor httpContextAccessor, IOfferQueries offerQueries, ITenderQueries tenderQueries)
        {
            _httpContextAccessor = httpContextAccessor;
            _offerQueries = offerQueries;
            _tenderQueries = tenderQueries;
        }

        public void IsValidToCreatePlain(Tender tender, string selectedCR)
        {
            DateTime awardingDate = tender.TenderHistories.Where(a => a.StatusId == (int)Enums.TenderStatus.OffersAwardedConfirmed).OrderByDescending(a => a.TenderHistoryId).Select(a => a.CreatedAt).FirstOrDefault();

            Offer offer = tender.Offers.FirstOrDefault(a => a.CommericalRegisterNo.Equals(selectedCR) && a.IsActive == true);
            if (offer == null)
            {
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.UnexpectedError);
            }
            else if (offer.PlaintRequests.FirstOrDefault() != null)
            {
                throw new UnHandledAccessException(Resources.TenderResources.ErrorMessages.UnexpectedError);
            }
            if (DateTime.Now < awardingDate || DateTime.Now > awardingDate.AddDays(offer.Tender.AwardingStoppingPeriod.Value))
            {
                throw new UnHandledAccessException(Resources.TenderResources.ErrorMessages.UnexpectedError);
            }
        }
        public async Task IsValidToGetSupplierPlain(TenderPlaintCommunicationRequestModel tender)
        {
            if (tender == null)
            {
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.UnexpectedError);
            }
            if ((DateTime.Now < tender.TenderAwardingDate || DateTime.Now > tender.TenderAwardingDate.Value.AddDays(tender.AwardingStoppingPeriod.Value)) /*|| tender.PlaintRequestId != 0*/)
            {
                throw new UnHandledAccessException(Resources.TenderResources.ErrorMessages.UnexpectedError);
            }
        }

        public async Task IsValidToManagerToChecklPlaint(TenderPLaintCommunicationModel model)
        {
            if (model == null)
            {
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.UnexpectedError);
            }
            if (!model.CanManagerTakeAction || (model.StatusId != (int)Enums.AgencyCommunicationRequestStatus.Pending))
            {
                throw new UnHandledAccessException(Resources.TenderResources.ErrorMessages.UnexpectedError);
            }
        }
        public async Task IsValidToManagerToChecklEscalation(TenderEscalatedPLaintModel model)
        {
            if (model == null)
            {
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.UnexpectedError);
            }
            if (!model.CanManagerTakeAction || (model.EscalationStatusId != (int)Enums.AgencyCommunicationRequestStatus.Pending))
            {
                throw new UnHandledAccessException(Resources.TenderResources.ErrorMessages.UnexpectedError);
            }
        }
        public async Task IsValidToCheckPlain(PlaintRequestModel plaint)
        {
            if (plaint == null)
            {
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.UnexpectedError);
            }
        }
        public async Task IsValidToSendPlaintDecission(TenderPLaintCommunicationModel model)
        {
            if (model == null)
            {
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.UnexpectedError);
            }
            if (!model.CanSecretaryTakeAction || (model.StatusId != (int)Enums.AgencyCommunicationRequestStatus.RequestSent && model.StatusId != (int)Enums.AgencyCommunicationRequestStatus.Rejected))
            {
                throw new UnHandledAccessException(Resources.TenderResources.ErrorMessages.UnexpectedError);
            }
        }


        public void IsValidToAcceptExtendOffersValidityBySupplier(ExtendOffersValiditySupplier supplierExtendOffersValidity)
        {
            if (supplierExtendOffersValidity == null)
                throw new NullReferenceException(Resources.TenderResources.ErrorMessages.UnexpectedError);
            if (supplierExtendOffersValidity.SupplierExtendOfferValidityStatusId != (int)Enums.SupplierExtendOffersValidityStatus.UnderProcessing)
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.UnexpectedError);
        }
        public void IsValidToRejectExtendOffersValidityBySupplier(ExtendOffersValiditySupplier supplierExtendOffersValidity)
        {
            if (supplierExtendOffersValidity == null)
                throw new NullReferenceException(Resources.TenderResources.ErrorMessages.UnexpectedError);
            if (supplierExtendOffersValidity.SupplierExtendOfferValidityStatusId != (int)Enums.SupplierExtendOffersValidityStatus.UnderProcessing)
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.UnexpectedError);
        }
        public void IsValidToUpdateModel(ExtendOfferPresentationDatesModel tenderDatesModel, decimal? estimatedValue)
        {
            if (tenderDatesModel == null)
            {
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.TenderNotExist);
            }
            CheckTenderDatesNullability(tenderDatesModel);
            CheckLastEnqueriesDate(tenderDatesModel);
            CheckOffersOpeningDate(tenderDatesModel);
            CheckOffersCheckingDate(tenderDatesModel);

        }
        private void CheckTenderDatesNullability(ExtendOfferPresentationDatesModel tenderDatesModel)
        {
            if (tenderDatesModel.LastEnqueriesDate == null)
            {
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.EnterLastEnqueriesDate);
            }
            if (tenderDatesModel.LastOfferPresentationDate == null)
            {
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.EnterLastOfferPresentationDate + "\n");
            }
            if (tenderDatesModel.OffersOpeningDate == null && tenderDatesModel.TenderTypeId != (int)Enums.TenderType.FirstStageTender && tenderDatesModel.TenderTypeId != (int)Enums.TenderType.PostQualification && tenderDatesModel.TenderTypeId != (int)Enums.TenderType.PreQualification && tenderDatesModel.TenderTypeId != (int)Enums.TenderType.NewDirectPurchase && tenderDatesModel.TenderTypeId != (int)Enums.TenderType.Competition)
            {
                throw new BusinessRuleException(Resources.SharedResources.ErrorMessages.EnterOffersOpeningDate + "\n");
            }
            if ((tenderDatesModel.TenderTypeId == (int)Enums.TenderType.NewDirectPurchase || tenderDatesModel.TenderTypeId == (int)Enums.TenderType.FirstStageTender) && tenderDatesModel.OffersCheckingDate == null)
            {
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.EnterOffersCheckingData);
            }
        }
        private void CheckLastEnqueriesDate(ExtendOfferPresentationDatesModel tenderDatesModel)
        {
            if (tenderDatesModel.LastEnqueriesDate.HasValue)
            {
                if (tenderDatesModel.LastEnqueriesDate.Value.Date < DateTime.Now)
                {
                    throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.LastEnqueriesDateLessThanToday);
                }
                if (tenderDatesModel.LastOfferPresentationDate.HasValue)
                {
                    if (tenderDatesModel.LastEnqueriesDate.Value.Date >= tenderDatesModel.LastOfferPresentationDate.Value.Date)
                    {
                        throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.EnquiryDateLargerThanPresentationDate);
                    }
                }
            }
        }

        private void CheckOffersOpeningDate(ExtendOfferPresentationDatesModel tenderDatesModel)
        {
            if (tenderDatesModel.OffersOpeningDate.HasValue)
            {
                if (tenderDatesModel.LastEnqueriesDate.HasValue)
                {
                    if (tenderDatesModel.OffersOpeningDate.Value <= tenderDatesModel.LastEnqueriesDate.Value)
                    {
                        throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.OpeningDateLessThanEnquiriesDate);
                    }
                }
                if (tenderDatesModel.LastOfferPresentationDate.HasValue)
                {
                    if (tenderDatesModel.OffersOpeningDate.Value <= tenderDatesModel.LastOfferPresentationDate.Value)
                    {
                        throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.OpeningDateLessThanPresentationDate);
                    }
                }
            }
        }
        private void CheckOffersCheckingDate(ExtendOfferPresentationDatesModel tenderDatesModel)
        {
            if (tenderDatesModel.OffersCheckingDate.HasValue)
            {
                if (tenderDatesModel.LastOfferPresentationDate.HasValue)
                {
                    if (tenderDatesModel.OffersCheckingDate.Value <= tenderDatesModel.LastOfferPresentationDate.Value)
                    {
                        throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.InvalidOffersCheckingDate);
                    }
                }
                if (tenderDatesModel.OffersOpeningDate.HasValue)
                {
                    if (tenderDatesModel.OffersCheckingDate.Value <= tenderDatesModel.OffersOpeningDate.Value)
                    {
                        throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.CheckingDateGreaterThanOpeining);
                    }
                }
            }
        }

        #region Extend Offers Validity
        public void IsValidToRejectExtendOffersRequest(AgencyCommunicationRequest request)
        {
            if (request == null)
                throw new BusinessRuleException(Resources.CommunicationRequest.ErrorMessages.RequestNotFound);
            if (request.StatusId != (int)Enums.AgencyCommunicationRequestStatus.Pending)
            {
                var RequestStatus = Resources.CommunicationRequest.Messages.ResourceManager.GetString(nameof(Enums.AgencyCommunicationRequestStatus.Pending));
                throw new BusinessRuleException(Resources.CommunicationRequest.ErrorMessages.CanNotProceedStatusIs + RequestStatus);
            }
            if (request.Tender.AgencyCode != _httpContextAccessor.HttpContext.User.UserAgency())
            {
                throw new UnHandledAccessException();
            }
        }
        public void IsValidToDeleteExtendOffersRequest(AgencyCommunicationRequest request)
        {
            if (request == null)
                throw new BusinessRuleException(Resources.CommunicationRequest.ErrorMessages.RequestNotFound);
            if (request.StatusId != (int)Enums.AgencyCommunicationRequestStatus.Rejected && request.StatusId != (int)Enums.AgencyCommunicationRequestStatus.UnderUpdate)
            {
                var RequestStatus = Resources.CommunicationRequest.Messages.ResourceManager.GetString(nameof(Enums.AgencyCommunicationRequestStatus.Rejected));
                throw new BusinessRuleException(Resources.CommunicationRequest.ErrorMessages.CanNotProceedStatusIs + RequestStatus);
            }
            if (request.Tender.AgencyCode != _httpContextAccessor.HttpContext.User.UserAgency())
            {
                throw new UnHandledAccessException();
            }
        }

        #endregion

        #region Extend offers presentation dates
        public void IsValidToApproveSupplierExtendOfferDatesRequest(AgencyCommunicationRequest request)
        {
            if (request == null)
                throw new BusinessRuleException(Resources.CommunicationRequest.ErrorMessages.RequestNotFound);
            if (request.StatusId != (int)Enums.AgencyCommunicationRequestStatus.RequestSent)
            {
                var RequestStatus = Resources.CommunicationRequest.Messages.ResourceManager.GetString(nameof(Enums.AgencyCommunicationRequestStatus.RequestSent));
                throw new BusinessRuleException(Resources.CommunicationRequest.ErrorMessages.CanNotProceedStatusIs + RequestStatus);
            }

        }
        public void IsValidToRejectSupplierExtendOfferDatesRequest(AgencyCommunicationRequest request)
        {
            if (request == null)
                throw new BusinessRuleException(Resources.CommunicationRequest.ErrorMessages.RequestNotFound);
            if (request.StatusId != (int)Enums.AgencyCommunicationRequestStatus.RequestSent)
            {
                var RequestStatus = Resources.CommunicationRequest.Messages.ResourceManager.GetString(nameof(Enums.AgencyCommunicationRequestStatus.RequestSent));
                throw new BusinessRuleException(Resources.CommunicationRequest.ErrorMessages.CanNotProceedStatusIs + RequestStatus);
            }
        }

        public async Task IsValidToCreateRequest(SupplierExtendOfferDatesRequest supplierExtendOfferDatesRequest, DateTime requestedDate, Tender tender)
        {
            if (supplierExtendOfferDatesRequest != null)
                throw new BusinessRuleException("عفوا لا يمكن اضافة اكثر من طلب");

            if (requestedDate <= DateTime.Now || requestedDate.Date <= tender.LastOfferPresentationDate.Value.Date)
            {
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.TenderOfferCannotBeTodayOrLessThanOffer);
            }
        }
        #endregion Extend offers presentation dates


        public async Task<List<Tender>> GetPostqualificationOnTenderForCancel(int tenderId)
        {
            var postqualifications = await _offerQueries.GetPostqualificationOnTenderForCancel(tenderId);
            return postqualifications;
        }




        public async Task<bool> IsSupplierFailedInPostqualification(int tenderId, string cr)
        {
            var result = await _tenderQueries.IsSupplierFailedInPostqualification(tenderId, cr);
            return result;
        }

        public async Task<bool> IsTenderHasActivePostqualification(int tenderId)
        {
            var result = await _tenderQueries.IsTenderHasActivePostqualification(tenderId);
            return result;
        }

    }
}
