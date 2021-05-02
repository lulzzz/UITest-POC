using System;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class AlternativeItemResponseModel
    {
        public bool IsSuccess { get; set; } = false;
        public String ResponseMessage { get; set; }
        public int Id { get; set; }
        public decimal TotalAfterDiscount { get; set; }
        public decimal TotalAlternative { get; set; }
        public decimal TotalAlternativeAfterDiscount { get; set; }
        public decimal Total { get; set; }


    }
}
