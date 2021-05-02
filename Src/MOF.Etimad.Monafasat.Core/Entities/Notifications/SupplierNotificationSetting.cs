using System;
using System.Collections.Generic;
using System.Text;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    public class SupplierNotificationSetting : BaseNotificationSetting
    {
        public SupplierNotificationSetting()
        {

        
        }

        public SupplierNotificationSetting(bool load)
        {

            OfferStatus = new NotificationBy() { Email = true, Mobile = true, }; ;
            InquiriesAboutTender = new NotificationBy() { Email = true, Mobile = true, }; ; 
            OperationsOnYourAccount = new NotificationBy() { Email = true, Mobile = true, }; ;
            OperationsOnTheTender = new NotificationBy() { Email = true, Mobile = true, }; ;
            PurchaseInvoiceNumber = new NotificationBy() { Email = true, Mobile = true, }; ;
            TenderRelatedToYourActivity = new NotificationBy() { Email = true, Mobile = true, }; ;
            ReceiveTheAmountOfTheBooklet = new NotificationBy() { Email = true, Mobile = true, }; ;
            EntityCreated();
        }
        public SupplierNotificationSetting(NotificationBy offerStatus, NotificationBy inquiriesAboutTender, NotificationBy operationsOnYourAccount, NotificationBy purchaseInvoiceNumber,
             NotificationBy tenderRelatedToYourActivity, NotificationBy receiveTheAmountOfTheBooklet, NotificationBy operationsOnTheTender)

        {
            OfferStatus = OfferStatus;
            InquiriesAboutTender = inquiriesAboutTender;
            OperationsOnYourAccount = operationsOnYourAccount;
            OperationsOnTheTender = operationsOnTheTender;
            PurchaseInvoiceNumber = purchaseInvoiceNumber;
          
            TenderRelatedToYourActivity = tenderRelatedToYourActivity;
            ReceiveTheAmountOfTheBooklet = receiveTheAmountOfTheBooklet;
            EntityCreated();
        }
        public NotificationBy OfferStatus { get; private set; }
        public NotificationBy InquiriesAboutTender { get; private set; }
        public NotificationBy OperationsOnYourAccount { get; private set; }
        public NotificationBy OperationsOnTheTender { get; private set; }
        public NotificationBy PurchaseInvoiceNumber { get; private set; }
     
        public NotificationBy TenderRelatedToYourActivity { get; private set; }
        public NotificationBy ReceiveTheAmountOfTheBooklet { get; private set; }


        public void  UpdateOfferStatus(bool email, bool mobile) {
            OfferStatus.Email= email;
            OfferStatus.Mobile = mobile;
            EntityUpdated();
        }
        public void  UpdateInquiriesAboutTender(bool email,bool mobile) {
            InquiriesAboutTender.Email = email;
            InquiriesAboutTender.Mobile = mobile;
            EntityUpdated();
        }
        public void  UpdateOperationsOnYourAccount(bool email,bool mobile) {
            OperationsOnYourAccount.Email= email;
            OperationsOnYourAccount.Mobile = mobile;
            EntityUpdated();
        }
        public void  UpdateOperationsOnTheTender(bool email,bool mobile) {
            OperationsOnTheTender.Email= email;
            OperationsOnTheTender.Mobile = mobile;
            EntityUpdated();
        }
        public void  UpdatePurchaseInvoiceNumber(bool email,bool mobile) {
            PurchaseInvoiceNumber.Email= email;
            PurchaseInvoiceNumber.Mobile = mobile;
            EntityUpdated();
        }
      
        public void  UpdateTenderRelatedToYourActivity(bool email,bool mobile) {
            TenderRelatedToYourActivity.Email= email;
            TenderRelatedToYourActivity.Mobile = mobile;
            EntityUpdated();
        }
        public void  UpdateReceiveTheAmountOfTheBooklet(bool email,bool mobile) {
            ReceiveTheAmountOfTheBooklet.Email= email;
            ReceiveTheAmountOfTheBooklet.Mobile = mobile;
            EntityUpdated();
        }
        
    }
}
