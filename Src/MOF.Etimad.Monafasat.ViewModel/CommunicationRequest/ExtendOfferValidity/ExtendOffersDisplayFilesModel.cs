using MOF.Etimad.Monafasat.ViewModel.Offer.OpenOfferStage;
using System.Collections.Generic;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class ExtendOffersDisplayFilesModel
    {
        public List<OfferAttachmentModel> offerAttachmentModels { get; set; }
        public List<ViewModel.Offer.OpenOfferStage.QuantityTableModel> quantityTableModels { get; set; }

        public string OfferIdString { get; set; }
    }
}
