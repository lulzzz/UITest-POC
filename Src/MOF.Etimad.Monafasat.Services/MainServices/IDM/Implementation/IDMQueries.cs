using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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
    public class IDMQueries : IIDMQueries
    {
        private readonly IAppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public IDMQueries(IAppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<GovAgency> FindGovAgencyByIdAsync(string agencyCode)
        {
            var result = await _context.GovAgencies.Where(a => a.AgencyCode == agencyCode).FirstOrDefaultAsync();
            return result;
        }
        public async Task<UserProfile> FindUserProfileByUserNameAsync(string UserName)
        {
            var result = await _context.UserProfiles.Include(x => x.NotificationSetting).Include(x => x.BranchUsers).Where(a => a.UserName == UserName).FirstOrDefaultAsync();
            return result;
        }

        public async Task<UserProfile> FindUserProfileByIdAsync(int userProfileId)
        {
            var result = await _context.UserProfiles
                                .Include(u => u.BranchUsers)
                                .ThenInclude(u => u.Branch).AsNoTracking()
                                .Include("BranchUsers.UserRole").AsNoTracking()
                                .Include(u => u.CommitteeUsers)
                                .ThenInclude(u => u.Committee)
                                .Include("CommitteeUsers.UserRole").AsNoTracking()
                                .Where(a => a.Id == userProfileId).FirstOrDefaultAsync();
            return result;
        }

        public async Task<UserProfile> FindUserProfileByIdWithoutIncludesAsync(int userProfileId)
        {
            var result = await _context.UserProfiles
                                .Where(a => a.Id == userProfileId).FirstOrDefaultAsync();
            return result;
        }

        public async Task<SupplierUserProfile> FindSupplierUserProfileByUserProfileIdAndCrAsync(int userProfileId, string cr)
        {
            var result = await _context.SupplierUserProfiles.Where(a => a.UserProfileId == userProfileId && a.SelectedCr == cr).FirstOrDefaultAsync();
            return result;
        }
        public async Task<Supplier> FindSupplierObjectByUserIdAsync(string cr)
        {
            var result = await _context.Suppliers.Where(a => a.SelectedCr == cr).Include(s => s.NotificationSetting).FirstOrDefaultAsync();
            return result;
        }
        public async Task<Supplier> FindSupplierByCrAsync(string cr)
        {
            var result = await _context.SupplierUserProfiles.Include(s => s.UserProfile).ThenInclude(u => u.SupplierUserProfiles).Where(a => a.SelectedCr == cr).Select(s => s.Supplier).FirstOrDefaultAsync();
            return result;
        }

        public async Task<List<GovAgencyModel>> GetAllAgencies()
        {
            return await _context.GovAgencies.Select(g => new GovAgencyModel
            {
                AgencyCode = g.AgencyCode,
                NameArabic = g.AgencyCode.Contains("VRO") ? g.NameArabic + (" - VRO") : g.NameArabic,
                NameEnglish = g.AgencyCode.Contains("VRO") ? g.NameEnglish + (" - VRO") : g.NameEnglish

            }).ToListAsync();
        }
        public async Task<List<LookupModel>> GetAllAgenciesList()
        {
            return await _context.GovAgencies.Select(g => new LookupModel
            {
                Value = g.AgencyCode,
                Name = g.AgencyCode.Contains("VRO") ? g.NameArabic + (" - VRO") : g.NameArabic
            }).ToListAsync();
        }
        public async Task<List<UserLookupModel>> FindUsersByRoleNameAndAgencyCodeAsync(string roleName, string agencyCode)
        {
            var result = await _context.CommitteeUsers.Include(x => x.UserProfile)
                .Where(a => a.UserRole.Name == roleName && a.Committee.AgencyCode == agencyCode && a.IsActive == true)
                .Select(c => new UserLookupModel
                {
                    UserId = c.UserProfileId,
                    Name = c.UserProfile.FullName
                }).ToListAsync();
            return result;
        }

        public async Task<List<UserLookupModel>> GetAllDataEntryAndAuditorUsers(string roleName, string agencyCode)
        {
            var result = await _context.BranchUsers.Include(x => x.UserProfile)
                .Where(a => a.UserRole.Name == roleName && a.IsActive == true)
                .WhereIf(!_httpContextAccessor.HttpContext.User.IsInRole(RoleNames.CustomerService) && !_httpContextAccessor.HttpContext.User.IsInRole(RoleNames.MonafasatAccountManager)
                , a => a.Branch.AgencyCode == agencyCode)
                .Select(c => new UserLookupModel
                {
                    UserId = c.UserProfileId,
                    Name = c.UserProfile.FullName,
                    UserName = c.UserProfile.UserName
                }).Distinct().ToListAsync();
            return result;
        }
        public async Task<List<UserLookupModel>> GetUserBasedOnlistOfRoleName(List<int> lstOfuserRoles, string agencyCode)
        {
            var result = await _context.UserProfiles
        .Where(a => a.BranchUsers.Any(u => lstOfuserRoles.Contains(u.UserRoleId) && u.Branch.AgencyCode == agencyCode) || a.CommitteeUsers.Any(u => lstOfuserRoles.Contains(u.UserRoleId) && u.Committee.AgencyCode == agencyCode))
        .Select(c => new UserLookupModel
        {
            UserId = c.Id,
            Name = c.FullName,
            UserName = c.UserName
        }).Distinct().ToListAsync();
            return result;
        }

        public async Task<QueryResult<AgencyExceptedModel>> GetAgencyExceptedModel(BlockSearchCriteriaModel criteria)
        {
            var tenderType = _context.TenderTypes.ToList();
            var result = await _context.GovAgencies
                .Where(a => a.IsActive == true)
                .WhereIf(!string.IsNullOrEmpty(criteria.AgencyCode), a => a.AgencyCode == criteria.AgencyCode)
                .WhereIf(!string.IsNullOrEmpty(criteria.AgencyName), a => a.NameArabic.Contains(criteria.AgencyName))
                .WhereIf(criteria.IsOldBlock != null, a => a.IsOldSystem == criteria.IsOldBlock)
                .WhereIf(criteria.TenderTypeId != 0, a => a.PurchaseMethods.Contains(criteria.TenderTypeId.ToString()))
                .Select(c => new AgencyExceptedModel
                {
                    AgencyIdString = c.AgencyCode,
                    NameArabic = c.NameArabic,
                    AgencyCode = c.AgencyCode,
                    CategoryId = c.CategoryId,
                    TenderTypes = GetAgencyPurchaseMethodsModels(c.PurchaseMethods, tenderType),
                    IsVro = c.IsVRO,
                    IsRelated = c.VROOfficeCode == null ? false : true,
                    IsOldSystem = c.IsOldSystem,
                    PurchaseMethodString = c.PurchaseMethods,
                    MobileNumber = c.MobileNumber
                }).ToQueryResult(criteria.PageNumber, criteria.PageSize);
            return result;
        }

        private static List<PurchaseMethodsModel> GetAgencyPurchaseMethodsModels(string GOVTypes, List<TenderType> tenderTypes)
        {
            if (string.IsNullOrEmpty(GOVTypes))
            {
                return new List<PurchaseMethodsModel>();
            }
            List<int> SelectedTypes = GOVTypes.Split(',').AsEnumerable().Select(r => int.Parse(r)).ToList();
            List<PurchaseMethodsModel> Selected = tenderTypes.Where(s => SelectedTypes.Any(d => d == s.TenderTypeId) && s.TenderTypeId != (int)Enums.TenderType.PreQualification && s.TenderTypeId != (int)Enums.TenderType.PostQualification && s.TenderTypeId != (int)Enums.TenderType.NationalTransformationProjects)
            .Select(d => new PurchaseMethodsModel
            {
                IsSelected = true,
                PurchaseMethodId = d.TenderTypeId,
                PurchaseMethodName = d.NameAr
            }).ToList();

            return Selected;
        }

        public async Task<AgencyExceptedModel> GetAgencyExceptedById(string agencyId)
        {
            var result = await _context.GovAgencies
                .Where(a => a.IsActive == true && a.AgencyCode == agencyId)
                .Select(c => new AgencyExceptedModel
                {
                    AgencyIdString = c.AgencyCode,
                    NameArabic = c.NameArabic,

                    TenderTypes = _context.TenderTypes.Where(r => r.IsActive == true && r.TenderTypeId != (int)Enums.TenderType.PreQualification && r.TenderTypeId != (int)Enums.TenderType.PostQualification).Select(r => new PurchaseMethodsModel { PurchaseMethodId = r.TenderTypeId, PurchaseMethodName = r.NameAr, IsSelected = false }).ToList(),
                    IsOldSystem = c.IsOldSystem,
                    IsVro = c.IsVRO,
                    IsRelated = c.VROOfficeCode == null ? false : true,
                    PurchaseMethodString = c.PurchaseMethods
                }).FirstOrDefaultAsync();
            return result;
        }


        public async Task<List<int>> FindUsersByRoleNameAndAgencyCodeAssignedToCommitteeIdAsync(string roleName, string agencyCode, int committeeId)
        {
            int roleId = (int)Enum.Parse(typeof(Enums.UserRole), roleName);
            var result = await _context.Committees.Include(r => r.CommitteeUsers).Where(a => a.CommitteeId == committeeId && a.AgencyCode == agencyCode && a.IsActive == true).FirstOrDefaultAsync();
            return result.CommitteeUsers.Where(u => u.UserRoleId == roleId && u.IsActive == true).Select(r => r.UserProfileId).ToList();
        }

        public async Task<List<string>> GetAllSupplierOnTender(int tenderId)
        {
            var invitedsuppliers = await _context.Invitations
            .Where(x => x.TenderId == tenderId && x.IsActive == true && x.StatusId == (int)Enums.InvitationStatus.Approved)
                .Select(c => c.CommericalRegisterNo).ToListAsync();
            var purchsaseSuppliers = await _context.ConditionsBooklets
            .Where(x => x.TenderId == tenderId && x.IsActive == true)
                .Select(c => c.CommericalRegisterNo).ToListAsync();
            invitedsuppliers.AddRange(purchsaseSuppliers);
            return invitedsuppliers;
        }



        public async Task<List<string>> GetAllSupplierOnQualification(int tenderId)
        {
            var invitedsuppliers = await _context.PostQualificationSuppliersInvitations
            .Where(x => x.PostQualificationId == tenderId && x.IsActive == true)
                .Select(c => c.CommercialNumber).ToListAsync();

            var qualificationSupplier = await _context.QualificationFinalResult
            .Where(x => x.TenderId == tenderId && x.IsActive == true)
                .Select(c => c.SupplierSelectedCr).ToListAsync();
            invitedsuppliers.AddRange(qualificationSupplier);
            return invitedsuppliers;
        }
        public async Task<List<string>> GetAllSupplierQualificationToSendInvitation(int tenderId)
        {

            var qualificationSupplier = await _context.QualificationFinalResult
            .Where(x => x.TenderId == tenderId && x.IsActive == true)
                .Select(c => c.SupplierSelectedCr).ToListAsync();

            var enquiryList = await _context.Enquiries.Where(x => x.TenderId == tenderId && x.IsActive == true).Select(c => c.CommericalRegisterNo).ToListAsync();

            qualificationSupplier.AddRange(enquiryList);


            return qualificationSupplier;
        }


        public async Task<List<int>> GetTechnicalCommitteeMembersOnTender(int committeeId)
        {
            var result = await _context.CommitteeUsers.Where(x => x.CommitteeId == committeeId && x.IsActive == true)
                .Select(x => x.UserProfileId).Distinct().ToListAsync();

            return result;
        }

        public async Task<List<CommitteeModel>> FindCommitteesNotAssignedToBranch(string agencyCode, int branchId)
        {
            var result = await _context.Committees
               .Where(a => a.IsActive == true && a.AgencyCode == agencyCode && !a.BranchCommittees.Any(u => u.BranchId == branchId && u.IsActive == true))
               .Select(c => new CommitteeModel
               {
                   CommitteeId = c.CommitteeId,
                   CommitteeName = c.CommitteeName
               }).ToListAsync();
            return result;
        }
        public async Task<List<CommitteeModel>> FindCommitteeNotAssignedToUser(string agencyCode, int userId)
        {
            var committeeAssignedToUserIds = await _context.CommitteeUsers
                            .Where(a => a.IsActive == true && a.UserProfileId == userId && (a.Committee.IsActive.HasValue && a.Committee.IsActive.Value))
                            .Select(c => c.CommitteeId).ToListAsync();

            var Committeelst = await _context.Committees
                            .Include(c => c.Agency)
                            .Where(a => a.IsActive == true && a.AgencyCode == agencyCode && !committeeAssignedToUserIds.Contains(a.CommitteeId))
                            .Select(c => new CommitteeModel
                            {
                                CommitteeId = c.CommitteeId,
                                CommitteeName = c.CommitteeName,
                                CommitteeTypeId = c.CommitteeTypeId,
                                CreatedAt = c.CreatedAt
                            }).OrderByDescending(o => o.CreatedAt).ToListAsync();

            return Committeelst;
        }
        public async Task<List<CommitteeUserAssignModel>> FindCommitteeAssignedToUser(string agencyCode, int userId)
        {
            var committeeUserAssignModel = await _context.CommitteeUsers
                            .Include(c => c.Committee.Agency)
                            .Where(a => a.IsActive == true && a.UserProfileId == userId && (!String.IsNullOrEmpty(agencyCode) ? a.Committee.AgencyCode == agencyCode : true) && (a.Committee.IsActive.HasValue && a.Committee.IsActive.Value)).OrderByDescending(c => c.CreatedAt)
                            .Select(c => new CommitteeUserAssignModel
                            {
                                CommitteeId = c.CommitteeId,
                                CommitteeName = c.Committee.CommitteeName,
                                RoleName = c.UserRole.Name,
                                CommitteeUserId = c.CommitteeUserId,

                            }).ToListAsync();
            return committeeUserAssignModel;
        }
        public async Task<List<BranchModel>> FindBranchesNotAssignedToUser(string agencyCode, int userId)
        {
            var branchAssignedToUserIds = await _context.BranchUsers
                             .Where(a => a.IsActive == true && a.UserProfileId == userId && (a.Branch.IsActive.HasValue && a.Branch.IsActive.Value)).OrderByDescending(b => b.CreatedAt)
                             .Select(c => c.BranchId).ToListAsync();
            var brancheslst = await _context.Branch
                             .Include(b => b.Agency)
                            .Where(a => a.IsActive == true && a.AgencyCode == agencyCode && !branchAssignedToUserIds.Contains(a.BranchId))
                            .Select(b => new BranchModel
                            {
                                BranchId = b.BranchId,
                                BranchName = b.BranchName,
                                CreatedAt = b.CreatedAt
                            }).OrderByDescending(o => o.CreatedAt).ToListAsync();

            return brancheslst;
        }

        public async Task<List<BranchUserAssignModel>> FindBranchesAssignedToUser(string agencyCode, int userId)
        {
            var branchesUserAssignModel = await _context.BranchUsers
                      .Include(b => b.Branch.Agency)
                      .Where(a => a.IsActive == true && a.UserProfileId == userId && (!String.IsNullOrEmpty(agencyCode) ? a.Branch.AgencyCode == agencyCode : true) && (a.Branch.IsActive.HasValue && a.Branch.IsActive.Value))
                      .Select(c => new BranchUserAssignModel
                      {
                          RoleId = c.UserRoleId,
                          BranchId = c.BranchId,
                          BranchName = c.Branch.BranchName,
                          RoleName = c.UserRole.DisplayNameAr,
                          BranchUserId = c.BranchUserId,
                          EstimatedValueTo = c.EstimatedValueTo,
                          EstimatedValueFrom = c.EstimatedValueFrom
                      }).ToListAsync();
            return branchesUserAssignModel;
        }

        public async Task<List<string>> FindUsersAssignedToBranch(UsersSearchCriteriaModel userSearchCriteriaModel)
        {
            var branchesAssignedToUser = await _context.BranchUsers
                      .Where(a => a.IsActive == true && a.BranchId == int.Parse(userSearchCriteriaModel.BranchId))
                      .WhereIf(!string.IsNullOrEmpty(userSearchCriteriaModel.NationalId), a => a.UserProfile.UserName == userSearchCriteriaModel.NationalId)

                      .WhereIf(!string.IsNullOrEmpty(userSearchCriteriaModel.MobileNumber), a => a.UserProfile.Mobile == userSearchCriteriaModel.MobileNumber.Trim())
                      .WhereIf(!string.IsNullOrEmpty(userSearchCriteriaModel.Name), a => a.UserProfile.FullName.Contains(userSearchCriteriaModel.Name.Trim()))
                      .WhereIf(!string.IsNullOrEmpty(userSearchCriteriaModel.Email), a => a.UserProfile.Email == userSearchCriteriaModel.Email.Trim())
                      .Select(c => c.UserProfile.UserName).Distinct().ToListAsync();
            if (branchesAssignedToUser.Count == 0)
                branchesAssignedToUser.Add("0");
            return branchesAssignedToUser;
        }

        public async Task<List<BranchModel>> FindBranchesNotAssignedByUserAndRole(string agencyCode, int userId, string roleName)
        {
            var branchesUserAssignLst = await _context.BranchUsers
                .Include(b => b.Branch.Agency)
                .Where(a => a.IsActive == true && a.UserProfileId == userId && (!String.IsNullOrEmpty(agencyCode) ? a.Branch.AgencyCode == agencyCode : true) && (a.Branch.IsActive.HasValue && a.Branch.IsActive.Value))
                .Select(c => new BranchUserAssignModel
                {
                    RoleId = c.UserRoleId,
                    BranchId = c.BranchId,
                    BranchName = c.Branch.BranchName,
                    RoleName = c.UserRole.DisplayNameAr,
                    BranchUserId = c.BranchUserId
                }).ToListAsync();
            var branchUserAssignList = branchesUserAssignLst.AsQueryable<BranchUserAssignModel>().Select(x => x.BranchId).ToArray();
            var brancheslst = await _context.Branch
             .Include(b => b.Agency)
            .Where(a => a.IsActive == true && a.AgencyCode == agencyCode && !branchUserAssignList.Any(c => c == a.BranchId))
                        .Select(b => new BranchModel
                        {
                            BranchId = b.BranchId,
                            BranchName = b.BranchName,
                            CreatedAt = b.CreatedAt
                        }).OrderByDescending(o => o.CreatedAt).ToListAsync();
            return brancheslst;
        }

        public async Task<List<int>> GetAssignedUsersByAgency(string agencyCode)
        {
            List<int> userIds = new List<int>();
            userIds.AddRange(await _context.BranchUsers
                      .Where(a => a.IsActive == true && a.Branch.AgencyCode == agencyCode)
                      .Select(c => c.UserProfileId).Distinct().ToListAsync());
            userIds.AddRange(await _context.CommitteeUsers
                    .Where(a => a.IsActive == true && a.Committee.AgencyCode == agencyCode)
                    .Select(c => c.UserProfileId).Distinct().ToListAsync());
            userIds = userIds.Distinct().ToList();
            return userIds;
        }
    }
}