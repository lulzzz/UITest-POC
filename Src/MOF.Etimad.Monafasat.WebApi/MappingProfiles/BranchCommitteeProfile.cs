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
    public class BranchCommitteeProfile : Profile
    {
        public BranchCommitteeProfile()
        {
            BranchCommitteeMap();
        }

       void BranchCommitteeMap()
        {
            CreateMap<BranchCommitteeSearchCriteriaModel, BranchCommitteeSearchCriteria>().ForMember(dest => dest.IsActive, opt => opt.Ignore());
            CreateMap<BranchCommitteeModel, BranchCommittee>().ForMember(dest => dest.IsActive, opt => opt.Ignore()).ForMember(dest => dest.IsActive, opt => opt.UseValue(true));
            CreateMap<BranchCommittee, BranchCommitteeModel>().ForMember(dest => dest.CommitteeName, opt => opt.MapFrom(src => src.Committee.CommitteeName));
                                                    //.ForMember(dest => dest.BranchName, opt => opt.MapFrom(src => src.Branch.BranchName));

        }

    }
}
