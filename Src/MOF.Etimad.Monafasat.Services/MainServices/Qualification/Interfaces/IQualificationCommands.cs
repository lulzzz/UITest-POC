using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Core.Entities.QualificationEvaluation;
using MOF.Etimad.Monafasat.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public interface IQualificationCommands
    {
        Task<FavouriteSupplierTender> UpdateTenderFavouriteListAsync(FavouriteSupplierTender favouriteSupplierTenders);
        Task<FavouriteSupplierTender> CreateTenderFavouriteAsync(FavouriteSupplierTender favouriteSupplierTenders);
        Task<Tender> UpdatePreQualificationAsync(Tender tender);
        Task<Tender> UpdateAsync(Tender tender);
        void CreateQualificationConfigurationAsync(List<QualificationConfiguration> qualificationConfiguration);
        void CreateSubConfigurationAsync(List<QualificationSubCategoryConfiguration> qualificationSubCategoryConfiguration);

        Task<string> UpdateReferenceNumber();

        Task AddQualificationSupplierDataAsync(List<QualificationSupplierData> qualificationSupplierDatas);

        Task AddQualificationSupplierDataYearly(List<QualificationSupplierDataYearly> qualificationSupplierDataYearlies);
        Task AddQualificationSubCategoryResult(List<QualificationSubCategoryResult> qualificationSubCategoryResults);
        Task AddQualificationCategoryResult(List<QualificationCategoryResult> qualificationCategoryResults);
        Task AddQualificationFinalResult(QualificationFinalResult qualificationFinalResults);


        Task RemoveSupplierDocAndEvaluationAsync(PreQualificationApplyDocumentsModel model, string supplierId);

        void UpdateFinalResult(QualificationFinalResult qualificationFinalResult);
        Task UpdateFinalResultAsync(QualificationFinalResult qualificationFinalResult);
        void UpdateQualificationDocument(SupplierPreQualificationDocument supplierPreQualificationAttachments);

        void UpdateQualificationAttachment(SupplierPreQualificationAttachment supplierPreQualificationAttachments);

        void DeactiveQualificationSupplierDataYearly(QualificationSupplierDataYearly qualificationSupplierDataYearly);

        void DeactiveQualificationSupplierProject(QualificationSupplierProject qualificationSupplierProject);

        void DeactiveQualificationSupplierData(QualificationSupplierData qualificationSupplierData);

        void DeactivequalificationConfigurationAttachment(QualificationConfigurationAttachment qualificationConfigurationAttachment);
    }
}