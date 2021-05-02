using MOF.Etimad.Monafasat.Core.Entities;
using System.Threading.Tasks;
namespace MOF.Etimad.Monafasat.Services
{
    public interface IBillQueries
    {
        Task<BillInfo> FindBillByInvoiceNumberAndAgencyCode(string invoiceNumber, string agencyCode);
        Task<BillInfo> FindBulkBillByInvoiceNumberWithTenderInfoAsync(string invoiceNumbers);
        Task<BillInfo> FindBillByInvoiceNumberWithTender(string invoiceNumbers);
        Task<BillInfo> FindBillByInvoiceNumberWithoutIncludesForRolloutTeam(string invoiceNumbers);
    }
}
