using MOF.Etimad.Monafasat.ViewModel.Offer;
using System.Collections.Generic;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class OfferQuantityTableHtmlVM
    {
        public int offerId { get; set; }
        public int statusId { get; set; }
        public List<TableFormHtml> tableFormHtmls { get; set; } = new List<TableFormHtml>();
        public OfferStatusModel offerStatusModel { get; set; }
        public int TenderStatusId { get; set; }
        public int TenderTypeId { get; set; }
        public int OfferPresentationWayId { get; set; }
    }

}
