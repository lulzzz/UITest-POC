﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MOF.Etimad.Monafasat.Integration.FileNetCheckIn {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://www.ejada.com", ConfigurationName="FileNetCheckIn.FileNetCheckIn")]
    public interface FileNetCheckIn {
        
        // CODEGEN: Generating message contract since the operation FileNetCheckIn is neither RPC nor document wrapped.
        [System.ServiceModel.OperationContractAttribute(Action="FileNetCheckIn", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        Integration.FileNetCheckIn.FileNetCheckInResponse FileNetCheckIn(Integration.FileNetCheckIn.FileNetCheckInRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="FileNetCheckIn", ReplyAction="*")]
        System.Threading.Tasks.Task<Integration.FileNetCheckIn.FileNetCheckInResponse> FileNetCheckInAsync(Integration.FileNetCheckIn.FileNetCheckInRequest request);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.3056.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.ejada.com")]
    public partial class FileNetCheckInRq_Type : object, System.ComponentModel.INotifyPropertyChanged {
        
        private MsgRqHdr_Type msgRqHdrField;
        
        private FileNetCheckInRqBody_Type bodyField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public MsgRqHdr_Type MsgRqHdr {
            get {
                return this.msgRqHdrField;
            }
            set {
                this.msgRqHdrField = value;
                this.RaisePropertyChanged("MsgRqHdr");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public FileNetCheckInRqBody_Type Body {
            get {
                return this.bodyField;
            }
            set {
                this.bodyField = value;
                this.RaisePropertyChanged("Body");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.3056.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.ejada.com")]
    public partial class MsgRqHdr_Type : object, System.ComponentModel.INotifyPropertyChanged {
        
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
        public string RqUID {
            get {
                return this.rqUIDField;
            }
            set {
                this.rqUIDField = value;
                this.RaisePropertyChanged("RqUID");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string ProcessUID {
            get {
                return this.processUIDField;
            }
            set {
                this.processUIDField = value;
                this.RaisePropertyChanged("ProcessUID");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public string SessionId {
            get {
                return this.sessionIdField;
            }
            set {
                this.sessionIdField = value;
                this.RaisePropertyChanged("SessionId");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=3)]
        public string SCId {
            get {
                return this.sCIdField;
            }
            set {
                this.sCIdField = value;
                this.RaisePropertyChanged("SCId");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=4)]
        public string ServiceId {
            get {
                return this.serviceIdField;
            }
            set {
                this.serviceIdField = value;
                this.RaisePropertyChanged("ServiceId");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=5)]
        public string FuncId {
            get {
                return this.funcIdField;
            }
            set {
                this.funcIdField = value;
                this.RaisePropertyChanged("FuncId");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=6)]
        public long RqMode {
            get {
                return this.rqModeField;
            }
            set {
                this.rqModeField = value;
                this.RaisePropertyChanged("RqMode");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool RqModeSpecified {
            get {
                return this.rqModeFieldSpecified;
            }
            set {
                this.rqModeFieldSpecified = value;
                this.RaisePropertyChanged("RqModeSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=7)]
        public PartyId_Type PartyId {
            get {
                return this.partyIdField;
            }
            set {
                this.partyIdField = value;
                this.RaisePropertyChanged("PartyId");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=8)]
        public string UserId {
            get {
                return this.userIdField;
            }
            set {
                this.userIdField = value;
                this.RaisePropertyChanged("UserId");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=9)]
        public string ProxyUserId {
            get {
                return this.proxyUserIdField;
            }
            set {
                this.proxyUserIdField = value;
                this.RaisePropertyChanged("ProxyUserId");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=10)]
        public string ClientDt {
            get {
                return this.clientDtField;
            }
            set {
                this.clientDtField = value;
                this.RaisePropertyChanged("ClientDt");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=11)]
        public string EchoData {
            get {
                return this.echoDataField;
            }
            set {
                this.echoDataField = value;
                this.RaisePropertyChanged("EchoData");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=12)]
        public string Version {
            get {
                return this.versionField;
            }
            set {
                this.versionField = value;
                this.RaisePropertyChanged("Version");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.3056.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.ejada.com")]
    public partial class PartyId_Type : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string partyIdNumField;
        
        private PartyIdType_Type partyIdTypeField;
        
        private bool partyIdTypeFieldSpecified;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string PartyIdNum {
            get {
                return this.partyIdNumField;
            }
            set {
                this.partyIdNumField = value;
                this.RaisePropertyChanged("PartyIdNum");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public PartyIdType_Type PartyIdType {
            get {
                return this.partyIdTypeField;
            }
            set {
                this.partyIdTypeField = value;
                this.RaisePropertyChanged("PartyIdType");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool PartyIdTypeSpecified {
            get {
                return this.partyIdTypeFieldSpecified;
            }
            set {
                this.partyIdTypeFieldSpecified = value;
                this.RaisePropertyChanged("PartyIdTypeSpecified");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.3056.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.ejada.com")]
    public enum PartyIdType_Type {
        
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.3056.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.ejada.com")]
    public partial class FileNetCheckInRsBody_Type : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string fileIdField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string FileId {
            get {
                return this.fileIdField;
            }
            set {
                this.fileIdField = value;
                this.RaisePropertyChanged("FileId");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.3056.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.ejada.com")]
    public partial class ResponseStatus_Type : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string statusCodeField;
        
        private string statusDescField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string StatusCode {
            get {
                return this.statusCodeField;
            }
            set {
                this.statusCodeField = value;
                this.RaisePropertyChanged("StatusCode");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string StatusDesc {
            get {
                return this.statusDescField;
            }
            set {
                this.statusDescField = value;
                this.RaisePropertyChanged("StatusDesc");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.3056.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.ejada.com")]
    public partial class OverrideStatus_Type : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string codeField;
        
        private string descField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string Code {
            get {
                return this.codeField;
            }
            set {
                this.codeField = value;
                this.RaisePropertyChanged("Code");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string Desc {
            get {
                return this.descField;
            }
            set {
                this.descField = value;
                this.RaisePropertyChanged("Desc");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.3056.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.ejada.com")]
    public partial class MsgRsHdr_Type : object, System.ComponentModel.INotifyPropertyChanged {
        
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
        public string RqUID {
            get {
                return this.rqUIDField;
            }
            set {
                this.rqUIDField = value;
                this.RaisePropertyChanged("RqUID");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string ProcessUID {
            get {
                return this.processUIDField;
            }
            set {
                this.processUIDField = value;
                this.RaisePropertyChanged("ProcessUID");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public string SessionId {
            get {
                return this.sessionIdField;
            }
            set {
                this.sessionIdField = value;
                this.RaisePropertyChanged("SessionId");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=3)]
        public string SPRefNum {
            get {
                return this.sPRefNumField;
            }
            set {
                this.sPRefNumField = value;
                this.RaisePropertyChanged("SPRefNum");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=4)]
        public string MsgRecDt {
            get {
                return this.msgRecDtField;
            }
            set {
                this.msgRecDtField = value;
                this.RaisePropertyChanged("MsgRecDt");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=5)]
        public OverrideStatus_Type OverrideStatus {
            get {
                return this.overrideStatusField;
            }
            set {
                this.overrideStatusField = value;
                this.RaisePropertyChanged("OverrideStatus");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=6)]
        public ResponseStatus_Type ResponseStatus {
            get {
                return this.responseStatusField;
            }
            set {
                this.responseStatusField = value;
                this.RaisePropertyChanged("ResponseStatus");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=7)]
        public string EchoData {
            get {
                return this.echoDataField;
            }
            set {
                this.echoDataField = value;
                this.RaisePropertyChanged("EchoData");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.3056.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.ejada.com")]
    public partial class FileNetCheckInRs_Type : object, System.ComponentModel.INotifyPropertyChanged {
        
        private MsgRsHdr_Type msgRsHdrField;
        
        private FileNetCheckInRsBody_Type bodyField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public MsgRsHdr_Type MsgRsHdr {
            get {
                return this.msgRsHdrField;
            }
            set {
                this.msgRsHdrField = value;
                this.RaisePropertyChanged("MsgRsHdr");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public FileNetCheckInRsBody_Type Body {
            get {
                return this.bodyField;
            }
            set {
                this.bodyField = value;
                this.RaisePropertyChanged("Body");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.3056.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.ejada.com")]
    public partial class UserCredentials_Type : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string userIdField;
        
        private string passwordField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string UserId {
            get {
                return this.userIdField;
            }
            set {
                this.userIdField = value;
                this.RaisePropertyChanged("UserId");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string Password {
            get {
                return this.passwordField;
            }
            set {
                this.passwordField = value;
                this.RaisePropertyChanged("Password");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.3056.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.ejada.com")]
    public partial class FileNetCheckInRqBody_Type : object, System.ComponentModel.INotifyPropertyChanged {
        
        private UserCredentials_Type userCredentialsField;
        
        private string repositoryIdField;
        
        private string fileIdField;
        
        private byte[] fileContentField;
        
        private string mIMETypeField;
        
        private Flg_Type majorUpdateField;
        
        private bool majorUpdateFieldSpecified;
        
        private string remarksField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public UserCredentials_Type UserCredentials {
            get {
                return this.userCredentialsField;
            }
            set {
                this.userCredentialsField = value;
                this.RaisePropertyChanged("UserCredentials");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string RepositoryId {
            get {
                return this.repositoryIdField;
            }
            set {
                this.repositoryIdField = value;
                this.RaisePropertyChanged("RepositoryId");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public string FileId {
            get {
                return this.fileIdField;
            }
            set {
                this.fileIdField = value;
                this.RaisePropertyChanged("FileId");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType="base64Binary", Order=3)]
        public byte[] FileContent {
            get {
                return this.fileContentField;
            }
            set {
                this.fileContentField = value;
                this.RaisePropertyChanged("FileContent");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=4)]
        public string MIMEType {
            get {
                return this.mIMETypeField;
            }
            set {
                this.mIMETypeField = value;
                this.RaisePropertyChanged("MIMEType");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=5)]
        public Flg_Type MajorUpdate {
            get {
                return this.majorUpdateField;
            }
            set {
                this.majorUpdateField = value;
                this.RaisePropertyChanged("MajorUpdate");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool MajorUpdateSpecified {
            get {
                return this.majorUpdateFieldSpecified;
            }
            set {
                this.majorUpdateFieldSpecified = value;
                this.RaisePropertyChanged("MajorUpdateSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=6)]
        public string Remarks {
            get {
                return this.remarksField;
            }
            set {
                this.remarksField = value;
                this.RaisePropertyChanged("Remarks");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.3056.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.ejada.com")]
    public enum Flg_Type {
        
        /// <remarks/>
        Y,
        
        /// <remarks/>
        N,
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class FileNetCheckInRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.ejada.com", Order=0)]
        public Integration.FileNetCheckIn.FileNetCheckInRq_Type FileNetCheckInRq;
        
        public FileNetCheckInRequest() {
        }
        
        public FileNetCheckInRequest(Integration.FileNetCheckIn.FileNetCheckInRq_Type FileNetCheckInRq) {
            this.FileNetCheckInRq = FileNetCheckInRq;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class FileNetCheckInResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.ejada.com", Order=0)]
        public Integration.FileNetCheckIn.FileNetCheckInRs_Type FileNetCheckInRs;
        
        public FileNetCheckInResponse() {
        }
        
        public FileNetCheckInResponse(Integration.FileNetCheckIn.FileNetCheckInRs_Type FileNetCheckInRs) {
            this.FileNetCheckInRs = FileNetCheckInRs;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface FileNetCheckInChannel : Integration.FileNetCheckIn.FileNetCheckIn, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class FileNetCheckInClient : System.ServiceModel.ClientBase<Integration.FileNetCheckIn.FileNetCheckIn>, Integration.FileNetCheckIn.FileNetCheckIn
    {
        
        public FileNetCheckInClient() {
        }
        
        public FileNetCheckInClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public FileNetCheckInClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public FileNetCheckInClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public FileNetCheckInClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public Integration.FileNetCheckIn.FileNetCheckInResponse FileNetCheckIn(Integration.FileNetCheckIn.FileNetCheckInRequest request) {
            return base.Channel.FileNetCheckIn(request);
        }
        
        public Integration.FileNetCheckIn.FileNetCheckInRs_Type FileNetCheckIn(Integration.FileNetCheckIn.FileNetCheckInRq_Type FileNetCheckInRq) {
            Integration.FileNetCheckIn.FileNetCheckInRequest inValue = new Integration.FileNetCheckIn.FileNetCheckInRequest();
            inValue.FileNetCheckInRq = FileNetCheckInRq;
            Integration.FileNetCheckIn.FileNetCheckInResponse retVal = ((Integration.FileNetCheckIn.FileNetCheckIn)(this)).FileNetCheckIn(inValue);
            return retVal.FileNetCheckInRs;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<Integration.FileNetCheckIn.FileNetCheckInResponse> Integration.FileNetCheckIn.FileNetCheckIn.FileNetCheckInAsync(Integration.FileNetCheckIn.FileNetCheckInRequest request) {
            return base.Channel.FileNetCheckInAsync(request);
        }
        
        public System.Threading.Tasks.Task<Integration.FileNetCheckIn.FileNetCheckInResponse> FileNetCheckInAsync(Integration.FileNetCheckIn.FileNetCheckInRq_Type FileNetCheckInRq) {
            Integration.FileNetCheckIn.FileNetCheckInRequest inValue = new Integration.FileNetCheckIn.FileNetCheckInRequest();
            inValue.FileNetCheckInRq = FileNetCheckInRq;
            return ((Integration.FileNetCheckIn.FileNetCheckIn)(this)).FileNetCheckInAsync(inValue);
        }
    }
}
