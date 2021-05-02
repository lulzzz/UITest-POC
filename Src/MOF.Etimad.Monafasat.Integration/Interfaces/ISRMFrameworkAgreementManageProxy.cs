using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Integration
{
    public interface ISRMFrameworkAgreementManageProxy
    {
        Task<bool> SRMFrameworkAgreementManage(SRMFrameworkAgreementManageModel model);
    }
}
