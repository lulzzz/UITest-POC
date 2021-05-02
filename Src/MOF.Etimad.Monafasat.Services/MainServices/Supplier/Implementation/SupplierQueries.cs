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
    public class SupplierQueries : ISupplierQueries
    {
        private readonly IAppDbContext _context;
        public SupplierQueries(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<FavouriteSupplierList> FindFavouriteList(int id)
        {
            var FavouriteList = _context.FavouriteSupplierLists.FirstOrDefault(s =>
                s.FavouriteSupplierListId == id);
            return FavouriteList;
        }



        public async Task<QueryResult<SupplierCombinedModelView>> GetAllSuppliersBySearchCretriaForCombinedQuery(SupplierSearchCretriaModel cretria, string CR)
        {
            var combinedCRs = _context.OfferSolidarities.Where(w => w.Offer.Tender.TenderId == cretria.InvitationTenderId && (w.Offer.OfferStatusId != (int)Enums.OfferStatus.Established && w.Offer.OfferStatusId != (int)Enums.OfferStatus.UnderEstablishing)).Select(a => (a).CRNumber).ToList();
            var suppliers = await _context.Suppliers.Include(b => b.SupplierBlocks)
                .Where(x => !x.SupplierBlocks.Any(s => s.CommercialRegistrationNo == cretria.CommericalRegisterNo
                && ((s.BlockStatusId == (int)Enums.BlockStatus.ApprovedManager
                && s.BlockStartDate.Value.Date <= DateTime.Now.Date && s.BlockEndDate.Value.Date >= DateTime.Now.Date))
                              ))
                .WhereIf(!string.IsNullOrEmpty(cretria.CommericalRegisterNo), x => x.SelectedCr == cretria.CommericalRegisterNo)
                .WhereIf(!string.IsNullOrEmpty(cretria.CommericalRegisterName), x => x.SelectedCrName == cretria.CommericalRegisterName)
                  .Where(w => w.SelectedCr != CR &&
                  !combinedCRs.Any(a => a == w.SelectedCr))
                  .Select(x => new SupplierCombinedModelView
                  {
                      CommericalRegisterNo = x.SelectedCr,
                      CommericalRegisterName = x.SelectedCrName,
                      SupplierName = x.SelectedCrName
                  }).ToQueryResult(cretria.PageNumber, cretria.PageSize);
            return suppliers;
        }


        public async Task<QueryResult<SupplierCombinedModelView>> FindSuppliers(CombinedSearchCretriaModel cretria)
        {
            return await _context.OfferSolidarities
                .Where(x => x.OfferId == cretria.OfferId && x.IsActive == true)
                .Include("Supplier")

                .Select(x => new SupplierCombinedModelView
                {
                    CommericalRegisterNo = x.CRNumber,
                    CommericalRegisterName = x.Supplier.SelectedCrName,
                    Email = x.Email,
                    MobileNumber = x.Mobile,

                }).ToQueryResult(cretria.PageNumber, cretria.PageSize);
        }
        public async Task<FavouriteSupplierList> FindFavouriteListByName(string Name, int BranchId = 0)
        {
            var FavouriteList = _context.FavouriteSupplierLists.WhereIf(BranchId > 0, s => s.BranchId == BranchId && s.IsActive == true).FirstOrDefault(s =>
                  s.Name == Name);
            return FavouriteList;
        }
        public async Task<Tender> GetTenderByTenderId(int tenderId)
        {
            var tender = await _context.Tenders.Where(s => s.IsActive == true && s.TenderId == tenderId).FirstOrDefaultAsync();
            return tender;
        }



        public async Task<FavouriteSupplier> FindFavouriteSupplierByListId(SupplierSearchCretriaModel cretria)
        {
            var FavouriteSupplier = _context.FavouriteSuppliers.Includes(f => f.FavouriteSupplierList).Where(s => s.FavouriteSupplierListId == cretria.FavouriteSupplierListId &&
            s.SupplierCRNumber == cretria.CommericalRegisterNo).FirstOrDefault();
            return FavouriteSupplier;
        }

        public async Task<List<FavouriteSupplierListModel>> GetFavouriteListsByAgencyBranchId(string agencyCode, int BranchId)
        {
            var FavouriteSupplierLists = _context.FavouriteSupplierLists
                .Where(f => f.IsActive == true &&
                f.AgencyCode == agencyCode &&
                f.BranchId == BranchId)
               .Select(x => new FavouriteSupplierListModel
               {
                   FavouriteSupplierListId = x.FavouriteSupplierListId,
                   Name = x.Name
               }).ToList();

            return FavouriteSupplierLists;
        }

        public async Task<QueryResult<SupplierInvitationModel>> GetFavouriteSuppliersByListId(SupplierSearchCretriaModel cretria)
        {
            cretria.FavouriteSupplierListId = cretria.FavouriteSupplierListId == null ? 0 : cretria.FavouriteSupplierListId;
            var suppliers = await _context.FavouriteSuppliers.Where(f => (f.IsActive == true || cretria.OnlyActive == false) &&
                f.FavouriteSupplierList.IsActive == true && f.FavouriteSupplierList.AgencyCode == cretria.AgencyCode)
                .WhereIf(cretria.FavouriteSupplierListId != 0, x => x.FavouriteSupplierListId == cretria.FavouriteSupplierListId)
               .Select(x => new SupplierInvitationModel
               {
                   CommericalRegisterNo = x.Supplier.SelectedCr,
                   CommericalRegisterName = x.Supplier.SelectedCrName,
                   FavouriteSupplierListId = x.FavouriteSupplierListId,
                   FavouriteName = x.FavouriteSupplierList.Name

               }).ToQueryResult(cretria.PageNumber, cretria.PageSize);

            return suppliers;
        }

        public async Task<Supplier> FindSupplierByCRNumber(string selectedCr)
        {
            return _context.Suppliers.Include(r => r.NotificationSetting).Where(s => s.SelectedCr == selectedCr && s.IsActive == true).FirstOrDefault();
        }
        public async Task<List<SupplierModel>> GetAllSuppliersData()
        {
            return await _context.Suppliers.Include(s => s.Invitations).Include(b => b.SupplierBlocks)
                    .Where(x => !x.Invitations.Where(i => i.IsActive == true).Select(m => m.CommericalRegisterNo).Contains(x.SelectedCr) && !x.SupplierBlocks.Select(m => m.CommercialRegistrationNo).Contains(x.SelectedCr))
                    .Select(x => new SupplierModel
                    {
                        CommericalRegisterNo = x.SelectedCr,
                        CommericalRegisterName = x.SelectedCrName,
                    }).ToListAsync();
        }

        public async Task<QueryResult<SolidarityInvitedRegisteredSupplierModel>> GetSolidarityInvitedSuppliers(SolidaritySearchCriteria cretria)
        {
            return await _context.OfferSolidarities.Include(s => s.Offer).ThenInclude(d => d.Supplier)
                .Where(x => x.OfferId == Util.Decrypt(cretria.offerIdString) &&
                 x.IsActive == true
                                  && x.SolidarityTypeId == (int)Enums.UnRegisteredSuppliersInvitationType.Existed)
                             .SortBy(cretria.Sort, cretria.SortDirection).Select(i => new SolidarityInvitedRegisteredSupplierModel()
                             {
                                 SolidarityIdString = Util.Encrypt(i.Id),
                                 CrName = ((OfferSolidarity)i).Supplier.SelectedCrName,
                                 CrNumber = ((OfferSolidarity)i).CRNumber,
                                 TenderId = i.Offer.TenderId,
                                 StatusId = i.StatusId,
                                 StatusName = i.SolidarityStatus.Name,
                                 OfferIdString = Util.Encrypt(i.OfferId)
                             }).ToQueryResult(cretria.PageNumber, cretria.PageSize);
        }
        public async Task<QueryResult<SolidarityInvitedUnRegisteredSupplierModel>> GetSolidarityInvitedUnregisteredSuppliers(SolidarityUnregisteredSearchCriteria cretria)
        {
            return await _context.OfferSolidarities.Include(s => s.Offer).ThenInclude(d => d.Supplier)
                .Where(x => x.OfferId == Util.Decrypt(cretria.offerIdString) &&
                 x.IsActive == true && x.SolidarityTypeId != (int)Enums.UnRegisteredSuppliersInvitationType.SolidarityLeader && x.SolidarityTypeId != (int)Enums.UnRegisteredSuppliersInvitationType.Existed)
                             .Select(i => new SolidarityInvitedUnRegisteredSupplierModel()
                             {
                                 SolidarityIdString = Util.Encrypt(i.Id),
                                 Mobile = i.Mobile,
                                 Email = i.Email,
                                 CR = i.CRNumber,
                                 CrName = i.Supplier.SelectedCrName,
                                 StatusId = i.StatusId,
                                 StatusName = i.SolidarityStatus.Name,
                                 OfferIdString = Util.Encrypt(i.OfferId)
                             }).ToQueryResult(cretria.PageNumber, cretria.PageSize);
        }
        public async Task<QueryResult<InvitationCrModel>> GetInvitedSuppliers(SupplierSearchCretriaModel cretria)
        {
            return await _context.Invitations.Include(s => s.Supplier)
                .Where(x => x.TenderId == cretria.InvitationTenderId &&
                 x.InvitationTypeId == (int)Enums.InvitationRequestType.Invitation &&
                 x.IsActive == true && x.SupplierType == (int)Enums.InvititedSupplierTypes.HasCR &&
                 (x.StatusId == (int)Enums.InvitationStatus.New || x.StatusId == (int)Enums.InvitationStatus.Approved || x.StatusId == (int)Enums.InvitationStatus.WaitingPayment))
                             .Select(i => new InvitationCrModel()
                             {
                                 CrName = i.Supplier.SelectedCrName,
                                 CrNumber = i.CommericalRegisterNo,
                                 TenderId = i.TenderId,
                                 StatusId = i.StatusId
                             }).ToQueryResult(cretria.PageNumber, cretria.PageSize);
        }

        public async Task<QueryResult<InvitationCrModel>> GetInvitedUnRegisterSuppliers(SupplierSearchCretriaModel cretria)
        {
            return await _context.UnRegisteredSuppliersInvitation
                .Where(x => x.TenderId == cretria.InvitationTenderId &&
                                  x.IsActive == true &&
                 (x.InvitationStatusId == (int)Enums.InvitationStatus.New || x.InvitationStatusId == (int)Enums.InvitationStatus.Approved || x.InvitationStatusId == (int)Enums.InvitationStatus.WaitingPayment))
                             .Select(i => new InvitationCrModel()
                             {
                                 CrNumber = i.CrNumber,
                                 TenderId = i.TenderId,
                                 StatusId = i.InvitationStatusId.Value,
                                 Mobile = i.MobileNo,
                                 Email = i.Email
                             }).ToQueryResult(cretria.PageNumber, cretria.PageSize);
        }


        public async Task<QueryResult<InvitationCrModel>> GetInvitedUnRegisterSuppliersForCreation(SupplierSearchCretriaModel cretria)
        {
            return await _context.UnRegisteredSuppliersInvitation
                .Where(x => x.TenderId == cretria.InvitationTenderId &&
                                  x.IsActive == true &&
                 (x.InvitationStatusId == (int)Enums.InvitationStatus.ToBeSent))
                             .Select(i => new InvitationCrModel()
                             {
                                 CrNumber = i.CrNumber,
                                 TenderId = i.TenderId,
                                 StatusId = i.InvitationStatusId.Value,
                                 Mobile = i.MobileNo,
                                 Email = i.Email
                             }).ToQueryResult(cretria.PageNumber, cretria.PageSize);
        }


        #region Invitation 

        public async Task<QueryResult<InvitationCrModel>> GetInvitedSuppliersForInvitationInTenderCreation(SupplierSearchCretriaModel cretria)
        {
            var suppliers = await _context.Invitations.Where(x => x.TenderId == cretria.InvitationTenderId && x.InvitationTypeId == (int)Enums.InvitationRequestType.Invitation &&
                 x.IsActive == true && x.SupplierType == (int)Enums.InvititedSupplierTypes.HasCR
                   /*&& x.StatusId == (int)Enums.InvitationStatus.ToBeSent*/)
                .Select(x => new InvitationCrModel
                {
                    CrNumber = x.CommericalRegisterNo,
                    CrName = x.Supplier.SelectedCrName,
                    StatusId = x.StatusId,
                    TenderId = cretria.InvitationTenderId
                }).ToQueryResult(cretria.PageNumber, cretria.PageSize);

            return suppliers;
        }
        public async Task<QueryResult<InvitationCrModel>> GetQualifiedSuppliers(TenderIdSearchCretriaModel cretria)
        {


            var suppliers = await _context.QualificationFinalResult.Include(c => c.Tender).ThenInclude(h => h.Invitations).Include(s => s.Supplier)
                .Where(x => x.TenderId == cretria.TenderId &&
                 x.QualificationLookupId == (int)Enums.QualificationResultLookup.Succeeded &&
                 x.IsActive == true)
                .Select(x => new InvitationCrModel
                {
                    CrNumber = x.SupplierSelectedCr,
                    CrName = x.Supplier.SelectedCrName,
                    StatusId = x.Tender.PreQualification.Invitations.Count > 0 ? (int)Enums.InvitationStatus.ToBeSent : 0,
                    TenderId = cretria.TenderId,
                }).ToQueryResult(cretria.PageNumber, cretria.PageSize);




            return suppliers;
        }
        public async Task<QueryResult<InvitationCrModel>> GetAcceptedSupplierInFirstStageTender(TenderIdSearchCretriaModel cretria)
        {
            var suppliers = await _context.Offers.Include(c => c.Tender).ThenInclude(h => h.Invitations).Include(s => s.Supplier)
                .Where(x => x.TenderId == cretria.TenderId &&
                 x.OfferTechnicalEvaluationStatusId == (int)Enums.OfferTechnicalEvaluationStatusId.IdenticalOffer &&
                 x.IsActive == true)
                .Select(x => new InvitationCrModel
                {
                    CrNumber = x.CommericalRegisterNo,
                    CrName = x.Supplier.SelectedCrName,
                    StatusId = x.Tender.Invitations.Count > 0 ? (int)Enums.InvitationStatus.ToBeSent : 0,
                    TenderId = cretria.TenderId
                }).ToQueryResult(cretria.PageNumber, cretria.PageSize);

            return suppliers;
        }
        public async Task<QueryResult<InvitationCrModel>> GetAnnouncementTemplateSuppliers(AnnouncementSupplierTemplateInvitationSearchmodel cretria)
        {
            var suppliers = await _context.AnnouncementRequestSupplierTemplate
                .Where(x => x.AnnouncementId == cretria.AnnouncementTemplateId && x.StatusId == (int)Enums.AnnouncementTemplateJoinRequestStatus.Accepted &&
                 x.IsActive == true)
                .Select(x => new InvitationCrModel
                {
                    CrNumber = x.Cr,
                    CrName = x.Supplier.SelectedCrName,
                    TenderId = cretria.TenderId
                }).ToQueryResult(cretria.PageNumber, cretria.PageSize);

            return suppliers;
        }
        public async Task<QueryResult<string>> GetEmailsForUnregisteredSuppliers(SupplierSearchCretriaModel cretria)
        {
            var suppliers = await _context.UnRegisteredSuppliersInvitation.Where(x => x.IsActive == true && x.TenderId == cretria.InvitationTenderId && x.Email != null)
                .Select(x => x.Email).ToQueryResult(1, 100);
            return suppliers;
        }
        public async Task<QueryResult<string>> GetMobileForUnregisteredSuppliers(SupplierSearchCretriaModel cretria)
        {
            var suppliers = await _context.UnRegisteredSuppliersInvitation.Where(x => x.IsActive == true && x.TenderId == cretria.InvitationTenderId && x.MobileNo != null)
                .Select(x => x.MobileNo).ToQueryResult(1, 100);
            return suppliers;
        }
        public async Task<QueryResult<SupplierInvitationModel>> GetAllSuppliersBySearchCretriaForInvitations(SupplierSearchCretriaModel cretria)
        {
            var suppliers = await _context.Suppliers.Include(s => s.Invitations).Include(b => b.SupplierBlocks)
                .Where(x => !x.SupplierBlocks.Any(s => s.CommercialRegistrationNo == cretria.CommericalRegisterNo && ((/*s.BlockStatusId == (int)Enums.BlockStatus.ApprovedManager &&*/ s.BlockStartDate.Value.Date <= DateTime.Now.Date && s.BlockEndDate.Value.Date >= DateTime.Now.Date))
                && (s.AgencyCode == null || s.AgencyCode == cretria.AgencyCode)))
                .WhereIf(!string.IsNullOrEmpty(cretria.CommericalRegisterNo), x => x.SelectedCr == cretria.CommericalRegisterNo)
                .WhereIf(!string.IsNullOrEmpty(cretria.CommericalRegisterName), x => x.SelectedCrName == cretria.CommericalRegisterName)
                .Select(x => new SupplierInvitationModel
                {
                    CommericalRegisterNo = x.SelectedCr,
                    CommericalRegisterName = x.SelectedCrName,
                    StatusId = 0,
                    TenderId = cretria.InvitationTenderId
                }).ToQueryResult(cretria.PageNumber, cretria.PageSize);

            return suppliers;
        }

        #endregion

        public async Task<QueryResult<UnRegisterSupplierInvitationModel>> GetInvitedUnregisteredSuppliersForInvitationAfterTenderApprovement(SupplierSearchCretriaModel cretria)
        {
            QueryResult<UnRegisterSupplierInvitationModel> suppliers = await _context.UnRegisteredSuppliersInvitation
                .Where(x => x.TenderId == Util.Decrypt(cretria.InvitationTenderIdString)
                && x.IsActive == true && (x.InvitationStatusId != null && (x.InvitationStatusId != (int)Enums.InvitationStatus.Withdrawal && x.InvitationStatusId != (int)Enums.InvitationStatus.Rejected)))
                .Select(y => new UnRegisterSupplierInvitationModel
                {
                    SupplierEmailOrMobileNo = y.Email != null ? y.Email : y.MobileNo,
                    InvitationStatusName = y.InvitationStatus.NameAr,

                }).ToQueryResult(cretria.PageNumber, cretria.PageSize);
            return suppliers;
        }
        public async Task<QueryResult<SupplierInvitationModel>> GetInvitedSuppliersForInvitationAfterTenderApprovement(SupplierSearchCretriaModel cretria)
        {
            QueryResult<SupplierInvitationModel> suppliers = await _context.Invitations.Include(s => s.Supplier)
                .Where(x => x.IsActive == true && x.TenderId == Util.Decrypt(cretria.InvitationTenderIdString)
                && x.InvitationTypeId == (int)Enums.InvitationRequestType.Invitation
                && x.IsActive == true && x.SupplierType == (int)Enums.InvititedSupplierTypes.HasCR
                && (x.StatusId != (int)Enums.InvitationStatus.Withdrawal && x.StatusId != (int)Enums.InvitationStatus.Rejected))
                .Select(x => new SupplierInvitationModel
                {
                    CommericalRegisterNo = x.CommericalRegisterNo,
                    CommericalRegisterName = x.Supplier.SelectedCrName,
                    StatusId = x.StatusId,
                    TenderId = cretria.InvitationTenderId
                }).ToQueryResult(cretria.PageNumber, cretria.PageSize);
            return suppliers;
        }

        public async Task SuppliersInFavourite(SupplierSearchCretriaModel cretria, List<SupplierInvitationModel> supplierInvitationModels)
        {

            var supplierLst = supplierInvitationModels.AsQueryable<SupplierInvitationModel>().Select(x => x.CommericalRegisterNo).ToArray();
            var suppliersInFav = await _context.FavouriteSuppliers
                            .Where(f => f.IsActive == true && f.FavouriteSupplierList.IsActive == true && f.FavouriteSupplierList.AgencyCode == cretria.AgencyCode)
                            .Where(x => supplierLst.Any(y => y == x.SupplierCRNumber))
                            .ToListAsync();
            foreach (var item in supplierInvitationModels)
            {
                if (Array.Exists(suppliersInFav.ToArray(), element => element.SupplierCRNumber == item.CommericalRegisterNo))
                {
                    item.IsFav = true;
                }
            }
        }
        public async Task GetInvitatedSupplier(SupplierSearchCretriaModel cretria, List<InvitationCrModel> supplierInvitationModels)
        {
            var supplierInvitationLst = supplierInvitationModels.AsQueryable<InvitationCrModel>().Select(x => x.CrNumber).ToArray();
            var invitationSupplierList = (await _context.Invitations
            .Where(x => x.InvitationTypeId == (int)Enums.InvitationRequestType.Invitation && x.IsActive == true &&
             (x.StatusId == (int)Enums.InvitationStatus.New || x.StatusId == (int)Enums.InvitationStatus.Approved))
             .Where(x => supplierInvitationLst.Any(y => y == x.CommericalRegisterNo)).ToListAsync());
            foreach (var item in supplierInvitationModels)
            {
                if (Array.Exists(invitationSupplierList.ToArray(), element => element.CommericalRegisterNo == item.CrNumber))
                {
                    item.invitationCount = invitationSupplierList.Where(x => x.CommericalRegisterNo == item.CrNumber).Count();
                }
            }
        }
    }
}
