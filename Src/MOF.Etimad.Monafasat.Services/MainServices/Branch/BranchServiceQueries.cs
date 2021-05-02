using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.Core;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Data;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace MOF.Etimad.Monafasat.Services
{
    public class BranchServiceQueries : IBranchServiceQueries
    {
        private readonly IAppDbContext _context;
        private readonly RootConfigurations _rootConfiguration;
        public BranchServiceQueries(IAppDbContext context, IOptionsSnapshot<RootConfigurations> rootConfiguration)
        {
            _context = context;
            _rootConfiguration = rootConfiguration.Value;

        }

        public async Task<QueryResult<BranchModel>> Find(BranchSearchCriteriaModel criteria)
        {
            var result = await _context.Branch.Include(t => t.Agency)
                .Where(x => x.IsActive == true && x.AgencyCode == criteria.AgencyCode)
                .WhereIf(!string.IsNullOrEmpty(criteria.BranchName), s => s.BranchName.Contains(criteria.BranchName))
                .OrderBy(x => x.BranchId)
                .SortBy(criteria.Sort, criteria.SortDirection)
               .Select(x => new BranchModel
               {
                   AddressesList = x.BranchAddresses.Where(s => s.AddressTypeId != (int)Enums.BranchAddressType.MainBranchAddress).Select(r => new OtherBranchAddressModel
                   {
                       BranchAddressId = r.BranchAddressId,
                       AddressName = r.AddressName,
                       Address = r.Address,
                       CityCode = r.CityCode,
                       Description = r.Description,
                       Email = r.Email,
                       Fax = r.Fax,
                       Fax2 = r.Fax2,
                       Phone = r.Phone,
                       Phone2 = r.Phone2,
                       Position = r.Position,
                       PostalCode = r.PostalCode,
                       ZipCode = r.ZipCode
                   }).ToList(),
                   MainBranchAddress = x.BranchAddresses.Where(s => s.AddressTypeId == (int)Enums.BranchAddressType.MainBranchAddress && s.IsActive == true).Select(m => new MainBranchAddressModel
                   {
                       BranchAddressId = m.BranchAddressId,
                       AddressName = m.AddressName,
                       Address = m.Address,
                       CityCode = m.CityCode,
                       Description = m.Description,
                       Email = m.Email,
                       Fax = m.Fax,
                       Fax2 = m.Fax2,
                       Phone = m.Phone,
                       Phone2 = m.Phone2,
                       Position = m.Position,
                       PostalCode = m.PostalCode,
                       ZipCode = m.ZipCode
                   }).FirstOrDefault() ?? new MainBranchAddressModel(),
                   techcommitteeIds = x.BranchCommittees.Where(d => d.Committee.CommitteeTypeId == (int)Enums.CommitteeType.TechincalCommittee && d.IsActive == true).Select(d => d.CommitteeId).ToList<int>(),
                   checkcommitteeIds = x.BranchCommittees.Where(d => d.Committee.CommitteeTypeId == (int)Enums.CommitteeType.CheckOfferCommittee && d.IsActive == true).Select(d => d.CommitteeId).ToList<int>(),
                   opencommitteeIds = x.BranchCommittees.Where(d => d.Committee.CommitteeTypeId == (int)Enums.CommitteeType.OpenOfferCommittee && d.IsActive == true).Select(d => d.CommitteeId).ToList<int>(),
                   purchaseCommitteeIds = x.BranchCommittees.Where(d => d.Committee.CommitteeTypeId == (int)Enums.CommitteeType.PurchaseCommittee && d.IsActive == true).Select(d => d.CommitteeId).ToList<int>(),
                   preQualificationCommitteeIds = x.BranchCommittees.Where(d => d.Committee.CommitteeTypeId == (int)Enums.CommitteeType.PreQualificationCommittee && d.IsActive == true).Select(d => d.CommitteeId).ToList<int>(),
                   vrocommitteeIds = x.BranchCommittees.Where(d => d.Committee.CommitteeTypeId == (int)Enums.CommitteeType.VROCommittee && d.IsActive == true).Select(d => d.CommitteeId).ToList<int>(),
                   BranchId = x.BranchId,
                   BranchIdString = Util.Encrypt(x.BranchId),
                   BranchName = x.BranchName,
                   AgencyName = x.Agency.NameArabic
               }).ToQueryResult(criteria.PageNumber, criteria.PageSize);

            return result;
        }
        public async Task<BranchModel> FindById(int id)
        {
            var result = await _context.Branch.Include("BranchAddresses.AddressType").Select(x => new BranchModel
            {
                AddressesList = x.BranchAddresses.Where(s => s.AddressTypeId != (int)Enums.BranchAddressType.MainBranchAddress && s.IsActive.HasValue && s.IsActive.Value).Select(r => new OtherBranchAddressModel
                {
                    BranchAddressTypeId = r.AddressTypeId,
                    BranchAddressId = r.BranchAddressId,
                    AddressName = r.AddressName,
                    Address = r.Address,
                    CityCode = r.CityCode,
                    Description = r.Description,
                    Email = r.Email,
                    Fax = r.Fax,
                    Fax2 = r.Fax2,
                    Phone = r.Phone,
                    Phone2 = r.Phone2,
                    Position = r.Position,
                    BranchAddressTypeName = r.AddressType.AddressTypeName,
                    PostalCode = r.PostalCode,
                    ZipCode = r.ZipCode
                }).ToList(),

                MainBranchAddress = x.BranchAddresses.Where(s => s.AddressTypeId == (int)Enums.BranchAddressType.MainBranchAddress && s.IsActive.HasValue && s.IsActive.Value).Select(m => new MainBranchAddressModel
                {
                    BranchAddressId = m.BranchAddressId,
                    AddressName = m.AddressName,
                    Address = m.Address,
                    CityCode = m.CityCode,
                    Description = m.Description,
                    Email = m.Email,
                    Fax = m.Fax,
                    Fax2 = m.Fax2,
                    Phone = m.Phone,
                    Phone2 = m.Phone2,
                    Position = m.Position,
                    PostalCode = m.PostalCode,
                    ZipCode = m.ZipCode
                }).FirstOrDefault(),
                techcommitteeIds = x.BranchCommittees.Where(d => d.IsActive.HasValue && d.IsActive.Value && d.Committee.IsActive.HasValue && d.Committee.IsActive.Value && d.Committee.CommitteeTypeId == (int)Enums.CommitteeType.TechincalCommittee).Select(d => d.CommitteeId).ToList<int>(),
                checkcommitteeIds = x.BranchCommittees.Where(d => d.IsActive.HasValue && d.IsActive.Value && d.Committee.IsActive.HasValue && d.Committee.IsActive.Value && d.Committee.CommitteeTypeId == (int)Enums.CommitteeType.CheckOfferCommittee).Select(d => d.CommitteeId).ToList<int>(),
                opencommitteeIds = x.BranchCommittees.Where(d => d.IsActive.HasValue && d.IsActive.Value && d.Committee.IsActive.HasValue && d.Committee.IsActive.Value && d.Committee.CommitteeTypeId == (int)Enums.CommitteeType.OpenOfferCommittee).Select(d => d.CommitteeId).ToList<int>(),
                purchaseCommitteeIds = x.BranchCommittees.Where(d => d.IsActive.HasValue && d.IsActive.Value && d.Committee.IsActive.HasValue && d.Committee.IsActive.Value && d.Committee.CommitteeTypeId == (int)Enums.CommitteeType.PurchaseCommittee).Select(d => d.CommitteeId).ToList<int>(),
                preQualificationCommitteeIds = x.BranchCommittees.Where(d => d.IsActive.HasValue && d.IsActive.Value && d.Committee.IsActive.HasValue && d.Committee.IsActive.Value && d.Committee.CommitteeTypeId == (int)Enums.CommitteeType.PreQualificationCommittee).Select(d => d.CommitteeId).ToList<int>(),
                vrocommitteeIds = x.BranchCommittees.Where(d => d.IsActive.HasValue && d.IsActive.Value && d.Committee.IsActive.HasValue && d.Committee.IsActive.Value && d.Committee.CommitteeTypeId == (int)Enums.CommitteeType.VROCommittee).Select(d => d.CommitteeId).ToList<int>(),
                BranchId = x.BranchId,
                BranchIdString = Util.Encrypt(x.BranchId),
                BranchName = x.BranchName
            }).FirstOrDefaultAsync(a => a.BranchId == id);
            return result;
        }
        public async Task<CreateTenderBasicDataModel> FindInsertionTypeAndPurchaseMenthodsById(int id)
        {
            string unitAgencyCode = _rootConfiguration.UnitAgencyCodeConfiguration.UnitAgencyCode;

            var result = await _context.Branch.Where(x => x.BranchId == id)
               .Select(x => new CreateTenderBasicDataModel
               {
                   IsVRO = x.Agency.IsVRO,
                   PurchaseMethodString = x.Agency.PurchaseMethods,
                   IsUnitAgency = x.AgencyCode == unitAgencyCode ? true : false
               }).FirstOrDefaultAsync();
            return result;
        }
        public async Task<QueryResult<BranchUserModel>> GetBranchUsers(BranchUserSearchCriteriaModel criteria)
        {
            var result = await _context.BranchUsers
                 .WhereIf(criteria.RoleName != null, x => x.UserRole.Name == criteria.RoleName)
                 .WhereIf(criteria.UserProfileId != 0, x => x.UserProfileId == criteria.UserProfileId)
                 .Where(c => c.BranchId == criteria.BranchId && c.IsActive == true)
                 .Select(x => new BranchUserModel
                 {
                     RoleId = x.UserRoleId,
                     UserId = x.UserProfileId,
                     FullName = x.UserProfile.FullName,
                     UserName = x.UserProfile.UserName,
                     RoleName = x.UserRole.DisplayNameAr,
                     EstimatedValueFrom = x.EstimatedValueFrom,
                     EstimatedValueTo = x.EstimatedValueTo,
                     Email = x.UserProfile.Email
                 }).ToQueryResult(criteria.PageNumber, criteria.PageSize);
            return result;
        }
        public async Task<List<BranchUserModel>> GetBranchUsersHasEstimateValue(int userId, int branchId)
        {
            var result = await _context.BranchUsers
                   .Where(x => x.IsActive == true && x.UserProfileId == userId)
                   .Where(x => x.EstimatedValueTo > 0)
                 .Select(x => new BranchUserModel
                 {
                     RoleId = x.UserRoleId,
                     UserId = x.UserProfileId,
                     EstimatedValueFrom = x.EstimatedValueFrom,
                     EstimatedValueTo = x.EstimatedValueTo
                 }).ToListAsync();
            return result;
        }
        public async Task<BranchUser> GetBranchUsersForAwardingApproval(int userId, int branchId)
        {

            var branchUser = await _context.BranchUsers
                   .Where(u => u.IsActive == true && u.EstimatedValueFrom.HasValue && u.EstimatedValueTo > 0 && u.BranchId == branchId && u.UserProfileId == userId)
                   .FirstOrDefaultAsync();

            return branchUser;
        }
        public async Task<Branch> FindBranchById(int id)
        {
            var result = await _context.Branch.Include(a => a.BranchAddresses).Include(d => d.BranchCommittees)
               .Where(w => w.BranchId == id).FirstOrDefaultAsync();
            return result;
        }
        public async Task<List<int>> GetVROBranchsByVROOfficeCode(string VROOfficeCode)
        {
            var result = await _context.Branch.Where(b => b.AgencyCode == VROOfficeCode && b.IsActive == true).Select(b => b.BranchId).Distinct().ToListAsync();
            return result;
        }
        public async Task<BranchCommittee> GetAssignedCommitteeByIdAndBranch(int committeeId, int branchId)
        {
            var result = await _context.BranchCommittees.Where(x => x.CommitteeId == committeeId && x.BranchId == branchId && x.IsActive == true).FirstOrDefaultAsync();
            return result;
        }
        public async Task<BranchUser> GetAssignedBranchUsers(int userId, int branchId, string roleName)
        {
            Enums.UserRole choice;
            if (Enum.TryParse(roleName, out choice))
            {
                uint value = (uint)choice;
            }
            int roleId = (int)choice;
            var result = await _context.BranchUsers
                .Where(
                b => b.IsActive == true && b.UserProfileId == userId && b.BranchId == branchId &&
        (
          ((b.UserRoleId == (int)Enums.UserRole.NewMonafasat_DataEntry || b.UserRoleId == (int)Enums.UserRole.NewMonafasat_Auditer) && (roleId == (int)Enums.UserRole.NewMonafasat_DataEntry || roleId == (int)Enums.UserRole.NewMonafasat_Auditer))
          ||
          ((b.UserRoleId == (int)Enums.UserRole.NewMonafasat_PlanningApprover || b.UserRoleId == (int)Enums.UserRole.NewMonafasat_PlanningOfficer) && (roleId == (int)Enums.UserRole.NewMonafasat_PlanningApprover || roleId == (int)Enums.UserRole.NewMonafasat_PlanningOfficer))
          )).FirstOrDefaultAsync();
            return result;
        }
        public async Task<BranchUser> GetAssignedUserForMangById(int userId, int branchId, int roleId)
        {
            var result = await _context.BranchUsers.Where(x => x.BranchId == branchId && x.UserProfileId == userId && (x.UserRoleId == roleId) && x.IsActive == true).Include(x => x.UserProfile).FirstOrDefaultAsync();
            return result;
        }
        public async Task<QueryResult<LookupModel>> GetBranchCommittees(BranchCommitteeSearchCriteriaModel criteria)
        {
            var result = await _context.BranchCommittees
                 .Where(c => c.BranchId == criteria.BranchId && c.IsActive == true && c.Committee.IsActive == true)
                 .Select(x => new LookupModel
                 {
                     Id = x.CommitteeId,
                     Name = x.Committee.CommitteeName
                 }).ToQueryResult(criteria.PageNumber, criteria.PageSize);
            return result;
        }
        public async Task<int> GetUserDefultBranch(int userId, string agencyCode)
        {
            var result = await _context.BranchUsers.Include(x => x.UserProfile)
            .Where(x => x.UserProfileId == userId
            && x.Branch.AgencyCode == agencyCode
            && x.IsActive == true && x.Branch.IsActive == true).Select(b => b.BranchId).FirstOrDefaultAsync();
            return result;
        }
        public async Task<int> GetUserDefultCommittees(int userId, string agencyCode)
        {
            var result = await _context.CommitteeUsers
           .Where(x => x.IsActive == true && x.UserProfileId == userId && x.Committee.AgencyCode == agencyCode).Select(b => b.CommitteeId).FirstOrDefaultAsync();
            return result;

        }
        public async Task<List<string>> GetUserRoleByCommittee(int committeeId, int userId)
        {
            var result = await _context.CommitteeUsers
           .Where(x => x.IsActive == true && x.UserProfileId == userId && x.CommitteeId == committeeId).Select(b => b.UserRole.Name).ToListAsync();
            return result;

        }
        public async Task<List<string>> GetUserRolesByBranch(int userId, int branchId)
        {
            var result = await _context.BranchUsers
            .Where(x => x.UserProfileId == userId && x.BranchId == branchId && x.IsActive == true).Select(b => b.UserRole.Name).ToListAsync();
            return result;
        }
        public async Task<List<string>> GetUserRolesByBranchFromCommittees(int userId, int branchId)
        {
            var result = await _context.CommitteeUsers
            .Where(x => x.UserProfileId == userId
            && x.Committee.BranchCommittees.Any(a => a.BranchId == branchId)
            && x.IsActive == true).Select(b => b.UserRole.Name).ToListAsync();
            return result;
        }
        public async Task<List<UserRolesModel>> GetAllUserRolesFromBranches(int userId, string agencyCode)
        {
            var result = await _context.BranchUsers.Where(x => x.IsActive == true && x.UserProfileId == userId && x.Branch.AgencyCode == agencyCode && x.Branch.IsActive == true)
                .Select(b => new UserRolesModel
                {
                    RoleValue = "1" + "," + b.BranchId + "," + b.UserRole.Name,
                    RoleName = b.UserRole.DisplayNameAr + " " + b.Branch.BranchName,
                }).ToListAsync();
            return result;
        }
        public async Task<List<UserRolesModel>> GetAllUserRolesFromCommittees(int userId, string agencyCode)
        {
            var result = await _context.CommitteeUsers.Include(s => s.Committee).Where(x => x.IsActive == true && x.UserProfileId == userId && x.Committee.AgencyCode == agencyCode && x.Committee.IsActive == true)
                .Select(b => new UserRolesModel
                {
                    RoleValue = "0" + "," + b.CommitteeId + "," + b.UserRole.Name,
                    RoleName = b.UserRole.DisplayNameAr + " " + b.Committee.CommitteeName,
                }).ToListAsync();
            return result;
        }
        public async Task<List<LookupModel>> GetAllBranchesByAgencyCode(string agencyCode)
        {
            var result = await _context.Branch.Where(b => b.IsActive == true)
                .WhereIf(!string.IsNullOrEmpty(agencyCode), x => x.AgencyCode == agencyCode && x.IsActive.HasValue && x.IsActive.Value)
                .Select(x => new LookupModel
                {
                    Id = x.BranchId,
                    Name = x.BranchName
                }).ToListAsync();
            return result;
        }
        public async Task<List<LookupModel>> GetAllBranchesNotAssignedToCommittee(int committeeId, int committeeType, string agencyCode)
        {
            var result = await _context.Branch.Where(x => x.IsActive == true && x.AgencyCode == agencyCode && (x.BranchCommittees.Any(b => b.CommitteeId == committeeId && b.IsActive == true) ||
                                                !x.BranchCommittees.Any(bc => bc.Committee.CommitteeTypeId == committeeType && bc.IsActive == true)))
                .Select(x => new LookupModel
                {
                    Id = x.BranchId,
                    Name = x.BranchName
                }).ToListAsync();
            return result;
        }
        public async Task<BranchModel> GetBranchByName(string branchName, string agecyCode)
        {
            var result = await _context.Branch.Where(a => a.BranchName == branchName && a.IsActive == true && a.AgencyCode == agecyCode).Select(x => new BranchModel
            {
                BranchName = x.BranchName,
                BranchIdString = Util.Encrypt(x.BranchId),
                BranchId = x.BranchId
            }).FirstOrDefaultAsync();
            return result;
        }
        public async Task<List<BranchModel>> GetBranchByAgency(string agencyCode)
        {
            var result = await _context.Branch.Where(a => (a.AgencyCode == agencyCode) && a.IsActive == true).Select(x => new BranchModel
            {
                BranchName = x.BranchName,
                BranchIdString = Util.Encrypt(x.BranchId),
                BranchId = x.BranchId
            }).ToListAsync();
            return result;
        }
        public async Task<GovAgency> GetAgencyCodeByBranchId(int id)
        {
            var result = await _context.Branch.Where(a => (a.BranchId == id) && a.IsActive == true).Select(b => b.Agency).FirstOrDefaultAsync();
            return result;
        }
        public async Task<bool> GetHasTendersByBranch(int branchId)
        {
            var result = await _context.Tenders.Where(r => r.IsActive == true && r.BranchId == branchId).FirstOrDefaultAsync();
            if (result != null)
                return true;
            else return false;
        }
    }
}
