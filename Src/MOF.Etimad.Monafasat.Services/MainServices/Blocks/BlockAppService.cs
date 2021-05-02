using AutoMapper;
using MOF.Etimad.Monafasat.Core;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Core.Interfaces;
using MOF.Etimad.Monafasat.Data;
using MOF.Etimad.Monafasat.Integration;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernal.Exceptions;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static MOF.Etimad.Monafasat.SharedKernel.Enums;

namespace MOF.Etimad.Monafasat.Services
{
    public class BlockAppService : IBlockAppService
    {
        private readonly INotificationAppService _notificationAppService;
        private readonly IBlockQueries _blockQueries;
        private readonly IBlockCommands _blockCommands;
        private readonly IGenericCommandRepository _genericrepository;
        private readonly IIDMQueries _iDMQueries;
        private readonly IIDMAppService _iDMAppService;
        private readonly IVerificationService _verification;
        private readonly IMapper _mapper;
        private readonly ISupplierQueries _supplierQueries;

        public BlockAppService(IAppDbContext context, INotificationAppService notificationAppService, IMapper mapper, IIDMAppService iDMAppService, IBlockQueries blockQueries, IBlockCommands blockCommands, IGenericCommandRepository genericrepository, IYasserProxy yesserProxy, ISupplierQueries supplierQueries, IIDMQueries iDMQueries, IVerificationService verification, INotificationQueries notificationQueries)
        {
            _notificationAppService = notificationAppService;
            _blockCommands = blockCommands;
            _blockQueries = blockQueries;
            _genericrepository = genericrepository;
            _mapper = mapper;
            _supplierQueries = supplierQueries;
            _iDMAppService = iDMAppService;
            _verification = verification;
            _iDMQueries = iDMQueries;
        }

        #region Queries =========================================

        public Task<SupplierBlock> FindBlockByIdAsync(int supplierBlockId)
        {
            var result = _blockQueries.FindBlockByIdAsync(supplierBlockId);
            return result;
        }

        public Task<QueryResult<SupplierBlockModel>> FindAsyn(BlockSearchCriteria criteria)
        {
            var result = _blockQueries.Find(criteria);
            return result;
        }

        public async Task<List<SupplierAgencyBlockModel>> GetAllSupplierBlocks(string agencyCode, List<string> CRs)
        {
            return await _blockQueries.GetAllCurrentBlockedSuppliers(agencyCode, CRs);
        }
        public async Task<List<SupplierAgencyBlockModel>> GetAllBlockedSuppliers(string agencyCode, List<string> CRs)
        {
            return await _blockQueries.GetAllBlockedSuppliers(agencyCode, CRs);
        }

        public Task<bool> CheckifSupplierBlockedByCrNo(string commericalRegisterNo, string AgencyCode = null)
        {
            return _blockQueries.CheckifSupplierBlockedByCrNo(commericalRegisterNo, AgencyCode);
        }
        #endregion

        #region Commands======================================

        public async Task<SupplierBlock> AddBlockAsyn(SupplierBlock block)
        {
            var supplier = await _supplierQueries.FindSupplierByCRNumber(block.CommercialRegistrationNo);
            if (supplier == null)
            {
                var supplierobj = new Supplier(block.CommercialRegistrationNo, block.CommercialSupplierName, await _notificationAppService.GetDefaultSettingByCr());
                await _genericrepository.CreateAsync(supplierobj);
            }
            else
            {
                List<UserNotificationSetting> defaultNotificationSettings = await _notificationAppService.GetDefaultSettingByCr();
                if (supplier.NotificationSetting.Count < defaultNotificationSettings.Count)
                {
                    supplier.AddNotificationSettings(defaultNotificationSettings);
                    _genericrepository.Update(supplier);
                }
            }

            if (block.BlockTypeId == 1)
            {
                if (block.BlockStartDate != null && block.BlockStartDate != DateTime.MinValue && block.BlockStartDate < DateTime.Now)
                    throw new BusinessRuleException(Resources.BlockResources.ErrorMessages.PermanentDateValidate);
            }
            else
            {
                if (block.BlockEndDate != null && block.BlockEndDate != DateTime.MinValue && block.BlockEndDate < DateTime.Now)
                    throw new BusinessRuleException(Resources.BlockResources.ErrorMessages.DateValidation);
            }
            QueryResult<SupplierBlockModel> supplierBlock = await _blockQueries.FindBlockedUser(new BlockSearchCriteria { AgencyCode = block.AgencyCode, CommercialRegistrationNo = block.CommercialRegistrationNo, BlockStatusId = (int)IsActive.Active });
            if (supplierBlock != null && supplierBlock.TotalCount > 0)
            {
                if (supplierBlock.Items.FirstOrDefault().BlockStatusId == (int)Enums.BlockStatus.NewAdmin || supplierBlock.Items.FirstOrDefault().BlockStatusId == (int)Enums.BlockStatus.NewSecretary || supplierBlock.Items.FirstOrDefault().BlockStatusId == (int)Enums.BlockStatus.ApprovedSecertary)
                {
                    throw new BusinessRuleException(Resources.BlockResources.Messages.BlockPendingRequest);
                }
                else
                {
                    throw new BusinessRuleException(Resources.BlockResources.ErrorMessages.AlreadyBlocked);
                }

            }
            Check.ArgumentNotNull(nameof(SupplierBlock), block);
            var res = await _genericrepository.CreateAsync(block);
            await _genericrepository.SaveAsync();

            NotificationArguments NotificationArguments = new NotificationArguments
            {
                BodyEmailArgs = new object[] { block.CommercialRegistrationNo, block.CommercialSupplierName },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { block.CommercialRegistrationNo, block.CommercialSupplierName },
                SMSArgs = new object[] { block.CommercialRegistrationNo, block.CommercialSupplierName }
            };

            NotificationArguments NotificationArgumentsForSupplier = new NotificationArguments
            {
                BodyEmailArgs = new object[] { block.CommercialSupplierName },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { block.CommercialSupplierName },
                SMSArgs = new object[] { block.CommercialSupplierName }
            };
            MainNotificationTemplateModel mainNotificationTemplateModelForSecretary = new MainNotificationTemplateModel(NotificationArguments, $"Block/SecretaryBlockDetails?Id={Util.Encrypt(block.SupplierBlockId)}", NotificationEntityType.Block, block.SupplierBlockId.ToString(), 0);
            MainNotificationTemplateModel mainNotificationTemplateModelForSupplier = new MainNotificationTemplateModel(NotificationArgumentsForSupplier, $"Block/BlockDetailsAsync?Id={Util.Encrypt(block.SupplierBlockId)}", NotificationEntityType.Block, block.SupplierBlockId.ToString(), 0);
            MainNotificationTemplateModel mainNotificationTemplateModelForAccountMangerSpecialist = new MainNotificationTemplateModel(NotificationArguments, $"Block/BlockDetailsAsync?Id={Util.Encrypt(block.SupplierBlockId)}", NotificationEntityType.Block, block.SupplierBlockId.ToString(), 0);
            await _notificationAppService.SendNotificationForSuppliers(NotificationOperations.BlockManager.OperationsToBeApproved.sendApprovedManagerApprovedBlocked, new List<string> { block.CommercialRegistrationNo }, mainNotificationTemplateModelForSupplier);
            await _notificationAppService.SendNotificationForUsersByRoleName(NotificationOperations.BlockSecrtary.OperationsToBeApproved.ApproveBlockFromManagerToSecretary, RoleNames.MonafasatBlockListSecritary, mainNotificationTemplateModelForSecretary);
            await _notificationAppService.SendNotificationForUsersByRoleName(NotificationOperations.BlockManager.OperationsToBeApproved.AppoveBlockFromMangerToAccountMangerSpecialist, RoleNames.AccountsManagementSpecialist, mainNotificationTemplateModelForAccountMangerSpecialist);
            return res;
        }


        public async Task<SupplierBlock> UpdateBlockAsync(SupplierBlockModel blockModel)
        {
            Check.ArgumentNotNull(nameof(SupplierBlockModel), blockModel);
            var block = await FindBlockByIdAsync(blockModel.SupplierBlockId);
            Check.ArgumentNotNull(nameof(SupplierBlock), block);
            block.UpdateBlock(blockModel.CommercialRegistrationNo, blockModel.CommercialSupplierName, blockModel.ResolutionNumber, blockModel.BlockStatusId, blockModel.AgencyCode, blockModel.BlockStartDate, blockModel.BlockEndDate, blockModel.Punishment, blockModel.AdminFileName, blockModel.AdminFileNetReferenceId, blockModel.SecretaryFileName, blockModel.SecretaryFileNetReferenceId, blockModel.AdminBlockReason, blockModel.SecretaryBlockReason);
            return await _blockCommands.UpdateAsync(block);
        }

        public async Task<SupplierBlock> DeactivateBlockAsyn(int blockId)
        {
            var block = await FindBlockByIdAsync(blockId);
            Check.ArgumentNotNull(nameof(block), block);
            block.DeActive();
            #region [Send Notification]

            #endregion

            return await _blockCommands.UpdateAsync(block);
        }


        public Task<QueryResult<BlockCommitteeModel>> FindBlockCommittee(BlockCommitteeSearchCriteria criteria)
        {
            return _blockQueries.FindBlockCommittee(criteria);
        }

        public async Task<QueryResult<UserProfileModel>> FindListUsers(BlockCommitteeSearchCriteria criteria)
        {
            var searchCriteria = new UsersSearchCriteriaModel();
            searchCriteria.Email = criteria.Email;
            searchCriteria.Name = criteria.EmployeeName;
            searchCriteria.MobileNumber = criteria.PhoneNumber;
            searchCriteria.PageNumber = criteria.PageNumber;
            searchCriteria.PageSize = criteria.PageSize;
            searchCriteria.AgencyId = criteria.AgencyId;
            searchCriteria.isGovVendor = criteria.isGovVendor;
            searchCriteria.isSemiGovAgency = criteria.isSemiGovAgency;
            searchCriteria.AgencyType = (int)criteria.AgencyType;
            searchCriteria.NationalId = criteria.CommercialRegistrationNo;
            if (!string.IsNullOrEmpty(searchCriteria.NationalId))
            {
                searchCriteria.NationalIds.Add(searchCriteria.NationalId);
            }
            searchCriteria.RoleNames = new List<string>
            {
                RoleNames.MonafasatBlockListSecritary,RoleNames.MonafasatBlackListCommittee
            };

            var idmUsers = await _iDMAppService.GetMonafasatUsersForBlockUserList(searchCriteria);
            var Users = _mapper.Map<QueryResult<UserProfileModel>>(idmUsers);
            return Users;
        }
        #endregion
        #region Queries  =========================================

        public Task<SupplierBlockModel> FindBlockById(int supplierBlockId)
        {
            var result = _blockQueries.FindBlockById(supplierBlockId);
            return result;
        }

        public SupplierBlock FindSupplierBlockById(int supplierBlockId)
        {
            var result = _blockQueries.FindSupplierBlockById(supplierBlockId);
            return result;
        }
        #endregion

        #region Commands======================================


        #region New Update
        public async Task<QueryResult<SupplierIntegrationModel>> GetAllSuppliers(SupplierIntegrationSearchCriteria searchCriteria)
        {
            var supplierList = await _iDMAppService.GetSupplierDetailsBySearchCriteria(searchCriteria);

            return supplierList;
        }

        public Task<QueryResult<SupplierBlockModel>> GetAdminBlockList(BlockSearchCriteria searchCriteria)
        {

            var result = _blockQueries.FindAdminBlockList(searchCriteria);
            return result;
        }

        #endregion

        public async Task<SupplierBlock> AddAdminBlock(SupplierBlockModel blockModel)
        {
            SupplierBlock block = new SupplierBlock(blockModel.CommercialRegistrationNo, blockModel.CommercialSupplierName, blockModel.ResolutionNumber, blockModel.BlockStatusId, 2, blockModel.AgencyCode, blockModel.BlockStartDate, blockModel.BlockEndDate, blockModel.Punishment, blockModel.BlockDetails, blockModel.AdminFileName, blockModel.AdminFileNetReferenceId, blockModel.SecretaryFileName, blockModel.SecretaryFileNetReferenceId, blockModel.AdminBlockReason, blockModel.SecretaryBlockReason, blockModel.SupplierTypeId, blockModel.OrganizationTypeId, blockModel.CommercialRegistrationNo, blockModel.CommercialRegistrationNo, false);
            string crNumber = blockModel.CommercialRegistrationNo;
            var supplier = await _supplierQueries.FindSupplierByCRNumber(crNumber);
            if (supplier == null)
            {
                Supplier supplierobj = new Supplier(crNumber, blockModel.CommercialSupplierName, await _notificationAppService.GetDefaultSettingByCr());
                await _genericrepository.CreateAsync<Supplier>(supplierobj);
            }
            else
            {
                List<UserNotificationSetting> defaultNotificationSettings = await _notificationAppService.GetDefaultSettingByCr();
                supplier.AddNotificationSettings(defaultNotificationSettings);
                _genericrepository.Update(supplier);
            }

            QueryResult<SupplierBlockModel> supplierBlock = await _blockQueries.FindBlockedUser(new BlockSearchCriteria { AgencyCode = block.AgencyCode, CommercialRegistrationNo = block.CommercialRegistrationNo, EnabledSearchByDate = blockModel.EnabledSearchByDate });
            if (supplierBlock != null && supplierBlock.TotalCount > 0)
            {
                if (supplierBlock.Items.FirstOrDefault().BlockStatusId == (int)Enums.BlockStatus.NewAdmin || supplierBlock.Items.FirstOrDefault().BlockStatusId == (int)Enums.BlockStatus.NewSecretary || supplierBlock.Items.FirstOrDefault().BlockStatusId == (int)Enums.BlockStatus.ApprovedSecertary)
                {
                    throw new BusinessRuleException(Resources.BlockResources.Messages.BlockPendingRequest);
                }
                else
                {
                    throw new BusinessRuleException(Resources.BlockResources.ErrorMessages.AlreadyBlocked);
                }

            }
            Check.ArgumentNotNull(nameof(SupplierBlock), block);
            var res = await _genericrepository.CreateAsync(block);
            await _genericrepository.SaveAsync();

            NotificationArguments NotificationArguments = new NotificationArguments
            {
                BodyEmailArgs = new object[] { blockModel.CommercialSupplierName },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { blockModel.CommercialSupplierName },
                SMSArgs = new object[] { blockModel.CommercialSupplierName }
            };

            if (blockModel.IsSecretaryNotify)
            {
                MainNotificationTemplateModel mainNotificationTemplateModelForManger = new MainNotificationTemplateModel(NotificationArguments,
                          $"Block/ManagerBlockDetails?Id={Util.Encrypt(block.SupplierBlockId)}", NotificationEntityType.Block, blockModel.SupplierBlockId.ToString(), 0);
                await _notificationAppService.SendNotificationForUsersByRoleName(NotificationOperations.BlockManager.OperationsToBeApproved.ApproveBlockManager, RoleNames.MonafasatBlackListCommittee, mainNotificationTemplateModelForManger);
            }
            else
            {
                MainNotificationTemplateModel mainNotificationTemplateModelForSecretary = new MainNotificationTemplateModel(NotificationArguments,
                $"Block/SecretaryBlockDetails?Id={Util.Encrypt(block.SupplierBlockId)}", NotificationEntityType.Block, block.SupplierBlockId.ToString(), 0);
                await _notificationAppService.SendNotificationForUsersByRoleName(NotificationOperations.BlockMonafasatAdmin.OperationsRequest.SendRequestBlockFromMonafasatAdminToBlockSecrtary, RoleNames.MonafasatBlockListSecritary, mainNotificationTemplateModelForSecretary);

            }

            return res;
        }
        public async Task<SupplierBlock> AddSecretaryBlock(SupplierBlockModel blockModel)
        {
            blockModel.BlockStatusId = (int)Enums.BlockStatus.ApprovedSecertary;
            if (blockModel.BlockEndDate != null && blockModel.BlockEndDate != DateTime.MinValue && blockModel.BlockEndDate < DateTime.Now)
                throw new BusinessRuleException(Resources.BlockResources.ErrorMessages.DateValidation);

            if (string.IsNullOrEmpty(blockModel.AgencyCode))
                blockModel.AgencyCode = null;

            if (await FindBlockById(blockModel.SupplierBlockId) == null && !(string.IsNullOrEmpty(blockModel.AgencyCode)))
            {
                throw new BusinessRuleException("تم حذف هذا الطلب من مقدم الطلب . لا يمكن استكمال العملية ");
            }

            if (await FindBlockById(blockModel.SupplierBlockId) == null)
            {
                blockModel.IsSecretaryNotify = true;
                return await AddAdminBlock(blockModel);
            }
            else
            {
                NotificationArguments NotificationArguments = new NotificationArguments
                {
                    BodyEmailArgs = new object[] { blockModel.CommercialSupplierName },
                    SubjectEmailArgs = new object[] { },
                    PanelArgs = new object[] { blockModel.CommercialSupplierName },
                    SMSArgs = new object[] { blockModel.CommercialSupplierName }
                };
                MainNotificationTemplateModel mainNotificationTemplateModelForManger = new MainNotificationTemplateModel(NotificationArguments,
                                $"Block/ManagerBlockDetails?Id={Util.Encrypt(blockModel.SupplierBlockId)}", NotificationEntityType.Block, blockModel.SupplierBlockId.ToString(), 0);
                await _notificationAppService.SendNotificationForUsersByRoleName(NotificationOperations.BlockManager.OperationsToBeApproved.ApproveBlockManager, RoleNames.MonafasatBlackListCommittee, mainNotificationTemplateModelForManger);
                return await UpdateBlockAsync(blockModel);
            }
        }

        public async Task<SupplierBlock> ManagerRejectionReason(int supplierBlockId, string reason)
        {
            var block = FindSupplierBlockById(supplierBlockId);
            block.SetManagerRejectionReason(reason);
            SupplierBlock supplierBlock = await _blockCommands.UpdateAsync(block);
            if (supplierBlock != null)
            {
                NotificationArguments NotificationArguments = new NotificationArguments
                {
                    BodyEmailArgs = new object[] { block.CommercialRegistrationNo, block.CommercialSupplierName },
                    SubjectEmailArgs = new object[] { },
                    PanelArgs = new object[] { block.CommercialRegistrationNo, block.CommercialSupplierName },
                    SMSArgs = new object[] { block.CommercialRegistrationNo, block.CommercialSupplierName }
                };

                MainNotificationTemplateModel mainNotificationTemplateModelForSecretary = new MainNotificationTemplateModel(NotificationArguments,
                                  $"Block/SecretaryBlockDetails?Id={Util.Encrypt(block.SupplierBlockId)}", NotificationEntityType.Block, block.SupplierBlockId.ToString(), 0);

                MainNotificationTemplateModel mainNotificationTemplateModelForMonafasatAdmin = new MainNotificationTemplateModel(NotificationArguments,
                             $"Block/DetailsAsync?Id={Util.Encrypt(block.SupplierBlockId)}", NotificationEntityType.Block, block.SupplierBlockId.ToString(), 0);


                await _notificationAppService.SendNotificationForUsersByRoleName(NotificationOperations.BlockSecrtary.OperationsToBeApproved.sendRejectBlockToSecretary, RoleNames.MonafasatBlockListSecritary, mainNotificationTemplateModelForSecretary);
                if (!string.IsNullOrEmpty(block.AgencyCode))
                {
                    await _notificationAppService.SendNotificationForUsersByRoleNameAndAgency(NotificationOperations.BlockMonafasatAdmin.OperationsRequest.sendRejectBlockToMonafasatAdmin, RoleNames.MonafasatAdmin, mainNotificationTemplateModelForMonafasatAdmin, block.AgencyCode, block.Agency.CategoryId.Value, null);
                }
            }
            return supplierBlock;
        }

        public async Task<SupplierBlock> SecretaryRejectionReason(int supplierBlockId, string reason)
        {
            var block = FindSupplierBlockById(supplierBlockId);
            block.SetSecretaryRejectionReason(reason);
            SupplierBlock supplierBlock = await _blockCommands.UpdateAsync(block);

            NotificationArguments NotificationArguments = new NotificationArguments
            {
                BodyEmailArgs = new object[] { block.CommercialRegistrationNo, block.CommercialSupplierName },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { block.CommercialRegistrationNo, block.CommercialSupplierName },
                SMSArgs = new object[] { block.CommercialRegistrationNo, block.CommercialSupplierName }
            };

            MainNotificationTemplateModel mainNotificationTemplateModelForMonafasatAdmin = new MainNotificationTemplateModel(NotificationArguments,
                         $"Block/DetailsAsync?Id={Util.Encrypt(block.SupplierBlockId)}", NotificationEntityType.Block, block.SupplierBlockId.ToString(), 0);

            if (!string.IsNullOrEmpty(block.AgencyCode))
            {
                await _notificationAppService.SendNotificationForUsersByRoleNameAndAgency(NotificationOperations.BlockMonafasatAdmin.OperationsRequest.sendRejectBlockToMonafasatAdmin, RoleNames.MonafasatAdmin, mainNotificationTemplateModelForMonafasatAdmin, block.AgencyCode, block.Agency.CategoryId.Value, null);
            }
            return supplierBlock;
        }


        public async Task<SupplierBlock> ResetSupplier(int supplierBlockId)
        {
            var block = FindSupplierBlockById(supplierBlockId);
            block.SetStatus((int)Enums.BlockStatus.RemoveBlock);
            NotificationArguments NotificationArgumentsForSupplier = new NotificationArguments
            {
                BodyEmailArgs = new object[] { block.CommercialSupplierName },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { block.CommercialSupplierName },
                SMSArgs = new object[] { block.CommercialSupplierName }
            };

            MainNotificationTemplateModel mainNotificationTemplateModelForSupplier = new MainNotificationTemplateModel(NotificationArgumentsForSupplier,
                                        $"Block/BlockDetailsAsync?Id={Util.Encrypt(block.SupplierBlockId)}", NotificationEntityType.Block, block.SupplierBlockId.ToString(), 0);

            await _notificationAppService.SendNotificationForSuppliers(NotificationOperations.BlockManager.OperationsToBeApproved.sendApprovedManagerUnBlocked, new List<string> { block.CommercialRegistrationNo }, mainNotificationTemplateModelForSupplier);

            return await _blockCommands.UpdateAsync(block);
        }

        public async Task<SupplierBlock> ManagerApproval(int supplierBlockId, string verificationCode)
        {

            bool check = await _verification.CheckForVerificationCode(supplierBlockId, verificationCode, (int)Enums.VerificationType.Block);
            var block = FindSupplierBlockById(supplierBlockId);
            block.SetStatus((int)Enums.BlockStatus.ApprovedManager);
            if (check)
            {
                block.TotallyBlocked();
                NotificationArguments NotificationArguments = new NotificationArguments
                {
                    BodyEmailArgs = new object[] { block.CommercialRegistrationNo, block.CommercialSupplierName },
                    SubjectEmailArgs = new object[] { },
                    PanelArgs = new object[] { block.CommercialRegistrationNo, block.CommercialSupplierName },
                    SMSArgs = new object[] { block.CommercialRegistrationNo, block.CommercialSupplierName }
                };

                NotificationArguments NotificationArgumentsForSupplier = new NotificationArguments
                {
                    BodyEmailArgs = new object[] { block.CommercialSupplierName, block.ResolutionNumber, block.BlockStartDate.Value.ToShortDateString() },
                    SubjectEmailArgs = new object[] { },
                    PanelArgs = new object[] { block.CommercialSupplierName, block.ResolutionNumber, block.BlockStartDate.Value.ToShortDateString() },
                    SMSArgs = new object[] { block.CommercialSupplierName, block.ResolutionNumber, block.BlockStartDate.Value.ToShortDateString() }
                };
                MainNotificationTemplateModel mainNotificationTemplateModelForSecretary = new MainNotificationTemplateModel(NotificationArguments,
                                  $"Block/SecretaryBlockDetails?Id={Util.Encrypt(block.SupplierBlockId)}", NotificationEntityType.Block, block.SupplierBlockId.ToString(), 0);

                MainNotificationTemplateModel mainNotificationTemplateModelForMonafasatAdmin = new MainNotificationTemplateModel(NotificationArguments,
                             $"Block/DetailsAsync?Id={Util.Encrypt(block.SupplierBlockId)}", NotificationEntityType.Block, block.SupplierBlockId.ToString(), 0);

                MainNotificationTemplateModel mainNotificationTemplateModelForSupplier = new MainNotificationTemplateModel(NotificationArgumentsForSupplier,
                             $"Block/BlockDetailsAsync?Id={Util.Encrypt(block.SupplierBlockId)}", NotificationEntityType.Block, block.SupplierBlockId.ToString(), 0);

                MainNotificationTemplateModel mainNotificationTemplateModelForAccountMangerSpecialist = new MainNotificationTemplateModel(NotificationArguments,
                            $"Block/BlockDetailsAsync?Id={Util.Encrypt(block.SupplierBlockId)}", NotificationEntityType.Block, block.SupplierBlockId.ToString(), 0);

                if (!string.IsNullOrEmpty(block.AgencyCode))
                {
                    await _notificationAppService.SendNotificationForUsersByRoleNameAndAgency(NotificationOperations.BlockManager.OperationsToBeApproved.ApproveBlockFromManagerToMonafasatAdmin, RoleNames.MonafasatAdmin, mainNotificationTemplateModelForMonafasatAdmin, block.AgencyCode, block.Agency.CategoryId.Value, null);
                    await _notificationAppService.SendNotificationForUsersByRoleName(NotificationOperations.BlockManager.OperationsToBeApproved.AppoveBlockFromMangerToAccountMangerSpecialist, RoleNames.AccountsManagementSpecialist, mainNotificationTemplateModelForAccountMangerSpecialist);
                }
                await _notificationAppService.SendNotificationForSuppliers(NotificationOperations.BlockManager.OperationsToBeApproved.sendApprovedManagerApprovedBlocked, new List<string> { block.CommercialRegistrationNo }, mainNotificationTemplateModelForSupplier);
                await _notificationAppService.SendNotificationForUsersByRoleName(NotificationOperations.BlockSecrtary.OperationsToBeApproved.ApproveBlockFromManagerToSecretary, RoleNames.MonafasatBlockListSecritary, mainNotificationTemplateModelForSecretary);
            }

            SupplierBlock supplierBlock = await _blockCommands.UpdateAsync(block);
            if (supplierBlock != null)
            {
                await _blockQueries.GetAgencyById(block.AgencyCode);
            }
            return supplierBlock;
        }

        public async Task<SupplierBlock> SecretaryApproval(int supplierBlockId)
        {
            var block = FindSupplierBlockById(supplierBlockId);
            block.SetStatus((int)Enums.BlockStatus.ApprovedSecertary);
            #region [Send Notification]
            NotificationArguments NotificationArguments = new NotificationArguments
            {
                BodyEmailArgs = new object[] { block.CommercialSupplierName },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { block.CommercialSupplierName },
                SMSArgs = new object[] { block.CommercialSupplierName }
            };
            #endregion
            MainNotificationTemplateModel mainNotificationTemplateModel = new MainNotificationTemplateModel(NotificationArguments,
                          $"Block/ManagerBlockDetails?Id={Util.Encrypt(block.SupplierBlockId)}",
                          NotificationEntityType.Block,
                          block.SupplierBlockId.ToString(),
                          0);
            await _notificationAppService.SendNotificationForUsersByRoleName(NotificationOperations.BlockManager.OperationsToBeApproved.ApproveBlockManager, RoleNames.MonafasatBlackListCommittee, mainNotificationTemplateModel);
            return await _blockCommands.UpdateAsync(block);
        }

        public Task<QueryResult<SupplierBlockModel>> GetManagerBlockList(BlockSearchCriteria searchCriteria)
        {
            var result = _blockQueries.FindManagerBlockList(searchCriteria);
            return result;
        }

        public Task<QueryResult<SupplierBlockModel>> GetSecretaryBlockList(BlockSearchCriteria searchCriteria)
        {
            var result = _blockQueries.FindSecretaryBlockList(searchCriteria);
            return result;
        }

        public Task<QueryResult<SupplierBlockModel>> GetBlockingListDetails(BlockSearchCriteria searchCriteria)
        {
            var result = _blockQueries.GetBlockingListDetails(searchCriteria);
            return result;
        }
        public Task<QueryResult<SupplierBlockModel>> GetAdminBlockingListDetails(BlockSearchCriteria searchCriteria)
        {
            var result = _blockQueries.GetAdminBlockingListDetails(searchCriteria);
            return result;
        }

        public async Task<GovAgency> GetAgency(string agencyCode)
        {
            return await _iDMQueries.FindGovAgencyByIdAsync(agencyCode);
        }
        #endregion
    }
}
