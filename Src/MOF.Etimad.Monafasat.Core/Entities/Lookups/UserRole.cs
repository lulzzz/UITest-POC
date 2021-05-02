using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("UserRole", Schema = "LookUps")]
    public class UserRole
    {
        #region Fields====================================================

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserRoleId { get; private set; }

        [StringLength(1024)]
        public string Name { get; private set; }

        [StringLength(1024)]
        public string DisplayNameAr { get; private set; }

        [StringLength(1024)]
        public string DisplayNameEn { get; private set; }
        public bool IsCombinedRole { get; set; } = false;
        #endregion

        #region NotMapped


        #endregion
    }

}