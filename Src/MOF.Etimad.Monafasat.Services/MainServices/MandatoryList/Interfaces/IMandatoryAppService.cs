using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public interface IMandatoryListAppService
    {
        Task<MandatoryListViewModel> Find(int id);

        Task<InputMandatoryListViewModel> FindForEdit(int id);

        Task<MandatoryListViewModel> Add(InputMandatoryListViewModel mandatoryList);

        Task Update(InputMandatoryListViewModel mandatoryList);

        Task<QueryResult<MandatoryListIndexViewModel>> Search(MandatoryListSearchViewModel criteria);

        Task SendToApproval(int id);

        Task Approve(int id, string verificationCode);

        Task Reject(MandatoryListRejectionViewModel viewModel);

        Task Reopen(int id);

        Task Delete(int id);

        Task RequestDelete(int id);

        Task ApproveDelete(int id, string verificationCode);

        Task RejectDelete(MandatoryListRejectionViewModel viewModel);

        Task<MandatoryListDetailsViewModel> GetMandatoryListDetails(int mandatoryListId);

        Task<MandatoryListApprovalViewModel> GetMandatoryListDetailsForApproval(int mandatoryListId);

        Task ApproveEdit(int id, string verificationCode);

        Task RejectEdit(MandatoryListRejectionViewModel rejectionModel);

        Task CloseEdit(int id);

        Task<List<MandatoryListForQuantityTableViewModel>> GetAllForQuantitiyTable();
        Task<CSICodeDetailsModel> GetMandatoryListCSICodeInfo(string code);

        Task<Dictionary<string, bool>> GetValidMandatoryListCodeForQauntityTableExcel(List<string> CSICodes);

    }
}
