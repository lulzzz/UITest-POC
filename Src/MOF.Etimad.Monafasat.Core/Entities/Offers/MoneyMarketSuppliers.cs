using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("MoneyMarketSuppliers", Schema = "Offer")]
    public class MoneyMarketSuppliers
    {
        [Key]
        public int Id { get; set; }
        [StringLength(1024)]
        public string SupplierCr { get; set; }
        [StringLength(1024)]
        public string SupplierName { get; set; }

        
    }
}
