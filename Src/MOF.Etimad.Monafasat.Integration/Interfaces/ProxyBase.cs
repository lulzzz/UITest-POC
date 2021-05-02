using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.SharedKernal;
using System;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Net.Security;

namespace MOF.Etimad.Monafasat.Integration
{
    public class ProxyBase
    {
        protected readonly string ChannelID = "MNFST";
        protected readonly string Version = "1.0";
        protected virtual string ServiceID { get; }
        protected virtual string FunctionID { get; }
        protected virtual string Port { get; }

        protected string RequestID { get => GetNewRequestID(); }

        protected readonly CultureInfo enProvider = CultureInfo.CreateSpecificCulture("en-USA");

        protected string ClientDate { get => GetCurrentDate(); }
        protected RootConfigurations _rootConfiguration;

        public ProxyBase(IOptionsSnapshot<RootConfigurations> rootConfiguration)
        {
            _rootConfiguration = rootConfiguration.Value;
            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, errors) =>
            {
                if (!_rootConfiguration.EsbSettingsConfiguration.IsProduction) return true;
                return errors == SslPolicyErrors.None;
            };
        }

        internal HttpClientHandler CertificaetHandler()
        {
            return new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, certificate, chain, errors) =>
                {
                    if (!_rootConfiguration.EsbSettingsConfiguration.IsProduction) return true;
                    return errors == SslPolicyErrors.None;
                }
            };
        }

        protected string ValidAgencyCodeForUGP(string agencyCode)
        {
            return (agencyCode.Length == 12) ? agencyCode : agencyCode.Substring(0, 12);
        }


        /// <summary>
        /// Must be 9 digits
        /// </summary>
        /// <param name="agencyCode"></param>
        /// <returns></returns>
        protected string ValidAgencyCode(string agencyCode)
        {
            if (agencyCode.Length == 9)
                return agencyCode;
            else
                return agencyCode.Substring(0, 9);
        }
        internal string GetNewRequestID()
        {
            return ChannelID + String.Format("{0:yyyyMMddHHmmssfff}", DateTime.Now);
        }

        internal string GetCurrentDate()
        {
            return DateTime.Now.ToString("yyyy-MM-ddTHH\\:mm\\:ss", enProvider);
        }
    }
}

