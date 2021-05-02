using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.TestsBuilders.TenderDefaults;
using MOF.Etimad.Monafasat.ViewModel;
using System;
using System.Collections.Generic;
using System.Reflection;
using MOF.Etimad.Monafasat.Core;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernel;

namespace MOF.Etimad.Monafasat.TestsBuilders.CommunicationRequestDefaults
{
    public class CommunicationRequestDefault
    {

        public readonly int agencyRequestId = 1;
        public readonly int tenderId = 1;
        public readonly int statusId = 1;
        public readonly string requestedByRoleName = "NewMonafasat_OffersCheckSecretary";
        public readonly int agencyRequestTypeId = 4;
        public readonly int extendOffersValidityId = 1;
        public readonly int offersDuration = 11;
        public readonly string extendOffersReason = "extendOffersReason";
        public readonly int replyReceivingDurationDays = 1;
        public readonly string replyReceivingDurationTime = "5:00 pm";
        public readonly int tenderTypeId = 6;
        public readonly string _agencyCode = "022001000000";
        const string _Cr = "1299659801";
        public AgencyCommunicationRequest GetGeneralAgencyCommunicationRequest()
        {
            AgencyCommunicationRequest generalAgencyCommunicationRequest = new AgencyCommunicationRequest(agencyRequestId, tenderId, agencyRequestTypeId, statusId, requestedByRoleName);
            return generalAgencyCommunicationRequest;
        }

        public AgencyCommunicationRequest GetAgencyCommunicationRequestById()
        {
            AgencyCommunicationRequest generalAgencyCommunicationRequest = new AgencyCommunicationRequest(agencyRequestId, tenderId, agencyRequestTypeId, statusId, requestedByRoleName);
            generalAgencyCommunicationRequest.AddAgencyCommunicationRequestValidity(extendOffersValidityId, offersDuration, extendOffersReason, replyReceivingDurationDays, replyReceivingDurationTime);
            generalAgencyCommunicationRequest.AddTender(tenderId, tenderTypeId, "TenderName", "1", "", 1, 1, _agencyCode, 1);
            generalAgencyCommunicationRequest.AddEscalationAcceptanceStatusForUnitTest();
            generalAgencyCommunicationRequest.EscalationAcceptanceStatus.SetIdAndNameForUnitTest((int)Enums.AgencyPlaintStatus.Accepted, "Accepted");
            return generalAgencyCommunicationRequest;
        }

        public List<AgencyCommunicationRequest> GetAgencyCommunicationRequests()
        {
            Tender tender = new TenderDefault().GetGeneralTender();
            List<AgencyCommunicationRequest> generalAgencyCommunicationRequests = new List<AgencyCommunicationRequest>();
            var communicationRequest = new AgencyCommunicationRequest(agencyRequestId, tenderId, agencyRequestTypeId, statusId, requestedByRoleName);
            communicationRequest.AddTender(tender);
            communicationRequest.UpdatePlaintAgencyCommunicationRequest(1, "no");
            communicationRequest.UpdateAgencyCommunicationRequestStatus(1, "");
            communicationRequest.UpdateAgencyCommunicationPlaintRequestStatus(1, 1, null, "", "");
            AgencyCommunicationPlaintStatus PlaintAcceptanceStatus = new AgencyCommunicationPlaintStatus();
            typeof(AgencyCommunicationPlaintStatus).GetProperty(nameof(AgencyCommunicationPlaintStatus.Name)).SetValue(PlaintAcceptanceStatus, "name");

            typeof(AgencyCommunicationRequest).GetProperty(nameof(AgencyCommunicationRequest.PlaintAcceptanceStatus)).SetValue(communicationRequest, PlaintAcceptanceStatus);
            communicationRequest.Negotiations = new List<Negotiation>();
            generalAgencyCommunicationRequests.Add(communicationRequest);

            var communicationRequest2 = new AgencyCommunicationRequest(2, 2, agencyRequestTypeId, statusId, requestedByRoleName);
            communicationRequest2.AddTender(tender);
            communicationRequest2.Negotiations = new List<Negotiation>();
            generalAgencyCommunicationRequests.Add(communicationRequest2);
            return generalAgencyCommunicationRequests;
        }

        public List<AgencyCommunicationRequest> GetAgencyCommunicationRequestsForDirectPurchase()
        {
            Tender tender = new TenderDefault().GetNewDirectPurchasePrivate();
            List<AgencyCommunicationRequest> generalAgencyCommunicationRequests = new List<AgencyCommunicationRequest>();
            var communicationRequest = new AgencyCommunicationRequest(agencyRequestId, tenderId, agencyRequestTypeId, statusId, requestedByRoleName);
            communicationRequest.AddTender(tender);
            communicationRequest.UpdatePlaintAgencyCommunicationRequest(1, "no");
            communicationRequest.UpdateAgencyCommunicationRequestStatus(1, "");
            communicationRequest.UpdateAgencyCommunicationPlaintRequestStatus(1, 1, null, "", "");
            AgencyCommunicationPlaintStatus PlaintAcceptanceStatus = new AgencyCommunicationPlaintStatus();
            typeof(AgencyCommunicationPlaintStatus).GetProperty(nameof(AgencyCommunicationPlaintStatus.Name)).SetValue(PlaintAcceptanceStatus, "name");

            typeof(AgencyCommunicationRequest).GetProperty(nameof(AgencyCommunicationRequest.PlaintAcceptanceStatus)).SetValue(communicationRequest, PlaintAcceptanceStatus);

            generalAgencyCommunicationRequests.Add(communicationRequest);

            var communicationRequest2 = new AgencyCommunicationRequest(2, 2, agencyRequestTypeId, statusId, requestedByRoleName);
            communicationRequest2.AddTender(tender);
            generalAgencyCommunicationRequests.Add(communicationRequest2);
            return generalAgencyCommunicationRequests;
        }

        public List<Offer> GetDataIdenticalAndAcceptedOffersByTenderId()
        {
            List<Offer> offers = new List<Offer>();
            return offers;
        }

        public List<Offer> GetExecludedOffers()
        {
            List<Offer> offers = new List<Offer>();
            Offer offer1 = new Offer(1, "1299659801", null, 1, false);
            offer1.ExtendOffersValiditySupplier = new ExtendOffersValiditySupplier(1, DateTime.Now, 1, "121212", 1);
            offers.Add(offer1);
            return offers;
        }

        public ExtendOffersValiditySupplier GetExtendOffersValiditySupplierById()
        {
            ExtendOffersValidity extendOffersValidity = new ExtendOffersValidity();
            extendOffersValidity.AddAgencyCommunicationRequest(tenderId, 1);

            return new ExtendOffersValiditySupplier()
            {
                ExtendOffersValidityId = 1,
                ExtendOffersValidity = extendOffersValidity
            };
        }

        public TenderCommunicationRequestModel GetDataTenderHasValidExtendOffersValidity()
        {
            return new TenderCommunicationRequestModel() { };
        }

        public PlaintRequest GetPlaintRequest()
        {
            var plaintRequest = new PlaintRequest(1, 1, "Test", new List<CommunicationAttachmentModel>(), true);
            plaintRequest.Offer = new Offer(1, "1299659801", null, 1, false);
            AgencyCommunicationRequest agencyCommunicationRequest = new AgencyCommunicationRequest(1, 1);

            PropertyInfo propertyInfo = plaintRequest.GetType().GetProperty("AgencyCommunicationRequest");
            propertyInfo.SetValue(plaintRequest, Convert.ChangeType(agencyCommunicationRequest, propertyInfo.PropertyType), null);

            return plaintRequest;
        }

        public List<PlaintRequest> GetPlaintRequests()
        {
            return new List<PlaintRequest>()
            {
                GetPlaintRequest()
            };
        }

        public EscalationRequest GetEscalationRequest()
        {
            return new EscalationRequest(1, 1, "1", "name");
        }

        public TenderPlaintDatailsModel GetTenderPlaintDatailsModel()
        {
            return new TenderPlaintDatailsModel();
        }

        public QueryResult<EscalatedTendersModel> GetEscalatedTendersModelQueryResult()
        {
            List<EscalatedTendersModel> sscalatedTendersModels = new List<EscalatedTendersModel>() { new EscalatedTendersModel() { AgencyName = "Agency" } };
            return new QueryResult<EscalatedTendersModel>(sscalatedTendersModels, sscalatedTendersModels.Count, 1, 10);
        }

        public QueryResult<TenderPlaintCheckingModel> GetTenderPlaintCheckingModelQueryResult()
        {
            List<TenderPlaintCheckingModel> tenderPlaintCheckingModel = new List<TenderPlaintCheckingModel>() { new TenderPlaintCheckingModel() };
            return new QueryResult<TenderPlaintCheckingModel>(tenderPlaintCheckingModel, tenderPlaintCheckingModel.Count, 1, 10);
        }

        public TenderPLaintCommunicationModel GetTenderPLaintCommunicationModel()
        {
            TenderHistory history = new TenderHistory() { CreatedAt = DateTime.Now };
            return new TenderPLaintCommunicationModel() { tenderHistory = history };
        }
        public TenderEscalatedPLaintModel GetTenderEscalatedPLaintModel()
        {
            TenderHistory history = new TenderHistory() { CreatedAt = DateTime.Now };
            return new TenderEscalatedPLaintModel() { tenderHistory = history, HasEscalatedPlaints = true, ApprovalDate = DateTime.Now };
        }

        public SupplierExtendOfferDatesRequest GetSupplierExtendOfferDatesRequest()
        {
            return new SupplierExtendOfferDatesRequest("reason", DateTime.Now, 1, _Cr, 1);
        }

        public SupplierExtendOfferDatesAgencyRequestModel GetSupplierExtendOfferDatesAgencyRequestModel()
        {
            return new SupplierExtendOfferDatesAgencyRequestModel();
        }

        public List<SupplierExtendOfferDatesAgencyRequestModel> GetSupplierExtendOfferDatesAgencyRequestModels()
        {
            return new List<SupplierExtendOfferDatesAgencyRequestModel>()
            {
                GetSupplierExtendOfferDatesAgencyRequestModel()
            };
        }

        public QueryResult<CommunicationRequestGrid> GetCommunicationRequestGridQueryResult()
        {
            List<CommunicationRequestGrid> communicationRequestGrids = new List<CommunicationRequestGrid>();
            return new QueryResult<CommunicationRequestGrid>(communicationRequestGrids, communicationRequestGrids.Count, 1, 6);
        }

        public TenderRevisionDateModel GetTenderRevisionDateModel()
        {
            return new TenderRevisionDateModel();
        }
    }
}
