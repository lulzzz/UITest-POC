using MOF.Etimad.Monafasat.Integration.Proxies;
using System;
using System.Collections.Generic;

namespace MOF.Etimad.Monafasat.Integration
{
    public static class ProxyResolver //: IServiceLocator
    {
        private static IDictionary<Type, Type> proxies;
        private static IDictionary<Type, Type> fakeProxies;
        static ProxyResolver()
        {
            proxies = new Dictionary<Type, Type>();
            fakeProxies = new Dictionary<Type, Type>();
            RegisterProxies();
        }

        private static void Register<TKey, TVal>() where TVal : ProxyBase
        {
            proxies.Add(typeof(TKey), typeof(TVal));
        }

        private static void RegisterProxies()
        {
            Register<INotificationProxy, NotificationProxy>();
            Register<IFileNetProxy, FileNetProxy>();
            Register<IBillingProxy, BillingProxy>();
            Register<IIDMProxy, IDMProxy>();
            Register<IYasserProxy, YasserProxy>();
            Register<IContentEncryptionManger, ContentEncryptionManger>();
            Register<ICertificateEncryption, CertificateEncryption>();
            Register<IQuantityTemplatesProxy, QuantityTemplatesProxy>();
            Register<IAgencyBudgetProxy, AgencyBudgetProxy>();
            Register<ISubscriptionProxy, SubscriptionProxy>();
            Register<IWathiqProxy, WathiqProxy>();
            Register<ISRMFrameworkAgreementManageProxy, SRMFrameworkAgreementManageProxy>();
            Register<ISMEASizeInquiryProxy, SMEASizeInquiryProxy>();

        }
    }
}
