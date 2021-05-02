using Microsoft.AspNetCore.Mvc;
using MOF.Etimad.Monafasat.Web.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.ViewComponents.Notification
{
   [ViewComponent(Name = "NavNotification")]
   public class NavNotificationComponent : ViewComponent
   {
      private IApiClient _ApiClient;
      public NavNotificationComponent(IApiClient ApiClient)
      {
         _ApiClient = ApiClient;
      }

      public async Task<IViewComponentResult> InvokeAsync()
      {
         List<NotificationPanelModel> notifications = await _ApiClient.GetListAsync<NotificationPanelModel>("Account/GetNotificationPanel", null);
         //notifications.Take(10)
         return View(notifications);
      }
   }
}
