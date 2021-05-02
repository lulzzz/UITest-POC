using AutoMapper;
using MOF.Etimad.Monafasat.Core;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Data;
using MOF.Etimad.Monafasat.Integration;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernal.Exceptions;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.SharedKernel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly IYasserProxy _IYasserproxy;
        private readonly ISupplierCommands _supplierCommands;
        private readonly ISupplierQueries _supplierQueries;
        private readonly IIDMAppService _iidmService;
        private readonly IMapper _mapper;
        private readonly IBlockAppService _blockAppService;
        private readonly INotificationAppService _notificationAppService;


        public SupplierService(IAppDbContext context, IYasserProxy Iyesserproxy, ISupplierCommands supplierCommands, ISupplierQueries supplierQueries, IIDMAppService iidmService, IMapper mapper, IBlockAppService blockAppService, INotificationAppService notificationAppService)
        {
            _supplierCommands = supplierCommands;
            _supplierQueries = supplierQueries;
            _iidmService = iidmService;
            _mapper = mapper;
            _blockAppService = blockAppService;
            _IYasserproxy = Iyesserproxy;
            _notificationAppService = notificationAppService;
        }
        public async Task<QueryResult<SupplierCombinedModelView>> GetCombinedSuppliersByOfferId(CombinedSearchCretriaModel cretria)
        {
            var suppliers = await _supplierQueries.FindSuppliers(cretria);

            return suppliers;
        }
        public async Task<QueryResult<SupplierCombinedModelView>> GetAllSuppliersBySearchCretriaForCombined(SupplierSearchCretriaModel cretria, string CR)
        {

            var suppliers = await _supplierQueries.GetAllSuppliersBySearchCretriaForCombinedQuery(cretria, CR);
            return suppliers;
        }
        public async Task<QueryResult<InvitationCrModel>> GetInvitedSuppliers(SupplierSearchCretriaModel cretria)
        {
            var suppliers = await _supplierQueries.GetInvitedSuppliers(cretria);
            return suppliers;
        }
        public async Task<QueryResult<InvitationCrModel>> GetInvitedUnRegisterSuppliers(SupplierSearchCretriaModel cretria)
        {
            var suppliers = await _supplierQueries.GetInvitedUnRegisterSuppliers(cretria);

            return suppliers;
        }

        public async Task<QueryResult<InvitationCrModel>> GetInvitedUnRegisterSuppliersForCreation(SupplierSearchCretriaModel cretria)
        {
            var suppliers = await _supplierQueries.GetInvitedUnRegisterSuppliersForCreation(cretria);
            return suppliers;
        }

        public async Task<QueryResult<SolidarityInvitedRegisteredSupplierModel>> GetSolidarityInvitedSuppliers(SolidaritySearchCriteria cretria)
        {
            var suppliers = await _supplierQueries.GetSolidarityInvitedSuppliers(cretria);

            return suppliers;
        }
        public async Task<QueryResult<SolidarityInvitedUnRegisteredSupplierModel>> GetInvitedUnregisteredSuppliers(SolidarityUnregisteredSearchCriteria cretria)
        {
            var suppliers = await _supplierQueries.GetSolidarityInvitedUnregisteredSuppliers(cretria);

            return suppliers;
        }

        public async Task<QueryResult<SolidarityInvitedRegisteredSupplierModel>> GetAllSuppliersBySearchCretriaForSolidarity(SolidaritySearchCriteria cretria)
        {
            SupplierIntegrationSearchCriteria integrationSearchCriteria = _mapper.Map<SupplierIntegrationSearchCriteria>(cretria);
            QueryResult<SupplierIntegrationModel> suppliers = await _iidmService.GetSupplierDetailsBySearchCriteria(integrationSearchCriteria);
            List<SolidarityInvitedRegisteredSupplierModel> suppliersInvitation = suppliers.Items.Where(d => d.supplierNumber != cretria.CurrentSupplierCR).Select(supplier => new SolidarityInvitedRegisteredSupplierModel()
            {
                CrName = supplier.supplierName,
                CrNumber = supplier.supplierNumber,


            }).ToList();
            QueryResult<SolidarityInvitedRegisteredSupplierModel> suppliersList = suppliersInvitation != null ? new QueryResult<SolidarityInvitedRegisteredSupplierModel>(suppliersInvitation, suppliers.TotalCount, suppliers.PageNumber, suppliers.PageSize) : new QueryResult<SolidarityInvitedRegisteredSupplierModel>(new List<SolidarityInvitedRegisteredSupplierModel>(), 0, 1, 1);
            return suppliersList;
        }



        public async Task<QueryResult<InvitationCrModel>> GetAllSuppliersBySearchCretriaForInvitation(SupplierSearchCretriaModel cretria)
        {
            Tender tender = await _supplierQueries.GetTenderByTenderId(cretria.InvitationTenderId);
            QueryResult<SupplierIntegrationModel> suppliers = await _iidmService.GetSupplierDetailsBySearchCriteria(_mapper.Map<SupplierIntegrationSearchCriteria>(cretria));


            bool isBlocked = await _blockAppService.CheckifSupplierBlockedByCrNo(cretria.CommericalRegisterNo, cretria.AgencyCode);

            List<InvitationCrModel> suppliersInvitation = suppliers.Items.Select(supplier => new InvitationCrModel()
            {
                CrName = supplier.supplierName,
                CrNumber = supplier.supplierNumber,
                TenderId = tender.TenderId,
                IsBlocked = isBlocked,
                IsOwner = supplier.supplierNumber == tender.AgencyCode ? true : false
            }).ToList();

            QueryResult<InvitationCrModel> suppliersList = suppliersInvitation != null ? new QueryResult<InvitationCrModel>(suppliersInvitation, suppliers.TotalCount, suppliers.PageNumber, suppliers.PageSize) : new QueryResult<InvitationCrModel>(new List<InvitationCrModel>(), 0, 1, 1);
            await _supplierQueries.GetInvitatedSupplier(cretria, suppliersList.Items.ToList());

            return suppliersList;
        }
        public async Task<List<SupplierModel>> GetAllSuppliersData()
        {
            var suppliers = await _supplierQueries.GetAllSuppliersData();
            return suppliers;
        }

        public async Task<QueryResult<SupplierModel>> GetSuppliersBySearchCretria(SupplierSearchCretriaModel cretria)
        {
            var suppliers = await _iidmService.GetSupplierDetailsBySearchCriteria(_mapper.Map<SupplierIntegrationSearchCriteria>(cretria));
            return _mapper.Map<QueryResult<SupplierModel>>(suppliers);
        }
        public async Task<int> GetSuppliersCountFromIDM()
        {
            SupplierIntegrationSearchCriteria criteria = new SupplierIntegrationSearchCriteria
            {
                IsCountOnly = true
            };
            var suppliers = await _iidmService.GetSupplierDetailsBySearchCriteria(criteria);
            return suppliers.TotalCount;
        }
        public async Task<QueryResult<SupplierInvitationModel>> GetAllSuppliers(SupplierSearchCretriaModel cretria)
        {
            if (cretria.FavouriteSupplierListId != null && cretria.FavouriteSupplierListId > 0)
            {
                    var CrNumbers = await GetFavouriteSuppliersByListId(cretria);
                    if (CrNumbers.TotalCount > 0)
                        cretria.SupplierNumbers = CrNumbers.Items.Select(c => c.CommericalRegisterNo).ToList();
                    else
                    {
                        return _mapper.Map<QueryResult<SupplierInvitationModel>>(new QueryResult<SupplierIntegrationModel>(
                            new List<SupplierIntegrationModel>()
                            , 0, 0, 10));
                    }
            }
            var supplierIntegrationSearchCriteria = _mapper.Map<SupplierIntegrationSearchCriteria>(cretria);
            if (!string.IsNullOrEmpty(cretria.CommericalRegisterNo))
                supplierIntegrationSearchCriteria.SupplierNumbers.Add(cretria.CommericalRegisterNo);
            var suppliers = await _iidmService.GetSupplierDetailsBySearchCriteria(supplierIntegrationSearchCriteria);
            var supplierInvitationModelList = _mapper.Map<QueryResult<SupplierInvitationModel>>(suppliers);
            await _supplierQueries.SuppliersInFavourite(cretria, supplierInvitationModelList.Items.ToList());
            return supplierInvitationModelList;
        }

        public async Task<SupplierInvitationModel> GetSupplierByName(string CommericalRegisterNo)
        {
            var suppliers = await _iidmService.GetSupplierDetailsBySearchCriteria(new SupplierIntegrationSearchCriteria() { CrNumber = CommericalRegisterNo });
            if (suppliers == null)
            {
                return new SupplierInvitationModel();
            }
            return _mapper.Map<QueryResult<SupplierInvitationModel>>(suppliers).Items.FirstOrDefault();
        }


        #region FavouriteSuppliers

        public async Task<FavouriteSupplierListModel> AddFavouriteSupplierList(FavouriteSupplierListModel favouriteSupplierList)
        {
            var supplier = await _supplierQueries.FindFavouriteListByName(favouriteSupplierList.Name, favouriteSupplierList.BranchId);
            if (supplier != null)
            {
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.FavouriteListNameExist);
            }

            var FavouriteSupplierList = new FavouriteSupplierList(favouriteSupplierList.Name, favouriteSupplierList.AgencyCode, favouriteSupplierList.BranchId);
            var result = await _supplierCommands.CreateFavouriteSupplierListAsync(FavouriteSupplierList);
            favouriteSupplierList.FavouriteSupplierListId = result.FavouriteSupplierListId;
            return favouriteSupplierList;
        }

        public async Task<bool> DeleteFavouriteSupplierList(FavouriteSupplierListModel favouriteSupplierList)
        {
            Check.ArgumentNotNullOrEmpty(nameof(favouriteSupplierList.FavouriteSupplierListId), favouriteSupplierList.FavouriteSupplierListId.ToString());
            var result = await _supplierQueries.FindFavouriteList(favouriteSupplierList.FavouriteSupplierListId);

            if (result.AgencyCode != favouriteSupplierList.AgencyCode)
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.FavouriteListNotForAgency);

            if (result.BranchId != favouriteSupplierList.BranchId)
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.FavouriteListNotForBranch);

            result.DeActive();
            await _supplierCommands.UpdateFavouriteSupplierListAsync(result);
            return true;
        }

        public async Task<List<FavouriteSupplierListModel>> GetFavouriteListsByAgencyBranchId(string agencyCode, int BranchId)
        {
            var FavouriteSupplierLists = await _supplierQueries.GetFavouriteListsByAgencyBranchId(agencyCode, BranchId);
            return FavouriteSupplierLists;
        }

        public async Task<QueryResult<SupplierInvitationModel>> GetFavouriteSuppliersByListId(SupplierSearchCretriaModel cretria)
        {
            cretria.OnlyActive = true;
            var suppliers = await _supplierQueries.GetFavouriteSuppliersByListId(cretria);
            return suppliers;
        }

        public async Task<bool> AddSupplierToFavouriteList(SupplierSearchCretriaModel cretria)
        {
            Check.ArgumentNotNullOrEmpty(nameof(cretria.FavouriteSupplierListId), cretria.FavouriteSupplierListId.ToString());
            Check.ArgumentNotNullOrEmpty(nameof(cretria.CommericalRegisterNo), cretria.CommericalRegisterNo.ToString());

            var supplier = await _supplierQueries.FindSupplierByCRNumber(cretria.CommericalRegisterNo);
            var favouriteSuppliers = (await _supplierQueries.GetFavouriteSuppliersByListId(new SupplierSearchCretriaModel()
            {
                OnlyActive = false,
                FavouriteSupplierListId = cretria.FavouriteSupplierListId,
                PageSize = int.MaxValue,
                AgencyCode = cretria.AgencyCode
            })).Items;

            if (favouriteSuppliers.Any(s => s.CommericalRegisterNo == cretria.CommericalRegisterNo))
            {
                var favouriteSupplier = await _supplierQueries.FindFavouriteSupplierByListId(new SupplierSearchCretriaModel
                {
                    FavouriteSupplierListId = cretria.FavouriteSupplierListId,
                    CommericalRegisterNo = cretria.CommericalRegisterNo
                });

                if (favouriteSuppliers.Any(s => s.CommericalRegisterName == cretria.CommericalRegisterName && s.CommericalRegisterNo == cretria.CommericalRegisterNo) && favouriteSupplier.IsActive == true)
                    throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.FavouriteListHasSupplierExist);

                if (favouriteSupplier.FavouriteSupplierList.AgencyCode != cretria.AgencyCode)
                    throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.FavouriteListNotForAgency);

                if (favouriteSupplier.FavouriteSupplierList.BranchId != cretria.BranchId)
                    throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.FavouriteListNotForBranch);

                supplier.Update(cretria.CommericalRegisterNo, cretria.CommericalRegisterName);
                await _supplierCommands.UpdateSupplierAsync(supplier);
                favouriteSupplier.SetActive();
                await _supplierCommands.UpdateTenderFavouriteSupplierAsync(favouriteSupplier);

                return true;
            }
            if (supplier != null)
            {
                var result = new FavouriteSupplier(supplier.SelectedCr, cretria.FavouriteSupplierListId.Value);
                await _supplierCommands.CreateTenderFavouriteAsync(result);
                return true;
            }
            else
            {
                await _supplierCommands.CreateSupplierAsync(new Supplier(cretria.CommericalRegisterNo, cretria.CommericalRegisterName, await _notificationAppService.GetDefaultSettingByCr()));
                var result = new FavouriteSupplier(cretria.CommericalRegisterNo, cretria.FavouriteSupplierListId.Value);
                await _supplierCommands.CreateTenderFavouriteAsync(result);
                return true;
            }
        }

        public async Task<bool> DeleteSupplierFromFavouriteList(SupplierSearchCretriaModel cretria)
        {
            Check.ArgumentNotNullOrEmpty(nameof(cretria.FavouriteSupplierListId), cretria.FavouriteSupplierListId.ToString());
            Check.ArgumentNotNullOrEmpty(nameof(cretria.CommericalRegisterNo), cretria.CommericalRegisterNo.ToString());


            var favouriteSupplier = await _supplierQueries.FindFavouriteSupplierByListId(new SupplierSearchCretriaModel
            {
                FavouriteSupplierListId = cretria.FavouriteSupplierListId,
                CommericalRegisterNo = cretria.CommericalRegisterNo
            });




            favouriteSupplier.DeActive();



            await _supplierCommands.UpdateTenderFavouriteSupplierAsync(favouriteSupplier);

            return true;
        }

        #endregion


        #region [Supplier Info From Yesser]
        public async Task<SupplierInfoStatusModel> ValidateMCICRAndGetInfo(string CR)
        {
            var res = new SupplierInfoStatusModel();
            var parameter = new MCICRInfoModelRequest { CommercialRegistrationNumber = CR };
            var result = await _IYasserproxy.ValidateMCICRAndGetInfo(parameter);
            if (result == null)
            {
                res.enSupplierInfoType = Enums.SupplierInfoType.Warnning;
                res.StatusName = Resources.TenderResources.ErrorMessages.CantFindRecord;
            }
            else if (result.ResponseCode == "E001199")
            {
                res.enSupplierInfoType = Enums.SupplierInfoType.Warnning;
                res.StatusName = Resources.SharedResources.ErrorMessages.CrExpired;
            }
            else
            {
                res.enSupplierInfoType = Enums.SupplierInfoType.Success;
                if (result.ExpiryDateHjri > DateTime.Now)
                {
                    res.StatusName = Resources.TenderResources.Messages.Valid;
                    res.Date = result.ExpiryDateHjri.ToLongDateString();
                }
                else
                {
                    res.enSupplierInfoType = Enums.SupplierInfoType.Wrong;
                    res.StatusName = Resources.TenderResources.Messages.Ended;
                }


            }

            return res;
        }

        public async Task<SupplierInfoStatusModel> GetCOCSubscriptionBySijilNumber(string LicenseNumber, string CityCode)
        {
            var parameter = new COCSubscriptionInquiryRequestModel { LicenseNumber = LicenseNumber, CityCode = CityCode };
            var result = await _IYasserproxy.GetCOCSubscriptionBySijilNumber(parameter);
            var res = new SupplierInfoStatusModel();
            if (result == null)
            {
                res.enSupplierInfoType = Enums.SupplierInfoType.Warnning;
                res.StatusName = Resources.TenderResources.ErrorMessages.CantFindRecord;
            }
            else
            {
                res.enSupplierInfoType = Enums.SupplierInfoType.Success;
                if (result.MembershipSijil.SijilToDate < DateTime.Now)
                {
                    res.StatusName = Resources.TenderResources.Messages.Valid;
                    res.Date = result.MembershipSijil.SijilToDateHjr;

                }
                else
                {

                    res.enSupplierInfoType = Enums.SupplierInfoType.Wrong;
                    res.StatusName = Resources.TenderResources.Messages.Ended;
                }


            }
            return res;
        }
        public async Task<SupplierInfoStatusModel> ZakatCertificateInquiryByCR(string CR)
        {
            var parameter = new ZakatCertificateInquiryRequestModel { CommercialRegistrationNumber = CR };
            var result = await _IYasserproxy.ZakatCertificateInquiryByCR(parameter);
            var res = new SupplierInfoStatusModel();
            if (result == null)
            {
                res.enSupplierInfoType = Enums.SupplierInfoType.Warnning;
                res.StatusName = Resources.TenderResources.ErrorMessages.CantFindRecord;
                res.Date = string.Empty;
            }
            else
            {
                res.enSupplierInfoType = Enums.SupplierInfoType.Success;
                if (result.ZakatCertificate.ExpiryDate < DateTime.Now)
                {
                    res.StatusName = Resources.TenderResources.Messages.Valid;
                    res.Date = result.ZakatCertificate.IssueDateHjri;
                }
                else
                {
                    res.enSupplierInfoType = Enums.SupplierInfoType.Wrong;
                    res.StatusName = Resources.TenderResources.Messages.Ended;
                    res.Date = string.Empty;
                }
            }
            return res;
        }

        public async Task<SupplierInfoStatusModel> ContractorDetailsInquiry(string partyNumberId)
        {
            var parameter = new ContractorDetailsRequestModel { PartyId = new PartyIdModel { PartyIdNumber = partyNumberId, PartyIdType = PartyType.NationalId } };
            var result = await _IYasserproxy.ContractorDetailsInquiry(parameter);
            var res = new SupplierInfoStatusModel();

            if (result == null)
            {
                res.enSupplierInfoType = Enums.SupplierInfoType.Warnning;
                res.StatusName = Resources.TenderResources.ErrorMessages.CantFindRecord;
            }
            else
            {
                var cultureInfo = new CultureInfo("ar-SA");
                cultureInfo.DateTimeFormat.Calendar = new UmAlQuraCalendar();
                res.enSupplierInfoType = Enums.SupplierInfoType.Success;
                DateTime date = DateTime.MinValue;
                if (date < DateTime.Now)
                {
                    res.StatusName = Resources.TenderResources.Messages.Valid;
                    res.Date = result.ContractorInfo.ClassificationDtHjr;

                }
                else
                {
                    res.enSupplierInfoType = Enums.SupplierInfoType.Wrong;

                    res.StatusName = Resources.TenderResources.Messages.Ended;
                }


            }
            return res;

        }
        public async Task<SupplierInfoStatusModel> LicenseStatusInquiry(string licenseNumber)
        {
            var parameter = new LicenseDetailsRequestModel { LicenseNumber = licenseNumber };
            var result = await _IYasserproxy.LicenseStatusInquiry(parameter);
            var res = new SupplierInfoStatusModel();

            if (result != null)
            {
                if (result.hasLicense)
                {
                    res.enSupplierInfoType = Enums.SupplierInfoType.Success;
                    if (result.LicenseInfo.LicenseStatus != "Invalid" && result.LicenseInfo.LicenseStatus != "Cancelled")
                    {
                        res.StatusName = Resources.TenderResources.Messages.Valid;
                        res.Date = result.LicenseInfo.ExpiryDt.ToLongDateString();
                    }
                    else
                    {
                        res.enSupplierInfoType = Enums.SupplierInfoType.Wrong;
                        res.StatusName = Resources.TenderResources.Messages.NotActive;
                        res.Date = result.LicenseInfo.ExpiryDt.ToLongDateString();
                    }
                }
                else
                {
                    res.enSupplierInfoType = Enums.SupplierInfoType.Wrong;
                    res.StatusName = Resources.TenderResources.Messages.recordNotFound;
                }

            }

            return res;
        }

        public async Task<SupplierInfoStatusModel> GOSICertificateInquiry(string GOSIId)
        {
            var parameter = new GOSICertificateInquiryRequestModel { GOSIId = GOSIId, GOSIIdType = GOSIIdType.GosiRegistrationID, GOSIOwnerPersonIdType = GOSIOwnerPersonIdType.NationalID };
            var result = await _IYasserproxy.GOSICertificateInquiry(parameter);
            var res = new SupplierInfoStatusModel();
            if (result == null)
            {
                res.enSupplierInfoType = Enums.SupplierInfoType.Warnning;
                res.StatusName = Resources.TenderResources.ErrorMessages.CantFindRecord;
            }
            else
            {
                res.enSupplierInfoType = Enums.SupplierInfoType.Success;
                res.StatusName = Resources.TenderResources.Messages.Valid;
                foreach (var company in result.CompanyInformationList)
                {
                    if (result.CompanyInformationList.IndexOf(company) == 0)
                        res.CompanyName = company.BusinessNameAr;
                    else
                        res.CompanyName = res.CompanyName + " / " + company.BusinessNameAr;
                }
            }

            return res;
        }


        public async Task<SupplierInfoStatusModel> NitaqatInformationInquiry(string EstablishmentLaborOfficeId, string EstablishmentSequenceNumber)
        {
            var parameter = new NitaqatInformationRequestModel { EstablishmentLaborOfficeId = EstablishmentLaborOfficeId, EstablishmentSequenceNumber = EstablishmentSequenceNumber };
            var result = await _IYasserproxy.NitaqatInformationInquiry(parameter);
            var res = new SupplierInfoStatusModel();

            if (result == null)
            {
                res.enSupplierInfoType = Enums.SupplierInfoType.Warnning;
                res.StatusName = Resources.TenderResources.ErrorMessages.CantFindRecord;
            }
            else
            {
                res.enSupplierInfoType = Enums.SupplierInfoType.Success;
                res.StatusName = Resources.TenderResources.Messages.Valid;
                res.CompanyName = result.EstablishmentName;
                res.Notes = result.EntityColor.EntityColorName;
                res.SaudiPercentage = result.SaudiPercentage;
            }
            return res;
        }

        #endregion

        #region Invitation 

        public async Task<QueryResult<InvitationCrModel>> GetInvitedSuppliersForInvitationInTenderCreation(SupplierSearchCretriaModel cretria)
        {
            var suppliers = await _supplierQueries.GetInvitedSuppliersForInvitationInTenderCreation(cretria);

            return suppliers;
        }
        public async Task<QueryResult<InvitationCrModel>> GetQualifiedSuppliers(TenderIdSearchCretriaModel cretria)
        {
            var suppliers = await _supplierQueries.GetQualifiedSuppliers(cretria);

            return suppliers;
        }
        public async Task<QueryResult<InvitationCrModel>> GetAcceptedSupplierInFirstStageTender(TenderIdSearchCretriaModel cretria)
        {
            var suppliers = await _supplierQueries.GetAcceptedSupplierInFirstStageTender(cretria);
            return suppliers;
        }
        public async Task<QueryResult<InvitationCrModel>> GetAnnouncementTemplateSuppliers(AnnouncementSupplierTemplateInvitationSearchmodel cretria)
        {
            var suppliers = await _supplierQueries.GetAnnouncementTemplateSuppliers(cretria);
            return suppliers;
        }
        public async Task<QueryResult<string>> GetEmailsForUnregisteredSuppliers(SupplierSearchCretriaModel cretria)
        {
            var suppliers = await _supplierQueries.GetEmailsForUnregisteredSuppliers(cretria);
            return suppliers;
        }
        public async Task<QueryResult<string>> GetMobileForUnregisteredSuppliers(SupplierSearchCretriaModel cretria)
        {
            var suppliers = await _supplierQueries.GetMobileForUnregisteredSuppliers(cretria);
            return suppliers;
        }
        public async Task<QueryResult<SupplierInvitationModel>> GetAllSuppliersBySearchCretriaForInvitations(SupplierSearchCretriaModel cretria)
        {
            var suppliers = await _supplierQueries.GetAllSuppliersBySearchCretriaForInvitations(cretria);
            return suppliers;
        }

        public async Task<QueryResult<UnRegisterSupplierInvitationModel>> GetInvitedUnregisteredSuppliersForInvitationAfterTenderApprovement(SupplierSearchCretriaModel cretria)
        {
            return await _supplierQueries.GetInvitedUnregisteredSuppliersForInvitationAfterTenderApprovement(cretria);
        }
        public async Task<QueryResult<SupplierInvitationModel>> GetInvitedSuppliersForInvitationAfterTenderApprovement(SupplierSearchCretriaModel cretria)
        {
            return await _supplierQueries.GetInvitedSuppliersForInvitationAfterTenderApprovement(cretria);
        }
        #endregion



    }
}
