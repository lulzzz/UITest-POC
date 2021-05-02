using MOF.Etimad.Monafasat.Core;
using MOF.Etimad.Monafasat.Data;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public class BlockCommands : IBlockCommands
    {
        private readonly IAppDbContext _context;
        public BlockCommands(IAppDbContext context)
        {
            _context = context;
        }
        public async Task<SupplierBlock> UpdateAsync(SupplierBlock block)
        {
            _context.SupplierBlock.Update(block);
            await _context.SaveChangesAsync();
            return block;
        }
    }
}
