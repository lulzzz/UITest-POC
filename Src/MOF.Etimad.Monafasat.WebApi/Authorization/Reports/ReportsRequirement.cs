using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using MOF.Etimad.Monafasat.SharedKernel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.WebApi.Authorization
{
    public class ReportsRequirement : IAuthorizationRequirement { }

    public class ReportsHandler : AuthorizationHandler<ReportsRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ReportsRequirement requirement)
        {

            var controllerFilter = context.Resource as AuthorizationFilterContext;
            var descriptor = controllerFilter?.ActionDescriptor as ControllerActionDescriptor;

            if (descriptor != null)
            {
                if (descriptor.ControllerName == "Reports")
                {
                    //General
                    if (descriptor.ActionName == "GetFinancialYear" ||
                        descriptor.ActionName == "GetTenderStatuses" ||
                        descriptor.ActionName == "GetTendersName")
                    {
                        context.Succeed(requirement);
                        return Task.CompletedTask;


                    }


                    //TenderPurshasesReport
                    if (descriptor.ActionName == "GetTendersPurshasesReportList" ||
                       descriptor.ActionName == "GetTenderPurshasesReportTotalAmount" ||
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
                    if (descriptor.ActionName == "GetTenderSalesMonthlyRecipetReportPerAgency" ||
                     descriptor.ActionName == "GetTenderSalesMonthlyCountsPerMonth" ||
                     descriptor.ActionName == "GetAllTenderSalesMonthlyCountsPerMonth" ||
                     descriptor.ActionName == "GetTenderSalesMonthlyTenderDetails")
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

                    //TenderValueToTypesReport
                    if (descriptor.ActionName == "TenderValueToTypesReport")
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
                        descriptor.ActionName == "DownloadAgencyFileReport"
                        )
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
                    if (descriptor.ActionName == "GetAmountOfSavingsAsync" ||
                        descriptor.ActionName == "GetTotalAmountOfSavingsAsync")
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
                    if (descriptor.ActionName == "DailyNotificationsList")
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
                    if (descriptor.ActionName == "DirectInvitationsReport")
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
                    if (descriptor.ActionName == "MessagesStatusReportAsync")
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
                    if (descriptor.ActionName == "Report_GetMostSuppliersHaveTendersAsync")
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
                    if (
                        descriptor.ActionName == "QualificationReportAsyncReport" ||
                        descriptor.ActionName == "DownloadQualificationReport"

                        )
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
                    if (descriptor.ActionName == "ReportGetCountsStatisticsAsync")
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
                    if (descriptor.ActionName == "ReportGetMostSuppliersActivitiesAsync")
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
                    if (descriptor.ActionName == "ReportGetMostSuppliersHaveTendersAsync")
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
                    if (descriptor.ActionName == "ReportGetMostTendersActivitiesAsync")
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
                    if (descriptor.ActionName == "ReportGetPublishedTendersAsync")
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
                    if (descriptor.ActionName == "ReportGetTendersCountReportAsync")
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
                    if (descriptor.ActionName == "ReportGetTendersSalesAsync")
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
                    if (descriptor.ActionName == "ReportTendersToActivity")
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
                    if (descriptor.ActionName == "FindSuppliersPurshaseReport" ||
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
                    if (descriptor.ActionName == "GetTendersCountReportList")
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
                    if (descriptor.ActionName == "TenderCountToTypesReport")
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
                    if (descriptor.ActionName == "DownloadTenderReport" ||
                        descriptor.ActionName == "GetTendersReportList")
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
                    if (descriptor.ActionName == "GetTendersSummaryAsync" ||
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
                    if (descriptor.ActionName == "TendersToAwardedSuppliersReport")
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

                }

                //DirectPurchaseReport
                if (descriptor.ActionName == "DirectPurchaseReportAsyncReport")
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
                if (
                    descriptor.ActionName == "QualificationCountAsync" ||
                    descriptor.ActionName == "QualificationCountListAsync"

                    )
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
                if (descriptor.ActionName == "PerformanceReportAsync")
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
                if (descriptor.ActionName == "ReportSpendingBySpendAgencyAsync")
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
                if (descriptor.ActionName == "ReportSpendingBySpendCategoryAsync")
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
