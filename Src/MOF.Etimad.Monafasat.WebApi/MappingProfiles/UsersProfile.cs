using AutoMapper;
using MOF.Etimad.Monafasat.Core;
using MOF.Etimad.Monafasat.Integration;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOF.Etimad.Monafasat.WebApi.MappingProfiles
{
    public class UsersProfile : Profile
    {
        public UsersProfile()
        {
            UsersMap();
        }
        private void UsersMap()
        {
            CreateMap<IDMRolesModel, RoleModel>().ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.Id))
              .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Name))
                 .ForMember(dest => dest.RoleNameAr, opt => opt.MapFrom(src => src.NormalizedName));
            CreateMap<RoleModel, IDMRolesModel>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.RoleId))
                 .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.RoleName))
                 .ForMember(dest => dest.NormalizedName, opt => opt.MapFrom(src => src.RoleNameAr));
            CreateMap<GovAgency, GovAgencyModel>();
            CreateMap<GovAgencyModel, GovAgency>();
            CreateMap<EmployeeIntegrationModel, ManageUsersAssignationModel>().ForMember(dest => dest.userIdString, opt => opt.MapFrom(src => Util.Encrypt(src.userId)));
            CreateMap<EmployeeIntegrationModel, ManageUsersAssignationModel>();
            CreateMap<EmployeeIntegrationModel, ManageUsersAssignationModel>().ForMember(dest => dest.userIdString, opt => opt.MapFrom(src => Util.Encrypt(src.userId)))
                .ForMember(dest => dest.crNumber, opt => opt.MapFrom(src => src.crNumber))
                .ForMember(dest => dest.fullName, opt => opt.MapFrom(src => src.fullName))
                .ForMember(dest => dest.agencies, opt => opt.MapFrom(src => src.agencies))
                 .ForMember(dest => dest.CRs, opt => opt.MapFrom(src => src.CRs))
                .ForMember(dest => dest.vendors, opt => opt.MapFrom(src => src.vendors))
                .ForMember(dest => dest.dateOfBirth, opt => opt.MapFrom(src => src.dateOfBirth))
                .ForMember(dest => dest.email, opt => opt.MapFrom(src => src.email))
                .ForMember(dest => dest.isAccountManager, opt => opt.MapFrom(src => src.isAccountManager))
                .ForMember(dest => dest.isAdmin, opt => opt.MapFrom(src => src.isAdmin))
                .ForMember(dest => dest.isBlackListCommuity, opt => opt.MapFrom(src => src.isBlackListCommuity))
                .ForMember(dest => dest.isCSO, opt => opt.MapFrom(src => src.isCSO))
                .ForMember(dest => dest.isReportsUser, opt => opt.MapFrom(src => src.isReportsUser))
                .ForMember(dest => dest.mobileNumber, opt => opt.MapFrom(src => src.mobileNumber))
                .ForMember(dest => dest.nationalId, opt => opt.MapFrom(src => src.nationalId))
                .ForMember(dest => dest.nationalIdString, opt => opt.MapFrom(src => Util.Encrypt(src.nationalId)))
                .ForMember(dest => dest.roles, opt => opt.MapFrom(src => src.roles))
               .ForMember(dest => dest.AgencyNames, opt => opt.MapFrom(src =>
                (src.agencies.Count > 0 ? String.Join(',', src.agencies.Select(a => a.agencyName).Distinct().ToList()) : (src.CRs.Count > 0 ? String.Join(',', src.CRs.Select(a => a.agencyName)) : String.Join(',', src.vendors.Select(a => a.agencyName))))
               ));
            CreateMap<ManageUsersAssignationModel, EmployeeIntegrationModel>().ForMember(dest => dest.userId, opt => opt.MapFrom(src => Util.Decrypt(src.userIdString)))
               .ForMember(dest => dest.crNumber, opt => opt.MapFrom(src => src.crNumber))
               .ForMember(dest => dest.fullName, opt => opt.MapFrom(src => src.fullName))
               .ForMember(dest => dest.agencies, opt => opt.MapFrom(src => src.agencies))
                 .ForMember(dest => dest.CRs, opt => opt.MapFrom(src => src.CRs))
                .ForMember(dest => dest.vendors, opt => opt.MapFrom(src => src.vendors))
               .ForMember(dest => dest.dateOfBirth, opt => opt.MapFrom(src => src.dateOfBirth))
               .ForMember(dest => dest.email, opt => opt.MapFrom(src => src.email))
               .ForMember(dest => dest.isAccountManager, opt => opt.MapFrom(src => src.isAccountManager))
               .ForMember(dest => dest.isAdmin, opt => opt.MapFrom(src => src.isAdmin))
               .ForMember(dest => dest.isBlackListCommuity, opt => opt.MapFrom(src => src.isBlackListCommuity))
               .ForMember(dest => dest.isCSO, opt => opt.MapFrom(src => src.isCSO))
               .ForMember(dest => dest.isReportsUser, opt => opt.MapFrom(src => src.isReportsUser))
               .ForMember(dest => dest.mobileNumber, opt => opt.MapFrom(src => src.mobileNumber))
               .ForMember(dest => dest.nationalId, opt => opt.MapFrom(src => src.nationalId))
               .ForMember(dest => dest.nationalId, opt => opt.MapFrom(src => Util.Decrypt(src.nationalIdString)))
               .ForMember(dest => dest.roles, opt => opt.MapFrom(src => src.roles));
        }
    }
}