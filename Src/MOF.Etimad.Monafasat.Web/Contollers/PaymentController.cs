//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Logging;
//using Microsoft.Extensions.Options;
//using MOF.Etimad.Monafasat.SharedKernal;
//using MOF.Etimad.Monafasat.SharedKernal.Exceptions;
//using MOF.Etimad.Monafasat.SharedKernel;
//using MOF.Etimad.Monafasat.ViewModel;
//using MOF.Etimad.Monafasat.Web.Base;
//using MOF.Etimad.Monafasat.Web.Infrastructure;
//using System;
//using System.Threading.Tasks;

//namespace MOF.Etimad.Monafasat.Web.Contollers
//{
//   // [Authorize(RoleNames.MonafasatAdminPolicy)]
//   public class PaymentController : BaseController
//   {
//      private IApiClient _ApiClient;
//      private readonly ILogger<PaymentController> _logger;
//      public PaymentController(IApiClient ApiClient, ILogger<PaymentController> logger, IOptionsSnapshot<RootConfiguration> rootConfiguration) : base(rootConfiguration)
//      {
//         _ApiClient = ApiClient;
//         _logger = logger;
//      }
//      public ActionResult PaidBill()
//      {
//         return View(new BillViewModel());
//      }
//      [HttpPost]
//      public async Task<ActionResult> PaidBill(BillViewModel billModel)
//      {
//         if (string.IsNullOrEmpty(billModel.BillInvoiceNumber))
//         {
//            return View(billModel);
//         }
//         try
//         {
//            // var result= await _ApiClient.PostAsync("Payment/PaidBill", null, billModel);
//            var result = await _ApiClient.PostAsync<bool>("Payment/PaidBill", null, billModel);
//            var message = "";
//            if (result)
//            {
//               message = "تم تسديد الفاتورة بنجاح";
//            }
//            else
//            {
//               message = "حدث خطأ أثناء عملية السداد";
//            }
//            AddMessage(message);
//            return View(billModel);
//         }
//         catch (AuthorizationException ex)
//         {
//            throw ex;
//         }
//         catch (BusinessRuleException ex)
//         {
//            AddError(ex.Message);
//            return View(billModel);
//         }
//         catch (Exception ex)
//         {
//            _logger.LogError(ex.ToString(), ex);
//            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
//            return View(billModel);
//         }
//      }
//   }
//}
