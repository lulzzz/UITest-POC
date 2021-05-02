using Microsoft.AspNetCore.Mvc;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.Web.Infrastructure;
using System;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.ViewComponents.Tender
{
   [ViewComponent(Name = "TenderDates")]
   public class TenderDatesViewComponent : ViewComponent
   {
      private IApiClient _ApiClient;
      public TenderDatesViewComponent(IApiClient ApiClient)
      {
         _ApiClient = ApiClient;
      }

      public async Task<IViewComponentResult> InvokeAsync(string tenderIdString)
      {
         TenderDatesModel tenderModel = await _ApiClient.GetAsync<TenderDatesModel>("Tender/GetTenderDatesDetailsById/" + Util.Decrypt(tenderIdString), null);
         if (tenderModel != null && tenderModel.OffersOpeningDate.HasValue)
            tenderModel.OffersOpeningDate = new DateTime(tenderModel.OffersOpeningDate.Value.Year, tenderModel.OffersOpeningDate.Value.Month, tenderModel.OffersOpeningDate.Value.Day);

         if ((tenderModel.ChangeRequestTypeId == (int)Enums.ChangeRequestType.ExtendDates && tenderModel.ChangeRequestStatusId == (int)Enums.ChangeStatusType.Pending && (User.IsInRole(RoleNames.Auditer) || User.IsInRole(RoleNames.DataEntry) || User.IsInRole(RoleNames.PurshaseSpecialist) || User.IsInRole(RoleNames.EtimadOfficer)))
         || (tenderModel.ChangeRequestTypeId == (int)Enums.ChangeRequestType.ExtendDates && tenderModel.ChangeRequestStatusId == (int)Enums.ChangeStatusType.New) && (User.IsInRole(RoleNames.DataEntry) || User.IsInRole(RoleNames.PurshaseSpecialist)))
         {
            var tenderRevisionDate = await _ApiClient.GetAsync<TenderRevisionDateModel>("Tender/GetRevisionDateByTenderId/" + Util.Decrypt(tenderIdString), null);
            if (tenderRevisionDate != null)
            {
               tenderModel.TenderRevisionDate = tenderRevisionDate;
            }
         }
         if (tenderModel.ChangeRequestTypeId == (int)Enums.ChangeRequestType.ExtendDates && tenderModel.ChangeRequestStatusId == (int)Enums.ChangeStatusType.Rejected && (User.IsInRole(RoleNames.DataEntry) || User.IsInRole(RoleNames.PurshaseSpecialist)))
         {
            // get revision date for review the reject
            var tenderRevisionDate = await _ApiClient.GetAsync<TenderRevisionDateModel>("Tender/GetRejectedRevisionDateByTenderId/" + Util.Decrypt(tenderIdString), null);
            if (tenderRevisionDate != null)
            {
               tenderModel.TenderRevisionDate = tenderRevisionDate;
            }
         }

         //TempData["isTenderDatesChanged"] = tenderModel.TenderRevisionDate != null;
         return View(tenderModel);
      }
   }
}
