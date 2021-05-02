using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MOF.Etimad.Monafasat.Data.Migrations
{
    public partial class metaData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterTable(
                name: "OfferSolidarity",
                schema: "Offer",
                comment: "Describe the Data related to offer solidarity ");

            migrationBuilder.AlterTable(
                name: "OfferHistory",
                schema: "Offer",
                comment: "Represent  Offer Data ");

            migrationBuilder.AlterTable(
                name: "Offer",
                schema: "Offer",
                comment: "Represent  Offer Data ");

            migrationBuilder.AlterTable(
                name: "OfferStatus",
                schema: "LookUps",
                comment: "Define the Offer status lookup");

            migrationBuilder.AlterTable(
                name: "OfferSolidarityStatus",
                schema: "LookUps",
                comment: "Define the Offer Solidarity  status lookup");

            migrationBuilder.AlterTable(
                name: "NegotiationType",
                schema: "LookUps",
                comment: "Describe the negotiation types");

            migrationBuilder.AlterTable(
                name: "NegotiationReason",
                schema: "LookUps",
                comment: "Describe the negotiation reason");

            migrationBuilder.AlterTable(
                name: "OfferPresentationWay",
                schema: "Lookups",
                comment: "Define the Offer Presentation Way lookup");

            migrationBuilder.AlterTable(
                name: "NegotiationFirstStageSupplier",
                schema: "CommunicationRequest",
                comment: "Describe first Stage Negotiation Suppliers List");

            migrationBuilder.AlterTable(
                name: "Negotiation",
                schema: "CommunicationRequest",
                comment: "Describe first Stage Negotiation");

            migrationBuilder.AlterTable(
                name: "NegotiationAttachment",
                schema: "CommunicationAgency",
                comment: "Describe the Negotiation Attachment");

            migrationBuilder.AlterColumn<int>(
                name: "VRORelatedBranchId",
                schema: "Tender",
                table: "Tender",
                nullable: true,
                comment: "Branch assigned to vro",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "VROCommitteeId",
                schema: "Tender",
                table: "Tender",
                nullable: true,
                comment: "The committe of VRO for opening, checking and awarding tender",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "UnitSpacialistWouldLikeToAttendTheCommitte",
                schema: "Tender",
                table: "Tender",
                nullable: true,
                comment: "Determine if the unit manger want to attend th committee",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TenderUnitStatusId",
                schema: "Tender",
                table: "Tender",
                nullable: true,
                comment: "The status of tender at unit review",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TenderTypeId",
                schema: "Tender",
                table: "Tender",
                nullable: false,
                comment: "Type of tender",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Define Type Of Tender");

            migrationBuilder.AlterColumn<int>(
                name: "TenderStatusId",
                schema: "Tender",
                table: "Tender",
                nullable: false,
                comment: "Status of tender",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "TenderPointsToPass",
                schema: "Tender",
                table: "Tender",
                nullable: false,
                comment: "The points needed to pass a prequalification",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<int>(
                name: "TenderFirstStageId",
                schema: "Tender",
                table: "Tender",
                nullable: true,
                comment: "The identity of tender of type first stage",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TenderConditionsTemplateId",
                schema: "Tender",
                table: "Tender",
                nullable: true,
                comment: "The Activity template of tender",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TemplateYears",
                schema: "Tender",
                table: "Tender",
                nullable: true,
                comment: "number of years in quantities tables",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TechnicalOrganizationId",
                schema: "Tender",
                table: "Tender",
                nullable: true,
                comment: "The technical committee for reply on suppliers enquireis",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TechnicalAdministrativeCapacity",
                schema: "Tender",
                table: "Tender",
                nullable: false,
                comment: "Technical administrative capacity",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<int>(
                name: "SpendingCategoryId",
                schema: "Tender",
                table: "Tender",
                nullable: true,
                comment: "Define the spending Category of the tender",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ReasonForPurchaseTenderTypeId",
                schema: "Tender",
                table: "Tender",
                nullable: true,
                comment: "The reason of selecting direct purchase tender",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ReasonForLimitedTenderTypeId",
                schema: "Tender",
                table: "Tender",
                nullable: true,
                comment: "The reason of selecting limited tender",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "QuantityTableVersionId",
                schema: "Tender",
                table: "Tender",
                nullable: true,
                comment: "The version of quantity table",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "QualificationTypeId",
                schema: "Tender",
                table: "Tender",
                nullable: true,
                comment: "determine the type of qualification post or prequalification",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PreviousFramWorkId",
                schema: "Tender",
                table: "Tender",
                nullable: true,
                comment: "The identity that identify the previous framework agreament",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PreQualificationId",
                schema: "Tender",
                table: "Tender",
                nullable: true,
                comment: "The identity that identify pre qualification on the tender",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PreQualificationCommitteeId",
                schema: "Tender",
                table: "Tender",
                nullable: true,
                comment: "Define PreQualification Committee",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PreAnnouncementId",
                schema: "Tender",
                table: "Tender",
                nullable: true,
                comment: "Tha pre-announcement related to tender",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PostQualificationTenderId",
                schema: "Tender",
                table: "Tender",
                nullable: true,
                comment: "The identity that identify post qualification on the tender",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "OpeningNotificationSent",
                schema: "Tender",
                table: "Tender",
                nullable: true,
                comment: "Flag that states that a notification is sent to the opening committee when the opening date is passed",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "OffersOpeningCommitteeId",
                schema: "Tender",
                table: "Tender",
                nullable: true,
                comment: "The opening committee for opening offers",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "OffersOpeningAddressId",
                schema: "Tender",
                table: "Tender",
                nullable: true,
                comment: "The Identity that identify address of open tender offers",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "OffersCheckingCommitteeId",
                schema: "Tender",
                table: "Tender",
                nullable: true,
                comment: "The Checking committee for checking and awarding tender",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "OfferPresentationWayId",
                schema: "Tender",
                table: "Tender",
                nullable: true,
                comment: "method of apply offers by suppliers one file or two files",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NumberOfWinners",
                schema: "Tender",
                table: "Tender",
                nullable: true,
                comment: "Number of winners in the competition",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "NationalProductPercentage",
                schema: "Tender",
                table: "Tender",
                nullable: true,
                comment: "The percentage of National Product",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsSendToEmarketPlace",
                schema: "Tender",
                table: "Tender",
                nullable: true,
                comment: "Flag determine if the awarded tender is sent to e-market place or not",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsNotificationSentForStoppingPeriod",
                schema: "Tender",
                table: "Tender",
                nullable: true,
                comment: "Flag determine if a notification sent after finishing the stoping period of tender",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsLowBudgetDirectPurchase",
                schema: "Tender",
                table: "Tender",
                nullable: true,
                comment: "determine if the tender is low budget or not if the estimatinn value less than 30000 sar",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "InvitationTypeId",
                schema: "Tender",
                table: "Tender",
                nullable: true,
                comment: "Type of invitation on tender public or private",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "HasGuarantee",
                schema: "Tender",
                table: "Tender",
                nullable: true,
                comment: "Boolean determine if the tender requires a final guarantee or not",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "HasAlternativeOffer",
                schema: "Tender",
                table: "Tender",
                nullable: true,
                comment: "determine if the supliers can apply an alternative offers",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FloarNumber",
                schema: "Tender",
                table: "Tender",
                maxLength: 100,
                nullable: true,
                comment: "Floar number at buliding for samples delivery",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "FinancialCheckingDateSet",
                schema: "Tender",
                table: "Tender",
                nullable: true,
                comment: "Date set by the checking secretary at the start of the financial checking",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "FinancialCapacity",
                schema: "Tender",
                table: "Tender",
                nullable: false,
                comment: "Financial Capacity",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "FinalGuaranteeDeliveryAddress",
                schema: "Tender",
                table: "Tender",
                maxLength: 500,
                nullable: true,
                comment: "The address of deleviry the final guarantee",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DirectPurchaseMemberId",
                schema: "Tender",
                table: "Tender",
                nullable: true,
                comment: "The user responsible for low budget direct purchase",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DirectPurchaseCommitteeId",
                schema: "Tender",
                table: "Tender",
                nullable: true,
                comment: "The direct purchase committee for checking and awarding tender from type direct purchase",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DepartmentName",
                schema: "Tender",
                table: "Tender",
                maxLength: 100,
                nullable: true,
                comment: "Department name at buliding for samples delivery",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeliveryDate",
                schema: "Tender",
                table: "Tender",
                nullable: true,
                comment: "The date of samples delivey to supplier",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CreatedByTypeId",
                schema: "Tender",
                table: "Tender",
                nullable: true,
                comment: "determine the tender created by vro or agency or agency related by vro",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ConditionTemplateStageStatusId",
                schema: "Tender",
                table: "Tender",
                nullable: true,
                comment: "The Status of condition template stage",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "CheckingNotificationSent",
                schema: "Tender",
                table: "Tender",
                nullable: true,
                comment: "Flag that states that a notification is sent to the direct purchase committee when the checking date is tomorrow",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "CheckingDateSet",
                schema: "Tender",
                table: "Tender",
                nullable: true,
                comment: "Is a date set when starting the checking stage",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BuildingName",
                schema: "Tender",
                table: "Tender",
                maxLength: 100,
                nullable: true,
                comment: "The location of building name for samples delivery",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BranchId",
                schema: "Tender",
                table: "Tender",
                nullable: false,
                comment: "The branch that create a tender",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "BonusValue",
                schema: "Tender",
                table: "Tender",
                nullable: true,
                comment: "Bonus value of competition",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AwardingMonths",
                schema: "Tender",
                table: "Tender",
                nullable: true,
                comment: "Final guarantee duration in months",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AwardingDiscountPercentage",
                schema: "Tender",
                table: "Tender",
                nullable: true,
                comment: "Value of the final guarantee",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AnnouncementTemplateId",
                schema: "Tender",
                table: "Tender",
                nullable: true,
                comment: "Tha announcement list of suppliers related to tender",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AgreementYears",
                schema: "Tender",
                table: "Tender",
                nullable: true,
                comment: "Number of years for framework agreement",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AgreementTypeId",
                schema: "Tender",
                table: "Tender",
                nullable: true,
                comment: "The identity that identify the type framework agreament opened or closed",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AgreementMonthes",
                schema: "Tender",
                table: "Tender",
                nullable: true,
                comment: "Number of months for framework agreement",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AgreementDays",
                schema: "Tender",
                table: "Tender",
                nullable: true,
                comment: "Number of days for framework agreement",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AgencyCode",
                schema: "Tender",
                table: "Tender",
                maxLength: 20,
                nullable: true,
                comment: "The Agency code of agency that create a tender",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                schema: "Tender",
                table: "ConditionsBooklet",
                maxLength: 256,
                nullable: true,
                comment: "Determine who updated request for purchased the condition booklet",
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                schema: "Tender",
                table: "ConditionsBooklet",
                nullable: true,
                comment: "Define updated date of purchased the condition booklet",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TenderId",
                schema: "Tender",
                table: "ConditionsBooklet",
                nullable: false,
                comment: "it's a forigne key that described tender related to condtion booklet",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                schema: "Tender",
                table: "ConditionsBooklet",
                nullable: true,
                comment: "Define condition booklet user is active or not",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                schema: "Tender",
                table: "ConditionsBooklet",
                maxLength: 256,
                nullable: true,
                comment: "Determine who cretead request for purchased the condition booklet",
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "Tender",
                table: "ConditionsBooklet",
                nullable: false,
                comment: "Define created date of purchased the condition booklet",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "CommericalRegisterNo",
                schema: "Tender",
                table: "ConditionsBooklet",
                maxLength: 20,
                nullable: true,
                comment: "it's described supplier commerical number that who purchased the condition booklet",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ConditionsBookletId",
                schema: "Tender",
                table: "ConditionsBooklet",
                nullable: false,
                comment: "Define identity of conditions booklet",
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "BranchId",
                schema: "Tender",
                table: "Address",
                nullable: true,
                comment: "Define forigne key of branch",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AddressTypeId",
                schema: "Tender",
                table: "Address",
                nullable: false,
                comment: "Define type of Address",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "AddressName",
                schema: "Tender",
                table: "Address",
                maxLength: 1024,
                nullable: true,
                comment: "Define name of Address",
                oldClrType: typeof(string),
                oldType: "nvarchar(1024)",
                oldMaxLength: 1024,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AddressId",
                schema: "Tender",
                table: "Address",
                nullable: false,
                comment: "Define identity of Address",
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                schema: "PrePlanning",
                table: "PrePlanningProjectType",
                maxLength: 256,
                nullable: true,
                comment: "Determine who updated pre-planning project type",
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                schema: "PrePlanning",
                table: "PrePlanningProjectType",
                nullable: true,
                comment: "Define updated date of pre-planning project type",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PrePlanningId",
                schema: "PrePlanning",
                table: "PrePlanningProjectType",
                nullable: false,
                comment: "it's a foreign  key that described pre-planning ",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                schema: "PrePlanning",
                table: "PrePlanningProjectType",
                nullable: true,
                comment: "Define pre-planning project type is active or not",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                schema: "PrePlanning",
                table: "PrePlanningProjectType",
                maxLength: 256,
                nullable: true,
                comment: "Determine who cretead pre-planning project type",
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "PrePlanning",
                table: "PrePlanningProjectType",
                nullable: false,
                comment: "Define created date of pre-planning project type",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<int>(
                name: "ActivityId",
                schema: "PrePlanning",
                table: "PrePlanningProjectType",
                nullable: false,
                comment: "it's a foreign  key that described activity of pre-planning project type",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                schema: "PrePlanning",
                table: "PrePlanningProjectType",
                nullable: false,
                comment: "define identity of pre-planning project type",
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                schema: "PrePlanning",
                table: "PrePlanningCountry",
                maxLength: 256,
                nullable: true,
                comment: "Determine who updated pre-planning country",
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                schema: "PrePlanning",
                table: "PrePlanningCountry",
                nullable: true,
                comment: "Define updated date of pre-planning country",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PrePlanningId",
                schema: "PrePlanning",
                table: "PrePlanningCountry",
                nullable: false,
                comment: "it's a foreign  key that described pre-planning ",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                schema: "PrePlanning",
                table: "PrePlanningCountry",
                nullable: true,
                comment: "Define pre-planning country is active or not",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                schema: "PrePlanning",
                table: "PrePlanningCountry",
                maxLength: 256,
                nullable: true,
                comment: "Determine who cretead pre-planning country",
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "PrePlanning",
                table: "PrePlanningCountry",
                nullable: false,
                comment: "Define created date of pre-planning country",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<int>(
                name: "CountryId",
                schema: "PrePlanning",
                table: "PrePlanningCountry",
                nullable: false,
                comment: "it's a foreign  key that described country related to pre-planning",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                schema: "PrePlanning",
                table: "PrePlanningCountry",
                nullable: false,
                comment: "define identity of pre-planning country",
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                schema: "PrePlanning",
                table: "PrePlanningArea",
                maxLength: 256,
                nullable: true,
                comment: "Determine who updated pre-planning area",
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                schema: "PrePlanning",
                table: "PrePlanningArea",
                nullable: true,
                comment: "Define updated date of pre-planning area",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PrePlanningId",
                schema: "PrePlanning",
                table: "PrePlanningArea",
                nullable: false,
                comment: "it's a foreign  key that described  pre-planning ",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                schema: "PrePlanning",
                table: "PrePlanningArea",
                nullable: true,
                comment: "Define pre-planning area is active or not",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                schema: "PrePlanning",
                table: "PrePlanningArea",
                maxLength: 256,
                nullable: true,
                comment: "Determine who cretead pre-planning area",
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "PrePlanning",
                table: "PrePlanningArea",
                nullable: false,
                comment: "Define created date of pre-planning area",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<int>(
                name: "AreaId",
                schema: "PrePlanning",
                table: "PrePlanningArea",
                nullable: false,
                comment: "it's a foreign  key that described area related to pre-planning",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                schema: "PrePlanning",
                table: "PrePlanningArea",
                nullable: false,
                comment: "define identity of pre-planning area",
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "YearQuarterId",
                schema: "PrePlanning",
                table: "PrePlanning",
                nullable: false,
                comment: "define pre-planning year quarter",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                schema: "PrePlanning",
                table: "PrePlanning",
                maxLength: 256,
                nullable: true,
                comment: "Determine who updated pre-planning",
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                schema: "PrePlanning",
                table: "PrePlanning",
                nullable: true,
                comment: "Define updated date of pre-planning",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "StatusId",
                schema: "PrePlanning",
                table: "PrePlanning",
                nullable: true,
                comment: "define pre-planning status",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RejectionReason",
                schema: "PrePlanning",
                table: "PrePlanning",
                maxLength: 500,
                nullable: true,
                comment: "define reject reason of pre-planning",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProjectNature",
                schema: "PrePlanning",
                table: "PrePlanning",
                maxLength: 500,
                nullable: true,
                comment: "define project nature of pre-planning",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProjectName",
                schema: "PrePlanning",
                table: "PrePlanning",
                maxLength: 200,
                nullable: true,
                comment: "define project name of pre-planning",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProjectDescription",
                schema: "PrePlanning",
                table: "PrePlanning",
                maxLength: 500,
                nullable: true,
                comment: "define project description of pre-planning",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleteRequested",
                schema: "PrePlanning",
                table: "PrePlanning",
                nullable: false,
                comment: "define pre-planning has deleted request or not",
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                schema: "PrePlanning",
                table: "PrePlanning",
                nullable: true,
                comment: "Define pre-planning is active or not",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "InsideKSA",
                schema: "PrePlanning",
                table: "PrePlanning",
                nullable: true,
                comment: "define pre-planning project is inside ksa or not",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DurationInYears",
                schema: "PrePlanning",
                table: "PrePlanning",
                nullable: true,
                comment: "define pre-planning duration in years",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DurationInMonths",
                schema: "PrePlanning",
                table: "PrePlanning",
                nullable: true,
                comment: "define pre-planning duration in month",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DurationInDays",
                schema: "PrePlanning",
                table: "PrePlanning",
                nullable: true,
                comment: "define pre-planning duration in days",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Duration",
                schema: "PrePlanning",
                table: "PrePlanning",
                maxLength: 100,
                nullable: true,
                comment: "define pre-planning duration",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DeleteRejectionReason",
                schema: "PrePlanning",
                table: "PrePlanning",
                maxLength: 500,
                nullable: true,
                comment: "describe of the reason for denying the deletion request of pre-planning",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                schema: "PrePlanning",
                table: "PrePlanning",
                maxLength: 256,
                nullable: true,
                comment: "Determine who cretead pre-planning",
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "PrePlanning",
                table: "PrePlanning",
                nullable: false,
                comment: "Define created date of pre-planning",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<int>(
                name: "BranchId",
                schema: "PrePlanning",
                table: "PrePlanning",
                nullable: false,
                comment: "it's a foreign  key that describe of the pre-planning branch",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "AgencyCode",
                schema: "PrePlanning",
                table: "PrePlanning",
                maxLength: 20,
                nullable: true,
                comment: "describe of the pre-planning government agency",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PrePlanningId",
                schema: "PrePlanning",
                table: "PrePlanning",
                nullable: false,
                comment: "define identity of pre-planning",
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<DateTime>(
                name: "SubmissionDate",
                schema: "Offer",
                table: "OfferSolidarity",
                nullable: true,
                comment: "The date of Accepting the solidarity Request",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "StatusId",
                schema: "Offer",
                table: "OfferSolidarity",
                nullable: false,
                comment: "Define the status of the request (Not sent , Rejected  or accepted)",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "SolidarityTypeId",
                schema: "Offer",
                table: "OfferSolidarity",
                nullable: false,
                comment: "The type of Request between Registered Supplier or Forign ",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "RejectionReason",
                schema: "Offer",
                table: "OfferSolidarity",
                maxLength: 1024,
                nullable: true,
                comment: "Define the reason if the supplier rejected the request",
                oldClrType: typeof(string),
                oldType: "nvarchar(1024)",
                oldMaxLength: 1024,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "OfferId",
                schema: "Offer",
                table: "OfferSolidarity",
                nullable: false,
                comment: "Define the related Offer",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Mobile",
                schema: "Offer",
                table: "OfferSolidarity",
                nullable: true,
                comment: "The Mobile number for the supplierwho will recieve the request to be partner of the offer",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                schema: "Offer",
                table: "OfferSolidarity",
                nullable: true,
                comment: "The email for the supplierwho will recieve the request to be partner of the offer",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CRNumber",
                schema: "Offer",
                table: "OfferSolidarity",
                nullable: true,
                comment: "Define the supplier Commercial Number if he was Registered on Monafasat",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                schema: "Offer",
                table: "OfferSolidarity",
                nullable: false,
                comment: "Define Unique identifer Of Offer Solidarity Table",
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "TenderId",
                schema: "Offer",
                table: "OfferHistory",
                nullable: false,
                comment: "Define the Id of related Tender",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "RejectionReason",
                schema: "Offer",
                table: "OfferHistory",
                maxLength: 2000,
                nullable: true,
                comment: "The Rejection reason in awrding stage",
                oldClrType: typeof(string),
                oldType: "nvarchar(2000)",
                oldMaxLength: 2000,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "OfferTechnicalEvaluationStatusId",
                schema: "Offer",
                table: "OfferHistory",
                nullable: true,
                comment: "Define the Technical evaluation result [Accepted or Rejected]",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "OfferStatusId",
                schema: "Offer",
                table: "OfferHistory",
                nullable: false,
                comment: "Define the status of offer like (under establishment,Sent,cancelled ...etc )",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "OfferId",
                schema: "Offer",
                table: "OfferHistory",
                nullable: false,
                comment: "Define the related offer",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "OfferAcceptanceStatusId",
                schema: "Offer",
                table: "OfferHistory",
                nullable: true,
                comment: "Define the financial evaluation result [Accepted or Rejected]",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CommericalRegisterNo",
                schema: "Offer",
                table: "OfferHistory",
                nullable: true,
                comment: "Define the Commerical Register Number for the owner supplier for the offer ",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ActionId",
                schema: "Offer",
                table: "OfferHistory",
                nullable: false,
                comment: "The type of action on the offer",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "OfferHistoryId",
                schema: "Offer",
                table: "OfferHistory",
                nullable: false,
                comment: "Define Unique identifer Of Offer Presentation Way lookup",
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalOfferAwardingValue",
                schema: "Offer",
                table: "Offer",
                nullable: true,
                comment: "Define the total value of awarding ",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TenderId",
                schema: "Offer",
                table: "Offer",
                nullable: false,
                comment: "Define the Id of related Tender",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "TechnicalEvaluationLevel",
                schema: "Offer",
                table: "Offer",
                nullable: false,
                comment: "Define the Technical Evaluation Level of the offer ",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "SuplierId",
                schema: "Offer",
                table: "Offer",
                nullable: false,
                comment: "Not Used",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "RejectionReason",
                schema: "Offer",
                table: "Offer",
                nullable: true,
                comment: "The Rejection reason in awrding stage",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PartialOfferAwardingValue",
                schema: "Offer",
                table: "Offer",
                nullable: true,
                comment: "Define the partial value of awarding if the tender partialy awarded",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "OfferWeightAfterCalcNPA",
                schema: "Offer",
                table: "Offer",
                nullable: true,
                comment: "The final financial value of the offer after appling VAT and discount and Calculation of National Product Equation",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "OfferTechnicalEvaluationStatusId",
                schema: "Offer",
                table: "Offer",
                nullable: true,
                comment: "Define the Technical evaluation result [Accepted or Rejected]",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "OfferStatusId",
                schema: "Offer",
                table: "Offer",
                nullable: false,
                comment: "Define the status of offer like (under establishment,Sent,cancelled ...etc )",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "OfferPresentationWayId",
                schema: "Offer",
                table: "Offer",
                nullable: true,
                comment: "Define if the offer is presented in one file or two files ",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "OfferLetterNumber",
                schema: "Offer",
                table: "Offer",
                maxLength: 500,
                nullable: true,
                comment: "Define  the Offer Letter Number",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "OfferLetterDate",
                schema: "Offer",
                table: "Offer",
                nullable: true,
                comment: "Define the Offer Letter Expiry Date",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "OfferAcceptanceStatusId",
                schema: "Offer",
                table: "Offer",
                nullable: true,
                comment: "Define the financial evaluation result [Accepted or Rejected]",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                schema: "Offer",
                table: "Offer",
                nullable: true,
                comment: "Represent the notes entered on Checkng Stage",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "JustificationOfRecommendation",
                schema: "Offer",
                table: "Offer",
                maxLength: 1024,
                nullable: true,
                comment: "Define Justification Of Recommendation",
                oldClrType: typeof(string),
                oldType: "nvarchar(1024)",
                oldMaxLength: 1024,
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsVisitationAttached",
                schema: "Offer",
                table: "Offer",
                nullable: true,
                comment: "Define if Visitation hard Copy appied to the agency ",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsSolidarity",
                schema: "Offer",
                table: "Offer",
                nullable: false,
                comment: "Define if More than one supplier in one offer ",
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<bool>(
                name: "IsPurchaseBillAttached",
                schema: "Offer",
                table: "Offer",
                nullable: true,
                comment: "Define if Purchase Bill hard Copy applied to the agency ",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsOpened",
                schema: "Offer",
                table: "Offer",
                nullable: true,
                comment: "Is the offer opended and all needed Data filled in oppenning stage",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsOfferLetterAttached",
                schema: "Offer",
                table: "Offer",
                nullable: true,
                comment: "Define if Offer Letter Attached ",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsOfferFinancialDetailsEntered",
                schema: "Offer",
                table: "Offer",
                nullable: false,
                comment: "Define if all financial Details entired or Not",
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<bool>(
                name: "IsOfferCopyAttached",
                schema: "Offer",
                table: "Offer",
                nullable: true,
                comment: "Define if hard copy of offer Applied",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsManuallyApplied",
                schema: "Offer",
                table: "Offer",
                nullable: false,
                comment: "Define if the offer was applied out of the system or by the system",
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<bool>(
                name: "IsFinantialOfferLetterCopyAttached",
                schema: "Offer",
                table: "Offer",
                nullable: true,
                comment: "Represent if acopy of Finaintial Offer Letter manually Applied",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsFinaintialOfferLetterAttached",
                schema: "Offer",
                table: "Offer",
                nullable: true,
                comment: "Represent if the  Finaintial Offer Letter Attached",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsBankGuaranteeAttached",
                schema: "Offer",
                table: "Offer",
                nullable: true,
                comment: "Define if Bank Guarantee hard Copy appied to the agency ",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FinantialRejectionReason",
                schema: "Offer",
                table: "Offer",
                nullable: true,
                comment: "The reason for financial rejection in checking stage",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FinantialOfferLetterNumber",
                schema: "Offer",
                table: "Offer",
                nullable: true,
                comment: "The number of Finantial Offer Letter",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FinantialNotes",
                schema: "Offer",
                table: "Offer",
                nullable: true,
                comment: "Represent the notes entered on Checkng Stage",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "FinancialEvaluationLevel",
                schema: "Offer",
                table: "Offer",
                nullable: false,
                comment: "Define the Financial Evaluation Level of the offer ",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "FinalPriceBeforeDiscount",
                schema: "Offer",
                table: "Offer",
                nullable: true,
                comment: "The final financial value of the offer before appling VAT and discount",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "FinalPriceAfterDiscount",
                schema: "Offer",
                table: "Offer",
                nullable: true,
                comment: "The final financial value of the offer after appling VAT and discount",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ExclusionReason",
                schema: "Offer",
                table: "Offer",
                nullable: true,
                comment: "Represent the Reason of excluding the offer ",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DiscountNotes",
                schema: "Offer",
                table: "Offer",
                nullable: true,
                comment: "Represent notes entered while adding  discount , used in awarding stage",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Discount",
                schema: "Offer",
                table: "Offer",
                nullable: true,
                comment: "Represent the Discount , used in awarding stage",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CommericalRegisterNo",
                schema: "Offer",
                table: "Offer",
                maxLength: 20,
                nullable: true,
                comment: "Define the Commerical Register Number for the owner supplier for the offer ",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "OfferId",
                schema: "Offer",
                table: "Offer",
                nullable: false,
                comment: "Define Unique identifer Of Offer",
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "LookUps",
                table: "TenderUnitUpdateType",
                maxLength: 1024,
                nullable: true,
                comment: "Define the name of tender unit type",
                oldClrType: typeof(string),
                oldType: "nvarchar(1024)",
                oldMaxLength: 1024,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TenderUnitUpdateTypeId",
                schema: "LookUps",
                table: "TenderUnitUpdateType",
                nullable: false,
                comment: "Define identity of tender unit type",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "LookUps",
                table: "TenderUnitStatus",
                maxLength: 100,
                nullable: true,
                comment: "Define the name of tender unit status",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TenderUnitStatusId",
                schema: "LookUps",
                table: "TenderUnitStatus",
                nullable: false,
                comment: "Define identity of tender unit status",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "NameEn",
                schema: "LookUps",
                table: "TenderType",
                maxLength: 500,
                nullable: true,
                comment: "Define the english name of tender type",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NameAr",
                schema: "LookUps",
                table: "TenderType",
                maxLength: 500,
                nullable: true,
                comment: "Define the arabic name of tender type",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "InvitationCost",
                schema: "LookUps",
                table: "TenderType",
                nullable: false,
                comment: "Define the Invitation Cost of tender type",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "BuyingCost",
                schema: "LookUps",
                table: "TenderType",
                nullable: false,
                comment: "Define the cost of buying  of tender type",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<int>(
                name: "TenderTypeId",
                schema: "LookUps",
                table: "TenderType",
                nullable: false,
                comment: "Define identity of tender type",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "NameEn",
                schema: "LookUps",
                table: "TenderStatus",
                maxLength: 100,
                nullable: true,
                comment: "Define the english name of tender status",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NameAr",
                schema: "LookUps",
                table: "TenderStatus",
                maxLength: 100,
                nullable: true,
                comment: "Define the arabic name of tender status",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TenderStatusId",
                schema: "LookUps",
                table: "TenderStatus",
                nullable: false,
                comment: "Define identity of tender status",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "NameEn",
                schema: "LookUps",
                table: "OfferStatus",
                maxLength: 100,
                nullable: true,
                comment: "Define the English Name Of  Offer Status",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NameAr",
                schema: "LookUps",
                table: "OfferStatus",
                maxLength: 100,
                nullable: true,
                comment: "Define the Arabic Name Of  Offer Status",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "OfferStatusId",
                schema: "LookUps",
                table: "OfferStatus",
                nullable: false,
                comment: "Define Unique identifer Of Offer Status lookup",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "LookUps",
                table: "OfferSolidarityStatus",
                maxLength: 1024,
                nullable: true,
                comment: "Define the  Name Of  Offer Solidarity Status",
                oldClrType: typeof(string),
                oldType: "nvarchar(1024)",
                oldMaxLength: 1024,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CombinedStatusId",
                schema: "LookUps",
                table: "OfferSolidarityStatus",
                nullable: false,
                comment: "Define Unique identifer Of Offer Solidarity  Status lookup",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "LookUps",
                table: "NegotiationType",
                maxLength: 100,
                nullable: true,
                comment: "Define The name of Negotiation Type Name",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NegotiationTypeId",
                schema: "LookUps",
                table: "NegotiationType",
                nullable: false,
                comment: "Define the unique  identifier for negotiation types",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "LookUps",
                table: "NegotiationReason",
                maxLength: 100,
                nullable: true,
                comment: "Define The name of Negotiation reason Name",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NegotiationReasonId",
                schema: "LookUps",
                table: "NegotiationReason",
                nullable: false,
                comment: "Define the unique  identifier for negotiation reason",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "NameEn",
                schema: "LookUps",
                table: "InvitationStatus",
                maxLength: 100,
                nullable: true,
                comment: "define english name of invitation status",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NameAr",
                schema: "LookUps",
                table: "InvitationStatus",
                maxLength: 100,
                nullable: true,
                comment: "define arabic name of invitation status",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "InvitationStatusId",
                schema: "LookUps",
                table: "InvitationStatus",
                nullable: false,
                comment: "define identity of invitation status",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "NameEn",
                schema: "LookUps",
                table: "Country",
                maxLength: 1024,
                nullable: true,
                comment: "Define english name of country",
                oldClrType: typeof(string),
                oldType: "nvarchar(1024)",
                oldMaxLength: 1024,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NameAr",
                schema: "LookUps",
                table: "Country",
                maxLength: 1024,
                nullable: true,
                comment: "Define arabic name of country",
                oldClrType: typeof(string),
                oldType: "nvarchar(1024)",
                oldMaxLength: 1024,
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsGolf",
                schema: "LookUps",
                table: "Country",
                nullable: true,
                comment: "Describe that is country affiliated with the gulf cooperation council or not",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CountryId",
                schema: "LookUps",
                table: "Country",
                nullable: false,
                comment: "Define identity of country",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ParentID",
                schema: "LookUps",
                table: "ConstructionWork",
                nullable: true,
                comment: "define parent id of construction work",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NameEn",
                schema: "LookUps",
                table: "ConstructionWork",
                maxLength: 1024,
                nullable: true,
                comment: "define english name of  construction work",
                oldClrType: typeof(string),
                oldType: "nvarchar(1024)",
                oldMaxLength: 1024,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NameAr",
                schema: "LookUps",
                table: "ConstructionWork",
                maxLength: 1024,
                nullable: false,
                comment: "define arabic name of  construction work",
                oldClrType: typeof(string),
                oldType: "nvarchar(1024)",
                oldMaxLength: 1024);

            migrationBuilder.AlterColumn<int>(
                name: "ConstructionWorkId",
                schema: "LookUps",
                table: "ConstructionWork",
                nullable: false,
                comment: "define identity of construction work",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "NameEn",
                schema: "LookUps",
                table: "CommitteeType",
                maxLength: 500,
                nullable: true,
                comment: "Define english name of committee type",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NameAr",
                schema: "LookUps",
                table: "CommitteeType",
                maxLength: 500,
                nullable: true,
                comment: "Define arabic name of committee type",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CommitteeTypeId",
                schema: "LookUps",
                table: "CommitteeType",
                nullable: false,
                comment: "Define identity of committee type",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "NameEn",
                schema: "LookUps",
                table: "Bank",
                maxLength: 1024,
                nullable: true,
                comment: "Define english name of bank",
                oldClrType: typeof(string),
                oldType: "nvarchar(1024)",
                oldMaxLength: 1024,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NameAr",
                schema: "LookUps",
                table: "Bank",
                maxLength: 1024,
                nullable: true,
                comment: "Define arabic name of bank",
                oldClrType: typeof(string),
                oldType: "nvarchar(1024)",
                oldMaxLength: 1024,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BankId",
                schema: "LookUps",
                table: "Bank",
                nullable: false,
                comment: "Define identity of bank",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "NameEn",
                schema: "LookUps",
                table: "Area",
                maxLength: 1024,
                nullable: true,
                comment: "Define english name of Area",
                oldClrType: typeof(string),
                oldType: "nvarchar(1024)",
                oldMaxLength: 1024,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NameAr",
                schema: "LookUps",
                table: "Area",
                maxLength: 1024,
                nullable: true,
                comment: "Define arabic name of Area",
                oldClrType: typeof(string),
                oldType: "nvarchar(1024)",
                oldMaxLength: 1024,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AreaId",
                schema: "LookUps",
                table: "Area",
                nullable: false,
                comment: "Define identity of area",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "LookUps",
                table: "AgencyCommunicationRequestType",
                maxLength: 1024,
                nullable: true,
                comment: "Define type name of agency communication request",
                oldClrType: typeof(string),
                oldType: "nvarchar(1024)",
                oldMaxLength: 1024,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                schema: "LookUps",
                table: "AgencyCommunicationRequestType",
                nullable: false,
                comment: "Define a unique identifer of agency communication request type",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "LookUps",
                table: "AgencyCommunicationRequestStatus",
                maxLength: 1024,
                nullable: true,
                comment: "Define status name of agency communication request",
                oldClrType: typeof(string),
                oldType: "nvarchar(1024)",
                oldMaxLength: 1024,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                schema: "LookUps",
                table: "AgencyCommunicationRequestStatus",
                nullable: false,
                comment: "Define a unique identifer of agency communication request status",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "LookUps",
                table: "AgencyCommunicationPlaintStatus",
                maxLength: 1024,
                nullable: true,
                comment: "Define the plaint status name of plaint request",
                oldClrType: typeof(string),
                oldType: "nvarchar(1024)",
                oldMaxLength: 1024,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                schema: "LookUps",
                table: "AgencyCommunicationPlaintStatus",
                nullable: false,
                comment: "Define a unique identifer of plaint request status",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "AddressTypeName",
                schema: "LookUps",
                table: "AddressType",
                nullable: true,
                comment: "Define name of Address type",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AddressTypeId",
                schema: "LookUps",
                table: "AddressType",
                nullable: false,
                comment: "Define identity of Address",
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "NameEn",
                schema: "LookUps",
                table: "Activitytemplate",
                maxLength: 1024,
                nullable: true,
                comment: "Define english name of activity template",
                oldClrType: typeof(string),
                oldType: "nvarchar(1024)",
                oldMaxLength: 1024,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NameAr",
                schema: "LookUps",
                table: "Activitytemplate",
                maxLength: 1024,
                nullable: true,
                comment: "Define arabic name of activity template",
                oldClrType: typeof(string),
                oldType: "nvarchar(1024)",
                oldMaxLength: 1024,
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "HasYears",
                schema: "LookUps",
                table: "Activitytemplate",
                nullable: true,
                comment: "Define activity template has years or not",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ActivitytemplatId",
                schema: "LookUps",
                table: "Activitytemplate",
                nullable: false,
                comment: "Define identity of activity template",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ParentID",
                schema: "LookUps",
                table: "Activity",
                nullable: true,
                comment: "define parent id of activity",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NameEn",
                schema: "LookUps",
                table: "Activity",
                maxLength: 1024,
                nullable: true,
                comment: "define english name of activity",
                oldClrType: typeof(string),
                oldType: "nvarchar(1024)",
                oldMaxLength: 1024,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NameAr",
                schema: "LookUps",
                table: "Activity",
                maxLength: 1024,
                nullable: true,
                comment: "define arabic name of activity",
                oldClrType: typeof(string),
                oldType: "nvarchar(1024)",
                oldMaxLength: 1024,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ActivitytemplateID",
                schema: "LookUps",
                table: "Activity",
                nullable: true,
                comment: "it's a foreign  key that described activity template of activity",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ActivityId",
                schema: "LookUps",
                table: "Activity",
                nullable: false,
                comment: "define identity of activity",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "NameEnglish",
                schema: "Lookups",
                table: "TenderFeesType",
                maxLength: 100,
                nullable: true,
                comment: "Define english name of tender fees type",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NameArabic",
                schema: "Lookups",
                table: "TenderFeesType",
                maxLength: 100,
                nullable: true,
                comment: "Define arabic name of tender fees type",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TenderFeesTypeId",
                schema: "Lookups",
                table: "TenderFeesType",
                nullable: false,
                comment: "Define identity of tender fees type",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "NameEn",
                schema: "Lookups",
                table: "PrePlanningStatus",
                maxLength: 100,
                nullable: true,
                comment: "define english name of preplanning status",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NameAr",
                schema: "Lookups",
                table: "PrePlanningStatus",
                maxLength: 100,
                nullable: true,
                comment: "define arabic name of preplanning status",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "StatusId",
                schema: "Lookups",
                table: "PrePlanningStatus",
                nullable: false,
                comment: "define identity of preplanning status",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "Lookups",
                table: "OfferPresentationWay",
                maxLength: 1024,
                nullable: true,
                comment: "Define the Name Of Offer Presentation Way lookup",
                oldClrType: typeof(string),
                oldType: "nvarchar(1024)",
                oldMaxLength: 1024,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                schema: "Lookups",
                table: "OfferPresentationWay",
                nullable: false,
                comment: "Define Unique identifer Of Offer Presentation Way lookup",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "PlaintReason",
                schema: "CommunicationRequest",
                table: "PlaintRequest",
                maxLength: 1000,
                nullable: true,
                comment: "Define the reason of plaint request",
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "OfferId",
                schema: "CommunicationRequest",
                table: "PlaintRequest",
                nullable: false,
                comment: "Define the related Offer for plaint request",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                schema: "CommunicationRequest",
                table: "PlaintRequest",
                maxLength: 1000,
                nullable: true,
                comment: "Define notes for more details about the plaint request",
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsEscalation",
                schema: "CommunicationRequest",
                table: "PlaintRequest",
                nullable: false,
                comment: "Flag determine if the plaint request has an escalation request or not",
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<int>(
                name: "AgencyCommunicationRequestId",
                schema: "CommunicationRequest",
                table: "PlaintRequest",
                nullable: false,
                comment: "Define the related agency communication request for plaint request",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "PlainRequestId",
                schema: "CommunicationRequest",
                table: "PlaintRequest",
                nullable: false,
                comment: "Define a unique identifer of plaint request",
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<decimal>(
                name: "offerOriginalAmount",
                schema: "CommunicationRequest",
                table: "NegotiationFirstStageSupplier",
                nullable: false,
                comment: "Define The Amount of the offer before deduction",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "SupplierCR",
                schema: "CommunicationRequest",
                table: "NegotiationFirstStageSupplier",
                nullable: true,
                comment: "Define the supplier who will recieved the negotiation request  ",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "PeriodStartDateTime",
                schema: "CommunicationRequest",
                table: "NegotiationFirstStageSupplier",
                nullable: true,
                comment: "Define The start date that the supplier recieved the request ",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "OfferId",
                schema: "CommunicationRequest",
                table: "NegotiationFirstStageSupplier",
                nullable: false,
                comment: "Define The related offer",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "NegotiationSupplierStatusId",
                schema: "CommunicationRequest",
                table: "NegotiationFirstStageSupplier",
                nullable: true,
                comment: "The status of Request for the supplier is accepted from supplier or rejected or still not sent to the supplier",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NegotiationId",
                schema: "CommunicationRequest",
                table: "NegotiationFirstStageSupplier",
                nullable: false,
                comment: "Define The Negotiation Table ",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<bool>(
                name: "IsReported",
                schema: "CommunicationRequest",
                table: "NegotiationFirstStageSupplier",
                nullable: false,
                comment: "Define if the supplier notified that he has Negotiation request",
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                schema: "CommunicationRequest",
                table: "NegotiationFirstStageSupplier",
                nullable: false,
                comment: "Define the Uniqee Identifier for  Negotiation First stage Suppliers List Table",
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<DateTime>(
                name: "PeriodStartDate",
                schema: "CommunicationRequest",
                table: "Negotiation",
                nullable: true,
                comment: "Define The start date that the supplier recieved the request ",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "OfferId",
                schema: "CommunicationRequest",
                table: "Negotiation",
                nullable: true,
                comment: "Define The project number from Etimad Budget",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NegotiationFirstStageId",
                schema: "CommunicationRequest",
                table: "Negotiation",
                nullable: true,
                comment: "Define the Related First Stage Negotiation Request",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProjectNumber",
                schema: "CommunicationRequest",
                table: "Negotiation",
                nullable: true,
                comment: "Define The project number from Etimad Budget",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsNewNegotiation",
                schema: "CommunicationRequest",
                table: "Negotiation",
                nullable: true,
                comment: "define if the negotiation request should take the new flow or old flow",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ExtraDiscountValue",
                schema: "CommunicationRequest",
                table: "Negotiation",
                nullable: true,
                comment: "Define The Amount of the offer after supplier deducted axtra amount from his offer ",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DiscountLetterRefID",
                schema: "CommunicationRequest",
                table: "Negotiation",
                nullable: true,
                comment: "Define the Referance for the Letter File of Negotiation",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "DiscountAmount",
                schema: "CommunicationRequest",
                table: "Negotiation",
                nullable: true,
                comment: "Define The amount which the agency needs to deduct from supplier offer",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SupplierReplyPeriodHours",
                schema: "CommunicationRequest",
                table: "Negotiation",
                nullable: false,
                comment: "the period that the defined for supplier to reply on the negotiation Request ",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "StatusId",
                schema: "CommunicationRequest",
                table: "Negotiation",
                nullable: false,
                comment: "Define the status of Negotiation ",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "RejectionReason",
                schema: "CommunicationRequest",
                table: "Negotiation",
                nullable: true,
                comment: "Define the Note written by the high level employee if he rejected the request ",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NegotiationTypeId",
                schema: "CommunicationRequest",
                table: "Negotiation",
                nullable: false,
                comment: "the type of negotiation [first stage or second stage] Note: this column refer to Negotiations types Table ",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "NegotiationReasonId",
                schema: "CommunicationRequest",
                table: "Negotiation",
                nullable: true,
                comment: "the reason for negotiation Note: this column refer to Negotiations Reasons Table ",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AgencyRequestId",
                schema: "CommunicationRequest",
                table: "Negotiation",
                nullable: false,
                comment: "Communication Request that the negotiation is related To",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "NegotiationId",
                schema: "CommunicationRequest",
                table: "Negotiation",
                nullable: false,
                comment: "Define Identity Of Negotiation Request",
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "PlaintRequestId",
                schema: "CommunicationRequest",
                table: "EscalationRequest",
                nullable: false,
                comment: "Define the related plaint request for the escalation request",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "EscalationNotes",
                schema: "CommunicationRequest",
                table: "EscalationRequest",
                maxLength: 1000,
                nullable: true,
                comment: "Define notes for more details about the escalation request",
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EscalationRequestId",
                schema: "CommunicationRequest",
                table: "EscalationRequest",
                nullable: false,
                comment: "Define a unique identifer of escalation request",
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "TenderPlaintRequestProcedureId",
                schema: "CommunicationRequest",
                table: "AgencyCommunicationRequest",
                nullable: true,
                comment: "Define the action caused by accepting plaint or esclation",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TenderId",
                schema: "CommunicationRequest",
                table: "AgencyCommunicationRequest",
                nullable: false,
                comment: "Define the related tender of agency communication request",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "SupplierExtendOfferDatesRequestId",
                schema: "CommunicationRequest",
                table: "AgencyCommunicationRequest",
                nullable: true,
                comment: "Define the Supplier extend offer dates request Id of agency communication request",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "StatusId",
                schema: "CommunicationRequest",
                table: "AgencyCommunicationRequest",
                nullable: false,
                comment: "Define the status of agency communication request",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "RequestedByRoleName",
                schema: "CommunicationRequest",
                table: "AgencyCommunicationRequest",
                nullable: true,
                comment: "Define the role who create the agency communication request",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RejectionReason",
                schema: "CommunicationRequest",
                table: "AgencyCommunicationRequest",
                maxLength: 1000,
                nullable: true,
                comment: "Define the rejection reason for any request",
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PlaintAcceptanceStatusId",
                schema: "CommunicationRequest",
                table: "AgencyCommunicationRequest",
                nullable: true,
                comment: "Define the plaint status set by secretary of plaint request",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsReported",
                schema: "CommunicationRequest",
                table: "AgencyCommunicationRequest",
                nullable: false,
                comment: "Define if the requeste is reported or not",
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<int>(
                name: "EscalationStatusId",
                schema: "CommunicationRequest",
                table: "AgencyCommunicationRequest",
                nullable: true,
                comment: "Define the esclation status set by manager of esclation request",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EscalationRejectionReason",
                schema: "CommunicationRequest",
                table: "AgencyCommunicationRequest",
                maxLength: 1000,
                nullable: true,
                comment: "Define the rejection reason for the esclation request",
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EscalationAcceptanceStatusId",
                schema: "CommunicationRequest",
                table: "AgencyCommunicationRequest",
                nullable: true,
                comment: "Define the esclation status set by secretary of esclation request",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AgencyRequestTypeId",
                schema: "CommunicationRequest",
                table: "AgencyCommunicationRequest",
                nullable: false,
                comment: "Define the Id of agency communication request type",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "AgencyRequestId",
                schema: "CommunicationRequest",
                table: "AgencyCommunicationRequest",
                nullable: false,
                comment: "Define a unique identifer of agency communication request",
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "NegotiationId",
                schema: "CommunicationAgency",
                table: "NegotiationAttachment",
                nullable: false,
                comment: "Define The related of Negotiation request",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "CommunicationAgency",
                table: "NegotiationAttachment",
                maxLength: 200,
                nullable: false,
                comment: "Define The name of the file",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "FileNetReferenceId",
                schema: "CommunicationAgency",
                table: "NegotiationAttachment",
                maxLength: 1024,
                nullable: true,
                comment: "Define The reference number from file Net",
                oldClrType: typeof(string),
                oldType: "nvarchar(1024)",
                oldMaxLength: 1024,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AttachmentTypeId",
                schema: "CommunicationAgency",
                table: "NegotiationAttachment",
                nullable: false,
                comment: "the type of attachment file like [supplier attachment or agency attachment]",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "AttachmentId",
                schema: "CommunicationAgency",
                table: "NegotiationAttachment",
                nullable: false,
                comment: "Define the unique  identifier for negotiation Attachment",
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "UserRoleId",
                schema: "Committee",
                table: "CommitteeUser",
                nullable: false,
                comment: "it's a forigne key that described user role",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "UserProfileId",
                schema: "Committee",
                table: "CommitteeUser",
                nullable: false,
                comment: "it's a forigne key that described user profile",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                schema: "Committee",
                table: "CommitteeUser",
                maxLength: 256,
                nullable: true,
                comment: "Determine who updated committee user",
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                schema: "Committee",
                table: "CommitteeUser",
                nullable: true,
                comment: "Define updated date of committee user",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RelatedAgencyCode",
                schema: "Committee",
                table: "CommitteeUser",
                nullable: true,
                comment: "Define identity related agencycode",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                schema: "Committee",
                table: "CommitteeUser",
                nullable: true,
                comment: "Define committee user is active or not",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                schema: "Committee",
                table: "CommitteeUser",
                maxLength: 256,
                nullable: true,
                comment: "Determine who cretead committee user",
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "Committee",
                table: "CommitteeUser",
                nullable: false,
                comment: "Define created date of committee user",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<int>(
                name: "CommitteeId",
                schema: "Committee",
                table: "CommitteeUser",
                nullable: false,
                comment: "it's a forigne key that described related committe",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CommitteeUserId",
                schema: "Committee",
                table: "CommitteeUser",
                nullable: false,
                comment: "Define identity of committee user",
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "ZipCode",
                schema: "Committee",
                table: "Committee",
                maxLength: 1024,
                nullable: true,
                comment: "Define zip code of committee",
                oldClrType: typeof(string),
                oldType: "nvarchar(1024)",
                oldMaxLength: 1024,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                schema: "Committee",
                table: "Committee",
                maxLength: 256,
                nullable: true,
                comment: "Determine who updated committee",
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                schema: "Committee",
                table: "Committee",
                nullable: true,
                comment: "Define updated date of committee",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PostalCode",
                schema: "Committee",
                table: "Committee",
                maxLength: 1024,
                nullable: true,
                comment: "Define postal code of committee",
                oldClrType: typeof(string),
                oldType: "nvarchar(1024)",
                oldMaxLength: 1024,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                schema: "Committee",
                table: "Committee",
                maxLength: 100,
                nullable: true,
                comment: "Define phone of committee",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                schema: "Committee",
                table: "Committee",
                nullable: true,
                comment: "Define committee is active or not",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Fax",
                schema: "Committee",
                table: "Committee",
                maxLength: 100,
                nullable: true,
                comment: "Define fax of committee",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                schema: "Committee",
                table: "Committee",
                maxLength: 1024,
                nullable: true,
                comment: "Define e-mail of committee",
                oldClrType: typeof(string),
                oldType: "nvarchar(1024)",
                oldMaxLength: 1024,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                schema: "Committee",
                table: "Committee",
                maxLength: 256,
                nullable: true,
                comment: "Determine who cretead committee",
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "Committee",
                table: "Committee",
                nullable: false,
                comment: "Define created date of committee",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<int>(
                name: "CommitteeTypeId",
                schema: "Committee",
                table: "Committee",
                nullable: false,
                comment: "it's a forigne key  that describe committe type",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "CommitteeName",
                schema: "Committee",
                table: "Committee",
                maxLength: 1024,
                nullable: true,
                comment: "Define  name of committee",
                oldClrType: typeof(string),
                oldType: "nvarchar(1024)",
                oldMaxLength: 1024,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AgencyCode",
                schema: "Committee",
                table: "Committee",
                nullable: true,
                comment: "Define agency code of committee",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                schema: "Committee",
                table: "Committee",
                maxLength: 1024,
                nullable: true,
                comment: "Define address of committee",
                oldClrType: typeof(string),
                oldType: "nvarchar(1024)",
                oldMaxLength: 1024,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CommitteeId",
                schema: "Committee",
                table: "Committee",
                nullable: false,
                comment: "Define identity of committee",
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                schema: "AnnouncementTemplate",
                table: "AnnouncementTemplateJoinRequestAttachment",
                maxLength: 256,
                nullable: true,
                comment: "Determine who updated attachment for announcement supplier template",
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                schema: "AnnouncementTemplate",
                table: "AnnouncementTemplateJoinRequestAttachment",
                nullable: true,
                comment: "Define updated date of attachment for announcement supplier template",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "AnnouncementTemplate",
                table: "AnnouncementTemplateJoinRequestAttachment",
                maxLength: 200,
                nullable: false,
                comment: "Define name of announcement supplier template attachment",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<int>(
                name: "JoinRequestSupplierTemplateId",
                schema: "AnnouncementTemplate",
                table: "AnnouncementTemplateJoinRequestAttachment",
                nullable: false,
                comment: "Define forigne key of join request announcement supplier template",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                schema: "AnnouncementTemplate",
                table: "AnnouncementTemplateJoinRequestAttachment",
                nullable: true,
                comment: "Define attachment is active or not",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FileNetReferenceId",
                schema: "AnnouncementTemplate",
                table: "AnnouncementTemplateJoinRequestAttachment",
                maxLength: 1024,
                nullable: true,
                comment: "Define file net id",
                oldClrType: typeof(string),
                oldType: "nvarchar(1024)",
                oldMaxLength: 1024,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                schema: "AnnouncementTemplate",
                table: "AnnouncementTemplateJoinRequestAttachment",
                maxLength: 256,
                nullable: true,
                comment: "Determine who cretead attachment for announcement supplier template",
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "AnnouncementTemplate",
                table: "AnnouncementTemplateJoinRequestAttachment",
                nullable: false,
                comment: "Define created date of attachment for announcement supplier template",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                schema: "AnnouncementTemplate",
                table: "AnnouncementTemplateJoinRequestAttachment",
                nullable: false,
                comment: "Define identity of announcement supplier template attachment",
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSupplierTemplate",
                maxLength: 256,
                nullable: true,
                comment: "Determine who updated announcement supplier templat",
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSupplierTemplate",
                nullable: true,
                comment: "Define updated date for announcement list",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TenderTypesId",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSupplierTemplate",
                nullable: true,
                comment: "Define type of tender",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TemplateExtendMechanism",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSupplierTemplate",
                nullable: true,
                comment: "Define mechanism to extend the list",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "StatusId",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSupplierTemplate",
                nullable: false,
                comment: "Define status of announcement supplier template",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "RequirementConditionsToJoinList",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSupplierTemplate",
                nullable: true,
                comment: "Define conditions necessary to join the list",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RequiredAttachment",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSupplierTemplate",
                nullable: true,
                comment: "Define required attachment of announcement list",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ReferenceNumber",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSupplierTemplate",
                maxLength: 100,
                nullable: true,
                comment: "Define reference number of announcement supplier template, its a unique identifier like announcement identity",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ReadyForApproval",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSupplierTemplate",
                nullable: true,
                comment: "Define status for announcement list is ready for approval or not ",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "PublishedDate",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSupplierTemplate",
                nullable: true,
                comment: "Define published date for announcement supplier template",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsRequiredAttachmentToJoinList",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSupplierTemplate",
                nullable: false,
                comment: "Define required attachment of announcement list  is required or not",
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<bool>(
                name: "IsEffectiveList",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSupplierTemplate",
                nullable: true,
                comment: "Define effective date of announcement list  is required or not",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSupplierTemplate",
                nullable: true,
                comment: "Define announcement template list is active or not ",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IntroAboutAnnouncementTemplate",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSupplierTemplate",
                maxLength: 5000,
                nullable: true,
                comment: "Define Intro about  Announcement Supplier Template",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldMaxLength: 5000,
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "InsidKsa",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSupplierTemplate",
                nullable: false,
                comment: "Define excution place  of  Announcement Supplier Template",
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EffectiveListDate",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSupplierTemplate",
                nullable: true,
                comment: "Define Effective date for announcement list",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Details",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSupplierTemplate",
                maxLength: 5000,
                nullable: true,
                comment: "Define details of  Announcement Supplier Template",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldMaxLength: 5000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Descriptions",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSupplierTemplate",
                maxLength: 5000,
                nullable: true,
                comment: "Define descriptions of  Announcement Supplier Template",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldMaxLength: 5000,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CreatedById",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSupplierTemplate",
                nullable: true,
                comment: "Determine who cretead announcement supplier template",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSupplierTemplate",
                maxLength: 256,
                nullable: true,
                comment: "Determine who cretead announcement supplier template",
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSupplierTemplate",
                nullable: false,
                comment: "Define created date for announcement list",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "CancelationReason",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSupplierTemplate",
                nullable: true,
                comment: "Define cancelation reason  of announcement list",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BranchId",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSupplierTemplate",
                nullable: true,
                comment: "Define branch of announcement supplier template",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ApprovedBy",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSupplierTemplate",
                maxLength: 200,
                nullable: true,
                comment: "Determine who approved announcement supplier template",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AnnouncementTemplateSuppliersListTypeId",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSupplierTemplate",
                nullable: true,
                comment: "Define the  type of announcement supplier template list that public or private ",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AnnouncementName",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSupplierTemplate",
                maxLength: 1024,
                nullable: true,
                comment: "Define Name Of Announcement Supplier Template",
                oldClrType: typeof(string),
                oldType: "nvarchar(1024)",
                oldMaxLength: 1024,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AgencyPhone",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSupplierTemplate",
                nullable: true,
                comment: "Define agency phone  for announcement supplier template",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AgencyFax",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSupplierTemplate",
                nullable: true,
                comment: "Define agency fax  for announcement supplier template",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AgencyEmail",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSupplierTemplate",
                nullable: true,
                comment: "Define agency email  for announcement supplier template",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AgencyCode",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSupplierTemplate",
                maxLength: 20,
                nullable: true,
                comment: "Define agency code for announcement supplier template",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AgencyAddress",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSupplierTemplate",
                nullable: true,
                comment: "Define agency address  for announcement supplier template",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ActivityDescription",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSupplierTemplate",
                maxLength: 2000,
                nullable: true,
                comment: "Define  description of activity for announcement supplier template",
                oldClrType: typeof(string),
                oldType: "nvarchar(2000)",
                oldMaxLength: 2000,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AnnouncementId",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSupplierTemplate",
                nullable: false,
                comment: "Define Identity Of Announcement Supplier Template",
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSuppliersTemplateAttachment",
                maxLength: 256,
                nullable: true,
                comment: "Determine who updated attachment for announcement supplier template",
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSuppliersTemplateAttachment",
                nullable: true,
                comment: "Define updated date of attachment for announcement supplier template",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ReviewStatusId",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSuppliersTemplateAttachment",
                nullable: true,
                comment: "Define review status id of attachment for announcement supplier template",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RejectionReason",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSuppliersTemplateAttachment",
                maxLength: 1024,
                nullable: true,
                comment: "Define rejection reason of attachment for announcement supplier template",
                oldClrType: typeof(string),
                oldType: "nvarchar(1024)",
                oldMaxLength: 1024,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSuppliersTemplateAttachment",
                maxLength: 200,
                nullable: false,
                comment: "Define name of announcement supplier template attachment",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSuppliersTemplateAttachment",
                nullable: true,
                comment: "Define attachment is active or not",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FileNetReferenceId",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSuppliersTemplateAttachment",
                maxLength: 1024,
                nullable: true,
                comment: "Define file net id",
                oldClrType: typeof(string),
                oldType: "nvarchar(1024)",
                oldMaxLength: 1024,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSuppliersTemplateAttachment",
                maxLength: 256,
                nullable: true,
                comment: "Determine who cretead attachment for announcement supplier template",
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSuppliersTemplateAttachment",
                nullable: false,
                comment: "Define created date of attachment for announcement supplier template",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<int>(
                name: "ChangeStatusId",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSuppliersTemplateAttachment",
                nullable: true,
                comment: "Define change status id of attachment for announcement supplier template",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AttachmentTypeId",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSuppliersTemplateAttachment",
                nullable: false,
                comment: "Define type of  attachment",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "AnnouncementId",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSuppliersTemplateAttachment",
                nullable: false,
                comment: "Define forigne key of announcement supplier template",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "AnnouncementSuppliersTemplateAttachmentId",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSuppliersTemplateAttachment",
                nullable: false,
                comment: "Define identity of announcement supplier template attachment",
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "AnnouncementTemplate",
                table: "AnnouncementStatusSupplierTemplate",
                maxLength: 1024,
                nullable: true,
                comment: "Define name of status supplier template",
                oldClrType: typeof(string),
                oldType: "nvarchar(1024)",
                oldMaxLength: 1024,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                schema: "AnnouncementTemplate",
                table: "AnnouncementStatusSupplierTemplate",
                nullable: false,
                comment: "Define identity of announcement status supplier template",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                schema: "AnnouncementTemplate",
                table: "AnnouncementMaintenanceRunnigWorkSupplierTemplate",
                maxLength: 256,
                nullable: true,
                comment: "Determine who updated announcement maintenance runnig work",
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                schema: "AnnouncementTemplate",
                table: "AnnouncementMaintenanceRunnigWorkSupplierTemplate",
                nullable: true,
                comment: "Define updated date for announcement maintenance runnig work",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MaintenanceRunningWorkId",
                schema: "AnnouncementTemplate",
                table: "AnnouncementMaintenanceRunnigWorkSupplierTemplate",
                nullable: false,
                comment: "Define forigne key of maintenance running work",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                schema: "AnnouncementTemplate",
                table: "AnnouncementMaintenanceRunnigWorkSupplierTemplate",
                nullable: true,
                comment: "Define announcement maintenance runnig work is active or not",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                schema: "AnnouncementTemplate",
                table: "AnnouncementMaintenanceRunnigWorkSupplierTemplate",
                maxLength: 256,
                nullable: true,
                comment: "Determine who cretead announcement maintenance runnig work",
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "AnnouncementTemplate",
                table: "AnnouncementMaintenanceRunnigWorkSupplierTemplate",
                nullable: false,
                comment: "Define created date for announcement maintenance runnig work",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<int>(
                name: "AnnouncementId",
                schema: "AnnouncementTemplate",
                table: "AnnouncementMaintenanceRunnigWorkSupplierTemplate",
                nullable: false,
                comment: "Define forigne key  of announcement supplier template",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                schema: "AnnouncementTemplate",
                table: "AnnouncementMaintenanceRunnigWorkSupplierTemplate",
                nullable: false,
                comment: "Define Identity of announcement maintenance runnig work supplier template",
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "Announcement",
                table: "AnnouncementStatus",
                maxLength: 1024,
                nullable: true,
                comment: "Define name of anouncement status",
                oldClrType: typeof(string),
                oldType: "nvarchar(1024)",
                oldMaxLength: 1024,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                schema: "Announcement",
                table: "AnnouncementStatus",
                nullable: false,
                comment: "Define identity of announcement status",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                schema: "Announcement",
                table: "AnnouncementMaintenanceRunnigWork",
                maxLength: 256,
                nullable: true,
                comment: "Determine who updated announcement maintenance runnig work",
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                schema: "Announcement",
                table: "AnnouncementMaintenanceRunnigWork",
                nullable: true,
                comment: "Define updated date for announcement maintenance runnig work",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MaintenanceRunningWorkId",
                schema: "Announcement",
                table: "AnnouncementMaintenanceRunnigWork",
                nullable: false,
                comment: "Define forigne key of maintenance running work",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                schema: "Announcement",
                table: "AnnouncementMaintenanceRunnigWork",
                nullable: true,
                comment: "Define announcement maintenance runnig work is active or not",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                schema: "Announcement",
                table: "AnnouncementMaintenanceRunnigWork",
                maxLength: 256,
                nullable: true,
                comment: "Determine who cretead announcement maintenance runnig work",
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "Announcement",
                table: "AnnouncementMaintenanceRunnigWork",
                nullable: false,
                comment: "Define created date for announcement maintenance runnig work",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<int>(
                name: "AnnouncementId",
                schema: "Announcement",
                table: "AnnouncementMaintenanceRunnigWork",
                nullable: false,
                comment: "Define forigne key of announcement",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                schema: "Announcement",
                table: "AnnouncementMaintenanceRunnigWork",
                nullable: false,
                comment: "Define Identity of announcement maintenance runnig work",
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2021, 2, 16, 8, 13, 41, 641, DateTimeKind.Local).AddTicks(6090));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2021, 2, 16, 8, 13, 41, 641, DateTimeKind.Local).AddTicks(7475));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2021, 2, 16, 8, 13, 41, 641, DateTimeKind.Local).AddTicks(7510));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2021, 2, 16, 8, 13, 41, 641, DateTimeKind.Local).AddTicks(7511));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2021, 2, 16, 8, 13, 41, 641, DateTimeKind.Local).AddTicks(7513));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2021, 2, 16, 8, 13, 41, 641, DateTimeKind.Local).AddTicks(7514));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2021, 2, 16, 8, 13, 41, 641, DateTimeKind.Local).AddTicks(7515));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2021, 2, 16, 8, 13, 41, 641, DateTimeKind.Local).AddTicks(7516));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2021, 2, 16, 8, 13, 41, 641, DateTimeKind.Local).AddTicks(7518));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2021, 2, 16, 8, 13, 41, 641, DateTimeKind.Local).AddTicks(7519));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2021, 2, 16, 8, 13, 41, 641, DateTimeKind.Local).AddTicks(7520));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2021, 2, 16, 8, 13, 41, 641, DateTimeKind.Local).AddTicks(7521));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2021, 2, 16, 8, 13, 41, 641, DateTimeKind.Local).AddTicks(7522));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "UserRole",
                keyColumn: "UserRoleId",
                keyValue: 100,
                column: "IsCombinedRole",
                value: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterTable(
                name: "OfferSolidarity",
                schema: "Offer",
                oldComment: "Describe the Data related to offer solidarity ");

            migrationBuilder.AlterTable(
                name: "OfferHistory",
                schema: "Offer",
                oldComment: "Represent  Offer Data ");

            migrationBuilder.AlterTable(
                name: "Offer",
                schema: "Offer",
                oldComment: "Represent  Offer Data ");

            migrationBuilder.AlterTable(
                name: "OfferStatus",
                schema: "LookUps",
                oldComment: "Define the Offer status lookup");

            migrationBuilder.AlterTable(
                name: "OfferSolidarityStatus",
                schema: "LookUps",
                oldComment: "Define the Offer Solidarity  status lookup");

            migrationBuilder.AlterTable(
                name: "NegotiationType",
                schema: "LookUps",
                oldComment: "Describe the negotiation types");

            migrationBuilder.AlterTable(
                name: "NegotiationReason",
                schema: "LookUps",
                oldComment: "Describe the negotiation reason");

            migrationBuilder.AlterTable(
                name: "OfferPresentationWay",
                schema: "Lookups",
                oldComment: "Define the Offer Presentation Way lookup");

            migrationBuilder.AlterTable(
                name: "NegotiationFirstStageSupplier",
                schema: "CommunicationRequest",
                oldComment: "Describe first Stage Negotiation Suppliers List");

            migrationBuilder.AlterTable(
                name: "Negotiation",
                schema: "CommunicationRequest",
                oldComment: "Describe first Stage Negotiation");

            migrationBuilder.AlterTable(
                name: "NegotiationAttachment",
                schema: "CommunicationAgency",
                oldComment: "Describe the Negotiation Attachment");

            migrationBuilder.AlterColumn<int>(
                name: "VRORelatedBranchId",
                schema: "Tender",
                table: "Tender",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true,
                oldComment: "Branch assigned to vro");

            migrationBuilder.AlterColumn<int>(
                name: "VROCommitteeId",
                schema: "Tender",
                table: "Tender",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true,
                oldComment: "The committe of VRO for opening, checking and awarding tender");

            migrationBuilder.AlterColumn<bool>(
                name: "UnitSpacialistWouldLikeToAttendTheCommitte",
                schema: "Tender",
                table: "Tender",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldNullable: true,
                oldComment: "Determine if the unit manger want to attend th committee");

            migrationBuilder.AlterColumn<int>(
                name: "TenderUnitStatusId",
                schema: "Tender",
                table: "Tender",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true,
                oldComment: "The status of tender at unit review");

            migrationBuilder.AlterColumn<int>(
                name: "TenderTypeId",
                schema: "Tender",
                table: "Tender",
                type: "int",
                nullable: false,
                comment: "Define Type Of Tender",
                oldClrType: typeof(int),
                oldComment: "Type of tender");

            migrationBuilder.AlterColumn<int>(
                name: "TenderStatusId",
                schema: "Tender",
                table: "Tender",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Status of tender");

            migrationBuilder.AlterColumn<decimal>(
                name: "TenderPointsToPass",
                schema: "Tender",
                table: "Tender",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldComment: "The points needed to pass a prequalification");

            migrationBuilder.AlterColumn<int>(
                name: "TenderFirstStageId",
                schema: "Tender",
                table: "Tender",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true,
                oldComment: "The identity of tender of type first stage");

            migrationBuilder.AlterColumn<int>(
                name: "TenderConditionsTemplateId",
                schema: "Tender",
                table: "Tender",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true,
                oldComment: "The Activity template of tender");

            migrationBuilder.AlterColumn<int>(
                name: "TemplateYears",
                schema: "Tender",
                table: "Tender",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true,
                oldComment: "number of years in quantities tables");

            migrationBuilder.AlterColumn<int>(
                name: "TechnicalOrganizationId",
                schema: "Tender",
                table: "Tender",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true,
                oldComment: "The technical committee for reply on suppliers enquireis");

            migrationBuilder.AlterColumn<decimal>(
                name: "TechnicalAdministrativeCapacity",
                schema: "Tender",
                table: "Tender",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldComment: "Technical administrative capacity");

            migrationBuilder.AlterColumn<int>(
                name: "SpendingCategoryId",
                schema: "Tender",
                table: "Tender",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true,
                oldComment: "Define the spending Category of the tender");

            migrationBuilder.AlterColumn<int>(
                name: "ReasonForPurchaseTenderTypeId",
                schema: "Tender",
                table: "Tender",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true,
                oldComment: "The reason of selecting direct purchase tender");

            migrationBuilder.AlterColumn<int>(
                name: "ReasonForLimitedTenderTypeId",
                schema: "Tender",
                table: "Tender",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true,
                oldComment: "The reason of selecting limited tender");

            migrationBuilder.AlterColumn<int>(
                name: "QuantityTableVersionId",
                schema: "Tender",
                table: "Tender",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true,
                oldComment: "The version of quantity table");

            migrationBuilder.AlterColumn<int>(
                name: "QualificationTypeId",
                schema: "Tender",
                table: "Tender",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true,
                oldComment: "determine the type of qualification post or prequalification");

            migrationBuilder.AlterColumn<int>(
                name: "PreviousFramWorkId",
                schema: "Tender",
                table: "Tender",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true,
                oldComment: "The identity that identify the previous framework agreament");

            migrationBuilder.AlterColumn<int>(
                name: "PreQualificationId",
                schema: "Tender",
                table: "Tender",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true,
                oldComment: "The identity that identify pre qualification on the tender");

            migrationBuilder.AlterColumn<int>(
                name: "PreQualificationCommitteeId",
                schema: "Tender",
                table: "Tender",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true,
                oldComment: "Define PreQualification Committee");

            migrationBuilder.AlterColumn<int>(
                name: "PreAnnouncementId",
                schema: "Tender",
                table: "Tender",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true,
                oldComment: "Tha pre-announcement related to tender");

            migrationBuilder.AlterColumn<int>(
                name: "PostQualificationTenderId",
                schema: "Tender",
                table: "Tender",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true,
                oldComment: "The identity that identify post qualification on the tender");

            migrationBuilder.AlterColumn<bool>(
                name: "OpeningNotificationSent",
                schema: "Tender",
                table: "Tender",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldNullable: true,
                oldComment: "Flag that states that a notification is sent to the opening committee when the opening date is passed");

            migrationBuilder.AlterColumn<int>(
                name: "OffersOpeningCommitteeId",
                schema: "Tender",
                table: "Tender",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true,
                oldComment: "The opening committee for opening offers");

            migrationBuilder.AlterColumn<int>(
                name: "OffersOpeningAddressId",
                schema: "Tender",
                table: "Tender",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true,
                oldComment: "The Identity that identify address of open tender offers");

            migrationBuilder.AlterColumn<int>(
                name: "OffersCheckingCommitteeId",
                schema: "Tender",
                table: "Tender",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true,
                oldComment: "The Checking committee for checking and awarding tender");

            migrationBuilder.AlterColumn<int>(
                name: "OfferPresentationWayId",
                schema: "Tender",
                table: "Tender",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true,
                oldComment: "method of apply offers by suppliers one file or two files");

            migrationBuilder.AlterColumn<int>(
                name: "NumberOfWinners",
                schema: "Tender",
                table: "Tender",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true,
                oldComment: "Number of winners in the competition");

            migrationBuilder.AlterColumn<decimal>(
                name: "NationalProductPercentage",
                schema: "Tender",
                table: "Tender",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldNullable: true,
                oldComment: "The percentage of National Product");

            migrationBuilder.AlterColumn<bool>(
                name: "IsSendToEmarketPlace",
                schema: "Tender",
                table: "Tender",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldNullable: true,
                oldComment: "Flag determine if the awarded tender is sent to e-market place or not");

            migrationBuilder.AlterColumn<bool>(
                name: "IsNotificationSentForStoppingPeriod",
                schema: "Tender",
                table: "Tender",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldNullable: true,
                oldComment: "Flag determine if a notification sent after finishing the stoping period of tender");

            migrationBuilder.AlterColumn<bool>(
                name: "IsLowBudgetDirectPurchase",
                schema: "Tender",
                table: "Tender",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldNullable: true,
                oldComment: "determine if the tender is low budget or not if the estimatinn value less than 30000 sar");

            migrationBuilder.AlterColumn<int>(
                name: "InvitationTypeId",
                schema: "Tender",
                table: "Tender",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true,
                oldComment: "Type of invitation on tender public or private");

            migrationBuilder.AlterColumn<bool>(
                name: "HasGuarantee",
                schema: "Tender",
                table: "Tender",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldNullable: true,
                oldComment: "Boolean determine if the tender requires a final guarantee or not");

            migrationBuilder.AlterColumn<bool>(
                name: "HasAlternativeOffer",
                schema: "Tender",
                table: "Tender",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldNullable: true,
                oldComment: "determine if the supliers can apply an alternative offers");

            migrationBuilder.AlterColumn<string>(
                name: "FloarNumber",
                schema: "Tender",
                table: "Tender",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true,
                oldComment: "Floar number at buliding for samples delivery");

            migrationBuilder.AlterColumn<bool>(
                name: "FinancialCheckingDateSet",
                schema: "Tender",
                table: "Tender",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldNullable: true,
                oldComment: "Date set by the checking secretary at the start of the financial checking");

            migrationBuilder.AlterColumn<decimal>(
                name: "FinancialCapacity",
                schema: "Tender",
                table: "Tender",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldComment: "Financial Capacity");

            migrationBuilder.AlterColumn<string>(
                name: "FinalGuaranteeDeliveryAddress",
                schema: "Tender",
                table: "Tender",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 500,
                oldNullable: true,
                oldComment: "The address of deleviry the final guarantee");

            migrationBuilder.AlterColumn<int>(
                name: "DirectPurchaseMemberId",
                schema: "Tender",
                table: "Tender",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true,
                oldComment: "The user responsible for low budget direct purchase");

            migrationBuilder.AlterColumn<int>(
                name: "DirectPurchaseCommitteeId",
                schema: "Tender",
                table: "Tender",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true,
                oldComment: "The direct purchase committee for checking and awarding tender from type direct purchase");

            migrationBuilder.AlterColumn<string>(
                name: "DepartmentName",
                schema: "Tender",
                table: "Tender",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true,
                oldComment: "Department name at buliding for samples delivery");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeliveryDate",
                schema: "Tender",
                table: "Tender",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldComment: "The date of samples delivey to supplier");

            migrationBuilder.AlterColumn<int>(
                name: "CreatedByTypeId",
                schema: "Tender",
                table: "Tender",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true,
                oldComment: "determine the tender created by vro or agency or agency related by vro");

            migrationBuilder.AlterColumn<int>(
                name: "ConditionTemplateStageStatusId",
                schema: "Tender",
                table: "Tender",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true,
                oldComment: "The Status of condition template stage");

            migrationBuilder.AlterColumn<bool>(
                name: "CheckingNotificationSent",
                schema: "Tender",
                table: "Tender",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldNullable: true,
                oldComment: "Flag that states that a notification is sent to the direct purchase committee when the checking date is tomorrow");

            migrationBuilder.AlterColumn<bool>(
                name: "CheckingDateSet",
                schema: "Tender",
                table: "Tender",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldNullable: true,
                oldComment: "Is a date set when starting the checking stage");

            migrationBuilder.AlterColumn<string>(
                name: "BuildingName",
                schema: "Tender",
                table: "Tender",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true,
                oldComment: "The location of building name for samples delivery");

            migrationBuilder.AlterColumn<int>(
                name: "BranchId",
                schema: "Tender",
                table: "Tender",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "The branch that create a tender");

            migrationBuilder.AlterColumn<decimal>(
                name: "BonusValue",
                schema: "Tender",
                table: "Tender",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldNullable: true,
                oldComment: "Bonus value of competition");

            migrationBuilder.AlterColumn<int>(
                name: "AwardingMonths",
                schema: "Tender",
                table: "Tender",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true,
                oldComment: "Final guarantee duration in months");

            migrationBuilder.AlterColumn<int>(
                name: "AwardingDiscountPercentage",
                schema: "Tender",
                table: "Tender",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true,
                oldComment: "Value of the final guarantee");

            migrationBuilder.AlterColumn<int>(
                name: "AnnouncementTemplateId",
                schema: "Tender",
                table: "Tender",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true,
                oldComment: "Tha announcement list of suppliers related to tender");

            migrationBuilder.AlterColumn<int>(
                name: "AgreementYears",
                schema: "Tender",
                table: "Tender",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true,
                oldComment: "Number of years for framework agreement");

            migrationBuilder.AlterColumn<int>(
                name: "AgreementTypeId",
                schema: "Tender",
                table: "Tender",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true,
                oldComment: "The identity that identify the type framework agreament opened or closed");

            migrationBuilder.AlterColumn<int>(
                name: "AgreementMonthes",
                schema: "Tender",
                table: "Tender",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true,
                oldComment: "Number of months for framework agreement");

            migrationBuilder.AlterColumn<int>(
                name: "AgreementDays",
                schema: "Tender",
                table: "Tender",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true,
                oldComment: "Number of days for framework agreement");

            migrationBuilder.AlterColumn<string>(
                name: "AgencyCode",
                schema: "Tender",
                table: "Tender",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "The Agency code of agency that create a tender");

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                schema: "Tender",
                table: "ConditionsBooklet",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 256,
                oldNullable: true,
                oldComment: "Determine who updated request for purchased the condition booklet");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                schema: "Tender",
                table: "ConditionsBooklet",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldComment: "Define updated date of purchased the condition booklet");

            migrationBuilder.AlterColumn<int>(
                name: "TenderId",
                schema: "Tender",
                table: "ConditionsBooklet",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "it's a forigne key that described tender related to condtion booklet");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                schema: "Tender",
                table: "ConditionsBooklet",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldNullable: true,
                oldComment: "Define condition booklet user is active or not");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                schema: "Tender",
                table: "ConditionsBooklet",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 256,
                oldNullable: true,
                oldComment: "Determine who cretead request for purchased the condition booklet");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "Tender",
                table: "ConditionsBooklet",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldComment: "Define created date of purchased the condition booklet");

            migrationBuilder.AlterColumn<string>(
                name: "CommericalRegisterNo",
                schema: "Tender",
                table: "ConditionsBooklet",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "it's described supplier commerical number that who purchased the condition booklet");

            migrationBuilder.AlterColumn<int>(
                name: "ConditionsBookletId",
                schema: "Tender",
                table: "ConditionsBooklet",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define identity of conditions booklet")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "BranchId",
                schema: "Tender",
                table: "Address",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true,
                oldComment: "Define forigne key of branch");

            migrationBuilder.AlterColumn<int>(
                name: "AddressTypeId",
                schema: "Tender",
                table: "Address",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define type of Address");

            migrationBuilder.AlterColumn<string>(
                name: "AddressName",
                schema: "Tender",
                table: "Address",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1024,
                oldNullable: true,
                oldComment: "Define name of Address");

            migrationBuilder.AlterColumn<int>(
                name: "AddressId",
                schema: "Tender",
                table: "Address",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define identity of Address")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                schema: "PrePlanning",
                table: "PrePlanningProjectType",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 256,
                oldNullable: true,
                oldComment: "Determine who updated pre-planning project type");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                schema: "PrePlanning",
                table: "PrePlanningProjectType",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldComment: "Define updated date of pre-planning project type");

            migrationBuilder.AlterColumn<int>(
                name: "PrePlanningId",
                schema: "PrePlanning",
                table: "PrePlanningProjectType",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "it's a foreign  key that described pre-planning ");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                schema: "PrePlanning",
                table: "PrePlanningProjectType",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldNullable: true,
                oldComment: "Define pre-planning project type is active or not");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                schema: "PrePlanning",
                table: "PrePlanningProjectType",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 256,
                oldNullable: true,
                oldComment: "Determine who cretead pre-planning project type");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "PrePlanning",
                table: "PrePlanningProjectType",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldComment: "Define created date of pre-planning project type");

            migrationBuilder.AlterColumn<int>(
                name: "ActivityId",
                schema: "PrePlanning",
                table: "PrePlanningProjectType",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "it's a foreign  key that described activity of pre-planning project type");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                schema: "PrePlanning",
                table: "PrePlanningProjectType",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "define identity of pre-planning project type")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                schema: "PrePlanning",
                table: "PrePlanningCountry",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 256,
                oldNullable: true,
                oldComment: "Determine who updated pre-planning country");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                schema: "PrePlanning",
                table: "PrePlanningCountry",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldComment: "Define updated date of pre-planning country");

            migrationBuilder.AlterColumn<int>(
                name: "PrePlanningId",
                schema: "PrePlanning",
                table: "PrePlanningCountry",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "it's a foreign  key that described pre-planning ");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                schema: "PrePlanning",
                table: "PrePlanningCountry",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldNullable: true,
                oldComment: "Define pre-planning country is active or not");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                schema: "PrePlanning",
                table: "PrePlanningCountry",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 256,
                oldNullable: true,
                oldComment: "Determine who cretead pre-planning country");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "PrePlanning",
                table: "PrePlanningCountry",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldComment: "Define created date of pre-planning country");

            migrationBuilder.AlterColumn<int>(
                name: "CountryId",
                schema: "PrePlanning",
                table: "PrePlanningCountry",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "it's a foreign  key that described country related to pre-planning");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                schema: "PrePlanning",
                table: "PrePlanningCountry",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "define identity of pre-planning country")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                schema: "PrePlanning",
                table: "PrePlanningArea",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 256,
                oldNullable: true,
                oldComment: "Determine who updated pre-planning area");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                schema: "PrePlanning",
                table: "PrePlanningArea",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldComment: "Define updated date of pre-planning area");

            migrationBuilder.AlterColumn<int>(
                name: "PrePlanningId",
                schema: "PrePlanning",
                table: "PrePlanningArea",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "it's a foreign  key that described  pre-planning ");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                schema: "PrePlanning",
                table: "PrePlanningArea",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldNullable: true,
                oldComment: "Define pre-planning area is active or not");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                schema: "PrePlanning",
                table: "PrePlanningArea",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 256,
                oldNullable: true,
                oldComment: "Determine who cretead pre-planning area");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "PrePlanning",
                table: "PrePlanningArea",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldComment: "Define created date of pre-planning area");

            migrationBuilder.AlterColumn<int>(
                name: "AreaId",
                schema: "PrePlanning",
                table: "PrePlanningArea",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "it's a foreign  key that described area related to pre-planning");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                schema: "PrePlanning",
                table: "PrePlanningArea",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "define identity of pre-planning area")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "YearQuarterId",
                schema: "PrePlanning",
                table: "PrePlanning",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "define pre-planning year quarter");

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                schema: "PrePlanning",
                table: "PrePlanning",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 256,
                oldNullable: true,
                oldComment: "Determine who updated pre-planning");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                schema: "PrePlanning",
                table: "PrePlanning",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldComment: "Define updated date of pre-planning");

            migrationBuilder.AlterColumn<int>(
                name: "StatusId",
                schema: "PrePlanning",
                table: "PrePlanning",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true,
                oldComment: "define pre-planning status");

            migrationBuilder.AlterColumn<string>(
                name: "RejectionReason",
                schema: "PrePlanning",
                table: "PrePlanning",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 500,
                oldNullable: true,
                oldComment: "define reject reason of pre-planning");

            migrationBuilder.AlterColumn<string>(
                name: "ProjectNature",
                schema: "PrePlanning",
                table: "PrePlanning",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 500,
                oldNullable: true,
                oldComment: "define project nature of pre-planning");

            migrationBuilder.AlterColumn<string>(
                name: "ProjectName",
                schema: "PrePlanning",
                table: "PrePlanning",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 200,
                oldNullable: true,
                oldComment: "define project name of pre-planning");

            migrationBuilder.AlterColumn<string>(
                name: "ProjectDescription",
                schema: "PrePlanning",
                table: "PrePlanning",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 500,
                oldNullable: true,
                oldComment: "define project description of pre-planning");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleteRequested",
                schema: "PrePlanning",
                table: "PrePlanning",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldComment: "define pre-planning has deleted request or not");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                schema: "PrePlanning",
                table: "PrePlanning",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldNullable: true,
                oldComment: "Define pre-planning is active or not");

            migrationBuilder.AlterColumn<bool>(
                name: "InsideKSA",
                schema: "PrePlanning",
                table: "PrePlanning",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldNullable: true,
                oldComment: "define pre-planning project is inside ksa or not");

            migrationBuilder.AlterColumn<int>(
                name: "DurationInYears",
                schema: "PrePlanning",
                table: "PrePlanning",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true,
                oldComment: "define pre-planning duration in years");

            migrationBuilder.AlterColumn<int>(
                name: "DurationInMonths",
                schema: "PrePlanning",
                table: "PrePlanning",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true,
                oldComment: "define pre-planning duration in month");

            migrationBuilder.AlterColumn<int>(
                name: "DurationInDays",
                schema: "PrePlanning",
                table: "PrePlanning",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true,
                oldComment: "define pre-planning duration in days");

            migrationBuilder.AlterColumn<string>(
                name: "Duration",
                schema: "PrePlanning",
                table: "PrePlanning",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true,
                oldComment: "define pre-planning duration");

            migrationBuilder.AlterColumn<string>(
                name: "DeleteRejectionReason",
                schema: "PrePlanning",
                table: "PrePlanning",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 500,
                oldNullable: true,
                oldComment: "describe of the reason for denying the deletion request of pre-planning");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                schema: "PrePlanning",
                table: "PrePlanning",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 256,
                oldNullable: true,
                oldComment: "Determine who cretead pre-planning");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "PrePlanning",
                table: "PrePlanning",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldComment: "Define created date of pre-planning");

            migrationBuilder.AlterColumn<int>(
                name: "BranchId",
                schema: "PrePlanning",
                table: "PrePlanning",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "it's a foreign  key that describe of the pre-planning branch");

            migrationBuilder.AlterColumn<string>(
                name: "AgencyCode",
                schema: "PrePlanning",
                table: "PrePlanning",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "describe of the pre-planning government agency");

            migrationBuilder.AlterColumn<int>(
                name: "PrePlanningId",
                schema: "PrePlanning",
                table: "PrePlanning",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "define identity of pre-planning")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<DateTime>(
                name: "SubmissionDate",
                schema: "Offer",
                table: "OfferSolidarity",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldComment: "The date of Accepting the solidarity Request");

            migrationBuilder.AlterColumn<int>(
                name: "StatusId",
                schema: "Offer",
                table: "OfferSolidarity",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define the status of the request (Not sent , Rejected  or accepted)");

            migrationBuilder.AlterColumn<int>(
                name: "SolidarityTypeId",
                schema: "Offer",
                table: "OfferSolidarity",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "The type of Request between Registered Supplier or Forign ");

            migrationBuilder.AlterColumn<string>(
                name: "RejectionReason",
                schema: "Offer",
                table: "OfferSolidarity",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1024,
                oldNullable: true,
                oldComment: "Define the reason if the supplier rejected the request");

            migrationBuilder.AlterColumn<int>(
                name: "OfferId",
                schema: "Offer",
                table: "OfferSolidarity",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define the related Offer");

            migrationBuilder.AlterColumn<string>(
                name: "Mobile",
                schema: "Offer",
                table: "OfferSolidarity",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true,
                oldComment: "The Mobile number for the supplierwho will recieve the request to be partner of the offer");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                schema: "Offer",
                table: "OfferSolidarity",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true,
                oldComment: "The email for the supplierwho will recieve the request to be partner of the offer");

            migrationBuilder.AlterColumn<string>(
                name: "CRNumber",
                schema: "Offer",
                table: "OfferSolidarity",
                type: "nvarchar(20)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true,
                oldComment: "Define the supplier Commercial Number if he was Registered on Monafasat");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                schema: "Offer",
                table: "OfferSolidarity",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define Unique identifer Of Offer Solidarity Table")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "TenderId",
                schema: "Offer",
                table: "OfferHistory",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define the Id of related Tender");

            migrationBuilder.AlterColumn<string>(
                name: "RejectionReason",
                schema: "Offer",
                table: "OfferHistory",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 2000,
                oldNullable: true,
                oldComment: "The Rejection reason in awrding stage");

            migrationBuilder.AlterColumn<int>(
                name: "OfferTechnicalEvaluationStatusId",
                schema: "Offer",
                table: "OfferHistory",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true,
                oldComment: "Define the Technical evaluation result [Accepted or Rejected]");

            migrationBuilder.AlterColumn<int>(
                name: "OfferStatusId",
                schema: "Offer",
                table: "OfferHistory",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define the status of offer like (under establishment,Sent,cancelled ...etc )");

            migrationBuilder.AlterColumn<int>(
                name: "OfferId",
                schema: "Offer",
                table: "OfferHistory",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define the related offer");

            migrationBuilder.AlterColumn<int>(
                name: "OfferAcceptanceStatusId",
                schema: "Offer",
                table: "OfferHistory",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true,
                oldComment: "Define the financial evaluation result [Accepted or Rejected]");

            migrationBuilder.AlterColumn<string>(
                name: "CommericalRegisterNo",
                schema: "Offer",
                table: "OfferHistory",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true,
                oldComment: "Define the Commerical Register Number for the owner supplier for the offer ");

            migrationBuilder.AlterColumn<int>(
                name: "ActionId",
                schema: "Offer",
                table: "OfferHistory",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "The type of action on the offer");

            migrationBuilder.AlterColumn<int>(
                name: "OfferHistoryId",
                schema: "Offer",
                table: "OfferHistory",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define Unique identifer Of Offer Presentation Way lookup")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalOfferAwardingValue",
                schema: "Offer",
                table: "Offer",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldNullable: true,
                oldComment: "Define the total value of awarding ");

            migrationBuilder.AlterColumn<int>(
                name: "TenderId",
                schema: "Offer",
                table: "Offer",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define the Id of related Tender");

            migrationBuilder.AlterColumn<int>(
                name: "TechnicalEvaluationLevel",
                schema: "Offer",
                table: "Offer",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define the Technical Evaluation Level of the offer ");

            migrationBuilder.AlterColumn<int>(
                name: "SuplierId",
                schema: "Offer",
                table: "Offer",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Not Used");

            migrationBuilder.AlterColumn<string>(
                name: "RejectionReason",
                schema: "Offer",
                table: "Offer",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true,
                oldComment: "The Rejection reason in awrding stage");

            migrationBuilder.AlterColumn<decimal>(
                name: "PartialOfferAwardingValue",
                schema: "Offer",
                table: "Offer",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldNullable: true,
                oldComment: "Define the partial value of awarding if the tender partialy awarded");

            migrationBuilder.AlterColumn<decimal>(
                name: "OfferWeightAfterCalcNPA",
                schema: "Offer",
                table: "Offer",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldNullable: true,
                oldComment: "The final financial value of the offer after appling VAT and discount and Calculation of National Product Equation");

            migrationBuilder.AlterColumn<int>(
                name: "OfferTechnicalEvaluationStatusId",
                schema: "Offer",
                table: "Offer",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true,
                oldComment: "Define the Technical evaluation result [Accepted or Rejected]");

            migrationBuilder.AlterColumn<int>(
                name: "OfferStatusId",
                schema: "Offer",
                table: "Offer",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define the status of offer like (under establishment,Sent,cancelled ...etc )");

            migrationBuilder.AlterColumn<int>(
                name: "OfferPresentationWayId",
                schema: "Offer",
                table: "Offer",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true,
                oldComment: "Define if the offer is presented in one file or two files ");

            migrationBuilder.AlterColumn<string>(
                name: "OfferLetterNumber",
                schema: "Offer",
                table: "Offer",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 500,
                oldNullable: true,
                oldComment: "Define  the Offer Letter Number");

            migrationBuilder.AlterColumn<DateTime>(
                name: "OfferLetterDate",
                schema: "Offer",
                table: "Offer",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldComment: "Define the Offer Letter Expiry Date");

            migrationBuilder.AlterColumn<int>(
                name: "OfferAcceptanceStatusId",
                schema: "Offer",
                table: "Offer",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true,
                oldComment: "Define the financial evaluation result [Accepted or Rejected]");

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                schema: "Offer",
                table: "Offer",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true,
                oldComment: "Represent the notes entered on Checkng Stage");

            migrationBuilder.AlterColumn<string>(
                name: "JustificationOfRecommendation",
                schema: "Offer",
                table: "Offer",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1024,
                oldNullable: true,
                oldComment: "Define Justification Of Recommendation");

            migrationBuilder.AlterColumn<bool>(
                name: "IsVisitationAttached",
                schema: "Offer",
                table: "Offer",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldNullable: true,
                oldComment: "Define if Visitation hard Copy appied to the agency ");

            migrationBuilder.AlterColumn<bool>(
                name: "IsSolidarity",
                schema: "Offer",
                table: "Offer",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldComment: "Define if More than one supplier in one offer ");

            migrationBuilder.AlterColumn<bool>(
                name: "IsPurchaseBillAttached",
                schema: "Offer",
                table: "Offer",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldNullable: true,
                oldComment: "Define if Purchase Bill hard Copy applied to the agency ");

            migrationBuilder.AlterColumn<bool>(
                name: "IsOpened",
                schema: "Offer",
                table: "Offer",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldNullable: true,
                oldComment: "Is the offer opended and all needed Data filled in oppenning stage");

            migrationBuilder.AlterColumn<bool>(
                name: "IsOfferLetterAttached",
                schema: "Offer",
                table: "Offer",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldNullable: true,
                oldComment: "Define if Offer Letter Attached ");

            migrationBuilder.AlterColumn<bool>(
                name: "IsOfferFinancialDetailsEntered",
                schema: "Offer",
                table: "Offer",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldComment: "Define if all financial Details entired or Not");

            migrationBuilder.AlterColumn<bool>(
                name: "IsOfferCopyAttached",
                schema: "Offer",
                table: "Offer",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldNullable: true,
                oldComment: "Define if hard copy of offer Applied");

            migrationBuilder.AlterColumn<bool>(
                name: "IsManuallyApplied",
                schema: "Offer",
                table: "Offer",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldComment: "Define if the offer was applied out of the system or by the system");

            migrationBuilder.AlterColumn<bool>(
                name: "IsFinantialOfferLetterCopyAttached",
                schema: "Offer",
                table: "Offer",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldNullable: true,
                oldComment: "Represent if acopy of Finaintial Offer Letter manually Applied");

            migrationBuilder.AlterColumn<bool>(
                name: "IsFinaintialOfferLetterAttached",
                schema: "Offer",
                table: "Offer",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldNullable: true,
                oldComment: "Represent if the  Finaintial Offer Letter Attached");

            migrationBuilder.AlterColumn<bool>(
                name: "IsBankGuaranteeAttached",
                schema: "Offer",
                table: "Offer",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldNullable: true,
                oldComment: "Define if Bank Guarantee hard Copy appied to the agency ");

            migrationBuilder.AlterColumn<string>(
                name: "FinantialRejectionReason",
                schema: "Offer",
                table: "Offer",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true,
                oldComment: "The reason for financial rejection in checking stage");

            migrationBuilder.AlterColumn<string>(
                name: "FinantialOfferLetterNumber",
                schema: "Offer",
                table: "Offer",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true,
                oldComment: "The number of Finantial Offer Letter");

            migrationBuilder.AlterColumn<string>(
                name: "FinantialNotes",
                schema: "Offer",
                table: "Offer",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true,
                oldComment: "Represent the notes entered on Checkng Stage");

            migrationBuilder.AlterColumn<int>(
                name: "FinancialEvaluationLevel",
                schema: "Offer",
                table: "Offer",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define the Financial Evaluation Level of the offer ");

            migrationBuilder.AlterColumn<decimal>(
                name: "FinalPriceBeforeDiscount",
                schema: "Offer",
                table: "Offer",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldNullable: true,
                oldComment: "The final financial value of the offer before appling VAT and discount");

            migrationBuilder.AlterColumn<decimal>(
                name: "FinalPriceAfterDiscount",
                schema: "Offer",
                table: "Offer",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldNullable: true,
                oldComment: "The final financial value of the offer after appling VAT and discount");

            migrationBuilder.AlterColumn<string>(
                name: "ExclusionReason",
                schema: "Offer",
                table: "Offer",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true,
                oldComment: "Represent the Reason of excluding the offer ");

            migrationBuilder.AlterColumn<string>(
                name: "DiscountNotes",
                schema: "Offer",
                table: "Offer",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true,
                oldComment: "Represent notes entered while adding  discount , used in awarding stage");

            migrationBuilder.AlterColumn<string>(
                name: "Discount",
                schema: "Offer",
                table: "Offer",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true,
                oldComment: "Represent the Discount , used in awarding stage");

            migrationBuilder.AlterColumn<string>(
                name: "CommericalRegisterNo",
                schema: "Offer",
                table: "Offer",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "Define the Commerical Register Number for the owner supplier for the offer ");

            migrationBuilder.AlterColumn<int>(
                name: "OfferId",
                schema: "Offer",
                table: "Offer",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define Unique identifer Of Offer")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "LookUps",
                table: "TenderUnitUpdateType",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1024,
                oldNullable: true,
                oldComment: "Define the name of tender unit type");

            migrationBuilder.AlterColumn<int>(
                name: "TenderUnitUpdateTypeId",
                schema: "LookUps",
                table: "TenderUnitUpdateType",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define identity of tender unit type");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "LookUps",
                table: "TenderUnitStatus",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true,
                oldComment: "Define the name of tender unit status");

            migrationBuilder.AlterColumn<int>(
                name: "TenderUnitStatusId",
                schema: "LookUps",
                table: "TenderUnitStatus",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define identity of tender unit status");

            migrationBuilder.AlterColumn<string>(
                name: "NameEn",
                schema: "LookUps",
                table: "TenderType",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 500,
                oldNullable: true,
                oldComment: "Define the english name of tender type");

            migrationBuilder.AlterColumn<string>(
                name: "NameAr",
                schema: "LookUps",
                table: "TenderType",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 500,
                oldNullable: true,
                oldComment: "Define the arabic name of tender type");

            migrationBuilder.AlterColumn<decimal>(
                name: "InvitationCost",
                schema: "LookUps",
                table: "TenderType",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldComment: "Define the Invitation Cost of tender type");

            migrationBuilder.AlterColumn<decimal>(
                name: "BuyingCost",
                schema: "LookUps",
                table: "TenderType",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldComment: "Define the cost of buying  of tender type");

            migrationBuilder.AlterColumn<int>(
                name: "TenderTypeId",
                schema: "LookUps",
                table: "TenderType",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define identity of tender type");

            migrationBuilder.AlterColumn<string>(
                name: "NameEn",
                schema: "LookUps",
                table: "TenderStatus",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true,
                oldComment: "Define the english name of tender status");

            migrationBuilder.AlterColumn<string>(
                name: "NameAr",
                schema: "LookUps",
                table: "TenderStatus",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true,
                oldComment: "Define the arabic name of tender status");

            migrationBuilder.AlterColumn<int>(
                name: "TenderStatusId",
                schema: "LookUps",
                table: "TenderStatus",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define identity of tender status");

            migrationBuilder.AlterColumn<string>(
                name: "NameEn",
                schema: "LookUps",
                table: "OfferStatus",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true,
                oldComment: "Define the English Name Of  Offer Status");

            migrationBuilder.AlterColumn<string>(
                name: "NameAr",
                schema: "LookUps",
                table: "OfferStatus",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true,
                oldComment: "Define the Arabic Name Of  Offer Status");

            migrationBuilder.AlterColumn<int>(
                name: "OfferStatusId",
                schema: "LookUps",
                table: "OfferStatus",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define Unique identifer Of Offer Status lookup");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "LookUps",
                table: "OfferSolidarityStatus",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1024,
                oldNullable: true,
                oldComment: "Define the  Name Of  Offer Solidarity Status");

            migrationBuilder.AlterColumn<int>(
                name: "CombinedStatusId",
                schema: "LookUps",
                table: "OfferSolidarityStatus",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define Unique identifer Of Offer Solidarity  Status lookup");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "LookUps",
                table: "NegotiationType",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true,
                oldComment: "Define The name of Negotiation Type Name");

            migrationBuilder.AlterColumn<int>(
                name: "NegotiationTypeId",
                schema: "LookUps",
                table: "NegotiationType",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define the unique  identifier for negotiation types");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "LookUps",
                table: "NegotiationReason",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true,
                oldComment: "Define The name of Negotiation reason Name");

            migrationBuilder.AlterColumn<int>(
                name: "NegotiationReasonId",
                schema: "LookUps",
                table: "NegotiationReason",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define the unique  identifier for negotiation reason");

            migrationBuilder.AlterColumn<string>(
                name: "NameEn",
                schema: "LookUps",
                table: "InvitationStatus",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true,
                oldComment: "define english name of invitation status");

            migrationBuilder.AlterColumn<string>(
                name: "NameAr",
                schema: "LookUps",
                table: "InvitationStatus",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true,
                oldComment: "define arabic name of invitation status");

            migrationBuilder.AlterColumn<int>(
                name: "InvitationStatusId",
                schema: "LookUps",
                table: "InvitationStatus",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "define identity of invitation status");

            migrationBuilder.AlterColumn<string>(
                name: "NameEn",
                schema: "LookUps",
                table: "Country",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1024,
                oldNullable: true,
                oldComment: "Define english name of country");

            migrationBuilder.AlterColumn<string>(
                name: "NameAr",
                schema: "LookUps",
                table: "Country",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1024,
                oldNullable: true,
                oldComment: "Define arabic name of country");

            migrationBuilder.AlterColumn<bool>(
                name: "IsGolf",
                schema: "LookUps",
                table: "Country",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldNullable: true,
                oldComment: "Describe that is country affiliated with the gulf cooperation council or not");

            migrationBuilder.AlterColumn<int>(
                name: "CountryId",
                schema: "LookUps",
                table: "Country",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define identity of country");

            migrationBuilder.AlterColumn<int>(
                name: "ParentID",
                schema: "LookUps",
                table: "ConstructionWork",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true,
                oldComment: "define parent id of construction work");

            migrationBuilder.AlterColumn<string>(
                name: "NameEn",
                schema: "LookUps",
                table: "ConstructionWork",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1024,
                oldNullable: true,
                oldComment: "define english name of  construction work");

            migrationBuilder.AlterColumn<string>(
                name: "NameAr",
                schema: "LookUps",
                table: "ConstructionWork",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 1024,
                oldComment: "define arabic name of  construction work");

            migrationBuilder.AlterColumn<int>(
                name: "ConstructionWorkId",
                schema: "LookUps",
                table: "ConstructionWork",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "define identity of construction work");

            migrationBuilder.AlterColumn<string>(
                name: "NameEn",
                schema: "LookUps",
                table: "CommitteeType",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 500,
                oldNullable: true,
                oldComment: "Define english name of committee type");

            migrationBuilder.AlterColumn<string>(
                name: "NameAr",
                schema: "LookUps",
                table: "CommitteeType",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 500,
                oldNullable: true,
                oldComment: "Define arabic name of committee type");

            migrationBuilder.AlterColumn<int>(
                name: "CommitteeTypeId",
                schema: "LookUps",
                table: "CommitteeType",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define identity of committee type");

            migrationBuilder.AlterColumn<string>(
                name: "NameEn",
                schema: "LookUps",
                table: "Bank",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1024,
                oldNullable: true,
                oldComment: "Define english name of bank");

            migrationBuilder.AlterColumn<string>(
                name: "NameAr",
                schema: "LookUps",
                table: "Bank",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1024,
                oldNullable: true,
                oldComment: "Define arabic name of bank");

            migrationBuilder.AlterColumn<int>(
                name: "BankId",
                schema: "LookUps",
                table: "Bank",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define identity of bank");

            migrationBuilder.AlterColumn<string>(
                name: "NameEn",
                schema: "LookUps",
                table: "Area",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1024,
                oldNullable: true,
                oldComment: "Define english name of Area");

            migrationBuilder.AlterColumn<string>(
                name: "NameAr",
                schema: "LookUps",
                table: "Area",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1024,
                oldNullable: true,
                oldComment: "Define arabic name of Area");

            migrationBuilder.AlterColumn<int>(
                name: "AreaId",
                schema: "LookUps",
                table: "Area",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define identity of area");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "LookUps",
                table: "AgencyCommunicationRequestType",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1024,
                oldNullable: true,
                oldComment: "Define type name of agency communication request");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                schema: "LookUps",
                table: "AgencyCommunicationRequestType",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define a unique identifer of agency communication request type");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "LookUps",
                table: "AgencyCommunicationRequestStatus",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1024,
                oldNullable: true,
                oldComment: "Define status name of agency communication request");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                schema: "LookUps",
                table: "AgencyCommunicationRequestStatus",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define a unique identifer of agency communication request status");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "LookUps",
                table: "AgencyCommunicationPlaintStatus",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1024,
                oldNullable: true,
                oldComment: "Define the plaint status name of plaint request");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                schema: "LookUps",
                table: "AgencyCommunicationPlaintStatus",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define a unique identifer of plaint request status");

            migrationBuilder.AlterColumn<string>(
                name: "AddressTypeName",
                schema: "LookUps",
                table: "AddressType",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true,
                oldComment: "Define name of Address type");

            migrationBuilder.AlterColumn<int>(
                name: "AddressTypeId",
                schema: "LookUps",
                table: "AddressType",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define identity of Address")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "NameEn",
                schema: "LookUps",
                table: "Activitytemplate",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1024,
                oldNullable: true,
                oldComment: "Define english name of activity template");

            migrationBuilder.AlterColumn<string>(
                name: "NameAr",
                schema: "LookUps",
                table: "Activitytemplate",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1024,
                oldNullable: true,
                oldComment: "Define arabic name of activity template");

            migrationBuilder.AlterColumn<bool>(
                name: "HasYears",
                schema: "LookUps",
                table: "Activitytemplate",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldNullable: true,
                oldComment: "Define activity template has years or not");

            migrationBuilder.AlterColumn<int>(
                name: "ActivitytemplatId",
                schema: "LookUps",
                table: "Activitytemplate",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define identity of activity template");

            migrationBuilder.AlterColumn<int>(
                name: "ParentID",
                schema: "LookUps",
                table: "Activity",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true,
                oldComment: "define parent id of activity");

            migrationBuilder.AlterColumn<string>(
                name: "NameEn",
                schema: "LookUps",
                table: "Activity",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1024,
                oldNullable: true,
                oldComment: "define english name of activity");

            migrationBuilder.AlterColumn<string>(
                name: "NameAr",
                schema: "LookUps",
                table: "Activity",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1024,
                oldNullable: true,
                oldComment: "define arabic name of activity");

            migrationBuilder.AlterColumn<int>(
                name: "ActivitytemplateID",
                schema: "LookUps",
                table: "Activity",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true,
                oldComment: "it's a foreign  key that described activity template of activity");

            migrationBuilder.AlterColumn<int>(
                name: "ActivityId",
                schema: "LookUps",
                table: "Activity",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "define identity of activity");

            migrationBuilder.AlterColumn<string>(
                name: "NameEnglish",
                schema: "Lookups",
                table: "TenderFeesType",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true,
                oldComment: "Define english name of tender fees type");

            migrationBuilder.AlterColumn<string>(
                name: "NameArabic",
                schema: "Lookups",
                table: "TenderFeesType",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true,
                oldComment: "Define arabic name of tender fees type");

            migrationBuilder.AlterColumn<int>(
                name: "TenderFeesTypeId",
                schema: "Lookups",
                table: "TenderFeesType",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define identity of tender fees type");

            migrationBuilder.AlterColumn<string>(
                name: "NameEn",
                schema: "Lookups",
                table: "PrePlanningStatus",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true,
                oldComment: "define english name of preplanning status");

            migrationBuilder.AlterColumn<string>(
                name: "NameAr",
                schema: "Lookups",
                table: "PrePlanningStatus",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true,
                oldComment: "define arabic name of preplanning status");

            migrationBuilder.AlterColumn<int>(
                name: "StatusId",
                schema: "Lookups",
                table: "PrePlanningStatus",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "define identity of preplanning status");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "Lookups",
                table: "OfferPresentationWay",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1024,
                oldNullable: true,
                oldComment: "Define the Name Of Offer Presentation Way lookup");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                schema: "Lookups",
                table: "OfferPresentationWay",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define Unique identifer Of Offer Presentation Way lookup");

            migrationBuilder.AlterColumn<string>(
                name: "PlaintReason",
                schema: "CommunicationRequest",
                table: "PlaintRequest",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1000,
                oldNullable: true,
                oldComment: "Define the reason of plaint request");

            migrationBuilder.AlterColumn<int>(
                name: "OfferId",
                schema: "CommunicationRequest",
                table: "PlaintRequest",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define the related Offer for plaint request");

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                schema: "CommunicationRequest",
                table: "PlaintRequest",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1000,
                oldNullable: true,
                oldComment: "Define notes for more details about the plaint request");

            migrationBuilder.AlterColumn<bool>(
                name: "IsEscalation",
                schema: "CommunicationRequest",
                table: "PlaintRequest",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldComment: "Flag determine if the plaint request has an escalation request or not");

            migrationBuilder.AlterColumn<int>(
                name: "AgencyCommunicationRequestId",
                schema: "CommunicationRequest",
                table: "PlaintRequest",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define the related agency communication request for plaint request");

            migrationBuilder.AlterColumn<int>(
                name: "PlainRequestId",
                schema: "CommunicationRequest",
                table: "PlaintRequest",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define a unique identifer of plaint request")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<decimal>(
                name: "offerOriginalAmount",
                schema: "CommunicationRequest",
                table: "NegotiationFirstStageSupplier",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldComment: "Define The Amount of the offer before deduction");

            migrationBuilder.AlterColumn<string>(
                name: "SupplierCR",
                schema: "CommunicationRequest",
                table: "NegotiationFirstStageSupplier",
                type: "nvarchar(20)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true,
                oldComment: "Define the supplier who will recieved the negotiation request  ");

            migrationBuilder.AlterColumn<DateTime>(
                name: "PeriodStartDateTime",
                schema: "CommunicationRequest",
                table: "NegotiationFirstStageSupplier",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldComment: "Define The start date that the supplier recieved the request ");

            migrationBuilder.AlterColumn<int>(
                name: "OfferId",
                schema: "CommunicationRequest",
                table: "NegotiationFirstStageSupplier",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define The related offer");

            migrationBuilder.AlterColumn<int>(
                name: "NegotiationSupplierStatusId",
                schema: "CommunicationRequest",
                table: "NegotiationFirstStageSupplier",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true,
                oldComment: "The status of Request for the supplier is accepted from supplier or rejected or still not sent to the supplier");

            migrationBuilder.AlterColumn<int>(
                name: "NegotiationId",
                schema: "CommunicationRequest",
                table: "NegotiationFirstStageSupplier",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define The Negotiation Table ");

            migrationBuilder.AlterColumn<bool>(
                name: "IsReported",
                schema: "CommunicationRequest",
                table: "NegotiationFirstStageSupplier",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldComment: "Define if the supplier notified that he has Negotiation request");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                schema: "CommunicationRequest",
                table: "NegotiationFirstStageSupplier",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define the Uniqee Identifier for  Negotiation First stage Suppliers List Table")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<DateTime>(
                name: "PeriodStartDate",
                schema: "CommunicationRequest",
                table: "Negotiation",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldComment: "Define The start date that the supplier recieved the request ");

            migrationBuilder.AlterColumn<int>(
                name: "OfferId",
                schema: "CommunicationRequest",
                table: "Negotiation",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true,
                oldComment: "Define The project number from Etimad Budget");

            migrationBuilder.AlterColumn<int>(
                name: "NegotiationFirstStageId",
                schema: "CommunicationRequest",
                table: "Negotiation",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true,
                oldComment: "Define the Related First Stage Negotiation Request");

            migrationBuilder.AlterColumn<string>(
                name: "ProjectNumber",
                schema: "CommunicationRequest",
                table: "Negotiation",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true,
                oldComment: "Define The project number from Etimad Budget");

            migrationBuilder.AlterColumn<bool>(
                name: "IsNewNegotiation",
                schema: "CommunicationRequest",
                table: "Negotiation",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldNullable: true,
                oldComment: "define if the negotiation request should take the new flow or old flow");

            migrationBuilder.AlterColumn<decimal>(
                name: "ExtraDiscountValue",
                schema: "CommunicationRequest",
                table: "Negotiation",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldNullable: true,
                oldComment: "Define The Amount of the offer after supplier deducted axtra amount from his offer ");

            migrationBuilder.AlterColumn<string>(
                name: "DiscountLetterRefID",
                schema: "CommunicationRequest",
                table: "Negotiation",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true,
                oldComment: "Define the Referance for the Letter File of Negotiation");

            migrationBuilder.AlterColumn<decimal>(
                name: "DiscountAmount",
                schema: "CommunicationRequest",
                table: "Negotiation",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldNullable: true,
                oldComment: "Define The amount which the agency needs to deduct from supplier offer");

            migrationBuilder.AlterColumn<int>(
                name: "SupplierReplyPeriodHours",
                schema: "CommunicationRequest",
                table: "Negotiation",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "the period that the defined for supplier to reply on the negotiation Request ");

            migrationBuilder.AlterColumn<int>(
                name: "StatusId",
                schema: "CommunicationRequest",
                table: "Negotiation",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define the status of Negotiation ");

            migrationBuilder.AlterColumn<string>(
                name: "RejectionReason",
                schema: "CommunicationRequest",
                table: "Negotiation",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true,
                oldComment: "Define the Note written by the high level employee if he rejected the request ");

            migrationBuilder.AlterColumn<int>(
                name: "NegotiationTypeId",
                schema: "CommunicationRequest",
                table: "Negotiation",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "the type of negotiation [first stage or second stage] Note: this column refer to Negotiations types Table ");

            migrationBuilder.AlterColumn<int>(
                name: "NegotiationReasonId",
                schema: "CommunicationRequest",
                table: "Negotiation",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true,
                oldComment: "the reason for negotiation Note: this column refer to Negotiations Reasons Table ");

            migrationBuilder.AlterColumn<int>(
                name: "AgencyRequestId",
                schema: "CommunicationRequest",
                table: "Negotiation",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Communication Request that the negotiation is related To");

            migrationBuilder.AlterColumn<int>(
                name: "NegotiationId",
                schema: "CommunicationRequest",
                table: "Negotiation",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define Identity Of Negotiation Request")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "PlaintRequestId",
                schema: "CommunicationRequest",
                table: "EscalationRequest",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define the related plaint request for the escalation request");

            migrationBuilder.AlterColumn<string>(
                name: "EscalationNotes",
                schema: "CommunicationRequest",
                table: "EscalationRequest",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1000,
                oldNullable: true,
                oldComment: "Define notes for more details about the escalation request");

            migrationBuilder.AlterColumn<int>(
                name: "EscalationRequestId",
                schema: "CommunicationRequest",
                table: "EscalationRequest",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define a unique identifer of escalation request")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "TenderPlaintRequestProcedureId",
                schema: "CommunicationRequest",
                table: "AgencyCommunicationRequest",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true,
                oldComment: "Define the action caused by accepting plaint or esclation");

            migrationBuilder.AlterColumn<int>(
                name: "TenderId",
                schema: "CommunicationRequest",
                table: "AgencyCommunicationRequest",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define the related tender of agency communication request");

            migrationBuilder.AlterColumn<int>(
                name: "SupplierExtendOfferDatesRequestId",
                schema: "CommunicationRequest",
                table: "AgencyCommunicationRequest",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true,
                oldComment: "Define the Supplier extend offer dates request Id of agency communication request");

            migrationBuilder.AlterColumn<int>(
                name: "StatusId",
                schema: "CommunicationRequest",
                table: "AgencyCommunicationRequest",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define the status of agency communication request");

            migrationBuilder.AlterColumn<string>(
                name: "RequestedByRoleName",
                schema: "CommunicationRequest",
                table: "AgencyCommunicationRequest",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true,
                oldComment: "Define the role who create the agency communication request");

            migrationBuilder.AlterColumn<string>(
                name: "RejectionReason",
                schema: "CommunicationRequest",
                table: "AgencyCommunicationRequest",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1000,
                oldNullable: true,
                oldComment: "Define the rejection reason for any request");

            migrationBuilder.AlterColumn<int>(
                name: "PlaintAcceptanceStatusId",
                schema: "CommunicationRequest",
                table: "AgencyCommunicationRequest",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true,
                oldComment: "Define the plaint status set by secretary of plaint request");

            migrationBuilder.AlterColumn<bool>(
                name: "IsReported",
                schema: "CommunicationRequest",
                table: "AgencyCommunicationRequest",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldComment: "Define if the requeste is reported or not");

            migrationBuilder.AlterColumn<int>(
                name: "EscalationStatusId",
                schema: "CommunicationRequest",
                table: "AgencyCommunicationRequest",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true,
                oldComment: "Define the esclation status set by manager of esclation request");

            migrationBuilder.AlterColumn<string>(
                name: "EscalationRejectionReason",
                schema: "CommunicationRequest",
                table: "AgencyCommunicationRequest",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1000,
                oldNullable: true,
                oldComment: "Define the rejection reason for the esclation request");

            migrationBuilder.AlterColumn<int>(
                name: "EscalationAcceptanceStatusId",
                schema: "CommunicationRequest",
                table: "AgencyCommunicationRequest",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true,
                oldComment: "Define the esclation status set by secretary of esclation request");

            migrationBuilder.AlterColumn<int>(
                name: "AgencyRequestTypeId",
                schema: "CommunicationRequest",
                table: "AgencyCommunicationRequest",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define the Id of agency communication request type");

            migrationBuilder.AlterColumn<int>(
                name: "AgencyRequestId",
                schema: "CommunicationRequest",
                table: "AgencyCommunicationRequest",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define a unique identifer of agency communication request")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "NegotiationId",
                schema: "CommunicationAgency",
                table: "NegotiationAttachment",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define The related of Negotiation request");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "CommunicationAgency",
                table: "NegotiationAttachment",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 200,
                oldComment: "Define The name of the file");

            migrationBuilder.AlterColumn<string>(
                name: "FileNetReferenceId",
                schema: "CommunicationAgency",
                table: "NegotiationAttachment",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1024,
                oldNullable: true,
                oldComment: "Define The reference number from file Net");

            migrationBuilder.AlterColumn<int>(
                name: "AttachmentTypeId",
                schema: "CommunicationAgency",
                table: "NegotiationAttachment",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "the type of attachment file like [supplier attachment or agency attachment]");

            migrationBuilder.AlterColumn<int>(
                name: "AttachmentId",
                schema: "CommunicationAgency",
                table: "NegotiationAttachment",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define the unique  identifier for negotiation Attachment")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "UserRoleId",
                schema: "Committee",
                table: "CommitteeUser",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "it's a forigne key that described user role");

            migrationBuilder.AlterColumn<int>(
                name: "UserProfileId",
                schema: "Committee",
                table: "CommitteeUser",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "it's a forigne key that described user profile");

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                schema: "Committee",
                table: "CommitteeUser",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 256,
                oldNullable: true,
                oldComment: "Determine who updated committee user");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                schema: "Committee",
                table: "CommitteeUser",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldComment: "Define updated date of committee user");

            migrationBuilder.AlterColumn<string>(
                name: "RelatedAgencyCode",
                schema: "Committee",
                table: "CommitteeUser",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true,
                oldComment: "Define identity related agencycode");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                schema: "Committee",
                table: "CommitteeUser",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldNullable: true,
                oldComment: "Define committee user is active or not");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                schema: "Committee",
                table: "CommitteeUser",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 256,
                oldNullable: true,
                oldComment: "Determine who cretead committee user");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "Committee",
                table: "CommitteeUser",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldComment: "Define created date of committee user");

            migrationBuilder.AlterColumn<int>(
                name: "CommitteeId",
                schema: "Committee",
                table: "CommitteeUser",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "it's a forigne key that described related committe");

            migrationBuilder.AlterColumn<int>(
                name: "CommitteeUserId",
                schema: "Committee",
                table: "CommitteeUser",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define identity of committee user")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "ZipCode",
                schema: "Committee",
                table: "Committee",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1024,
                oldNullable: true,
                oldComment: "Define zip code of committee");

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                schema: "Committee",
                table: "Committee",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 256,
                oldNullable: true,
                oldComment: "Determine who updated committee");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                schema: "Committee",
                table: "Committee",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldComment: "Define updated date of committee");

            migrationBuilder.AlterColumn<string>(
                name: "PostalCode",
                schema: "Committee",
                table: "Committee",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1024,
                oldNullable: true,
                oldComment: "Define postal code of committee");

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                schema: "Committee",
                table: "Committee",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true,
                oldComment: "Define phone of committee");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                schema: "Committee",
                table: "Committee",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldNullable: true,
                oldComment: "Define committee is active or not");

            migrationBuilder.AlterColumn<string>(
                name: "Fax",
                schema: "Committee",
                table: "Committee",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true,
                oldComment: "Define fax of committee");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                schema: "Committee",
                table: "Committee",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1024,
                oldNullable: true,
                oldComment: "Define e-mail of committee");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                schema: "Committee",
                table: "Committee",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 256,
                oldNullable: true,
                oldComment: "Determine who cretead committee");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "Committee",
                table: "Committee",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldComment: "Define created date of committee");

            migrationBuilder.AlterColumn<int>(
                name: "CommitteeTypeId",
                schema: "Committee",
                table: "Committee",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "it's a forigne key  that describe committe type");

            migrationBuilder.AlterColumn<string>(
                name: "CommitteeName",
                schema: "Committee",
                table: "Committee",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1024,
                oldNullable: true,
                oldComment: "Define  name of committee");

            migrationBuilder.AlterColumn<string>(
                name: "AgencyCode",
                schema: "Committee",
                table: "Committee",
                type: "nvarchar(20)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true,
                oldComment: "Define agency code of committee");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                schema: "Committee",
                table: "Committee",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1024,
                oldNullable: true,
                oldComment: "Define address of committee");

            migrationBuilder.AlterColumn<int>(
                name: "CommitteeId",
                schema: "Committee",
                table: "Committee",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define identity of committee")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                schema: "AnnouncementTemplate",
                table: "AnnouncementTemplateJoinRequestAttachment",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 256,
                oldNullable: true,
                oldComment: "Determine who updated attachment for announcement supplier template");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                schema: "AnnouncementTemplate",
                table: "AnnouncementTemplateJoinRequestAttachment",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldComment: "Define updated date of attachment for announcement supplier template");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "AnnouncementTemplate",
                table: "AnnouncementTemplateJoinRequestAttachment",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 200,
                oldComment: "Define name of announcement supplier template attachment");

            migrationBuilder.AlterColumn<int>(
                name: "JoinRequestSupplierTemplateId",
                schema: "AnnouncementTemplate",
                table: "AnnouncementTemplateJoinRequestAttachment",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define forigne key of join request announcement supplier template");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                schema: "AnnouncementTemplate",
                table: "AnnouncementTemplateJoinRequestAttachment",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldNullable: true,
                oldComment: "Define attachment is active or not");

            migrationBuilder.AlterColumn<string>(
                name: "FileNetReferenceId",
                schema: "AnnouncementTemplate",
                table: "AnnouncementTemplateJoinRequestAttachment",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1024,
                oldNullable: true,
                oldComment: "Define file net id");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                schema: "AnnouncementTemplate",
                table: "AnnouncementTemplateJoinRequestAttachment",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 256,
                oldNullable: true,
                oldComment: "Determine who cretead attachment for announcement supplier template");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "AnnouncementTemplate",
                table: "AnnouncementTemplateJoinRequestAttachment",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldComment: "Define created date of attachment for announcement supplier template");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                schema: "AnnouncementTemplate",
                table: "AnnouncementTemplateJoinRequestAttachment",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define identity of announcement supplier template attachment")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSupplierTemplate",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 256,
                oldNullable: true,
                oldComment: "Determine who updated announcement supplier templat");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSupplierTemplate",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldComment: "Define updated date for announcement list");

            migrationBuilder.AlterColumn<string>(
                name: "TenderTypesId",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSupplierTemplate",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true,
                oldComment: "Define type of tender");

            migrationBuilder.AlterColumn<string>(
                name: "TemplateExtendMechanism",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSupplierTemplate",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true,
                oldComment: "Define mechanism to extend the list");

            migrationBuilder.AlterColumn<int>(
                name: "StatusId",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSupplierTemplate",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define status of announcement supplier template");

            migrationBuilder.AlterColumn<string>(
                name: "RequirementConditionsToJoinList",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSupplierTemplate",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true,
                oldComment: "Define conditions necessary to join the list");

            migrationBuilder.AlterColumn<string>(
                name: "RequiredAttachment",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSupplierTemplate",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true,
                oldComment: "Define required attachment of announcement list");

            migrationBuilder.AlterColumn<string>(
                name: "ReferenceNumber",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSupplierTemplate",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true,
                oldComment: "Define reference number of announcement supplier template, its a unique identifier like announcement identity");

            migrationBuilder.AlterColumn<int>(
                name: "ReadyForApproval",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSupplierTemplate",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true,
                oldComment: "Define status for announcement list is ready for approval or not ");

            migrationBuilder.AlterColumn<DateTime>(
                name: "PublishedDate",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSupplierTemplate",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldComment: "Define published date for announcement supplier template");

            migrationBuilder.AlterColumn<bool>(
                name: "IsRequiredAttachmentToJoinList",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSupplierTemplate",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldComment: "Define required attachment of announcement list  is required or not");

            migrationBuilder.AlterColumn<bool>(
                name: "IsEffectiveList",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSupplierTemplate",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldNullable: true,
                oldComment: "Define effective date of announcement list  is required or not");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSupplierTemplate",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldNullable: true,
                oldComment: "Define announcement template list is active or not ");

            migrationBuilder.AlterColumn<string>(
                name: "IntroAboutAnnouncementTemplate",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSupplierTemplate",
                type: "nvarchar(max)",
                maxLength: 5000,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 5000,
                oldNullable: true,
                oldComment: "Define Intro about  Announcement Supplier Template");

            migrationBuilder.AlterColumn<bool>(
                name: "InsidKsa",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSupplierTemplate",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldComment: "Define excution place  of  Announcement Supplier Template");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EffectiveListDate",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSupplierTemplate",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldComment: "Define Effective date for announcement list");

            migrationBuilder.AlterColumn<string>(
                name: "Details",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSupplierTemplate",
                type: "nvarchar(max)",
                maxLength: 5000,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 5000,
                oldNullable: true,
                oldComment: "Define details of  Announcement Supplier Template");

            migrationBuilder.AlterColumn<string>(
                name: "Descriptions",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSupplierTemplate",
                type: "nvarchar(max)",
                maxLength: 5000,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 5000,
                oldNullable: true,
                oldComment: "Define descriptions of  Announcement Supplier Template");

            migrationBuilder.AlterColumn<int>(
                name: "CreatedById",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSupplierTemplate",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true,
                oldComment: "Determine who cretead announcement supplier template");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSupplierTemplate",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 256,
                oldNullable: true,
                oldComment: "Determine who cretead announcement supplier template");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSupplierTemplate",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldComment: "Define created date for announcement list");

            migrationBuilder.AlterColumn<string>(
                name: "CancelationReason",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSupplierTemplate",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true,
                oldComment: "Define cancelation reason  of announcement list");

            migrationBuilder.AlterColumn<int>(
                name: "BranchId",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSupplierTemplate",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true,
                oldComment: "Define branch of announcement supplier template");

            migrationBuilder.AlterColumn<string>(
                name: "ApprovedBy",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSupplierTemplate",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 200,
                oldNullable: true,
                oldComment: "Determine who approved announcement supplier template");

            migrationBuilder.AlterColumn<int>(
                name: "AnnouncementTemplateSuppliersListTypeId",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSupplierTemplate",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true,
                oldComment: "Define the  type of announcement supplier template list that public or private ");

            migrationBuilder.AlterColumn<string>(
                name: "AnnouncementName",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSupplierTemplate",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1024,
                oldNullable: true,
                oldComment: "Define Name Of Announcement Supplier Template");

            migrationBuilder.AlterColumn<string>(
                name: "AgencyPhone",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSupplierTemplate",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true,
                oldComment: "Define agency phone  for announcement supplier template");

            migrationBuilder.AlterColumn<string>(
                name: "AgencyFax",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSupplierTemplate",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true,
                oldComment: "Define agency fax  for announcement supplier template");

            migrationBuilder.AlterColumn<string>(
                name: "AgencyEmail",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSupplierTemplate",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true,
                oldComment: "Define agency email  for announcement supplier template");

            migrationBuilder.AlterColumn<string>(
                name: "AgencyCode",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSupplierTemplate",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "Define agency code for announcement supplier template");

            migrationBuilder.AlterColumn<string>(
                name: "AgencyAddress",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSupplierTemplate",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true,
                oldComment: "Define agency address  for announcement supplier template");

            migrationBuilder.AlterColumn<string>(
                name: "ActivityDescription",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSupplierTemplate",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 2000,
                oldNullable: true,
                oldComment: "Define  description of activity for announcement supplier template");

            migrationBuilder.AlterColumn<int>(
                name: "AnnouncementId",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSupplierTemplate",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define Identity Of Announcement Supplier Template")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSuppliersTemplateAttachment",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 256,
                oldNullable: true,
                oldComment: "Determine who updated attachment for announcement supplier template");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSuppliersTemplateAttachment",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldComment: "Define updated date of attachment for announcement supplier template");

            migrationBuilder.AlterColumn<int>(
                name: "ReviewStatusId",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSuppliersTemplateAttachment",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true,
                oldComment: "Define review status id of attachment for announcement supplier template");

            migrationBuilder.AlterColumn<string>(
                name: "RejectionReason",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSuppliersTemplateAttachment",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1024,
                oldNullable: true,
                oldComment: "Define rejection reason of attachment for announcement supplier template");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSuppliersTemplateAttachment",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 200,
                oldComment: "Define name of announcement supplier template attachment");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSuppliersTemplateAttachment",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldNullable: true,
                oldComment: "Define attachment is active or not");

            migrationBuilder.AlterColumn<string>(
                name: "FileNetReferenceId",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSuppliersTemplateAttachment",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1024,
                oldNullable: true,
                oldComment: "Define file net id");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSuppliersTemplateAttachment",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 256,
                oldNullable: true,
                oldComment: "Determine who cretead attachment for announcement supplier template");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSuppliersTemplateAttachment",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldComment: "Define created date of attachment for announcement supplier template");

            migrationBuilder.AlterColumn<int>(
                name: "ChangeStatusId",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSuppliersTemplateAttachment",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true,
                oldComment: "Define change status id of attachment for announcement supplier template");

            migrationBuilder.AlterColumn<int>(
                name: "AttachmentTypeId",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSuppliersTemplateAttachment",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define type of  attachment");

            migrationBuilder.AlterColumn<int>(
                name: "AnnouncementId",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSuppliersTemplateAttachment",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define forigne key of announcement supplier template");

            migrationBuilder.AlterColumn<int>(
                name: "AnnouncementSuppliersTemplateAttachmentId",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSuppliersTemplateAttachment",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define identity of announcement supplier template attachment")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "AnnouncementTemplate",
                table: "AnnouncementStatusSupplierTemplate",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1024,
                oldNullable: true,
                oldComment: "Define name of status supplier template");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                schema: "AnnouncementTemplate",
                table: "AnnouncementStatusSupplierTemplate",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define identity of announcement status supplier template");

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                schema: "AnnouncementTemplate",
                table: "AnnouncementMaintenanceRunnigWorkSupplierTemplate",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 256,
                oldNullable: true,
                oldComment: "Determine who updated announcement maintenance runnig work");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                schema: "AnnouncementTemplate",
                table: "AnnouncementMaintenanceRunnigWorkSupplierTemplate",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldComment: "Define updated date for announcement maintenance runnig work");

            migrationBuilder.AlterColumn<int>(
                name: "MaintenanceRunningWorkId",
                schema: "AnnouncementTemplate",
                table: "AnnouncementMaintenanceRunnigWorkSupplierTemplate",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define forigne key of maintenance running work");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                schema: "AnnouncementTemplate",
                table: "AnnouncementMaintenanceRunnigWorkSupplierTemplate",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldNullable: true,
                oldComment: "Define announcement maintenance runnig work is active or not");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                schema: "AnnouncementTemplate",
                table: "AnnouncementMaintenanceRunnigWorkSupplierTemplate",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 256,
                oldNullable: true,
                oldComment: "Determine who cretead announcement maintenance runnig work");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "AnnouncementTemplate",
                table: "AnnouncementMaintenanceRunnigWorkSupplierTemplate",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldComment: "Define created date for announcement maintenance runnig work");

            migrationBuilder.AlterColumn<int>(
                name: "AnnouncementId",
                schema: "AnnouncementTemplate",
                table: "AnnouncementMaintenanceRunnigWorkSupplierTemplate",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define forigne key  of announcement supplier template");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                schema: "AnnouncementTemplate",
                table: "AnnouncementMaintenanceRunnigWorkSupplierTemplate",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define Identity of announcement maintenance runnig work supplier template")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "Announcement",
                table: "AnnouncementStatus",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1024,
                oldNullable: true,
                oldComment: "Define name of anouncement status");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                schema: "Announcement",
                table: "AnnouncementStatus",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define identity of announcement status");

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                schema: "Announcement",
                table: "AnnouncementMaintenanceRunnigWork",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 256,
                oldNullable: true,
                oldComment: "Determine who updated announcement maintenance runnig work");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                schema: "Announcement",
                table: "AnnouncementMaintenanceRunnigWork",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldComment: "Define updated date for announcement maintenance runnig work");

            migrationBuilder.AlterColumn<int>(
                name: "MaintenanceRunningWorkId",
                schema: "Announcement",
                table: "AnnouncementMaintenanceRunnigWork",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define forigne key of maintenance running work");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                schema: "Announcement",
                table: "AnnouncementMaintenanceRunnigWork",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldNullable: true,
                oldComment: "Define announcement maintenance runnig work is active or not");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                schema: "Announcement",
                table: "AnnouncementMaintenanceRunnigWork",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 256,
                oldNullable: true,
                oldComment: "Determine who cretead announcement maintenance runnig work");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "Announcement",
                table: "AnnouncementMaintenanceRunnigWork",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldComment: "Define created date for announcement maintenance runnig work");

            migrationBuilder.AlterColumn<int>(
                name: "AnnouncementId",
                schema: "Announcement",
                table: "AnnouncementMaintenanceRunnigWork",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define forigne key of announcement");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                schema: "Announcement",
                table: "AnnouncementMaintenanceRunnigWork",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define Identity of announcement maintenance runnig work")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2021, 2, 10, 17, 9, 29, 719, DateTimeKind.Local).AddTicks(8106));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2021, 2, 10, 17, 9, 29, 719, DateTimeKind.Local).AddTicks(9629));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2021, 2, 10, 17, 9, 29, 719, DateTimeKind.Local).AddTicks(9658));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2021, 2, 10, 17, 9, 29, 719, DateTimeKind.Local).AddTicks(9661));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2021, 2, 10, 17, 9, 29, 719, DateTimeKind.Local).AddTicks(9662));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2021, 2, 10, 17, 9, 29, 719, DateTimeKind.Local).AddTicks(9664));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2021, 2, 10, 17, 9, 29, 719, DateTimeKind.Local).AddTicks(9666));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2021, 2, 10, 17, 9, 29, 719, DateTimeKind.Local).AddTicks(9667));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2021, 2, 10, 17, 9, 29, 719, DateTimeKind.Local).AddTicks(9669));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2021, 2, 10, 17, 9, 29, 719, DateTimeKind.Local).AddTicks(9670));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2021, 2, 10, 17, 9, 29, 719, DateTimeKind.Local).AddTicks(9672));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2021, 2, 10, 17, 9, 29, 719, DateTimeKind.Local).AddTicks(9674));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2021, 2, 10, 17, 9, 29, 719, DateTimeKind.Local).AddTicks(9675));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "UserRole",
                keyColumn: "UserRoleId",
                keyValue: 100,
                column: "IsCombinedRole",
                value: true);
        }
    }
}
