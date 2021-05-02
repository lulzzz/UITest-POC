using MOF.Etimad.Monafasat.SharedKernel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("TenderArea", Schema = "Tender")]
    public class TenderArea : AuditableEntity
    {
        #region Fields====================================================
        [Key]
        public int Id { get; private set; }

        [ForeignKey(nameof(Area))]
        public int AreaId { get; private set; }
        public Area Area { private set; get; }

        [ForeignKey(nameof(Tender))]
        public int TenderId { get; private set; }
        public Tender Tender { private set; get; }


        #endregion

        #region NotMapped


        #endregion

        #region Constructors====================================================

        public TenderArea(int areaId)
        {
            AreaId = areaId;
            EntityCreated();
        }

        public TenderArea()
        {

        }
        #endregion

        #region Methods====================================================
        public void Delete()
        {
            EntityDeleted();
        }
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
        #endregion

    }
}
