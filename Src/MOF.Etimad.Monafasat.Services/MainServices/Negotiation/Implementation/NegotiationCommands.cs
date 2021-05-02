
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Core.Entities.Negotiations;
using MOF.Etimad.Monafasat.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public class NegotiationCommands : INegotiationCommands
    {
        private IAppDbContext _context;
        public NegotiationCommands(IAppDbContext context)
        {
            _context = context;
        }
        public async Task<NegotiationFirstStage> CreateNegotiationFirstStageAsync(NegotiationFirstStage NegotiationFirstStage)
        {
            try
            {
                await _context.NegotiationFirstStages.AddAsync(NegotiationFirstStage);
                await _context.SaveChangesAsync();
                return NegotiationFirstStage;
            }
            catch
            {
                throw;
            }
        }

        public void UpdateNegotiationFirstStage(NegotiationFirstStage NegotiationFirstStage)
        {

            _context.NegotiationFirstStages.Update(NegotiationFirstStage);

        }
        public async Task SaveChanges()
        {

            await _context.SaveChangesAsync();

        }


        public async Task<NegotiationFirstStage> UpdateNegotiationFirstStageAsync(NegotiationFirstStage NegotiationFirstStage)
        {


            _context.NegotiationFirstStages.Update(NegotiationFirstStage);
            await _context.SaveChangesAsync();
            return NegotiationFirstStage;


        }

        public async Task<NegotiationSecondStage> CreateNegotiationSecondStageAsync(NegotiationSecondStage NegotiationSecondStage)
        {
            await _context.NegotiationSecondStages.AddAsync(NegotiationSecondStage);
            await _context.SaveChangesAsync();
            return NegotiationSecondStage;




        }

        public async Task<NegotiationSecondStage> UpdateNegotiationSecondStageAsync(NegotiationSecondStage NegotiationSecondStage)
        {
            _context.NegotiationSecondStages.Update(NegotiationSecondStage);
            await _context.SaveChangesAsync();
            return NegotiationSecondStage;




        }


        public async Task UpdateNegotiationSupplierQuantityTableItemAsync(NegotiationSupplierQuantityTableItem NegotiationSecondStageQTitem)
        {

            // _context.NegotiationSupplierQuantityTableItems.Update(NegotiationSecondStageQTitem);
        }
        public async Task UpdateNegotiationSupplierQuantityTablesAsync(List<NegotiationSupplierQuantityTable> tables)
        {
            _context.NegotiationSupplierQuantityTables.UpdateRange(tables);
            // _context.NegotiationSupplierQuantityTableItems.Update(NegotiationSecondStageQTitem);
        }
        #region Negotiation second stage



        #endregion Negotiation second stage
        public async Task<Negotiation> UpdateNegotiationAsync(Negotiation negotiation)
        {
            _context.Negotiations.Update(negotiation);
            await _context.SaveChangesAsync();
            return negotiation;
        }

        //public async Task<NegotiationQuantityTableItem> UpdateNegotiationQuantityTableItem(NegotiationQuantityTableItem negotiation)
        //{
        //    _context.NegotiationQuantityTableItems.Update(negotiation);
        //    await _context.SaveChangesAsync();
        //    return negotiation;
        //}
    }
}
