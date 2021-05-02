using AutoMapper;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Integration;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.ViewModel;
using System;
using System.Globalization;
using System.Linq;

namespace MOF.Etimad.Monafasat.WebApi.MappingProfiles
{
    public class InvitationProfile : Profile
    {
        public InvitationProfile()
        {
            InvitationMap();
            InvitationDetailsMap();
            TenderInvitationModelMap();
            CreateMap<ContactOfficerModel, SupplierInvitationModel>()
                .ForMember(t => t.Email, opt => opt.MapFrom(d => d.email))
                .ForMember(t => t.mobile, opt => opt.MapFrom(d => d.mobile))
                .ForMember(t => t.CommericalRegisterNo, opt => opt.MapFrom(d => d.supplierNumber))
                .IgnoreAllSourcePropertiesWithAnInaccessibleSetter()
            .IgnoreAllPropertiesWithAnInaccessibleSetter();
        }

        #region Mapping

        private void InvitationMap()
        {
            HijriCalendar hijriCal = new HijriCalendar();
            CultureInfo arProvider = CultureInfo.CreateSpecificCulture("ar-SA");

            CreateMap<Invitation, SupplierInvitationModel>()
                                    .ForMember(tm => tm.InvitationIdString, opt => opt.MapFrom(t => (Util.Encrypt(t.InvitationId))))
                                    .ForMember(tm => tm.TenderName, opt => opt.MapFrom(t => t.Tender.TenderName))
                                    .ForMember(tm => tm.CreatedAtHihri, opt => opt.MapFrom(t => t.Tender.CreatedAt.ToString("yyyy-MM-dd", arProvider)))
                                    .ForMember(tm => tm.TenderNumber, opt => opt.MapFrom(t => t.Tender.TenderNumber))
                                    .ForMember(tm => tm.CommericalRegisterName, opt => opt.MapFrom(t => t.Supplier.SelectedCrName))
                                    .ForMember(tm => tm.InvitationTypeId, opt => opt.MapFrom(t => t.InvitationTypeId))
                                    .ForMember(tm => tm.InvitationStatusId, opt => opt.MapFrom(t => t.InvitationStatus.InvitationStatusId));

            CreateMap<SupplierIntegrationModel, SupplierInvitationModel>().ForMember(dest => dest.CommericalRegisterName, opt => opt.MapFrom(src => src.supplierName))
                .ForMember(dest => dest.CommericalRegisterNo, opt => opt.MapFrom(src => src.supplierNumber))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.SupplierName, opt => opt.MapFrom(src => src.supplierName))
                .ForMember(dest => dest.SupplierMobileNo, opt => opt.MapFrom(src => src.contactOfficers.Select(c => c.mobile)))
                .ForMember(dest => dest.SupplierContactOfficersEmails, opt => opt.MapFrom(src => src.contactOfficers.Select(c => c.email)));

            CreateMap<SupplierInvitationModel, SupplierIntegrationModel>().ForMember(dest => dest.supplierName, opt => opt.MapFrom(src => src.CommericalRegisterName))
                .ForMember(dest => dest.supplierNumber, opt => opt.MapFrom(src => src.CommericalRegisterNo))
                .ForMember(dest => dest.supplierName, opt => opt.MapFrom(src => src.SupplierName));

        }

        private void InvitationDetailsMap()
        {
            CreateMap<SupplierInvitationModel, Invitation>();

            CreateMap<Tender, TenderInvitationDetailsModel>()
                            .ForMember(tm => tm.TenderIdString, opt => opt.MapFrom(t => (Util.Encrypt(t.TenderId))))
                            .ForMember(tm => tm.InvitationId, opt => opt.ResolveUsing<int>((src, dst, arg, context) =>
                                            GetInvitationId(src, context)))
                            .ForMember(tm => tm.InvitationStatusId, opt => opt.ResolveUsing<int>((src, dst, arg, context) =>
                                             GetInvitationStatus(src, context)))
                            .ForMember(tm => tm.InvitationRequistTypeId, opt => opt.ResolveUsing<int>((src, dst, arg, context) =>
                                             GetInvitationType(src, context)))
                            .ForMember(tm => tm.InvitationStatusName, opt => opt.MapFrom(t => t.Invitations.Where(i => i.IsActive == true).FirstOrDefault().InvitationStatus.NameAr))
                            .ForMember(tm => tm.SubmitionDate, opt => opt.MapFrom(t => t.SubmitionDate))
                             .ForMember(tm => tm.AgencyName, opt => opt.MapFrom(t => t.Agency.NameArabic))
                            .ForMember(tm => tm.TenderName, opt => opt.MapFrom(t => t.TenderName))
                            .ForMember(tm => tm.TenderNumber, opt => opt.MapFrom(t => t.TenderNumber))
                               .ForMember(tm => tm.ReferenceNumber, opt => opt.MapFrom(t => t.ReferenceNumber))
                            .ForMember(tm => tm.OffersOpeningDate, opt => opt.MapFrom(t => t.OffersOpeningDate))
                            .ForMember(tm => tm.LastEnqueriesDate, opt => opt.MapFrom(t => t.LastEnqueriesDate))
                            .ForMember(tm => tm.LastOfferPresentationDate, opt => opt.MapFrom(t => t.LastOfferPresentationDate))
                            .ForMember(tm => tm.TechnicalOrganizationId, opt => opt.MapFrom(t => t.TechnicalOrganizationId))
                            .ForMember(tm => tm.InsideKSA, opt => opt.MapFrom(t => t.InsideKSA))
                            .ForMember(tm => tm.CondetionalBookletPrice, opt => opt.MapFrom(t => t.ConditionsBookletPrice))
                            .ForMember(tm => tm.TenderStatusId, opt => opt.MapFrom(t => t.TenderStatusId))
                            .ForMember(tm => tm.TenderStatusIdString, opt => opt.MapFrom(t => (Util.Encrypt(t.TenderStatusId))))
                            .ForMember(tm => tm.IsPurchased, opt => opt.MapFrom(t => t.ConditionsBooklets.Count() > 0 ? true : false))
                            .ForMember(tm => tm.TenderConstructionWorks, opt => opt.MapFrom(t => t.TenderConstructionWorks.Select(a => a.ConstructionWork.NameAr).ToList()))
                            .ForMember(tm => tm.IsFavouriteTender, opt => opt.ResolveUsing<bool>((src, dst, arg, context) =>
                                                                src.FavouriteSupplierTenders.Any(
                                                                f => f.IsActive == true && string.Equals(f.SupplierCRNumber, context.Options.Items["cr"].ToString()))))
                .ForMember(tm => tm.TenderMaintenanceRunnigWorks, opt => opt.MapFrom(t => t.TenderMaintenanceRunnigWorks.Select(a => a.MaintenanceRunningWork.NameAr).ToList()))
                .ForMember(tm => tm.TenderActivities, opt => opt.MapFrom(t => t.TenderActivities.Select(a => a.Activity.NameAr).ToList()));

        }

        private void TenderInvitationModelMap()
        {
            CultureInfo arProvider = CultureInfo.CreateSpecificCulture("ar-SA");
            CreateMap<TenderInvitationDetailsModel, Invitation>();
            CreateMap<Invitation, TenderInvitationDetailsModel>()
            .ForMember(tm => tm.TenderIdString, opt => opt.MapFrom(t => (Util.Encrypt(t.TenderId))))
            .ForMember(tm => tm.InvitationStatusName, opt => opt.MapFrom(t => t.InvitationStatus.NameAr))
            .ForMember(tm => tm.TenderName, opt => opt.MapFrom(t => t.Tender.TenderName))
            .ForMember(tm => tm.CommericalName, opt => opt.MapFrom(t => t.Supplier.SelectedCrName))
            .ForMember(tm => tm.TenderNumber, opt => opt.MapFrom(t => t.Tender.TenderNumber))
            .ForMember(tm => tm.ReferenceNumber, opt => opt.MapFrom(t => t.Tender.ReferenceNumber))
            .ForMember(tm => tm.OffersOpeningDate, opt => opt.MapFrom(t => t.Tender.OffersOpeningDate))
            .ForMember(tm => tm.LastEnqueriesDate, opt => opt.MapFrom(t => t.Tender.LastEnqueriesDate))
            .ForMember(tm => tm.LastOfferPresentationDate, opt => opt.MapFrom(t => t.Tender.LastOfferPresentationDate))

           .ForMember(tm => tm.OffersOpeningDateHijri, opt => opt.MapFrom(t => t.Tender.OffersOpeningDate.HasValue ? t.Tender.OffersOpeningDate.Value.ToString("yyyy-MM-dd", arProvider) : ""))

            .ForMember(tm => tm.LastEnqueriesDateHijri, opt => opt.MapFrom(t => t.Tender.LastEnqueriesDate.HasValue ? t.Tender.LastEnqueriesDate.Value.ToString("yyyy-MM-dd", arProvider) : ""))

            .ForMember(tm => tm.LastOfferPresentationDateHijri, opt => opt.MapFrom(t => t.Tender.LastOfferPresentationDate.HasValue ? t.Tender.LastOfferPresentationDate.Value.ToString("yyyy-MM-dd", arProvider) : ""

            ))
            .ForMember(tm => tm.TechnicalOrganizationId, opt => opt.MapFrom(t => t.Tender.TechnicalOrganizationId))
            .ForMember(tm => tm.InsideKSA, opt => opt.MapFrom(t => t.Tender.InsideKSA))
            .ForMember(tm => tm.InvitationStatusId, opt => opt.MapFrom(t => t.StatusId))
            .ForMember(tm => tm.InvitationRequistTypeId, opt => opt.MapFrom(t => t.InvitationTypeId))
            .ForMember(tm => tm.CondetionalBookletPrice, opt => opt.MapFrom(t => t.Tender.ConditionsBookletPrice))
            .ForMember(tm => tm.TenderStatusId, opt => opt.MapFrom(t => t.Tender.TenderStatusId))
            .ForMember(tm => tm.AgencyName, opt => opt.MapFrom(t => t.Tender.Agency.NameArabic))
            .ForMember(tm => tm.TenderStatusIdString, opt => opt.MapFrom(t => (Util.Encrypt(t.Tender.TenderStatusId))));
        }

        #endregion

        #region Methods

        private int GetInvitationId(Tender src, ResolutionContext context)
        {
            var invitation = src.Invitations.Where(f => context.Options.Items["cr"].ToString() != "" ? (f.CommericalRegisterNo == context.Options.Items["cr"].ToString() && f.IsActive == true) : f.IsActive == true).FirstOrDefault();

            return invitation == null ? 0 : invitation.InvitationId;
        }

        private int GetInvitationStatus(Tender src, ResolutionContext context)
        {
            var invitation = src.Invitations.Where(f => context.Options.Items["cr"].ToString() != "" ? (f.CommericalRegisterNo == context.Options.Items["cr"].ToString() && f.IsActive == true) : f.IsActive == true).FirstOrDefault();

            return invitation == null ? 0 : invitation.StatusId;
        }

        private int GetInvitationType(Tender src, ResolutionContext context)
        {
            var invitation = src.Invitations.Where(f => context.Options.Items["cr"].ToString() != "" ? (f.CommericalRegisterNo == context.Options.Items["cr"].ToString() && f.IsActive == true) : f.IsActive == true).FirstOrDefault();

            return invitation == null ? 0 : invitation.InvitationTypeId;
        }

        #endregion

    }
}
