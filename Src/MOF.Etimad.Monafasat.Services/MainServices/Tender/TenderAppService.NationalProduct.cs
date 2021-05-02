using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernal.Exceptions;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public partial class TenderAppService
    {
        public async Task UpdateTenderLocalContentValues(UpdateTenderNationalProductValuesViewModel viewModel)
        {
            int tenderId = Util.Decrypt(viewModel.TenderIdString);
            var tender = await _tenderQueries.GetTenderForLocalContent(tenderId);
            var tenderLocalContentMechanismIds = await _tenderQueries.GetTenderLocalContentMechanism((int)tender.TenderLocalContentId);
            var isTenderHasNationalProductMechanism = tenderLocalContentMechanismIds.Contains((int)Enums.LocalContentMechanismPerfermance.ThePricePreferenceMechanismNationalProduct);

            if (tender.TenderLocalContentId == null)
            {
                tender.SetNationalProductPercentage(viewModel.NationalProductRate);
            }
            else
            {
                tender.SetLocalContenetNationalProductPercentage(isTenderHasNationalProductMechanism, viewModel.NationalProductPercentage);
                
                if(tenderLocalContentMechanismIds.Contains((int)Enums.LocalContentMechanismPerfermance.MinimumRequiredMechanismLocalContent) && viewModel.MinimumPercentageLocalContentTarget == null)
                {
                    throw new BusinessRuleException(Resources.LocalContentSettingsResources.DisplayInputs.MinimumPercentageLocalContentTargetRequired);

                }
                tender.TenderLocalContent.UpdateTenderLocalContentSettingsForUnit(tenderId, (int)viewModel.MinimumBaseline, (int)viewModel.MinimumPercentageLocalContentTarget
                , viewModel.LocalContentMaximumPercentage, viewModel.PriceWeightAfterAdjustment, viewModel.LocalContentWeightAndFinancialMarket
                , viewModel.BaselineWeight, viewModel.LocalContentTargetWeight, viewModel.FinancialMarketListedWeight);
            }

            await _tenderCommands.UpdateAsync(tender);
        }
    }
}
