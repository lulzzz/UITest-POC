using MOF.Etimad.Monafasat.Core;
using MOF.Etimad.Monafasat.Core.Entities;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public interface ISupplierCommands
    {
        Task<Supplier> UpdateSupplierAsync(Supplier supplier);
        Task<Supplier> CreateSupplierAsync(Supplier supplier);
        Task<FavouriteSupplier> UpdateTenderFavouriteSupplierAsync(FavouriteSupplier favouriteSupplier);
        Task<FavouriteSupplier> CreateTenderFavouriteAsync(FavouriteSupplier favouriteSupplier);
        Task<FavouriteSupplierList> CreateFavouriteSupplierListAsync(FavouriteSupplierList favouriteSupplierList);
        Task<FavouriteSupplierList> UpdateFavouriteSupplierListAsync(FavouriteSupplierList favouriteSupplierList);
    }
}
