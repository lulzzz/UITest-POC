using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Data;
using MOF.Etimad.SharedKernel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public class OfferCommands : IOfferCommands
    {
        private readonly IAppDbContext _context;
        public OfferCommands(IAppDbContext context)
        {
            _context = context;
        }
        public async Task<Offer> CreateAsync(Offer offer)
        {

            Check.ArgumentNotNull(nameof(offer), offer);
            await _context.Offers.AddAsync(offer);
            await _context.SaveChangesAsync();
            return offer;


        }

        public async Task<Offer> UpdateAsync(Offer offer)
        {


            Check.ArgumentNotNull(nameof(offer), offer);
            _context.Offers.Update(offer);
            await _context.SaveChangesAsync();
            return offer;
        }
        public async Task UpdateRangAsync(List<Offer> offers)
        {
            Check.ArgumentNotNull(nameof(offers), offers);
            _context.Offers.UpdateRange(offers);
        }
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<SupplierTenderQuantityTable> UpdateQuantityTableAsync(SupplierTenderQuantityTable quantityTable)
        {
            Check.ArgumentNotNull(nameof(quantityTable), quantityTable);
            _context.SupplierTenderQuantityTables.Update(quantityTable);
            _context.SaveChanges();
            return quantityTable;
        }
        public async Task<SupplierCombinedDetail> UpdateCombinedDetailAsync(SupplierCombinedDetail details)
        {
            Check.ArgumentNotNull(nameof(details), details);
            _context.SupplierCombinedDetails.Update(details);
            await _context.SaveChangesAsync();
            return details;
        }
        public async Task<OfferSolidarity> UpdateCombinedAsync(OfferSolidarity combined)
        {
            Check.ArgumentNotNull(nameof(combined), combined);
            _context.OfferSolidarities.Update(combined);
            await _context.SaveChangesAsync();
            return combined;
        }

        public async Task<OfferlocalContentDetails> UpdateOfferlocalContentDetailsAsync(OfferlocalContentDetails offerDetails)
        {
            Check.ArgumentNotNull(nameof(offerDetails), offerDetails);
            _context.OfferlocalContentDetails.Update(offerDetails);
            await _context.SaveChangesAsync();
            return offerDetails;
        }
    }
}
