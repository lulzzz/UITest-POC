using MOF.Etimad.Monafasat.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{

    public interface INegotiationAppService
    {
        Task AgreeOnOfferNegotiationFirstStage(string NegotiationId, string TenderId, string CR, string CRCompanyName);
        Task AgreeOnFirstStageNegotiationBySupplier(string NegotiationId, string TenderId, string CR, string CRCompanyName);
        Task AgreeOnNegotiationFirstStageWithExtraDiscount(AcceptNegotiationWithExtraDiscountModel extraDiscountModel);
        Task DisAgreeOfferAfterNegotiationFirstStage(string NegotiationId, string TenderId, string CR);
        Task<bool> ChangeCommunicationRequestStatus(NegotiationAgencyActionStatusModel negotiationActionStatus);
        Task<bool> ApproveNegotiationRequestFirstStage(NegotiationAgencyActionStatusModel negotiationActionStatus);
        Task<bool> RejectNegotiationRequestFirstStage(NegotiationAgencyActionStatusModel negotiationActionStatus);
        Task<NegotiationSupplierViewModel> GetSupplierNegotiationPageInfo(string TenderId, string NegotiationId, string CR);
        Task<NegotiationSupplierViewModel> GetSupplierNegotiationFirstStageInfo(string TenderId, string NegotiationId, string CR);

        #region Negotiation second stage
        Task<AjaxResponse<OffersCompareViewModel>> DeleteNegotiationTenderQuantityTable(int table);
        Task<NegotiationQuantityTableItemModel> UpdateNegotiationQuantityTableItem(NegotiationQuantityTableItemModel model);

        Task SendToApproveNegotiationSecondStageByCheckManagerAsync(int NegotiationId, int Days, int Hours);
        Task ApproveNegotiationSecondStageByCheckManagerAsync(int NegotiationId, string verficationCode, int Days, int Hours);
        Task RejectNegotiationSecondStageByCheckManagerAsync(int NegotiationId, string RejectionReason);
        Task ApproveNegotiationSecondStageByUnitSecretaryAsync(int NegotiationId, string verficationCode);
        Task RejectNegotiationSecondStageByUnitSecretaryAsync(int NegotiationId, string RejectionReason);
        Task EditNegotiationInfoAsync(int NegotiationId);
        Task FinishNegotiationAsync(int NegotiationId);
        Task ResetSecondNegotiation(int NegotiationId);
        Task ReopenNegotiationSecondStageAsync(int NegotiationId);
        Task<string> ApproveNegotiationSecondStageBySupplierAsync(int NegotiationId);
        Task RejectNegotiationSecondStageBySupplierAsync(int NegotiationId);
        Task DeleteNegotiationQuantityItem(string quantityItemId);
        Task UpdateNegotiationQuantityItem(string quantityItemId, int QTY);
        Task<OfferQuantityTableHtmlVM> GetNegotiationQuantityTables(int negotiationId);
        Task<AjaxResponse<OffersCompareViewModel>> SaveNegotiationQitems(Dictionary<string, string> authorList);


        #endregion Negotiation second stage
    }
}
