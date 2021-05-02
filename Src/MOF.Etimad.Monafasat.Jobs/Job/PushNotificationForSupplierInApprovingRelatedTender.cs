using FluentScheduler;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MOF.Etimad.Monafasat.Core.Interfaces;
using MOF.Etimad.Monafasat.Data;
using MOF.Etimad.Monafasat.Services;
using MOF.Etimad.Monafasat.Services.Common.NotificationServices;
using MOF.Etimad.Monafasat.SharedKernel;
using System;
using System.Text;
namespace MOF.Etimad.Monafasat.Jobs
{
    public class PushNotificationForSupplierInApprovingRelatedTender : IJob
    {
         IMobileAppService _mobileAppService = null;
       
        public void Execute()
        {
            using (var serviceProvider = new PrepareDependancies().SetupDependancies().BuildServiceProvider())
            {
                _mobileAppService = serviceProvider.GetService<IMobileAppService>();
            }
            AsyncHelper.RunSync(() => _mobileAppService.SendPostedNotificationForSuppliersJob());
        }

        public static void Log(Exception ex, string refNo = null)
        {
            if (ex == null) return;

            var sb = new StringBuilder();

            sb.AppendFormat("DateTime: {0}", DateTime.Now.ToString())
                .AppendLine()
                .AppendFormat("RefNo: {0}", refNo)
                .AppendLine()
                .AppendFormat("Ex: {0}", ex.ToString())
                .AppendLine()
                .AppendFormat("Trace: {0}", ex.StackTrace.ToString())
                .AppendLine()
                .AppendLine("=============================================================")
                .AppendLine();

            System.IO.File.AppendAllLines(AppDomain.CurrentDomain.BaseDirectory + "/_log.txt", new string[] { sb.ToString() });

        }
    }
}


