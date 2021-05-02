using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Core.SearchCriterias;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;

using MOF.Etimad.Monafasat.ViewModel.Offer;
using MOF.Etimad.Monafasat.ViewModel.Offer.OpenOfferStage;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{

    public interface INegotiationJobAppService
    {
        Task UpdateAllNegotiationWaitingSupplierResponse(); 
        Task UpdateAllSecondNegotiationWaitingSupplierResponse();

    }
}
