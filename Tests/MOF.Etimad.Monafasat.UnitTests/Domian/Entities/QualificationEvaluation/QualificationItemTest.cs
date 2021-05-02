using MOF.Etimad.Monafasat.Core.Entities;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.QualificationEvaluation
{
    public class QualificationItemTest
    {
        [Fact]
        public void Should_Construct_QualificationItem()
        {
            QualificationItem qualificationFinalResult = new QualificationItem();
            _ = qualificationFinalResult.ID;
            _ = qualificationFinalResult.SubCategoryId;
            _ = qualificationFinalResult.QualificationItemTypeId;
            _ = qualificationFinalResult.Name;
            _ = qualificationFinalResult.NameEn;
            _ = qualificationFinalResult.IsDeleted;
            _ = qualificationFinalResult.Code;
            _ = qualificationFinalResult.IsConfigure;
            _ = qualificationFinalResult.QualificationItemType;
            _ = qualificationFinalResult.QualificationSubCategory;

            qualificationFinalResult.ShouldNotBeNull();

        }
    }
}
