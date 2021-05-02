using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.SharedKernal;
using System;
using System.IO;
using System.Net;

namespace MOF.Etimad.Monafasat.Services
{
    public class PushNotificationService : IPushNotificationService
    {


        public PushNotificationService(IOptionsSnapshot<RootConfigurations> optionsSnapshot)
        {

        }

        public string SendNotification(string NotificationMessage, string deviceToken)
        {
            try
            {
                string serverKey = "AAAACVjlf5c:APA91bGNBLbXo_iLAoh_N7wY6Pkjg0b5YCi86dsaGPyC0N71B0WR9fTCOFSEtX64a8qzNrzpBU-UQcQNrDjLPAef8AGYQsiNOpHJ7J3QYWJp7oBKM0ySORSlpbfNKkCoVaOW7jd1uwJf";
                string senderId = "40146141079";

                const string contentType = "application/json;  charset=utf-8";
                ServicePointManager.DefaultConnectionLimit = 1000;
                CookieContainer cookies = new CookieContainer();
                HttpWebRequest webRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send") as HttpWebRequest;
                webRequest.Method = "POST";
                webRequest.Headers["Authorization"] = "Key=" + serverKey;
                webRequest.Headers.Add(string.Format("Sender: id={0}", senderId));
                string Data = "{\"to\":\"" + deviceToken + "\",\"data\": {\"message\": \"" + NotificationMessage + "\"},\"notification\": {\"text\": \"" + NotificationMessage + "\",\"sound\":true}}";
                webRequest.ContentType = contentType;
                webRequest.CookieContainer = cookies;
                webRequest.ContentLength = Data.Length;
                webRequest.SendChunked = true;
                webRequest.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US; rv:1.9.0.1) Gecko/2008070208 Firefox/3.0.1";
                webRequest.Accept = "text/html,application/xhtml+xml,application/json,application/xml;q=0.9,*/*;q=0.8";
                webRequest.Referer = "https://accounts.craigslist.org";
                StreamWriter requestWriter = new StreamWriter(webRequest.GetRequestStream());
                requestWriter.Write(Data);
                requestWriter.Flush();
                StreamReader responseReader = new StreamReader(webRequest.GetResponse().GetResponseStream());
                responseReader.ReadToEnd();
                responseReader.Close();
                webRequest.GetResponse().Close();
                return "1";
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }
    }
}
