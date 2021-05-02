using AutoMapper;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;
using static MOF.Etimad.Monafasat.SharedKernel.Enums;

namespace MOF.Etimad.Monafasat.Services
{
    public class MandatoryListAppService : IMandatoryListAppService
    {
        private readonly IMandatoryListQueries _mandatoryListQueries;
        private readonly IMandatoryListCommands _mandatoryListCommands;
        private readonly IMapper _mapper;
        private readonly INotificationAppService _notificationAppService;
        private readonly IVerificationService _verification;



        public MandatoryListAppService(IMandatoryListQueries mandatoryListQueries, IMandatoryListCommands mandatoryListCommands, IMapper mapper, INotificationAppService notificationAppService, IVerificationService verification/*, IMandatoryListDomainService mandatoryListDomainService*/)
        {
            _mandatoryListQueries = mandatoryListQueries;
            _mandatoryListCommands = mandatoryListCommands;
            _notificationAppService = notificationAppService;
            _mapper = mapper;
            _verification = verification;

        }

        public async Task<MandatoryListViewModel> Add(InputMandatoryListViewModel mandatoryList)
        {
            var entity = _mapper.Map<MandatoryList>(mandatoryList);
            entity.Add();
            await _mandatoryListCommands.Add(entity);
            return _mapper.Map<MandatoryListViewModel>(entity);
        }

        public async Task Update(InputMandatoryListViewModel mandatoryList)
        {
            var entity = _mapper.Map<MandatoryList>(mandatoryList);
            var dbEntity = await _mandatoryListQueries.Find(entity.Id);

            if (dbEntity.StatusId == (int)Enums.MandatoryListStatus.Approved)
            {
                await AddChangeRequest(entity, dbEntity);
            }
            else
            {
                dbEntity.Update(entity);
                await _mandatoryListCommands.Update(dbEntity);
            }
        }

        private async Task AddChangeRequest(MandatoryList entity, MandatoryList dbEntity)
        {
            var changeRequest = _mapper.Map<MandatoryListChangeRequest>(entity);
            dbEntity.AddChangeRequest(changeRequest);
            await _mandatoryListCommands.Update(dbEntity);
            NotificationArguments NotificationArguments = new NotificationArguments
            {
                BodyEmailArgs = new object[] { "", entity.DivisionNameAr, entity.DivisionCode },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { entity.DivisionNameAr, entity.DivisionCode },
                SMSArgs = new object[] { entity.DivisionNameAr, entity.DivisionCode }
            };

            MainNotificationTemplateModel mainNotificationTemplateModel = new MainNotificationTemplateModel(NotificationArguments,
              $"MandatoryList/Details?id={Util.Encrypt(entity.Id)}",
              NotificationEntityType.MandatoryList,
              entity.Id.ToString());

            await _notificationAppService.SendNotificationForUsersByRoleName(NotificationOperations.MandatoryListApprover.MandatoryListProducts.SendUpdateMandatoryListToApprove, RoleNames.MandatoryListApprover, mainNotificationTemplateModel);

        }

        public Task<MandatoryListViewModel> Find(int id)
        {
            return _mandatoryListQueries.ProjectedFind(id);
        }

        public async Task<InputMandatoryListViewModel> FindForEdit(int id)
        {
            var viewModel = await _mandatoryListQueries.ProjectedFind(id);
            return _mapper.Map<InputMandatoryListViewModel>(viewModel);
        }

        public Task<QueryResult<MandatoryListIndexViewModel>> Search(MandatoryListSearchViewModel criteria)
        {
            return _mandatoryListQueries.Search(criteria);
        }

        public async Task SendToApproval(int id)
        {
            var entity = await _mandatoryListQueries.Find(id);
            entity.SendToApproval();

            NotificationArguments NotificationArguments = new NotificationArguments
            {
                BodyEmailArgs = new object[] { "", entity.DivisionNameAr, entity.DivisionCode },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { entity.DivisionNameAr, entity.DivisionCode },
                SMSArgs = new object[] { entity.DivisionNameAr, entity.DivisionCode }
            };

            MainNotificationTemplateModel mainNotificationTemplateModel = new MainNotificationTemplateModel(NotificationArguments,
              $"MandatoryList/Approval?encryptedMandatoryListId={Util.Encrypt(id)}",
              NotificationEntityType.MandatoryList,
              id.ToString());

            await _notificationAppService.SendNotificationForUsersByRoleName(NotificationOperations.MandatoryListApprover.MandatoryListProducts.SendMandatoryListToApprove, RoleNames.MandatoryListApprover, mainNotificationTemplateModel);

            await _mandatoryListCommands.Update(entity);
        }

        public async Task Approve(int id, string verificationCode)
        {
            await _verification.CheckForVerificationCode(id, verificationCode, (int)Enums.VerificationType.MandatoryList);
            var entity = await _mandatoryListQueries.Find(id);
            entity.Approve();
            NotificationArguments NotificationArguments = new NotificationArguments
            {
                BodyEmailArgs = new object[] { "", entity.DivisionNameAr, entity.DivisionCode },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { entity.DivisionNameAr, entity.DivisionCode },
                SMSArgs = new object[] { entity.DivisionNameAr, entity.DivisionCode }
            };

            MainNotificationTemplateModel mainNotificationTemplateModel = new MainNotificationTemplateModel(NotificationArguments,
              $"MandatoryList/Details?id={Util.Encrypt(id)}",
              NotificationEntityType.MandatoryList,
              id.ToString());

            await _notificationAppService.SendNotificationForUsersByRoleName(NotificationOperations.MandatoryListOfficer.MandatoryListProducts.ApproveMandatoryList, RoleNames.MandatoryListOfficer, mainNotificationTemplateModel);
            await _mandatoryListCommands.Update(entity);
        }

        public async Task Reject(MandatoryListRejectionViewModel viewModel)
        {
            var entity = await _mandatoryListQueries.Find(Util.Decrypt(viewModel.Id));
            entity.Reject(viewModel.RejectionReason);

            NotificationArguments NotificationArguments = new NotificationArguments
            {
                BodyEmailArgs = new object[] { "", entity.DivisionNameAr, entity.DivisionCode },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { entity.DivisionNameAr, entity.DivisionCode },
                SMSArgs = new object[] { entity.DivisionNameAr, entity.DivisionCode }
            };

            MainNotificationTemplateModel mainNotificationTemplateModel = new MainNotificationTemplateModel(NotificationArguments,
              $"MandatoryList/SendToApproval?encryptedMandatoryListId={Util.Encrypt(entity.Id)}",
              NotificationEntityType.MandatoryList,
              viewModel.Id.ToString());

            await _notificationAppService.SendNotificationForUsersByRoleName(NotificationOperations.MandatoryListOfficer.MandatoryListProducts.RejectMandatoryList, RoleNames.MandatoryListOfficer, mainNotificationTemplateModel);

            await _mandatoryListCommands.Update(entity);
        }

        public async Task Reopen(int id)
        {
            var entity = await _mandatoryListQueries.Find(id);
            entity.Reopen();
            await _mandatoryListCommands.Update(entity);
        }

        public async Task Delete(int id)
        {
            var entity = await _mandatoryListQueries.Find(id);
            entity.Delete();
            await _mandatoryListCommands.Update(entity);
        }

        public async Task RequestDelete(int id)
        {
            var entity = await _mandatoryListQueries.Find(id);
            entity.RequestDelete();

            NotificationArguments NotificationArguments = new NotificationArguments
            {
                BodyEmailArgs = new object[] { "", entity.DivisionNameAr, entity.DivisionCode },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { entity.DivisionNameAr, entity.DivisionCode },
                SMSArgs = new object[] { entity.DivisionNameAr, entity.DivisionCode }
            };

            MainNotificationTemplateModel mainNotificationTemplateModel = new MainNotificationTemplateModel(NotificationArguments,
              $"MandatoryList/Details?id={Util.Encrypt(id)}",
              NotificationEntityType.MandatoryList,
              id.ToString());

            await _notificationAppService.SendNotificationForUsersByRoleName(NotificationOperations.MandatoryListApprover.MandatoryListProducts.SendDeleteMandatoryListToApprove, RoleNames.MandatoryListApprover, mainNotificationTemplateModel);
            await _mandatoryListCommands.Update(entity);
        }

        public async Task ApproveDelete(int id, string verificationCode)
        {
            await _verification.CheckForVerificationCode(id, verificationCode, (int)Enums.VerificationType.MandatoryList);

            var entity = await _mandatoryListQueries.Find(id);
            entity.ApproveDelete();

            NotificationArguments NotificationArguments = new NotificationArguments
            {
                BodyEmailArgs = new object[] { "", entity.DivisionNameAr, entity.DivisionCode },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { entity.DivisionNameAr, entity.DivisionCode },
                SMSArgs = new object[] { entity.DivisionNameAr, entity.DivisionCode }
            };

            MainNotificationTemplateModel mainNotificationTemplateModel = new MainNotificationTemplateModel(NotificationArguments,
             $"MandatoryList/Details?id={Util.Encrypt(id)}",
             NotificationEntityType.MandatoryList,
             id.ToString());

            await _notificationAppService.SendNotificationForUsersByRoleName(NotificationOperations.MandatoryListOfficer.MandatoryListProducts.ApproveDeleteMandatoryList, RoleNames.MandatoryListOfficer, mainNotificationTemplateModel);
            await _mandatoryListCommands.Update(entity);
        }

        public async Task RejectDelete(MandatoryListRejectionViewModel viewModel)
        {
            var entity = await _mandatoryListQueries.Find(Util.Decrypt(viewModel.Id));
            entity.RejectDelete(viewModel.RejectionReason);
            NotificationArguments NotificationArguments = new NotificationArguments
            {
                BodyEmailArgs = new object[] { "", entity.DivisionNameAr, entity.DivisionCode },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { entity.DivisionNameAr, entity.DivisionCode },
                SMSArgs = new object[] { entity.DivisionNameAr, entity.DivisionCode }
            };

            MainNotificationTemplateModel mainNotificationTemplateModel = new MainNotificationTemplateModel(NotificationArguments,
             $"MandatoryList/Details?id={viewModel.Id}",
             NotificationEntityType.MandatoryList,
             viewModel.Id.ToString());

            await _notificationAppService.SendNotificationForUsersByRoleName(NotificationOperations.MandatoryListOfficer.MandatoryListProducts.RejectDeleteMandatoryList, RoleNames.MandatoryListOfficer, mainNotificationTemplateModel);
            await _mandatoryListCommands.Update(entity);

        }

        public async Task<MandatoryListDetailsViewModel> GetMandatoryListDetails(int mandatoryListId)
        {
            MandatoryListDetailsViewModel detailsViewModel = await _mandatoryListQueries.GetMandatoryListDetails(mandatoryListId);
            return detailsViewModel;
        }

        public async Task<MandatoryListApprovalViewModel> GetMandatoryListDetailsForApproval(int mandatoryListId)
        {
            MandatoryListApprovalViewModel approvalViewModel = await _mandatoryListQueries.GetMandatoryListDetailsForApproval(mandatoryListId);
            return approvalViewModel;
        }

        public async Task ApproveEdit(int id, string verificationCode)
        {
            await _verification.CheckForVerificationCode(id, verificationCode, (int)Enums.VerificationType.MandatoryList);

            var entity = await _mandatoryListQueries.Find(id);
            var changeRequestEntity = entity.ApproveEdit();
            var updatedEntity = _mapper.Map<MandatoryList>(changeRequestEntity);
            entity.Update(updatedEntity);

            NotificationArguments NotificationArguments = new NotificationArguments
            {
                BodyEmailArgs = new object[] { "", entity.DivisionNameAr, entity.DivisionCode },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { entity.DivisionNameAr, entity.DivisionCode },
                SMSArgs = new object[] { entity.DivisionNameAr, entity.DivisionCode }
            };

            MainNotificationTemplateModel mainNotificationTemplateModel = new MainNotificationTemplateModel(NotificationArguments,
              $"MandatoryList/Details?id={Util.Encrypt(entity.Id)}",
              NotificationEntityType.MandatoryList,
              entity.Id.ToString());

            await _notificationAppService.SendNotificationForUsersByRoleName(NotificationOperations.MandatoryListOfficer.MandatoryListProducts.ApproveUpdateMandatoryList, RoleNames.MandatoryListOfficer, mainNotificationTemplateModel);
            await _mandatoryListCommands.Update(entity);

        }

        public async Task CloseEdit(int id)
        {
            var entity = await _mandatoryListQueries.Find(id);
            entity.CloseEdit();
            await _mandatoryListCommands.Update(entity);
        }

        public async Task RejectEdit(MandatoryListRejectionViewModel rejectionModel)
        {
            var entity = await _mandatoryListQueries.Find(Util.Decrypt(rejectionModel.Id));
            entity.RejectEdit(rejectionModel.RejectionReason);

            NotificationArguments NotificationArguments = new NotificationArguments
            {
                BodyEmailArgs = new object[] { "", entity.DivisionNameAr, entity.DivisionCode },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { entity.DivisionNameAr, entity.DivisionCode },
                SMSArgs = new object[] { entity.DivisionNameAr, entity.DivisionCode }
            };

            MainNotificationTemplateModel mainNotificationTemplateModel = new MainNotificationTemplateModel(NotificationArguments,
              $"MandatoryList/Details?id={Util.Encrypt(entity.Id)}",
              NotificationEntityType.MandatoryList,
              entity.Id.ToString());

            await _notificationAppService.SendNotificationForUsersByRoleName(NotificationOperations.MandatoryListOfficer.MandatoryListProducts.RejectUpdateMandatoryList, RoleNames.MandatoryListOfficer, mainNotificationTemplateModel);

            await _mandatoryListCommands.Update(entity);

        }

        public async Task<List<MandatoryListForQuantityTableViewModel>> GetAllForQuantitiyTable()
        {
            return await _mandatoryListQueries.GetAllForQuantityTable();
        }
        public async Task<CSICodeDetailsModel> GetMandatoryListCSICodeInfo(string code)
        {
            return await _mandatoryListQueries.GetMandatoryListCSICodeInfo(code);
        }

        public async Task<Dictionary<string, bool>> GetValidMandatoryListCodeForQauntityTableExcel(List<string> CSICodes)
        {
            var result = await _mandatoryListQueries.GetValidMandatoryListCodeForQauntityTableExcel(CSICodes);

            Dictionary<string, bool> data = new Dictionary<string, bool>();
            foreach (var item in CSICodes)
            {
                data.Add(item, result.Contains(item));
            }
            return data;
        }
    }
}
