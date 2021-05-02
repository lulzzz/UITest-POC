using Microsoft.EntityFrameworkCore;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Data;
using System.Linq;
using System.Threading.Tasks;
using static MOF.Etimad.Monafasat.SharedKernel.Enums;
namespace MOF.Etimad.Monafasat.Services
{
    public class BillQueries : IBillQueries
    {
        #region private Members
        private readonly IAppDbContext _context;
        #endregion

        #region Constructor
        public BillQueries(IAppDbContext context)
        {
            _context = context;
        }
        #endregion
        #region Search for Captured Bills
        public async Task<BillInfo> FindBulkBillByInvoiceNumberWithTenderInfoAsync(string invoiceNumbers)
        {
            var result = await _context.BillInfos
                .Include(b => b.ConditionsBooklet.Supplier.SupplierUserProfiles)
                .ThenInclude(b => b.UserProfile)
                .Include(b => b.Invitation.Tender)
                .Where(a => a.BillInvoiceNumber == invoiceNumbers).AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<BillInfo> FindBillByInvoiceNumberWithTender(string invoiceNumbers)
        {
            var result = await _context.BillInfos
                .Include(b => b.ConditionsBooklet.Supplier.SupplierUserProfiles)
                .ThenInclude(b => b.UserProfile)
                .Include(b => b.ConditionsBooklet.Tender)
                .Where(a => a.BillInvoiceNumber == invoiceNumbers).AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<BillInfo> FindBillByInvoiceNumberAndAgencyCode(string invoiceNumber, string agencyCode)
        {
            var result = await _context.BillInfos
                .Where(a => a.BillInvoiceNumber == invoiceNumber)
                .Where(a => a.AgencyCode == agencyCode)
                .Where(a => a.BillStatusId == (int)BillStatus.SuccessUploaded).AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<BillInfo> FindBillByInvoiceNumberWithoutIncludesForRolloutTeam(string invoiceNumbers)
        {
            var result = await _context.BillInfos
                .Where(a => a.BillInvoiceNumber == invoiceNumbers)
                .FirstOrDefaultAsync();
            return result;
        }
        #endregion
    }
}
