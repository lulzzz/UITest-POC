using MOF.Etimad.Monafasat.Core.Entities;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Integration
{
    public interface IBillingProxy
    {
        Task<string> CreateNewBill(NewBillModel newBillModel);
        Task<bool> CancelBill(BillModelForCancelRequest newBillModel);
        Task<bool> UpdateBill(BillModelForUpdateRequest newBillModel);
        Task<bool> UpdateBillActionStatus(BillInfo bill);
    }
}
