using BulkNotificationService;
using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.Integration.Enums;
using MOF.Etimad.Monafasat.Integration.Infrastructure;
using MOF.Etimad.Monafasat.Integration.Models;
using MOF.Etimad.Monafasat.SharedKernal;
using NotificationService;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Security;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Integration
{
    public class NotificationProxy : ProxyBase, INotificationProxy
    {
        readonly bool _isProduction;
        readonly string _clientCertificateValue;
        public NotificationProxy(IOptionsSnapshot<RootConfigurations> rootConfiguration) : base(rootConfiguration)
        {
            _isProduction = _rootConfiguration.EsbSettingsConfiguration.IsProduction;
            _clientCertificateValue = _rootConfiguration.EsbSettingsConfiguration.ClientCertificateFindValue;
        }
        public async Task<bool> SendSMS(string mobileNumber, string messageContent)
        {
            var isNotificationWork = _rootConfiguration.isNotificationConfiguration.isNotificationWork;
            if (isNotificationWork)
            {
                try
                {
                    var service = GetNotificationClient();
                    var request = new NotificationService.NotificationSendRq_Type
                    {
                        Body = new NotificationService.NotificationSendRqBody_Type()
                        {
                            NotificationMethodInfo = new NotificationService.NotificationMethodInfo_Type()
                            {
                                EmailFrom = "Etimad",
                                Subject = "Etimad",
                                NotificationMethod = NotificationService.NotificationMethod_Type.SMS,
                                Contact = mobileNumber,
                                Username = _rootConfiguration.bulkNotification.Username,
                                Password = _rootConfiguration.bulkNotification.Password
                            },
                            MessageContent = messageContent
                        },
                        MsgRqHdr = FillSmsHeader()
                    };
                    var result = (await service.NotificationSendAsync(request))?.NotificationSendRs;
                    Logger.LogToFile(request, result);
                    return result != null && result.MsgRsHdr.ResponseStatus.StatusCode == ServiceStatusCodes.Success;
                }
                catch (Exception ex) { Logger.LogToFile(ex.Message, ex.StackTrace); return false; }
            }
            else
            {
                return true;
            }
        }
        public async Task<bool> SendEmail(string emailAddress, string emailSubject, string emailContent, string emailCC = null, string emailBCC = null)
        {
            var isNotificationWork = _rootConfiguration.isNotificationConfiguration.isNotificationWork;
            if (isNotificationWork)
            {
                try
                {
                    var service = GetNotificationClient();
                    var request = new NotificationService.NotificationSendRq_Type
                    {
                        Body = new NotificationService.NotificationSendRqBody_Type()
                        {
                            NotificationMethodInfo = new NotificationService.NotificationMethodInfo_Type()
                            {
                                EmailFrom = "no-reply@etimad.sa",
                                Subject = emailSubject,
                                NotificationMethod = NotificationService.NotificationMethod_Type.EMAIL,
                                Contact = emailAddress,
                                Username = _rootConfiguration.bulkNotification.Username,
                                Password = _rootConfiguration.bulkNotification.Password
                            },
                            MessageContent = emailContent
                        },
                        MsgRqHdr = FillSmsHeader()
                    };
                    var result = (await service.NotificationSendAsync(request))?.NotificationSendRs;
                    Logger.LogToFile(request, result);
                    return result != null && result.MsgRsHdr.ResponseStatus.StatusCode == ServiceStatusCodes.Success;
                }
                catch (Exception ex) { Logger.LogToFile(ex.Message, ex.StackTrace); return false; }
            }
            else
            {
                return true;
            }
        }
        private NotificationService.MsgRqHdr_Type FillSmsHeader()
        {
            return new NotificationService.MsgRqHdr_Type()
            {
                RqUID = RequestID,
                SCId = ChannelID,
                ServiceId = "NotificationSend",
                FuncId = "34520100",
                ClientDt = ClientDate,
                Version = Version,
            };
        }
        private NotificationSendClient GetNotificationClient()
        {
            var service = new NotificationSendClient(NotificationSendClient.EndpointConfiguration.NotificationSendSOAP11, _rootConfiguration.NotificationEndPointConfiguration.Value);
            service.ChannelFactory.Endpoint.EndpointBehaviors.Add(new DebugOutputEndpointBehavior());
            ((BasicHttpBinding)service.ChannelFactory.Endpoint.Binding).Security.Mode = BasicHttpSecurityMode.Transport;
            if (_isProduction)
            {
                ((BasicHttpBinding)service.ChannelFactory.Endpoint.Binding).Security.Transport.ClientCredentialType = HttpClientCredentialType.Certificate;
                service.ClientCredentials.ClientCertificate.SetCertificate(StoreLocation.LocalMachine, StoreName.My, X509FindType.FindBySubjectName, _clientCertificateValue);
            }
            else
            {
                service.ClientCredentials.ServiceCertificate.SslCertificateAuthentication =
                new X509ServiceCertificateAuthentication()
                {
                    CertificateValidationMode = X509CertificateValidationMode.None,
                    RevocationMode = X509RevocationMode.NoCheck
                };
            }
            return service;
        }

        public async Task<bool> SendBulkEmail(List<EmailMessageNoitification> lstEmails)
        {
            var isNotificationWork = _rootConfiguration.isNotificationConfiguration.isNotificationWork;
            if (isNotificationWork)
            {
                try
                {
                    var service = GetBulkNotificationClient();

                    List<RequestMsg_Type> lstMessages = new List<RequestMsg_Type>();

                    foreach (var email in lstEmails)
                    {
                        BulkNotificationService.NotificationSendRqBody_Type body = new BulkNotificationService.NotificationSendRqBody_Type
                        {
                            Item = email.emailMessage,
                            ItemElementName = ItemChoiceType.MessageContent,
                            NotificationMethodInfo = new BulkNotificationService.NotificationMethodInfo_Type
                            {
                                Contact = email.emailContact,
                                EmailBCC = "",
                                EmailCC = "",
                                EmailFrom = _rootConfiguration.bulkNotification.EmailFrom,
                                EmailReplyTo = "",
                                NotificationMethod = BulkNotificationService.NotificationMethod_Type.EMAIL,
                                Subject = "TestEmailNotificationSend",

                            },
                            OTP = "",
                            PartyId = new BulkNotificationService.PartyId_Type
                            {
                                PartyIdNum = "",
                                PartyIdType = BulkNotificationService.PartyIdType_Type.AccountNumber,
                                PartyIdTypeSpecified = false
                            }

                        };

                        RequestMsg_Type requestMsg_Type = new RequestMsg_Type
                        {
                            NotificationSendRq = new BulkNotificationService.NotificationSendRq_Type
                            {
                                Body = body,
                                MsgRqHdr = FillBulkBodyHeader()
                            }

                        };

                        lstMessages.Add(requestMsg_Type);

                    }

                    var messeages = lstMessages.ToArray();


                    BulkHandlerRqBody_Type bulkHandlerRqBody_Type = new BulkHandlerRqBody_Type
                    {

                        XMLRequestList = messeages
                    };

                    BulkHandlerRq_Type req = new BulkHandlerRq_Type
                    {
                        Body = bulkHandlerRqBody_Type,
                        MsgRqHdr = FillBulkHeader()

                    };

                    var result = (await service.BulkHandlerAsync(req))?.BulkHandlerRs;
                    Logger.LogToFile(req, result);
                    return result != null && result.MsgRsHdr.ResponseStatus.StatusCode == ServiceStatusCodes.Success;
                }
                catch (Exception ex) { Logger.LogToFile(ex.Message, ex.StackTrace); return false; }
            }
            else
            {
                return true;
            }
        }

        public async Task<bool> SendBulkSMS(List<SMSMessageNoitification> lstSMSs)
        {
            var isNotificationWork = _rootConfiguration.isNotificationConfiguration.isNotificationWork;
            if (isNotificationWork)
            {
                try
                {
                    var service = GetBulkNotificationClient();
                    List<RequestMsg_Type> lstMessages = new List<RequestMsg_Type>();

                    foreach (var sms in lstSMSs)
                    {
                        BulkNotificationService.NotificationSendRqBody_Type body = new BulkNotificationService.NotificationSendRqBody_Type
                        {
                            Item = sms.SMSMessage,
                            ItemElementName = ItemChoiceType.MessageContent,
                            NotificationMethodInfo = new BulkNotificationService.NotificationMethodInfo_Type
                            {
                                Contact = sms.SMSContact,
                                NotificationMethod = BulkNotificationService.NotificationMethod_Type.SMS,
                                Password = _rootConfiguration.bulkNotification.Password,
                                Username = _rootConfiguration.bulkNotification.Username,

                            },
                            OTP = "",
                            PartyId = new BulkNotificationService.PartyId_Type
                            {
                                PartyIdNum = "",
                                PartyIdType = BulkNotificationService.PartyIdType_Type.AccountNumber,
                                PartyIdTypeSpecified = false
                            }

                        };

                        RequestMsg_Type requestMsg_Type = new RequestMsg_Type
                        {
                            NotificationSendRq = new BulkNotificationService.NotificationSendRq_Type
                            {
                                Body = body,
                                MsgRqHdr = FillBulkBodyHeader()
                            }

                        };

                        lstMessages.Add(requestMsg_Type);

                    }

                    var messeages = lstMessages.ToArray();

                    BulkHandlerRqBody_Type bulkHandlerRqBody_Type = new BulkHandlerRqBody_Type
                    {

                        XMLRequestList = messeages
                    };

                    BulkHandlerRq_Type req = new BulkHandlerRq_Type
                    {
                        Body = bulkHandlerRqBody_Type,
                        MsgRqHdr = FillBulkHeader()

                    };

                    var result = (await service.BulkHandlerAsync(req))?.BulkHandlerRs;
                    Logger.LogToFile(req, result);
                    return result != null && result.MsgRsHdr.ResponseStatus.StatusCode == ServiceStatusCodes.Success;
                }
                catch (Exception ex) { Logger.LogToFile(ex.Message, ex.StackTrace); return false; }
            }
            else
            {
                return true;
            }
        }


        private BulkNotificationService.MsgRqHdr_Type FillBulkHeader()
        {
            return new BulkNotificationService.MsgRqHdr_Type()
            {
                RqUID = RequestID,
                SCId = _rootConfiguration.bulkNotification.SCId,
                ServiceId = _rootConfiguration.bulkNotification.ServiceId,
                FuncId = _rootConfiguration.bulkNotification.FuncId,
                ClientDt = ClientDate,
                Version = Version,
            };
        }

        private BulkNotificationService.MsgRqHdr_Type FillBulkBodyHeader()
        {
            return new BulkNotificationService.MsgRqHdr_Type()
            {
                RqUID = RequestID,
                SCId = _rootConfiguration.bulkNotification.SCId,
                ServiceId = _rootConfiguration.bulkNotification.ServiceId,
                FuncId = _rootConfiguration.bulkNotification.BodyHeaderFuncId,
                ClientDt = ClientDate,
                Version = Version,
            };
        }


        private BulkHandlerClient GetBulkNotificationClient()
        {
            var service = new BulkHandlerClient(BulkHandlerClient.EndpointConfiguration.BulkHandlerSOAP11, _rootConfiguration.BulkNotificationEndPointConfiguration.Value);
            service.ChannelFactory.Endpoint.EndpointBehaviors.Add(new DebugOutputEndpointBehavior());
            ((BasicHttpBinding)service.ChannelFactory.Endpoint.Binding).Security.Mode = BasicHttpSecurityMode.Transport;
            if (_isProduction)
            {
                ((BasicHttpBinding)service.ChannelFactory.Endpoint.Binding).Security.Transport.ClientCredentialType = HttpClientCredentialType.Certificate;
                service.ClientCredentials.ClientCertificate.SetCertificate(StoreLocation.LocalMachine, StoreName.My, X509FindType.FindBySubjectName, _clientCertificateValue);
            }
            else
            {
                service.ClientCredentials.ServiceCertificate.SslCertificateAuthentication =
                new X509ServiceCertificateAuthentication()
                {
                    CertificateValidationMode = X509CertificateValidationMode.None,
                    RevocationMode = X509RevocationMode.NoCheck
                };
            }
            return service;
        }

    }
}
