using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("MaintenanceRunningWork", Schema = "LookUps")]
    public class MaintenanceRunningWork
    {
        #region Fields====================================================
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaintenanceRunningWorkId { get; private set; }
        [StringLength(1024)]
        public string NameEn { get; private set; }

        [Required]
        [StringLength(1024)]
        public string NameAr { get; private set; }
        public List<TenderMaintenanceRunnigWork> TenderMentainanceRunnigWorks { set; get; }


        #endregion

        #region NotMapped


        #endregion


    }
}