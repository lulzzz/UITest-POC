using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public interface IAnnouncementQueries
    {
        Task<AnnouncementDetailsModel> FindAnnouncementDetailsByAnnouncementId(int announcementId);
        Task<LookupModel> GetAnnouncementNameByAnnouncementId(int announcementId);
        Task<Announcement> FindAnnouncementByAnnouncementId(int announcementId);
        Task<QueryResult<SupplierGridAnnouncementModel>> FindSupplierAnnouncements(SupplierAnnouncementSearchCriteria cretria);
        Task<SupplierAnnouncementDetailsModel> FindAnnouncementDetailsForSupplierByAnnouncementId(int announcementId, string Cr);
        Task<Announcement> GetAnnouncementByIdForCreationStep(int announcementId);
        Task<CreateAnnouncementModel> GetAnnouncementByIdForEdit(int announcementId);
        Task<Announcement> GetAnnouncementWithNoIncludsById(int announcementId);
        Task<QueryResult<AllAnouuncementsForSupplierVisitorModel>> FindAllSupplierAnnouncements(AllSupplierAnnouncementSearchCriteria criteria);
        Task<QueryResult<AllAnouuncementsForAgencyModel>> FindAllAgencyAnnouncements(AllAgencyAnnouncementSearchCriteriaModel criteria);
        Task<QueryResult<UnderOperationAnouuncementsForAgencyModel>> FindUnderOperationAgencyAnnouncements(UnderOperationAgencyAnnouncementSearchCriteria criteria);
        Task<List<TenderTypeModel>> GetTenderTypes();
    }
}
