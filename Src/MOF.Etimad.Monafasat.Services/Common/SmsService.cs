//using Microsoft.Extensions.Configuration;
//using MOF.Etimad.Monafasat.Integration;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Text;
//using System.Threading.Tasks;

//namespace MOF.Etimad.Monafasat.Services.Common
//{
//    public class SmsService
//    {
//        private IConfigurationRoot configuration;

//        public SmsService()
//        {
//            configuration = new ConfigurationBuilder()
//                .SetBasePath(Directory.GetCurrentDirectory())
//                .AddJsonFile("appsettings.json")
//                .Build();
//        }

//        public async Task SendTokenAsync(string mobileNumber, string token)
//        {
//            var proxy = ProxyResolver.Resolve<INotificationProxy>();
//            string body = string.Format(configuration.GetSection("SMS:TokenSMS").Value,  token);
//            await proxy.SendSMS(mobileNumber, body);
//        }


//        public async Task SendUserEditedAsync(string mobileNumber)
//        {
//            var proxy = ProxyResolver.Resolve<INotificationProxy>();
//            string body = "عزيزي مستخدم اعتماد لقد تم تحديث بياناتك";
//            await proxy.SendSMS(mobileNumber, body);

//        }

//        public async Task SendNewRegireation(string mobileNumber, string password)
//        {
//            var proxy = ProxyResolver.Resolve<INotificationProxy>();

//            string body = string.Format(configuration.GetSection("SMS:NewRegiterationSMS").Value, "https://etimad.sa", password);
//            await proxy.SendSMS(mobileNumber, body);
//            ;
//        }
//    }
//}
