using MOF.Etimad.Monafasat.Core;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Core.Entities.Negotiations;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public interface INegotiationQueries
    {

        Task<NegotiationFirstStage> FindWithAgencyRequestById(int NegotiationId);
        Task<NegotiationFirstStage> FindWithAgencyRequestAndSuppliersById(int negotiationId);
        Task<NegotiationFirstStage> FindNegotiationWithAgencyRequestById(int negotiationId);
        Task<NegotiationFirstStage> FindWithSuppliersById(int NegotiationId);
        Task<Supplier> GetSupplierInfoByCr(string cR);
        Task<NegotiationFirstStage> GetNegotiationFirstStageWithAgency(int NegotiationId);
        Task<NegotiationFirstSatgeSupplierOfferInfo> FindSupplierOfferDetails(int NegotiationId, int OfferId);
        Task<NegotiationFirstSatgeSupplierOfferInfo> FindSupplierOfferDetailsForNegotiationFirstStage(int NegotiationId, int offerid);
        Task<SupplierTenderMainInfo> FindTenderDetails(int TenderId);
        Task<SupplierTenderMainInfo> FindTenderDetailsForSecondNegotiation(int TenderId);
        Task<List<PendingAgencyRequestAlarm>> GetPendingNegotiations(string cR);
        Task<NegotiationSecondStage> FindSecondStageNegotiationbyId(int NegotiationId);
        Task<NegotiationSecondStage> FindSecondStageNegotiationWithTablesbyId(int NegotiationId);
        Task<SecondStageNegotiationModelcs> FindNegotiationSecondStagebyId(int negotiationId);
        Task<SecondStageNegotiationModelcs> FindNegotiationSecondbyId(int negotiationId);
        Task<SecondStageNegotiationModelcs> FindNegotiationSecondbyId(int negotiationId, int userId);
        Task<List<TenderQuantityItemDTO>> FindSecondStageNegotiationbyTQIId(int NegotiationId);
        Task<List<TableModel>> FindTableHtmlTemplatebyNegotiationId(int NegotiationId, int tenderid);
        Task<QuantitiesTemplateModel> FindNegotiationQuantityItemsForSecondNegotiation(int negotiationId);
        #region Negotiation second stage

        Task<long> FindFirstQTId(int negotiationId, int formid);
        Task<Offer> FindOfferByNegotiationTableId(int negotiationQuantityTableId);
        Task<NegotiationSecondStage> GetNegotiationByQuantityTableId(int negotiationQuantityTableId);
        Task<Negotiation> FindNegotiationById(int NegotiationId);
        Task<NegotiationSecondStage> FindNegotiationSecondStageById(int NegotiationId);
        Task<NegotiationSecondStage> FindNegotiationSecondStageForSupplierById(int NegotiationId);
        Task<NegotiationSecondStage> FindNegotiationSecondStageByTenderId(int TenderId);
        Task<bool> IsTenderHasActiveNegotiationRequests(int tenderId);
        Task<int> GetNegotaitionTableQuantityItems(long tableId);
        Task<QueryResult<TenderQuantityItemDTO>> GetSupplierQTableItemsForNegotiation(QuantityTableSearchCretriaModel quantityTableSearchCretriaModel);
        Task<QuantitiesTemplateModel> GetNegotiationQuantityTableTemplateForNegotiation(int tenderid, int negoid);
        #endregion Negotiation second stage
    }
}
