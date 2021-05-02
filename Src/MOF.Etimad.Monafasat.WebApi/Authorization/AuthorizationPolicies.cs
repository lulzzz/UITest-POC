using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using MOF.Etimad.Monafasat.SharedKernel;

namespace MOF.Etimad.Monafasat.WebApi.Authorization
{
    public static class AuthorizationPolicies
    {
        public static AuthorizationOptions LoadPolicies(AuthorizationOptions authorizationOptions)
        {
            #region Qualification-Policy

            authorizationOptions.AddPolicy(
            PolicyNames.CreatePostQualificationPolicy,
            policyBuilder =>
            {
                policyBuilder.RequireAuthenticatedUser();
                policyBuilder.AddRequirements(new CreatePostQualificationRequirment());
            });

            authorizationOptions.AddPolicy(
            PolicyNames.AddPreQualificationPolicy,
            policyBuilder =>
            {
                policyBuilder.RequireAuthenticatedUser();
                policyBuilder.AddRequirements(new AddPreQualificationRequirment());
            });

            authorizationOptions.AddPolicy(
            PolicyNames.QualificationPagingPolicy,
            policyBuilder =>
            {
                policyBuilder.RequireAuthenticatedUser();
                policyBuilder.AddRequirements(new QualificationPagingRequirement());
            });

            authorizationOptions.AddPolicy(
          PolicyNames.PreQualificationApprovalPolicy,
          policyBuilder =>
          {
              policyBuilder.RequireAuthenticatedUser();
              policyBuilder.AddRequirements(new PreQualificationApprovalRequirment());
          });

            authorizationOptions.AddPolicy(
             PolicyNames.PostQualificationApprovalPolicy,
             policyBuilder =>
             {
                 policyBuilder.RequireAuthenticatedUser();
                 policyBuilder.AddRequirements(new PostQualificationApprovalRequirment());
             });
            authorizationOptions.AddPolicy(
       PolicyNames.SendQualificationForApprovePolicy,
       policyBuilder =>
       {
           policyBuilder.RequireAuthenticatedUser();
           policyBuilder.AddRequirements(new SendQualificationForApproveRequirment());
       });
            authorizationOptions.AddPolicy(
             PolicyNames.ApproveQualificationPolicy,
             policyBuilder =>
             {
                 policyBuilder.RequireAuthenticatedUser();
                 policyBuilder.AddRequirements(new ApproveQualificationRequirment());
             });
            authorizationOptions.AddPolicy(
             PolicyNames.RejectApproveQualificationPolicy,
             policyBuilder =>
             {
                 policyBuilder.RequireAuthenticatedUser();
                 policyBuilder.AddRequirements(new RejectApproveQualificationRequirment());
             });

            authorizationOptions.AddPolicy(
             PolicyNames.ReopenQualificationPolicy,
             policyBuilder =>
             {
                 policyBuilder.RequireAuthenticatedUser();
                 policyBuilder.AddRequirements(new ReopenQualificationRequirment());
             });
            authorizationOptions.AddPolicy(
     PolicyNames.SendQualificationForApproveToCommitteeManagerPolicy,
     policyBuilder =>
     {
         policyBuilder.RequireAuthenticatedUser();
         policyBuilder.AddRequirements(new SendQualificationForApproveToCommitteeManagerRequirment());
     });

            authorizationOptions.AddPolicy(
             PolicyNames.ApproveQualificationFromCommitteeManagerPolicy,
             policyBuilder =>
             {
                 policyBuilder.RequireAuthenticatedUser();
                 policyBuilder.AddRequirements(new ApproveQualificationFromCommitteeManagerRequirment());
             });
            authorizationOptions.AddPolicy(
             PolicyNames.RejectApproveQualificationFromCommitteManagerPolicy,
             policyBuilder =>
             {
                 policyBuilder.RequireAuthenticatedUser();
                 policyBuilder.AddRequirements(new RejectApproveQualificationFromCommitteManagerRequirment());
             });

            authorizationOptions.AddPolicy(
             PolicyNames.ReopenQualificationFromCommitteeSecrtraryPolicy,
             policyBuilder =>
             {
                 policyBuilder.RequireAuthenticatedUser();
                 policyBuilder.AddRequirements(new ReopenQualificationFromCommitteeSecrtraryRequirment());
             });

            #endregion Qualification-Policy
            #region Commmon-Requirements

            authorizationOptions.AddPolicy(
            RoleNames.AdminAndAccountManagerPolicy,
            policyBuilder =>
            {
                policyBuilder.RequireAuthenticatedUser();
                policyBuilder.AddRequirements(new AdminAndAccountManagerRequirement());
            });

            authorizationOptions.AddPolicy(
            RoleNames.DataEntry + RoleNames.Auditer,
            policeBuilder =>
            {
                policeBuilder.RequireAuthenticatedUser();
                policeBuilder.AddRequirements(new DataEntryAuditorRequirement());
            });
            authorizationOptions.AddPolicy(
            RoleNames.DataEntry,
            policeBuilder =>
            {
                policeBuilder.RequireAuthenticatedUser();
                policeBuilder.RequireClaim("role", RoleNames.DataEntry)
                .AddRequirements(new MonafasatUserGeneralRequirement());
            });
            authorizationOptions.AddPolicy(
            RoleNames.supplier,
            policeBuilder =>
            {
                policeBuilder.RequireAuthenticatedUser();
                policeBuilder.RequireClaim("role", RoleNames.supplier)
                    .AddRequirements(new MonafasatUserAddTenderRequirement());
            });
            authorizationOptions.AddPolicy(
            RoleNames.Auditer,
            policeBuilder =>
            {
                policeBuilder.RequireAuthenticatedUser();
                policeBuilder.RequireClaim("role", RoleNames.Auditer);
                policeBuilder.AddRequirements(new MonafasatUserUpdateTenderRequirement());
            });
            authorizationOptions.AddPolicy(
            RoleNames.OffersAttachmentsDetailsPolicy,
            policyBuilder =>
            {
                policyBuilder.RequireAuthenticatedUser();
                policyBuilder.AddRequirements(new GetOfferAttachmentsDetailsRequirement());
            });
            authorizationOptions.AddPolicy(
            RoleNames.GetTendersCheckingStagePolicy,
            policyBuilder =>
            {
                policyBuilder.RequireAuthenticatedUser();
                policyBuilder.AddRequirements(new GetTendersCheckingStageRequirement());
            });
            authorizationOptions.AddPolicy(
            RoleNames.DataEntryPolicy,
            policyBuilder =>
            {
                policyBuilder.RequireAuthenticatedUser();
                policyBuilder.AddRequirements(new DataEntryRequirement());
            });
            authorizationOptions.AddPolicy(
            RoleNames.AuditerPolicy,
            policyBuilder =>
            {
                policyBuilder.RequireAuthenticatedUser();
                policyBuilder.AddRequirements(new AuditerRequirement());
            });
            authorizationOptions.AddPolicy(
            RoleNames.DataEntryAndAuditerPolicy,
            policyBuilder =>
            {
                policyBuilder.RequireAuthenticatedUser();
                policyBuilder.AddRequirements(new DataEntryAndAuditerRequirement());
            });

            authorizationOptions.AddPolicy(
            RoleNames.GetInvitedUnregisteredSuppliersForInvitationAfterTenderApprovementPolicy,
            policyBuilder =>
            {
                policyBuilder.RequireAuthenticatedUser();
                policyBuilder.AddRequirements(new GetInvitedUnregisteredSuppliersForInvitationAfterTenderApprovementRequirement());
            });

            authorizationOptions.AddPolicy(
            RoleNames.OffersCheckManagerPolicy,
            policyBuilder =>
            {
                policyBuilder.RequireAuthenticatedUser();
                policyBuilder.AddRequirements(new OffersCheckManagerRequirement());
            });
            authorizationOptions.AddPolicy(
          RoleNames.AwardingInitialApprovalPolicy,
          policyBuilder =>
          {
              policyBuilder.RequireAuthenticatedUser();
              policyBuilder.AddRequirements(new AwardingInitialApprovalRequirement());
          });
            authorizationOptions.AddPolicy(
            RoleNames.QualificationSecretaryAndManagerPolicy,
            policyBuilder =>
            {
                policyBuilder.RequireAuthenticatedUser();
                policyBuilder.AddRequirements(new QualificationSecretaryAndManagerRequirement());
            });
            authorizationOptions.AddPolicy(
            RoleNames.OffersCheckSecretaryPolicy,
            policyBuilder =>
            {
                policyBuilder.RequireAuthenticatedUser();
                policyBuilder.AddRequirements(new OffersCheckSecretaryRequirement());
            });
            authorizationOptions.AddPolicy(
            RoleNames.OffersOppeningManagerPolicy,
            policyBuilder =>
            {
                policyBuilder.RequireAuthenticatedUser();
                policyBuilder.AddRequirements(new OffersOppeningManagerRequirement());
            });
            authorizationOptions.AddPolicy(
            RoleNames.OffersOppeningSecretaryPolicy,
            policyBuilder =>
            {
                policyBuilder.RequireAuthenticatedUser();
                policyBuilder.AddRequirements(new OffersOppeningSecretaryRequirement());
            });
            authorizationOptions.AddPolicy(
            RoleNames.OffersCheckSecretaryAndManagerPolicy,
            policyBuilder =>
            {
                policyBuilder.RequireAuthenticatedUser();
                policyBuilder.AddRequirements(new OffersCheckSecretaryAndManagerRequirement());
            });
            authorizationOptions.AddPolicy(
            RoleNames.OffersOppeningSecretaryAndManagerPolicy,
            policyBuilder =>
            {
                policyBuilder.RequireAuthenticatedUser();
                policyBuilder.AddRequirements(new OffersOppeningSecretaryAndManagerRequirement());
            });
            authorizationOptions.AddPolicy(
            RoleNames.MonafasatAccountManagerPolicy,
            policyBuilder =>
            {
                policyBuilder.RequireAuthenticatedUser();
                policyBuilder.AddRequirements(new MonafasatAccountManagerRequirement());
            });
            authorizationOptions.AddPolicy(
            RoleNames.SupplierPolicy,
            policyBuilder =>
            {
                policyBuilder.RequireAuthenticatedUser();
                policyBuilder.AddRequirements(new SupplierRequirement());
            });

            authorizationOptions.AddPolicy(
            RoleNames.AdminAndBlackListCommitteePolicy,
            policyBuilder =>
            {
                policyBuilder.RequireAuthenticatedUser();
                policyBuilder.AddRequirements(new AdminAndBlackListCommitteeRequirement());
            });

            authorizationOptions.AddPolicy(
            RoleNames.AdminBlackListAccountManagerAndCustomerServicePolicy,
            policyBuilder =>
            {
                policyBuilder.RequireAuthenticatedUser();
                policyBuilder.AddRequirements(new AdminBlackListAccountManagerAndCustomerServiceRequirement());
            });

            authorizationOptions.AddPolicy(
            RoleNames.AdminBlackListAndAccountManagerPolicy,
            policyBuilder =>
            {
                policyBuilder.RequireAuthenticatedUser();
                policyBuilder.AddRequirements(new AdminBlackListAndAccountManagerRequirement());
            });
            authorizationOptions.AddPolicy(
            RoleNames.SupplierPolicy,
            policyBuilder =>
            {
                policyBuilder.RequireAuthenticatedUser();
                policyBuilder.AddRequirements(new SupplierRequirement());
            });
            authorizationOptions.AddPolicy(
        RoleNames.ApplyOffersPolicy,
        policyBuilder =>
        {
            policyBuilder.RequireAuthenticatedUser();
            policyBuilder.AddRequirements(new ApplayOfferRequirement());
        });
            authorizationOptions.AddPolicy(
            RoleNames.OpenOffersReportPolicy,
            policyBuilder =>
            {
                policyBuilder.RequireAuthenticatedUser();
                policyBuilder.AddRequirements(new OpenOffersReportRequirement());
            });

            authorizationOptions.AddPolicy(
               RoleNames.SuppliersReportPolicy,
               policyBuilder =>
             {
                 policyBuilder.RequireAuthenticatedUser();
                 policyBuilder.AddRequirements(new SuppliersReportRequirement());
             });

            authorizationOptions.AddPolicy(
            RoleNames.AdminAndDataEntryPolicy,
            policyBuilder =>
            {
                policyBuilder.RequireAuthenticatedUser();
                policyBuilder.AddRequirements(new AdminAndDataEntryRequirement());
            });
            authorizationOptions.AddPolicy(
            RoleNames.MonafasatAdminPolicy,
            policyBuilder =>
            {
                policyBuilder.RequireAuthenticatedUser();
                policyBuilder.AddRequirements(new MonafasatAdminRequirement());
            });

            authorizationOptions.AddPolicy(
            RoleNames.TechnicalCommitteeUserPolicy,
            policyBuilder =>
            {
                policyBuilder.RequireAuthenticatedUser();
                policyBuilder.AddRequirements(new TechnicalCommitteeRequirement());
            });
            #endregion

            #region Tender-Actions-Requiremnts
            authorizationOptions.AddPolicy(
           RoleNames.GetFinancialYearPolicy,
           policyBuilder =>
           {
               policyBuilder.RequireAuthenticatedUser();
               policyBuilder.AddRequirements(new GetFinancialYearRequirement());
           });
            authorizationOptions.AddPolicy(
           RoleNames.TenderRevisionsPolicy,
           policyBuilder =>
           {
               policyBuilder.RequireAuthenticatedUser();
               policyBuilder.AddRequirements(new TenderRevisionsRequirement());
           });
            authorizationOptions.AddPolicy(
            RoleNames.QualificationExtendDateApprovementPolicy,
            policyBuilder =>
            {
                policyBuilder.RequireAuthenticatedUser();
                policyBuilder.AddRequirements(new QualificationExtendDateApprovementRequirement());
            });
            authorizationOptions.AddPolicy(
           RoleNames.GetRelatedTendersByActivitiesPolicy,
           policyBuilder =>
           {
               policyBuilder.RequireAuthenticatedUser();
               policyBuilder.AddRequirements(new GetRelatedTendersByActivitiesRequirement());
           });
            authorizationOptions.AddPolicy(
           RoleNames.CountAndCloseAppliedOffersPolicy,
           policyBuilder =>
           {
               policyBuilder.RequireAuthenticatedUser();
               policyBuilder.AddRequirements(new CountAndCloseAppliedOffersRequirement());
           });
            authorizationOptions.AddPolicy(
           RoleNames.OffersReportPolicy,
           policyBuilder =>
           {
               policyBuilder.RequireAuthenticatedUser();
               policyBuilder.AddRequirements(new OffersReportRequirement());
           });
            authorizationOptions.AddPolicy(
           RoleNames.AwardingReportPolicy,
           policyBuilder =>
           {
               policyBuilder.RequireAuthenticatedUser();
               policyBuilder.AddRequirements(new AwardingReportRequirement());
           });
            authorizationOptions.AddPolicy(
           RoleNames.OpenTenderDetailsPolicy,
           policyBuilder =>
           {
               policyBuilder.RequireAuthenticatedUser();
               policyBuilder.AddRequirements(new OpenTenderDetailsRequirement());
           });
            authorizationOptions.AddPolicy(
           RoleNames.GetFavouriteSuppliersByListIdPolicy,
           policyBuilder =>
           {
               policyBuilder.RequireAuthenticatedUser();
               policyBuilder.AddRequirements(new GetFavouriteSuppliersByListIdRequirement());
           });
            authorizationOptions.AddPolicy(
           RoleNames.CreateCancelTenderRequestPolicy,
           policyBuilder =>
           {
               policyBuilder.RequireAuthenticatedUser();
               policyBuilder.AddRequirements(new CreateCancelTenderRequestRequirement());
           });
            authorizationOptions.AddPolicy(
           RoleNames.ApproveOrRejectTenderCancelRequestPolicy,
           policyBuilder =>
           {
               policyBuilder.RequireAuthenticatedUser();
               policyBuilder.AddRequirements(new ApproveOrRejectTenderCancelRequestRequirement());
           });
            authorizationOptions.AddPolicy(
           RoleNames.CancelTenderPolicy,
           policyBuilder =>
           {
               policyBuilder.RequireAuthenticatedUser();
               policyBuilder.AddRequirements(new CancelTenderRequirement());
           });
            authorizationOptions.AddPolicy(
            RoleNames.SupplierExtendOfferDatesPolicy,
            policyBuilder =>
            {
                policyBuilder.RequireAuthenticatedUser();
                policyBuilder.AddRequirements(new SupplierExtendOfferDatesRequirement());
            });
            authorizationOptions.AddPolicy(
            RoleNames.ApproveSupplierExtendOfferDatesPolicy,
            policyBuilder =>
            {
                policyBuilder.RequireAuthenticatedUser();
                policyBuilder.AddRequirements(new ApproveSupplierExtendOfferDatesRequirement());
            });
            authorizationOptions.AddPolicy(
            RoleNames.DetailsPolicy,
            policyBuilder =>
            {
                policyBuilder.RequireAuthenticatedUser();
                policyBuilder.AddRequirements(new DetailsRequirement());
            });
            authorizationOptions.AddPolicy(
           RoleNames.JoiningRequestDetailsPolicy,
           policyBuilder =>
           {
               policyBuilder.RequireAuthenticatedUser();
               policyBuilder.AddRequirements(new JoiningRequestDetailsRequirement());
           });
            authorizationOptions.AddPolicy(
           RoleNames.UpdateInvitationStatusPolicy,
           policyBuilder =>
           {
               policyBuilder.RequireAuthenticatedUser();
               policyBuilder.AddRequirements(new UpdateInvitationStatusRequirement());
           });
            authorizationOptions.AddPolicy(
          RoleNames.CreateVerificationCodePolicy,
          policyBuilder =>
          {
              policyBuilder.RequireAuthenticatedUser();
              policyBuilder.AddRequirements(new CreateVerificationCodeRequirement());
          });
            authorizationOptions.AddPolicy(
          RoleNames.GetTenderByEnquiryIdPolicy,
          policyBuilder =>
          {
              policyBuilder.RequireAuthenticatedUser();
              policyBuilder.AddRequirements(new GetTenderByEnquiryIdRequirement());
          });
            authorizationOptions.AddPolicy(
          RoleNames.GetTenderOffersByIdPolicy,
          policyBuilder =>
          {
              policyBuilder.RequireAuthenticatedUser();
              policyBuilder.AddRequirements(new GetTenderOffersByIdRequirement());
          });
            authorizationOptions.AddPolicy(
  RoleNames.DataEntryAndSupplierPolicy,
  policyBuilder =>
  {
      policyBuilder.RequireAuthenticatedUser();
      policyBuilder.AddRequirements(new DataEntryAndSupplierRequirement());
  });
            authorizationOptions.AddPolicy(
  RoleNames.DataEntryAndAuditerAndSupplierPolicy,
  policyBuilder =>
  {
      policyBuilder.RequireAuthenticatedUser();
      policyBuilder.AddRequirements(new DataEntryAndAuditerAndSupplierRequirement());
  });
            authorizationOptions.AddPolicy(
  RoleNames.IndexPolicy,
  policyBuilder =>
  {
      policyBuilder.RequireAuthenticatedUser();
      policyBuilder.AddRequirements(new IndexRequirement());
  });
            authorizationOptions.AddPolicy(
                 RoleNames.EditAddedValuePolicy,
                 policyBuilder =>
                 {
                     policyBuilder.RequireAuthenticatedUser();
                     policyBuilder.AddRequirements(new EditAddedValueRequirement());
                 });
            authorizationOptions.AddPolicy(
                 RoleNames.ViewAddedValuePolicy,
                 policyBuilder =>
                 {
                     policyBuilder.RequireAuthenticatedUser();
                     policyBuilder.AddRequirements(new ViewAddedValueRequirement());
                 });
            #endregion

            #region Dashboard
            authorizationOptions.AddPolicy(
            RoleNames.DashboardPolicy,
            policyBuilder =>
            {
                policyBuilder.RequireAuthenticatedUser();
                policyBuilder.AddRequirements(new DashboardRequirement());
            });

            authorizationOptions.AddPolicy(
            RoleNames.DashboardRejectedTendersPolicy,
            policyBuilder =>
            {
                policyBuilder.RequireAuthenticatedUser();
                policyBuilder.AddRequirements(new DashboardRejectedTendersRequirement());
            });

            authorizationOptions.AddPolicy(
            RoleNames.DashboardProcessNeedsActionPolicy,
            policyBuilder =>
            {
                policyBuilder.RequireAuthenticatedUser();
                policyBuilder.AddRequirements(new DashboardProcessNeedsActionRequirement());
            });

            authorizationOptions.AddPolicy(
            RoleNames.AuditerAndTechnicalPolicy,
            policyBuilder =>
            {
                policyBuilder.RequireAuthenticatedUser();
                policyBuilder.AddRequirements(new AuditerAndTechnicalRequirement());
            });
            #endregion

            #region Enquieries
            authorizationOptions.AddPolicy(
            RoleNames.SupplierEnquiriesOnTenderPolicy,
            policyBuilder =>
            {
                policyBuilder.RequireAuthenticatedUser();
                policyBuilder.AddRequirements(new SupplierEnquiriesOnTenderRequirement());
            });
            #endregion

            #region Unit

            authorizationOptions.AddPolicy(
            RoleNames.UnitManagerUserPolicy,
            policyBuilder =>
            {
                policyBuilder.RequireAuthenticatedUser();
                policyBuilder.AddRequirements(new UnitManagerRequirement());
            });
            authorizationOptions.AddPolicy(
            RoleNames.UnitSpecialistLevel1Policy,
            policyBuilder =>
            {
                policyBuilder.RequireAuthenticatedUser();
                policyBuilder.AddRequirements(new UnitSpecialistLevel1Requirement());
            });
            authorizationOptions.AddPolicy(
            RoleNames.UnitSpecialistLevel2Policy,
            policyBuilder =>
            {
                policyBuilder.RequireAuthenticatedUser();
                policyBuilder.AddRequirements(new UnitSpecialistLevel2Requirement());
            });
            authorizationOptions.AddPolicy(
            RoleNames.UnitSpecialistsAndManagerUserPolicy,
            policyBuilder =>
            {
                policyBuilder.RequireAuthenticatedUser();
                policyBuilder.AddRequirements(new UnitSpecialistsAndManagerRequirement());
            });

            authorizationOptions.AddPolicy(
       RoleNames.UnitSpecialistLevel2AndUnitManagerPolicy,
       policyBuilder =>
       {
           policyBuilder.RequireAuthenticatedUser();
           policyBuilder.AddRequirements(new UnitSpecialistLevel2AndUnitManagerRequirement());
       });


            #endregion

            #region DirecrPurchase

            authorizationOptions.AddPolicy(
            RoleNames.OffersPurchaseManagerPolicy,
            policyBuilder =>
            {
                policyBuilder.RequireAuthenticatedUser();
                policyBuilder.AddRequirements(new DirectPurchaseManagerRequirement());
            });
            authorizationOptions.AddPolicy(
            RoleNames.OffersPurchaseSecretaryPolicy,
            policyBuilder =>
            {
                policyBuilder.RequireAuthenticatedUser();
                policyBuilder.AddRequirements(new DirectPurchaseSecretaryRequirement());
            });
            authorizationOptions.AddPolicy(
            RoleNames.OffersPurchaseSecretaryAndManagerPolicy,
            policyBuilder =>
            {
                policyBuilder.RequireAuthenticatedUser();
                policyBuilder.AddRequirements(new DirectPurchaseSecretaryAndManagerRequirement());
            });

            authorizationOptions.AddPolicy(
             RoleNames.ApproveExtendOffersRequestPolicy,
             policyBuilder =>
             {
                 policyBuilder.RequireAuthenticatedUser();
                 policyBuilder.AddRequirements(new ApproveExtendOffersRequestRequirement());
             });

            #endregion

            #region Checking Requirement

            authorizationOptions.AddPolicy(
            RoleNames.CheckTenderOffersPolicy,
            policyBuilder =>
            {
                policyBuilder.RequireAuthenticatedUser();
                policyBuilder.AddRequirements(new CheckTenderOffersRequirement());
            });
            authorizationOptions.AddPolicy(
            RoleNames.ReOpenTendeFinancialCheckingPolicy,
            policyBuilder =>
            {
                policyBuilder.RequireAuthenticatedUser();
                policyBuilder.AddRequirements(new ReOpenTendeFinancialCheckingRequirement());
            });
            authorizationOptions.AddPolicy(
            RoleNames.EndOpenFinantialOffersStagePolicy,
            policyBuilder =>
            {
                policyBuilder.RequireAuthenticatedUser();
                policyBuilder.AddRequirements(new EndOpenFinantialOffersStageRequirement());
            });
            authorizationOptions.AddPolicy(
            RoleNames.AddFinantialCheckingResultPolicy,
            policyBuilder =>
            {
                policyBuilder.RequireAuthenticatedUser();
                policyBuilder.AddRequirements(new AddFinantialCheckingResultRequirement());
            });
            authorizationOptions.AddPolicy(
            RoleNames.ApproveTenderAwardPolicy,
            policyBuilder =>
            {
                policyBuilder.RequireAuthenticatedUser();
                policyBuilder.AddRequirements(new ApproveTenderAwardRequirement());
            });

            #endregion

            #region PrePlanning
            authorizationOptions.AddPolicy(
  RoleNames.PrePlanningIndexPolicy,
  policyBuilder =>
  {
      policyBuilder.RequireAuthenticatedUser();
      policyBuilder.AddRequirements(new PrePlanningIndexRequirement());
  });
            authorizationOptions.AddPolicy(
            RoleNames.PrePlanningAuditingPolicy,
            policyBuilder =>
            {
                policyBuilder.RequireAuthenticatedUser();
                policyBuilder.AddRequirements(new PrePlanningAuditingRequirement());
            });
            authorizationOptions.AddPolicy(
            RoleNames.PrePlanningCreationPolicy,
            policyBuilder =>
            {
                policyBuilder.RequireAuthenticatedUser();
                policyBuilder.AddRequirements(new PrePlanningCreationRequirement());
            });
            authorizationOptions.AddPolicy(
            RoleNames.PlaintRequestDataPolicy,
            policyBuilder =>
            {
                policyBuilder.RequireAuthenticatedUser();
                policyBuilder.AddRequirements(new PlaintRequesData());
            });
            authorizationOptions.AddPolicy(
            RoleNames.CheckPlaintDataPolicy,
            policyBuilder =>
            {
                policyBuilder.RequireAuthenticatedUser();
                policyBuilder.AddRequirements(new CheckPlaintData());
            });
            authorizationOptions.AddPolicy(
            RoleNames.ApprovePlaintDataPolicy,
            policyBuilder =>
            {
                policyBuilder.RequireAuthenticatedUser();
                policyBuilder.AddRequirements(new ApprovePlaintData());
            });
            authorizationOptions.AddPolicy(
            RoleNames.EscalatedTendersIndexPolicy,
            policyBuilder =>
            {
                policyBuilder.RequireAuthenticatedUser();
                policyBuilder.AddRequirements(new EscalatedTendersIndex());
            });
            #endregion

            #region Reports


            authorizationOptions.AddPolicy(
            RoleNames.ReportsPolicy,
            policyBuilder =>
            {
                policyBuilder.RequireAuthenticatedUser();
                policyBuilder.AddRequirements(new ReportsRequirement());
            });

            #endregion

            #region VRO
            authorizationOptions.AddPolicy(
                RoleNames.VROOpenAndCheckingViewPolicy,
                policyBuilder =>
                {
                    policyBuilder.RequireAuthenticatedUser();
                    policyBuilder.AddRequirements(new VROOpenAndCheckingRequirement());
                });
            #endregion VRO

            // Mandatory List Requirements.
            #region Mandatory-List
            authorizationOptions.AddPolicy(PolicyNames.AddMandatoryListPolicy, policyBuilder =>
            {
                policyBuilder.RequireAuthenticatedUser();
                policyBuilder.AddRequirements(new GetAnnouncementDetailsRequirement());
            });
            authorizationOptions.AddPolicy(PolicyNames.ViewMandatoryListPolicy, policyBuilder =>
            {
                policyBuilder.RequireAuthenticatedUser();
                policyBuilder.AddRequirements(new ViewMandatoryListRequirement());
            });
            authorizationOptions.AddPolicy(PolicyNames.EditMandatoryListPolicy, policyBuilder =>
            {
                policyBuilder.RequireAuthenticatedUser();
                policyBuilder.AddRequirements(new EditMandatoryListRequirement());
            });
            authorizationOptions.AddPolicy(PolicyNames.MandatoryListApprovalPolicy, policyBuilder =>
            {
                policyBuilder.RequireAuthenticatedUser();
                policyBuilder.AddRequirements(new MandatoryListApprovalRequirement());
            });

            #endregion Mandatory-List

            #region Announcement
            authorizationOptions.AddPolicy(RoleNames.GetAnnouncementDetailsPolicy, policyBuilder =>
            {
                policyBuilder.RequireAuthenticatedUser();
                policyBuilder.AddRequirements(new GetAnnouncementDetailsRequirement());
            });

            authorizationOptions.AddPolicy(RoleNames.GetUnderOperationAgencyAnnouncementPolicy, policyBuilder =>
            {
                policyBuilder.RequireAuthenticatedUser();
                policyBuilder.AddRequirements(new GetUnderOperationAgencyAnnouncementRequirement());
            });

            authorizationOptions.AddPolicy(PolicyNames.CreateAnnouncementPolicy, p =>
            {
                p.RequireAuthenticatedUser();
                p.AddRequirements(new CreateAnnouncementRequirement());
            });

            authorizationOptions.AddPolicy(PolicyNames.SendAnnouncementForApprovalPolicy, p =>
            {
                p.RequireAuthenticatedUser();
                p.AddRequirements(new SendAnnouncementForApprovalRequirement());
            });

            authorizationOptions.AddPolicy(PolicyNames.ApproveAnnouncementPolicy, p =>
            {
                p.RequireAuthenticatedUser();
                p.AddRequirements(new ApproveAnnouncementRequirement());
            });

            authorizationOptions.AddPolicy(PolicyNames.RejectApproveAnnouncementPolicy, p =>
            {
                p.RequireAuthenticatedUser();
                p.AddRequirements(new RejectApproveAnnouncementRequirement());
            });

            authorizationOptions.AddPolicy(PolicyNames.ReopenAnnouncementPolicy, p =>
            {
                p.RequireAuthenticatedUser();
                p.AddRequirements(new ReopenAnnouncementRequirement());
            });

            authorizationOptions.AddPolicy(PolicyNames.DeleteAnnouncementPolicy, p =>
            {
                p.RequireAuthenticatedUser();
                p.AddRequirements(new DeleteAnnouncementRequirement());
            });

            authorizationOptions.AddPolicy(RoleNames.GetAllAgencyAnnouncementPolicy, policyBuilder =>
            {
                policyBuilder.RequireAuthenticatedUser();
                policyBuilder.AddRequirements(new GetAllAgencyAnnouncementRequirement());
            });

            #endregion

            #region AnnouncementSupplierTemplate

            authorizationOptions.AddPolicy(RoleNames.GetAnnouncementSupplierTemplatePolicy, policyBuilder =>
            {
                policyBuilder.RequireAuthenticatedUser();
                policyBuilder.AddRequirements(new GetAnnouncementSupplierTemplateRequirement());
            });

            authorizationOptions.AddPolicy(PolicyNames.GetAllAnnouncementSupplierTemplatesForSupplierPolicy, policyBuilder =>
            {
                policyBuilder.RequireAuthenticatedUser();
                policyBuilder.AddRequirements(new GetAllAnnouncementSupplierTemplatesForSupplierRequirements());
            });

            #endregion AnnouncementSupplierTemplate

            return authorizationOptions;
        }


        public static void LoadAuthorizationInjection(IServiceCollection services)
        {
            // inject hendler here
            #region Qualificaton
            services.AddScoped<IAuthorizationHandler, PostQualificationApprovalHandler>();
            services.AddScoped<IAuthorizationHandler, CreatePostQualificationHandler>();
            services.AddScoped<IAuthorizationHandler, PreQualificationApprovalHandler>();
            services.AddScoped<IAuthorizationHandler, AddPreQualificationHandler>();
            services.AddScoped<IAuthorizationHandler, SendQualificationForApproveHandler>();
            services.AddScoped<IAuthorizationHandler, SendQualificationForApproveToCommitteeManagerHandler>();
            services.AddScoped<IAuthorizationHandler, ApproveQualificationHandler>();
            services.AddScoped<IAuthorizationHandler, ApproveQualificationFromCommitteeManagerHandler>();
            services.AddScoped<IAuthorizationHandler, RejectApproveQualificationHandler>();
            services.AddScoped<IAuthorizationHandler, RejectApproveQualificationFromCommitteManagerHandler>();
            services.AddScoped<IAuthorizationHandler, ReopenQualificationHandler>();
            services.AddScoped<IAuthorizationHandler, ReopenQualificationFromCommitteeSecrtraryHandler>();
            services.AddScoped<IAuthorizationHandler, QualificationPagingHandler>();
            #endregion

            #region Common
            services.AddScoped<IAuthorizationHandler, PrePlanningIndexHandler>();
            services.AddScoped<IAuthorizationHandler, PrePlanningAuditingHandler>();
            services.AddScoped<IAuthorizationHandler, PrePlanningCreationHandler>();
            services.AddScoped<IAuthorizationHandler, PlaintRequesHandler>();
            services.AddScoped<IAuthorizationHandler, CheckPlaintDataHandler>();
            services.AddScoped<IAuthorizationHandler, ApprovePlaintDataHandler>();
            services.AddScoped<IAuthorizationHandler, AdminAndAccountManagerHandler>();
            services.AddScoped<IAuthorizationHandler, MonafasatUserAddTenderHandler>();
            services.AddScoped<IAuthorizationHandler, MonafasatUserUpdateTenderHandler>();
            services.AddScoped<IAuthorizationHandler, MonafasatUserViewTenderHandler>();
            services.AddScoped<IAuthorizationHandler, MonafasatUserGeneralHandler>();
            services.AddScoped<IAuthorizationHandler, GetTendersCheckingStageHandler>();
            services.AddScoped<IAuthorizationHandler, DataEntryHandler>();
            services.AddScoped<IAuthorizationHandler, AuditerHandler>();
            services.AddScoped<IAuthorizationHandler, DataEntryAndAuditerHandler>();
            services.AddScoped<IAuthorizationHandler, OffersCheckManagerHandler>();
            services.AddScoped<IAuthorizationHandler, OffersCheckSecretaryHandler>();
            services.AddScoped<IAuthorizationHandler, OffersOppeningMangaerHandler>();
            services.AddScoped<IAuthorizationHandler, AwardingInitialApprovalHandler>();
            services.AddScoped<IAuthorizationHandler, OffersOppeningSecretaryHandler>();
            services.AddScoped<IAuthorizationHandler, OffersCheckSecretaryAndManagerHandler>();
            services.AddScoped<IAuthorizationHandler, QualificationSecretaryAndManagerHandler>();
            services.AddScoped<IAuthorizationHandler, OffersOppeningSecretaryAndManagerHandler>();
            services.AddScoped<IAuthorizationHandler, SupplierHandler>();
            services.AddScoped<IAuthorizationHandler, MonafasatAccountManagerHandler>();
            services.AddScoped<IAuthorizationHandler, AdminAndBlackListCommitteeHandler>();
            services.AddScoped<IAuthorizationHandler, AdminBlackListAccountManagerAndCustomerServiceHandler>();
            services.AddScoped<IAuthorizationHandler, AdminBlackListAndAccountManagerHandler>();
            services.AddScoped<IAuthorizationHandler, ApplayOfferHandler>();
            services.AddScoped<IAuthorizationHandler, SuppliersReportHandler>();
            services.AddScoped<IAuthorizationHandler, OpenOffersReportHandler>();
            services.AddScoped<IAuthorizationHandler, MonafasatAdminHandler>();
            services.AddScoped<IAuthorizationHandler, AdminAndDataEntryHandler>();
            services.AddScoped<IAuthorizationHandler, AuditerAndTechnicalHandler>();
            services.AddScoped<IAuthorizationHandler, TechnicalCommitteeHandler>();
            services.AddScoped<IAuthorizationHandler, EscalatedTendersIndexHandler>();
            services.AddScoped<IAuthorizationHandler, GetInvitedUnregisteredSuppliersForInvitationAfterTenderApprovementHandler>();
            #endregion

            #region Tender-Actions
            services.AddScoped<IAuthorizationHandler, GetFinancialYearHandler>();
            services.AddScoped<IAuthorizationHandler, TenderRevisionsHandler>();
            services.AddScoped<IAuthorizationHandler, GetRelatedTendersByActivitiesHandler>();
            services.AddScoped<IAuthorizationHandler, GetOfferAttachmentsDetailsHandler>();
            services.AddScoped<IAuthorizationHandler, CountAndCloseAppliedOffersHandler>();
            services.AddScoped<IAuthorizationHandler, OffersReportHandler>();
            services.AddScoped<IAuthorizationHandler, AwardingReportHandler>();
            services.AddScoped<IAuthorizationHandler, OpenTenderDetailsHandler>();
            services.AddScoped<IAuthorizationHandler, GetFavouriteSuppliersByListIdHandler>();
            services.AddScoped<IAuthorizationHandler, CreateCancelTenderRequestHandler>();
            services.AddScoped<IAuthorizationHandler, ApproveOrRejectTenderCancelRequestHandler>();
            services.AddScoped<IAuthorizationHandler, CancelTenderHandler>();
            services.AddScoped<IAuthorizationHandler, SupplierExtendOfferDatesHandler>();
            services.AddScoped<IAuthorizationHandler, QualificationExtendDateApprovementHandler>();
            services.AddScoped<IAuthorizationHandler, ApproveSupplierExtendOfferDatesHandler>();
            services.AddScoped<IAuthorizationHandler, DetailsHandler>();
            services.AddScoped<IAuthorizationHandler, JoiningRequestDetailsHandler>();
            services.AddScoped<IAuthorizationHandler, UpdateInvitationStatusHandler>();
            services.AddScoped<IAuthorizationHandler, CreateVerificationCodeHandler>();
            services.AddScoped<IAuthorizationHandler, GetTenderByEnquiryIdHandler>();
            services.AddScoped<IAuthorizationHandler, GetTenderOffersByIdHandler>();
            services.AddScoped<IAuthorizationHandler, DataEntryAndSupplierHandler>();
            services.AddScoped<IAuthorizationHandler, DataEntryAndAuditerAndSupplierHandler>();
            services.AddScoped<IAuthorizationHandler, IndexHandler>();
            services.AddScoped<IAuthorizationHandler, EditAddedValueHandler>();
            services.AddScoped<IAuthorizationHandler, ViewAddedValueHandler>();
            #endregion

            #region Dashboard 
            services.AddScoped<IAuthorizationHandler, DashboardHandler>();
            services.AddScoped<IAuthorizationHandler, DashboardRejectedTendersHandler>();
            services.AddScoped<IAuthorizationHandler, DashboardProcessNeedsActionHandler>();
            #endregion

            #region Enquieries
            services.AddScoped<IAuthorizationHandler, SupplierEnquiriesOnTenderHandler>();
            #endregion

            #region Unit
            services.AddScoped<IAuthorizationHandler, UnitManagerHandler>();
            services.AddScoped<IAuthorizationHandler, UnitSpecialistLevel1Handler>();
            services.AddScoped<IAuthorizationHandler, UnitSpecialistLevel2Handler>();
            services.AddScoped<IAuthorizationHandler, UnitSpecialistsAndManagerHandler>();
            services.AddScoped<IAuthorizationHandler, UnitSpecialistLevel2AndUnitManagerHandler>();
            #endregion

            #region DirectPurchase
            services.AddScoped<IAuthorizationHandler, DirectPurchaseSecretaryAndManagerHandler>();
            services.AddScoped<IAuthorizationHandler, DirectPurchaseManagerHandler>();
            services.AddScoped<IAuthorizationHandler, DirectPurchaseSecretaryHandler>();
            services.AddScoped<IAuthorizationHandler, ApproveExtendOffersRequestHandler>();
            #endregion

            #region Checking Requirement

            services.AddScoped<IAuthorizationHandler, CheckTenderOffersHandler>();
            services.AddScoped<IAuthorizationHandler, ApproveTenderAwardHandler>();
            services.AddScoped<IAuthorizationHandler, EndOpenFinantialOffersStageHandler>();
            services.AddScoped<IAuthorizationHandler, AddFinantialCheckingResultHandler>();
            services.AddScoped<IAuthorizationHandler, ReOpenTendeFinancialCheckingHandler>();

            #endregion

            #region Reports
            services.AddScoped<IAuthorizationHandler, ReportsHandler>();
            #endregion

            #region VRO
            services.AddScoped<IAuthorizationHandler, VROOpenAndCheckingHandler>();
            #endregion VRO

            // Mandatory List 
            #region Mandatory-List
            services.AddScoped<IAuthorizationHandler, MandatoryListApprovalHandler>();
            services.AddScoped<IAuthorizationHandler, AddMandatoryListHandler>();
            services.AddScoped<IAuthorizationHandler, ViewMandatoryListHandler>();
            services.AddScoped<IAuthorizationHandler, EditMandatoryListHandler>();
            services.AddScoped<IAuthorizationHandler, GetAnnouncementDetailsHandler>();

            #endregion Mandatory-List

            // End of mandatory list

            #region Announcement

            services.AddScoped<IAuthorizationHandler, CreateAnnouncementHandler>();
            services.AddScoped<IAuthorizationHandler, SendAnnouncementForApprovalHandler>();
            services.AddScoped<IAuthorizationHandler, ApproveAnnouncementHandler>();
            services.AddScoped<IAuthorizationHandler, RejectApproveAnnouncementHandler>();
            services.AddScoped<IAuthorizationHandler, ReopenAnnouncementHandler>();
            services.AddScoped<IAuthorizationHandler, DeleteAnnouncementHandler>();
            services.AddScoped<IAuthorizationHandler, GetAllAgencyAnnouncementHandler>();
            services.AddScoped<IAuthorizationHandler, GetUnderOperationAgencyAnnouncementHandler>();

            #endregion Announcement

            #region AnnouncementSupplierTemplate

            services.AddScoped<IAuthorizationHandler, GetAnnouncementSupplierTemplateHandler>();
            services.AddScoped<IAuthorizationHandler, GetAllAnnouncementSupplierTemplatesForSupplierHandler>();

            #endregion AnnouncementSupplierTemplate

        }
    }
}
