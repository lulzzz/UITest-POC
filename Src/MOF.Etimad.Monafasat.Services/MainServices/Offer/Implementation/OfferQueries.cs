using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Core.Entities.Negotiations;
using MOF.Etimad.Monafasat.Core.SearchCriterias;
using MOF.Etimad.Monafasat.Data;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.ViewModel.Invitation;
using MOF.Etimad.Monafasat.ViewModel.Negotiation;
using MOF.Etimad.Monafasat.ViewModel.Offer;
using MOF.Etimad.Monafasat.ViewModel.Offer.OpenOfferStage;
using MOF.Etimad.Monafasat.ViewModel.Reports;
using MOF.Etimad.SharedKernel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using static MOF.Etimad.Monafasat.SharedKernel.Enums;

namespace MOF.Etimad.Monafasat.Services
{
    public class OfferQueries : IOfferQueries
    {
        private readonly IAppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly RootConfigurations _rootConfiguration;
        private readonly ILocalContentConfigurationSettings _localContentConfigurationSettings;
        public OfferQueries(IAppDbContext context, IHttpContextAccessor httpContextAccessor, IOptionsSnapshot<RootConfigurations> rootConfiguration, ILocalContentConfigurationSettings localContentConfigurationSettings)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _rootConfiguration = rootConfiguration.Value;
            _localContentConfigurationSettings = localContentConfigurationSettings;
        }

        public async Task<QueryResult<Offer>> FindOffers(OfferSearchCriteria criteria)
        {
            Check.ArgumentNotNull(nameof(criteria), criteria);
            criteria.IsActive = criteria.IsActive == null ? true : criteria.IsActive;
            var result = await _context.Offers
                .Where(x => x.IsActive == criteria.IsActive && x.TenderId == criteria.TenderId)
                .OrderBy(x => x.OfferId)
                .SortBy(criteria.Sort, criteria.SortDirection)
                .ToQueryResult(criteria.PageNumber, criteria.PageSize);

            return result;
        }

        public async Task<Offer> GetOfferWithQuantitiesTablesByOfferId(int offerId)
        {
            var result = await _context.Offers.Include(sq => sq.SupplierTenderQuantityTables)
                                              .ThenInclude(si => si.QuantitiyItemsJson)
                                              .Include(si => si.Tender)
                                              .Where(t => t.OfferId == offerId && t.IsActive == true).FirstOrDefaultAsync();
            return result;
        }

        public async Task<QueryResult<AppliedSuppliersReportModel>> FindSuppliersReportByTenderId(int tenderId, int branchId, string AgencyCode, int committeeId, Enums.CommitteeType committeeType)
        {
            var allowedRoles = new List<string> { RoleNames.UnitManagerUser, RoleNames.UnitSpecialistLevel1, RoleNames.UnitSpecialistLevel2 };
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            var result = await _context.Tenders
                                .Where(t => t.IsActive == true && t.TenderId == tenderId)
                                .WhereIf(branchId != 0, t => t.BranchId == branchId)
                                .WhereIf(string.IsNullOrEmpty(AgencyCode) && !allowedRoles.Contains(_httpContextAccessor.HttpContext.User.UserRole()), t => t.AgencyCode == AgencyCode)
                                .Select(t => new AppliedSuppliersReportModel
                                {
                                    TenderName = t.TenderName,
                                    BranchId = branchId,
                                    TenderNumber = t.TenderNumber,
                                    TenderReferenceNumber = t.ReferenceNumber,
                                    TenderTypeName = t.TenderType.NameAr,
                                    TenderStatusName = t.Status.NameAr,
                                    // InvitationsCount = tender.Invitations.Where(x => x.IsActive == true && x.StatusId == (int)InvitationStatus.Approved).Count(),
                                    InvitationsCount = (t.Invitations.Where(x => x.IsActive == true).Count() > 0 ?
                     t.Invitations.Where(x => x.IsActive == true && x.StatusId == (int)Enums.InvitationStatus.Approved).Count() : 0),

                                    // OffersCount = tender.Offers.Where(x => x.IsActive == true && (x.OfferStatusId == (int)OfferStatus.Received || x.OfferStatusId == (int)OfferStatus.Excluded)).Count(),
                                    OffersCount = t.Offers.Where(x => x.IsActive == true && (x.OfferStatusId == (int)Enums.OfferStatus.Received || x.OfferStatusId == (int)Enums.OfferStatus.Excluded)).Count(),

                                    BuyersCount = t.ConditionsBooklets.Where(x => x.IsActive == true).Count() > 0 ?
                    (t.ConditionsBooklets.Where(x => x.IsActive == true && x.BillInfo.BillStatusId == (int)Enums.BillStatus.Paid).Count()) : 0,

                                    SuppliersDetails = t.Offers.Where(x => x.IsActive == true && (x.OfferStatusId == (int)OfferStatus.Received || x.OfferStatusId == (int)OfferStatus.Excluded)).Select(offer => new AppliedSuppliersReportDetailsModel
                                    {
                                        OfferId = offer.OfferId,
                                        offerIdString = Util.Encrypt(offer.OfferId),
                                        OfferStatusId = offer.OfferStatusId,
                                        CanSeeDetails = CanEmployeeSeeOpeningDetails(t.TenderStatusId),
                                        CommericalRegisterNo = offer.CommericalRegisterNo,
                                        CompanyName = offer.Supplier.SelectedCrName,
                                        InvitationStatusName = offer.Tender.Invitations.FirstOrDefault(x => x.IsActive == true && x.CommericalRegisterNo == offer.CommericalRegisterNo && x.StatusId == (int)Enums.InvitationStatus.Approved) != null ? offer.Tender.Invitations.FirstOrDefault(x => x.IsActive == true && x.CommericalRegisterNo == offer.CommericalRegisterNo && x.StatusId == (int)Enums.InvitationStatus.Approved).InvitationStatus.NameAr : Resources.SharedResources.DisplayInputs.NotExist,
                                        PurchaseStatusName = offer.Tender.ConditionsBooklets.Count > 0 ? (offer.Tender.ConditionsBooklets.FirstOrDefault(x => x.IsActive == true && x.CommericalRegisterNo == offer.CommericalRegisterNo).BillInfo.BillStatusId == (int)BillStatus.Paid ? Resources.TenderResources.DisplayInputs.Purchased : Resources.TenderResources.DisplayInputs.NotPurchased) : Resources.SharedResources.DisplayInputs.NotExist,
                                        InvitationSendingDate = offer.Tender.Invitations.FirstOrDefault(x => x.IsActive == true && x.CommericalRegisterNo == offer.CommericalRegisterNo && x.StatusId == (int)Enums.InvitationStatus.Approved).SendingDate.Value,
                                        PurchaseDate = offer.Tender.ConditionsBooklets.Count > 0 ? (offer.Tender.ConditionsBooklets.FirstOrDefault(x => x.IsActive == true && x.CommericalRegisterNo == offer.CommericalRegisterNo).BillInfo.PurchaseDate.HasValue ? offer.Tender.ConditionsBooklets.FirstOrDefault(x => x.IsActive == true && x.CommericalRegisterNo == offer.CommericalRegisterNo).BillInfo.PurchaseDate : null) : null,
                                        InvitationAcceptanceDate = offer.Tender.Invitations.FirstOrDefault(x => x.IsActive == true && x.CommericalRegisterNo == offer.CommericalRegisterNo && x.StatusId == (int)Enums.InvitationStatus.Approved).SubmissionDate.HasValue ? offer.Tender.Invitations.FirstOrDefault(x => x.IsActive == true && x.CommericalRegisterNo == offer.CommericalRegisterNo).SubmissionDate.Value : (DateTime?)null,
                                        InvitationRejectionDate = offer.Tender.Invitations.FirstOrDefault(x => x.IsActive == true && x.CommericalRegisterNo == offer.CommericalRegisterNo && x.StatusId == (int)Enums.InvitationStatus.Rejected).SubmissionDate.HasValue ? offer.Tender.Invitations.FirstOrDefault(x => x.IsActive == true && x.CommericalRegisterNo == offer.CommericalRegisterNo).SubmissionDate.Value : (DateTime?)null,
                                        InvitationWithdrawalDate = offer.Tender.Invitations.Where(a => a.CommericalRegisterNo == offer.CommericalRegisterNo && a.IsActive == true).FirstOrDefault().WithdrawalDate.HasValue ? offer.Tender.Invitations.Where(a => a.CommericalRegisterNo == offer.CommericalRegisterNo && a.IsActive == true).FirstOrDefault().WithdrawalDate : (DateTime?)null,
                                        OfferWithdrawalDate = t.Offers.Where(o => offer.CommericalRegisterNo == o.CommericalRegisterNo && o.IsActive == true && o.OfferStatusId == (int)OfferStatus.Canceled).FirstOrDefault() == null ? null : t.Offers.Where(o => offer.CommericalRegisterNo == o.CommericalRegisterNo && o.OfferStatusId == (int)OfferStatus.Canceled).FirstOrDefault().WithrdrawlDate,
                                    }).ToList()
                                }).ToQueryResult(1, 50);
            return result;
        }

        public async Task<QueryResult<AppliedSuppliersReportDetailsModel>> FindSuppliersReportByTenderId__(int tenderId, int pageNumber)
        {
            // Get All Offers Regardless It's Status 
            var allowedRoles = new List<string> { RoleNames.UnitManagerUser, RoleNames.UnitSpecialistLevel1, RoleNames.UnitSpecialistLevel2 };
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            var result = await _context.Offers.Where(t => t.IsActive == true && t.TenderId == tenderId)
                                .Select(offer => new AppliedSuppliersReportDetailsModel
                                {
                                    OfferId = offer.OfferId,
                                    offerIdString = Util.Encrypt(offer.OfferId),
                                    OfferStatusId = offer.OfferStatusId,
                                    CanSeeDetails = CanEmployeeSeeOpeningDetails(offer.Tender.TenderStatusId),
                                    CommericalRegisterNo = offer.CommericalRegisterNo,
                                    CompanyName = offer.Supplier.SelectedCrName,
                                    InvitationStatusName = offer.Tender.Invitations.FirstOrDefault(x => x.IsActive == true && x.CommericalRegisterNo == offer.CommericalRegisterNo && x.StatusId == (int)Enums.InvitationStatus.Approved) != null ? offer.Tender.Invitations.FirstOrDefault(x => x.IsActive == true && x.CommericalRegisterNo == offer.CommericalRegisterNo && x.StatusId == (int)Enums.InvitationStatus.Approved).InvitationStatus.NameAr : Resources.SharedResources.DisplayInputs.NotExist,
                                    PurchaseStatusName = offer.Tender.ConditionsBooklets.Count > 0 ? (offer.Tender.ConditionsBooklets.FirstOrDefault(x => x.IsActive == true && x.CommericalRegisterNo == offer.CommericalRegisterNo).BillInfo.BillStatusId == (int)BillStatus.Paid ? Resources.TenderResources.DisplayInputs.Purchased : Resources.TenderResources.DisplayInputs.NotPurchased) : Resources.SharedResources.DisplayInputs.NotExist,
                                    InvitationSendingDate = offer.Tender.Invitations.FirstOrDefault(x => x.IsActive == true && x.CommericalRegisterNo == offer.CommericalRegisterNo && (x.StatusId == (int)Enums.InvitationStatus.Approved || x.StatusId == (int)Enums.InvitationStatus.Withdrawal)).SendingDate.HasValue ? offer.Tender.Invitations.FirstOrDefault(x => x.IsActive == true && x.CommericalRegisterNo == offer.CommericalRegisterNo).SendingDate.Value : (DateTime?)null,
                                    PurchaseDate = offer.Tender.ConditionsBooklets.Count > 0 ? (offer.Tender.ConditionsBooklets.FirstOrDefault(x => x.IsActive == true && x.CommericalRegisterNo == offer.CommericalRegisterNo).BillInfo.PurchaseDate.HasValue ? offer.Tender.ConditionsBooklets.FirstOrDefault(x => x.IsActive == true && x.CommericalRegisterNo == offer.CommericalRegisterNo).BillInfo.PurchaseDate : null) : null,
                                    InvitationAcceptanceDate = offer.Tender.Invitations.FirstOrDefault(x => x.IsActive == true && x.CommericalRegisterNo == offer.CommericalRegisterNo && x.StatusId == (int)Enums.InvitationStatus.Approved).SubmissionDate.HasValue ? offer.Tender.Invitations.FirstOrDefault(x => x.IsActive == true && x.CommericalRegisterNo == offer.CommericalRegisterNo).SubmissionDate.Value : (DateTime?)null,
                                    InvitationRejectionDate = offer.Tender.Invitations.FirstOrDefault(x => x.IsActive == true && x.CommericalRegisterNo == offer.CommericalRegisterNo && x.StatusId == (int)Enums.InvitationStatus.Rejected).SubmissionDate.HasValue ? offer.Tender.Invitations.FirstOrDefault(x => x.IsActive == true && x.CommericalRegisterNo == offer.CommericalRegisterNo).SubmissionDate.Value : (DateTime?)null,
                                    InvitationWithdrawalDate = offer.Tender.Invitations.Where(a => a.CommericalRegisterNo == offer.CommericalRegisterNo && a.IsActive == true).FirstOrDefault().WithdrawalDate.HasValue ? offer.Tender.Invitations.Where(a => a.CommericalRegisterNo == offer.CommericalRegisterNo && a.IsActive == true).FirstOrDefault().WithdrawalDate : (DateTime?)null,
                                    OfferWithdrawalDate = offer.WithrdrawlDate,
                                }).ToListAsync();

            // Get Only Paid Conditional Booklet On Tender And Not Applied an Offer
            var purchasedSuppliers = await _context.ConditionsBooklets.Where(x => x.IsActive == true && x.TenderId == tenderId && x.BillInfo != null && x.BillInfo.BillStatusId == (int)Enums.BillStatus.Paid
            && !x.Tender.Offers.Any(o => o.IsActive == true && o.CommericalRegisterNo == x.CommericalRegisterNo))
                .Select(booklet => new AppliedSuppliersReportDetailsModel
                {
                    OfferId = 0,
                    offerIdString = null,
                    OfferStatusId = 0,
                    CanSeeDetails = false,
                    CommericalRegisterNo = booklet.CommericalRegisterNo,
                    CompanyName = booklet.Supplier.SelectedCrName,
                    InvitationStatusName = booklet.Tender.Invitations.Any(x => x.IsActive == true && x.CommericalRegisterNo == booklet.CommericalRegisterNo && x.StatusId == (int)Enums.InvitationStatus.Approved) ? booklet.Tender.Invitations.FirstOrDefault(x => x.IsActive == true && x.CommericalRegisterNo == booklet.CommericalRegisterNo).InvitationStatus.NameAr : Resources.SharedResources.DisplayInputs.NotExist,
                    PurchaseStatusName = booklet.BillInfo.BillStatusId == (int)BillStatus.Paid ? Resources.TenderResources.DisplayInputs.Purchased : Resources.TenderResources.DisplayInputs.NotPurchased,
                    InvitationSendingDate = booklet.Tender.Invitations.FirstOrDefault(x => x.IsActive == true && x.CommericalRegisterNo == booklet.CommericalRegisterNo && x.StatusId == (int)Enums.InvitationStatus.Approved).SendingDate.HasValue ? booklet.Tender.Invitations.FirstOrDefault(x => x.IsActive == true && x.CommericalRegisterNo == booklet.CommericalRegisterNo).SendingDate.Value : (DateTime?)null,
                    PurchaseDate = booklet.BillInfo.PurchaseDate.HasValue ? booklet.BillInfo.PurchaseDate : null,
                    InvitationAcceptanceDate = booklet.Tender.Invitations.FirstOrDefault(x => x.IsActive == true && x.CommericalRegisterNo == booklet.CommericalRegisterNo && x.StatusId == (int)Enums.InvitationStatus.Approved).SubmissionDate.HasValue ? booklet.Tender.Invitations.FirstOrDefault(x => x.IsActive == true && x.CommericalRegisterNo == booklet.CommericalRegisterNo).SubmissionDate.Value : (DateTime?)null,
                    InvitationRejectionDate = null,
                    InvitationWithdrawalDate = null,
                    OfferWithdrawalDate = null,
                }).ToListAsync();


            // Get Approved, Rejected and Withdeawl Invitations On Tender And Did Not Apply An Offer And Did Not Buy The Booklet
            var invitedSuppliers = await _context.Invitations.Where(x => x.IsActive == true && (x.StatusId != (int)InvitationStatus.ToBeSent && x.StatusId != (int)InvitationStatus.New && x.StatusId != (int)InvitationStatus.WaitingPayment)
            && x.TenderId == tenderId
            && !x.Tender.Offers.Any(o => o.IsActive == true && o.CommericalRegisterNo == x.CommericalRegisterNo)
            && !x.Tender.ConditionsBooklets.Any(o => o.IsActive == true && o.CommericalRegisterNo == x.CommericalRegisterNo && o.BillInfo != null && o.BillInfo.BillStatusId == (int)Enums.BillStatus.Paid))
                .Select(invitation => new AppliedSuppliersReportDetailsModel
                {
                    OfferId = 0,
                    offerIdString = null,
                    OfferStatusId = 0,
                    CanSeeDetails = false,
                    CommericalRegisterNo = invitation.CommericalRegisterNo,
                    CompanyName = invitation.Supplier.SelectedCrName,
                    InvitationStatusName = invitation.InvitationStatus.NameAr,
                    PurchaseStatusName = Resources.SharedResources.DisplayInputs.NotExist,
                    InvitationSendingDate = invitation.SendingDate.Value,
                    PurchaseDate = null,
                    InvitationAcceptanceDate = invitation.StatusId != (int)Enums.InvitationStatus.Rejected ? invitation.SubmissionDate.Value : (DateTime?)null,
                    InvitationRejectionDate = invitation.StatusId == (int)Enums.InvitationStatus.Rejected ? invitation.SubmissionDate.Value : (DateTime?)null,
                    InvitationWithdrawalDate = invitation.WithdrawalDate, //invitation.StatusId == (int)Enums.InvitationStatus.Withdrawal ? invitation.SubmissionDate.Value : (DateTime?)null,
                    OfferWithdrawalDate = null,
                }).ToListAsync();
            var queryResult = await result.Concat(invitedSuppliers).Concat(purchasedSuppliers).ToQueryResult(pageNumber, 6);

            return queryResult;
        }

        public async Task<List<AppliedSuppliersReportDetailsModel>> ExportAppliedSuppliersReport(int tenderId)
        {
            // Get All Offers Regardless It's Status 
            var allowedRoles = new List<string> { RoleNames.UnitManagerUser, RoleNames.UnitSpecialistLevel1, RoleNames.UnitSpecialistLevel2 };
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            var result = await _context.Offers.Where(t => t.IsActive == true && t.TenderId == tenderId)
                                .Select(offer => new AppliedSuppliersReportDetailsModel
                                {
                                    OfferId = offer.OfferId,
                                    offerIdString = Util.Encrypt(offer.OfferId),
                                    OfferStatusId = offer.OfferStatusId,
                                    OfferStatusName = offer.OfferStatusId == (int)Enums.OfferStatus.Received ? Resources.OfferResources.DisplayInputs.Recieved : offer.OfferStatusId == (int)Enums.OfferStatus.Excluded ? Resources.TenderResources.DisplayInputs.Excluded : Resources.OfferResources.DisplayInputs.NotRecieved,
                                    CanSeeDetails = CanEmployeeSeeOpeningDetails(offer.Tender.TenderStatusId),
                                    CommericalRegisterNo = offer.CommericalRegisterNo,
                                    CompanyName = offer.Supplier.SelectedCrName,
                                    InvitationStatusName = offer.Tender.Invitations.FirstOrDefault(x => x.IsActive == true && x.CommericalRegisterNo == offer.CommericalRegisterNo && x.StatusId == (int)Enums.InvitationStatus.Approved) != null ? offer.Tender.Invitations.FirstOrDefault(x => x.IsActive == true && x.CommericalRegisterNo == offer.CommericalRegisterNo && x.StatusId == (int)Enums.InvitationStatus.Approved).InvitationStatus.NameAr : Resources.SharedResources.DisplayInputs.NotExist,
                                    PurchaseStatusName = offer.Tender.ConditionsBooklets.Count > 0 ? (offer.Tender.ConditionsBooklets.FirstOrDefault(x => x.IsActive == true && x.CommericalRegisterNo == offer.CommericalRegisterNo).BillInfo.BillStatusId == (int)BillStatus.Paid ? Resources.TenderResources.DisplayInputs.Purchased : Resources.TenderResources.DisplayInputs.NotPurchased) : Resources.SharedResources.DisplayInputs.NotExist,
                                    InvitationSendingDate = offer.Tender.Invitations.FirstOrDefault(x => x.IsActive == true && x.CommericalRegisterNo == offer.CommericalRegisterNo && (x.StatusId == (int)Enums.InvitationStatus.Approved || x.StatusId == (int)Enums.InvitationStatus.Withdrawal)).SendingDate.HasValue ? offer.Tender.Invitations.FirstOrDefault(x => x.IsActive == true && x.CommericalRegisterNo == offer.CommericalRegisterNo).SendingDate.Value : (DateTime?)null,
                                    PurchaseDate = offer.Tender.ConditionsBooklets.Count > 0 ? (offer.Tender.ConditionsBooklets.FirstOrDefault(x => x.IsActive == true && x.CommericalRegisterNo == offer.CommericalRegisterNo).BillInfo.PurchaseDate.HasValue ? offer.Tender.ConditionsBooklets.FirstOrDefault(x => x.IsActive == true && x.CommericalRegisterNo == offer.CommericalRegisterNo).BillInfo.PurchaseDate : null) : null,
                                    InvitationAcceptanceDate = offer.Tender.Invitations.FirstOrDefault(x => x.IsActive == true && x.CommericalRegisterNo == offer.CommericalRegisterNo && x.StatusId == (int)Enums.InvitationStatus.Approved).SubmissionDate.HasValue ? offer.Tender.Invitations.FirstOrDefault(x => x.IsActive == true && x.CommericalRegisterNo == offer.CommericalRegisterNo).SubmissionDate.Value : (DateTime?)null,
                                    InvitationRejectionDate = offer.Tender.Invitations.FirstOrDefault(x => x.IsActive == true && x.CommericalRegisterNo == offer.CommericalRegisterNo && x.StatusId == (int)Enums.InvitationStatus.Rejected).SubmissionDate.HasValue ? offer.Tender.Invitations.FirstOrDefault(x => x.IsActive == true && x.CommericalRegisterNo == offer.CommericalRegisterNo).SubmissionDate.Value : (DateTime?)null,
                                    InvitationWithdrawalDate = offer.Tender.Invitations.Where(a => a.CommericalRegisterNo == offer.CommericalRegisterNo && a.IsActive == true).FirstOrDefault().WithdrawalDate.HasValue ? offer.Tender.Invitations.Where(a => a.CommericalRegisterNo == offer.CommericalRegisterNo && a.IsActive == true).FirstOrDefault().WithdrawalDate : (DateTime?)null,
                                    OfferWithdrawalDate = offer.WithrdrawlDate,
                                }).ToListAsync();

            // Get Only Paid Conditional Booklet On Tender And Not Applied an Offer
            var purchasedSuppliers = await _context.ConditionsBooklets.Where(x => x.IsActive == true && x.TenderId == tenderId && x.BillInfo != null && x.BillInfo.BillStatusId == (int)Enums.BillStatus.Paid
            && !x.Tender.Offers.Any(o => o.IsActive == true && o.CommericalRegisterNo == x.CommericalRegisterNo))
                .Select(booklet => new AppliedSuppliersReportDetailsModel
                {
                    OfferId = 0,
                    offerIdString = null,
                    OfferStatusId = 0,
                    CanSeeDetails = false,
                    CommericalRegisterNo = booklet.CommericalRegisterNo,
                    CompanyName = booklet.Supplier.SelectedCrName,
                    InvitationStatusName = booklet.Tender.Invitations.Any(x => x.IsActive == true && x.CommericalRegisterNo == booklet.CommericalRegisterNo && x.StatusId == (int)Enums.InvitationStatus.Approved) ? booklet.Tender.Invitations.FirstOrDefault(x => x.IsActive == true && x.CommericalRegisterNo == booklet.CommericalRegisterNo).InvitationStatus.NameAr : Resources.SharedResources.DisplayInputs.NotExist,
                    PurchaseStatusName = booklet.BillInfo.BillStatusId == (int)BillStatus.Paid ? Resources.TenderResources.DisplayInputs.Purchased : Resources.TenderResources.DisplayInputs.NotPurchased,
                    InvitationSendingDate = booklet.Tender.Invitations.FirstOrDefault(x => x.IsActive == true && x.CommericalRegisterNo == booklet.CommericalRegisterNo && x.StatusId == (int)Enums.InvitationStatus.Approved).SendingDate.HasValue ? booklet.Tender.Invitations.FirstOrDefault(x => x.IsActive == true && x.CommericalRegisterNo == booklet.CommericalRegisterNo).SendingDate.Value : (DateTime?)null,
                    PurchaseDate = booklet.BillInfo.PurchaseDate.HasValue ? booklet.BillInfo.PurchaseDate : null,
                    InvitationAcceptanceDate = booklet.Tender.Invitations.FirstOrDefault(x => x.IsActive == true && x.CommericalRegisterNo == booklet.CommericalRegisterNo && x.StatusId == (int)Enums.InvitationStatus.Approved).SubmissionDate.HasValue ? booklet.Tender.Invitations.FirstOrDefault(x => x.IsActive == true && x.CommericalRegisterNo == booklet.CommericalRegisterNo).SubmissionDate.Value : (DateTime?)null,
                    InvitationRejectionDate = null,
                    InvitationWithdrawalDate = null,
                    OfferWithdrawalDate = null,
                }).ToListAsync();


            // Get Approved, Rejected and Withdeawl Invitations On Tender And Did Not Apply An Offer And Did Not Buy The Booklet
            var invitedSuppliers = await _context.Invitations.Where(x => x.IsActive == true && (x.StatusId != (int)InvitationStatus.ToBeSent && x.StatusId != (int)InvitationStatus.New && x.StatusId != (int)InvitationStatus.WaitingPayment)
            && x.TenderId == tenderId
            && !x.Tender.Offers.Any(o => o.IsActive == true && o.CommericalRegisterNo == x.CommericalRegisterNo)
            && !x.Tender.ConditionsBooklets.Any(o => o.IsActive == true && o.CommericalRegisterNo == x.CommericalRegisterNo && o.BillInfo != null && o.BillInfo.BillStatusId == (int)Enums.BillStatus.Paid))
                .Select(invitation => new AppliedSuppliersReportDetailsModel
                {
                    OfferId = 0,
                    offerIdString = null,
                    OfferStatusId = 0,
                    CanSeeDetails = false,
                    CommericalRegisterNo = invitation.CommericalRegisterNo,
                    CompanyName = invitation.Supplier.SelectedCrName,
                    InvitationStatusName = invitation.InvitationStatus.NameAr,
                    PurchaseStatusName = Resources.SharedResources.DisplayInputs.NotExist,
                    InvitationSendingDate = invitation.SendingDate.Value,
                    PurchaseDate = null,
                    InvitationAcceptanceDate = invitation.StatusId != (int)Enums.InvitationStatus.Rejected ? invitation.SubmissionDate.Value : (DateTime?)null,
                    InvitationRejectionDate = invitation.StatusId == (int)Enums.InvitationStatus.Rejected ? invitation.SubmissionDate.Value : (DateTime?)null,
                    InvitationWithdrawalDate = invitation.WithdrawalDate, //invitation.StatusId == (int)Enums.InvitationStatus.Withdrawal ? invitation.SubmissionDate.Value : (DateTime?)null,
                    OfferWithdrawalDate = null,
                }).ToListAsync();
            var allResult = result.Concat(invitedSuppliers).Concat(purchasedSuppliers).ToList();

            return allResult;
        }


        private static bool CanEmployeeSeeOpeningDetails(int statusId)
        {
            if (statusId != (int)Enums.TenderStatus.UnderEstablishing && statusId != (int)Enums.TenderStatus.Established && statusId != (int)Enums.TenderStatus.Pending && statusId != (int)Enums.TenderStatus.Approved && statusId != (int)Enums.TenderStatus.Rejected && statusId != (int)Enums.TenderStatus.OffersOppening && statusId != (int)Enums.TenderStatus.OffersOppenedPending
              //&& statusId != (int)Enums.TenderStatus.OffersOppenedConfirmed
              && statusId != (int)Enums.TenderStatus.OffersOppenedRejected)
                return true;
            else
                return false;
        }


        public async Task<OfferCheckingDetailsModel> FindOfferDetailsById(int offerId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(offerId), offerId.ToString());
            var offerEntity = await _context.Offers
                .Where(t => t.OfferId == offerId && t.IsActive == true)
                .Select(offer => new OfferCheckingDetailsModel
                {
                    TenderStatusId = offer.Tender.TenderStatusId,
                    OfferId = offer.OfferId,
                    TenderId = offer.TenderId,
                    TenderTypeId = offer.Tender.TenderTypeId,
                    RejectionReason = offer.RejectionReason,
                    OfferAcceptanceStatusId = offer.OfferAcceptanceStatusId,
                    OfferTechnicalEvaluationStatusId = offer.OfferTechnicalEvaluationStatusId != null ? offer.OfferTechnicalEvaluationStatusId : 0,
                    Notes = offer.Notes,
                    TechniciansReportAttachmentsRef = string.Join(",", offer.TechnicianReportAttachments.Select(d => d.FileNetReferenceId + ":" + d.Name).ToList()),
                    TechniciansReportAttachments = offer.TechnicianReportAttachments.Select(t => new TechniciansReportAttachmentModel
                    {
                        AttachmentId = t.AttachmentId,
                        AttachmentTypeId = t.AttachmentTypeId,
                        FileNetReferenceId = t.FileNetReferenceId,
                        Name = t.Name,
                        OfferId = t.OfferId
                    }).ToList(),
                    OffersCheckingDateTime = offer.Tender.OffersCheckingDate,
                    IsOpened = offer.IsOpened.HasValue ? offer.IsOpened.Value ? Resources.SharedResources.DisplayInputs.Yes : Resources.SharedResources.DisplayInputs.No : "",
                }).FirstOrDefaultAsync();
            return offerEntity;
        }

        public async Task<OfferCheckingAttachmentsDetailsModel> FindOfferDetailsForCheckById(int offerId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(offerId), offerId.ToString());
            var offerEntity = await _context.Offers
                .Include(offer => offer.Tender)
                .Include(x => x.Attachment)
                .Include("Attachment.Bank")
                .Include("Attachment.ConstructionWork")
                .Include("Attachment.MaintenanceRunningWork")
                .Where(t => t.OfferId == offerId)
                .Select(offer => new OfferCheckingAttachmentsDetailsModel
                {
                    IsOpened = offer.IsOpened.HasValue ? offer.IsOpened.Value ? Resources.SharedResources.DisplayInputs.Yes : Resources.SharedResources.DisplayInputs.No : "",
                    BankGuaranteeFiles = offer.Attachment
                      .Where(x => x is SupplierBankGuaranteeAttachment)
                      .Select(x => ((SupplierBankGuaranteeAttachment)x)).Select(x => new SupplierBankGuaranteeModel()
                      {
                          OfferId = x.OfferId,
                          AttachmentId = x.AttachmentId,
                          IsBankGuaranteeValid = x.IsBankGuaranteeValid,
                          IsBankGuaranteeValidString = x.IsBankGuaranteeValid.HasValue ? (x.IsBankGuaranteeValid.Value ? Resources.TenderResources.DisplayInputs.IsValid : Resources.TenderResources.DisplayInputs.IsNotValid) : "",
                          GuaranteeNumber = x.GuaranteeNumber,
                          BankId = x.BankId,
                          BankName = x.Bank.NameAr,
                          Amount = x.Amount,
                          GuaranteeStartDate = x.GuaranteeStartDate,
                          GuaranteeEndDate = x.GuaranteeEndDate,
                          GuaranteeStartDateDisplay = (x.GuaranteeStartDate.HasValue) ? (x.GuaranteeStartDate.Value.ToString("dd/MM/yyyy", System.Threading.Thread.CurrentThread.CurrentCulture)) : "",
                          GuaranteeEndDateDisplay = (x.GuaranteeEndDate.HasValue) ? (x.GuaranteeEndDate.Value.ToString("dd/MM/yyyy", System.Threading.Thread.CurrentThread.CurrentCulture)) : "",
                          GuaranteeDays = x.GuaranteeDays
                      }).ToList(),
                    SpecificationsFiles = offer.Attachment
                      .Where(x => x is SupplierSpecificationAttachment)
                      .Select(x => ((SupplierSpecificationAttachment)x)).Select(x => new SupplierSpecificationModel()
                      {
                          OfferId = x.OfferId,
                          IsForApplier = x.IsForApplier,
                          IsForApplierString = x.IsForApplier.HasValue ? (x.IsForApplier.Value ? Resources.TenderResources.DisplayInputs.Offeror : Resources.TenderResources.DisplayInputs.Combined) : "",
                          MaintenanceRunningWorkId = x.MaintenanceRunningWorkId,
                          MaintenanceRunningWorkString = x.MaintenanceRunningWork == null ? "" : x.MaintenanceRunningWork.NameAr,
                          ConstructionWorkId = x.ConstructionWorkId,
                          ConstructionWorkString = x.ConstructionWork != null ? x.ConstructionWork.NameAr : "",
                          ConstructionWork = x.ConstructionWork,
                          Degree = x.Degree,
                          AttachmentId = x.AttachmentId
                      }).ToList()
                }).FirstOrDefaultAsync();
            return offerEntity;
        }
        public async Task<List<OfferSolidarity>> GetSolidaritybyCRs(List<string> crs, int tenderId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(crs), crs);
            var offerEntity = await _context.OfferSolidarities.Where(d => d.IsActive == true && d.Offer.IsActive == true && d.Offer.TenderId == tenderId && d.Offer.Tender.IsActive == true && d.Offer.OfferStatusId != (int)Enums.OfferStatus.Canceled && d.SolidarityTypeId != (int)Enums.UnRegisteredSuppliersInvitationType.SolidarityLeader && crs.Any(r => r == d.CRNumber)).ToListAsync();
            return offerEntity;
        }
        public async Task<Offer> FindOfferById(int offerId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(offerId), offerId.ToString());
            var offerEntity = await _context.Offers
                .Include(o => o.Attachment)
                .Include(o => o.Tender)
                .ThenInclude(t => t.Status)
                .Include(d => d.Status)
                .Include(o => o.Supplier)
                .Include(o => o.Combined)
                .Include(o => o.TechnicianReportAttachments)
                //.Include(o => o.OfferlocalContentDetails)
                //.Include(o => o.QuantityTable).ThenInclude(si => si.QuantityTableItem)
                .AsTracking()
                .Where(t => t.OfferId == offerId).FirstOrDefaultAsync();
            return offerEntity;
        }
        public async Task<Offer> GetOfferWithTenderDetailsById(int offerId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(offerId), offerId.ToString());
            var offerEntity = await _context.Offers
                .Include(o => o.Attachment)
                .Include(o => o.Tender)
                .ThenInclude(t => t.Status)
                .Include(o => o.Tender)
                .ThenInclude(t => t.TenderLocalContent)
                .Include(d => d.Status)
                .Include(o => o.Supplier)
                .Include(o => o.Combined)
                .Include(o => o.TechnicianReportAttachments)
                .AsTracking()
                .Where(t => t.OfferId == offerId).FirstOrDefaultAsync();
            return offerEntity;
        }
        public async Task<Offer> FindOfferWithCombinedById(int offerId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(offerId), offerId.ToString());
            var offerEntity = await _context.Offers
                .Include(o => o.Combined)
                //.Include(o => o.QuantityTable).ThenInclude(si => si.QuantityTableItem)
                .AsTracking()
                .Where(t => t.OfferId == offerId).FirstOrDefaultAsync();
            return offerEntity;
        }

        public async Task<Offer> GetOfferWithTenderById(int offerId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(offerId), offerId.ToString());
            var offerEntity = await _context.Offers
                .Include(o => o.Tender)
                .Where(t => t.OfferId == offerId).FirstOrDefaultAsync();
            return offerEntity;
        }

        public async Task<Offer> FindOfferWithTenderAndStatusAndNegotiationById(int offerId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(offerId), offerId.ToString());
            var offerEntity = await _context.Offers
               .Include(o => o.Tender)
                .ThenInclude(t => t.Status)
                .Include(d => d.Status).Include(f => f.negotiations)
                .AsTracking()
                .Where(t => t.OfferId == offerId).FirstOrDefaultAsync();
            return offerEntity;
        }
        public async Task<Offer> FindOfferForCheckingById(int offerId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(offerId), offerId.ToString());
            var offerEntity = await _context.Offers
                .Include(o => o.Attachment)
                .Include(o => o.Tender)
                .ThenInclude(t => t.Status)
                .Include(o => o.Tender)
                .ThenInclude(t => t.TenderActivities).ThenInclude(tt => tt.Activity).ThenInclude(d => d.ActivityTemplateVersions)
                .Include(d => d.Status)
                .Include(o => o.Supplier)
                .Include(o => o.Combined)
                .Include(o => o.TechnicianReportAttachments)
                .Include(o => o.OfferlocalContentDetails)
                .Include(o => o.SupplierTenderQuantityTables).ThenInclude(si => si.QuantitiyItemsJson)
                .AsTracking()
                .Where(t => t.OfferId == offerId).FirstOrDefaultAsync();
            return offerEntity;
        }
        public async Task<Offer> FindOfferWithStatusById(int offerId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(offerId), offerId.ToString());
            var offerEntity = await _context.Offers
                .Include(d => d.SupplierTenderQuantityTables)
                .ThenInclude(d => d.TenderQuantityTable)
                .Include(d => d.SupplierTenderQuantityTables)
                .ThenInclude(p => p.QuantitiyItemsJson)
                .Include(t => t.Status)
                .Include(o => o.Tender)
                .AsTracking()
                .Where(t => t.OfferId == offerId).FirstOrDefaultAsync();
            return offerEntity;
        }
        public async Task<Offer> GetofferWithTenderAndSupplierQT(int offerId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(offerId), offerId.ToString());
            var offerEntity = await _context.Offers
                .Include(d => d.SupplierTenderQuantityTables)
                .Include(o => o.Tender)
                .Where(t => t.OfferId == offerId && t.IsActive == true).FirstOrDefaultAsync();
            return offerEntity;
        }
        public async Task<Offer> FindOfferWithQTJSONId(int offerId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(offerId), offerId.ToString());
            var offerEntity = await _context.Offers.Where(t => t.OfferId == offerId).Include(o => o.Tender)
                .Include(d => d.SupplierTenderQuantityTables)
                .ThenInclude(d => d.QuantitiyItemsJson)
                .AsTracking()
                .FirstOrDefaultAsync();
            return offerEntity;
        }
        public async Task<Offer> FindOfferWithSupplierQTItemsForApplyOfferByIdAndTableId(int offerId, int tableId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(offerId), offerId.ToString());
            var offer = await _context.Offers
                .Include(o => o.Status)
                .Include(o => o.Tender)
                .AsTracking()
                .Include(o => o.SupplierTenderQuantityTables)
                .ThenInclude(o => o.TenderQuantityTable)
                .Where(t => t.OfferId == offerId).FirstOrDefaultAsync();
            foreach (var item in offer.SupplierTenderQuantityTables)
            {

                var items = await _context.SupplierQuantityTableItemsView.Where(i => i.QTableId == item.Id).ToListAsync();
                item.QuantitiyItemsJson = new SupplierTenderQuantityTableItemJson()
                {
                    Id = items.First().QTableItemsId,
                    SupplierTenderQuantityTableItems = items.Select(s => new SupplierTenderQuantityTableItem()
                    {

                        ActivityTemplateId = s.ActivityTemplateId,
                        AlternativeValue = s.AlternativeValue,
                        ColumnId = s.ColumnId,
                        Id = s.Id,
                        ItemNumber = s.ItemNumber.Value,
                        TenderFormHeaderId = s.TenderFormHeaderId,
                        Value = s.Value,
                        IsDefault = s.IsDefault
                    }).ToList(),
                    SupplierTenderQuantityTableId = item.Id
                };
                item.QuantitiyItemsJson.SupplierTenderQuantityTable = item;
            };
            return offer;
        }

        public async Task<OfferSummaryStatusModel> FindOfferSummaryStatusModelById(int offerId, string cr)
        {
            var localContentSetting = await _localContentConfigurationSettings.getLocalContentSettingsDate();

            Check.ArgumentNotNullOrEmpty(nameof(offerId), offerId.ToString());
            var offer = await _context.Offers.Where(t => t.OfferId == offerId
            && t.CommericalRegisterNo == cr).Select(d =>
               new OfferSummaryStatusModel
               {
                   OfferStringId = Util.Encrypt(d.OfferId),
                   StatusId = d.OfferStatusId,
                   StatusName = d.Status.NameAr,
                   tenderTypeId = d.Tender.TenderTypeId,
                   IsSolidarity = d.IsSolidarity,
                   tenderStringId = Util.Encrypt(d.TenderId),
                   CommercialNumber = d.CommericalRegisterNo,
                   IsOfferOwner = d.Combined.Where(w => w.SolidarityTypeId == (int)UnRegisteredSuppliersInvitationType.SolidarityLeader).Any(a => a.CRNumber == cr),
                   TenderLocalContentId = d.Tender.TenderLocalContent.Id,
                   IsValidToApplyOfferLocalContentChanges = d.Tender.CreatedAt > localContentSetting.Date 
               }).FirstOrDefaultAsync();
 
            if (offer.IsValidToApplyOfferLocalContentChanges && !offer.IsOldTender)
            {
                offer.IsTenderHasLocalContentMechanism = await _context.LocalContentMechanism.AnyAsync(x => x.TenderLocalContentId == offer.TenderLocalContentId && (x.LocalContentMechanismPreferenceId == (int)Enums.LocalContentMechanismPerfermance.MinimumRequiredMechanismLocalContent
                     || x.LocalContentMechanismPreferenceId == (int)Enums.LocalContentMechanismPerfermance.MechanismForWeighingLocalContentFinancialEvaluation));
             }
            return offer;
        }

        public async Task<Offer> FindOfferByIdWithAttaches(int offerId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(offerId), offerId.ToString());
            var offerEntity = await _context.Offers
                .Where(t => t.OfferId == offerId)
                .Include(x => x.Attachment).AsTracking()
                .FirstOrDefaultAsync();
            return offerEntity;
        }

        public async Task<OfferFinancialDetailsModel> OfferFinancialDetails(int offerId)
        {
            var result = (await _context.Offers
               .Where(t => t.OfferId == offerId)
                .Include(x => x.Attachment)
                .ToListAsync())
                  .Select(offer => new OfferFinancialDetailsModel
                  {
                      Notes = offer.Notes,
                      OfferId = offer.OfferId,
                      FinalPrice = offer.FinalPriceAfterDiscount,
                      DiscountNotes = offer.DiscountNotes,
                      Discount = offer.Discount,
                      Attachments = offer.Attachment
                      .Select(x => new AttachmentModel
                      {
                          AttachmentId = x.AttachmentId,
                          FileName = x.FileName,
                          FileNetId = x.FileNetReferenceId
                      })
                      .ToList()
                  }).FirstOrDefault();
            return result;
        }
        public async Task<OfferLocalContentDetailsModel> GetOfferLocalContentDetails(int offerId)
        {
            var userid = _httpContextAccessor.HttpContext.User.UserId();
            var result = await _context.Offers
               .Where(t => t.IsActive == true && t.OfferId == offerId)
                  .Select(offer => new OfferLocalContentDetailsModel
                  {
                      OfferIdString = Util.Encrypt(offerId),
                      IsIncludedInMoneyMarket = offer.OfferlocalContentDetails.IsIncludedInMoneyMarket.HasValue && offer.OfferlocalContentDetails.IsIncludedInMoneyMarket.Value ? "نعم" : "لا",
                      IsSmallOrMediumCorporation = offer.OfferlocalContentDetails.IsSmallOrMediumCorporation.HasValue && offer.OfferlocalContentDetails.IsSmallOrMediumCorporation.Value ? "نعم" : "لا",
                      LocalContentDesiredPercentage = offer.OfferlocalContentDetails.LocalContentDesiredPercentage.HasValue ? offer.OfferlocalContentDetails.LocalContentDesiredPercentage.Value.ToString() + " % " : "لا يوجد",
                      LocalContentPercentage = offer.OfferlocalContentDetails.LocalContentPercentage.HasValue ? offer.OfferlocalContentDetails.LocalContentPercentage.Value.ToString() + " % " : "لا يوجد",
                      CorporationSizeDate = offer.OfferlocalContentDetails.CorporationSizeUpdatedDate.ToHijriDateWithFormat("yyyy-MM-dd"),
                      BaseLineDate = offer.OfferlocalContentDetails.BaseLineUpdatedDate.ToHijriDateWithFormat("yyyy-MM-dd"),
                      IsIncludedInMoneyMarketDate = offer.OfferlocalContentDetails.IsIncludedInMoneyMarketUpdatedDate.ToHijriDateWithFormat("yyyy-MM-dd"),
                      LocalContentDesiredPercentageDate = offer.OfferlocalContentDetails.LocalContentDesiredPercentageUpdatedDate.ToHijriDateWithFormat("yyyy-MM-dd"),
                      SupplierCr = offer.CommericalRegisterNo,
                      TenderStatusId = offer.Tender.TenderStatusId,
                      IsDirectPurchaseMember = offer.Tender.IsLowBudgetDirectPurchase.HasValue && userid == offer.Tender.DirectPurchaseMemberId
                  }).FirstOrDefaultAsync();
            return result;
        }

        public async Task<OfferSolidarity> FindOfferByCombinedSupplierId(int combinedSupplierId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(combinedSupplierId), combinedSupplierId.ToString());
            var offerEntity = await _context.OfferSolidarities
                .Include(o => o.Supplier)
                .Include(s => s.Offer).ThenInclude(o => o.Attachment).Where(s => s.Id == combinedSupplierId).FirstOrDefaultAsync();
            return offerEntity;
        }

        public async Task<Offer> GetOfferByIdForFinancialChecking(int offerId)
        {
            var result = await _context.Offers
               .Where(t => t.OfferId == offerId)
               .Include(a => a.SupplierTenderQuantityTables).
                ThenInclude(a => a.QuantitiyItemsJson)
               .Include(x => x.Tender)
               .ThenInclude(t => t.Status)
                .Include(x => x.Combined)
                .Include(x => x.TechnicianReportAttachments)
                .Include(b => b.BankGuaranteeDetails)
                .Include(o => o.OfferlocalContentDetails)
                .FirstOrDefaultAsync();
            if (result != null)
            {
                var tenderActivities = await _context.TenderActivities.Include(a => a.Activity).ThenInclude(s => s.ActivityTemplateVersions).Where(a => a.TenderId == result.TenderId).ToListAsync();
                result.Tender.TenderActivities.AddRange(tenderActivities);
            }
            return result;
        }

        public async Task<Offer> FindOfferWithSupplierQuantitiesByOfferId(int offerId)
        {
            var result = await _context.Offers
               .Where(t => t.OfferId == offerId)
               .Where(t => t.OfferAcceptanceStatusId == (int)Enums.OfferAcceptanceStatusId.AcceptedOffer)
               //.Include(x => x.QuantityTable).ThenInclude(x => x.QuantityTableItem)
               .FirstOrDefaultAsync();
            return result;
        }

        public async Task<Offer> FindOfferWithSupplierTenderQuantitiesByOfferId(int offerId)
        {
            var result = await _context.Offers
               .Where(t => t.OfferId == offerId)
               .Where(t =>
               t.OfferAcceptanceStatusId == (int)Enums.OfferAcceptanceStatusId.AcceptedOffer)
               .Include(x => x.SupplierTenderQuantityTables).ThenInclude(x => x.QuantitiyItemsJson)
               .FirstOrDefaultAsync();
            return result;
        }

        public async Task<OfferAttachmentsModel> GetOfferAttachmentsDetails(int offerId, int combinedId)
        {
            var res = await _context.SupplierCombinedDetails
                .Where(d => d.CombinedId == combinedId)
                .Select(SCD => new OfferAttachmentsModel
                {
                    SupplierName = SCD.Combined.Supplier.SelectedCrName,
                    CombinedDetailId = SCD.CombinedDetailId,
                    CombinedId = SCD.CombinedId,
                    TenderIDString = Util.Encrypt(SCD.Combined.Offer.Tender.TenderId),
                    TenderStatusId = SCD.Combined.Offer.Tender.TenderStatusId,
                    CombinationLetter = SCD.Combined.Offer.Attachment.Where(x => x is SupplierCombinedAttachment)
                    .Select(x => new AttachmentModel
                    {
                        FileName = x.FileName,
                        FileNetId = x.FileNetReferenceId
                    }).FirstOrDefault(),
                    OfferIdString = Util.Encrypt(SCD.Combined.Offer.OfferId),
                    OfferId = SCD.Combined.Offer.OfferId,
                    CombinDetailsIDString = Util.Encrypt(SCD.CombinedDetailId),
                    IsChamberJoiningAttached = SCD.IsChamberJoiningAttached,
                    IsChamberJoiningValid = SCD.IsChamberJoiningValid,
                    IsCommercialRegisterAttached = SCD.IsCommercialRegisterAttached,
                    IsCommercialRegisterValid = SCD.IsCommercialRegisterValid,
                    IsOfferCopyAttached = SCD.Combined.Offer.IsOfferCopyAttached,
                    IsOfferLetterAttached = SCD.Combined.Offer.IsOfferLetterAttached,
                    OfferLetterNumber = SCD.Combined.Offer.OfferLetterNumber,
                    OfferLetterDate = SCD.Combined.Offer.OfferLetterDate,
                    OfferLetterDateDisplay = (SCD.Combined.Offer.OfferLetterDate.HasValue) ? (SCD.Combined.Offer.OfferLetterDate.Value.ToString("dd/MM/yyyy", System.Threading.Thread.CurrentThread.CurrentCulture)) : "",
                    IsPurchaseBillAttached = SCD.Combined.Offer.IsPurchaseBillAttached,
                    IsSaudizationAttached = SCD.IsSaudizationAttached,
                    IsSaudizationValidDate = SCD.IsSaudizationValidDate,
                    IsSocialInsuranceAttached = SCD.IsSocialInsuranceAttached,
                    IsSocialInsuranceValidDate = SCD.IsSocialInsuranceValidDate,
                    IsVisitationAttached = SCD.Combined.Offer.IsVisitationAttached,
                    IsZakatAttached = SCD.IsZakatAttached,
                    IsZakatValidDate = SCD.IsZakatValidDate,
                    IsOpened = SCD.Combined.Offer.IsOpened,
                    IsBankGuaranteeAttached = SCD.Combined.Offer.IsBankGuaranteeAttached.Value,
                    IsSpecificationAttached = SCD.IsSpecificationAttached.Value,
                    IsSpecificationValidDate = SCD.IsSpecificationValidDate.Value,
                    OfferPresentationWayId = SCD.Combined.Offer.Tender.OfferPresentationWayId.Value,
                    BankGuaranteeFiles = SCD.Combined.Offer.BankGuaranteeDetails
                                .Select(x => new SupplierBankGuaranteeModel
                                {
                                    IsBankGuaranteeValid = x.IsBankGuaranteeValid,
                                    IsBankGuaranteeValidString = x.IsBankGuaranteeValid.HasValue ? (x.IsBankGuaranteeValid.Value ? Resources.OfferResources.DisplayInputs.Valid : Resources.OfferResources.DisplayInputs.Invalid) : "",
                                    GuaranteeNumber = x.GuaranteeNumber,
                                    BankId = x.BankId,
                                    BankName = x.Bank.NameAr,
                                    Amount = x.Amount,
                                    GuaranteeStartDate = x.GuaranteeStartDate.Value.Date,
                                    GuaranteeEndDate = x.GuaranteeEndDate.Value.Date,
                                    GuaranteeStartDateDisplay = (x.GuaranteeStartDate.HasValue) ? (x.GuaranteeStartDate.Value.ToString("dd/MM/yyyy", System.Threading.Thread.CurrentThread.CurrentCulture)) : "",
                                    GuaranteeEndDateDisplay = (x.GuaranteeEndDate.HasValue) ? (x.GuaranteeEndDate.Value.ToString("dd/MM/yyyy", System.Threading.Thread.CurrentThread.CurrentCulture)) : "",
                                    GuaranteeDays = x.GuaranteeDays
                                }).ToList(),
                    SpecificationsFiles = SCD.SpecificationDetails.Select(x => new SupplierSpecificationModel()
                    {
                        IsForApplier = x.IsForApplier,
                        IsForApplierString = x.IsForApplier.HasValue ? (x.IsForApplier.Value ? Resources.OfferResources.DisplayInputs.OfferApplier : Resources.OfferResources.DisplayInputs.Solidarity) : "",
                        MaintenanceRunningWorkId = x.MaintenanceRunningWorkId,
                        MaintenanceRunningWorkString = x.MaintenanceRunningWork == null ? "" : x.MaintenanceRunningWork.NameAr,
                        ConstructionWorkId = x.ConstructionWorkId,
                        ConstructionWorkString = x.ConstructionWork == null ? "" : x.ConstructionWork.NameAr,
                        Degree = x.Degree,
                    }).ToList()
                }).FirstOrDefaultAsync();

            return res;
        }

        public async Task<OfferDetailModel> GetOfferDetails(int combinedsupplierId)
        {
            var res = await _context.SupplierCombinedDetails
                .Where(d => d.CombinedId == combinedsupplierId)
                .Select(SCD => new OfferDetailModel
                {
                    CombinedDetailId = SCD.CombinedDetailId,
                    CombinedId = SCD.CombinedId,
                    TenderIDString = Util.Encrypt(SCD.Combined.Offer.Tender.TenderId),
                    OfferIdString = Util.Encrypt(SCD.Combined.Offer.OfferId),
                    OfferId = SCD.Combined.Offer.OfferId,
                    CombinDetailsIDString = Util.Encrypt(SCD.CombinedDetailId),
                    CombinedOwner = SCD.Combined.SolidarityTypeId == (int)Enums.UnRegisteredSuppliersInvitationType.SolidarityLeader,
                    IsChamberJoiningAttached = SCD.IsChamberJoiningAttached,
                    IsChamberJoiningValid = SCD.IsChamberJoiningValid,
                    IsCommercialRegisterAttached = SCD.IsCommercialRegisterAttached,
                    IsCommercialRegisterValid = SCD.IsCommercialRegisterValid,
                    IsOfferCopyAttached = SCD.Combined.Offer.IsOfferCopyAttached,
                    IsOfferLetterAttached = SCD.Combined.Offer.IsOfferLetterAttached,
                    OfferLetterNumber = SCD.Combined.Offer.OfferLetterNumber,
                    OfferLetterDate = SCD.Combined.Offer.OfferLetterDate,
                    OfferLetterDateDisplay = (SCD.Combined.Offer.OfferLetterDate.HasValue) ? (SCD.Combined.Offer.OfferLetterDate.Value.ToString("dd/MM/yyyy", System.Threading.Thread.CurrentThread.CurrentCulture)) : "",
                    IsPurchaseBillAttached = SCD.Combined.Offer.IsPurchaseBillAttached,
                    IsSaudizationAttached = SCD.IsSaudizationAttached,
                    IsSaudizationValidDate = SCD.IsSaudizationValidDate,
                    IsSocialInsuranceAttached = SCD.IsSocialInsuranceAttached,
                    IsSocialInsuranceValidDate = SCD.IsSocialInsuranceValidDate,
                    IsVisitationAttached = SCD.Combined.Offer.IsVisitationAttached,
                    IsZakatAttached = SCD.IsZakatAttached,
                    IsZakatValidDate = SCD.IsZakatValidDate,
                    IsOpened = SCD.Combined.Offer.IsOpened,
                    IsBankGuaranteeAttached = SCD.Combined.Offer.IsBankGuaranteeAttached.HasValue && SCD.Combined.Offer.IsBankGuaranteeAttached == true ? true : false,
                    IsSpecificationAttached = SCD.IsSpecificationAttached.HasValue && SCD.IsSpecificationAttached == true ? true : false,
                    IsSpecificationValidDate = SCD.IsSpecificationValidDate.HasValue && SCD.IsSpecificationValidDate == true ? true : false,
                    OfferPresentationWayId = SCD.Combined.Offer.Tender.OfferPresentationWayId.Value,
                    BankGuaranteeFiles = SCD.Combined.Offer.BankGuaranteeDetails
                                .Select(x => new SupplierBankGuaranteeModel
                                {
                                    IsBankGuaranteeValid = x.IsBankGuaranteeValid,
                                    IsBankGuaranteeValidString = x.IsBankGuaranteeValid.HasValue ? (x.IsBankGuaranteeValid.Value ? Resources.OfferResources.DisplayInputs.Valid : Resources.OfferResources.DisplayInputs.Invalid) : "",
                                    GuaranteeNumber = x.GuaranteeNumber,
                                    BankId = x.BankId,
                                    BankName = x.Bank.NameAr,
                                    Amount = x.Amount,
                                    GuaranteeStartDate = x.GuaranteeStartDate.Value.Date,
                                    GuaranteeEndDate = x.GuaranteeEndDate.Value.Date,
                                    GuaranteeStartDateDisplay = (x.GuaranteeStartDate.HasValue) ? (x.GuaranteeStartDate.Value.ToString("dd/MM/yyyy", System.Threading.Thread.CurrentThread.CurrentCulture)) : "",
                                    GuaranteeEndDateDisplay = (x.GuaranteeEndDate.HasValue) ? (x.GuaranteeEndDate.Value.ToString("dd/MM/yyyy", System.Threading.Thread.CurrentThread.CurrentCulture)) : "",
                                    GuaranteeDays = x.GuaranteeDays
                                }).ToList(),
                    SpecificationsFiles = SCD.SpecificationDetails.Select(x => new SupplierSpecificationModel()
                    {
                        IsForApplier = x.IsForApplier,
                        IsForApplierString = x.IsForApplier.HasValue ? (x.IsForApplier.Value ? Resources.OfferResources.DisplayInputs.OfferApplier : Resources.OfferResources.DisplayInputs.Solidarity) : "",
                        MaintenanceRunningWorkId = x.MaintenanceRunningWorkId,
                        MaintenanceRunningWorkString = x.MaintenanceRunningWork == null ? "" : x.MaintenanceRunningWork.NameAr,
                        ConstructionWorkId = x.ConstructionWorkId,
                        ConstructionWorkString = x.ConstructionWork == null ? "" : x.ConstructionWork.NameAr,
                        Degree = x.Degree,
                    }).ToList()
                }).FirstOrDefaultAsync();

            return res;
        }

        public async Task<List<Tender>> GetPostqualificationOnTenderForCancel(int tenderId)
        {
            var result = await _context.Tenders
                .Where(t => t.PostQualificationTenderId == tenderId && t.TenderStatusId != (int)Enums.TenderStatus.Canceled)
                .ToListAsync();
            return result;
        }
        public async Task<List<PostQualificationSuppliersInvitations>> GetPostqualificationOnTender(int tenderId)
        {
            var result = await _context.PostQualificationSuppliersInvitations
                .Include(x => x.PostQualification)
                    .ThenInclude(x => x.QualificationFinalResults)
                .Where(t => t.PostQualification.PostQualificationTenderId == tenderId && t.IsActive == true)
                .Where(t => t.PostQualification.IsActive == true && t.PostQualification.TenderStatusId != (int)Enums.TenderStatus.Canceled)
                .ToListAsync();
            return result;
        }
        public async Task<QueryResult<OfferFinancialDetailsModel>> OfferFinancialDetailsForTender(int tenderId)
        {
            var result = await (_context.Offers
               .Where(t => t.TenderId == tenderId && t.IsActive == true)
               .Where(t => t.OfferStatusId == (int)Enums.OfferStatus.Received && t.OfferTechnicalEvaluationStatusId == (int)Enums.OfferTechnicalEvaluationStatusId.IdenticalOffer &&
               t.TotalOfferAwardingValue == null && t.PartialOfferAwardingValue == null))
               .OrderBy(o => o.FinalPriceAfterDiscount)
               .Select(offer => new OfferFinancialDetailsModel
               {

                   OfferId = offer.OfferId,
                   FinalPrice = offer.FinalPriceAfterDiscount,
                   OfferAcceptanceStatusId = offer.OfferAcceptanceStatusId,
                   OfferTechnicalEvaluationStatusId = offer.OfferTechnicalEvaluationStatusId,
                   InvitationTypeName = offer.Tender.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase ? (offer.Tender.TenderType.NameAr)
                      + (offer.Tender.Invitations.Any(i => i.CommericalRegisterNo == offer.CommericalRegisterNo) ? " / " + offer.Tender.Invitations.FirstOrDefault(i => i.CommericalRegisterNo == offer.CommericalRegisterNo).InvitationStatus.NameAr : "") : offer.Tender.TenderType.NameAr,
                   State = offer.Status.NameAr,
                   CommericalRegisterNo = offer.CommericalRegisterNo,
                   CommericalRegisterName = offer.Supplier.SelectedCrName,
                   TenderStatusId = offer.Tender.TenderStatusId,
                   OfferPrice = offer.FinalPriceAfterDiscount,
                   //OfferDiscountValue = offer.QuantityTable.Select(q => Util.DecryptNewDecimal(q.TotalPrice) * (Util.DecryptNewDecimal(q.TotalDiscount) / 100)).Sum(),
                   AwardingValue = offer.Tender.TenderAwardingType == true ? offer.TotalOfferAwardingValue : offer.PartialOfferAwardingValue,
                   Notes = offer.Notes,
                   FinancialEvaluationLevel = offer.FinancialEvaluationLevel,
                   TechnicalEvaluationLevel = offer.TechnicalEvaluationLevel,
                   HasPrequalification = offer.Tender.PreQualificationId.HasValue,
                   HasPostQualification = offer.Tender.PostQualifications.Count > 0,
                   postQualificationTenderIdString = offer.Tender.PreQualificationId != null ? Util.Encrypt(offer.Tender.PreQualificationId.Value) : "",
                   PrequalificationHistoryModels = offer.Tender.PreQualificationId.HasValue && offer.Tender.PreQualification.TenderHistories.Any(h => h.StatusId == (int)Enums.TenderStatus.DocumentCheckConfirmed) ?
                    offer.Tender.PreQualification.TenderHistories.Where(h => h.StatusId == (int)Enums.TenderStatus.DocumentCheckConfirmed).Select(h => new TenderHistoryModel
                    {
                        TenderHistoryId = h.TenderHistoryId,
                        CreatedDate = h.CreatedAt
                    }).ToList() : new List<TenderHistoryModel>()

               }).ToQueryResult(1, 50);
            return result;
        }
        public async Task<QueryResult<OfferFinancialDetailsModel>> GetOffersForAwardingByTenderId(int tenderId, string crsuppliers)
        {
            DateTime newAwardingReleaseDate = _rootConfiguration.NewAwarding.ReleaseDate.ToDateTime();
            string[] AwardedSuppliers = null;
            if (!string.IsNullOrEmpty(crsuppliers))
            {
                AwardedSuppliers = crsuppliers.Split(",");
            }
            var postqualifiedSuppliers = await GetPostqualificationOnTender(tenderId);

            var result = await _context.Offers
                .Include(a => a.Supplier)
                .Include(a => a.Status)
                .Where(o => o.TenderId == tenderId && o.IsActive == true)
               //.Where(o => !o.Tender.AgencyCommunicationRequests.Any(g => g.StatusId == (int)Enums.AgencyCommunicationRequestStatus.Approved && g.AgencyRequestTypeId == (int)Enums.AgencyCommunicationRequestType.ExtendOfferValidtiy) ||
               //o.Tender.AgencyCommunicationRequests.Any(g => g.AgencyRequestTypeId == (int)Enums.AgencyCommunicationRequestType.ExtendOfferValidtiy
               // && g.ExtendOffersValidity.ExtendOffersValiditySuppliers.Any(e => e.SupplierCR == o.CommericalRegisterNo &&
               //(g.ExtendOffersValidity.NewOffersExpiryDate > DateTime.Now || e.SupplierExtendOfferValidityStatusId != (int)Enums.SupplierExtendOffersValidityStatus.Rejected || e.SupplierExtendOfferValidityStatusId == (int)Enums.SupplierExtendOffersValidityStatus.Accepted))))
               .Where(o => o.TotalOfferAwardingValue == null && o.PartialOfferAwardingValue == null).ToListAsync();

            var tender = await _context.Tenders.Where(t => t.TenderId == tenderId && t.IsActive == true).Include(t => t.PostQualifications).FirstOrDefaultAsync();
            var tenderHistories = await _context.TenderHistories.Where(h => h.TenderId == tenderId && h.StatusId == (int)Enums.TenderStatus.OffersAwarding).ToListAsync();

            tender.SetTenderHistories(tenderHistories);

            var results = result.OrderBy(o => o.FinalPriceAfterDiscount)
                .Where(o => tender.IsNewAwarding(newAwardingReleaseDate)
                ? (o.OfferStatusId == (int)Enums.OfferStatus.Received && o.OfferTechnicalEvaluationStatusId == (int)Enums.OfferTechnicalEvaluationStatusId.IdenticalOffer
               && o.OfferAcceptanceStatusId == (int)Enums.OfferAcceptanceStatusId.AcceptedOffer)
               :
               (o.OfferStatusId == (int)Enums.OfferStatus.Received && o.OfferTechnicalEvaluationStatusId == (int)Enums.OfferTechnicalEvaluationStatusId.IdenticalOffer))
           .Select(offer => new OfferFinancialDetailsModel
           {
               OfferId = offer.OfferId,
               TenderId = offer.TenderId,
               FinalPrice = offer.FinalPriceAfterDiscount,
               OfferAcceptanceStatusId = offer.OfferAcceptanceStatusId,
               OfferTechnicalEvaluationStatusId = offer.OfferTechnicalEvaluationStatusId,
               State = offer.Status.NameAr,
               CommericalRegisterNo = offer.CommericalRegisterNo,
               CommericalRegisterName = offer.Supplier.SelectedCrName,
               TenderStatusId = tender.TenderStatusId,
               OfferPrice = offer.FinalPriceAfterDiscount,
               OfferWeightAfterCalcNPA = offer.OfferWeightAfterCalcNPA,
               AwardingValue = tender.TenderAwardingType == true ? offer.TotalOfferAwardingValue : offer.PartialOfferAwardingValue,
               Notes = offer.Notes,
               FinancialEvaluationLevel = offer.FinancialEvaluationLevel,
               TechnicalEvaluationLevel = offer.TechnicalEvaluationLevel,
               ExclusionReason = offer.ExclusionReason,
               DirectPurchaseReasonId = tender.ReasonForPurchaseTenderTypeId,
               IsExclusionReasonEmpty = string.IsNullOrEmpty(offer.ExclusionReason) ? true : false,
               HasPrequalification = tender.PreQualificationId.HasValue,
               HasPostQualification = tender.PostQualifications.Any(),
               postQualificationTenderIdString = tender.PreQualificationId != null ? Util.Encrypt(tender.PreQualificationId.Value) : "",
           }).ToList();

            QueryResult<OfferFinancialDetailsModel> resultValue;
            if (AwardedSuppliers != null)
            {
                resultValue = await results.Where(x => !AwardedSuppliers.Contains(x.CommericalRegisterNo)).ToQueryResult(1, 50);
            }
            else
            {
                resultValue = await results.ToQueryResult(1, 50);
            }
            foreach (var item in resultValue.Items)
            {

                item.CanAddExclustionReasonIfExtendOfferValidityExist = await CanAddExclusionReasonIfTenderHasExtendOfferValidity(item.TenderId);
                item.HasActivePostQaulification = postqualifiedSuppliers.Any(t => t.CommercialNumber == item.CommericalRegisterNo
                && t.PostQualification.TenderStatusId != (int)Enums.TenderStatus.DocumentCheckConfirmed);

                if (!postqualifiedSuppliers.Any(s => s.CommercialNumber == item.CommericalRegisterNo))
                {
                    item.IsPasssPostQaulification = null;
                }
                else
                {
                    if (postqualifiedSuppliers.Any(s => s.PostQualification.TenderStatusId == (int)Enums.TenderStatus.DocumentCheckConfirmed)
                        && postqualifiedSuppliers.Any(s => s.PostQualification.QualificationFinalResults.Any(f => f.SupplierSelectedCr == item.CommericalRegisterNo && f.QualificationLookupId == (int)Enums.QualificationResultLookup.Succeeded && f.IsActive == true)))
                    {
                        item.IsPasssPostQaulification = true;
                    }
                    else if (postqualifiedSuppliers.Any(s => s.PostQualification.TenderStatusId == (int)Enums.TenderStatus.DocumentCheckConfirmed)
                        && postqualifiedSuppliers.Any(s => s.PostQualification.QualificationFinalResults.Any(f => f.SupplierSelectedCr == item.CommericalRegisterNo && f.QualificationLookupId == (int)Enums.QualificationResultLookup.Failed && f.IsActive == true)))
                    {
                        item.IsPasssPostQaulification = false;
                    }
                }
            }
            return resultValue;
        }

        public async Task<List<OfferDeatilsReportForTenderModel>> OfferDetailsReportForTender(int tenderId)
        {
            //this is used to confirm that the tender offers opening is done
            List<int> OneFileTenderStatusIds = GetOneFileTenderStatusIdsForOpenOffersReportHiding();
            List<int> TwoFileTenderStatusIds = GetTwoFileTenderStatusIdsForOpenOffersReportHiding();
            var agencyCode = _httpContextAccessor.HttpContext.User.UserAgency();
            var CR = _httpContextAccessor.HttpContext.User.SupplierNumber();
            List<OfferDeatilsReportForTenderModel> result;
            var hasData = await _context.Tenders.AnyAsync(d => d.TenderId == tenderId
            && (!OneFileTenderStatusIds.Contains(d.TenderStatusId))
            && (d.Invitations.Any(f => f.IsActive == true && f.CommericalRegisterNo == CR && f.StatusId == (int)InvitationStatus.Approved)
                                || (d.Offers.Any() && d.Offers.Any(o => o.CommericalRegisterNo == CR && o.OfferStatusId != (int)OfferStatus.Canceled)
                                || (d.Offers.Any() && d.Offers.Any(o => o.Combined.Any(c => c != null && c.CRNumber == CR && c.StatusId != (int)SupplierSolidarityStatus.ToBeSent && c.StatusId != (int)SupplierSolidarityStatus.Rejected) && o.OfferStatusId != (int)OfferStatus.Canceled)))
                                || (d.ConditionsBooklets.FirstOrDefault(b => b.CommericalRegisterNo == CR) != null)));
            if (!hasData && !string.IsNullOrEmpty(CR))
                return new List<OfferDeatilsReportForTenderModel>();

            result = await _context.Offers
                .WhereIf(!string.IsNullOrEmpty(agencyCode), x => x.Tender.AgencyCode == agencyCode
                && (!OneFileTenderStatusIds.Contains(x.Tender.TenderStatusId)))
              .Where(t => t.TenderId == tenderId &&
              t.IsActive == true && t.Tender.TenderStatusId != (int)Enums.TenderStatus.Approved)
               .Select(offer => new OfferDeatilsReportForTenderModel
               {
                   TenderId = offer.Tender.TenderId,
                   TenderName = offer.Tender.TenderName,
                   TenderNumber = offer.Tender.ReferenceNumber,
                   OpenOffersDate = offer.Tender.OffersOpeningDate.Value.ToString("dd/MM/yyyy"),
                   OfferId = offer.OfferId,
                   Notes = offer.Notes,
                   SupplierName = offer.Supplier.SelectedCrName,
                   OfferPrice = Math.Round(offer.FinalPriceAfterDiscount ?? 0, 2, MidpointRounding.ToEven),
                   OfferDiscountValue = Math.Round((offer.FinalPriceBeforeDiscount ?? 0) - (offer.FinalPriceAfterDiscount ?? 0), 2, MidpointRounding.ToEven),
                   CreateDate = offer.CreatedAt.ToString("dd/MM/yyyy"),
                   OfferLetter = offer.OfferStatusId > 0 ? Resources.OfferResources.DisplayInputs.Valid : Resources.SharedResources.DisplayInputs.NotValid,
                   CommercialRegistrationValidity = offer.Combined.FirstOrDefault(c => c.SolidarityTypeId == (int)UnRegisteredSuppliersInvitationType.SolidarityLeader)
                   .SupplierCombinedDetail.IsCommercialRegisterValid == true ? Resources.SharedResources.DisplayInputs.Valid : Resources.SharedResources.DisplayInputs.NotValid,
                   ZekatCertificateValidity = offer.Combined.FirstOrDefault(c => c.SolidarityTypeId == (int)UnRegisteredSuppliersInvitationType.SolidarityLeader)
                   .SupplierCombinedDetail.IsZakatValidDate ? Resources.SharedResources.DisplayInputs.Valid : Resources.SharedResources.DisplayInputs.NotValid,
                   ChamberJoiningValidity = offer.Combined.FirstOrDefault(c => c.SolidarityTypeId == (int)UnRegisteredSuppliersInvitationType.SolidarityLeader)
                   .SupplierCombinedDetail.IsChamberJoiningValid == true ? Resources.SharedResources.DisplayInputs.Valid : Resources.SharedResources.DisplayInputs.NotValid,
                   IsValidList = offer.Attachment.Where(x => x is SupplierSpecificationAttachment).Select(o => o.IsValid).ToList(),
                   CanViewPrice = (offer.Tender.OfferPresentationWayId == (int)Enums.OfferPresentationWayId.OneFile ? !OneFileTenderStatusIds.Contains(offer.Tender.TenderStatusId) : !TwoFileTenderStatusIds.Contains(offer.Tender.TenderStatusId)),
                   BankGuarantees = offer.BankGuaranteeDetails.Select(x => new BankGuarantee
                   {
                       GuaranteeNumber = x.GuaranteeNumber,
                       Amount = x.Amount,
                       GuaranteeStartDate = x.GuaranteeStartDate.HasValue ? x.GuaranteeStartDate.Value.ToString("dd/MM/yyyy") : "",
                       BankName = x.Bank.NameAr,
                       Percentage = ((offer.FinalPriceAfterDiscount ?? 0) / x.Amount).ToString("0.00") + " % "
                   }).ToList()
               }).ToListAsync();
            if (result != null)
                result.ForEach(x => x.ConstructionCertificationValidity = x.IsValidList.Contains(true) ? Resources.SharedResources.DisplayInputs.Valid : Resources.SharedResources.DisplayInputs.NotValid);
            else
            {
                result = _context.Tenders.Where(t => t.TenderId == tenderId && t.IsActive == true)
               .Select(t => new OfferDeatilsReportForTenderModel
               {
                   TenderId = t.TenderId,
                   TenderName = t.TenderName,
                   TenderNumber = t.ReferenceNumber,
               }).ToList();
            }
            return result;
        }

        private List<int> GetOneFileTenderStatusIdsForOpenOffersReportHiding()
        {
            List<int> ids = new List<int>();
            ids.Add((int)Enums.TenderStatus.UnderEstablishing);
            ids.Add((int)Enums.TenderStatus.Established);
            ids.Add((int)Enums.TenderStatus.Canceled);
            ids.Add((int)Enums.TenderStatus.Rejected);
            ids.Add((int)Enums.TenderStatus.Approved);
            ids.Add((int)Enums.TenderStatus.OffersOppening);
            ids.Add((int)Enums.TenderStatus.OffersOppenedRejected);
            ids.Add((int)Enums.TenderStatus.OffersOppenedPending);
            return ids;
        }
        private List<int> GetTwoFileTenderStatusIdsForOpenOffersReportHiding()
        {
            List<int> ids = new List<int>();
            ids.Add((int)Enums.TenderStatus.UnderEstablishing);
            ids.Add((int)Enums.TenderStatus.Established);
            ids.Add((int)Enums.TenderStatus.Canceled);
            ids.Add((int)Enums.TenderStatus.Rejected);
            ids.Add((int)Enums.TenderStatus.Approved);
            ids.Add((int)Enums.TenderStatus.OffersOppening);
            ids.Add((int)Enums.TenderStatus.OffersOppenedRejected);
            ids.Add((int)Enums.TenderStatus.OffersOppenedPending);
            ids.Add((int)Enums.TenderStatus.OffersOppenedConfirmed);
            ids.Add((int)Enums.TenderStatus.OffersTechnicalOppening);
            ids.Add((int)Enums.TenderStatus.OffersTechnicalOppeningConfirmed);
            ids.Add((int)Enums.TenderStatus.OffersTechnicalOppeningPending);
            ids.Add((int)Enums.TenderStatus.OffersTechnicalOppeningRejected);
            ids.Add((int)Enums.TenderStatus.OffersChecking);
            ids.Add((int)Enums.TenderStatus.OffersTechnicalCheckingPending);
            ids.Add((int)Enums.TenderStatus.OffersTechnicalCheckingRejected);
            ids.Add((int)Enums.TenderStatus.OffersTechnicalCheckingConfirmed);
            ids.Add((int)Enums.TenderStatus.OffersOpenFinancialStage);
            ids.Add((int)Enums.TenderStatus.OffersOpenFinancialStageApproved);
            ids.Add((int)Enums.TenderStatus.OffersOpenFinancialStagePending);
            ids.Add((int)Enums.TenderStatus.OffersOpenFinancialStageRejected);
            ids.Add((int)Enums.TenderStatus.OffersTechnicalOppeningConfirmed);
            ids.Add((int)Enums.TenderStatus.OffersTechnicalOppeningPending);
            ids.Add((int)Enums.TenderStatus.OffersTechnicalOppeningRejected);
            ids.Add((int)Enums.TenderStatus.OffersTechnicalOppening);
            return ids;
        }

        public async Task<QueryResult<OfferFinancialDetailsModel>> OfferAwardeFinancialDetailsForTender(int tenderId)
        {
            var postqualifiedSuppliers = await GetPostqualificationOnTender(tenderId);
            var result = await (_context.Offers
               .Where(t => t.TenderId == tenderId && t.IsActive == true && t.OfferStatusId == (int)Enums.OfferStatus.Received
               && t.OfferTechnicalEvaluationStatusId == (int)Enums.OfferTechnicalEvaluationStatusId.IdenticalOffer &&
                ((t.TotalOfferAwardingValue.HasValue && t.TotalOfferAwardingValue > 0) || (t.PartialOfferAwardingValue.HasValue && t.PartialOfferAwardingValue > 0)))
                                                                                                                                                                 )
                       .OrderBy(o => o.FinalPriceAfterDiscount)
                  .Select(offer => new OfferFinancialDetailsModel
                  {
                      OfferId = offer.OfferId,
                      FinalPrice = offer.FinalPriceAfterDiscount,
                      AwardingValue = offer.Tender.TenderAwardingType == true ? offer.TotalOfferAwardingValue : offer.PartialOfferAwardingValue,
                      OfferAcceptanceStatusId = offer.OfferAcceptanceStatusId,
                      OfferTechnicalEvaluationStatusId = offer.OfferTechnicalEvaluationStatusId,
                      InvitationTypeName = offer.Tender.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase ? (offer.Tender.TenderType.NameAr)
                      + (offer.Tender.Invitations.Any(i => i.CommericalRegisterNo == offer.CommericalRegisterNo) ? " / " + offer.Tender.Invitations.FirstOrDefault(i => i.CommericalRegisterNo == offer.CommericalRegisterNo).InvitationStatus.NameAr : "") : offer.Tender.TenderType.NameAr,
                      State = offer.Status.NameAr,
                      CommericalRegisterNo = offer.CommericalRegisterNo,
                      postQualificationTenderIdString = Util.Encrypt(offer.Tender.PreQualificationId != null ? offer.Tender.PreQualificationId.Value : offer.TenderId),
                      CommericalRegisterName = offer.Supplier.SelectedCrName,
                      JustificationOfRecommendation = offer.JustificationOfRecommendation,
                      TenderStatusId = offer.Tender.TenderStatusId,
                      OfferPrice = offer.FinalPriceAfterDiscount
                  }).ToQueryResult(1, 30);
            foreach (var item in result.Items)
            {
                if (!postqualifiedSuppliers.Any(s => s.CommercialNumber == item.CommericalRegisterNo))
                {
                    item.IsPasssPostQaulification = null;
                }
                else
                {
                    if (postqualifiedSuppliers.Any(s => s.PostQualification.TenderStatusId == (int)Enums.TenderStatus.DocumentCheckConfirmed)
                        && postqualifiedSuppliers.Any(s => s.PostQualification.QualificationFinalResults.Any(f => f.SupplierSelectedCr == item.CommericalRegisterNo && f.QualificationLookupId == (int)Enums.QualificationResultLookup.Succeeded && f.IsActive == true)))
                    {
                        item.IsPasssPostQaulification = true;
                    }
                    else if (postqualifiedSuppliers.Any(s => s.PostQualification.TenderStatusId == (int)Enums.TenderStatus.DocumentCheckConfirmed)
                        && postqualifiedSuppliers.Any(s => s.PostQualification.QualificationFinalResults.Any(f => f.SupplierSelectedCr == item.CommericalRegisterNo && f.QualificationLookupId == (int)Enums.QualificationResultLookup.Failed && f.IsActive == true)))
                    {
                        item.IsPasssPostQaulification = false;
                    }
                }
                //item.IsPasssPostQaulification = tenders.Count > 0 ? (tenders.Any(s => s.QualificationFinalResults.Any(p => p.SupplierSelectedCr == item.CommericalRegisterNo)) ? (bool?)(tenders.Any(s => s.QualificationFinalResults.Any(q => q.QualificationLookupId == (int)Enums.QualificationResultLookup.Succeeded && q.SupplierSelectedCr == item.CommericalRegisterNo)) ? true : false) : null) : null;
            }

            return result;
        }


        public async Task<List<string>> GetFailedSuppliersOnPostQualification(int tenderId)
        {
            var result = await _context.Tenders.Include(t => t.QualificationFinalResults)
                .Where(t => t.PostQualificationTenderId == tenderId && t.IsActive == true && t.TenderStatusId == (int)Enums.TenderStatus.DocumentCheckConfirmed)
                  .Where(t => t.QualificationFinalResults.Any(q => q.IsActive == true && q.QualificationLookupId == (int)Enums.QualificationResultLookup.Failed))
                  .SelectMany(q => q.QualificationFinalResults)
                .Where(s => s.QualificationLookupId == (int)Enums.QualificationResultLookup.Failed)
                .Select(s => s.SupplierSelectedCr).ToListAsync();
            return result;
        }

        public async Task<List<TenderQuantityItemDTO>> GetSupplierQTableItemsForDirectPurchase(int offerId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(offerId), offerId.ToString());

            var offerEntity = await _context.SupplierTenderQuantityTables
                .Where(t => t.OfferId == offerId && t.IsActive == true && t.Offer.IsActive == true)
                .Include(f => f.TenderQuantityTable).Include(f => f.Offer).Include(t => t.QuantitiyItemsJson)
                .ToListAsync();
            //.ThenInclude(f => f.SupplierTenderQuantityTableItems)
            var tenderid = 0;
            long tableid = 0;
            var tablename = "";
            List<TenderQuantityItemDTO> res = new List<TenderQuantityItemDTO>();
            foreach (var tbl in offerEntity)
            {
                tablename = tbl.TenderQuantityTable.Name;
                tableid = tbl.Id;
                tenderid = tbl.Offer.TenderId;
                foreach (var itm in tbl.QuantitiyItemsJson.SupplierTenderQuantityTableItems)
                {
                    res.Add(new TenderQuantityItemDTO
                    {
                        ColumnId = itm.ColumnId,
                        ItemNumber = itm.ItemNumber,
                        ColumnName = "",
                        TableName = tablename,
                        TableId = tableid,
                        TemplateId = itm.ActivityTemplateId,
                        TenderId = tenderid,
                        TenderFormHeaderId = itm.TenderFormHeaderId,
                        Value = itm.Value,
                        AlternativeValue = itm.AlternativeValue,
                        IsDefault = itm.IsDefault,
                        Id = itm.Id
                    });
                }
            }
            return res;
        }



        #region Apply offer


        public async Task<Offer> GetOfferById(int offerId)
        {
            var result = await _context.Offers
               .Where(t => t.OfferId == offerId)
               .Include(x => x.Tender).ThenInclude(d => d.TenderActivities).ThenInclude(d => d.Activity)
               .Include(x => x.Combined)
               .FirstOrDefaultAsync();
            return result;
        }
        public async Task<Offer> FindOfferByOfferId(int offerId)
        {
            var result = await _context.Offers
               .Where(o => o.IsActive == true && o.OfferId == offerId)
               .FirstOrDefaultAsync();
            return result;
        }
        public async Task<Offer> GetOfferWithLocalContentDetailsById(int offerId)
        {
            var result = await _context.Offers.Include(offer => offer.OfferlocalContentDetails).Include(offer => offer.Tender).ThenInclude(t => t.TenderLocalContent)
               .Where(o => o.IsActive == true && o.OfferId == offerId)
               .FirstOrDefaultAsync();
            return result;
        }
        public async Task<Offer> GetOfferWithQTById(int offerId)
        {
            var result = await _context.Offers.Include(x => x.Tender)
               //.Include(x => x.SupplierTenderQuantityTables).ThenInclude(x => x.QuantitiyItemsJson)
               .Include(x => x.SupplierTenderQuantityTables).ThenInclude(x => x.TenderQuantityTable)
               .Where(t => t.OfferId == offerId)
               .FirstOrDefaultAsync();

            foreach (var table in result.SupplierTenderQuantityTables)
            {
                var itemsCount = _context.SupplierQuantityTableItemsView.Where(i => i.QTableId == table.Id).Count();


                if (itemsCount > 0)
                {
                    var items = await _context.SupplierQuantityTableItemsView.Where(i => i.QTableId == table.Id).ToListAsync();
                    table.QuantitiyItemsJson = new SupplierTenderQuantityTableItemJson()
                    {
                        Id = items.FirstOrDefault().QTableItemsId,
                        SupplierTenderQuantityTableItems = items.Select(s => new SupplierTenderQuantityTableItem()
                        {
                            ActivityTemplateId = s.ActivityTemplateId,
                            AlternativeValue = s.AlternativeValue,
                            ColumnId = s.ColumnId,
                            Id = s.Id,
                            IsDefault = s.IsDefault,
                            ItemNumber = s.ItemNumber.Value,
                            TenderFormHeaderId = s.TenderFormHeaderId,
                            Value = s.Value
                        }).ToList(),
                        SupplierTenderQuantityTableId = table.Id
                    };
                }
            }
            return result;
        }
        public async Task<Offer> GetOfferWithQTByIdForOpenningDetails(int offerId)
        {
            var result = await _context.SupplierTenderQuantityTables.Where(a => a.IsActive == true && a.OfferId == offerId).Include(x => x.TenderQuantityTable).Include(a => a.QuantitiyItemsJson).Include(a => a.Offer).ThenInclude(a => a.Tender).ToListAsync();
            var tables = result.Where(a => a.QuantitiyItemsJson != null && a.QuantitiyItemsJson.SupplierTenderQuantityTableItems.Any()).ToList();
            Offer offer;
            if (tables.Count > 0)
            {
                offer = tables.FirstOrDefault().Offer;
                offer.SupplierTenderQuantityTables.Clear();
                offer.SupplierTenderQuantityTables.AddRange(tables);
            }
            else
                offer = await _context.Offers.Include(x => x.Tender)
               .Where(t => t.OfferId == offerId)
               .FirstOrDefaultAsync();
            return offer;
        }
        public async Task<Offer> GetOfferForDirectPurchaseChekingById(int offerId)
        {
            var result = await _context.Offers
               .Where(t => t.OfferId == offerId)
               .Include(x => x.Tender)
               .Include(x => x.BankGuaranteeDetails)
               .Include(x => x.Combined)
               .Include(x => x.OfferlocalContentDetails)
               .Include(x => x.TechnicianReportAttachments)
               .Include(x => x.SupplierTenderQuantityTables)
               .ThenInclude(x => x.QuantitiyItemsJson)
               .FirstOrDefaultAsync();
            if (result != null)
            {
                var tenderActivities = await _context.TenderActivities.Include(a => a.Activity).ThenInclude(a => a.ActivityTemplateVersions).Where(a => a.TenderId == result.TenderId).ToListAsync();
                result.Tender.TenderActivities.AddRange(tenderActivities);
            }
            return result;
        }
        public async Task<Offer> GetOfferWithQuantityTablesForApplyOfferById(int offerId)
        {
            var offer = await _context.Offers.Where(t => t.OfferId == offerId).FirstOrDefaultAsync();

            return offer;
        }
        public async Task<Offer> GetOfferWithCombinedById(int offerId)
        {
            var result = await _context.Offers
               .Where(t => t.OfferId == offerId)
               .Include(x => x.Combined)
               .FirstOrDefaultAsync();
            return result;
        }


        public async Task<Offer> GetOfferByOfferId(int offerId)
        {
            var result = await _context.Offers
               .Where(t => t.OfferId == offerId)
               .FirstOrDefaultAsync();
            return result;
        }

        #endregion

        public async Task<Offer> FindOfferWithQuantityTableByTenderIdAndCR(int tenderId, string suplierCR)
        {
            return await _context.Offers./*Include(d => d.QuantityTable).ThenInclude(d => d.QuantityTableItem).*/Where(w => w.IsActive == true && w.TenderId == tenderId && suplierCR == w.CommericalRegisterNo && w.OfferStatusId != (int)Enums.OfferStatus.Canceled).FirstOrDefaultAsync();
        }

        public async Task<Tender> CheckOfferAdding(int tenderId, string suplierCR)
        {
            var result = await
                _context.Tenders
               .Include(i => i.Invitations)
               .Include(i => i.ConditionsBooklets)
               .ThenInclude(i => i.BillInfo)
               .Include(o => o.Offers)
               .Where(t => t.TenderId.Equals(tenderId)
               && (t.ConditionsBooklets.Any(c => c.CommericalRegisterNo == suplierCR && c.BillInfo.BillStatusId == (int)Enums.BillStatus.Paid)
               || (t.Invitations.Any(i => i.CommericalRegisterNo == suplierCR && i.StatusId == (int)Enums.InvitationStatus.Approved)
               ))).FirstOrDefaultAsync();
            return result;
        }


        public async Task<OfferModel> GeOfferByTenderId(int tenderId)
        {
            var result = await _context.Offers
          .Where(o => o.IsActive == true && o.TenderId == tenderId)
          .Select(offer => new OfferModel
          {
              TenderName = offer.Tender.TenderName,
              TenderNumber = offer.Tender.TenderNumber,
              InsideKSA = offer.Tender.InsideKSA,
              OfferId = offer.OfferId,
              OfferStatusId = offer.OfferStatusId,
              TenderId = offer.TenderId,
              CommericalRegisterNo = offer.CommericalRegisterNo,

              Attachment = offer.Attachment.Select(a => new SupplierAttachmentModel
              {
                  AttachmentId = a.AttachmentId,
                  FileName = a.FileName,
                  FileNetReferenceId = a.FileNetReferenceId,
                  OfferId = a.OfferId
              }).ToList()
          }).FirstOrDefaultAsync();
            return result;
        }


        public async Task<Offer> GeOfferByTenderIdAndOfferIdForChecking(int offerId)
        {
            return await _context.Offers.Include(o => o.OfferlocalContentDetails)
                .Include(a => a.Tender)
                .ThenInclude(a => a.TenderActivities)
                .ThenInclude(a => a.Activity)
                .Include(a => a.SupplierTenderQuantityTables)
                //.ThenInclude(a => a.SupplierTenderQuantityTableItems)
                .ThenInclude(a => a.QuantitiyItemsJson)
                .Include(a => a.SupplierTenderQuantityTables)
                .ThenInclude(a => a.TenderQuantityTable)
                .Include(a => a.Combined)
                .ThenInclude(a => a.Supplier)
                .Include(a => a.TechnicianReportAttachments)
                .Include(a => a.Attachment)
                .Include(a => a.OfferlocalContentDetails)
                .FirstOrDefaultAsync(o => o.IsActive == true && o.OfferId == offerId);
        }

        public async Task<Offer> GetOfferForQuantityTableForAwarding(int offerId)
        {
            return await _context.Offers
                .Include(a => a.Tender)
                .ThenInclude(a => a.TenderActivities)
                .ThenInclude(a => a.Activity)
                .Include(a => a.SupplierTenderQuantityTables)
                .ThenInclude(a => a.QuantitiyItemsJson)
                .Include(a => a.SupplierTenderQuantityTables)
                .ThenInclude(a => a.TenderQuantityTable)
                .Include(a => a.Combined)
                .ThenInclude(a => a.Supplier)
                .Include(a => a.TechnicianReportAttachments)
                .Include(a => a.Attachment)
                .FirstOrDefaultAsync(o => o.IsActive == true && o.OfferId == offerId);
        }


        public async Task<Core.Entities.Settings.ConfigurationSetting> GetLocalContentSettingsDate()
        {
            var result = await _localContentConfigurationSettings.getLocalContentSettingsDate();
            return result;
        }
        public async Task<OfferModel> OfferCheckingDetailsForAwarding(int tenderId, int offerId)
        {
            var localContentSetting = await _localContentConfigurationSettings.getLocalContentSettingsDate();
            var result = await _context.Offers
                .Where(o => o.IsActive == true && o.TenderId == tenderId && o.OfferId == offerId)
                .Select(offer => new OfferModel
                {
                    TechnicalWeigth = offer.TechnicalEvaluationLevel,
                    FinancialWeigth = offer.FinancialEvaluationLevel,
                    TenderTypeId = offer.Tender.TenderTypeId,
                    OfferPresentationWayId = offer.Tender.OfferPresentationWayId,
                    FinantialOfferStatusId = offer.OfferAcceptanceStatusId,
                    FinantialRejectionReason = offer.FinantialRejectionReason,
                    FinantialNotes = offer.FinantialNotes,
                    OfferAcceptanceStatusId = offer.OfferAcceptanceStatusId,
                    OfferTechnicalEvaluationStatusId = offer.OfferTechnicalEvaluationStatusId,
                    Notes = offer.Notes,
                    FinantialOfferStatusString = offer.OfferAcceptanceStatusId == (int)Enums.OfferAcceptanceStatusId.AcceptedOffer ? Resources.OfferResources.DisplayInputs.AccepteOffer : Resources.OfferResources.DisplayInputs.RejectedOffer,
                    TechnicalOfferStatusId = offer.OfferTechnicalEvaluationStatusId,
                    TechnicalOfferStatusString = offer.OfferTechnicalEvaluationStatusId == (int)Enums.OfferTechnicalEvaluationStatusId.IdenticalOffer ? Resources.OfferResources.DisplayInputs.Matching : Resources.OfferResources.DisplayInputs.NotMatching,
                    RejectionReason = offer.RejectionReason,
                    ExclusionReason = offer.ExclusionReason,
                    IsExclusionReasonEmpty = string.IsNullOrEmpty(offer.ExclusionReason),
                    CombinedOwner = offer.Combined.Count() == 1,
                    CombinedIdString = Util.Encrypt(offer.Combined.Where(c => c.SolidarityTypeId == (int)UnRegisteredSuppliersInvitationType.SolidarityLeader).Select(x => x.Id).FirstOrDefault()),
                    TechniciansReportAttachments = offer.TechnicianReportAttachments.Select(a => new TechniciansReportAttachmentModel
                    {
                        AttachmentId = a.AttachmentId,
                        Name = a.Name,
                        FileNetReferenceId = a.FileNetReferenceId,
                        OfferId = a.OfferId,
                        AttachmentTypeId = a.AttachmentTypeId
                    }).ToList(),
                    TenderStatusId = offer.Tender.TenderStatusId,
                    OfferId = offer.OfferId,
                    TenderId = offer.TenderId,
                    TenderIdString = Util.Encrypt(offer.TenderId),
                    TechniciansReportAttachmentsRef = string.Join(",", offer.TechnicianReportAttachments.Select(d => d.FileNetReferenceId + ":" + d.Name).ToList()),

                    OffersCheckingDateTime = offer.Tender.OffersCheckingDate,
                    IsOpened = offer.IsOpened.HasValue ? offer.IsOpened.Value ? Resources.SharedResources.DisplayInputs.Yes : Resources.SharedResources.DisplayInputs.No : "",
                    IsValidToApplyOfferLocalContentChanges = offer.Tender.CreatedAt > localContentSetting.Date && offer.OfferlocalContentDetails != null,
                    IsSupplierBindedToBaseLine = offer.OfferlocalContentDetails.IsBindedToTheLowestBaseLine,
                    IsSupplierBindedToLocalContent = offer.OfferlocalContentDetails.IsBindedToTheLowestLocalContent,
                }).FirstOrDefaultAsync();
            return result;
        }

        public async Task<SupplierCombinedDetail> GetCombinedOfferDetail(int combinedId)
        {
            var res = await _context.SupplierCombinedDetails
                .Include(d => d.Combined)
                .ThenInclude(d => d.Offer)
                .ThenInclude(a => a.SupplierTenderQuantityTables)
                .ThenInclude(a => a.QuantitiyItemsJson)
                .Include(d => d.Combined)
                .ThenInclude(d => d.Offer)
                .ThenInclude(x => x.TechnicianReportAttachments)
                .Include(d => d.Combined)
                .ThenInclude(d => d.Offer)
                .ThenInclude(x => x.Tender)
                .Include(x => x.Combined)
                .ThenInclude(d => d.Offer)
                .ThenInclude(b => b.BankGuaranteeDetails)
                .Include(d => d.Combined)
                .ThenInclude(d => d.Offer)
                .ThenInclude(x => x.OfferlocalContentDetails)
                .Include(d => d.SpecificationDetails)
                .Where(d => d.Combined.Id == combinedId)
                .FirstOrDefaultAsync();
            if (res != null)
            {
                var tenderActivities = await _context.TenderActivities.Include(a => a.Activity).ThenInclude(m => m.ActivityTemplateVersions).Where(a => a.TenderId == res.Combined.Offer.TenderId).ToListAsync();
                res.Combined.Offer.Tender.TenderActivities.AddRange(tenderActivities);
            }
            return res;
        }

        public async Task<SupplierCombinedDetail> GetCombinedOfferDetailForVRO(int combinedId)
        {
            var res = await _context.SupplierCombinedDetails
                .Include(d => d.Combined)
                .ThenInclude(d => d.Offer)
                .ThenInclude(a => a.SupplierTenderQuantityTables)
                .ThenInclude(a => a.QuantitiyItemsJson)
                .Include(d => d.Combined)
                .ThenInclude(d => d.Offer)
                .ThenInclude(x => x.Tender)
                .Include(x => x.Combined)
                .ThenInclude(d => d.Offer)
                .ThenInclude(b => b.BankGuaranteeDetails)
                .Include(d => d.SpecificationDetails)
                .Where(d => d.Combined.Id == combinedId)
                .FirstOrDefaultAsync();
            return res;
        }

        public async Task<OfferSolidarity> GetCombinedbyId(int combinedId)
        {
            var res = await _context.OfferSolidarities
                    .Include(a => a.Offer)
                    .ThenInclude(a => a.SupplierTenderQuantityTables)
                    .ThenInclude(a => a.QuantitiyItemsJson)
                    .Include(a => a.Offer)
                    .ThenInclude(x => x.Tender)
                    .Include(a => a.Offer)
                    .ThenInclude(b => b.BankGuaranteeDetails)
                    .Include(a => a.Offer)
                    .ThenInclude(b => b.OfferlocalContentDetails)
                .Where(x => x.Id == combinedId).FirstOrDefaultAsync();
            if (res != null)
            {
                var tenderActivities = await _context.TenderActivities.Include(a => a.Activity).Where(a => a.TenderId == res.Offer.TenderId).ToListAsync();
                res.Offer.Tender.TenderActivities.AddRange(tenderActivities);
            }
            return res;
        }

        public async Task<OpenOfferModel> OpenOffersDetailsForAwarding(int offerid)
        {
            var Query = await _context.Offers
                     .Where(x => x.OfferId == offerid)
                        .Select(off => new OpenOfferModel
                        {
                            OfferAttachmentModels = off.Attachment.Select(s => new OfferAttachmentModel
                            {
                                FileName = s.FileName,
                                FileNetId = s.FileNetReferenceId,
                                FiletypeName = DefineAttachmentTypeName(s),
                                FiletypeID = DefineAttachmentTypeId(s),
                                TenderStatusId = off.Tender.TenderStatusId,
                                OfferPresentationWayId = off.Tender.OfferPresentationWayId
                            }).ToList(),
                            OfferIdString = Util.Encrypt(off.OfferId),
                            TenderIdString = Util.Encrypt(off.TenderId),
                            IsSolidriaty = off.IsSolidarity,
                            TenderDataTabModel = new TenderDataTabModel { TenderName = off.Tender.TenderName, RefNumber = off.Tender.ReferenceNumber, tenderIdString = Util.Encrypt(off.Tender.TenderId), tenderStatus = (Enums.TenderStatus)off.Tender.TenderStatusId, OfferPresentationWayId = (Enums.OfferPresentationWayId)off.Tender.OfferPresentationWayId, TenderTypeId = off.Tender.TenderTypeId },
                            CombinedSupplierModel =
                            off.Combined.Select(y => new CombinedSupplierModel
                            {
                                SupplierName = (y.Supplier.SelectedCrName)
                            }).ToList(),
                            OfferStatus = (OfferStatus)off.OfferStatusId,
                            QuantityTableforInsertModel = new QuantityTableforInsertModel { tenderIdString = Util.Encrypt(off.TenderId), offerIdString = Util.Encrypt(off.OfferId) }
                        }).FirstOrDefaultAsync();
            return Query;
        }

        public async Task<OfferDetailModel> GetTenderStatus(int tenderId)
        {
            var oldflow = _rootConfiguration.OldFlow;
            var oldflowdate = _rootConfiguration.OldFlow.EndDate.ToDateTime();
            return await _context.Tenders.Where(t => t.TenderId == tenderId).Select(x => new OfferDetailModel
            {
                TenderStatusId = x.TenderStatusId,
                isOldFlow = oldflow.isApplied ? x.IsOldFlow(oldflowdate) : false
            }).FirstOrDefaultAsync();
        }

        public async Task<List<Offer>> GetIdenticalOffersByTenderId(int tenderId)
        {
            var offers = await _context.Offers.Where(x => x.IsActive == true && x.TenderId == tenderId && x.OfferTechnicalEvaluationStatusId == (int)OfferTechnicalEvaluationStatusId.IdenticalOffer).ToListAsync();
            return offers;
        }
        public async Task<List<Offer>> GetReceivedAndIdenticalOffersByTenderId(int tenderId)
        {
            var offers = await _context.Offers.Where(x => x.IsActive == true && x.TenderId == tenderId && x.OfferStatusId == (int)Enums.OfferStatus.Received && x.OfferTechnicalEvaluationStatusId == (int)OfferTechnicalEvaluationStatusId.IdenticalOffer).ToListAsync();
            return offers;
        }
        public async Task<List<Offer>> GetNotIdenticalOffersByTenderId(int tenderId)
        {
            var offers = await _context.Offers.Where(x => x.IsActive == true && x.TenderId == tenderId && x.OfferStatusId == (int)Enums.OfferStatus.Received && x.OfferTechnicalEvaluationStatusId == (int)OfferTechnicalEvaluationStatusId.NotIdenticalOffer).ToListAsync();
            return offers;
        }

        public async Task<List<Offer>> GetFinancialAcceeptedOffersByTenderId(int tenderId)
        {
            var offers = await _context.Offers
                .Where(x => x.IsActive == true && x.TenderId == tenderId
                && x.OfferStatusId == (int)Enums.OfferStatus.Received
                && x.OfferAcceptanceStatusId == (int)Enums.OfferAcceptanceStatusId.AcceptedOffer)
                .ToListAsync();
            return offers;
        }

        public async Task<List<Offer>> GetFinancialRejectedOffersByTenderId(int tenderId)
        {
            var offers = await _context.Offers
                .Where(x => x.IsActive == true && x.TenderId == tenderId
                && x.OfferStatusId == (int)Enums.OfferStatus.Received
                && x.OfferAcceptanceStatusId == (int)Enums.OfferAcceptanceStatusId.RejectedOffer)
                .ToListAsync();
            return offers;
        }
        public async Task<List<Offer>> GetReceivedOffersByTenderId(int tenderId)
        {
            var offers = await _context.Offers.Where(x => x.TenderId == tenderId && x.OfferStatusId == (int)Enums.OfferStatus.Received && x.IsActive == true).ToListAsync();
            return offers;
        }

        public List<SupplierAttachment> GetDeletedAttachment(int attachmentId, Offer offer)
        {
            return offer.Attachment.Where(x => x.AttachmentId == attachmentId).ToList();
        }


        public async Task<List<NegotiationOfferModel>> GetOffersForNegotiationByTenderId(int TenderId, decimal Discount)
        {
            var Query = await _context.Offers
                               .Where(e => e.IsActive == true
                               && e.TenderId == TenderId

                               && e.OfferStatusId == (int)Enums.OfferStatus.Received
                               && e.OfferTechnicalEvaluationStatusId == (int)OfferTechnicalEvaluationStatusId.IdenticalOffer)
                               .Select(s => new NegotiationOfferModel
                               {
                                   AmountAfterNegotiationDiscountText = (new ConvertNumberToText(CalculateQuantityTableFinalPrice(s.FinalPriceAfterDiscount ?? 0, Discount, s.negotiationFirstStageSuppliers.Where(d => d.IsActive == true).FirstOrDefault()), new CurrencyInfo(CurrencyInfo.Currencies.SaudiArabia)).ConvertToArabic()),
                                   AmountAfterNegotiationDiscount = CalculateQuantityTableFinalPrice(s.FinalPriceAfterDiscount ?? 0, Discount, s.negotiationFirstStageSuppliers.FirstOrDefault()),
                                   OfferAmount = GetOfferAmount(s.negotiationFirstStageSuppliers.Where(d => d.IsActive == true).FirstOrDefault(), s.FinalPriceAfterDiscount ?? 0),
                                   BuyStatus = s.Tender.Invitations.Count > 0 && s.Tender.ConditionsBooklets.Count == 0 ?
                                   s.Tender.Invitations.FirstOrDefault(x => x.IsActive == true && x.CommericalRegisterNo == s.CommericalRegisterNo).InvitationStatus.NameAr :
                                   s.Tender.ConditionsBooklets.FirstOrDefault(x => x.IsActive == true && x.CommericalRegisterNo == s.CommericalRegisterNo && x.BillInfo != null).BillInfo.BillStatusId == (int)BillStatus.Paid ? Resources.TenderResources.DisplayInputs.Purchased : Resources.TenderResources.DisplayInputs.NotPurchased,
                                   statusName = s.negotiationFirstStageSuppliers.Where(d => d.IsActive == true).FirstOrDefault().NegotiationSupplierStatus.Name ?? "لم يتم ارسال الطلب",
                                   CommercialCompanyName = s.Supplier.SelectedCrName,
                                   CommericalNumber = s.CommericalRegisterNo,
                                   offerIdString = Util.Encrypt(s.OfferId),
                                   OfferCheck = s.OfferAcceptanceStatusId == (int)OfferTechnicalEvaluationStatusId.IdenticalOffer ? Resources.SharedResources.DisplayInputs.Accepted : Resources.SharedResources.DisplayInputs.NotAccepeted,
                                   OfferNumber = s.OfferLetterNumber,
                                   TechnicalCheck = s.OfferTechnicalEvaluationStatusId == (int)OfferTechnicalEvaluationStatusId.IdenticalOffer ? Resources.OfferResources.DisplayInputs.Matching : Resources.OfferResources.DisplayInputs.NotMatching,
                                   tenderIdString = Util.Encrypt(s.TenderId),
                                   solidarityIdString = s.Combined.Where(x => x.SolidarityTypeId == (int)UnRegisteredSuppliersInvitationType.SolidarityLeader).Count() == 1 ? Util.Encrypt(s.Combined.Where(a => a.OfferId == s.OfferId && (a.SolidarityTypeId == (int)UnRegisteredSuppliersInvitationType.SolidarityLeader) && a.StatusId == (int)SupplierSolidarityStatus.Approved).Select(x => x.Id).FirstOrDefault()) : "",
                                   offerPresentationWay = s.OfferPresentationWayId.Value
                               }).ToListAsync();
            return Query;
        }

        public async Task<QueryResult<NegotiationOfferModel>> GetOffersListForFirstStageNegotiation(NegotiationOffersSearchModel negotiationOffersSearchModel)
        {
            int versionId = (int)Enums.QuantityTableVersion.Version2;

            var Query = await _context.Offers
                               .Where(e => e.IsActive == true && e.OfferStatusId == (int)Enums.OfferStatus.Received
                               && e.TenderId == Util.Decrypt(negotiationOffersSearchModel.TenderIdString)
                               && e.OfferTechnicalEvaluationStatusId == (int)OfferTechnicalEvaluationStatusId.IdenticalOffer).OrderBy(s => s.Tender.TenderActivities.Any(d => d.Activity.ActivityTemplateVersions.Any(t => t.TemplateId == (int)Enums.ConditionsTemplateCategory.GeneralSuppliesSupply)) ? s.OfferWeightAfterCalcNPA : s.FinalPriceAfterDiscount)
                               .Select(s => new NegotiationOfferModel
                               {
                                   AmountAfterNegotiationDiscountText = (new ConvertNumberToText(CalculateQuantityTableFinalPrice(s.FinalPriceAfterDiscount ?? 0, negotiationOffersSearchModel.DiscountValue, s.negotiationFirstStageSuppliers.Where(d => d.IsActive == true).FirstOrDefault()), new CurrencyInfo(CurrencyInfo.Currencies.SaudiArabia)).ConvertToArabic()),
                                   AmountAfterNegotiationDiscount = CalculateQuantityTableFinalPrice(s.FinalPriceAfterDiscount ?? 0, negotiationOffersSearchModel.DiscountValue, s.negotiationFirstStageSuppliers.FirstOrDefault()),
                                   OfferAmount = GetOfferAmount(s.negotiationFirstStageSuppliers.Where(d => d.IsActive == true).FirstOrDefault(), s.FinalPriceAfterDiscount ?? 0),
                                   AmountAfterNegotiationDiscountNP = CalculateQuantityTableFinalPrice(s.OfferWeightAfterCalcNPA ?? 0, negotiationOffersSearchModel.DiscountValue, s.negotiationFirstStageSuppliers.FirstOrDefault()),
                                   OfferAmountNP = GetOfferAmount(s.negotiationFirstStageSuppliers.Where(d => d.IsActive == true).FirstOrDefault(), s.OfferWeightAfterCalcNPA ?? 0),
                                   IsTawreed = s.Tender.QuantityTableVersionId == versionId,
                                   BuyStatus = s.Tender.Invitations.Count > 0 && s.Tender.ConditionsBooklets.Count == 0 ?
                                s.Tender.Invitations.FirstOrDefault(x => x.IsActive == true && x.CommericalRegisterNo == s.CommericalRegisterNo).InvitationStatus.NameAr :
                                s.Tender.ConditionsBooklets.FirstOrDefault(x => x.IsActive == true && x.CommericalRegisterNo == s.CommericalRegisterNo && x.BillInfo != null).BillInfo.BillStatusId == (int)Enums.BillStatus.Paid ? Resources.TenderResources.DisplayInputs.Purchased : Resources.TenderResources.DisplayInputs.NotPurchased,
                                   statusName = s.negotiationFirstStageSuppliers.Where(d => d.IsActive == true).FirstOrDefault().NegotiationSupplierStatus.Name ?? "لم يتم ارسال الطلب",
                                   CommercialCompanyName = s.Supplier.SelectedCrName,
                                   CommericalNumber = s.CommericalRegisterNo,
                                   offerIdString = Util.Encrypt(s.OfferId),
                                   OfferCheck = s.OfferAcceptanceStatusId == (int)OfferTechnicalEvaluationStatusId.IdenticalOffer ? Resources.SharedResources.DisplayInputs.Accepted : Resources.SharedResources.DisplayInputs.NotAccepeted,
                                   OfferNumber = s.OfferLetterNumber,
                                   TechnicalCheck = s.OfferTechnicalEvaluationStatusId == (int)OfferTechnicalEvaluationStatusId.IdenticalOffer ? Resources.OfferResources.DisplayInputs.Matching : Resources.OfferResources.DisplayInputs.NotMatching,
                                   tenderIdString = Util.Encrypt(s.TenderId),
                                   solidarityIdString = s.Combined.Where(x => x.SolidarityTypeId == (int)UnRegisteredSuppliersInvitationType.SolidarityLeader).Count() == 1 ? Util.Encrypt(s.Combined.Where(a => a.OfferId == s.OfferId && (a.SolidarityTypeId == (int)Enums.UnRegisteredSuppliersInvitationType.SolidarityLeader) && a.StatusId == (int)Enums.SupplierSolidarityStatus.Approved).Select(x => x.Id).FirstOrDefault()) : "",
                                   offerPresentationWay = s.OfferPresentationWayId.Value
                               }).ToQueryResult(negotiationOffersSearchModel.PageNumber, negotiationOffersSearchModel.PageSize);
            return Query;


        }


        public async Task<QueryResult<NegotiationOfferModel>> GetOffersForFirstStageNegotiation(NegotiationOffersSearchModel negotiationOffersSearchModel)
        {
            int versionId = (int)Enums.QuantityTableVersion.Version2;
            negotiationOffersSearchModel.DiscountValue = decimal.Parse(negotiationOffersSearchModel.DiscountValue.ToString("0.00"));
            var Query = await _context.Offers
                               .Where(e => e.IsActive == true && e.OfferStatusId == (int)Enums.OfferStatus.Received
                               && e.TenderId == Util.Decrypt(negotiationOffersSearchModel.TenderIdString) && e.negotiationFirstStageSuppliers.Any(x => x.NegotiationId == Util.Decrypt(negotiationOffersSearchModel.NegotiationIdString))
                               && e.OfferTechnicalEvaluationStatusId == (int)OfferTechnicalEvaluationStatusId.IdenticalOffer && e.OfferAcceptanceStatusId == (int)Enums.OfferAcceptanceStatusId.AcceptedOffer)
                               .OrderBy(s => (!string.IsNullOrEmpty(s.OfferWeightAfterCalcNPA.ToString()) && s.Tender.TenderActivities.Any(d => d.Activity.ActivityTemplateVersions.Any(t => t.TemplateId == (int)Enums.ConditionsTemplateCategory.GeneralSuppliesSupply))) ? s.OfferWeightAfterCalcNPA : s.FinalPriceAfterDiscount)
                               .Select(s => new NegotiationOfferModel
                               {
                                   AmountAfterNegotiationDiscountText = (new ConvertNumberToText(negotiationOffersSearchModel.DiscountValue, new CurrencyInfo(CurrencyInfo.Currencies.SaudiArabia)).ConvertToArabic()),
                                   AmountAfterNegotiationDiscount = negotiationOffersSearchModel.DiscountValue,
                                   DiscountPercent = GetDiscountPercentAfterDiseredAmount(s.negotiationFirstStageSuppliers.FirstOrDefault(d => d.IsActive == true), s.FinalPriceAfterDiscount ?? 0, negotiationOffersSearchModel.DiscountValue),
                                   OfferAmount = GetOfferAmount(s.negotiationFirstStageSuppliers.FirstOrDefault(d => d.IsActive == true), s.FinalPriceAfterDiscount ?? 0),
                                   AmountAfterNegotiationDiscountNP = negotiationOffersSearchModel.DiscountValue,
                                   OfferAmountNP = GetOfferAmount(s.negotiationFirstStageSuppliers.FirstOrDefault(d => d.IsActive == true), s.OfferWeightAfterCalcNPA ?? 0),
                                   IsTawreed = s.Tender.QuantityTableVersionId == versionId,
                                   BuyStatus = s.Tender.Invitations.Count > 0 && s.Tender.ConditionsBooklets.Count == 0 ?
                                s.Tender.Invitations.FirstOrDefault(x => x.IsActive == true && x.CommericalRegisterNo == s.CommericalRegisterNo).InvitationStatus.NameAr :
                                s.Tender.ConditionsBooklets.FirstOrDefault(x => x.IsActive == true && x.CommericalRegisterNo == s.CommericalRegisterNo && x.BillInfo != null).BillInfo.BillStatusId == (int)Enums.BillStatus.Paid ? Resources.TenderResources.DisplayInputs.Purchased : Resources.TenderResources.DisplayInputs.NotPurchased,
                                   statusName = s.negotiationFirstStageSuppliers.FirstOrDefault(d => d.IsActive == true).NegotiationSupplierStatus.Name ?? "لم يتم ارسال الطلب",
                                   CommercialCompanyName = s.Supplier.SelectedCrName,
                                   CommericalNumber = s.CommericalRegisterNo,
                                   offerIdString = Util.Encrypt(s.OfferId),
                                   OfferCheck = s.OfferAcceptanceStatusId == (int)OfferTechnicalEvaluationStatusId.IdenticalOffer ? Resources.SharedResources.DisplayInputs.Accepted : Resources.SharedResources.DisplayInputs.NotAccepeted,
                                   OfferNumber = s.OfferLetterNumber,
                                   TechnicalCheck = s.OfferTechnicalEvaluationStatusId == (int)OfferTechnicalEvaluationStatusId.IdenticalOffer ? Resources.OfferResources.DisplayInputs.Matching : Resources.OfferResources.DisplayInputs.NotMatching,
                                   tenderIdString = Util.Encrypt(s.TenderId),
                                   solidarityIdString = s.Combined.Where(x => x.SolidarityTypeId == (int)UnRegisteredSuppliersInvitationType.SolidarityLeader).Count() == 1 ? Util.Encrypt(s.Combined.Where(a => a.OfferId == s.OfferId && (a.SolidarityTypeId == (int)Enums.UnRegisteredSuppliersInvitationType.SolidarityLeader) && a.StatusId == (int)Enums.SupplierSolidarityStatus.Approved).Select(x => x.Id).FirstOrDefault()) : "",
                                   offerPresentationWay = s.OfferPresentationWayId.Value
                               }).ToQueryResult(negotiationOffersSearchModel.PageNumber, negotiationOffersSearchModel.PageSize);
            return Query;


        }


        public async Task<QueryResult<NegotiationOfferModel>> GetNotNegotitedOffersForFirstStageNegotiation(NegotiationOffersSearchModel negotiationOffersSearchModel)
        {
            int versionId = (int)Enums.QuantityTableVersion.Version2;
            negotiationOffersSearchModel.DiscountValue = decimal.Parse(negotiationOffersSearchModel.DiscountValue.ToString("0.00"));
            var Query = await _context.Offers
                               .Where(e => e.IsActive == true && e.OfferStatusId == (int)Enums.OfferStatus.Received
                               && e.TenderId == Util.Decrypt(negotiationOffersSearchModel.TenderIdString)
                               && e.OfferTechnicalEvaluationStatusId == (int)OfferTechnicalEvaluationStatusId.IdenticalOffer && e.OfferAcceptanceStatusId == (int)Enums.OfferAcceptanceStatusId.AcceptedOffer)
                               .Where(x => !x.negotiationFirstStageSuppliers.Any() || x.negotiationFirstStageSuppliers.Any(s => s.IsActive == true && s.NegotiationSupplierStatusId == (int)Enums.enNegotiationSupplierStatus.NotSent))
                               .OrderBy(s => (!string.IsNullOrEmpty(s.OfferWeightAfterCalcNPA.ToString()) && s.OfferWeightAfterCalcNPA != 0 && s.Tender.TenderActivities.Any(d => d.Activity.ActivityTemplateVersions.Any(t => t.TemplateId == (int)Enums.ConditionsTemplateCategory.GeneralSuppliesSupply))) ? s.OfferWeightAfterCalcNPA : s.FinalPriceAfterDiscount)
                               .Select(s => new NegotiationOfferModel
                               {
                                   AmountAfterNegotiationDiscountText = (new ConvertNumberToText(negotiationOffersSearchModel.DiscountValue, new CurrencyInfo(CurrencyInfo.Currencies.SaudiArabia)).ConvertToArabic()),
                                   AmountAfterNegotiationDiscount = negotiationOffersSearchModel.DiscountValue,
                                   DiscountPercent = GetDiscountPercentAfterDiseredAmount(s.negotiationFirstStageSuppliers.FirstOrDefault(d => d.IsActive == true), s.FinalPriceAfterDiscount ?? 0, negotiationOffersSearchModel.DiscountValue), //(s.FinalPriceAfterDiscount - negotiationOffersSearchModel.DiscountValue) / (s.FinalPriceAfterDiscount) * 100,
                                   OfferAmount = GetOfferAmount(s.negotiationFirstStageSuppliers.Where(d => d.IsActive == true).FirstOrDefault(), s.FinalPriceAfterDiscount ?? 0),
                                   AmountAfterNegotiationDiscountNP = negotiationOffersSearchModel.DiscountValue, //CalculateQuantityTableFinalPrice(s.OfferWeightAfterCalcNPA ?? 0, negotiationOffersSearchModel.DiscountValue, s.negotiationFirstStageSuppliers.FirstOrDefault()),
                                   OfferAmountNP = GetOfferAmount(s.negotiationFirstStageSuppliers.Where(d => d.IsActive == true).FirstOrDefault(), s.OfferWeightAfterCalcNPA ?? 0),
                                   IsTawreed = s.Tender.QuantityTableVersionId == versionId,
                                   BuyStatus = s.Tender.Invitations.Count > 0 && s.Tender.ConditionsBooklets.Count == 0 ?
                                s.Tender.Invitations.FirstOrDefault(x => x.IsActive == true && x.CommericalRegisterNo == s.CommericalRegisterNo).InvitationStatus.NameAr :
                                s.Tender.ConditionsBooklets.FirstOrDefault(x => x.IsActive == true && x.CommericalRegisterNo == s.CommericalRegisterNo && x.BillInfo != null).BillInfo.BillStatusId == (int)Enums.BillStatus.Paid ? Resources.TenderResources.DisplayInputs.Purchased : Resources.TenderResources.DisplayInputs.NotPurchased,
                                   statusName = s.negotiationFirstStageSuppliers.FirstOrDefault(d => d.IsActive == true).NegotiationSupplierStatus.Name ?? "لم يتم ارسال الطلب",
                                   CommercialCompanyName = s.Supplier.SelectedCrName,
                                   CommericalNumber = s.CommericalRegisterNo,
                                   offerIdString = Util.Encrypt(s.OfferId),
                                   OfferCheck = s.OfferAcceptanceStatusId == (int)OfferTechnicalEvaluationStatusId.IdenticalOffer ? Resources.SharedResources.DisplayInputs.Accepted : Resources.SharedResources.DisplayInputs.NotAccepeted,
                                   OfferNumber = s.OfferLetterNumber,
                                   TechnicalCheck = s.OfferTechnicalEvaluationStatusId == (int)OfferTechnicalEvaluationStatusId.IdenticalOffer ? Resources.OfferResources.DisplayInputs.Matching : Resources.OfferResources.DisplayInputs.NotMatching,
                                   tenderIdString = Util.Encrypt(s.TenderId),
                                   solidarityIdString = s.Combined.Where(x => x.SolidarityTypeId == (int)UnRegisteredSuppliersInvitationType.SolidarityLeader).Count() == 1 ? Util.Encrypt(s.Combined.Where(a => a.OfferId == s.OfferId && (a.SolidarityTypeId == (int)Enums.UnRegisteredSuppliersInvitationType.SolidarityLeader) && a.StatusId == (int)Enums.SupplierSolidarityStatus.Approved).Select(x => x.Id).FirstOrDefault()) : "",
                                   offerPresentationWay = s.OfferPresentationWayId.Value
                               }).ToQueryResult(negotiationOffersSearchModel.PageNumber, negotiationOffersSearchModel.PageSize);
            return Query;


        }


        public async Task<List<NegotiationFirstStageSupplier>> GetNegotiationFirstStageSuppliersByTenderId(int tenderId)
        {
            return await _context.NegotiationFirstStageSuppliers.Where(a => a.Offer.TenderId == tenderId).ToListAsync();
        }

        private static decimal GetOfferAmount(NegotiationFirstStageSupplier supplierfirstStageNegotiation, decimal offerAmount)
        {
            if (supplierfirstStageNegotiation != null && (supplierfirstStageNegotiation.NegotiationSupplierStatusId == (int)enNegotiationSupplierStatus.Agree || supplierfirstStageNegotiation.NegotiationSupplierStatusId == (int)enNegotiationSupplierStatus.AgreeWithExtraDiscount))
            {
                return supplierfirstStageNegotiation.offerOriginalAmount;
            }
            else
            {
                return offerAmount;
            }
        }
        private static decimal GetDiscountPercentAfterDiseredAmount(NegotiationFirstStageSupplier supplierfirstStageNegotiation, decimal offerAmount, decimal discountValue)
        {

            if (supplierfirstStageNegotiation != null && (supplierfirstStageNegotiation.NegotiationSupplierStatusId == (int)enNegotiationSupplierStatus.Agree || supplierfirstStageNegotiation.NegotiationSupplierStatusId == (int)enNegotiationSupplierStatus.AgreeWithExtraDiscount))
            {
                var discValue = (supplierfirstStageNegotiation.offerOriginalAmount - discountValue) / (supplierfirstStageNegotiation.offerOriginalAmount) * 100;
                return discValue;
            }
            else
            {
                var discValue = (offerAmount - discountValue) / (offerAmount) * 100;
                return discValue;
            }
        }
        private static decimal CalculateQuantityTableFinalPrice(decimal finalAmount, decimal Discount, NegotiationFirstStageSupplier supplierfirstStageNegotiation)
        {
            if (supplierfirstStageNegotiation == null)
            {
                return (finalAmount - ((Discount * finalAmount) / 100));
            }
            else if (supplierfirstStageNegotiation.NegotiationSupplierStatusId == (int)enNegotiationSupplierStatus.Agree)
            {
                return finalAmount;
            }
            else
            {
                return (finalAmount - ((Discount * finalAmount) / 100));
            }
        }

        public async Task<List<Offer>> GetOffersForSecondNegotiationByTenderId(int tenderId)
        {
            var Query = await _context.Offers.Include(t => t.SupplierTenderQuantityTables)
                                        .ThenInclude(t => t.QuantitiyItemsJson)
                                        .Include(t => t.Tender)
                                        .ThenInclude(c => c.ConditionsBooklets)
                                        .ThenInclude(b => b.BillInfo)
                                        .Include(t => t.Tender)
                                        .ThenInclude(i => i.Invitations)
                                        .ThenInclude(s => s.InvitationStatus)
                                        .Include(t => t.Tender)
                                        .ThenInclude(d => d.TenderActivities)
                                        .Include(d => d.Supplier)
                                        .Include(d => d.Combined)
                .Where(e => e.IsActive == true && e.TenderId == tenderId && e.OfferStatusId == (int)Enums.OfferStatus.Received)
                .Where(e => e.IsActive == true && e.OfferTechnicalEvaluationStatusId == (int)Enums.OfferTechnicalEvaluationStatusId.IdenticalOffer)
                .ToListAsync();
            return Query;
        }

        public async Task<List<OfferSolidarity>> GetOtherOfferSolidarity(int offerid, int tenderId)
        {
            var combinedSuppliers = await _context.OfferSolidarities.Where(w => w.Offer.Tender.TenderId == tenderId &&
           w.IsActive == true && w.Offer.IsActive == true && (w.Offer.OfferStatusId == (int)Enums.OfferStatus.Received && w.OfferId != offerid)).ToListAsync();
            return combinedSuppliers;
        }
        public async Task<List<NegotiationAgencySupplierOfferList>> GetOffersForFirstStageNegotiationByTenderId(int TenderId)
        {
            var Query = await _context.Offers/*.Include(d => d.QuantityTable)*/.Include(f => f.Combined).Where(d => d.OfferStatusId == (int)Enums.OfferStatus.Received)
              // .Where(a => !a.Tender.AgencyCommunicationRequests.Any(g => g.StatusId == (int)Enums.AgencyCommunicationRequestStatus.Approved && g.AgencyRequestTypeId == (int)Enums.AgencyCommunicationRequestType.ExtendOfferValidtiy) ||
              //a.Tender.AgencyCommunicationRequests.Any(g => g.AgencyRequestTypeId == (int)Enums.AgencyCommunicationRequestType.ExtendOfferValidtiy
              // && g.ExtendOffersValidity.ExtendOffersValiditySuppliers.Any(e => e.SupplierCR == a.CommericalRegisterNo &&
              //(e.SupplierExtendOfferValidityStatusId != (int)Enums.SupplierExtendOffersValidityStatus.Rejected || g.ExtendOffersValidity.NewOffersExpiryDate > DateTime.Now || e.SupplierExtendOfferValidityStatusId == (int)Enums.SupplierExtendOffersValidityStatus.Accepted))))
              .Where(e => e.IsActive == true
                 && e.OfferTechnicalEvaluationStatusId == (int)OfferTechnicalEvaluationStatusId.IdenticalOffer).Include(d => d.Supplier).Include(w => w.Tender).Where(w => w.TenderId == TenderId)
                 .Select(s => new NegotiationAgencySupplierOfferList
                 {
                     OfferAmount = s.FinalPriceAfterDiscount ?? 0,
                     OfferAmountNP = s.OfferWeightAfterCalcNPA ?? 0,
                     OfferId = s.OfferId,
                     CommericalRegisterNo = s.CommericalRegisterNo,
                     SupplierName = s.Supplier.SelectedCrName
                 }).ToListAsync();
            return Query;
        }



        public async Task<List<NegotiationAgencySupplierOfferList>> GetAcceptedOffersForFirstStageNegotiationByTenderId(int tenderId)
        {
            var Query = await _context.Offers.Where(d => d.IsActive == true && d.TenderId == tenderId && d.OfferStatusId == (int)Enums.OfferStatus.Received)
              .Where(e => e.OfferAcceptanceStatusId == (int)Enums.OfferAcceptanceStatusId.AcceptedOffer && e.OfferTechnicalEvaluationStatusId == (int)OfferTechnicalEvaluationStatusId.IdenticalOffer)
              .Where(x => !x.negotiationFirstStageSuppliers.Any() || x.negotiationFirstStageSuppliers.Any(x => x.NegotiationSupplierStatusId == (int)Enums.enNegotiationSupplierStatus.NotSent))
                 .Select(s => new NegotiationAgencySupplierOfferList
                 {
                     OfferAmount = s.FinalPriceAfterDiscount ?? 0,
                     OfferAmountNP = s.OfferWeightAfterCalcNPA ?? 0,
                     OfferId = s.OfferId,
                     CommericalRegisterNo = s.CommericalRegisterNo,
                     SupplierName = s.Supplier.SelectedCrName,
                     IsNegotiated = s.negotiationFirstStageSuppliers.Any(),
                 }).ToListAsync();
            return Query;
        }
        public async Task<List<Offer>> GetNotNegotiatedOffersForFirstStageNegotiationByTenderId(int tenderId)
        {
            var offers = await _context.Offers
                .Include(s => s.negotiationFirstStageSuppliers)
                .Where(o => o.IsActive == true && o.TenderId == tenderId && o.OfferStatusId == (int)Enums.OfferStatus.Received)
                .Where(o => o.OfferAcceptanceStatusId == (int)Enums.OfferAcceptanceStatusId.AcceptedOffer && o.OfferTechnicalEvaluationStatusId == (int)OfferTechnicalEvaluationStatusId.IdenticalOffer)
                .Where(x => !x.negotiationFirstStageSuppliers.Any() || x.negotiationFirstStageSuppliers.Any(x => x.NegotiationSupplierStatusId == (int)Enums.enNegotiationSupplierStatus.NotSent))
                .ToListAsync();

            //var offerIds = new List<int>();
            //foreach (var offer in offers)
            //{
            //    offerIds.Add(offer.OfferId);
            //}

            //var negotiationFirstStageSuppliers = await _context.NegotiationFirstStageSuppliers.Where(n => offerIds.Contains(n.OfferId)).ToListAsync();
            //offers.AddRange(negotiationFirstStageSuppliers.AsEnumerable());
            //offers.Where(o => !o.negotiationFirstStageSuppliers.Any() || o.negotiationFirstStageSuppliers.Any(n => n.NegotiationSupplierStatusId == (int)Enums.enNegotiationSupplierStatus.NotSent))
            return offers;
        }

        #region Check

        public async Task<CheckOfferModel> GetOpenOfferDataForCheckStage(int offerid)
        {
            var Query = await _context.Offers
                        .Where(x => x.OfferId == offerid)
                        .Select(off => new CheckOfferModel
                        {
                            IsSolidarity = off.IsSolidarity,
                            TenderBasicInfo = new CheckOfferTenderBasicInfo { TenderName = off.Tender.TenderName, TenderRefrenceNumber = off.Tender.ReferenceNumber, InsideKSA = off.Tender.InsideKSA, TenderStatusId = off.Tender.TenderStatusId },
                            OfferAttachmentModels = off.Attachment.Select(s => new CheckOfferAttachmentModel
                            {
                                FileName = s.FileName,
                                FileNetId = s.FileNetReferenceId,
                                FiletypeName = DefineAttachmentTypeName(s),
                                FiletypeID = DefineAttachmentTypeId(s),
                                TenderStatusId = off.Tender.TenderStatusId,
                                OfferPresentationWayId = off.Tender.OfferPresentationWayId
                            }).ToList(),
                            OfferIdString = Util.Encrypt(off.OfferId),

                            TenderDataTabModel = new CheckOfferTenderDataTabModel
                            {
                                TenderName = off.Tender.TenderName,
                                RefNumber = off.Tender.ReferenceNumber,
                                tenderIdString = Util.Encrypt(off.Tender.TenderId),
                                tenderStatus = (Enums.TenderStatus)off.Tender.TenderStatusId,
                                OfferPresentationWayId = off.Tender.OfferPresentationWayId.HasValue ? off.Tender.OfferPresentationWayId.Value : 0
                            },
                            CombinedSupplierModels = off.Combined.Select(y => new CheckOfferCombinedSupplierModel
                            {
                                SupplierName = y.Supplier.SelectedCrName
                            }).ToList(),
                            OfferStatus = (Enums.OfferStatus)off.OfferStatusId,
                            QuantityTableforInsertModel = new ViewModel.CheckOfferQuantityTableforInsertModel() { tenderIdString = Util.Encrypt(off.Tender.TenderId), offerIdString = Util.Encrypt(off.OfferId) },
                            TenderAreaNameList = off.Tender.InsideKSA == true ? off.Tender.TenderAreas.Select(y => y.Area.NameAr).ToList() : off.Tender.TenderCountries.Select(y => y.Country.NameAr).ToList(),
                        }).FirstOrDefaultAsync();

            return Query;
        }

        public async Task<QueryResult<CombinedSupplierModel>> GetCombinedSuppliersForCombinedSuppliersCheckingStageAsync(int offerid, int userId)
        {
            var Query = await _context.OfferSolidarities
                               .Where(x =>
                               x.OfferId == offerid &&
                               (x.Offer.Tender.TenderStatusId == (int)Enums.TenderStatus.Approved ||
                               x.Offer.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersOppenedConfirmed ||
                               x.Offer.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersChecking ||
                               x.Offer.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersTechnicalCheckingPending ||
                               x.Offer.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersTechnicalCheckingConfirmed ||
                               x.Offer.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersTechnicalCheckingRejected ||
                               x.Offer.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersOpenFinancialStage ||
                               x.Offer.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersOpenFinancialStagePending ||
                               x.Offer.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersOpenFinancialStageRejected ||
                               x.Offer.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersOpenFinancialStageApproved ||
                               x.Offer.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersFinancialChecking ||
                               x.Offer.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersFinancialOfferCheckingApproved ||
                               x.Offer.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersFinancialOfferCheckingPending ||
                               x.Offer.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersFinancialOfferCheckingRejected ||
                               x.Offer.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersCheckedConfirmed ||
                               x.Offer.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersCheckedPending ||
                               x.Offer.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersCheckedRejected ||
                               x.Offer.Tender.TenderStatusId == (int)Enums.TenderStatus.DirectPurchaseOffersChecking ||
                               x.Offer.Tender.TenderStatusId == (int)Enums.TenderStatus.DirectPurchaseOffersCheckingApproved ||
                               x.Offer.Tender.TenderStatusId == (int)Enums.TenderStatus.DirectPurchaseOffersCheckingApprovePending ||
                               x.Offer.Tender.TenderStatusId == (int)Enums.TenderStatus.DirectPurchaseOffersCheckingPending ||
                               x.Offer.Tender.TenderStatusId == (int)Enums.TenderStatus.DirectPurchaseOffersCheckingRejected ||
                               x.Offer.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersTechnicalOppeningConfirmed ||
                               x.Offer.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersTechnicalChecking) &&
                               (x.Offer.Tender.OffersCheckingCommittee.CommitteeUsers.Any(c =>
                               c.UserProfileId == userId &&
                               (c.UserRoleId == (int)Enums.UserRole.NewMonafasat_OffersCheckSecretary || c.UserRoleId == (int)Enums.UserRole.NewMonafasat_OffersCheckManager))
                               ||
                               x.Offer.Tender.OffersOpeningCommittee.CommitteeUsers.Any(c =>
                               c.UserProfileId == userId &&
                               (c.UserRoleId == (int)Enums.UserRole.NewMonafasat_OffersOpeningSecretary || c.UserRoleId == (int)Enums.UserRole.NewMonafasat_OffersOpeningManager))
                               ||
                               x.Offer.Tender.DirectPurchaseCommittee.CommitteeUsers.Any(c =>
                                c.UserProfileId == userId &&
                                (c.UserRoleId == (int)Enums.UserRole.NewMonafasat_ManagerDirtectPurshasingCommittee || c.UserRoleId == (int)Enums.UserRole.NewMonafasat_SecretaryDirtectPurshasingCommittee))))
                                                                                       .Select(comb => new CombinedSupplierModel
                                                                                       {
                                                                                           CombinedIdString = Util.Encrypt(comb.Id),
                                                                                           CombinedId = comb.Id,
                                                                                           IsOwner = comb.SolidarityTypeId == (int)Enums.UnRegisteredSuppliersInvitationType.SolidarityLeader,
                                                                                           roleName = (comb.SolidarityTypeId == (int)Enums.UnRegisteredSuppliersInvitationType.SolidarityLeader) ? Resources.OfferResources.DisplayInputs.SolidarityLeader : Resources.OfferResources.DisplayInputs.Solidarity,
                                                                                           SupplierName = comb.Supplier.SelectedCrName,
                                                                                           SupplierCr = comb.CRNumber,
                                                                                           OfferPresentationWayId = (int)comb.Offer.OfferPresentationWayId
                                                                                       }).ToQueryResult();
            return Query;
        }


        public async Task<OfferSolidarity> GetCombinedWithOfferandTenderById(int combinedId)
        {

            var offer = await _context.OfferSolidarities.Include(f => f.Offer).ThenInclude(g => g.Tender).Where(e => e.Id == combinedId && (e.StatusId == (int)Enums.SupplierSolidarityStatus.New)).FirstOrDefaultAsync();
            return offer;
        }

        #endregion
        public async Task<Offer> GetTheLowestOffer(int tenderId)
        {
            int _versionId = (int)Enums.QuantityTableVersion.Version2;
            var result = await _context.Offers.Include(a => a.SupplierTenderQuantityTables).ThenInclude(a => a.QuantitiyItemsJson)
                    .Where(t => t.OfferStatusId == (int)Enums.OfferStatus.Received && t.IsActive == true && t.TenderId == tenderId)
                    .Where(t => t.OfferTechnicalEvaluationStatusId == (int)Enums.OfferTechnicalEvaluationStatusId.IdenticalOffer)
                    .OrderBy(s => s.Tender.QuantityTableVersionId == _versionId ? s.OfferWeightAfterCalcNPA : s.FinalPriceAfterDiscount).ThenBy(d => d.OfferId)
                .FirstOrDefaultAsync();
            return result;
        }
        public async Task<Offer> GetTheLowestOfferByTenderId(int tenderId)
        {
            var result = await _context.Offers
                    .Where(t => t.OfferStatusId == (int)Enums.OfferStatus.Received && t.IsActive == true && t.TenderId == tenderId)
                    .Where(t => t.OfferTechnicalEvaluationStatusId == (int)Enums.OfferTechnicalEvaluationStatusId.IdenticalOffer && t.OfferAcceptanceStatusId == (int)Enums.OfferAcceptanceStatusId.AcceptedOffer)
                    .OrderBy(s => s.FinalPriceAfterDiscount)
                .FirstOrDefaultAsync();
            return result;
        }

        public async Task<Offer> GetOfferForNegotiation(int TenderId, string CR)
        {
            var result = await _context.Offers
               .Where(t => t.TenderId == TenderId && t.CommericalRegisterNo == CR && t.IsActive == true)
                .FirstOrDefaultAsync();
            return result;
        }
        public async Task<Offer> GetOfferByTenderAndCR(int TenderId, string CR)
        {
            var result = await _context.Offers
               .Where(t => t.TenderId == TenderId && t.CommericalRegisterNo == CR && t.IsActive == true)
               .Include(d => d.Combined)
               .Include(x => x.Tender)
                //.Include(d => d.QuantityTable)
                .FirstOrDefaultAsync();
            return result;
        }
        public async Task<Offer> GetOfferByTenderIdAndCROfOfferOwner(int TenderId, string CR)
        {
            var result = await _context.Offers
               .Where(t => t.TenderId == TenderId && (t.CommericalRegisterNo == CR) && t.IsActive == true)
               .Include(d => d.Combined).Include(d => d.Tender).Include(a => a.SupplierTenderQuantityTables).ThenInclude(a => a.QuantitiyItemsJson)
                .FirstOrDefaultAsync();
            return result;
        }

        public async Task<Offer> GetOfferByTenderAndOwnerOrSolidarityCR(int TenderId, string CR)
        {
            var result = await _context.Offers
               .Where(t => t.IsActive == true && t.TenderId == TenderId && (t.CommericalRegisterNo == CR || t.Combined.Any(d => d.CRNumber == CR)))
               .FirstOrDefaultAsync();

            return result;
        }

        public async Task<Offer> GetOfferByTenderIdAndSolidarityCR(int TenderId, string CR)
        {
            var result = await _context.Offers
               .Where(t => t.IsActive == true && t.OfferStatusId == (int)Enums.OfferStatus.Received && t.TenderId == TenderId && (t.CommericalRegisterNo == CR || t.Combined.Any(d => d.CRNumber == CR)))
               .FirstOrDefaultAsync();

            return result;
        }

        #region Checking Stage Direct Purchase
        public async Task<OfferDetailsForCheckingModel> GetOfferDetailsForDirectPurchaseChecking(int combinedsupplierId)
        {
            var res = await _context.SupplierCombinedDetails
                .Where(d => d.Combined.Id == combinedsupplierId)
                .Select(SCD => new OfferDetailsForCheckingModel
                {
                    CombinedDetailId = SCD.CombinedDetailId,
                    CombinedId = SCD.CombinedId,
                    TenderIdString = Util.Encrypt(SCD.Combined.Offer.Tender.TenderId),
                    OfferIdString = Util.Encrypt(SCD.Combined.Offer.OfferId),
                    CombinDetailsIDString = Util.Encrypt(SCD.CombinedDetailId),
                    TenderTypeId = SCD.Combined.Offer.Tender.TenderTypeId,
                    IsChamberJoiningAttached = SCD.IsChamberJoiningAttached,
                    IsChamberJoiningValid = SCD.IsChamberJoiningValid,
                    IsCommercialRegisterAttached = SCD.IsCommercialRegisterAttached,
                    IsCommercialRegisterValid = SCD.IsCommercialRegisterValid,
                    IsOfferCopyAttached = SCD.Combined.Offer.IsOfferCopyAttached,
                    IsOfferLetterAttached = SCD.Combined.Offer.IsOfferLetterAttached,
                    OfferLetterNumber = SCD.Combined.Offer.OfferLetterNumber,
                    OfferLetterDate = SCD.Combined.Offer.OfferLetterDate,
                    OfferLetterDateDisplay = (SCD.Combined.Offer.OfferLetterDate.HasValue) ? (SCD.Combined.Offer.OfferLetterDate.Value.ToString("dd/MM/yyyy", System.Threading.Thread.CurrentThread.CurrentCulture)) : "",
                    IsPurchaseBillAttached = SCD.Combined.Offer.IsPurchaseBillAttached,
                    IsSaudizationAttached = SCD.IsSaudizationAttached,
                    IsSaudizationValidDate = SCD.IsSaudizationValidDate,
                    IsSocialInsuranceAttached = SCD.IsSocialInsuranceAttached,
                    IsSocialInsuranceValidDate = SCD.IsSocialInsuranceValidDate,
                    IsVisitationAttached = SCD.Combined.Offer.IsVisitationAttached,
                    IsZakatAttached = SCD.IsZakatAttached,
                    IsZakatValidDate = SCD.IsZakatValidDate,
                    IsBankGuaranteeAttached = SCD.Combined.Offer.IsBankGuaranteeAttached.HasValue && SCD.Combined.Offer.IsBankGuaranteeAttached == true ? true : false,
                    IsSpecificationAttached = SCD.IsSpecificationAttached.HasValue && SCD.IsSpecificationAttached == true ? true : false,
                    IsSpecificationValidDate = SCD.IsSpecificationValidDate.HasValue && SCD.IsSpecificationValidDate == true ? true : false,
                    OfferPresentationWayId = SCD.Combined.Offer.Tender.OfferPresentationWayId.Value,
                    TenderStatusId = SCD.Combined.Offer.Tender.TenderStatusId,
                    TechnicalOfferStatusId = SCD.Combined.Offer.OfferTechnicalEvaluationStatusId,
                    TechnicalOfferStatusString = SCD.Combined.Offer.OfferTechnicalEvaluationStatusId == (int)Enums.OfferTechnicalEvaluationStatusId.IdenticalOffer ? Resources.OfferResources.DisplayInputs.Matching : Resources.OfferResources.DisplayInputs.NotMatching,
                    FinantialOfferStatusString = SCD.Combined.Offer.OfferAcceptanceStatusId == (int)OfferAcceptanceStatusId.AcceptedOffer ? Resources.OfferResources.DisplayInputs.AccepteOffer : Resources.OfferResources.DisplayInputs.RejectedOffer,
                    FinantialOfferStatusId = SCD.Combined.Offer.OfferAcceptanceStatusId,
                    RejectionReason = SCD.Combined.Offer.RejectionReason,
                    FinantialRejectionReason = SCD.Combined.Offer.FinantialRejectionReason,
                    Notes = SCD.Combined.Offer.Notes,
                    FinantialOfferLetterDate = SCD.Combined.Offer.FinantialOfferLetterDate,
                    FinantialOfferLetterNumber = SCD.Combined.Offer.FinantialOfferLetterNumber,
                    IsFinaintialOfferLetterAttached = SCD.Combined.Offer.IsFinaintialOfferLetterAttached,
                    IsFinantialOfferLetterCopyAttached = SCD.Combined.Offer.IsFinantialOfferLetterCopyAttached,
                    FinantialNotes = SCD.Combined.Offer.FinantialNotes,
                    CombinedOwner = SCD.Combined.SolidarityTypeId == (int)Enums.UnRegisteredSuppliersInvitationType.SolidarityLeader,
                    BankGuaranteeFiles = SCD.Combined.Offer.BankGuaranteeDetails
                                .Select(x => new SupplierBankGuaranteeModel
                                {
                                    IsBankGuaranteeValid = x.IsBankGuaranteeValid,
                                    IsBankGuaranteeValidString = x.IsBankGuaranteeValid.HasValue ? (x.IsBankGuaranteeValid.Value ? Resources.OfferResources.DisplayInputs.Valid : Resources.OfferResources.DisplayInputs.Invalid) : "",
                                    GuaranteeNumber = x.GuaranteeNumber,
                                    BankId = x.BankId,
                                    BankName = x.Bank.NameAr,
                                    Amount = x.Amount,
                                    GuaranteeStartDate = x.GuaranteeStartDate,
                                    GuaranteeEndDate = x.GuaranteeEndDate,
                                    GuaranteeStartDateDisplay = (x.GuaranteeStartDate.HasValue) ? (x.GuaranteeStartDate.Value.ToString("dd/MM/yyyy", System.Threading.Thread.CurrentThread.CurrentCulture)) : "",
                                    GuaranteeEndDateDisplay = (x.GuaranteeEndDate.HasValue) ? (x.GuaranteeEndDate.Value.ToString("dd/MM/yyyy", System.Threading.Thread.CurrentThread.CurrentCulture)) : "",
                                    GuaranteeDays = x.GuaranteeDays
                                }).ToList()
                                ,
                    SpecificationsFiles = SCD.SpecificationDetails.Select(x => new SupplierSpecificationModel()
                    {
                        IsForApplier = x.IsForApplier,
                        IsForApplierString = x.IsForApplier.HasValue ? (x.IsForApplier.Value ? Resources.OfferResources.DisplayInputs.OfferApplier : Resources.OfferResources.DisplayInputs.Solidarity) : "",
                        MaintenanceRunningWorkId = x.MaintenanceRunningWorkId,
                        MaintenanceRunningWorkString = x.MaintenanceRunningWork == null ? "" : x.MaintenanceRunningWork.NameAr,
                        ConstructionWorkId = x.ConstructionWorkId,
                        ConstructionWorkString = x.ConstructionWork == null ? "" : x.ConstructionWork.NameAr,
                        Degree = x.Degree,
                    }).ToList()
                }).FirstOrDefaultAsync();

            return res;
        }

        public async Task<OfferDetailsForCheckingModel> GetOfferDetailsForDirectPurchaseCheckingByOfferId(int offerId)
        {
            var res = await _context.Offers
                .Where(d => d.OfferId == offerId)
                .Select(offer => new OfferDetailsForCheckingModel
                {
                    TenderIdString = Util.Encrypt(offer.Tender.TenderId),
                    OfferIdString = Util.Encrypt(offer.OfferId),
                    IsOfferCopyAttached = offer.IsOfferCopyAttached,
                    IsOfferLetterAttached = offer.IsOfferLetterAttached,
                    OfferLetterNumber = offer.OfferLetterNumber,
                    OfferLetterDate = offer.OfferLetterDate,
                    OfferLetterDateDisplay = (offer.OfferLetterDate.HasValue)
                    ? (offer.OfferLetterDate.Value.ToString("dd/MM/yyyy", System.Threading.Thread.CurrentThread.CurrentCulture)) : "",
                    OfferPresentationWayId = offer.Tender.OfferPresentationWayId.Value,
                    TenderStatusId = offer.Tender.TenderStatusId,
                    TechnicalOfferStatusId = offer.OfferTechnicalEvaluationStatusId,
                    TechnicalOfferStatusString = offer.OfferTechnicalEvaluationStatusId == (int)Enums.OfferTechnicalEvaluationStatusId.IdenticalOffer ? Resources.OfferResources.DisplayInputs.Matching : Resources.OfferResources.DisplayInputs.NotMatching,
                    FinantialOfferStatusString = offer.OfferAcceptanceStatusId == (int)Enums.OfferAcceptanceStatusId.AcceptedOffer ? Resources.OfferResources.DisplayInputs.AccepteOffer : Resources.OfferResources.DisplayInputs.RejectedOffer,
                    FinantialOfferStatusId = offer.OfferAcceptanceStatusId,
                    RejectionReason = offer.RejectionReason,
                    FinantialRejectionReason = offer.FinantialRejectionReason,
                    FinantialNotes = offer.FinantialNotes,
                    FinantialOfferLetterDate = offer.FinantialOfferLetterDate,
                    FinantialOfferLetterNumber = offer.FinantialOfferLetterNumber,
                    IsFinaintialOfferLetterAttached = offer.IsFinaintialOfferLetterAttached,
                    IsFinantialOfferLetterCopyAttached = offer.IsFinantialOfferLetterCopyAttached,
                    BankGuaranteeFiles = offer.BankGuaranteeDetails.Where(s => s.IsActive == true)
                                .Select(x => new SupplierBankGuaranteeModel
                                {
                                    IsBankGuaranteeValid = x.IsBankGuaranteeValid,
                                    IsBankGuaranteeValidString = x.IsBankGuaranteeValid.HasValue ? (x.IsBankGuaranteeValid.Value ? Resources.OfferResources.DisplayInputs.Valid : Resources.OfferResources.DisplayInputs.Invalid) : "",
                                    GuaranteeNumber = x.GuaranteeNumber,
                                    BankId = x.BankId,
                                    BankName = x.Bank.NameAr,
                                    Amount = x.Amount,
                                    GuaranteeStartDate = x.GuaranteeStartDate,
                                    GuaranteeEndDate = x.GuaranteeEndDate,
                                    GuaranteeStartDateDisplay = (x.GuaranteeStartDate.HasValue) ? (x.GuaranteeStartDate.Value.ToString("dd/MM/yyyy", System.Threading.Thread.CurrentThread.CurrentCulture)) : "",
                                    GuaranteeEndDateDisplay = (x.GuaranteeEndDate.HasValue) ? (x.GuaranteeEndDate.Value.ToString("dd/MM/yyyy", System.Threading.Thread.CurrentThread.CurrentCulture)) : "",
                                    GuaranteeDays = x.GuaranteeDays
                                }).ToList()
                }).FirstOrDefaultAsync();
            return res;
        }
        #endregion Checking Stage Direct Purchase

        public async Task<CombinedSupplierModel> GetTenderOfferIDsByOfferID(int offerid)
        {
            return null;
        }

        public async Task<QueryResult<CombinedListModel>> GetAllCombinedInvitationForSupplier(CombinedSearchCriteria model)
        {
            var result = await _context.OfferSolidarities
                .Where(x => x.IsActive == true && ((OfferSolidarity)x).CRNumber == model.CommericalRegisterNo && x.StatusId == (int)Enums.SupplierSolidarityStatus.ToBeSent)
                .Select(s => new CombinedListModel
                {
                    CombinedIdString = Util.Encrypt(s.Id),
                    OfferIdString = Util.Encrypt(s.OfferId),
                    TenderName = s.Offer.Tender.TenderName,
                    TenderReferenceNo = s.Offer.Tender.ReferenceNumber,
                    CombinedLeaderCommericalName = s.Offer.Supplier.SelectedCrName,
                    CombinedLeaderCR = s.Offer.CommericalRegisterNo
                })
                .ToQueryResult(model.PageNumber, model.PageSize);
            return result;
        }


        public async Task<CombinedInvitationDetailsModel> GetCombinedInvitationDetailsByOfferIdAndCr(int offerId, string cR)
        {
            var result = await _context.OfferSolidarities
                .Where(x => x.IsActive == true && x.OfferId == offerId && x.CRNumber == cR)
                .Select(s => new CombinedInvitationDetailsModel
                {
                    isActive = s.IsActive.HasValue ? s.IsActive.Value : false,
                    CombinedIdString = Util.Encrypt(s.Id),
                    OfferStatusId = s.Offer.OfferStatusId,
                    SolidarityStatusId = s.StatusId,
                    OfferIdString = Util.Encrypt(s.OfferId),
                    TenderName = s.Offer.Tender.TenderName,
                    TenderReferenceNo = s.Offer.Tender.ReferenceNumber,
                    ExcutionPlace = s.Offer.Tender.InsideKSA == true ? s.Offer.Tender.TenderAreas.Select(c => c.Area.NameAr).ToList() : s.Offer.Tender.TenderCountries.Select(c => c.Country.NameAr).ToList(),
                    IntialGuranteeAddress = s.Offer.Tender.InitialGuaranteeAddress,
                    ApplyOfferWithComnined = Resources.OfferResources.DisplayInputs.ApplyOfferWithCombined,
                    FileReferenceId = s.Offer.Attachment.Where(x => x is SupplierCombinedAttachment).Count() > 0 ? ((SupplierCombinedAttachment)s.Offer.Attachment.Where(x => x is SupplierCombinedAttachment).FirstOrDefault()).FileNetReferenceId : null,
                    FileName = s.Offer.Attachment.Where(x => x is SupplierCombinedAttachment).Count() > 0 ? ((SupplierCombinedAttachment)s.Offer.Attachment.Where(x => x is SupplierCombinedAttachment).FirstOrDefault()).FileName : null,
                    OfferStatus = s.Offer.Status.NameAr
                }).FirstOrDefaultAsync();
            return result;
        }
        public async Task<QueryResult<CombinedSuppliersListModel>> GetAllCombinedSuppliers(CombinedSearchCretriaModel model)
        {
            var result = await _context.OfferSolidarities
                .Where(x => x.IsActive == true && x.OfferId == model.OfferId)
                .Select(s => new CombinedSuppliersListModel
                {
                    CommericalName = s.Supplier.SelectedCrName,
                    CommericalRegisterNo = s.Supplier.SelectedCr,
                    CombinedType = DefineSolidarityTypeName(s)
                })
                .ToQueryResult(model.PageNumber, model.PageSize);
            return result;
        }
        public async Task<OfferSolidarity> GetCombinedWithOfferAndTenderById(int combinedId)
        {
            var result = await _context.OfferSolidarities.Include("Offer.Supplier").Include("Offer.Tender")
                .Where(x => x.IsActive == true && x.Id == combinedId).FirstOrDefaultAsync();
            return result;
        }
        public async Task<OfferDetailsForAcceptingSolidarityInviationsModel> GetOfferDetailsByCombinedId(int combinedId, int userId)
        {
            var Query = await _context.OfferSolidarities.Where(x => x.Id == combinedId)
                      .Select(off => new OfferDetailsForAcceptingSolidarityInviationsModel
                      {
                          OfferAttachmentModels = off.Offer.Attachment.Select(s => new OfferAttachmentModel
                          {
                              FileName = s.FileName,
                              FileNetId = s.FileNetReferenceId,
                              FiletypeID = DefineAttachmentType(s),
                              FiletypeName = DefineAttachmentTypeName(s)
                          }).ToList(),
                          tenderIdString = Util.Encrypt(off.Offer.TenderId),
                          OfferIdString = Util.Encrypt(off.OfferId),
                          OfferStatusId = off.Offer.OfferStatusId,
                          OfferOpenDate = off.Offer.Tender.OffersOpeningDate,
                          SolidarityIdString = Util.Encrypt(combinedId),
                          TenderTypeId = off.Offer.Tender.TenderTypeId,
                          InvitaionStatusId = off.StatusId,
                          InvitaionStatusName = off.SolidarityStatus.Name
                      }).FirstOrDefaultAsync();
            return Query;
        }

        public async Task<Offer> GetOfferWithQuantityItems(int OfferId)
        {
            var offer = await _context.Offers
                .Include(a => a.SupplierTenderQuantityTables).
                ThenInclude(a => a.QuantitiyItemsJson)
                   .Where(t => t.IsActive == true && t.OfferId == OfferId).AsTracking()
                   .FirstOrDefaultAsync();
            return offer;
        }

        public async Task<Tender> GetTenderbyTenderIdAsync(int TenderId)
        {
            var Query = await _context.Tenders.Where(x => x.TenderId == TenderId).FirstOrDefaultAsync();
            return Query;
        }

        #region OffersChecking

        public async Task<OfferDetailsForCheckingModel> GetOfferDetailsForFinancialCheckingByOfferId(int offerId)
        {
            var result = await _context.Offers
                .Where(offer => offer.OfferId == offerId && offer.IsActive == true)
                .Select(obj => new OfferDetailsForCheckingModel
                {
                    IsBankGuaranteeAttached = obj.IsBankGuaranteeAttached.HasValue && obj.IsBankGuaranteeAttached == true,
                    BankGuaranteeFiles = obj.BankGuaranteeDetails.Where(g => g.IsActive == true)
                                .Select(x => new SupplierBankGuaranteeModel
                                {
                                    IsBankGuaranteeValid = x.IsBankGuaranteeValid,
                                    IsBankGuaranteeValidString = x.IsBankGuaranteeValid.HasValue ? (x.IsBankGuaranteeValid.Value ? Resources.OfferResources.DisplayInputs.Valid : Resources.OfferResources.DisplayInputs.Invalid) : "",
                                    GuaranteeNumber = x.GuaranteeNumber,
                                    BankId = x.BankId,
                                    BankName = x.Bank.NameAr,
                                    Amount = x.Amount,
                                    GuaranteeStartDate = x.GuaranteeStartDate,
                                    GuaranteeEndDate = x.GuaranteeEndDate,
                                    GuaranteeStartDateDisplay = (x.GuaranteeStartDate.HasValue) ? (x.GuaranteeStartDate.Value.ToString("dd/MM/yyyy", System.Threading.Thread.CurrentThread.CurrentCulture)) : "",
                                    GuaranteeEndDateDisplay = (x.GuaranteeEndDate.HasValue) ? (x.GuaranteeEndDate.Value.ToString("dd/MM/yyyy", System.Threading.Thread.CurrentThread.CurrentCulture)) : "",
                                    GuaranteeDays = x.GuaranteeDays
                                }).ToList(),
                    IsFinaintialOfferLetterAttached = obj.IsFinaintialOfferLetterAttached ?? true,
                    FinantialOfferLetterDate = obj.FinantialOfferLetterDate,
                    FinantialOfferStatusId = obj.OfferAcceptanceStatusId,
                    FinantialOfferStatusString = obj.OfferAcceptanceStatusId == (int)Enums.OfferAcceptanceStatusId.AcceptedOffer ? Resources.OfferResources.DisplayInputs.AccepteOffer : Resources.OfferResources.DisplayInputs.RejectedOffer,
                    FinantialOfferLetterNumber = obj.FinantialOfferLetterNumber,
                    IsFinantialOfferLetterCopyAttached = obj.IsFinantialOfferLetterCopyAttached,
                    TenderStatusId = obj.Tender.TenderStatusId,
                    FinantialRejectionReason = obj.FinantialRejectionReason,
                    FinantialNotes = obj.FinantialNotes,
                    OfferPresentationWayId = obj.Tender.OfferPresentationWayId.Value,
                    OfferIdString = Util.Encrypt(obj.OfferId),
                    TechnicalOfferStatusId = obj.OfferTechnicalEvaluationStatusId,
                    RejectionReason = obj.RejectionReason,
                    Notes = obj.Notes,
                    TechnicalOfferStatusString = obj.OfferTechnicalEvaluationStatusId == (int)Enums.OfferTechnicalEvaluationStatusId.IdenticalOffer ? Resources.OfferResources.DisplayInputs.Matching : Resources.OfferResources.DisplayInputs.NotMatching,
                    SupplierName = obj.Supplier.SelectedCrName
                }).FirstOrDefaultAsync();
            return result;
        }

        //public async Task<SupplierQuantityTableItem> FindQuantityTableItemByChildId(int Id)
        //{
        //    return await _context.SupplierQuantityTableItems.FirstOrDefaultAsync(d => d.OriginalItemId == Id);
        //}

        #endregion

        public async Task<OfferFullDetailsModel> FindOfferFullDetailsModelbyOfferId(int offerId, string CR)
        {
            var Query = await _context.Offers.Where(w => w.IsActive == true && w.OfferId == offerId)
                          .Select(
                          s => new OfferFullDetailsModel
                          {
                              isOwner = s.Combined.Where(w => w.SolidarityTypeId == (int)UnRegisteredSuppliersInvitationType.SolidarityLeader).Any(a => a.CRNumber == CR),
                              isSolidarity = s.IsSolidarity,
                              isAltenative = s.Tender.HasAlternativeOffer ?? false,
                              tenderTypeId = s.Tender.TenderTypeId,
                              OfferIdString = Util.Encrypt(s.OfferId),
                              SupplierName = s.Supplier.SelectedCrName,
                              offerStatus = s.Status.NameAr,
                              statusId = s.OfferStatusId,
                              yearsCount = (s.Tender.TemplateYears ?? 0),
                              TenderIdString = Util.Encrypt(s.TenderId),
                              attachments = s.Attachment.Select(a =>
                             new AttachmentModel
                             {
                                 FileName = a.FileName,
                                 AttachmentId = a.AttachmentId,
                                 FileNetId = a.FileNetReferenceId,
                                 type = DefineAttachmentType(a)
                             }).ToList()
                              ,
                              CombinedSuppliers = s.Combined.Select(w =>
                                new SupplierCombinedModel
                                {
                                    CommericalName = w.Supplier.SelectedCrName,
                                    CommericalRegisterNo = w.CRNumber,
                                    SolidarityTypeName = DefineSolidarityTypeName(w)
                                }).ToList()
                          }).FirstOrDefaultAsync();
            return Query;
        }

        public async Task<TechnicianReportAttachment> GetTechnicianReportAttachment(string referenceId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(referenceId), referenceId);
            var entity = await _context.TechnicianReportAttachments
                .Where(a => a.FileNetReferenceId == referenceId).FirstOrDefaultAsync();
            return entity;
        }

        private static int DefineAttachmentType(SupplierAttachment s)
        {
            if (s is SupplierOriginalAttachment)
            {
                return (int)Enums.AttachmentType.SupplierOriginalAttachment;
            }
            else if (s is SupplierTechnicalProposalAttachment)
            {
                return (int)Enums.AttachmentType.SupplierTechnicalProposalAttachment;
            }
            else if (s is SupplierFinancialProposalAttachment)
            {
                return (int)Enums.AttachmentType.SupplierFinancialProposalAttachment;
            }
            else if (s is SupplierFinancialandTechnicalProposalAttachment)
            {
                return (int)Enums.AttachmentType.SupplierTechnicalAndFinancialProposalAttachment;
            }
            else
            {
                return 17;
            }
        }


        private static string DefineAttachmentTypeName(SupplierAttachment s)
        {
            if (s is SupplierOriginalAttachment)
            {
                return Resources.OfferResources.DisplayInputs.OfferAttachment;
            }
            else if (s is SupplierTechnicalProposalAttachment)
            {
                return Resources.OfferResources.DisplayInputs.TechnicalFile;

            }
            else if (s is SupplierFinancialProposalAttachment)
            {
                return Resources.OfferResources.DisplayInputs.FinancialOffer;
            }
            else if (s is SupplierFinancialandTechnicalProposalAttachment)
            {
                return Resources.OfferResources.DisplayInputs.TechnicalFinancialFiles;

            }
            else if (s is SupplierCombinedAttachment)
            {
                return Resources.OfferResources.DisplayInputs.CombinationLetter;
            }
            else
            {
                return "";
            }
        }
        private static int DefineAttachmentTypeId(SupplierAttachment s)
        {
            if (s is SupplierOriginalAttachment)
            {
                return (int)Enums.AttachmentType.SupplierOriginalAttachment;
            }
            else if (s is SupplierTechnicalProposalAttachment)
            {
                return (int)Enums.AttachmentType.SupplierTechnicalProposalAttachment;

            }
            else if (s is SupplierFinancialProposalAttachment)
            {
                return (int)Enums.AttachmentType.SupplierFinancialProposalAttachment;
            }
            else if (s is SupplierFinancialandTechnicalProposalAttachment)
            {
                return (int)Enums.AttachmentType.SupplierTechnicalAndFinancialProposalAttachment;

            }
            else if (s is SupplierCombinedAttachment)
            {
                return (int)Enums.AttachmentType.SupplierCombinedAttachment;
            }
            else
            {
                return 0;
            }
        }

        private static string DefineSolidarityTypeName(OfferSolidarity s)
        {
            switch (s.SolidarityTypeId)
            {
                case (int)UnRegisteredSuppliersInvitationType.Existed:
                    return "مورد مسجل ";
                case (int)UnRegisteredSuppliersInvitationType.HasCR:
                    return "مورد غير مسجل - يملك سجل فى بلد المنشأ";
                case (int)UnRegisteredSuppliersInvitationType.HasLicience:
                    return "مورد غير مسجل - لدية رخصة مزاولة مهنة";
                case (int)UnRegisteredSuppliersInvitationType.Foriegn:
                    return "مورد غير مسجل - أجنبي";
                case (int)UnRegisteredSuppliersInvitationType.SolidarityLeader:
                    return "قائد التضامن";
                default:
                    return "متضامن";
            }
        }

        #region VRO

        public async Task<VROOffersTechnicalCheckingViewModel> FindVROOfferDetailsById(int offerId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(offerId), offerId.ToString());
            var offerEntity = await _context.Offers
                .Where(t =>
                t.OfferId == offerId &&
                t.Tender.TenderTypeId == (int)Enums.TenderType.NationalTransformationProjects &&
                t.IsActive == true)
                .Select(offer => new VROOffersTechnicalCheckingViewModel
                {
                    OfferIdString = Util.Encrypt(offer.OfferId),
                    TenderIdString = Util.Encrypt(offer.TenderId),
                    TenderStatusId = offer.Tender.TenderStatusId,
                    TenderTypeId = offer.Tender.TenderTypeId,
                    RejectionReason = offer.RejectionReason,
                    OfferAcceptanceStatusId = offer.OfferAcceptanceStatusId,
                    OfferTechnicalEvaluationStatusId = offer.OfferTechnicalEvaluationStatusId != null ? offer.OfferTechnicalEvaluationStatusId : 0,
                    Notes = offer.Notes,
                    OffersCheckingDateTime = offer.Tender.OffersCheckingDate,
                    IsOpened = offer.IsOpened.HasValue ? offer.IsOpened.Value ? Resources.SharedResources.DisplayInputs.Yes : Resources.SharedResources.DisplayInputs.No : "",
                    TechnicalEvaluationLevel = offer.TechnicalEvaluationLevel,
                }).FirstOrDefaultAsync();
            return offerEntity;
        }

        public async Task<VROFinantialCheckingModel> GeOfferByTenderIdAndOfferIdForFinancialChecking(int tenderId, int offerId)
        {
            var result = await _context.Offers
                .Where(o => o.IsActive == true && o.TenderId == tenderId && o.OfferId == offerId)
                .Select(offer => new VROFinantialCheckingModel
                {
                    FinalPrice = offer.FinalPriceAfterDiscount,
                    OfferId = offer.OfferId,
                    FinancialWeigth = offer.FinancialEvaluationLevel,
                    OfferIdString = Util.Encrypt(offer.OfferId),
                    DiscountNotes = offer.DiscountNotes,
                    Discount = offer.Discount,
                    OfferStatusId = offer.OfferStatusId,
                    HasAlternativeOffer = offer.Tender.HasAlternativeOffer ?? false,
                    TenderId = offer.TenderId,
                    TenderIdString = Util.Encrypt(offer.TenderId),
                    CommericalRegisterNo = offer.CommericalRegisterNo,
                    TenderStatusId = offer.Tender.TenderStatusId,
                    OfferPresentationWayId = offer.Tender.OfferPresentationWayId ?? 0,
                    IsSupplierCombinedLeader = offer.Combined.Count() == 1 ? true : false,
                    CombinedOwner = offer.Combined.Count() == 1 ? true : false,
                    FinantialOfferStatusId = offer.OfferAcceptanceStatusId,
                    FinantialRejectionReason = offer.FinantialRejectionReason,
                    FinantialNotes = offer.FinantialNotes,
                    Notes = offer.Notes,
                    TechnicalWeigth = offer.TechnicalEvaluationLevel,
                    FinantialOfferStatusString = offer.OfferAcceptanceStatusId == (int)Enums.OfferAcceptanceStatusId.AcceptedOffer ? Resources.OfferResources.DisplayInputs.AccepteOffer : Resources.OfferResources.DisplayInputs.RejectedOffer,
                    TechnicalOfferStatusId = offer.OfferTechnicalEvaluationStatusId,
                    TechnicalOfferStatusString = offer.OfferTechnicalEvaluationStatusId == (int)Enums.OfferTechnicalEvaluationStatusId.IdenticalOffer ? Resources.OfferResources.DisplayInputs.Matching : Resources.OfferResources.DisplayInputs.NotMatching,
                    TechnicalRejectionReason = offer.RejectionReason,
                    CombinedIdString = Util.Encrypt(offer.Combined.Where(c => c.SolidarityTypeId == (int)Enums.UnRegisteredSuppliersInvitationType.SolidarityLeader).Select(x => x.Id).FirstOrDefault()),
                    OffersCheckingDateTime = offer.Tender.OffersCheckingDate,
                    TechniciansReportAttachments = offer.TechnicianReportAttachments.Select(a => new TechniciansReportAttachmentModel
                    {
                        AttachmentId = a.AttachmentId,
                        Name = a.Name,
                        FileNetReferenceId = a.FileNetReferenceId,
                        OfferId = a.OfferId,
                        AttachmentTypeId = a.AttachmentTypeId
                    }).ToList(),
                    TechniciansReportAttachmentsRef = string.Join(",", offer.TechnicianReportAttachments.Select(at =>
                            "/Upload/GetFile/" + at.FileNetReferenceId + ":" + at.Name
                            )),
                    //QuantityTable = offer.QuantityTable.Select(q => new SupplierQuantityTableModel
                    //{
                    //    TableQuantityId = q.TableQuantityId,
                    //    OfferId = q.OfferId,
                    //    TableQuantityName = q.TableQuantityName,
                    //    TotalPrice = Util.DecryptNewDecimal(q.TotalPrice),
                    //    Discount = Util.DecryptNewDecimal(q.TotalDiscount),
                    //    OpeningTotalPrice = !string.IsNullOrEmpty(q.AdjustedTotalPrice) ? Util.DecryptNewDecimal(q.AdjustedTotalPrice) : 0,
                    //    OpeningDiscount = !string.IsNullOrEmpty(q.AdjustedTotalDiscount) ? Util.DecryptNewDecimal(q.AdjustedTotalDiscount) : 0,
                    //    OpeningVat = q.AdjustedTotalVAT,
                    //    QuantityTableItem = q.QuantityTableItem.Select(i => new SupplierQuantityTableItemModel
                    //    {
                    //        Discount = Util.DecryptNewDecimal(i.Discount),
                    //        Price = Util.DecryptNewDecimal(i.Price),
                    //        Id = i.Id,
                    //        Quantity = i.Quantity,
                    //        Name = i.Name,
                    //        Unit = i.Unit,
                    //        ItemAttachmentName = i.ItemAttachmentName,
                    //        ItemAttachmentId = i.ItemAttachmentId,
                    //        isAlternative = i.OriginalQuantityItem == null ? false : true,
                    //        VAT = i.VAT,
                    //        TenderQuantityTableItem = new QuantityTableItemModel
                    //        {
                    //            Name = i.TenderQuantityTableItem.Name,
                    //            Quantity = i.TenderQuantityTableItem.Quantity,
                    //            QuantityTableItemId = i.TenderQuantityTableItem.QuantityTableItemId,
                    //            Unit = i.TenderQuantityTableItem.Unit,
                    //            ItemAttachmentId = i.TenderQuantityTableItem.ItemAttachmentId,
                    //            ItemAttachmentName = i.TenderQuantityTableItem.ItemAttachmentName
                    //        }
                    //    }).ToList(),
                    //}).ToList(),
                    Attachment = offer.Attachment.Select(a => new SupplierAttachmentModel
                    {
                        AttachmentId = a.AttachmentId,
                        FileName = a.FileName,
                        FileNetReferenceId = a.FileNetReferenceId,
                        OfferId = a.OfferId
                    }).ToList(),
                    CombinedSupplierModel = offer.Combined.Select(y => new CombinedSupplierModel
                    {
                        SupplierName = y.Supplier.SelectedCrName,
                    }).ToList(),
                }).FirstOrDefaultAsync();
            return result;
        }
        #endregion VRO

        #region Offer New
        public async Task<OfferFileVModel> FindOfferFileVModelByOfferId(int offerId)
        {
            var localContentSetting = await _localContentConfigurationSettings.getLocalContentSettingsDate();

            var offer = await _context.Offers.Where(w => w.IsActive == true && w.OfferId == offerId).Select(s =>
               new OfferFileVModel
               {
                   tenderIdString = Util.Encrypt(s.TenderId),
                   tenderType = (Enums.TenderType)s.Tender.TenderTypeId,
                   isSolidarity = s.IsSolidarity,
                   offerIdString = Util.Encrypt(s.OfferId),
                   OfferPresentationWay = (OfferPresentationWayId)s.OfferPresentationWayId,
                   offerStatusModel = new OfferStatusModel { offerStatusName = s.Status.NameAr, OfferStatusId = s.OfferStatusId },
                   FinancialFilesReferenceId = string.Join(",", s.Attachment.Where(d => d is SupplierFinancialProposalAttachment).Select(d => d.FileNetReferenceId + ":" + d.FileName).ToList()),
                   TechnicalandFinancialFilesReferenceIds = string.Join(",", s.Attachment.Where(d => d is SupplierFinancialandTechnicalProposalAttachment).Select(d => d.FileNetReferenceId + ":" + d.FileName).ToList()),
                   SolidarityrFilesReferenceIds = string.Join(",", s.Attachment.Where(d => d is SupplierCombinedAttachment).Select(d => d.FileNetReferenceId + ":" + d.FileName).ToList()),
                   TechnicalFilesReferenceIds = string.Join(",", s.Attachment.Where(d => d is SupplierTechnicalProposalAttachment).Select(d => d.FileNetReferenceId + ":" + d.FileName).ToList()),
                   IsValidToApplyOfferLocalContentChanges = s.Tender.CreatedAt > localContentSetting.Date 
               }
            ).FirstOrDefaultAsync();
            
            return offer;
        }

        public async Task<OfferFileVModel> FindOfferToCheckTenderTypeByOfferId(int offerId)
        {
            var Query = await _context.Offers.Where(w => w.IsActive == true && w.OfferId == offerId).Select(s =>
               new OfferFileVModel
               {
                   isSolidarity = s.IsSolidarity,
                   tenderType = (Enums.TenderType)s.Tender.TenderTypeId,
                   offerIdString = Util.Encrypt(s.OfferId),
               }
            ).FirstOrDefaultAsync();
            return Query;
        }

        public async Task<OfferMainVModel> FindActiveOfferMainVModelByOfferId(int offerId, string CR)
        {
            var Query = await _context.Offers.Where(t => t.IsActive == true && t.OfferId == offerId && t.CommericalRegisterNo == CR).Select(s => new OfferMainVModel
            {
                isSolidarity = s.IsSolidarity,
                offerOwner = s.CommericalRegisterNo == CR ? true : false,
                offerIdString = Util.Encrypt(s.OfferId),
                OfferStatusId = s.OfferStatusId,
                offerStatusName = s.Status.NameAr,
                tenderIdString = Util.Encrypt(s.TenderId),
                tenderTypeId = s.Tender.TenderTypeId,
                CR = CR
            }).FirstOrDefaultAsync();
            return Query;
        }

        public async Task<SupplierAttachmentPartialVModel> FindOfferByIdWithAttachementsModel(int offerId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(offerId), offerId.ToString());
            var offerEntity = await _context.Offers
                .Where(t => t.OfferId == offerId).Select(
                o =>

        new SupplierAttachmentPartialVModel
        {

            statusModel = new OfferStatusModel { OfferStatusId = o.Status.OfferStatusId, offerStatusName = o.Status.NameAr },
            supplierAttachmentModels = o.Attachment.Select(
        s => new SupplierAttachmentModel
        {
            AttachmentId = s.AttachmentId,
            FileName = s.FileName,
            OfferId = offerId,
            AttachmentTypeName = GetFileTypeName(s),
            type = GetFileType(s),
            FileNetReferenceId = s.FileNetReferenceId
        }).ToList()
        }
        ).FirstOrDefaultAsync();
            return offerEntity;
        }
        public async Task<List<TenderQuantityItemDTO>> GetSupplierQTableItems(int offerId, int tenderId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(offerId), offerId.ToString());
            var offerEntity = (await _context.SupplierTenderQuantityTables
                .Where(t =>
                t.OfferId == offerId
                && t.IsActive == true
                && t.Offer.IsActive == true
                && t.IsActive == true
                && t.Offer.OfferStatusId != (int)Enums.OfferStatus.ChangeByQT)
                //.Include(f => f.QuantitiyItemsJson)
                ./*Select(d => d.QuantitiyItemsJson).*/ToListAsync());

            foreach (var table in offerEntity)
            {

                var hasItems = _context.SupplierQuantityTableItemsView.Where(i => i.QTableId == table.Id).Count() > 0;

                if (hasItems)
                {

                    var items = await _context.SupplierQuantityTableItemsView.Where(i => i.QTableId == table.Id).ToListAsync();

                    //await getQuantityTableItemsJson(table.Id);

                    //var id = await _context.SupplierTenderQuantityTableItemJsons.Where(x => x.SupplierTenderQuantityTableId == table.Id).Select(s => s.Id).FirstOrDefaultAsync();
                    table.QuantitiyItemsJson = new SupplierTenderQuantityTableItemJson()
                    {
                        Id = items.FirstOrDefault().QTableItemsId,
                        SupplierTenderQuantityTableItems = items.Select(s => new SupplierTenderQuantityTableItem()
                        {
                            ActivityTemplateId = s.ActivityTemplateId,
                            AlternativeValue = s.AlternativeValue,
                            ColumnId = s.ColumnId,
                            Id = s.Id,
                            IsDefault = s.IsDefault,
                            ItemNumber = s.ItemNumber.Value,
                            TenderFormHeaderId = s.TenderFormHeaderId,
                            Value = s.Value
                        }).ToList(),
                        SupplierTenderQuantityTableId = table.Id
                    };
                }
            }

            var tqIds = offerEntity.Select(s => s.TenederQuantityId).ToList();
            var tqNames = await _context.TenderQuantityTables.Where(w => tqIds.Any(a => a == w.Id)).Select(s => new
            {
                s.Id,
                s.Name
            }).ToListAsync();



            var _tenderid = 0;
            long tableid = 0;
            var tablename = "";
            List<TenderQuantityItemDTO> res = new List<TenderQuantityItemDTO>();
            foreach (var tbl in offerEntity)
            {
                tablename = tqNames.FirstOrDefault(w => w.Id == tbl.TenederQuantityId.Value).Name;
                tableid = tbl.Id;
                _tenderid = tenderId;
                foreach (var itm in tbl.QuantitiyItemsJson.SupplierTenderQuantityTableItems)
                {
                    res.Add(new TenderQuantityItemDTO
                    {
                        ColumnId = itm.ColumnId,
                        ItemNumber = itm.ItemNumber,
                        ColumnName = "",
                        TableName = tablename,
                        TableId = tableid,
                        TemplateId = itm.ActivityTemplateId,
                        TenderId = _tenderid,
                        TenderFormHeaderId = itm.TenderFormHeaderId,
                        Value = itm.Value,
                        AlternativeValue = itm.AlternativeValue,
                        IsDefault = itm.IsDefault,
                        Id = itm.Id
                    });
                }
            }
            return res;
        }
        public async Task<SupplierTenderQuantityTable> GetSupplierQTableByTableId(int tableId)
        {
            return await _context.SupplierTenderQuantityTables.Include(d => d.QuantitiyItemsJson).FirstOrDefaultAsync(w => w.Id == tableId);
        }
        public async Task<List<TenderQuantityItemDTO>> GetSupplierQTableItemsByTableId(int offerId, int tableId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(offerId), offerId.ToString());
            var quantityTables = await _context.SupplierTenderQuantityTables.Include(a => a.Offer).Include(a => a.QuantitiyItemsJson)
                .Where(t => t.OfferId == offerId && t.Id == tableId && t.IsActive == true).Include(f => f.QuantitiyItemsJson).ToListAsync();
            var offerEntity = quantityTables.SelectMany(d => d.QuantitiyItemsJson.SupplierTenderQuantityTableItems).Select(
                s => new TenderQuantityItemDTO
                {
                    ColumnId = s.ColumnId,
                    ItemNumber = s.ItemNumber,
                    ColumnName = "",
                    TableName = quantityTables.FirstOrDefault(a => a.IsActive == true && a.QuantitiyItemsJson != null && a.QuantitiyItemsJson.SupplierTenderQuantityTableItems.Any(i => i.Id == s.Id)).Name,
                    TableId = quantityTables.FirstOrDefault(a => a.IsActive == true && a.QuantitiyItemsJson != null && a.QuantitiyItemsJson.SupplierTenderQuantityTableItems.Any(i => i.Id == s.Id)).Id,
                    TemplateId = s.ActivityTemplateId,
                    TenderId = quantityTables.FirstOrDefault().Offer.TenderId,
                    TenderFormHeaderId = s.TenderFormHeaderId,
                    Value = s.Value,
                    AlternativeValue = s.AlternativeValue,
                    IsDefault = s.IsDefault,
                    Id = s.Id
                }).ToList();
            return offerEntity;
        }

        public async Task<List<TenderQuantityItemDTO>> GetSupplierQTableItemsForOpening(int offerId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(offerId), offerId.ToString());
            var offerEntity = await _context.SupplierTenderQuantityTables
                .Where(t => t.OfferId == offerId && t.IsActive == true && t.Offer.IsActive == true)
                .Include(f => f.TenderQuantityTable).Include(f => f.Offer).Include(t => t.QuantitiyItemsJson)
                .ToListAsync();
            //.ThenInclude(f => f.SupplierTenderQuantityTableItems)
            var tenderid = 0;
            long tableid = 0;
            var tablename = "";
            List<TenderQuantityItemDTO> res = new List<TenderQuantityItemDTO>();
            foreach (var tbl in offerEntity)
            {
                tablename = tbl.TenderQuantityTable.Name;
                tableid = tbl.Id;
                tenderid = tbl.Offer.TenderId;
                foreach (var itm in tbl.QuantitiyItemsJson.SupplierTenderQuantityTableItems)
                {
                    res.Add(new TenderQuantityItemDTO
                    {
                        ColumnId = itm.ColumnId,
                        ItemNumber = itm.ItemNumber,
                        ColumnName = "",
                        TableName = tablename,
                        TableId = tableid,
                        TemplateId = itm.ActivityTemplateId,
                        TenderId = tenderid,
                        TenderFormHeaderId = itm.TenderFormHeaderId,
                        Value = itm.Value,
                        AlternativeValue = itm.AlternativeValue,
                        IsDefault = itm.IsDefault,
                        Id = itm.Id
                    });
                }
            }
            return res;
        }

        public async Task<List<TenderQuantityItemDTO>> GetNegotiationQTableItems(int offerId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(offerId), offerId.ToString());
            //var offerEntity = await _context.NegotiationSupplierQuantityTableItems
            //    .Where(t => t.negotiationSupplierQuantityTable.NegotiationSecondStage.OfferId == offerId && t.IsActive == true).Select(
            //    s => new TenderQuantityItemDTO
            //    {
            //        ColumnId = s.ColumnId,
            //        ItemNumber = s.ItemNumber,
            //        ColumnName = "",
            //        TableName = s.negotiationSupplierQuantityTable.SupplierQuantityTable.TenderQuantityTable.Name,
            //        TableId = s.NegotiationSupplierQuantityTableId,
            //        TemplateId = s.ActivityTemplateId,
            //        TenderId = s.negotiationSupplierQuantityTable.NegotiationSecondStage.Offer.TenderId,
            //        TenderFormHeaderId = s.TenderFormHeaderId,
            //        Value = s.Value,
            //        IsDefault = true,
            //        Id = s.Id
            //    }).ToListAsync();
            //return offerEntity;


            var tables = await _context.NegotiationSupplierQuantityTables
                                .Where(t => t.NegotiationSecondStage.OfferId == offerId && t.IsActive == true).Select(
              t => new QuantitiesTemplateModel
              {

                  QuantitiesItems = t.NegotiationQuantityItemJson.NegotiationSupplierQuantityTableItems.Select(i => new TenderQuantityItemDTO
                  {
                      Id = i.Id,
                      TableId = t.Id, //t.NegotiationQuantityItemJson.NegotiationSupplierQuantityTableItems.FirstOrDefault(a => a.Id == i.Id).Id,
                      TableName = t.Name, // string.IsNullOrEmpty(x.TenderQuantityTables.FirstOrDefault(a => a.IsActive == true && a.QuantitiyItemsJson != null && a.QuantitiyItemsJson.TenderQuantityTableItems.Any(i => i.Id == q.Id)).Name) ? "اسم الجدول" :
                                          // x.TenderQuantityTables.FirstOrDefault(a => a.IsActive == true && a.QuantitiyItemsJson != null && a.QuantitiyItemsJson.TenderQuantityTableItems.Any(i => i.Id == q.Id)).Name,
                      ColumnId = i.ColumnId,
                      ItemNumber = i.ItemNumber,
                      TenderId = t.NegotiationSecondStage.Offer.TenderId,
                      TenderFormHeaderId = i.TenderFormHeaderId,
                      TemplateId = i.ActivityTemplateId,
                      Value = i.Value,
                      ColumnName = "",
                      IsDefault = true,
                  }).ToList(),
              }


              ).ToListAsync();
            var result = tables.SelectMany(s => s.QuantitiesItems).ToList();
            return result;
        }

        #region New quantity tables 10/09/19
        public async Task<QueryResult<TenderQuantityItemDTO>> GetSupplierQTableItemsForChecking(QuantityTableSearchCretriaModel quantityTableSearchCretriaModel)
        {
            Check.ArgumentNotNullOrEmpty(nameof(quantityTableSearchCretriaModel.OfferId), quantityTableSearchCretriaModel.OfferId.ToString());
            var offerEntity = await _context.SupplierTenderQuantityTables
                .Where(t => t.OfferId == quantityTableSearchCretriaModel.OfferId && t.QuantitiyItemsJson != null && t.IsActive == true && t.Offer.IsActive == true && t.Id == quantityTableSearchCretriaModel.QuantityTableId)
                .Include(f => f.TenderQuantityTable).Include(f => f.Offer).Include(t => t.QuantitiyItemsJson)
                .ToListAsync();
            var tenderid = 0;
            long tableid = 0;
            var tablename = "";
            List<TenderQuantityItemDTO> res = new List<TenderQuantityItemDTO>();
            foreach (var tbl in offerEntity)
            {
                tablename = tbl.TenderQuantityTable?.Name;
                tableid = tbl.Id;
                tenderid = tbl.Offer.TenderId;
                foreach (var itm in tbl.QuantitiyItemsJson.SupplierTenderQuantityTableItems.OrderBy(a => a.ItemNumber).ToList())
                {
                    res.Add(new TenderQuantityItemDTO
                    {
                        ColumnId = itm.ColumnId,
                        ItemNumber = itm.ItemNumber,
                        ColumnName = "",
                        TableName = tablename,
                        TableId = tableid,
                        TemplateId = itm.ActivityTemplateId,
                        TenderId = tenderid,
                        TenderFormHeaderId = itm.TenderFormHeaderId,
                        Value = itm.Value,
                        AlternativeValue = itm.AlternativeValue,
                        IsDefault = itm.IsDefault,
                        Id = itm.Id
                    });
                }
            }
            return await res.ToQueryResult(quantityTableSearchCretriaModel.PageNumber, quantityTableSearchCretriaModel.PageSize * quantityTableSearchCretriaModel.CellsCount);
        }
        public async Task<QueryResult<TenderQuantityItemDTO>> GetSupplierQTableItemsForApplyOffer(QuantityTableSearchCretriaModel quantityTableSearchCretriaModel)
        {
            Check.ArgumentNotNullOrEmpty(nameof(quantityTableSearchCretriaModel.OfferId), quantityTableSearchCretriaModel.OfferId.ToString());
            var SupplierTenderQuantityTableEntity = await _context.SupplierTenderQuantityTables.Include(d => d.Offer)
                                .Where(t => t.OfferId == quantityTableSearchCretriaModel.OfferId && t.Id == quantityTableSearchCretriaModel.QuantityTableId && t.IsActive == true)
                                //.Include(f => f.QuantitiyItemsJson)
                                .FirstOrDefaultAsync();


            var result = await _context.SupplierQuantityTableItemsView.Where(i => i.QTableId == SupplierTenderQuantityTableEntity.Id).OrderBy(d => d.ItemNumber).ToQueryResult(quantityTableSearchCretriaModel.PageNumber, quantityTableSearchCretriaModel.PageSize * quantityTableSearchCretriaModel.CellsCount);

            //.Select(s => new SupplierTenderQuantityTableItem()
            //{
            //    ActivityTemplateId = s.ActivityTemplateId,
            //    AlternativeValue = s.AlternativeValue,
            //    ColumnId = s.ColumnId,
            //    Id = s.Id,
            //    IsDefault = s.IsDefault,
            //    ItemNumber = s.ItemNumber.Value,
            //    TenderFormHeaderId = s.TenderFormHeaderId,
            //    Value = s.Value
            //}).;
            //.ToQueryResult(quantityTableSearchCretriaModel.PageNumber, quantityTableSearchCretriaModel.PageSize * quantityTableSearchCretriaModel.CellsCount);

            // var id = await _context.SupplierTenderQuantityTableItemJsons.Where(x => x.SupplierTenderQuantityTableId == SupplierTenderQuantityTableEntity.Id).Select(s => s.Id).FirstOrDefaultAsync();
            SupplierTenderQuantityTableEntity.QuantitiyItemsJson = new SupplierTenderQuantityTableItemJson()
            {
                Id = result.Items.FirstOrDefault().QTableItemsId,
                SupplierTenderQuantityTableItems = result.Items.Select(s => new SupplierTenderQuantityTableItem()
                {
                    ActivityTemplateId = s.ActivityTemplateId,
                    AlternativeValue = s.AlternativeValue,
                    ColumnId = s.ColumnId,
                    Id = s.Id,
                    IsDefault = s.IsDefault,
                    ItemNumber = s.ItemNumber.Value,
                    TenderFormHeaderId = s.TenderFormHeaderId,
                    Value = s.Value
                }).ToList(),
                SupplierTenderQuantityTableId = SupplierTenderQuantityTableEntity.Id
            };

            var TenderId = SupplierTenderQuantityTableEntity.Offer.TenderId;
            var tablename = SupplierTenderQuantityTableEntity.Name;
            var QuantityTableId = quantityTableSearchCretriaModel.QuantityTableId;
            return new QueryResult<TenderQuantityItemDTO>(SupplierTenderQuantityTableEntity.QuantitiyItemsJson.SupplierTenderQuantityTableItems.OrderBy(d => d.ItemNumber)
                .Select(s => new TenderQuantityItemDTO
                {
                    ColumnId = s.ColumnId,
                    ItemNumber = s.ItemNumber,
                    ColumnName = "",
                    TableName = tablename,
                    TableId = QuantityTableId,
                    TemplateId = s.ActivityTemplateId,
                    TenderId = TenderId,
                    TenderFormHeaderId = s.TenderFormHeaderId,
                    Value = s.Value,
                    AlternativeValue = s.AlternativeValue,
                    IsDefault = s.IsDefault,
                    Id = s.Id
                }), result.TotalCount, result.PageNumber, result.PageSize);
            //return (await .ToQueryResult(quantityTableSearchCretriaModel.PageNumber, quantityTableSearchCretriaModel.PageSize * quantityTableSearchCretriaModel.CellsCount));

            // return offerEntity;
        }
        public async Task<int> GetOfferTableQuantityItems(long tableId)
        {

            var result = await _context.SP_QuantityTableCellCount.FromSqlRaw($"SP_QuantityTableCellCount {tableId}").ToListAsync();

            return int.Parse(result.FirstOrDefault().ItemCount.ToString());


            //var result = (await _context.SupplierTenderQuantityTables.Where(t => t.Id == tableId && t.IsActive == true).Select(d => d.QuantitiyItemsJson).ToListAsync()).SelectMany(r => r.SupplierTenderQuantityTableItems).GroupBy(t => t.ItemNumber).Select(a => a.Count()).ToList();

            //return result.Count == 0 ? 0 : result.Max(a => a);
        }

        public int GetOfferTableQuantityItems(Offer offer, long tableId)
        {
            if (!offer.SupplierTenderQuantityTables.Any(a => a.Id == tableId) || !offer.SupplierTenderQuantityTables.FirstOrDefault(a => a.Id == tableId).QuantitiyItemsJson.SupplierTenderQuantityTableItems.Any())
                return 0;
            var result = offer.SupplierTenderQuantityTables.FirstOrDefault(a => a.Id == tableId).QuantitiyItemsJson.SupplierTenderQuantityTableItems.GroupBy(s => s.ItemNumber).Select(a => a.Count()).ToList();
            return result.Count == 0 ? 0 : result.Max(a => a);
        }
        public async Task<QuantitiesTemplateModel> GetOfferQuantityTableTemplateForApplyOffer(int offerId)
        {
            var entitiess = await _context.Offers
                .Where(t => t.IsActive == true && t.OfferId == offerId).AsTracking()
                .Select(x => new QuantitiesTemplateModel()
                {
                    TemplateYears = x.Tender.TemplateYears,
                    TenderID = x.TenderId,
                    OfferId = x.OfferId,
                    ActivityTemplates = x.Tender.TenderActivities.SelectMany(a => a.Activity.ActivityTemplateVersions.Select(t => t.TemplateId.Value)).Distinct().ToList(),
                    TenderCreatedByTypeId = x.Tender.CreatedByTypeId,
                    TenderIdString = Util.Encrypt(x.TenderId),
                    PreQualificationId = x.Tender.PreQualificationId,
                    InvitationTypeId = x.InvitationTypeId,
                    HasAlternativeOffer = x.Tender.HasAlternativeOffer ?? false,
                    TenderName = x.Tender.TenderName,
                    TenderStatusId = x.Tender.TenderStatusId,
                    ReferenceNumber = x.Tender.ReferenceNumber,
                    TenderNumber = x.Tender.TenderNumber,
                    TenderTypeId = x.Tender.TenderTypeId,
                    OfferPresentationWayId = x.OfferPresentationWayId,

                })
                .FirstOrDefaultAsync();
            return entitiess;
        }

        public async Task<QuantitiesTemplateModel> GetOfferQuantityItems(int offerId, long tableId)
        {
            var entitiess = await _context.Offers
                //.Include(a => a.SupplierTenderQuantityTables)
                //.ThenInclude(a => a.SupplierTenderQuantityTableItems)
                .Where(t => t.IsActive == true && t.OfferId == offerId).AsTracking()
                .Select(x => new QuantitiesTemplateModel()
                {
                    TemplateYears = x.Tender.TemplateYears,
                    TenderID = x.TenderId,
                    OfferId = x.OfferId,
                    ActivityTemplates = x.Tender.TenderActivities.SelectMany(a => a.Activity.ActivityTemplateVersions.Select(t => t.TemplateId.Value)).Distinct().ToList(),
                    TenderCreatedByTypeId = x.Tender.CreatedByTypeId,
                    TenderIdString = Util.Encrypt(x.TenderId),
                    PreQualificationId = x.Tender.PreQualificationId,
                    InvitationTypeId = x.InvitationTypeId,
                    HasAlternativeOffer = x.Tender.HasAlternativeOffer ?? false,
                    TenderName = x.Tender.TenderName,
                    TenderStatusId = x.Tender.TenderStatusId,
                    ReferenceNumber = x.Tender.ReferenceNumber,
                    TenderNumber = x.Tender.TenderNumber,
                    TenderTypeId = x.Tender.TenderTypeId,
                    OfferPresentationWayId = x.OfferPresentationWayId
                }).FirstOrDefaultAsync();
            var offerEntity = await _context.SupplierTenderQuantityTables
                .Where(t => t.OfferId == offerId && t.IsActive == true && t.Offer.IsActive == true && t.Id == tableId)
                .Include(f => f.TenderQuantityTable).Include(f => f.Offer).Include(t => t.QuantitiyItemsJson)
                .ToListAsync();
            var tenderid = 0;
            long tableid = 0;
            var tablename = "";
            foreach (var tbl in offerEntity)
            {
                tablename = tbl.TenderQuantityTable?.Name;
                tableid = tbl.Id;
                tenderid = tbl.Offer.TenderId;
                if (tbl.QuantitiyItemsJson != null)
                {
                    foreach (var itm in tbl.QuantitiyItemsJson.SupplierTenderQuantityTableItems)
                    {
                        entitiess.QuantitiesItems.Add(new TenderQuantityItemDTO
                        {
                            ColumnId = itm.ColumnId,
                            ItemNumber = itm.ItemNumber,
                            ColumnName = "",
                            TableName = tablename,
                            TableId = tableid,
                            TemplateId = itm.ActivityTemplateId,
                            TenderId = tenderid,
                            TenderFormHeaderId = itm.TenderFormHeaderId,
                            Value = itm.Value,
                            AlternativeValue = itm.AlternativeValue,
                            IsDefault = itm.IsDefault,
                            Id = itm.Id
                        });
                    }
                }
            }
            return entitiess;
        }
        public async Task<QuantitiesTemplateModel> GetOfferQuantityItemsForApplyOffer(int offerId)
        {
            var x = await _context.Offers.Include(a => a.Tender).ThenInclude(a => a.TenderQuantityTables)
                .ThenInclude(a => a.QuantitiyItemsJson)
                //.Include(a => a.SupplierTenderQuantityTables)
                //.ThenInclude(a => a.SupplierTenderQuantityTableItems)
                .Where(t => t.IsActive == true && t.OfferId == offerId).AsTracking()
                .FirstOrDefaultAsync();

            var entitiess = new QuantitiesTemplateModel()
            {
                TemplateYears = x.Tender.TemplateYears,
                TenderID = x.TenderId,
                OfferId = x.OfferId,
                FormIds = x.Tender.TenderQuantityTables.Where(t => t.IsActive == true && t.QuantitiyItemsJson != null && t.QuantitiyItemsJson.TenderQuantityTableItems != null && t.QuantitiyItemsJson.TenderQuantityTableItems.Count() > 0).Select(f => f.FormId).Distinct().ToList(),
                TenderCreatedByTypeId = x.Tender.CreatedByTypeId,
                TenderIdString = Util.Encrypt(x.TenderId),
                PreQualificationId = x.Tender.PreQualificationId,
                InvitationTypeId = x.InvitationTypeId,
                HasAlternativeOffer = x.Tender.HasAlternativeOffer ?? false,
                TenderName = x.Tender.TenderName,
                TenderStatusId = x.Tender.TenderStatusId,
                ReferenceNumber = x.Tender.ReferenceNumber,
                TenderNumber = x.Tender.TenderNumber,
                TenderTypeId = x.Tender.TenderTypeId,
                OfferPresentationWayId = x.OfferPresentationWayId,
            };
            return entitiess;
        }

        #endregion

        private Enums.AttachmentType GetFileType(SupplierAttachment supplierattachment)
        {
            if (supplierattachment is SupplierTechnicalProposalAttachment)
            {
                return Enums.AttachmentType.SupplierTechnicalProposalAttachment;
            }
            else if (supplierattachment is SupplierFinancialProposalAttachment)
            {
                return Enums.AttachmentType.SupplierFinancialProposalAttachment;
            }
            else if (supplierattachment is SupplierFinancialandTechnicalProposalAttachment)
            {
                return Enums.AttachmentType.SupplierTechnicalAndFinancialProposalAttachment;
            }
            else
            {
                return Enums.AttachmentType.SupplierCombinedAttachment;
            }
        }
        private string GetFileTypeName(SupplierAttachment supplierattachment)
        {
            if (supplierattachment is SupplierTechnicalProposalAttachment)
            {
                return "عرض فنى";
            }
            else if (supplierattachment is SupplierFinancialProposalAttachment)
            {
                return "عرض مالى";
            }
            else if (supplierattachment is SupplierFinancialandTechnicalProposalAttachment)
            {
                return "ملف مالى وفنى";
            }
            else
            {
                return "ملف تضامن";
            }
        }

        public async Task<SupplierAttachment> FindSupplierAttachmentById(string referenceNumber)
        {
            return await _context
                  .SupplierAttachmentes.Include(d => d.Offer).FirstOrDefaultAsync(d => d.FileNetReferenceId == referenceNumber);
        }
        public async Task<SupplierAttachment> FindSupplierAttachmentById(int id)
        {
            return await _context
                  .SupplierAttachmentes.FirstOrDefaultAsync(d => d.AttachmentId == id);
        }
        public async Task<OfferQuantityTableModel> FindOfferQuantityTableModel(int offerId)
        {

            Check.ArgumentNotNullOrEmpty(nameof(offerId), offerId.ToString());
            var entities = await _context.Offers.Where(d => d.IsActive == true && d.OfferId == offerId)
                .Select(t => new OfferQuantityTableModel
                {
                    IsSolidarity = t.IsSolidarity,
                    OfferIdString = Util.Encrypt(t.OfferId),
                    OfferStatusModel = new OfferStatusModel
                    {
                        OfferStatusId = t.OfferStatusId,
                        offerStatusName = t.Status.NameAr
                    }
                }
                ).FirstOrDefaultAsync();

            return entities;
        }
        public async Task<OfferSolidarityModel> FindOfferSolidarity(int offerId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(offerId), offerId.ToString());
            var entities = await _context.Offers.Include(d => d.Tender).Where(t => t.IsActive == true && t.OfferId == offerId /*&& t.OfferStatusId != (int)Enums.OfferStatus.Canceled*/)
                .Select(t => new OfferSolidarityModel
                {
                    IsSolidarity = t.IsSolidarity,
                    TenderId = t.Tender.TenderId,
                    TenderTypeId = t.Tender.TenderTypeId,
                    statusId = t.OfferStatusId,
                    offerIdString = Util.Encrypt(t.OfferId),
                    TenderIdString = Util.Encrypt(t.Tender.TenderId),
                    InsideKSA = t.Tender.InsideKSA,
                    LastOfferPresentationDate = t.Tender.LastOfferPresentationDate,
                    OffersOpeningDate = t.Tender.OffersOpeningDate,
                    CommericailNo = t.CommericalRegisterNo

                }).FirstOrDefaultAsync();
            return entities;
        }

        public async Task<Offer> findOfferbyCRandTenderID(int tenderId, string CR)
        {
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            var entities = await _context.Offers.Where(d => d.TenderId == tenderId && d.CommericalRegisterNo == CR).FirstOrDefaultAsync();
            return entities;
        }

        #endregion



        #region Open Offers
        public async Task<OpenOfferModel> GetOpenOfferbyId(int offerid)
        {
            var Query = await _context.Offers
                     .Where(x => x.OfferId == offerid)
                        .Select(off => new OpenOfferModel
                        {
                            OfferAttachmentModels = off.Attachment.Select(s => new OfferAttachmentModel
                            {
                                FileName = s.FileName,
                                FileNetId = s.FileNetReferenceId,
                                FiletypeName = DefineAttachmentTypeName(s),
                                FiletypeID = DefineAttachmentTypeId(s),
                                TenderStatusId = off.Tender.TenderStatusId,
                                OfferPresentationWayId = off.Tender.OfferPresentationWayId
                            }).ToList(),
                            OfferIdString = Util.Encrypt(off.OfferId),
                            IsSolidriaty = off.IsSolidarity,
                            TenderDataTabModel = new TenderDataTabModel { TenderName = off.Tender.TenderName, tenderStatusId = off.Tender.TenderStatusId, RefNumber = off.Tender.ReferenceNumber, tenderIdString = Util.Encrypt(off.Tender.TenderId), tenderStatus = (Enums.TenderStatus)off.Tender.TenderStatusId, OfferPresentationWayId = (Enums.OfferPresentationWayId)off.Tender.OfferPresentationWayId, TenderTypeId = off.Tender.TenderTypeId },
                            OfferStatus = (Enums.OfferStatus)off.OfferStatusId,
                            QuantityTableforInsertModel = new QuantityTableforInsertModel { tenderIdString = Util.Encrypt(off.TenderId), offerIdString = Util.Encrypt(off.OfferId) }
                        }).FirstOrDefaultAsync();
            return Query;
        }

        public async Task<QueryResult<CombinedSupplierModel>> GetCombinedSuppliersByOfferid(CombinedSupplierModel model)
        {
            var Query = await _context.OfferSolidarities.Where(x => x.OfferId == Util.Decrypt(model.OfferIdString))
                           .Select(y => new CombinedSupplierModel
                           {
                               SupplierName = y.Supplier.SelectedCrName,
                               SupplierCr = y.CRNumber,
                               roleName = DefineSolidarityTypeName(y)
                           }).ToQueryResult(model.PageNumber, model.PageSize);
            return Query;
        }

        public async Task<QueryResult<CombinedSupplierModel>> GetCombinedSuppliers(CombinedSupplierModel model, int userId)
        {
            var Query = await _context.OfferSolidarities
                               .Where(x => x.OfferId == Util.Decrypt(model.OfferIdString))
                               .Select(comb => new CombinedSupplierModel
                               {
                                   CombinedIdString = Util.Encrypt(comb.Id),
                                   CombinedId = comb.Id,
                                   IsOwner = comb.SolidarityTypeId == (int)Enums.UnRegisteredSuppliersInvitationType.SolidarityLeader,
                                   roleName = DefineSolidarityTypeName(comb),
                                   SupplierName = comb.Supplier.SelectedCrName,
                                   SupplierCr = comb.CRNumber,
                                   OfferId = Util.Decrypt(model.OfferIdString),
                               }).ToQueryResult(model.PageNumber, model.PageSize);
            return Query;
        }

        public async Task<OfferModel> GeOfferByTenderIdAndOfferIdForOpening(int tenderId, int offerId)
        {
            var result = await _context.Offers
                .Where(o => o.IsActive == true && o.TenderId == tenderId && o.OfferId == offerId)
                .Select(offer => new OfferModel
                {
                    FinalPrice = offer.FinalPriceAfterDiscount,
                    OfferId = offer.OfferId,
                    IsSolidarity = offer.IsSolidarity,
                    DiscountNotes = offer.DiscountNotes,
                    Discount = offer.Discount,
                    OfferStatusId = offer.OfferStatusId,
                    TenderId = offer.TenderId,
                    TenderIdString = Util.Encrypt(offer.TenderId),
                    CommericalRegisterNo = offer.CommericalRegisterNo,
                    TenderStatusId = offer.Tender.TenderStatusId,
                    OfferPresentationWayId = offer.Tender.OfferPresentationWayId,
                    TenderTypeId = offer.Tender.TenderTypeId,
                    CombinedIdString = Util.Encrypt(offer.Combined.Select(x => x.Id).FirstOrDefault()),
                    Attachment = offer.Attachment.Select(a => new SupplierAttachmentModel
                    {
                        AttachmentId = a.AttachmentId,
                        FileName = a.FileName,
                        FileNetReferenceId = a.FileNetReferenceId,
                        OfferId = a.OfferId
                    }).ToList(),
                    CombinedSupplierModel = offer.Combined.Select(y => new CombinedSupplierModel
                    {
                        SupplierName = y.Supplier.SelectedCrName,
                    }).ToList(),
                }).FirstOrDefaultAsync();
            return result;
        }

        public async Task<OfferDetailsDisplayModel> GetFinancialOfferDetailsForDisplay(int offerId)
        {
            var result = await _context.Offers
                .Where(o => o.IsActive == true && o.OfferId == offerId)
                .Select(offer => new OfferDetailsDisplayModel
                {
                    TenderIdString = Util.Encrypt(offer.TenderId),
                    IsSolidarity = offer.IsSolidarity,
                    OfferPresentationWayId = offer.Tender.OfferPresentationWayId,
                    TenderTypeId = offer.Tender.TenderTypeId,
                    OfferIdString = Util.Encrypt(offer.OfferId),
                    CombinedIdString = !offer.IsSolidarity ? Util.Encrypt(offer.Combined.Where(c => c.SolidarityTypeId == (int)Enums.UnRegisteredSuppliersInvitationType.SolidarityLeader).Select(x => x.Id).FirstOrDefault()) : "",
                    Discount = offer.Discount,
                    DiscountNotes = offer.DiscountNotes,
                }).FirstOrDefaultAsync();
            return result;
        }
        #endregion

        public async Task<bool> CanAddExclusionReasonIfTenderHasExtendOfferValidity(int tenderId)
        {
            var result = await _context.ExtendOffersValiditys
              .Where(e => e.AgencyCommunicationRequest.TenderId == tenderId && e.IsActive == true && e.AgencyCommunicationRequest.IsActive == true)
              .Where(e => e.AgencyCommunicationRequest.StatusId != (int)Enums.AgencyCommunicationRequestStatus.Finished)
              .AnyAsync();
            return result;
        }

        public async Task<long> addOfferQTItemsJson(List<SupplierTenderQuantityTableItem> items, long TableId, long offerId)
        {
            var dt = new DataTable();
            dt.Columns.Add("Id", typeof(long));
            dt.Columns.Add("ColumnId", typeof(long));
            dt.Columns.Add("TenderFormHeaderId", typeof(long));
            dt.Columns.Add("ActivityTemplateId", typeof(int));
            dt.Columns.Add("ItemNumber", typeof(long));
            dt.Columns.Add("Value", typeof(string));
            dt.Columns.Add("AlternativeValue", typeof(string));
            dt.Columns.Add("IsDefault", typeof(bool));

            foreach (var item in items)
                dt.Rows.Add(item.Id, item.ColumnId, item.TenderFormHeaderId, item.ActivityTemplateId, item.ItemNumber, item.Value, item.AlternativeValue, item.IsDefault);

            var OfferQuantityItems = new SqlParameter("@items", System.Data.SqlDbType.Structured);
            OfferQuantityItems.Value = dt;
            OfferQuantityItems.TypeName = "dbo.OfferQuantityItems";

            var result = await _context.SP_ConvertOfferQTItemsToJson.FromSqlRaw("exec SP_convertOfferQTItemsToJson {0}, {1}, {2}", TableId, offerId, @OfferQuantityItems).ToListAsync();
            return result.FirstOrDefault().ItemsJsonId;
        }

        public async Task<OfferlocalContentDetails> GetOfferLocalContentDetailsByOfferId(int offerId)
        {
            return await _context.OfferlocalContentDetails.Where(x => x.OfferId == offerId).FirstOrDefaultAsync();
        }
        public async Task<OfferlocalContentDetails> GetOfferLocalContentDetailsWithOfferByOfferId(int offerId)
        {
            return await _context.OfferlocalContentDetails.Include(o => o.Offer).Where(x => x.OfferId == offerId).FirstOrDefaultAsync();
        }

        public async Task<List<Offer>> GetIdenticalOffersWithLocalContentByTenderId(int tenderId)
        {
            var offers = await _context.Offers.Include(x => x.OfferlocalContentDetails).Where(x => x.IsActive == true && x.TenderId == tenderId && x.OfferTechnicalEvaluationStatusId == (int)OfferTechnicalEvaluationStatusId.IdenticalOffer).ToListAsync();
            return offers;
        }

        public async Task<MoneyMarketSuppliers> GetSupplierFromMonayMarketWithCr(string cr)
        {
            var result = await _context.MoneyMarketSuppliers.FirstOrDefaultAsync(c => c.SupplierCr == cr);
            return result;
        }
    }
}
