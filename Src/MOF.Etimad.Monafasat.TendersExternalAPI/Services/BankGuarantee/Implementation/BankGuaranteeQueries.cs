using Microsoft.EntityFrameworkCore;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Data;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.TendersAPI.Services
{
    public class BankGuaranteeQueries:IBankGuaranteeQueries
    {
        private readonly IAppDbContext _dbContext;
        public BankGuaranteeQueries(IAppDbContext appDbContext)
        {
            _dbContext = appDbContext;
        }


        public async Task<ApiResponseObject<List<PurchasedSupplierTendersDto>>> GetPurchasedTendersForSupplierByCrAndBankGuaranteeType(string cr, int bankGauranteeTypeId)
        {
            var result = await _dbContext.Tenders.Where(tender => tender.IsActive == true)
                .Where(tender => tender.ConditionsBooklets.Any(booklet => booklet.IsActive == true && booklet.CommericalRegisterNo == cr && booklet.BillInfo.BillStatusId == (int)Enums.BillStatus.Paid)
                || tender.Invitations.Any(invitation => invitation.IsActive == true && invitation.CommericalRegisterNo == cr && invitation.StatusId == (int)Enums.InvitationStatus.Approved))
                .WhereIf(bankGauranteeTypeId == (int)Enums.BankGuaranteeType.Primary, tender => !string.IsNullOrEmpty(tender.InitialGuaranteeAddress) && tender.TenderStatusId != (int)Enums.TenderStatus.Canceled && tender.TenderStatusId != (int)Enums.TenderStatus.OffersAwardedConfirmed)
                .WhereIf(bankGauranteeTypeId == (int)Enums.BankGuaranteeType.Final, tender => tender.TenderStatusId == (int)Enums.TenderStatus.OffersAwardedConfirmed && tender.Offers.Any(offer => offer.IsActive == true && offer.CommericalRegisterNo == cr && (offer.TotalOfferAwardingValue.HasValue || offer.PartialOfferAwardingValue.HasValue)))
                .Select(tender => new PurchasedSupplierTendersDto
                {
                    TenderName = tender.TenderName,
                    ReferenceNumber = tender.ReferenceNumber,
                    AgencyCode = tender.AgencyCode,
                    AgencyName = tender.Agency.NameArabic,
                    TenderStatsId = tender.TenderStatusId,
                    TenderStatusName = tender.Status.NameAr,
                    OffersOpeningDate = tender.OffersOpeningDate.Value,
                })
                .ToListAsync();

            var resultResponse = new ApiResponseObject<List<PurchasedSupplierTendersDto>>(result);
            if (!result.Any() || (bankGauranteeTypeId != (int)Enums.BankGuaranteeType.Primary && bankGauranteeTypeId != (int)Enums.BankGuaranteeType.Final))
            {
                resultResponse.message = "لا يوجد بيانات";
                resultResponse.success = false;
                resultResponse.data = new List<PurchasedSupplierTendersDto>();
            }
            return resultResponse;
        }

        public async Task<ApiResponseObject<List<TenderDetailsDto>>> GetTenderDetailsByReferenceNumberAndCr(string referenceNumber, string cr)
        {
            var result = await _dbContext.Tenders.Where(tender => tender.IsActive == true && tender.ReferenceNumber == referenceNumber)
                .Where(tender => tender.ConditionsBooklets.Any(booklet => booklet.IsActive == true && booklet.CommericalRegisterNo == cr && booklet.BillInfo.BillStatusId == (int)Enums.BillStatus.Paid)
                || tender.Invitations.Any(invitation => invitation.IsActive == true && invitation.CommericalRegisterNo == cr && invitation.StatusId == (int)Enums.InvitationStatus.Approved))

                .Select(tender => new TenderDetailsDto
                {
                    TenderName = tender.TenderName,
                    ReferenceNumber = tender.ReferenceNumber,
                    AgencyCode = tender.AgencyCode,
                    AgencyName = tender.Agency.NameArabic,
                    TenderStatsId = tender.TenderStatusId,
                    TenderStatusName = tender.Status.NameAr,
                    OffersOpeningDate = tender.OffersOpeningDate.Value,
                    AwardedSuppliers = tender.TenderStatusId == (int)Enums.TenderStatus.OffersAwardedConfirmed ? tender.Offers.Where(offer => offer.IsActive == true && (offer.TotalOfferAwardingValue.HasValue || offer.PartialOfferAwardingValue.HasValue)).Select(x => new AwardedSuppliers() { SupplierCr = x.CommericalRegisterNo, SupplierName = x.Supplier.SelectedCrName }).ToList() : new List<AwardedSuppliers>()
                })
                .ToListAsync();


            var resultResponse = new ApiResponseObject<List<TenderDetailsDto>>(result);
            if (!result.Any())
            {
                resultResponse.message = "غير مصرح";
                resultResponse.success = false;
            }
            return resultResponse;
        }

        public async Task<Offer> GetOfferDetailsByTenderNumberAndCr(string tenderNumber , string cr)
        {
            var result = await _dbContext.Offers.
                FirstOrDefaultAsync(offer => offer.IsActive ==true 
                && offer.CommericalRegisterNo == cr 
                && offer.Tender.ReferenceNumber == tenderNumber
                && !string.IsNullOrEmpty(offer.Tender.InitialGuaranteeAddress));

            return result;
        }
    }
}
