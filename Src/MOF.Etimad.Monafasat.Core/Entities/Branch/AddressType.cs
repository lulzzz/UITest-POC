using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("AddressType", Schema = "LookUps")]
    public class AddressType
    {
        public int AddressTypeId { get; set; }
        public string AddressTypeName { get; set; }
    }
}
