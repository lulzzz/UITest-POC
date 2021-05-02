using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Services;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.WebApi.Infrastructure;
using MOF.Etimad.SharedKernel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class MandatoryListController : BaseController
    {
        private readonly IMandatoryListAppService _mandatoryListAppService;
        private readonly IVerificationService _verification;

        public MandatoryListController(IMandatoryListAppService mandatoryListAppService, IOptionsSnapshot<RootConfigurations> rootConfiguration, IVerificationService verification) : base(rootConfiguration)
        {
            _mandatoryListAppService = mandatoryListAppService;
            _verification = verification;
        }

        [Authorize(PolicyNames.ViewMandatoryListPolicy)]
        [HttpGet]
        [Route("Search")]
        public async Task<QueryResult<MandatoryListIndexViewModel>> Search(MandatoryListSearchViewModel criteria)
        {
            return await _mandatoryListAppService.Search(criteria);
        }

        [Authorize(PolicyNames.ViewMandatoryListPolicy)]
        [HttpGet]
        [Route("Find/{id}")]
        public async Task<MandatoryListViewModel> Find(string id)
        {
            return await _mandatoryListAppService.Find(Util.Decrypt(id));
        }

        [Authorize(PolicyNames.EditMandatoryListPolicy)]
        [HttpGet]
        [Route("FindForEdit/{id}")]
        public async Task<InputMandatoryListViewModel> FindForEdit(string id)
        {
            return await _mandatoryListAppService.FindForEdit(Util.Decrypt(id));
        }

        [Authorize(PolicyNames.AddMandatoryListPolicy)]
        [HttpPost]
        [Route("Add")]
        public async Task<MandatoryListViewModel> Add([FromBody] InputMandatoryListViewModel viewModel)
        {
            return await _mandatoryListAppService.Add(viewModel);
        }

        [Authorize(PolicyNames.EditMandatoryListPolicy)]
        [HttpPost]
        [Route("Update")]
        public async Task Update([FromBody] InputMandatoryListViewModel viewModel)
        {
            await _mandatoryListAppService.Update(viewModel);
        }


        [Authorize(PolicyNames.EditMandatoryListPolicy)]
        [HttpPost]
        [Route("SendToApproval/{id}")]
        public async Task SendToApproval(string id)
        {
            await _mandatoryListAppService.SendToApproval(Util.Decrypt(id));
        }

        [Authorize(PolicyNames.MandatoryListApprovalPolicy)]
        [HttpPost]
        [Route("Approve/{id}/{verificationCode}")]
        public async Task Approve(string id, string verificationCode)
        {
            await _mandatoryListAppService.Approve(Util.Decrypt(id), verificationCode);
        }


        [Authorize(PolicyNames.MandatoryListApprovalPolicy)]
        [HttpPost]
        [Route("Reject")]
        public async Task Reject([FromBody] MandatoryListRejectionViewModel rejectionModel)
        {
            await _mandatoryListAppService.Reject(rejectionModel);
        }

        [Authorize(PolicyNames.EditMandatoryListPolicy)]
        [HttpPost]
        [Route("Reopen/{id}")]
        public async Task Reopen(string id)
        {
            await _mandatoryListAppService.Reopen(Util.Decrypt(id));
        }

        [Authorize(PolicyNames.MandatoryListApprovalPolicy)]
        [HttpPost]
        [Route("ApproveEdit/{id}/{verificationCode}")]
        public async Task ApproveEdit(string id, string verificationCode)
        {
            await _mandatoryListAppService.ApproveEdit(Util.Decrypt(id), verificationCode);
        }

        [Authorize(PolicyNames.MandatoryListApprovalPolicy)]
        [HttpPost]
        [Route("RejectEdit")]
        public async Task RejectEdit([FromBody] MandatoryListRejectionViewModel rejectionModel)
        {
            await _mandatoryListAppService.RejectEdit(rejectionModel);
        }

        [Authorize(PolicyNames.EditMandatoryListPolicy)]
        [HttpPost]
        [Route("CloseEdit/{id}")]
        public async Task CloseEdit(string id)
        {
            await _mandatoryListAppService.CloseEdit(Util.Decrypt(id));
        }

        [Authorize(PolicyNames.EditMandatoryListPolicy)]
        [HttpPost]
        [Route("Delete/{id}")]
        public async Task Delete(string id)
        {
            await _mandatoryListAppService.Delete(Util.Decrypt(id));
        }

        [Authorize(PolicyNames.EditMandatoryListPolicy)]
        [HttpPost]
        [Route("RequestDelete/{id}")]
        public async Task RequestDelete(string id)
        {
            await _mandatoryListAppService.RequestDelete(Util.Decrypt(id));
        }

        [Authorize(PolicyNames.MandatoryListApprovalPolicy)]
        [HttpPost]
        [Route("ApproveDelete/{id}/{verificationCode}")]
        public async Task ApproveDelete(string id, string verificationCode)
        {
            await _mandatoryListAppService.ApproveDelete(Util.Decrypt(id), verificationCode);
        }

        [Authorize(PolicyNames.MandatoryListApprovalPolicy)]
        [HttpPost]
        [Route("RejectDelete")]
        public async Task RejectDelete([FromBody] MandatoryListRejectionViewModel viewModel)
        {
            await _mandatoryListAppService.RejectDelete(viewModel);
        }

        [Authorize(PolicyNames.ViewMandatoryListPolicy)]
        [HttpGet]
        [Route("GetMandatoryListDetails/{mandatoryListId}")]
        public async Task<MandatoryListDetailsViewModel> GetMandatoryListDetails(string mandatoryListId)
        {
            MandatoryListDetailsViewModel detailsViewModel = await _mandatoryListAppService.GetMandatoryListDetails(Util.Decrypt(mandatoryListId));
            return detailsViewModel;
        }

        [Authorize(PolicyNames.ViewMandatoryListPolicy)]
        [HttpGet]
        [Route("GetMandatoryListDetailsForApproval/{mandatoryListId}")]
        public async Task<MandatoryListApprovalViewModel> GetMandatoryListDetailsForApproval(string mandatoryListId)
        {
            MandatoryListApprovalViewModel approvalViewModel = await _mandatoryListAppService.GetMandatoryListDetailsForApproval(Util.Decrypt(mandatoryListId));
            return approvalViewModel;
        }

        [HttpPost]
        [Route("CreateVerificationCode/{id}")]
        public async Task CreateVerificationCode(string id)
        {
            var userEmail = User.Email();
            var userMobile = User.Mobile();
            int mandatoryListId = Util.Decrypt(id);
            await _verification.CreateVerificationCode(mandatoryListId, userMobile, userEmail, User.UserId(), (int)Enums.VerificationType.MandatoryList);
        }

        [HttpGet]
        [Route("GetAllForQuantityTable")]
        public async Task<List<MandatoryListForQuantityTableViewModel>> GetAllForQuantityTable()
        {
            return await _mandatoryListAppService.GetAllForQuantitiyTable();
        }
        [HttpGet]
        [Route("GetMandatoryListCSICodeInfo/{code}")]
        public async Task<CSICodeDetailsModel> GetMandatoryListCSICodeInfo(string code)
        {
            return await _mandatoryListAppService.GetMandatoryListCSICodeInfo(code);
        }

        [HttpPost]
        [Route("GetValidMandatoryListCodeForQauntityTableExcel")]
        public async Task<Dictionary<string, bool>> GetValidMandatoryListCodeForQauntityTableExcel([FromBody] List<string> CSICodes)
        {
            Dictionary<string, bool> result = await _mandatoryListAppService.GetValidMandatoryListCodeForQauntityTableExcel(CSICodes);
            return result;
        }

    }
}
