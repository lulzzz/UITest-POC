using MOF.Etimad.Monafasat.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.TendersAPI.Services
{
    public interface IBankGuaranteeAppService
    {
        Task<ApiResponseObject<List<PurchasedSupplierTendersDto>>> GetPurchasedTendersForSupplierByCrAndBankGuaranteeType(string cr, int bankGauranteeTypeId);
        Task<ApiResponseObject<List<TenderDetailsDto>>> GetTenderDetailsByReferenceNumberAndCr(string referenceNumber, string cr);
        Task<ApiResponseObject<string>> UpdateBankGuaranteeFile(BankGuaranteeFileDto bankGuarantee);
    }
}
