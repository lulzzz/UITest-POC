using AutoMapper;
using MOF.Etimad.Monafasat.Core;
using MOF.Etimad.Monafasat.ViewModel;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.WebApi.MappingProfiles
{
    public class LookuProfile : Profile
    {
        public LookuProfile()
        {
            LookupMap();
        }
        void LookupMap()
        {
            CreateMap<GovAgency, GovAgencyModel>().ForMember(x => x.NameArabic, opt => opt.MapFrom(i => i.NameArabic))
                .ForMember(x => x.AgencyCode, opt => opt.MapFrom(i => i.AgencyCode));
        }
    }
}
