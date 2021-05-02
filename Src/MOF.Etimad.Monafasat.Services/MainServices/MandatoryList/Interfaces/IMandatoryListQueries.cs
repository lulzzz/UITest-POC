using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public interface IMandatoryListQueries
    {
        Task<MandatoryList> Find(int id);
        Task<MandatoryListViewModel> ProjectedFind(int id);
        Task<QueryResult<MandatoryListIndexViewModel>> Search(MandatoryListSearchViewModel criteria);
        Task<MandatoryListDetailsViewModel> GetMandatoryListDetails(int mandatoryListId);
        Task<MandatoryListApprovalViewModel> GetMandatoryListDetailsForApproval(int mandatoryListId);
        Task<List<MandatoryListForQuantityTableViewModel>> GetAllForQuantityTable();
        Task<CSICodeDetailsModel> GetMandatoryListCSICodeInfo(string code);
        Task<List<string>> GetValidMandatoryListCodeForQauntityTableExcel(List<string> CSICodes);
    }
}
