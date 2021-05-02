using MOF.Etimad.Monafasat.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services.Common.NotificationServices.Abstractions
{
    public interface IOfferNotification
    {
        Task<bool> SendOffer(Offer offer);
        Task<bool> ConfirmReceived(Offer offer);
    }
}
