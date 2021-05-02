using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.SharedKernal;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Security;

namespace MOF.Etimad.Monafasat.Integration.Infrastructure
{
    public class ProxyBase : IDisposable
    {

        #region [..Variables..]

        protected readonly static string ChannelID = "ETIMAD";
        protected readonly string RequestID = GetNewRequestID();
        protected RootConfigurations _rootConfiguration;

        internal static string GetNewRequestID()
        {
            return ChannelID + String.Format("{0:yyyyMMddHHmmssfff}", DateTime.Now);
        }


        protected readonly static string ClientDate = String.Format("{0:yyyy-MM-ddTHH:mm:ss}", DateTime.Now);
        protected readonly static string Version = "1.0";
        #endregion


        protected virtual string ServiceID { get; }
        protected virtual string FunctionID { get; }
        protected virtual string Port { get; }



        public ProxyBase(IOptionsSnapshot<RootConfigurations> rootConfiguration)
        {
            _rootConfiguration = rootConfiguration.Value;
            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, errors) =>
            {
                if (!_rootConfiguration.EsbSettingsConfiguration.IsProduction) return true;
                return errors == SslPolicyErrors.None;
            };
        }

        public decimal GetPaymentPercentage(decimal actualPayment, decimal basicBudgetCash)
        {
            if (actualPayment == default || basicBudgetCash == default)
            {
                return 0;
            }
            decimal result = actualPayment / basicBudgetCash * 100;
            return Math.Round(result, 2);
        }

        public string GetBudgetChapterName(string budgetChabterId)
        {
            var budgetChapters = new Dictionary<string, string>
            {
                {"11","الضرائب"},
                {"14","إيرادات أخرى"},
                {"21","تعويضات العاملين"},
                {"22","السلع والخدمات"},
                {"24","نفقات تمويل"},
                {"25","الإعانات"},
                {"26","المنح"},
                {"27","المنافع الاجتماعية"},
                {"28","مصروفات أخرى"},
                {"31","الأصول الغير مالية"},
                {"32","الأصول المالية"},
                {"33","الخصوم المالية"},
                {"34","حسابات التسوية"}
            };

            return budgetChapters[budgetChabterId];
        }

        public void Dispose()
        {

        }
    }
}
