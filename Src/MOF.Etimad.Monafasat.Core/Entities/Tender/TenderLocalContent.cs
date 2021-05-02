using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernal.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("TenderLocalContent", Schema = "Tender")]
    public class TenderLocalContent : BaseEntity
    {
        [Key]
        public int Id { get; private set; }

        [Required]
        [ForeignKey(nameof(Tender))]
        public int TenderId { get; private set; }
        public Tender Tender { get; private set; }

        public bool IsApplyProvisionsMandatoryList { get; private set; }
        public int? MinimumBaseline { get; private set; }
        public int? MinimumPercentageLocalContentTarget { get; private set; }

        public decimal? NationalProductPercentage { get; private set; }
        public decimal? HighValueContractsAmmount { get; private set; }
        public decimal? LocalContentMaximumPercentage { get; private set; }
        public decimal? PriceWeightAfterAdjustment { get; private set; }
        public decimal? LocalContentWeightAndFinancialMarket { get; private set; }
        public decimal? BaselineWeight { get; private set; }
        public decimal? LocalContentTargetWeight { get; private set; }
        public decimal? FinancialMarketListedWeight { get; private set; }


        public List<LocalContentMechanism> LocalContentMechanism { private set; get; } = new List<LocalContentMechanism>();
        public string StudyMinimumTargetFileNetReferenceId { get; private set; }

        public TenderLocalContent()
        {
        }
        public void UpdateTenderLocalContent(int tenderId, List<int> localContentMechanismIds, bool isApplyProvisionsMandatoryList, int? minimumBaseline, int? minimumPercentageLocalContentTarget)
        {
            TenderId = tenderId;
            IsApplyProvisionsMandatoryList = isApplyProvisionsMandatoryList;
            MinimumBaseline = minimumBaseline;
            MinimumPercentageLocalContentTarget = minimumPercentageLocalContentTarget;
            CreateLocalContentMechanism(localContentMechanismIds);
            if (Id == 0)
                EntityCreated();
            else
                EntityUpdated();
        }
        public void UpdateTenderLocalContentSettingsForUnit(int tenderId, int? minimumBaseline, int? minimumPercentageLocalContentTarget,
            decimal? localContentMaximumPercentage, decimal? priceWeightAfterAdjustment, decimal? localContentWeightAndFinancialMarket
            , decimal? baselineWeight, decimal? localContentTargetWeight, decimal? financialMarketListedWeight)
        {
            if(priceWeightAfterAdjustment + localContentWeightAndFinancialMarket != 100)
            {
                throw new BusinessRuleException(Resources.LocalContentSettingsResources.DisplayInputs.TotalValuesMustEqual100);
            }

            TenderId = tenderId;
            MinimumBaseline = minimumBaseline;
            MinimumPercentageLocalContentTarget = minimumPercentageLocalContentTarget;
            LocalContentMaximumPercentage = localContentMaximumPercentage;
            PriceWeightAfterAdjustment = priceWeightAfterAdjustment;
            LocalContentWeightAndFinancialMarket = localContentWeightAndFinancialMarket;
            BaselineWeight = baselineWeight;
            LocalContentTargetWeight = localContentTargetWeight;
            FinancialMarketListedWeight = financialMarketListedWeight;
            EntityUpdated();
        }
        public void SetTenderLocalContentSettings(int tenderId, decimal? highValueContractsAmmount,
            decimal? localContentMaximumPercentage, decimal? priceWeightAfterAdjustment,
            decimal? localContentWeightAndFinancialMarket)
        {
            TenderId = tenderId;
            HighValueContractsAmmount = highValueContractsAmmount;
            LocalContentMaximumPercentage = localContentMaximumPercentage;
            PriceWeightAfterAdjustment = priceWeightAfterAdjustment;
            LocalContentWeightAndFinancialMarket = localContentWeightAndFinancialMarket;
            BaselineWeight = 50;
            LocalContentTargetWeight = 50;
            FinancialMarketListedWeight = 5;
            EntityUpdated();
        }

        public void SetNationalProductPercentage(decimal nationalProductPercentage)
        {

            if (nationalProductPercentage <= 0)
                throw new BusinessRuleException(Resources.LocalContentSettingsResources.DisplayInputs.NationalProductPercentageMoreThanZero);

            NationalProductPercentage = nationalProductPercentage;
            EntityUpdated();

        }

        public void CreateLocalContentMechanism(IList<int> localContentMechanismIds)
        {
            if (localContentMechanismIds != null)
            {
                if (LocalContentMechanism == null)
                    LocalContentMechanism = new List<LocalContentMechanism>();
                var mutualAreas = LocalContentMechanism.Where(x => localContentMechanismIds.Contains(x.LocalContentMechanismPreferenceId)).ToList();
                var mutualIds = mutualAreas.Select(a => a.LocalContentMechanismPreferenceId).ToList<int>();
                List<LocalContentMechanism> AreasToBeDeleted = LocalContentMechanism.Where(x => !localContentMechanismIds.Contains(x.LocalContentMechanismPreferenceId)).ToList();
                List<int> AreasAddedIDs = localContentMechanismIds.Where(x => !mutualIds.Contains(x)).ToList<int>();
                if (LocalContentMechanism != null)
                {
                    foreach (LocalContentMechanism item in AreasToBeDeleted)
                    {
                        item.Delete();
                    }
                }
                if (localContentMechanismIds != null)
                {
                    foreach (var item in AreasAddedIDs)
                    {
                        LocalContentMechanism.Add(new LocalContentMechanism(item));
                    }
                }
            }
        }

    }
}