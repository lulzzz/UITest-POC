//using Microsoft.Extensions.Configuration;
//using MOF.Etimad.Monafasat.SharedKernal;
//using MOF.Etimad.Monafasat.ViewModel;
//using Newtonsoft.Json;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Net;
//using System.Net.Http;
//using System.Net.Http.Headers;
//using System.Text;
//using System.Threading.Tasks;

//namespace MOF.Etimad.Monafasat.Integration
//{
//    public class PHPFilesProxy : ProxyBase, IPHPFilesProxy
//    {
//        private IConfigurationRoot configuration;

//        public PHPFilesProxy()
//        {
//            configuration = new ConfigurationBuilder()
//                .SetBasePath(Directory.GetCurrentDirectory())
//                .AddJsonFile("appsettings.json")
//                .Build();
//        }
//        #region Methods

//        public async Task<byte[]> GetFileByHash(string hash)
//        {

//            HttpClient client = new HttpClient();
//            client.BaseAddress = new Uri(configuration.GetSection("PHPMonafasat:URL").Value + "?hash=" + hash);
//            client.DefaultRequestHeaders.Accept.Clear();
//            client.Timeout = new TimeSpan(0, 30, 0);
//            client.DefaultRequestHeaders.Add("accessToken", configuration.GetSection("PHPMonafasat:AccessToken").Value);
//            //client.DefaultRequestHeaders.Add("accessToken", "mxJYc4h2QbAV8C9gEIUjScAkUle2gKmNW4zM1XYj");
//            //client.DefaultRequestHeaders.Add("clientsecret", configuration.GetSection("Authority:clientsecret").Value);
//            //client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/octet-stream"));
//            var response = await client.GetAsync(new Uri(configuration.GetSection("PHPMonafasat:URL").Value + "?hash=" + hash));
//            Stream result = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
//            byte[] buffer = new byte[16 * 1024];
//            using (MemoryStream ms = new MemoryStream())
//            {
//                int read;
//                while ((read = result.Read(buffer, 0, buffer.Length)) > 0)
//                {
//                    ms.Write(buffer, 0, read);
//                }
//                return ms.ToArray();
//            }
            

//            //using (var client = new HttpClient())
//            {
//                //var request = new HttpRequestMessage
//                //{
//                //    RequestUri = new Uri(configuration.GetSection("PHPMonafasat:URL").Value + "?hash = " + hash),
//                //    Method = HttpMethod.Get,
//                //};
//                //request.Headers.Add("accessToken", configuration.GetSection("Authority:Token").Value);

//                //HttpContent content = new StringContent(JsonConvert.SerializeObject(new
//                //{

//                //}), Encoding.UTF8, "application/octet-stream");
//                //request.Content = content;
//                //request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream"); // change as necessary
//                //var result = client.SendAsync(request).Result;
//                //var responseBody = await result.Content.ReadAsStringAsync().ConfigureAwait(false);
//                //var resultModel = JsonConvert.DeserializeObject<byte[]>(responseBody);
//                //return resultModel;
//            }
//        }
//        #endregion
//    }
//}
