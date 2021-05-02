using MOF.Etimad.Monafasat.Core.Entities;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Tenders
{
    public class TenderConditionsTemplateTechnicalDeclrationTest
    {
        private const int tenderConditionsTemplateTechnicalDeclrationId = 1;
        private const string term = "term";
        private const string decleration = "decleration";

        [Fact]
        public void Should_Construct_TenderConditionsTemplateTechnicalDeclration()
        {
            TenderConditionsTemplateTechnicalDeclration tenderConditionsTemplateTechnicalDeclration = new TenderConditionsTemplateTechnicalDeclration(tenderConditionsTemplateTechnicalDeclrationId, term, decleration);
            _ = new TenderConditionsTemplateTechnicalDeclration(term, decleration);
            _ = new TenderConditionsTemplateTechnicalDeclration();
            _ = tenderConditionsTemplateTechnicalDeclration.ConditionsTemplateId;
            _ = tenderConditionsTemplateTechnicalDeclration.ConditionsTemplate;

            tenderConditionsTemplateTechnicalDeclration.ShouldNotBeNull();
        }

        [Fact]
        public void Should_Update()
        {
            TenderConditionsTemplateTechnicalDeclration tenderConditionsTemplateTechnicalDeclration = new TenderConditionsTemplateTechnicalDeclration(tenderConditionsTemplateTechnicalDeclrationId, term, decleration);
            string newTerm = "newTerm";
            tenderConditionsTemplateTechnicalDeclration.Update(newTerm, decleration);
            Assert.Equal(newTerm, tenderConditionsTemplateTechnicalDeclration.Term);
        }

        [Fact]
        public void Should_Deactive()
        {
            TenderConditionsTemplateTechnicalDeclration tenderConditionsTemplateTechnicalDeclration = new TenderConditionsTemplateTechnicalDeclration(tenderConditionsTemplateTechnicalDeclrationId, term, decleration);
            tenderConditionsTemplateTechnicalDeclration.DeActive();
            Assert.False(tenderConditionsTemplateTechnicalDeclration.IsActive);
        }

        [Fact]
        public void Should_SetAddedState()
        {
            TenderConditionsTemplateTechnicalDeclration tenderConditionsTemplateTechnicalDeclration = new TenderConditionsTemplateTechnicalDeclration(tenderConditionsTemplateTechnicalDeclrationId, term, decleration);
            tenderConditionsTemplateTechnicalDeclration.SetAddedState();
            Assert.Equal(0, tenderConditionsTemplateTechnicalDeclration.TenderConditionsTemplateTechnicalDeclrationId);
        }

    }
}
