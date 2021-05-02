using MOF.Etimad.Monafasat.SharedKernal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("TenderAddress", Schema = "Tender")]
    public class TenderAddress : BaseEntity
    {
        #region Fields====================================================
        [Key]
        public int Id { get; private set; }

        [Required]
        [ForeignKey(nameof(Tender))]
        public int TenderId { get; private set; }
        public Tender Tender { get; private set; }
        public bool? IsSampleAddresSameOffersAddress { get; private set; }

        [StringLength(200)]
        public string OffersDeliveryAddress { get; private set; }
        [StringLength(100)]
        public string OfferBuildingName { get; private set; }
        [StringLength(100)]
        public string OfferFloarNumber { get; private set; }
        [StringLength(100)]
        public string OfferDepartmentName { get; private set; }
        #endregion

        #region Constructors====================================================

        public TenderAddress()
        {
        }

        public TenderAddress(bool? isSampleAddresSameOffersAddress, string offersDeliveryAddress, string buildingName, string floarNumber, string departmentName)
        {
            IsSampleAddresSameOffersAddress = isSampleAddresSameOffersAddress;
            OffersDeliveryAddress = offersDeliveryAddress;
            OfferBuildingName = buildingName;
            OfferFloarNumber = floarNumber;
            OfferDepartmentName = departmentName;
            EntityCreated();
        }



        #endregion

        #region Methods====================================================

        public TenderAddress UpdateTenderAddress(bool? isSampleAddresSameOffersAddress, string offersDeliveryAddress, string buildingName, string floarNumber, string departmentName)
        {
            IsSampleAddresSameOffersAddress = isSampleAddresSameOffersAddress;
            OffersDeliveryAddress = offersDeliveryAddress;
            OfferBuildingName = buildingName;
            OfferFloarNumber = floarNumber;
            OfferDepartmentName = departmentName;
            EntityUpdated();
            return this;
        }

        #endregion
    }
}