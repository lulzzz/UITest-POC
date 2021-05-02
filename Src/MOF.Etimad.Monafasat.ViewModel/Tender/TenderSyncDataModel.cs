using System.Collections.Generic;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class TenderSyncDataModel
    {
        public string userNAT { get; set; }
        public string type { get; set; }
        public string title { get; set; }
        public string ref_no { get; set; }
        public string description { get; set; }
        public float price { get; set; }
        public string status { get; set; }
        public string faq_delivery_date_hijri { get; set; }
        public string offer_delivery_date_hijri { get; set; }
        public int offer_delivery_time_hour { get; set; }
        public double offer_delivery_time_minute { get; set; }
        public string open_env_date_hijri { get; set; }
        public int open_env_time_hour { get; set; }
        public double open_env_time_minute { get; set; }
        public string agency_code { get; set; }
        public int branch_id { get; set; }
        public string branch_name { get; set; }
        public List<string> categories { get; set; }
        public List<string> regions { get; set; }
    }
}
