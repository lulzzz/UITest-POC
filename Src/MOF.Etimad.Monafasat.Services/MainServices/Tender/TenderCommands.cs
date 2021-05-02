using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Data;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.SharedKernel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public class TenderCommands : ITenderCommands
    {
        private readonly IAppDbContext _context;
        public TenderCommands(IAppDbContext context)
        {
            _context = context;
        }

        #region  Service Commands

        public async Task<Tender> CreateAsync(Tender tender)
        {
            await _context.Tenders.AddAsync(tender);
            await _context.SaveChangesAsync();
            return tender;
        }
        public async Task<string> UpdateReferenceNumber()
        {
            return await _context.GenerateReferenceNumber((int)Enums.ReferenceNumberType.Monafasat);
        }

        public async Task<Tender> UpdateAsync(Tender tender)
        {
            try
            {
                Check.ArgumentNotNull(nameof(tender), tender);
                _context.Tenders.Update(tender);
                await _context.SaveChangesAsync();
                return tender;
            }
            catch (System.Exception ex)
            {

                throw ex;
            }

        }

        public async Task<TenderAttachmentChanges> UpdateAttachmentChangeAsync(TenderAttachmentChanges attachmentChange)
        {
            Check.ArgumentNotNull(nameof(attachmentChange), attachmentChange);
            _context.AttachmentChanges.Update(attachmentChange);
            await _context.SaveChangesAsync();
            return attachmentChange;
        }

        public async Task<Invitation> UpdateInvititionAsync(Invitation invitation)
        {
            Check.ArgumentNotNull(nameof(invitation), invitation);
            _context.Invitations.Update(invitation);
            await _context.SaveChangesAsync();
            return invitation;
        }

        public async Task<List<Tender>> UpdateRangeAsync(List<Tender> tenders)
        {
            Check.ArgumentNotNull(nameof(tenders), tenders);
            foreach (var tender in tenders)
                _context.Tenders.Update(tender);
            await _context.SaveChangesAsync();
            return tenders;
        }

        public async Task<TenderAttachment> UpdateAttachmentAsync(TenderAttachment tenderAttachment)
        {
            Check.ArgumentNotNull(nameof(tenderAttachment), tenderAttachment);
            _context.TenderAttachments.Update(tenderAttachment);
            await _context.SaveChangesAsync();
            return tenderAttachment;
        }



        public async Task<int> CreateAddressAsync(Address address)
        {
            await _context.addresses.AddAsync(address);
            await _context.SaveChangesAsync();
            return address.AddressId;
        }

        public async Task<FavouriteSupplierTender> UpdateTenderFavouriteListAsync(FavouriteSupplierTender favouriteSupplierTenders)
        {
            _context.FavouriteSupplierTenders.Update(favouriteSupplierTenders);
            await _context.SaveChangesAsync();
            return favouriteSupplierTenders;
        }

        public async Task<FavouriteSupplierTender> CreateTenderFavouriteAsync(FavouriteSupplierTender favouriteSupplierTenders)
        {
            await _context.FavouriteSupplierTenders.AddAsync(favouriteSupplierTenders);
            await _context.SaveChangesAsync();
            return favouriteSupplierTenders;
        }



        public async Task<TenderChangeRequest> CreateTenderChangeRequestAsync(TenderChangeRequest changeRequest)
        {
            await _context.TenderChangeRequests.AddAsync(changeRequest);
            await _context.SaveChangesAsync();
            return changeRequest;
        }

        public async Task<TenderChangeRequest> UpdateTenderChangeRequestAsync(TenderChangeRequest changeRequest)
        {
            _context.TenderChangeRequests.Update(changeRequest);
            await _context.SaveChangesAsync();
            return changeRequest;
        }


        #endregion

        #region SyncTenderData

        public async Task<SyncDataWithOldMonafasat> CreateSyncDataWithOldMonafasat(SyncDataWithOldMonafasat syncDataWithOldMonafasat)
        {
            await _context.SyncDataWithOldMonafasat.AddAsync(syncDataWithOldMonafasat);
            await _context.SaveChangesAsync();
            return syncDataWithOldMonafasat;
        }

        #endregion SyncTenderData

    }
}
