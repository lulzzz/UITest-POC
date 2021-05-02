using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.Core;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Core.Entities.Negotiations;
using MOF.Etimad.Monafasat.Data;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public class CommunicationRequestJobQueries : ICommunicationRequestJobQueries
    {
        private readonly IAppDbContext _context;
        public CommunicationRequestJobQueries(IAppDbContext context, IOptionsSnapshot<RootConfigurations> rootConfiguration)
        {
            _context = context;
        }

        public async Task<List<AgencyCommunicationRequest>> FindTendersWithPlaintsAfterStoppingPeriodJob()
        {
            var entities = await _context.AgencyCommunicationRequests
                .Include(o => o.Tender)
                .Include(a => a.PlaintRequests).ThenInclude(p => p.Offer)
                .Where(t => t.IsActive == true && t.PlaintNotification != null && t.PlaintNotification.IsSent == false && t.AgencyRequestTypeId == (int)Enums.AgencyCommunicationRequestType.Plaint && t.StatusId == (int)Enums.AgencyCommunicationRequestStatus.RequestSent && t.EscalationStatusId == null)
                .Where(t => t.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersAwardedConfirmed)
                .Where(t => t.Tender.TenderHistories.OrderByDescending(a => a.CreatedAt).FirstOrDefault(a => a.StatusId == (int)Enums.TenderStatus.OffersAwardedConfirmed) == null ? false : t.Tender.TenderHistories.OrderByDescending(a => a.CreatedAt).FirstOrDefault().CreatedAt.AddDays(t.Tender.AwardingStoppingPeriod.Value).Date <= DateTime.Now.Date)
                .ToListAsync();
            return entities;
        }
        public async Task<List<ExtendOffersValiditySupplier>> GetExtendOfferValiditySuppliers()
        {
            var result = await _context.AgencyCommunicationRequests
                .Include(c => c.ExtendOffersValidity)
                    .ThenInclude(e => e.ExtendOffersValiditySuppliers)
                 .Where(c => c.AgencyRequestTypeId == (int)Enums.AgencyCommunicationRequestType.ExtendOfferValidtiy)
                .Where(c => c.StatusId == (int)Enums.AgencyCommunicationRequestStatus.Approved && c.IsActive == true)
                .Where(c => c.ExtendOffersValidity.ExtendOffersValiditySuppliers.Any(s => s.Offer.OfferStatusId != (int)Enums.OfferStatus.Excluded))
                .ToListAsync();
            return result.SelectMany(c => c.ExtendOffersValidity.ExtendOffersValiditySuppliers).ToList();
        }

        public async Task<List<Offer>> GetNonExecludedOffersForExtendOffersByValidityIds(List<int> supplierExtendOfferValidityIds)
        {
            var model = await _context.Offers
                .Include(o => o.ExtendOffersValiditySupplier)
                .Where(o => supplierExtendOfferValidityIds.Contains(o.ExtendOffersValiditySupplier.Id))
                .Where(o => o.OfferStatusId != (int)Enums.OfferStatus.Excluded)
                .ToListAsync();
            return model;
        }


        public async Task<List<AgencyCommunicationRequest>> GetExtendOffersValidityForExecludeSuppliers()
        {
            var model = await _context.AgencyCommunicationRequests
                .Where(s => s.StatusId == (int)Enums.AgencyCommunicationRequestStatus.Approved && s.IsActive == true)
                .Where(s => s.AgencyRequestTypeId == (int)Enums.AgencyCommunicationRequestType.ExtendOfferValidtiy)
                .Where(s => DateTime.Now > s.ExtendOffersValidity.NewOffersExpiryDate
                  || (s.ExtendOffersValidity.ExtendOffersValiditySuppliers.Any() && !s.ExtendOffersValidity.ExtendOffersValiditySuppliers.Any(a => a.SupplierExtendOfferValidityStatusId == (int)Enums.SupplierExtendOffersValidityStatus.Sent
                       || a.SupplierExtendOfferValidityStatusId == (int)Enums.SupplierExtendOffersValidityStatus.AcceptedInitially
                       || a.SupplierExtendOfferValidityStatusId == (int)Enums.SupplierExtendOffersValidityStatus.UnderProcessing)))
                 .ToListAsync();
            return model;
        }
        public async Task<List<NegotiationFirstStage>> FindAllNegotiationWaitingSupplierResponse()
        {

            var query = await _context.NegotiationFirstStages.Include
                ("NegotiationFirstStageSuppliers.Offer")
                                                .Where(d => d.StatusId == (int)Enums.enNegotiationStatus.SentToSuppliers
                    && d.IsActive == true).ToListAsync();
            return query;
        }

        public async Task<Supplier> FindSupplierByCRNumber(string selectedCr)
        {
            return _context.Suppliers.Include(r => r.NotificationSetting).Where(s => s.SelectedCr == selectedCr && s.IsActive == true).FirstOrDefault();
        }
        public async Task<Tender> FindTenderByTenderId(int tenderId)
        {
            var tenderEntity = await _context.Tenders.Where(t => t.TenderId == tenderId).AsTracking().FirstOrDefaultAsync();
            return tenderEntity;
        }
        public async Task<Offer> GetOfferById(int offerId)
        {
            var result = await _context.Offers
               .Where(t => t.OfferId == offerId)
               .Include(x => x.Tender).ThenInclude(d => d.TenderActivities).ThenInclude(d => d.Activity)
               .Include(x => x.Combined)
               .FirstOrDefaultAsync();
            return result;
        }
        public async Task<Negotiation> UpdateNegotiationAsync(Negotiation negotiation)
        {
            _context.Negotiations.Update(negotiation);
            await _context.SaveChangesAsync();
            return negotiation;
        }

        public async Task SaveChanges()
        {

            await _context.SaveChangesAsync();

        }


        public async Task<List<GovAgency>> FindAgenciesByAgencyCodes(List<string> agencyCodes)
        {
            return await _context.GovAgencies.Where(a => a.IsActive == true && agencyCodes.Contains(a.AgencyCode)).ToListAsync();
        }

        public async Task<List<NegotiationSecondStage>> FindAllSecondNegotiationWaitingSupplierResponse()
        {


            var _currentDate = DateTime.Now;
            var query = (await _context.NegotiationSecondStages.Include(d => d.AgencyCommunicationRequest).ThenInclude(f => f.Tender).ThenInclude(f => f.Agency)
                    .Where(d => d.StatusId == (int)Enums.enNegotiationStatus.SentToSuppliers
                    &&
                    d.IsActive == true
                    ).ToListAsync()).Where(d => d.PeriodStartDate.HasValue ? (_currentDate.Subtract(d.PeriodStartDate.Value).TotalHours > d.SupplierReplyPeriodHours) : false).ToList();
            return query;
        }

        public async Task<NegotiationSecondStage> UpdateNegotiationSecondStageAsync(NegotiationSecondStage NegotiationSecondStage)
        {
            _context.NegotiationSecondStages.Update(NegotiationSecondStage);
            await _context.SaveChangesAsync();
            return NegotiationSecondStage;




        }
        public void UpdateNegotiationFirstStage(NegotiationFirstStage NegotiationFirstStage)
        {

            _context.NegotiationFirstStages.Update(NegotiationFirstStage);

        }
    }
}
