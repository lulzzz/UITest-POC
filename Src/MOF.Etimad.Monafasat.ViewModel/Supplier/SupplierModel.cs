using System.Collections.Generic;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class SupplierModel
    {
        #region Fields
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string Fax { get; set; }
        public string PostOffice { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string CommericalRegisterNo { get; set; }
        public string CommericalRegisterName { get; set; }
        public int CivilRegisterNo { get; set; }
        public int MainActivitiesId { get; set; }
        public int SubActivitesId { get; set; }
        public string ActivityDescription { get; set; }
        public int CityId { get; set; }
        public List<string> EMail { get; set; }
        public List<string> MobileNo { get; set; }
        public int OrderById { get; set; }
        public bool IsChecked { get; set; }
        public bool IsFavourite { get; set; }
        #endregion
        #region Constractor
        public SupplierModel()
        {

        }
        #endregion
        #region Methods
        public static List<SupplierModel> GetSuppliers()
        {
            SupplierModel S1 = new SupplierModel();
            S1.SupplierId = 1;
            S1.SupplierName = "Joe";
            S1.Fax = "2343242";
            S1.PostOffice = "2315454";
            S1.City = "City";
            S1.PostalCode = "32432423";
            S1.CommericalRegisterName = "Comerical Name 1";
            S1.EMail = new List<string>() { "abahna@etimad.sa", "abahna@etimad.sa" };
            S1.MobileNo = new List<string>() { "0559877246" };
            S1.CommericalRegisterNo = "1024901843";
            SupplierModel S2 = new SupplierModel();
            S2.SupplierId = 1;
            S2.SupplierName = "Supplier r2";
            S1.Fax = "2343242";
            S1.PostOffice = "2315454";
            S1.City = "City";
            S1.PostalCode = "32432423";
            S2.EMail = new List<string>() { "Mohammed.youssef112355@gmail.com" };
            S2.CommericalRegisterName = "Comerical Name 2";
            S2.MobileNo = new List<string>() { "0504583476" };
            S2.CommericalRegisterNo = "1010245397";
            SupplierModel S3 = new SupplierModel();
            S3.SupplierId = 1;
            S3.SupplierName = "Supplier f3";
            S1.Fax = "2343242";
            S1.PostOffice = "2315454";
            S1.City = "City";
            S1.PostalCode = "32432423";
            S3.CommericalRegisterName = "Comerical Name 3";
            S3.MobileNo = new List<string>() { "0542746493" };
            S3.EMail = new List<string>() { "abahqqna@etimad.sa" };
            S3.CommericalRegisterNo = "1010314118";
            SupplierModel S4 = new SupplierModel();
            S4.SupplierId = 1;
            S4.SupplierName = "Supplier b4";
            S1.Fax = "2343242";
            S1.PostOffice = "2315454";
            S1.City = "City";
            S1.PostalCode = "32432423";
            S4.MobileNo = new List<string>() { "0540814154" };
            S4.EMail = new List<string>() { "aismaiql@etimad.sa" };
            S4.CommericalRegisterName = "Comerical Name 4";
            S4.CommericalRegisterNo = "121211221";
            SupplierModel S5 = new SupplierModel();
            S5.SupplierId = 2;
            S5.SupplierName = "Supplier m5";
            S1.Fax = "2343242";
            S1.PostOffice = "2315454";
            S1.City = "City";
            S1.PostalCode = "32432423";
            S5.MobileNo = new List<string>() { "054081415422" };
            S5.EMail = new List<string>() { "Mohammed.yousseqf1123+aaa@gmail.com" };
            S5.CommericalRegisterName = "Comerical Name 5";
            S5.CommericalRegisterNo = "12121";
            SupplierModel S6 = new SupplierModel();
            S6.SupplierId = 2;
            S6.SupplierName = "Supplier s6";
            S1.Fax = "2343242";
            S1.PostOffice = "2315454";
            S1.City = "City";
            S1.PostalCode = "32432423";
            S6.MobileNo = new List<string>() { "050458347622" };
            S6.CommericalRegisterName = "Comerical Name 6";
            S6.EMail = new List<string>() { "Mohammed@gmail.com" };
            S6.CommericalRegisterNo = "1010435859";
            S6.IsChecked = true;
            SupplierModel S7 = new SupplierModel();
            S7.SupplierId = 2;
            S7.SupplierName = "Supplier iS7";
            S1.Fax = "2343242";
            S1.PostOffice = "2315454";
            S1.City = "City";
            S1.PostalCode = "32432423";
            S7.MobileNo = new List<string>() { "05408141542" };
            S7.CommericalRegisterName = "Comerical Name 6";
            S7.EMail = new List<string>() { "Mohasmme22d@gmail.com" };
            S7.CommericalRegisterNo = "101043585";
            S7.IsChecked = true;
            SupplierModel S8 = new SupplierModel();
            S8.SupplierId = 2;
            S8.SupplierName = "يوسف";
            S1.Fax = "2343242";
            S1.PostOffice = "2315454";
            S1.City = "City";
            S1.PostalCode = "32432423";
            S8.MobileNo = new List<string>() { "05408141543" };
            S8.CommericalRegisterName = "Comerical Name 6";
            S8.EMail = new List<string>() { "Mohammed.youssef112565253@gmail.com" };
            S8.CommericalRegisterNo = "10104358";
            S8.IsChecked = true;
            List<SupplierModel> suppliers = new List<SupplierModel>();
            suppliers.Add(S1);
            suppliers.Add(S2);
            suppliers.Add(S3);
            suppliers.Add(S4);
            suppliers.Add(S5);
            suppliers.Add(S6);
            suppliers.Add(S7);
            suppliers.Add(S8);
            return suppliers;
        }
        public static List<SupplierModel> GetSuppliersSearch(SupplierSearchCretriaModel supplirtSearchCretria)
        {
            List<SupplierModel> result = new List<SupplierModel>();
            List<SupplierModel> supplierList = GetSuppliers();
            if (supplirtSearchCretria.CommericalRegisterNo == null && supplirtSearchCretria.CommericalRegisterName == null)
            {
                result = supplierList;
            }
            else if (supplirtSearchCretria.CommericalRegisterNo != null && supplirtSearchCretria.CommericalRegisterName == null)
                foreach (var item in supplierList)
                {
                    if (item.CommericalRegisterNo == supplirtSearchCretria.CommericalRegisterNo)
                    {
                        result.Add(item);
                    }
                }
            else if (supplirtSearchCretria.CommericalRegisterName != null && supplirtSearchCretria.CommericalRegisterNo == null)
                foreach (var item in supplierList)
                {
                    if (item.CommericalRegisterName == supplirtSearchCretria.CommericalRegisterName)
                    {
                        result.Add(item);
                    }
                }
            else
                foreach (var item in supplierList)
                {
                    if (item.CommericalRegisterName == supplirtSearchCretria.CommericalRegisterName)
                    {

                    }
                    else if (item.CommericalRegisterNo == supplirtSearchCretria.CommericalRegisterNo)
                    {
                        result.Add(item);
                    }
                }
            return result;
        }
        #endregion

    }
}
