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
    public class AssignUserCommitteeProfile : Profile
    {
        public AssignUserCommitteeProfile()
        {
            AssignUserCommitteeMap();
        }

       void AssignUserCommitteeMap()
        {
            CreateMap<AssignUserCommitteeSearchCriteriaModel, AssignUserCommitteeSearchCriteria>().ForMember(dest => dest.IsActive, opt => opt.Ignore());
            CreateMap<AssignUserCommitteeModel, AssignUserCommittee>().ForMember(dest => dest.IsActive, opt => opt.Ignore()).ForMember(dest => dest.IsActive, opt => opt.UseValue(true));
            CreateMap<AssignUserCommittee, AssignUserCommitteeModel>().ForMember(dest => dest.CommitteeName, opt => opt.MapFrom(src => src.Committee.CommitteeName));
            //.ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName));

        }

    }
}
