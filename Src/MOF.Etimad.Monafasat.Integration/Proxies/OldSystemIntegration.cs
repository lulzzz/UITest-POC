//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.Logging;
//using MOF.Etimad.Monafasat.Core.Entities;
//using MOF.Etimad.Monafasat.Integration.Infrastructure;
//using Newtonsoft.Json;
//using System;
//using System.Collections.Generic;
//using System.Net.Http;
//using System.Net.Http.Headers;
//using System.Text;
//using System.Threading.Tasks;
//using MOF.Etimad.Monafasat.SharedKernel;
//using static MOF.Etimad.Monafasat.SharedKernel.Enums;
//using System.Linq;
//using MOF.Etimad.Monafasat.ViewModel;

//namespace MOF.Etimad.Monafasat.Integration
//{
//    public class OldSystemIntegration : ProxyBase, IOldSystemIntegration
//    {
//        public OldSystemIntegration()
//        {

//        }

//        public async Task<bool> SyncTenderDetails(int tenderId, TenderSyncDataModel tenderSyncDataModel, string uRL, string token)
//        {
//            //tenderSyncDataModel.categories = null;
//            //tenderSyncDataModel.regions = null;
//            using (HttpClient client = new HttpClient())
//            {
//                TenderSyncDataResultModel resultModel;
//                var request = new HttpRequestMessage();
//                try
//                {
//                    request = new HttpRequestMessage
//                    {
//                        RequestUri = new Uri(uRL),
//                        Method = HttpMethod.Post,
//                    };
//                    request.Headers.Add("accessToken", token);

//                    HttpContent content = new StringContent(JsonConvert.SerializeObject(tenderSyncDataModel), Encoding.UTF8, "application/json");
//                    request.Content = content;
//                    request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
//                    var result = await client.SendAsync(request);
//                    var responseBody = await result.Content.ReadAsStringAsync().ConfigureAwait(false);
//                    resultModel = JsonConvert.DeserializeObject<TenderSyncDataResultModel>(responseBody);
//                    if (resultModel.status == "sucess")
//                    {
//                        return true;
//                    }
//                    return false;
//                }
//                catch //(Exception e)
//                {
//                    return false;
//                }
//            }
//        }

//        public async Task<bool> UpdateTenderSyncDetails(int tenderId, string requestContent, bool tenderUpdateStatus, string uRL)
//        {
//            using (HttpClient client = new HttpClient())
//            {
//                var request = new HttpRequestMessage();
//                try
//                {
//                    request = new HttpRequestMessage
//                    {
//                        RequestUri = new Uri(uRL),
//                        Method = HttpMethod.Post,
//                    };
//                    //request.Headers.Add("Authorization", "Bearer " + _configuration.GetSection("Authority:OldMonafasatSyncAPIToken").Value);

//                    HttpContent content = new StringContent(JsonConvert.SerializeObject(new
//                    {
//                        tenderId,
//                        requestContent,
//                        tenderUpdateStatus
//                    }));
//                    request.Content = content;
//                    request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
//                    var result = await client.SendAsync(request);
//                    var responseBody = await result.Content.ReadAsStringAsync().ConfigureAwait(false);
//                    var resultModel = JsonConvert.DeserializeObject<bool>(responseBody);
//                    return true;
//                }
//                catch //(Exception e)
//                {
//                    return false;
//                }
//            }
//        }
//    }
//}
