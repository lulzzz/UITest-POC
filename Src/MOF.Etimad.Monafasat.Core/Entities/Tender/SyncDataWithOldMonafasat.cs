using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("SyncDataWithOldMonafasat", Schema = "Tender")]
    public class SyncDataWithOldMonafasat
    {
        public SyncDataWithOldMonafasat()
        {

        }

        public SyncDataWithOldMonafasat(int tenderId, string requestInformation, bool isSendSuccessfully)
        {
            this.TenderId = tenderId;
            this.RequestInformation = requestInformation;
            this.IsSendSuccessfully = isSendSuccessfully;
        }

        public int SyncDataWithOldMonafasatId { get; private set; }
        public int TenderId { get; private set; }
        public string RequestInformation { get; private set; }
        public bool IsSendSuccessfully { get; private set; }
    }
}
