using MOF.Etimad.Monafasat.Core;
using System.Threading.Tasks;
namespace MOF.Etimad.Monafasat.Services
{
    public interface IBlockCommands
    {
        Task<SupplierBlock> UpdateAsync(SupplierBlock block);
    }
}
