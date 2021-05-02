using MOF.Etimad.Monafasat.SharedKernel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("FavouriteSupplierTenders", Schema = "Tender")]
    public class FavouriteSupplierTender : AuditableEntity
    {
        #region Fields
        [ForeignKey(nameof(Tender))]
        public int TenderId { get; private set; }

        [StringLength(25)]
        public string SupplierCRNumber { get; private set; }

        public Tender Tender { get; private set; }

        #endregion

        #region Constructors

        public FavouriteSupplierTender()
        {

        }

        public FavouriteSupplierTender(string SupplierCRNumber)
        {
            this.SupplierCRNumber = SupplierCRNumber;
            EntityCreated();
        }

        public FavouriteSupplierTender(int TenderId, string SupplierCRNumber)
        {
            this.SupplierCRNumber = SupplierCRNumber;
            this.TenderId = TenderId;
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
