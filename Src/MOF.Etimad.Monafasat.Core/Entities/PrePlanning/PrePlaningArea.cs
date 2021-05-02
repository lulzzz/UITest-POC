using MOF.Etimad.Monafasat.SharedKernel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("PrePlanningArea", Schema = "PrePlanning")]
    public class PrePlanningArea : AuditableEntity
    {
        #region Fields====================================================
        [Key]
        public int Id { get; private set; }

        [ForeignKey(nameof(Area))]
        public int AreaId { get; private set; }
        public Area Area { private set; get; }

        [ForeignKey(nameof(PrePlanning))]
        public int PrePlanningId { get; private set; }
        public PrePlanning PrePlanning { private set; get; }


        #endregion

        #region NotMapped


        #endregion

        #region Constructors====================================================

        public PrePlanningArea(int areaId)
        {
            AreaId = areaId;
            EntityCreated();
        }

        public PrePlanningArea()
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
