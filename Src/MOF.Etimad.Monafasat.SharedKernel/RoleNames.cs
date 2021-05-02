using System;
using System.Collections.Generic;
using System.Linq;

namespace MOF.Etimad.Monafasat.SharedKernel
{
    public class RoleNames
    {
        #region Monafasat Policies
        [PermissionCaption("عرض منافسات مرحلة الفحص")]
        public const string GetTendersCheckingStagePolicy = "GetTendersCheckingStagePolicy";
        [PermissionCaption("تقديم عرض")]
        public const string ApplyOffersPolicy = "ApplyOffersPolicy";
        [PermissionCaption("تقرير الموردين")]
        public const string OpenOffersReportPolicy = "OpenOffersReportPolicy";
        [PermissionCaption("تقرير فتح العروض")]
        public const string SuppliersReportPolicy = "SuppliersReportPolicy";
        public const string GetOpeningQuantityTablesComponentPolicy = "GetOpeningQuantityTablesComponentPolicy";
        public const string GetFinancialYearPolicy = "GetFinancialYearPolicy";
        public const string TenderRevisionsPolicy = "TenderRevisionsPolicy";
        public const string GetRelatedTendersByActivitiesPolicy = "GetRelatedTendersByActivitiesPolicy";
        public const string CountAndCloseAppliedOffersPolicy = "CountAndCloseAppliedOffersPolicy";
        public const string OffersReportPolicy = "OffersReportPolicy";
        public const string AwardingReportPolicy = "AwardingReportPolicy";
        public const string OpenTenderDetailsPolicy = "OpenTenderDetailsPolicy";
        public const string GetFavouriteSuppliersByListIdPolicy = "GetFavouriteSuppliersByListIdPolicy";
        public const string CreateCancelTenderRequestPolicy = "CreateCancelTenderRequestPolicy";
        public const string ApproveOrRejectTenderCancelRequestPolicy = "ApproveOrRejectTenderCancelRequestPolicy";
        public const string QualificationExtendDateApprovementPolicy = "QualificationExtendDateApprovementPolicy";
        public const string CancelTenderPolicy = "CancelTenderPolicy";
        public const string EditAddedValuePolicy = "EditAddedValuePolicy";
        public const string ViewAddedValuePolicy = "ViewAddedValuePolicy";
        public const string SupplierExtendOfferDatesPolicy = "SupplierExtendOfferDatesPolicy";
        public const string ApproveSupplierExtendOfferDatesPolicy = "ApproveSupplierExtendOfferDatesPolicy";
        public const string DetailsPolicy = "DetailsPolicy";
        public const string JoiningRequestDetailsPolicy = "JoiningRequestDetailsPolicy";
        public const string UpdateInvitationStatusPolicy = "UpdateInvitationStatusPolicy";
        public const string CreateVerificationCodePolicy = "CreateVerificationCodePolicy";
        public const string IndexPolicy = "IndexPolicy";
        public const string QualificationIndexPolicy = "QualificationIndexPolicy";
        public const string GetTenderByEnquiryIdPolicy = "GetTenderByEnquiryIdPolicy";
        public const string GetTenderOffersByIdPolicy = "GetTenderOffersByIdPolicy";
        public const string VROOpenAndCheckingViewPolicy = "VROOpenAndCheckingViewPolicy";
        public const string AddFinantialCheckingResultPolicy = "AddFinantialCheckingResultPolicy";
        [PermissionCaption("مستخدم غير معين")]
        [NonAssignedRole]
        public const string UnAssingedUser = "UnAssingedUser";
        #endregion
        #region MonafasatUser
        [PermissionCaption("مدير حساب منافسات بإعتماد")]
        [NonAssignedRole]
        public const string MonafasatAccountManager = "NewMonafasat_AccountManager";

        [PermissionCaption("مدير منافسات")]
        [NonAssignedRole]
        public const string MonafasatAdmin = "NewMonafasat_Admin";

        [PermissionCaption("المراقب المالى")]
        [NonAssignedRole]
        public const string FinancialSupervisor = "NewMonafasat_FinancialSupervisor";

        [PermissionCaption("مدخل بيانات المنافسة")]
        public const string DataEntry = "NewMonafasat_DataEntry";

        [PermissionCaption("خدمة عملاء")]
        [NonAssignedRole]
        public const string CustomerService = "NewMonafasat_CustomerService";

        [PermissionCaption("مدقق بيانات المنافسة")]
        public const string Auditer = "NewMonafasat_Auditer";

        [PermissionCaption("مورد")]
        [NonAssignedRole]
        public const string supplier = "NewMonafasat_Supplier";

        [PermissionCaption("رئيس لجنة فتح العروض")]
        public const string OffersOppeningManager = "NewMonafasat_OffersOpeningManager";

        [PermissionCaption("سكرتير لجنة فتح العروض")]
        public const string OffersOppeningSecretary = "NewMonafasat_OffersOpeningSecretary";

        [PermissionCaption("رئيس لجنة فحص العروض")]
        public const string OffersCheckManager = "NewMonafasat_OffersCheckManager";

        [PermissionCaption("سكرتير لجنة فحص العروض")]
        public const string OffersCheckSecretary = "NewMonafasat_OffersCheckSecretary";

        [PermissionCaption("رئيس لجنة شراء العروض")]
        public const string OffersPurchaseManager = "NewMonafasat_ManagerDirtectPurshasingCommittee";

        [PermissionCaption("سكرتير لجنة شراء العروض")]
        public const string OffersPurchaseSecretary = "NewMonafasat_SecretaryDirtectPurshasingCommittee";

        [PermissionCaption("مسؤول الجهة الفنية")]
        public const string TechnicalCommitteeUser = "NewMonafasat_TechnicalCommitteeUser";

        [PermissionCaption("مختص مركز تحقيق كفاءة الإنفاق")]
        public const string UnitSecretaryUser = "NewMonafasat_UnitSpecialist";

        [PermissionCaption("مختص مستوى أول مركز تحقيق كفاءة الإنفاق")]
        [NonAssignedRole]
        public const string UnitSpecialistLevel1 = "NewMonafasat_UnitSpecialistLevel1";

        [PermissionCaption("مختص مستوى ثاني مركز تحقيق كفاءة الإنفاق")]
        [NonAssignedRole]
        public const string UnitSpecialistLevel2 = "NewMonafasat_UnitSpecialistLevel2";

        [PermissionCaption("رئيس مركز تحقيق كفاءة الإنفاق")]
        [NonAssignedRole]
        public const string UnitManagerUser = "NewMonafasat_UnitManager";

        [PermissionCaption("اعتماد ترسية المنافسة لصاحب الصلاحية")]
        public const string ApproveTenderAward = "NewMonafasat_ApproveTenderAward";

        [PermissionCaption("رئيس لجنة النظر فى التظلم")]
        [NonAssignedRole]
        public const string ManagerGrievanceCommittee = "NewMonafasat_ManagerGrievanceCommittee";

        [PermissionCaption("سكرتير لجنة النظر فى التظلم")]
        [NonAssignedRole]
        public const string SecretaryGrievanceCommittee = "NewMonafasat_SecretaryGrievanceCommittee";


        [PermissionCaption("رئيس لجنة  التأهيل")]
        public const string PreQualificationCommitteeManager = "NewMonafasat_PreQualificationCommitteeManager";

        [PermissionCaption("سكرتير لجنة  التأهيل")]
        public const string PreQualificationCommitteeSecretary = "NewMonafasat_PreQualificationCommitteeSecretary";

        [PermissionCaption("مسئول التخطيط")]
        public const string PrePlanningCreator = "NewMonafasat_PlanningOfficer";


        [PermissionCaption("معتمد التخطيط")]
        public const string PrePlanningAuditor = "NewMonafasat_PlanningApprover";

        [PermissionCaption("صاحب صلاحية إدارة أعمال مركز تحقيق كفاءة الإنفاق")]
        [NonAssignedRole]
        public const string UnitBusinessManagement = "NewMonafasat_UnitBusinessManagement";

        [PermissionCaption("أخصائي مشتريات")]
        public const string PurshaseSpecialist = "NewMonafasat_PurshaseSpecialist";

        [PermissionCaption("مسؤول اعتماد")]
        public const string EtimadOfficer = "NewMonafasat_EtimadOfficer";

        [PermissionCaption("مختص ادارة الحسابات")]
        public const string ccountsManagementSpecialist = "NewMonafasat_AccountsManagementSpecialist    ";

        [PermissionCaption(" رئيس لجنة فتح و فحص العروض")]
        public const string OffersOpeningAndCheckManager = "NewMonafasat_OffersOpeningAndCheckManager";

        [PermissionCaption("سكرتير لجنة فتح و فحص العروض")]
        public const string OffersOpeningAndCheckSecretary = "NewMonafasat_OffersOpeningAndCheckSecretary";

        [PermissionCaption("مدير إدارة المنتجات - تعديل")]
        [NonAssignedRole]
        public const string ProductManager = "NewMonafasat_ProductionManager";

        [PermissionCaption("مدير إدارة المنتجات - عرض")]
        [NonAssignedRole]
        public const string ProductManagerDisplay = "NewMonafasat_ProductionManagerDisplay";

        #region Block

        [PermissionCaption("سكرتير لجنه المنع")]
        [NonAssignedRole]
        public const string MonafasatBlockListSecritary = "NewMonafasat_SecretaryBlockCommittee";

        [PermissionCaption("لجنة المنع (منافسات)")]
        [NonAssignedRole]
        public const string MonafasatBlackListCommittee = "NewMonafasat_ManagerBlockCommittee";

        [PermissionCaption("مختص ادارة الحسابات")]
        [NonAssignedRole]
        public const string AccountsManagementSpecialist = "NewMonafasat_AccountsManagementSpecialist";

        #endregion

        #endregion
        #region User-Policies
        [PermissionCaption("مدير منافسات")]
        public const string MonafasatAdminPolicy = "MonafasatAdminPolicy";
        [PermissionCaption("مدخل بيانات")]
        public const string DataEntryPolicy = "DataEntryPolicy";
        [PermissionCaption("مدقق بيانات")]
        public const string AuditerPolicy = "AuditerPolicy";
        [PermissionCaption("مدخل و مدقق بيانات")]
        public const string DataEntryAndAuditerPolicy = "DataEntryAndAuditerPolicy";
        [PermissionCaption("مدير لحنة فحص العروض")]
        public const string OffersCheckManagerPolicy = "OffersCheckManagerPolicy";
        public const string AwardingInitialApprovalPolicy = "AwardingInitialApprovalPolicy";
        [PermissionCaption("سكرتير لجنة فحص العروض")]
        public const string OffersCheckSecretaryPolicy = "OffersCheckSecretaryPolicy";
        [PermissionCaption("مدير لجنة فتح العروض")]
        public const string OffersOppeningManagerPolicy = "OffersOppeningManagerPolicy";
        [PermissionCaption("سكرتير لجنة فتح العروض")]
        public const string OffersOppeningSecretaryPolicy = "OffersOppeningSecretaryPolicy";
        [PermissionCaption("سكرتير ومدير لجنة فتح العروض")]
        public const string OffersOppeningSecretaryAndManagerPolicy = "OffersOppeningSecretaryAndManagerPolicy";
        [PermissionCaption("سكرتير ومدير لجنة فتح العروض و سكرتير و مدير لجنة فحص العروض")]
        public const string OffersAttachmentsDetailsPolicy = "OffersAttachmentsDetailsPolicy ";
        [PermissionCaption("سكرتير  ومدير لجنة فحص العروض")]
        public const string OffersCheckSecretaryAndManagerPolicy = "OffersCheckSecretaryAndManagerPolicy ";
        [PermissionCaption("مورد")]
        public const string SupplierPolicy = "SupplierPolicy";
        [PermissionCaption("سكرتير لجنة الشراء المباشر")]
        public const string OffersPurchaseSecretaryPolicy = "OffersPurchaseSecretaryPolicy";
        [PermissionCaption("مدير لجنة الشراء المباشر")]
        public const string OffersPurchaseManagerPolicy = "OffersPurchaseManagerPolicy";
        [PermissionCaption("سكرتير و مدير لجنة الشراء المباشر")]
        public const string OffersPurchaseSecretaryAndManagerPolicy = "OffersPurchaseSecretaryAndManagerPolicy";
        [PermissionCaption("سكرتير و مدير لجنة التأهيل")]
        public const string QualificationSecretaryAndManagerPolicy = "QualificationSecretaryAndManagerPolicy";
        [PermissionCaption("مدير حساب منافسات")]
        public const string MonafasatAccountManagerPolicy = "MonafasatAccountManagerPolicy";
        [PermissionCaption("مدير منافسات و لجنة المنع (منافسات)")]
        public const string AdminAndBlackListCommitteePolicy = "AdminAndBlackListCommitteePolicy";
        [PermissionCaption("مدير منافسات, لجنة المنع (منافسات), مدير حساب منافسات و خدمة العملاء")]
        public const string AdminBlackListAccountManagerAndCustomerServicePolicy = "AdminBlackListAccountManagerAndCustomerServicePolicy";
        [PermissionCaption("مدير منافسات, لجنة المنع (منافسات) و مدير حساب منافسات")]
        public const string AdminBlackListAndAccountManagerPolicy = "AdminBlackListAndAccountManagerPolicy";
        [PermissionCaption("مدير منافسات, لجنة المنع (منافسات) و مدير حساب منافسات")]
        public const string AdminAndDataEntryPolicy = "AdminAndDataEntryPolicy";
        [PermissionCaption("لوحة التحكم")]
        public const string DashboardPolicy = "DashboardPolicy";
        [PermissionCaption("لوحة التحكم, تقرير المنافسات المرفوضة")]
        public const string DashboardRejectedTendersPolicy = "DashboardRejectedTendersPolicy";
        [PermissionCaption("مدير منافسات و مدير حساب منافسات")]
        public const string AdminAndAccountManagerPolicy = "AdminAndAccountManagerPolicy";
        [PermissionCaption("التقارير")]
        public const string ReportsPolicy = "ReportsPolicy";
        [PermissionCaption("لوحة التحكم, عمليات تحت الاجراء")]
        public const string DashboardProcessNeedsActionPolicy = "DashboardProcessNeedsActionPolicy";
        [PermissionCaption("مدقق البيانات و الدعم الفني")]
        public const string AuditerAndTechnicalPolicy = "AuditerAndTechnicalPolicy";
        [PermissionCaption("الاستعلامات")]
        public const string SupplierEnquiriesOnTenderPolicy = "SupplierEnquiriesOnTenderPolicy";
        [PermissionCaption("الدعم الفني")]
        public const string TechnicalCommitteeUserPolicy = "TechnicalCommitteeUserPolicy";
        [PermissionCaption("مدخل بيانات و مورد")]
        public const string DataEntryAndSupplierPolicy = "DataEntryAndSupplierPolicy";
        [PermissionCaption("مدخل بيانات ومدقق بيانات و مورد")]
        public const string DataEntryAndAuditerAndSupplierPolicy = "DataEntryAndAuditerAndSupplierPolicy";
        [PermissionCaption("مختص مستوى أول مركز تحقيق كفاءة الإنفاق")]
        public const string UnitSpecialistLevel1Policy = "UnitSpecialistLevel1Policy";
        [PermissionCaption("مختص مستوى ثاني مركز تحقيق كفاءة الإنفاق")]
        public const string UnitSpecialistLevel2Policy = "UnitSpecialistLevel2Policy";
        [PermissionCaption("مختص  مركز تحقيق كفاءة الإنفاق 2 و مدير  مركز تحقيق كفاءة الإنفاق")]
        public const string UnitSpecialistLevel2AndUnitManagerPolicy = "UnitSpecialistLevel2AndUnitManagerPolicy";
        [PermissionCaption("رئيس مركز تحقيق كفاءة الإنفاق")]
        public const string UnitManagerUserPolicy = "UnitManagerUserPolicy";
        [PermissionCaption("مختص ورئيس مركز تحقيق كفاءة الإنفاق")]
        public const string UnitSpecialistsAndManagerUserPolicy = "UnitSpecialistsAndManagerUserPolicy";
        [PermissionCaption("عرض العروض")]
        public const string CheckTenderOffersPolicy = "CheckTenderOffersPolicy";
        public const string EndOpenFinantialOffersStagePolicy = "EndOpenFinantialOffersStagePolicy";
        public const string ReOpenTendeFinancialCheckingPolicy = "ReOpenTendeFinancialCheckingPolicy";
        [PermissionCaption("صاحب صلاحية اعتماد الترسية")]
        public const string ApproveTenderAwardPolicy = "ApproveTenderAward";
        [PermissionCaption("مستخدم لجنة المنع")]
        public const string MonafasatBlockListSecritaryPolicy = "MonafasatBlockListSecritary";
        [PermissionCaption("التخطبطات المسبقة")]
        public const string PrePlanningIndexPolicy = "PrePlanningIndexPolicy";
        [PermissionCaption("إنشاء التخطيط المسبق")]
        public const string PrePlanningCreationPolicy = "PrePlanningCreationPolicy";
        [PermissionCaption("مراجعة التخطيط المسبق")]
        public const string PrePlanningAuditingPolicy = "PrePlanningAuditingPolicy";
        [PermissionCaption("طلب التظلم")]
        public const string PlaintRequestDataPolicy = "PlaintRequestDataPolicy";
        [PermissionCaption("اعتماد طلب التظلم")]
        public const string ApprovePlaintDataPolicy = "ApprovePlaintDataPolicy";
        [PermissionCaption("فحص طلب التظلم")]
        public const string CheckPlaintDataPolicy = "CheckPlaintDataPolicy";
        public const string EscalatedTendersIndexPolicy = "EscalatedTendersIndexPolicy";
        [PermissionCaption(" رئيس لجنة فتح و فحص العروض")]
        public const string OffersOpeningAndCheckManagerPolicy = "NewMonafasat_OffersOpeningAndCheckManagerPolicy";
        [PermissionCaption("سكرتير لجنة فتح و فحص العروض")]
        public const string OffersOpeningAndCheckSecretaryPolicy = "NewMonafasat_OffersOpeningAndCheckSecretaryPolicy";
        [PermissionCaption("مسؤول منتجات القائمة الإلزامية")]
        [NonAssignedRole]
        public const string MandatoryListOfficer = "LC_ProductsOfficer";
        [PermissionCaption("معتمد منتجات القائمة الإلزامية")]
        [NonAssignedRole]
        public const string MandatoryListApprover = "LC_ProductsApprover";
        [PermissionCaption("مسؤول متطلبات المحتوى المحلي")]
        [NonAssignedRole]
        public const string LocalContentOfficer = "NewMonafasat_LocalContentOfficer";
        [PermissionCaption("صلاحية الدعوات")]
        public const string GetInvitedUnregisteredSuppliersForInvitationAfterTenderApprovementPolicy = "GetInvitedUnregisteredSuppliersForInvitationAfterTenderApprovementPolicy";
        [PermissionCaption("صلاحية تفاصيل الاعلان")]
        public const string GetAnnouncementDetailsPolicy = "GetAnnouncementDetailsPolicy";
        [PermissionCaption("صلاحية تفاصيل اعلانات قوائم الموردين")]
        public const string GetAnnouncementSupplierTemplatePolicy = "GetAnnouncementSupplierTemplatePolicy";
        [PermissionCaption("صلاحية اعلانات تحت الانشاء")]
        public const string GetUnderOperationAgencyAnnouncementPolicy = "GetUnderOperationAgencyAnnouncementPolicy";
        [PermissionCaption("صلاحية كل الاعلان")]
        public const string GetAllAgencyAnnouncementPolicy = "GetAllAgencyAnnouncementPolicy";

        [PermissionCaption("عضو لجنة الشراء المباشر")]
        public const string ApproveExtendOffersRequestPolicy = "ApproveExtendOffersRequestPolicy";
        #endregion
        #region Admin

        #endregion

        public static Dictionary<string, string> GetRolesWithCaptions()
        {
            var result = new Dictionary<string, string>();

            foreach (var field in typeof(RoleNames).GetFields())
            {
                if (field.IsPublic && field.IsLiteral)
                {
                    string value = field.GetValue(null) as string;
                    string caption = value; //Default caption
                    object[] attrs = field.GetCustomAttributes(typeof(PermissionCaptionAttribute), false);
                    if (attrs != null && attrs.Length > 0)
                        caption = ((PermissionCaptionAttribute)attrs[0]).Caption;

                    result.Add(value, caption);
                }
            }
            return result;
        }

        public static string[] GetMonafasatRoles()
        {
            string[] RoleNames = {
                    Enum.GetName(typeof(Enums.UserRole), Enums.UserRole.NewMonafasat_TechnicalCommitteeUser) ,
                    Enum.GetName(typeof(Enums.UserRole), Enums.UserRole.NewMonafasat_OffersOpeningManager),
                    Enum.GetName(typeof(Enums.UserRole), Enums.UserRole.NewMonafasat_OffersOpeningSecretary) ,
                    Enum.GetName(typeof(Enums.UserRole), Enums.UserRole.NewMonafasat_OffersCheckManager),
                    Enum.GetName(typeof(Enums.UserRole), Enums.UserRole.NewMonafasat_OffersCheckSecretary),
                    Enum.GetName(typeof(Enums.UserRole), Enums.UserRole.NewMonafasat_ManagerBlockCommittee),
                    Enum.GetName(typeof(Enums.UserRole), Enums.UserRole.NewMonafasat_Auditer),
                    Enum.GetName(typeof(Enums.UserRole), Enums.UserRole.NewMonafasat_DataEntry),
                    Enum.GetName(typeof(Enums.UserRole), Enums.UserRole.NewMonafasat_PlanningOfficer),
                    Enum.GetName(typeof(Enums.UserRole), Enums.UserRole.NewMonafasat_PlanningApprover),
                    Enum.GetName(typeof(Enums.UserRole), Enums.UserRole.NewMonafasat_EtimadOfficer),
                    Enum.GetName(typeof(Enums.UserRole), Enums.UserRole.NewMonafasat_PurshaseSpecialist),
                    Enum.GetName(typeof(Enums.UserRole), Enums.UserRole.NewMonafasat_ApproveTenderAward),
                    Enum.GetName(typeof(Enums.UserRole), Enums.UserRole.NewMonafasat_PlanningApprover),
                    Enum.GetName(typeof(Enums.UserRole), Enums.UserRole.NewMonafasat_ManagerDirtectPurshasingCommittee),
                    Enum.GetName(typeof(Enums.UserRole), Enums.UserRole.NewMonafasat_SecretaryDirtectPurshasingCommittee),
                    Enum.GetName(typeof(Enums.UserRole), Enums.UserRole.NewMonafasat_PreQualificationCommitteeManager),
                    Enum.GetName(typeof(Enums.UserRole), Enums.UserRole.NewMonafasat_PreQualificationCommitteeSecretary),
                    Enum.GetName(typeof(Enums.UserRole), Enums.UserRole.NewMonafasat_OffersOpeningAndCheckManager),
                    Enum.GetName(typeof(Enums.UserRole), Enums.UserRole.NewMonafasat_OffersOpeningAndCheckSecretary),
                    Enum.GetName(typeof(Enums.UserRole), Enums.UserRole.NewMonafasat_AccountsManagementSpecialist),
                    Enum.GetName(typeof(Enums.UserRole), Enums.UserRole.NewMonafasat_FinancialSupervisor)
                };
            return RoleNames;
        }
        public static string[] GetVROMonafasatRoles()
        {
            string[] RoleNames = {
                    Enum.GetName(typeof(Enums.UserRole), Enums.UserRole.NewMonafasat_TechnicalCommitteeUser) ,
                    Enum.GetName(typeof(Enums.UserRole), Enums.UserRole.NewMonafasat_EtimadOfficer),
                    Enum.GetName(typeof(Enums.UserRole), Enums.UserRole.NewMonafasat_PurshaseSpecialist),
                    Enum.GetName(typeof(Enums.UserRole), Enums.UserRole.NewMonafasat_OffersOpeningAndCheckSecretary),
                    Enum.GetName(typeof(Enums.UserRole), Enums.UserRole.NewMonafasat_OffersOpeningAndCheckManager),
                };
            return RoleNames;
        }

        public static IEnumerable<string> GetAllParentRoles(string childRole)
        {
            foreach (var role in RoleNames.GetRolesWithCaptions())
            {
                if (childRole.StartsWith(role.Key.ToString()))
                    yield return role.Key.ToString();
            }
        }

        public static List<string> GetNonAssignedRoles()
        {
            return typeof(RoleNames).GetFields()
            .Where(a => a.IsPublic && a.IsLiteral && a.IsDefined(typeof(NonAssignedRoleAttribute), false))
            .Select(a => a.GetValue(null).ToString())
            .ToList();
        }
    }

    public sealed class PermissionCaptionAttribute : Attribute
    {
        // This is a positional argument
        public PermissionCaptionAttribute(string caption)
        {
            this.Caption = caption;
        }
        public string Caption { get; private set; }
    }

    public sealed class NonAssignedRoleAttribute : Attribute
    {

    }
}
