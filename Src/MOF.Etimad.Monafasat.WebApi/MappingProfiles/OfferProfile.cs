using AutoMapper;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.ViewModel;
using System;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.WebApi
{
    public class OfferProfile : Profile
    {
        public OfferProfile()
        {
            OfferMap();
        }

        private void OfferMap()
        {
            CreateMap<TechnicianReportAttachment, TechniciansReportAttachmentModel>()
               .ForMember(tr => tr.AttachmentTypeId, opt => opt.MapFrom(s => s.AttachmentTypeId));
            CreateMap<TechniciansReportAttachmentModel, TechnicianReportAttachment>()
                .ForMember(tr => tr.AttachmentTypeId, opt => opt.MapFrom(s => s.AttachmentTypeId));


            CreateMap<OfferModel, Offer>()
                .ConstructUsing(fr => new Offer(fr.CommericalRegisterNo, fr.TenderId, fr.IsActive));
            CreateMap<Offer, OfferModel>().ForMember(o => o.TenderStatusId, opt => opt.MapFrom(of => of.Tender.TenderStatusId));
            CreateMap<Offer, OfferCheckingDetailsModel>().ForMember(o => o.TenderStatusId, opt => opt.MapFrom(of => of.Tender.TenderStatusId));
            CreateMap<SupplierBankGuaranteeModel, SupplierBankGuaranteeAttachment>()
                .ConstructUsing(tr => new SupplierBankGuaranteeAttachment(tr.AttachmentId, tr.OfferId, tr.IsBankGuaranteeValid, tr.GuaranteeNumber, tr.BankId.Value, tr.Amount, tr.GuaranteeStartDate, tr.GuaranteeEndDate, tr.GuaranteeDays));

            CreateMap<SupplierSpecificationModel, SupplierSpecificationAttachment>()
                .ConstructUsing(tr => new SupplierSpecificationAttachment(tr.AttachmentId, tr.OfferId, tr.IsForApplier, tr.Degree, tr.ConstructionWorkId, tr.MaintenanceRunningWorkId));
            CreateMap<SupplierCombinedDetail, OfferModel>();
        }
    }
}
