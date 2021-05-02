using MOF.Etimad.Monafasat.Core.Entities;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Lookups
{
    public class BankTest
    {
        [Fact]
        public void Should_Construct_Bank()
        {
            Bank bank = new Bank();
            _ = bank.BankId;
            _ = bank.NameAr;
            _ = bank.NameEn;

            bank.ShouldNotBeNull();
        }
    }
}
