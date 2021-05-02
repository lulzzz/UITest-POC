using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.Integration;
using MOF.Etimad.Monafasat.Integration.Models;
using MOF.Etimad.Monafasat.SharedKernal;
using Moq;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace MOF.Etimad.Monafasat.IntegrationTests.Proxies
{
    public class NotificationProxyTest
    {
        private readonly NotificationProxy _notificationProxy;
        private readonly Mock<IOptionsSnapshot<RootConfigurations>> _rootConfiguration;
        private readonly RootConfigurations _configuration;
        public NotificationProxyTest()
        {
            _configuration = SetupConfigurations.GetApplicationConfiguration(Directory.GetCurrentDirectory());

            _rootConfiguration = new Mock<IOptionsSnapshot<RootConfigurations>>();
            _rootConfiguration.Setup(m => m.Value).Returns(_configuration);
            _notificationProxy = new NotificationProxy(_rootConfiguration.Object);
        }

        [Fact]
        public async Task ShouldSendSMS()
        {
            //Arrange

            //Act
            var result = await _notificationProxy.SendSMS("0561982341", "Test Message");
            //Assert
            Assert.True(result);
        }

        [Fact]
        public async Task ShouldSendEmail()
        {
            //Arrange

            //Act
            var result = await _notificationProxy.SendEmail("a.farouk.tqh@mof.gov.sa", "Test Email", "Test Email Content");
            //Assert
            Assert.True(result);
        }

        [Fact]
        public async Task ShouldSendBulkEmail()
        {
            //Arrange
            var emiallst = new List<EmailMessageNoitification> {

                new EmailMessageNoitification{
                    emailContact = "a.farouk.tqh@mof.gov.sa",
                    emailMessage = "Email Message"
                },
                new EmailMessageNoitification{
                    emailContact = "afarouk.fci@gmail.com",
                    emailMessage = "Email Message"
                },
            };

            //Act
            var result = await _notificationProxy.SendBulkEmail(emiallst);
            //Assert
            Assert.True(result);
        }

        [Fact]
        public async Task ShouldSendBulkSMS()
        {
            //Arrange
            var smslst = new List<SMSMessageNoitification> {

                new SMSMessageNoitification{

                    SMSContact = "0561982341",
                    SMSMessage = "SMS Test Message"
                },
                new SMSMessageNoitification{
                    SMSContact = "0504583476",
                    SMSMessage = "SMS Test Message"
                },
            };

            //Act
            var result = await _notificationProxy.SendBulkSMS(smslst);
            //Assert
            Assert.True(result);
        }
    }
}
