using MOF.Etimad.Monafasat.Core.Entities.Negotiations;
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
    [Table("Offer", Schema = "Offer")]
    public class Offer : AuditableEntity
    {
        #region Constructors


        public Offer(int tenderId, string commericalRegisterNo, List<SupplierTenderQuantityTable> quantityTable, int? offerPresentationWayid, bool isSolidarity = false)
        {
            CommericalRegisterNo = commericalRegisterNo;
            TenderId = tenderId;
            OfferStatusId = (int)Enums.OfferStatus.UnderEstablishing;
            SupplierTenderQuantityTables = quantityTable;
            SetAttachmentsValuesToTrue();
            this.IsSolidarity = isSolidarity;
            OfferPresentationWayId = offerPresentationWayid;
            Combined.Add(new OfferSolidarity((commericalRegisterNo), (int)Enums.UnRegisteredSuppliersInvitationType.SolidarityLeader));
            EntityCreated();
        }

        public Offer() { }
        public Offer(string commericalRegisterNo, int tenderId, bool? isActive)
        {
            CommericalRegisterNo = commericalRegisterNo;
            TenderId = tenderId;
            IsActive = isActive;
            SetAttachmentsValuesToTrue();
            EntityCreated();
        }

        public void UpdateOfferSupplierQItems(List<SupplierTenderQuantityTableItem> quantityTable, int tableId)
        {
            var table = SupplierTenderQuantityTables.FirstOrDefault(p => p.Id == tableId);
            table.UpadteSupplierQTableItems(quantityTable);
            EntityUpdated();
        }
        public void UpdateOfferSupplierQItemsDefault(Dictionary<long, bool> quantityTable, int tableId)
        {
            var table = SupplierTenderQuantityTables.FirstOrDefault(p => p.Id == tableId);
            table.UpadteSupplierQTableItemsDefault(quantityTable);
            EntityUpdated();
        }
        public void UpadteSupplierQTableItemsAlternativeValue(List<SupplierTenderQuantityTableItem> quantityTable, int tableId)
        {
            var table = SupplierTenderQuantityTables.FirstOrDefault(p => p.Id == tableId);
            table.UpadteSupplierQTableItemsAlternativeValue(quantityTable);
            EntityUpdated();
        }
        public void UpdateOpeningDiscountNotes(string discount, string discountNotes)
        {
            Discount = discount;
            DiscountNotes = discountNotes;
            EntityUpdated();
        }

        //Abdelfattah
        public void SetAttachmentsValuesToTrue()
        {
            IsOpened = false;
        }
        public void AddRegisteredSolidaritySupplier(List<OfferSolidarity> solidarityRegisteredSuppliers)
        {

            foreach (var supplier in solidarityRegisteredSuppliers)
            {
                this.Combined.Add(supplier);
            }

            EntityUpdated();
        }
        public void AddRegisteredCombinedSupplier(OfferSolidarity registeredSupplierCombined)
        {


            this.Combined.Add(registeredSupplierCombined);
            EntityUpdated();
        }
        public void AddUnRegisteredCombinedSupplier(OfferSolidarity unRegisteredSupplierCombined)
        {
            this.Combined.Add(unRegisteredSupplierCombined);

            EntityUpdated();
        }

        #endregion

        #region Fields 
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OfferId { get; private set; }
        public int SuplierId { get; private set; }
        public int TenderId { get; private set; }
        public int OfferStatusId { get; private set; }
        public int? OfferPresentationWayId { get; private set; }
        public int? OfferAcceptanceStatusId { get; set; }
        public int? OfferTechnicalEvaluationStatusId { get; set; }
        public bool IsManuallyApplied { get; private set; }
        [StringLength(20)]
        public string CommericalRegisterNo { get; private set; }
        public DateTime? WithrdrawlDate { get; private set; }
        public string Notes { get; private set; }
        public string FinantialNotes { get; private set; }
        public string DiscountNotes { get; private set; }
        public string Discount { get; private set; }
        public bool? IsOfferCopyAttached { get; private set; }
        public bool IsSolidarity { get; private set; } = false;
        public bool? IsOfferLetterAttached { get; private set; }
        [StringLength(500)]
        public string OfferLetterNumber { get; private set; }
        public DateTime? OfferLetterDate { get; private set; }
        public bool? IsPurchaseBillAttached { get; private set; }
        public bool? IsBankGuaranteeAttached { get; private set; }
        public bool? IsVisitationAttached { get; private set; }
        [StringLength(1024)]
        public string JustificationOfRecommendation { get; private set; } //مبررات التوصية
        public bool? IsOpened { get; private set; }
        public decimal? TotalOfferAwardingValue { get; private set; }
        public decimal? PartialOfferAwardingValue { get; private set; }
        public decimal? FinalPriceAfterDiscount { get; private set; } = 0;
        public decimal? FinalPriceBeforeDiscount { get; private set; } = 0;
        public decimal? OfferWeightAfterCalcNPA { get; private set; } = 0;
        public string RejectionReason { get; private set; }
        public string FinantialRejectionReason { get; private set; }
        public bool? IsFinaintialOfferLetterAttached { get; private set; }
        public string FinantialOfferLetterNumber { get; private set; }
        public DateTime? FinantialOfferLetterDate { get; private set; }
        public bool? IsFinantialOfferLetterCopyAttached { get; private set; }
        public bool IsOfferFinancialDetailsEntered { get; private set; }
        public int TechnicalEvaluationLevel { get; private set; }
        public int FinancialEvaluationLevel { get; private set; }
        public string ExclusionReason { get; private set; }
        public decimal? OfferFinalPrice { get; private set; } = 0;


        #endregion

        #region NavigationProperties
        public OfferlocalContentDetails OfferlocalContentDetails { get; set; }
        [ForeignKey("CommericalRegisterNo")]
        public Supplier Supplier { get; set; }
        [ForeignKey("TenderId")]
        public Tender Tender { get; set; }
        [ForeignKey(nameof(OfferPresentationWayId))]
        public OfferPresentationWay OfferPresentationWay { get; private set; }
        [ForeignKey("OfferStatusId")]
        public Entities.Lookups.OfferStatus Status { get; private set; }
        public List<NegotiationFirstStageSupplier> negotiationFirstStageSuppliers { get; private set; }
        public List<NegotiationSecondStage> negotiations { get; private set; }
        public List<SupplierTenderQuantityTable> SupplierTenderQuantityTables { get; private set; } = new List<SupplierTenderQuantityTable>();
        public List<SupplierAttachment> Attachment { get; private set; } = new List<SupplierAttachment>();
        public List<OfferHistory> OffersHistory { get; private set; } = new List<OfferHistory>();
        public List<SupplierBankGuaranteeDetail> BankGuaranteeDetails { get; private set; } = new List<SupplierBankGuaranteeDetail>();
        public List<OfferSolidarity> Combined { get; private set; } = new List<OfferSolidarity>();
        public ExtendOffersValiditySupplier ExtendOffersValiditySupplier { get; set; }
        public List<TechnicianReportAttachment> TechnicianReportAttachments { private set; get; } = new List<TechnicianReportAttachment>();
        public List<PlaintRequest> PlaintRequests { get; private set; } = new List<PlaintRequest>();

        #endregion

        #region Methods

        public void UpdateOfferAttachments(bool? isOfferCopyAttached, bool? isOfferLetterAttached, string offerLetterNumber, DateTime? offerLetterDate, bool? isVisitationAttached, bool? isPurchaseBillAttached, bool? isBankGuaranteeAttached)
        {
            IsOfferCopyAttached = isOfferCopyAttached;
            IsOfferLetterAttached = isOfferLetterAttached;
            OfferLetterNumber = offerLetterNumber;
            OfferLetterDate = offerLetterDate;
            IsVisitationAttached = isVisitationAttached;
            IsPurchaseBillAttached = isPurchaseBillAttached;
            IsBankGuaranteeAttached = isBankGuaranteeAttached;
            EntityUpdated();
        }
        public void UpdateOfferFinancialAttachments(bool? isFinaintialOfferLetterAttached, bool? iIsFinantialOfferLetterCopyAttached)
        {
            IsFinaintialOfferLetterAttached = isFinaintialOfferLetterAttached;
            IsFinantialOfferLetterCopyAttached = iIsFinantialOfferLetterCopyAttached;
            EntityUpdated();
        }
        public void UpdateTechnicianReportAttachments(List<TechnicianReportAttachment> technicianReportAttachments)
        {
            if (TechnicianReportAttachments != null)
                foreach (var item in TechnicianReportAttachments)
                {
                    item.Delete();
                }
            if (technicianReportAttachments != null)
                foreach (var attachment in technicianReportAttachments)
                {
                    TechnicianReportAttachments.Add(attachment);
                }
            EntityUpdated();
        }
        public void AddAttachment(SupplierAttachment attachment)
        {
            Attachment.Add(attachment);
            EntityUpdated();

        }
        public void AddExclusionReasonForSuppliers(string exclusionReason)
        {
            this.ExclusionReason = exclusionReason;
            EntityUpdated();
        }
        public void RemoveExclusionReasonForSuppliers()
        {
            this.ExclusionReason = string.Empty;
            EntityUpdated();
        }
        public void ResetAwardingValue()
        {
            PartialOfferAwardingValue = null;
            TotalOfferAwardingValue = null;
            EntityUpdated();
        }
        public void ResetOfferToAwarding()
        {
            PartialOfferAwardingValue = null;
            TotalOfferAwardingValue = null;
            ExclusionReason = string.Empty;

            if (OfferStatusId == (int)Enums.OfferStatus.Excluded)
            {
                OfferStatusId = (int)Enums.OfferStatus.Received;
            }
            EntityUpdated();
        }
        public void ResetOfferToCheck()
        {
            PartialOfferAwardingValue = null;
            TotalOfferAwardingValue = null;
            OfferAcceptanceStatusId = null;
            OfferTechnicalEvaluationStatusId = null;
            OfferWeightAfterCalcNPA = null;
            ExclusionReason = string.Empty;
            if (OfferStatusId == (int)Enums.OfferStatus.Excluded)
            {
                OfferStatusId = (int)Enums.OfferStatus.Received;
            }
            EntityUpdated();
        }
        public void UpadatePriceAfterNegotiation(decimal price)
        {

            this.FinalPriceAfterDiscount = price;
            EntityUpdated();

        }
        public void UpadatePriceNPAfterNegotiation(decimal priceNP)
        {
            this.OfferWeightAfterCalcNPA = priceNP;
            EntityUpdated();

        }
        public void DeleteAttachment()
        {
            foreach (var item in Attachment)
            {
                item.Delete();
            }
            EntityUpdated();
        }
        public void AddAttachment(IEnumerable<SupplierAttachment> attachment)
        {
            if (attachment.Any())
            {
                Attachment.AddRange(attachment);
                EntityUpdated();
            }

        }

        public void AddBankGuarantee(IEnumerable<SupplierBankGuaranteeDetail> attachment)
        {
            BankGuaranteeDetails.AddRange(attachment);
            EntityUpdated();
        }
        public void RemoveAttachment(SupplierAttachment attachment)
        {
            Attachment.FirstOrDefault(d => d.AttachmentId == attachment.AttachmentId).DeleteAttachment();
            EntityUpdated();
        }
        public void AddActionToOfferHistory(int tenderStatusId, int offerStatusId, TenderActions action, int currentUserId, string cr)
        {
            OffersHistory.Add(new OfferHistory(tenderStatusId, offerStatusId, action, currentUserId, cr));
            EntityUpdated();
        }
        public void UpdateStatus(Enums.OfferStatus status)
        {
            OfferStatusId = (int)status;
            if (status == Enums.OfferStatus.Canceled)
            {
                WithrdrawlDate = DateTime.Now;
            }
            else
            {
                WithrdrawlDate = null;
            }
            EntityUpdated();
        }
        public void UpdateFinalPriceAfterDiscount(decimal _price)
        {
            this.FinalPriceAfterDiscount = _price;
            OfferFinalPrice = _price;
            EntityUpdated();
        }
        public void UpdateOfferWeightAfterCalcNPA(decimal? _price)
        {
            this.OfferWeightAfterCalcNPA = _price;
            this.OfferFinalPrice = _price;
            EntityUpdated();
        }
        public void UpdateFinalPriceBeforeDiscount(decimal _price)
        {
            this.FinalPriceBeforeDiscount = _price;

            EntityUpdated();
        }
        public void SetFinalPrice(decimal totalPrice)
        {
            this.FinalPriceAfterDiscount = totalPrice;
            this.OfferFinalPrice = totalPrice;
            EntityUpdated();
        }
        public void UpdateFinalPriceForNegotiation(decimal discountAmount)
        {
            OfferWeightAfterCalcNPA = (OfferWeightAfterCalcNPA - (discountAmount / 100 * OfferWeightAfterCalcNPA));
            FinalPriceAfterDiscount = (FinalPriceAfterDiscount - (discountAmount / 100 * FinalPriceAfterDiscount));
            EntityUpdated();
        }
        public void UpdateFinalPriceForNegotiationFirstStage(decimal discountAmount)
        {
            OfferWeightAfterCalcNPA = discountAmount;
            FinalPriceAfterDiscount = discountAmount;
            EntityUpdated();
        }
        public void DeActive()
        {
            IsActive = false;
            EntityUpdated();
        }
        public void Delete()
        {
            EntityDeleted();
        }
        public void UpdateOfferTechnicalWeight(int TechnicalEvaluationLevel)
        {
            this.TechnicalEvaluationLevel = TechnicalEvaluationLevel;
            EntityUpdated();
        }
        public void UpdateOfferFinancialWeight(int FinancialWeight)
        {
            this.FinancialEvaluationLevel = FinancialWeight;
            EntityUpdated();
        }
        public void UpdateVROOfferFinancialWeight(int FinancialWeight)
        {
            this.FinancialEvaluationLevel = FinancialWeight;
            IsOfferFinancialDetailsEntered = true;
            EntityUpdated();
        }
        public void UpdateOfferCheckingStatus(int? acceptanceStatusId, int? technicalEvaluationStatusId, string note, string rejectionReason = "")
        {
            OfferAcceptanceStatusId = acceptanceStatusId;
            OfferTechnicalEvaluationStatusId = technicalEvaluationStatusId;
            Notes = note;
            RejectionReason = rejectionReason;
            EntityUpdated();

        }
        public void UpdateOfferTecknicalCheckingStatus(int? technicalEvaluationStatusId, string rejectionReason = "", string notes = "")
        {
            OfferTechnicalEvaluationStatusId = technicalEvaluationStatusId;
            RejectionReason = rejectionReason;
            Notes = notes;
            EntityUpdated();
        }
        public void UpdateSolidarity(bool isSolidarity)
        {
            IsSolidarity = isSolidarity;
            EntityUpdated();
        }
        public void sendSolidarityInvitations(Enums.SupplierSolidarityStatus supplierSolidarity)
        {
            foreach (var item in this.Combined.Where(f => f.SolidarityTypeId != (int)Enums.UnRegisteredSuppliersInvitationType.SolidarityLeader))
            {
                item.UpdateStatus(supplierSolidarity);
            }
            EntityUpdated();
        }
        public void UpdateOfferFinantialCheckingStatus(int? offerAcceptanceStatusId, string rejectionReason = "", string finantialNotes = "")
        {
            OfferAcceptanceStatusId = offerAcceptanceStatusId;
            FinantialRejectionReason = rejectionReason;
            FinantialNotes = finantialNotes;
            EntityUpdated();
        }
        public void UpdateOfferFinantialCheckingDetails(bool? isFinaintialOfferLetterAttached, string finantialOfferLetterNumber, DateTime? finantialOfferLetterDate, bool? isFinantialOfferLetterCopyAttached, bool? isBankGuaranteeAttached)
        {
            IsFinaintialOfferLetterAttached = isFinaintialOfferLetterAttached;
            FinantialOfferLetterNumber = finantialOfferLetterNumber;
            FinantialOfferLetterDate = finantialOfferLetterDate.HasValue ? finantialOfferLetterDate != DateTime.MinValue ? finantialOfferLetterDate : null : null;
            IsFinantialOfferLetterCopyAttached = isFinantialOfferLetterCopyAttached;
            IsBankGuaranteeAttached = isBankGuaranteeAttached;
            IsOfferFinancialDetailsEntered = true;
            EntityUpdated();
        }
        public void UpdateBankGurnteesDetails(List<SupplierBankGuaranteeDetail> bankGuaranteeDetails)
        {
            foreach (var item in BankGuaranteeDetails)
            {
                item.Delete();
            }
            BankGuaranteeDetails.AddRange(bankGuaranteeDetails);
            if (OfferId == 0)
            {
                EntityCreated();
            }
            else
            {
                EntityUpdated();
            }
        }
        public void updateBankGurnteeList(List<SupplierBankGuaranteeDetail> bankGuranteelst)
        {
            List<SupplierBankGuaranteeDetail> SpecList = bankGuranteelst.Where(a => a.BankGuaranteeId == 0).ToList();
            AddBankGuarantee(SpecList);

            //Deleted Attachments

            List<int> newbankGuranteesIDs = bankGuranteelst.Where(a => a.BankGuaranteeId != 0).Select(a => a.BankGuaranteeId).ToList();
            var bankGurantees = BankGuaranteeDetails.Where(x => !newbankGuranteesIDs.Contains(x.BankGuaranteeId) && x.BankGuaranteeId != 0).ToList();

            foreach (var item in bankGurantees)
            {
                item.Delete();
            }
        }
        public void SaveOfferAwardingValues(int offerId, decimal? totalOfferAwardingValue, decimal? partialOfferAwardingValue, string justificationOfRecommendation)
        {
            OfferId = offerId;
            TotalOfferAwardingValue = totalOfferAwardingValue;
            PartialOfferAwardingValue = partialOfferAwardingValue;
            JustificationOfRecommendation = justificationOfRecommendation;
            EntityUpdated();
        }
        public void RemoveOfferAwardingValue(int offerId)
        {
            OfferId = offerId;
            TotalOfferAwardingValue = null;
            PartialOfferAwardingValue = null;
            JustificationOfRecommendation = null;
            EntityUpdated();
        }
        public void DeleteOfferAwardingValues(int offerId)
        {
            OfferId = offerId;
            TotalOfferAwardingValue = null;
            PartialOfferAwardingValue = null;
            JustificationOfRecommendation = null;
            EntityUpdated();
        }
        public void RemoveRegisteredSupplierCombined(OfferSolidarity entity)
        {
            var _cm = this.Combined.FirstOrDefault(e => e.CRNumber == entity.CRNumber);
            if (_cm == null)
                return;
            _cm.Delete();
            EntityUpdated();
        }
        public void RemoveSolidaritySupplier(int solidarityId)
        {
            var _cm = this.Combined.FirstOrDefault(e => e.Id == solidarityId);
            _cm.Delete();
            EntityUpdated();
        }
        public long CreateOfferQuantityTables(int tableId, string name = "")
        {
            SupplierTenderQuantityTables.Add(new SupplierTenderQuantityTable(tableId, name));
            EntityUpdated();
            return SupplierTenderQuantityTables.OrderByDescending(a => a.Id).FirstOrDefault().Id;
        }
        public void CreateVROOfferQuantityTables(int tableId, string name = "")
        {
            SupplierTenderQuantityTables.Add(new SupplierTenderQuantityTable(tableId, name));
            EntityUpdated();
        }
        public void SaveOfferQuantityTables(int tableid, List<TenderQuantityItemDTO> items)
        {
            var table = this.SupplierTenderQuantityTables.FirstOrDefault(d => d.Id == tableid);
            table.AddSupplierQTableItems(items);
            this.EntityUpdated();
        }
        public Offer SaveSupplierTenderQuantityTables(List<TenderQuantityItemDTO> table, string tableName, long currentItemId, out long itemId)
        {
            SupplierTenderQuantityTable tbl;
            long tableId = table[0].TableId;
            tbl = SupplierTenderQuantityTables.FirstOrDefault(a => a.Id == tableId);
            tbl = tbl.SaveQuantityTableItems(tableId, table, tableName, currentItemId, out itemId);
            EntityUpdated();
            return this;
        }
        public Offer DeleteQuantityTableItems(long tableId, int itemNumber)
        {
            SupplierTenderQuantityTable table = SupplierTenderQuantityTables.FirstOrDefault(a => a.Id == tableId && a.IsActive == true); //Starting from 1
            if (table != null)
                table.DeleteQuantityTableItems(itemNumber);
            EntityUpdated();
            return this;
        }

        public void UpdateOfferWeightAfterCalcSMEA(decimal? _price)
        {
            this.OfferlocalContentDetails.SetOfferPriceAfterSMEA(_price.Value);
            this.OfferFinalPrice = _price.Value;
            EntityUpdated();
        }
        public void ResetOfferLocalContentPreference()
        {
            this.OfferlocalContentDetails.SetOfferFinancialWeight(null);
            this.OfferlocalContentDetails.SetOfferPriceAfterSMEA(null);
            EntityUpdated();
        }
        public void SetOfferLocalContentDetails()
        {
            OfferlocalContentDetails = new OfferlocalContentDetails();
           
            EntityUpdated();
        }

        public void UpdateOfferFinalPrice(decimal price)
        {
            this.OfferFinalPrice = price;
            EntityUpdated();
        }
        #endregion

        #region NotMapped
        [NotMapped]
        public int InvitationTypeId { get; private set; }
        [NotMapped]
        public int InvitationId { get; private set; }
        #endregion

    }

}

