using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.TestsBuilders.PrePlanningDefaults;
using Shouldly;
using System;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.PrePlanning
{
    public class PrePlanningTest
    {
        private readonly PrePlanningDefaults prePlanningDefaults = new PrePlanningDefaults();

        [Fact]
        public void ConstructPrePlanning()
        {
            Core.Entities.PrePlanning prePlanning = prePlanningDefaults.ReturnPrePlanningDefaults();

            prePlanning.ShouldNotBeNull();
            prePlanning.AgencyCode.ShouldBe(prePlanningDefaults._agencyCode);
            prePlanning.ProjectName.ShouldBe(prePlanningDefaults._projectName);
            prePlanning.ProjectNature.ShouldBe(prePlanningDefaults._projectNature);
            prePlanning.InsideKSA.ShouldBe(prePlanningDefaults._insideKSA);
            prePlanning.StatusId.ShouldBe(prePlanningDefaults._statusId);
            prePlanning.YearQuarterId.ShouldBe(prePlanningDefaults._yearQuarterId);
            prePlanning.BranchId.ShouldBe(prePlanningDefaults._branchId);
            prePlanning.Duration.ShouldBe(prePlanningDefaults._duration);
            prePlanning.ProjectDescription.ShouldBe(prePlanningDefaults._projectDescription);
            prePlanning.DurationInDays.ShouldBe(prePlanningDefaults._durationInDays);
            prePlanning.DurationInMonths.ShouldBe(prePlanningDefaults._durationInMonths);
            prePlanning.DurationInYears.ShouldBe(prePlanningDefaults._durationInYears);
            prePlanning.CreatedAt.Date.ShouldBe(DateTime.Now.Date);
            prePlanning.IsActive.ShouldBe(true);
            prePlanning.State.ShouldBe(ObjectState.Added);
        }

        [Fact]
        public void ConstructPrePlanning_Update()
        {
            Core.Entities.PrePlanning prePlanning = prePlanningDefaults.ReturnPrePlanningDefaults_Update();

            prePlanning.ShouldNotBeNull();
            prePlanning.AgencyCode.ShouldBe(prePlanningDefaults._agencyCode);
            prePlanning.ProjectName.ShouldBe(prePlanningDefaults._projectName);
            prePlanning.ProjectNature.ShouldBe(prePlanningDefaults._projectNature);
            prePlanning.InsideKSA.ShouldBe(prePlanningDefaults._insideKSA);
            prePlanning.StatusId.ShouldBe(prePlanningDefaults._statusId);
            prePlanning.YearQuarterId.ShouldBe(prePlanningDefaults._yearQuarterId);
            prePlanning.BranchId.ShouldBe(prePlanningDefaults._branchId);
            prePlanning.Duration.ShouldBe(prePlanningDefaults._duration);
            prePlanning.ProjectDescription.ShouldBe(prePlanningDefaults._projectDescription);
            prePlanning.DurationInDays.ShouldBe(prePlanningDefaults._durationInDays);
            prePlanning.DurationInMonths.ShouldBe(prePlanningDefaults._durationInMonths);
            prePlanning.DurationInYears.ShouldBe(prePlanningDefaults._durationInYears);
            prePlanning.State.ShouldBe(ObjectState.Modified);
        }

        [Fact]
        public void Should_UpdateStatusRejectionReason()
        {
            Core.Entities.PrePlanning prePlanning = prePlanningDefaults.ReturnPrePlanningDefaults();
            prePlanning.UpdateStatus(Enums.PrePlanningStatus.Rejected, prePlanningDefaults._rejectionReason);
            prePlanning.ShouldNotBeNull();
            prePlanning.StatusId.ShouldBe(prePlanningDefaults._rejectStatusId);
        }

        [Fact]
        public void Should_DeActive()
        {
            Core.Entities.PrePlanning prePlanning = prePlanningDefaults.ReturnPrePlanningDefaults();
            prePlanning.DeActive();
            prePlanning.ShouldNotBeNull();
            prePlanning.IsActive.ShouldNotBeNull();
            prePlanning.IsActive.Value.ShouldBeFalse();
        }

        [Fact]
        public void Should_SetDeleteRejectionReason()
        {
            string request = "";
            Core.Entities.PrePlanning prePlanning = prePlanningDefaults.ReturnPrePlanningDefaults();
            prePlanning.SetDeleteRejectionReason(request);
            prePlanning.ShouldNotBeNull();
        }

        [Fact]
        public void Should_SetDeActiveRequest()
        {
            bool request = true;
            Core.Entities.PrePlanning prePlanning = prePlanningDefaults.ReturnPrePlanningDefaults();
            prePlanning.SetDeActiveRequest(request);
            prePlanning.ShouldNotBeNull();
        }


        [Fact]
        public void Should_UpdatePrePlanning()
        {
            Core.Entities.PrePlanning prePlanning = prePlanningDefaults.ReturnPrePlanningDefaults();
            prePlanning.Update(prePlanningDefaults._agencyCode, prePlanningDefaults._projectName, prePlanningDefaults._projectNature, prePlanningDefaults._projectDescription,
            prePlanningDefaults._insideKSA, prePlanningDefaults._statusId, prePlanningDefaults._duration, prePlanningDefaults._durationInDays, prePlanningDefaults._durationInMonths, prePlanningDefaults._durationInYears, prePlanningDefaults._yearQuarterId);

            prePlanning.ShouldNotBeNull();
            prePlanning.AgencyCode.ShouldBe(prePlanningDefaults._agencyCode);
            prePlanning.ProjectName.ShouldBe(prePlanningDefaults._projectName);
            prePlanning.ProjectNature.ShouldBe(prePlanningDefaults._projectNature);
            prePlanning.InsideKSA.ShouldBe(prePlanningDefaults._insideKSA);
            prePlanning.StatusId.ShouldBe(prePlanningDefaults._statusId);
            prePlanning.YearQuarterId.ShouldBe(prePlanningDefaults._yearQuarterId);
            prePlanning.BranchId.ShouldBe(prePlanningDefaults._branchId);
            prePlanning.Duration.ShouldBe(prePlanningDefaults._duration);
            prePlanning.ProjectDescription.ShouldBe(prePlanningDefaults._projectDescription);
            prePlanning.DurationInDays.ShouldBe(prePlanningDefaults._durationInDays);
            prePlanning.DurationInMonths.ShouldBe(prePlanningDefaults._durationInMonths);
            prePlanning.DurationInYears.ShouldBe(prePlanningDefaults._durationInYears);
            prePlanning.CreatedAt.Date.ShouldBe(DateTime.Now.Date);
            prePlanning.IsActive.ShouldBe(true);
        }

        [Fact]
        public void Should_AddtAreasCountriesTypes()
        {
            Core.Entities.PrePlanning prePlanning = prePlanningDefaults.ReturnPrePlanningDefaults();
            prePlanning.AddtAreasCountriesTypes(prePlanningDefaults._areaIDs, prePlanningDefaults._insideKSA, prePlanningDefaults._countriesIds, prePlanningDefaults._projectTypeIds);
            prePlanning.ShouldNotBeNull();
        }

        [Fact]
        public void Should_UpdatetAreasCountriesTypes()
        {
            Core.Entities.PrePlanning prePlanning = prePlanningDefaults.ReturnPrePlanningDefaults();
            prePlanning.UpdatetAreasCountriesTypes(prePlanningDefaults._areaIDs, prePlanningDefaults._insideKSA, prePlanningDefaults._countriesIds, prePlanningDefaults._projectTypeIds);
            prePlanning.ShouldNotBeNull();
        }



        [Fact]
        public void Should_IsDurationValid_Sucess()
        {
            Core.Entities.PrePlanning prePlanning = prePlanningDefaults.ReturnPrePlanningDefaults();
            prePlanning.ShouldNotBeNull();
            prePlanning.IsDurationValid();
            prePlanning.ShouldNotBeNull();
        }


        [Fact]
        public void Should_UpdatetAreas()
        {
            Core.Entities.PrePlanning prePlanning = prePlanningDefaults.ReturnPrePlanningDefaults();
            prePlanning.UpdateAreas(prePlanningDefaults._areaIDs);
            prePlanning.ShouldNotBeNull();
        }

        [Fact]
        public void Should_UpdatetCountries()
        {
            Core.Entities.PrePlanning prePlanning = prePlanningDefaults.ReturnPrePlanningDefaults();
            prePlanning.UpdateCountries(prePlanningDefaults._countriesIds);
            prePlanning.ShouldNotBeNull();
        }


        [Fact]
        public void Should_UpdatetProjectTypes()
        {
            Core.Entities.PrePlanning prePlanning = prePlanningDefaults.ReturnPrePlanningDefaults();
            prePlanning.UpdateProjectTypes(prePlanningDefaults._projectTypeIds);
            prePlanning.ShouldNotBeNull();
        }

        [Fact]
        public void Should_RemoveCancelationRejectReason()
        {
            Core.Entities.PrePlanning prePlanning = prePlanningDefaults.ReturnPrePlanningDefaults();
            prePlanning.RemoveCancelationRejectReason();
            Assert.Null(prePlanning.DeleteRejectionReason);
        }



    }
}
