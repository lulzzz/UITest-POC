using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Data;
using System.Threading.Tasks;
namespace MOF.Etimad.Monafasat.Services
{
    public class BillCommand : IBillCommand
    {
        #region private Members
        private readonly  IAppDbContext _context;
        #endregion
        #region Constructor
        public BillCommand(IAppDbContext context)
        {
            _context = context;
        }
        #endregion
        #region DeleteBillAsync
        public async Task<BillInfo> DeleteBillAsync(BillInfo bill)
        {
            _context.BillInfos.Remove(bill);
            await _context.SaveChangesAsync();
            return bill;
        }
        #endregion
        #region UpdateBillAsync
        public async Task<BillInfo> UpdateBillAsync(BillInfo bill)
        {
            _context.BillInfos.Update(bill);
            await _context.SaveChangesAsync();
            return bill;
        }
        public async Task<BillInfo> UpdateWithoutSave(BillInfo bill)
        {
            _context.BillInfos.Update(bill);
            return bill;
        }
        public async Task<BillInfo> DeleteWithoutSave(BillInfo bill)
        {
            _context.BillInfos.Remove(bill);
            return bill;
        }
        public async Task<bool> Save()
        {
            await _context.SaveChangesAsync();
            return true;
        }
        #endregion
    }
}
