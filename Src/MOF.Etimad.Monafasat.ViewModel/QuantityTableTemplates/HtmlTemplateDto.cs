using System.Collections.Generic;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class HtmlTemplateDto
    {
        public string FormName { get; set; }
        public string TemplateName { get; set; }
        public long FormId { get; set; }
        public int? TenderId { get; set; }
        public string FormHtml { get; set; }
        public string tableHtml { get; set; }
        public string EditFormHtml { get; set; }
        public string JsScript { get; set; }
        public string DeleteFormHtml { get; set; }
        public string FormExcellTemplate { get; set; }
        public string EncryptedOfferId { get; set; }
        public string EncryptedTenderId { get; set; }
        public string TableId { get; set; }
        public string TableName { get; set; }
        public int ColumnsCount { get; set; }
        public string DescriptionTableHtml { get; set; }
        public bool IsTableHasAlternative { get; set; }
        public int FormCategoryId { get; set; }

    }

    public class NegotiationHtmlTemplate
    {


        public List<HtmlTemplateDto> tables { get; set; }

        public string Message { get; set; }


        public bool isSuccess { get; set; }

    }
}

