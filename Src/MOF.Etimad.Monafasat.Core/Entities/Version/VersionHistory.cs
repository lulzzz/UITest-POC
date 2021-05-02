using MOF.Etimad.Monafasat.SharedKernal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("VersionHistory", Schema = "Versions")]
    public class VersionHistory : BaseEntity
    {
        #region Fields====================================================
        [Key]
        public int Id { get; private set; }

        [StringLength(100)]
        public string Name { get; private set; }
        public bool IsCurrentVersion { get; private set; }

        [ForeignKey(nameof(VersionType))]
        public int VersionTypeId { get; private set; }
        public VersionType VersionType { get; private set; }

        [StringLength(1000)]
        public string Description { get; private set; }
        #endregion

        #region Constructors====================================================

        public VersionHistory()
        {

        }

        public VersionHistory(string name, int versionTypeId, bool isCurrentVersion, string description)
        {
            Name = name;
            VersionTypeId = versionTypeId;
            Description = description;
            IsCurrentVersion = isCurrentVersion;
            EntityCreated();
        }



        #endregion

        #region Methods====================================================
 

        #endregion
    }
}