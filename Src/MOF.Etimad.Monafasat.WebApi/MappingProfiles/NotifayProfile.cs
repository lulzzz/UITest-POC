using AutoMapper;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static MOF.Etimad.Monafasat.SharedKernel.Enums;

namespace MOF.Etimad.Monafasat.WebApi.MappingProfiles
{
    public class NotifayProfile : Profile
    {
        public NotifayProfile()
        {
            CreateNotifayProfile();
        }

        private void CreateNotifayProfile()
        {

            CreateMap<UserProfile, UserProfileModel>()
                .ForMember(s => s.NotificationSetting, opt => opt.ResolveUsing<NotificationSettingModel>((src, dst, arg, context) =>
                                          GetNotificationSetting(src, context)))
                                          .ForMember(x => x.UserType, opt => opt.Ignore());

            CreateMap<BaseNotificationSetting, NotificationSettingModel>()
                .Include<SupplierNotificationSetting, NotificationSettingModel>()
                .Include<AuditerNotificationSetting, NotificationSettingModel>()
                .Include<DataEntryNotificationSetting, NotificationSettingModel>()
                .Include<OffersCheckManagerNotificationSetting, NotificationSettingModel>()
                .Include<OffersCheckSecretaryNotificationSetting, NotificationSettingModel>()
                .Include<OffersManagerNotificationSeting, NotificationSettingModel>()
                .Include<OffersOppeningManagerNotificationSetting, NotificationSettingModel>()
                .Include<OffersOppeningSecretaryNotificationSetting, NotificationSettingModel>();
            CreateMap<NotificationPanelModel, NotificationPanel>()
                .ForMember(x => x.NotifacationStatusEntity, opt => opt.Ignore())
               // .ForMember(x => x.NotifayType, opt => opt.Ignore())
                .ForMember(x => x.User, opt => opt.Ignore());
            CreateMap<NotificationPanel, NotificationPanelModel>()
                 .ForMember(x => x.NotifacationStatusEntity, opt => opt.Ignore())
                 .ForMember(x => x.NotifayType, opt => opt.Ignore())
                 .ForMember(x => x.User, opt => opt.Ignore());
            CreateMap<UserProfileModel, UserProfile>();
            CreateMap<NotificationSettingModel, SupplierNotificationSetting>();
            CreateMap<NotificationSettingModel, AuditerNotificationSetting>();
            CreateMap<NotificationSettingModel, DataEntryNotificationSetting>();
            CreateMap<NotificationSettingModel, OffersCheckManagerNotificationSetting>();
            CreateMap<NotificationSettingModel, OffersCheckSecretaryNotificationSetting>();
            CreateMap<NotificationSettingModel, OffersManagerNotificationSeting>();
            CreateMap<NotificationSettingModel, OffersOppeningManagerNotificationSetting>();
            CreateMap<NotificationSettingModel, OffersOppeningSecretaryNotificationSetting>();
            CreateMap<NotificationBy, NotificationByModel>();
            CreateMap<SupplierNotificationSetting, NotificationSettingModel>();
            CreateMap<AuditerNotificationSetting, NotificationSettingModel>();
            CreateMap<DataEntryNotificationSetting, NotificationSettingModel>();
            CreateMap<OffersCheckManagerNotificationSetting, NotificationSettingModel>();
            CreateMap<OffersCheckSecretaryNotificationSetting, NotificationSettingModel>();
            CreateMap<OffersManagerNotificationSeting, NotificationSettingModel>();
            CreateMap<OffersOppeningManagerNotificationSetting, NotificationSettingModel>();
            CreateMap<OffersOppeningSecretaryNotificationSetting, NotificationSettingModel>();
            CreateMap<BaseNotification, BasicNotificationModel>();

        }


        private NotificationSettingModel GetNotificationSetting(UserProfile src, ResolutionContext context)
        {
            if (!context.Options.Items.ContainsKey("SelectRoleName"))
                return null;

            var userType = (UserType)context.Options.Items["SelectRoleName"];

            var setting = src.NotificationSetting.FirstOrDefault(f => (f.GetType() == UserProfile.GetObjectType(userType)));

            if (setting == null) return null;
            // var xxxx = (NotificationBy)setting.GetType().GetProperty(nameof(NotificationSettingModel.InquiriesAboutTender)).GetValue(setting);


            return new NotificationSettingModel
            {
                Id = setting.Id,
                InquiriesAboutTender = setting.GetType().GetProperty(nameof(NotificationSettingModel.InquiriesAboutTender)) == null ? null : mapNotifayObject((NotificationBy)setting.GetType().GetProperty(nameof(NotificationSettingModel.InquiriesAboutTender)).GetValue(setting)),
                OfferStatus = setting.GetType().GetProperty(nameof(NotificationSettingModel.OfferStatus)) == null ? null : mapNotifayObject((NotificationBy)setting.GetType().GetProperty(nameof(NotificationSettingModel.OfferStatus)).GetValue(setting)),
                OperationsNeedToBeAccredited = setting.GetType().GetProperty(nameof(NotificationSettingModel.OperationsNeedToBeAccredited)) == null ? null : mapNotifayObject((NotificationBy)setting.GetType().GetProperty(nameof(NotificationSettingModel.OperationsNeedToBeAccredited)).GetValue(setting)),
                OperationsOnTheTender = setting.GetType().GetProperty(nameof(NotificationSettingModel.OperationsOnTheTender)) == null ? null : mapNotifayObject((NotificationBy)setting.GetType().GetProperty(nameof(NotificationSettingModel.OperationsOnTheTender)).GetValue(setting)),
                OperationsOnYourAccount = setting.GetType().GetProperty(nameof(NotificationSettingModel.OperationsOnYourAccount)) == null ? null : mapNotifayObject((NotificationBy)setting.GetType().GetProperty(nameof(NotificationSettingModel.OperationsOnYourAccount)).GetValue(setting)),
                PurchaseInvoiceNumber = setting.GetType().GetProperty(nameof(NotificationSettingModel.PurchaseInvoiceNumber)) == null ? null : mapNotifayObject((NotificationBy)setting.GetType().GetProperty(nameof(NotificationSettingModel.PurchaseInvoiceNumber)).GetValue(setting)),
                ReceiveTheAmountOfTheBooklet = setting.GetType().GetProperty(nameof(NotificationSettingModel.ReceiveTheAmountOfTheBooklet)) == null ? null : mapNotifayObject((NotificationBy)setting.GetType().GetProperty(nameof(NotificationSettingModel.ReceiveTheAmountOfTheBooklet)).GetValue(setting)),
                TenderRelatedToYourActivity = setting.GetType().GetProperty(nameof(NotificationSettingModel.TenderRelatedToYourActivity)) == null ? null : mapNotifayObject((NotificationBy)setting.GetType().GetProperty(nameof(NotificationSettingModel.TenderRelatedToYourActivity)).GetValue(setting)),

            };
        }
        Func<NotificationBy, NotificationByModel> mapNotifayObject = (src) => new NotificationByModel(src.Mobile, src.Email);






    }
}
