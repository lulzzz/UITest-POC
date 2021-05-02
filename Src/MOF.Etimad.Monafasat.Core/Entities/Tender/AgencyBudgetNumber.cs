using MOF.Etimad.Monafasat.SharedKernal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("AgencyBudgetNumber", Schema = "Tender")]
    public class AgencyBudgetNumber : BaseEntity
    {
        #region Fields
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AgencyBudgetNumberId { get; private set; }

        [ForeignKey(nameof(TenderId))]
        public int? TenderId { get; private set; }
        public Tender Tender { get; private set; }

        public string ProjectNumber { get; private set; }
        public string ProjectName { get; private set; }

        public decimal Cache { get; private set; }
        public decimal Cost { get; private set; }
        public bool? IsProject { get; private set; }
        #endregion

        #region Constractore
        public AgencyBudgetNumber() { }
        public void Delete()
        {
            EntityDeleted();
        }
        public AgencyBudgetNumber(int tenderId, string projectNumber, string projectName, decimal cost, decimal cache, bool? isProject)
        {
            TenderId = tenderId;
            ProjectNumber = projectNumber;
            ProjectName = projectName;
            Cost = cost;
            Cache = cache;
            IsProject = isProject;
            EntityCreated();
        }
        #endregion

        #region Methods

        #endregion
    }
}
