using Microsoft.AspNetCore.Http;
using MOF.Etimad.Monafasat.Web.Infrastructure;
using MOF.Etimad.Monafasat.Web.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.EntitiesProxy
{
    public class InvitationStatusProxy
   {
      public static HttpContext CurrentContext { get; set; }
      private static ApiRequest<InvitationStatusModel> _connect = new ApiRequest<InvitationStatusModel>(CurrentContext);
      public static async Task<List<InvitationStatusModel>> GetAsync()
      {
         var tenders = JsonConvert.DeserializeObject<List<InvitationStatusModel>>(await _connect.GetAsync("InvitationStatus/Get"));
         return tenders;
      }
      // Get By Id
      public static async Task<InvitationStatusModel> GetBYIdAsync(int Id)
      {
         var tender = JsonConvert.DeserializeObject<InvitationStatusModel>(await _connect.GetAsync("InvitationStatus/GetById/" + Id));
         return tender;
      }
      // Update Invitation Status
      public static async Task<InvitationStatusModel> UpdateStatus(int id,int statusId)
      {
         var tender = JsonConvert.DeserializeObject<InvitationStatusModel>(await _connect.GetAsync("InvitationStatus/UpdateStatus/" + id + "/" + statusId));
         return tender;
      }
      // Create New InvitationStatus
      public static async Task CreateAsync(InvitationStatusModel invitationStatus)
      {
         await _connect.PostAsync("InvitationStatus/Create", invitationStatus);
      }
      //// Update InvitationStatus
      public static async Task  UpdateAsync(int id, InvitationStatusModel invitationStatus)
      {
         await _connect.PostAsync("InvitationStatus/Update/" + id, invitationStatus);
      }

      //// Delete InvitationStatus
      public static async Task DeleteAsync(int id)
      {
         await _connect.GetAsync("InvitationStatus/Delete/" + id);
      }
   }
}
