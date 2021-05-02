using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.TestsBuilders.TenderDefaults.ChangeRequestDefault;
using MOF.Etimad.Monafasat.ViewModel;
using Shouldly;
using System;
using System.Collections.Generic;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Tenders
{
    public class TenderChangeRequestTest
    {
        private readonly int _tenderId = 2003;
        private readonly int _tenderStatusId = 201;
        private readonly int _changeRequestTypeId = 1003;
        private readonly int _changeRequestStatusId = 2002;
        private readonly string _requestedById = "2001";
        private readonly bool _hasAlternativeOffer = true;
        private readonly int _changeStatusId = 10;
        private readonly int _userId = 100;
        private readonly int _cancelationReasonId = 105;
        private readonly string _cancelationReasonDescription = "CANCELATION REASON DESCRIPTION 10001";
        private readonly List<string> _supplierViolatorCRs = new List<string>() { "Cr1", "Cr2" };
        private readonly string _cr = "CR1000";
        private readonly string _rejectionReason = "REJECTIONREASON";
        private readonly DateTime _lastEnqueriesDate = DateTime.Now;
        private readonly DateTime _lastOfferPresentationDate = DateTime.Now.AddDays(1);
        private readonly string _lastOfferPresentationTime = DateTime.Now.TimeOfDay.ToString();
        private readonly DateTime _offersOpeningDate = DateTime.Now.AddDays(4);
        private readonly string _offersOpeningTime = DateTime.Now.AddDays(7).TimeOfDay.ToString();
        private readonly DateTime _offersCheckingDate = DateTime.Now.AddMonths(1);
        private readonly string _offersCheckingTime = DateTime.Now.AddDays(9).TimeOfDay.ToString();
        private readonly int _tableId = 800;
        private readonly int _itemNumber = 10011;
        private readonly string _tableName = "TB_Name";
        private readonly int _formId = 1010;
        private readonly List<TenderQuantityItemDTO> _table = new List<TenderQuantityItemDTO>() { new TenderQuantityItemDTO() };
        private readonly long _currentItemId = 1050;
        private readonly int _changeTableId = 0;

        [Fact]
        public void Should_Empty_Construct_TenderChangeRequest()
        {
            var tenderChangeRequest = new TenderChangeRequest();
            tenderChangeRequest.ShouldNotBeNull();
        }

        [Fact]
        public void Should_Constructor_SetValues()
        {
            var tenderChangeRequest = new TenderChangeRequest(_tenderId, _changeRequestTypeId, _changeRequestStatusId, _requestedById, _hasAlternativeOffer);
            tenderChangeRequest.ShouldNotBeNull();
            tenderChangeRequest.TenderId.ShouldBe(_tenderId);
            tenderChangeRequest.ChangeRequestTypeId.ShouldBe(_changeRequestTypeId);
            tenderChangeRequest.ChangeRequestStatusId.ShouldBe(_changeRequestStatusId);
            tenderChangeRequest.RequestedByRoleName.ShouldBe(_requestedById);
            tenderChangeRequest.HasAlternativeOffer.ShouldBe(true);
            tenderChangeRequest.State.ShouldBe(SharedKernal.ObjectState.Added);
        }

        [Fact]
        public void Should_UpdateStatus()
        {
            var tenderChangeRequest = new TenderChangeRequest();
            tenderChangeRequest.SetTender(new Tender());
            tenderChangeRequest.UpdateStatus(_changeStatusId, _userId, SharedKernel.Enums.TenderActions.AcceptInvitation);
            tenderChangeRequest.ShouldNotBeNull();
            tenderChangeRequest.State.ShouldBe(SharedKernal.ObjectState.Modified);
        }

        [Fact]
        public void Should_CreateCancelationRequest()
        {
            var tenderChangeRequest = new TenderChangeRequest();
            var _rslt = tenderChangeRequest.CreateCancelationRequest(new Tender(), _changeRequestTypeId, _changeRequestStatusId, _requestedById, _cancelationReasonId, _cancelationReasonDescription, _supplierViolatorCRs, _userId);
            tenderChangeRequest.ShouldNotBeNull();
            _rslt.ShouldBeOfType(typeof(TenderChangeRequest));
            _rslt.ChangeRequestStatusId.ShouldBe(_changeRequestStatusId);
            _rslt.ChangeRequestTypeId.ShouldBe(_changeRequestTypeId);
            _rslt.RequestedByRoleName.ShouldBe(_requestedById);
            _rslt.CancelationReasonId.ShouldBe(_cancelationReasonId);
            _rslt.CancelationReasonDescription.ShouldBe(_cancelationReasonDescription);
            _rslt.State.ShouldBe(SharedKernal.ObjectState.Added);
        }

        [Fact]
        public void Should_AddSupplierViolators()
        {
            var tenderChangeRequest = new TenderChangeRequest();
            tenderChangeRequest.AddSupplierViolators(_cr);
            tenderChangeRequest.SupplierViolators.ShouldNotBeEmpty();
        }

        [Fact]
        public void Should_UpdateStatusToRejection()
        {
            var tenderChangeRequest = new TenderChangeRequest();
            tenderChangeRequest.UpdateStatusToRejection();
            tenderChangeRequest.ChangeRequestStatusId.ShouldBe((int)Enums.ChangeStatusType.Rejected);
            tenderChangeRequest.RejectionReason.ShouldBe(Resources.TenderResources.Messages.CancelRequestRejectedBySysytem);
            tenderChangeRequest.State.ShouldBe(SharedKernal.ObjectState.Modified);
        }

        [Fact]
        public void Should_UpdateStatusToRejection_AddHistory()
        {
            var tenderChangeRequest = new TenderChangeRequest();
            tenderChangeRequest.SetTender(new Tender());
            tenderChangeRequest.UpdateStatusToRejection(_tenderStatusId, _changeStatusId, _rejectionReason, _userId, SharedKernel.Enums.TenderActions.AcceptOffers);
            tenderChangeRequest.State.ShouldBe(SharedKernal.ObjectState.Modified);
        }

        [Fact]
        public void Should_UpdateAttachmnetsChanges()
        {
            var tenderChangeRequest = new TenderChangeRequest();
            tenderChangeRequest.UpdateAttachmnetsChanges(new List<TenderAttachmentChanges>(), new List<TenderAttachmentChanges>());
            tenderChangeRequest.State.ShouldBe(SharedKernal.ObjectState.Modified);
        }

        [Fact]
        public void Should_AddRevisionDates()
        {
            var tenderChangeRequest = new TenderChangeRequest();
            tenderChangeRequest.AddRevisionDates(_lastEnqueriesDate, _lastOfferPresentationDate, _lastOfferPresentationTime, _offersOpeningDate, _offersOpeningTime, _offersCheckingDate, _offersCheckingTime, _tenderId);

            tenderChangeRequest.State.ShouldBe(SharedKernal.ObjectState.Modified);
        }

        [Fact]
        public void Should_AddRevisionCancel()
        {
            var tenderChangeRequest = new TenderChangeRequest();
            tenderChangeRequest.AddRevisionCancel(_lastEnqueriesDate, _lastOfferPresentationDate, _lastOfferPresentationTime, _offersOpeningDate, _offersOpeningTime, _offersCheckingDate, _offersCheckingTime, _tenderId);

            tenderChangeRequest.State.ShouldBe(SharedKernal.ObjectState.Modified);
        }

        [Fact]
        public void Should_AddActionHistory()
        {
            var tenderChangeRequest = new TenderChangeRequest();
            tenderChangeRequest.SetTender(new Tender());
            tenderChangeRequest.AddActionHistory(_changeStatusId, Enums.TenderActions.Addfile, _rejectionReason, _userId);
            tenderChangeRequest.State.ShouldBe(SharedKernal.ObjectState.Modified);
        }

        [Fact]
        public void Should_Delete()
        {
            var tenderChangeRequest = new TenderChangeRequest();
            tenderChangeRequest.Delete();
            tenderChangeRequest.State.ShouldBe(SharedKernal.ObjectState.Deleted);
        }

        [Fact]
        public void Should_Update()
        {
            var tenderChangeRequest = new TenderChangeRequest();
            tenderChangeRequest.Update();
            tenderChangeRequest.State.ShouldBe(SharedKernal.ObjectState.Modified);
        }

        [Fact]
        public void Should_DeActive()
        {
            var tenderChangeRequest = new TenderChangeRequest();
            tenderChangeRequest.DeActive();
            tenderChangeRequest.IsActive.ShouldBe(false);
            tenderChangeRequest.State.ShouldBe(SharedKernal.ObjectState.Modified);
        }

        [Fact]
        public void Should_SetActive()
        {
            var tenderChangeRequest = new TenderChangeRequest();
            tenderChangeRequest.SetActive();
            tenderChangeRequest.IsActive.ShouldBe(true);
            tenderChangeRequest.State.ShouldBe(SharedKernal.ObjectState.Modified);
        }

        [Fact]
        public void Should_CreateTenderQuantityTables()
        {
            var tenderChangeRequest = new TenderChangeRequest();
            tenderChangeRequest.CreateTenderQuantityTables(_tableId, _tableName, _formId);
            tenderChangeRequest.State.ShouldBe(SharedKernal.ObjectState.Modified);
        }

        [Fact]
        public void Should_SaveTenderQuantityTables()
        {
            var tenderChangeRequest = new TenderChangeRequest();
            tenderChangeRequest.CreateTenderQuantityTables(_tableId, _tableName, _formId);
            long _itemId = 0;
            var _rslt = tenderChangeRequest.SaveTenderQuantityTables(_table, _currentItemId, out _itemId, _changeTableId);
            tenderChangeRequest.State.ShouldBe(SharedKernal.ObjectState.Modified);
            _rslt.ShouldBeOfType(typeof(TenderChangeRequest));
        }

        [Fact]
        public void Should_DeleteQuantityTableItemsChanges()
        {
            var tenderChangeRequest = new TenderChangeRequest();
            tenderChangeRequest.CreateTenderQuantityTables(_tableId, _tableName, _formId);
            tenderChangeRequest.DeleteQuantityTableItemsChanges(_tableId, _itemNumber);
            tenderChangeRequest.State.ShouldBe(SharedKernal.ObjectState.Modified);
        }

        [Fact]
        public void Should_UpdateQuantityTableName()
        {
            var tenderChangeRequest = new TenderChangeRequest();
            tenderChangeRequest.CreateTenderQuantityTables(_tableId, _tableName, _formId);
            tenderChangeRequest.UpdateQuantityTableName(0, _tableName + "New");
            tenderChangeRequest.State.ShouldBe(SharedKernal.ObjectState.Modified);
        }

        [Fact]
        public void Should_DeleteTenderQuantityTableWithItems()
        {
            var tenderChangeRequest = new TenderChangeRequest();
            tenderChangeRequest.CreateTenderQuantityTables(_tableId, _tableName, _formId);
            tenderChangeRequest.DeleteTenderQuantityTableWithItems(0);
            tenderChangeRequest.State.ShouldBe(SharedKernal.ObjectState.Modified);
        }

        [Fact]
        public void Should_ChangeHasAlternativeOffer()
        {
            var tenderChangeRequest = new TenderChangeRequest();
            tenderChangeRequest.ChangeHasAlternativeOffer(true);
            tenderChangeRequest.HasAlternativeOffer.ShouldBe(true);
            tenderChangeRequest.State.ShouldBe(SharedKernal.ObjectState.Modified);
        }

        [Fact]
        public void Should_DeleteExistingTenderQuantityTable()
        {
            var tenderChangeRequest = new TenderChangeRequest();
            tenderChangeRequest.CreateTenderQuantityTables(_tableId, _tableName, _formId);
            tenderChangeRequest.DeleteExistingTenderQuantityTable(0, _tableId);
            tenderChangeRequest.State.ShouldBe(SharedKernal.ObjectState.Modified);
        }

        [Fact]
        public void Should_SaveTenderQuantityTableItems()
        {
            var tenderChangeRequest = new TenderChangeRequest();
            tenderChangeRequest.CreateTenderQuantityTables(_tableId, _tableName, _formId);
            tenderChangeRequest.SaveTenderQuantityTableItems(_table, _tableName);
            tenderChangeRequest.State.ShouldBe(SharedKernal.ObjectState.Modified);
        }

        [Fact]
        public void ShouldGetTenderQuantityTableLastIndexInEdit()
        {
            TenderChangeRequest tenderChangeRequest = new TenderChangeRequest(1, (int)Enums.ChangeRequestType.Canceling, (int)Enums.ChangeStatusType.Pending, "NewMonafasat_DataEntry", null);
            typeof(TenderChangeRequest).GetProperty(nameof(TenderChangeRequest.TenderChangeRequestId)).SetValue(tenderChangeRequest, 1);
            
            var changeRequestItems = new ChangeRequestDefault().GeTenderQuantityTableChanges();
            typeof(TenderQuantityTableChanges).GetProperty(nameof(TenderQuantityTableChanges.Id)).SetValue(changeRequestItems, 1);
            
            tenderChangeRequest.TenderQuantityTableChanges.Add(changeRequestItems);



            var lastIndex = tenderChangeRequest.LastItemIndex(1);

            Assert.Equal(0, lastIndex);
        }
    }
}
