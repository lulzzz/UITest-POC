using FileNetDocDownloadService;

namespace MOF.Etimad.Monafasat.Integration
{
    public class FileNetDocDownloadBody
    {
        public UserCredentials_Type userCredentialsField { get; set; }

        public string RepositoryIdField { get; set; }

        public string FileIdField { get; set; }
    }
}
