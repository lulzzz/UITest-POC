using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Core.Entities.Negotiations;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.TestsBuilders.TenderDefaults;
using MOF.Etimad.Monafasat.ViewModel;
using System;
using System.Collections.Generic;

namespace MOF.Etimad.Monafasat.TestsBuilders
{
    public class NegotiationDefaults
    {
        private const string _Cr = "1299659801";

        public NegotiationFirstStage GetNegotiationFirstStage()
        {

            NegotiationFirstStage nego = new NegotiationFirstStage(2, "232", 1500, 1, 2, new List<ViewModel.NegotiationAttachmentViewModel>(), 5, "4343");
            nego.AgencyCommunicationRequest = new Core.Entities.AgencyCommunicationRequest(1, 1);
            nego.NegotiationFirstStageSuppliers = new List<NegotiationFirstStageSupplier>();
            nego.NegotiationFirstStageSuppliers.Add(new NegotiationFirstStageSupplier((int)(Enums.enNegotiationSupplierStatus.PendeingSupplierReply), DateTime.Now, 1, "", 1, 1232));
            var tender = new TenderDefault().GetGeneralTender();
            nego.AgencyCommunicationRequest.AddTender(tender);
            return nego;
        }

        public NegotiationFirstStage GetNegotiationFirstStageWaitingNotInvitedSupplier()
        {

            NegotiationFirstStage nego = new NegotiationFirstStage(2, "232", 1500, 1, 2, new List<ViewModel.NegotiationAttachmentViewModel>(), 5, "4343");
            nego.AgencyCommunicationRequest = new Core.Entities.AgencyCommunicationRequest(1, 1);
            nego.NegotiationFirstStageSuppliers = new List<NegotiationFirstStageSupplier>();
            nego.NegotiationFirstStageSuppliers.Add(new NegotiationFirstStageSupplier((int)(Enums.enNegotiationSupplierStatus.PendeingSupplierReply), DateTime.Now.AddDays(-1), 1, "", 1, 1232));
            var tender = new TenderDefault().GetGeneralTender();
            nego.AgencyCommunicationRequest.AddTender(tender);
            return nego;
        }


        public NegotiationFirstStage GetNegotiationFirstStageWaitingSupplier()
        {

            NegotiationFirstStage nego = new NegotiationFirstStage(2, "232", 1500, 1, 2, new List<ViewModel.NegotiationAttachmentViewModel>(), 5, "4343");
            nego.AgencyCommunicationRequest = new Core.Entities.AgencyCommunicationRequest(1, 1);
            nego.NegotiationFirstStageSuppliers = new List<NegotiationFirstStageSupplier>();
            nego.NegotiationFirstStageSuppliers.Add(new NegotiationFirstStageSupplier((int)(Enums.enNegotiationSupplierStatus.PendeingSupplierReply), DateTime.Now.AddDays(-1), 1, "", 1, 1232));
            var tender = new TenderDefault().GetGeneralTender();
            nego.AgencyCommunicationRequest.AddTender(tender);
            return nego;
        }

        public NegotiationFirstStage GetNegotiationFirstStageWithFaildStatus()
        {
            NegotiationFirstStage nego = new NegotiationFirstStage(2, "232", 1500, 1, 2, new List<ViewModel.NegotiationAttachmentViewModel>(), 5, "4343");
            nego.AgencyCommunicationRequest = new Core.Entities.AgencyCommunicationRequest(1, 1);
            nego.NegotiationFirstStageSuppliers = new List<NegotiationFirstStageSupplier>();
            nego.NegotiationFirstStageSuppliers.Add(new NegotiationFirstStageSupplier((int)(Enums.enNegotiationSupplierStatus.NoReply), DateTime.Now.AddDays(-1), 0, "", 1, 1232));
            return nego;
        }
        public NegotiationFirstStage GetNegotiationFirstStageWithTimePassed()
        {
            NegotiationFirstStage nego = new NegotiationFirstStage(2, "232", 1500, 1, 2, new List<ViewModel.NegotiationAttachmentViewModel>(), 5, "4343");
            nego.AgencyCommunicationRequest = new Core.Entities.AgencyCommunicationRequest(1, 1);
            var tender = new TenderDefault().GetGeneralTender();
            nego.AgencyCommunicationRequest.AddTender(tender);
            nego.NegotiationFirstStageSuppliers = new List<NegotiationFirstStageSupplier>();
            nego.NegotiationFirstStageSuppliers.Add(new NegotiationFirstStageSupplier((int)(Enums.enNegotiationSupplierStatus.PendeingSupplierReply), DateTime.Now.AddDays(-1), 0, "", 1, 1232));
            return nego;
        }

        public NegotiationFirstStage GetNegotiationFirstStageWithSupplierAgreeStatus()
        {
            NegotiationFirstStage nego = new NegotiationFirstStage(2, "232", 1500, 1, 2, new List<ViewModel.NegotiationAttachmentViewModel>(), 5, "4343");
            nego.AgencyCommunicationRequest = new Core.Entities.AgencyCommunicationRequest(1, 1);
            nego.NegotiationFirstStageSuppliers = new List<NegotiationFirstStageSupplier>();
            nego.NegotiationFirstStageSuppliers.Add(new NegotiationFirstStageSupplier((int)(Enums.enNegotiationSupplierStatus.Agree), DateTime.Now.AddDays(-1), 1, "", 1, 1232));
            return nego;
        }     
        public NegotiationFirstStage GetNegotiationFirstStageWithSupplierDisAgreeStatus()
        {
            NegotiationFirstStage nego = new NegotiationFirstStage(2, "232", 1500, 1, 2, new List<ViewModel.NegotiationAttachmentViewModel>(), 5, "4343");
            nego.AgencyCommunicationRequest = new Core.Entities.AgencyCommunicationRequest(1, 1);
            nego.NegotiationFirstStageSuppliers = new List<NegotiationFirstStageSupplier>();
            nego.NegotiationFirstStageSuppliers.Add(new NegotiationFirstStageSupplier((int)(Enums.enNegotiationSupplierStatus.DisAgree), DateTime.Now.AddDays(-1), 1, "", 1, 1232));
            return nego;
        }

        public NegotiationFirstStageSupplier GetNegotiationFirstStageSupplier(int offerId, string cr)
        {
            NegotiationFirstStageSupplier negotiationSupplier = new NegotiationFirstStageSupplier(1, DateTime.Now, offerId, cr, 1, 500);
            return negotiationSupplier;
        }

        public List<NegotiationFirstStageSupplier> GetNegotiationFirstStageSupplier()
        {
            return new List<NegotiationFirstStageSupplier>()
            {
                GetNegotiationFirstStageSupplier(1, _Cr)
            };
        }

        public List<NegotiationAttachmentViewModel> GetNegotiationAttachments()
        {
            List<NegotiationAttachmentViewModel> attachments = new List<NegotiationAttachmentViewModel>();
            var att = new NegotiationAttachmentViewModel() { Name = "First Attachment", FileNetReferenceId = "Diirrekrekr", AttachmentTypeId = 1 };
            var att2 = new NegotiationAttachmentViewModel() { Name = "Second Attachment", FileNetReferenceId = "Diirrekrekr", AttachmentTypeId = 1 };
            var att3 = new NegotiationAttachmentViewModel() { Name = "Third Attachment", FileNetReferenceId = "Diirrekrekr", AttachmentTypeId = 1 };
            attachments.Add(att);
            attachments.Add(att2);
            attachments.Add(att3);
            return attachments;
        }

        public NegotiationQuantityItemJson GetNegotiationQuantityJsonWithItems()
        {
            NegotiationQuantityItemJson itemJson = new NegotiationQuantityItemJson();
            List<NegotiationSupplierQuantityTableItem> tableItems = new List<NegotiationSupplierQuantityTableItem>() {
            new NegotiationSupplierQuantityTableItem(1, 1, 1, 1,"Price", 1),
            new NegotiationSupplierQuantityTableItem(1, 1, 1, 1,"Price", 1),
            new NegotiationSupplierQuantityTableItem(1, 1, 1, 1,"Discount", 1),
            new NegotiationSupplierQuantityTableItem(1, 1, 1, 1,"Price", 2),
            new NegotiationSupplierQuantityTableItem(1, 1, 1, 1,"Price", 2),
            new NegotiationSupplierQuantityTableItem(1, 1, 1, 1,"Discount", 2)
            };
            itemJson.NegotiationSupplierQuantityTableItems.AddRange(tableItems);
            return itemJson;
        }

        public SupplierTenderQuantityTable GetSupplierTenderQuantityTable()
        {
            SupplierTenderQuantityTable supplierTenderTable = new SupplierTenderQuantityTable(1,"table 1");
            List<SupplierTenderQuantityTableItem> itemList = new List<SupplierTenderQuantityTableItem>()
            {
            new SupplierTenderQuantityTableItem(1, 1, 1, 1,"Price", 1),
            new SupplierTenderQuantityTableItem(1, 1, 1, 1,"Price", 1),
            new SupplierTenderQuantityTableItem(1, 1, 1, 1,"Discount", 1),
            new SupplierTenderQuantityTableItem(1, 1, 1, 1,"Price", 2),
            new SupplierTenderQuantityTableItem(1, 1, 1, 1,"Price", 2),
            new SupplierTenderQuantityTableItem(1, 1, 1, 1,"Discount", 2)
            };
            supplierTenderTable.QuantitiyItemsJson = new SupplierTenderQuantityTableItemJson(itemList);
            return supplierTenderTable;
        }
        
        public TenderQuantityItemDTO GetTenderQuantityItemDTO()
        {
            TenderQuantityItemDTO supplierTenderTable = new TenderQuantityItemDTO()
            {
                ColumnId = 1,
                ColumnName = "Price",
                Id = 1,
                IsDefault = true,
                TableId = 0,
                ItemNumber = 1,
                Value = "10",
                TableName = "Table Name",

            };
           
            return supplierTenderTable;
        }

        public NegotiationSecondStage GetNegotiationSecondStageWithQTables()
        {
            NegotiationSecondStage negotiationSecondStage = new NegotiationSecondStage(1, 1, 1, 1);
            negotiationSecondStage.negotiationSupplierQuantitiestable.Add(GetNegotiationSecondStageQunaitityTables());
            return negotiationSecondStage;
        }

        public NegotiationSecondStage GetNegotiationSecondStage()
        {
            NegotiationSecondStage negotiationSecondStage = new NegotiationSecondStage(1, 1, 1, 1);
            negotiationSecondStage.AgencyCommunicationRequest = new AgencyCommunicationRequest(1,1);
            var tender = new TenderDefault().GetNewDirectPurchasePrivate();
            negotiationSecondStage.AgencyCommunicationRequest.AddTender(tender);
            negotiationSecondStage.negotiationSupplierQuantitiestable.Add(GetNegotiationSecondStageQunaitityTables());
            return negotiationSecondStage;
        }

        public NegotiationSupplierQuantityTable GetNegotiationSecondStageQunaitityTables()
        {
            var negotiationQTItem = GetNegotiationQuantityJsonWithItems().NegotiationSupplierQuantityTableItems;
            NegotiationSupplierQuantityTable negotiationSecondStage = new NegotiationSupplierQuantityTable(1, "Table 1", 1, negotiationQTItem);
            return negotiationSecondStage;
        }
    }
}
