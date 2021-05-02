using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.Integration;
using MOF.Etimad.Monafasat.IntegrationTests;
using MOF.Etimad.Monafasat.Services;
using MOF.Etimad.Monafasat.SharedKernal;
using Moq;
using System;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests
{
    public class FileNetProxyTest
    {
        private readonly Mock<IOptionsSnapshot<RootConfigurations>> _rootConfiguration;
        private readonly RootConfigurations _configuration;

        private readonly Mock<ILogger<FileNetScan>> _mockLogger;
        private readonly IFileNetScan _fileNetScan;
        private readonly FileNetService _fileNetService;
        private readonly IFileNetProxy _fileNetProxy;
        private readonly IContentEncryptionManger _contentEncryptionManger;
        private readonly ICertificateEncryption _certificateEncryption;

        public FileNetProxyTest()
        {
            _configuration = SetupConfigurations.GetApplicationConfiguration(Directory.GetCurrentDirectory());
            _rootConfiguration = new Mock<IOptionsSnapshot<RootConfigurations>>();
            _rootConfiguration.Setup(m => m.Value).Returns(_configuration);
            _certificateEncryption = new CertificateEncryption(_rootConfiguration.Object);
            _contentEncryptionManger = new ContentEncryptionManger(_certificateEncryption, _rootConfiguration.Object);
            _mockLogger = new Mock<ILogger<FileNetScan>>();
            _fileNetScan = new FileNetScan(_mockLogger.Object, _rootConfiguration.Object);
            _fileNetProxy = new FileNetProxy(_contentEncryptionManger, _rootConfiguration.Object);
            _fileNetService = new FileNetService(_fileNetScan, _fileNetProxy);
        }

        [Fact]
        public async Task TestDeleting()
        {
            //Act
            var result = await _fileNetService.deleteFile("idd_0D57535F-608D-CCB3-8456-749713B00000");
            //Assert
            Assert.Equal("", result);
        }

        [Fact]
        public async Task TestDownloading()
        {
            //Act
            var result = await _fileNetService.downlaodFile("idd_0D57535F-608D-CCB3-8456-749713B00000");
            //Assert
            Assert.NotEmpty(result);
        }

        public byte[] Getbytes()
        {

            FileStream fs = File.OpenRead("../../../logo.PNG");
            try
            {
                byte[] bytes = new byte[fs.Length];
                fs.Read(bytes, 0, Convert.ToInt32(fs.Length));
                fs.Close();
                return bytes;
            }
            finally
            {
                fs.Close();
            }
        }

        [Fact]
        public async Task TestUploading()
        {
            //Arrange
            UploadFileModel model = new UploadFileModel { FileContentField = Getbytes(), FileNameField = "TestFile", MIMETypeField = "PNG" };
            //Act
            var result = await _fileNetService.UploadFile(model);
            //Assert
            Assert.False(string.IsNullOrEmpty(result));
        }
    }
}
