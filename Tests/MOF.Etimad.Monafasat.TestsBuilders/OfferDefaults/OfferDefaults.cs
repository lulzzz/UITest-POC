using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.TestsBuilders.TenderDefaults;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.ViewModel.Offer.OpenOfferStage;
using MOF.Etimad.Monafasat.ViewModel.QuantityTableTemplates;
using System;
using System.Collections.Generic;

namespace MOF.Etimad.Monafasat.TestsBuilders.OfferDefaults
{
    public class OfferDefaults
    {
        public Offer GetOfferDefaults()
        {
            Offer offer = new Offer(1, "1299659801", null, 1, false);
            offer.Supplier = new Core.Supplier("1299659801", "CR Name1", null);
            return offer;
        }

        public Offer GetOfferDefaultsWithQt()
        {
            Offer offer = new Offer(1, "1299659801", GetSupplierTenderQuantityTables(), 1, false);
            offer.Tender = new TenderDefault().GetGeneralTender();
            offer.Tender.UpdateTenderStatus(Enums.TenderStatus.OffersOppenedConfirmed);
            offer.Tender.UpdateTenderDates(DateTime.Now.Date.AddDays(1), DateTime.Now.Date.AddDays(2), DateTime.Now.Date.AddDays(3), DateTime.Now.Date.AddDays(-1), null, null, false, null, 1, "building name", "Floar number", "Department Number", null);
            offer.AddRegisteredCombinedSupplier(new OfferSolidarity("1299659801", Enums.SupplierSolidarityStatus.Approved, Enums.UnRegisteredSuppliersInvitationType.SolidarityLeader));
            offer.AddAttachment(new SupplierOriginalAttachment("name", "1"));
            offer.UpdateTechnicianReportAttachments(new List<TechnicianReportAttachment>() { new TechnicianReportAttachment("name", "1", (int)Enums.AttachmentType.TechnicianReport) });
            return offer;
        }

        public OfferSolidarity GetOfferSolidarityLeader()
        {
            OfferSolidarity offerSolidarity =  new OfferSolidarity("1299659801", Enums.SupplierSolidarityStatus.Approved, Enums.UnRegisteredSuppliersInvitationType.SolidarityLeader);
            offerSolidarity.LinkOfferForTest(GetOfferDefaults());
            offerSolidarity.Offer.Tender = new TenderDefault().GetGeneralTender();
            return offerSolidarity;
        }

        public Offer GetOfferDefaultsWithOfferId()
        {
            Offer offer = new Offer(1, "1299659801", null, 1, false);
            offer.SaveOfferAwardingValues(1, 50, 50, "");
            return offer;
        }
        public Offer GetOfferDefaultsByOfferId(int offerId)
        {
            Offer offer = new Offer(1, "1299659801", null, 1, false);
            offer.SaveOfferAwardingValues(offerId, 50,null, "");
            return offer;
        }
        public List<Offer> GetOfferList()
        {
            List<Offer> offers = new List<Offer>();
            Offer offer1 = new Offer(1, "1299659801", null, 1, false);
            Offer offer2 = new Offer(1, "1299659100", null, 1, false);
            offer1.SaveOfferAwardingValues(1, 1000, null, null);
            offer1.Supplier = new Core.Supplier("1299659801", "CR Name1", null);
            offer1.AddActionToOfferHistory((int)Enums.TenderStatus.OffersAwardedConfirmed, (int)Enums.OfferStatus.Received, Enums.TenderActions.ApproveAwarding, 111, "1299659801");
            offer1.AddActionToOfferHistory((int)Enums.TenderStatus.OffersAwardedConfirmed, (int)Enums.OfferStatus.Received, Enums.TenderActions.ApproveAwarding, 111, "1299659100");
            offer2.Supplier = new Core.Supplier("1299659100", "CR Name2", null);
            offers.Add(offer1);
            offers.Add(offer2);
            offer1.SetOfferLocalContentDetails();
            return offers;
        } 
        public List<Offer> GetFailedFinantialOfferList()
        {
            List<Offer> offers = new List<Offer>();
            Offer offer1 = new Offer(1, "2222", null, 1, false);
            offer1.Supplier = new Core.Supplier("111", "CR1", null);
            offer1.SetOfferLocalContentDetails();
            offers.Add(offer1);

            return offers;
        }      
        public List<Offer> GetFaildTechnicalOfferList()
        {
            List<Offer> offers = new List<Offer>();
            Offer offer1 = new Offer(1, "111", null, 1, false);
            offer1.SetOfferLocalContentDetails();
            offers.Add(offer1);
            return offers;
        }

        public OfferAttachmentsModel GetOfferAttachmentsModelDefaults()
        {
            var _supplierBankGuaranteeModel = new List<SupplierBankGuaranteeModel>() {
                GetSupplierBankGuaranteeModelDefaults()
            };
            var _specificationsFiles = new List<SupplierSpecificationModel>()
            {
                GetSupplierSpecificationModelDefaults()
            };
            return new OfferAttachmentsModel()
            {
                BankGuaranteeFiles = _supplierBankGuaranteeModel,
                SpecificationsFiles = _specificationsFiles,
                OfferIDString = Util.Encrypt(1)
            };
        }

        public SupplierAttachmentPartialVModel GetSupplierAttachmentPartialVModel()
        {
            return new SupplierAttachmentPartialVModel()
            {
                supplierAttachmentModels = new List<SupplierAttachmentModel>(),
                statusModel = new ViewModel.Offer.OfferStatusModel()
            };
        }

        public SupplierOriginalAttachment GetSupplierOriginalAttachment()
        {
            return new SupplierOriginalAttachment(1, 1,"name", "1");
        }

        public SupplierBankGuaranteeModel GetSupplierBankGuaranteeModelDefaults()
        {
            return new SupplierBankGuaranteeModel()
            {
                BankGuaranteeId = 1,
                IsBankGuaranteeValid = true,
                GuaranteeNumber = "100",
                BankId = 18,
                Amount = 200,
                GuaranteeStartDate = DateTime.Now,
                GuaranteeEndDate = DateTime.Now.AddDays(10),
                GuaranteeDays = 10,
                OfferId = 1
            };
        }
        public SupplierSpecificationModel GetSupplierSpecificationModelDefaults()
        {
            return new SupplierSpecificationModel()
            {
                IsForApplier = true,
                SpecificationId = 1,
                Degree = 3,
                ConstructionWork = 3,
                ConstructionWorkId = 3,
                MaintenanceRunningWorkId = 3,
                OfferId = 1,

            };
        }
        public OfferDetailModel GetOfferDetailModelDefaults()
        {
            var _supplierBankGuaranteeModel = new List<SupplierBankGuaranteeModel>() {
                GetSupplierBankGuaranteeModelDefaults()
            };
            var _specificationsFiles = new List<SupplierSpecificationModel>()
            {
                GetSupplierSpecificationModelDefaults()
            };

            return new OfferDetailModel()
            {
                OfferId = 1,
                TenderID = 1,
                TenderStatusId = 4,
                CombinedIdString = Util.Encrypt(7),
                IsChamberJoiningAttached = true,
                IsChamberJoiningValid = true,
                IsCommercialRegisterAttached = true,
                IsCommercialRegisterValid = true,
                IsOfferCopyAttached = true,
                IsOfferLetterAttached = true,
                IsPurchaseBillAttached = true,
                IsSaudizationAttached = true,
                IsSaudizationValidDate = true,
                IsSocialInsuranceValidDate = true,
                IsVisitationAttached = true,
                IsZakatAttached = true,
                IsZakatValidDate = true,
                IsBankGuaranteeAttached = true,
                IsSpecificationAttached = true,
                IsSpecificationValidDate = true,
                isOldFlow = true,
                BankGuaranteeFiles = _supplierBankGuaranteeModel,
                SpecificationsFiles = _specificationsFiles
            };
        }

        public OfferDetailsForCheckingModel GetOfferDetailsForCheckingModel()
        {
            var _supplierBankGuaranteeModel = new List<SupplierBankGuaranteeModel>() {
                GetSupplierBankGuaranteeModelDefaults()
            };
            var _specificationsFiles = new List<SupplierSpecificationModel>()
            {
                GetSupplierSpecificationModelDefaults()
            };

            return new OfferDetailsForCheckingModel()
            {
                OfferId = 1,
                TenderID = 1,
                TenderStatusId = 4,
                CombinedIdString = Util.Encrypt(7),
                IsChamberJoiningAttached = true,
                IsChamberJoiningValid = true,
                IsCommercialRegisterAttached = true,
                IsCommercialRegisterValid = true,
                IsOfferCopyAttached = true,
                IsOfferLetterAttached = true,
                IsPurchaseBillAttached = true,
                IsSaudizationAttached = true,
                IsSaudizationValidDate = true,
                IsSocialInsuranceValidDate = true,
                IsVisitationAttached = true,
                IsZakatAttached = true,
                IsZakatValidDate = true,
                IsBankGuaranteeAttached = true,
                IsSpecificationAttached = true,
                IsSpecificationValidDate = true,
                isOldFlow = true,
                BankGuaranteeFiles = _supplierBankGuaranteeModel,
                SpecificationsFiles = _specificationsFiles
            };
        }

        public DiscountNotesModel GetDiscountNotesModelDefaults()
        {
            return new DiscountNotesModel() { Discount = "5", DiscountNotes = "D-Notes", OfferIdString = Util.Encrypt(1) };
        }

        public OfferModel GetOfferModel()
        {
            return new OfferModel()
            {
                OfferId = 1,
                OfferPresentationWayId = (int)Enums.OfferPresentationWayId.OneFile,
                TenderStatusId = (int)Enums.TenderStatus.OffersOppening,
                CommericalRegisterNo = "1299659801"

            };
        }

        public List<SupplierTenderQuantityTable> GetSupplierTenderQuantityTables()
        {
            SupplierTenderQuantityTable supplierTenderQuantityTable = new SupplierTenderQuantityTable("table", 1, null);
            supplierTenderQuantityTable.QuantitiyItemsJson = new SupplierTenderQuantityTableItemJson();
            supplierTenderQuantityTable.UpadteSupplierQTableItems(new List<SupplierTenderQuantityTableItem>()
            {
                new SupplierTenderQuantityTableItem(1,1,1,1,"value", 1)
            });
            return new List<SupplierTenderQuantityTable>() { supplierTenderQuantityTable };

        }

        public ApiResponse<List<TemplateFormDTO>> EmptyTemplateFormDTOsApiResponse()
        {
            return new ApiResponse<List<TemplateFormDTO>>()
            {
                Data = new List<TemplateFormDTO>()
                {
                    new TemplateFormDTO()
                    {
                        FormDTOs = new List<FormDTO>()
                    }
                }
            };
        }

        public ApiResponse<List<HtmlTemplateDto>> HtmlTemplateDtoApiResponse()
        {
            return new ApiResponse<List<HtmlTemplateDto>>()
            {
                Data = new List<HtmlTemplateDto>()
                {
                    new HtmlTemplateDto()
                    {
                        TableId = "1",
                        FormHtml = "<body></body>",
                        FormId = 1,
                        FormName = "form",
                        TemplateName = "name",
                        FormExcellTemplate = "False",
                        JsScript = "<script></script>"
                    }
                }
            };
        }

        public ApiResponse<TotalCostDTO> TotalCostDTOApiResponse()
        {
            return new ApiResponse<TotalCostDTO>()
            {
                StatusCode = 200,
                Data = new TotalCostDTO()
                {
                    TotalCostWithdiscount = 5.00m
                }
            };
        }

        public QueryResult<TenderQuantityItemDTO> tenderQuantityItemDTOQueryResult()
        {
            List<TenderQuantityItemDTO> tenderQuantityItemDTOs = new List<TenderQuantityItemDTO>()
            {
                new TenderQuantityItemDTO()
                {
                    TableId = 1,
                    ItemNumber = 1
                }
            };
            return new QueryResult<TenderQuantityItemDTO>(tenderQuantityItemDTOs, tenderQuantityItemDTOs.Count, 1, 1);
        }

        public QueryResult<CombinedListModel> EmptyCombinedListModelsQueryResult()
        {
            List<CombinedListModel> CombinedListModels = new List<CombinedListModel>()
            {
                new CombinedListModel()
                {

                }
            };
            return new QueryResult<CombinedListModel>(CombinedListModels, CombinedListModels.Count, 1, 1);
        }

        public QueryResult<SolidarityInvitedRegisteredSupplierModel> GetSolidarityInvitedRegisteredSupplierModelQueryResult()
        {
            List<SolidarityInvitedRegisteredSupplierModel> lst = new List<SolidarityInvitedRegisteredSupplierModel>();
            return new QueryResult<SolidarityInvitedRegisteredSupplierModel>(lst, lst.Count, 1, 6);
        }

        public QueryResult<SolidarityInvitedUnRegisteredSupplierModel> GetSolidarityInvitedUnRegisteredSupplierModelQueryResult()
        {
            List<SolidarityInvitedUnRegisteredSupplierModel> lst = new List<SolidarityInvitedUnRegisteredSupplierModel>();
            return new QueryResult<SolidarityInvitedUnRegisteredSupplierModel>(lst, lst.Count, 1, 6);
        }

        public List<SupplierModel> GetSupplierModels()
        {
            return new List<SupplierModel>()
            {
                new SupplierModel()
            };
        }
    }
}