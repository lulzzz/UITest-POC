using MOF.Etimad.Monafasat.SharedKernal;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.Infrastructure
{
   public interface IApiClient
   {
      ApiHttpClient SetTimeout(int valueInSeconds);
      Task<string> GetStringAsync(string path, Dictionary<string, object> parameters);
      Task<T> GetAsync<T>(string segments, Dictionary<string, object> parameters);
      Task<List<T>> GetListAsync<T>(string segments, Dictionary<string, object> parameters);
      Task<QueryResult<T>> GetQueryResultAsync<T>(string segments, Dictionary<string, object> parameters) where T : class;
      Task<T> PostAsync<T>(string segments, Dictionary<string, object> parameters, object model);
      Task<string> PostAsync(string segments, Dictionary<string, object> parameters, object model);
      Task<T> PutAsync<T>(string segments, Dictionary<string, object> parameters, object model);
      Task<T> DeleteAsync<T>(string segments, Dictionary<string, object> parameters, object model);
      Task<T> PostAsync<T>(string segments, Dictionary<string, object> parameters, object model, string accessToken);

   }
}
