using AutoMapper;
using MOF.Etimad.Monafasat.Core;
using MOF.Etimad.Monafasat.Api.Models;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.WebApi
{
    public class BranchProfile : Profile
    {
        public BranchProfile()
        {
            BranchMap();
        }

       void BranchMap()
        {
            CreateMap<BranchSearchCriteriaModel, BranchSearchCriteria>().ForMember(dest => dest.IsActive, opt => opt.Ignore());
            CreateMap<BranchModel, Branch>().ForMember(dest => dest.IsActive, opt => opt.Ignore()).ForMember(dest => dest.IsActive, opt => opt.UseValue(true));
            CreateMap<Branch, BranchModel>()
                                            //Main Branch Address
                                            .ForMember(dest => dest.MainBranchAddress, opt => opt.MapFrom(src => src.MainBranchAddress.Address))
                                            .ForMember(dest => dest.MainBranchEmail, opt => opt.MapFrom(src => src.MainBranchAddress.Email))
                                            .ForMember(dest => dest.MainBranchFax, opt => opt.MapFrom(src => src.MainBranchAddress.Fax))
                                            .ForMember(dest => dest.MainBranchFax2, opt => opt.MapFrom(src => src.MainBranchAddress.Fax2))
                                            .ForMember(dest => dest.MainBranchPhone, opt => opt.MapFrom(src => src.MainBranchAddress.Phone))
                                            .ForMember(dest => dest.MainBranchPhone2, opt => opt.MapFrom(src => src.MainBranchAddress.Phone2))
                                            .ForMember(dest => dest.MainBranchPosition, opt => opt.MapFrom(src => src.MainBranchAddress.Position))
                                            //Deliver Offer Address
                                            .ForMember(dest => dest.DeliverOfferAddressName, opt => opt.MapFrom(src => src.DeliverOfferAddress.AddressName))
                                            .ForMember(dest => dest.DeliverOfferCityCode, opt => opt.MapFrom(src => src.DeliverOfferAddress.CityCode))
                                            .ForMember(dest => dest.DeliverOfferDescription, opt => opt.MapFrom(src => src.DeliverOfferAddress.Description))
                                            .ForMember(dest => dest.DeliverOfferPhone, opt => opt.MapFrom(src => src.DeliverOfferAddress.Phone))
                                            .ForMember(dest => dest.DeliverOfferPostalCode, opt => opt.MapFrom(src => src.DeliverOfferAddress.PostalCode))
                                            .ForMember(dest => dest.DeliverOfferZipCode, opt => opt.MapFrom(src => src.DeliverOfferAddress.ZipCode))
                                            //Open Offer Address
                                            .ForMember(dest => dest.OpenOfferAddressName, opt => opt.MapFrom(src => src.OpenOfferAddress.AddressName))
                                            .ForMember(dest => dest.OpenOfferCityCode, opt => opt.MapFrom(src => src.OpenOfferAddress.CityCode))
                                            .ForMember(dest => dest.OpenOfferDescription, opt => opt.MapFrom(src => src.OpenOfferAddress.Description))
                                            .ForMember(dest => dest.OpenOfferPhone, opt => opt.MapFrom(src => src.OpenOfferAddress.Phone))
                                            .ForMember(dest => dest.OpenOfferPostalCode, opt => opt.MapFrom(src => src.OpenOfferAddress.PostalCode))
                                            .ForMember(dest => dest.OpenOfferZipCode, opt => opt.MapFrom(src => src.OpenOfferAddress.ZipCode))
                                            //Deliver Specification Book Address
                                            .ForMember(dest => dest.DeliverSpecificationBookAddressName, opt => opt.MapFrom(src => src.DeliverSpecificationBookOfferAddress.AddressName))
                                            .ForMember(dest => dest.DeliverSpecificationBookCityCode, opt => opt.MapFrom(src => src.DeliverSpecificationBookOfferAddress.CityCode))
                                            .ForMember(dest => dest.DeliverSpecificationBookDescription, opt => opt.MapFrom(src => src.DeliverSpecificationBookOfferAddress.Description))
                                            .ForMember(dest => dest.DeliverSpecificationBookPhone, opt => opt.MapFrom(src => src.DeliverSpecificationBookOfferAddress.Phone))
                                            .ForMember(dest => dest.DeliverSpecificationBookPostalCode, opt => opt.MapFrom(src => src.DeliverSpecificationBookOfferAddress.PostalCode))
                                            .ForMember(dest => dest.DeliverSpecificationBookZipCode, opt => opt.MapFrom(src => src.DeliverSpecificationBookOfferAddress.ZipCode));
                                                    //.ForMember(dest => dest.BranchName, opt => opt.MapFrom(src => src.Branch.BranchName));

        }

    }
}
