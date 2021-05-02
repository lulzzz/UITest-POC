using Microsoft.EntityFrameworkCore;
using MOF.Etimad.Monafasat.Core;
using MOF.Etimad.Monafasat.Data;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static MOF.Etimad.Monafasat.SharedKernel.Enums;

namespace MOF.Etimad.Monafasat.Services
{
    public class BlockQueries : IBlockQueries
    {
        private readonly IAppDbContext _context;
        public BlockQueries(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<QueryResult<SupplierBlockModel>> Find(BlockSearchCriteria criteria)
        {
            var result = await _context.SupplierBlock.Where(x => x.AgencyCode == criteria.AgencyCode && x.IsActive == true)
            .Where(x => x.BlockStatusId != (int)Enums.BlockStatus.RemoveBlock)
            .Where(x => x.BlockEndDate >= DateTime.Now || (x.BlockTypeId == (int)Enums.BlockType.Permenant))
            .WhereIf(criteria.OrganizationTypeId != 0, x => x.OrganizationTypeId == criteria.OrganizationTypeId)
            .WhereIf(!string.IsNullOrEmpty(criteria.CommercialRegistrationNo), x => x.CommercialRegistrationNo == criteria.CommercialRegistrationNo)
            .WhereIf(!string.IsNullOrEmpty(criteria.CommercialSupplierName), x => x.CommercialSupplierName.Contains(criteria.CommercialSupplierName))
            .WhereIf(criteria.ResolutionNumber != null, x => x.ResolutionNumber == criteria.ResolutionNumber)
            .WhereIf(criteria.BlockTypeId != 0, x => x.BlockTypeId == criteria.BlockTypeId)
            .WhereIf(criteria.FromDate != null, x => x.BlockStartDate >= criteria.FromDate)
            .WhereIf(criteria.ToDate != null, x => x.BlockEndDate <= criteria.ToDate)

            .WhereIf(criteria.IsDeletedId == (int)IsDeleted.Deleted, r => r.IsActive.HasValue && !r.IsActive.Value)
            .WhereIf(criteria.IsDeletedId == (int)IsDeleted.Active, r => r.IsActive.HasValue && r.IsActive.Value)
            .OrderBy(x => x.SupplierBlockId)
            .SortBy(criteria.Sort, criteria.SortDirection).Select(r => new SupplierBlockModel
            {
                AdminBlockReason = r.AdminBlockReason,
                AdminFileName = r.AdminFileName,
                AdminFileNetReferenceId = r.AdminFileNetReferenceId,
                AgencyCode = r.AgencyCode,
                BlockEndDate = r.BlockEndDate,
                BlockStartDate = r.BlockStartDate,
                BlockStatusId = r.BlockStatusId,
                CommercialRegistrationNo = r.CommercialRegistrationNo,
                CommercialSupplierName = r.CommercialSupplierName,
                Punishment = r.Punishment,
                ResolutionNumber = r.ResolutionNumber,
                SecretaryBlockReason = r.SecretaryBlockReason,
                SecretaryFileName = r.SecretaryFileName,
                SecretaryFileNetReferenceId = r.SecretaryFileNetReferenceId,
                BlockStatusName = r.BlockStatus.BlockStatusNameAr,
                SupplierBlockIdString = Util.Encrypt(r.SupplierBlockId),
                SupplierTypeId = r.SupplierTypeId,
                OrganizationName = RetturnOrganizationName(r.OrganizationTypeId),
            })
             .ToQueryResult(criteria.PageNumber, criteria.PageSize);
            return result;
        }


        public async Task<QueryResult<SupplierBlockModel>> FindBlockedUser(BlockSearchCriteria criteria)
        {
            var result = await _context.SupplierBlock.Where(x => x.IsActive == true)
            .Where(x => x.BlockStatusId != (int)Enums.BlockStatus.RemoveBlock)
            .Where(x => ((x.BlockStatusId == (int)Enums.BlockStatus.ApprovedManager && (x.BlockEndDate > DateTime.Now || x.BlockEndDate == null)) || x.BlockStatusId == (int)Enums.BlockStatus.ApprovedSecertary || x.BlockStatusId == (int)Enums.BlockStatus.NewAdmin || x.BlockStatusId == (int)Enums.BlockStatus.NewSecretary))
            .WhereIf(criteria.OrganizationTypeId != 0, x => x.OrganizationTypeId == criteria.OrganizationTypeId)
            .WhereIf(!string.IsNullOrEmpty(criteria.CommercialRegistrationNo), x => x.CommercialRegistrationNo == criteria.CommercialRegistrationNo)
            .WhereIf(!string.IsNullOrEmpty(criteria.CommercialSupplierName), x => x.CommercialSupplierName.Contains(criteria.CommercialSupplierName))
            .OrderBy(x => x.SupplierBlockId)
            .SortBy(criteria.Sort, criteria.SortDirection).Select(r => new SupplierBlockModel
            {
                AdminBlockReason = r.AdminBlockReason,
                AdminFileName = r.AdminFileName,
                AdminFileNetReferenceId = r.AdminFileNetReferenceId,
                AgencyCode = r.AgencyCode,
                BlockEndDate = r.BlockEndDate,
                BlockStartDate = r.BlockStartDate,
                BlockStatusId = r.BlockStatusId,
                CommercialRegistrationNo = r.CommercialRegistrationNo,
                CommercialSupplierName = r.CommercialSupplierName,
                Punishment = r.Punishment,
                ResolutionNumber = r.ResolutionNumber,
                SecretaryBlockReason = r.SecretaryBlockReason,
                SecretaryFileName = r.SecretaryFileName,
                SecretaryFileNetReferenceId = r.SecretaryFileNetReferenceId,
                BlockStatusName = r.BlockStatus.BlockStatusNameAr,
                SupplierBlockIdString = Util.Encrypt(r.SupplierBlockId),
                SupplierTypeId = r.SupplierTypeId,
                OrganizationTypeId = r.OrganizationTypeId,
                OrganizationName = RetturnOrganizationName(r.OrganizationTypeId)
            })
             .ToQueryResult(criteria.PageNumber, criteria.PageSize);
            return result;
        }

        public async Task<List<SupplierAgencyBlockModel>> GetAllCurrentBlockedSuppliers(string agencyCode, List<string> CRs)
        {
            var entities = await _context.SupplierBlock
                .Where(b => b.IsActive == true)
                .WhereIf(!string.IsNullOrEmpty(agencyCode), b => b.AgencyCode == agencyCode || b.AgencyCode == null || b.IsTotallyBlocked == true)
                .Where(b => CRs.Contains(b.CommercialRegistrationNo))
                .Where(b => (b.BlockTypeId == (int)Enums.BlockType.Permenant || (b.BlockTypeId == (int)Enums.BlockType.Tamporary && (b.BlockStartDate <= DateTime.Now && b.BlockEndDate >= DateTime.Now))) && (b.BlockStatusId == (int)Enums.BlockStatus.ApprovedManager))
                .Select(b => new SupplierAgencyBlockModel { AgencyCode = b.AgencyCode, CommercialRegistrationNo = b.CommercialRegistrationNo })
                .ToListAsync();
            return entities;
        }

        public async Task<List<SupplierAgencyBlockModel>> GetAllBlockedSuppliers(string agencyCode, List<string> CRs)
        {
            var entities = await _context.SupplierBlock
                .Where(b => b.IsActive == true)
                .Where(b => b.AgencyCode == agencyCode || b.AgencyCode == null || b.IsTotallyBlocked == true)
                .Where(b => b.BlockTypeId == (int)Enums.BlockType.Permenant || (b.BlockTypeId == (int)Enums.BlockType.Tamporary && (b.BlockStartDate <= DateTime.Now && b.BlockEndDate >= DateTime.Now)))
                .Select(b => new SupplierAgencyBlockModel { AgencyCode = b.AgencyCode, CommercialRegistrationNo = b.CommercialRegistrationNo })
                .ToListAsync();
            return entities;
        }

        public async Task<SupplierBlock> FindBlockByIdAsync(int supplierBlockId)
        {
            return await _context.SupplierBlock.Include(d => d.Supplier).Include(r => r.Agency).Where(a => a.SupplierBlockId == supplierBlockId).FirstOrDefaultAsync();
        }

        public async Task<QueryResult<BlockCommitteeModel>> FindBlockCommittee(BlockCommitteeSearchCriteria criteria)
        {
            var result = await _context.SupplierBlock.Include(x => x.BlockType)
            .WhereIf(!string.IsNullOrEmpty(criteria.CommercialRegistrationNo), x => x.CommercialRegistrationNo.Contains(criteria.CommercialRegistrationNo))
            .WhereIf(!string.IsNullOrEmpty(criteria.CommercialSupplierName), x => x.CommercialSupplierName.Contains(criteria.CommercialSupplierName))
            .OrderBy(x => x.SupplierBlockId)
            .SortBy(criteria.Sort, criteria.SortDirection).Select(r => new BlockCommitteeModel { CommercialRegistrationNo = r.CommercialRegistrationNo, CommercialSupplierName = r.CommercialSupplierName })
            .ToQueryResult(criteria.PageNumber, criteria.PageSize);
            return result;
        }

        public async Task<bool> CheckifSupplierBlockedByCrNo(string commericalRegisterNo, string AgencyCode = null)
        {
            var nowDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            var entities = await _context.SupplierBlock
                .Where(b => b.CommercialRegistrationNo == commericalRegisterNo && b.IsActive == true && b.BlockStatusId == (int)Enums.BlockStatus.ApprovedManager)
                .Where(b => b.IsTotallyBlocked == true || b.AgencyCode == AgencyCode || b.AgencyCode == null)
                .Where(b => (b.BlockTypeId == (int)Enums.BlockType.Permenant) || (b.BlockTypeId == (int)Enums.BlockType.Tamporary && b.BlockStartDate <= nowDate && b.BlockEndDate >= nowDate))
                .OrderByDescending(x => x.CreatedAt).FirstOrDefaultAsync();
            if (entities == null)
                return false;
            else
                return true;
        }

        public async Task<QueryResult<SupplierBlockModel>> FindAdminBlockList(BlockSearchCriteria criteria)
        {
            char[] spearator = { '|' };
            var result = await _context.SupplierBlock
            .Where(x => x.IsActive == true)
            .WhereIf(!string.IsNullOrEmpty(criteria.CommercialRegistrationNo), x => x.CommercialRegistrationNo.Contains(criteria.CommercialRegistrationNo))
            .WhereIf(!string.IsNullOrEmpty(criteria.CommercialSupplierName), x => x.CommercialSupplierName.Contains(criteria.CommercialSupplierName))
                        .WhereIf(!string.IsNullOrEmpty(criteria.AgencyCode), x => x.AgencyCode == criteria.AgencyCode)
            .OrderBy(x => x.SupplierBlockId)
            .SortBy(criteria.Sort, criteria.SortDirection).Select(r => new SupplierBlockModel
            {
                AgencyCode = r.AgencyCode,
                CommercialRegistrationNo = r.CommercialRegistrationNo,
                CommercialSupplierName = r.CommercialSupplierName,
                SupplierBlockIdString = Util.Encrypt(r.SupplierBlockId),
                CommercialSupplierNameEncrypted = Util.Encrypt(r.CommercialSupplierName),
                CommercialRegistrationNoEncrypted = Util.Encrypt(r.CommercialRegistrationNo),
                BlockStatusId = r.BlockStatusId,
                BlockStatusName = r.BlockStatus.BlockStatusNameAr,
                SupplierTypeId = r.SupplierTypeId,
                OrganizationTypeId = r.OrganizationTypeId,
                CreatedBy = r.CreatedBy.Split(spearator)[0],
                BlockEndDate = r.BlockEndDate,
                BlockStartDate = r.BlockStartDate,
                FromDateString = r.BlockStartDate != null ? Util.GetGregorianDateWithFormat(r.BlockStartDate, "dd/MM/yyyy") : null,
                ToDateString = r.BlockEndDate != null ? Util.GetGregorianDateWithFormat(r.BlockEndDate, "dd/MM/yyyy") : null,
                OrganizationName = RetturnOrganizationName(r.OrganizationTypeId)

            })
            .ToQueryResult(criteria.PageNumber, criteria.PageSize);
            return result;
        }

        public async Task<GovAgency> GetAgencyById(string agencyId)
        {
            return await _context.GovAgencies.Where(x => x.IsActive == true && x.AgencyCode == agencyId).FirstOrDefaultAsync();
        }

        public async Task<SupplierBlockModel> FindBlockById(int supplierBlockId)
        {

            return await _context.SupplierBlock.Where(a => a.SupplierBlockId == supplierBlockId && a.IsActive == true).Select(r => new SupplierBlockModel
            {
                AdminBlockReason = r.AdminBlockReason,
                AdminFileName = r.AdminFileName,
                AdminFileNetReferenceId = r.AdminFileNetReferenceId,
                AgencyCode = r.AgencyCode,
                BlockEndDate = r.BlockEndDate,
                BlockStartDate = r.BlockStartDate,
                BlockEndDateHijri = r.BlockEndDate != null ? Util.GetHigriDateWithFormat(r.BlockStartDate, "dd/MM/yyyy") : null,
                BlockStartDateHijri = r.BlockStartDate != null ? Util.GetHigriDateWithFormat(r.BlockStartDate, "dd/MM/yyyy") : null,
                BlockStatusId = r.BlockStatusId,
                CommercialRegistrationNo = r.CommercialRegistrationNo,
                CommercialSupplierName = r.CommercialSupplierName,
                Punishment = r.Punishment,
                ResolutionNumber = r.ResolutionNumber,
                SecretaryBlockReason = r.SecretaryBlockReason,
                SecretaryFileName = r.SecretaryFileName,
                SecretaryFileNetReferenceId = r.SecretaryFileNetReferenceId,
                ManagerRejectionReason = r.ManagerRejectionReason,
                SecretaryRejectionReason = r.SecretaryRejectionReason,
                SupplierBlockId = r.SupplierBlockId,
                SupplierBlockIdString = Util.Encrypt(r.SupplierBlockId),
                AgencyName = r.Agency.NameArabic,
                SupplierTypeId = r.SupplierTypeId,
                OrganizationTypeId = r.OrganizationTypeId,
                BlockDetails = r.BlockDetails,
                BlockStatusName = r.BlockStatus.BlockStatusNameAr,
                IsOldSystem = r.IsOldBlock,
                FromDateString = r.BlockStartDate != null ? Util.GetGregorianDateWithFormat(r.BlockStartDate, "dd/MM/yyyy") : null,
                ToDateString = r.BlockEndDate != null ? Util.GetGregorianDateWithFormat(r.BlockEndDate, "dd/MM/yyyy") : null,
                OrganizationName = RetturnOrganizationName(r.OrganizationTypeId)
            }).FirstOrDefaultAsync();
        }

        public SupplierBlock FindSupplierBlockById(int supplierBlockId)
        {
            return _context.SupplierBlock.Include(a => a.Agency).Where(a => a.SupplierBlockId == supplierBlockId).FirstOrDefault();
        }

        public async Task<QueryResult<SupplierBlockModel>> FindManagerBlockList(BlockSearchCriteria criteria)
        {
            var result = await _context.SupplierBlock
             .Where(x => x.IsActive == true && x.BlockStatusId != (int)Enums.BlockStatus.NewAdmin && x.BlockStatusId != (int)Enums.BlockStatus.RejectedSecertary).Where(x => x.BlockEndDate == null ? true : (x.BlockEndDate > DateTime.Now.Date))
             .WhereIf(!string.IsNullOrEmpty(criteria.CommercialRegistrationNo), x => x.CommercialRegistrationNo.Contains(criteria.CommercialRegistrationNo))
             .WhereIf(!string.IsNullOrEmpty(criteria.CommercialSupplierName), x => x.CommercialSupplierName.Contains(criteria.CommercialSupplierName))
             .WhereIf(!string.IsNullOrEmpty(criteria.AgencyCode), x => x.AgencyCode == criteria.AgencyCode)
             .OrderBy(x => x.SupplierBlockId)
             .SortBy(criteria.Sort, criteria.SortDirection).Select(r => new SupplierBlockModel
             {
                 BlockStatusId = r.BlockStatusId,
                 CommercialRegistrationNo = r.CommercialRegistrationNo,
                 CommercialSupplierName = r.CommercialSupplierName,
                 SupplierTypeId = r.SupplierTypeId,
                 OrganizationTypeId = r.OrganizationTypeId,
                 BlockStatusName = r.BlockStatus.BlockStatusNameAr,
                 SupplierBlockIdString = Util.Encrypt(r.SupplierBlockId)
             })
             .ToQueryResult(criteria.PageNumber, criteria.PageSize);
            return result;
        }

        public async Task<QueryResult<SupplierBlockModel>> FindSecretaryBlockList(BlockSearchCriteria criteria)
        {
            var currentDate = Convert.ToDateTime(DateTime.Now.Date.ToShortDateString());
            var result = await _context.SupplierBlock
             .Where(x => x.IsActive == true && x.BlockEndDate == null ? true : (x.BlockEndDate >= currentDate))
             .WhereIf(!string.IsNullOrEmpty(criteria.CommercialRegistrationNo), x => x.CommercialRegistrationNo.Contains(criteria.CommercialRegistrationNo))
             .WhereIf(!string.IsNullOrEmpty(criteria.CommercialSupplierName), x => x.CommercialSupplierName.Contains(criteria.CommercialSupplierName))
             .WhereIf(!string.IsNullOrEmpty(criteria.AgencyCode), x => x.AgencyCode == criteria.AgencyCode)
             .OrderBy(x => x.SupplierBlockId)
             .SortBy(criteria.Sort, criteria.SortDirection).Select(r => new SupplierBlockModel
             {
                 BlockStatusId = r.BlockStatusId,
                 CommercialRegistrationNo = r.CommercialRegistrationNo,
                 CommercialSupplierName = r.CommercialSupplierName,
                 SupplierTypeId = r.SupplierTypeId,
                 OrganizationTypeId = r.OrganizationTypeId,
                 BlockStatusName = r.BlockStatus.BlockStatusNameAr,
                 SupplierBlockIdString = Util.Encrypt(r.SupplierBlockId),
                 IsOldSystem = r.IsOldBlock
             })
             .ToQueryResult(criteria.PageNumber, criteria.PageSize);
            return result;
        }
        public async Task<QueryResult<SupplierBlockModel>> GetBlockingListDetails(BlockSearchCriteria criteria)
        {
            var result = await _context.SupplierBlock
             .Where(x => (x.BlockStatusId == (int)Enums.BlockStatus.ApprovedManager || x.BlockStatusId == (int)Enums.BlockStatus.RemoveBlock) && x.IsActive == true)
             .WhereIf(!string.IsNullOrEmpty(criteria.CommercialRegistrationNo), x => x.CommercialRegistrationNo.Contains(criteria.CommercialRegistrationNo))
             .WhereIf(!string.IsNullOrEmpty(criteria.CommercialSupplierName), x => x.CommercialSupplierName.Contains(criteria.CommercialSupplierName))
             .WhereIf(!string.IsNullOrEmpty(criteria.ResolutionNumber), x => x.ResolutionNumber == criteria.ResolutionNumber)
             .WhereIf(!string.IsNullOrEmpty(criteria.AgencyCode), x => x.AgencyCode == criteria.AgencyCode)
             .WhereIf((criteria.FromDate == null && criteria.ToDate != null), x => x.BlockEndDate == criteria.ToDate)
             .WhereIf((criteria.FromDate != null && criteria.ToDate == null), x => x.BlockStartDate == criteria.FromDate)
             .WhereIf(criteria.ToDate != null, x => x.BlockEndDate <= criteria.ToDate)
             .WhereIf(criteria.FromDate != null, x => x.BlockStartDate >= criteria.FromDate)
             .WhereIf(criteria.BlockStatusId == (int)IsActive.Active, x => (x.BlockEndDate >= DateTime.Now && x.BlockStartDate <= DateTime.Now))
             .WhereIf(criteria.IsDeletedId != 0, r => r.BlockStatusId == criteria.IsDeletedId)
             .OrderBy(x => x.SupplierBlockId)
             .SortBy(criteria.Sort, criteria.SortDirection).Select(r => new SupplierBlockModel
             {
                 AdminBlockReason = r.AdminBlockReason,
                 AdminFileName = r.AdminFileName,
                 AdminFileNetReferenceId = r.AdminFileNetReferenceId,
                 AgencyCode = r.AgencyCode,
                 BlockEndDate = r.BlockEndDate,
                 BlockStartDate = r.BlockStartDate,
                 BlockStatusId = r.BlockStatusId,
                 CommercialRegistrationNo = r.CommercialRegistrationNo,
                 CommercialSupplierName = r.CommercialSupplierName,
                 Punishment = r.Punishment,
                 BlockDuration = (r.BlockEndDate - r.BlockStartDate).ToString(),
                 ResolutionNumber = r.ResolutionNumber,
                 SecretaryBlockReason = r.SecretaryBlockReason,
                 SecretaryFileName = r.SecretaryFileName,
                 SecretaryFileNetReferenceId = r.SecretaryFileNetReferenceId,
                 BlockStatusName = r.BlockStatus.BlockStatusNameAr,
                 SupplierBlockIdString = Util.Encrypt(r.SupplierBlockId),
                 BlockDetails = r.BlockDetails,
                 ManagerRejectionReason = r.ManagerRejectionReason,
                 SecretaryRejectionReason = r.SecretaryRejectionReason,
                 SupplierTypeId = r.SupplierTypeId,
                 OrganizationTypeId = r.OrganizationTypeId,
                 IsOldSystem = r.IsOldBlock,
                 FromDateString = r.BlockStartDate != null ? Util.GetGregorianDateWithFormat(r.BlockStartDate, "dd/MM/yyyy") : null,
                 ToDateString = r.BlockEndDate != null ? Util.GetGregorianDateWithFormat(r.BlockEndDate, "dd/MM/yyyy") : null,

             })
             .ToQueryResult(criteria.PageNumber, criteria.PageSize);
            return result;
        }

        public async Task<QueryResult<SupplierBlockModel>> GetAdminBlockingListDetails(BlockSearchCriteria criteria)
        {
            var result = await _context.SupplierBlock.Where(x => x.AgencyCode == criteria.AgencyCode && x.IsActive == true)
             .Where(x => (x.BlockStatusId == (int)Enums.BlockStatus.ApprovedManager || x.BlockStatusId == (int)Enums.BlockStatus.RemoveBlock))
             .WhereIf(!string.IsNullOrEmpty(criteria.CommercialRegistrationNo), x => x.CommercialRegistrationNo.Contains(criteria.CommercialRegistrationNo))
             .WhereIf(!string.IsNullOrEmpty(criteria.CommercialSupplierName), x => x.CommercialSupplierName.Contains(criteria.CommercialSupplierName))
             .WhereIf(!string.IsNullOrEmpty(criteria.ResolutionNumber), x => x.ResolutionNumber == criteria.ResolutionNumber)
             .WhereIf((criteria.FromDate == null && criteria.ToDate != null), x => x.BlockEndDate == criteria.ToDate)
             .WhereIf((criteria.FromDate != null && criteria.ToDate == null), x => x.BlockStartDate == criteria.FromDate)
             .WhereIf(criteria.ToDate != null, x => x.BlockEndDate <= criteria.ToDate)
             .WhereIf(criteria.FromDate != null, x => x.BlockStartDate >= criteria.FromDate)
             .WhereIf(criteria.BlockStatusId == (int)IsActive.Active, x => (x.BlockEndDate >= DateTime.Now && x.BlockStartDate <= DateTime.Now))
             .WhereIf(criteria.IsDeletedId != 0, r => r.BlockStatusId == criteria.IsDeletedId)
             .OrderBy(x => x.SupplierBlockId)
             .SortBy(criteria.Sort, criteria.SortDirection).Select(r => new SupplierBlockModel
             {
                 AdminBlockReason = r.AdminBlockReason,
                 AdminFileName = r.AdminFileName,
                 AdminFileNetReferenceId = r.AdminFileNetReferenceId,
                 AgencyCode = r.AgencyCode,
                 BlockEndDate = r.BlockEndDate,
                 BlockStartDate = r.BlockStartDate,
                 BlockStatusId = r.BlockStatusId,
                 CommercialRegistrationNo = r.CommercialRegistrationNo,
                 CommercialSupplierName = r.CommercialSupplierName,
                 Punishment = r.Punishment,
                 BlockDuration = (r.BlockEndDate - r.BlockStartDate).ToString(),
                 ResolutionNumber = r.ResolutionNumber,
                 SecretaryBlockReason = r.SecretaryBlockReason,
                 SecretaryFileName = r.SecretaryFileName,
                 SecretaryFileNetReferenceId = r.SecretaryFileNetReferenceId,
                 BlockStatusName = r.BlockStatus.BlockStatusNameAr,
                 SupplierBlockIdString = Util.Encrypt(r.SupplierBlockId),
                 BlockDetails = r.BlockDetails,
                 ManagerRejectionReason = r.ManagerRejectionReason,
                 SecretaryRejectionReason = r.SecretaryRejectionReason,
                 SupplierTypeId = r.SupplierTypeId,
                 OrganizationTypeId = r.OrganizationTypeId,
                 IsOldSystem = r.IsOldBlock,
                 FromDateString = r.BlockStartDate != null ? Util.GetGregorianDateWithFormat(r.BlockStartDate, "dd/MM/yyyy") : null,
                 ToDateString = r.BlockEndDate != null ? Util.GetGregorianDateWithFormat(r.BlockEndDate, "dd/MM/yyyy") : null,
             })
             .ToQueryResult(criteria.PageNumber, criteria.PageSize);
            return result;
        }

        private static object RetturnOrganizationName(int organizationTypeId)
        {
            if (organizationTypeId == 1)
            {
                return Resources.BlockResources.DisplayInputs.ForeignCustomer;
            }
            return organizationTypeId == 2 ? Resources.BlockResources.DisplayInputs.ProfessionLicense : Resources.BlockResources.DisplayInputs.HasCR;
        }

    }
}