using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.ViewModel.Offer;
using MOF.Etimad.Monafasat.Web.Infrastructure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.EntitiesProxy
{
   public class NotifayProxy
   {
      private static ApiRequest<UserProfileModel> apiRequest;
      private static ApiRequest<NotificationSettingModel> apiRequestSetting;


      public static void Initialize(ControllerContext currentContext)
      {
         apiRequest = new ApiRequest<UserProfileModel>(currentContext);
         apiRequestSetting = new ApiRequest<NotificationSettingModel>(currentContext);

      }

      public static async Task<UserProfileModel> GetMyProfile()
      {
         CheckInstance();
         var tenderList = JsonConvert.DeserializeObject<UserProfileModel>(await apiRequest.GetAsync("Account/GetMyProfile"));
         return tenderList;
      }

      public static async Task<bool> CreateMyProfile()
      {
         CheckInstance();
         var result = await apiRequest.PostAsync("Account/CreateMyProfile", null);
         return true;
      }

      public static async Task<bool> SaveMyProfile(UserProfileModel model)
      {
         CheckInstance();
         var result = await apiRequest.PostAsync("Account/SaveMyProfile", model);
         return true;
      }
      public static async Task<List<NotificationPanelModel>> GetNotifications()
      {
         CheckInstance();


         var result = JsonConvert.DeserializeObject<List<NotificationPanelModel>>(await apiRequest.GetAsync("Account/GetNotificationPanel"));
         return result;
      }
      public static async Task<int> GetNotificationsCount()
      {
         CheckInstance();
         var result = JsonConvert.DeserializeObject<int>(await apiRequest.GetAsync("Account/GetNotificationPanelCount"));
         return result;
      }
      public static async Task SetReadStateToNotification(int notificationId)
      {
         var result = await apiRequest.PostModelAsync("Account/SetReadStateToNotification", notificationId);

      }


      private static void CheckInstance() {
         if (apiRequest == null)
         {
            apiRequest = new ApiRequest<UserProfileModel>(new ControllerContext());
            apiRequestSetting = new ApiRequest<NotificationSettingModel>(new ControllerContext());
         }
      }

   }
}
