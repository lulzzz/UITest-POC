using AutoMapper;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.ViewModel;
using System.Linq;

namespace MOF.Etimad.Monafasat.WebApi
{
    public class MandatoryListProfile : Profile
    {
        public MandatoryListProfile()
        {
            // Entity To View Model 
            CreateMap<MandatoryList, MandatoryListViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Util.Encrypt(src.Id)))
                .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products.Where(a => a.IsActive == true)));

            CreateMap<MandatoryListProduct, MandatoryListProductViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Util.Encrypt(src.Id)));

            // Inputs View Model to Entity
            CreateMap<InputMandatoryListViewModel, MandatoryList>()
                  .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Util.Decrypt(src.Id)));
            CreateMap<InputMandatoryListProductViewModel, MandatoryListProduct>()
                  .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Util.Decrypt(src.Id)));

            CreateMap<MandatoryListViewModel, InputMandatoryListViewModel>();
            CreateMap<MandatoryListProductViewModel, InputMandatoryListProductViewModel>();

            CreateMap<MandatoryList, InputMandatoryListViewModel>();
            CreateMap<MandatoryListProduct, InputMandatoryListProductViewModel>();


            CreateMap<MandatoryList, MandatoryListIndexViewModel>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.NameAr))
                .ForMember(dest => dest.EncryptedId, opt => opt.MapFrom(src => Util.Encrypt(src.Id)))
                .ForMember(dest => dest.EncryptedStatusId, opt => opt.MapFrom(src => Util.Encrypt(src.StatusId)))
                .ForMember(dest => dest.ProductsCount, opt => opt.MapFrom(src => src.Products.Where(a => a.IsActive == true).Count()));

            // Entity To Change Request

            CreateMap<MandatoryListChangeRequest, MandatoryListChangeRequestViewModel>();

            CreateMap<MandatoryList, MandatoryListChangeRequest>()
                .ForMember(opt => opt.Id, src => src.Ignore());

            CreateMap<MandatoryListProduct, MandatoryListProductChangeRequest>()
                .ForMember(opt => opt.Id, src => src.Ignore());

            // Change Request to Entity
            CreateMap<MandatoryListChangeRequest, MandatoryList>()
                .ForMember(opt => opt.Id, src => src.Ignore());

            CreateMap<MandatoryListProductChangeRequest, MandatoryListProduct>()
                .ForMember(opt => opt.Id, src => src.Ignore());
        }
    }
}
