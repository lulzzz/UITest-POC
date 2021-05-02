using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.ViewModel.QuantityTableTemplates;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Integration
{
    public class QuantityTemplatesProxy : ProxyBase, IQuantityTemplatesProxy
    {
        public QuantityTemplatesProxy(IOptionsSnapshot<RootConfigurations> rootConfiguration) : base(rootConfiguration)
        {
        }

        #region Methods

        public async Task<ApiResponse<HtmlTemplateDto>> GetTemplateFormHtml(int tenderId, int FormId = 1, int YearsCount = 5, long tableId = 0)
        {

            HttpClientHandler handler = CertificaetHandler();
            using HttpClient client = new HttpClient(handler);
            client.BaseAddress = new Uri( /*configuration.GetSection("API:QuantityTemplates").Value*/ _rootConfiguration.APIConfiguration.QuantityTemplates);
            client.DefaultRequestHeaders.Accept.Clear();
            client.Timeout = new TimeSpan(0, 30, 0);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.GetAsync($"api/QuantityTable/GetEmptyForm/" + FormId + "/" + tenderId + "/" + YearsCount + "/" + tableId);
            using (var stream = await response.Content.ReadAsStreamAsync())
            {
                using (var streamReader = new StreamReader(stream))
                {
                    using (var reader = new JsonTextReader(streamReader))
                    {
                        var serializer = new JsonSerializer();
                        var resultModel = serializer.Deserialize<ApiResponse<HtmlTemplateDto>>(reader);
                        return resultModel;
                    }
                }
            }
        }


        public async Task<ApiResponse<HtmlTemplateDto>> GetBayanTemplateFormHtml(int tenderId, int FormId = 1, int YearsCount = 5, long tableId = 0)
        {

            HttpClientHandler handler = CertificaetHandler();
            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri( /*configuration.GetSection("API:QuantityTemplates").Value*/ _rootConfiguration.APIConfiguration.QuantityTemplates);
                client.DefaultRequestHeaders.Accept.Clear();
                client.Timeout = new TimeSpan(0, 30, 0);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync($"api/QuantityTable/GetEmptyForm/4/" + FormId + "/" + tenderId + "/" + YearsCount + "/" + tableId);
                using (var stream = await response.Content.ReadAsStreamAsync())
                {
                    using (var streamReader = new StreamReader(stream))
                    {
                        using (var reader = new JsonTextReader(streamReader))
                        {
                            var serializer = new JsonSerializer();
                            var resultModel = serializer.Deserialize<ApiResponse<HtmlTemplateDto>>(reader);
                            return resultModel;
                        }
                    }
                }
            }
        }

        public async Task<ApiResponse<List<HtmlTemplateDto>>> GetTemplateFormHtml(int tenderId, int offerId, int FormId = 1, int YearsCount = 5, long tableId = 0)
        {

            HttpClientHandler handler = CertificaetHandler();
            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri( /*configuration.GetSection("API:QuantityTemplates").Value*/ _rootConfiguration.APIConfiguration.QuantityTemplates);
                client.DefaultRequestHeaders.Accept.Clear();
                client.Timeout = new TimeSpan(0, 30, 0);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync($"api/Committee/GetEmptyForm/" + FormId + "/" + tenderId + "/" + YearsCount + "/" + tableId + "/" + Util.Encrypt(offerId));
                using (var stream = await response.Content.ReadAsStreamAsync())
                {
                    using (var streamReader = new StreamReader(stream))
                    {
                        using (var reader = new JsonTextReader(streamReader))
                        {
                            var serializer = new JsonSerializer();
                            var resultModel = serializer.Deserialize<ApiResponse<List<HtmlTemplateDto>>>(reader);
                            return resultModel;
                        }
                    }
                }
            }

        }

        public async Task<int> GetQuantityTableVersion()
        {

            HttpClientHandler handler = CertificaetHandler();
            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri( /*configuration.GetSection("API:QuantityTemplates").Value*/ _rootConfiguration.APIConfiguration.QuantityTemplates);
                client.DefaultRequestHeaders.Accept.Clear();
                client.Timeout = new TimeSpan(0, 30, 0);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync($"api/QuantityTable/GetPublishedVersion");

                using (var stream = await response.Content.ReadAsStreamAsync())
                {
                    using (var streamReader = new StreamReader(stream))
                    {
                        return int.Parse(await streamReader.ReadToEndAsync());
                    }
                }
            }

        }

        public async Task<string> GetTemplate(int tenderId, int FormId = 1, int YearsCount = 5)
        {

            HttpClientHandler handler = CertificaetHandler();
            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri( /*configuration.GetSection("API:QuantityTemplates").Value*/ _rootConfiguration.APIConfiguration.QuantityTemplates);
                client.DefaultRequestHeaders.Accept.Clear();
                client.Timeout = new TimeSpan(0, 30, 0);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync($"api/QuantityTableApi/GetFormItemTemplate?FormId=" + FormId + "&YearsCount=" + YearsCount + "&TenderId=" + tenderId);
                using (var stream = await response.Content.ReadAsStreamAsync())
                {
                    using (var streamReader = new StreamReader(stream))
                    {
                        return await streamReader.ReadToEndAsync();
                    }
                }
            }

        }
        public async Task<ApiResponse<List<long>>> GetMadatoryListColumnIdByTemplatesId(int versionid, List<int> items)
        {

            HttpClientHandler handler = CertificaetHandler();
            using (var client = new HttpClient(handler))
            {
                {
                    var request = new HttpRequestMessage
                    {
                        RequestUri = new Uri(_rootConfiguration.APIConfiguration.QuantityTemplates + $"/api/QuantityTable/GetMadatoryListColumnIdByTemplatesId/"+ versionid),
                        Method = HttpMethod.Post,
                    };
                    var x = JsonConvert.SerializeObject(new { items });
                    x = x.Replace("{\"DTOModel\":", "");
                    x = x.Replace("}}", "}");
                    HttpContent content = new StringContent(x, Encoding.UTF8, "application/json");
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
                                var resultModel = serializer.Deserialize<ApiResponse<List<long>>>(reader);
                                return resultModel;
                            }
                        }
                    }
                }
            }

        }
        public async Task<ApiResponse<List<HtmlTemplateDto>>> GetMonafasatGOVReadOnly(FormConfigurationDTO DTOModel)
        {

            HttpClientHandler handler = CertificaetHandler();
            using (var client = new HttpClient(handler))
            {
                {
                    var request = new HttpRequestMessage
                    {
                        RequestUri = new Uri( /*configuration.GetSection("API:QuantityTemplates").Value*/ _rootConfiguration.APIConfiguration.QuantityTemplates + $"/api/QuantityTable/GenerateReadOnlyTemplate/1"),
                        Method = HttpMethod.Post,
                    };
                    var x = JsonConvert.SerializeObject(new { DTOModel });
                    x = x.Replace("{\"DTOModel\":", "");
                    x = x.Replace("}}", "}");
                    HttpContent content = new StringContent(x, Encoding.UTF8, "application/json");
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
                                var resultModel = serializer.Deserialize<ApiResponse<List<HtmlTemplateDto>>>(reader);
                                return resultModel;
                            }
                        }
                    }
                }
            }

        }

        public async Task<ApiResponse<List<HtmlTemplateDto>>> GetMonafasatGOV(FormConfigurationDTO DTOModel)
        {

            HttpClientHandler handler = CertificaetHandler();
            using (var client = new HttpClient(handler))
            {
                {
                    var request = new HttpRequestMessage
                    {
                        RequestUri = new Uri( /*configuration.GetSection("API:QuantityTemplates").Value*/ _rootConfiguration.APIConfiguration.QuantityTemplates + $"/api/QuantityTable/Gov/GenerateGOVTemplate"),
                        Method = HttpMethod.Post,
                    };
                    var x = JsonConvert.SerializeObject(new { DTOModel });
                    x = x.Replace("{\"DTOModel\":", "");
                    x = x.Replace("}}", "}");
                    HttpContent content = new StringContent(x, Encoding.UTF8, "application/json");
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
                                var resultModel = serializer.Deserialize<ApiResponse<List<HtmlTemplateDto>>>(reader);
                                return resultModel;
                            }
                        }
                    }
                }
            }

        }

        public async Task<ApiResponse<List<HtmlTemplateDto>>> GetSupplierTemplatesToApplyOffer(FormConfigurationDTO DTOModel)
        {

            HttpClientHandler handler = CertificaetHandler();
            using HttpClient client = new HttpClient(handler);
            {
                HttpRequestMessage request = new HttpRequestMessage
                {
                    RequestUri = new Uri( /*configuration.GetSection("API:QuantityTemplates").Value*/ _rootConfiguration.APIConfiguration.QuantityTemplates + $"/api/QuantityTable/Supplier/GetSupplierTemplates"),
                    Method = HttpMethod.Post,
                };
                var x = JsonConvert.SerializeObject(new { DTOModel });
                x = x.Replace("{\"DTOModel\":", "");
                x = x.Replace("}}", "}");
                HttpContent content = new StringContent(x, Encoding.UTF8, "application/json");
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
                            var resultModel = serializer.Deserialize<ApiResponse<List<HtmlTemplateDto>>>(reader);
                            return resultModel;
                        }
                    }
                }
            }

        }

        public async Task<ApiResponse<List<HtmlTemplateDto>>> GenerateGovTableTemplate(FormConfigurationDTO DTOModel)
        {

            HttpClientHandler handler = CertificaetHandler();
            using HttpClient client = new HttpClient(handler);
            {
                var request = new HttpRequestMessage
                {
                    RequestUri = new Uri( /*configuration.GetSection("API:QuantityTemplates").Value*/ _rootConfiguration.APIConfiguration.QuantityTemplates + $"/api/QuantityTable/GenerateTemplate/1"),
                    Method = HttpMethod.Post,
                };
                var x = JsonConvert.SerializeObject(new { DTOModel });
                x = x.Replace("{\"" + nameof(DTOModel) + "\":", "");
                x = x.Replace("}}", "}");
                HttpContent content = new StringContent(x, Encoding.UTF8, "application/json");
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
                            var resultModel = serializer.Deserialize<ApiResponse<List<HtmlTemplateDto>>>(reader);
                            return resultModel;
                        }
                    }
                }
            }

        }

        public async Task<ApiResponse<List<TemplateFormDTO>>> GetActivitiesTables(TenderActivityDTO tenderActivityDTO)
        {
            HttpClientHandler handler = CertificaetHandler();
            using HttpClient client = new HttpClient(handler);
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(_rootConfiguration.APIConfiguration.QuantityTemplates + $"/api/QuantityTable/GenerateActivitiesTemplate/1/" + tenderActivityDTO.VesionId),
                Method = HttpMethod.Post,
            };
            var x = JsonConvert.SerializeObject(new { tenderActivityDTO });
            x = x.Replace("{\"" + nameof(tenderActivityDTO) + "\":", "");
            x = x.Replace("}}", "}");
            HttpContent content = new StringContent(x, Encoding.UTF8, "application/json");
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
                        var resultModel = serializer.Deserialize<ApiResponse<List<TemplateFormDTO>>>(reader);
                        return resultModel;
                    }
                }
            }
        }

        private HttpClientHandler CertificaetHandler()
        {
            return new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, certificate, chain, errors) =>
                {
                    if (!_rootConfiguration.EsbSettingsConfiguration.IsProduction) return true;
                    return errors == SslPolicyErrors.None;
                }
            };
        }

        public async Task<ApiResponse<List<TemplateFormDTO>>> GenerateTemplatesByFormIds(List<long> formIds)
        {

            HttpClientHandler handler = CertificaetHandler();
            using (var client = new HttpClient(handler))
            {

                var request = new HttpRequestMessage
                {
                    RequestUri = new Uri(_rootConfiguration.APIConfiguration.QuantityTemplates + $"/api/QuantityTable/GenerateTemplatesByFormIds/1"),
                    Method = HttpMethod.Post,
                };
                var x = JsonConvert.SerializeObject(new { formIds });
                x = x.Replace("{\"" + nameof(formIds) + "\":", "");
                x = x.Replace("}", "}");
                HttpContent content = new StringContent(x, Encoding.UTF8, "application/json");
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
                            var resultModel = serializer.Deserialize<ApiResponse<List<TemplateFormDTO>>>(reader);
                            return resultModel;
                        }
                    }
                }
            }

        }

        public async Task<ApiResponse<List<HtmlTemplateDto>>> GetBayanMonafasatGOV(FormConfigurationDTO DTOModel)
        {

            HttpClientHandler handler = CertificaetHandler();
            using (var client = new HttpClient(handler))
            {

                var request = new HttpRequestMessage
                {
                    RequestUri = new Uri( /*configuration.GetSection("API:QuantityTemplates").Value*/ _rootConfiguration.APIConfiguration.QuantityTemplates + $"/api/QuantityTable/GenerateTemplate/4"),
                    Method = HttpMethod.Post,
                };
                var x = JsonConvert.SerializeObject(new { DTOModel });
                x = x.Replace("{\"DTOModel\":", "");
                x = x.Replace("}}", "}");
                HttpContent content = new StringContent(x, Encoding.UTF8, "application/json");
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
                            var resultModel = serializer.Deserialize<ApiResponse<List<HtmlTemplateDto>>>(reader);
                            return resultModel;
                        }
                    }
                }

            }

        }

        public async Task<ApiResponse<List<HtmlTemplateDto>>> GenerateTemplate(FormConfigurationDTO DTOModel)
        {

            HttpClientHandler handler = CertificaetHandler();
            using (var client = new HttpClient(handler))
            {

                var request = new HttpRequestMessage
                {
                    RequestUri = new Uri( /*configuration.GetSection("API:QuantityTemplates").Value*/ _rootConfiguration.APIConfiguration.QuantityTemplates + $"/api/QuantityTable/GenerateBookletTemplate/1"),
                    Method = HttpMethod.Post,
                };
                var x = JsonConvert.SerializeObject(new { DTOModel });
                x = x.Replace("{\"DTOModel\":", "");
                x = x.Replace("}}", "}");
                HttpContent content = new StringContent(x, Encoding.UTF8, "application/json");
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
                            var resultModel = serializer.Deserialize<ApiResponse<List<HtmlTemplateDto>>>(reader);
                            return resultModel;
                        }
                    }
                }

            }

        }

        public async Task<ApiResponse<HtmlTableRowTemplateDto>> ValidateTenderQuantityItem(int Years, Dictionary<string, string> collection)
        {

            HttpClientHandler handler = CertificaetHandler();
            using (var client = new HttpClient(handler))
            {

                var request = new HttpRequestMessage
                {
                    RequestUri = new Uri( /*configuration.GetSection("API:QuantityTemplates").Value*/ _rootConfiguration.APIConfiguration.QuantityTemplates + $"/api/QuantityTable/ValidateTenderQuantityItem/4/" + Years),
                    Method = HttpMethod.Post,
                };
                var xx = JsonConvert.SerializeObject(new { collection });
                xx = xx.Replace("{\"collection\":", "");
                xx = xx.Replace("}}", "}");
                HttpContent content = new StringContent(xx, Encoding.UTF8, "application/json");
                request.Content = content;
                request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var result = await client.SendAsync(request);
                string responseBody = "";
                using (var stream = await result.Content.ReadAsStreamAsync())
                {
                    using (var streamReader = new StreamReader(stream))
                    {
                        responseBody = streamReader.ReadToEnd();
                    }
                }
                responseBody = responseBody.Replace("{\"content\":", "");
                responseBody = responseBody.Replace("]}", "]");
                ApiResponse<HtmlTableRowTemplateDto> resultModel = JsonConvert.DeserializeObject<ApiResponse<HtmlTableRowTemplateDto>>(responseBody);
                return resultModel;

            }

        }

        public async Task<ApiResponse<HtmlTableRowTemplateDto>> ImportTenderTableQuantityItems(UploadTableExcelModel model)
        {

            HttpClientHandler handler = CertificaetHandler();
            using (var client = new HttpClient(handler))
            {

                var request = new HttpRequestMessage
                {
                    RequestUri = new Uri( /*configuration.GetSection("API:QuantityTemplates").Value*/ _rootConfiguration.APIConfiguration.QuantityTemplates + $"/api/QuantityTable/GOV/ValidateTenderQuantityItems"),
                    Method = HttpMethod.Post,
                };
                var xx = JsonConvert.SerializeObject(new { model });
                xx = xx.Replace("{\"model\":", "");
                xx = xx.Replace("}}", "}");
                HttpContent content = new StringContent(xx, Encoding.UTF8, "application/json");
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
                            var resultModel = serializer.Deserialize<ApiResponse<HtmlTableRowTemplateDto>>(reader);
                            return resultModel;
                        }
                    }
                }

            }

        }



        #endregion


        #region Open Offers

        public async Task<ApiResponse<List<HtmlTemplateDto>>> GetMonafasatSupplierForOpening(FormConfigurationDTO DTOModel)
        {

            HttpClientHandler handler = CertificaetHandler();
            using (var client = new HttpClient(handler))
            {
                var request = new HttpRequestMessage
                {
                    RequestUri = new Uri( /*configuration.GetSection("API:QuantityTemplates").Value*/ _rootConfiguration.APIConfiguration.QuantityTemplates + $"/api/QuantityTable/Supplier/GenerateSupplierTemplate"),
                    Method = HttpMethod.Post,
                };
                var x = JsonConvert.SerializeObject(new { DTOModel });
                x = x.Replace("{\"DTOModel\":", "");
                x = x.Replace("}}", "}");
                HttpContent content = new StringContent(x, Encoding.UTF8, "application/json");
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
                            var resultModel = serializer.Deserialize<ApiResponse<List<HtmlTemplateDto>>>(reader);
                            return resultModel;
                        }
                    }
                }

            }

        }

        #region Checking

        public async Task<ApiResponse<List<HtmlTemplateDto>>> GetMonafasatSupplierForChecking(FormConfigurationDTO DTOModel)
        {

            HttpClientHandler handler = CertificaetHandler();
            using (var client = new HttpClient(handler))
            {

                var request = new HttpRequestMessage
                {
                    RequestUri = new Uri( /*configuration.GetSection("API:QuantityTemplates").Value*/ _rootConfiguration.APIConfiguration.QuantityTemplates + $"/api/QuantityTable/Committee/GenerateCommitteeTemplate"),
                    Method = HttpMethod.Post,
                };
                var x = JsonConvert.SerializeObject(new { DTOModel });
                x = x.Replace("{\"DTOModel\":", "");
                x = x.Replace("}}", "}");
                HttpContent content = new StringContent(x, Encoding.UTF8, "application/json");
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
                            var resultModel = serializer.Deserialize<ApiResponse<List<HtmlTemplateDto>>>(reader);
                            return resultModel;
                        }
                    }
                }

            }

        }

        public async Task<ApiResponse<List<HtmlTemplateDto>>> GenerateCostTable(FormConfigurationDTO DTOModel)
        {

            HttpClientHandler handler = CertificaetHandler();
            using (var client = new HttpClient(handler))
            {

                var request = new HttpRequestMessage
                {
                    RequestUri = new Uri( /*configuration.GetSection("API:QuantityTemplates").Value*/ _rootConfiguration.APIConfiguration.QuantityTemplates + $"/api/QuantityTable/GenerateCostTable"),
                    Method = HttpMethod.Post,
                };
                var x = JsonConvert.SerializeObject(new { DTOModel });
                x = x.Replace("{\"DTOModel\":", "");
                x = x.Replace("}}", "}");
                HttpContent content = new StringContent(x, Encoding.UTF8, "application/json");
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
                            var resultModel = serializer.Deserialize<ApiResponse<List<HtmlTemplateDto>>>(reader);
                            return resultModel;
                        }
                    }
                }

            }

        }

        public async Task<ApiResponse<TotalCostDTO>> GetTotalCostForChecking(FormConfigurationDTO DTOModel)
        {

            HttpClientHandler handler = CertificaetHandler();
            using (var client = new HttpClient(handler))
            {


                var request = new HttpRequestMessage
                {
                    RequestUri = new Uri( /*configuration.GetSection("API:QuantityTemplates").Value*/ _rootConfiguration.APIConfiguration.QuantityTemplates + $"/api/QuantityTable/GetTotalCost"),
                    Method = HttpMethod.Post,
                };
                var x = JsonConvert.SerializeObject(new { DTOModel });
                x = x.Replace("{\"DTOModel\":", "");
                x = x.Replace("}}", "}");
                HttpContent content = new StringContent(x, Encoding.UTF8, "application/json");
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
                            var resultModel = serializer.Deserialize<ApiResponse<TotalCostDTO>>(reader);
                            return resultModel;
                        }
                    }
                }
            }

        }

        /// <summary>
        /// Validate Data 
        /// </summary>
        /// <param name="DTOModel"></param>
        /// <returns></returns>
        public async Task<ApiResponse<List<TableTemplateDTO>>> ValidateCheckingData(FormConfigurationDTO DTOModel)
        {

            HttpClientHandler handler = CertificaetHandler();
            using (var client = new HttpClient(handler))
            {

                var request = new HttpRequestMessage
                {
                    RequestUri = new Uri( /*configuration.GetSection("API:QuantityTemplates").Value*/ _rootConfiguration.APIConfiguration.QuantityTemplates + $"/api/QuantityTable/Committee/ValidateCommitteeQuantityItem"),
                    Method = HttpMethod.Post,
                };
                var x = JsonConvert.SerializeObject(new { DTOModel });
                x = x.Replace("{\"DTOModel\":", "");
                x = x.Replace("}}", "}");
                HttpContent content = new StringContent(x, Encoding.UTF8, "application/json");
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
                            var resultModel = serializer.Deserialize<ApiResponse<List<TableTemplateDTO>>>(reader);
                            return resultModel;
                        }
                    }
                }
            }

        }

        #endregion Checking

        /// <summary>
        /// Validate Data 
        /// </summary>
        /// <param name="DTOModel"></param>
        /// <returns></returns>
        public async Task<ApiResponse<List<TableTemplateDTO>>> ValidateOpeningData(FormConfigurationDTO DTOModel)
        {

            HttpClientHandler handler = CertificaetHandler();
            using (var client = new HttpClient(handler))
            {

                var request = new HttpRequestMessage
                {
                    RequestUri = new Uri( /*configuration.GetSection("API:QuantityTemplates").Value*/ _rootConfiguration.APIConfiguration.QuantityTemplates + $"/api/QuantityTable/Committee/ValidateCommitteeQuantityItem"),
                    Method = HttpMethod.Post,
                };
                var x = JsonConvert.SerializeObject(new { DTOModel });
                x = x.Replace("{\"DTOModel\":", "");
                x = x.Replace("}}", "}");
                HttpContent content = new StringContent(x, Encoding.UTF8, "application/json");
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
                            var resultModel = serializer.Deserialize<ApiResponse<List<TableTemplateDTO>>>(reader);
                            return resultModel;
                        }
                    }
                }
            }

        }

        #endregion

        #region [SUPPIER]

        public async Task<ApiResponse<List<HtmlTemplateDto>>> GenerateSupplierTemplate(FormConfigurationDTO DTOModel)
        {

            HttpClientHandler handler = CertificaetHandler();
            using (var client = new HttpClient(handler))
            {
                var request = new HttpRequestMessage
                {
                    RequestUri = new Uri( /*configuration.GetSection("API:QuantityTemplates").Value*/ _rootConfiguration.APIConfiguration.QuantityTemplates + $"/api/QuantityTable/Supplier/GenerateSupplierTemplate"),
                    Method = HttpMethod.Post,
                };
                var x = JsonConvert.SerializeObject(new { DTOModel });
                x = x.Replace("{\"DTOModel\":", "");
                x = x.Replace("}}", "}");
                HttpContent content = new StringContent(x, Encoding.UTF8, "application/json");
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
                            var resultModel = serializer.Deserialize<ApiResponse<List<HtmlTemplateDto>>>(reader);
                            return resultModel;
                        }
                    }
                }
            }

        }

        public async Task<ApiResponse<List<HtmlTemplateDto>>> GenerateSupplierReadOnlyTemplate(FormConfigurationDTO DTOModel)
        {

            HttpClientHandler handler = CertificaetHandler();
            using (var client = new HttpClient(handler))
            {
                var request = new HttpRequestMessage
                {
                    RequestUri = new Uri( /*configuration.GetSection("API:QuantityTemplates").Value*/ _rootConfiguration.APIConfiguration.QuantityTemplates + $"/api/QuantityTable/GenerateReadOnlyTemplate/2"),
                    Method = HttpMethod.Post,
                };
                var x = JsonConvert.SerializeObject(new { DTOModel });
                x = x.Replace("{\"DTOModel\":", "");
                x = x.Replace("}}", "}");
                HttpContent content = new StringContent(x, Encoding.UTF8, "application/json");
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
                            var resultModel = serializer.Deserialize<ApiResponse<List<HtmlTemplateDto>>>(reader);
                            return resultModel;
                        }
                    }
                }
            }

        }

        public async Task<ApiResponse<List<HtmlTemplateDto>>> GenerateCommitteeReadOnlyTemplate(FormConfigurationDTO DTOModel)
        {

            HttpClientHandler handler = CertificaetHandler();
            using (var client = new HttpClient(handler))
            {
                var request = new HttpRequestMessage
                {
                    RequestUri = new Uri( /*configuration.GetSection("API:QuantityTemplates").Value*/ _rootConfiguration.APIConfiguration.QuantityTemplates + $"/api/QuantityTable/Committee/GenerateCommitteeReadOnlyTemplate"),
                    Method = HttpMethod.Post,
                };
                var x = JsonConvert.SerializeObject(new { DTOModel });
                x = x.Replace("{\"DTOModel\":", "");
                x = x.Replace("}}", "}");

                HttpContent content = new StringContent(x, Encoding.UTF8, "application/json");
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
                            var resultModel = serializer.Deserialize<ApiResponse<List<HtmlTemplateDto>>>(reader);
                            return resultModel;
                        }
                    }
                }
            }

        }

        /// <summary>
        /// Return editable Template for Opening
        /// </summary>
        /// <param name="DTOModel"></param>
        /// <returns></returns>
        public async Task<ApiResponse<List<HtmlTemplateDto>>> GenerateCommitteeTemplate(FormConfigurationDTO DTOModel)
        {

            HttpClientHandler handler = CertificaetHandler();
            using (var client = new HttpClient(handler))
            {
                var request = new HttpRequestMessage
                {
                    RequestUri = new Uri( /*configuration.GetSection("API:QuantityTemplates").Value*/ _rootConfiguration.APIConfiguration.QuantityTemplates + $"/api/QuantityTable/Committee/GenerateCommitteeTemplate"),
                    Method = HttpMethod.Post,
                };
                var x = JsonConvert.SerializeObject(new { DTOModel });
                x = x.Replace("{\"DTOModel\":", "");
                x = x.Replace("}}", "}");
                HttpContent content = new StringContent(x, Encoding.UTF8, "application/json");
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
                            var resultModel = serializer.Deserialize<ApiResponse<List<HtmlTemplateDto>>>(reader);
                            return resultModel;
                        }
                    }
                }
            }

        }

        public async Task<ApiResponse<FormConfigurationDTO>> GetMonafasatSupplierColumns(FormConfigurationDTO DTOModel)
        {


            HttpClientHandler handler = CertificaetHandler();
            using (var client = new HttpClient(handler))
            {
                var request = new HttpRequestMessage
                {
                    RequestUri = new Uri( /*configuration.GetSection("API:QuantityTemplates").Value*/ _rootConfiguration.APIConfiguration.QuantityTemplates + $"/api/QuantityTable/Supplier/GetSupplierData"),
                    Method = HttpMethod.Post,
                };
                var x = JsonConvert.SerializeObject(new { DTOModel });
                x = x.Replace("{\"DTOModel\":", "");
                x = x.Replace("}}", "}");
                HttpContent content = new StringContent(x, Encoding.UTF8, "application/json");
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
                            var resultModel = serializer.Deserialize<ApiResponse<FormConfigurationDTO>>(reader);
                            return resultModel;
                        }
                    }
                }
            }

        }

        public async Task<ApiResponse<List<TableTemplateDTO>>> ValidateSupplierData(FormConfigurationDTO DTOModel)
        {

            HttpClientHandler handler = CertificaetHandler();
            using (var client = new HttpClient(handler))
            {
                var request = new HttpRequestMessage
                {
                    RequestUri = new Uri( /*configuration.GetSection("API:QuantityTemplates").Value*/ _rootConfiguration.APIConfiguration.QuantityTemplates + $"/api/QuantityTable/Supplier/ValidateSupplierQuantityItem"),
                    Method = HttpMethod.Post,
                };
                var x = JsonConvert.SerializeObject(new { DTOModel });
                x = x.Replace("{\"DTOModel\":", "");
                x = x.Replace("}}", "}");
                HttpContent content = new StringContent(x, Encoding.UTF8, "application/json");
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
                            var resultModel = serializer.Deserialize<ApiResponse<List<TableTemplateDTO>>>(reader);
                            return resultModel;
                        }
                    }
                }
            }

        }

        /// <summary>
        /// ValidateAlternativeQuantityItem
        /// </summary>
        /// <param name="Years"></param>
        /// <param name="collection"></param>
        /// <returns></returns>
        public async Task<ApiResponse<HtmlTableRowTemplateDto>> ValidateAlternativeQuantityItem(int Years, Dictionary<string, string> collection)
        {

            HttpClientHandler handler = CertificaetHandler();
            using (var client = new HttpClient(handler))
            {
                var request = new HttpRequestMessage
                {
                    RequestUri = new Uri( /*configuration.GetSection("API:QuantityTemplates").Value*/ _rootConfiguration.APIConfiguration.QuantityTemplates + $"/api/QuantityTable/GOV/ValidateSupplierAlternativeItem/" + Years),
                    Method = HttpMethod.Post,
                };
                var xx = JsonConvert.SerializeObject(new { collection });
                xx = xx.Replace("{\"collection\":", "");
                xx = xx.Replace("}}", "}");
                HttpContent content = new StringContent(xx, Encoding.UTF8, "application/json");
                request.Content = content;
                request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var result = await client.SendAsync(request);
                string responseBody = "";

                using (var stream = await result.Content.ReadAsStreamAsync())
                {
                    using (var streamReader = new StreamReader(stream))
                    {
                        responseBody = streamReader.ReadToEnd();
                    }
                }
                responseBody = responseBody.Replace("{\"content\":", "");
                responseBody = responseBody.Replace("]}", "]");
                ApiResponse<HtmlTableRowTemplateDto> resultModel = JsonConvert.DeserializeObject<ApiResponse<HtmlTableRowTemplateDto>>(responseBody);
                return resultModel;
            }

        }

        #endregion

        #region [NEGOTIATION]

        /// <summary>
        /// Return Negotiation  Edit Template
        /// </summary>
        /// <param name="DTOModel"></param>
        /// <returns></returns>
        public async Task<ApiResponse<List<HtmlTemplateDto>>> GenerateNegotiationTemplate(FormConfigurationDTO DTOModel)
        {

            HttpClientHandler handler = CertificaetHandler();
            using (var client = new HttpClient(handler))
            {
                var request = new HttpRequestMessage
                {
                    RequestUri = new Uri( /*configuration.GetSection("API:QuantityTemplates").Value*/ _rootConfiguration.APIConfiguration.QuantityTemplates + $"/api/QuantityTable/Negotiation/GenerateTemplate"),
                    Method = HttpMethod.Post,
                };
                var x = JsonConvert.SerializeObject(new { DTOModel });
                x = x.Replace("{\"DTOModel\":", "");
                x = x.Replace("}}", "}");
                HttpContent content = new StringContent(x, Encoding.UTF8, "application/json");
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
                            var resultModel = serializer.Deserialize<ApiResponse<List<HtmlTemplateDto>>>(reader);
                            return resultModel;
                        }
                    }
                }
            }

        }
        /// <summary>
        /// Return Negotiation  Edit Template
        /// </summary>
        /// <param name="DTOModel"></param>
        /// <returns></returns>
        public async Task<ApiResponse<List<HtmlTemplateDto>>> GetNegotiationGOVTemplates(FormConfigurationDTO DTOModel)
        {

            HttpClientHandler handler = CertificaetHandler();
            using (var client = new HttpClient(handler))
            {
                var request = new HttpRequestMessage
                {
                    RequestUri = new Uri( /*configuration.GetSection("API:QuantityTemplates").Value*/ _rootConfiguration.APIConfiguration.QuantityTemplates + $"/api/QuantityTable/Negotiation/GenerateNegotiationGOVTemplates"),
                    Method = HttpMethod.Post,
                };
                var x = JsonConvert.SerializeObject(new { DTOModel });
                x = x.Replace("{\"DTOModel\":", "");
                x = x.Replace("}}", "}");
                HttpContent content = new StringContent(x, Encoding.UTF8, "application/json");
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
                            var resultModel = serializer.Deserialize<ApiResponse<List<HtmlTemplateDto>>>(reader);
                            return resultModel;
                        }
                    }
                }
            }

        }

        /// <summary>
        /// Return Negotiation  ReadOnly Template
        /// </summary>
        /// <param name="DTOModel"></param>
        /// <returns></returns>
        public async Task<ApiResponse<List<HtmlTemplateDto>>> GenerateNegotiationReadOnlyTemplate(FormConfigurationDTO DTOModel)
        {

            HttpClientHandler handler = CertificaetHandler();
            using (var client = new HttpClient(handler))
            {

                var request = new HttpRequestMessage
                {
                    RequestUri = new Uri( /*configuration.GetSection("API:QuantityTemplates").Value*/ _rootConfiguration.APIConfiguration.QuantityTemplates + $"/api/QuantityTable/Negotiation/GenerateReadOnlyTemplate"),
                    Method = HttpMethod.Post,
                };
                var x = JsonConvert.SerializeObject(new { DTOModel });
                x = x.Replace("{\"DTOModel\":", "");
                x = x.Replace("}}", "}");
                HttpContent content = new StringContent(x, Encoding.UTF8, "application/json");
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
                            var resultModel = serializer.Deserialize<ApiResponse<List<HtmlTemplateDto>>>(reader);
                            return resultModel;
                        }
                    }
                }
            }

        }

        #endregion

        #region [GENERAL]

        public async Task<ApiResponse<TotalCostDTO>> CalculateOfferFinalPricebyItems(FormConfigurationDTO DTOModel)
        {

            HttpClientHandler handler = CertificaetHandler();
            using (var client = new HttpClient(handler))
            {
                var request = new HttpRequestMessage
                {
                    RequestUri = new Uri( /*configuration.GetSection("API:QuantityTemplates").Value*/ _rootConfiguration.APIConfiguration.QuantityTemplates + $"/api/QuantityTable/Supplier/GetSupplierTotalCostNP"),
                    Method = HttpMethod.Post,
                };
                var x = JsonConvert.SerializeObject(new { DTOModel });
                x = x.Replace("{\"DTOModel\":", "");
                x = x.Replace("}}", "}");
                HttpContent content = new StringContent(x, Encoding.UTF8, "application/json");
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
                            var resultModel = serializer.Deserialize<ApiResponse<TotalCostDTO>>(reader);
                            return resultModel;
                        }
                    }
                }

            }

        }

        public async Task<ApiResponse<TotalCostDTO>> GetSupplierTotalCostNP(FormConfigurationDTO DTOModel)
        {

            HttpClientHandler handler = CertificaetHandler();
            using (var client = new HttpClient(handler))
            {
                var request = new HttpRequestMessage
                {
                    RequestUri = new Uri( /*configuration.GetSection("API:QuantityTemplates").Value*/ _rootConfiguration.APIConfiguration.QuantityTemplates + $"/api/QuantityTable/Supplier/GetSupplierTotalCostNP"),
                    Method = HttpMethod.Post,
                };
                var x = JsonConvert.SerializeObject(new { DTOModel });
                x = x.Replace("{\"DTOModel\":", "");
                x = x.Replace("}}", "}");
                HttpContent content = new StringContent(x, Encoding.UTF8, "application/json");
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
                            var resultModel = serializer.Deserialize<ApiResponse<TotalCostDTO>>(reader);
                            return resultModel;
                        }
                    }
                }
            }

        }

        public async Task<ApiResponse<List<TableTemplateDTO>>> NegotiationValidateQuantityItem(FormConfigurationDTO DTOModel)
        {


            HttpClientHandler handler = CertificaetHandler();
            using (var client = new HttpClient(handler))
            {
                var request = new HttpRequestMessage
                {
                    RequestUri = new Uri( /*configuration.GetSection("API:QuantityTemplates").Value*/ _rootConfiguration.APIConfiguration.QuantityTemplates + $"/api/QuantityTable/Negotiation/ValidateQuantityItem"),
                    Method = HttpMethod.Post,
                };
                var x = JsonConvert.SerializeObject(new { DTOModel });
                x = x.Replace("{\"DTOModel\":", "");
                x = x.Replace("}}", "}");
                HttpContent content = new StringContent(x, Encoding.UTF8, "application/json");
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
                            var resultModel = serializer.Deserialize<ApiResponse<List<TableTemplateDTO>>>(reader);
                            return resultModel;
                        }
                    }
                }
            }

        }

        public async Task<QuantityTableModelForExcel> GenerateQuantityTableTemplateExcel(int stageId, int formId, int yearsCount)
        {

            HttpClientHandler handler = CertificaetHandler();
            using (var client = new HttpClient(handler))
            {
                var request = new HttpRequestMessage
                {
                    RequestUri = new Uri( /*configuration.GetSection("API:QuantityTemplates").Value*/ _rootConfiguration.APIConfiguration.QuantityTemplates + "/api/QuantityTable/" + "GetFormsForExcel/" + stageId + "/" + formId + "/" + yearsCount),
                    Method = HttpMethod.Get,
                };
                var DTOModnewel = new FormConfigurationDTO();
                var x = JsonConvert.SerializeObject(new { DTOModnewel });
                x = x.Replace("{\"DTOModel\":", "");
                x = x.Replace("}}", "}");
                HttpContent content = new StringContent(x, Encoding.UTF8, "application/json");
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
                            var resultModel = serializer.Deserialize<QuantityTableModelForExcel>(reader);
                            return resultModel;
                        }
                    }
                }
            }

        }

        public async Task<ExcelHeader> GenerateQuantityTableTemplateExcelHeader(int stageId, int formId, int yearsCount)
        {

            HttpClientHandler handler = CertificaetHandler();
            using (var client = new HttpClient(handler))
            {
                var request = new HttpRequestMessage
                {
                    RequestUri = new Uri( /*configuration.GetSection("API:QuantityTemplates").Value*/ _rootConfiguration.APIConfiguration.QuantityTemplates + "/api/QuantityTable/" + "GetExcelHeaderCellsForExport/" + stageId + "/" + formId + "/" + yearsCount),
                    Method = HttpMethod.Get,
                };
                var DTOModnewel = new FormConfigurationDTO();
                var x = JsonConvert.SerializeObject(new { DTOModnewel });
                x = x.Replace("{\"DTOModel\":", "");
                x = x.Replace("}}", "}");
                HttpContent content = new StringContent(x, Encoding.UTF8, "application/json");
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
                            var resultModel = serializer.Deserialize<ExcelHeader>(reader);
                            return resultModel;
                        }
                    }
                }
            }

        }


        #endregion

        #region
        public async Task<ApiResponse<List<EmarketPlaceResponse>>> GetEmarketPlace(List<EmarketPlaceRequest> model)
        {
            HttpClientHandler handler = CertificaetHandler();
            using (var client = new HttpClient(handler))
            {

                var request = new HttpRequestMessage
                {
                    RequestUri = new Uri(_rootConfiguration.APIConfiguration.QuantityTemplates + $"/api/QuantityTable/GOV/GetEmarketPlaceColumns"),
                    Method = HttpMethod.Post,
                };
                var xx = JsonConvert.SerializeObject(new { model });
                xx = xx.Replace("{\"model\":", "");
                xx = xx.Replace("}}", "}");
                HttpContent content = new StringContent(xx, Encoding.UTF8, "application/json");
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
                            var resultModel = serializer.Deserialize<ApiResponse<List<EmarketPlaceResponse>>>(reader);
                            return resultModel;
                        }
                    }
                }

            }

        }

        public async Task<List<int>> GetCanIgnoreMandatoryValidationColumnsId()
        {
            HttpClientHandler handler = CertificaetHandler();
            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(_rootConfiguration.APIConfiguration.QuantityTemplates);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync($"api/QuantityTable/GetCanIgnoreMandatoryValidationColumnsIds");

                using (var stream = await response.Content.ReadAsStreamAsync())
                {
                    using (var streamReader = new StreamReader(stream))
                    {
                        using (var reader = new JsonTextReader(streamReader))
                        {
                            var serializer = new JsonSerializer();
                            var resultModel = serializer.Deserialize<List<int>>(reader);
                            return resultModel;
                        }
                    }
                }
            }
        }

        #endregion



        public async Task<List<int>> GetFormIdsWithTemplateIdAndQtVersionId(int templateId, int qtVersionId)
        {

            HttpClientHandler handler = CertificaetHandler();
            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri( /*configuration.GetSection("API:QuantityTemplates").Value*/ _rootConfiguration.APIConfiguration.QuantityTemplates);
                client.DefaultRequestHeaders.Accept.Clear();
                client.Timeout = new TimeSpan(0, 30, 0);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync($"api/QuantityTable/GetFormsIdByTemplateId/" + templateId + "/" + qtVersionId);
                using (var stream = await response.Content.ReadAsStreamAsync())
                {
                    using (var streamReader = new StreamReader(stream))
                    {
                        using (var reader = new JsonTextReader(streamReader))
                        {
                            var serializer = new JsonSerializer();
                            var resultModel = serializer.Deserialize<List<int>>(reader);
                            return resultModel;
                        }
                    }
                }
            }

        }

        public async Task<List<ColumnDto>> GetCostColumnsIdByFormIdForNotSupply(List<long> formIds)
        {
            HttpClientHandler handler = CertificaetHandler();
            using (var client = new HttpClient(handler))
            {
                {
                    var request = new HttpRequestMessage
                    {
                        RequestUri = new Uri(_rootConfiguration.APIConfiguration.QuantityTemplates + $"/api/QuantityTable/GetCostColumnsIdByFormIdForNotTawreed"),
                        Method = HttpMethod.Post,
                    };
                    var x = JsonConvert.SerializeObject(new { formIds });
                    x = x.Replace("{\"formIds\":", "");
                    x = x.Replace("}", "");
                    HttpContent content = new StringContent(x, Encoding.UTF8, "application/json");
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
                                var resultModel = serializer.Deserialize<List<ColumnDto>>(reader);
                                return resultModel;
                            }
                        }
                    }
                }
            }
        }
    }
}