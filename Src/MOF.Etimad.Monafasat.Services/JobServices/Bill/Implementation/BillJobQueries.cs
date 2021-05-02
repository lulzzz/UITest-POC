using Microsoft.EntityFrameworkCore;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Data;
using MOF.Etimad.Monafasat.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public class BillJobQueries : IBillJobQueries
    {
        private readonly IAppDbContext _context;
        public BillJobQueries(IAppDbContext context)
        {
            _context = context;
        }
        public async Task<List<BillInfo>> GetAllBillInfos()
        {
            var tenderEntity = await _context.BillInfos
                .Include(i => i.Invitation.Tender)
                .Include(b => b.ConditionsBooklet.Tender)
                .Where(x => x.BillStatusId != (int)Enums.BillStatus.Paid && x.ExpiryDate > DateTime.Now && x.ActionStatus.HasValue && (x.ActionStatus.Value == (int)Enums.BillActionStatus.UpdateBill || x.ActionStatus.Value == (int)Enums.BillActionStatus.CancelBill))
                .ToListAsync();
            return tenderEntity;
        }
        public async Task<BillInfo> FindBillByInvoiceNumberWithoutIncludes(string invoiceNumbers)
        {
            var result = await _context.BillInfos
                .Where(a => a.BillInvoiceNumber == invoiceNumbers)
                .FirstOrDefaultAsync();
            return result;
        }
    }
}
