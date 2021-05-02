using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.Services;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.WebApi.Infrastructure;
using System.Threading.Tasks;
using static MOF.Etimad.Monafasat.SharedKernel.Enums;

namespace MOF.Etimad.Monafasat.Api.Controllers
{
    [Route("api/[controller]")]
    public class PaymentController : BaseController
    {
        private readonly IBillAppService _billsInqueryAppService;
        public PaymentController(IBillAppService billAppService, IOptionsSnapshot<RootConfigurations> rootConfiguration) : base(rootConfiguration)
        {
            _billsInqueryAppService = billAppService;
        }

        [HttpPost("PaidBill")]
        public async Task<bool> PaidBill([FromBody] BillViewModel billViewModel)
        {
            return await _billsInqueryAppService.SetBillPaidForRolloutTeam(billViewModel);
        }
    }
}
