using MOF.Etimad.Monafasat.Integration;
using MOF.Etimad.Monafasat.ViewModel.Wathiq;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public class WathiqServices : IWathiqService
    {
        private readonly IWathiqProxy _wathiqProxy;
        public WathiqServices(IWathiqProxy wathiqProxy)
        {
            _wathiqProxy = wathiqProxy;
        }
        public async Task<WathiqViewModel> GetCRDataByCR(string crNumber)
        {
            return await _wathiqProxy.GetCRDataByCR(crNumber);
        }
    }
}
