using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.TestsBuilders.TenderDefaults;
using System;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Tenders
{
    public class TenderUnitTest
    {

        [Theory]
        [InlineData("Comment", 1, 100, 1)]
        public void ShouldCreateNewTenderUnitStatusesHistory(string comment, int tenderUnitStatusId, decimal estimatedValue, int updateTypeId )
        {
            TenderUnitStatusesHistory tenderUnitStatusesHistory = new TenderUnitStatusesHistory(comment,tenderUnitStatusId,estimatedValue,updateTypeId);

            Assert.Equal(comment, tenderUnitStatusesHistory.Comment);
            Assert.Equal(tenderUnitStatusId, tenderUnitStatusesHistory.TenderUnitStatusId);
            Assert.Equal(estimatedValue, tenderUnitStatusesHistory.EstimatedValue);
            Assert.Equal(updateTypeId, tenderUnitStatusesHistory.TenderUnitUpdateTypeId);
            Assert.Equal(ObjectState.Added, tenderUnitStatusesHistory.State);
        }

        [Theory]
        [InlineData(1, 1, true, 1)]
        public void ShouldCreateNewTenderUnitAssign(int userProfileId, int tenderId, bool isCurrentlyAssigned, int unitSpecialistLevel)
        {
            TenderUnitAssign tenderUnitAssign = new TenderUnitAssign(userProfileId,tenderId,isCurrentlyAssigned,unitSpecialistLevel);

            Assert.Equal(userProfileId, tenderUnitAssign.UserProfileId);
            Assert.Equal(tenderId, tenderUnitAssign.TenderId);
            Assert.True(tenderUnitAssign.IsCurrentlyAssigned);
            Assert.Equal(unitSpecialistLevel, tenderUnitAssign.UnitSpecialistLevel);
            Assert.Equal(ObjectState.Added, tenderUnitAssign.State);
        }

        [Fact]
        public void Should_IsValidToApplyOfferLocalContentChanges_ReturnTrue()
        {
            DateTime LCDate = new DateTime(2021, 4, 4);
            var tender = new TenderDefault().GetGeneralTender();
            var result = tender.IsValidToApplyOfferLocalContentChanges(LCDate);
            Assert.True(result);
        }


    }
}
