using MOF.Etimad.Monafasat.SharedKernel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("FavouriteSuppliers", Schema = "Supplier")]
    public class FavouriteSupplier : AuditableEntity
    {
        #region Fields

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int FavouriteSupplierId { get; private set; }

        [StringLength(20)]
        [ForeignKey(nameof(Supplier))]
        public string SupplierCRNumber { get; private set; }

        [ForeignKey(nameof(FavouriteSupplierList))]
        public int FavouriteSupplierListId { get; private set; }


        public Supplier Supplier { get; set; }
        public FavouriteSupplierList FavouriteSupplierList { get; set; }

        #endregion

        #region Constructors

        public FavouriteSupplier()
        {

        }

        public FavouriteSupplier(string supplierCRNumber, int favouriteSupplierListId)
        {
            this.SupplierCRNumber = supplierCRNumber;
            this.FavouriteSupplierListId = favouriteSupplierListId;
            EntityCreated();
        }

        #endregion

        #region Methods

        public void DeActive()
        {
            IsActive = false;
            EntityUpdated();
        }

        public void SetActive()
        {
            IsActive = true;
            EntityUpdated();
        }

        #endregion
    }
}
