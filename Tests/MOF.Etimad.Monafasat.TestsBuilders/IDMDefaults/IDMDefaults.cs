using MOF.Etimad.Monafasat.Core;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Integration;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.ViewModel;
using System;
using System.Collections.Generic;

namespace MOF.Etimad.Monafasat.TestsBuilders.IDMDefaults
{
    public class IDMDefaults
    {
        public readonly string _Cr = "1299659801";
        public QueryResult<SupplierIntegrationModel> GetSupplierIntegrationModelQueryResult()
        {
            SupplierIntegrationModel supplierIntegrationModel = new SupplierIntegrationModel();
            supplierIntegrationModel.commercialRegistrationID = 1;
            supplierIntegrationModel.supplierNumber = _Cr;
            supplierIntegrationModel.supplierName = "dd";
            supplierIntegrationModel.activities = "activities";
            supplierIntegrationModel.cityName = "Riyadh";
            supplierIntegrationModel.postalCode = "11111";
            supplierIntegrationModel.postOfficeNumber = "1";
            supplierIntegrationModel.mobile = "1111111111";
            supplierIntegrationModel.phoneNumber = "1111111111";
            supplierIntegrationModel.faxNumber = "1111111111";
            supplierIntegrationModel.Email = "a@a.a";
            supplierIntegrationModel.IsOldBlock = false;
            supplierIntegrationModel.contactOfficers = new List<ContactOfficerModel>();
            supplierIntegrationModel.IsFav = false;
            List<SupplierIntegrationModel> supplierIntegrationModels = new List<SupplierIntegrationModel>();
            supplierIntegrationModels.Add(supplierIntegrationModel);
            QueryResult<SupplierIntegrationModel> supplierIntegrationQueryResult =  new QueryResult<SupplierIntegrationModel>(supplierIntegrationModels, supplierIntegrationModels.Count, 1, 6);
            return supplierIntegrationQueryResult;
        }

        public QueryResult<SupplierInvitationModel> GetSupplierInvitationModelQueryResults()
        {
            SupplierInvitationModel supplierInvitationModel = new SupplierInvitationModel()
            {
                SupplierId = 1,
                SupplierName = "sds",
                CommericalRegisterName = "sds",
                CommericalRegisterNo = _Cr,
            };
            List<SupplierInvitationModel> supplierInvitationModels = new List<SupplierInvitationModel>();
            supplierInvitationModels.Add(supplierInvitationModel);
            QueryResult<SupplierInvitationModel> queryResult = new QueryResult<SupplierInvitationModel>(supplierInvitationModels, supplierInvitationModels.Count, 1, 6);
            return queryResult;
        }

        public QueryResult<UnRegisterSupplierInvitationModel> UnRegisterSupplierInvitationModelQueryResults()
        {
            UnRegisterSupplierInvitationModel supplierInvitationModel = new UnRegisterSupplierInvitationModel()
            {
                InvitationStatusName = "status",
                SupplierEmailOrMobileNo = "1111111111"
            };
            List<UnRegisterSupplierInvitationModel> supplierInvitationModels = new List<UnRegisterSupplierInvitationModel>();
            supplierInvitationModels.Add(supplierInvitationModel);
            QueryResult<UnRegisterSupplierInvitationModel> queryResult = new QueryResult<UnRegisterSupplierInvitationModel>(supplierInvitationModels, supplierInvitationModels.Count, 1, 6);
            return queryResult;
        }

        public QueryResult<InvitationCrModel> InvitationCrModelsQueryResults()
        {
            InvitationCrModel invitationModel = new InvitationCrModel()
            {
                CrNumber = _Cr,
                CrName = "ss",
                TenderId = 1,
                Email = "a@a.a",
                Mobile = "1111111111"
            };
            List<InvitationCrModel> invitationModels = new List<InvitationCrModel>();
            invitationModels.Add(invitationModel);
            QueryResult<InvitationCrModel> queryResult = new QueryResult<InvitationCrModel>(invitationModels, invitationModels.Count, 1, 6);
            return queryResult;
        }


        

        public Supplier GetSupplierData()
        {
            List<UserNotificationSetting> userNotificationSettings = new List<UserNotificationSetting>();
            UserNotificationSetting userNotificationSetting = new UserNotificationSetting("opcode", _Cr, 1,true,true,1);
            userNotificationSetting.SetIdForTest(100);
            userNotificationSetting.setNotificationOperationCodeForTest();
            userNotificationSettings.Add(userNotificationSetting);
            Supplier supplier = new Supplier(_Cr, _Cr, userNotificationSettings);
            supplier.UpdateNotificationSetting(100,true,true);
            return supplier;
        }

        public SupplierUserProfile GetSupplierUserProfileData()
        {
            SupplierUserProfile supplier = new SupplierUserProfile(_Cr, 1);
            return supplier;
        }

        public FavouriteSupplierList GetFavouriteSupplierList()
        {
            return new FavouriteSupplierList() {
            Name="name"
            };
        }

        public NitaqatInformationResponseModel GetNitaqatInformationResponseModel() {

            return new NitaqatInformationResponseModel() { 
            EstablishmentName= "EstablishmentName",
            SaudiPercentage="1",
            EntityColor=new EntityColorModel() { EntityColorName="colorname"}
            };
        }


        public LicenseDetailsResponseModel GetLicenseDetailsResponseData(bool _hasLicense,string licenseStatus)
        {

            return new LicenseDetailsResponseModel()
            {
                hasLicense = _hasLicense,
                LicenseInfo = new LicenseInfoModel() { LicenseStatus = licenseStatus, ExpiryDt = DateTime.Now },
            };
        }


        public ContractorDetailsResponseModel GetContractorDetailsResponseData()
        {

            return new ContractorDetailsResponseModel()
            {
                ContractorInfo=new ContractorInfoModel() {ClassificationDtHjr="1442-09-09" }
            };
        }
        
        public COCSubscriptionInquiryModel GetCOCSubscriptionInquiryData(int days)
        {

            return new COCSubscriptionInquiryModel()
            {
                MembershipSijil = new MembershipSijil(DateTime.Now,"","","",DateTime.Now,"") {SijilToDate= DateTime.Now.AddDays(days),SijilToDateHjr="1442-09-09" }
            };
        }

        public ZakatCertificateInquiryResponseModel GetZakatCertificateInquiryResponseData(int days)
        {

            return new ZakatCertificateInquiryResponseModel()
            {
                ZakatCertificate = new ZakatCertificateModel() { ExpiryDate = DateTime.Now.AddDays(days), IssueDateHjri = "1442-09-09" }
            };
        }


        public GOSICertificateInquiryResponseModel GetGOSICertificateInquiryResponseModel()
        {
            List<CompanyInformationModel> companyInformationModels = new List<CompanyInformationModel>();
            CompanyInformationModel company = new CompanyInformationModel() { BusinessNameAr = "nameAr", BusinessNameEn = "nameen",GOSIRegistrationId="1" };
            companyInformationModels.Add(company);
            return new GOSICertificateInquiryResponseModel()
            {
                CompanyInformationList = companyInformationModels
            };
        }
    }
}
