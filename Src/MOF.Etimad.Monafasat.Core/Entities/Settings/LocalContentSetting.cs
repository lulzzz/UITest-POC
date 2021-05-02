using MOF.Etimad.Monafasat.SharedKernal.Exceptions;
using MOF.Etimad.Monafasat.SharedKernel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities.Settings
{
    [Table("LocalContentSetting", Schema = "Settings")]
    public class LocalContentSetting : AuditableEntity
    {
        [Key]
        public int Id { get; set; }
        public decimal NationalProductPercentage { get; private set; }
        public decimal HighValueContractsAmmount { get; private set; }
        public decimal LocalContentMaximumPercentage { get; private set; }
        public decimal PriceWeightAfterAdjustment { get; private set; }
        public decimal LocalContentWeightAndFinancialMarket { get; private set; }

        public void Update(decimal nationalProductPercentage, decimal highValueContractsAmmount, decimal localContentMaximumPercentage, decimal priceWeightAfterAdjustment, decimal localContentWeightAndFinancialMarket)
        {

            if (nationalProductPercentage <= 0)
                throw new BusinessRuleException(Resources.LocalContentSettingsResources.DisplayInputs.NationalProductPercentageMoreThanZero);

            if (highValueContractsAmmount < 0 || localContentMaximumPercentage < 0 || priceWeightAfterAdjustment < 0 || localContentWeightAndFinancialMarket < 0)
                throw new BusinessRuleException(Resources.LocalContentSettingsResources.DisplayInputs.ValueCannotBoLessThanZero);

            if (nationalProductPercentage > 100 || localContentMaximumPercentage > 100 || priceWeightAfterAdjustment > 100 || localContentWeightAndFinancialMarket > 100)
                throw new BusinessRuleException(Resources.LocalContentSettingsResources.DisplayInputs.ValueCannotBeMoreThan100);

            if (priceWeightAfterAdjustment + localContentWeightAndFinancialMarket != 100)
            {
                throw new BusinessRuleException(Resources.LocalContentSettingsResources.DisplayInputs.TotalValuesMustEqual100);
            }
            NationalProductPercentage = nationalProductPercentage;
            HighValueContractsAmmount = highValueContractsAmmount;
            LocalContentMaximumPercentage = localContentMaximumPercentage;
            PriceWeightAfterAdjustment = priceWeightAfterAdjustment;
            LocalContentWeightAndFinancialMarket = localContentWeightAndFinancialMarket;
            EntityUpdated();
        }
    }
}
