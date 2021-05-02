using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class DeviceTokenModel
    {
        public string UserName { set; get; }

        [MaxLength(500)]
        public string DeviceTokenValue { get; set; }

        [MaxLength(15)]
        public string DeviceVersion { get; set; }

        [MaxLength(100)]
        public string DeviceName { get; set; }

        [MaxLength(30)]
        public string Model { get; set; }

        [MaxLength(60)]
        public string UserDeviceId { get; set; }

        public int TimeStamp { get; set; }

        [MaxLength(20)]
        public string SourceIP { get; set; }

        public int Android { get; set; }
    }
}
