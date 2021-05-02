using AutoMapper;
using MOF.Etimad.Monafasat.Core;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace MOF.Etimad.Monafasat.WebApi
{
    public class PreQualificationProfile : Profile
    {
        public PreQualificationProfile()
        {
            TenderPreQualificationDetailsModelMapping();
            TenderPreQualificationSavingModelMapping();
            PreQualificationCardModelMapping();
            SupplierPreQualificationRequestMap();
            SupplierDocumentAttachmentModel();
            PreQualificationApproval();
        }

        private void SupplierDocumentAttachmentModel()
        {
            CreateMap<PreQualificationSavingModel, Tender>();
        }

        private void TenderPreQualificationDetailsModelMapping()
        {
            CultureInfo arProvider = CultureInfo.CreateSpecificCulture("ar-SA");

            CultureInfo enProvider = CultureInfo.CreateSpecificCulture("en-USA");
            CreateMap<SupplierPreQualificationAttachment, PreQualificationSupplierAttachmentModel>().ReverseMap();



            #region [PreQualificationDetailsModel==Tender
            CreateMap<Tender, PreQualificationDetailsModel>()

.ForMember(m => m.QualificationNumber, opt => opt.MapFrom(src => src.ReferenceNumber))

            // .ForMember(m => m.QualificationNumber, opt => opt.MapFrom(src => src.TenderNumber))
            .ForMember(m => m.TenderStatusIdString, opt => opt.MapFrom(src => (Util.Encrypt(src.TenderStatusId))))
            .ForMember(m => m.TenderIdString, opt => opt.MapFrom(src => (Util.Encrypt(src.TenderId))))
            #region Dates

            .ForMember(tm => tm.CreatedDateHijri, opt => opt.MapFrom(t => t.CreatedAt.ToString("dd/MM/yyyy", arProvider)))
            .ForMember(tm => tm.SubmitionDateHijri, opt => opt.MapFrom(t => t.SubmitionDate.HasValue ? t.SubmitionDate.Value.ToString("dd/MM/yyyy", arProvider) : ""))
            .ForMember(tm => tm.LastEnqueriesDateHijri, opt => opt.MapFrom(t => t.LastEnqueriesDate.HasValue ? t.LastEnqueriesDate.Value.ToString("dd/MM/yyyy", arProvider) : ""))
            .ForMember(tm => tm.LastOfferPresentationDateString, opt => opt.MapFrom(t => t.LastOfferPresentationDate.HasValue ? t.LastOfferPresentationDate.Value.ToString("dd/MM/yyyy", enProvider) : ""))
            .ForMember(tm => tm.LastOfferPresentationDateHijri, opt => opt.MapFrom(t => t.LastOfferPresentationDate.HasValue ? t.LastOfferPresentationDate.Value.ToString("dd/MM/yyyy", arProvider) : ""))
            .ForMember(tm => tm.LastOfferPresentationDateHijriString, opt => opt.MapFrom(t => t.LastOfferPresentationDate.HasValue ? t.LastOfferPresentationDate.Value.ToString("dd/MM/yyyy", arProvider) : ""))

            //.ForMember(tm => tm.RemainingDays, opt => opt.MapFrom(t => (t.LastOfferPresentationDate.Value - DateTime.Now).Days))
            //.ForMember(tm => tm.RemainingHours, opt => opt.MapFrom(t => (t.LastOfferPresentationDate.Value - DateTime.Now).Hours))
            //.ForMember(tm => tm.RemainingMins, opt => opt.MapFrom(t => (t.LastOfferPresentationDate.Value - DateTime.Now).Minutes))

            .ForMember(tm => tm.LastEnqueriesDateString, opt => opt.MapFrom(t => t.LastEnqueriesDate.HasValue ? t.LastEnqueriesDate.Value.ToString("dd/MM/yyyy", enProvider) : ""))
            .ForMember(tm => tm.LastEnqueriesDateHijriString, opt => opt.MapFrom(t => t.LastEnqueriesDate.HasValue ? t.LastEnqueriesDate.Value.ToString("dd/MM/yyyy", arProvider) : ""))


            .ForMember(tm => tm.OffersCheckingDateString, opt => opt.MapFrom(t => t.OffersCheckingDate.HasValue ? t.OffersCheckingDate.Value.ToString("dd/MM/yyyy", enProvider) : ""))
            .ForMember(tm => tm.OffersCheckingDateHijriString, opt => opt.MapFrom(t => t.OffersCheckingDate.HasValue ? t.OffersCheckingDate.Value.ToString("dd/MM/yyyy", arProvider) : ""))



            #endregion
            #region  Basic Info 
             .ForMember(tm => tm.AgencyName, opt => opt.MapFrom(t => t.Agency.NameArabic))
            #endregion

            #region  Address Mapping
                .ForPath(a => a.Areas, b => b.MapFrom(c => c.TenderAreas.Select(x => x.Area)))
                .ForPath(a => a.Countries, b => b.MapFrom(c => c.TenderCountries.Select(x => x.Country)))
            #endregion
            #region  Attachments                
                .ForPath(a => a.Attachments, b => b.MapFrom(c => c.Attachments))
            #endregion
            #endregion
         ;
        }

        private void TenderPreQualificationSavingModelMapping()
        {
            CultureInfo arProvider = CultureInfo.CreateSpecificCulture("ar-SA");
            CultureInfo enProvider = CultureInfo.CreateSpecificCulture("en-USA");
            CreateMap<Tender, PreQualificationSavingModel>()
                .ForMember(tm => tm.TenderAreaIDs, opt => opt.MapFrom(t => t.TenderAreas.Select(a => a.AreaId).ToList()))
                .ForMember(tm => tm.TenderCountriesIDs, opt => opt.MapFrom(t => t.TenderCountries.Select(a => a.CountryId).ToList()))
                .ForMember(tm => tm.TenderActivitieIDs, opt => opt.MapFrom(t => t.TenderActivities.Select(a => a.ActivityId).ToList()))
                .ForMember(tm => tm.TenderConstructionWorkIDs, opt => opt.MapFrom(t => t.TenderConstructionWorks.Select(a => a.ConstructionWorkId).ToList()))
                .ForMember(tm => tm.TenderMentainanceRunnigWorkIDs, opt => opt.MapFrom(t => t.TenderMaintenanceRunnigWorks.Select(a => a.MaintenanceRunningWorkId).ToList()))
                .ForMember(tm => tm.LastEnqueriesDate, opt => opt.MapFrom(t => t.LastEnqueriesDate))
                .ForMember(tm => tm.TenderIdString, opt => opt.MapFrom(t => (Util.Encrypt(t.TenderId))))
                .ForMember(tm => tm.LastOfferPresentationDate, opt => opt.MapFrom(t => t.LastOfferPresentationDate))
                .ForMember(tm => tm.OffersCheckingDate, opt => opt.MapFrom(t => t.OffersCheckingDate));


        }

        private void PreQualification_PreQualificationApplyDocuments()
        {

            CreateMap<Tender, PreQualificationApplyDocumentsModel>()

                       .ForMember(tm => tm.PreQualificationIdString, opt => opt.MapFrom(t => Util.Encrypt(t.TenderId)));
            ;
        }
        private void PreQualificationApproval()
        {

            CreateMap<Tender, PreQulaificationApprovalModel>()
                .ForMember(x => x.PreQualificationId, opt => opt.MapFrom(t => t.TenderId))
 .ForMember(x => x.PreQualificationTypeId, opt => opt.MapFrom(t => t.QualificationTypeId))
                .ForMember(x => x.PreQualificationIdString, opt => opt.MapFrom(t => Util.Encrypt(t.TenderId)))
                .ForMember(x => x.PreQualificationStatusId, opt => opt.MapFrom(t => t.TenderStatusId))
                .ForMember(x => x.RejectionReason, opt => opt.MapFrom(t => t.TenderHistories.OrderByDescending(th => th.TenderHistoryId).FirstOrDefault().RejectionReason));

        }


        private void PreQualificationCardModelMapping()
        {
            CultureInfo arProvider = CultureInfo.CreateSpecificCulture("ar-SA");

            CreateMap<Tender, PreQualificationCardModel>()
              .ForMember(m => m.QualificationNumber, opt => opt.MapFrom(src => src.ReferenceNumber))
              .ForMember(m => m.QualificationName, opt => opt.MapFrom(src => src.TenderName))
              .ForMember(m => m.TenderStatusIdString, opt => opt.MapFrom(src => (Util.Encrypt(src.TenderStatusId))))
              .ForMember(m => m.TenderIdString, opt => opt.MapFrom(src => (Util.Encrypt(src.TenderId))))
              .ForMember(tm => tm.QualificationStatusName, opt => opt.MapFrom(t => t.Status.NameAr))

              .ForMember(tm => tm.QualificationTypeName, opt => opt.MapFrom(t => t.TenderType.NameAr))
              .ForMember(tm => tm.SubmitionDate, opt => opt.MapFrom(t => t.SubmitionDate))

              .ForMember(tm => tm.LastEnqueriesDateHijri, opt => opt.MapFrom(t => t.LastEnqueriesDate.HasValue ? t.LastEnqueriesDate.Value.ToString("yyyy-MM-dd", arProvider) : ""))
              .ForMember(tm => tm.LastOfferPresentationDateHijri, opt => opt.MapFrom(t => t.LastOfferPresentationDate.HasValue ? t.LastOfferPresentationDate.Value.ToString("yyyy-MM-dd", arProvider) : ""))
              .ForMember(tm => tm.OffersCheckingDateHijri, opt => opt.MapFrom(t => t.OffersCheckingDate.HasValue ? t.OffersCheckingDate.Value.ToString("yyyy-MM-dd", arProvider) : ""))
              .ForMember(tm => tm.SubmitionDateHijri, opt => opt.MapFrom(t => t.SubmitionDate.HasValue ? t.SubmitionDate.Value.ToString("yyyy-MM-dd", arProvider) : ""))
              .ForMember(tm => tm.TenderChangeRequestIds, opt => opt.MapFrom(t => t.ChangeRequests.Where(c => c.IsActive == true).Select(x => x.TenderChangeRequestId).ToList()))
              .ForMember(tm => tm.ChangeRequestStatusIds, opt => opt.MapFrom(t => t.ChangeRequests.Where(c => c.IsActive == true).Select(x => x.ChangeRequestStatusId).ToList()))
              .ForMember(tm => tm.UserCommitteeType, opt => opt.ResolveUsing<int>((src, dst, arg, context) => GetUserCommitteeType(src, context)))

            #region  Basic Info 
            .ForMember(tm => tm.AgencyName, opt => opt.MapFrom(t => t.Agency.NameArabic))
            #endregion

            //#region  Address Mapping
            //    .ForPath(a => a.Areas, b => b.MapFrom(c => c.TenderAreas.Select(x => x.Area)))
            //    .ForPath(a => a.Countries, b => b.MapFrom(c => c.TenderCountries.Select(x => x.Country)))
            //#endregion
         ;
        }
        private void SupplierPreQualificationRequestMap()
        {
            CreateMap<SupplierPreQualificationDocument, SupplierPreQulaificationRequestsModel>()
                       .ForMember(tm => tm.ComercialName, opt => opt.MapFrom(t => t.Supplier.SelectedCrName))
                       .ForMember(tm => tm.ComercialNumber, opt => opt.MapFrom(t => t.Supplier.SelectedCr))

                       .ForMember(tm => tm.PreQualificationResult, opt => opt.MapFrom(t => t.PreQualificationResult))
                      .ForMember(tm => tm.SupplierPreQualificationDocumentIdString, opt => opt.MapFrom(t => Util.Encrypt(t.SupplierPreQualificationDocumentId)))
                       .ForMember(tm => tm.RejectionReason, opt => opt.MapFrom(t => t.RejectionReason));


            CreateMap<SupplierPreQualificationDocument, SupplierPreQualificationDocumentModel>()
                   .ForMember(tm => tm.PreQualificationResult, opt => opt.MapFrom(t => t.PreQualificationResult))
                   .ForMember(tm => tm.RejectionReason, opt => opt.MapFrom(t => t.RejectionReason))

                   .ForMember(tm => tm.TenderStatusId, opt => opt.MapFrom(t => t.PreQualification.TenderStatusId))
                   .ForMember(tm => tm.SupplierPreQualificationDocumentIdString, opt => opt.MapFrom(t => Util.Encrypt(t.SupplierPreQualificationDocumentId)))
                   .ForMember(tm => tm.PreQualificationIdString, opt => opt.MapFrom(t => Util.Encrypt(t.PreQualificationId)));

            CreateMap<Tender, PreQualificationBasicDetailsModel>()
             .ForMember(tm => tm.QualificationName, opt => opt.MapFrom(t => t.TenderName))
                .ForMember(tm => tm.TenderIdString, opt => opt.MapFrom(t => Util.Encrypt(t.TenderId)))
                .ForMember(tm => tm.TenderStatusId, opt => opt.MapFrom(t => t.TenderStatusId))
                .ForMember(tm => tm.InsideKSA, opt => opt.MapFrom(t => t.InsideKSA))
              .ForMember(tm => tm.RejectionReason, opt => opt.MapFrom(t => t.TenderHistories.Where(x => x.IsActive == true && x.StatusId == (int)Enums.TenderStatus.OffersCheckedRejected).OrderByDescending(th => th.TenderHistoryId).FirstOrDefault().RejectionReason))
              .ForMember(tm => tm.RemainingDays, opt => opt.MapFrom(t => (t.LastOfferPresentationDate.Value - DateTime.Now).Days))
              .ForMember(tm => tm.RemainingHours, opt => opt.MapFrom(t => (t.LastOfferPresentationDate.Value - DateTime.Now).Hours))
              .ForMember(tm => tm.RemainingMins, opt => opt.MapFrom(t => (t.LastOfferPresentationDate.Value - DateTime.Now).Minutes))
               .ForMember(tm => tm.TenderActivities, opt => opt.MapFrom(t => t.TenderActivities.Select(a => a.Activity.NameAr).ToList()))
                .ForMember(tm => tm.TenderConstructionWorks, opt => opt.MapFrom(t => t.TenderConstructionWorks.Select(a => a.ConstructionWork.NameAr).ToList()))
                .ForMember(tm => tm.TenderMaintenanceRunnigWorks, opt => opt.MapFrom(t => t.TenderMaintenanceRunnigWorks.Select(a => a.MaintenanceRunningWork.NameAr).ToList()));


            CreateMap<SupplierPreQualificationDocument, PreQualificationApplyDocumentsModel>()
             .ForMember(tm => tm.PreQualificationId, opt => opt.MapFrom(t => t.PreQualificationId))
               .ForMember(tm => tm.PreQualificationResult, opt => opt.MapFrom(t => t.PreQualificationResult))
                //.ForMember(tm => tm.PreQualificationResult, opt => opt.MapFrom(t => t.StatusId))
                .ForMember(tm => tm.RejectionReason, opt => opt.MapFrom(t => t.RejectionReason))
                .ForMember(tm => tm.SupplierPreQualificationDocumentIdString, opt => opt.MapFrom(t => Util.Encrypt(t.SupplierPreQualificationDocumentId)))

                 .ForMember(tm => tm.PreQualificationIdString, opt => opt.MapFrom(t => Util.Encrypt(t.PreQualificationId)))
                    .ForMember(m => m.preQualificationSupplierAttachmentModels, opt => opt.MapFrom(src => src.supplierPreQualificationAttachments));



            ;

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
    }
}