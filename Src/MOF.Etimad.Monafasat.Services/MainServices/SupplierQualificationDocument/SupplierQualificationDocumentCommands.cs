using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Data;
using MOF.Etimad.Monafasat.Data.GenericRepository;
using MOF.Etimad.SharedKernel;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services.MainServices.SupplierQualificationDocument.Abstract
{
    public class SupplierQualificationDocumentCommands : GenericCommandRepository, ISupplierQualificationDocumentCommands
    {
        private readonly IAppDbContext _context;
        public SupplierQualificationDocumentCommands(IAppDbContext context) : base(context)
        {
            _context = context;
        }


        public async Task<SupplierPreQualificationDocument> UpdateAsync(SupplierPreQualificationDocument supplierPreQualificationDocument)
        {
            Check.ArgumentNotNull(nameof(supplierPreQualificationDocument), supplierPreQualificationDocument);
            _context.SupplierPreQualificationDocument.Update(supplierPreQualificationDocument);
            await _context.SaveChangesAsync();
            return supplierPreQualificationDocument;
        }

    }
}
