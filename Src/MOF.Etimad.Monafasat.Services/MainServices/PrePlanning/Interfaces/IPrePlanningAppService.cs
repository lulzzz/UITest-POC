using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.ViewModel;
using System.Threading.Tasks;
namespace MOF.Etimad.Monafasat.Services
{
    public interface IPrePlanningAppService
    {
        Task<QueryResult<PrePlanningModel>> FindPrePlanningBySearchCriteria(PrePlanningSearchCriteria criteria);
        Task<PrePlanningModel> CreateAsync(PrePlanningModel model);
        Task<PrePlanningModel> FindPrePlanningById(int id);
        Task<PrePlanningModel> GetPrePlanningDetailsById(int id);
        Task<PrePlanningModel> SetPrePlanningLookUps();
        Task SendToApprove(int planningId);
        Task Reject(int planningId, string rejectionReason);
        Task Approve(int planningId, string verificationCode);
        Task<int> Deactivate(int id, bool userRole);
        Task ReOpen(int planningId);
        Task ReOpenForCancelation(int planningId);

    }
}
