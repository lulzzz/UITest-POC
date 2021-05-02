using MOF.Etimad.Monafasat.SharedKernel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("RejectionHistory", Schema = "Tender")]
    public class RejectionHistory : AuditableEntity
    {
        #region Fields====================================================
        [Key]
        public int RejectionHistoryId { get; private set; }
        [Required]
        public int ReuestId { get; private set; }
        [Required]
        [ForeignKey(nameof(RequestsRejectionType))]
        public int RequestsRejectionTypeId { get; private set; }
        public RequestsRejectionType RequestsRejectionType { get; private set; }
        [StringLength(2000)]
        public string RejectionReason { get; private set; }
        public int? StatusId { get; private set; }
        #endregion

        #region Constructors====================================================

        public RejectionHistory()
        {

        }

        public RejectionHistory(int reuestId, int requestsRejectionTypeId, int statusId, string rejectionReason = "")
        {
            ReuestId = reuestId;
            StatusId = statusId;
            RequestsRejectionTypeId = requestsRejectionTypeId;
            RejectionReason = rejectionReason;
            EntityCreated();
        }
        #endregion
    }
}