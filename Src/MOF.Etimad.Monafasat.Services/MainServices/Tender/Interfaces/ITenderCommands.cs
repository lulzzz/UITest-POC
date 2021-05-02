using MOF.Etimad.Monafasat.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public interface ITenderCommands
    {
        Task<string> UpdateReferenceNumber();
        Task<TenderAttachmentChanges> UpdateAttachmentChangeAsync(TenderAttachmentChanges attachmentChange);
        Task<Tender> UpdateAsync(Tender tender);
        Task<List<Tender>> UpdateRangeAsync(List<Tender> tenders);
        Task<TenderAttachment> UpdateAttachmentAsync(TenderAttachment tenderAttachment);
        Task<Tender> CreateAsync(Tender tender);
        Task<int> CreateAddressAsync(Address address);
        Task<FavouriteSupplierTender> UpdateTenderFavouriteListAsync(FavouriteSupplierTender favouriteSupplierTenders);
        Task<FavouriteSupplierTender> CreateTenderFavouriteAsync(FavouriteSupplierTender favouriteSupplierTenders);
        Task<TenderChangeRequest> CreateTenderChangeRequestAsync(TenderChangeRequest changeRequest);
        Task<TenderChangeRequest> UpdateTenderChangeRequestAsync(TenderChangeRequest changeRequest);
        Task<Invitation> UpdateInvititionAsync(Invitation invitation);
        Task<SyncDataWithOldMonafasat> CreateSyncDataWithOldMonafasat(SyncDataWithOldMonafasat syncDataWithOldMonafasat);
    }
}