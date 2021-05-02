using AutoMapper;
using MOF.Etimad.Monafasat.Core;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOF.Etimad.Monafasat.WebApi
{
    public class EnquiryProfile : Profile
    {
        public EnquiryProfile()
        {
            EnquiryMap();
            EnquiryReplyMap();
            JoinCommitteeMap();
        }

        #region Mapping

        private void JoinCommitteeMap()
        {
            CreateMap<JoinTechnicalCommittee, JoinTechnicalCommitteeModel>()
                         .ForMember(tm => tm.CommitteeName, opt => opt.MapFrom(t => t.Committee.CommitteeName))
                         .ForMember(tm => tm.EnquiryIdString, opt => opt.MapFrom(t => (Util.Encrypt(t.EnquiryId))))
                         .ForMember(tm => tm.EnquirySendingDate, opt => opt.MapFrom(t => t.Enquiry.CreatedAt))
                         .ForMember(tm => tm.EnquiryName, opt => opt.MapFrom(t => t.Enquiry.EnquiryName));

            CreateMap<JoinTechnicalCommitteeModel, JoinTechnicalCommittee>();

        }

        private void EnquiryReplyMap()
        {
            CreateMap<EnquiryReply, EnquiryReplyModel>()
                        .ForMember(tm => tm.EnquiryName, opt => opt.MapFrom(t => t.Enquiry.EnquiryName))
                        .ForMember(tm => tm.EnquiryReplyStatusName, opt => opt.MapFrom(t => t.EnquiryReplyStatus.NameAr))
                        .ForMember(tm => tm.TenderId, opt => opt.MapFrom(t => t.Enquiry.Tender.TenderId))
                        .ForMember(tm => tm.AgencyCommunicationRequestId, opt => opt.MapFrom(t => t.Enquiry.AgencyCommunicationRequestId))
                        .ForMember(tm => tm.TenderTypeId, opt => opt.MapFrom(t => t.Enquiry.Tender.TenderTypeId))
                        .ForMember(tm => tm.BranchId, opt => opt.MapFrom(t => t.Enquiry.Tender.BranchId))
                        .ForMember(tm => tm.TenderIdString, opt => opt.MapFrom(t => (Util.Encrypt(t.Enquiry.TenderId))))
                        .ForMember(tm => tm.EnquiryReplyIdString, opt => opt.MapFrom(t => (Util.Encrypt(t.EnquiryReplyId))))
                        .ForMember(tm => tm.TenderNumber, opt => opt.MapFrom(t => t.Enquiry.Tender.TenderNumber))
                        .ForMember(tm => tm.ReferenceNumber, opt => opt.MapFrom(t => t.Enquiry.Tender.ReferenceNumber))
                        .ForMember(tm => tm.EnquiryReplyDate, opt => opt.MapFrom(t => t.UpdatedAt))
                        .ForMember(tm => tm.CreationDate, opt => opt.MapFrom(t => t.CreatedAt))
                        .ForMember(tm => tm.EnquirySendingDate, opt => opt.MapFrom(t => t.Enquiry.CreatedAt))
                        .ForMember(tm => tm.TenderName, opt => opt.MapFrom(t => t.Enquiry.Tender.TenderName))
                        .ForMember(tm => tm.TenderPurpose, opt => opt.MapFrom(t => t.Enquiry.Tender.Purpose))
                        .ForMember(tm => tm.LastEnqueriesDate, opt => opt.MapFrom(t => t.Enquiry.Tender.LastEnqueriesDate))
                        .ForMember(tm => tm.LastOfferPresentationDate, opt => opt.MapFrom(t => t.Enquiry.Tender.LastOfferPresentationDate))
                        .ForMember(tm => tm.OffersOpeningDate, opt => opt.MapFrom(t => t.Enquiry.Tender.OffersOpeningDate))
                        .ForMember(tm => tm.IsComment, opt => opt.MapFrom(t => t.IsComment))
                        .ForMember(tm => tm.CommitteeId, opt => opt.MapFrom(t => t.CommitteeId))
                        .ForMember(tm => tm.CommitteeName, opt => opt.MapFrom(t => t.Committee.CommitteeName));

            CreateMap<EnquiryReplyModel, EnquiryReply>();
        }

        private void EnquiryMap()
        {
            CreateMap<Enquiry, EnquiryModel>()
                .ForMember(tm => tm.EnquirySendingDate, opt => opt.MapFrom(t => t.CreatedAt))
                .ForMember(tm => tm.TenderName, opt => opt.MapFrom(t => t.Tender.TenderName))

                .ForMember(tm => tm.SupplierName, opt => opt.MapFrom(t => t.Supplier.SelectedCrName))
                .ForMember(tm => tm.LastEnqueriesDate, opt => opt.MapFrom(t => t.Tender.LastEnqueriesDate))
                .ForMember(tm => tm.PendingEnquiryReplyCount, opt => opt.MapFrom(t => t.EnquiryReplies.Count(r => r.IsActive == true && r.IsComment != true && r.EnquiryReplyStatusId == (int)Enums.EnquiryReplyStatus.Pending)))
                .ForMember(tm => tm.ApprovedEnquiryReplyCount, opt => opt.MapFrom(t => t.EnquiryReplies.Count(r => r.IsActive == true && r.IsComment != true && r.EnquiryReplyStatusId == (int)Enums.EnquiryReplyStatus.Approved)))
                .ForMember(tm => tm.ReplyCount, opt => opt.ResolveUsing<int>((src, dst, arg, context) =>
                                                                 GetEnquiryReplyStatus(src, context)))
                .ForMember(tm => tm.TechnicalCommitteeId, opt => opt.ResolveUsing<int>((src, dst, arg, context) =>
                                                    GetTechnicalCommitteeId(src, context)))
                 .ForMember(tm => tm.TechnicalCommitteeName, opt => opt.ResolveUsing<string>((src, dst, arg, context) =>
                                                  GetTechnicalCommitteeName(src, context)))
                .ForMember(tm => tm.IsJoinedCommittee, opt => opt.ResolveUsing<bool?>((src, dst, arg, context) =>
                                                    IsJoinedCommittee(src, context)))
                .ForMember(tm => tm.EnquiryIdString, opt => opt.MapFrom(t => (Util.Encrypt(t.EnquiryId))))
                .ForMember(tm => tm.TenderStatusId, opt => opt.MapFrom(t => t.Tender.TenderStatusId))
                .ForMember(tm => tm.TenderTypeId, opt => opt.MapFrom(t => t.Tender.TenderTypeId))
                .ForMember(tm => tm.TenderNumber, opt => opt.MapFrom(t => t.Tender.TenderNumber))
                .ForMember(tm => tm.EnquiryReplies, opt => opt.ResolveUsing<List<EnquiryReply>>((src, dst, arg, context) =>
                                                                 GetEnquiryReplyList(src, context)));

            CreateMap<EnquiryModel, Enquiry>();
        }

        #endregion

        #region Methods
        private int GetEnquiryReplyStatus(Enquiry src, ResolutionContext context)
        {
            var replyStatusCount = 0;
            if (bool.Parse(context.Options.Items["statusFlag"].ToString()) == true)
            {
                replyStatusCount = src.EnquiryReplies.Where(r => r.EnquiryReplyStatusId == (int)Enums.EnquiryReplyStatus.Approved && r.IsComment != true && r.IsActive == true).Count();
            }
            else
            {
                replyStatusCount = src.EnquiryReplies.Where(r => r.EnquiryReplyStatusId != (int)Enums.EnquiryReplyStatus.New && r.IsComment != true).Count();
            }
            return replyStatusCount;
        }

        private List<EnquiryReply> GetEnquiryReplyList(Enquiry src, ResolutionContext context)
        {
            List<EnquiryReply> replyStatusList = new List<EnquiryReply>();
            if (bool.Parse(context.Options.Items["statusFlag"].ToString()) == true)
            {
                replyStatusList = src.EnquiryReplies.Where(r => r.EnquiryReplyStatusId == (int)Enums.EnquiryReplyStatus.Approved && r.IsActive == true && r.IsComment != true).ToList();
            }
            else
            {
                replyStatusList = src.EnquiryReplies.Where(r => r.EnquiryReplyStatusId != (int)Enums.EnquiryReplyStatus.New && r.IsActive == true && r.IsComment != true).ToList();
            }
            return replyStatusList;
        }

        private string GetTechnicalCommitteeMember(EnquiryReply src, ResolutionContext context)
        {
            var TechnicalCommitteeMemberName = "";

            if (bool.Parse(context.Options.Items["statusFlag"].ToString()) == true)
                return "";

            if (!context.Options.Items.Keys.Contains("userIdFlag"))
                return "";

            var userId = int.Parse(context.Options.Items["userIdFlag"].ToString());

            if (src.Committee != null && src.Committee.CommitteeUsers.Any(c => c.UserProfileId == userId && c.UserRole.Name == RoleNames.TechnicalCommitteeUser))

                TechnicalCommitteeMemberName = src.Committee.CommitteeUsers.Where(c => c.UserProfileId == userId && c.UserRole.Name == RoleNames.TechnicalCommitteeUser).FirstOrDefault().ToString();

            return TechnicalCommitteeMemberName;

        }

        private int GetTechnicalCommitteeId(Enquiry src, ResolutionContext context)
        {
            var TechnicalCommitteeId = 0;

            if (bool.Parse(context.Options.Items["statusFlag"].ToString()) == true)
                return 0;

            if (!context.Options.Items.Keys.Contains("userIdFlag"))
                return 0;

            var userId = int.Parse(context.Options.Items["userIdFlag"].ToString());

            var TechnicalCommitteIdForTender = src.Tender.TechnicalOrganizationId.Value;
            var TechnicalCommitteIdsForUser = src.Tender.TechnicalOrganization.CommitteeUsers.Any(x => x.UserProfileId == userId);

            if (TechnicalCommitteIdsForUser)
            {
                TechnicalCommitteeId = TechnicalCommitteIdForTender;
            }
            else
            {
                TechnicalCommitteeId = src.JoinTechnicalCommittees.Where(t => t.Committee.CommitteeUsers.Any(x => x.UserProfileId == userId) && t.IsActive == true).Select(t => t.CommitteeId).FirstOrDefault();
            }

            return TechnicalCommitteeId;
        }
        private string GetTechnicalCommitteeName(Enquiry src, ResolutionContext context)
        {
            var TechnicalCommitteeName = "";

            if (bool.Parse(context.Options.Items["statusFlag"].ToString()) == true)
                return "";

            if (!context.Options.Items.Keys.Contains("userIdFlag"))
                return "";

            var userId = int.Parse(context.Options.Items["userIdFlag"].ToString());

            var TechnicalCommitteNameForTender = src.Tender.TechnicalOrganization.CommitteeName;
            var TechnicalCommitteIdsForUser = src.Tender.TechnicalOrganization.CommitteeUsers.Any(x => x.UserProfileId == userId);

            if (TechnicalCommitteIdsForUser)
            {
                TechnicalCommitteeName = TechnicalCommitteNameForTender;
            }
            else
            {
                TechnicalCommitteeName = src.JoinTechnicalCommittees.Where(t => t.Committee.CommitteeUsers.Any(x => x.UserProfileId == userId) && t.IsActive == true).Select(t => t.Committee.CommitteeName).FirstOrDefault();

            }
            return TechnicalCommitteeName;
        }

        private bool? IsJoinedCommittee(Enquiry src, ResolutionContext context)
        {
            bool? TechnicalCommitteeId;

            if (bool.Parse(context.Options.Items["statusFlag"].ToString()) == true)
                return null;

            if (!context.Options.Items.Keys.Contains("userIdFlag"))
                return null;

            var userId = int.Parse(context.Options.Items["userIdFlag"].ToString());

            var TechnicalCommitteIdForTender = src.Tender.TechnicalOrganizationId.Value;
            var TechnicalCommitteIdsForUser = src.Tender.TechnicalOrganization.CommitteeUsers.Any(x => x.UserProfileId == userId);
            //var TechnicalCommitteIdsForUser = src.Tender.TechnicalOrganization.CommitteeUsers.Where(x => x.UserProfileId == userId).Select(x => x.CommitteeId).ToList();
            if (TechnicalCommitteIdsForUser)
            {
                TechnicalCommitteeId = false;
            }
            else
            {
                TechnicalCommitteeId = true;
            }
            return TechnicalCommitteeId;
        }

        #endregion
    }
}
