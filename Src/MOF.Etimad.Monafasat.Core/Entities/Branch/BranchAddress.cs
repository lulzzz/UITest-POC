using MOF.Etimad.Monafasat.SharedKernel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("BranchAddresse", Schema = "Branch")]
    public class BranchAddress : AuditableEntity
    {

        public BranchAddress() { }
        public BranchAddress(int addressTypeId, string position, string address, string phone, string fax, string phone2, string fax2, string email, string description, string addressName, string cityCode, string postalCode, string zipCode, bool? isActive = true)
        {
            Position = position;
            Address = address;
            Phone = phone;
            Fax = fax;
            Phone2 = phone2;
            Fax2 = fax2;
            Email = email;
            Description = description;
            AddressName = addressName;
            CityCode = cityCode;
            PostalCode = postalCode;
            ZipCode = zipCode;
            IsActive = isActive;
            AddressTypeId = addressTypeId;
            EntityCreated();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BranchAddressId { get; private set; }
        [StringLength(1024)]
        public string Position { get; private set; }
        [StringLength(1024)]
        public string Address { get; private set; }
        [StringLength(100)]
        public string Phone { get; private set; }
        [StringLength(100)]
        public string Fax { get; private set; }
        [StringLength(100)]
        public string Phone2 { get; private set; }
        [StringLength(100)]
        public string Fax2 { get; private set; }
        [StringLength(200)]
        public string Email { get; private set; }
        [StringLength(1024)]
        public string Description { get; private set; }
        [StringLength(1024)]
        public string AddressName { get; private set; }
        [StringLength(1024)]
        public string CityCode { get; private set; }
        [StringLength(1024)]
        public string PostalCode { get; private set; }
        [StringLength(1024)]
        public string ZipCode { get; private set; }

        [ForeignKey(nameof(AddressType))]
        public int AddressTypeId { get; private set; }
        public AddressType AddressType { get; private set; }

        #region Methods
        public void DeActive()
        {
            IsActive = false;
            EntityUpdated();
        }
        #endregion
    }
}
