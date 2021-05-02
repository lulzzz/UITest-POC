using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    public class SupplierChamberJoiningAttachment : SupplierAttachment
    {
        [Required]
        public bool IsChamberJoiningAttached { get; private set; }
        [Required]
        public bool IsChamberJoiningValid { get; private set; }
    }
}
