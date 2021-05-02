using MOF.Etimad.Monafasat.SharedKernal;
using static MOF.Etimad.Monafasat.SharedKernel.Enums;

namespace MOF.Etimad.Monafasat.Core
{
    public class BlockCommitteeSearchCriteria : SearchCriteria
    {


        #region Constructors


        public BlockCommitteeSearchCriteria()
        {
        }

        #endregion
        #region Fields ======== 

        public string CommercialRegistrationNo { get; set; }

        public string CommercialSupplierName { get; set; }
        public string CivilRegistrationNumber { get; set; }

        public string EmployeeName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }
        public bool isGovVendor { get; set; }
        public bool isSemiGovAgency { get; set; }

        public AgencyType AgencyType { get; set; }
        public string AgencyId { get; set; }
        #endregion
    }
}
