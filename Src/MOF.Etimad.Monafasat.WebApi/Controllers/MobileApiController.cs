using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Services;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernel.Exceptions;
using MOF.Etimad.Monafasat.ViewModel.Mobile;
using MOF.Etimad.Monafasat.WebApi.Infrastructure;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.WebApi.Controllers
{


    public class MobileApiController : BaseController
    {
        private readonly IMobileAppService _appService;

        public MobileApiController(IMobileAppService appService, IOptionsSnapshot<RootConfigurations> rootConfiguration) : base(rootConfiguration)
        {

            _appService = appService;
        }



        [HttpGet]
        [HttpPost]
        [Route("[controller]/GetTenders")]
        public async Task<IActionResult> index([FromQuery(Name = "tender-type")] string tender_type, int page, string title, string ref_no, string ga_head, int main_category, TimeSpan publish_date, bool no_old_tenders)
        {
            var result = await _appService.GetTenders(tender_type, page, title, ref_no, ga_head, main_category, publish_date, no_old_tenders);
            return Ok(result);
        }


        [HttpGet]
        [Route("[controller]/GetActivities")]

        public async Task<IActionResult> getActivities()
        {
            var result = await _appService.GetActivities();
            return Ok(result);
        }
        [HttpGet]
        [Route("[controller]/GovAgencies")]
        public async Task<IActionResult> GetAgencies()
        {
            var result = await _appService.GovAgencies();
            return Ok(result);
        }


        [HttpPost]
        [Route("[controller]/UpdateNotificationsStatus")]
        public async Task<IActionResult> UpdateNotificationsStatus(string deviceToken, int cid, bool status)
        {
            var result = await _appService.UpdateNotificationsStatus(deviceToken, cid, status);
            return Ok(result);

        }
        [HttpGet]
        [HttpPost]
        [Route("[controller]/RegisterToken")]
        public async Task<IActionResult> RegisterToken([FromBody] RegisterTokenModel model)
        {
            var result = await _appService.RegisterToken(model);
            return Ok(result);

        }

        [HttpGet]
        [HttpPost]
        [Route("[controller]/FetchNotificationsStatus/{deviceToken}")]
        public async Task<IActionResult> FetchNotificationsStatus(string deviceToken)
        {
            var result = await _appService.FetchNotificationsStatus(deviceToken);
            return Ok(result);

        }






    }
}
