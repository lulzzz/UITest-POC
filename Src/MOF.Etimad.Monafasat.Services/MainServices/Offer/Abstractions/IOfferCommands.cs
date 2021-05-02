using MOF.Etimad.Monafasat.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public interface IOfferCommands
    {
        Task<Offer> CreateAsync(Offer offer);
        Task<Offer> UpdateAsync(Offer offer);
        Task UpdateRangAsync(List<Offer> offers);
        Task<SupplierTenderQuantityTable> UpdateQuantityTableAsync(SupplierTenderQuantityTable quantityTable);
        Task SaveAsync();
        Task<SupplierCombinedDetail> UpdateCombinedDetailAsync(SupplierCombinedDetail details);
        Task<OfferSolidarity> UpdateCombinedAsync(OfferSolidarity combined);
        Task<OfferlocalContentDetails> UpdateOfferlocalContentDetailsAsync(OfferlocalContentDetails offerDetails);
    }
}
