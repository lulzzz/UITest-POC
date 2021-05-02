using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Services;
using MOF.Etimad.Monafasat.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.TendersAPI.Services
{
    public class BankGuaranteeAppService:IBankGuaranteeAppService
    {
        private readonly IBankGuaranteeQueries _bankGuaranteeQueries;
        private readonly IOfferCommands _offerCommands;

        public BankGuaranteeAppService(IBankGuaranteeQueries bankGuaranteeQueries,IOfferCommands offerCommands)
        {
            _bankGuaranteeQueries = bankGuaranteeQueries;
            _offerCommands = offerCommands;
        }

        public async Task<ApiResponseObject<List<PurchasedSupplierTendersDto>>> GetPurchasedTendersForSupplierByCrAndBankGuaranteeType(string cr, int bankGauranteeTypeId)
        {
            var result = await _bankGuaranteeQueries.GetPurchasedTendersForSupplierByCrAndBankGuaranteeType(cr,bankGauranteeTypeId);

            return result;
        }  
        public async Task<ApiResponseObject<List<TenderDetailsDto>>> GetTenderDetailsByReferenceNumberAndCr(string referenceNumber, string cr)
        {
            var result = await _bankGuaranteeQueries.GetTenderDetailsByReferenceNumberAndCr(referenceNumber,cr);

            return result;
        }

        public async Task<ApiResponseObject<string>> UpdateBankGuaranteeFile(BankGuaranteeFileDto bankGuarantee)
        {
           var checkData = CheckValidData(bankGuarantee);
            if (!checkData.success)
            {
                return checkData;
            }
            var offer = await _bankGuaranteeQueries.GetOfferDetailsByTenderNumberAndCr(bankGuarantee.Tender_number, bankGuarantee.CR_number);
            if (offer == null)
                return new ApiResponseObject<string>("") { success = false, data = "", message = "خطا في البيانات" };

            List<SupplierBankGuaranteeDetail> guaranteesLst = new List<SupplierBankGuaranteeDetail>();
            var offerBankGuarantee = new SupplierBankGuaranteeDetail(0, offer.OfferId, true, bankGuarantee.Guarantee_number, bankGuarantee.Bank_identity, bankGuarantee.Guarantee_value, bankGuarantee.Guarantee_start_date, bankGuarantee.Guarantee_end_date, int.Parse(bankGuarantee.GuaranteeDays.ToString()));
            guaranteesLst.Add(offerBankGuarantee);
            offer.UpdateBankGurnteesDetails(guaranteesLst);
            var updatedOffer = await _offerCommands.UpdateAsync(offer);
            var response = new ApiResponseObject<string>("");
            if (updatedOffer == null)
                response = new ApiResponseObject<string>("") { success = false, data = "", message = "خطا في البيانات" };
            else
                response = new ApiResponseObject<string>("") { success = true, data = "", message = "تم استلام ملف الضمان البنكي بنجاح" };

            return response;
        }

        private ApiResponseObject<string> CheckValidData(BankGuaranteeFileDto bankGuarantee)
        {
            if (bankGuarantee.Guarantee_start_date > bankGuarantee.Guarantee_end_date)
            {
                return new ApiResponseObject<string>("") { success = false, data = "", message = "عفوا تاريخ بداية الضمان البنكي اكبر من تاريخ نهاية الضمان البنكي" };
            }
            if (bankGuarantee.Guarantee_value == 0)
            {
                return new ApiResponseObject<string>("") { success = false, data = "", message = "عفوا قيمة الضمان البنكي غير صحيحة " };
            }
            if (bankGuarantee.Guarantee_number == "0")
            {
                return new ApiResponseObject<string>("") { success = false, data = "", message = "عفوا رقم الضمان البنكي غير صحيح " };
            }
            else
            {
                return new ApiResponseObject<string>("") { success = true};

            }
        }
    }
}
