//using MOF.Etimad.Monafasat.Core.Entities.Lookups;
//using MOF.Etimad.Monafasat.SharedKernel;
//using System.ComponentModel.DataAnnotations.Schema;

//namespace MOF.Etimad.Monafasat.Core.Entities.Negotiations
//{
//    [Table("NegotiationSecondStageSupplier", Schema = "CommunicationRequest")]
//    public class NegotiationSecondStageSupplier : AuditableEntity
//    {
//        public int Id { get; set; }

//        public int OfferId { get; set; }

//        public int SupplierStatusId { get; set; }

//        public int NegotiationId { get; set; }

//        public bool IsReported { get; set; } = false;

//        #region [Navigation]

//        [ForeignKey(nameof(SupplierStatusId))]
//        public SupplierSecondNegotiationStatus SupplierNegotiationStatus { get; set; }

//        [ForeignKey(nameof(NegotiationId))]
//        public NegotiationSecondStage NegotiationSecondStage { get; set; }

//        [ForeignKey(nameof(OfferId))]
//        public Offer Offer { get; set; }

//        #endregion

//        public NegotiationSecondStageSupplier()
//        {

//        }

//        public NegotiationSecondStageSupplier(int neqotiationId, int offerid)
//        {
//            NegotiationId = neqotiationId;
//            OfferId = OfferId;
//            IsReported = false;
//            SupplierStatusId = (int)Enums.enNegotiationSupplierStatus.NotSent;
//            EntityCreated();
//        }
//        public void updateStatus(int statusId)
//        {
//            SupplierStatusId = statusId;
//            EntityUpdated();
//        }

//    }

//}
