using System.Collections.Generic;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class HtmlTableRowTemplateDto
    {
        public List<TenderQuantityItemDTO> QuantityItemDto { get; set; }
        public string FormHtml { get; set; }
        public string TableId { set; get; }
    }
}
