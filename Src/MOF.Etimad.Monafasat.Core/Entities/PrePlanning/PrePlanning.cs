using MOF.Etimad.Monafasat.SharedKernal.Exceptions;
using MOF.Etimad.Monafasat.SharedKernel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("PrePlanning", Schema = "PrePlanning")]
    public class PrePlanning : AuditableEntity
    {

        #region Constructors

        public PrePlanning() { }

        public PrePlanning(int prePlanningId, string agencyCode, int branchId, string projectName, string projectNature, int projectTypeId, string projectDescription, bool? insideKSA, int statusId, string duration, int? durationInDays, int? durationInMonths, int? durationInYears, int yearQuarterId)
        {
            PrePlanningId = prePlanningId;
            AgencyCode = agencyCode;
            BranchId = branchId;
            ProjectName = projectName;
            ProjectNature = projectNature;
            ProjectDescription = projectDescription;
            InsideKSA = insideKSA;
            StatusId = statusId;
            YearQuarterId = yearQuarterId;
            Duration = duration;
            DurationInDays = durationInDays;
            DurationInMonths = durationInMonths;
            DurationInYears = durationInYears;
            if (prePlanningId == 0)
                EntityCreated();
            else
                EntityUpdated();
        }

        public PrePlanning(int prePlanningId, string agencyCode, int branchId, string projectName, string projectNature, int projectTypeId, string projectDescription, bool? insideKSA)
        {
            PrePlanningId = prePlanningId;
            AgencyCode = agencyCode;
            BranchId = branchId;
            ProjectName = projectName;
            ProjectNature = projectNature;
            ProjectDescription = projectDescription;
            InsideKSA = insideKSA;
            EntityCreated();
        }

        #endregion

        #region Fields 
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PrePlanningId { get; private set; }
        [StringLength(20)]
        public string AgencyCode { get; private set; }
        public int BranchId { get; private set; }
        [ForeignKey(nameof(AgencyCode))]
        public GovAgency Agency { get; private set; }
        public bool? InsideKSA { get; private set; }
        public int? StatusId { get; private set; }
        [ForeignKey(nameof(StatusId))]
        public PrePlanningStatus Status { get; private set; }
        [StringLength(200)]
        public string ProjectName { get; private set; }
        [StringLength(500)]
        public string ProjectNature { get; private set; }
        [StringLength(500)]
        public string ProjectDescription { get; private set; }
        [StringLength(500)]
        public string RejectionReason { get; private set; }
        [StringLength(100)]
        public string Duration { get; private set; }
        public int? DurationInDays { get; set; }
        public int? DurationInMonths { get; set; }
        public int? DurationInYears { get; set; }

        [StringLength(500)]
        public string DeleteRejectionReason { get; private set; }
        public bool IsDeleteRequested { get; private set; }
        public int YearQuarterId { get; set; }
        [ForeignKey(nameof(YearQuarterId))]
        public YearQuarter YearQuarter { get; private set; }
        public List<PrePlanningArea> PrePlanningAreas { private set; get; } = new List<PrePlanningArea>();
        public List<PrePlanningProjectType> PrePlanningProjectTypes { private set; get; } = new List<PrePlanningProjectType>();
        public List<PrePlanningCountry> PrePlanningCountries { private set; get; } = new List<PrePlanningCountry>();

        #endregion



        #region Methods

        public void Update(string agencyCode, string projectName, string projectNature, string projectDescription, bool? insideKSA, int underUpdate, string duration, int? durationInDays, int? durationInMonths, int? durationInYears, int yearQuarterId)
        {
            AgencyCode = agencyCode;
            ProjectName = projectName;
            ProjectNature = projectNature;

            ProjectDescription = projectDescription;
            InsideKSA = insideKSA;

            Status = null;
            StatusId = (int)Enums.PrePlanningStatus.UnderUpdate;
            Duration = duration;
            DurationInDays = durationInDays;
            DurationInMonths = durationInMonths;
            DurationInYears = durationInYears;
            YearQuarterId = yearQuarterId;
            EntityUpdated();
        }

        public void UpdateStatus(Enums.PrePlanningStatus statusId, string rejectionReason)
        {
            StatusId = (int)statusId;
            Status = null;
            if (!string.IsNullOrEmpty(rejectionReason))
                RejectionReason = rejectionReason;
            EntityUpdated();
        }


        public void RemoveCancelationRejectReason()
        {
            DeleteRejectionReason = null;
            IsDeleteRequested = false;
            EntityUpdated();
        }


        public void DeActive()
        {
            IsActive = false;
            EntityUpdated();
        }
        public void SetDeActiveRequest(bool request)
        {
            IsDeleteRequested = request;
            EntityUpdated();
        }
        public void SetDeleteRejectionReason(string request)
        {
            DeleteRejectionReason = request;
            EntityUpdated();
        }
        private PrePlanning setAreasCountriesTypes(IList<int> AreaIds, bool? insideKSA, IList<int> CountriesIds, IList<int> ProjectTypeIds)
        {
            InsideKSA = insideKSA;
            UpdateProjectTypes(ProjectTypeIds);
            UpdateAreas(AreaIds);
            UpdateCountries(CountriesIds);
            return this;
        }

        public void IsDurationValid()
        {
            var sumOfDuration = DurationInDays.GetValueOrDefault() + DurationInMonths.GetValueOrDefault() + DurationInYears.GetValueOrDefault();
            if (sumOfDuration <= 0)
                throw new BusinessRuleException(Resources.PrePlanningResources.ErrorMessages.TotalOfDurationCannotBeZero);
        }

        public PrePlanning AddtAreasCountriesTypes(IList<int> AreaIds, bool? insideKSA, IList<int> CountriesIds, IList<int> ProjectTypeIds)
        {
            setAreasCountriesTypes(AreaIds, insideKSA, CountriesIds, ProjectTypeIds);
            EntityCreated();
            return this;
        }
        public PrePlanning UpdatetAreasCountriesTypes(IList<int> AreaIds, bool? insideKSA, IList<int> CountriesIds, IList<int> ProjectTypeIds)
        {
            setAreasCountriesTypes(AreaIds, insideKSA, CountriesIds, ProjectTypeIds);
            EntityUpdated();
            return this;
        }
        #endregion
        public void UpdateAreas(IList<int> AreaIds)
        {
            if (AreaIds != null)
            {
                var mutualAreas = PrePlanningAreas.Where(x => AreaIds.Contains(x.AreaId)).ToList();
                var mutualIds = mutualAreas.Select(a => a.AreaId).ToList<int>();
                var AreasToBeDeleted = PrePlanningAreas.Where(x => !AreaIds.Contains(x.AreaId)).ToList();
                List<int> AreasAddedIDs = AreaIds.Where(x => !mutualIds.Contains(x)).ToList<int>();
                if (PrePlanningAreas != null)
                {
                    foreach (PrePlanningArea item in AreasToBeDeleted)
                    {
                        item.Delete();
                    }
                }
                if (AreaIds != null)
                {
                    foreach (var item in AreasAddedIDs)
                    {
                        PrePlanningAreas.Add(new PrePlanningArea(item));
                    }
                }
            }
        }
        public void UpdateProjectTypes(IList<int> ProjectTypeIds)
        {
            if (ProjectTypeIds != null)
            {
                //will not cahnge
                var mutualAreas = PrePlanningProjectTypes.Where(x => ProjectTypeIds.Contains(x.ActivityId)).ToList();
                var mutualIds = mutualAreas.Select(a => a.ActivityId).ToList<int>();
                //Will be deleted
                var AreasToBeDeleted = PrePlanningProjectTypes.Where(x => !ProjectTypeIds.Contains(x.ActivityId)).ToList();
                //Will be added
                List<int> AreasAddedIDs = ProjectTypeIds.Where(x => !mutualIds.Contains(x)).ToList<int>();
                if (PrePlanningProjectTypes != null)
                {
                    foreach (PrePlanningProjectType item in AreasToBeDeleted)
                    {
                        item.Delete();
                    }
                }
                if (ProjectTypeIds != null)
                {
                    foreach (var item in AreasAddedIDs)
                    {
                        PrePlanningProjectTypes.Add(new PrePlanningProjectType(item));
                    }
                }
            }
        }


        public void UpdateCountries(IList<int> countriesIds)
        {
            if (countriesIds != null)
            {
                //will not cahnge
                var mutualCountries = PrePlanningCountries.Where(x => countriesIds.Contains(x.CountryId)).ToList();
                var mutualIds = mutualCountries.Select(a => a.CountryId).ToList<int>();
                //Will be deleted
                var CountriesToBeDeleted = PrePlanningCountries.Where(x => !countriesIds.Contains(x.CountryId)).ToList();
                //Will be added
                List<int> CountriesAddedIDs = countriesIds.Where(x => !mutualIds.Contains(x)).ToList<int>();
                if (PrePlanningCountries != null)
                {
                    foreach (PrePlanningCountry item in CountriesToBeDeleted)
                    {
                        item.Delete();
                    }
                }
                if (countriesIds != null)
                {
                    foreach (var item in CountriesAddedIDs)
                    {
                        PrePlanningCountries.Add(new PrePlanningCountry(item));
                    }
                }
            }
        }



    }
}
