using MOF.Etimad.Monafasat.ViewModel.Wathiq;
using System.Threading.Tasks;
namespace MOF.Etimad.Monafasat.Integration
{
    public interface IWathiqProxy
    {
        Task<WathiqViewModel> GetCRDataByCR(string crNumber);
    }
}
