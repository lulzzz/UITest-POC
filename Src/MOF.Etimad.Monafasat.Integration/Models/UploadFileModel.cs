namespace MOF.Etimad.Monafasat.Integration
{
    public class UploadFileModel
    {
        public string RepositoryIdField { get; set; }
        public string FileNameField { get; set; }
        public byte[] FileContentField { get; set; }
        public string MIMETypeField { get; set; }
        public string FileLengthField { get; set; }
        public string FileVersionField { get; set; }
        public string ClassField { get; set; }
        public string FolderIdField { get; set; }
        public FileNetDocUploadService.FileProperty_Type[] FilePropertyListField { get; set; }
    }
}
