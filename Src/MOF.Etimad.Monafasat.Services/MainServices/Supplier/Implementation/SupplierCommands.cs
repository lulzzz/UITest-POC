using MOF.Etimad.Monafasat.Core;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Data;
using System;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public class SupplierCommands : ISupplierCommands
    {
        private readonly IAppDbContext _context;
        public SupplierCommands(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<Supplier> UpdateSupplierAsync(Supplier supplier)
        {
            _context.Suppliers.Update(supplier);
            await _context.SaveChangesAsync();
            return supplier;
        }

        public async Task<Supplier> CreateSupplierAsync(Supplier supplier)
        {

            await _context.Suppliers.AddAsync(supplier);
            await _context.SaveChangesAsync();
            return supplier;
        }

        public async Task<FavouriteSupplierList> CreateFavouriteSupplierListAsync(FavouriteSupplierList favouriteSupplierList)
        {
            await _context.FavouriteSupplierLists.AddAsync(favouriteSupplierList);
            await _context.SaveChangesAsync();
            return favouriteSupplierList;
        }

        public async Task<FavouriteSupplierList> UpdateFavouriteSupplierListAsync(FavouriteSupplierList favouriteSupplierList)
        {
            _context.FavouriteSupplierLists.Update(favouriteSupplierList);
            await _context.SaveChangesAsync();
            return favouriteSupplierList;
        }

        public async Task<FavouriteSupplier> UpdateTenderFavouriteSupplierAsync(FavouriteSupplier favouriteSupplier)
        {
            try
            {
                _context.FavouriteSuppliers.Update(favouriteSupplier);
                await _context.SaveChangesAsync();
                return favouriteSupplier;
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public async Task<FavouriteSupplier> CreateTenderFavouriteAsync(FavouriteSupplier favouriteSupplier)
        {
            await _context.FavouriteSuppliers.AddAsync(favouriteSupplier);
            await _context.SaveChangesAsync();
            return favouriteSupplier;
        }
    }
}
