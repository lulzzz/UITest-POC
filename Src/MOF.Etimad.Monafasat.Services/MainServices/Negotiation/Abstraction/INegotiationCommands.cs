using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Core.Entities.Negotiations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public interface INegotiationCommands
    {
        Task<NegotiationFirstStage> CreateNegotiationFirstStageAsync(NegotiationFirstStage NegotiationFirstStage);
        Task<NegotiationFirstStage> UpdateNegotiationFirstStageAsync(NegotiationFirstStage NegotiationFirstStage);
        void UpdateNegotiationFirstStage(NegotiationFirstStage NegotiationFirstStage);
        Task<NegotiationSecondStage> CreateNegotiationSecondStageAsync(NegotiationSecondStage NegotiationSecondStage);
        Task<NegotiationSecondStage> UpdateNegotiationSecondStageAsync(NegotiationSecondStage NegotiationSecondStage);
        Task UpdateNegotiationSupplierQuantityTableItemAsync(NegotiationSupplierQuantityTableItem NegotiationSecondStageQTitem);
        Task UpdateNegotiationSupplierQuantityTablesAsync(List<NegotiationSupplierQuantityTable> tables);

        #region Negotiation second stage
        #endregion Negotiation second stage

        //Task<NegotiationQuantityTableItem> UpdateNegotiationQuantityTableItem(NegotiationQuantityTableItem negotiation);
        Task<Negotiation> UpdateNegotiationAsync(Negotiation negotiation);
        Task SaveChanges();


    }
}
