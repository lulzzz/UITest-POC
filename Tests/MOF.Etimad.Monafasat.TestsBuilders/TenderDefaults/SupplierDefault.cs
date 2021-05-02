using MOF.Etimad.Monafasat.Core;
using MOF.Etimad.Monafasat.ViewModel;
using System.Collections.Generic;

namespace MOF.Etimad.Monafasat.TestsBuilders.TenderDefaults
{
    public class SupplierDefault
    {
        public Supplier GetSupplier()
        {
            return new Supplier("121212", "name 1", new List<Core.Entities.UserNotificationSetting>() { new Core.Entities.UserNotificationSetting() });
        }
        public SupplierFullDataViewModel BuildSupplierFullDataViewModelObject()
        {
            var _supplierFullDataViewModel = new SupplierFullDataViewModel()
            {
                supplierNumber = "1010000154",
                Addresses = new List<ViewModel.SupllierAddressesApiModel>()
                {
                    new ViewModel.SupllierAddressesApiModel(){
                        City = "جدة",
                        FaxNumber = "00966114444444",
                        PhoneNumber = "00966114444444",
                        PostOffice = "1234",
                        PostalCode = "433245",
                        IsPrimary = true
                    },
                    new ViewModel.SupllierAddressesApiModel(){
                        City = "1860134434",
                        FaxNumber = "00966113757758",
                        PhoneNumber = "00966113757758",
                        PostOffice = "321323",
                        PostalCode = "342234",
                        IsPrimary = true
                    },
                     new ViewModel.SupllierAddressesApiModel(){
                        City = "الرياض",
                        FaxNumber = "00966114444444",
                        PhoneNumber = "00966114444444",
                        PostOffice = "345",
                        PostalCode = "23462",
                        IsPrimary = false
                    }
                },
                Email = "h@h.com",
                IsicActivity = new List<ViewModel.IsicActivityApiModel>() {
                 new ViewModel.IsicActivityApiModel(){  IsicActivityID = 331509 , Description = "أعمال إصلاح و صيانة و إعادة بناء قاطرات وعربات السكك الحديدية"},
                 new ViewModel.IsicActivityApiModel(){ IsicActivityID = 531001 , Description = "أنشطة البريد العادي"}
                 },
                MainActivity = "البيع بالجملة والتجزئة لاطارات السيارات وتوابعها",
                MainActivityDescription = "test122",
                Mobile = "0559877246",
                SupplierType = 1,
                capital = "0",
                supplierName = "شركة خالد عبدالله الصافي",
                YasserInfo = new ViewModel.YasserApiModel()
                {
                    InvestmentLicenseNumber = "999",
                    MembershipCityCode = "101",
                    MembershipCityName = "الرياض",
                    OfficeFacilityNumberInMinistryOfLabor = "1",
                    SequenceFacilityNumberInMinistryOfLabor = "248621",
                    SocialSecuritySubscriptionNumber = "502612832"
                }
            };

            return _supplierFullDataViewModel;
        }
    }
}
