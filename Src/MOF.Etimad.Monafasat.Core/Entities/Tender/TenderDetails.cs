using MOF.Eitimad.SharedKernel;
using MOF.Etimad.Monafasat.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using static MOF.Etimad.Monafasat.Core.Enums;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("Tender",Schema = "TenderDetails")]
    public class TenderDetails : AuditableEntity
    {
        #region Fields====================================================
        [Key]
        public int TenderDetailsId { get; private set; }
        public int TenderId { get;private set; }
        [ForeignKey("TenderId")]
        public Tender tender { get; private set; } = new Tender();
        [StringLength(1024)]
        public string OfferPresentationPlace { get; private set; }
        [StringLength(1024)]
        [Required]
        public string OffersOpeningAddress { get; private set; }
        [StringLength(1024)]
        public string ConditionsBookletDeliveryAddress { get; private set; }
        [Required]
        public DateTime? LastEnqueriesDate { get; private set; }
        [Required]
        public DateTime? LastOfferPresentationDate { get; private set; }
        [Required]
        public TimeSpan? LastOfferPresentationTime { get; private set; }
        //Required if there is offer opening Place
        public DateTime? OffersOpeningDate { get; private set; }
        //Required if there is offer opening Place
        public TimeSpan? OffersOpeningTime { get; private set; }

        [Required]
        public int? TechnicalOrganizationId { get; private set; }
        [ForeignKey("TechnicalOrganizationId")]
        public Committee TechnicalOrganization { get; private set; } = new Committee();

        
        public int? OffersOpeningCommitteeId { get; private set; }
        [ForeignKey("OffersOpeningCommitteeId")]
        public Committee OffersOpeningCommittee { get; private set; } = new Committee();

        
        public int? OffersCheckingCommitteeId { get; private set; }
        [ForeignKey("OffersCheckingCommitteeId")]
        public Committee OffersCheckingCommittee { get; private set; } = new Committee();

        public Decimal ConditionsBookletPrice { get; private set; }
        [Required]
        public int OfferPresentationWayId { get; private set; }
        [ForeignKey("OfferPresentationWayId")]
        public TenderApplyType ApplyType { get; private set; } = new TenderApplyType();

        #endregion

        #region NotMapped

        #endregion

        #region Constructors====================================================

        public TenderDetails()
        {

        }

        #endregion
        #region Methods====================================================
        public TenderDetails UpdateOffersData(int technicalOrganizationID, int offersOpeningCommitteeID, int offersCheckingCommitteeID, Decimal conditionsBookletPrice, int offerPresentationWay, string offerPresentationPlace, string offersOpeningAddress,string conditionsBookletDeliveryAddress)
        {
            TechnicalOrganizationId = technicalOrganizationID;
            OffersOpeningCommitteeId = offersOpeningCommitteeID;
            OffersCheckingCommitteeId = offersCheckingCommitteeID;
            ConditionsBookletPrice = conditionsBookletPrice;
            OfferPresentationWayId= offerPresentationWay;
            OfferPresentationPlace = offerPresentationPlace;
            OffersOpeningAddress = offersOpeningAddress;
            ConditionsBookletDeliveryAddress = conditionsBookletDeliveryAddress;
            EntityUpdated();
            return this;
        }
        
        public TenderDetails Update()
        {
            EntityUpdated();
            return this;
        }
        public void DeActive()
        {
            IsActive = false;
            EntityUpdated();
        }
        public void SetActive()
        {
            IsActive = true;
            EntityUpdated();
        }
        #endregion
    }
}
