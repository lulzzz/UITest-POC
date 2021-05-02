using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.Integration;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.ViewModel;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace MOF.Etimad.Monafasat.IntegrationTests.Proxies
{
    public class QuantityTemplatesProxyTest
    {
        private readonly QuantityTemplatesProxy _QuantityTemplatesProxy;
        private readonly Mock<IOptionsSnapshot<RootConfigurations>> _rootConfiguration;
        private readonly RootConfigurations _configuration;

        public QuantityTemplatesProxyTest()
        {
            _configuration = SetupConfigurations.GetApplicationConfiguration(Directory.GetCurrentDirectory());

            _rootConfiguration = new Mock<IOptionsSnapshot<RootConfigurations>>();
            _rootConfiguration.Setup(m => m.Value).Returns(_configuration);
            _QuantityTemplatesProxy = new QuantityTemplatesProxy(_rootConfiguration.Object);
        }

        [Fact]
        public async Task ShouldGetTemplateFormHtml()
        {
            //Arrange
            int tenderId = 7;
            //Act
            var result = await _QuantityTemplatesProxy.GetTemplateFormHtml(tenderId);
            //Assert
            Assert.NotNull(result);

        }

        [Fact]
        public async Task ShouldGetBayanTemplateFormHtml()
        {
            //Arrange
            int tenderId = 7;
            //Act
            var result = await _QuantityTemplatesProxy.GetBayanTemplateFormHtml(tenderId);
            //Assert
            Assert.NotNull(result);

        }

        [Fact]
        public async Task ShouldGetTemplateFormHtmlWithOffer()
        {
            //Arrange
            int tenderId = 7;
            int offerId = 1;
            //Act
            var result = await _QuantityTemplatesProxy.GetTemplateFormHtml(tenderId, offerId, 1, 5, 0);
            //Assert
            Assert.NotNull(result);

        }

        [Fact]
        public async Task ShouldGetQuantityTableVersion()
        {
            //Arrange

            //Act
            var result = await _QuantityTemplatesProxy.GetQuantityTableVersion();
            //Assert
            Assert.NotEqual(0, result);

        }


#pragma warning disable S125 // Sections of code should not be commented out
        //No HTTP resource was found that matches the request URI 'https://10.14.9.32:8886/api/QuantityTableApi/GetFormItemTemplate?FormId=1&YearsCount=5&TenderId=7'.\"}
#pragma warning restore S125 // Sections of code should not be commented out
        [Fact]
        public async Task ShouldGetTemplate()
        {
            //Arrange
            int tenderId = 7;
            //Act
            var result = await _QuantityTemplatesProxy.GetTemplate(tenderId);
            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldGetActivitiesTables()
        {
            //Arrange
            var model = new TenderActivityDTO { VesionId = 5000, ActivityIdsList = new List<int> { 13 } };
            //Act
            var result = await _QuantityTemplatesProxy.GetActivitiesTables(model);
            //Assert
            Assert.NotNull(result);
        }

        private FormConfigurationDTO GetFormConfigurationDTO()
        {
            return new FormConfigurationDTO
            {
                TenderId = 21759,
                QuantityItemDtos = new List<TenderQuantityItemDTO> {
                new TenderQuantityItemDTO {
                ColumnId = 5792,
                ColumnName = "وحدة القياس",
                Id = 313501,
                IsDefault = false,
                ItemNumber = 1,
                TableId= 22249 ,
                TableName =  "اسم الجدول",
                TemplateId=  13  ,
                TenderFormHeaderId = 0 ,
                TenderId =21759,
                Value = "d",
                },
                new TenderQuantityItemDTO {
                 ColumnId  =  5789,
                 ColumnName = "الوصف" ,
                 Id = 313502  ,
                 IsDefault =  false,
                 ItemNumber = 1,
                 TableId= 22249,
                 TableName  = "اسم الجدول",
                 TemplateId=  13 ,
                 TenderFormHeaderId=  0 ,
                 TenderId =   21759,
                 Value =  "Desc"

                }

            }
            };
        }

        [Fact]
        public async Task ShouldGetMonafasatGOVReadOnly()
        {
            //Arrange
            var model = GetFormConfigurationDTO();
            //Act
            var result = await _QuantityTemplatesProxy.GetMonafasatGOVReadOnly(model);
            //Assert
            Assert.NotNull(result);

        }

        [Fact]
        public async Task ShouldGetMonafasatGOV()
        {
            //Arrange
            var model = GetFormConfigurationDTO();
            //Act
            var result = await _QuantityTemplatesProxy.GetMonafasatGOV(model);
            //Assert
            Assert.NotNull(result);

        }

        //System.Exception: حدث خطاء فى جلب البيانات ---> System.Exception: حدث خطاء فى جلب البيانات ---> System.NullReferenceException: Object reference not set to an instance of an object.
        [Fact]
        public async Task ShouldGetSupplierTemplatesToApplyOffer()
        {
            //Arrange
            var model = GetFormConfigurationDTO();
            //Act
            var result = await _QuantityTemplatesProxy.GetSupplierTemplatesToApplyOffer(model);
            //Assert
            Assert.NotNull(result);

        }

        [Fact]
        public async Task ShouldGenerateGovTableTemplate()
        {
            //Arrange
            var model = GetFormConfigurationDTO();
            //Act
            var result = await _QuantityTemplatesProxy.GenerateGovTableTemplate(model);
            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldGetBayanMonafasatGOV()
        {
            //Arrange
            var model = GetFormConfigurationDTO();
            //Act
            var result = await _QuantityTemplatesProxy.GetBayanMonafasatGOV(model);
            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldGenerateTemplate()
        {
            //Arrange
            var model = GetFormConfigurationDTO();
            //Act
            var result = await _QuantityTemplatesProxy.GenerateTemplate(model);
            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldGetMonafasatSupplierForOpening()
        {
            //Arrange
            var model = GetFormConfigurationDTO();
            //Act
            var result = await _QuantityTemplatesProxy.GetMonafasatSupplierForOpening(model);
            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldGetMonafasatSupplierForChecking()
        {
            //Arrange
            var model = GetFormConfigurationDTO();
            //Act
            var result = await _QuantityTemplatesProxy.GetMonafasatSupplierForChecking(model);
            //Assert
            Assert.NotNull(result);
        }

        //Sequence contains no elements
        [Fact]
        public async Task ShouldGenerateCostTable()
        {
            //Arrange
            var model = GetFormConfigurationDTO();
            //Act
            var result = await _QuantityTemplatesProxy.GenerateCostTable(model);
            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldGetTotalCostForChecking()
        {
            //Arrange
            var model = GetFormConfigurationDTO();
            //Act
            var result = await _QuantityTemplatesProxy.GetTotalCostForChecking(model);
            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldValidateCheckingData()
        {
            //Arrange
            var model = GetFormConfigurationDTO();
            //Act
            var result = await _QuantityTemplatesProxy.ValidateCheckingData(model);
            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldValidateOpeningData()
        {
            //Arrange
            var model = GetFormConfigurationDTO();
            //Act
            var result = await _QuantityTemplatesProxy.ValidateOpeningData(model);
            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldGenerateSupplierTemplate()
        {
            //Arrange
            var model = GetFormConfigurationDTO();
            //Act
            var result = await _QuantityTemplatesProxy.GenerateSupplierTemplate(model);
            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldGenerateSupplierReadOnlyTemplate()
        {
            //Arrange
            var model = GetFormConfigurationDTO();
            //Act
            var result = await _QuantityTemplatesProxy.GenerateSupplierReadOnlyTemplate(model);
            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldGenerateCommitteeReadOnlyTemplate()
        {
            //Arrange
            var model = GetFormConfigurationDTO();
            //Act
            var result = await _QuantityTemplatesProxy.GenerateCommitteeReadOnlyTemplate(model);
            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldGenerateCommitteeTemplate()
        {
            //Arrange
            var model = GetFormConfigurationDTO();
            //Act
            var result = await _QuantityTemplatesProxy.GenerateCommitteeTemplate(model);
            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldGetMonafasatSupplierColumns()
        {
            //Arrange
            var model = GetFormConfigurationDTO();
            //Act
            var result = await _QuantityTemplatesProxy.GetMonafasatSupplierColumns(model);
            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldValidateSupplierData()
        {
            //Arrange
            var model = GetFormConfigurationDTO();
            //Act
            var result = await _QuantityTemplatesProxy.ValidateSupplierData(model);
            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldGenerateNegotiationTemplate()
        {
            //Arrange
            var model = GetFormConfigurationDTO();
            //Act
            var result = await _QuantityTemplatesProxy.GenerateNegotiationTemplate(model);
            //Assert
            Assert.NotNull(result);
        }

        //No HTTP resource was found that matches the request URI 'https://10.14.9.32:8886/api/QuantityTable/Negotiation/GenerateNegotiationGOVTemplates
        [Fact]
        public async Task ShouldGetNegotiationGOVTemplates()
        {
            //Arrange
            var model = GetFormConfigurationDTO();
            //Act
            var result = await _QuantityTemplatesProxy.GetNegotiationGOVTemplates(model);
            //Assert
            Assert.NotNull(result);
        }


        [Fact]
        public async Task ShouldGenerateNegotiationReadOnlyTemplate()
        {
            //Arrange
            var model = GetFormConfigurationDTO();
            //Act
            var result = await _QuantityTemplatesProxy.GenerateNegotiationReadOnlyTemplate(model);
            //Assert
            Assert.NotNull(result);
        }


        [Fact]
        public async Task ShouldCalculateOfferFinalPricebyItems()
        {
            //Arrange
            var model = GetFormConfigurationDTO();
            //Act
            var result = await _QuantityTemplatesProxy.CalculateOfferFinalPricebyItems(model);
            //Assert
            Assert.NotNull(result);
        }


        [Fact]
        public async Task ShouldGetSupplierTotalCostNP()
        {
            //Arrange
            var model = GetFormConfigurationDTO();
            //Act
            var result = await _QuantityTemplatesProxy.GetSupplierTotalCostNP(model);
            //Assert
            Assert.NotNull(result);
        }


        [Fact]
        public async Task ShouldNegotiationValidateQuantityItem()
        {
            //Arrange
            var model = GetFormConfigurationDTO();
            //Act
            var result = await _QuantityTemplatesProxy.NegotiationValidateQuantityItem(model);
            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldGenerateQuantityTableTemplateExcelHeader()
        {
            //Arrange
            int stageId = 1;
            int formId = 5011;
            int yearsCount = 0;

            //Act
            var result = await _QuantityTemplatesProxy.GenerateQuantityTableTemplateExcelHeader(stageId, formId, yearsCount);
            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldGenerateQuantityTableTemplateExcel()
        {
            //Arrange
            int stageId = 1;
            int formId = 5011;
            int yearsCount = 0;

            //Act
            var result = await _QuantityTemplatesProxy.GenerateQuantityTableTemplateExcel(stageId, formId, yearsCount);
            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldValidateTenderQuantityItem()
        {
            //Arrange
            var dictionary = new Dictionary<string, string>() {
                { "FormId", "5011" },{ "TableId", "22261" },{ "IntialTableId", "22261" },{ "TenderId", "21762" },
                { "TableName", "" },{ "ItemNumber", "" },{ "0_5073", "23" },{ "0_5074", "" },{ "0_5075", "test" },
                { "0_5076", "3" },{ "0_6399", "1" },{ "0_6150", "9999" },{ "0_5887", "" },{ "0_5789", "test" },
                { "0_6030", "2009" },{ "0_5792", "2" },
            };
            //Act
            var result = await _QuantityTemplatesProxy.ValidateTenderQuantityItem(0, dictionary);
            //Assert
            Assert.NotNull(result);
        }

        //The given key was not present in the dictionary.
        [Fact]
        public async Task ShouldValidateAlternativeQuantityItem()
        {
            //Arrange
            var dictionary = new Dictionary<string, string>() {
                { "FormId", "5011" },{ "TableId", "22261" },{ "IntialTableId", "22261" },{ "TenderId", "21762" },
                { "TableName", "" },{ "ItemNumber", "" },{ "0_5073", "23" },{ "0_5074", "" },{ "0_5075", "test" },
                { "0_5076", "3" },{ "0_6399", "1" },{ "0_6150", "9999" },{ "0_5887", "" },{ "0_5789", "test" },
                { "0_6030", "2009" },{ "0_5792", "2" },
            };
            //Act
            var result = await _QuantityTemplatesProxy.ValidateAlternativeQuantityItem(0, dictionary);
            //Assert
            Assert.NotNull(result);
        }

        //The file is not an valid Package file. If the file is encrypted, please supply the password in the constructor
        [Fact]
        public async Task ShouldImportTenderTableQuantityItems()
        {
            //Arrange
            var model = new UploadTableExcelModel
            {
                FileContentField = Getbytes(),
                FileLengthField = Getbytes().Length.ToString(),
                FileNameField = "test.png",
                FormId = 5011,
                IsNewTable = false,
                TableId = 22264,
                TableName = "اسم الجدول",
                TenderId = 21762,
                Years = 0
            };
            //Act
            var result = await _QuantityTemplatesProxy.ImportTenderTableQuantityItems(model);
            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldGetEmarketPlace()
        {
            //Arrange
            var model = new List<EmarketPlaceRequest>() {
                new EmarketPlaceRequest{
                    FormId = 5011,
                    TableId = 22264
                }
            };
            //Act
            var result = await _QuantityTemplatesProxy.GetEmarketPlace(model);
            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldGetFormIdsWithTemplateIdAndQtVersionId()
        {
            var result = await _QuantityTemplatesProxy.GetFormIdsWithTemplateIdAndQtVersionId(22, 15000);

            Assert.NotNull(result);
            Assert.Equal("4", result.Count.ToString());
        }

        [Fact]
        public async Task ShouldGetCostColumnsIdByFormIdForNotSupply()
        {
            // 1 supply template and 1 not
            var result = await _QuantityTemplatesProxy.GetCostColumnsIdByFormIdForNotSupply(new List<long>() { 15005, 15050 });

            Assert.NotNull(result);
            Assert.Equal("1", result.Count.ToString());
        }

        private byte[] Getbytes()
        {

            FileStream fs = File.OpenRead("../../../logo.PNG");
            try
            {
                byte[] bytes = new byte[fs.Length];
                fs.Read(bytes, 0, Convert.ToInt32(fs.Length));
                fs.Close();
                return bytes;
            }
            finally
            {
                fs.Close();
            }
        }
    }
}
