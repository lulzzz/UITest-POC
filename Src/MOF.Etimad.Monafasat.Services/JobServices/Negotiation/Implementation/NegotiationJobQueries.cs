using Microsoft.EntityFrameworkCore;
using MOF.Etimad.SharedKernel;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Data;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MOF.Etimad.Monafasat.Core.Entities.Negotiations;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.Core;

namespace MOF.Etimad.Monafasat.Services
{
    public class NegotiationJobQueries : INegotiationJobQueries
    {
        private IAppDbContext _context;
        public NegotiationJobQueries(IAppDbContext context)
        {
            _context = context;
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

        public async Task<List<NegotiationSecondStage>> FindAllSecondNegotiationWaitingSupplierResponse()
        {


            var _currentDate = DateTime.Now;
            var query = (await _context.NegotiationSecondStages.Include(d => d.AgencyCommunicationRequest).ThenInclude(f => f.Tender)
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
