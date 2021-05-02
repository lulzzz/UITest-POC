using MOF.Etimad.Monafasat.SharedKernel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("FavouriteSupplierLists", Schema = "Supplier")]
    public class FavouriteSupplierList : AuditableEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int FavouriteSupplierListId { get; private set; }
        public string Name { get; set; }
        public string AgencyCode { get; private set; }
        [ForeignKey(nameof(AgencyCode))]
        public GovAgency Agency { get; set; }

        public int BranchId { get; private set; }
        [ForeignKey(nameof(BranchId))]
        public Branch Branch { get; private set; }

        public List<FavouriteSupplier> FavouriteSuppliers { get; set; }



        #region Constructors
        public FavouriteSupplierList()
        {

        }

        public FavouriteSupplierList(string name, string AgencyCode, int branchId)
        {
            this.Name = name;
            this.AgencyCode = AgencyCode;
            this.BranchId = branchId;
            EntityCreated();
        }
        #endregion

        #region Methods

        public void DeActive()
        {
            IsActive = false;
            EntityUpdated();
        }
        #endregion
    }
}
