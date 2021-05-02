using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.ViewModel;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public interface IPrePlanningQueries
    {
        Task<QueryResult<PrePlanningModel>> FindPrePlanningBySearchCriteria(PrePlanningSearchCriteria criteria);
        Task<PrePlanning> FindById(int planningId, int? statusId = 0);
        Task<bool> GetByName(string name, int branchId, string agencyCode, int id);
        Task<PrePlanningModel> FindPrePlanningById(int id);
        Task<PrePlanningModel> GetPrePlanningDetailsById(int id);
        Task<PrePlanningModel> SetPrePlanningLookUps();
        Task<PrePlanning> FindByIdForEdit(int planningId, int? statusId = 0);
        Task<int> Deactivate(int id, bool userRole);
    }
}
