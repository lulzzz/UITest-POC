using MOF.Etimad.Monafasat.SharedKernel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static MOF.Etimad.Monafasat.SharedKernel.Enums;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("TenderHistory", Schema = "Tender")]
    public class TenderHistory : AuditableEntity
    {
        #region Fields====================================================
        [Key]
        public int TenderHistoryId { get; private set; }
        [Required]
        public int UserId { get; private set; }
        [Required]
        [ForeignKey(nameof(Tender))]
        public int TenderId { get; private set; }
        public Tender Tender { get; private set; }
        [ForeignKey(nameof(Status))]
        public int StatusId { get; private set; }
        public TenderStatus Status { get; private set; }
        [StringLength(2000)]
        public string RejectionReason { get; private set; }

        [ForeignKey(nameof(Action))]
        public int ActionId { get; set; }
        public TenderAction Action { get; set; }

        #endregion



        #region Constructors====================================================

        public TenderHistory()
        {

        }

        public TenderHistory(int userId, int statusId, TenderActions action, string rejectionReason = "")
        {
            UserId = userId;
            StatusId = statusId;
            ActionId = (int)action;
            RejectionReason = rejectionReason;
            EntityCreated();
        }

        public TenderHistory(int userId, int statusId, TenderActions action, int tenderId, string rejectionReason = "")
        {
            UserId = userId;
            StatusId = statusId;
            ActionId = (int)action;
            Action = null;
            RejectionReason = rejectionReason;
            TenderId = tenderId;
            EntityCreated();
        }


        #endregion

        #region Methods====================================================

        public void UpdateStatus(string rejectionReason)
        {
            this.RejectionReason = rejectionReason;
        }
        #endregion
    }
}