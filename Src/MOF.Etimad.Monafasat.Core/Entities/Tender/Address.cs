using MOF.Etimad.Monafasat.SharedKernal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("Address", Schema = "Tender")]
    public class Address : BaseEntity
    {
        #region Fields
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AddressId { get; private set; }

        [StringLength(1024)]
        public string AddressName { get; private set; }

        public int AddressTypeId { get; private set; }
        #endregion
        [ForeignKey(nameof(Branch))]
        public int? BranchId { get; private set; }
        public Branch Branch { get; private set; }
        #region Constractore
        public Address() { }
        public Address(string addressName, int addressTypeId, int branchId)
        {
            AddressName = addressName;
            AddressTypeId = addressTypeId;
            BranchId = branchId;
            State = ObjectState.Added;
        }
        #endregion

        #region Methods

        #endregion
    }
}
