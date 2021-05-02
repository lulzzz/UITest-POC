using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class AddressModel
    {
        public int AddressId { get; set; }
        [StringLength(1024)]
        public string AddressName { get; set; }
        public int AddressTypeId { get; set; }
    }
}
