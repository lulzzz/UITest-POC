using MOF.Etimad.Monafasat.Core;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
 

namespace MOF.Etimad.Monafasat.Services.Common.NotificationServices
{
    public interface IPreQualificationNotification
    {
        Task<bool> SendNotificationForSendingSupplierDocsToDataEntry(Tender tender);
        Task<bool> SendNotificationToSupplier(Tender tender, string supplierId);

    }
}
