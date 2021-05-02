using MOF.Etimad.Monafasat.Core.Entities;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Tenders
{
    public class TenderConditionsTemplateMaterialInofmrationTest
    {
        private const string basicInformation = "info";
        private const string requiredDcoumentationBefore = "doc";
        private const string tests = "name";
        private const string intilizationAndStartWork = "init";
        private const string requiredDcoumentationAfter = "doc";
        private const string trainging = "trainging";
        private const string guarantee = "guarantee";
        private const string maintanance = "maintanance";
        private const string machineGuarantee = "machineGuarantee";
        private const string machineMaintanance = "machineMaintanance";

        [Fact]
        public void Should_Construct_TenderConditionsTemplateMaterialInofmration()
        {
            TenderConditionsTemplateMaterialInofmration tenderConditionsTemplateMaterialInofmration = new TenderConditionsTemplateMaterialInofmration(basicInformation, requiredDcoumentationBefore, tests, intilizationAndStartWork,
                requiredDcoumentationAfter, trainging, guarantee, maintanance, machineGuarantee, machineMaintanance);
            _ = new TenderConditionsTemplateMaterialInofmration();
            _ = tenderConditionsTemplateMaterialInofmration.TenderConditionsTemplate;

            tenderConditionsTemplateMaterialInofmration.ShouldNotBeNull();
        }

        [Fact]
        public void Should_Update()
        {
            TenderConditionsTemplateMaterialInofmration tenderConditionsTemplateMaterialInofmration = new TenderConditionsTemplateMaterialInofmration(basicInformation, requiredDcoumentationBefore, tests, intilizationAndStartWork,
                requiredDcoumentationAfter, trainging, guarantee, maintanance, machineGuarantee, machineMaintanance);

            string newBasicInfo = "newInfo";

            tenderConditionsTemplateMaterialInofmration.Update(newBasicInfo, requiredDcoumentationBefore, tests, intilizationAndStartWork,
                requiredDcoumentationAfter, trainging, guarantee, maintanance, machineGuarantee, machineMaintanance);
            Assert.Equal(newBasicInfo, tenderConditionsTemplateMaterialInofmration.BasicInformation);
        }

        [Fact]
        public void Should_SetAddedState()
        {
            TenderConditionsTemplateMaterialInofmration tenderConditionsTemplateMaterialInofmration = new TenderConditionsTemplateMaterialInofmration(basicInformation, requiredDcoumentationBefore, tests, intilizationAndStartWork,
                requiredDcoumentationAfter, trainging, guarantee, maintanance, machineGuarantee, machineMaintanance);

            tenderConditionsTemplateMaterialInofmration.SetAddedState();
            Assert.Equal(0, tenderConditionsTemplateMaterialInofmration.TenderConditionsTemplateMaterialInofmrationId);
        }
    }
}
