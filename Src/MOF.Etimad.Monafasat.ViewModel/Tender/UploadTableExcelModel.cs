namespace MOF.Etimad.Monafasat.ViewModel
{
    public class UploadTableExcelModel
    {
        public string RepositoryIdField { get; set; }

        public string FileNameField { get; set; }

        public byte[] FileContentField { get; set; }

        public string MIMETypeField { get; set; }

        public string FileLengthField { get; set; }

        public string FileVersionField { get; set; }

        public string ClassField { get; set; }

        public string FolderIdField { get; set; }

        public long TableId { get; set; }

        public int FormId { get; set; }

        public string TableName { get; set; }

        public int TenderId { get; set; }

        public int Years { get; set; }

        public bool? IsNewTable { get; set; }

    }
}
