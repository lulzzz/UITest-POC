using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Integration;
using MOF.Etimad.Monafasat.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public interface IBillAppService
    {
        Task<bool> StoreBillsAndUploadToSadad(BillModel billInfoModel, BillInfo billInfo, string cr, int agencyType, bool throughA4Sadad = true);
        Task<string> SetBillPaid(PaymentNotificationServiceModel postedModel);
        Task<bool> UpdateBulkBillsStatus(List<BillModel> billModel);
        Task<bool> SetBillPaidForRolloutTeam(BillViewModel postedModel);
    }
}
