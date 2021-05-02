using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.ViewModel.QuantityTableTemplates;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Integration
{
    public interface IQuantityTemplatesProxy
    {
        Task<ApiResponse<List<HtmlTemplateDto>>> GenerateSupplierReadOnlyTemplate(FormConfigurationDTO DTOModel);
        Task<ApiResponse<List<HtmlTemplateDto>>> GenerateCommitteeReadOnlyTemplate(FormConfigurationDTO DTOModel);
        Task<ApiResponse<FormConfigurationDTO>> GetMonafasatSupplierColumns(FormConfigurationDTO DTOModel);
        Task<ApiResponse<List<TableTemplateDTO>>> ValidateSupplierData(FormConfigurationDTO DTOModel);
        Task<ApiResponse<List<HtmlTemplateDto>>> GenerateSupplierTemplate(FormConfigurationDTO DTOModel);
        Task<ApiResponse<List<HtmlTemplateDto>>> GetMonafasatGOV(FormConfigurationDTO DTOModel);
        Task<ApiResponse<List<HtmlTemplateDto>>> GetBayanMonafasatGOV(FormConfigurationDTO DTOModel);
        Task<ApiResponse<List<HtmlTemplateDto>>> GenerateTemplate(FormConfigurationDTO DTOModel);
        Task<ApiResponse<List<HtmlTemplateDto>>> GetMonafasatGOVReadOnly(FormConfigurationDTO DTOModel);
        Task<ApiResponse<List<HtmlTemplateDto>>> GenerateNegotiationTemplate(FormConfigurationDTO DTOModel);
        Task<ApiResponse<List<HtmlTemplateDto>>> GetNegotiationGOVTemplates(FormConfigurationDTO DTOModel);
        Task<string> GetTemplate(int tenderId, int FormId, int YearsCount);
        Task<int> GetQuantityTableVersion();
        Task<ApiResponse<HtmlTemplateDto>> GetTemplateFormHtml(int tenderId, int FormId, int YearsCount, long tableId);
        Task<ApiResponse<HtmlTableRowTemplateDto>> ValidateTenderQuantityItem(int Years, Dictionary<string, string> collection);
        Task<ApiResponse<HtmlTableRowTemplateDto>> ImportTenderTableQuantityItems(UploadTableExcelModel model);

        Task<ApiResponse<HtmlTableRowTemplateDto>> ValidateAlternativeQuantityItem(int Years, Dictionary<string, string> collection);
        Task<ApiResponse<List<HtmlTemplateDto>>> GetMonafasatSupplierForOpening(FormConfigurationDTO DTOModel);
        Task<ApiResponse<List<HtmlTemplateDto>>> GetMonafasatSupplierForChecking(FormConfigurationDTO DTOModel);
        Task<ApiResponse<List<HtmlTemplateDto>>> GenerateNegotiationReadOnlyTemplate(FormConfigurationDTO DTOModel);
        Task<ApiResponse<TotalCostDTO>> CalculateOfferFinalPricebyItems(FormConfigurationDTO DTOModel);
        Task<ApiResponse<TotalCostDTO>> GetSupplierTotalCostNP(FormConfigurationDTO DTOModel);
        Task<ApiResponse<List<HtmlTemplateDto>>> GenerateCommitteeTemplate(FormConfigurationDTO DTOModel);
        Task<ApiResponse<List<TableTemplateDTO>>> ValidateOpeningData(FormConfigurationDTO DTOModel);
        Task<ApiResponse<List<TableTemplateDTO>>> NegotiationValidateQuantityItem(FormConfigurationDTO DTOModel);
        Task<ApiResponse<List<HtmlTemplateDto>>> GetTemplateFormHtml(int tenderId, int offerId, int FormId = 1, int YearsCount = 5, long tableId = 0);
        Task<ApiResponse<List<TableTemplateDTO>>> ValidateCheckingData(FormConfigurationDTO DTOModel);
        Task<ApiResponse<TotalCostDTO>> GetTotalCostForChecking(FormConfigurationDTO DTOModel);
        Task<ApiResponse<List<HtmlTemplateDto>>> GenerateCostTable(FormConfigurationDTO DTOModel);
        Task<ApiResponse<HtmlTemplateDto>> GetBayanTemplateFormHtml(int tenderId, int FormId = 1, int YearsCount = 5, long tableId = 0);
        Task<ApiResponse<List<TemplateFormDTO>>> GetActivitiesTables(TenderActivityDTO tenderActivityDTO);
        Task<ApiResponse<List<HtmlTemplateDto>>> GenerateGovTableTemplate(FormConfigurationDTO DTOModel);
        Task<ApiResponse<List<HtmlTemplateDto>>> GetSupplierTemplatesToApplyOffer(FormConfigurationDTO DTOModel);
        Task<ExcelHeader> GenerateQuantityTableTemplateExcelHeader(int stageId, int formId, int yearsCount);
        Task<QuantityTableModelForExcel> GenerateQuantityTableTemplateExcel(int stageId, int formId, int yearsCount);
        Task<ApiResponse<List<TemplateFormDTO>>> GenerateTemplatesByFormIds(List<long> formIds);
        Task<ApiResponse<List<EmarketPlaceResponse>>> GetEmarketPlace(List<EmarketPlaceRequest> emarketPlaceRequest);
        Task<List<int>> GetCanIgnoreMandatoryValidationColumnsId();
        Task<List<int>> GetFormIdsWithTemplateIdAndQtVersionId(int templateId, int qtVersionId);
        Task<ApiResponse<List<long>>> GetMadatoryListColumnIdByTemplatesId(int versionid, List<int> items);
        Task<List<ColumnDto>> GetCostColumnsIdByFormIdForNotSupply(List<long> formIds);
    }
}
