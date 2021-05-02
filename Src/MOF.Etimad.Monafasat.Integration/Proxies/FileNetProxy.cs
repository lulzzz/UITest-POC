using FileNetDeleteService;
using FileNetDocDownloadService;
using FileNetDocUploadService;
using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.Integration.Enums;
using MOF.Etimad.Monafasat.Integration.Infrastructure;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernal.Exceptions;
using MOF.Etimad.Monafasat.SharedKernel.Exceptions;
using System;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Security;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Integration
{
    public class FileNetProxy : ProxyBase, IFileNetProxy
    {
        #region Properties
        protected override string Port { get { return _rootConfiguration.EsbSettingsConfiguration.PortValue; } }
        readonly string Repository;
        readonly bool _isProduction;
        readonly string _clientCertificateValue;
        private string FileNetCertification { get { return _rootConfiguration.FileNetCertificationConfiguration.FileNetCertification; } }
        readonly IContentEncryptionManger _contentEncryptionManger;
        #endregion
        #region Constructor
        public FileNetProxy(IContentEncryptionManger contentEncryptionManger, IOptionsSnapshot<RootConfigurations> rootConfiguration) : base(rootConfiguration)
        {
            _isProduction = _rootConfiguration.EsbSettingsConfiguration.IsProduction;
            _clientCertificateValue = _clientCertificateValue = _rootConfiguration.EsbSettingsConfiguration.ClientCertificateFindValue;
            Repository = _rootConfiguration.EsbSettingsConfiguration.RepositoryID;
            _contentEncryptionManger = contentEncryptionManger;
        }
        #endregion
        #region Upload
        public async Task<string> UploadFile(UploadFileModel file, IFileNetScan fileScan)
        {
            var isFileNetWork = _rootConfiguration.isFileNetWorkConfiguration.isFileNetWork;
            if (isFileNetWork)
            {
                var result = await HandleFileUpload(file, fileScan);
                return result;
            }
            else
            {
                return "idd_2CE19608-84B1-C336-8521-6875EB300000" + DateTime.Now.ToString("hhMMss");
            }
        }
        private async Task<string> HandleFileUpload(UploadFileModel file, IFileNetScan fileNetScan)
        {
            var service = GetFileNetUploadClient();
            var request = new FileNetDocUploadRq_Type
            {
                MsgRqHdr = FillUploadHeader()
            };
            request.Body = await MapUploadFile(file, fileNetScan);

            service.InnerChannel.OperationTimeout = new TimeSpan(0, 30, 0); // For 30 minute timeout - adjust as necessary 
            try
            {
                var responseeee = await service.FileNetDocUploadAsync(request);
                var response = responseeee.FileNetDocUploadRs;
                request.Body.FileContent = null;
                Logger.LogToFile(request, response);
                if (response.MsgRsHdr.ResponseStatus.StatusCode == ServiceStatusCodes.Success)
                {
                    return response?.Body?.FileId;
                }
                throw new BusinessRuleException("حدث خطأ فى الارسال");
            }
            catch (Exception ex)
            {
                request.Body.FileContent = null;
                Logger.LogToFile(request, ex.Message.ToString());
                throw;
            }
        }

        private FileNetDocUploadClient GetFileNetUploadClient()
        {
            string _FileUploadServiceAddress = _rootConfiguration.ServicesConfiguration.FileNetUploadService;
            var service = new FileNetDocUploadClient(FileNetDocUploadClient.EndpointConfiguration.FileNetDocUploadSOAP11, _FileUploadServiceAddress);
            service.ChannelFactory.Endpoint.EndpointBehaviors.Add(new DebugOutputEndpointBehavior());
            ((BasicHttpBinding)service.ChannelFactory.Endpoint.Binding).Security.Mode = BasicHttpSecurityMode.Transport;
            if (_isProduction)
            {
                ((BasicHttpBinding)service.ChannelFactory.Endpoint.Binding).Security.Transport.ClientCredentialType = HttpClientCredentialType.Certificate;
                service.ClientCredentials.ClientCertificate.SetCertificate(StoreLocation.LocalMachine, StoreName.My, X509FindType.FindBySubjectName, _clientCertificateValue);
            }
            else
            {
                ((BasicHttpBinding)service.ChannelFactory.Endpoint.Binding).Security.Mode = BasicHttpSecurityMode.Transport;
                service.ClientCredentials.ServiceCertificate.SslCertificateAuthentication =
                new X509ServiceCertificateAuthentication()
                {
                    CertificateValidationMode = X509CertificateValidationMode.None,
                    RevocationMode = X509RevocationMode.NoCheck
                };
            }
            return service;
        }

        private async Task<FileNetDocUploadRqBody_Type> MapUploadFile(UploadFileModel uploadFileModel, IFileNetScan fileNetScan)
        {
            var credintials = FillUserCredentials();
            if (_rootConfiguration.isFileScanConfiguration.Value.ToString().ToLower() == "true") await fileNetScan.ScanFile(uploadFileModel.FileContentField);
            byte[] fileContent;
            if (_rootConfiguration.isFileEncryptionConfiguration.Value.ToString().ToLower() == "true")
            { fileContent = _contentEncryptionManger.Encrypt(uploadFileModel.FileContentField, FileNetCertification); }
            else
            { fileContent = uploadFileModel.FileContentField; }
            var serviceModel = new FileNetDocUploadRqBody_Type
            {
                RepositoryId = Repository,
                FileName = DateTime.Now.ToLongTimeString() + uploadFileModel.FileNameField,
                FilePropertyList = uploadFileModel.FilePropertyListField,
                FileContent = fileContent,
                MIMEType = MimeMapping.MimeUtility.GetMimeMapping(uploadFileModel.MIMETypeField),
                FileLength = uploadFileModel.FileLengthField,
                FileVersion = uploadFileModel.FileVersionField,
                Class = _rootConfiguration.EsbSettingsConfiguration.Class,
                FolderId = _rootConfiguration.EsbSettingsConfiguration.FolderID,
                UserCredentials = new FileNetDocUploadService.UserCredentials_Type { UserId = credintials.UserId, Password = credintials.Password },
            };
            return serviceModel;
        }
        private FileNetDocUploadService.MsgRqHdr_Type FillUploadHeader()
        {
            string uploadServiceID = _rootConfiguration.FileNetServiceIDConfiguration.FileNetUpload;
            string uploadFunctionID = _rootConfiguration.FileNetFunctionIDConfiguration.UploadFunctionID;
            return new FileNetDocUploadService.MsgRqHdr_Type()
            {
                RqUID = RequestID,
                SCId = ChannelID,
                ServiceId = uploadServiceID,
                ClientDt = ClientDate,
                Version = Version,
                FuncId = uploadFunctionID
            };
        }
        #endregion
        #region Download
        public async Task<Byte[]> DownloadFile(string referenceId)
        {
            try
            {
                return await HandleFileDownload(referenceId);
            }
            catch (Exception ex)
            {
                throw new WebServiceException(ex.Message);
            }
        }

        private async Task<byte[]> HandleFileDownload(string referenceId)
        {
            var service = GetFileNetDocDownloadClient();
            var request = new FileNetDocDownloadRq_Type()
            {
                MsgRqHdr = FillDownloadHeader(),
                Body = MapDownloadFileBody(referenceId)
            };
            int timeout;
            timeout = _rootConfiguration.isFileNetWorkConfiguration.TimeOutMinutes;
            if (timeout < 5) timeout = 5;
            service.InnerChannel.OperationTimeout = new TimeSpan(0, timeout, 0); // For 30 minute timeout - adjust as necessary 
            var response = (await service.FileNetDocDownloadAsync(request)).FileNetDocDownloadRs;
            if (response.MsgRsHdr.ResponseStatus.StatusCode == ServiceStatusCodes.Success)
            {
                byte[] result;
                if (_rootConfiguration.isFileEncryptionConfiguration.Value.ToString().ToLower() == "true") result = _contentEncryptionManger.Decrypt(response?.Body.FileContent, FileNetCertification);
                else result = response?.Body.FileContent;
                return result;
            }
            response.Body.FileContent = null;
            Logger.LogToFile(request, response);
            throw new WebServiceException(Logger.GetJsonString(request, response));
        }

        private FileNetDocDownloadService.FileNetDocDownloadRqBody_Type MapDownloadFileBody(string referenceId)
        {
            var credintials = FillUserCredentials();
            var serviceModel = new FileNetDocDownloadService.FileNetDocDownloadRqBody_Type
            {
                UserCredentials = new FileNetDocDownloadService.UserCredentials_Type { UserId = credintials.UserId, Password = credintials.Password },
                FileId = referenceId,
                RepositoryId = Repository
            };
            return serviceModel;
        }

        private FileNetDocDownloadClient GetFileNetDocDownloadClient()
        {
            string _FileDownloadServiceAddress = _rootConfiguration.ServicesConfiguration.FileNetDownloadService;
            var service = new FileNetDocDownloadClient(FileNetDocDownloadClient.EndpointConfiguration.FileNetDocDownloadSOAP11, _FileDownloadServiceAddress);
            service.ChannelFactory.Endpoint.EndpointBehaviors.Add(new DebugOutputEndpointBehavior());
            ((BasicHttpBinding)service.ChannelFactory.Endpoint.Binding).Security.Mode = BasicHttpSecurityMode.Transport;

            if (_isProduction)
            {
                ((BasicHttpBinding)service.ChannelFactory.Endpoint.Binding).Security.Transport.ClientCredentialType = HttpClientCredentialType.Certificate;
                service.ClientCredentials.ClientCertificate.SetCertificate(StoreLocation.LocalMachine, StoreName.My, X509FindType.FindBySubjectName, _clientCertificateValue);
            }
            else
            {
                ((BasicHttpBinding)service.ChannelFactory.Endpoint.Binding).Security.Mode = BasicHttpSecurityMode.Transport;
                service.ClientCredentials.ServiceCertificate.SslCertificateAuthentication =
                new X509ServiceCertificateAuthentication()
                {
                    CertificateValidationMode = X509CertificateValidationMode.None,
                    RevocationMode = X509RevocationMode.NoCheck
                };
            }
            return service;
        }

        private FileNetDocDownloadService.MsgRqHdr_Type FillDownloadHeader()
        {
            string downloadServiceID = _rootConfiguration.FileNetServiceIDConfiguration.FileNetDownload;
            string downloadFunctionID = _rootConfiguration.FileNetFunctionIDConfiguration.DownloadFunctionID;
            return new FileNetDocDownloadService.MsgRqHdr_Type()
            {
                RqUID = RequestID,
                SCId = ChannelID,
                ServiceId = downloadServiceID,
                ClientDt = ClientDate,
                Version = Version,
                FuncId = downloadFunctionID,
            };
        }
        #endregion
        #region Delete
        public async Task<string> DeleteFile(string referenceId)
        {
            return await HandleFileDelete(referenceId);
        }
        private async Task<string> HandleFileDelete(string referenceId)
        {
            try
            {
                var service = GetFileNetDocDeleteClient();
                var request = new FileNetDeleteRq_Type();
                request.MsgRqHdr = FillDeleteHeader();
                request.Body = MapDeleteFileBody(referenceId);
                var response = (await service.FileNetDeleteAsync(request)).FileNetDeleteRs;
                Logger.LogToFile(request, response);

                if (response.MsgRsHdr.ResponseStatus.StatusCode == ServiceStatusCodes.Success)
                {
                    return response.MsgRsHdr.ResponseStatus.StatusDesc;
                }
                throw new WebServiceException(/*Logger.GetJsonString(request, response)*/);
            }
            catch //(Exception ex)
            {
                return "";
            }
        }
        private FileNetDeleteClient GetFileNetDocDeleteClient()
        {
            string _FileDeleteServiceAddress = _rootConfiguration.ServicesConfiguration.FileNetDeleteService;
            var service = new FileNetDeleteClient(FileNetDeleteClient.EndpointConfiguration.FileNetDeleteSOAP11, _FileDeleteServiceAddress);
            service.ChannelFactory.Endpoint.EndpointBehaviors.Add(new DebugOutputEndpointBehavior());
            ((BasicHttpBinding)service.ChannelFactory.Endpoint.Binding).Security.Mode = BasicHttpSecurityMode.Transport;
            if (_isProduction)
            {
                ((BasicHttpBinding)service.ChannelFactory.Endpoint.Binding).Security.Transport.ClientCredentialType = HttpClientCredentialType.Certificate;
                service.ClientCredentials.ClientCertificate.SetCertificate(StoreLocation.LocalMachine, StoreName.My, X509FindType.FindBySubjectName, _clientCertificateValue);
            }
            else
            {
                ((BasicHttpBinding)service.ChannelFactory.Endpoint.Binding).Security.Mode = BasicHttpSecurityMode.Transport;
                service.ClientCredentials.ServiceCertificate.SslCertificateAuthentication =
                new X509ServiceCertificateAuthentication()
                {
                    CertificateValidationMode = X509CertificateValidationMode.None,
                    RevocationMode = X509RevocationMode.NoCheck
                };
            }
            return service;
        }

        private FileNetDeleteService.MsgRqHdr_Type FillDeleteHeader()
        {
            return new FileNetDeleteService.MsgRqHdr_Type()
            {
                RqUID = RequestID,
                SCId = ChannelID,
                ServiceId = "FileNetDelete",
                ClientDt = ClientDate,
                Version = Version,
                FuncId = FileNet.Delete,
            };
        }
        private FileNetDeleteRqBody_Type MapDeleteFileBody(string referenceId)
        {
            var credintial = FillUserCredentials();
            var serviceModel = new FileNetDeleteRqBody_Type
            {
                UserCredentials = new FileNetDeleteService.UserCredentials_Type { UserId = credintial.UserId, Password = credintial.Password },
                FileId = referenceId,
                RepositoryId = Repository,
            };
            return serviceModel;
        }
        #endregion
        #region General Methods
        private UserCredentialsModel FillUserCredentials()
        {
            UserCredentialsModel userCredentials_Type = new UserCredentialsModel();
            userCredentials_Type.UserId = _rootConfiguration.FileNetCredintialConfiguration.FileNetUserID;
            userCredentials_Type.Password = _rootConfiguration.FileNetCredintialConfiguration.FileNetPassword;
            return userCredentials_Type;
        }
        #endregion
    }
}
