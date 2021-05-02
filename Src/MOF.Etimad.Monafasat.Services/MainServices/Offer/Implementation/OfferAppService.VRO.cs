using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernal.Exceptions;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.ViewModel.QuantityTableTemplates;
using MOF.Etimad.SharedKernel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static MOF.Etimad.Monafasat.SharedKernel.Enums;

namespace MOF.Etimad.Monafasat.Services
{
    public partial class OfferAppService
    {
        public async Task<VROOffersTechnicalCheckingViewModel> FindVROOfferDetailsById(string offerIdString)
        {
            int offerId = Util.Decrypt(offerIdString);
            return await _offerQueries.FindVROOfferDetailsById(offerId);
        }

        public async Task<VROOffersTechnicalCheckingViewModel> UpdateVROOfferCheckingStatus(VROOffersTechnicalCheckingViewModel offerModel)
        {
            Check.ArgumentNotNull(nameof(offerModel), offerModel);
            if (((offerModel.TenderTypeId != (int)Enums.TenderType.FirstStageTender && offerModel.TenderTypeId != (int)Enums.TenderType.ReverseBidding && offerModel.TenderTypeId != (int)Enums.TenderType.Competition) && offerModel.OfferAcceptanceStatusId == null) || offerModel.OfferTechnicalEvaluationStatusId == null)
                throw new BusinessRuleException(Resources.TenderResources.DisplayInputs.OfferNotTechnicallyEvaluated);
            Offer offer = await FindById(Util.Decrypt(offerModel.OfferIdString));
            offer.UpdateOfferCheckingStatus(offerModel.OfferAcceptanceStatusId, offerModel.OfferTechnicalEvaluationStatusId, offerModel.Notes, offerModel.RejectionReason);
            offer.UpdateOfferTechnicalWeight(offerModel.TechnicalEvaluationLevel);
            if (offer.Tender.TenderStatusId != (int)Enums.TenderStatus.VROOffersCheckingAndTechnicalEval)
                offer.Tender.UpdateTenderStatus(Enums.TenderStatus.VROOffersCheckingAndTechnicalEval, "", _httpContextAccessor.HttpContext.User.UserId(), TenderActions.StartVROOffersTechnicalChecking);
            await _offerCommands.UpdateAsync(offer);
            return await FindVROOfferDetailsById(offerModel.OfferIdString);
        }

        public async Task<VROFinantialCheckingModel> GeOfferByTenderIdAndOfferIdForFinantialChecking(int tenderId, int offerId)
        {
            VROFinantialCheckingModel result = await _offerQueries.GeOfferByTenderIdAndOfferIdForFinancialChecking(tenderId, offerId);
            if (_httpContextAccessor.HttpContext.User.IsInRole(RoleNames.OffersOpeningAndCheckSecretary)
                && (result.TenderStatusId == (int)Enums.TenderStatus.VROOffersFinancialChecking || result.TenderStatusId == (int)Enums.TenderStatus.VROFinancialCheckingOpening))
            {
                result.QuantityTableStepDTO = await FindOfferQuantityItemsVROById(tenderId, offerId, _httpContextAccessor.HttpContext.User.UserBranch(), false);
            }
            else
            {
                result.QuantityTableStepDTO = await FindOfferQuantityItemsVROById(tenderId, offerId, _httpContextAccessor.HttpContext.User.UserBranch(), true);
            }
            return result;
        }

        public async Task<Offer> AddFinancialOfferAttachmentsInVRO(OfferDetailsForCheckingModel m)
        {
            SupplierCombinedDetail details = await _offerQueries.GetCombinedOfferDetailForVRO(Util.Decrypt(m.CombinedIdString));
            if (details == null)
            {
                details = new SupplierCombinedDetail
                {
                    Combined = await _offerQueries.GetCombinedbyId(Util.Decrypt(m.CombinedIdString))
                };
            }
            var offer = details.Combined.Offer;
            if (offer.Tender.TenderStatusId != (int)Enums.TenderStatus.VROOffersFinancialChecking)
            {
                offer.Tender.UpdateTenderStatus(Enums.TenderStatus.VROOffersFinancialChecking, "", _httpContextAccessor.HttpContext.User.UserId(), TenderActions.FinancialCheckingStarted);
            }
            List<SupplierBankGuaranteeDetail> GuaranteesLst = new List<SupplierBankGuaranteeDetail>();
            List<SupplierSpecificationDetail> SpecificationsLst = new List<SupplierSpecificationDetail>();

            foreach (var item in m.SpecificationsFiles)
            {
                var Specification = new SupplierSpecificationDetail(item.SpecificationId, Util.Decrypt(m.CombinedIdString), item.IsForApplier, item.Degree, item.ConstructionWorkId, item.MaintenanceRunningWorkId);
                SpecificationsLst.Add(Specification);
            }

            foreach (var item in m.BankGuaranteeFiles)
            {
                var BankGuarantee = new SupplierBankGuaranteeDetail(item.BankGuaranteeId, offer.OfferId, item.IsBankGuaranteeValid, item.GuaranteeNumber, item.BankId.Value, item.Amount, item.GuaranteeStartDate, item.GuaranteeEndDate, item.GuaranteeDays);
                GuaranteesLst.Add(BankGuarantee);
            }
            offer.UpdateOfferAttachments(m.IsOfferCopyAttached, m.IsOfferLetterAttached, m.OfferLetterNumber, m.OfferLetterDate, m.IsVisitationAttached, m.IsPurchaseBillAttached, m.IsBankGuaranteeAttached);
            offer.updateBankGurnteeList(GuaranteesLst);
            offer.UpdateVROOfferFinancialWeight(m.FinancialWeight);
            offer.UpdateOpeningDiscountNotes(m.Discount, m.DiscountNotes);

            details.UpdateAttachmentDetails(details.CombinedDetailId, Util.Decrypt(m.CombinedIdString), m.IsChamberJoiningAttached, m.IsChamberJoiningValid,
                m.IsCommercialRegisterAttached, m.IsCommercialRegisterValid, m.IsSaudizationAttached, m.IsSaudizationValidDate, m.IsSocialInsuranceAttached,
                m.IsSocialInsuranceValidDate, m.IsZakatAttached, m.IsZakatValidDate, m.IsSpecificationAttached, m.IsSpecificationValidDate);
            details.updateAttachmentsList(SpecificationsLst);

            await CalcFinalPrice(offer);
            await _offerCommands.UpdateCombinedDetailAsync(details);

            return offer;
        }

        #region Offer checking QTServices

        public async Task<QuantityTableStepDTO> FindOfferQuantityItemsVROById(int tenderId, int offerId, int branchId, bool isReadOnly)
        {
            Offer offer = await _offerQueries.GeOfferByTenderIdAndOfferIdForChecking(offerId);
            List<long> formIds = offer.SupplierTenderQuantityTables.Where(a =>
            (a.QuantitiyItemsJson != null && a.QuantitiyItemsJson.SupplierTenderQuantityTableItems.Any()) && a.IsActive == true)
                .Select(a => long.Parse(a.TenderQuantityTable != null ? a.TenderQuantityTable.FormId.ToString() : "0")).ToList();
            ApiResponse<List<TemplateFormDTO>> obj = await _qantityTemplatesProxy.GenerateTemplatesByFormIds(formIds);
            QuantityTableStepDTO quantityTableStepDTO = new QuantityTableStepDTO
            {
                TemplateYears = offer.Tender.TemplateYears ?? 0,
                TenderID = offer.Tender.TenderId,
                TenderCreatedByTypeId = offer.Tender.CreatedByTypeId ?? 0,
                TenderIdString = Util.Encrypt(offer.Tender.TenderId),
                PreQualificationId = offer.Tender.PreQualificationId ?? 0,
                InvitationTypeId = offer.Tender.InvitationTypeId ?? 0,
                HasAlternativeOffer = offer.Tender.HasAlternativeOffer ?? false,
                TenderName = offer.Tender.TenderName,
                TenderStatusId = offer.Tender.TenderStatusId,
                ReferenceNumber = offer.Tender.ReferenceNumber,
                TenderNumber = offer.Tender.TenderNumber,
                TenderTypeId = offer.Tender.TenderTypeId,
                OfferPresentationWayId = offer.Tender.OfferPresentationWayId ?? 0,
                OfferId = offer.OfferId,
                TemplateFormDTOs = obj.Data
            };
            quantityTableStepDTO.TemplateFormDTOs.ForEach(a =>
            {
                a.FormDTOs.ForEach(f =>
                {
                    f.Tables = offer.SupplierTenderQuantityTables.Where(q => q.IsActive == true &&
                    (q.QuantitiyItemsJson != null && q.QuantitiyItemsJson.SupplierTenderQuantityTableItems.Count() > 0) && q.TenderQuantityTable?.FormId == f.FormId)
                    .Select(t => new TableDTO { TableId = t.Id, TableName = t.Name }).ToList();
                });
            });
            quantityTableStepDTO.IsReadOnly = isReadOnly;
            return quantityTableStepDTO;
        }

        public async Task<QueryResult<TableModel>> GetOfferTableQuantityItemsVRO(QuantityTableSearchCretriaModel quantityTableSearchCretriaModel)
        {
            var cellsCount = 0;
            cellsCount = await _offerQueries.GetOfferTableQuantityItems(quantityTableSearchCretriaModel.QuantityTableId);
            quantityTableSearchCretriaModel.CellsCount = cellsCount;
            var offer = await _offerQueries.FindOfferWithStatusById(quantityTableSearchCretriaModel.OfferId);
            var quantityItems = await _offerQueries.GetSupplierQTableItemsForChecking(quantityTableSearchCretriaModel);

            QuantitiesTemplateModel lst = await _offerQueries.GetOfferQuantityItems(quantityTableSearchCretriaModel.OfferId, quantityTableSearchCretriaModel.QuantityTableId);
            lst.QuantitiesItems = quantityItems.Items.ToList();
            if (lst == null)
                lst = new QuantitiesTemplateModel();
            lst.grid = new List<string>();
            lst.ActivityTemplates = new List<int> { (int)TenderActivityTamplate.OldSystemAndVRO };
            foreach (var item in lst.ActivityTemplates)
            {
                FormConfigurationDTO DTOModel = new FormConfigurationDTO()
                {
                    SubmitAction = "/Offer/SaveVROCheckingQuantityItem",
                    TenderId = quantityTableSearchCretriaModel.TenderId,
                    HasAlternative = offer.Tender.HasAlternativeOffer ?? false,
                    ApplySelected = offer.Tender.HasAlternativeOffer ?? false,
                    CanEditAlternative = ((_httpContextAccessor.HttpContext.User.IsInRole(RoleNames.OffersOpeningAndCheckSecretary)
                    && (offer.Tender.TenderStatusId == (int)Enums.TenderStatus.VROFinancialCheckingOpening || offer.Tender.TenderStatusId == (int)Enums.TenderStatus.VROOffersFinancialChecking))) ? true : false,
                    AllowEdit = false,
                    ActivityId = 1,
                    EncryptedOfferId = Util.Encrypt(quantityTableSearchCretriaModel.OfferId),
                    EncryptedTenderId = Util.Encrypt(quantityTableSearchCretriaModel.TenderId),
                    QuantityItemDtos = lst.QuantitiesItems.ToList(),
                    YearsCount = (offer.Tender.TemplateYears ?? 0)
                };
                ApiResponse<List<HtmlTemplateDto>> resultList = new ApiResponse<List<HtmlTemplateDto>>();
                ApiResponse<HtmlTemplateDto> resultItem = new ApiResponse<HtmlTemplateDto>();

                if (_httpContextAccessor.HttpContext.User.IsInRole(RoleNames.OffersOpeningAndCheckSecretary)
                    && (offer.Tender.TenderStatusId == (int)Enums.TenderStatus.VROOffersFinancialChecking || offer.Tender.TenderStatusId == (int)Enums.TenderStatus.VROFinancialCheckingOpening))
                {
                    resultList = resultList = await _qantityTemplatesProxy.GetMonafasatSupplierForChecking(DTOModel);
                }
                else
                {
                    resultList = await _qantityTemplatesProxy.GenerateSupplierReadOnlyTemplate(DTOModel);
                }

                HTMLObject obje = new HTMLObject();
                ApiResponse<List<HtmlTemplateDto>> obj = new ApiResponse<List<HtmlTemplateDto>> { Data = resultList.Data };
                if (obj.Data != null && obj.Data.Count > 0 && obj.Data[0] != null)
                {
                    lst.grid.AddRange(obj.Data.Select(a => a.FormHtml).ToList());
                    obje.grid.AddRange(obj.Data.Select(a => a.FormHtml).ToList());
                    lst.grids.AddRange(obj.Data.GroupBy(o => new { o.FormId, o.FormName, o.TemplateName, o.FormExcellTemplate }).Select(o => new HTMLObject
                    {
                        FormId = o.Key.FormId,
                        FormName = o.Key.FormName,
                        TemplateName = o.Key.TemplateName,
                        FormExcellTemplate = _configuration.APIConfiguration.QuantityTemplates + "/api/QuantityTable/" + o.Key.FormExcellTemplate,
                        Javascript = o.FirstOrDefault().JsScript,
                        gridTables = o.Select(u => new TableModel { TableHtml = u.FormHtml, TableId = u.TableId, TableName = !string.IsNullOrEmpty(u.TableName) ? u.TableName : "اسم الجدول", FormId = u.FormId, DeleteFormHtml = u.DeleteFormHtml, EditFormHtml = u.EditFormHtml, Javascript = u.JsScript }).ToList()
                    }).ToList());
                }
            }
            lst.IsReadOnly = quantityTableSearchCretriaModel.IsReadOnly;
            return new QueryResult<TableModel>(lst.grids[0].gridTables.ToList(), quantityItems.TotalCount, quantityTableSearchCretriaModel.PageNumber, quantityTableSearchCretriaModel.PageSize * cellsCount);
        }

        public async Task<string> ValidateandSaveVROCheckingQuantitiesTable(Dictionary<string, string> AuthorList)
        {
            int tenderId = Util.Decrypt(AuthorList["EncryptedTenderId"]);
            int offerId = Util.Decrypt(AuthorList["EncryptedOfferId"]);
            int formId = int.Parse(AuthorList["FormId"]);
            int tableId = int.Parse(AuthorList["TableId"]);
            var offer = await _offerQueries.FindOfferWithStatusById(offerId);
            var Qitems = await _offerQueries.GetSupplierQTableItemsByTableId(offerId, tableId);

            foreach (KeyValuePair<string, string> entry in AuthorList)
            {
                if (!entry.Key.Contains("IsDefault"))
                    continue;

                var _itemNumber = int.Parse(entry.Key.Split('_')[2]);
                var SQTItem = Qitems.Where(d => d.ItemNumber == _itemNumber).Select(d => d.Id).ToList();
                bool isDefualt = bool.Parse(entry.Value);
                Dictionary<long, bool> keyValues = SQTItem.Select(x => new KeyValuePair<long, bool>(x, isDefualt)).ToDictionary(x => x.Key, x => x.Value);
                offer.UpdateOfferSupplierQItemsDefault(keyValues, tableId);
            }

            FormConfigurationDTO DTOModel = new FormConfigurationDTO()
            {
                SubmitAction = "/Offer/SaveCheckingQuantityItem",
                TenderId = offer.TenderId,
                HasAlternative = offer.Tender.HasAlternativeOffer ?? false,
                CanEditAlternative = offer.Tender.HasAlternativeOffer ?? false,
                AllowEdit = true,
                ActivityId = 1,
                EncryptedOfferId = Util.Encrypt(offer.OfferId),
                EncryptedTenderId = Util.Encrypt(offer.TenderId),
                QuantityItemDtos = offer.SupplierTenderQuantityTables.SelectMany(q => q.QuantitiyItemsJson.SupplierTenderQuantityTableItems).Select(qq => new TenderQuantityItemDTO
                {
                    ColumnId = qq.ColumnId,
                    ItemNumber = qq.ItemNumber,
                    ColumnName = "",
                    TableName = offer.SupplierTenderQuantityTables.FirstOrDefault(a => a.IsActive == true && a.QuantitiyItemsJson != null && a.QuantitiyItemsJson.SupplierTenderQuantityTableItems.Any(i => i.Id == qq.Id)).Name,
                    TableId = offer.SupplierTenderQuantityTables.FirstOrDefault(a => a.IsActive == true && a.QuantitiyItemsJson != null && a.QuantitiyItemsJson.SupplierTenderQuantityTableItems.Any(i => i.Id == qq.Id)).Id,
                    TemplateId = qq.ActivityTemplateId,
                    TenderId = offer.TenderId,
                    TenderFormHeaderId = qq.TenderFormHeaderId,
                    Value = qq.Value,
                    AlternativeValue = qq.AlternativeValue,
                    IsDefault = qq.IsDefault,
                    Id = qq.Id
                }).ToList(),
                YearsCount = (offer.Tender.TemplateYears ?? 0)
            };
            ApiResponse<List<TableTemplateDTO>> obj;
            obj = await _qantityTemplatesProxy.ValidateCheckingData(DTOModel);
            if (obj.StatusCode == 200)
            {
                var items = obj.Data.FirstOrDefault().QuantityItemDtos.Select(item => new SupplierTenderQuantityTableItem()
                {
                    ColumnId = item.ColumnId,
                    Id = item.Id,
                    TenderFormHeaderId = item.TenderFormHeaderId,
                    ActivityTemplateId = item.TemplateId,
                    Value = item.Value,
                    AlternativeValue = item.AlternativeValue,
                    IsDefault = item.IsDefault,
                    ItemNumber = item.ItemNumber
                }).ToList();
                await CalcFinalPrice(offer);
                await _offerCommands.UpdateAsync(offer);
                await _offerCommands.SaveAsync();
            }
            return obj.Data.FirstOrDefault().FormHtml;
        }

        public async Task<SupplierCombinedDetail> SaveCombinedTechnicalDetailsVRO(OfferDetailModel m)
        {
            bool isBankGuaranteeAttached = false;
            Offer offer = await _offerQueries.GetOfferById(Util.Decrypt(m.OfferIdString));
            SupplierCombinedDetail details = await _offerQueries.GetCombinedOfferDetail(Util.Decrypt(m.CombinedIdString));
            if (details == null)
            {
                details = new SupplierCombinedDetail();
            }
            List<SupplierBankGuaranteeDetail> GuaranteesLst = new List<SupplierBankGuaranteeDetail>();
            List<SupplierSpecificationDetail> SpecificationsLst = new List<SupplierSpecificationDetail>();

            foreach (var item in m.BankGuaranteeFiles)
            {
                var BankGuarantee = new SupplierBankGuaranteeDetail(item.BankGuaranteeId, offer.OfferId, item.IsBankGuaranteeValid, item.GuaranteeNumber, item.BankId.Value, item.Amount, item.GuaranteeStartDate, item.GuaranteeEndDate, item.GuaranteeDays);
                GuaranteesLst.Add(BankGuarantee);
            }
            isBankGuaranteeAttached = true;

            offer.UpdateOfferFinantialCheckingDetails(m.IsFinaintialOfferLetterAttached, m.FinantialOfferLetterNumber, m.FinantialOfferLetterDate, m.IsFinantialOfferLetterCopyAttached, isBankGuaranteeAttached);

            foreach (var item in m.SpecificationsFiles)
            {
                var Specification = new SupplierSpecificationDetail(item.SpecificationId, Util.Decrypt(m.CombinedIdString), item.IsForApplier, item.Degree, item.ConstructionWorkId, item.MaintenanceRunningWorkId);
                SpecificationsLst.Add(Specification);
            }
            if (m.CombinedOwner == true)
            {
                offer.UpdateOfferAttachments(m.IsOfferCopyAttached, m.IsOfferLetterAttached, m.OfferLetterNumber, m.OfferLetterDate, m.IsVisitationAttached, m.IsPurchaseBillAttached, m.IsBankGuaranteeAttached);
                offer.UpdateOfferFinancialAttachments(m.IsFinaintialOfferLetterAttached, m.IsFinantialOfferLetterCopyAttached);
                offer.UpdateBankGurnteesDetails(GuaranteesLst);
            }
            details.UpdateAttachmentDetails(details.CombinedDetailId, Util.Decrypt(m.CombinedIdString), m.IsChamberJoiningAttached, m.IsChamberJoiningValid,
                m.IsCommercialRegisterAttached, m.IsCommercialRegisterValid, m.IsSaudizationAttached, m.IsSaudizationValidDate, m.IsSocialInsuranceAttached,
                m.IsSocialInsuranceValidDate, m.IsZakatAttached, m.IsZakatValidDate, m.IsSpecificationAttached, m.IsSpecificationValidDate);
            details.updateAttachmentsList(SpecificationsLst);

            offer = await _offerCommands.UpdateAsync(offer);
            details = await _offerCommands.UpdateCombinedDetailAsync(details);
            return details;
        }

        #endregion Offer checking QTServices


        private async Task CalcFinalPrice(Offer offer)
        {
            FormConfigurationDTO DTOModel = new FormConfigurationDTO()
            {
                SubmitAction = "",
                TenderId = offer.TenderId,
                HasAlternative = offer.Tender.HasAlternativeOffer ?? false,
                CanEditAlternative = offer.Tender.HasAlternativeOffer ?? false,
                AllowEdit = false,
                ActivityId = 1,
                ApplySelected = offer.Tender.HasAlternativeOffer ?? false,
                EncryptedOfferId = Util.Encrypt(offer.OfferId),
                EncryptedTenderId = Util.Encrypt(offer.TenderId),
                QuantityItemDtos = offer.SupplierTenderQuantityTables.SelectMany(q => q.QuantitiyItemsJson.SupplierTenderQuantityTableItems).Select(qq => new TenderQuantityItemDTO
                {
                    ColumnId = qq.ColumnId,
                    ItemNumber = qq.ItemNumber,
                    ColumnName = "",
                    TableName = offer.SupplierTenderQuantityTables.FirstOrDefault(a => a.IsActive == true && a.QuantitiyItemsJson != null && a.QuantitiyItemsJson.SupplierTenderQuantityTableItems.Any(i => i.Id == qq.Id)).Name,
                    TableId = offer.SupplierTenderQuantityTables.FirstOrDefault(a => a.IsActive == true && a.QuantitiyItemsJson != null && a.QuantitiyItemsJson.SupplierTenderQuantityTableItems.Any(i => i.Id == qq.Id)).Id,
                    TemplateId = qq.ActivityTemplateId,
                    TenderId = offer.TenderId,
                    TenderFormHeaderId = qq.TenderFormHeaderId,
                    Value = qq.Value,
                    AlternativeValue = qq.AlternativeValue,
                    IsDefault = qq.IsDefault,
                    Id = qq.Id
                }).ToList(),
                YearsCount = (offer.Tender.TemplateYears ?? 0)
            };
            ApiResponse<TotalCostDTO> obj;

            obj = await _qantityTemplatesProxy.GetTotalCostForChecking(DTOModel);
            if (obj.StatusCode == 200)
            {
                offer.SetFinalPrice(obj.Data.TotalCostWithdiscount);
            }
        }
    }

}

