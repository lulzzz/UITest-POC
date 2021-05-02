using MOF.Etimad.Monafasat.ViewModel.Offer.OpenOfferStage;
using System;
using System.Collections.Generic;

namespace MOF.Etimad.Monafasat.ViewModel.Offer
{
    public class OfferDetailsForAcceptingSolidarityInviationsModel
    {
        #region New Props
        public string tenderIdString { get; set; }
        public string SolidarityIdString { get; set; }
        public DateTime? OfferOpenDate { get; set; }
        public int OfferStatusId { get; set; }
        public int InvitaionStatusId { get; set; }
        public string InvitaionStatusName { get; set; }
        public List<OfferAttachmentModel> OfferAttachmentModels { get; set; }
        public string OfferIdString { get; set; }
        public int TenderTypeId { get; set; }
        #endregion
    }
}
