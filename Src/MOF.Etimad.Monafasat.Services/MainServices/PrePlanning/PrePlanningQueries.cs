using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Data;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.SharedKernel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public class PrePlanningQueries : Data.GenericRepository.GenericQueryRepository, IPrePlanningQueries
    {
        private readonly IAppDbContext _context;
        public PrePlanningQueries(IAppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<PrePlanning> FindById(int planningId, int? statusId = 0)
        {
            Check.ArgumentNotNullOrEmpty(nameof(planningId), planningId.ToString());
            var entities = await _context.PrePlannings.Include(p => p.Status).Include(p => p.YearQuarter)
                .Where(t => t.PrePlanningId == planningId)
                .WhereIf(statusId != 0, p => p.StatusId == statusId)
                .FirstOrDefaultAsync();
            return entities;
        }
        public async Task<int> Deactivate(int id, bool userRole)
        {
            PrePlanning prePlanning = _context.PrePlannings.Find(id);
            if (prePlanning.StatusId == (int)Enums.PrePlanningStatus.Approved && userRole)
                prePlanning.SetDeActiveRequest(true);
            else
                prePlanning.DeActive();
            _context.PrePlannings.Update(prePlanning);
            return await _context.SaveChangesAsync();
        }
        public async Task<PrePlanning> FindByIdForEdit(int planningId, int? statusId = 0)
        {
            Check.ArgumentNotNullOrEmpty(nameof(planningId), planningId.ToString());
            var entities = await _context.PrePlannings
                .Where(t => t.PrePlanningId == planningId)
                .WhereIf(statusId != 0, p => p.StatusId == statusId)
                .FirstOrDefaultAsync();
            return entities;
        }
        public async Task<bool> GetByName(string name, int branchId, string agencyCode, int id)
        {
            return await _context.PrePlannings
                .Where(x => x.ProjectName == name && x.IsActive == true)
                .WhereIf(id != 0, x => x.PrePlanningId != id)
                .WhereIf(branchId != 0, x => x.BranchId == branchId)
                .WhereIf(agencyCode != "", x => x.AgencyCode == agencyCode)
                .FirstOrDefaultAsync() != null;
        }
        public async Task<QueryResult<PrePlanningModel>> FindPrePlanningBySearchCriteria(PrePlanningSearchCriteria criteria)
        {
            var result = await _context.PrePlannings
                .Where(x => x.IsActive == true)
                .WhereIf(criteria.StatusId != 0, x => x.StatusId == criteria.StatusId)
                .WhereIf(!string.IsNullOrEmpty(criteria.ProjectName), x => x.ProjectName.Contains(criteria.ProjectName))
                .WhereIf(criteria.YearQuarterId != 0, x => x.YearQuarterId == criteria.YearQuarterId)
                .WhereIf(!string.IsNullOrEmpty(criteria.AgencyCode), x => x.AgencyCode == criteria.AgencyCode)
                .WhereIf(criteria.ProjectTypeId != 0, x => x.PrePlanningProjectTypes.FirstOrDefault().ActivityId == criteria.ProjectTypeId)
                .SortBy(criteria.Sort, criteria.SortDirection)
                .Select(x => new PrePlanningModel
                {
                    StatusId = x.StatusId,
                    StatusName = x.Status.NameAr,
                    EncyptedPrePlanningId = Util.Encrypt(x.PrePlanningId),
                    AgencyCode = x.AgencyCode,
                    AgencyName = x.Agency.NameArabic,
                    InsideKSA = x.InsideKSA,
                    ProjectNature = x.ProjectNature,
                    IsDeleteRequested = x.IsDeleteRequested,
                    ProjectName = x.ProjectName,
                    DeleteRejectionReason = x.DeleteRejectionReason,
                    ProjectDescription = x.ProjectDescription,
                    Duration = x.Duration,
                    DurationInDays = x.DurationInDays,
                    DurationInMonths = x.DurationInMonths,
                    DurationInYears = x.DurationInYears,
                    CreatedBy = x.CreatedBy,
                    YearQuarterId = x.YearQuarterId,
                    YearQuarterName = x.YearQuarter.NameAr,
                    Year = x.CreatedAt.Year.ToString(),
                    ProjectTypesIDs = x.PrePlanningProjectTypes.Select(a => a.ActivityId).ToList(),
                    ProjectTypesList = _context.Activities.Where(r => x.PrePlanningProjectTypes.Select(s => s.ActivityId).Any(d => d == r.ActivityId)).Select(r => new SelectListItem { Value = r.ActivityId.ToString(), Text = r.NameAr }).ToList()
                }).ToQueryResult(criteria.PageNumber, criteria.PageSize);
            return result;
        }

        public async Task<PrePlanningModel> FindPrePlanningById(int id)
        {
            var result = await _context.PrePlannings
                .Where(x => x.IsActive == true && x.PrePlanningId == id)
                                .Select(x => new PrePlanningModel
                                {
                                    PrePlanningId = x.PrePlanningId,
                                    EncyptedPrePlanningId = Util.Encrypt(x.PrePlanningId),
                                    StatusId = x.StatusId,
                                    StatusName = x.Status.NameAr,
                                    AgencyCode = x.AgencyCode,
                                    AgencyName = x.Agency.NameArabic,
                                    InsideKSA = x.InsideKSA,
                                    IsDeleteRequested = x.IsDeleteRequested,
                                    ProjectNature = x.ProjectNature,
                                    ProjectName = x.ProjectName,
                                    ProjectDescription = x.ProjectDescription,
                                    Duration = x.Duration,
                                    DurationInDays = x.DurationInDays,
                                    DurationInMonths = x.DurationInMonths,
                                    DurationInYears = x.DurationInYears,
                                    PrePlanningCountriesIDs = x.PrePlanningCountries.Select(a => a.CountryId).ToList(),
                                    PrePlanningAreaIDs = x.PrePlanningAreas.Select(a => a.AreaId).ToList(),
                                    ProjectTypesIDs = x.PrePlanningProjectTypes.Select(a => a.ActivityId).ToList(),
                                    CreatedBy = x.CreatedBy,
                                    RejectionReason = x.RejectionReason,
                                    YearQuarterId = x.YearQuarterId,
                                    Year = x.CreatedAt.Year.ToString(),

                                    YearQuarterName = x.YearQuarter.NameAr
                                }).FirstOrDefaultAsync();
            return result;
        }

        public async Task<PrePlanningModel> GetPrePlanningDetailsById(int id)
        {

            var result = await _context.PrePlannings
                .Where(x => x.IsActive == true && x.PrePlanningId == id)
                                .Select(x => new PrePlanningModel
                                {
                                    PrePlanningId = x.PrePlanningId,
                                    EncyptedPrePlanningId = Util.Encrypt(x.PrePlanningId),
                                    StatusId = x.StatusId,
                                    StatusName = x.Status.NameAr,
                                    AgencyCode = x.AgencyCode,
                                    AgencyName = x.Agency.NameArabic,
                                    InsideKSA = x.InsideKSA,
                                    IsDeleteRequested = x.IsDeleteRequested,
                                    ProjectNature = x.ProjectNature,
                                    ProjectName = x.ProjectName,
                                    ProjectDescription = x.ProjectDescription,
                                    Duration = x.Duration,
                                    DurationInDays = x.DurationInDays,
                                    DurationInMonths = x.DurationInMonths,
                                    DurationInYears = x.DurationInYears,
                                    PrePlanningCountriesIDs = x.PrePlanningCountries.Select(a => a.CountryId).ToList(),
                                    PrePlanningAreaIDs = x.PrePlanningAreas.Select(a => a.AreaId).ToList(),
                                    ProjectTypesIDs = x.PrePlanningProjectTypes.Select(a => a.ActivityId).ToList(),
                                    Countries = _context.Countries.Where(r => x.PrePlanningCountries.Select(s => s.CountryId).Any(d => d == r.CountryId)).Select(r => new CountryModel { CountryId = r.CountryId, Name = r.NameAr }).ToList(),
                                    Areas = _context.Areas.Where(r => x.PrePlanningAreas.Select(s => s.AreaId).Any(d => d == r.AreaId)).Select(r => new LookupModel { Id = r.AreaId, Name = r.NameAr }).ToList(),
                                    ProjectTypesList = _context.Activities.Where(r => x.PrePlanningProjectTypes.Select(s => s.ActivityId).Any(d => d == r.ActivityId)).Select(r => new SelectListItem { Value = r.ActivityId.ToString(), Text = r.NameAr }).ToList(),
                                    CreatedBy = x.CreatedBy,
                                    RejectionReason = x.RejectionReason,
                                    YearQuarterId = x.YearQuarterId,
                                    Year = x.CreatedAt.Year.ToString(),
                                    DeleteRejectionReason = x.DeleteRejectionReason,
                                    YearQuarterName = x.YearQuarter.NameAr
                                }).FirstOrDefaultAsync();
            return result;
        }

        public async Task<PrePlanningModel> SetPrePlanningLookUps()
        {
            var result = new PrePlanningModel
            {
                PrePlanningAreaIDs = new List<int>(),
                PrePlanningCountriesIDs = new List<int>(),
                ProjectTypesIDs = new List<int>(),
                Countries = _context.Countries.Select(r => new CountryModel { CountryId = r.CountryId, Name = r.NameAr }).ToList(),
                Areas = _context.Areas.Select(r => new LookupModel { Id = r.AreaId, Name = r.NameAr }).ToList(),
            };
            return result;
        }
    }
}
