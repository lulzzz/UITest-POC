using MOF.Etimad.Monafasat.SharedKernal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class BranchModel : SearchCriteria
    {
        public int BranchId { get; set; }
        //public int MainBranchAddressId { get; private set; }
        [Required]
        public MainBranchAddressModel MainBranchAddress { get; set; }
        //public int DeliverOfferAddressId { get; set; }
        public string AgencyCode { get; set; }

        public List<OtherBranchAddressModel> AddressesList { get; set; }
        //public OtherBranchAddressModel DeliverOfferAddress { get; set; }
        //public int OpenOfferAddressId { get; private set; }
        //public OtherBranchAddressModel OpenOfferAddress { get; set; }
        //public int DeliverSpecificationBookAddressId { get; set; }
        //public OtherBranchAddressModel DeliverSpecificationBookOfferAddress { get; set; }
        [Required(ErrorMessageResourceType = (typeof(Resources.BranchResources.ErrorMessages)), ErrorMessageResourceName = "EnterBranchName")]
        [StringLength(500)]
        public string BranchName { get; set; }
        public string AgencyName { get; set; }
        public string BranchIdString { get; set; }
        public DateTime? CreatedAt { get; set; }
        public List<int> CommittieeIds { get; set; } = new List<int>();
        public List<int> opencommitteeIds { get; set; } = new List<int>();
        public List<int> purchaseCommitteeIds { get; set; } = new List<int>();
        public List<int> preQualificationCommitteeIds { get; set; } = new List<int>();

        public List<int> vrocommitteeIds { get; set; } = new List<int>();
        public List<int> techcommitteeIds { get; set; } = new List<int>();
        public List<int> checkcommitteeIds { get; set; } = new List<int>();
        public CommitteesModel Committiees { get; set; }
        public List<LookupModel> BranchAddressTypes { get; set; }
        public string RelatedAgencyCode { get; set; }
    }
}
