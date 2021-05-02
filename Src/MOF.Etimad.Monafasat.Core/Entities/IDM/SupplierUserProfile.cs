using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernel;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core
{
    [Table("SupplierUserProfile", Schema = "IDM")]
    public class SupplierUserProfile : AuditableEntity
    {
        #region Properties


        //[Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string SelectedCr { get; private set; }
        [ForeignKey("SelectedCr")]
        public Supplier Supplier { get; private set; }
        public int UserProfileId { get; private set; }
        [ForeignKey("UserProfileId")]
        public UserProfile UserProfile { get; private set; }

        #endregion

        #region Constructors

        public SupplierUserProfile()
        {
        }

        public SupplierUserProfile(string selectedCr, int userProfileId)
        {
            SelectedCr = selectedCr;
            UserProfileId = userProfileId;
            EntityCreated();
        }

        #endregion
    }
}
