using MOF.Etimad.Monafasat.SharedKernal;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("OfferlocalContentDetails", Schema = "Offer")]
    public class OfferlocalContentDetails :BaseEntity
    {
        [Key]
        public int OfferLocalContentId { get;private set; }
        public decimal? LocalContentPercentage { get;private set; } = 0;
        public decimal? LocalContentDesiredPercentage { get;private set; } = 0;
        public bool? IsSmallOrMediumCorporation { get;private set; }
        public bool? IsIncludedInMoneyMarket { get;private set; }

        public decimal? OfferPriceAfterLocalContent { get;private set; } = 0;
        public decimal? PricePreferancePercentage { get;private set; } = 0;
        public decimal? OfferPriceAfterSmallAndMediumCorporations { get;private set; } = 0;

        public bool IsBindedToMandatoryList { get;private set; }
        public bool IsBindedToTheLowestBaseLine { get;private set; }
        public bool IsBindedToTheLowestLocalContent { get;private set; }
        public DateTime CorporationSizeUpdatedDate { get;private set; }
        public DateTime BaseLineUpdatedDate { get;private set; }
        public DateTime IsIncludedInMoneyMarketUpdatedDate { get;private set; }
        public DateTime LocalContentDesiredPercentageUpdatedDate { get;private set; }



        [Required]
        [ForeignKey(nameof(Offer))]
        public int OfferId { get; set; }
        public Offer Offer { get; private set; }

        public OfferlocalContentDetails()
        {
        }

        public void CreateEntity()
        {
            EntityCreated();
        }
        public void SetCorporationSizeSmallOrMedium()
        {
            IsSmallOrMediumCorporation = true;
            EntityUpdated();
        }
        public void SetCorporationSizeLarg()
        {
            IsSmallOrMediumCorporation = false;
            EntityUpdated();
        }
        public void SetIsIncludedInMoneyMarket(bool isInMoneyMarket)
        {
            IsIncludedInMoneyMarket = isInMoneyMarket;
            EntityUpdated();
        }
        public void SetLocalContentDesiredPercentage(decimal? desiredPercent)
        {
            LocalContentDesiredPercentage = desiredPercent;
            EntityUpdated();
        }
        public void SetPricePreferancePercentage(decimal? desiredPercent)
        {
            PricePreferancePercentage = desiredPercent;
            EntityUpdated();
        }
        public void SetLocalContentBaseLinePercentage(decimal? baseLinePercent)
        {
            LocalContentPercentage = baseLinePercent;
            EntityUpdated();
        }

        public void SetIsSupplierBindedToBaseLine(bool isbaseLine)
        {
            IsBindedToTheLowestBaseLine = isbaseLine;
            EntityUpdated();
        }

        public void SetIsSupplierBindedToTheLowestLocalContent(bool isbindedToLocaclContent)
        {
            IsBindedToTheLowestLocalContent = isbindedToLocaclContent;
            EntityUpdated();
        }
        public void SetOfferFinancialWeight(decimal? OfferWeight)
        {
            OfferPriceAfterLocalContent = OfferWeight;
            EntityUpdated();
        }

        public void SetIsBindedToMandatoryList(bool isBindedToMandatoryList)
        {
            IsBindedToMandatoryList = isBindedToMandatoryList;
            EntityUpdated();
        }

        public void SetOfferPriceAfterSMEA(decimal? price) {
            OfferPriceAfterSmallAndMediumCorporations = price;
            EntityUpdated();
        }
        public void UpdateCorporationSizeDate()
        {
            CorporationSizeUpdatedDate = DateTime.Now;
            EntityUpdated();
        }
        public void UpdateBaseLineDate()
        {
            BaseLineUpdatedDate = DateTime.Now;
            EntityUpdated();
        }
        public void UpdateIsExistInMoneyMarketDate()
        {
            IsIncludedInMoneyMarketUpdatedDate =DateTime.Now;
            EntityUpdated();
        }
        public void UpdateLocalContentDesiredPercentageDate()
        {
            LocalContentDesiredPercentageUpdatedDate = DateTime.Now;
            EntityUpdated();
        }

        public void AddInitialDataAtOfferApply()
        {
            CorporationSizeUpdatedDate = DateTime.Now;
            BaseLineUpdatedDate = DateTime.Now;
            IsIncludedInMoneyMarketUpdatedDate = DateTime.Now;
            LocalContentDesiredPercentageUpdatedDate = DateTime.Now;
            IsBindedToMandatoryList = true;
            EntityUpdated();
        }
    }

}
