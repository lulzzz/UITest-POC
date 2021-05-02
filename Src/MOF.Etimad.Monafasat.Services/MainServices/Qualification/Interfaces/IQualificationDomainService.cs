using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public interface IQualificationDomainService
    {
        Task CheckMasterWeightForQualification(PreQualificationSavingModel tender);
        Task IsValidToCreate(PreQualificationSavingModel tender);
        Task IsValidToSendQualificationForApprovement(Tender tender);
        Task IsValidToSendQualificationForApprovementFromCommitteeManager(Tender tender);
        Task IsValidToAcceptQualificationFromCommitteSecrtary(Tender qulaification);
        Task IsValidToApproveQualification(Tender tender);
        Task IsValidToApprovePreQualificationFromCommitteeManager(Tender tender, string agencyCode);
        Task IsValidToRejectQualification(Tender tender);
        Task IsValidToReopenQualification(Tender tender);
        void IsValidToStartCheckingTender(Tender tender);
        void IsValidToReopenCheckTender(Tender tender);
        void IsValidToSendTenderToApproveChecking(Tender tender, List<string> roles = null);
        void IsValidToApproveTenderChecking(Tender tender, List<string> roles = null);
        void IsValidToRejectCheckTender(Tender tender, List<string> roles = null);
        void IsValidToAccecssPostQualificationCheckingTender(Tender tender, List<string> roles);
        Task CheckQualificationDateValidation(Tender qualification);
        void IsValidToAccecssCheckingTender(Tender tender);
        Task IsValidToCreatePostQualification(PostQualificationApplyDocumentsModel tender);
        Task CanAddPostqualificationIfTenderHasExtendOfferValidity(int tenderId);
        Task CheckIfSupplierHavePostQualificationBefore(List<string> crs, int tenderId);
        Task<UserProfile> GetDirectPurchaseMemberProfile(int userId);
    }
}
