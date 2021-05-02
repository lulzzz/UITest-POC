using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.Integration.Infrastructure;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Integration
{
    public class IDMProxy : ProxyBase, IIDMProxy
    {
        public IDMProxy(IOptionsSnapshot<RootConfigurations> rootConfiguration) : base(rootConfiguration)
        {
        }
        #region Methods

        public async Task<List<EmployeeIntegrationModel>> GetEmployeeDetailsByRoleName(string roleName)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(_rootConfiguration.APIConfiguration.IDM);
            client.DefaultRequestHeaders.Accept.Clear();
            client.Timeout = new TimeSpan(0, 30, 0);
            client.DefaultRequestHeaders.Add("clientid", _rootConfiguration.AuthorityConfiguration.clientid);
            client.DefaultRequestHeaders.Add("clientsecret", _rootConfiguration.AuthorityConfiguration.clientsecret);
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            var response = await client.GetAsync($"api/common/getusersbyrolesname?" + "rolesName=" + roleName);
            using (var stream = await response.Content.ReadAsStreamAsync())
            {
                using (var streamReader = new StreamReader(stream))
                {
                    using (var reader = new JsonTextReader(streamReader))
                    {
                        var serializer = new JsonSerializer();
                        var resultModel = serializer.Deserialize<List<EmployeeIntegrationModel>>(reader);
                        return resultModel;
                    }
                }
            }
        }

        public async Task<QueryResult<SupplierIntegrationModel>> GetSuppliersBySearchCriteria(SupplierIntegrationSearchCriteria searchCriteria)
        {
            if (!string.IsNullOrEmpty(searchCriteria.CrNumber))
            {
                searchCriteria.CrNumber = searchCriteria.CrNumber.Trim();
            }
            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage
                {
                    RequestUri = new Uri(_rootConfiguration.APIConfiguration.IDM + $"/api/Supplier/search"),
                    Method = HttpMethod.Post,
                };
                request.Headers.Add("clientid", _rootConfiguration.AuthorityConfiguration.clientid);
                request.Headers.Add("clientsecret", _rootConfiguration.AuthorityConfiguration.clientsecret);
                if (searchCriteria.SupplierNumbers.Count == 0 && !string.IsNullOrEmpty(searchCriteria.CrNumber))
                    searchCriteria.SupplierNumbers = new List<string> { searchCriteria.CrNumber };
                HttpContent content = new StringContent(JsonConvert.SerializeObject(new
                {
                    searchCriteria.SupplierNumbers,
                    searchCriteria.NationalId,
                    searchCriteria.IsCountOnly,
                    searchCriteria.SupplierName,
                    searchCriteria.Activity,
                    searchCriteria.activityDescription,
                    searchCriteria.isicActivityID,
                    searchCriteria.isicActivityLevel,
                    searchCriteria.CityName,
                    searchCriteria.Email,
                    searchCriteria.PageSize,
                    searchCriteria.PageNumber,
                    searchCriteria.Sort,
                    searchCriteria.SortDirection,
                    searchCriteria.IsRandomSort
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
                            var resultModel = serializer.Deserialize<QueryResult<SupplierIntegrationModel>>(reader);
                            return resultModel;
                        }
                    }
                }
            }
        }

        public async Task<QueryResult<EmployeeIntegrationModel>> GetMonafasatUsersByAgencyType(UsersSearchCriteriaModel searchCriteria)
        {
            if (!string.IsNullOrEmpty(searchCriteria.Email))
            {
                searchCriteria.Email = searchCriteria.Email.Trim();
            }
            if (!string.IsNullOrEmpty(searchCriteria.Name))
            {
                searchCriteria.Name = searchCriteria.Name.Trim();
            }
            if (!string.IsNullOrEmpty(searchCriteria.MobileNumber))
            {
                searchCriteria.MobileNumber = searchCriteria.MobileNumber.Trim();
            }
            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage
                {
                    RequestUri = new Uri(_rootConfiguration.APIConfiguration.IDM + $"/api/Agency/SearchUsers"),
                    Method = HttpMethod.Post,
                };
                request.Headers.Add("clientid", _rootConfiguration.AuthorityConfiguration.clientid);
                request.Headers.Add("clientsecret", _rootConfiguration.AuthorityConfiguration.clientsecret);
                string[] RolesNames = RoleNames.GetMonafasatRoles();

                if (searchCriteria.RoleNames == null || searchCriteria.RoleNames.Count == 0)
                {
                    searchCriteria.RoleNames = new List<string>();
                    searchCriteria.RoleNames.AddRange(RolesNames);
                }
                HttpContent content = new StringContent(JsonConvert.SerializeObject(new
                {
                    searchCriteria.AgencyId,
                    searchCriteria.AgencyType,
                    searchCriteria.RoleNames,
                    searchCriteria.Name,
                    searchCriteria.MobileNumber,
                    searchCriteria.NationalIds,
                    searchCriteria.Email,
                    searchCriteria.PageSize,
                    searchCriteria.PageNumber,
                    searchCriteria.SortDirection,
                    searchCriteria.AssignedUserIds
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
                            var resultModel = serializer.Deserialize<QueryResult<EmployeeIntegrationModel>>(reader);
                            return resultModel;
                        }
                    }
                }
            }
        }
        public async Task<QueryResult<EmployeeIntegrationModel>> GetMonafasatUsersByAgencyTypeAndRoleName(UsersSearchCriteriaModel searchCriteria)
        {
            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage
                {
                    RequestUri = new Uri(_rootConfiguration.APIConfiguration.IDM + $"/api/Agency/SearchUsers"),
                    Method = HttpMethod.Post,
                };
                request.Headers.Add("clientid", _rootConfiguration.AuthorityConfiguration.clientid);
                request.Headers.Add("clientsecret", _rootConfiguration.AuthorityConfiguration.clientsecret);
                searchCriteria.RoleNames.Add(searchCriteria.RoleName);
                HttpContent content = new StringContent(JsonConvert.SerializeObject(new
                {
                    searchCriteria.AgencyId,
                    searchCriteria.AgencyType,
                    searchCriteria.RoleNames,
                    PageSize = 1000
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
                            if (reader == null)
                            {
                                return new QueryResult<EmployeeIntegrationModel>(new List<EmployeeIntegrationModel>().AsEnumerable(), 0, 1, 10);
                            }

                            var serializer = new JsonSerializer();
                            var resultModel = serializer.Deserialize<QueryResult<EmployeeIntegrationModel>>(reader);
                            return resultModel;
                        }
                    }

                }
            }
        }

        public async Task<AgencyInfoModel> GetAgencyDetailsRelatedToSadad(string agencyCode, int agencyType)
        {
            HttpClient client = new HttpClient();
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(_rootConfiguration.APIConfiguration.IDM + $"/api/Agency/SadadDetails"),
                Method = HttpMethod.Post,
            };
            request.Headers.Add("clientid", _rootConfiguration.AuthorityConfiguration.clientid);
            request.Headers.Add("clientsecret", _rootConfiguration.AuthorityConfiguration.clientsecret);
            HttpContent content = new StringContent(JsonConvert.SerializeObject(new
            {
                agencyType,
                agencyCode
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
                        var resultModel = serializer.Deserialize<AgencyInfoModel>(reader);
                        return resultModel;
                    }
                }
            }
        }
        public async Task<UserAPIModel> GetUserbyUserName(string userName)
        {

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(_rootConfiguration.APIConfiguration.IDM);
            client.DefaultRequestHeaders.Accept.Clear();
            client.Timeout = new TimeSpan(0, 30, 0);
            client.DefaultRequestHeaders.Add("clientid", _rootConfiguration.AuthorityConfiguration.clientid);
            client.DefaultRequestHeaders.Add("clientsecret", _rootConfiguration.AuthorityConfiguration.clientsecret);
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            var response = await client.GetAsync($"api/common/getuserbyusername?" + "userName=" + userName);
            using (var stream = await response.Content.ReadAsStreamAsync())
            {
                using (var streamReader = new StreamReader(stream))
                {
                    using (var reader = new JsonTextReader(streamReader))
                    {
                        var serializer = new JsonSerializer();
                        var resultModel = serializer.Deserialize<UserAPIModel>(reader);
                        return resultModel;
                    }
                }
            }




        }
        public async Task<Dictionary<string, string>> GetAgencyLogos(List<string> agencyCodes)
        {
            HttpClient client = new HttpClient();
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(_rootConfiguration.APIConfiguration.IDM + $"/api/Agency/GetAgencyLogo"),
                Method = HttpMethod.Post,
            };
            request.Headers.Add("clientid", _rootConfiguration.AuthorityConfiguration.clientid);
            request.Headers.Add("clientsecret", _rootConfiguration.AuthorityConfiguration.clientsecret);
            HttpContent content = new StringContent(JsonConvert.SerializeObject(new
            {
                agencyCodes
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
                        var resultModel = serializer.Deserialize<AgencyLogoDetailsModel>(reader);
                        return resultModel.logos.Where(x => !string.IsNullOrEmpty(x.logoReferenceId)).ToDictionary(r => r.agencyCode, r => CheckURL(r));
                    }
                }
            }
        }

        private string CheckURL(AgencyLogoModel r)
        {
            var url = r.logoReferenceId.StartsWith('/') ? r.logoReferenceId.Substring(1) : r.logoReferenceId;
            return _rootConfiguration.APIConfiguration.IDM /*configuration.GetSection("API:IDM").Value*/ + "/" + url;
        }

        public async Task<SupplierFullDataModel> GetSupplierInfoByCR(string supplierNumber)
        {
            HttpClient client = new HttpClient();
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(_rootConfiguration.APIConfiguration.IDM + $"/api/Supplier/getProfile"),
                Method = HttpMethod.Post,
            };
            request.Headers.Add("clientid", _rootConfiguration.AuthorityConfiguration.clientid);
            request.Headers.Add("clientsecret", _rootConfiguration.AuthorityConfiguration.clientsecret);

            HttpContent content = new StringContent(JsonConvert.SerializeObject(new
            {
                supplierNumber
            }), Encoding.UTF8, "application/json");
            request.Content = content;
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json"); // change as necessary
            var result = await client.SendAsync(request);
            using (var stream = await result.Content.ReadAsStreamAsync())
            {
                using (var streamReader = new StreamReader(stream))
                {
                    using (var reader = new JsonTextReader(streamReader))
                    {
                        try
                        {
                            var serializer = new JsonSerializer();
                            var resultModel = serializer.Deserialize<SupplierFullDataModel>(reader);
                            if (resultModel != null)
                            {
                                if (resultModel.supplierName != null && resultModel.supplierName.Length > 99)
                                {
                                    resultModel.supplierName = resultModel.supplierName.Substring(0, 99);
                                }
                                if (resultModel.CRNameEN != null && resultModel.CRNameEN.Length > 99)
                                {
                                    resultModel.CRNameEN = resultModel.CRNameEN.Substring(0, 99);
                                }
                            }
                            return resultModel;
                        }
                        catch (Exception)
                        {

                            return new SupplierFullDataModel();
                        }
                    }
                }
            }
        }

        public async Task<QueryResult<ContactOfficerModel>> GetContactOfficersByCR(List<string> supplierNumbers)
        {
            HttpClient client = new HttpClient();
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(_rootConfiguration.APIConfiguration.IDM + $"/api/Supplier/searchOfficers"),
                Method = HttpMethod.Post,
            };
            request.Headers.Add("clientid", _rootConfiguration.AuthorityConfiguration.clientid);
            request.Headers.Add("clientsecret", _rootConfiguration.AuthorityConfiguration.clientsecret);
            HttpContent content = new StringContent(JsonConvert.SerializeObject(new
            {
                supplierNumbers = supplierNumbers.ToArray(),
                PageSize = 100
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
                        if (reader == null)
                        {
                            return new QueryResult<ContactOfficerModel>(new List<ContactOfficerModel>().AsEnumerable(), 0, 1, 10);
                        }
                        var serializer = new JsonSerializer();
                        try
                        {
                            return serializer.Deserialize<QueryResult<ContactOfficerModel>>(reader);
                        }
                        catch (Exception ex)
                        {
                            var responseString = await result.Content.ReadAsStringAsync();
                            Logger.LogToFile(request,responseString);
                            throw new ArgumentNullException(string.Join(',', supplierNumbers));
                        }
                    }
                }
            }
        }
        #endregion
    }
}