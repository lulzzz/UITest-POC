using MOF.Etimad.Monafasat.SharedKernal;
using System;

namespace MOF.Etimad.Monafasat.Core
{
    public class BlockSearchCriteria : SearchCriteria
    {


        #region Constructors


        public BlockSearchCriteria()
        {
        }
        public BlockSearchCriteria(string commercialRegistrationNo, string commercialSupplierName)
        {
            CommercialRegistrationNo = commercialRegistrationNo;
            CommercialSupplierName = commercialSupplierName;
        }

        #endregion
        #region Fields ======== 
        public string CommercialRegistrationNo { get; set; }

        public string CommercialSupplierName { get; set; }

        public string ResolutionNumber { get; set; }

        public int BlockTypeId { get; set; }

        public int BlockStatusId { get; set; }
        public string AgencyCode { get; set; }
        public int IsDeletedId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

        public int OrganizationTypeId { get; set; }

        public bool EnabledSearchByDate { get; set; }
        #endregion
    }
}
