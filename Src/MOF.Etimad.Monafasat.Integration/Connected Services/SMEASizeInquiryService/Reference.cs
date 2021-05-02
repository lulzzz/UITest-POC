﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SMEASizeInquiryService
{
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://www.ejada.com", ConfigurationName="SMEASizeInquiryService.SMEASizeInquiry")]
    public interface SMEASizeInquiry
    {
        
        [System.ServiceModel.OperationContractAttribute(Action="SMEASizeInquiry", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<SMEASizeInquiryService.SMEASizeInquiryResponse> SMEASizeInquiryAsync(SMEASizeInquiryService.SMEASizeInquiryRequest request);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.ejada.com")]
    public partial class SMEASizeInquiryRq_Type
    {
        
        private MsgRqHdr_Type msgRqHdrField;
        
        private SMEASizeInquiryRqBody_Type bodyField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public MsgRqHdr_Type MsgRqHdr
        {
            get
            {
                return this.msgRqHdrField;
            }
            set
            {
                this.msgRqHdrField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public SMEASizeInquiryRqBody_Type Body
        {
            get
            {
                return this.bodyField;
            }
            set
            {
                this.bodyField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.ejada.com")]
    public partial class MsgRqHdr_Type
    {
        
        private string rqUIDField;
        
        private string processUIDField;
        
        private string sessionIdField;
        
        private string sCIdField;
        
        private string serviceIdField;
        
        private string funcIdField;
        
        private long rqModeField;
        
        private bool rqModeFieldSpecified;
        
        private PartyId_Type partyIdField;
        
        private string userIdField;
        
        private string proxyUserIdField;
        
        private string clientDtField;
        
        private string echoDataField;
        
        private string versionField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string RqUID
        {
            get
            {
                return this.rqUIDField;
            }
            set
            {
                this.rqUIDField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string ProcessUID
        {
            get
            {
                return this.processUIDField;
            }
            set
            {
                this.processUIDField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public string SessionId
        {
            get
            {
                return this.sessionIdField;
            }
            set
            {
                this.sessionIdField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=3)]
        public string SCId
        {
            get
            {
                return this.sCIdField;
            }
            set
            {
                this.sCIdField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=4)]
        public string ServiceId
        {
            get
            {
                return this.serviceIdField;
            }
            set
            {
                this.serviceIdField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=5)]
        public string FuncId
        {
            get
            {
                return this.funcIdField;
            }
            set
            {
                this.funcIdField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=6)]
        public long RqMode
        {
            get
            {
                return this.rqModeField;
            }
            set
            {
                this.rqModeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool RqModeSpecified
        {
            get
            {
                return this.rqModeFieldSpecified;
            }
            set
            {
                this.rqModeFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=7)]
        public PartyId_Type PartyId
        {
            get
            {
                return this.partyIdField;
            }
            set
            {
                this.partyIdField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=8)]
        public string UserId
        {
            get
            {
                return this.userIdField;
            }
            set
            {
                this.userIdField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=9)]
        public string ProxyUserId
        {
            get
            {
                return this.proxyUserIdField;
            }
            set
            {
                this.proxyUserIdField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=10)]
        public string ClientDt
        {
            get
            {
                return this.clientDtField;
            }
            set
            {
                this.clientDtField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=11)]
        public string EchoData
        {
            get
            {
                return this.echoDataField;
            }
            set
            {
                this.echoDataField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=12)]
        public string Version
        {
            get
            {
                return this.versionField;
            }
            set
            {
                this.versionField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.ejada.com")]
    public partial class PartyId_Type
    {
        
        private string partyIdNumField;
        
        private PartyIdType_Type partyIdTypeField;
        
        private bool partyIdTypeFieldSpecified;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string PartyIdNum
        {
            get
            {
                return this.partyIdNumField;
            }
            set
            {
                this.partyIdNumField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public PartyIdType_Type PartyIdType
        {
            get
            {
                return this.partyIdTypeField;
            }
            set
            {
                this.partyIdTypeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool PartyIdTypeSpecified
        {
            get
            {
                return this.partyIdTypeFieldSpecified;
            }
            set
            {
                this.partyIdTypeFieldSpecified = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.ejada.com")]
    public enum PartyIdType_Type
    {
        
        /// <remarks/>
        IqamaNumber,
        
        /// <remarks/>
        NationalId,
        
        /// <remarks/>
        CommercialRegistration,
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("700Code")]
        Item700Code,
        
        /// <remarks/>
        AccountNumber,
        
        /// <remarks/>
        FamilyCardNumber,
        
        /// <remarks/>
        BorderNumber,
        
        /// <remarks/>
        PassportNumber,
        
        /// <remarks/>
        AgencyId,
        
        /// <remarks/>
        VendorCr,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.ejada.com")]
    public partial class SMEASizeInquiryRsBody_Type
    {
        
        private string enterpriseSizeField;
        
        private string revenueDescField;
        
        private string revenueDescArField;
        
        private string laborerCountDescField;
        
        private string laborerCountDescArField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string EnterpriseSize
        {
            get
            {
                return this.enterpriseSizeField;
            }
            set
            {
                this.enterpriseSizeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string RevenueDesc
        {
            get
            {
                return this.revenueDescField;
            }
            set
            {
                this.revenueDescField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public string RevenueDescAr
        {
            get
            {
                return this.revenueDescArField;
            }
            set
            {
                this.revenueDescArField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=3)]
        public string LaborerCountDesc
        {
            get
            {
                return this.laborerCountDescField;
            }
            set
            {
                this.laborerCountDescField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=4)]
        public string LaborerCountDescAr
        {
            get
            {
                return this.laborerCountDescArField;
            }
            set
            {
                this.laborerCountDescArField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.ejada.com")]
    public partial class ResponseStatus_Type
    {
        
        private string statusCodeField;
        
        private string statusDescField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string StatusCode
        {
            get
            {
                return this.statusCodeField;
            }
            set
            {
                this.statusCodeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string StatusDesc
        {
            get
            {
                return this.statusDescField;
            }
            set
            {
                this.statusDescField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.ejada.com")]
    public partial class OverrideStatus_Type
    {
        
        private string codeField;
        
        private string descField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string Code
        {
            get
            {
                return this.codeField;
            }
            set
            {
                this.codeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string Desc
        {
            get
            {
                return this.descField;
            }
            set
            {
                this.descField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.ejada.com")]
    public partial class MsgRsHdr_Type
    {
        
        private string rqUIDField;
        
        private string processUIDField;
        
        private string sessionIdField;
        
        private string sPRefNumField;
        
        private string msgRecDtField;
        
        private OverrideStatus_Type overrideStatusField;
        
        private ResponseStatus_Type responseStatusField;
        
        private string echoDataField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string RqUID
        {
            get
            {
                return this.rqUIDField;
            }
            set
            {
                this.rqUIDField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string ProcessUID
        {
            get
            {
                return this.processUIDField;
            }
            set
            {
                this.processUIDField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public string SessionId
        {
            get
            {
                return this.sessionIdField;
            }
            set
            {
                this.sessionIdField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=3)]
        public string SPRefNum
        {
            get
            {
                return this.sPRefNumField;
            }
            set
            {
                this.sPRefNumField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=4)]
        public string MsgRecDt
        {
            get
            {
                return this.msgRecDtField;
            }
            set
            {
                this.msgRecDtField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=5)]
        public OverrideStatus_Type OverrideStatus
        {
            get
            {
                return this.overrideStatusField;
            }
            set
            {
                this.overrideStatusField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=6)]
        public ResponseStatus_Type ResponseStatus
        {
            get
            {
                return this.responseStatusField;
            }
            set
            {
                this.responseStatusField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=7)]
        public string EchoData
        {
            get
            {
                return this.echoDataField;
            }
            set
            {
                this.echoDataField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.ejada.com")]
    public partial class SMEASizeInquiryRs_Type
    {
        
        private MsgRsHdr_Type msgRsHdrField;
        
        private SMEASizeInquiryRsBody_Type bodyField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public MsgRsHdr_Type MsgRsHdr
        {
            get
            {
                return this.msgRsHdrField;
            }
            set
            {
                this.msgRsHdrField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public SMEASizeInquiryRsBody_Type Body
        {
            get
            {
                return this.bodyField;
            }
            set
            {
                this.bodyField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.ejada.com")]
    public partial class SMEASizeInquiryRqBody_Type
    {
        
        private string cRNumberField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string CRNumber
        {
            get
            {
                return this.cRNumberField;
            }
            set
            {
                this.cRNumberField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class SMEASizeInquiryRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.ejada.com", Order=0)]
        public SMEASizeInquiryService.SMEASizeInquiryRq_Type SMEASizeInquiryRq;
        
        public SMEASizeInquiryRequest()
        {
        }
        
        public SMEASizeInquiryRequest(SMEASizeInquiryService.SMEASizeInquiryRq_Type SMEASizeInquiryRq)
        {
            this.SMEASizeInquiryRq = SMEASizeInquiryRq;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class SMEASizeInquiryResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.ejada.com", Order=0)]
        public SMEASizeInquiryService.SMEASizeInquiryRs_Type SMEASizeInquiryRs;
        
        public SMEASizeInquiryResponse()
        {
        }
        
        public SMEASizeInquiryResponse(SMEASizeInquiryService.SMEASizeInquiryRs_Type SMEASizeInquiryRs)
        {
            this.SMEASizeInquiryRs = SMEASizeInquiryRs;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    public interface SMEASizeInquiryChannel : SMEASizeInquiryService.SMEASizeInquiry, System.ServiceModel.IClientChannel
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    public partial class SMEASizeInquiryClient : System.ServiceModel.ClientBase<SMEASizeInquiryService.SMEASizeInquiry>, SMEASizeInquiryService.SMEASizeInquiry
    {
        
        /// <summary>
        /// Implement this partial method to configure the service endpoint.
        /// </summary>
        /// <param name="serviceEndpoint">The endpoint to configure</param>
        /// <param name="clientCredentials">The client credentials</param>
        static partial void ConfigureEndpoint(System.ServiceModel.Description.ServiceEndpoint serviceEndpoint, System.ServiceModel.Description.ClientCredentials clientCredentials);
        
        public SMEASizeInquiryClient(EndpointConfiguration endpointConfiguration) : 
                base(SMEASizeInquiryClient.GetBindingForEndpoint(endpointConfiguration), SMEASizeInquiryClient.GetEndpointAddress(endpointConfiguration))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public SMEASizeInquiryClient(EndpointConfiguration endpointConfiguration, string remoteAddress) : 
                base(SMEASizeInquiryClient.GetBindingForEndpoint(endpointConfiguration), new System.ServiceModel.EndpointAddress(remoteAddress))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public SMEASizeInquiryClient(EndpointConfiguration endpointConfiguration, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(SMEASizeInquiryClient.GetBindingForEndpoint(endpointConfiguration), remoteAddress)
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public SMEASizeInquiryClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress)
        {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<SMEASizeInquiryService.SMEASizeInquiryResponse> SMEASizeInquiryService.SMEASizeInquiry.SMEASizeInquiryAsync(SMEASizeInquiryService.SMEASizeInquiryRequest request)
        {
            return base.Channel.SMEASizeInquiryAsync(request);
        }
        
        public System.Threading.Tasks.Task<SMEASizeInquiryService.SMEASizeInquiryResponse> SMEASizeInquiryAsync(SMEASizeInquiryService.SMEASizeInquiryRq_Type SMEASizeInquiryRq)
        {
            SMEASizeInquiryService.SMEASizeInquiryRequest inValue = new SMEASizeInquiryService.SMEASizeInquiryRequest();
            inValue.SMEASizeInquiryRq = SMEASizeInquiryRq;
            return ((SMEASizeInquiryService.SMEASizeInquiry)(this)).SMEASizeInquiryAsync(inValue);
        }
        
        public virtual System.Threading.Tasks.Task OpenAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndOpen));
        }
        
        public virtual System.Threading.Tasks.Task CloseAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginClose(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndClose));
        }
        
        private static System.ServiceModel.Channels.Binding GetBindingForEndpoint(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.SMEASizeInquirySOAP12))
            {
                System.ServiceModel.Channels.CustomBinding result = new System.ServiceModel.Channels.CustomBinding();
                System.ServiceModel.Channels.TextMessageEncodingBindingElement textBindingElement = new System.ServiceModel.Channels.TextMessageEncodingBindingElement();
                textBindingElement.MessageVersion = System.ServiceModel.Channels.MessageVersion.CreateVersion(System.ServiceModel.EnvelopeVersion.Soap12, System.ServiceModel.Channels.AddressingVersion.None);
                result.Elements.Add(textBindingElement);
                System.ServiceModel.Channels.HttpTransportBindingElement httpBindingElement = new System.ServiceModel.Channels.HttpTransportBindingElement();
                httpBindingElement.AllowCookies = true;
                httpBindingElement.MaxBufferSize = int.MaxValue;
                httpBindingElement.MaxReceivedMessageSize = int.MaxValue;
                result.Elements.Add(httpBindingElement);
                return result;
            }
            if ((endpointConfiguration == EndpointConfiguration.SMEASizeInquirySOAP11))
            {
                System.ServiceModel.BasicHttpBinding result = new System.ServiceModel.BasicHttpBinding();
                result.MaxBufferSize = int.MaxValue;
                result.ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max;
                result.MaxReceivedMessageSize = int.MaxValue;
                result.AllowCookies = true;
                result.Security.Mode = System.ServiceModel.BasicHttpSecurityMode.Transport;
                return result;
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        private static System.ServiceModel.EndpointAddress GetEndpointAddress(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.SMEASizeInquirySOAP12))
            {
                return new System.ServiceModel.EndpointAddress("http://localhost:7800/SMEASizeInquiry");
            }
            if ((endpointConfiguration == EndpointConfiguration.SMEASizeInquirySOAP11))
            {
                return new System.ServiceModel.EndpointAddress("https://10.14.8.25:7909/SMEASizeInquiry11");
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        public enum EndpointConfiguration
        {
            
            SMEASizeInquirySOAP12,
            
            SMEASizeInquirySOAP11,
        }
    }
}
