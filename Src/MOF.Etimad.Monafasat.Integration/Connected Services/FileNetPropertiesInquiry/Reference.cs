﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     //
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FileNetPropertiesInquiry
{
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://www.ejada.com", ConfigurationName="FileNetPropertiesInquiry")]
    public interface FileNetPropertiesInquiry
    {
        
        [System.ServiceModel.OperationContractAttribute(Action="FileNetPropertiesInquiry", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<FileNetPropertiesInquiryResponse> FileNetPropertiesInquiryAsync(FileNetPropertiesInquiryRequest request);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.ejada.com")]
    public partial class FileNetPropertiesInquiryRq_Type
    {
        
        private MsgRqHdr_Type msgRqHdrField;
        
        private FileNetPropertiesInquiryRqBody_Type bodyField;
        
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
        public FileNetPropertiesInquiryRqBody_Type Body
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
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
        AgencyCode,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.ejada.com")]
    public partial class FileProperty_Type
    {
        
        private FilePropertyType_Type filePropertyTypeField;
        
        private string definitionIdField;
        
        private string propertyValueField;
        
        private string localNameField;
        
        private string displayNameField;
        
        private string queryNameField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public FilePropertyType_Type FilePropertyType
        {
            get
            {
                return this.filePropertyTypeField;
            }
            set
            {
                this.filePropertyTypeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string DefinitionId
        {
            get
            {
                return this.definitionIdField;
            }
            set
            {
                this.definitionIdField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public string PropertyValue
        {
            get
            {
                return this.propertyValueField;
            }
            set
            {
                this.propertyValueField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=3)]
        public string LocalName
        {
            get
            {
                return this.localNameField;
            }
            set
            {
                this.localNameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=4)]
        public string DisplayName
        {
            get
            {
                return this.displayNameField;
            }
            set
            {
                this.displayNameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=5)]
        public string QueryName
        {
            get
            {
                return this.queryNameField;
            }
            set
            {
                this.queryNameField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.ejada.com")]
    public enum FilePropertyType_Type
    {
        
        /// <remarks/>
        Boolean,
        
        /// <remarks/>
        Id,
        
        /// <remarks/>
        Integer,
        
        /// <remarks/>
        DateTime,
        
        /// <remarks/>
        Decimal,
        
        /// <remarks/>
        Html,
        
        /// <remarks/>
        String,
        
        /// <remarks/>
        Uri,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.ejada.com")]
    public partial class FileNetPropertiesInquiryRsBody_Type
    {
        
        private FileProperty_Type[] filePropertyListField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Order=0)]
        [System.Xml.Serialization.XmlArrayItemAttribute("FileProperty", IsNullable=false)]
        public FileProperty_Type[] FilePropertyList
        {
            get
            {
                return this.filePropertyListField;
            }
            set
            {
                this.filePropertyListField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.ejada.com")]
    public partial class FileNetPropertiesInquiryRs_Type
    {
        
        private MsgRsHdr_Type msgRsHdrField;
        
        private FileNetPropertiesInquiryRsBody_Type bodyField;
        
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
        public FileNetPropertiesInquiryRsBody_Type Body
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.ejada.com")]
    public partial class UserCredentials_Type
    {
        
        private string userIdField;
        
        private string passwordField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
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
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string Password
        {
            get
            {
                return this.passwordField;
            }
            set
            {
                this.passwordField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.ejada.com")]
    public partial class FileNetPropertiesInquiryRqBody_Type
    {
        
        private UserCredentials_Type userCredentialsField;
        
        private string repositoryIdField;
        
        private string fileIdField;
        
        private string filterField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public UserCredentials_Type UserCredentials
        {
            get
            {
                return this.userCredentialsField;
            }
            set
            {
                this.userCredentialsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string RepositoryId
        {
            get
            {
                return this.repositoryIdField;
            }
            set
            {
                this.repositoryIdField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public string FileId
        {
            get
            {
                return this.fileIdField;
            }
            set
            {
                this.fileIdField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=3)]
        public string Filter
        {
            get
            {
                return this.filterField;
            }
            set
            {
                this.filterField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class FileNetPropertiesInquiryRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.ejada.com", Order=0)]
        public FileNetPropertiesInquiryRq_Type FileNetPropertiesInquiryRq;
        
        public FileNetPropertiesInquiryRequest()
        {
        }
        
        public FileNetPropertiesInquiryRequest(FileNetPropertiesInquiryRq_Type FileNetPropertiesInquiryRq)
        {
            this.FileNetPropertiesInquiryRq = FileNetPropertiesInquiryRq;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class FileNetPropertiesInquiryResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.ejada.com", Order=0)]
        public FileNetPropertiesInquiryRs_Type FileNetPropertiesInquiryRs;
        
        public FileNetPropertiesInquiryResponse()
        {
        }
        
        public FileNetPropertiesInquiryResponse(FileNetPropertiesInquiryRs_Type FileNetPropertiesInquiryRs)
        {
            this.FileNetPropertiesInquiryRs = FileNetPropertiesInquiryRs;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    public interface FileNetPropertiesInquiryChannel : FileNetPropertiesInquiry, System.ServiceModel.IClientChannel
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    public partial class FileNetPropertiesInquiryClient : System.ServiceModel.ClientBase<FileNetPropertiesInquiry>, FileNetPropertiesInquiry
    {
        
    /// <summary>
    /// Implement this partial method to configure the service endpoint.
    /// </summary>
    /// <param name="serviceEndpoint">The endpoint to configure</param>
    /// <param name="clientCredentials">The client credentials</param>
    static partial void ConfigureEndpoint(System.ServiceModel.Description.ServiceEndpoint serviceEndpoint, System.ServiceModel.Description.ClientCredentials clientCredentials);
        
        public FileNetPropertiesInquiryClient(EndpointConfiguration endpointConfiguration) : 
                base(FileNetPropertiesInquiryClient.GetBindingForEndpoint(endpointConfiguration), FileNetPropertiesInquiryClient.GetEndpointAddress(endpointConfiguration))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public FileNetPropertiesInquiryClient(EndpointConfiguration endpointConfiguration, string remoteAddress) : 
                base(FileNetPropertiesInquiryClient.GetBindingForEndpoint(endpointConfiguration), new System.ServiceModel.EndpointAddress(remoteAddress))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public FileNetPropertiesInquiryClient(EndpointConfiguration endpointConfiguration, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(FileNetPropertiesInquiryClient.GetBindingForEndpoint(endpointConfiguration), remoteAddress)
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public FileNetPropertiesInquiryClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress)
        {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public System.Threading.Tasks.Task<FileNetPropertiesInquiryResponse> FileNetPropertiesInquiryAsync(FileNetPropertiesInquiryRequest request)
        {
            return base.Channel.FileNetPropertiesInquiryAsync(request);
        }
        
        public System.Threading.Tasks.Task<FileNetPropertiesInquiryResponse> FileNetPropertiesInquiryAsync(FileNetPropertiesInquiryRq_Type FileNetPropertiesInquiryRq)
        {
            FileNetPropertiesInquiryRequest inValue = new FileNetPropertiesInquiryRequest();
            inValue.FileNetPropertiesInquiryRq = FileNetPropertiesInquiryRq;
            return ((FileNetPropertiesInquiry)(this)).FileNetPropertiesInquiryAsync(inValue);
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
            if ((endpointConfiguration == EndpointConfiguration.FileNetPropertiesInquirySOAP12))
            {
                System.ServiceModel.Channels.CustomBinding result = new System.ServiceModel.Channels.CustomBinding();
                System.ServiceModel.Channels.TextMessageEncodingBindingElement textBindingElement = new System.ServiceModel.Channels.TextMessageEncodingBindingElement();
                textBindingElement.MessageVersion = System.ServiceModel.Channels.MessageVersion.CreateVersion(System.ServiceModel.EnvelopeVersion.Soap12, System.ServiceModel.Channels.AddressingVersion.None);
                result.Elements.Add(textBindingElement);
                System.ServiceModel.Channels.HttpsTransportBindingElement httpsBindingElement = new System.ServiceModel.Channels.HttpsTransportBindingElement();
                httpsBindingElement.AllowCookies = true;
                httpsBindingElement.MaxBufferSize = int.MaxValue;
                httpsBindingElement.MaxReceivedMessageSize = int.MaxValue;
                result.Elements.Add(httpsBindingElement);
                return result;
            }
            if ((endpointConfiguration == EndpointConfiguration.FileNetPropertiesInquirySOAP11))
            {
                System.ServiceModel.BasicHttpBinding result = new System.ServiceModel.BasicHttpBinding();
                result.MaxBufferSize = int.MaxValue;
                result.ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max;
                result.MaxReceivedMessageSize = int.MaxValue;
                result.AllowCookies = true;
                return result;
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        private static System.ServiceModel.EndpointAddress GetEndpointAddress(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.FileNetPropertiesInquirySOAP12))
            {
                return new System.ServiceModel.EndpointAddress("https://ry1drvesbiib01.mof.gov.sa:7941/FileNetPropertiesInquiry");
            }
            if ((endpointConfiguration == EndpointConfiguration.FileNetPropertiesInquirySOAP11))
            {
                return new System.ServiceModel.EndpointAddress("http://localhost:7800/FileNetPropertiesInquiry11");
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        public enum EndpointConfiguration
        {
            
            FileNetPropertiesInquirySOAP12,
            
            FileNetPropertiesInquirySOAP11,
        }
    }
}