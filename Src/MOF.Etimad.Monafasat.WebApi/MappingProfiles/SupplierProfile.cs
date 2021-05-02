using AutoMapper;
using MOF.Etimad.Monafasat.Integration;
using MOF.Etimad.Monafasat.ViewModel;
using System.Linq;

namespace MOF.Etimad.Monafasat.WebApi.MappingProfiles
{
    public class SupplierProfile : Profile
    {
        public SupplierProfile()
        {
            BlockMap();
        }
        private void BlockMap()
        {
            CreateMap<SupplierFullDataModel, SupplierFullDataViewModel>();
        }
    }
}