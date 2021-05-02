using System;
using System.Collections.Generic;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class CommunicationRequestGrid
    {
        public string TenderIdString { get; set; }
        public bool? IsNewNegotiation { set; get; }
        public int AgencyRequestId { get; set; }
        public bool canApproveExtendOfferPresentation { get; set; }
        public string AgencyRequestIdString { get; set; }
        public int AgencyRequestTypeId { get; set; }
        public string AgencyRequestType { get; set; }
        public int StatusId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Status { get; set; }
        public int? ExtevdOfferSupplierStatusId { get; set; }
        public List<NegotiationRequest> NegotiationRequests { get; set; }

    }

    public class NegotiationRequest
    {
        public string Id { get; set; }
        public string RequestId { get; set; }
        public string SupplierCR { get; set; }
        public SharedKernel.Enums.enNegotiationType enNegotiationType { get; set; }
    }
}
