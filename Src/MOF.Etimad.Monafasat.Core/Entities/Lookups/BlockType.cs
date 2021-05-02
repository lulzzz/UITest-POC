using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core
{


    [Table("BlockStatus", Schema = "LookUps")]
    public class BlockStatus
    {
        public BlockStatus(int blockStatusId, string blockStatusName, string blockStatusNameAr)
        {
            BlockStatusId = blockStatusId;
            BlockStatusName = blockStatusName;
            BlockStatusNameAr = blockStatusNameAr;
        }

        #region Fields====================================================
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BlockStatusId { get; set; }

        [StringLength(200)]
        public string BlockStatusName { get; set; }
        [StringLength(200)]
        public string BlockStatusNameAr { get; set; }

        #endregion

        #region NotMapped


        #endregion
    }

    [Table("BlockType", Schema = "LookUps")]
    public class BlockType
    {
        public BlockType(int blockTypeId, string nameEn, string nameAr)
        {
            BlockTypeId = blockTypeId;
            NameAr = nameAr;
            NameEn = nameEn;
        }

        #region Fields====================================================
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BlockTypeId { get; set; }

        [StringLength(200)]
        public string NameEn { get; set; }

        [StringLength(200)]
        public string NameAr { get; set; }

        #endregion

        #region NotMapped


        #endregion
    }
}
