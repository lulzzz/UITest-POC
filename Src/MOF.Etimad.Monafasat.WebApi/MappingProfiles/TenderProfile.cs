using AutoMapper;
using MOF.Etimad.Monafasat.Core;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.ViewModel.AgencyBudget;
using MOF.Etimad.Monafasat.ViewModel.DTO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using static MOF.Etimad.Monafasat.SharedKernel.Enums;

namespace MOF.Etimad.Monafasat.Reporting
{
    public class TenderProfile : Profile
    {
        public TenderProfile()
        {
            TenderInformationsMapping();
            TenderDetailsMapping();
            TenderBasicDataCreationMapping();
            TenderBasicDataMapping();
            BasicTenderMapping();
            TenderSearchCriteriaMap();
            TenderRelationsMap();
            TenderAttachmentsMap();
            VacationDatesMap();
            TenderDatesMap();
            SuppliersEnquirtMap();
            TenderRevisionDateMap();
            TenderQuantityTableMaping();
            TenderDashboardMap();
            TenderQuantityTableChangesMaping();
            TenderAttachmentsChangesMaping();
            TenderCommitteesMapping();
            TenderAgencyBudgetNumberCreationMapping();
            TenderOffersMapping();
            VROTendersMapping();

        }

        private void VROTendersMapping()
        {
            CultureInfo arProvider = CultureInfo.CreateSpecificCulture("ar-SA");
            CultureInfo enProvider = CultureInfo.CreateSpecificCulture("en-USA");

            CreateMap<VROTendersDTO, VROTendersCreatedByGovAgencyModel>()
                                                    .ForMember(tm => tm.TenderIdString, opt => opt.MapFrom(t => Util.Encrypt(t.TenderId)))
                                                    .ForMember(tm => tm.AgencyIdString, opt => opt.MapFrom(t => Util.Encrypt(t.AgencyCode)))
                                                    .ForMember(tm => tm.BranchIdString, opt => opt.MapFrom(t => Util.Encrypt(t.BranchId)))
                                                    .ForMember(tm => tm.SubmitionDateHijri, opt => opt.MapFrom(t => t.SubmitionDate.HasValue ? t.SubmitionDate.Value.ToString("yyyy-MM-dd", arProvider) : ""))
                                                    .ForMember(tm => tm.LastEnqueriesDateHijri, opt => opt.MapFrom(t => t.LastEnqueriesDate.HasValue ? t.LastEnqueriesDate.Value.ToString("yyyy-MM-dd", arProvider) : ""))
                                                    .ForMember(tm => tm.OffersOpeningDateHijri, opt => opt.MapFrom(t => t.OffersOpeningDate.HasValue ? t.OffersOpeningDate.Value.ToString("yyyy-MM-dd", arProvider) : ""))
                                                    .ForMember(tm => tm.OffersCheckingDateHijri, opt => opt.MapFrom(t => t.OffersCheckingDate.HasValue ? t.OffersCheckingDate.Value.ToString("yyyy-MM-dd", arProvider) : ""))
                                                    .ForMember(tm => tm.LastOfferPresentationDateHijri, opt => opt.MapFrom(t => t.LastOfferPresentationDate.HasValue ? t.LastOfferPresentationDate.Value.ToString("yyyy-MM-dd", arProvider) : ""))
                                                    .ForMember(tm => tm.OffersOpeningTime, opt => opt.MapFrom(t => t.OffersOpeningDate.HasValue ? t.OffersOpeningDate.Value.ToString("hh:mm tt", enProvider) : ""))
                                                    .ForMember(tm => tm.OffersCheckingTime, opt => opt.MapFrom(t => t.OffersCheckingDate.HasValue ? t.OffersCheckingDate.Value.ToString("hh:mm tt", enProvider) : ""))
                                                    .ForMember(tm => tm.LastOfferPresentationTime, opt => opt.MapFrom(t => t.LastOfferPresentationDate.HasValue ? t.LastOfferPresentationDate.Value.ToString("hh:mm tt", enProvider) : ""));
        }
        private void TenderOffersMapping()
        {


            CreateMap<Offer, TenderCheckOfferModel>().ForMember(tm => tm.InvitationPurchaseDate,
                                                     opt => opt.MapFrom(t => (t.Tender.Invitations.FirstOrDefault(i => i.CommericalRegisterNo == t.CommericalRegisterNo).SubmissionDate)))
                                                     .ForMember(tm => tm.InvitationTypeName, opt => opt.MapFrom(t => (t.Tender.TenderTypeId == (int)Enums.TenderType.Competition && t.Tender.InvitationTypeId == (int)Enums.InvitationType.Public) ? "لا يوجد" :
                                                     t.Tender.Invitations.Any(i => i.CommericalRegisterNo == t.CommericalRegisterNo) ? t.Tender.Invitations.FirstOrDefault(i => i.IsActive == true && i.CommericalRegisterNo == t.CommericalRegisterNo).InvitationStatus.NameAr : ""))
                                                     .ForMember(tm => tm.CommericalRegisterName, opt => opt.MapFrom(t => t.Supplier.SelectedCrName))

                                                     .ForMember(tm => tm.OfferAcceptanceStatusId, opt => opt.MapFrom(t => t.OfferAcceptanceStatusId))
                                                     .ForMember(tm => tm.OfferPrice, opt => opt.MapFrom(t => t.FinalPriceAfterDiscount))
                                                     .ForMember(tm => tm.OfferAcceptanceStatusId, opt => opt.MapFrom(t => t.OfferAcceptanceStatusId))
                                                     .ForMember(tm => tm.TenderAwardingType, opt => opt.MapFrom(t => t.Tender.TenderAwardingType))
                                                     .ForMember(tm => tm.CheckingDate, opt => opt.MapFrom(t => t.Tender.OffersCheckingDate))
                                                     .ForMember(tm => tm.OfferStatus, opt => opt.MapFrom(t => t.Status != null ? t.Status.NameAr : ""))
                                                     .ForMember(tm => tm.InvitationPurchaseStatus,
                                                     opt => opt.MapFrom(t => (t.Tender.Invitations.FirstOrDefault(i => i.CommericalRegisterNo == t.CommericalRegisterNo).InvitationStatus.NameAr)))
                                                     .ForMember(tm => tm.BaiscActivity, opt => opt.MapFrom(t => t.Tender.TenderActivities.Select(s => s.Activity.NameAr).FirstOrDefault()))
                                                     .ForMember(tm => tm.IsChecked, opt => opt.MapFrom(t => (t.TotalOfferAwardingValue.HasValue && t.TotalOfferAwardingValue > 0) ||
                                                     (t.PartialOfferAwardingValue.HasValue && t.PartialOfferAwardingValue > 0)));


            CreateMap<Tender, TenderOffersModel>().ForMember(tm => tm.TenderIdString, opt => opt.MapFrom(t => (Util.Encrypt(t.TenderId))))
                                                 .ForMember(tm => tm.TenderRefrenceNumber, opt => opt.MapFrom(t => t.ReferenceNumber))
                                                 .ForMember(tm => tm.IsPasssPreqaulification, opt => opt.MapFrom(t => t.PreQualification != null ? (t.PreQualification.QualificationFinalResults.Any(q => q.QualificationLookupId == (int)Enums.QualificationResultLookup.Succeeded) ? true : false) : false))
                                                 .ForMember(tm => tm.TenderAwardingType, opt => opt.MapFrom(t => t.TenderAwardingType))
                                                 .ForMember(tm => tm.TenderTypeName, opt => opt.MapFrom(t => t.TenderType.NameAr))
                                                 .ForMember(tm => tm.ContainSupply, opt => opt.MapFrom(t => t.ContainSupply))
                                                 .ForMember(tm => tm.OffersCount, opt => opt.MapFrom(t => t.Offers.Where(x => (x.IsActive == true && x.OfferStatusId == (int)Enums.OfferStatus.Received))
                                                 .Where(x => (x.OfferTechnicalEvaluationStatusId.HasValue && x.Tender.OfferPresentationWayId == (int)OfferPresentationWayId.TwoSepratedFiles)
                                                    && (x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersTechnicalCheckingConfirmed || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersFinancialChecking
                                                    || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersFinancialOfferCheckingPending || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersFinancialOfferCheckingApproved
                                                    || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersFinancialOfferCheckingRejected || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersOpenFinancialStage)
                                                    ? x.OfferTechnicalEvaluationStatusId == (int)Enums.OfferTechnicalEvaluationStatusId.IdenticalOffer
                                                    : (x.OfferTechnicalEvaluationStatusId == null || x.OfferTechnicalEvaluationStatusId == (int)Enums.OfferTechnicalEvaluationStatusId.IdenticalOffer || x.OfferTechnicalEvaluationStatusId == (int)Enums.OfferTechnicalEvaluationStatusId.NotIdenticalOffer)).Count()))

                                                 .ForMember(tm => tm.TenderAwardingType, opt => opt.MapFrom(t => t.Offers.Where(x => x.IsActive == true && x.OfferStatusId == (int)Enums.OfferStatus.Received).Count() <= 1 ? true : t.TenderAwardingType))

                                                 .ForMember(tm => tm.TenderAreaNameList, opt => opt.MapFrom(t => t.InsideKSA == true ? t.TenderAreas.Select(y => y.Area.NameAr).ToList() : t.TenderCountries.Select(y => y.Country.NameAr).ToList()))
                                                 .ForMember(tm => tm.InvitationsCount, opt => opt.MapFrom(t => t.Invitations.Where(inv => inv.StatusId == (int)Enums.InvitationStatus.Approved && inv.IsActive == true).Count()))
                                                 .ForMember(tm => tm.RejectionReason, opt => opt.MapFrom(t => t.TenderHistories.OrderByDescending(th => th.TenderHistoryId).FirstOrDefault().RejectionReason))
                                                 .ForMember(tm => tm.IsValidToGoToFinancailCheck, opt => opt.MapFrom(t => t.Offers.Where(o => o.OfferTechnicalEvaluationStatusId == (int)Enums.OfferTechnicalEvaluationStatusId.IdenticalOffer).All(o => o.IsOfferFinancialDetailsEntered == true)))
                                                 .ForMember(tm => tm.IsTenderFinancialChecked, opt => opt.MapFrom(t => t.Offers.Where(o => o.OfferTechnicalEvaluationStatusId == (int)Enums.OfferTechnicalEvaluationStatusId.IdenticalOffer).All(o => o.OfferAcceptanceStatusId.HasValue)))
                                                 .ForMember(tm => tm.IsTenderTechnicalChecked, opt => opt.MapFrom(t => t.Offers.Where(o => o.OfferStatusId == (int)Enums.OfferStatus.Received).All(o => o.OfferTechnicalEvaluationStatusId.HasValue)))
                                                 .ForMember(tm => tm.DoIHaveApprovedSideAction, opt => opt.MapFrom(t => (t.PostQualifications.Where(q => q.IsActive == true && q.TenderStatusId != (int)Enums.TenderStatus.OffersCheckedConfirmed)
                                                 .Any(q => q.TenderStatusId != (int)Enums.TenderStatus.UnderEstablishing && q.TenderStatusId != (int)Enums.TenderStatus.Established &&
                                                 q.TenderStatusId != (int)Enums.TenderStatus.Pending && q.TenderStatusId != (int)Enums.TenderStatus.Rejected)) ||
                                                 (t.AgencyCommunicationRequests.Where(q => q.IsActive == true && q.StatusId != (int)Enums.AgencyCommunicationRequestStatus.Finished)
                                                 .Any(a => a.StatusId == (int)Enums.AgencyCommunicationRequestStatus.Approved))))
                                                 .ForMember(tm => tm.DoIHavePendingSideAction, opt => opt.MapFrom(t => (t.PostQualifications.Where(q => q.IsActive == true && q.TenderStatusId != (int)Enums.TenderStatus.OffersCheckedConfirmed)
                                                 .Any(q => q.TenderStatusId != (int)Enums.TenderStatus.UnderEstablishing && q.TenderStatusId != (int)Enums.TenderStatus.Established &&
                                                 q.TenderStatusId != (int)Enums.TenderStatus.Pending && q.TenderStatusId != (int)Enums.TenderStatus.Rejected)) ||
                                                 (t.AgencyCommunicationRequests.Where(q => q.IsActive == true && q.StatusId != (int)Enums.AgencyCommunicationRequestStatus.Finished)
                                                 .Any(a => !(a.StatusId == (int)Enums.AgencyCommunicationRequestStatus.Approved)))))
                                                 .ForMember(tm => tm.InvitationsCount, opt => opt.MapFrom(t => t.Invitations.Where(inv => inv.StatusId == (int)Enums.InvitationStatus.Approved && inv.IsActive == true).Count()))
                                                 .ForMember(tm => tm.RejectionReason, opt => opt.MapFrom(t => t.TenderHistories.OrderByDescending(th => th.TenderHistoryId).FirstOrDefault().RejectionReason))
                                                 .ForMember(tm => tm.HasCheckingDate, opt => opt.MapFrom(t => t.OffersCheckingDate.HasValue ? true : false))
                                                 .ForMember(tm => tm.CheckingDate, opt => opt.MapFrom(t => t.OffersCheckingDate))
                                                 .ForMember(tm => tm.UncheckedCombinedSuppliers,
                                                 opt => opt.MapFrom(t => t.Offers.Where(o => o.IsActive == true && o.OfferStatusId == (int)Enums.OfferStatus.Received).SelectMany(o => o.Combined.Where(c => c.SupplierCombinedDetail == null)).ToList().Count))
                                                 .ForMember(tm => tm.CheckingDateSet, opt => opt.MapFrom(t => t.CheckingDateSet.HasValue ? t.CheckingDateSet.Value : false))
                                                 .ForMember(tm => tm.IsAllOffersNotIdenticallyMatched, opt => opt.MapFrom(t => t.Offers.All(a => a.OfferTechnicalEvaluationStatusId == (int)Enums.OfferTechnicalEvaluationStatusId.NotIdenticalOffer)))
                                                 .ForMember(tm => tm.BiddingRoundStartDate, opt => opt.MapFrom(t => t.BiddingRounds.Any(a => a.IsActive == true && a.BiddingRoundStatusId == (int)Enums.BiddingRoundStatus.New) ? t.BiddingRounds.FirstOrDefault(a => a.IsActive == true && a.BiddingRoundStatusId == (int)Enums.BiddingRoundStatus.New).StartDate : DateTime.Now))
                                                 .ForMember(tm => tm.BiddingRoundEndDate, opt => opt.MapFrom(t => t.BiddingRounds.Any(a => a.IsActive == true && a.BiddingRoundStatusId == (int)Enums.BiddingRoundStatus.New) ? t.BiddingRounds.FirstOrDefault(a => a.IsActive == true && a.BiddingRoundStatusId == (int)Enums.BiddingRoundStatus.New).EndDate : DateTime.Now));
        }

        #region Mappping

        private void TenderBasicDataMapping()
        {
            CreateMap<Tender, TenderBasicDataModel>()
                .ForMember(m => m.TenderStatusId, opt => opt.MapFrom(src => src.TenderStatusId))
                .ForMember(tm => tm.OffersCheckingCommitteeName, opt => opt.MapFrom(t => t.OffersCheckingCommittee.CommitteeName))
                .ForMember(tm => tm.OffersOpeningCommitteeName, opt => opt.MapFrom(t => t.OffersOpeningCommittee.CommitteeName))
                .ForMember(tm => tm.TechnicalOrganizationName, opt => opt.MapFrom(t => t.TechnicalOrganization.CommitteeName))
                .ForMember(tm => tm.TenderStatusName, opt => opt.MapFrom(t => t.Status.NameAr))
                .ForMember(tm => tm.AgencyName, opt => opt.MapFrom(t => t.Agency.NameArabic))
                .ForMember(tm => tm.TenderTypeName, opt => opt.MapFrom(t => t.TenderType.NameAr))
                .ForMember(tm => tm.OffersOpeningAddress, opt => opt.MapFrom(t => t.OffersOpeningAddress.AddressName))
                .ForMember(m => m.TenderIdString, opt => opt.MapFrom(src => (Util.Encrypt(src.TenderId))))
                .ForMember(m => m.TenderStatusIdString, opt => opt.MapFrom(src => (Util.Encrypt(src.TenderStatusId))))
                .ForMember(tm => tm.RemainingDays, opt => opt.MapFrom(t => t.LastOfferPresentationDate.HasValue ? (t.LastOfferPresentationDate.Value - DateTime.Now).Days : 0))
                .ForMember(tm => tm.RemainingHours, opt => opt.MapFrom(t => t.LastOfferPresentationDate.HasValue ? (t.LastOfferPresentationDate.Value - DateTime.Now).Hours : 0))
                .ForMember(tm => tm.RemainingMins, opt => opt.MapFrom(t => t.LastOfferPresentationDate.HasValue ? (t.LastOfferPresentationDate.Value - DateTime.Now).Minutes : 0));
        }
        private void TenderBasicDataCreationMapping()
        {
            CreateMap<Tender, CreateTenderBasicDataModel>()
                .ForMember(m => m.TenderIdString, opt => opt.MapFrom(src => (Util.Encrypt(src.TenderId))))
                .ForMember(m => m.TenderStatusIdString, opt => opt.MapFrom(src => (Util.Encrypt(src.TenderStatusId))))
                .ForMember(m => m.TenderAgreementAgencyIDs, opt => opt.MapFrom(src => src.TenderAgreementAgencies.Select(t => t.AgencyCode)));
        }

        private void TenderAgencyBudgetNumberCreationMapping()
        {
            CreateMap<AgencyBudgetNumber, AgencyBudgetNumberModel>()
         .ForMember(m => m.Cost, opt => opt.MapFrom(src => (src.Cost)))
         .ForMember(m => m.Cache, opt => opt.MapFrom(src => (src.Cache)))
           .ForMember(m => m.ProjectNumber, opt => opt.MapFrom(src => (src.ProjectNumber)))
            .ForMember(m => m.TenderId, opt => opt.MapFrom(src => (src.TenderId)))
                .ForMember(m => m.IsProject, opt => opt.MapFrom(src => (src.IsProject)))
         .ForMember(m => m.ProjectName, opt => opt.MapFrom(src => src.ProjectName));
            CreateMap<AgencyBudgetNumberModel, AgencyBudgetNumber>()
            .ConstructUsing(tr => new AgencyBudgetNumber(tr.TenderId, tr.ProjectNumber, tr.ProjectName, tr.Cost, tr.Cache, tr.IsProject));
        }

        private void TenderDetailsMapping()
        {
            CreateMap<Tender, TenderDetailsModel>()
                .ForMember(tm => tm.TenderStatusId, opt => opt.MapFrom(t => t.TenderStatusId))
                .ForMember(tm => tm.TenderIdString, opt => opt.MapFrom(t => (Util.Encrypt(t.TenderId))))
                .ForMember(tm => tm.InvitationStatusId, opt => opt.ResolveUsing<int>((src, dst, arg, context) =>
                                       GetInvitationStatus(src, context)))
                .ForMember(tm => tm.IsPurchased, opt => opt.ResolveUsing<bool>((src, dst, arg, context) =>
                                       CheckIsSypplierPurchaseTheTender(src, context)))
                .ForMember(tm => tm.TenderStatusIdString, opt => opt.MapFrom(t => (Util.Encrypt(t.TenderStatusId))));
        }
        //this TenderInformationModel model used for pages that require to show some info about a tender
        private void TenderInformationsMapping()
        {
            CreateMap<Tender, TenderInformationModel>()
                .ForMember(tm => tm.TenderTypeName, opt => opt.MapFrom(t => t.TenderType.NameAr))
                .ForMember(tm => tm.TenderIdString, opt => opt.MapFrom(t => (Util.Encrypt(t.TenderId))))
                .ForMember(tm => tm.TenderStatusIdString, opt => opt.MapFrom(t => (Util.Encrypt(t.TenderStatusId))));
        }
        private void TenderCommitteesMapping()
        {
            CreateMap<Tender, EditeCommitteesModel>();
            CreateMap<Tender, TenderSamplesAddressModel>();
            CreateMap<Tender, TenderAreasModel>()
            .ForMember(tm => tm.TenderAreaIDs, opt => opt.MapFrom(t => t.TenderAreas.Select(a => a.AreaId).ToList()))
            .ForMember(tm => tm.TenderCountriesIDs, opt => opt.MapFrom(t => t.TenderCountries.Select(a => a.CountryId).ToList()));
        }
        private void BasicTenderMapping()
        {
            CultureInfo arProvider = CultureInfo.CreateSpecificCulture("ar-SA");

            CreateMap<Tender, BasicTenderModel>().ForMember(tm => tm.InvitationTypeName, opt => opt.MapFrom(t => t.InvitationType.NameAr))
                                                 .ForMember(tm => tm.TenderTypeName, opt => opt.MapFrom(t => t.TenderType.NameAr))
                                                 .ForMember(tm => tm.AgencyName, opt => opt.MapFrom(t => t.Agency.NameArabic))

                                                 .ForMember(tm => tm.BranchName, opt => opt.MapFrom(t => t.Branch.BranchName))

                                                 .ForMember(tm => tm.TenderChangeRequestIds, opt => opt.MapFrom(t => t.ChangeRequests.Where(c => c.IsActive == true).Select(x => x.TenderChangeRequestId).ToList()))
                                                 .ForMember(tm => tm.ChangeRequestStatusIds, opt => opt.MapFrom(t => t.ChangeRequests.Where(c => c.IsActive == true).Select(x => x.ChangeRequestStatusId).ToList()))
                                                 .ForMember(tm => tm.ChangeRequestStatusNames, opt => opt.MapFrom(t => t.ChangeRequests.Where(c => c.IsActive == true).Select(x => x.ChangeRequestStatus.NameAr).ToList()))
                                                 .ForMember(tm => tm.ChangeRequestTypeId, opt => opt.MapFrom(t => t.ChangeRequests.Where(c => c.IsActive == true).Select(x => x.ChangeRequestTypeId).FirstOrDefault()))
                                                 .ForMember(tm => tm.QuantitiesTableStatus, opt => opt.ResolveUsing<int>((src, dst, arg, context) =>
                                                                GetQuantitiesChangeStatus(src, context)))
                                                  .ForMember(tm => tm.ExtendDatesStatus, opt => opt.ResolveUsing<int>((src, dst, arg, context) =>
                                                                GetDatesChangeStatus(src, context)))
                                                  .ForMember(tm => tm.AttachmentsStatus, opt => opt.ResolveUsing<int>((src, dst, arg, context) =>
                                                                GetAttachmentsChangeStatus(src, context)))
                                                 .ForMember(tm => tm.CancelRequestStatus, opt => opt.ResolveUsing<int>((src, dst, arg, context) =>
                                                                GetCancelChangeStatus(src, context)))
                                                 .ForMember(tm => tm.ChangeRequestedBy, opt => opt.ResolveUsing<string>((src, dst, arg, context) =>
                                                                GetCancelRole(src, context)))
                                                 .ForMember(tm => tm.TenderStatusName, opt => opt.MapFrom(t => t.Status.NameAr))
                                                 .ForMember(tm => tm.TechnicalOrganizationName, opt => opt.MapFrom(t => t.TechnicalOrganization.CommitteeName))
                                                 .ForMember(tm => tm.OffersOpeningCommitteeName, opt => opt.MapFrom(t => t.OffersOpeningCommittee.CommitteeName))
                                                 .ForMember(tm => tm.CreatedDate, opt => opt.MapFrom(t => t.CreatedAt))
                                                 .ForMember(tm => tm.CreatedBy, opt => opt.MapFrom(t => t.CreatedBy))
                                                 .ForMember(tm => tm.BranchId, opt => opt.MapFrom(t => t.BranchId))
                                                 .ForMember(tm => tm.ApprovedBy, opt => opt.MapFrom(t => t.TenderHistories.OrderByDescending(h => h.TenderHistoryId).FirstOrDefault(h => h.StatusId == (int)Enums.TenderStatus.Approved).CreatedBy))
                                                 .ForMember(tm => tm.AgencyName, opt => opt.MapFrom(t => t.Agency.NameArabic))
                                                 .ForMember(tm => tm.SubmitionDate, opt => opt.MapFrom(t => t.SubmitionDate))
                                                 .ForMember(tm => tm.TenderIdString, opt => opt.MapFrom(t => (Util.Encrypt(t.TenderId))))
                                                 .ForMember(tm => tm.TenderStatusIdString, opt => opt.MapFrom(t => (Util.Encrypt(t.TenderStatusId))))
                                                 .ForMember(tm => tm.InvitationStatusId, opt => opt.ResolveUsing<int>((src, dst, arg, context) =>
                                                                    GetInvitationStatus(src, context)))
                                                 .ForMember(tm => tm.OffersCheckingCommitteeName, opt => opt.MapFrom(t => t.OffersCheckingCommittee.CommitteeName))
                                                 .ForMember(tm => tm.HasEnquiry, opt => opt.MapFrom(t => t.Enquiries.Any(e => e.TenderId == t.TenderId && e.IsActive == true) ? true : false))
                                                 .ForMember(tm => tm.EnquirySendingDate, opt => opt.MapFrom(t => t.Enquiries.Select(e => e.CreatedAt)))
                                                 .ForMember(tm => tm.IsPurchased, opt => opt.MapFrom(t => t.ConditionsBooklets.Any() ? true : false))
                                                 .ForMember(tm => tm.CreatedDateHijri, opt => opt.MapFrom(t => t.CreatedAt.ToString("yyyy-MM-dd", arProvider)))
                                                 .ForMember(tm => tm.SubmitionDateHijri, opt => opt.MapFrom(t => t.SubmitionDate.HasValue ? t.SubmitionDate.Value.ToString("yyyy-MM-dd", arProvider) : ""))
                                                 .ForMember(tm => tm.LastEnqueriesDateHijri, opt => opt.MapFrom(t => t.LastEnqueriesDate.HasValue ? t.LastEnqueriesDate.Value.ToString("yyyy-MM-dd", arProvider) : ""))
                                                 .ForMember(tm => tm.OffersOpeningDateHijri, opt => opt.MapFrom(t => t.OffersOpeningDate.HasValue ? t.OffersOpeningDate.Value.ToString("yyyy-MM-dd", arProvider) : ""))
                                                 .ForMember(tm => tm.LastOfferPresentationDateHijri, opt => opt.MapFrom(t => t.LastOfferPresentationDate.HasValue ? t.LastOfferPresentationDate.Value.ToString("yyyy-MM-dd", arProvider) : ""))
                                                 .ForMember(tm => tm.EnquiriesCount, opt => opt.ResolveUsing<int>((src, dst, arg, context) =>
                                                                 GetEnquiriesCount(src, context)))
                                                 .ForMember(tm => tm.PendingEnquiryReplyCount, opt => opt.MapFrom(t => t.Enquiries.Select(e => e.EnquiryReplies.Count(r => r.IsActive == true && r.EnquiryReplyStatusId == (int)Enums.EnquiryReplyStatus.Pending)).Count()))
                                                 .ForMember(tm => tm.ApprovedEnquiryReplyCount, opt => opt.MapFrom(t => t.Enquiries.Select(e => e.EnquiryReplies.Count(r => r.IsActive == true && r.EnquiryReplyStatusId == (int)Enums.EnquiryReplyStatus.Approved)).Count()))
                                                 .ForMember(tm => tm.UserCommitteeType, opt => opt.ResolveUsing<int>((src, dst, arg, context) =>
                                                                 GetUserCommitteeType(src, context)))
                                                 .ForMember(tm => tm.AwardingValue, opt => opt.ResolveUsing<decimal?>((src, dst, arg, context) =>
                                                                 GetawardingValue(src, context)))
                                                 .ForMember(tm => tm.RemainingDays, opt => opt.MapFrom(t => t.LastOfferPresentationDate.HasValue ? (t.LastOfferPresentationDate.Value - DateTime.Now).Days : 0))
                                                 .ForMember(tm => tm.RemainingHours, opt => opt.MapFrom(t => t.LastOfferPresentationDate.HasValue ? (t.LastOfferPresentationDate.Value - DateTime.Now).Hours : 0))
                                                 .ForMember(tm => tm.RemainingMins, opt => opt.MapFrom(t => t.LastOfferPresentationDate.HasValue ? (t.LastOfferPresentationDate.Value - DateTime.Now).Minutes : 0));
        }



        private void TenderAttachmentsMap()
        {
            CreateMap<TenderAttachmentModel, TenderAttachment>();
            CreateMap<TenderAttachment, TenderAttachmentModel>();
            CreateMap<TenderAttachmentsModelChanges, TenderAttachmentChanges>();
            CreateMap<TenderAttachmentChanges, TenderAttachmentsModelChanges>();
            CreateMap<TenderOldAttachmentsModel, TenderAttachment>();
            CreateMap<TenderAttachment, TenderOldAttachmentsModel>();
            CreateMap<TenderAttachmentsModelChanges, TenderAttachmentChanges>();
            CreateMap<TenderAttachmentChanges, TenderAttachmentsModelChanges>();
        }

        private void TenderSearchCriteriaMap()
        {
            CreateMap<TenderSearchCriteriaModel, TenderSearchCriteria>(); //map from source (TenderSearchCriteriaModel) to destination (TenderSearchCriteria)
            CreateMap<TenderHistoryModel, TenderHistory>();
            CreateMap<TenderHistory, TenderHistoryModel>();
        }

        private void VacationDatesMap()
        {
            CreateMap<VacationsDateModel, VacationsDate>();
            CreateMap<VacationsDate, VacationsDateModel>();
        }
        private void TenderDatesMap()
        {
            CreateMap<TenderDatesModel, Tender>();
            CreateMap<Tender, TenderDatesModel>();
        }
        private void TenderQuantityTableChangesMaping()
        {

            CreateMap<TenderQuantityTableItem, TenderQuantityTableItemChanges>();
            CreateMap<TenderQuantityTableItemChanges, TenderQuantityTableItem>();
            CreateMap<TenderQuantitiyItemsChangeJson, TenderQuantitiyItemsJson>().ForMember(a => a.TenderQuantityTableItems, opt => opt.MapFrom(t => t.TenderQuantityTableItemChanges));
            CreateMap<TenderQuantityTable, TenderQuantityTableChanges>();
            CreateMap<TenderQuantityTableChanges, TenderQuantityTable>().ForMember(a => a.QuantitiyItemsJson, opt => opt.MapFrom(t => t.QuantitiyItemsChangeJson));
        }
        private void TenderAttachmentsChangesMaping()
        {
            CreateMap<TenderAttachmentModel, TenderAttachment>();
            CreateMap<TenderAttachment, TenderAttachmentModel>();
            CreateMap<TenderAttachmentChanges, TenderAttachment>();
            CreateMap<TenderAttachment, TenderAttachmentChanges>();
            CreateMap<TenderAttachmentChanges, TenderAttachmentModel>();
            CreateMap<TenderAttachmentModel, TenderAttachmentChanges>();
        }
        private void TenderQuantityTableMaping()
        {
            CreateMap<ConditionsBooklet, PurchaseModel>()
                .ForMember(tm => tm.TenderName, opt => opt.MapFrom(t => t.Tender.TenderName))
                .ForMember(tm => tm.CommericalRegisterNo, opt => opt.MapFrom(t => t.Supplier.SelectedCr))
                .ForMember(tm => tm.CommericalRegisterName, opt => opt.MapFrom(t => t.Supplier.SelectedCrName))
                .ForMember(tm => tm.ConditionsBookletPrice, opt => opt.MapFrom(t => t.Tender.ConditionsBookletPrice))
                .ForMember(tm => tm.PurshaseDateString, opt => opt.MapFrom(t => t.BillInfo.PurchaseDate.HasValue ? t.BillInfo.PurchaseDate.Value.ToString("dd/MM/yyyy hh:mm tt") : ""));
        }

        private void TenderRelationsMap()
        {
            CultureInfo arProvider = CultureInfo.CreateSpecificCulture("ar-SA");
            CultureInfo enProvider = CultureInfo.CreateSpecificCulture("en-USA");

            CreateMap<TenderChangeRequestModel, TenderChangeRequest>();
            CreateMap<TenderChangeRequest, TenderChangeRequestModel>()
                .ForMember(tm => tm.ChangeRequestStatusString, opt => opt.MapFrom(t => t.ChangeRequestStatus.NameAr))
                .ForMember(tm => tm.ChangeRequestTypeString, opt => opt.MapFrom(t => t.ChangeRequestType.NameAr));

            CreateMap<TenderOffersStepModel, Tender>();
            CreateMap<Tender, TenderOffersStepModel>()
                .ForMember(tm => tm.ConditionsBookletDeliveryAddress, opt => opt.MapFrom(t => t.ConditionsBookletDeliveryAddress.AddressName))
                .ForMember(tm => tm.OffersOpeningAddress, opt => opt.MapFrom(t => t.OffersOpeningAddress.AddressName))
                .ForMember(tm => tm.TenderChangeRequestIds, opt => opt.MapFrom(t => t.ChangeRequests.Where(x => x.IsActive == true).Select(x => x.TenderChangeRequestId).ToList()))
                .ForMember(tm => tm.RejectionReason, opt => opt.MapFrom(t => t.ChangeRequests.Where(x => x.IsActive == true).Select(x => x.RejectionReason).FirstOrDefault()))
                .ForMember(tm => tm.ChangeRequestStatusIds, opt => opt.MapFrom(t => t.ChangeRequests.Where(x => x.IsActive == true).Select(x => x.ChangeRequestStatusId).ToList()))
                .ForMember(tm => tm.ChangeRequestTypeId, opt => opt.MapFrom(t => t.ChangeRequests.Where(x => x.IsActive == true).Select(x => x.ChangeRequestTypeId).FirstOrDefault()))
                .ForMember(tm => tm.InvitationStatusId, opt => opt.MapFrom(t => t.Invitations.Where(x => x.StatusId == (int)Enums.InvitationStatus.Approved)))
                .ForMember(tm => tm.LastEnqueriesDateHijriString, opt => opt.MapFrom(t => t.LastEnqueriesDate.HasValue ? t.LastEnqueriesDate.Value.ToString("yyyy-MM-dd", arProvider) : ""))
                .ForMember(tm => tm.LastOfferPresentationDateHijriString, opt => opt.MapFrom(t => t.LastOfferPresentationDate.HasValue ? t.LastOfferPresentationDate.Value.ToString("yyyy-MM-dd", arProvider) : ""))
                .ForMember(tm => tm.OffersOpeningDateHijriString, opt => opt.MapFrom(t => t.OffersOpeningDate.HasValue ? t.OffersOpeningDate.Value.ToString("yyyy-MM-dd", arProvider) : ""))
                 .ForMember(tm => tm.LastEnqueriesDateString, opt => opt.MapFrom(t => t.LastEnqueriesDate.HasValue ? t.LastEnqueriesDate.Value.ToString("yyyy-MM-dd", enProvider) : ""))
                .ForMember(tm => tm.LastOfferPresentationDateString, opt => opt.MapFrom(t => t.LastOfferPresentationDate.HasValue ? t.LastOfferPresentationDate.Value.ToString("yyyy-MM-dd", enProvider) : ""))
                .ForMember(tm => tm.OffersOpeningDateString, opt => opt.MapFrom(t => t.OffersOpeningDate.HasValue ? t.OffersOpeningDate.Value.ToString("yyyy-MM-dd", enProvider) : ""));

            CreateMap<TenderRelationsModel, Tender>();
            CreateMap<Tender, TenderRelationsModel>()
                .ForMember(tm => tm.TenderAreaIDs, opt => opt.MapFrom(t => t.TenderAreas.Select(a => a.AreaId).ToList()))
                .ForMember(tm => tm.TenderCountriesIDs, opt => opt.MapFrom(t => t.TenderCountries.Select(a => a.CountryId).ToList()))
                .ForMember(tm => tm.TenderActivitieIDs, opt => opt.MapFrom(t => t.TenderActivities.Select(a => a.ActivityId).ToList()))
                .ForMember(tm => tm.TenderConstructionWorkIDs, opt => opt.MapFrom(t => t.TenderConstructionWorks.Select(a => a.ConstructionWorkId).ToList()))
                .ForMember(tm => tm.TenderMentainanceRunnigWorkIDs, opt => opt.MapFrom(t => t.TenderMaintenanceRunnigWorks.Select(a => a.MaintenanceRunningWorkId).ToList()))
               .ForPath(a => a.Areas, b => b.MapFrom(c => c.TenderAreas.Select(x => new LookupModel { Id = x.Area.AreaId, Name = x.Area.NameAr }).ToList()))
                                   .ForPath(a => a.Activities, b => b.MapFrom(c => c.TenderActivities.Select(x => x.Activity)))
                                   .ForPath(a => a.ConstructionWorks, b => b.MapFrom(c => c.TenderConstructionWorks.Select(x => x.ConstructionWork)))
                                   .ForPath(a => a.MaintenanceWorks, b => b.MapFrom(c => c.TenderMaintenanceRunnigWorks.Select(x => x.MaintenanceRunningWork)))
                                   .ForPath(a => a.Countries, b => b.MapFrom(c => c.TenderCountries.Select(x => x.Country)));


        }

        #endregion

        #region Methods
        private decimal? GetawardingValue(Tender src, object context)
        {
            var awardingValue = src.Offers.Select(t => t.TotalOfferAwardingValue).Sum() == 0 ? src.Offers.Select(p => p.PartialOfferAwardingValue).Sum() : src.Offers.Select(t => t.TotalOfferAwardingValue).Sum();
            return awardingValue;
        }
        private int GetEnquiriesCount(Tender src, ResolutionContext context)
        {
            if (!context.Options.Items.Keys.Contains("isTechnicalCommitteeUser"))
                return 0;
            // show Enquiries Count according to tehnical member is joined to tender or not
            int EnquiriesCount = 0;
            if (!context.Options.Items.Keys.Contains("userIdFlag"))
                return 0;
            var userId = int.Parse(context.Options.Items["userIdFlag"].ToString());

            if (bool.Parse(context.Options.Items["statusFlag"].ToString()) == true) //technical member
            {
                var TechnicalCommitteIdsForTender = src.TechnicalOrganization.CommitteeUsers.Any(x => x.UserProfileId == userId);

                if (TechnicalCommitteIdsForTender)
                {
                    EnquiriesCount = src.Enquiries.Count(e => e.TenderId == src.TenderId && e.IsActive == true);
                }
                else
                {
                    EnquiriesCount = src.Enquiries.Where(e => e.JoinTechnicalCommittees.Any(t => t.Committee.CommitteeUsers.Any(x => x.UserProfileId == userId) && t.IsActive == true)).Count();
                }
            }
            else //Auditor
            {
                EnquiriesCount = src.Enquiries.Count(e => e.TenderId == src.TenderId && e.IsActive == true);
            }

            return EnquiriesCount;
        }
        private int GetUserCommitteeType(Tender src, ResolutionContext context)
        {
            if (!context.Options.Items.Keys.Contains("userIdFlag"))
                return (int)Enums.UserCommitteeType.None;

            var userId = int.Parse(context.Options.Items["userIdFlag"].ToString());
            if (src.OffersOpeningCommittee != null && src.OffersOpeningCommittee.CommitteeUsers.Any(c => c.UserProfileId == userId && c.UserRoleId == (int)Enums.UserRole.NewMonafasat_OffersOpeningSecretary))
                return (int)Enums.UserCommitteeType.NewMonafasat_OffersOpeningSecretary;
            if (src.OffersOpeningCommittee != null && src.OffersOpeningCommittee.CommitteeUsers.Any(c => c.UserProfileId == userId && c.UserRoleId == (int)Enums.UserRole.NewMonafasat_OffersOpeningManager))
                return (int)Enums.UserCommitteeType.NewMonafasat_OffersOpeningManager;
            if (src.OffersCheckingCommittee != null && src.OffersCheckingCommittee.CommitteeUsers.Any(c => c.UserProfileId == userId && c.UserRoleId == (int)Enums.UserRole.NewMonafasat_OffersCheckSecretary))
                return (int)Enums.UserCommitteeType.NewMonafasat_OffersCheckSecretary;
            if (src.OffersCheckingCommittee != null && src.OffersCheckingCommittee.CommitteeUsers.Any(c => c.UserProfileId == userId && c.UserRoleId == (int)Enums.UserRole.NewMonafasat_OffersCheckManager))
                return (int)Enums.UserCommitteeType.NewMonafasat_OffersCheckManager;
            if (src.TechnicalOrganization != null && src.TechnicalOrganization.CommitteeUsers.Any(c => c.UserProfileId == userId && c.UserRoleId == (int)Enums.UserRole.NewMonafasat_TechnicalCommitteeUser))

                return (int)Enums.UserCommitteeType.NewMonafasat_TechnicalCommitteeUser;

            return (int)Enums.UserCommitteeType.None;
        }

        private int GetQuantitiesChangeStatus(Tender src, ResolutionContext context)
        {
            var QuantityStatus = src.ChangeRequests.Where(x => x.IsActive == true && x.ChangeRequestTypeId == (int)Enums.ChangeRequestType.QuantitiesTables).Select(s => s.ChangeRequestStatusId).LastOrDefault();
            return QuantityStatus;
        }
        private int GetDatesChangeStatus(Tender src, ResolutionContext context)
        {
            var QuantityStatus = src.ChangeRequests.Where(x => x.IsActive == true && x.ChangeRequestTypeId == (int)Enums.ChangeRequestType.ExtendDates).Select(s => s.ChangeRequestStatusId).LastOrDefault();
            return QuantityStatus;
        }
        private int GetAttachmentsChangeStatus(Tender src, ResolutionContext context)
        {
            var QuantityStatus = src.ChangeRequests.Where(x => x.IsActive == true && x.ChangeRequestTypeId == (int)Enums.ChangeRequestType.Attachments).Select(s => s.ChangeRequestStatusId).LastOrDefault();
            return QuantityStatus;
        }
        private int GetCancelChangeStatus(Tender src, ResolutionContext context)
        {
            var QuantityStatus = src.ChangeRequests.Where(x => x.IsActive == true && x.ChangeRequestTypeId == (int)Enums.ChangeRequestType.Canceling).Select(s => s.ChangeRequestStatusId).LastOrDefault();
            return QuantityStatus;
        }
        private string GetCancelRole(Tender src, ResolutionContext context)
        {
            var role = src.ChangeRequests.Where(x => x.IsActive == true && x.ChangeRequestTypeId == (int)Enums.ChangeRequestType.Canceling).Select(s => s.RequestedByRoleName).LastOrDefault();
            return role;
        }

        private void TenderDashboardMap()
        {
            CreateMap<Tender, RejectTenderViewModel>()
                .ForMember(tm => tm.TenderStatusName, opt => opt.MapFrom(t => t.Status.NameAr))
                .ForMember(tm => tm.TenderStatusIdString, opt => opt.MapFrom(t => Util.Encrypt(t.TenderStatusId)))
                .ForMember(tm => tm.TenderStatusId, opt => opt.MapFrom(t => t.TenderStatusId))
                .ForMember(tm => tm.TenderIdString, opt => opt.MapFrom(t => Util.Encrypt(t.TenderId)))
                //.ForMember(tm => tm.RejectionReason, opt => opt.MapFrom(t => t.TenderHistories.Where(h => h.StatusId == (int)Enums.TenderStatus.Rejected && h.IsActive != false ).FirstOrDefault().RejectionReason));

                .ForMember(tm => tm.RejectionReason, opt => opt.ResolveUsing<string>((src, dst, arg, context) => GetRejectionReason(src, context)));


            CreateMap<Tender, ProcessNeedsActionViewModel>()
                .ForMember(tm => tm.AcceptanceTypeName, opt => opt.MapFrom(t => t.TenderType.NameAr))
                .ForMember(tm => tm.TenderStatusIdString, opt => opt.MapFrom(t => Util.Encrypt(t.TenderStatusId)))
                .ForMember(tm => tm.TenderReferenceNumber, opt => opt.MapFrom(t => t.ReferenceNumber))
                .ForMember(tm => tm.ChangeRequestTypeId, opt => opt.MapFrom(t => t.ChangeRequests.Count > 0 ? t.ChangeRequests.OrderBy(r => r.TenderChangeRequestId).Last().ChangeRequestTypeId : 0))
                .ForMember(tm => tm.TenderIdString, opt => opt.MapFrom(t => Util.Encrypt(t.TenderId)));

            CreateMap<Tender, SalesListViewModel>().ForMember(tm => tm.AgencyName, opt => opt.MapFrom(t => t.Agency.NameArabic))

                .ForMember(tm => tm.Count, opt => opt.MapFrom(t => t.ConditionsBooklets.Where(c => c.IsActive == true /*&& c.ConfirmPurchase == true*/).Count()))
                .ForMember(tm => tm.Price, opt => opt.MapFrom(t => t.ConditionsBooklets.Where(c => c.IsActive == true).Select(c => c.BillInfo.AmountDue).Sum()));

        }

        /// <summary>
        /// To get rejected reason from tender history in case tender rejected or change request in case change request rejected 
        /// </summary>
        /// <param name="src"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        private string GetRejectionReason(Tender src, object context)
        {
            var rejectionReason = string.Empty;
            if (src.TenderHistories.Where(r => r.StatusId == (int)Enums.TenderStatus.Rejected).Count() > 0)
                rejectionReason = src.TenderHistories.Where(r => r.StatusId == (int)Enums.TenderStatus.Rejected).Last().RejectionReason;
            else if (src.TenderHistories.Where(r => r.StatusId == (int)Enums.TenderStatus.OffersOppenedRejected).Count() > 0)
                rejectionReason = src.TenderHistories.Where(r => r.StatusId == (int)Enums.TenderStatus.OffersOppenedRejected).Last().RejectionReason;
            else if (src.TenderHistories.Where(r => r.StatusId == (int)Enums.TenderStatus.OffersCheckedRejected).Count() > 0)
                rejectionReason = src.TenderHistories.Where(r => r.StatusId == (int)Enums.TenderStatus.OffersCheckedRejected).Last().RejectionReason;
            else if (src.TenderHistories.Where(r => r.StatusId == (int)Enums.TenderStatus.OffersAwardedRejected).Count() > 0)
                rejectionReason = src.TenderHistories.Where(r => r.StatusId == (int)Enums.TenderStatus.OffersAwardedRejected).Last().RejectionReason;
            else rejectionReason = src.ChangeRequests.Where(h => h.ChangeRequestStatusId == (int)Enums.ChangeStatusType.Rejected && h.IsActive == true).Last().RejectionReason;
            return rejectionReason;
        }

        private void SuppliersEnquirtMap()
        {
            CreateMap<Enquiry, SupplierEnquiryModel>()
                .ForMember(tm => tm.SupplierName, opt => opt.MapFrom(t => t.Supplier.SelectedCrName))
                .ForMember(tm => tm.TenderName, opt => opt.MapFrom(t => t.Tender.TenderName))
                .ForMember(tm => tm.CommericalRegisterNo, opt => opt.MapFrom(t => t.Supplier.SelectedCr))
                .ForMember(tm => tm.EnquiryName, opt => opt.MapFrom(t => t.EnquiryName));
        }

        private void TenderRevisionDateMap()
        {
            CreateMap<TenderRevisionDateModel, TenderDatesChange>();
            CreateMap<TenderDatesChange, TenderRevisionDateModel>()
              .ForMember(tm => tm.StatusName, opt => opt.MapFrom(t => t.ChangeRequest.ChangeRequestStatus.NameAr))
              .ForMember(tm => tm.TenderIdString, opt => opt.MapFrom(t => Util.Encrypt(t.ChangeRequest.Tender.TenderId)))
              .ForMember(tm => tm.TenderName, opt => opt.MapFrom(t => t.ChangeRequest.Tender.TenderName))
              .ForMember(tm => tm.TenderNumber, opt => opt.MapFrom(t => t.ChangeRequest.Tender.TenderNumber));

            CreateMap<TenderChangeRequest, TenderRevisionCancelModel>()
              .ForMember(tm => tm.StatusName, opt => opt.MapFrom(t => t.ChangeRequestStatus.NameAr))
              .ForMember(tm => tm.TenderIdString, opt => opt.MapFrom(t => Util.Encrypt(t.Tender.TenderId)))
              .ForMember(tm => tm.CreatedBy, opt => opt.MapFrom(t => t.CreatedBy))
              .ForMember(tm => tm.TenderName, opt => opt.MapFrom(t => t.Tender.TenderName))
              .ForMember(tm => tm.TenderNumber, opt => opt.MapFrom(t => t.Tender.TenderNumber));

            //قائمة التمديدات
            CreateMap<TenderChangeRequest, TenderExtendDateModel>()
             .ForMember(trd => trd.LastEnqueriesDate, opt => opt.MapFrom(t => t.Tender.LastEnqueriesDate))
             .ForMember(trd => trd.LastOfferPresentationDate, opt => opt.MapFrom(t => t.Tender.LastOfferPresentationDate))
             .ForMember(trd => trd.OffersOpeningDate, opt => opt.MapFrom(t => t.Tender.OffersOpeningDate))
             .ForMember(trd => trd.TenderIdString, opt => opt.MapFrom(t => Util.Encrypt(t.Tender.TenderId)))
             .ForMember(trd => trd.CreatedBy, opt => opt.MapFrom(t => t.CreatedBy))
             .ForMember(trd => trd.ChangeRequestStatusName, opt => opt.MapFrom(t => t.ChangeRequestStatus.NameAr))
             .ForMember(trd => trd.TenderName, opt => opt.MapFrom(t => t.Tender.TenderName))
             .ForMember(trd => trd.TenderStatusIdString, opt => opt.MapFrom(t => Util.Encrypt(t.Tender.TenderStatusId)))
             .ForMember(trd => trd.TenderNumber, opt => opt.MapFrom(t => t.Tender.TenderNumber));
        }
        private int GetInvitationStatus(Tender src, ResolutionContext context)
        {
            if (!context.Options.Items.ContainsKey("cr"))
                return 0;
            var invitation = src.Invitations.Where(f => context.Options.Items["cr"].ToString() != "" ? (f.CommericalRegisterNo == context.Options.Items["cr"].ToString() && f.IsActive == true) : f.IsActive == true).FirstOrDefault();

            return invitation == null ? 0 : invitation.StatusId;
        }
        private bool CheckIsSypplierPurchaseTheTender(Tender src, ResolutionContext context)
        {
            var purchased = src.ConditionsBooklets.Any(f => context.Options.Items["cr"].ToString() != "" ? (f.CommericalRegisterNo == context.Options.Items["cr"].ToString() && f.IsActive == true) : false);

            return purchased;
        }
        #endregion

    }
}
