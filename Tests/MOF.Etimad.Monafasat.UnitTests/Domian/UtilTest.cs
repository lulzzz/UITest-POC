using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernal.Exceptions;
using System;
using Xunit;
namespace MOF.Etimad.Monafasat.UnitTests.Domian
{
    public class UtilTest
    {
        [Theory]
        [InlineData(1, 7, 20, 9, 14)]
        [InlineData(2, 7, 20, 9, 14)]
        [InlineData(3, 7, 20, 9, 14)]
        [InlineData(4, 7, 20, 9, 14)]
        public void DetermineTimesForVacationDaysDatesShouldReturnBussinessException(int timeMessage, int startOfferTime, int endOfferTime, int startOfferVacationTime, int endOfferVacationTime)
        {
            DateTime checkTimeOfDay = new DateTime(2020, 11, 20);
            Assert.Throws<BusinessRuleException>(() => Util.DetermineTimesForDates(checkTimeOfDay, timeMessage, startOfferTime, endOfferTime, startOfferVacationTime, endOfferVacationTime));
        }


        [Theory]
        [InlineData(1, 7, 20, 9, 14)]
        [InlineData(2, 7, 20, 9, 14)]
        [InlineData(3, 7, 20, 9, 14)]
        [InlineData(4, 7, 20, 9, 14)]
        public void DetermineTimesForNormalDaysDatesShouldReturnBussinessException(int timeMessage, int startOfferTime, int endOfferTime, int startOfferVacationTime, int endOfferVacationTime)
        {
            DateTime checkTimeOfDay = new DateTime(2020, 11, 19);
            Assert.Throws<BusinessRuleException>(() => Util.DetermineTimesForDates(checkTimeOfDay, timeMessage, startOfferTime, endOfferTime, startOfferVacationTime, endOfferVacationTime));
        }

        [Theory]
        [InlineData(1, 7, 20, 9, 14)]
        [InlineData(2, 7, 20, 9, 14)]
        [InlineData(3, 7, 20, 9, 14)]
        [InlineData(4, 7, 20, 9, 14)]
        public void DetermineTimesForNormalDaysShouldSuccess(int timeMessage, int startOfferTime, int endOfferTime, int startOfferVacationTime, int endOfferVacationTime)
        {
            DateTime checkTimeOfDay = new DateTime(2020, 11, 19, 17, 5, 5, 5);
            var result = Record.Exception(() => Util.DetermineTimesForDates(checkTimeOfDay, timeMessage, startOfferTime, endOfferTime, startOfferVacationTime, endOfferVacationTime));
            Assert.Null(result);
        }

        [Theory]
        [InlineData(1, 7, 20, 9, 14)]
        [InlineData(2, 7, 20, 9, 14)]
        [InlineData(3, 7, 20, 9, 14)]
        [InlineData(4, 7, 20, 9, 14)]
        public void DetermineTimesForVacationDaysShouldSuccess(int timeMessage, int startOfferTime, int endOfferTime, int startOfferVacationTime, int endOfferVacationTime)
        {
            DateTime checkTimeOfDay = new DateTime(2020, 11, 20, 13, 5, 5, 5);
            var result = Record.Exception(() => Util.DetermineTimesForDates(checkTimeOfDay, timeMessage, startOfferTime, endOfferTime, startOfferVacationTime, endOfferVacationTime));
            Assert.Null(result);
        }
    }


}
