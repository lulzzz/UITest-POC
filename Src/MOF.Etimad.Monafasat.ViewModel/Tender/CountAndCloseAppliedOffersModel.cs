using MOF.Etimad.Monafasat.SharedKernel;
using System.Collections.Generic;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class CountAndCloseAppliedOffersModel
    {
        public int TenderId { get; set; }
        public string TenderNumber { get; set; }
        public string TenderReferenceNumber { get; set; }
        public string TenderName { get; set; }
        public Enums.TenderType TenderTypeId { get; set; }
        public int OffersNumbers { get; set; }
        public int InvetationsNumbers { get; set; }
        public int PurshaseNumbers { get; set; }
        public string SubmitionDate { get; set; }
        public string AgencyName { get; set; }
        public List<PurshesSupplier> PurshesSuppliers { get; set; }
        public List<InvitationedSupplier> InvitationedSuppliers { get; set; }

        public class InvitationedSupplier
        {
            public string SupplierName { get; set; }
            public string CommercialNumber { get; set; }
            public string InvitationSendingDate { get; set; }
            public string InvitaionStatus { get; set; }
            public string InvitationAcceptanceDate { get; set; }
        }
        public class PurshesSupplier
        {
            public string SupplierName { get; set; }
            public string CommercialNumber { get; set; }
            public string PurshaseDate { get; set; }
            public string PurshaseStatus { get; set; }
        }
    }
}
