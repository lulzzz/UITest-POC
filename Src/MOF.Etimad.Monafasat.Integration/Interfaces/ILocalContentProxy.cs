using MOF.Etimad.Monafasat.ViewModel;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Integration
{
    public interface ILocalContentProxy
    {
        Task<LocalContentServiceResponse<decimal?>> GetBaselineScoreInquiry(string supplierId);
        Task<LocalContentServiceResponse<decimal?>> GetTargetPlanScoreInquiry(string supplierId, string tenderId);

    }
}
