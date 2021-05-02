using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MOF.Etimad.Monafasat.Core;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.Web.Infrastructure;
using MOF.Etimad.Monafasat.Web.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.EntitiesProxy
{
    public class CommitteeProxy
   {
      private static ApiRequest<CommitteeModel> _connect;
      private static ApiRequest<CommitteeUserModel> _userCommittee;
      private static ApiRequest<CommitteeTenderModel> _tenderCommittee;
      public static void Initialize(ControllerContext currentContext)
      {
         _connect = new ApiRequest<CommitteeModel>(currentContext);
         _userCommittee = new ApiRequest<CommitteeUserModel>(currentContext);
         _tenderCommittee = new ApiRequest<CommitteeTenderModel>(currentContext);
      }
      public static async Task<QueryResult<CommitteeModel>> GetAsync(CommitteeSearchCriteriaModel criteria)
      {
         var committees = JsonConvert.DeserializeObject<QueryResult<CommitteeModel>>(await _connect.GetAsync("Committee/Get?" + UrlHelper.ToQueryString(criteria)));
         return committees;
      }
      // Get By Id
      public static async Task<CommitteeModel> GetByIdAsync(int Id)
      {
         var committee = JsonConvert.DeserializeObject<CommitteeModel>(await _connect.GetAsync("Committee/GetById?id=" + Id));
         return committee;
      }
      
      //internal static Task GetAsync(CommitteeSearchCriteria criteria)
      //{
      //   throw new NotImplementedException();
      //}

      // Create New Committee
      public static async Task CreateAsync(CommitteeModel committee)
      {
         await _connect.PostAsync("Committee", committee);
      }
      //// Update Committee
      public static async Task  UpdateAsync(CommitteeModel committee)
      {
         await _connect.PostAsync("Committee/Update/", committee);
      }

      //// Delete Committee
      public static async Task DeleteAsync(int id)
      {
         await _connect.GetAsync("Committee/Delete/" + id);
      }
      public static async Task AssignUsersCommitteeAsync(CommitteeUserModel committeeUserModel)
      {
         await _userCommittee.PostAsync("Committee/AssignUsersCommittee/", committeeUserModel);
      }
      public static async Task<CommitteeTenderModel> GetCommitteeTendersAsync(int committeeId)
      {
         return JsonConvert.DeserializeObject<CommitteeTenderModel>(await _tenderCommittee.GetAsync("Committee/GetCommitteeTendersAsync/" + committeeId));
      }
      public static async Task JoinCommitteeAllTendersAsync(CommitteeTenderModel committeeTenderModel)
      {
         await _tenderCommittee.PostAsync("Tender/JoinCommitteeAllTendersAsync/", committeeTenderModel);
      }
      public static async Task JoinCommitteeToTenderAsync(CommitteeTenderModel committeeTenderModel)
      {
         await _tenderCommittee.PostAsync("Tender/JoinCommitteeToTenderAsync/", committeeTenderModel);
      }
      public static async Task JoinCommitteeTenderAsync(CommitteeTenderModel committeeTenderModel)
      {
         await _tenderCommittee.PostAsync("Tender/JoinCommitteeTenderAsync/", committeeTenderModel);
      }
      public static async Task<QueryResult<LookupModel>> GetCommitteeUsersAsync(CommitteeUserSearchCriteriaModel criteria)
      {
         var agenciesList = JsonConvert.DeserializeObject<QueryResult<LookupModel>>(await _connect.GetAsync($"Committee/GetCommitteeUsers?" + UrlHelper.ToQueryString(criteria)));
         return agenciesList;
      }
      public static async Task RemoveAssignedUserAsync(int userId)
      {
         await _userCommittee.PostAsync($"Committee/RemoveAssignedUser?userId={userId}", null);
      }
   }
}
