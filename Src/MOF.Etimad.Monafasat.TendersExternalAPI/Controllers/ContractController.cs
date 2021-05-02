using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MOF.Etimad.Monafasat.Integration.Enums;
using MOF.Etimad.Monafasat.Integration.Infrastructure;
using MOF.Etimad.Monafasat.TendersAPI.Model;
using MOF.Etimad.Monafasat.TendersAPI.Services;

namespace MOF.Etimad.Monafasat.TendersAPI.Controllers
{
    [Route("api/[controller]")]
    public class ContractController : ControllerBase
    {
        private readonly IContractAppService _contractAppService;
        private readonly ILogger<ContractController> _logger;
        private readonly IMapper _mapper;
        public ContractController(ILogger<ContractController> logger, IMapper mapper, IContractAppService contractAppService)
        {
            _contractAppService = contractAppService;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost("CheckTenderCanBeLinkedToContract")]
        public async Task<ContractResponseModel> CheckTenderCanBeLinkedToContract([FromBody]ContractCanBeLinkedEnquiryModel contractCanBeLinkedEnquiryModel)
        {
            try
            {
                if (contractCanBeLinkedEnquiryModel == null) { throw new Exception("missing parameters"); }
                var res = await _contractAppService.CheckTenderCanBeLinkedToContract(contractCanBeLinkedEnquiryModel.TenderReferenceNumber, contractCanBeLinkedEnquiryModel.AgencyCode);
                ContractResponseModel contractResponseModel = _mapper.Map<ContractResponseModel>(res);
                return contractResponseModel;
            }
            catch (NullReferenceException ex)
            {
                Logger.LogToFile(ex.Message.ToString(), "NullReferenceException");
                _logger.LogError(ex.ToString(), ex);
                return new ContractResponseModel() { StatusCode = ServiceStatusCodes.NoDataFound, StatusDesc = ex.Message };
            }
            catch (Exception ex)
            {
                Logger.LogToFile(ex.Message.ToString(), "Exception");
                _logger.LogError(ex.ToString(), ex);
                return new ContractResponseModel() { StatusCode = ServiceStatusCodes.WrongInputData, StatusDesc = ex.Message };
            }
        }
    }
}