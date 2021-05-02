using MOF.Etimad.Monafasat.Core.Entities;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.QualificationEvaluation
{
    public class PointTest
    {
        [Fact]
        public void TestPointFileds()
        {
            Point point = new Point();
            point.ID = 1;
            point.Name = "point 1";
            point.PointValue = 60;

            point.ShouldNotBeNull();
        }
    }
}
