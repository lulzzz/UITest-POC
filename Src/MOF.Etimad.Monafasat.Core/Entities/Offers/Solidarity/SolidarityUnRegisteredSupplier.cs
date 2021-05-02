//using MOF.Etimad.Monafasat.SharedKernel;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
//using static MOF.Etimad.Monafasat.SharedKernel.Enums;

//namespace MOF.Etimad.Monafasat.Core.Entities
//{
//    public class SolidarityUnRegfgisteredSupplier : OfferSolidarity
//    {

//        #region CTOR
//        public SolidarityUnRegfgisteredSupplier()
//        {

//        }
//        public SolidarityUnRegfgisteredSupplier(string email, string mobile, int offerId) : base(offerId)
//        {
//            Email = email;
//            Mobile = mobile;

//        }
//        public SolidarityUnRegfgisteredSupplier(string email, string mobile, Enums.UnRegisteredSuppliersInvitationType unRegisteredSuppliersInvitationType = UnRegisteredSuppliersInvitationType.Existed, Enums.SupplierSolidarityStatus supplierSolidarityStatus = SupplierSolidarityStatus.New, string LicenceOrCR = "", string Desc = "")
//        {
//            Email = email;
//            UnregisteredSupplierTypeId = (int)unRegisteredSuppliersInvitationType;
//            if (unRegisteredSuppliersInvitationType == UnRegisteredSuppliersInvitationType.HasLicience)
//                LicenceNumber = LicenceOrCR;
//            else if (unRegisteredSuppliersInvitationType == UnRegisteredSuppliersInvitationType.Foriegn)
//                CountryCommericalRegisterNo = LicenceOrCR;
//            else
//                CommericalRegisterNo = LicenceOrCR;
//            Mobile = mobile;
//            Description = Desc;
//            StatusId = (int)supplierSolidarityStatus;
//            EntityCreated();

//        }
//        public SolidarityUnRegfgisteredSupplier(string email, string mobile, string x = "")
//        {
//            Email = email;
//            Mobile = mobile;
//            StatusId = (int)Enums.SupplierSolidarityStatus.New;
//            EntityCreated();

//        }
//        #endregion

//        #region Fields

//        public string Description { get; private set; }
//        public int UnregisteredSupplierTypeId { get; set; }
//        public string CommericalRegisterNo { get; private set; }
//        public string LicenceNumber { get; private set; }
//        public string CountryCommericalRegisterNo { get; private set; }

//        #endregion
//    }

//}
