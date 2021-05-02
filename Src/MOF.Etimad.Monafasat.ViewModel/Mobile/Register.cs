namespace MOF.Etimad.Monafasat.ViewModel.Mobile
{


    public class RegisterTokenModel
    {
        public string deviceToken { get; set; }
        public string deviceVerion { get; set; }
        public string deviceName { get; set; }
        public string model { get; set; }
        public string udid { get; set; }

    }

    public class StatusModel
    {
        public StatusModel()
        {

        }
        public StatusModel(string status)
        {
            this.status = status;
        }
        public string status { get; set; }
    }
}
