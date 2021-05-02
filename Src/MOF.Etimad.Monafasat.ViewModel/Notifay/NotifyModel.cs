namespace MOF.Etimad.Monafasat.ViewModel
{
    public class NotifyModel
    {
        public string Title { get; set; }
        public int SentCount { get; set; }
        public int FailedToSendCount { get; set; }
        public int UnsentCount { get; set; }
        public string StartDateTime { get; set; }
        public string StartDateTimeHijri { get; set; }
        public string EndDateTime { get; set; }
        public string EndDateTimeHijri { get; set; }

    }
}
