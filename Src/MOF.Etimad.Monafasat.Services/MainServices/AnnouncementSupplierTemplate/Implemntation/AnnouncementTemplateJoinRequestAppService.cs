using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;
using static MOF.Etimad.Monafasat.SharedKernel.Enums;

namespace MOF.Etimad.Monafasat.Services
{

    public class AnnouncementTemplateJoinRequestAppService : IAnnouncementTemplateJoinRequestAppService
    {
        private readonly IAnnouncementTemplateJoinRequestCommands _joinRequestCommands;
        private readonly IAnnouncementTemplateJoinRequestQueries _joinRequestQueries;
        private readonly INotificationAppService _notificationAppService;

        public AnnouncementTemplateJoinRequestAppService(IAnnouncementTemplateJoinRequestCommands joinRequestCommands, IAnnouncementTemplateJoinRequestQueries joinRequestQueries, INotificationAppService notificationAppService)
        {
            _joinRequestCommands = joinRequestCommands;
            _joinRequestQueries = joinRequestQueries;
            _notificationAppService = notificationAppService;
        }

        public async Task<AnnouncementSuppliersTemplateJoinRequestsDetailsModel> SaveJoinRequestResult(AnnouncementSuppliersTemplateJoinRequestsDetailsModel joinRequestModel)
        {
            AnnouncementJoinRequestSupplierTemplate joinRequest = await _joinRequestQueries.GetAnnouncementJoinRequestByRequestId(joinRequestModel.JoinRequestId);

            joinRequest.UpdateAnnouncementJoinRequest(joinRequestModel.UserId, joinRequest.Id, joinRequestModel.RequestResultId, joinRequestModel.RejectionReason, joinRequestModel.Notes);
            await _joinRequestCommands.UpdateAnnouncementJoinRequestAsync(joinRequest);

            return joinRequestModel;
        }

        public async Task WithdrawFromAnnouncementTemplate(int userId, int joinRequestId)
        {

            AnnouncementJoinRequestSupplierTemplate joinRequest = await _joinRequestQueries.GetAnnouncementJoinRequestWithAttachmentsByRequestId(joinRequestId);
            joinRequest.Withdraw(userId, joinRequestId);
            await _joinRequestCommands.UpdateAnnouncementJoinRequestAsync(joinRequest);
        }
        public async Task DeleteSupplierFromAnnouncementTemplate(DeleteSupplierFromAnnouncementModel deleteModel)
        {
            int joinRequestId = Util.Decrypt(deleteModel.JoinRequestIdString);
            AnnouncementJoinRequestSupplierTemplate joinRequest = await _joinRequestQueries.GetAnnouncementJoinRequestWithAttachmentsAndAnnouncementByRequestId(joinRequestId);
            joinRequest.DeleteSupplierJoinRequest(deleteModel.UserId, joinRequestId, deleteModel.DeleteReason);

            var announcementList = joinRequest.AnnouncementSupplierTemplate;
            await SendNotificationAfterDeleteSupplierFromAnnoincementList(deleteModel, announcementList);
            await _joinRequestCommands.UpdateAnnouncementJoinRequestAsync(joinRequest);
        }

        private async Task SendNotificationAfterDeleteSupplierFromAnnoincementList(DeleteSupplierFromAnnouncementModel deleteModel, AnnouncementSupplierTemplate announcementList)
        {
            var announcementTemplateIdString = Util.Encrypt(announcementList.AnnouncementId);
            List<string> crs = new List<string> { deleteModel.CR };
            MainNotificationTemplateModel templateModel;
            NotificationArguments NotificationArguments = new NotificationArguments();
            NotificationArguments.BodyEmailArgs = new object[] { announcementList.ReferenceNumber };
            NotificationArguments.SubjectEmailArgs = new object[] { };
            NotificationArguments.PanelArgs = new object[] { announcementList.ReferenceNumber };
            NotificationArguments.SMSArgs = new object[] { announcementList.ReferenceNumber };
            templateModel = new MainNotificationTemplateModel(NotificationArguments, $"AnnouncementSuppliersTemplate/AnnouncementSuppliersTemplateDetailsForSuppliers?announcmentIdString={announcementTemplateIdString}", NotificationEntityType.Tender, announcementList.AnnouncementId.ToString(), 0);
            await _notificationAppService.SendNotificationForSuppliers(NotificationOperations.Supplier.OperationsOnTheTender.DeleteSupplierFromAnnouncementList, crs, templateModel);
        }

        public async Task<AnnouncementSuppliersTemplateJoinRequestsDetailsModel> GetAnnouncementJoinRequestDetails(int announcementId, string cr)
        {
            AnnouncementSuppliersTemplateJoinRequestsDetailsModel joinRequest = await _joinRequestQueries.GetAnnouncementJoinRequestByAnnouncementIdAndCR(announcementId, cr);

            return joinRequest;
        }

    }
}
