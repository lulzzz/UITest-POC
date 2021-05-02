using MOF.Etimad.Monafasat.Core.Entities;
using Shouldly;
using System;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Tenders
{
    public class ConditionsBookletTest
    {
        private readonly string _commericalRegisterNo = "Cr200";
        private readonly BillInfo _billInfo = new BillInfo();

        [Fact]
        public void Should_Empty_Construct_BiddingRoundOffer()
        {
            var conditionsBooklet = new ConditionsBooklet();

            conditionsBooklet.ShouldNotBeNull();
        }

        [Fact]
        public void Should_Construct_CommericalRegister()
        {
            var conditionsBooklet = new ConditionsBooklet(_commericalRegisterNo, _billInfo);

            conditionsBooklet.ShouldNotBeNull();
            conditionsBooklet.CommericalRegisterNo.ShouldNotBeNullOrEmpty();
            conditionsBooklet.BillInfo.ShouldNotBeNull();
        }

        [Fact]
        public void Should_Update()
        {
            var conditionsBooklet = new ConditionsBooklet();
            conditionsBooklet.UpdateConfirm(true);
            conditionsBooklet.State.ShouldBe(SharedKernal.ObjectState.Modified);
        }

        [Fact]
        public void Should_Delete()
        {
            var conditionsBooklet = new ConditionsBooklet();
            conditionsBooklet.Delete();
            conditionsBooklet.State.ShouldBe(SharedKernal.ObjectState.Deleted);
        }

        [Fact]
        public void Should_DeleteWithBill()
        {
            var billInfo = new BillInfo(10, DateTime.Now, DateTime.Now.AddYears(1), "AGC1001");
            var conditionsBooklet = new ConditionsBooklet(_commericalRegisterNo, billInfo);
            conditionsBooklet.DeleteWithBill();
            conditionsBooklet.BillInfo.State.ShouldBe(SharedKernal.ObjectState.Deleted);
            conditionsBooklet.State.ShouldBe(SharedKernal.ObjectState.Deleted);
        }
    }
}
