using MOF.Etimad.Monafasat.SharedKernel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("TenderActivity", Schema = "Tender")]
    public class TenderActivity : AuditableEntity
    {
        #region Fields====================================================
        [Key]
        public int TenderActivityId { get; private set; }
        [ForeignKey(nameof(ActivityId))]

        public int ActivityId { private set; get; }
        public virtual Activity Activity { private set; get; }
        public int TenderId { private set; get; }
        [ForeignKey(nameof(TenderId))]
        public Tender Tender { private set; get; }

        #endregion

        #region NotMapped


        #endregion

        #region Constructors====================================================
        public TenderActivity()
        {

        }

        public TenderActivity(int activityId/*,description*/)
        {
            ActivityId = activityId;
            EntityCreated();
        }
        #endregion

        #region Methods====================================================
        public void Update()
        {

            EntityUpdated();
        }
        public void DeActive()
        {
            IsActive = false;
            EntityUpdated();
        }
        public void SetAddedState()
        {
            TenderActivityId = 0;
            EntityCreated();
        }
        public void SetActive()
        {
            IsActive = true;
            EntityUpdated();
        }

        internal void Delete()
        {
            EntityDeleted();
        }
        #endregion

    }
}
