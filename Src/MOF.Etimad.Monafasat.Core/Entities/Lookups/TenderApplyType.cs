using MOF.Etimad.Monafasat.SharedKernel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("TenderApplyType", Schema = "LookUps")]
    public class TenderApplyType 
    {
        #region Fields====================================================
        [Key]
        [Required]
        public int TenderApplyTypeId { get; private set; }

        [StringLength(1024)]
        public string Name { get; private set; }

       
        public List<Branch> Branches { private set; get; }
        #endregion

        #region Constructors====================================================

        public TenderApplyType()
        {

        }

        
        #endregion

        #region Methods====================================================


        
        #endregion
    }
}