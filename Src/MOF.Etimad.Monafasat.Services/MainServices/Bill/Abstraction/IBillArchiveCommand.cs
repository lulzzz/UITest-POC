using MOF.Etimad.Monafasat.Core.Entities;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public interface IBillArchiveCommand
    {
        Task<BillArchive> CreateAsync(BillArchive billArchive);
        Task<BillArchive> CreateWithoutSave(BillArchive billArchive);
    }
}
