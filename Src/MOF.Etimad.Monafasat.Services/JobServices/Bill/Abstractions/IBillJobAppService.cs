using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public interface IBillJobAppService
    {
        Task<bool> UpdateBillThroughTahseel();
    }
}
