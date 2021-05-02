using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.Web.Infrastructure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.EntitiesProxy
{
   public class LookupProxy
   {
      private static ApiRequest<AreaModel> apiRequest;
      private static ApiRequest<UserLookupModel> userRequest;
      public static void Initialize(ControllerContext currentContext)
      {
         apiRequest = new ApiRequest<AreaModel>(currentContext);
         userRequest = new ApiRequest<UserLookupModel>(currentContext);
      }

      public static async Task<List<AreaModel>> GetAreasAsync()
      {
         var areasList = JsonConvert.DeserializeObject<List<AreaModel>>(await apiRequest.GetAsync("Lookup/GetAreas"));
         return areasList;
      }

      public static async Task<List<CountryModel>> GetCountriesAsync()
      {
         var countriesList = JsonConvert.DeserializeObject<List<CountryModel>>(await apiRequest.GetAsync("Lookup/GetCountries"));
         return countriesList;
      }

      public static async Task<List<ActivityModel>> GetActivitiesAsync()
      {
         var activitiesList = JsonConvert.DeserializeObject<List<ActivityModel>>(await apiRequest.GetAsync("Lookup/GetActivities"));
         return activitiesList;
      }

      public static async Task<List<ConstructionWorkModel>> GetConstructionWorksAsync()
      {
         var constructionWorkList = JsonConvert.DeserializeObject<List<ConstructionWorkModel>>(await apiRequest.GetAsync("Lookup/GetConstructionWorks"));
         return constructionWorkList;
      }

      public static async Task<List<MaintenanceRunningWorkModel>> GetMaintenanceRunningWorksAsync()
      {
         var maintenanceRunningWorkList = JsonConvert.DeserializeObject<List<MaintenanceRunningWorkModel>>(await apiRequest.GetAsync("Lookup/GetMaintenanceRunningWorks"));
         return maintenanceRunningWorkList;
      }

      public static async Task<List<CommitteeModel>> GetCommitteesByCommitteeTypeIdAsync(int committeeTypeId, int agencyId)
      {
         //Deserializing the response recieved from web api and storing into the Tender List  
         var committeesList = JsonConvert.DeserializeObject<List<CommitteeModel>>(await apiRequest.GetAsync("Committee/GetByCommitteeType?committeeTypeId=" + committeeTypeId + "&agencyId=" + agencyId));
         return committeesList;
      }

      public static async Task<List<BankModel>> GetBanksAsync()
      {
         var banksList = JsonConvert.DeserializeObject<List<BankModel>>(await apiRequest.GetAsync("Lookup/GetBanks"));
         return banksList;
      }
      public static async Task<List<LookupModel>> GetAgenciesAsync()
      {
         var agenciesList = JsonConvert.DeserializeObject<List<LookupModel>>(await apiRequest.GetAsync("Lookup/GetALlAgencies"));
         return agenciesList;
      }

      public static async Task<List<UserLookupModel>> GetUsersByRoleNameAsync(string roleName)
      {
         var agenciesList = JsonConvert.DeserializeObject<List<UserLookupModel>>(await userRequest.GetAsync("Lookup/GetUsersByRoleName/" + roleName));
         return agenciesList;
      }

   }
}
