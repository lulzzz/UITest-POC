using System.Collections.Generic;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using System.Text;
using MOF.Etimad.Monafasat.Core;
using System;

namespace MOF.Etimad.Monafasat.TestsBuilders.TenderDefaults.ChangeRequestDefault
{
    public class ChangeRequestDefault
    {
        const int TENDER_ID = 1;
        const string VERIFICATION_CODE = "123";

        public TenderChangeRequest GetCancelChangeRequestData()
        {
            var changeRequest = new TenderChangeRequest(1, (int)Enums.ChangeRequestType.Canceling, (int)Enums.ChangeStatusType.New, RoleNames.DataEntry, null);

            return changeRequest;
        }
              public TenderChangeRequest GetCancelChangeRequestDataVRO()
        {
            var changeRequest = new TenderChangeRequest(1, (int)Enums.ChangeRequestType.Canceling, (int)Enums.ChangeStatusType.New, RoleNames.PurshaseSpecialist, null);

            return changeRequest;
        }

        public List<TenderChangeRequest> GetCancelChangeRequestDataList()
        {
            var changeRequest = new TenderChangeRequest(1, (int)Enums.ChangeRequestType.Canceling, (int)Enums.ChangeStatusType.New, "1", null);

            return new List<TenderChangeRequest>() { changeRequest };
        }

        public TenderChangeRequest GetExtendDatedChangeRequestDataList()
        {
            var changeRequest = new TenderChangeRequest(1, (int)Enums.ChangeRequestType.ExtendDates, (int)Enums.ChangeStatusType.Pending, "1", null);

            return changeRequest;
        }

        public TenderChangeRequest GetQTChangeRequestDataList()
        {
            var changeRequest = new TenderChangeRequest(1, (int)Enums.ChangeRequestType.QuantitiesTables, (int)Enums.ChangeStatusType.Pending, "1", null);
            changeRequest.SetActive();
            changeRequest.SetTender(new TenderDefault().GetGeneralTender());
            return changeRequest;
        }

        public TenderChangeRequest GetCancelChangeRequestWithTender()
        {
            var changeRequest = new TenderChangeRequest(1, (int)Enums.ChangeRequestType.Canceling, (int)Enums.ChangeStatusType.New, RoleNames.DataEntry, null);
            var tender = new TenderDefault().GetGeneralTender();
            changeRequest.SetTender(tender);
            return changeRequest;
        }
        public TenderRevisionCancelModel GetTenderRevisionCancelModel()
        {
            var tenderCancelModel = new TenderRevisionCancelModel
            {
                TenderIdString = Util.Encrypt(TENDER_ID),
                CancelationReasonDescription = "CancelationReasonDescription",
                CreatedByRoleName = RoleNames.DataEntry,
                CancelationReasonId = 1
            };
            return tenderCancelModel;
        }

        public TenderCancelModel GetTenderCancelModel()
        {
            var tenderCancelModel = new TenderCancelModel
            {
                TenderIdString = Util.Encrypt(TENDER_ID),
                CancelationReasonDescription = "CancelationReasonDescription",
                CancelationReasonId = 1,
                VerificationCode = VERIFICATION_CODE
            };
            return tenderCancelModel;
        }

        public TenderQuantityTableChanges GeTenderQuantityTableChanges()
        {
            TenderQuantityTableChanges quantityTable = new TenderQuantityTableChanges(1, "Table Name",
                 Enums.QuantityTableChangeStatus.Add, 1);
            return quantityTable;
        }

        public TenderDatesChange GetTenderDatesChange()
        {
            return new TenderDatesChange(DateTime.Now, DateTime.Now, "11:11", DateTime.Now, "11:11", DateTime.Now, "11:11", 1);
        }
    }
}
