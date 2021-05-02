using System.Collections.Generic;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class InvitationsInCreateTenderModel
    {
        public int TenderId { get; set; }
        //public List<string> Suppliers { get; set; } = new List<string>();
        public List<CrModel> InvitedSuppliers { get; set; } = new List<CrModel>();
        public List<string> MobileNumbers { get; set; } = new List<string>();
        public List<string> Emails { get; set; } = new List<string>();
    }
    public class CrModel
    {
        public string CrName { get; set; }
        public string CrNumber { get; set; }
    }
}
