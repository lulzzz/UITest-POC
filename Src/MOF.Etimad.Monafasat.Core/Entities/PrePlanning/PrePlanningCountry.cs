using MOF.Etimad.Monafasat.SharedKernel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("PrePlanningCountry", Schema = "PrePlanning")]
    public class PrePlanningCountry : AuditableEntity
    {
        #region Fields====================================================
        [Key]
        public int Id { get; private set; }

        [ForeignKey(nameof(Country))]
        public int CountryId { get; private set; }
        public Country Country { private set; get; }

        [ForeignKey(nameof(PrePlanning))]
        public int PrePlanningId { get; private set; }
        public PrePlanning PrePlanning { private set; get; }


        #endregion

        #region NotMapped


        #endregion

        #region Constructors====================================================

        public PrePlanningCountry(int countryId)
        {
            CountryId = countryId;
            EntityCreated();
        }

        public PrePlanningCountry()
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
