using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
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
    public class MandatoryListQueries : IMandatoryListQueries
    {
        private readonly IAppDbContext _dbContext;
        private readonly IMapper _mapper;

        public MandatoryListQueries(IAppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<MandatoryListViewModel> ProjectedFind(int id)
        {
            return await _dbContext.MandatoryLists
                .Where(a => a.Id == id)
                .ProjectTo<MandatoryListViewModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }

        public async Task<MandatoryList> Find(int id)
        {
            return await _dbContext.MandatoryLists
                .Include(a => a.Products)
                .Include(a => a.ChangeRequests)
                .Include("ChangeRequests.Products")
                .FirstOrDefaultAsync(a => a.Id == id && a.IsActive == true);
        }

        public Task<QueryResult<MandatoryListIndexViewModel>> Search(MandatoryListSearchViewModel criteria)
        {
            return _dbContext.MandatoryLists
                .Where(a => a.IsActive == true)
                .WhereIf(!String.IsNullOrEmpty(criteria.DivisionCode), a => a.DivisionCode == criteria.DivisionCode)
                .WhereIf(!String.IsNullOrEmpty(criteria.DivisionName), a => a.DivisionNameAr.Contains(criteria.DivisionName))
                .WhereIf(criteria.StatusId.HasValue, a => a.StatusId == criteria.StatusId)
                .WhereIf(!String.IsNullOrEmpty(criteria.CSICode), a => a.Products.Any(a => a.CSICode.Contains(criteria.CSICode) && a.IsActive == true))
                .WhereIf(!String.IsNullOrEmpty(criteria.ProductNameAr), a => a.Products.Any(a => a.NameAr.Contains(criteria.ProductNameAr) && a.IsActive == true))
                .WhereIf(criteria.PriceCelling.HasValue, a => a.Products.Any(a => a.PriceCelling == criteria.PriceCelling.Value && a.IsActive == true))
                .OrderBy(x => x.Id)
                .SortBy(criteria.Sort, criteria.SortDirection)
                .ProjectTo<MandatoryListIndexViewModel>(_mapper.ConfigurationProvider)
                .ToQueryResult(criteria.PageNumber, criteria.PageSize);
        }

        public async Task<MandatoryListDetailsViewModel> GetMandatoryListDetails(int mandatoryListId)
        {
            MandatoryListDetailsViewModel detailsViewModel = await _dbContext.MandatoryLists.Where(m => m.Id == mandatoryListId && m.IsActive == true)
                .Select(m => new MandatoryListDetailsViewModel
                {
                    Id = m.Id,
                    DivisionCode = m.DivisionCode,
                    DivisionNameEn = m.DivisionNameEn,
                    DivisionNameAr = m.DivisionNameAr,

                    Products = m.Products.Where(a => a.IsActive == true).Select(p => new MandatoryListProductViewModel
                    {
                        Id = Util.Encrypt(p.Id),
                        CSICode = p.CSICode,
                        DescriptionAr = p.DescriptionAr,
                        DescriptionEn = p.DescriptionEn,
                        PriceCelling = p.PriceCelling,
                        NameAr = p.NameAr,
                        NameEn = p.NameEn
                    }).ToList()
                }).FirstOrDefaultAsync();

            return detailsViewModel;
        }
        public async Task<MandatoryListApprovalViewModel> GetMandatoryListDetailsForApproval(int mandatoryListId)
        {
            MandatoryListApprovalViewModel detailsViewModel = await _dbContext.MandatoryLists.Where(m => m.Id == mandatoryListId && m.IsActive == true)
                .Select(m => new MandatoryListApprovalViewModel
                {
                    DivisionCode = m.DivisionCode,
                    DivisionNameEn = m.DivisionNameEn,
                    DivisionNameAr = m.DivisionNameAr,
                    StatusId = m.StatusId,
                    EncryptedId = Util.Encrypt(m.Id),
                    RejectionReason = m.RejectionReason,
                    Products = m.Products.Select(a => new MandatoryListProductViewModel()
                    {
                        Id = Util.Encrypt(a.Id),
                        CSICode = a.CSICode,
                        DescriptionAr = a.DescriptionAr,
                        DescriptionEn = a.DescriptionEn,
                        NameAr = a.NameAr,
                        NameEn = a.NameEn,
                        PriceCelling = a.PriceCelling
                    }).ToList()
                }).FirstOrDefaultAsync();

            return detailsViewModel;
        }

        public async Task<List<MandatoryListForQuantityTableViewModel>> GetAllForQuantityTable()
        {
            var mdl = await _dbContext.MandatoryListProducts.Where(d => d.IsActive == true && (d.MandatoryList.StatusId == (int)Enums.MandatoryListStatus.Approved || d.MandatoryList.StatusId == (int)Enums.MandatoryListStatus.WaitingCancelApproval) && d.MandatoryList.IsActive == true).Select(a => new MandatoryListForQuantityTableViewModel()
            {
                Id = a.Id,
                ProductName = a.NameAr,
                CSICode = a.CSICode
            }).ToListAsync();

            return mdl;
        }
        public async Task<CSICodeDetailsModel> GetMandatoryListCSICodeInfo(string code)
        {
            var mdl = await _dbContext.MandatoryListProducts.Where(d => d.IsActive == true && d.CSICode == code).Select(a => new CSICodeDetailsModel()
            {
                CSIName = a.NameAr,
                Price = a.PriceCelling,
                DivisionCode = a.MandatoryList.DivisionCode,
                DivisionName = a.MandatoryList.DivisionNameAr
            }).FirstOrDefaultAsync();
            return mdl;
        }

        public async Task<List<string>> GetValidMandatoryListCodeForQauntityTableExcel(List<string> CSICodes)
        {
            var result = await _dbContext.MandatoryListProducts
                .Where(d => d.IsActive == true && d.MandatoryList.StatusId == (int)Enums.MandatoryListStatus.Approved && d.MandatoryList.IsActive == true/* && d.MandatoryList.IsValid == true*/)
                .Where(d => CSICodes.Contains(d.CSICode))
                .Select(d => d.CSICode).Distinct()
                .ToListAsync();
            return result;
        }


    }
}
