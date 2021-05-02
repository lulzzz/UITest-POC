using System.Collections.Generic;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class TenderSyncDataResultModel
    {
        public string status { get; set; }
        public Reason reason { get; set; }
        public List<Details> details { get; set; }
    }

    public class Reason
    {
        public int id { get; set; }
        public string message { get; set; }
    }

    public class Details
    {
        public string element_id { get; set; }
        public string label { get; set; }
        public string error_code { get; set; }
        public string error_message { get; set; }
    }
}
