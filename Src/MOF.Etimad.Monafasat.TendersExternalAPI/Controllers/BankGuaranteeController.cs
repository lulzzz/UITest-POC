using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MOF.Etimad.Monafasat.TendersAPI.Services;
using MOF.Etimad.Monafasat.ViewModel;

namespace MOF.Etimad.Monafasat.TendersAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class BankGuaranteeController : ControllerBase
    {
        private readonly IBankGuaranteeAppService _bankGuaranteeAppService;
        public BankGuaranteeController(IBankGuaranteeAppService bankGuaranteeAppService)
        {
            _bankGuaranteeAppService = bankGuaranteeAppService;
        }

        [HttpGet]
        [Route("VerifyPurchase/{CR_number}/{Bank_guarantee_type}")]
        public async Task<ApiResponseObject<List<PurchasedSupplierTendersDto>>> VerifyPurchase(string CR_number, int Bank_guarantee_type)
        {

            var result = await _bankGuaranteeAppService.GetPurchasedTendersForSupplierByCrAndBankGuaranteeType(CR_number, Bank_guarantee_type);
            return result;
        }

        [HttpGet]
        [Route("TenderDetails/{referenceNumber}/{cr}")]
        public async Task<ApiResponseObject<List<TenderDetailsDto>>> TenderDetails(string referenceNumber, string cr)
        {

            var result = await _bankGuaranteeAppService.GetTenderDetailsByReferenceNumberAndCr(referenceNumber, cr);
            return result;
        }


        [HttpPost]
        [Route("BankGuaranteeFile")]
        public async Task<ApiResponseObject<string>> BankGuaranteeFile(BankGuaranteeFileDto bankGuarantee)
        {
            var response = await _bankGuaranteeAppService.UpdateBankGuaranteeFile(bankGuarantee);
            return response;
        }

    }
}