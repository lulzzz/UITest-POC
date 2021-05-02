using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernal;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using SharedEnums = MOF.Etimad.Monafasat.SharedKernel;

namespace MOF.Etimad.Monafasat.Integration
{
    public class BillingProxy : ProxyBase, IBillingProxy
    {
        #region Properties
        private readonly string _clientKey;
        #endregion
        #region Constructors
        public BillingProxy(IOptionsSnapshot<RootConfigurations> rootConfiguration) : base(rootConfiguration)
        {
            _clientKey = _rootConfiguration.BillingConfiguration.ClientKey;
        }
        #endregion
        // forJobs
        public async Task<bool> UpdateBillActionStatus(BillInfo bill)
        {
            if (bill.ActionStatus == (int)SharedEnums.Enums.BillActionStatus.UpdateBill)
            {
                BillModelForUpdateRequest billModel = new BillModelForUpdateRequest();
                billModel.sadadInvoiceNumber = bill.BillInvoiceNumber;
                billModel.ClientKey = _clientKey;
                billModel.ExpDate = bill.ExpiryDate;
                var result = await UpdateBill(billModel);
                return result;
            }
            else
            {
                BillModelForCancelRequest billModel = new BillModelForCancelRequest();
                billModel.ActionReason = bill.ActionReason;
                billModel.ClientKey = _clientKey;
                billModel.sadadInvoiceNumber = bill.BillInvoiceNumber;
                var result = await CancelBill(billModel);
                return result;
            }
        }

        public async Task<string> CreateNewBill(NewBillModel newBillModel)
        {

            HttpClientHandler handler = CertificaetHandler();
            using (var client = new HttpClient(handler))
            {
                var request = new HttpRequestMessage
                {
                    RequestUri = new Uri(_rootConfiguration.BillingConfiguration.URL/* _configuration.GetSection("Billing:URL").Value*/ + $"/api/RequestInvoice/RequestInvoiceAsync"),
                    Method = HttpMethod.Post,
                };
                var username = _rootConfiguration.EsbSettingsConfiguration.PayemntNotificationUserName;
                var pass = _rootConfiguration.EsbSettingsConfiguration.PayemntNotificationUserPassword;
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(string.Format("{0}:{1}", username, pass))));
                var newObj = JsonConvert.SerializeObject(new { newBillModel });
                newObj = newObj.Replace("{\"newBillModel\":", "");
                newObj = newObj.Replace("}}", "}");
                HttpContent content = new StringContent(newObj, Encoding.UTF8, "application/json");
                request.Content = content;
                request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var result = await client.SendAsync(request);
                using (var stream = await result.Content.ReadAsStreamAsync())
                {
                    using (var streamReader = new StreamReader(stream))
                    {
                        using (var reader = new JsonTextReader(streamReader))
                        {
                            var serializer = new JsonSerializer();
                            var resultModel = serializer.Deserialize<NewBillModel>(reader);
                            return resultModel.sadadInvoiceNumber;
                        }
                    }
                }
            }


        }

        public async Task<bool> CancelBill(BillModelForCancelRequest newBillModel)
        {

            HttpClientHandler handler = CertificaetHandler();
            using (var client = new HttpClient(handler))
            {
                var request = new HttpRequestMessage
                {
                    RequestUri = new Uri(_rootConfiguration.BillingConfiguration.URL + $"/api/RequestInvoice/CancelInvoice"),
                    Method = HttpMethod.Post,
                };
                var username = _rootConfiguration.EsbSettingsConfiguration.PayemntNotificationUserName;
                var pass = _rootConfiguration.EsbSettingsConfiguration.PayemntNotificationUserPassword;
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(string.Format("{0}:{1}", username, pass))));
                var newObj = JsonConvert.SerializeObject(new { newBillModel });
                newObj = newObj.Replace("{\"newBillModel\":", "");
                newObj = newObj.Replace("}}", "}");
                HttpContent content = new StringContent(newObj, Encoding.UTF8, "application/json");
                request.Content = content;
                request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var result = await client.SendAsync(request);
                using (var stream = await result.Content.ReadAsStreamAsync())
                {
                    using (var streamReader = new StreamReader(stream))
                    {
                        using (var reader = new JsonTextReader(streamReader))
                        {
                            var serializer = new JsonSerializer();
                            var resultModel = serializer.Deserialize<NewBillModel>(reader);
                            if (resultModel.StatusCode == 1)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                }
            }
        }

        public async Task<bool> UpdateBill(BillModelForUpdateRequest newBillModel)
        {

            HttpClientHandler handler = CertificaetHandler();
            using (var client = new HttpClient(handler))
            {
                var request = new HttpRequestMessage
                {
                    RequestUri = new Uri(_rootConfiguration.BillingConfiguration.URL + $"/api/RequestInvoice/UpdateInvoiceExpireDate"),
                    Method = HttpMethod.Post,
                };
                var username = _rootConfiguration.EsbSettingsConfiguration.PayemntNotificationUserName;
                var pass = _rootConfiguration.EsbSettingsConfiguration.PayemntNotificationUserPassword;
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(string.Format("{0}:{1}", username, pass))));
                var newObj = JsonConvert.SerializeObject(new { newBillModel });
                newObj = newObj.Replace("{\"newBillModel\":", "");
                newObj = newObj.Replace("}}", "}");
                HttpContent content = new StringContent(newObj, Encoding.UTF8, "application/json");
                request.Content = content;
                request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var result = await client.SendAsync(request);
                using (var stream = await result.Content.ReadAsStreamAsync())
                {
                    using (var streamReader = new StreamReader(stream))
                    {
                        using (var reader = new JsonTextReader(streamReader))
                        {
                            var serializer = new JsonSerializer();
                            var resultModel = serializer.Deserialize<NewBillModel>(reader);
                            if (resultModel.StatusCode == 1)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                }
            }



        }

    }
}
