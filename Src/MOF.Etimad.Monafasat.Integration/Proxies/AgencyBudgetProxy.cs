using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.ViewModel.AgencyBudget;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Integration
{
    public class AgencyBudgetProxy : ProxyBase, IAgencyBudgetProxy
    {
        public AgencyBudgetProxy(IOptionsSnapshot<RootConfigurations> rootConfiguration) : base(rootConfiguration)
        {
        }
        public async Task<AgencyBudgetModel> GetAgencyProjectBudget(string ProjectNumber, bool IsGfs, string AgencyCode)
        {
            if (_rootConfiguration.isAgencyBudgetConfiguration.Value.ToString().ToLower() == "false")
            {
                AgencyBudgetModel obj = new AgencyBudgetModel()
                { ProjectName = "خدمات الهاتف الجوال", Cash = 10, Cost = 0 };
                return obj;
            }
            else
            {
                HttpClient client = new HttpClient();
                var request = new HttpRequestMessage
                {
                    RequestUri = new Uri(_rootConfiguration.EtimadAgencyBudgetConfiguration.EtimadAddress + $"/api/ProjectBudget/GetAgencyProjectBudget"),
                    Method = HttpMethod.Get,
                };
                request.Headers.Add("client-id", _rootConfiguration.EtimadAgencyBudgetConfiguration.clientid);
                request.Headers.Add("client-secret", _rootConfiguration.EtimadAgencyBudgetConfiguration.ClientSecret);
                HttpContent content = new StringContent(JsonConvert.SerializeObject(new
                {
                    ProjectNumber,
                    IsGfs,
                    AgencyCode
                }), Encoding.UTF8, "application/json");
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
                            var resultModel = serializer.Deserialize<AgencyBudgetModel>(reader);
                            return resultModel;
                        }
                    }
                }
            }
        }
    }
}
