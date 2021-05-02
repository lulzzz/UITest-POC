using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.TendersAPI.Services
{
    public interface IBankGuaranteeQueries
    {
        Task<ApiResponseObject<List<PurchasedSupplierTendersDto>>> GetPurchasedTendersForSupplierByCrAndBankGuaranteeType(string cr, int bankGauranteeTypeId);
        Task<ApiResponseObject<List<TenderDetailsDto>>> GetTenderDetailsByReferenceNumberAndCr(string referenceNumber, string cr);
        Task<Offer> GetOfferDetailsByTenderNumberAndCr(string tenderNumber, string cr);
    }
}
