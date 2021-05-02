using MOF.Etimad.Monafasat.Core.Entities;
using Shouldly;
using System.Collections.Generic;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Tenders
{
    public class TenderConditionsTemplateTest
    {

        [Fact]
        public void Should_Empty_Construct_TenderConditionsTemplate()
        {
            var tenderConditionsTemplate = new TenderConditionsTemplate();
            tenderConditionsTemplate.ShouldNotBeNull();
        }

        [Fact]
        public void Should_CreateCertificates()
        {
            var tenderConditionsTemplate = new TenderConditionsTemplate();
            tenderConditionsTemplate.CreateCertificates(new List<int>() { 10, 20, 30 });
            tenderConditionsTemplate.ShouldNotBeNull();
            tenderConditionsTemplate.TemplateCertificates.ShouldNotBeNull();
        }
    }
}
