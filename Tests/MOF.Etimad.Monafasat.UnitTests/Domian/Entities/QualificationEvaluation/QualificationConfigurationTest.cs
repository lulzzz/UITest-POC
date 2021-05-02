using MOF.Etimad.Monafasat.Core.Entities;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.QualificationEvaluation
{
    public class QualificationConfigurationTest
    {
        private const int id = 1;
        private const int prequalificationID = 1;
        private const int qualificationItemId = 1;
        private const decimal max = 20;
        private const decimal min = 10;
        private const decimal weight = 50;


        [Fact]
        public void Should_Construct_QualificationCategoryResult()
        {
            QualificationConfiguration qualificationConfiguration = new QualificationConfiguration(id, prequalificationID, qualificationItemId, min, max, weight);
            QualificationConfiguration _qualificationConfiguration = new QualificationConfiguration();

            _ = qualificationConfiguration.Value;
            _ = qualificationConfiguration.QualificationItem;
            _ = qualificationConfiguration.Tender;
            _ = _qualificationConfiguration.QualificationSupplierData;

            qualificationConfiguration.ShouldNotBeNull();
            _qualificationConfiguration.ShouldNotBeNull();

        }

    }
}
