using MOF.Etimad.Monafasat.SharedKernal;
using System.Collections.Generic;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class SupplierSearchCretriaModel : SearchCriteria
    {
        #region Fields
        public string AgencyCode { get; set; }
        public int BranchId { get; set; }
        public int InvitationTenderId { get; set; }
        public string InvitationTenderIdString { get; set; }
        public string CommericalRegisterNo { get; set; }
        public string CommericalRegisterName { get; set; }
        public int? CivilRegisterNo { get; set; }
        public int? MainActivitiesId { get; set; }
        public int? SubActivitesId { get; set; }
        public int? ActivitesLevelId { get; set; }
        public string NationalId { get; set; }
        /// <summary>
        /// CR Description
        /// </summary>
        public string ActivityDescription { get; set; }
        public int? CityId { get; set; }
        public string CityName { get; set; }
        public string EMail { get; set; }
        public int? OrderById { get; set; }
        public int? FavouriteSupplierListId { get; set; }
        public bool IsFavourite { get; set; }
        public bool OnlyActive { get; set; }
        public List<string> SupplierNumbers { get; set; }
        public bool IsCountOnly { get; set; } = false;

        public bool IsRandomSort { get; set; }

        #endregion
    }
}
