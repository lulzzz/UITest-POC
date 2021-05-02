using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Data;
using MOF.Etimad.Monafasat.Services;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MOF.Etimad.Monafasat.IntegrationTests.Services.Common.CommonServices
{
    public class CommonServicesTests
    {
        private readonly Mock<IHttpContextAccessor> _httpContext;
        private readonly AppDbContext _dbContext;
        private readonly CommandService _commonServices;


        private readonly decimal _amountDue = 10;
        private readonly DateTime _dueDt = DateTime.Now;
        private readonly DateTime _expiryDate = DateTime.Now.AddDays(1);
        private readonly string _agencyCode = "AAA";


        public CommonServicesTests()
        {
            var db = new DbContextOptionsBuilder<AppDbContext>()
                 .UseInMemoryDatabase(databaseName: "TestingDB")
                 .Options;

            _httpContext = new Mock<IHttpContextAccessor>();
            _dbContext = new AppDbContext(db, _httpContext.Object);

            _commonServices = new CommandService(_dbContext);
        }

        [Fact]
        public void CreateAsyncTest()
        {
            var result = _commonServices.CreateAsync<BillInfo>(new BillInfo(_amountDue, _dueDt, _expiryDate, _agencyCode));
            Assert.Equal(10, result.Result.AmountDue);
        }

        [Fact]
        public async Task UpdateAsyncTest()
        {
            var bill = new BillInfo(_amountDue, _dueDt, _expiryDate, _agencyCode);
            _dbContext.BillInfos.Add(bill);
            await _commonServices.SaveAsync();

            bill.UpdateBillStatus(SharedKernel.Enums.BillStatus.Paid);

            _commonServices.UpdateAsync(bill);

            await _commonServices.SaveAsync();

            var result = await _dbContext.BillInfos.FirstAsync<BillInfo>();

            Assert.NotNull(result);
            Assert.Equal(4, result.BillStatusId);
        }
    }
}
