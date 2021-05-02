using MOF.Etimad.Monafasat.Core.Entities.Lookups;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using static MOF.Etimad.Monafasat.SharedKernel.Enums;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("TenderChangeRequest", Schema = "Tender")]
    public class TenderChangeRequest : AuditableEntity
    {
        #region Fields====================================================
        [Key]
        public int TenderChangeRequestId { get; private set; }

        [ForeignKey(nameof(Tender))]
        public int TenderId { get; private set; }
        public Tender Tender { private set; get; }

        [ForeignKey(nameof(ChangeRequestStatus))]
        public int ChangeRequestStatusId { get; private set; }
        public ChangeRequestStatus ChangeRequestStatus { private set; get; }

        [ForeignKey(nameof(ChangeRequestType))]
        public int ChangeRequestTypeId { get; private set; }
        public ChangeRequestType ChangeRequestType { private set; get; }
        public string RequestedByRoleName { get; private set; }
        public string RejectionReason { get; private set; }
        public bool? HasAlternativeOffer { get; set; }

        [StringLength(1000)]
        public string CancelationReasonDescription { get; private set; }

        [ForeignKey(nameof(CancelationReason))]
        public int? CancelationReasonId { get; private set; }
        public CancelationReason CancelationReason { private set; get; }
        public List<TenderQuantityTableChanges> TenderQuantityTableChanges { get; private set; } = new List<TenderQuantityTableChanges>();
        public List<TenderAttachmentChanges> AttachmentChanges { get; private set; } = new List<TenderAttachmentChanges>();
        public List<TenderDatesChange> RevisionDates { get; private set; } = new List<TenderDatesChange>();
        public List<SupplierViolator> SupplierViolators { get; private set; } = new List<SupplierViolator>();
        #endregion

        #region NotMapped


        #endregion

        #region Constructors====================================================

        public TenderChangeRequest(int tenderId, int changeRequestTypeId, int changeRequestStatusId, string requestedById, bool? hasAlternativeOffer)
        {
            TenderId = tenderId;
            ChangeRequestStatusId = changeRequestStatusId;
            ChangeRequestTypeId = changeRequestTypeId;
            RequestedByRoleName = requestedById;
            HasAlternativeOffer = hasAlternativeOffer;

            EntityCreated();
        }

        public TenderChangeRequest()
        {

        }
        #endregion

        #region Methods====================================================
        public void UpdateStatus(int changeStatusId, int userId, TenderActions tenderAction)
        {
            ChangeRequestStatusId = changeStatusId;
            AddActionHistory(changeStatusId, tenderAction, "", userId);
            EntityUpdated();
        }

        public TenderChangeRequest CreateCancelationRequest(Tender tender, int changeRequestTypeId, int changeRequestStatusId, string requestedById, int? cancelationReasonId, string cancelationReasonDescription, List<string> supplierViolatorCRs, int userId)
        {
            TenderId = tender.TenderId;
            ChangeRequestStatusId = changeRequestStatusId;
            ChangeRequestTypeId = changeRequestTypeId;
            RequestedByRoleName = requestedById;
            CancelationReasonId = cancelationReasonId;
            CancelationReasonDescription = cancelationReasonDescription;
            EntityCreated();

            tender.AddActionHistory(tender.TenderStatusId, TenderActions.SendtenderCancelRequest, "", userId);
            tender.ChangeRequests.Add(this);
            if (supplierViolatorCRs != null)
            {
                foreach (var cr in supplierViolatorCRs)
                    SupplierViolators.Add(new SupplierViolator(cr));
            }
            return this;
        }

        public void AddSupplierViolators(string cr)
        {
            SupplierViolators.Add(new SupplierViolator(cr));

        }

        public void UpdateStatusToRejection(int tenderStatusId, int changeStatusId, string rejectionReason, int userId, TenderActions tenderAction)
        {
            ChangeRequestStatusId = changeStatusId;
            RejectionReason = rejectionReason;
            AddActionHistory(tenderStatusId, tenderAction, rejectionReason, userId);
            EntityUpdated();
        }
        public void UpdateStatusToRejection()
        {
            ChangeRequestStatusId = (int)Enums.ChangeStatusType.Rejected;
            RejectionReason = Resources.TenderResources.Messages.CancelRequestRejectedBySysytem;
            EntityUpdated();
        }
        public TenderChangeRequest UpdateAttachmnetsChanges(List<TenderAttachmentChanges> oldItems, List<TenderAttachmentChanges> newItems)
        {
            newItems.ForEach(x => AttachmentChanges.Add(new TenderAttachmentChanges(x.Name, x.FileNetReferenceId, x.AttachmentTypeId)));
            oldItems.ForEach(x => AttachmentChanges.Add(new TenderAttachmentChanges(x.Name, x.FileNetReferenceId, x.AttachmentTypeId, x.TenderAttachmentId)));
            EntityUpdated();
            return this;
        }
        public TenderChangeRequest AddRevisionDates(DateTime? lastEnqueriesDate, DateTime? lastOfferPresentationDate, string lastOfferPresentationTime,
            DateTime? offersOpeningDate, string offersOpeningTime, DateTime? offersCheckingDate, string offersCheckingTime, int tenderId)//, int userId)
        {
            RevisionDates.Add(new TenderDatesChange(lastEnqueriesDate, lastOfferPresentationDate,
                   lastOfferPresentationTime, offersOpeningDate, offersOpeningTime, offersCheckingDate, offersCheckingTime, TenderId));
            EntityUpdated();
            return this;
        }

        public TenderChangeRequest AddRevisionCancel(DateTime? lastEnqueriesDate, DateTime? lastOfferPresentationDate, string lastOfferPresentationTime,
            DateTime? offersOpeningDate, string offersOpeningTime, DateTime? offersCheckingDate, string offersCheckingTime, int tenderId)//, int userId)
        {
            RevisionDates.Add(new TenderDatesChange(lastEnqueriesDate, lastOfferPresentationDate,
                   lastOfferPresentationTime, offersOpeningDate, offersOpeningTime, offersCheckingDate, offersCheckingTime, TenderId));
            EntityUpdated();
            return this;
        }
        public void AddActionHistory(int statusId, TenderActions action, string rejectionReason, int currentUserId)
        {
            Tender.TenderHistories.Add(new TenderHistory(currentUserId, statusId, action, rejectionReason));
            EntityUpdated();
        }

        public void SetTender(Tender tender)
        {
            this.Tender = tender;
        }
        public void Delete()
        {
            EntityDeleted();
        }
        public void Update()
        {
            EntityUpdated();
        }
        public void DeActive()
        {
            IsActive = false;
            EntityUpdated();
        }
        public void SetActive()
        {
            IsActive = true;
            EntityUpdated();
        }

        public TenderQuantityTableChanges CreateTenderQuantityTables(long tableId, string name, int formId)
        {
            var table = new TenderQuantityTableChanges(tableId, string.IsNullOrEmpty(name) ? "اسم الجدول" : name, QuantityTableChangeStatus.Add, formId);
            TenderQuantityTableChanges.Add(table);
            EntityUpdated();
            return table;
        }
        public TenderChangeRequest SaveTenderQuantityTables(List<TenderQuantityItemDTO> table, long currentItemId, out long itemId, long changeTableId)
        {
            TenderQuantityTableChanges tbl;
            tbl = TenderQuantityTableChanges.FirstOrDefault(a => a.Id == changeTableId);
            tbl = tbl.SaveQuantityTableItems(changeTableId, table, currentItemId, out itemId);
            EntityUpdated();
            return this;
        }
        public TenderQuantityTableChanges DeleteQuantityTableItemsChanges(long tableId, int itemNumber)
        {
            TenderQuantityTableChanges table = TenderQuantityTableChanges.FirstOrDefault(a => a.Id == tableId && a.IsActive == true); //Starting from 1
            if (table != null)
                table.DeleteQuantityTableItem(itemNumber);
            EntityUpdated();
            return table;
        }
        public void UpdateQuantityTableName(long tableId, string name)
        {
            TenderQuantityTableChanges quantityTable = TenderQuantityTableChanges.FirstOrDefault(q => q.Id == tableId);
            quantityTable.UpdateName(name);
            EntityUpdated();
        }
        public void DeleteTenderQuantityTableWithItems(int tableId)
        {
            TenderQuantityTableChanges tenderQuantityTable = TenderQuantityTableChanges.FirstOrDefault(t => t.Id == tableId);
            if (tenderQuantityTable != null)
            {
                tenderQuantityTable.DeleteTenderQuantityTableWithItems();
            }
            EntityUpdated();
        }
        public void ChangeHasAlternativeOffer(bool hasAlternativeOffer)
        {
            HasAlternativeOffer = hasAlternativeOffer;
            EntityUpdated();
        }
        public void DeleteExistingTenderQuantityTable(long tableId, long currentTableId)
        {
            TenderQuantityTableChanges tenderQuantityTable = TenderQuantityTableChanges.FirstOrDefault(t => t.Id == tableId);
            tenderQuantityTable.DeleteExistingTenderQuantityTable(currentTableId);
            EntityUpdated();
        }
        public void SaveTenderQuantityTableItems(List<TenderQuantityItemDTO> table, string tableName)
        {
            TenderQuantityTableChanges tbl;
            long tableId = table[0].TableId;
            tbl = TenderQuantityTableChanges.FirstOrDefault(a => a.Id == tableId);
            tbl = tbl.SaveQuantityTableBulkItems(tableId, table, tableName);
            EntityUpdated();
        }
        
        public long LastItemIndex(long changeTableId)
        {
            TenderQuantityTableChanges tbl;
            tbl = TenderQuantityTableChanges.FirstOrDefault(a => a.Id == changeTableId);
            return tbl.LastItemIndex();
        }

        #endregion


    }
}
