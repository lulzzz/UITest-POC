using Microsoft.AspNetCore.Mvc;
using MOF.Etimad.Monafasat.Web.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.ViewComponents.Notification
{
   [ViewComponent(Name = "Notification")]
   public class NotificationComponent : ViewComponent
   {
      private IApiClient _ApiClient;
      public NotificationComponent(IApiClient ApiClient)
      {
         _ApiClient = ApiClient;
      }
      public async Task<IViewComponentResult> InvokeAsync()
      {
         List<NotificationPanelModel> notifications = await _ApiClient.GetListAsync<NotificationPanelModel>("Account/GetNotificationPanel", null);
         return View(notifications);
      }
   }
}
