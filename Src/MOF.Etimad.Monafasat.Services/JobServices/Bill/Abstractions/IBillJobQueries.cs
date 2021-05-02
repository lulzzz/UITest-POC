using MOF.Etimad.Monafasat.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public interface IBillJobQueries
    {
        Task<List<BillInfo>> GetAllBillInfos();
        Task<BillInfo> FindBillByInvoiceNumberWithoutIncludes(string invoiceNumbers);
    }
}
