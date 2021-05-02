using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.APIs
{
   public class ApiCalling
   {

      //public string ApiEndPoint = "";
      public string ApiEndPoint { get; set; }

      public ApiCalling(string MethodName)
      {
         ApiEndPoint = "http://localhost:51629/api/" + MethodName;

      }

      public async Task<string> GetAsync()
      {

         HttpClient client = new HttpClient();
         client.BaseAddress = new Uri(ApiEndPoint); //Passing service base url 

         //Define request data format  
         client.DefaultRequestHeaders.Accept.Clear();
         client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

         //Sending request to find web api REST service resource using HttpClient
         HttpResponseMessage responseMessage = await client.GetAsync(ApiEndPoint);

         //Checking the response is successful or not which is sent using HttpClient  
         if (responseMessage.IsSuccessStatusCode)
         {
            //Storing the response details recieved from web api 
            var responseData = await responseMessage.Content.ReadAsStringAsync();

            //Deserializing the response recieved from web api and storing into the Competition list        
            //CompetitionList = JsonConvert.DeserializeObject<List<Competition>>(EmpResponse);

            return responseData;
         }

         return "Resource Not Found";
      }
   }
}
