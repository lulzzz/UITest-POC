using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MOF.Etimad.Monafasat.Integration;
using MOF.Etimad.Monafasat.Integration.Infrastructure;
using MOF.Etimad.Monafasat.PaymentCallbackAPI.Enums;
using MOF.Etimad.Monafasat.PaymentCallbackAPI.Model;
using MOF.Etimad.Monafasat.Services;
using MOF.Etimad.Monafasat.SharedKernel.Exceptions;
using System;
using System.IO;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.PaymentCallbackAPI.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class PaymentNotificationController : ControllerBase
    {
        private readonly IBillAppService _billsInqueryAppService;
        private readonly ILogger<PaymentNotificationController> _logger;
        public PaymentNotificationController(IBillAppService billsInqueryAppService, ILogger<PaymentNotificationController> logger)
        {
            _billsInqueryAppService = billsInqueryAppService;
            _logger = logger;
        }

        [HttpPost("SetBillPaidPaymentNotification")]
        public async Task<PaymentNotificationResponseModel> SetBillPaidPaymentNotification([FromBody] PaymentNotificationModel paymentNotificationModelList)
        {
            try
            {
                if (paymentNotificationModelList == null) { throw new Exception("model is null"); }
                PaymentNotificationServiceModel paymentNotification = new PaymentNotificationServiceModel(paymentNotificationModelList.AgencyCode, paymentNotificationModelList.IntermediatePaymentId, paymentNotificationModelList.BillCategory, paymentNotificationModelList.BillAccount, paymentNotificationModelList.PaymentAmount, paymentNotificationModelList.PaymentDate, paymentNotificationModelList.PaymentMethod, paymentNotificationModelList.PaymentStatusCode, paymentNotificationModelList?.PaymentReferenceInfo, paymentNotificationModelList.PaymentMethodDetail.SADADPaymentTransactionId, paymentNotificationModelList.PaymentMethodDetail.BankId, paymentNotificationModelList.PaymentMethodDetail.BankReversalTransactionID, paymentNotificationModelList.PaymentMethodDetail.BankPaymentId, paymentNotificationModelList.PaymentMethodDetail.PaymentChannel, paymentNotificationModelList?.PayorPOI?.POINumber, paymentNotificationModelList?.PayorPOI?.POIType);
                var updatedBillNumbersList = await _billsInqueryAppService.SetBillPaid(paymentNotification);
                return new PaymentNotificationResponseModel() { StatusCode = ServiceStatusCodes.Success, StatusDesc = "Success", UpdatedPaymentNotificationList = updatedBillNumbersList };
            }
            catch (EntityNotFoundException ex)
            {
                Logger.LogToFile(ex.Message.ToString(), "EntityNotFoundException");
                _logger.LogError(ex.ToString(), ex);
                return new PaymentNotificationResponseModel() { StatusCode = ServiceStatusCodes.NoDataFound, StatusDesc = ex.Message };
            }
            catch (NullReferenceException ex)
            {
                Logger.LogToFile(ex.Message.ToString(), "NullReferenceException");
                _logger.LogError(ex.ToString(), ex);
                return new PaymentNotificationResponseModel() { StatusCode = ServiceStatusCodes.NoDataFound, StatusDesc = ex.Message };
            }
            catch (InvalidDataException ex)
            {
                Logger.LogToFile(ex.Message.ToString(), "InvalidDataException");
                _logger.LogError(ex.ToString(), ex);
                return new PaymentNotificationResponseModel() { StatusCode = ServiceStatusCodes.WrongTypeCode, StatusDesc = ex.Message };
            }
            catch (Exception ex)
            {
                Logger.LogToFile(ex.Message.ToString(), "Exception");
                _logger.LogError(ex.ToString(), ex);
                return new PaymentNotificationResponseModel() { StatusCode = ServiceStatusCodes.UnrecoverableDatabaseError, StatusDesc = ex.Message };
            }
        }
    }
}