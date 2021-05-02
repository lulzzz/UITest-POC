using AutoMapper;
using MOF.Etimad.Monafasat.Core;
using MOF.Etimad.Monafasat.Integration;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.ViewModel;
using System;
using System.Linq;

namespace MOF.Etimad.Monafasat.WebApi.MappingProfiles
{
    public class BlockProfile : Profile
    {
        public BlockProfile()
        {
            BlockMap();
        }
        private void BlockMap()
        {
            CreateMap<BlockSearchCriteriaModel, BlockSearchCriteria>();
            CreateMap<BlockCommitteeSearchCriteriaModel, BlockCommitteeSearchCriteria>();
            CreateMap<SupplierBlockModel, SupplierBlock>().ForMember(dest => dest.IsActive, opt => opt.Ignore());
            CreateMap<SupplierBlock, SupplierBlockModel>().ForMember(dest => dest.BlockTypeName, opt => opt.MapFrom(src => src.BlockType.NameAr))
                .ForMember(dest => dest.AgencyName, opt => opt.MapFrom(src => src.Agency.NameArabic))
                .ForMember(dest => dest.FileName, opt => opt.MapFrom(src => Util.Encrypt(src.AdminFileName)))
                .ForMember(dest => dest.FileNetReferenceId, opt => opt.MapFrom(src => Util.Encrypt(src.AdminFileNetReferenceId)))
                                                   .ForMember(dest => dest.SupplierBlockIdString, opt => opt.MapFrom(src => Util.Encrypt(src.SupplierBlockId)));
            CreateMap<SupplierSearchCretriaModel, SupplierIntegrationSearchCriteria>().ForMember(dest => dest.CrNumber, opt => opt.MapFrom(src => src.CommericalRegisterNo))
                .ForMember(dest => dest.SupplierName, opt => opt.MapFrom(src => src.CommericalRegisterName))
                .ForMember(dest => dest.activityDescription, opt => opt.MapFrom(src => src.ActivityDescription))
                .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.CityName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.EMail))
                .ForMember(dest => dest.isicActivityID, opt => opt.MapFrom(src => src.MainActivitiesId))
                .ForMember(dest => dest.isicActivityLevel, opt => opt.MapFrom(src => src.ActivitesLevelId));

            CreateMap<SolidaritySearchCriteria, SupplierIntegrationSearchCriteria>()
           .ForMember(dest => dest.CrNumber, opt => opt.MapFrom(src => src.CommericalRegisterNo))
           .ForMember(dest => dest.SupplierName, opt => opt.MapFrom(src => src.CommericalRegisterName))
           .ForMember(dest => dest.activityDescription, opt => opt.MapFrom(src => src.ActivityDescription))
           .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.CityName))
           .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.EMail))
           .ForMember(dest => dest.isicActivityID, opt => opt.MapFrom(src => src.MainActivitiesId))
           .ForMember(dest => dest.isicActivityLevel, opt => opt.MapFrom(src => src.ActivitesLevelId));

            CreateMap<SupplierIntegrationModel, SupplierModel>().ForMember(dest => dest.ActivityDescription, opt => opt.MapFrom(src => src.activities))
                       .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.cityName))
                       .ForMember(dest => dest.CommericalRegisterName, opt => opt.MapFrom(src => src.supplierName))
                       .ForMember(dest => dest.CommericalRegisterNo, opt => opt.MapFrom(src => src.supplierNumber))
                       .ForMember(dest => dest.EMail, opt => opt.MapFrom(src => src.Email))
                       .ForMember(dest => dest.Fax, opt => opt.MapFrom(src => src.faxNumber))
                       .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.postalCode))
                       .ForMember(dest => dest.PostOffice, opt => opt.MapFrom(src => src.postOfficeNumber))
                       .ForMember(dest => dest.SupplierName, opt => opt.MapFrom(src => src.supplierName));


            CreateMap<EmployeeIntegrationModel, UserProfileModel>()
                 .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.fullName))
                 .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.email))
                 .ForMember(dest => dest.Mobile, opt => opt.MapFrom(src => src.mobileNumber))
                 .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.nationalId))
                 .ForMember(dest => dest.Role, opt => opt.MapFrom(src => string.Join(",", src.roles.Select(r => r.NormalizedName).ToList()).ToString()));
        }
    }
}