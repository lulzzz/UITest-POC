using MOF.Etimad.Monafasat.Core.Entities;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public interface IBillCommand
    {
        Task<BillInfo> UpdateBillAsync(BillInfo bill);
        Task<BillInfo> DeleteBillAsync(BillInfo bill);
        Task<BillInfo> UpdateWithoutSave(BillInfo bill);
        Task<BillInfo> DeleteWithoutSave(BillInfo bill);
        Task<bool> Save();
    }
}
