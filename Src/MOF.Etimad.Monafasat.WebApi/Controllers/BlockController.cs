using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.Core;
using MOF.Etimad.Monafasat.Integration;
using MOF.Etimad.Monafasat.Services;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.WebApi.Infrastructure;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Linq;
using System.Threading.Tasks;
using static MOF.Etimad.Monafasat.SharedKernel.Enums;

namespace MOF.Etimad.Monafasat.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class BlockController : BaseController
    {
        #region constructor and parameter
        private readonly IBlockAppService _blockService;
        private readonly IVerificationService _verification;
        private readonly ISupplierService _supplierService;
        private readonly IIDMAppService _iDMAppService;
        public BlockController(IBlockAppService blockService, ISupplierService supplierService, IVerificationService verification, IIDMAppService iDMAppService, IOptionsSnapshot<RootConfigurations> rootConfiguration) : base(rootConfiguration)
        {
            _blockService = blockService;
            _supplierService = supplierService;
            _verification = verification;
            _iDMAppService = iDMAppService;
        }
        #endregion

        #region Old
        [Authorize(RoleNames.AdminAndBlackListCommitteePolicy)]
        [HttpGet("GetBlocks")]
        public async Task<QueryResult<SupplierBlockModel>> GetBlocks(BlockSearchCriteriaModel blockSearchCriteriaModel)
        {
            var blockSearchCriteria = new BlockSearchCriteria(blockSearchCriteriaModel.CommercialRegistrationNo, blockSearchCriteriaModel.CommercialSupplierName); ;
            if (User.UserRole() == RoleNames.MonafasatAdmin)
            {
                blockSearchCriteria.AgencyCode = User.UserAgency();
            }
            else if (User.UserRole() == RoleNames.MonafasatBlackListCommittee)
            {
                blockSearchCriteria.AgencyCode = null;
            }
            var blocks = await _blockService.FindAsyn(blockSearchCriteria);
            return blocks;
        }

        [Authorize(RoleNames.AdminBlackListAccountManagerAndCustomerServicePolicy)]
        [HttpGet("GetBlockCommittee")]
        public async Task<QueryResult<BlockCommitteeModel>> GetBlockCommittee(BlockCommitteeSearchCriteriaModel blockCommitteeSearchCriteriaModel)
        {
            var blockSearchCriteria = new BlockCommitteeSearchCriteria()
            {
                CommercialRegistrationNo = blockCommitteeSearchCriteriaModel.CommercialRegistrationNo,
                CommercialSupplierName = blockCommitteeSearchCriteriaModel.CommercialSupplierName,
                EmployeeName = blockCommitteeSearchCriteriaModel.EmployeeName,
                PageNumber = blockCommitteeSearchCriteriaModel.PageNumber
            };
            var blockCommitteeModel = await _blockService.FindBlockCommittee(blockSearchCriteria);
            return blockCommitteeModel;
        }

        [Authorize(RoleNames.AdminBlackListAccountManagerAndCustomerServicePolicy)]
        [HttpGet("GetBlockListUsers")]
        public async Task<QueryResult<UserProfileModel>> GetBlockListUsers(BlockCommitteeSearchCriteriaModel blockCommitteeSearchCriteriaModel)
        {
            var blockSearchCriteria = new BlockCommitteeSearchCriteria()
            {
                CommercialRegistrationNo = blockCommitteeSearchCriteriaModel.CommercialRegistrationNo,
                EmployeeName = blockCommitteeSearchCriteriaModel.CommercialSupplierName,
                PageNumber = blockCommitteeSearchCriteriaModel.PageNumber,
                PhoneNumber = blockCommitteeSearchCriteriaModel.PhoneNumber,
                Email = blockCommitteeSearchCriteriaModel.Email,

            };
            blockSearchCriteria = GetUserAgencyTypeAndIdWithFlags(blockSearchCriteria);
            var blockCommitteeModel = await _blockService.FindListUsers(blockSearchCriteria);
            return blockCommitteeModel;
        }

        private BlockCommitteeSearchCriteria GetUserAgencyTypeAndIdWithFlags(BlockCommitteeSearchCriteria userSearchCriteriaModel)
        {
            if (User.IsInRole(RoleNames.MonafasatAdmin))
            {
                userSearchCriteriaModel.isGovVendor = User.UserIsGovVendor();
                userSearchCriteriaModel.isSemiGovAgency = User.UserIsSemiGovAgency();
                userSearchCriteriaModel.AgencyType = userSearchCriteriaModel.isSemiGovAgency ? AgencyType.SemiGovAgency : (userSearchCriteriaModel.isGovVendor ? AgencyType.GovVendor : AgencyType.Agency);
                userSearchCriteriaModel.AgencyId = User.UserAgency();
            }
            else
            {
                userSearchCriteriaModel.AgencyType = AgencyType.None;
            }
            return userSearchCriteriaModel;
        }

        [Authorize(RoleNames.AdminAndBlackListCommitteePolicy)]
        [HttpGet("GetBlockById/{supplierBlockId}")]
        public async Task<SupplierBlockModel> GetBlockById(int supplierBlockId)
        {
            SupplierBlockModel block = await _blockService.FindBlockById(supplierBlockId);
            return block;
        }

        [Authorize(Roles = RoleNames.supplier + "," + RoleNames.AccountsManagementSpecialist)]
        [HttpGet("GetBlockByIdForSupplier/{supplierBlockId}")]
        public async Task<SupplierBlockModel> GetBlockByIdForSupplier(int supplierBlockId)
        {
            SupplierBlockModel block = await _blockService.FindBlockById(supplierBlockId);
            return block;
        }

        [Authorize(RoleNames.AdminAndBlackListCommitteePolicy)]
        [HttpPost("AddBlock")]
        public async Task<IActionResult> AddBlock([FromBody] SupplierBlockModel blockModel)
        {
            if (User.UserRole() == RoleNames.MonafasatAdmin)
            {
                blockModel.AgencyCode = User.UserAgency();
            }
            else if (User.UserRole() == RoleNames.MonafasatBlackListCommittee || User.UserRole() == RoleNames.MonafasatBlockListSecritary)
            {
                blockModel.AgencyCode = null;
            }
            blockModel.BranchId = User.UserBranch();
            if (blockModel.IsOldSystem == true)
            {
                blockModel.BlockStatusId = (int)Enums.BlockStatus.ApprovedManager;
            }
            var block = new SupplierBlock(blockModel.CommercialRegistrationNo, blockModel.CommercialSupplierName, blockModel.ResolutionNumber, (int)Enums.BlockStatus.ApprovedManager, blockModel.BlockTypeId, blockModel.AgencyCode, blockModel.BlockStartDate, blockModel.BlockEndDate, blockModel.Punishment, blockModel.BlockDetails, blockModel.FileName, blockModel.FileNetReferenceId, blockModel.SecretaryFileName, blockModel.SecretaryFileNetReferenceId, blockModel.AdminBlockReason, blockModel.SecretaryBlockReason, blockModel.SupplierTypeId, blockModel.OrganizationTypeId, null, null, true);
            var result = await _blockService.AddBlockAsyn(block);
            return Ok(result);
        }

        [HttpGet("GetAgency")]
        public async Task<GovAgencyModel> GetAgency()
        {
            string AgencyCode = User.UserAgency();
            GovAgencyModel agencyModel = new GovAgencyModel();
            GovAgency gAgency = await _blockService.GetAgency(AgencyCode);
            if (gAgency != null)
                agencyModel = new GovAgencyModel() { IsOldSystem = gAgency.IsOldSystem };
            return agencyModel;
        }


        [HttpPost("GetCrName/{Cr}")]
        public async Task<string> GetCrName(string Cr)
        {
            SupplierSearchCretriaModel supplierSearchCretriaModel = new SupplierSearchCretriaModel { CommericalRegisterNo = Cr };
            QueryResult<SupplierModel> result = await _supplierService.GetSuppliersBySearchCretria(supplierSearchCretriaModel);
            return result.Items.FirstOrDefault().CommericalRegisterName;
        }

        [Authorize(RoleNames.AdminAndBlackListCommitteePolicy)]
        [HttpPost("UpdateBlock")]
        public async Task<IActionResult> UpdateBlock([FromBody] SupplierBlockModel blockModel)
        {
            var result = await _blockService.UpdateBlockAsync(blockModel);
            return Ok(result);
        }

        [Authorize(RoleNames.AdminAndBlackListCommitteePolicy)]
        [HttpPost("DeactivateBlock/{blockModel}")]
        public async Task<IActionResult> DeactivateBlock(int blockModel)
        {
            await _blockService.DeactivateBlockAsyn(blockModel);
            return Ok();
        }

        [Authorize(RoleNames.AdminAndBlackListCommitteePolicy)]
        [HttpPost("ResetSupplier/{blockModel}")]
        public async Task<IActionResult> ResetSupplier(int blockModel)
        {
            var result = await _blockService.ResetSupplier(blockModel);
            return Ok(result);
        }

        [Authorize(RoleNames.AdminAndBlackListCommitteePolicy)]
        [HttpGet("GetAllSuppliers")]
        public async Task<QueryResult<SupplierModel>> GetAllSuppliers(SupplierSearchCretriaModel supplierSearchCretriaModel)
        {
            var result = await _supplierService.GetSuppliersBySearchCretria(supplierSearchCretriaModel);
            return result;
        }
        #endregion

        #region New Update

        [Authorize(RoleNames.AdminAndBlackListCommitteePolicy)]
        [HttpGet("GetAllIDMSuppliers")]
        public async Task<QueryResult<SupplierIntegrationModel>> GetAllIDMSuppliers(SupplierSearchCretriaModel supplierSearchCretriaModel)
        {
            SupplierIntegrationSearchCriteria supplierIntegrationSearchCriteria = new SupplierIntegrationSearchCriteria
            {
                CrNumber = supplierSearchCretriaModel.CommericalRegisterNo,
                SupplierName = supplierSearchCretriaModel.CommericalRegisterName,
                PageNumber = supplierSearchCretriaModel.PageNumber
            };
            QueryResult<SupplierIntegrationModel> result = await _blockService.GetAllSuppliers(supplierIntegrationSearchCriteria);
            return result;
        }

        [Authorize(RoleNames.AdminAndBlackListCommitteePolicy)]
        [HttpGet("GetBlockingListDetails")]
        public async Task<QueryResult<SupplierBlockModel>> GetBlockingListDetails(BlockSearchCriteria supplierSearchCretriaModel)
        {
            BlockSearchCriteria blockSearchCriteria = new BlockSearchCriteria
            {
                CommercialRegistrationNo = supplierSearchCretriaModel.CommercialRegistrationNo,
                CommercialSupplierName = supplierSearchCretriaModel.CommercialSupplierName,
                PageNumber = supplierSearchCretriaModel.PageNumber,
                PageSize = supplierSearchCretriaModel.PageSize,
                IsDeletedId = supplierSearchCretriaModel.IsDeletedId,
                BlockStatusId = supplierSearchCretriaModel.BlockStatusId,
                FromDate = supplierSearchCretriaModel.FromDate,
                ToDate = supplierSearchCretriaModel.ToDate,
                ResolutionNumber = supplierSearchCretriaModel.ResolutionNumber
            };
            var result = await _blockService.GetBlockingListDetails(blockSearchCriteria);
            return result;
        }

        [Authorize(Roles = RoleNames.MonafasatAdmin + "," + RoleNames.AccountsManagementSpecialist)]
        [HttpGet("GetAdminBlockingListDetails")]
        public async Task<QueryResult<SupplierBlockModel>> GetAdminBlockingListDetails(BlockSearchCriteria supplierSearchCretriaModel)
        {

            BlockSearchCriteria blockSearchCriteria = new BlockSearchCriteria
            {
                CommercialRegistrationNo = supplierSearchCretriaModel.CommercialRegistrationNo,
                CommercialSupplierName = supplierSearchCretriaModel.CommercialSupplierName,
                PageNumber = supplierSearchCretriaModel.PageNumber,
                PageSize = supplierSearchCretriaModel.PageSize,
                IsDeletedId = supplierSearchCretriaModel.IsDeletedId,
                BlockStatusId = supplierSearchCretriaModel.BlockStatusId,
                FromDate = supplierSearchCretriaModel.FromDate,
                ToDate = supplierSearchCretriaModel.ToDate,
                ResolutionNumber = supplierSearchCretriaModel.ResolutionNumber,
                AgencyCode = User.UserAgency()
            };
            var result = await _blockService.GetAdminBlockingListDetails(blockSearchCriteria);
            return result;
        }

        [Authorize(Roles = RoleNames.MonafasatAdmin)]
        [HttpGet("GetAdminBlockList")]
        public async Task<QueryResult<SupplierBlockModel>> GetAdminBlockList(BlockSearchCriteria blockSearchCriteriaModel)
        {
            BlockSearchCriteria supplierIntegrationSearchCriteria = new BlockSearchCriteria
            {
                CommercialRegistrationNo = blockSearchCriteriaModel.CommercialRegistrationNo,
                CommercialSupplierName = blockSearchCriteriaModel.CommercialSupplierName,
                PageNumber = blockSearchCriteriaModel.PageNumber,
                PageSize = blockSearchCriteriaModel.PageSize,
                AgencyCode = User.UserAgency()
            };
            var result = await _blockService.GetAdminBlockList(supplierIntegrationSearchCriteria);
            return result;
        }

        [Authorize(RoleNames.AdminAndBlackListCommitteePolicy)]
        [HttpPost("AddAdminBlock")]
        public async Task<IActionResult> AddAdminBlock([FromBody] SupplierBlockModel blockModel)
        {
            blockModel.AgencyCode = User.UserAgency();
            blockModel.BlockStatusId = (int)Enums.BlockStatus.NewAdmin;
            blockModel.EnabledSearchByDate = false;
            await _blockService.AddAdminBlock(blockModel);
            return Ok();
        }

        [Authorize(Roles = RoleNames.MonafasatBlockListSecritary)]
        [HttpGet("GetSecretaryBlockList")]
        public async Task<QueryResult<SupplierBlockModel>> GetSecretaryBlockList(BlockSearchCriteriaModel blockSearchCriteriaModel)
        {
            BlockSearchCriteria supplierIntegrationSearchCriteria = new BlockSearchCriteria
            {
                CommercialRegistrationNo = blockSearchCriteriaModel.CommercialRegistrationNo,
                CommercialSupplierName = blockSearchCriteriaModel.CommercialSupplierName,
                PageSize = blockSearchCriteriaModel.PageSize,
                PageNumber = blockSearchCriteriaModel.PageNumber
            };
            var result = await _blockService.GetSecretaryBlockList(supplierIntegrationSearchCriteria);
            return result;
        }

        [Authorize(Roles = RoleNames.MonafasatBlockListSecritary)]
        [HttpPost("AddSecretaryBlock")]
        public async Task<IActionResult> AddSecretaryBlock([FromBody] SupplierBlockModel blockModel)
        {
            if (string.IsNullOrEmpty(blockModel.AgencyCode))
                blockModel.AgencyCode = User.UserAgency();
               await _blockService.AddSecretaryBlock(blockModel);
            return Ok();
        }

        [Authorize(Roles = RoleNames.MonafasatBlackListCommittee)]
        [HttpGet("GetManagerBlockList")]
        public async Task<QueryResult<SupplierBlockModel>> GetManagerBlockList(BlockSearchCriteriaModel blockSearchCriteriaModel)
        {
            BlockSearchCriteria supplierIntegrationSearchCriteria = new BlockSearchCriteria
            {
                CommercialRegistrationNo = blockSearchCriteriaModel.CommercialRegistrationNo,
                CommercialSupplierName = blockSearchCriteriaModel.CommercialSupplierName,
                PageNumber = blockSearchCriteriaModel.PageNumber,
                PageSize = blockSearchCriteriaModel.PageSize
            };
            var result = await _blockService.GetManagerBlockList(supplierIntegrationSearchCriteria);
            return result;
        }

        [Authorize(Roles = RoleNames.MonafasatBlackListCommittee)]
        //[HttpPost("ManagerRejectionReason/{supplierBlockId}/{reason}")]
        //public async Task ManagerRejectionReason(int supplierBlockId, string reason)
        [HttpPost("ManagerRejectionReason")]

        public async Task ManagerRejectionReason([FromBody] SupplierBlockModel blockSearchCriteria)
        {
            await _blockService.ManagerRejectionReason(blockSearchCriteria.SupplierBlockId, blockSearchCriteria.ManagerRejectionReason);
        }

        [Authorize(Roles = RoleNames.MonafasatBlackListCommittee)]
        [HttpPost("ManagerApproval/{supplierBlockId}/{verificationCode}")]
        public async Task ManagerApproval(int supplierBlockId, string verificationCode)
        {
            await _blockService.ManagerApproval(supplierBlockId, verificationCode);
        }

        [Authorize(Roles = RoleNames.MonafasatBlockListSecritary)]
        [HttpPost("SecretaryApproval/{supplierBlockId}")]
        public async Task SecretaryApproval(int supplierBlockId)
        {
            await _blockService.SecretaryApproval(supplierBlockId);
        }

        [Authorize(Roles = RoleNames.MonafasatBlockListSecritary)]
        [HttpPost("SecretaryRejectionReason")]
        public async Task SecretaryRejectionReason([FromBody] SupplierBlockModel blockSearchCriteria)
        {
            await _blockService.SecretaryRejectionReason(blockSearchCriteria.SupplierBlockId, blockSearchCriteria.SecretaryRejectionReason);
        }
        #endregion

        #region Agency
        [HttpGet("GetAgencyExceptedModel")]
        public async Task<QueryResult<AgencyExceptedModel>> GetAgencyExceptedModel(BlockSearchCriteriaModel blockSearchCriteriaModel)
        {
            return await _iDMAppService.GetAgencyExceptedModel(blockSearchCriteriaModel);
        }

        [HttpGet("GetAgencyExceptedById/{Id}")]
        public async Task<AgencyExceptedModel> GetAgencyExceptedById(string Id)
        {
            AgencyExceptedModel purchaseModel = await _iDMAppService.GetAgencyExceptedById(Id);
            if (string.IsNullOrEmpty(purchaseModel.PurchaseMethodString))
                return purchaseModel;
            purchaseModel.PurchaseMethods = purchaseModel.PurchaseMethodString.Split(',').ToList().Select(r => int.Parse(r)).ToList();
            foreach (PurchaseMethodsModel tm in purchaseModel.TenderTypes)
            {
                foreach (int pm in purchaseModel.PurchaseMethods)
                {
                    if (tm.PurchaseMethodId == pm)
                    {
                        tm.IsSelected = true;
                    }
                }
            }

            return purchaseModel;
        }

        [HttpPost("SaveAgency")]
        public async Task<bool> SaveAgency([FromBody] AgencyExceptedModel agencyExceptedModel)
        {
            return await _iDMAppService.SaveAgency(agencyExceptedModel);
        }

        #endregion

        #region Create Verification Code

        [Authorize(Roles = RoleNames.MonafasatBlackListCommittee + "," + RoleNames.OffersOppeningManager + "," + RoleNames.PreQualificationCommitteeManager + "," + RoleNames.OffersCheckManager)]
        [HttpPost]
        [Route("CreateVerificationCode/{PreQualificationID}")]
        public async Task CreateVerificationCode(int PreQualificationID)
        {
            int typeId = (int)Enums.VerificationType.Block;
            var userEmail = User.Email();
            var userMobile = User.Mobile();
            await _verification.CreateVerificationCode(PreQualificationID, userMobile, userEmail, User.UserId(), typeId);
        }

        [HttpPost]
        [Route("UpdateAgencyStatus/{AgencyId}/{IsExcepted}")]
        public async Task UpdateAgencyStatus(string AgencyId, bool IsExcepted)
        {
            await _iDMAppService.UpdateAgencyStatus(AgencyId, IsExcepted);
        }

        [HttpPost]
        [Route("UpdateAgencyIsOld/{AgencyId}/{IsOld}")]
        public async Task UpdateAgencyIsOld(string AgencyId, bool IsOld)
        {
            await _iDMAppService.UpdateAgencyIsOld(AgencyId, IsOld);
        }
        #endregion


        [HttpGet("DownloadSupplierBlockedAccordingAgency")]
        public async Task<byte[]> DownloadSupplierBlockedAccordingAgency(BlockSearchCriteria supplierSearchCretriaModel)
        {
            supplierSearchCretriaModel.PageSize = 999;
            byte[] fileContents = null;
            BlockSearchCriteria blockSearchCriteria = new BlockSearchCriteria
            {
                CommercialRegistrationNo = supplierSearchCretriaModel.CommercialRegistrationNo,
                CommercialSupplierName = supplierSearchCretriaModel.CommercialSupplierName,
                PageNumber = supplierSearchCretriaModel.PageNumber,
                PageSize = supplierSearchCretriaModel.PageSize,
                IsDeletedId = supplierSearchCretriaModel.IsDeletedId,
                BlockStatusId = supplierSearchCretriaModel.BlockStatusId,
                FromDate = supplierSearchCretriaModel.FromDate,
                ToDate = supplierSearchCretriaModel.ToDate,
                ResolutionNumber = supplierSearchCretriaModel.ResolutionNumber,
                AgencyCode = User.UserAgency()
            };
            var result = await _blockService.GetAdminBlockingListDetails(blockSearchCriteria);
            if (result != null)
            {
                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add("Report");
                    worksheet.View.RightToLeft = true;
                    int rowNum = 1;
                    worksheet.Cells[rowNum, 1].Value = Resources.BlockResources.DisplayInputs.CommercialSupplierName;
                    worksheet.Cells[rowNum, 1].Style.Font.Size = 12;
                    worksheet.Cells[rowNum, 1].Style.Font.Bold = true;
                    worksheet.Cells[rowNum, 1].Style.Border.Top.Style = ExcelBorderStyle.Hair;
                    worksheet.Cells[rowNum, 2].Value = Resources.BlockResources.DisplayInputs.CommercialRegistrationNo;
                    worksheet.Cells[rowNum, 2].Style.Font.Size = 12;
                    worksheet.Cells[rowNum, 2].Style.Font.Bold = true;
                    worksheet.Cells[rowNum, 2].Style.Border.Top.Style = ExcelBorderStyle.Hair;
                    worksheet.Cells[rowNum, 3].Value = Resources.BlockResources.DisplayInputs.BlockReason;
                    worksheet.Cells[rowNum, 3].Style.Font.Size = 12;
                    worksheet.Cells[rowNum, 3].Style.Font.Bold = true;
                    worksheet.Cells[rowNum, 3].Style.Border.Top.Style = ExcelBorderStyle.Hair;
                    worksheet.Cells[rowNum, 4].Value = Resources.PrePlanningResources.DisplayInputs.Duration;
                    worksheet.Cells[rowNum, 4].Style.Font.Size = 12;
                    worksheet.Cells[rowNum, 4].Style.Font.Bold = true;
                    worksheet.Cells[rowNum, 4].Style.Border.Top.Style = ExcelBorderStyle.Hair;
                    worksheet.Cells[rowNum, 5].Value = Resources.BlockResources.DisplayInputs.BlockStartDate;
                    worksheet.Cells[rowNum, 5].Style.Font.Size = 12;
                    worksheet.Cells[rowNum, 5].Style.Font.Bold = true;
                    worksheet.Cells[rowNum, 5].Style.Border.Top.Style = ExcelBorderStyle.Hair;
                    worksheet.Cells[rowNum, 6].Value = Resources.BlockResources.DisplayInputs.BlockEndDate;
                    worksheet.Cells[rowNum, 6].Style.Font.Size = 12;
                    worksheet.Cells[rowNum, 6].Style.Font.Bold = true;
                    worksheet.Cells[rowNum, 6].Style.Border.Top.Style = ExcelBorderStyle.Hair;
                    worksheet.Cells[rowNum, 7].Value = Resources.TenderResources.DisplayInputs.Status;
                    worksheet.Cells[rowNum, 7].Style.Font.Size = 12;
                    worksheet.Cells[rowNum, 7].Style.Font.Bold = true;
                    worksheet.Cells[rowNum, 7].Style.Border.Top.Style = ExcelBorderStyle.Hair;
                    if (result.Items.Count() > 0)
                    {
                        foreach (var blockItem in result.Items)
                        {
                            rowNum++;
                            worksheet.Cells[rowNum, 1].Value = blockItem.CommercialSupplierName;
                            worksheet.Cells[rowNum, 2].Value = blockItem.CommercialRegistrationNo;
                            worksheet.Cells[rowNum, 3].Value = blockItem.BlockDetails;
                            worksheet.Cells[rowNum, 4].Value = blockItem.BlockDuration;
                            worksheet.Cells[rowNum, 5].Value = blockItem.FromDateString;
                            worksheet.Cells[rowNum, 6].Value = blockItem.ToDateString;
                            worksheet.Cells[rowNum, 7].Value = blockItem.BlockStatusName;
                        }
                    }
                    fileContents = package.GetAsByteArray();
                }
            }
            return fileContents;
        }

        [HttpGet("DownloadSupplierBlocked")]
        public async Task<byte[]> DownloadSupplierBlocked(BlockSearchCriteria supplierSearchCretriaModel)
        {
            supplierSearchCretriaModel.PageSize = 999;
            byte[] fileContents = null;
            BlockSearchCriteria blockSearchCriteria = new BlockSearchCriteria
            {
                CommercialRegistrationNo = supplierSearchCretriaModel.CommercialRegistrationNo,
                CommercialSupplierName = supplierSearchCretriaModel.CommercialSupplierName,
                PageNumber = supplierSearchCretriaModel.PageNumber,
                PageSize = supplierSearchCretriaModel.PageSize,
                IsDeletedId = supplierSearchCretriaModel.IsDeletedId,
                BlockStatusId = supplierSearchCretriaModel.BlockStatusId,
                FromDate = supplierSearchCretriaModel.FromDate,
                ToDate = supplierSearchCretriaModel.ToDate,
                ResolutionNumber = supplierSearchCretriaModel.ResolutionNumber
            };
            var result = await _blockService.GetBlockingListDetails(blockSearchCriteria);
            if (result != null)
            {
                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add("Report");
                    worksheet.View.RightToLeft = true;
                    int rowNum = 1;
                    worksheet.Cells[rowNum, 1].Value = Resources.BlockResources.DisplayInputs.CommercialSupplierName;
                    worksheet.Cells[rowNum, 1].Style.Font.Size = 12;
                    worksheet.Cells[rowNum, 1].Style.Font.Bold = true;
                    worksheet.Cells[rowNum, 1].Style.Border.Top.Style = ExcelBorderStyle.Hair;
                    worksheet.Cells[rowNum, 2].Value = Resources.BlockResources.DisplayInputs.CommercialRegistrationNo;
                    worksheet.Cells[rowNum, 2].Style.Font.Size = 12;
                    worksheet.Cells[rowNum, 2].Style.Font.Bold = true;
                    worksheet.Cells[rowNum, 2].Style.Border.Top.Style = ExcelBorderStyle.Hair;
                    worksheet.Cells[rowNum, 3].Value = Resources.BlockResources.DisplayInputs.BlockReason;
                    worksheet.Cells[rowNum, 3].Style.Font.Size = 12;
                    worksheet.Cells[rowNum, 3].Style.Font.Bold = true;
                    worksheet.Cells[rowNum, 3].Style.Border.Top.Style = ExcelBorderStyle.Hair;
                    worksheet.Cells[rowNum, 4].Value = Resources.PrePlanningResources.DisplayInputs.Duration;
                    worksheet.Cells[rowNum, 4].Style.Font.Size = 12;
                    worksheet.Cells[rowNum, 4].Style.Font.Bold = true;
                    worksheet.Cells[rowNum, 4].Style.Border.Top.Style = ExcelBorderStyle.Hair;
                    worksheet.Cells[rowNum, 5].Value = Resources.BlockResources.DisplayInputs.BlockStartDate;
                    worksheet.Cells[rowNum, 5].Style.Font.Size = 12;
                    worksheet.Cells[rowNum, 5].Style.Font.Bold = true;
                    worksheet.Cells[rowNum, 5].Style.Border.Top.Style = ExcelBorderStyle.Hair;
                    worksheet.Cells[rowNum, 6].Value = Resources.BlockResources.DisplayInputs.BlockEndDate;
                    worksheet.Cells[rowNum, 6].Style.Font.Size = 12;
                    worksheet.Cells[rowNum, 6].Style.Font.Bold = true;
                    worksheet.Cells[rowNum, 6].Style.Border.Top.Style = ExcelBorderStyle.Hair;
                    worksheet.Cells[rowNum, 7].Value = Resources.TenderResources.DisplayInputs.Status;
                    worksheet.Cells[rowNum, 7].Style.Font.Size = 12;
                    worksheet.Cells[rowNum, 7].Style.Font.Bold = true;
                    worksheet.Cells[rowNum, 7].Style.Border.Top.Style = ExcelBorderStyle.Hair;
                    if (result.Items.Count() > 0)
                    {
                        foreach (var blockItem in result.Items)
                        {
                            rowNum++;
                            worksheet.Cells[rowNum, 1].Value = blockItem.CommercialSupplierName;
                            worksheet.Cells[rowNum, 2].Value = blockItem.CommercialRegistrationNo;
                            worksheet.Cells[rowNum, 3].Value = blockItem.BlockDetails;
                            worksheet.Cells[rowNum, 4].Value = blockItem.BlockDuration;
                            worksheet.Cells[rowNum, 5].Value = blockItem.FromDateString;
                            worksheet.Cells[rowNum, 6].Value = blockItem.ToDateString;
                            worksheet.Cells[rowNum, 7].Value = blockItem.BlockStatusName;
                        }
                    }
                    fileContents = package.GetAsByteArray();
                }
            }
            return fileContents;
        }
    }
}

