using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernel;

namespace MOF.Etimad.Monafasat.Services
{
    public class NotifayManager: INotifayManager
    {
        public static IOfferAppService _offerAppService;
        public NotifayManager(IOfferAppService offerAppService)
        {

            _offerAppService = offerAppService;

        }

        public Task CreateOffer(AuditableEntity entity, string methodName)
        {
            throw new NotImplementedException();
        }

        public Task DeleteOffer(AuditableEntity entity, string methodName)
        {
            throw new NotImplementedException();
        }

        public Task UpdateOffer(AuditableEntity entity, string methodName)
        {
            throw new NotImplementedException();
        }
    }
}
