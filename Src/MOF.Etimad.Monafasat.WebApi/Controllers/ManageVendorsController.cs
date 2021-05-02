
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Services;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.WebApi.Infrastructure;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class ManageVendorsController : BaseController
    {
        private readonly ISupplierService _supplierService;
        private readonly IIDMAppService _iDMAppService;
        private readonly IMapper _mapper;
        private readonly IBranchAppService _branchService;
        private readonly ICommitteeAppService _committeeApplication;


        public ManageVendorsController(IMapper mapper, IIDMAppService iDMAppService, ICommitteeAppService committeeApplication, IBranchAppService branchService, ISupplierService supplierService, IOptionsSnapshot<RootConfigurations> rootConfiguration) : base(rootConfiguration)
        {
            _iDMAppService = iDMAppService;
            _mapper = mapper;
            _branchService = branchService;
            _committeeApplication = committeeApplication;
            _supplierService = supplierService;
        }

        // GET api/ManageVendors
        [HttpGet("Find")]
        [Authorize(Roles = RoleNames.MonafasatAdmin + "," + RoleNames.MonafasatAccountManager + "," + RoleNames.CustomerService + "," + RoleNames.OffersOppeningManager + "," + RoleNames.Auditer + "," + RoleNames.OffersCheckManager + "," + RoleNames.OffersCheckSecretary + "," + RoleNames.OffersOppeningSecretary + "," + RoleNames.DataEntry + "," + RoleNames.UnitSpecialistLevel1 + "," + RoleNames.UnitSpecialistLevel2 + "," + RoleNames.UnitManagerUser + "," + RoleNames.UnitBusinessManagement + "," + RoleNames.PurshaseSpecialist + "," + RoleNames.EtimadOfficer + "," + RoleNames.TechnicalCommitteeUser + "," + RoleNames.OffersPurchaseManager + "," + RoleNames.OffersPurchaseSecretary)]
        public async Task<QueryResult<SupplierInvitationModel>> Find(SupplierSearchCretriaModel supplierSearchCretriaModel)
        {
            if (supplierSearchCretriaModel.PageNumber <= 0 && supplierSearchCretriaModel.PageSize <= 0) { supplierSearchCretriaModel.PageNumber = 1; supplierSearchCretriaModel.PageSize = 10; }
            var Suppliers = await _supplierService.GetAllSuppliers(supplierSearchCretriaModel);
            return Suppliers;
        }

        [Authorize(Roles = RoleNames.MonafasatAdmin + "," + RoleNames.MonafasatAccountManager + "," + RoleNames.CustomerService + "," + RoleNames.OffersOppeningManager + "," + RoleNames.Auditer + "," + RoleNames.OffersCheckManager + "," + RoleNames.OffersCheckSecretary + "," + RoleNames.OffersOppeningSecretary + "," + RoleNames.DataEntry + "," + RoleNames.UnitSpecialistLevel1 + "," + RoleNames.UnitSpecialistLevel2 + "," + RoleNames.UnitManagerUser + "," + RoleNames.UnitBusinessManagement)]
        [HttpGet("GetIDMSuppliers")]
        public async Task<SupplierInvitationModel> GetSupplierByCr(string CommericalRegisterNo)
        {
            var Suppliers = await _supplierService.GetSupplierByName(CommericalRegisterNo);
            return Suppliers;
        }



    }
}