using Microsoft.Extensions.Configuration;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernel;
using System.Collections.Generic;
using System.Threading.Tasks;

using static MOF.Etimad.Monafasat.SharedKernel.Enums;

namespace MOF.Etimad.Monafasat.Services.Common.NotificationServices
{
    public class PreQualificationNotification : IPreQualificationNotification
    {
        private readonly INotifayAppService _notifayAppService;
        private readonly IConfiguration _configuration;
        private readonly ISupplierService _supplierService;
        private readonly IIDMAppService _iDMAppService;
        //private readonly ITenderAppService _tenderAppService;
        public PreQualificationNotification(INotifayAppService notifayAppService, IConfiguration configuration, ISupplierService supplierService, IIDMAppService iDMAppService)//, ITenderAppService tenderAppService)
        {
            _notifayAppService = notifayAppService;
            _configuration = configuration;
            _supplierService = supplierService;
            //_tenderAppService = tenderAppService;
            _iDMAppService = iDMAppService;
        }

        public async Task<bool> SendNotificationForSendingSupplierDocsToDataEntry(Tender tender)
        {
            var taskList = new List<Task>();
            var list = (await _notifayAppService.GetUsersByRoleNameAndAgencyCode(RoleNames.DataEntry, tender.AgencyCode));
            foreach (var u in list)
            {
                await _notifayAppService.AddNotifayToAllWithSend(u.Id, Enums.NotifayType.OperationsOnTheTender,
                    string.Format(string.Format(_configuration.GetSection("SupplierApplyDocumentRecieved:SendMessage:subject").Value, tender.TenderNumber??"")),
                    string.Format(string.Format(_configuration.GetSection("SupplierApplyDocumentRecieved:SendMessage:body").Value, tender.TenderNumber??"")),
                    $"Qualification/ApplyPreQualificationDocument?qualificationId={Util.Encrypt(tender.TenderId)}", false, UserType.NewMonafasat_DataEntry);
            }
            return true;
        }


        public async Task<bool> SendNotificationToSupplier(Tender tender, string supplierId)
        {
            var supplierlst = (await _notifayAppService.GetUserByCR(supplierId));
            foreach (var supplier in supplierlst)
            {


                await _notifayAppService.AddNotifayToAllWithSend(supplier.UserProfileId, Enums.NotifayType.OperationsOnTheTender,
                    string.Format(string.Format(_configuration.GetSection("SupplierApplyDocumentRecieved:SendMessage:subject").Value, tender.TenderNumber)),
                    string.Format(string.Format(_configuration.GetSection("SupplierApplyDocumentRecieved:SendMessage:body").Value, tender.TenderNumber)),
                    $"Qualification/ApplyPreQualificationDocument?qualificationId={Util.Encrypt(tender.TenderId)}&STenderStatusId={Util.Encrypt(tender.TenderStatusId)}&displayOnlyMode=true", false, UserType.NewMonafasat_Supplier);


            }

            return true;
        }


    }
}
