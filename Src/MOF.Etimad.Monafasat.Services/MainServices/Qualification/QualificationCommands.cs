using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Core.Entities.QualificationEvaluation;
using MOF.Etimad.Monafasat.Data;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.SharedKernel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public class QualificationCommands : IQualificationCommands
    {
        private IAppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public QualificationCommands(IAppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        #region  Service Commands
        public async Task<Tender> UpdatePreQualificationAsync(Tender tender)
        {
            _context.Tenders.Update(tender);
            await _context.SaveChangesAsync();
            return tender;
        }
        public async Task<FavouriteSupplierTender> UpdateTenderFavouriteListAsync(FavouriteSupplierTender favouriteSupplierTenders)
        {
            _context.FavouriteSupplierTenders.Update(favouriteSupplierTenders);
            await _context.SaveChangesAsync();
            return favouriteSupplierTenders;
        }

        public async Task<FavouriteSupplierTender> CreateTenderFavouriteAsync(FavouriteSupplierTender favouriteSupplierTenders)
        {
            await _context.FavouriteSupplierTenders.AddAsync(favouriteSupplierTenders);
            await _context.SaveChangesAsync();
            return favouriteSupplierTenders;
        }

        public async Task<Tender> UpdateAsync(Tender tender)
        {
            Check.ArgumentNotNull(nameof(tender), tender);
            _context.Tenders.Update(tender);
            await _context.SaveChangesAsync();
            return tender;
        }

        public async Task<string> UpdateReferenceNumber()
        {
            return await _context.GenerateReferenceNumber((int)Enums.ReferenceNumberType.Qualification);
        }

        public void CreateQualificationConfigurationAsync(List<QualificationConfiguration> qualificationConfiguration)
        {
            Check.ArgumentNotNull(nameof(qualificationConfiguration), qualificationConfiguration);
            _context.QualificationConfiguration.AddRange(qualificationConfiguration);
        }



        public void CreateSubConfigurationAsync(List<QualificationSubCategoryConfiguration> qualificationSubCategoryConfiguration)
        {
            Check.ArgumentNotNull(nameof(qualificationSubCategoryConfiguration), qualificationSubCategoryConfiguration);
            _context.QualificationSubCategoryConfiguration.AddRange(qualificationSubCategoryConfiguration);
        }

        #endregion




        #region QualificationService


        public async Task AddQualificationSupplierDataAsync(List<QualificationSupplierData> qualificationSupplierDatas)
        {
            await _context.QualificationSupplierData.AddRangeAsync(qualificationSupplierDatas);
        }


        public async Task AddQualificationSupplierDataYearly(List<QualificationSupplierDataYearly> qualificationSupplierDataYearlies)
        {
            await _context.QualificationSupplierDataYearly.AddRangeAsync(qualificationSupplierDataYearlies);
        }

        public async Task AddQualificationSubCategoryResult(List<QualificationSubCategoryResult> qualificationSubCategoryResults)
        {
            await _context.QualificationSubCategoryResult.AddRangeAsync(qualificationSubCategoryResults);
        }

        public async Task AddQualificationCategoryResult(List<QualificationCategoryResult> qualificationCategoryResults)
        {
            await _context.QualificationCategoryResult.AddRangeAsync(qualificationCategoryResults);
        }

        public async Task AddQualificationFinalResult(QualificationFinalResult qualificationFinalResults)
        {
            await _context.QualificationFinalResult.AddRangeAsync(qualificationFinalResults);
        }


        public void UpdateQualificationDocument(SupplierPreQualificationDocument supplierPreQualificationDocument)
        {
            _context.SupplierPreQualificationDocument.Update(supplierPreQualificationDocument);
        }


        public void UpdateQualificationAttachment(SupplierPreQualificationAttachment supplierPreQualificationAttachments)
        {
            _context.SupplierPreQualificationAttachment.Update(supplierPreQualificationAttachments);
        }

        public void UpdateFinalResult(QualificationFinalResult qualificationFinalResult)
        {
            _context.QualificationFinalResult.Update(qualificationFinalResult);

        }
        public async Task UpdateFinalResultAsync(QualificationFinalResult qualificationFinalResult)
        {
            _context.QualificationFinalResult.Update(qualificationFinalResult);
            await _context.SaveChangesAsync();
        }

        public void DeactiveQualificationSupplierDataYearly(QualificationSupplierDataYearly qualificationSupplierDataYearly)
        {
            _context.QualificationSupplierDataYearly.Update(qualificationSupplierDataYearly);
        }


        public void DeactiveQualificationSupplierProject(QualificationSupplierProject qualificationSupplierProject)
        {
            _context.QualificationSupplierProject.Update(qualificationSupplierProject);
        }

        public void DeactiveQualificationSupplierData(QualificationSupplierData qualificationSupplierData)
        {
            _context.QualificationSupplierData.Update(qualificationSupplierData);
        }

        public void DeactivequalificationConfigurationAttachment(QualificationConfigurationAttachment qualificationConfigurationAttachment)
        {
            _context.QualificationConfigurationAttachment.Update(qualificationConfigurationAttachment);
        }


        public async Task RemoveSupplierDocAndEvaluationAsync(PreQualificationApplyDocumentsModel model, string supplierId)
        {
            model.PreQualificationId = Util.Decrypt(model.PreQualificationIdString);


            var supplierFinancialData = await _context.QualificationSupplierDataYearly.Where(a => a.TenderId == model.PreQualificationId && a.SupplierSelectedCr == supplierId).ToListAsync();



            var supplierData = await _context.QualificationSupplierData.Where(a => a.SupplierSelectedCr == supplierId && a.TenderId == model.PreQualificationId).Includes(a => a.QualificationSupplierProjects).Includes(a => a.QualificationConfigurationAttachments).ToListAsync();


            var finalresult = await _context.QualificationFinalResult.Where(a => a.SupplierSelectedCr == supplierId && a.TenderId == model.PreQualificationId).FirstOrDefaultAsync();





            _context.QualificationSupplierDataYearly.RemoveRange(supplierFinancialData);
            _context.QualificationSupplierData.RemoveRange(supplierData);
            _context.QualificationFinalResult.RemoveRange(finalresult);

            await _context.SaveChangesAsync();
        }

        #endregion


    }
}
