using System.Collections.Generic;

namespace MOF.Etimad.Monafasat.ViewModel.Qualification
{
    public class PostQualificationRelatedTenderDetailsModel
    {
        public string TenderName { get; set; }
        public string TenderReferenceNumber { get; set; }

        public string TenderTypeName { get; set; }
        public List<PostQualificationSuppliersInvitationsModel> lstOfSupplierInvitation { get; set; }


    }
}
