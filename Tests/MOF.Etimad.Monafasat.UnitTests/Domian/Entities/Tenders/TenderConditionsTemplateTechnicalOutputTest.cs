using MOF.Etimad.Monafasat.Core.Entities;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Tenders
{
    public class TenderConditionsTemplateTechnicalOutputTest
    {
        private const int tenderConditionsTemplateTechnicalOutputId = 1;
        private const string outState = "outState";
        private const string outputName = "outputName";
        private const string outDescriptions = "outDescriptions";
        private const string outDeliveryTime = "outDeliveryTime";

        [Fact]
        public void Should_Construct_TenderConditionsTemplateTechnicalOutput()
        {
            TenderConditionsTemplateTechnicalOutput tenderConditionsTemplateTechnicalOutput = new TenderConditionsTemplateTechnicalOutput(tenderConditionsTemplateTechnicalOutputId, outState, outputName, outDescriptions, outDeliveryTime);
            _ = new TenderConditionsTemplateTechnicalOutput(outState, outputName, outDescriptions, outDeliveryTime);
            _ = new TenderConditionsTemplateTechnicalOutput();
            _ = tenderConditionsTemplateTechnicalOutput.ConditionsTemplateId;
            _ = tenderConditionsTemplateTechnicalOutput.ConditionsTemplate;

            tenderConditionsTemplateTechnicalOutput.ShouldNotBeNull();
        }

        [Fact]
        public void Should_Update()
        {
            TenderConditionsTemplateTechnicalOutput tenderConditionsTemplateTechnicalOutput = new TenderConditionsTemplateTechnicalOutput(tenderConditionsTemplateTechnicalOutputId, outState, outputName, outDescriptions, outDeliveryTime);

            string newOutState = "newOutState";
            tenderConditionsTemplateTechnicalOutput.Update(newOutState, outputName, outDescriptions, outDeliveryTime);
            Assert.Equal(newOutState, tenderConditionsTemplateTechnicalOutput.OutputStage);
        }

        [Fact]
        public void Should_Deactive()
        {
            TenderConditionsTemplateTechnicalOutput tenderConditionsTemplateTechnicalOutput = new TenderConditionsTemplateTechnicalOutput(tenderConditionsTemplateTechnicalOutputId, outState, outputName, outDescriptions, outDeliveryTime);
            tenderConditionsTemplateTechnicalOutput.DeActive();
            Assert.False(tenderConditionsTemplateTechnicalOutput.IsActive);
        }

        [Fact]
        public void Should_SetAddedState()
        {
            TenderConditionsTemplateTechnicalOutput tenderConditionsTemplateTechnicalOutput = new TenderConditionsTemplateTechnicalOutput(tenderConditionsTemplateTechnicalOutputId, outState, outputName, outDescriptions, outDeliveryTime);

            tenderConditionsTemplateTechnicalOutput.SetAddedState();
            Assert.Equal(0, tenderConditionsTemplateTechnicalOutput.TenderConditionsTemplateTechnicalOutputId);
        }
    }
}
