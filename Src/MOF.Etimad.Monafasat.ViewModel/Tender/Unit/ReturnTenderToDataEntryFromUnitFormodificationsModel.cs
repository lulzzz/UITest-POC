using System.Collections.Generic;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class ReturnTenderToDataEntryFromUnitFormodificationsModel
    {
        public string tenderIdString { get; set; }
        public int modificationTypeId { get; set; }
        public string notes { get; set; }
        public string filesString { get; set; }
        public List<ExtendOffersValidityAttachementViewModel> files { get; set; }
    }
}
