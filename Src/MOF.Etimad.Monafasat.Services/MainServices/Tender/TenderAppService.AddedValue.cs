using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.SharedKernel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public partial class TenderAppService
    {
        public async Task<List<TenderTypeWithAddedValueModel>> GetAllTenderTypesAddedValue()
        {
            return await _tenderQueries.GetTenderTypesWithAddedValue();
        }

        public async Task UpdateTenderTypeAddedValueAsync(List<TenderTypeWithAddedValueModel> tenderTypes)
        {
            Check.ArgumentNotNull(nameof(tenderTypes), tenderTypes);
            var tenderTypesFromDb = new List<Core.Entities.TenderType>();
            foreach (var tenderType in tenderTypes)
            {
                _tenderDomainService.IsValidToUpdateTenderTypeCosts(tenderType);
                var dbTenderType = await _tenderQueries.GetTenderTypeById(tenderType.Id);
                dbTenderType.UpdateCosts(tenderType.BuyingCost, tenderType.InvitationCost);
                tenderTypesFromDb.Add(dbTenderType);
            }
            _genericCommandRepository.UpdateRange(tenderTypesFromDb);
            await _genericCommandRepository.SaveAsync();
        }
    }
}
