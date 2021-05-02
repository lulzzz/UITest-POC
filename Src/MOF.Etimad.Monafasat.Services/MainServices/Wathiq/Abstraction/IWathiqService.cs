using MOF.Etimad.Monafasat.ViewModel.Wathiq;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public interface IWathiqService
    {
        Task<WathiqViewModel> GetCRDataByCR(string crNumber);
    }
}
