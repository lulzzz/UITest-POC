using MOF.Etimad.Monafasat.SharedKernel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("TenderConstructionWork", Schema = "Tender")]
    public class TenderConstructionWork : AuditableEntity
    {
        #region Fields====================================================
        [Key]
        public int Id { get; private set; }
        [ForeignKey("ConstructionWork")]

        public int ConstructionWorkId { private set; get; }

        public ConstructionWork ConstructionWork { private set; get; }
        [ForeignKey("Tender")]
        public int TenderId { get; private set; }
        public Tender Tender { private set; get; }

        #endregion

        #region NotMapped

        #endregion

        #region Constructors====================================================

        public TenderConstructionWork()
        {

        }

        public TenderConstructionWork(int constructionWorkId)
        {
            ConstructionWorkId = constructionWorkId;
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
            Id = 0;
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
