using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernal.Exceptions;
using nClam;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Integration
{
    public class FileNetScan : IFileNetScan
    {
        protected RootConfigurations _rootConfiguration;
        private readonly ILogger<FileNetScan> _logger;
        public FileNetScan(ILogger<FileNetScan> logger, IOptionsSnapshot<RootConfigurations> rootConfiguration)
        {
            _logger = logger;
            _rootConfiguration = rootConfiguration.Value;
        }
        public async Task ScanFile(byte[] content)
        {
            if (content.Length == 0)
                throw new BusinessRuleException("");
            await ScanNClamResult(content);
        }

        private async Task ScanNClamResult(byte[] content)
        {
            var serverUrl = _rootConfiguration.isFileScanConfiguration.ServerUrl;
            var port = int.Parse(_rootConfiguration.isFileScanConfiguration.Port);
            var clam = new ClamClient(serverUrl, port);
            ClamScanResult scanResult;
            try
            {
                scanResult = await clam.SendAndScanFileAsync(content);
            }
            catch (Exception ex)
            { _logger.LogError("File Scan Error : " + ex.Message + "\n" + ex.StackTrace); throw new BusinessRuleException("Exception raised while scanning file"); }
            switch (scanResult.Result)
            {
                case ClamScanResults.Clean:
                    break;
                case ClamScanResults.VirusDetected:
                    throw new BusinessRuleException(string.Format("Virus Found! Virus name: {0}", scanResult.InfectedFiles.First().VirusName));
                case ClamScanResults.Error:
                    throw new BusinessRuleException(string.Format("An error occured! Error: {0}", scanResult.RawResult));
            }
        }
    }
}
