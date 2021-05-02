using MOF.Etimad.Monafasat.Core;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Data;
using System;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public class CommunicationCommands : ICommunicationCommands
    {
        private readonly  IAppDbContext _context;
        public CommunicationCommands(IAppDbContext context)
        {
            _context = context;
        }


        #region  Service Commands
        public async Task<SupplierExtendOfferDatesRequest> CreateSupplierExtendOfferDatesRequestAsync(SupplierExtendOfferDatesRequest supplierExtendOfferDatesRequest)
        {
            await _context.SupplierExtendOfferDatesRequests.AddAsync(supplierExtendOfferDatesRequest);
            await _context.SaveChangesAsync();
            return supplierExtendOfferDatesRequest;
        }

        #endregion


        #region 

        public async Task<AgencyCommunicationRequest> CreateAsync(AgencyCommunicationRequest agencyCommunicationRequest)
        {
            try
            {
                await _context.AgencyCommunicationRequests.AddAsync(agencyCommunicationRequest);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return agencyCommunicationRequest;
        }

        public async Task<AgencyCommunicationRequest> UpdateAgencytRequestAsync(AgencyCommunicationRequest request)
        {
            _context.AgencyCommunicationRequests.Update(request);
            await _context.SaveChangesAsync();
            return request;
        }
        public async Task<AgencyCommunicationRequest> UpdateAsync(AgencyCommunicationRequest agencyCommunicationRequest)
        {

            _context.AgencyCommunicationRequests.Update(agencyCommunicationRequest);
            await _context.SaveChangesAsync();
            return agencyCommunicationRequest;


        }

        public async Task<PlaintRequest> UpdatePlaintRequestAsync(PlaintRequest plaintRequest)
        {
            _context.PlaintRequests.Update(plaintRequest);
            await _context.SaveChangesAsync();
            return plaintRequest;
        }
        public async Task<EscalationRequest> UpdateEscalationRequestAsync(EscalationRequest request)
        {
            _context.EscalationRequests.Update(request);
            await _context.SaveChangesAsync();
            return request;
        }
        #endregion


        #region Extend Offers 

        public async Task<ExtendOffersValiditySupplier> UpdateExtendOffersValiditySupplier(ExtendOffersValiditySupplier extendOffersValiditySupplier)
        {
            extendOffersValiditySupplier = _context.ExtendOffersValiditySuppliers.Update(extendOffersValiditySupplier).Entity;
            await _context.SaveChangesAsync();
            return extendOffersValiditySupplier;
        }
        public async Task<ExtendOffersValiditySupplier> CreateExtendOffersValiditySupplier(ExtendOffersValiditySupplier extendOffersValiditySupplier)
        {
            extendOffersValiditySupplier = _context.ExtendOffersValiditySuppliers.Add(extendOffersValiditySupplier).Entity;
            await _context.SaveChangesAsync();
            return extendOffersValiditySupplier;
        }

        public async Task AddExtendOffersValidityAttachment(ExtendOffersValidityAttachment extendOffersValidityAttachment)
        {
            _context.ExtendOffersValidityAttachments.Add(extendOffersValidityAttachment);
            await _context.SaveChangesAsync();
        }
        #endregion


    }
}
