using MOF.Etimad.Monafasat.Data;
using MOF.Etimad.Monafasat.SharedKernal.Exceptions;
using MOF.Etimad.Monafasat.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public class SupplierQualificationDocumentDomainService : ISupplierQualificationDocumentDomainService
    {

        private ISupplierQualificationDocumentQueries _supplierqualificationDocumentQueries;
        private readonly IAppDbContext _context;
        private readonly ISupplierQualificationDocumentCommands _supplierqualificationDocumentcommands;
        private readonly IBlockAppService _blockAppService;

        public SupplierQualificationDocumentDomainService(IAppDbContext context,
            ISupplierQualificationDocumentCommands supplierqualificationDocumentcommands,
            ISupplierQualificationDocumentQueries supplierqualificationDocumentQueries,
              IBlockAppService blockAppService)
        {
            _context = context;
            _supplierqualificationDocumentcommands = supplierqualificationDocumentcommands;
            _supplierqualificationDocumentQueries = supplierqualificationDocumentQueries;
            _blockAppService = blockAppService;
        }

        public async Task CheckApplyingDuplicationValidation(int PreQualificationId, string supplierId)
        {
            var supplierFinalRes = _context.QualificationFinalResult.Where(a => a.SupplierSelectedCr == supplierId && a.TenderId == PreQualificationId && a.IsActive == true).FirstOrDefault();
            if (supplierFinalRes != null)
            {
                throw new BusinessRuleException(Resources.QualificationResources.ErrorMessages.AlreadyApplied);
            }

        }

        public async Task CheckIfPreQualificationInCorrectStatus(int PreQualificationId, params Enums.TenderStatus[] tenderStatus)
        {
            List<int> statusIds = new List<int>();
            foreach (var item in tenderStatus)
            {
                statusIds.Add((int)item);
            }
            var tender = await _supplierqualificationDocumentQueries.GetPreQualificationbyIdAndStatus(PreQualificationId, statusIds);

            if (tender == null)
            {
                throw new BusinessRuleException(Resources.QualificationResources.ErrorMessages.QualificationNotApproved);
            }
        }

        public async Task CheckPresentationDateValidation(int PreQualificationId, string supplierId)
        {
            var tender = await _supplierqualificationDocumentQueries.GetTenderAndSupplierDocuments(PreQualificationId);
            if (tender.LastOfferPresentationDate.HasValue && tender.LastOfferPresentationDate.Value.Date < DateTime.Now.Date)
            {
                throw new BusinessRuleException(Resources.QualificationResources.ErrorMessages.AfterDate);
            }
            if (tender.LastOfferPresentationDate.HasValue &&
                tender.LastOfferPresentationDate.Value.Date == DateTime.Now.Date &&
                Convert.ToDateTime(tender.LastOfferPresentationDate.Value.ToString("hh:mm tt")).TimeOfDay < DateTime.Now.TimeOfDay)
            {
                throw new BusinessRuleException(Resources.QualificationResources.ErrorMessages.AfterDate);

            }
        }


        public async Task CheckIfSupplierBlocked(string CR)
        {
            if (await _blockAppService.CheckifSupplierBlockedByCrNo(CR))
            {
                throw new BusinessRuleException("لا يمكن تقديم لان السجل التجارى " + CR + " ممنوع من التقديم");
            }
        }

    }
}
