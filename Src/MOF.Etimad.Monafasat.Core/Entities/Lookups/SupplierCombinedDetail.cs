using MOF.Etimad.Monafasat.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("SupplierCombinedDetail", Schema = "Offer")]
    public class SupplierCombinedDetail : AuditableEntity
    {
        //////////////  Offer Details That moved from offer entity   ////////////////////////
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CombinedDetailId { get; private set; }
        public int CombinedId { get; set; }
        [ForeignKey("CombinedId")]
        public OfferSolidarity Combined { get; set; }
        public bool? IsChamberJoiningAttached { get; private set; }
        public bool? IsChamberJoiningValid { get; private set; }
        public bool? IsCommercialRegisterAttached { get; private set; }
        public bool? IsCommercialRegisterValid { get; private set; }
        public bool? IsSaudizationAttached { get; private set; }
        public bool? IsSaudizationValidDate { get; private set; }
        public bool? IsSpecificationValidDate { get; private set; }
        public bool? IsSpecificationAttached { get; private set; }
        public bool? IsSocialInsuranceValidDate { get; private set; }
        public bool? IsSocialInsuranceAttached { get; private set; }
        public bool IsZakatAttached { get; private set; }
        public bool IsZakatValidDate { get; private set; }
        public List<SupplierSpecificationDetail> SpecificationDetails { get; private set; } = new List<SupplierSpecificationDetail>();
        ////////////////////////////////////////


        public SupplierCombinedDetail UpdateAttachmentDetails(int combinedDetailId, int combinedId, bool? isChamberJoiningAttached, bool? isChamberJoiningValid, bool? isCommercialRegisterAttached, bool? isCommercialRegisterValid,
                   bool? isSaudizationAttached, bool? isSaudizationValidDate, bool? isSocialInsuranceAttached, bool? isSocialInsuranceValidDate, bool? isZakatAttached, bool? isZakatValidDate
            , bool? isSpecificationAttached
            , bool? isSpecificationValidDate
            )
        {

            CombinedId = combinedId;
            IsChamberJoiningAttached = isChamberJoiningAttached;
            IsChamberJoiningValid = isChamberJoiningValid;
            IsCommercialRegisterAttached = isCommercialRegisterAttached;
            IsCommercialRegisterValid = isCommercialRegisterValid;
            IsSaudizationAttached = isSaudizationAttached;
            IsSaudizationValidDate = isSaudizationValidDate;
            IsSocialInsuranceAttached = isSocialInsuranceAttached;
            IsSocialInsuranceValidDate = isSocialInsuranceValidDate;
            IsZakatAttached = (bool)isZakatAttached;
            IsZakatValidDate = (bool)isZakatValidDate;
            IsSpecificationAttached = (bool)isSpecificationAttached;
            IsSpecificationValidDate = (bool)isSpecificationValidDate;
            if (combinedDetailId == 0)
            {
                EntityCreated();
            }
            else
            {
                EntityUpdated();
            }
            return this;
        }

        public SupplierCombinedDetail UpdateTechnicalAttachmentDetails(int combinedDetailId, int combinedId, bool? isChamberJoiningAttached, bool? isChamberJoiningValid, bool? isCommercialRegisterAttached, bool? isCommercialRegisterValid, bool? isOfferCopyAttached,
        bool? isOfferLetterAttached, string offerLetterNumber, DateTime? offerLetterDate, bool? isSaudizationAttached, bool? isSaudizationValidDate, bool? isSocialInsuranceAttached
        , bool? isSocialInsuranceValidDate, bool? isVisitationAttached, bool? isZakatAttached, bool? isZakatValidDate
        , bool? isSpecificationAttached
        , bool? isSpecificationValidDate
        )
        {
            CombinedId = combinedId;
            IsChamberJoiningAttached = isChamberJoiningAttached;
            IsChamberJoiningValid = isChamberJoiningValid;
            IsCommercialRegisterAttached = isCommercialRegisterAttached;
            IsCommercialRegisterValid = isCommercialRegisterValid;
            IsSaudizationAttached = isSaudizationAttached;
            IsSaudizationValidDate = isSaudizationValidDate;
            IsSocialInsuranceAttached = isSocialInsuranceAttached;
            IsSocialInsuranceValidDate = isSocialInsuranceValidDate;
            IsZakatAttached = (bool)isZakatAttached;
            IsZakatValidDate = (bool)isZakatValidDate;
            IsSpecificationAttached = (bool)isSpecificationAttached;
            IsSpecificationValidDate = (bool)isSpecificationValidDate;
            if (combinedDetailId == 0)
            {
                EntityCreated();
            }
            else
            {
                EntityUpdated();
            }
            return this;
        }



        public void updateAttachmentsList(List<SupplierSpecificationDetail> classificationAttachments)
        {
            List<SupplierSpecificationDetail> SpecList = classificationAttachments.Where(a => a.SpecificationId == 0).ToList();
            AddSpecification(SpecList);

            //Deleted Attachments

            List<int> newSpecificationsIDs = classificationAttachments.Where(a => a.SpecificationId != 0).Select(a => a.SpecificationId).ToList();
            var specifications = SpecificationDetails.Where(x => !newSpecificationsIDs.Contains(x.SpecificationId) && x.SpecificationId != 0).ToList();

            foreach (var item in specifications)
            {
                item.Delete();
            }
        }


        public void AddCalssificationAttachments(List<SupplierSpecificationDetail> classificationAttachments)
        {
            List<SupplierSpecificationDetail> SpecList = classificationAttachments.Where(a => a.SpecificationId == 0).ToList();
            AddSpecification(SpecList);
            List<int> newSpecificationsIDs = classificationAttachments.Where(a => a.SpecificationId != 0).Select(a => a.SpecificationId).ToList();
            var specifications = SpecificationDetails.Where(x => !newSpecificationsIDs.Contains(x.SpecificationId) && x.SpecificationId != 0).ToList();
            foreach (var item in specifications)
            {
                item.Delete();
            }
        }

        public void AddSpecification(IEnumerable<SupplierSpecificationDetail> attachment)
        {
            SpecificationDetails.AddRange(attachment);
            EntityUpdated();
        }


    }
}
