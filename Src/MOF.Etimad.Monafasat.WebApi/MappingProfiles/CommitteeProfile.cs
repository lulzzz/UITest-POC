using AutoMapper;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.ViewModel;
using System;
using System.Threading.Tasks;
using static MOF.Etimad.Monafasat.SharedKernel.Enums;

namespace MOF.Etimad.Monafasat.WebApi
{
    public class CommitteeProfile : Profile
    {
        public CommitteeProfile()
        {
            CommitteeMap();
            UserCommitteeMap();
        }

        #region Mapping

        void CommitteeMap()
        {
            CreateMap<CommitteeSearchCriteriaModel, CommitteeSearchCriteria>().ForMember(dest => dest.IsActive, opt => opt.Ignore());
            CreateMap<CommitteeModel, Committee>().ForMember(dest => dest.IsActive, opt => opt.Ignore()).ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true));
            CreateMap<Committee, CommitteeModel>().ForMember(dest => dest.CommitteeTypeName, opt => opt.MapFrom(src => src.CommitteeType.NameAr))
                                                  .ForMember(dest => dest.CommitteeIdString, opt => opt.MapFrom(src => Util.Encrypt(src.CommitteeId)))
                                                  .ForMember(dest => dest.CommitteeTypeIdString, opt => opt.MapFrom(src => Util.Encrypt(src.CommitteeTypeId)));
            CreateMap<Committee, CommitteeTenderModel>();
        }

        void UserCommitteeMap()
        {
            CreateMap<CommitteeUser, CommitteeUserModel>();

            CreateMap<CommitteeUserModel, CommitteeUser>()
                .ConstructUsing(tr => new CommitteeUser(tr.CommitteeId, tr.UserId, (int)((SharedKernel.Enums.UserRole)Enum.Parse(typeof(SharedKernel.Enums.UserRole), tr.RoleName))));
        }
        #endregion 
    }
}
