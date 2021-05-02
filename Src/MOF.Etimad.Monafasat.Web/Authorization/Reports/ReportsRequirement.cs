using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using MOF.Etimad.Monafasat.SharedKernel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.Authorization
{
   public class ReportsRequirement : IAuthorizationRequirement { }

   public class ReportsHandler : AuthorizationHandler<ReportsRequirement>
   {
      protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ReportsRequirement requirement)
      {
         var controllerFilter = context.Resource as AuthorizationFilterContext;
         var descriptor = controllerFilter?.ActionDescriptor as ControllerActionDescriptor;
         if (descriptor != null && descriptor.ControllerName == "Reports")
         {
            //General
            if (descriptor.ActionName == "GetBranchByAgency" ||
                descriptor.ActionName == "GetFinancialYear" ||
                descriptor.ActionName == "GetTenderStatuses" ||
                descriptor.ActionName == "GetTendersName")
            {
               context.Succeed(requirement);
               return Task.CompletedTask;
            }
            //TenderPurshasesReport
            if (descriptor.ActionName == "TenderPurshasesReportTotalAmount" ||
               descriptor.ActionName == "TenderPurshasesReport" ||
               descriptor.ActionName == "DownloadTenderPurchaseReport" ||
               descriptor.ActionName == "TenderPurshasesReportPagingAsync")
            {
               var userRoles = context.User.UserRoles();
               List<string> accessedRoles = new List<string> { RoleNames.MonafasatAdmin, RoleNames.MonafasatAccountManager };
               bool hasAccess = IdentityConfigs.CheckForUserNewRoles(userRoles, accessedRoles);
               if (hasAccess)
               {
                  context.Succeed(requirement);
                  return Task.CompletedTask;
               }
               context.Fail();
               return Task.CompletedTask;
            }
            //TenderSalesMonthly
            if (descriptor.ActionName == "GetTenderSalesMonthlyRecipetReportPerAgencyAsync" ||
                descriptor.ActionName == "TenderSalesMonthlyReport" ||
                descriptor.ActionName == "GetTenderSalesMonthlyCountsPerMonthAsync" ||
                descriptor.ActionName == "GetAllTenderSalesMonthlyCountsPerMonthAsync" ||
                descriptor.ActionName == "GetTenderSalesMonthlyTenderDetailsAsync")
            {
               var userRoles = context.User.UserRoles();
               List<string> accessedRoles = new List<string> { RoleNames.MonafasatAdmin, RoleNames.MonafasatAccountManager };
               bool hasAccess = IdentityConfigs.CheckForUserNewRoles(userRoles, accessedRoles);
               if (hasAccess)
               {
                  context.Succeed(requirement);
                  return Task.CompletedTask;
               }
               context.Fail();
               return Task.CompletedTask;
            }
            //TenderValuesToTypesReport
            if (descriptor.ActionName == "TenderValuesToTypesReportAsync" ||
                descriptor.ActionName == "TenderValuesToTypesReport")
            {
               var userRoles = context.User.UserRoles();
               List<string> accessedRoles = new List<string> { RoleNames.MonafasatAdmin, RoleNames.MonafasatAccountManager };
               bool hasAccess = IdentityConfigs.CheckForUserNewRoles(userRoles, accessedRoles);
               if (hasAccess)
               {
                  context.Succeed(requirement);
                  return Task.CompletedTask;
               }
               context.Fail();
               return Task.CompletedTask;
            }
            //AgencyFileReport
            if (descriptor.ActionName == "AgencyFileReport" ||
                descriptor.ActionName == "AgencyFileReportAsync" ||
                descriptor.ActionName == "DownloadAgencyFileReport")
            {
               var userRoles = context.User.UserRoles();
               List<string> accessedRoles = new List<string> { RoleNames.MonafasatAccountManager };
               bool hasAccess = IdentityConfigs.CheckForUserNewRoles(userRoles, accessedRoles);
               if (hasAccess)
               {
                  context.Succeed(requirement);
                  return Task.CompletedTask;
               }
               context.Fail();
               return Task.CompletedTask;
            }
            //AmountOfSavingsReport
            if (descriptor.ActionName == "AmountOfSavingsReport" ||
                descriptor.ActionName == "AmountOfSavingsReportAsync" ||
                descriptor.ActionName == "TotalAmountOfSavingsAsync")
            {
               var userRoles = context.User.UserRoles();
               List<string> accessedRoles = new List<string> { RoleNames.UnitSpecialistLevel2, RoleNames.UnitManagerUser };
               bool hasAccess = IdentityConfigs.CheckForUserNewRoles(userRoles, accessedRoles);
               if (hasAccess)
               {
                  context.Succeed(requirement);
                  return Task.CompletedTask;
               }
               context.Fail();
               return Task.CompletedTask;
            }
            //DailyNotificationsListReport
            if (descriptor.ActionName == "DailyNotificationsListReport" ||
                descriptor.ActionName == "DailyNotificationsListReportAsync")
            {
               var userRoles = context.User.UserRoles();
               List<string> accessedRoles = new List<string> { RoleNames.MonafasatAdmin, RoleNames.MonafasatAccountManager };
               bool hasAccess = IdentityConfigs.CheckForUserNewRoles(userRoles, accessedRoles);
               if (hasAccess)
               {
                  context.Succeed(requirement);
                  return Task.CompletedTask;
               }
               context.Fail();
               return Task.CompletedTask;
            }
            //DirectInvitationsReport
            if (descriptor.ActionName == "DirectInvitationsReportAsync" ||
                descriptor.ActionName == "DirectInvitationsReport")
            {
               var userRoles = context.User.UserRoles();
               List<string> accessedRoles = new List<string> { RoleNames.MonafasatAdmin, RoleNames.MonafasatAccountManager };
               bool hasAccess = IdentityConfigs.CheckForUserNewRoles(userRoles, accessedRoles);
               if (hasAccess)
               {
                  context.Succeed(requirement);
                  return Task.CompletedTask;
               }
               context.Fail();
               return Task.CompletedTask;
            }
            //MessagesStatusReport
            if (descriptor.ActionName == "MessagesStatusReportAsync" ||
                descriptor.ActionName == "MessagesStatusReport")
            {
               var userRoles = context.User.UserRoles();
               List<string> accessedRoles = new List<string> { RoleNames.MonafasatAccountManager };
               bool hasAccess = IdentityConfigs.CheckForUserNewRoles(userRoles, accessedRoles);
               if (hasAccess)
               {
                  context.Succeed(requirement);
                  return Task.CompletedTask;
               }
               context.Fail();
               return Task.CompletedTask;
            }
            //MostSuppliersHaveTendersReport
            if (descriptor.ActionName == "MostSuppliersHaveTendersReport" ||
                descriptor.ActionName == "MostSuppliersHaveTendersReportAsync")
            {
               var userRoles = context.User.UserRoles();
               List<string> accessedRoles = new List<string> { RoleNames.MonafasatAccountManager, RoleNames.MonafasatAdmin };
               bool hasAccess = IdentityConfigs.CheckForUserNewRoles(userRoles, accessedRoles);
               if (hasAccess)
               {
                  context.Succeed(requirement);
                  return Task.CompletedTask;
               }
               context.Fail();
               return Task.CompletedTask;
            }
            //QualificationReport
            if (descriptor.ActionName == "QualificationReport" ||
                descriptor.ActionName == "QualificationReportAsync" ||
                descriptor.ActionName == "DownloadQualificationReport")
            {
               var userRoles = context.User.UserRoles();
               List<string> accessedRoles = new List<string> { RoleNames.MonafasatAccountManager, RoleNames.MonafasatAdmin };
               bool hasAccess = IdentityConfigs.CheckForUserNewRoles(userRoles, accessedRoles);
               if (hasAccess)
               {
                  context.Succeed(requirement);
                  return Task.CompletedTask;
               }
               context.Fail();
               return Task.CompletedTask;
            }
            //ReportAgencyStatistics
            if (descriptor.ActionName == "ReportAgencyStatistics" ||
                descriptor.ActionName == "AgencyStatisticsReportPagingAsync")
            {
               var userRoles = context.User.UserRoles();
               List<string> accessedRoles = new List<string> { RoleNames.MonafasatAccountManager, RoleNames.MonafasatAdmin };
               bool hasAccess = IdentityConfigs.CheckForUserNewRoles(userRoles, accessedRoles);
               if (hasAccess)
               {
                  context.Succeed(requirement);
                  return Task.CompletedTask;
               }
               context.Fail();
               return Task.CompletedTask;
            }
            //ReportMostSuppliersActivitiesAsync
            if (descriptor.ActionName == "ReportMostSuppliersActivitiesAsync" ||
                descriptor.ActionName == "ReportGetMostSuppliersActivitiesAsync")
            {
               var userRoles = context.User.UserRoles();
               List<string> accessedRoles = new List<string> { RoleNames.MonafasatAccountManager, RoleNames.MonafasatAdmin };
               bool hasAccess = IdentityConfigs.CheckForUserNewRoles(userRoles, accessedRoles);
               if (hasAccess)
               {
                  context.Succeed(requirement);
                  return Task.CompletedTask;
               }
               context.Fail();
               return Task.CompletedTask;
            }
            //ReportMostSuppliersHaveTendersAsync
            if (descriptor.ActionName == "ReportMostSuppliersHaveTendersAsync" ||
                descriptor.ActionName == "ReportGetMostSuppliersHaveTendersAsync")
            {
               var userRoles = context.User.UserRoles();
               List<string> accessedRoles = new List<string> { RoleNames.MonafasatAccountManager, RoleNames.MonafasatAdmin };
               bool hasAccess = IdentityConfigs.CheckForUserNewRoles(userRoles, accessedRoles);
               if (hasAccess)
               {
                  context.Succeed(requirement);
                  return Task.CompletedTask;
               }
               context.Fail();
               return Task.CompletedTask;
            }
            //ReportMostSuppliersHaveTendersAsync
            if (descriptor.ActionName == "ReportMostTendersActivitiesAsync" ||
                descriptor.ActionName == "ReportGetMostSuppliersHaveTendersAsync")
            {
               var userRoles = context.User.UserRoles();
               List<string> accessedRoles = new List<string> { RoleNames.MonafasatAccountManager, RoleNames.MonafasatAdmin };
               bool hasAccess = IdentityConfigs.CheckForUserNewRoles(userRoles, accessedRoles);
               if (hasAccess)
               {
                  context.Succeed(requirement);
                  return Task.CompletedTask;
               }
               context.Fail();
               return Task.CompletedTask;
            }
            //ReportMostTendersActivitiesAsync
            if (descriptor.ActionName == "ReportMostTendersActivitiesAsync" ||
                descriptor.ActionName == "ReportGetMostTendersActivitiesAsync")
            {
               var userRoles = context.User.UserRoles();
               List<string> accessedRoles = new List<string> { RoleNames.MonafasatAccountManager, RoleNames.MonafasatAdmin };
               bool hasAccess = IdentityConfigs.CheckForUserNewRoles(userRoles, accessedRoles);
               if (hasAccess)
               {
                  context.Succeed(requirement);
                  return Task.CompletedTask;
               }
               context.Fail();
               return Task.CompletedTask;
            }
            //ReportPublishedTendersAsync
            if (descriptor.ActionName == "ReportPublishedTendersAsync" ||
                descriptor.ActionName == "ReportGetPublishedTendersAsync")
            {
               var userRoles = context.User.UserRoles();
               List<string> accessedRoles = new List<string> { RoleNames.MonafasatAccountManager, RoleNames.MonafasatAdmin };
               bool hasAccess = IdentityConfigs.CheckForUserNewRoles(userRoles, accessedRoles);
               if (hasAccess)
               {
                  context.Succeed(requirement);
                  return Task.CompletedTask;
               }
               context.Fail();
               return Task.CompletedTask;
            }
            //ReportTenderCountsReportAsync
            if (descriptor.ActionName == "ReportTenderCountsReportAsync" ||
                descriptor.ActionName == "ReportCountTenderCountsReportAsync")
            {
               var userRoles = context.User.UserRoles();
               List<string> accessedRoles = new List<string> { RoleNames.MonafasatAccountManager, RoleNames.MonafasatAdmin };
               bool hasAccess = IdentityConfigs.CheckForUserNewRoles(userRoles, accessedRoles);
               if (hasAccess)
               {
                  context.Succeed(requirement);
                  return Task.CompletedTask;
               }
               context.Fail();
               return Task.CompletedTask;
            }
            //ReportTendersSalesAsync
            if (descriptor.ActionName == "ReportTendersSalesAsync" ||
                descriptor.ActionName == "ReportGetTendersSalesAsync")
            {
               var userRoles = context.User.UserRoles();
               List<string> accessedRoles = new List<string> { RoleNames.MonafasatAccountManager, RoleNames.MonafasatAdmin };
               bool hasAccess = IdentityConfigs.CheckForUserNewRoles(userRoles, accessedRoles);
               if (hasAccess)
               {
                  context.Succeed(requirement);
                  return Task.CompletedTask;
               }
               context.Fail();
               return Task.CompletedTask;
            }
            //ReportTendersToActivity
            if (descriptor.ActionName == "ReportTendersToActivity" ||
                descriptor.ActionName == "ReportTendersToActivityAsync")
            {
               var userRoles = context.User.UserRoles();
               List<string> accessedRoles = new List<string> { RoleNames.MonafasatAccountManager, RoleNames.MonafasatAdmin };
               bool hasAccess = IdentityConfigs.CheckForUserNewRoles(userRoles, accessedRoles);
               if (hasAccess)
               {
                  context.Succeed(requirement);
                  return Task.CompletedTask;
               }
               context.Fail();
               return Task.CompletedTask;
            }
            //SupplierPurshasesReport
            if (descriptor.ActionName == "SupplierPurshasesReport" ||
                descriptor.ActionName == "SupplierPurshasesReportPagingAsync" ||
                descriptor.ActionName == "DownloadSupplierPurchaseReport")
            {
               var userRoles = context.User.UserRoles();
               List<string> accessedRoles = new List<string> { RoleNames.MonafasatAccountManager, RoleNames.MonafasatAdmin, RoleNames.CustomerService };
               bool hasAccess = IdentityConfigs.CheckForUserNewRoles(userRoles, accessedRoles);
               if (hasAccess)
               {
                  context.Succeed(requirement);
                  return Task.CompletedTask;
               }
               context.Fail();
               return Task.CompletedTask;
            }
            //TenderCountsReport
            if (descriptor.ActionName == "TenderCountsReport" ||
                descriptor.ActionName == "TenderCountsReportPagingAsync")
            {
               var userRoles = context.User.UserRoles();
               List<string> accessedRoles = new List<string> { RoleNames.MonafasatAccountManager, RoleNames.MonafasatAdmin };
               bool hasAccess = IdentityConfigs.CheckForUserNewRoles(userRoles, accessedRoles);
               if (hasAccess)
               {
                  context.Succeed(requirement);
                  return Task.CompletedTask;
               }
               context.Fail();
               return Task.CompletedTask;
            }
            //TenderCountToTypesReport
            if (descriptor.ActionName == "TenderCountToTypesReport" ||
                descriptor.ActionName == "TenderCountToTypesReportAsync")
            {
               var userRoles = context.User.UserRoles();
               List<string> accessedRoles = new List<string> { RoleNames.MonafasatAccountManager, RoleNames.MonafasatAdmin };
               bool hasAccess = IdentityConfigs.CheckForUserNewRoles(userRoles, accessedRoles);
               if (hasAccess)
               {
                  context.Succeed(requirement);
                  return Task.CompletedTask;
               }
               context.Fail();
               return Task.CompletedTask;
            }
            //TenderReport
            if (descriptor.ActionName == "TenderReport" ||
                descriptor.ActionName == "DownloadTenderReport" ||
                descriptor.ActionName == "TenderReportPagingAsync")
            {
               var userRoles = context.User.UserRoles();
               List<string> accessedRoles = new List<string> { RoleNames.MonafasatAccountManager, RoleNames.MonafasatAdmin };
               bool hasAccess = IdentityConfigs.CheckForUserNewRoles(userRoles, accessedRoles);
               if (hasAccess)
               {
                  context.Succeed(requirement);
                  return Task.CompletedTask;
               }
               context.Fail();
               return Task.CompletedTask;
            }
            //TendersSummaryReport
            if (descriptor.ActionName == "TendersSummaryReport" ||
                descriptor.ActionName == "GetTendersSummaryAsync" ||
                descriptor.ActionName == "GetTendersSummaryCountsAsync" ||
                descriptor.ActionName == "DownloadTenderSummaryReport")
            {
               var userRoles = context.User.UserRoles();
               List<string> accessedRoles = new List<string> { RoleNames.MonafasatAccountManager, RoleNames.MonafasatAdmin };
               bool hasAccess = IdentityConfigs.CheckForUserNewRoles(userRoles, accessedRoles);
               if (hasAccess)
               {
                  context.Succeed(requirement);
                  return Task.CompletedTask;
               }
               context.Fail();
               return Task.CompletedTask;
            }
            //TendersToAwardedSuppliersReport
            if (descriptor.ActionName == "TendersToAwardedSuppliersReport" ||
                descriptor.ActionName == "TendersToAwardedSuppliersReportAsync")
            {
               var userRoles = context.User.UserRoles();
               List<string> accessedRoles = new List<string> { RoleNames.MonafasatAccountManager, RoleNames.MonafasatAdmin };
               bool hasAccess = IdentityConfigs.CheckForUserNewRoles(userRoles, accessedRoles);
               if (hasAccess)
               {
                  context.Succeed(requirement);
                  return Task.CompletedTask;
               }
               context.Fail();
               return Task.CompletedTask;
            }
            //DirectPurchaseReport
            if (descriptor.ActionName == "DirectPurchaseReport" ||
                   descriptor.ActionName == "DirectPurchaseReportAsync")
            {
               var userRoles = context.User.UserRoles();
               List<string> accessedRoles = new List<string> { RoleNames.MonafasatAccountManager, RoleNames.MonafasatAdmin };
               bool hasAccess = IdentityConfigs.CheckForUserNewRoles(userRoles, accessedRoles);
               if (hasAccess)
               {
                  context.Succeed(requirement);
                  return Task.CompletedTask;
               }
               context.Fail();
               return Task.CompletedTask;
            }
            //QualificationCount
            if (descriptor.ActionName == "QualificationCount" ||
               descriptor.ActionName == "QualificationReportCountList" ||
               descriptor.ActionName == "QualificationCountListAsync" ||
               descriptor.ActionName == "QualificationCountAsync")
            {
               var userRoles = context.User.UserRoles();
               List<string> accessedRoles = new List<string> { RoleNames.MonafasatAccountManager, RoleNames.MonafasatAdmin };
               bool hasAccess = IdentityConfigs.CheckForUserNewRoles(userRoles, accessedRoles);
               if (hasAccess)
               {
                  context.Succeed(requirement);
                  return Task.CompletedTask;
               }
               context.Fail();
               return Task.CompletedTask;
            }
            //PerformanceReport
            if (descriptor.ActionName == "PerformanceReportAsync" ||
                   descriptor.ActionName == "PerformanceReport")
            {
               var userRoles = context.User.UserRoles();
               List<string> accessedRoles = new List<string> { RoleNames.UnitManagerUser };
               bool hasAccess = IdentityConfigs.CheckForUserNewRoles(userRoles, accessedRoles);
               if (hasAccess)
               {
                  context.Succeed(requirement);
                  return Task.CompletedTask;
               }
               context.Fail();
               return Task.CompletedTask;
            }
            //ReportSpendingBySpendAgency
            if (descriptor.ActionName == "ReportSpendingBySpendAgency" ||
                   descriptor.ActionName == "ReportSpendingBySpendAgencyAsync")
            {
               var userRoles = context.User.UserRoles();
               List<string> accessedRoles = new List<string> { RoleNames.MonafasatAdmin, RoleNames.MonafasatAccountManager };
               bool hasAccess = IdentityConfigs.CheckForUserNewRoles(userRoles, accessedRoles);
               if (hasAccess)
               {
                  context.Succeed(requirement);
                  return Task.CompletedTask;
               }
               context.Fail();
               return Task.CompletedTask;
            }
            //ReportSpendingBySpendCategory
            if (descriptor.ActionName == "ReportSpendingBySpendCategory" ||
                   descriptor.ActionName == "ReportSpendingBySpendCategoryAsync")
            {
               var userRoles = context.User.UserRoles();
               List<string> accessedRoles = new List<string> { RoleNames.MonafasatAdmin, RoleNames.MonafasatAccountManager };
               bool hasAccess = IdentityConfigs.CheckForUserNewRoles(userRoles, accessedRoles);
               if (hasAccess)
               {
                  context.Succeed(requirement);
                  return Task.CompletedTask;
               }
               context.Fail();
               return Task.CompletedTask;
            }
         }
         context.Fail();
         return Task.CompletedTask;
      }
   }
}
