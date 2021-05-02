﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     //
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CheckFundService
{
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://www.ejada.com", ConfigurationName="CheckFundService.AgencyBudgetCalcFunds")]
    public interface AgencyBudgetCalcFunds
    {
        
        [System.ServiceModel.OperationContractAttribute(Action="AgencyBudgetCalcFunds", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<CheckFundService.AgencyBudgetCalcFundsResponse> AgencyBudgetCalcFundsAsync(CheckFundService.AgencyBudgetCalcFundsRequest request);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.ejada.com")]
    public partial class AgencyBudgetCalcFundsRq_Type
    {
        
        private MsgRqHdr_Type msgRqHdrField;
        
        private AgencyBudgetCalcFundsRqBody_Type bodyField;
        
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
        public AgencyBudgetCalcFundsRqBody_Type Body
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
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
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.ejada.com")]
    public partial class AgencyBudgetCalcFundsRsBody_Type
    {
        
        private decimal basicBudgetField;
        
        private bool basicBudgetFieldSpecified;
        
        private decimal currentBudgetField;
        
        private bool currentBudgetFieldSpecified;
        
        private decimal obligationENCField;
        
        private bool obligationENCFieldSpecified;
        
        private decimal commitmentENCField;
        
        private bool commitmentENCFieldSpecified;
        
        private decimal postedActualField;
        
        private bool postedActualFieldSpecified;
        
        private decimal unpostedActualField;
        
        private bool unpostedActualFieldSpecified;
        
        private decimal availableFundsField;
        
        private bool availableFundsFieldSpecified;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public decimal BasicBudget
        {
            get
            {
                return this.basicBudgetField;
            }
            set
            {
                this.basicBudgetField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool BasicBudgetSpecified
        {
            get
            {
                return this.basicBudgetFieldSpecified;
            }
            set
            {
                this.basicBudgetFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public decimal CurrentBudget
        {
            get
            {
                return this.currentBudgetField;
            }
            set
            {
                this.currentBudgetField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool CurrentBudgetSpecified
        {
            get
            {
                return this.currentBudgetFieldSpecified;
            }
            set
            {
                this.currentBudgetFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public decimal ObligationENC
        {
            get
            {
                return this.obligationENCField;
            }
            set
            {
                this.obligationENCField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ObligationENCSpecified
        {
            get
            {
                return this.obligationENCFieldSpecified;
            }
            set
            {
                this.obligationENCFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=3)]
        public decimal CommitmentENC
        {
            get
            {
                return this.commitmentENCField;
            }
            set
            {
                this.commitmentENCField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool CommitmentENCSpecified
        {
            get
            {
                return this.commitmentENCFieldSpecified;
            }
            set
            {
                this.commitmentENCFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=4)]
        public decimal PostedActual
        {
            get
            {
                return this.postedActualField;
            }
            set
            {
                this.postedActualField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool PostedActualSpecified
        {
            get
            {
                return this.postedActualFieldSpecified;
            }
            set
            {
                this.postedActualFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=5)]
        public decimal UnpostedActual
        {
            get
            {
                return this.unpostedActualField;
            }
            set
            {
                this.unpostedActualField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool UnpostedActualSpecified
        {
            get
            {
                return this.unpostedActualFieldSpecified;
            }
            set
            {
                this.unpostedActualFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=6)]
        public decimal AvailableFunds
        {
            get
            {
                return this.availableFundsField;
            }
            set
            {
                this.availableFundsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool AvailableFundsSpecified
        {
            get
            {
                return this.availableFundsFieldSpecified;
            }
            set
            {
                this.availableFundsFieldSpecified = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.ejada.com")]
    public partial class AgencyBudgetCalcFundsRs_Type
    {
        
        private MsgRsHdr_Type msgRsHdrField;
        
        private AgencyBudgetCalcFundsRsBody_Type bodyField;
        
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
        public AgencyBudgetCalcFundsRsBody_Type Body
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.ejada.com")]
    public partial class AgencyBudgetCalcFundsRqBody_Type
    {
        
        private string agencyIdField;
        
        private string gFSCodeField;
        
        private string projectIdField;
        
        private string sourceCodeField;
        
        private AmountType_Type amountTypeField;
        
        private object itemField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string AgencyId
        {
            get
            {
                return this.agencyIdField;
            }
            set
            {
                this.agencyIdField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string GFSCode
        {
            get
            {
                return this.gFSCodeField;
            }
            set
            {
                this.gFSCodeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public string ProjectId
        {
            get
            {
                return this.projectIdField;
            }
            set
            {
                this.projectIdField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=3)]
        public string SourceCode
        {
            get
            {
                return this.sourceCodeField;
            }
            set
            {
                this.sourceCodeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=4)]
        public AmountType_Type AmountType
        {
            get
            {
                return this.amountTypeField;
            }
            set
            {
                this.amountTypeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("EffDt", typeof(System.DateTime), DataType="date", Order=5)]
        [System.Xml.Serialization.XmlElementAttribute("PeriodName", typeof(string), Order=5)]
        public object Item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.ejada.com")]
    public enum AmountType_Type
    {
        
        /// <remarks/>
        YTDE,
        
        /// <remarks/>
        QTDE,
        
        /// <remarks/>
        PTD,
        
        /// <remarks/>
        PJTD,
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class AgencyBudgetCalcFundsRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.ejada.com", Order=0)]
        public CheckFundService.AgencyBudgetCalcFundsRq_Type AgencyBudgetCalcFundsRq;
        
        public AgencyBudgetCalcFundsRequest()
        {
        }
        
        public AgencyBudgetCalcFundsRequest(CheckFundService.AgencyBudgetCalcFundsRq_Type AgencyBudgetCalcFundsRq)
        {
            this.AgencyBudgetCalcFundsRq = AgencyBudgetCalcFundsRq;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class AgencyBudgetCalcFundsResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.ejada.com", Order=0)]
        public CheckFundService.AgencyBudgetCalcFundsRs_Type AgencyBudgetCalcFundsRs;
        
        public AgencyBudgetCalcFundsResponse()
        {
        }
        
        public AgencyBudgetCalcFundsResponse(CheckFundService.AgencyBudgetCalcFundsRs_Type AgencyBudgetCalcFundsRs)
        {
            this.AgencyBudgetCalcFundsRs = AgencyBudgetCalcFundsRs;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    public interface AgencyBudgetCalcFundsChannel : CheckFundService.AgencyBudgetCalcFunds, System.ServiceModel.IClientChannel
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    public partial class AgencyBudgetCalcFundsClient : System.ServiceModel.ClientBase<CheckFundService.AgencyBudgetCalcFunds>, CheckFundService.AgencyBudgetCalcFunds
    {
        
    /// <summary>
    /// Implement this partial method to configure the service endpoint.
    /// </summary>
    /// <param name="serviceEndpoint">The endpoint to configure</param>
    /// <param name="clientCredentials">The client credentials</param>
    static partial void ConfigureEndpoint(System.ServiceModel.Description.ServiceEndpoint serviceEndpoint, System.ServiceModel.Description.ClientCredentials clientCredentials);
        
        public AgencyBudgetCalcFundsClient(EndpointConfiguration endpointConfiguration) : 
                base(AgencyBudgetCalcFundsClient.GetBindingForEndpoint(endpointConfiguration), AgencyBudgetCalcFundsClient.GetEndpointAddress(endpointConfiguration))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public AgencyBudgetCalcFundsClient(EndpointConfiguration endpointConfiguration, string remoteAddress) : 
                base(AgencyBudgetCalcFundsClient.GetBindingForEndpoint(endpointConfiguration), new System.ServiceModel.EndpointAddress(remoteAddress))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public AgencyBudgetCalcFundsClient(EndpointConfiguration endpointConfiguration, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(AgencyBudgetCalcFundsClient.GetBindingForEndpoint(endpointConfiguration), remoteAddress)
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public AgencyBudgetCalcFundsClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress)
        {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<CheckFundService.AgencyBudgetCalcFundsResponse> CheckFundService.AgencyBudgetCalcFunds.AgencyBudgetCalcFundsAsync(CheckFundService.AgencyBudgetCalcFundsRequest request)
        {
            return base.Channel.AgencyBudgetCalcFundsAsync(request);
        }
        
        public System.Threading.Tasks.Task<CheckFundService.AgencyBudgetCalcFundsResponse> AgencyBudgetCalcFundsAsync(CheckFundService.AgencyBudgetCalcFundsRq_Type AgencyBudgetCalcFundsRq)
        {
            CheckFundService.AgencyBudgetCalcFundsRequest inValue = new CheckFundService.AgencyBudgetCalcFundsRequest();
            inValue.AgencyBudgetCalcFundsRq = AgencyBudgetCalcFundsRq;
            return ((CheckFundService.AgencyBudgetCalcFunds)(this)).AgencyBudgetCalcFundsAsync(inValue);
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
            if ((endpointConfiguration == EndpointConfiguration.AgencyBudgetCalcFundsSOAP12))
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
            if ((endpointConfiguration == EndpointConfiguration.AgencyBudgetCalcFundsSOAP11))
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
            if ((endpointConfiguration == EndpointConfiguration.AgencyBudgetCalcFundsSOAP12))
            {
                return new System.ServiceModel.EndpointAddress("http://localhost:7800/AgencyBudgetCalcFunds");
            }
            if ((endpointConfiguration == EndpointConfiguration.AgencyBudgetCalcFundsSOAP11))
            {
                return new System.ServiceModel.EndpointAddress("https://10.14.8.25:7921/AgencyBudgetCalcFunds11");
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        public enum EndpointConfiguration
        {
            
            AgencyBudgetCalcFundsSOAP12,
            
            AgencyBudgetCalcFundsSOAP11,
        }
    }
}
