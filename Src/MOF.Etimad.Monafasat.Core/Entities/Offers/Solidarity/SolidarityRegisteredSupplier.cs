//using MOF.Etimad.Monafasat.SharedKernel;
//using System.ComponentModel.DataAnnotations.Schema;

//namespace MOF.Etimad.Monafasat.Core.Entities
//{
//    public class OfferSolidarity : OfferSolidarity
//    {
//        public OfferSolidarity()
//        //{
//        }
//        public OfferSolidarity(int offerId, string commericalRegisterNo) : base(offerId)
//        {
//            CRNumber = commericalRegisterNo;
//        }

//        public OfferSolidarity(Offer offer, Supplier supplier)
//        {
//            Offer = offer;
//           // Supplier = supplier;
//        }
//        public OfferSolidarity(string cr)
//        {
//            CRNumber = cr;
//            StatusId = (int)Enums.SupplierSolidarityStatus.New;
//            EntityCreated();
//        }
//        public OfferSolidarity(string cr, Enums.SupplierSolidarityStatus status = Enums.SupplierSolidarityStatus.New)
//        {
//            CRNumber = cr;
//            StatusId = (int)status;
//            EntityCreated();
//        }
//        //[ForeignKey(nameof(SupplierCombinedDetail))]
//        //public int? SupplierCombinedId { get; private set; }
//        //public string CRNumber { get; private set; }
//        //[ForeignKey("CommericalRegisterNo")]
//        //public Supplier Supplier { get; private set; }

//    }


//}
