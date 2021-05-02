using MOF.Etimad.Monafasat.Core.Interfaces;
using MOF.Etimad.Monafasat.Integration;
using MOF.Etimad.Monafasat.SharedKernel;
using System;
using System.Threading.Tasks;
using static MOF.Etimad.Monafasat.SharedKernel.Enums;
namespace MOF.Etimad.Monafasat.Services
{
    public class BillJobAppService : IBillJobAppService
    {
        private readonly IGenericCommandRepository _genericCommandRepository;
        private readonly IBillJobQueries _billQueries;
        private readonly IBillingProxy _billProxy;
        public BillJobAppService(IBillingProxy billingProxy, IGenericCommandRepository genericCommandRepository, IBillJobQueries billQueries)
        {
            _billProxy = billingProxy;
            _genericCommandRepository = genericCommandRepository;
            _billQueries = billQueries;
        }
        public async Task<bool> UpdateBillThroughTahseel()
        {
            var Bills = await _billQueries.GetAllBillInfos();
            DateTime expirydate = new DateTime();
            if (Bills.Count > 0)
            {
                foreach (var bill in Bills)
                {
                    var billToUpdte = await _billQueries.FindBillByInvoiceNumberWithoutIncludes(bill.BillInvoiceNumber);
                    if (bill.ActionStatus == (int)Enums.BillActionStatus.UpdateBill)
                    {
                        if (bill.ConditionsBookletID != null)
                        {
                            expirydate = bill.ConditionsBooklet.Tender.LastOfferPresentationDate.Value;

                        }
                        else
                        {
                            expirydate = bill.Invitation.Tender.LastOfferPresentationDate.Value;
                        }
                        bill.UpdateExpiryDateWithoutChangeState(expirydate);
                    }

                    var result = await _billProxy.UpdateBillActionStatus(bill);
                    if (result)
                    {
                        if (bill.ActionStatus == (int)Enums.BillActionStatus.UpdateBill)
                        {
                            if (bill.ConditionsBookletID != null)
                            {
                                expirydate = bill.ConditionsBooklet.Tender.LastOfferPresentationDate.Value;

                            }
                            else
                            {
                                expirydate = bill.Invitation.Tender.LastOfferPresentationDate.Value;
                            }
                            billToUpdte.UpdateActionStatusAndExpiryDate(BillActionStatus.SuccessUpdateBill, expirydate);
                        }
                        else
                        {
                            billToUpdte.UpdateActionStatus(BillActionStatus.SuccessCancelBill);
                        }
                        _genericCommandRepository.Update(billToUpdte);
                    }
                }
                await _genericCommandRepository.SaveAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
