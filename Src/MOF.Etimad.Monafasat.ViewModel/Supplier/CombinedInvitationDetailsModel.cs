using System.Collections.Generic;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class CombinedInvitationDetailsModel
    {
        public string OfferIdString { get; set; }
        public string CombinedIdString { get; set; }
        public string TenderName { get; set; }
        public string TenderReferenceNo { get; set; }
        public List<string> ExcutionPlace { get; set; }
        public string IntialGuranteeAddress { get; set; }
        public string ApplyOfferWithComnined { get; set; }
        public string OfferStatus { get; set; }
        public string FileReferenceId { get; set; }
        public string FileName { get; set; }
        public int OfferStatusId { get; set; }
        public int SolidarityStatusId { get; set; }
        public bool isActive { get; set; }
    }
}
