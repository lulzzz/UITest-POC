using Microsoft.AspNetCore.Mvc;
using MOF.Etimad.Monafasat.Services;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.ViewModel.LCGDto;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.TendersAPI.Controllers
{
    public class TendersController : Controller
    {
        private readonly ITenderAppService _tenderAppService;
        public TendersController(ITenderAppService tenderAppService)
        {
            _tenderAppService = tenderAppService;
        }

        [HttpGet("GetTendersList/{supplierId}/{pageSize}/{pageNumber}")]
        public async Task<ActionResult<QueryResult<TenderInfo>>> GetTendersList(string supplierId, int pageSize, int pageNumber)
        {
            if (pageSize > 100)
                return BadRequest("max size is 100 objects");

            var result = await _tenderAppService.GetTenderList(supplierId, pageSize, pageNumber);
            return Ok(result);
        }
        [HttpGet("GetTenderInfo/{tenderReferenceId}")]
        public async Task<ActionResult<TenderInfo>> GetTenderInfo(string tenderReferenceId)
        {
            if (string.IsNullOrEmpty(tenderReferenceId))
                return BadRequest("please enter the tenderReferenceId");

            var result = await _tenderAppService.GetTenderInfo(tenderReferenceId);
            return Ok(result);
        }
    }
}