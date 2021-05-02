using AutoMapper;
using MOF.Etimad.Monafasat.Integration;
using MOF.Etimad.Monafasat.PaymentCallbackAPI.Model;

namespace MOF.Etimad.Monafasat.PaymentAPI.MappingProfiles
{
    public class PaymentNotificationProfile : Profile
    {

        public PaymentNotificationProfile()
        {
            PaymentNotificationMap();
        }

        private void PaymentNotificationMap()
        {
            CreateMap<PaymentNotificationServiceModel, PaymentNotificationModel>();
            CreateMap<PaymentNotificationModel, PaymentNotificationServiceModel>();
        }
    }
}
