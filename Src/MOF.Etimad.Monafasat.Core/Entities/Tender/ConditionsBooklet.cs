using MOF.Etimad.Monafasat.SharedKernel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("ConditionsBooklet", Schema = "Tender")]
    public class ConditionsBooklet : AuditableEntity
    {

        public ConditionsBooklet()
        {

        }
        public ConditionsBooklet(string commericalRegisterNo, BillInfo billInfo)
        {
            CommericalRegisterNo = commericalRegisterNo;
            BillInfo = billInfo;
            EntityCreated();
        }


        [Key]
        public int ConditionsBookletId { get; private set; }

        [StringLength(20)]
        public string CommericalRegisterNo { get; private set; }
        [ForeignKey(nameof(CommericalRegisterNo))]
        public Supplier Supplier { get; set; }

        public int TenderId { get; private set; }

        [ForeignKey(nameof(TenderId))]
        public Tender Tender { get; private set; }

        public BillInfo BillInfo { get; private set; }
        public void UpdateConfirm(bool confirm)
        {
            EntityUpdated();

        }
        public void Delete()
        {
            EntityDeleted();

        }
        public void DeleteWithBill()
        {
            BillInfo.DeleteBillInWithdrawAndRejectFromInvitaion();
            EntityDeleted();
        }



        #region Test Helper Methods

        public void Test_AddTender(Tender tender)
        {
            this.Tender = tender;
        }

        public void Test_AddSupplier(Supplier supplier)
        {
            this.Supplier = supplier;
        }


        #endregion

    }
}
