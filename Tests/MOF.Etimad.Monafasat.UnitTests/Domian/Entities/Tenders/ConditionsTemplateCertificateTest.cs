using MOF.Etimad.Monafasat.Core.Entities;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Tenders
{
    public class ConditionsTemplateCertificateTest
    {
        private readonly int _cerificateId = 10;

        [Fact]
        public void Should_Empty_Construct_ConditionsTemplateCertificate()
        {
            var conditionsTemplateCertificate = new ConditionsTemplateCertificate();
            conditionsTemplateCertificate.ShouldNotBeNull();
        }

        [Fact]
        public void Should_Construct_CerificateId()
        {
            var conditionsTemplateCertificate = new ConditionsTemplateCertificate(_cerificateId);
            conditionsTemplateCertificate.ShouldNotBeNull();
            conditionsTemplateCertificate.CerificateId.ShouldBeGreaterThanOrEqualTo(10);
            conditionsTemplateCertificate.State.ShouldBe(SharedKernal.ObjectState.Added);
        }

        [Fact]
        public void Should_Delete()
        {
            var conditionsTemplateCertificate = new ConditionsTemplateCertificate();
            conditionsTemplateCertificate.Delete();
            conditionsTemplateCertificate.State.ShouldBe(SharedKernal.ObjectState.Deleted);
        }

        [Fact]
        public void Should_SetAddedState()
        {
            var conditionsTemplateCertificate = new ConditionsTemplateCertificate();
            conditionsTemplateCertificate.SetAddedState();
            conditionsTemplateCertificate.Id.ShouldBeGreaterThanOrEqualTo(0);
            conditionsTemplateCertificate.State.ShouldBe(SharedKernal.ObjectState.Added);
        }

        [Fact]
        public void Should_SetActive()
        {
            var conditionsTemplateCertificate = new ConditionsTemplateCertificate();
            conditionsTemplateCertificate.SetActive();
            conditionsTemplateCertificate.IsActive.ShouldBe(true);
            conditionsTemplateCertificate.State.ShouldBe(SharedKernal.ObjectState.Modified);
        }
    }
}
