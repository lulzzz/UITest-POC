using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Data;
using System.Threading.Tasks;
namespace MOF.Etimad.Monafasat.Services
{
    public class BillArchiveCommand : IBillArchiveCommand
    {
        private readonly IAppDbContext _context;
        public BillArchiveCommand(IAppDbContext context)
        {
            _context = context;
        }
        public async Task<BillArchive> CreateAsync(BillArchive billArchive)
        {
            await _context.BillArchives.AddAsync(billArchive);
            await _context.SaveChangesAsync();
            return billArchive;
        }

        public async Task<BillArchive> CreateWithoutSave(BillArchive billArchive)
        {
            await _context.BillArchives.AddAsync(billArchive);
            return billArchive;
        }
    }
}
