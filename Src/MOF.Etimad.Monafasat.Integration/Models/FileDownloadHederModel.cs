using FileNetDocDownloadService;
namespace MOF.Etimad.Monafasat.Integration
{
    public class FileDownloadHederModel
    {
        public string RqUIDField { get; set; }

        public string ProcessUIDField { get; set; }

        public string SessionIdField { get; set; }

        public string SCIdField { get; set; }

        public string ServiceIdField { get; set; }

        public string FuncIdField { get; set; }

        public long RqModeField { get; set; }

        public bool RqModeFieldSpecified { get; set; }

        public PartyId_Type partyIdField { get; set; }

        public string UserIdField { get; set; }

        public string ProxyUserIdField { get; set; }

        public string ClientDtField { get; set; }

        public string EchoDataField { get; set; }

        public string VersionField { get; set; }
    }
}
