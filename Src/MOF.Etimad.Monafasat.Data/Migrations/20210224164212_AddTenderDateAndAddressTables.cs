using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MOF.Etimad.Monafasat.Data.Migrations
{
    public partial class AddTenderDateAndAddressTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Versions");

            migrationBuilder.AlterTable(
                name: "TenderMentainanceRunnigWork",
                schema: "Tender",
                comment: "Describe the relation between Tender and Maintenance Runnig Work");

            migrationBuilder.AlterTable(
                name: "TenderCountry",
                schema: "Tender",
                comment: "Describe the relation between Tender and Country");

            migrationBuilder.AlterTable(
                name: "TenderChangeRequest",
                schema: "Tender",
                comment: "Describe the Change request on Tender");

            migrationBuilder.AlterTable(
                name: "TenderArea",
                schema: "Tender",
                comment: "Describe the relation between Tender and Areas");

            migrationBuilder.AlterTable(
                name: "TenderAgreementAgency",
                schema: "Tender",
                comment: "Describe the relation between Tender and Agencies");

            migrationBuilder.AlterTable(
                name: "TenderActivity",
                schema: "Tender",
                comment: "Describe the Tender activities");

            migrationBuilder.AlterTable(
                name: "Attachment",
                schema: "Tender",
                comment: "Describe the Atachment for tender");

            migrationBuilder.AlterTable(
                name: "SupplierTenderQuantityTableItemJson",
                schema: "Offer",
                comment: "Contains Quantity table Items ");

            migrationBuilder.AlterTable(
                name: "UserNotificationSetting",
                schema: "Notification",
                comment: "Contains the user settings for every notification");

            migrationBuilder.AlterTable(
                name: "Notification",
                schema: "Notification",
                comment: "Contains the Data of Notifications");

            migrationBuilder.AlterTable(
                name: "MobileAlert",
                schema: "Mobile",
                comment: "Not Used");

            migrationBuilder.AlterTable(
                name: "DeviceTokenNotification",
                schema: "Mobile",
                comment: "Not Used");

            migrationBuilder.AlterTable(
                name: "DeviceToken",
                schema: "Mobile",
                comment: "Not Used");

            migrationBuilder.AlterTable(
                name: "SupplierSecondNegotiationStatus",
                schema: "LookUps",
                comment: "Define the Status of Second Stage Negotiation like [Approved , Supplier Agree , ETC..]");

            migrationBuilder.AlterTable(
                name: "NotificationOperationCode",
                schema: "LookUps",
                comment: "Describe the Notifications Templates and involved roles");

            migrationBuilder.AlterTable(
                name: "NotificationCategory",
                schema: "LookUps",
                comment: "Define the diffrent categories of notification ");

            migrationBuilder.AlterTable(
                name: "OfferPresentationWay",
                schema: "Lookups",
                comment: "Define the way that supplier can apply offer if it was on one file or two files",
                oldComment: "Define the Offer Presentation Way lookup");

            migrationBuilder.AlterTable(
                name: "City",
                schema: "Lookups",
                comment: "look up for all cities");

            migrationBuilder.AlterTable(
                name: "UnRegisteredSuppliersInvitation",
                schema: "Invitation",
                comment: "Contains the Invitations for uregistered suppliers");

            migrationBuilder.AlterTable(
                name: "_UserAudit",
                schema: "dbo",
                comment: "Descripes the Auditing Data to track users Actions");

            migrationBuilder.AlterTable(
                name: "NegotiationSupplierQuantityTable",
                schema: "CommunicationRequest",
                comment: "Define the Quantity Table for the Negotiation");

            migrationBuilder.AlterTable(
                name: "BranchUser",
                schema: "Branch",
                comment: "Contain Data for Users for each Branch");

            migrationBuilder.AlterTable(
                name: "BranchCommittee",
                schema: "Branch",
                comment: "Contain the relation between Branches and Committees");

            migrationBuilder.AlterTable(
                name: "BranchAddresse",
                schema: "Branch",
                comment: "Contain the Branch address data");

            migrationBuilder.AlterTable(
                name: "Branch",
                schema: "Branch",
                comment: "Contain the Brnach main data");

            migrationBuilder.AlterColumn<string>(
                name: "VerificationTypeName",
                schema: "Verification",
                table: "VerificationType",
                maxLength: 1024,
                nullable: true,
                comment: "define name of verification type",
                oldClrType: typeof(string),
                oldType: "nvarchar(1024)",
                oldMaxLength: 1024,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "VerificationTypeId",
                schema: "Verification",
                table: "VerificationType",
                nullable: false,
                comment: "define identity of verification type",
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "VerificationTypeId",
                schema: "Verification",
                table: "VerificationCode",
                nullable: false,
                comment: "it's a foreign  key that described type of verification code",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "VerificationCodeNo",
                schema: "Verification",
                table: "VerificationCode",
                maxLength: 1024,
                nullable: true,
                comment: "describe verfication code number",
                oldClrType: typeof(string),
                oldType: "nvarchar(1024)",
                oldMaxLength: 1024,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                schema: "Verification",
                table: "VerificationCode",
                nullable: false,
                comment: "refer to who received verfication code",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                schema: "Verification",
                table: "VerificationCode",
                maxLength: 256,
                nullable: true,
                comment: "Determine who updated  verification code",
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                schema: "Verification",
                table: "VerificationCode",
                nullable: true,
                comment: "Define updated date of  verification code",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                schema: "Verification",
                table: "VerificationCode",
                nullable: true,
                comment: "Define  verification code  is active or not",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpiredDate",
                schema: "Verification",
                table: "VerificationCode",
                nullable: false,
                comment: "define expiry date of verfication code",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                schema: "Verification",
                table: "VerificationCode",
                maxLength: 256,
                nullable: true,
                comment: "Determine who cretead verification code",
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "Verification",
                table: "VerificationCode",
                nullable: false,
                comment: "Define created date of  verification code",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<int>(
                name: "VerificationCodeId",
                schema: "Verification",
                table: "VerificationCode",
                nullable: false,
                comment: "define identity of verification code",
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "VacationName",
                schema: "Tender",
                table: "VacationsDate",
                maxLength: 1024,
                nullable: true,
                comment: "define name of vacation ",
                oldClrType: typeof(string),
                oldType: "nvarchar(1024)",
                oldMaxLength: 1024,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "VacationDate",
                schema: "Tender",
                table: "VacationsDate",
                nullable: true,
                comment: "define date of vacation ",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "VacationId",
                schema: "Tender",
                table: "VacationsDate",
                nullable: false,
                comment: "define identity of vacation",
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "TenderId",
                schema: "Tender",
                table: "TenderMentainanceRunnigWork",
                nullable: false,
                comment: "Define the related Tender",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "MaintenanceRunningWorkId",
                schema: "Tender",
                table: "TenderMentainanceRunnigWork",
                nullable: false,
                comment: "Define the related Maintenance Runnig Work",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                schema: "Tender",
                table: "TenderMentainanceRunnigWork",
                nullable: false,
                comment: "Define the unique udentifier for Tender Maintenance Runnig Work",
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "TenderId",
                schema: "Tender",
                table: "TenderCountry",
                nullable: false,
                comment: "Define the related Tender",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CountryId",
                schema: "Tender",
                table: "TenderCountry",
                nullable: false,
                comment: "Define the related Country",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                schema: "Tender",
                table: "TenderCountry",
                nullable: false,
                comment: "Define the unique udentifier for Tender Country",
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "TenderId",
                schema: "Tender",
                table: "TenderChangeRequest",
                nullable: false,
                comment: "Define the Related Tender",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "RequestedByRoleName",
                schema: "Tender",
                table: "TenderChangeRequest",
                nullable: true,
                comment: "Define the user role who created the Request",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RejectionReason",
                schema: "Tender",
                table: "TenderChangeRequest",
                nullable: true,
                comment: "Define the Rejection Reason if the request was rejected",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "HasAlternativeOffer",
                schema: "Tender",
                table: "TenderChangeRequest",
                nullable: true,
                comment: "Define if the Tender allow alternative offer, used if the request was Change in Quantity Table",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ChangeRequestTypeId",
                schema: "Tender",
                table: "TenderChangeRequest",
                nullable: false,
                comment: "Define the type of change if it was [Change Dates, Change in files ,  Cancelation Request ....ETC]",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ChangeRequestStatusId",
                schema: "Tender",
                table: "TenderChangeRequest",
                nullable: false,
                comment: "Define the status of the Request",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CancelationReasonId",
                schema: "Tender",
                table: "TenderChangeRequest",
                nullable: true,
                comment: "Define the reason why the  Cancelation Request created",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CancelationReasonDescription",
                schema: "Tender",
                table: "TenderChangeRequest",
                maxLength: 1000,
                nullable: true,
                comment: "Define the Cancelation Request Reason Description",
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TenderChangeRequestId",
                schema: "Tender",
                table: "TenderChangeRequest",
                nullable: false,
                comment: "Define the unique udentifier for Tender Change Request",
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "TenderId",
                schema: "Tender",
                table: "TenderArea",
                nullable: false,
                comment: "Define the related Tender",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "AreaId",
                schema: "Tender",
                table: "TenderArea",
                nullable: false,
                comment: "Define the related Area",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                schema: "Tender",
                table: "TenderArea",
                nullable: false,
                comment: "Define the unique udentifier for Tender area",
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "TenderId",
                schema: "Tender",
                table: "TenderAgreementAgency",
                nullable: false,
                comment: "Define the related Tender",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "AgencyCode",
                schema: "Tender",
                table: "TenderAgreementAgency",
                nullable: true,
                comment: "Define the related Agency",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TenderAgreementAgencyId",
                schema: "Tender",
                table: "TenderAgreementAgency",
                nullable: false,
                comment: "Define the unique udentifier for Tender Agreement Agency",
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "TenderId",
                schema: "Tender",
                table: "TenderActivity",
                nullable: false,
                comment: "Define the related Tender",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ActivityId",
                schema: "Tender",
                table: "TenderActivity",
                nullable: false,
                comment: "Define the related Activity",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "TenderActivityId",
                schema: "Tender",
                table: "TenderActivity",
                nullable: false,
                comment: "Define the unique udentifier for Tender Activity",
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "VROCommitteeId",
                schema: "Tender",
                table: "Tender",
                nullable: true,
                comment: "The committe of VRO for opening, checking and awarding Tender",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldComment: "The committe of VRO for opening, checking and awarding tender");

            migrationBuilder.AlterColumn<int>(
                name: "TenderUnitStatusId",
                schema: "Tender",
                table: "Tender",
                nullable: true,
                comment: "The status of Tender at unit review",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldComment: "The status of tender at unit review");

            migrationBuilder.AlterColumn<string>(
                name: "TenderTypeOtherSelectionReason",
                schema: "Tender",
                table: "Tender",
                maxLength: 1024,
                nullable: true,
                comment: "Define the reason of selecting direct purchase or limited Tender that not exist in reasons list",
                oldClrType: typeof(string),
                oldType: "nvarchar(1024)",
                oldMaxLength: 1024,
                oldNullable: true,
                oldComment: "Define the reason of selecting direct purchase or limited tender that not exist in reasons list");

            migrationBuilder.AlterColumn<int>(
                name: "TenderTypeId",
                schema: "Tender",
                table: "Tender",
                nullable: false,
                comment: "Type of Tender",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Type of tender");

            migrationBuilder.AlterColumn<int>(
                name: "TenderStatusId",
                schema: "Tender",
                table: "Tender",
                nullable: false,
                comment: "Status of Tender",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Status of tender");

            migrationBuilder.AlterColumn<int>(
                name: "TenderFirstStageId",
                schema: "Tender",
                table: "Tender",
                nullable: true,
                comment: "The identity of Tender of type first stage",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldComment: "The identity of tender of type first stage");

            migrationBuilder.AlterColumn<int>(
                name: "TenderConditionsTemplateId",
                schema: "Tender",
                table: "Tender",
                nullable: true,
                comment: "The Activity template of Tender",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldComment: "The Activity template of tender");

            migrationBuilder.AlterColumn<bool>(
                name: "TenderAwardingType",
                schema: "Tender",
                table: "Tender",
                nullable: true,
                comment: "Define the awarding type of Tender partial or total awarding",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true,
                oldComment: "Define the awarding type of tender partial or total awarding");

            migrationBuilder.AlterColumn<bool>(
                name: "SupplierNeedSample",
                schema: "Tender",
                table: "Tender",
                nullable: true,
                comment: "Boolean detrmine tf the supplier need samples of the Tender",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true,
                oldComment: "Boolean detrmine tf the supplier need samples of the tender");

            migrationBuilder.AlterColumn<DateTime>(
                name: "SubmitionDate",
                schema: "Tender",
                table: "Tender",
                nullable: true,
                comment: "Define the publish/approval date of Tender",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldComment: "Define the publish/approval date of tender");

            migrationBuilder.AlterColumn<int>(
                name: "SpendingCategoryId",
                schema: "Tender",
                table: "Tender",
                nullable: true,
                comment: "Define the spending Category of the Tender",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldComment: "Define the spending Category of the tender");

            migrationBuilder.AlterColumn<string>(
                name: "SamplesDeliveryAddress",
                schema: "Tender",
                table: "Tender",
                maxLength: 2048,
                nullable: true,
                comment: "Define the address of samples deleviry of the Tender",
                oldClrType: typeof(string),
                oldType: "nvarchar(2048)",
                oldMaxLength: 2048,
                oldNullable: true,
                oldComment: "Define the address of samples deleviry of the tender");

            migrationBuilder.AlterColumn<int>(
                name: "ReasonForPurchaseTenderTypeId",
                schema: "Tender",
                table: "Tender",
                nullable: true,
                comment: "The reason of selecting direct purchase Tender",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldComment: "The reason of selecting direct purchase tender");

            migrationBuilder.AlterColumn<int>(
                name: "ReasonForLimitedTenderTypeId",
                schema: "Tender",
                table: "Tender",
                nullable: true,
                comment: "The reason of selecting limited Tender",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldComment: "The reason of selecting limited tender");

            migrationBuilder.AlterColumn<int>(
                name: "PreQualificationId",
                schema: "Tender",
                table: "Tender",
                nullable: true,
                comment: "The identity that identify pre qualification on the Tender",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldComment: "The identity that identify pre qualification on the tender");

            migrationBuilder.AlterColumn<int>(
                name: "PreAnnouncementId",
                schema: "Tender",
                table: "Tender",
                nullable: true,
                comment: "Tha pre-announcement related to Tender",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldComment: "Tha pre-announcement related to tender");

            migrationBuilder.AlterColumn<int>(
                name: "PostQualificationTenderId",
                schema: "Tender",
                table: "Tender",
                nullable: true,
                comment: "The identity that identify post qualification on the Tender",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldComment: "The identity that identify post qualification on the tender");

            migrationBuilder.AlterColumn<int>(
                name: "OffersOpeningAddressId",
                schema: "Tender",
                table: "Tender",
                nullable: true,
                comment: "The Identity that identify address of open Tender offers",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldComment: "The Identity that identify address of open tender offers");

            migrationBuilder.AlterColumn<int>(
                name: "OffersCheckingCommitteeId",
                schema: "Tender",
                table: "Tender",
                nullable: true,
                comment: "The Checking committee for checking and awarding Tender",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldComment: "The Checking committee for checking and awarding tender");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastEnqueriesDate",
                schema: "Tender",
                table: "Tender",
                nullable: true,
                comment: "Define the last date that supplier can enquiry or ask questions for Tender",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldComment: "Define the last date that supplier can enquiry or ask questions for tender");

            migrationBuilder.AlterColumn<bool>(
                name: "IsSendToEmarketPlace",
                schema: "Tender",
                table: "Tender",
                nullable: true,
                comment: "Flag determine if the awarded Tender is sent to e-market place or not",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true,
                oldComment: "Flag determine if the awarded tender is sent to e-market place or not");

            migrationBuilder.AlterColumn<bool>(
                name: "IsNotificationSentForStoppingPeriod",
                schema: "Tender",
                table: "Tender",
                nullable: true,
                comment: "Flag determine if a notification sent after finishing the stoping period of Tender",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true,
                oldComment: "Flag determine if a notification sent after finishing the stoping period of tender");

            migrationBuilder.AlterColumn<bool>(
                name: "IsLowBudgetDirectPurchase",
                schema: "Tender",
                table: "Tender",
                nullable: true,
                comment: "determine if the Tender is low budget or not if the estimatinn value less than 30000 sar",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true,
                oldComment: "determine if the tender is low budget or not if the estimatinn value less than 30000 sar");

            migrationBuilder.AlterColumn<int>(
                name: "InvitationTypeId",
                schema: "Tender",
                table: "Tender",
                nullable: true,
                comment: "Type of invitation on Tender public or private",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldComment: "Type of invitation on tender public or private");

            migrationBuilder.AlterColumn<bool>(
                name: "InsideKSA",
                schema: "Tender",
                table: "Tender",
                nullable: true,
                comment: "Define the location of Tender",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true,
                oldComment: "Define the location of tender");

            migrationBuilder.AlterColumn<bool>(
                name: "HasGuarantee",
                schema: "Tender",
                table: "Tender",
                nullable: true,
                comment: "Boolean determine if the Tender requires a final guarantee or not",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true,
                oldComment: "Boolean determine if the tender requires a final guarantee or not");

            migrationBuilder.AlterColumn<decimal>(
                name: "EstimatedValue",
                schema: "Tender",
                table: "Tender",
                nullable: true,
                comment: "Define the estimation value of Tender",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true,
                oldComment: "Define the estimation value of tender");

            migrationBuilder.AlterColumn<int>(
                name: "DirectPurchaseCommitteeId",
                schema: "Tender",
                table: "Tender",
                nullable: true,
                comment: "The direct purchase committee for checking and awarding Tender from type direct purchase",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldComment: "The direct purchase committee for checking and awarding tender from type direct purchase");

            migrationBuilder.AlterColumn<string>(
                name: "Details",
                schema: "Tender",
                table: "Tender",
                maxLength: 5000,
                nullable: true,
                comment: "Define more information about Tender",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldMaxLength: 5000,
                oldNullable: true,
                oldComment: "Define more information about tender");

            migrationBuilder.AlterColumn<int>(
                name: "CreatedByTypeId",
                schema: "Tender",
                table: "Tender",
                nullable: true,
                comment: "determine the Tender created by vro or agency or agency related by vro",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldComment: "determine the tender created by vro or agency or agency related by vro");

            migrationBuilder.AlterColumn<int>(
                name: "BranchId",
                schema: "Tender",
                table: "Tender",
                nullable: false,
                comment: "The branch that create a Tender",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "The branch that create a tender");

            migrationBuilder.AlterColumn<int>(
                name: "AwardingStoppingPeriod",
                schema: "Tender",
                table: "Tender",
                nullable: true,
                comment: "Define the period that suppliers can add plaint on Tender after awarding between 5 and 10 days",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldComment: "Define the period that suppliers can add plaint on tender after awarding between 5 and 10 days");

            migrationBuilder.AlterColumn<int>(
                name: "AnnouncementTemplateId",
                schema: "Tender",
                table: "Tender",
                nullable: true,
                comment: "Tha announcement list of suppliers related to Tender",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldComment: "Tha announcement list of suppliers related to tender");

            migrationBuilder.AlterColumn<string>(
                name: "AgencyCode",
                schema: "Tender",
                table: "Tender",
                maxLength: 20,
                nullable: true,
                comment: "The Agency code of agency that create a Tender",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "The Agency code of agency that create a tender");

            migrationBuilder.AlterColumn<string>(
                name: "ActivityDescription",
                schema: "Tender",
                table: "Tender",
                maxLength: 2000,
                nullable: true,
                comment: "Define the description of Tender activiites",
                oldClrType: typeof(string),
                oldType: "nvarchar(2000)",
                oldMaxLength: 2000,
                oldNullable: true,
                oldComment: "Define the description of tender activiites");

            migrationBuilder.AddColumn<decimal>(
                name: "FinalGuaranteePercentage",
                schema: "Tender",
                table: "Tender",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TenderId",
                schema: "Tender",
                table: "ConditionsBooklet",
                nullable: false,
                comment: "it's a forigne key that described Tender related to condtion booklet",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "it's a forigne key that described tender related to condtion booklet");

            migrationBuilder.AlterColumn<int>(
                name: "TenderId",
                schema: "Tender",
                table: "Attachment",
                nullable: false,
                comment: "Define the related Tender",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ReviewStatusId",
                schema: "Tender",
                table: "Attachment",
                nullable: true,
                comment: "Define status of review ",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RejectionReason",
                schema: "Tender",
                table: "Attachment",
                maxLength: 1024,
                nullable: true,
                comment: "Rejection reason if there were change request on the attachment",
                oldClrType: typeof(string),
                oldType: "nvarchar(1024)",
                oldMaxLength: 1024,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "Tender",
                table: "Attachment",
                maxLength: 200,
                nullable: false,
                comment: "The name of the file attached",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "FileNetReferenceId",
                schema: "Tender",
                table: "Attachment",
                maxLength: 1024,
                nullable: true,
                comment: "Define the file net reference Id ",
                oldClrType: typeof(string),
                oldType: "nvarchar(1024)",
                oldMaxLength: 1024,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ChangeStatusId",
                schema: "Tender",
                table: "Attachment",
                nullable: true,
                comment: "Define the the status of change request",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AttachmentTypeId",
                schema: "Tender",
                table: "Attachment",
                nullable: false,
                comment: "The category of this attachment [Condition bocklet  ETC.... ]",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "TenderAttachmentId",
                schema: "Tender",
                table: "Attachment",
                nullable: false,
                comment: "Define the unique udentifier for Tender Attachment",
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "SupplierTenderQuantityTableItems",
                schema: "Offer",
                table: "SupplierTenderQuantityTableItemJson",
                nullable: true,
                comment: "Define the Quantity Table Items",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "SupplierTenderQuantityTableId",
                schema: "Offer",
                table: "SupplierTenderQuantityTableItemJson",
                nullable: false,
                comment: "Define the related Quatity Table",
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                schema: "Offer",
                table: "SupplierTenderQuantityTableItemJson",
                nullable: false,
                comment: "Define Unique identifer Of Supplier  Quantity Table items",
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<decimal>(
                name: "PartialOfferAwardingValue",
                schema: "Offer",
                table: "Offer",
                nullable: true,
                comment: "Define the partial value of awarding if the Tender partialy awarded",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true,
                oldComment: "Define the partial value of awarding if the tender partialy awarded");

            migrationBuilder.AlterColumn<int>(
                name: "UserRoleId",
                schema: "Notification",
                table: "UserNotificationSetting",
                nullable: false,
                comment: "Define the role of the user who will recieve the Notification",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "UserProfileId",
                schema: "Notification",
                table: "UserNotificationSetting",
                nullable: true,
                comment: "Define the related user if he was Governate user ",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Sms",
                schema: "Notification",
                table: "UserNotificationSetting",
                nullable: false,
                comment: "Define if the user needs to recieve SMS or Not",
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "OperationCode",
                schema: "Notification",
                table: "UserNotificationSetting",
                nullable: true,
                comment: "Define the Notification Code",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NotificationCodeId",
                schema: "Notification",
                table: "UserNotificationSetting",
                nullable: false,
                comment: "Define the Notification Code ID",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<bool>(
                name: "IsArabic",
                schema: "Notification",
                table: "UserNotificationSetting",
                nullable: false,
                comment: "Define if the user configured Notification to be in arabic Language ",
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<bool>(
                name: "Email",
                schema: "Notification",
                table: "UserNotificationSetting",
                nullable: false,
                comment: "Define if the user needs to recieve EMAIL or Not",
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "Cr",
                schema: "Notification",
                table: "UserNotificationSetting",
                nullable: true,
                comment: "Refere to the Supplier Commercial Registeration Number who will recieve the Notification",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                schema: "Notification",
                table: "UserNotificationSetting",
                nullable: false,
                comment: "Define Unique identifer Of  User Notification Setting",
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<DateTime>(
                name: "sendAt",
                schema: "Notification",
                table: "Notification",
                nullable: true,
                comment: "the date of sending the notification",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                schema: "Notification",
                table: "Notification",
                nullable: true,
                comment: "Define the user id who recieve the notification",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NotificationSettingId",
                schema: "Notification",
                table: "Notification",
                nullable: true,
                comment: "the related setting of the setting",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NotifacationStatusId",
                schema: "Notification",
                table: "Notification",
                nullable: false,
                comment: "Define the status of Notification if sent or not or Faild",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Link",
                schema: "Notification",
                table: "Notification",
                nullable: true,
                comment: "link for reciever to access direct the related page of monafasat",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Key",
                schema: "Notification",
                table: "Notification",
                nullable: true,
                comment: "compination of string 'Tender' and the Id of Tender ",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CR",
                schema: "Notification",
                table: "Notification",
                nullable: true,
                comment: "Define the Supplier id who recieve the notification",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                schema: "Notification",
                table: "Notification",
                nullable: false,
                comment: "Define Unique identifer Of Notification",
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "MessageStatusId",
                schema: "Mobile",
                table: "MobileAlert",
                nullable: false,
                comment: "Not Used",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "MessageId",
                schema: "Mobile",
                table: "MobileAlert",
                nullable: true,
                comment: "Not Used",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Message",
                schema: "Mobile",
                table: "MobileAlert",
                nullable: true,
                comment: "Not Used",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "GroupCode",
                schema: "Mobile",
                table: "MobileAlert",
                nullable: true,
                comment: "Not Used",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DeviceId",
                schema: "Mobile",
                table: "MobileAlert",
                nullable: false,
                comment: "Not Used",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "MobileAlertId",
                schema: "Mobile",
                table: "MobileAlert",
                nullable: false,
                comment: "Not Used",
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<bool>(
                name: "Status",
                schema: "Mobile",
                table: "DeviceTokenNotification",
                nullable: false,
                comment: "Not Used",
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<int>(
                name: "DeviceId",
                schema: "Mobile",
                table: "DeviceTokenNotification",
                nullable: false,
                comment: "Not Used",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ActivityId",
                schema: "Mobile",
                table: "DeviceTokenNotification",
                nullable: false,
                comment: "Not Used",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "DeviceTokenNotificationId",
                schema: "Mobile",
                table: "DeviceTokenNotification",
                nullable: false,
                comment: "Not Used",
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "UserProfileId",
                schema: "Mobile",
                table: "DeviceToken",
                nullable: true,
                comment: "Not Used",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserDeviceId",
                schema: "Mobile",
                table: "DeviceToken",
                maxLength: 60,
                nullable: true,
                comment: "Not Used",
                oldClrType: typeof(string),
                oldType: "nvarchar(60)",
                oldMaxLength: 60,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TimeStamp",
                schema: "Mobile",
                table: "DeviceToken",
                nullable: false,
                comment: "Not Used",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "SourceIP",
                schema: "Mobile",
                table: "DeviceToken",
                maxLength: 20,
                nullable: true,
                comment: "Not Used",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Model",
                schema: "Mobile",
                table: "DeviceToken",
                maxLength: 30,
                nullable: true,
                comment: "Not Used",
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DeviceVersion",
                schema: "Mobile",
                table: "DeviceToken",
                maxLength: 15,
                nullable: true,
                comment: "Not Used",
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DeviceTokenValue",
                schema: "Mobile",
                table: "DeviceToken",
                maxLength: 500,
                nullable: true,
                comment: "Not Used",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DeviceName",
                schema: "Mobile",
                table: "DeviceToken",
                maxLength: 100,
                nullable: true,
                comment: "Not Used",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DeviceId",
                schema: "Mobile",
                table: "DeviceToken",
                nullable: false,
                comment: "Not Used",
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "NameEn",
                schema: "LookUps",
                table: "VendorCertificate",
                maxLength: 1024,
                nullable: true,
                comment: "define english name of vendor certificate",
                oldClrType: typeof(string),
                oldType: "nvarchar(1024)",
                oldMaxLength: 1024,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NameAr",
                schema: "LookUps",
                table: "VendorCertificate",
                maxLength: 1024,
                nullable: true,
                comment: "define arabic name of vendor certificate",
                oldClrType: typeof(string),
                oldType: "nvarchar(1024)",
                oldMaxLength: 1024,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "VendorCertificateId",
                schema: "LookUps",
                table: "VendorCertificate",
                nullable: false,
                comment: "define identity of vendor certificate",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "LookUps",
                table: "UserRole",
                maxLength: 1024,
                nullable: true,
                comment: "define name of role",
                oldClrType: typeof(string),
                oldType: "nvarchar(1024)",
                oldMaxLength: 1024,
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsCombinedRole",
                schema: "LookUps",
                table: "UserRole",
                nullable: false,
                defaultValue: false,
                comment: "describe notification for low budget module",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "DisplayNameEn",
                schema: "LookUps",
                table: "UserRole",
                maxLength: 1024,
                nullable: true,
                comment: "define english name of role",
                oldClrType: typeof(string),
                oldType: "nvarchar(1024)",
                oldMaxLength: 1024,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DisplayNameAr",
                schema: "LookUps",
                table: "UserRole",
                maxLength: 1024,
                nullable: true,
                comment: "define arabic name of role",
                oldClrType: typeof(string),
                oldType: "nvarchar(1024)",
                oldMaxLength: 1024,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserRoleId",
                schema: "LookUps",
                table: "UserRole",
                nullable: false,
                comment: "define identity of user role",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "LookUps",
                table: "TenderUnitUpdateType",
                maxLength: 1024,
                nullable: true,
                comment: "Define the name of Tender unit type",
                oldClrType: typeof(string),
                oldType: "nvarchar(1024)",
                oldMaxLength: 1024,
                oldNullable: true,
                oldComment: "Define the name of tender unit type");

            migrationBuilder.AlterColumn<int>(
                name: "TenderUnitUpdateTypeId",
                schema: "LookUps",
                table: "TenderUnitUpdateType",
                nullable: false,
                comment: "Define identity of Tender unit type",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Define identity of tender unit type");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "LookUps",
                table: "TenderUnitStatus",
                maxLength: 100,
                nullable: true,
                comment: "Define the name of Tender unit status",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true,
                oldComment: "Define the name of tender unit status");

            migrationBuilder.AlterColumn<int>(
                name: "TenderUnitStatusId",
                schema: "LookUps",
                table: "TenderUnitStatus",
                nullable: false,
                comment: "Define identity of Tender unit status",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Define identity of tender unit status");

            migrationBuilder.AlterColumn<string>(
                name: "NameEn",
                schema: "LookUps",
                table: "TenderType",
                maxLength: 500,
                nullable: true,
                comment: "Define the english name of Tender type",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true,
                oldComment: "Define the english name of tender type");

            migrationBuilder.AlterColumn<string>(
                name: "NameAr",
                schema: "LookUps",
                table: "TenderType",
                maxLength: 500,
                nullable: true,
                comment: "Define the arabic name of Tender type",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true,
                oldComment: "Define the arabic name of tender type");

            migrationBuilder.AlterColumn<decimal>(
                name: "InvitationCost",
                schema: "LookUps",
                table: "TenderType",
                nullable: false,
                comment: "Define the Invitation Cost of Tender type",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComment: "Define the Invitation Cost of tender type");

            migrationBuilder.AlterColumn<decimal>(
                name: "BuyingCost",
                schema: "LookUps",
                table: "TenderType",
                nullable: false,
                comment: "Define the cost of buying  of Tender type",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComment: "Define the cost of buying  of tender type");

            migrationBuilder.AlterColumn<int>(
                name: "TenderTypeId",
                schema: "LookUps",
                table: "TenderType",
                nullable: false,
                comment: "Define identity of Tender type",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Define identity of tender type");

            migrationBuilder.AlterColumn<string>(
                name: "NameEn",
                schema: "LookUps",
                table: "TenderStatus",
                maxLength: 100,
                nullable: true,
                comment: "Define the english name of Tender status",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true,
                oldComment: "Define the english name of tender status");

            migrationBuilder.AlterColumn<string>(
                name: "NameAr",
                schema: "LookUps",
                table: "TenderStatus",
                maxLength: 100,
                nullable: true,
                comment: "Define the arabic name of Tender status",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true,
                oldComment: "Define the arabic name of tender status");

            migrationBuilder.AlterColumn<int>(
                name: "TenderStatusId",
                schema: "LookUps",
                table: "TenderStatus",
                nullable: false,
                comment: "Define identity of Tender status",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Define identity of tender status");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "LookUps",
                table: "SupplierSecondNegotiationStatus",
                maxLength: 100,
                nullable: true,
                comment: "Define the  Name Of Second Stage Negotiation Status",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SupplierNegotiaitionStatusId",
                schema: "LookUps",
                table: "SupplierSecondNegotiationStatus",
                nullable: false,
                comment: "Define Unique identifer Of  Second Stage Negotiation Status",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "UserRoleId",
                schema: "LookUps",
                table: "NotificationOperationCode",
                nullable: false,
                comment: "Define the related user role  that will recieve the notification",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "SmsTemplateEn",
                schema: "LookUps",
                table: "NotificationOperationCode",
                maxLength: 2000,
                nullable: true,
                comment: "The SMS English Template",
                oldClrType: typeof(string),
                oldType: "nvarchar(2000)",
                oldMaxLength: 2000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SmsTemplateAr",
                schema: "LookUps",
                table: "NotificationOperationCode",
                maxLength: 2000,
                nullable: true,
                comment: "The SMS arabic Template",
                oldClrType: typeof(string),
                oldType: "nvarchar(2000)",
                oldMaxLength: 2000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PanelTemplateEn",
                schema: "LookUps",
                table: "NotificationOperationCode",
                maxLength: 1000,
                nullable: true,
                comment: "The English Panel Subject for noification ",
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PanelTemplateAr",
                schema: "LookUps",
                table: "NotificationOperationCode",
                maxLength: 1000,
                nullable: true,
                comment: "The Arabic Panel Subject for noification ",
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "OperationCode",
                schema: "LookUps",
                table: "NotificationOperationCode",
                maxLength: 100,
                nullable: true,
                comment: "its  unique Text the represent the notification template",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NotificationCategoryId",
                schema: "LookUps",
                table: "NotificationOperationCode",
                nullable: true,
                comment: "Define if the Category of notificatopn item like [operations on Tender , negotiation ETC..]",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EnglishName",
                schema: "LookUps",
                table: "NotificationOperationCode",
                maxLength: 2000,
                nullable: true,
                comment: "Define notification template english Name",
                oldClrType: typeof(string),
                oldType: "nvarchar(2000)",
                oldMaxLength: 2000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EmailSubjectTemplateEn",
                schema: "LookUps",
                table: "NotificationOperationCode",
                maxLength: 2000,
                nullable: true,
                comment: "The English EMail Body for noification ",
                oldClrType: typeof(string),
                oldType: "nvarchar(2000)",
                oldMaxLength: 2000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EmailSubjectTemplateAr",
                schema: "LookUps",
                table: "NotificationOperationCode",
                maxLength: 2000,
                nullable: true,
                comment: "The Arabic EMail Subject for noification ",
                oldClrType: typeof(string),
                oldType: "nvarchar(2000)",
                oldMaxLength: 2000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EmailBodyTemplateAr",
                schema: "LookUps",
                table: "NotificationOperationCode",
                maxLength: 10000,
                nullable: true,
                comment: "The Arabic EMail Subject for noification ",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldMaxLength: 10000,
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "DefaultSMS",
                schema: "LookUps",
                table: "NotificationOperationCode",
                nullable: false,
                comment: "Define if the reciever role will recieve SMS by default  or not",
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<bool>(
                name: "DefaultEmail",
                schema: "LookUps",
                table: "NotificationOperationCode",
                nullable: false,
                comment: "Define if the reciever role will recieve Email by default  or not",
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<bool>(
                name: "CanEditSMS",
                schema: "LookUps",
                table: "NotificationOperationCode",
                nullable: false,
                comment: "Define if the reciever role can change Default setting for recieving SMS or not",
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<bool>(
                name: "CanEditEmail",
                schema: "LookUps",
                table: "NotificationOperationCode",
                nullable: false,
                comment: "Define if the reciever role can change Default setting for recieving Email or not",
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "ArabicName",
                schema: "LookUps",
                table: "NotificationOperationCode",
                maxLength: 2000,
                nullable: true,
                comment: "Define notification template arabic Name",
                oldClrType: typeof(string),
                oldType: "nvarchar(2000)",
                oldMaxLength: 2000,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NotificationOperationCodeId",
                schema: "LookUps",
                table: "NotificationOperationCode",
                nullable: false,
                comment: "Define Unique identifer Of Notification Operation Code",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "EnglishName",
                schema: "LookUps",
                table: "NotificationCategory",
                maxLength: 1024,
                nullable: true,
                comment: "Define the English Name Of Notification Category",
                oldClrType: typeof(string),
                oldType: "nvarchar(1024)",
                oldMaxLength: 1024,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ArabicName",
                schema: "LookUps",
                table: "NotificationCategory",
                maxLength: 1024,
                nullable: true,
                comment: "Define the arabic Name Of Notification Category",
                oldClrType: typeof(string),
                oldType: "nvarchar(1024)",
                oldMaxLength: 1024,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                schema: "LookUps",
                table: "NotificationCategory",
                nullable: false,
                comment: "Define Unique identifer Of  Notification Category",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "NotifacationStatusEntityId",
                schema: "LookUps",
                table: "NotifacationStatusEntity",
                nullable: false,
                comment: "Define Unique identifer Of Notification Status lookup [مرسل,لم يتم الارسال , فشل فى الارسال , مقروءه , غير مقروءه]",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "NameEn",
                schema: "LookUps",
                table: "AgreementType",
                maxLength: 500,
                nullable: true,
                comment: "Define the arabic name of agreement type",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NameAr",
                schema: "LookUps",
                table: "AgreementType",
                maxLength: 500,
                nullable: true,
                comment: "Define the arabic name of agreement type",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AgreementTypeId",
                schema: "LookUps",
                table: "AgreementType",
                nullable: false,
                comment: "Define the identity of agreement type",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "RelatedActivityId",
                schema: "LookUps",
                table: "Activity",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NameEn",
                schema: "Lookups",
                table: "YearQuarter",
                maxLength: 100,
                nullable: true,
                comment: "define english name of year quarter",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NameAr",
                schema: "Lookups",
                table: "YearQuarter",
                maxLength: 100,
                nullable: true,
                comment: "define arabic name of year quarter",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "YearQuarterId",
                schema: "Lookups",
                table: "YearQuarter",
                nullable: false,
                comment: "define identity of year quarter",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "NameEnglish",
                schema: "Lookups",
                table: "TenderFeesType",
                maxLength: 100,
                nullable: true,
                comment: "Define english name of Tender fees type",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true,
                oldComment: "Define english name of tender fees type");

            migrationBuilder.AlterColumn<string>(
                name: "NameArabic",
                schema: "Lookups",
                table: "TenderFeesType",
                maxLength: 100,
                nullable: true,
                comment: "Define arabic name of Tender fees type",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true,
                oldComment: "Define arabic name of tender fees type");

            migrationBuilder.AlterColumn<int>(
                name: "TenderFeesTypeId",
                schema: "Lookups",
                table: "TenderFeesType",
                nullable: false,
                comment: "Define identity of Tender fees type",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Define identity of tender fees type");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "Lookups",
                table: "OfferPresentationWay",
                maxLength: 1024,
                nullable: true,
                comment: "Define the Name Of Notification Status",
                oldClrType: typeof(string),
                oldType: "nvarchar(1024)",
                oldMaxLength: 1024,
                oldNullable: true,
                oldComment: "Define the Name Of Offer Presentation Way lookup");

            migrationBuilder.AlterColumn<string>(
                name: "NameEnglish",
                schema: "Lookups",
                table: "City",
                maxLength: 100,
                nullable: true,
                comment: "Define English name of City",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NameArabic",
                schema: "Lookups",
                table: "City",
                maxLength: 100,
                nullable: true,
                comment: "Define arabic name of City",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CityID",
                schema: "Lookups",
                table: "City",
                nullable: false,
                comment: "Define a unique identifer of City",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "TenderId",
                schema: "Invitation",
                table: "UnRegisteredSuppliersInvitation",
                nullable: false,
                comment: "Define the related Tender for the invitations",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "MobileNo",
                schema: "Invitation",
                table: "UnRegisteredSuppliersInvitation",
                nullable: true,
                comment: "Define the supplier Mobile Number",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "InvitationTypeId",
                schema: "Invitation",
                table: "UnRegisteredSuppliersInvitation",
                nullable: false,
                comment: "Define the invitaion Type if it was by email or mobile ETC...",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "InvitationStatusId",
                schema: "Invitation",
                table: "UnRegisteredSuppliersInvitation",
                nullable: true,
                comment: "Define the status of invitation id sent aor not and if accepted by supplier or Not",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                schema: "Invitation",
                table: "UnRegisteredSuppliersInvitation",
                maxLength: 1024,
                nullable: true,
                comment: "Define the supplier email that he will recieve the invitaion on it ",
                oldClrType: typeof(string),
                oldType: "nvarchar(1024)",
                oldMaxLength: 1024,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "Invitation",
                table: "UnRegisteredSuppliersInvitation",
                maxLength: 2056,
                nullable: true,
                comment: "Define the description written with the invitaion  ",
                oldClrType: typeof(string),
                oldType: "nvarchar(2056)",
                oldMaxLength: 2056,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CrNumber",
                schema: "Invitation",
                table: "UnRegisteredSuppliersInvitation",
                maxLength: 50,
                nullable: true,
                comment: "Define the supplier Commercial Registeration Number",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                schema: "Invitation",
                table: "UnRegisteredSuppliersInvitation",
                nullable: false,
                comment: "Define Unique identifer Of UnRegistered Suppliers Invitation",
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<DateTime>(
                name: "WithdrawalDate",
                schema: "Invitation",
                table: "Invitation",
                nullable: true,
                comment: "define withdrawal date of invitation",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                schema: "Invitation",
                table: "Invitation",
                maxLength: 256,
                nullable: true,
                comment: "Determine who updated invitation",
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                schema: "Invitation",
                table: "Invitation",
                nullable: true,
                comment: "Define updated date of invitation",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TenderId",
                schema: "Invitation",
                table: "Invitation",
                nullable: false,
                comment: "it's a foreign key described Tender that related to  invitation",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "SupplierType",
                schema: "Invitation",
                table: "Invitation",
                nullable: true,
                comment: "define supplier type  of invitation",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "SubmissionDate",
                schema: "Invitation",
                table: "Invitation",
                nullable: true,
                comment: "define submission date of invitation",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "StatusId",
                schema: "Invitation",
                table: "Invitation",
                nullable: false,
                comment: "it's a foreign  key that described status of invitation",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "SendingDate",
                schema: "Invitation",
                table: "Invitation",
                nullable: true,
                comment: "define sending date of invitation",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RejectionReason",
                schema: "Invitation",
                table: "Invitation",
                nullable: true,
                comment: "define reject reason of invitation",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                schema: "Invitation",
                table: "Invitation",
                nullable: true,
                comment: "Define invitation is active or not",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "InvitedByQualification",
                schema: "Invitation",
                table: "Invitation",
                nullable: true,
                comment: "define supplier invited by qualification or not",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "InvitationTypeId",
                schema: "Invitation",
                table: "Invitation",
                nullable: false,
                comment: "it's a foreign  key that described type of invitation",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                schema: "Invitation",
                table: "Invitation",
                maxLength: 256,
                nullable: true,
                comment: "Determine who cretead invitation",
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "Invitation",
                table: "Invitation",
                nullable: false,
                comment: "Define created date of invitation",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "CommericalRegisterNo",
                schema: "Invitation",
                table: "Invitation",
                maxLength: 20,
                nullable: true,
                comment: "define supplier commerical register number",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "InvitationId",
                schema: "Invitation",
                table: "Invitation",
                nullable: false,
                comment: "define identity of invitation",
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                schema: "IDM",
                table: "UserProfile",
                maxLength: 20,
                nullable: true,
                comment: "define user name of user profile",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                schema: "IDM",
                table: "UserProfile",
                maxLength: 256,
                nullable: true,
                comment: "Determine who updated user profile",
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                schema: "IDM",
                table: "UserProfile",
                nullable: true,
                comment: "Define updated date of user profile",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<byte[]>(
                name: "RowVersion",
                schema: "IDM",
                table: "UserProfile",
                nullable: true,
                comment: "define row version of user profile",
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Mobile",
                schema: "IDM",
                table: "UserProfile",
                maxLength: 20,
                nullable: true,
                comment: "define mobile of user profile",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                schema: "IDM",
                table: "UserProfile",
                nullable: true,
                comment: "Define user profile is active or not",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                schema: "IDM",
                table: "UserProfile",
                maxLength: 200,
                nullable: true,
                comment: "define full name of user profile",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                schema: "IDM",
                table: "UserProfile",
                maxLength: 100,
                nullable: true,
                comment: "define email of user profile",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DefaultUserRole",
                schema: "IDM",
                table: "UserProfile",
                maxLength: 200,
                nullable: true,
                comment: "define default user role of user profile",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                schema: "IDM",
                table: "UserProfile",
                maxLength: 256,
                nullable: true,
                comment: "Determine who cretead user profile",
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "IDM",
                table: "UserProfile",
                nullable: false,
                comment: "Define created date of user profile",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                schema: "IDM",
                table: "UserProfile",
                nullable: false,
                comment: "define identity of user profile",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                schema: "dbo",
                table: "_UserAudit",
                nullable: true,
                comment: "Define User Name of the user who take the action ",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                schema: "dbo",
                table: "_UserAudit",
                nullable: true,
                comment: "Define the user id who making the action",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                schema: "dbo",
                table: "_UserAudit",
                nullable: false,
                comment: "Define time that action was done on ",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "ProcessId",
                schema: "dbo",
                table: "_UserAudit",
                nullable: true,
                comment: "Unique Number to the process",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Process",
                schema: "dbo",
                table: "_UserAudit",
                nullable: true,
                comment: "Define the Process status Success or Fail",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IpAddress",
                schema: "dbo",
                table: "_UserAudit",
                nullable: true,
                comment: "Define ip Address of Device that the user using ",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                schema: "dbo",
                table: "_UserAudit",
                nullable: true,
                comment: "Define User Full Name of the user who take the action",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AuditEvent",
                schema: "dbo",
                table: "_UserAudit",
                nullable: true,
                comment: "Define the type of Action if Edit or Add or Delete",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                schema: "dbo",
                table: "_UserAudit",
                nullable: false,
                comment: "Define Unique identifer Of  User Action Auditting",
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "refNegotiationSecondStage",
                schema: "CommunicationRequest",
                table: "NegotiationSupplierQuantityTable",
                nullable: false,
                comment: "Refere to the related Negotioation Request ",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<long>(
                name: "SupplierQuantityTableId",
                schema: "CommunicationRequest",
                table: "NegotiationSupplierQuantityTable",
                nullable: false,
                comment: "Refer to the Original Supplier Quantity Table that Filled by supplier",
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "CommunicationRequest",
                table: "NegotiationSupplierQuantityTable",
                nullable: false,
                comment: "Define the Name Of Quantity Table ",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                schema: "CommunicationRequest",
                table: "NegotiationSupplierQuantityTable",
                nullable: false,
                comment: "Define Unique identifer Of Quantity Table ",
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "SupplierExtendOfferValidityStatusId",
                schema: "CommunicationRequest",
                table: "ExtendOffersValiditySupplier",
                nullable: true,
                comment: "Define the status of extend offers validity supplier based on supplier action on request",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SupplierCR",
                schema: "CommunicationRequest",
                table: "ExtendOffersValiditySupplier",
                nullable: true,
                comment: "Define the supplier cr that extend offers validity sent to",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "PeriodStartDateTime",
                schema: "CommunicationRequest",
                table: "ExtendOffersValiditySupplier",
                nullable: true,
                comment: "Define the the start date of extend offers validity period",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "OfferId",
                schema: "CommunicationRequest",
                table: "ExtendOffersValiditySupplier",
                nullable: false,
                comment: "Define the related  supplier offer for extend offers validity",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<bool>(
                name: "IsReported",
                schema: "CommunicationRequest",
                table: "ExtendOffersValiditySupplier",
                nullable: false,
                comment: "Define if the request is reported or not",
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<int>(
                name: "ExtendOffersValidityId",
                schema: "CommunicationRequest",
                table: "ExtendOffersValiditySupplier",
                nullable: false,
                comment: "Define the parent extend offers validity for extend offers validity supplier request",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                schema: "CommunicationRequest",
                table: "ExtendOffersValiditySupplier",
                nullable: false,
                comment: "Define a unique identifer of extend offers validity supplier",
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "ReplyReceivingDurationTime",
                schema: "CommunicationRequest",
                table: "ExtendOffersValidity",
                nullable: true,
                comment: "Define the time to reply the extend offers validity request",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ReplyReceivingDurationDays",
                schema: "CommunicationRequest",
                table: "ExtendOffersValidity",
                nullable: false,
                comment: "Define number of days to allow suppliers to reply the extend offers validity request",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "OffersDuration",
                schema: "CommunicationRequest",
                table: "ExtendOffersValidity",
                nullable: false,
                comment: "Define the duration in days to end expire the extend offers validity request",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "NewOffersExpiryDate",
                schema: "CommunicationRequest",
                table: "ExtendOffersValidity",
                nullable: false,
                comment: "Define the expiration date of extend offers validity request",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "ExtendOffersReason",
                schema: "CommunicationRequest",
                table: "ExtendOffersValidity",
                nullable: true,
                comment: "Define the reason of extend offers validity request",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AgencyCommunicationRequestId",
                schema: "CommunicationRequest",
                table: "ExtendOffersValidity",
                nullable: false,
                comment: "Define the parent commmunication request for extend offers validity request",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ExtendOffersValidityId",
                schema: "CommunicationRequest",
                table: "ExtendOffersValidity",
                nullable: false,
                comment: "Define a unique identifer of extend offers validity request",
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "TenderId",
                schema: "CommunicationRequest",
                table: "AgencyCommunicationRequest",
                nullable: false,
                comment: "Define the related Tender of agency communication request",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Define the related tender of agency communication request");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "CommunicationAgency",
                table: "ExtendOffersValidityAttachment",
                maxLength: 200,
                nullable: false,
                comment: "Define the name of file attached",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "FileNetReferenceId",
                schema: "CommunicationAgency",
                table: "ExtendOffersValidityAttachment",
                maxLength: 1024,
                nullable: true,
                comment: "Define The reference number from file Net",
                oldClrType: typeof(string),
                oldType: "nvarchar(1024)",
                oldMaxLength: 1024,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ExtendOffersValiditySupplierId",
                schema: "CommunicationAgency",
                table: "ExtendOffersValidityAttachment",
                nullable: false,
                comment: "Define the parent extend offers validity for extend offers validity supplier request",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "AttachmentTypeId",
                schema: "CommunicationAgency",
                table: "ExtendOffersValidityAttachment",
                nullable: false,
                comment: "the type of attachment file like [supplier attachment or agency attachment]",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "AttachmentId",
                schema: "CommunicationAgency",
                table: "ExtendOffersValidityAttachment",
                nullable: false,
                comment: "Define the parent extend offers validity for extend offers validity supplier request",
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "UserRoleId",
                schema: "Branch",
                table: "BranchUser",
                nullable: false,
                comment: "Define user role inside the branch",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "UserProfileId",
                schema: "Branch",
                table: "BranchUser",
                nullable: false,
                comment: "reference the user full profile",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "RelatedAgencyCode",
                schema: "Branch",
                table: "BranchUser",
                nullable: true,
                comment: "Define the agency which the branch is related to",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "EstimatedValueTo",
                schema: "Branch",
                table: "BranchUser",
                nullable: true,
                comment: "Define the biggest estimated value",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "EstimatedValueFrom",
                schema: "Branch",
                table: "BranchUser",
                nullable: true,
                comment: "Define the smallest estimated value",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BranchId",
                schema: "Branch",
                table: "BranchUser",
                nullable: false,
                comment: "Refere to the Branch",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "BranchUserId",
                schema: "Branch",
                table: "BranchUser",
                nullable: false,
                comment: "Define a unique identifer of Branch User",
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "CommitteeId",
                schema: "Branch",
                table: "BranchCommittee",
                nullable: false,
                comment: "Refere to the Committie",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "BranchId",
                schema: "Branch",
                table: "BranchCommittee",
                nullable: false,
                comment: "Refere to the Branch",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "BranchCommitteeId",
                schema: "Branch",
                table: "BranchCommittee",
                nullable: false,
                comment: "Define a unique identifer of Branch Committee",
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "ZipCode",
                schema: "Branch",
                table: "BranchAddresse",
                maxLength: 1024,
                nullable: true,
                comment: "Define Zip Code of the address",
                oldClrType: typeof(string),
                oldType: "nvarchar(1024)",
                oldMaxLength: 1024,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PostalCode",
                schema: "Branch",
                table: "BranchAddresse",
                maxLength: 1024,
                nullable: true,
                comment: "Define Postal Code of the address",
                oldClrType: typeof(string),
                oldType: "nvarchar(1024)",
                oldMaxLength: 1024,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Position",
                schema: "Branch",
                table: "BranchAddresse",
                maxLength: 1024,
                nullable: true,
                comment: "Define the Position of the address",
                oldClrType: typeof(string),
                oldType: "nvarchar(1024)",
                oldMaxLength: 1024,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Phone2",
                schema: "Branch",
                table: "BranchAddresse",
                maxLength: 100,
                nullable: true,
                comment: "Define second phone number of the address",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                schema: "Branch",
                table: "BranchAddresse",
                maxLength: 100,
                nullable: true,
                comment: "Define phone number of the address",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Fax2",
                schema: "Branch",
                table: "BranchAddresse",
                maxLength: 100,
                nullable: true,
                comment: "Define second FAX number of the address",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Fax",
                schema: "Branch",
                table: "BranchAddresse",
                maxLength: 100,
                nullable: true,
                comment: "Define FAX number of the address",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "Branch",
                table: "BranchAddresse",
                maxLength: 1024,
                nullable: true,
                comment: "Define Description of the address",
                oldClrType: typeof(string),
                oldType: "nvarchar(1024)",
                oldMaxLength: 1024,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CityCode",
                schema: "Branch",
                table: "BranchAddresse",
                maxLength: 1024,
                nullable: true,
                comment: "Define City Code of the address",
                oldClrType: typeof(string),
                oldType: "nvarchar(1024)",
                oldMaxLength: 1024,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AddressTypeId",
                schema: "Branch",
                table: "BranchAddresse",
                nullable: false,
                comment: "Define the type of address",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "AddressName",
                schema: "Branch",
                table: "BranchAddresse",
                maxLength: 1024,
                nullable: true,
                comment: "Define the branch Address name",
                oldClrType: typeof(string),
                oldType: "nvarchar(1024)",
                oldMaxLength: 1024,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                schema: "Branch",
                table: "BranchAddresse",
                maxLength: 1024,
                nullable: true,
                comment: "Define The detailed loction  of the Branch Address",
                oldClrType: typeof(string),
                oldType: "nvarchar(1024)",
                oldMaxLength: 1024,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BranchAddressId",
                schema: "Branch",
                table: "BranchAddresse",
                nullable: false,
                comment: "Define a unique identifer of Branch Address",
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "BranchName",
                schema: "Branch",
                table: "Branch",
                maxLength: 1024,
                nullable: false,
                comment: "Define the branch name",
                oldClrType: typeof(string),
                oldType: "nvarchar(1024)",
                oldMaxLength: 1024);

            migrationBuilder.AlterColumn<string>(
                name: "AgencyCode",
                schema: "Branch",
                table: "Branch",
                nullable: true,
                comment: "Define the related Agency",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BranchId",
                schema: "Branch",
                table: "Branch",
                nullable: false,
                comment: "Define a unique identifer of Branch",
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "TenderTypesId",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSupplierTemplate",
                nullable: true,
                comment: "Define type of Tender",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "Define type of tender");

            migrationBuilder.CreateTable(
                name: "VersionType",
                schema: "Lookups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    NameEnglish = table.Column<string>(maxLength: 100, nullable: true),
                    NameArabic = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VersionType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TenderAddress",
                schema: "Tender",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenderId = table.Column<int>(nullable: false),
                    IsSampleAddresSameOffersAddress = table.Column<bool>(nullable: true),
                    OffersDeliveryAddress = table.Column<string>(maxLength: 200, nullable: true),
                    OfferBuildingName = table.Column<string>(maxLength: 100, nullable: true),
                    OfferFloarNumber = table.Column<string>(maxLength: 100, nullable: true),
                    OfferDepartmentName = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenderAddress", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TenderAddress_Tender_TenderId",
                        column: x => x.TenderId,
                        principalSchema: "Tender",
                        principalTable: "Tender",
                        principalColumn: "TenderId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TenderDates",
                schema: "Tender",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenderId = table.Column<int>(nullable: false),
                    AwardingExpectedDate = table.Column<DateTime>(nullable: true),
                    StartingBusinessOrServicesDate = table.Column<DateTime>(nullable: true),
                    ParticipationConfirmationLetterDate = table.Column<DateTime>(nullable: true),
                    OffersDeliveryDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenderDates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TenderDates_Tender_TenderId",
                        column: x => x.TenderId,
                        principalSchema: "Tender",
                        principalTable: "Tender",
                        principalColumn: "TenderId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TenderLocalContent",
                schema: "Tender",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenderId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenderLocalContent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TenderLocalContent_Tender_TenderId",
                        column: x => x.TenderId,
                        principalSchema: "Tender",
                        principalTable: "Tender",
                        principalColumn: "TenderId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Versions",
                schema: "Versions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    IsCurrentVersion = table.Column<bool>(nullable: false),
                    VersionTypeId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Versions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Versions_VersionType_VersionTypeId",
                        column: x => x.VersionTypeId,
                        principalSchema: "Lookups",
                        principalTable: "VersionType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ActivityVersion",
                schema: "Tender",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    IsActive = table.Column<bool>(nullable: true),
                    ActivityId = table.Column<int>(nullable: false),
                    ActivityTemplateId = table.Column<int>(nullable: false),
                    VersionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityVersion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActivityVersion_Activity_ActivityId",
                        column: x => x.ActivityId,
                        principalSchema: "LookUps",
                        principalTable: "Activity",
                        principalColumn: "ActivityId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ActivityVersion_Activitytemplate_ActivityTemplateId",
                        column: x => x.ActivityTemplateId,
                        principalSchema: "LookUps",
                        principalTable: "Activitytemplate",
                        principalColumn: "ActivitytemplatId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ActivityVersion_Versions_VersionId",
                        column: x => x.VersionId,
                        principalSchema: "Versions",
                        principalTable: "Versions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TenderVersion",
                schema: "Tender",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    IsActive = table.Column<bool>(nullable: true),
                    TenderId = table.Column<int>(nullable: false),
                    VersionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenderVersion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TenderVersion_Tender_TenderId",
                        column: x => x.TenderId,
                        principalSchema: "Tender",
                        principalTable: "Tender",
                        principalColumn: "TenderId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TenderVersion_Versions_VersionId",
                        column: x => x.VersionId,
                        principalSchema: "Versions",
                        principalTable: "Versions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActivityVersion_ActivityId",
                schema: "Tender",
                table: "ActivityVersion",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityVersion_ActivityTemplateId",
                schema: "Tender",
                table: "ActivityVersion",
                column: "ActivityTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityVersion_VersionId",
                schema: "Tender",
                table: "ActivityVersion",
                column: "VersionId");

            migrationBuilder.CreateIndex(
                name: "IX_TenderAddress_TenderId",
                schema: "Tender",
                table: "TenderAddress",
                column: "TenderId");

            migrationBuilder.CreateIndex(
                name: "IX_TenderDates_TenderId",
                schema: "Tender",
                table: "TenderDates",
                column: "TenderId");

            migrationBuilder.CreateIndex(
                name: "IX_TenderLocalContent_TenderId",
                schema: "Tender",
                table: "TenderLocalContent",
                column: "TenderId");

            migrationBuilder.CreateIndex(
                name: "IX_TenderVersion_TenderId",
                schema: "Tender",
                table: "TenderVersion",
                column: "TenderId");

            migrationBuilder.CreateIndex(
                name: "IX_TenderVersion_VersionId",
                schema: "Tender",
                table: "TenderVersion",
                column: "VersionId");

            migrationBuilder.CreateIndex(
                name: "IX_Versions_VersionTypeId",
                schema: "Versions",
                table: "Versions",
                column: "VersionTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivityVersion",
                schema: "Tender");

            migrationBuilder.DropTable(
                name: "TenderAddress",
                schema: "Tender");

            migrationBuilder.DropTable(
                name: "TenderDates",
                schema: "Tender");

            migrationBuilder.DropTable(
                name: "TenderLocalContent",
                schema: "Tender");

            migrationBuilder.DropTable(
                name: "TenderVersion",
                schema: "Tender");

            migrationBuilder.DropTable(
                name: "Versions",
                schema: "Versions");

            migrationBuilder.DropTable(
                name: "VersionType",
                schema: "Lookups");

            migrationBuilder.DropColumn(
                name: "FinalGuaranteePercentage",
                schema: "Tender",
                table: "Tender");

            migrationBuilder.DropColumn(
                name: "RelatedActivityId",
                schema: "LookUps",
                table: "Activity");

            migrationBuilder.AlterTable(
                name: "TenderMentainanceRunnigWork",
                schema: "Tender",
                oldComment: "Describe the relation between Tender and Maintenance Runnig Work");

            migrationBuilder.AlterTable(
                name: "TenderCountry",
                schema: "Tender",
                oldComment: "Describe the relation between Tender and Country");

            migrationBuilder.AlterTable(
                name: "TenderChangeRequest",
                schema: "Tender",
                oldComment: "Describe the Change request on Tender");

            migrationBuilder.AlterTable(
                name: "TenderArea",
                schema: "Tender",
                oldComment: "Describe the relation between Tender and Areas");

            migrationBuilder.AlterTable(
                name: "TenderAgreementAgency",
                schema: "Tender",
                oldComment: "Describe the relation between Tender and Agencies");

            migrationBuilder.AlterTable(
                name: "TenderActivity",
                schema: "Tender",
                oldComment: "Describe the Tender activities");

            migrationBuilder.AlterTable(
                name: "Attachment",
                schema: "Tender",
                oldComment: "Describe the Atachment for tender");

            migrationBuilder.AlterTable(
                name: "SupplierTenderQuantityTableItemJson",
                schema: "Offer",
                oldComment: "Contains Quantity table Items ");

            migrationBuilder.AlterTable(
                name: "UserNotificationSetting",
                schema: "Notification",
                oldComment: "Contains the user settings for every notification");

            migrationBuilder.AlterTable(
                name: "Notification",
                schema: "Notification",
                oldComment: "Contains the Data of Notifications");

            migrationBuilder.AlterTable(
                name: "MobileAlert",
                schema: "Mobile",
                oldComment: "Not Used");

            migrationBuilder.AlterTable(
                name: "DeviceTokenNotification",
                schema: "Mobile",
                oldComment: "Not Used");

            migrationBuilder.AlterTable(
                name: "DeviceToken",
                schema: "Mobile",
                oldComment: "Not Used");

            migrationBuilder.AlterTable(
                name: "SupplierSecondNegotiationStatus",
                schema: "LookUps",
                oldComment: "Define the Status of Second Stage Negotiation like [Approved , Supplier Agree , ETC..]");

            migrationBuilder.AlterTable(
                name: "NotificationOperationCode",
                schema: "LookUps",
                oldComment: "Describe the Notifications Templates and involved roles");

            migrationBuilder.AlterTable(
                name: "NotificationCategory",
                schema: "LookUps",
                oldComment: "Define the diffrent categories of notification ");

            migrationBuilder.AlterTable(
                name: "OfferPresentationWay",
                schema: "Lookups",
                comment: "Define the Offer Presentation Way lookup",
                oldComment: "Define the way that supplier can apply offer if it was on one file or two files");

            migrationBuilder.AlterTable(
                name: "City",
                schema: "Lookups",
                oldComment: "look up for all cities");

            migrationBuilder.AlterTable(
                name: "UnRegisteredSuppliersInvitation",
                schema: "Invitation",
                oldComment: "Contains the Invitations for uregistered suppliers");

            migrationBuilder.AlterTable(
                name: "_UserAudit",
                schema: "dbo",
                oldComment: "Descripes the Auditing Data to track users Actions");

            migrationBuilder.AlterTable(
                name: "NegotiationSupplierQuantityTable",
                schema: "CommunicationRequest",
                oldComment: "Define the Quantity Table for the Negotiation");

            migrationBuilder.AlterTable(
                name: "BranchUser",
                schema: "Branch",
                oldComment: "Contain Data for Users for each Branch");

            migrationBuilder.AlterTable(
                name: "BranchCommittee",
                schema: "Branch",
                oldComment: "Contain the relation between Branches and Committees");

            migrationBuilder.AlterTable(
                name: "BranchAddresse",
                schema: "Branch",
                oldComment: "Contain the Branch address data");

            migrationBuilder.AlterTable(
                name: "Branch",
                schema: "Branch",
                oldComment: "Contain the Brnach main data");

            migrationBuilder.AlterColumn<string>(
                name: "VerificationTypeName",
                schema: "Verification",
                table: "VerificationType",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1024,
                oldNullable: true,
                oldComment: "define name of verification type");

            migrationBuilder.AlterColumn<int>(
                name: "VerificationTypeId",
                schema: "Verification",
                table: "VerificationType",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "define identity of verification type")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "VerificationTypeId",
                schema: "Verification",
                table: "VerificationCode",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "it's a foreign  key that described type of verification code");

            migrationBuilder.AlterColumn<string>(
                name: "VerificationCodeNo",
                schema: "Verification",
                table: "VerificationCode",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1024,
                oldNullable: true,
                oldComment: "describe verfication code number");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                schema: "Verification",
                table: "VerificationCode",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "refer to who received verfication code");

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                schema: "Verification",
                table: "VerificationCode",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 256,
                oldNullable: true,
                oldComment: "Determine who updated  verification code");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                schema: "Verification",
                table: "VerificationCode",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldComment: "Define updated date of  verification code");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                schema: "Verification",
                table: "VerificationCode",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldNullable: true,
                oldComment: "Define  verification code  is active or not");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpiredDate",
                schema: "Verification",
                table: "VerificationCode",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldComment: "define expiry date of verfication code");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                schema: "Verification",
                table: "VerificationCode",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 256,
                oldNullable: true,
                oldComment: "Determine who cretead verification code");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "Verification",
                table: "VerificationCode",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldComment: "Define created date of  verification code");

            migrationBuilder.AlterColumn<int>(
                name: "VerificationCodeId",
                schema: "Verification",
                table: "VerificationCode",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "define identity of verification code")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "VacationName",
                schema: "Tender",
                table: "VacationsDate",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1024,
                oldNullable: true,
                oldComment: "define name of vacation ");

            migrationBuilder.AlterColumn<DateTime>(
                name: "VacationDate",
                schema: "Tender",
                table: "VacationsDate",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldComment: "define date of vacation ");

            migrationBuilder.AlterColumn<int>(
                name: "VacationId",
                schema: "Tender",
                table: "VacationsDate",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "define identity of vacation")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "TenderId",
                schema: "Tender",
                table: "TenderMentainanceRunnigWork",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define the related Tender");

            migrationBuilder.AlterColumn<int>(
                name: "MaintenanceRunningWorkId",
                schema: "Tender",
                table: "TenderMentainanceRunnigWork",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define the related Maintenance Runnig Work");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                schema: "Tender",
                table: "TenderMentainanceRunnigWork",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define the unique udentifier for Tender Maintenance Runnig Work")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "TenderId",
                schema: "Tender",
                table: "TenderCountry",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define the related Tender");

            migrationBuilder.AlterColumn<int>(
                name: "CountryId",
                schema: "Tender",
                table: "TenderCountry",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define the related Country");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                schema: "Tender",
                table: "TenderCountry",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define the unique udentifier for Tender Country")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "TenderId",
                schema: "Tender",
                table: "TenderChangeRequest",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define the Related Tender");

            migrationBuilder.AlterColumn<string>(
                name: "RequestedByRoleName",
                schema: "Tender",
                table: "TenderChangeRequest",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true,
                oldComment: "Define the user role who created the Request");

            migrationBuilder.AlterColumn<string>(
                name: "RejectionReason",
                schema: "Tender",
                table: "TenderChangeRequest",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true,
                oldComment: "Define the Rejection Reason if the request was rejected");

            migrationBuilder.AlterColumn<bool>(
                name: "HasAlternativeOffer",
                schema: "Tender",
                table: "TenderChangeRequest",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldNullable: true,
                oldComment: "Define if the Tender allow alternative offer, used if the request was Change in Quantity Table");

            migrationBuilder.AlterColumn<int>(
                name: "ChangeRequestTypeId",
                schema: "Tender",
                table: "TenderChangeRequest",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define the type of change if it was [Change Dates, Change in files ,  Cancelation Request ....ETC]");

            migrationBuilder.AlterColumn<int>(
                name: "ChangeRequestStatusId",
                schema: "Tender",
                table: "TenderChangeRequest",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define the status of the Request");

            migrationBuilder.AlterColumn<int>(
                name: "CancelationReasonId",
                schema: "Tender",
                table: "TenderChangeRequest",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true,
                oldComment: "Define the reason why the  Cancelation Request created");

            migrationBuilder.AlterColumn<string>(
                name: "CancelationReasonDescription",
                schema: "Tender",
                table: "TenderChangeRequest",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1000,
                oldNullable: true,
                oldComment: "Define the Cancelation Request Reason Description");

            migrationBuilder.AlterColumn<int>(
                name: "TenderChangeRequestId",
                schema: "Tender",
                table: "TenderChangeRequest",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define the unique udentifier for Tender Change Request")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "TenderId",
                schema: "Tender",
                table: "TenderArea",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define the related Tender");

            migrationBuilder.AlterColumn<int>(
                name: "AreaId",
                schema: "Tender",
                table: "TenderArea",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define the related Area");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                schema: "Tender",
                table: "TenderArea",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define the unique udentifier for Tender area")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "TenderId",
                schema: "Tender",
                table: "TenderAgreementAgency",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define the related Tender");

            migrationBuilder.AlterColumn<string>(
                name: "AgencyCode",
                schema: "Tender",
                table: "TenderAgreementAgency",
                type: "nvarchar(20)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true,
                oldComment: "Define the related Agency");

            migrationBuilder.AlterColumn<int>(
                name: "TenderAgreementAgencyId",
                schema: "Tender",
                table: "TenderAgreementAgency",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define the unique udentifier for Tender Agreement Agency")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "TenderId",
                schema: "Tender",
                table: "TenderActivity",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define the related Tender");

            migrationBuilder.AlterColumn<int>(
                name: "ActivityId",
                schema: "Tender",
                table: "TenderActivity",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define the related Activity");

            migrationBuilder.AlterColumn<int>(
                name: "TenderActivityId",
                schema: "Tender",
                table: "TenderActivity",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define the unique udentifier for Tender Activity")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "VROCommitteeId",
                schema: "Tender",
                table: "Tender",
                type: "int",
                nullable: true,
                comment: "The committe of VRO for opening, checking and awarding tender",
                oldClrType: typeof(int),
                oldNullable: true,
                oldComment: "The committe of VRO for opening, checking and awarding Tender");

            migrationBuilder.AlterColumn<int>(
                name: "TenderUnitStatusId",
                schema: "Tender",
                table: "Tender",
                type: "int",
                nullable: true,
                comment: "The status of tender at unit review",
                oldClrType: typeof(int),
                oldNullable: true,
                oldComment: "The status of Tender at unit review");

            migrationBuilder.AlterColumn<string>(
                name: "TenderTypeOtherSelectionReason",
                schema: "Tender",
                table: "Tender",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: true,
                comment: "Define the reason of selecting direct purchase or limited tender that not exist in reasons list",
                oldClrType: typeof(string),
                oldMaxLength: 1024,
                oldNullable: true,
                oldComment: "Define the reason of selecting direct purchase or limited Tender that not exist in reasons list");

            migrationBuilder.AlterColumn<int>(
                name: "TenderTypeId",
                schema: "Tender",
                table: "Tender",
                type: "int",
                nullable: false,
                comment: "Type of tender",
                oldClrType: typeof(int),
                oldComment: "Type of Tender");

            migrationBuilder.AlterColumn<int>(
                name: "TenderStatusId",
                schema: "Tender",
                table: "Tender",
                type: "int",
                nullable: false,
                comment: "Status of tender",
                oldClrType: typeof(int),
                oldComment: "Status of Tender");

            migrationBuilder.AlterColumn<int>(
                name: "TenderFirstStageId",
                schema: "Tender",
                table: "Tender",
                type: "int",
                nullable: true,
                comment: "The identity of tender of type first stage",
                oldClrType: typeof(int),
                oldNullable: true,
                oldComment: "The identity of Tender of type first stage");

            migrationBuilder.AlterColumn<int>(
                name: "TenderConditionsTemplateId",
                schema: "Tender",
                table: "Tender",
                type: "int",
                nullable: true,
                comment: "The Activity template of tender",
                oldClrType: typeof(int),
                oldNullable: true,
                oldComment: "The Activity template of Tender");

            migrationBuilder.AlterColumn<bool>(
                name: "TenderAwardingType",
                schema: "Tender",
                table: "Tender",
                type: "bit",
                nullable: true,
                comment: "Define the awarding type of tender partial or total awarding",
                oldClrType: typeof(bool),
                oldNullable: true,
                oldComment: "Define the awarding type of Tender partial or total awarding");

            migrationBuilder.AlterColumn<bool>(
                name: "SupplierNeedSample",
                schema: "Tender",
                table: "Tender",
                type: "bit",
                nullable: true,
                comment: "Boolean detrmine tf the supplier need samples of the tender",
                oldClrType: typeof(bool),
                oldNullable: true,
                oldComment: "Boolean detrmine tf the supplier need samples of the Tender");

            migrationBuilder.AlterColumn<DateTime>(
                name: "SubmitionDate",
                schema: "Tender",
                table: "Tender",
                type: "datetime2",
                nullable: true,
                comment: "Define the publish/approval date of tender",
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldComment: "Define the publish/approval date of Tender");

            migrationBuilder.AlterColumn<int>(
                name: "SpendingCategoryId",
                schema: "Tender",
                table: "Tender",
                type: "int",
                nullable: true,
                comment: "Define the spending Category of the tender",
                oldClrType: typeof(int),
                oldNullable: true,
                oldComment: "Define the spending Category of the Tender");

            migrationBuilder.AlterColumn<string>(
                name: "SamplesDeliveryAddress",
                schema: "Tender",
                table: "Tender",
                type: "nvarchar(2048)",
                maxLength: 2048,
                nullable: true,
                comment: "Define the address of samples deleviry of the tender",
                oldClrType: typeof(string),
                oldMaxLength: 2048,
                oldNullable: true,
                oldComment: "Define the address of samples deleviry of the Tender");

            migrationBuilder.AlterColumn<int>(
                name: "ReasonForPurchaseTenderTypeId",
                schema: "Tender",
                table: "Tender",
                type: "int",
                nullable: true,
                comment: "The reason of selecting direct purchase tender",
                oldClrType: typeof(int),
                oldNullable: true,
                oldComment: "The reason of selecting direct purchase Tender");

            migrationBuilder.AlterColumn<int>(
                name: "ReasonForLimitedTenderTypeId",
                schema: "Tender",
                table: "Tender",
                type: "int",
                nullable: true,
                comment: "The reason of selecting limited tender",
                oldClrType: typeof(int),
                oldNullable: true,
                oldComment: "The reason of selecting limited Tender");

            migrationBuilder.AlterColumn<int>(
                name: "PreQualificationId",
                schema: "Tender",
                table: "Tender",
                type: "int",
                nullable: true,
                comment: "The identity that identify pre qualification on the tender",
                oldClrType: typeof(int),
                oldNullable: true,
                oldComment: "The identity that identify pre qualification on the Tender");

            migrationBuilder.AlterColumn<int>(
                name: "PreAnnouncementId",
                schema: "Tender",
                table: "Tender",
                type: "int",
                nullable: true,
                comment: "Tha pre-announcement related to tender",
                oldClrType: typeof(int),
                oldNullable: true,
                oldComment: "Tha pre-announcement related to Tender");

            migrationBuilder.AlterColumn<int>(
                name: "PostQualificationTenderId",
                schema: "Tender",
                table: "Tender",
                type: "int",
                nullable: true,
                comment: "The identity that identify post qualification on the tender",
                oldClrType: typeof(int),
                oldNullable: true,
                oldComment: "The identity that identify post qualification on the Tender");

            migrationBuilder.AlterColumn<int>(
                name: "OffersOpeningAddressId",
                schema: "Tender",
                table: "Tender",
                type: "int",
                nullable: true,
                comment: "The Identity that identify address of open tender offers",
                oldClrType: typeof(int),
                oldNullable: true,
                oldComment: "The Identity that identify address of open Tender offers");

            migrationBuilder.AlterColumn<int>(
                name: "OffersCheckingCommitteeId",
                schema: "Tender",
                table: "Tender",
                type: "int",
                nullable: true,
                comment: "The Checking committee for checking and awarding tender",
                oldClrType: typeof(int),
                oldNullable: true,
                oldComment: "The Checking committee for checking and awarding Tender");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastEnqueriesDate",
                schema: "Tender",
                table: "Tender",
                type: "datetime2",
                nullable: true,
                comment: "Define the last date that supplier can enquiry or ask questions for tender",
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldComment: "Define the last date that supplier can enquiry or ask questions for Tender");

            migrationBuilder.AlterColumn<bool>(
                name: "IsSendToEmarketPlace",
                schema: "Tender",
                table: "Tender",
                type: "bit",
                nullable: true,
                comment: "Flag determine if the awarded tender is sent to e-market place or not",
                oldClrType: typeof(bool),
                oldNullable: true,
                oldComment: "Flag determine if the awarded Tender is sent to e-market place or not");

            migrationBuilder.AlterColumn<bool>(
                name: "IsNotificationSentForStoppingPeriod",
                schema: "Tender",
                table: "Tender",
                type: "bit",
                nullable: true,
                comment: "Flag determine if a notification sent after finishing the stoping period of tender",
                oldClrType: typeof(bool),
                oldNullable: true,
                oldComment: "Flag determine if a notification sent after finishing the stoping period of Tender");

            migrationBuilder.AlterColumn<bool>(
                name: "IsLowBudgetDirectPurchase",
                schema: "Tender",
                table: "Tender",
                type: "bit",
                nullable: true,
                comment: "determine if the tender is low budget or not if the estimatinn value less than 30000 sar",
                oldClrType: typeof(bool),
                oldNullable: true,
                oldComment: "determine if the Tender is low budget or not if the estimatinn value less than 30000 sar");

            migrationBuilder.AlterColumn<int>(
                name: "InvitationTypeId",
                schema: "Tender",
                table: "Tender",
                type: "int",
                nullable: true,
                comment: "Type of invitation on tender public or private",
                oldClrType: typeof(int),
                oldNullable: true,
                oldComment: "Type of invitation on Tender public or private");

            migrationBuilder.AlterColumn<bool>(
                name: "InsideKSA",
                schema: "Tender",
                table: "Tender",
                type: "bit",
                nullable: true,
                comment: "Define the location of tender",
                oldClrType: typeof(bool),
                oldNullable: true,
                oldComment: "Define the location of Tender");

            migrationBuilder.AlterColumn<bool>(
                name: "HasGuarantee",
                schema: "Tender",
                table: "Tender",
                type: "bit",
                nullable: true,
                comment: "Boolean determine if the tender requires a final guarantee or not",
                oldClrType: typeof(bool),
                oldNullable: true,
                oldComment: "Boolean determine if the Tender requires a final guarantee or not");

            migrationBuilder.AlterColumn<decimal>(
                name: "EstimatedValue",
                schema: "Tender",
                table: "Tender",
                type: "decimal(18,2)",
                nullable: true,
                comment: "Define the estimation value of tender",
                oldClrType: typeof(decimal),
                oldNullable: true,
                oldComment: "Define the estimation value of Tender");

            migrationBuilder.AlterColumn<int>(
                name: "DirectPurchaseCommitteeId",
                schema: "Tender",
                table: "Tender",
                type: "int",
                nullable: true,
                comment: "The direct purchase committee for checking and awarding tender from type direct purchase",
                oldClrType: typeof(int),
                oldNullable: true,
                oldComment: "The direct purchase committee for checking and awarding Tender from type direct purchase");

            migrationBuilder.AlterColumn<string>(
                name: "Details",
                schema: "Tender",
                table: "Tender",
                type: "nvarchar(max)",
                maxLength: 5000,
                nullable: true,
                comment: "Define more information about tender",
                oldClrType: typeof(string),
                oldMaxLength: 5000,
                oldNullable: true,
                oldComment: "Define more information about Tender");

            migrationBuilder.AlterColumn<int>(
                name: "CreatedByTypeId",
                schema: "Tender",
                table: "Tender",
                type: "int",
                nullable: true,
                comment: "determine the tender created by vro or agency or agency related by vro",
                oldClrType: typeof(int),
                oldNullable: true,
                oldComment: "determine the Tender created by vro or agency or agency related by vro");

            migrationBuilder.AlterColumn<int>(
                name: "BranchId",
                schema: "Tender",
                table: "Tender",
                type: "int",
                nullable: false,
                comment: "The branch that create a tender",
                oldClrType: typeof(int),
                oldComment: "The branch that create a Tender");

            migrationBuilder.AlterColumn<int>(
                name: "AwardingStoppingPeriod",
                schema: "Tender",
                table: "Tender",
                type: "int",
                nullable: true,
                comment: "Define the period that suppliers can add plaint on tender after awarding between 5 and 10 days",
                oldClrType: typeof(int),
                oldNullable: true,
                oldComment: "Define the period that suppliers can add plaint on Tender after awarding between 5 and 10 days");

            migrationBuilder.AlterColumn<int>(
                name: "AnnouncementTemplateId",
                schema: "Tender",
                table: "Tender",
                type: "int",
                nullable: true,
                comment: "Tha announcement list of suppliers related to tender",
                oldClrType: typeof(int),
                oldNullable: true,
                oldComment: "Tha announcement list of suppliers related to Tender");

            migrationBuilder.AlterColumn<string>(
                name: "AgencyCode",
                schema: "Tender",
                table: "Tender",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "The Agency code of agency that create a tender",
                oldClrType: typeof(string),
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "The Agency code of agency that create a Tender");

            migrationBuilder.AlterColumn<string>(
                name: "ActivityDescription",
                schema: "Tender",
                table: "Tender",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: true,
                comment: "Define the description of tender activiites",
                oldClrType: typeof(string),
                oldMaxLength: 2000,
                oldNullable: true,
                oldComment: "Define the description of Tender activiites");

            migrationBuilder.AlterColumn<int>(
                name: "TenderId",
                schema: "Tender",
                table: "ConditionsBooklet",
                type: "int",
                nullable: false,
                comment: "it's a forigne key that described tender related to condtion booklet",
                oldClrType: typeof(int),
                oldComment: "it's a forigne key that described Tender related to condtion booklet");

            migrationBuilder.AlterColumn<int>(
                name: "TenderId",
                schema: "Tender",
                table: "Attachment",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define the related Tender");

            migrationBuilder.AlterColumn<int>(
                name: "ReviewStatusId",
                schema: "Tender",
                table: "Attachment",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true,
                oldComment: "Define status of review ");

            migrationBuilder.AlterColumn<string>(
                name: "RejectionReason",
                schema: "Tender",
                table: "Attachment",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1024,
                oldNullable: true,
                oldComment: "Rejection reason if there were change request on the attachment");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "Tender",
                table: "Attachment",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 200,
                oldComment: "The name of the file attached");

            migrationBuilder.AlterColumn<string>(
                name: "FileNetReferenceId",
                schema: "Tender",
                table: "Attachment",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1024,
                oldNullable: true,
                oldComment: "Define the file net reference Id ");

            migrationBuilder.AlterColumn<int>(
                name: "ChangeStatusId",
                schema: "Tender",
                table: "Attachment",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true,
                oldComment: "Define the the status of change request");

            migrationBuilder.AlterColumn<int>(
                name: "AttachmentTypeId",
                schema: "Tender",
                table: "Attachment",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "The category of this attachment [Condition bocklet  ETC.... ]");

            migrationBuilder.AlterColumn<int>(
                name: "TenderAttachmentId",
                schema: "Tender",
                table: "Attachment",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define the unique udentifier for Tender Attachment")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "SupplierTenderQuantityTableItems",
                schema: "Offer",
                table: "SupplierTenderQuantityTableItemJson",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true,
                oldComment: "Define the Quantity Table Items");

            migrationBuilder.AlterColumn<long>(
                name: "SupplierTenderQuantityTableId",
                schema: "Offer",
                table: "SupplierTenderQuantityTableItemJson",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldComment: "Define the related Quatity Table");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                schema: "Offer",
                table: "SupplierTenderQuantityTableItemJson",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldComment: "Define Unique identifer Of Supplier  Quantity Table items")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<decimal>(
                name: "PartialOfferAwardingValue",
                schema: "Offer",
                table: "Offer",
                type: "decimal(18,2)",
                nullable: true,
                comment: "Define the partial value of awarding if the tender partialy awarded",
                oldClrType: typeof(decimal),
                oldNullable: true,
                oldComment: "Define the partial value of awarding if the Tender partialy awarded");

            migrationBuilder.AlterColumn<int>(
                name: "UserRoleId",
                schema: "Notification",
                table: "UserNotificationSetting",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define the role of the user who will recieve the Notification");

            migrationBuilder.AlterColumn<int>(
                name: "UserProfileId",
                schema: "Notification",
                table: "UserNotificationSetting",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true,
                oldComment: "Define the related user if he was Governate user ");

            migrationBuilder.AlterColumn<bool>(
                name: "Sms",
                schema: "Notification",
                table: "UserNotificationSetting",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldComment: "Define if the user needs to recieve SMS or Not");

            migrationBuilder.AlterColumn<string>(
                name: "OperationCode",
                schema: "Notification",
                table: "UserNotificationSetting",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true,
                oldComment: "Define the Notification Code");

            migrationBuilder.AlterColumn<int>(
                name: "NotificationCodeId",
                schema: "Notification",
                table: "UserNotificationSetting",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define the Notification Code ID");

            migrationBuilder.AlterColumn<bool>(
                name: "IsArabic",
                schema: "Notification",
                table: "UserNotificationSetting",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldComment: "Define if the user configured Notification to be in arabic Language ");

            migrationBuilder.AlterColumn<bool>(
                name: "Email",
                schema: "Notification",
                table: "UserNotificationSetting",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldComment: "Define if the user needs to recieve EMAIL or Not");

            migrationBuilder.AlterColumn<string>(
                name: "Cr",
                schema: "Notification",
                table: "UserNotificationSetting",
                type: "nvarchar(20)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true,
                oldComment: "Refere to the Supplier Commercial Registeration Number who will recieve the Notification");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                schema: "Notification",
                table: "UserNotificationSetting",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define Unique identifer Of  User Notification Setting")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<DateTime>(
                name: "sendAt",
                schema: "Notification",
                table: "Notification",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldComment: "the date of sending the notification");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                schema: "Notification",
                table: "Notification",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true,
                oldComment: "Define the user id who recieve the notification");

            migrationBuilder.AlterColumn<int>(
                name: "NotificationSettingId",
                schema: "Notification",
                table: "Notification",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true,
                oldComment: "the related setting of the setting");

            migrationBuilder.AlterColumn<int>(
                name: "NotifacationStatusId",
                schema: "Notification",
                table: "Notification",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define the status of Notification if sent or not or Faild");

            migrationBuilder.AlterColumn<string>(
                name: "Link",
                schema: "Notification",
                table: "Notification",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true,
                oldComment: "link for reciever to access direct the related page of monafasat");

            migrationBuilder.AlterColumn<string>(
                name: "Key",
                schema: "Notification",
                table: "Notification",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true,
                oldComment: "compination of string 'Tender' and the Id of Tender ");

            migrationBuilder.AlterColumn<string>(
                name: "CR",
                schema: "Notification",
                table: "Notification",
                type: "nvarchar(20)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true,
                oldComment: "Define the Supplier id who recieve the notification");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                schema: "Notification",
                table: "Notification",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define Unique identifer Of Notification")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "MessageStatusId",
                schema: "Mobile",
                table: "MobileAlert",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Not Used");

            migrationBuilder.AlterColumn<int>(
                name: "MessageId",
                schema: "Mobile",
                table: "MobileAlert",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true,
                oldComment: "Not Used");

            migrationBuilder.AlterColumn<string>(
                name: "Message",
                schema: "Mobile",
                table: "MobileAlert",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true,
                oldComment: "Not Used");

            migrationBuilder.AlterColumn<string>(
                name: "GroupCode",
                schema: "Mobile",
                table: "MobileAlert",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true,
                oldComment: "Not Used");

            migrationBuilder.AlterColumn<int>(
                name: "DeviceId",
                schema: "Mobile",
                table: "MobileAlert",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Not Used");

            migrationBuilder.AlterColumn<int>(
                name: "MobileAlertId",
                schema: "Mobile",
                table: "MobileAlert",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Not Used")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<bool>(
                name: "Status",
                schema: "Mobile",
                table: "DeviceTokenNotification",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldComment: "Not Used");

            migrationBuilder.AlterColumn<int>(
                name: "DeviceId",
                schema: "Mobile",
                table: "DeviceTokenNotification",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Not Used");

            migrationBuilder.AlterColumn<int>(
                name: "ActivityId",
                schema: "Mobile",
                table: "DeviceTokenNotification",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Not Used");

            migrationBuilder.AlterColumn<int>(
                name: "DeviceTokenNotificationId",
                schema: "Mobile",
                table: "DeviceTokenNotification",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Not Used")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "UserProfileId",
                schema: "Mobile",
                table: "DeviceToken",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true,
                oldComment: "Not Used");

            migrationBuilder.AlterColumn<string>(
                name: "UserDeviceId",
                schema: "Mobile",
                table: "DeviceToken",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 60,
                oldNullable: true,
                oldComment: "Not Used");

            migrationBuilder.AlterColumn<int>(
                name: "TimeStamp",
                schema: "Mobile",
                table: "DeviceToken",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Not Used");

            migrationBuilder.AlterColumn<string>(
                name: "SourceIP",
                schema: "Mobile",
                table: "DeviceToken",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "Not Used");

            migrationBuilder.AlterColumn<string>(
                name: "Model",
                schema: "Mobile",
                table: "DeviceToken",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 30,
                oldNullable: true,
                oldComment: "Not Used");

            migrationBuilder.AlterColumn<string>(
                name: "DeviceVersion",
                schema: "Mobile",
                table: "DeviceToken",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 15,
                oldNullable: true,
                oldComment: "Not Used");

            migrationBuilder.AlterColumn<string>(
                name: "DeviceTokenValue",
                schema: "Mobile",
                table: "DeviceToken",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 500,
                oldNullable: true,
                oldComment: "Not Used");

            migrationBuilder.AlterColumn<string>(
                name: "DeviceName",
                schema: "Mobile",
                table: "DeviceToken",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true,
                oldComment: "Not Used");

            migrationBuilder.AlterColumn<int>(
                name: "DeviceId",
                schema: "Mobile",
                table: "DeviceToken",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Not Used")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "NameEn",
                schema: "LookUps",
                table: "VendorCertificate",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1024,
                oldNullable: true,
                oldComment: "define english name of vendor certificate");

            migrationBuilder.AlterColumn<string>(
                name: "NameAr",
                schema: "LookUps",
                table: "VendorCertificate",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1024,
                oldNullable: true,
                oldComment: "define arabic name of vendor certificate");

            migrationBuilder.AlterColumn<int>(
                name: "VendorCertificateId",
                schema: "LookUps",
                table: "VendorCertificate",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "define identity of vendor certificate");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "LookUps",
                table: "UserRole",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1024,
                oldNullable: true,
                oldComment: "define name of role");

            migrationBuilder.AlterColumn<bool>(
                name: "IsCombinedRole",
                schema: "LookUps",
                table: "UserRole",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldDefaultValue: false,
                oldComment: "describe notification for low budget module");

            migrationBuilder.AlterColumn<string>(
                name: "DisplayNameEn",
                schema: "LookUps",
                table: "UserRole",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1024,
                oldNullable: true,
                oldComment: "define english name of role");

            migrationBuilder.AlterColumn<string>(
                name: "DisplayNameAr",
                schema: "LookUps",
                table: "UserRole",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1024,
                oldNullable: true,
                oldComment: "define arabic name of role");

            migrationBuilder.AlterColumn<int>(
                name: "UserRoleId",
                schema: "LookUps",
                table: "UserRole",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "define identity of user role");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "LookUps",
                table: "TenderUnitUpdateType",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: true,
                comment: "Define the name of tender unit type",
                oldClrType: typeof(string),
                oldMaxLength: 1024,
                oldNullable: true,
                oldComment: "Define the name of Tender unit type");

            migrationBuilder.AlterColumn<int>(
                name: "TenderUnitUpdateTypeId",
                schema: "LookUps",
                table: "TenderUnitUpdateType",
                type: "int",
                nullable: false,
                comment: "Define identity of tender unit type",
                oldClrType: typeof(int),
                oldComment: "Define identity of Tender unit type");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "LookUps",
                table: "TenderUnitStatus",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                comment: "Define the name of tender unit status",
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true,
                oldComment: "Define the name of Tender unit status");

            migrationBuilder.AlterColumn<int>(
                name: "TenderUnitStatusId",
                schema: "LookUps",
                table: "TenderUnitStatus",
                type: "int",
                nullable: false,
                comment: "Define identity of tender unit status",
                oldClrType: typeof(int),
                oldComment: "Define identity of Tender unit status");

            migrationBuilder.AlterColumn<string>(
                name: "NameEn",
                schema: "LookUps",
                table: "TenderType",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                comment: "Define the english name of tender type",
                oldClrType: typeof(string),
                oldMaxLength: 500,
                oldNullable: true,
                oldComment: "Define the english name of Tender type");

            migrationBuilder.AlterColumn<string>(
                name: "NameAr",
                schema: "LookUps",
                table: "TenderType",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                comment: "Define the arabic name of tender type",
                oldClrType: typeof(string),
                oldMaxLength: 500,
                oldNullable: true,
                oldComment: "Define the arabic name of Tender type");

            migrationBuilder.AlterColumn<decimal>(
                name: "InvitationCost",
                schema: "LookUps",
                table: "TenderType",
                type: "decimal(18,2)",
                nullable: false,
                comment: "Define the Invitation Cost of tender type",
                oldClrType: typeof(decimal),
                oldComment: "Define the Invitation Cost of Tender type");

            migrationBuilder.AlterColumn<decimal>(
                name: "BuyingCost",
                schema: "LookUps",
                table: "TenderType",
                type: "decimal(18,2)",
                nullable: false,
                comment: "Define the cost of buying  of tender type",
                oldClrType: typeof(decimal),
                oldComment: "Define the cost of buying  of Tender type");

            migrationBuilder.AlterColumn<int>(
                name: "TenderTypeId",
                schema: "LookUps",
                table: "TenderType",
                type: "int",
                nullable: false,
                comment: "Define identity of tender type",
                oldClrType: typeof(int),
                oldComment: "Define identity of Tender type");

            migrationBuilder.AlterColumn<string>(
                name: "NameEn",
                schema: "LookUps",
                table: "TenderStatus",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                comment: "Define the english name of tender status",
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true,
                oldComment: "Define the english name of Tender status");

            migrationBuilder.AlterColumn<string>(
                name: "NameAr",
                schema: "LookUps",
                table: "TenderStatus",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                comment: "Define the arabic name of tender status",
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true,
                oldComment: "Define the arabic name of Tender status");

            migrationBuilder.AlterColumn<int>(
                name: "TenderStatusId",
                schema: "LookUps",
                table: "TenderStatus",
                type: "int",
                nullable: false,
                comment: "Define identity of tender status",
                oldClrType: typeof(int),
                oldComment: "Define identity of Tender status");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "LookUps",
                table: "SupplierSecondNegotiationStatus",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true,
                oldComment: "Define the  Name Of Second Stage Negotiation Status");

            migrationBuilder.AlterColumn<int>(
                name: "SupplierNegotiaitionStatusId",
                schema: "LookUps",
                table: "SupplierSecondNegotiationStatus",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define Unique identifer Of  Second Stage Negotiation Status");

            migrationBuilder.AlterColumn<int>(
                name: "UserRoleId",
                schema: "LookUps",
                table: "NotificationOperationCode",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define the related user role  that will recieve the notification");

            migrationBuilder.AlterColumn<string>(
                name: "SmsTemplateEn",
                schema: "LookUps",
                table: "NotificationOperationCode",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 2000,
                oldNullable: true,
                oldComment: "The SMS English Template");

            migrationBuilder.AlterColumn<string>(
                name: "SmsTemplateAr",
                schema: "LookUps",
                table: "NotificationOperationCode",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 2000,
                oldNullable: true,
                oldComment: "The SMS arabic Template");

            migrationBuilder.AlterColumn<string>(
                name: "PanelTemplateEn",
                schema: "LookUps",
                table: "NotificationOperationCode",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1000,
                oldNullable: true,
                oldComment: "The English Panel Subject for noification ");

            migrationBuilder.AlterColumn<string>(
                name: "PanelTemplateAr",
                schema: "LookUps",
                table: "NotificationOperationCode",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1000,
                oldNullable: true,
                oldComment: "The Arabic Panel Subject for noification ");

            migrationBuilder.AlterColumn<string>(
                name: "OperationCode",
                schema: "LookUps",
                table: "NotificationOperationCode",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true,
                oldComment: "its  unique Text the represent the notification template");

            migrationBuilder.AlterColumn<int>(
                name: "NotificationCategoryId",
                schema: "LookUps",
                table: "NotificationOperationCode",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true,
                oldComment: "Define if the Category of notificatopn item like [operations on Tender , negotiation ETC..]");

            migrationBuilder.AlterColumn<string>(
                name: "EnglishName",
                schema: "LookUps",
                table: "NotificationOperationCode",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 2000,
                oldNullable: true,
                oldComment: "Define notification template english Name");

            migrationBuilder.AlterColumn<string>(
                name: "EmailSubjectTemplateEn",
                schema: "LookUps",
                table: "NotificationOperationCode",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 2000,
                oldNullable: true,
                oldComment: "The English EMail Body for noification ");

            migrationBuilder.AlterColumn<string>(
                name: "EmailSubjectTemplateAr",
                schema: "LookUps",
                table: "NotificationOperationCode",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 2000,
                oldNullable: true,
                oldComment: "The Arabic EMail Subject for noification ");

            migrationBuilder.AlterColumn<string>(
                name: "EmailBodyTemplateAr",
                schema: "LookUps",
                table: "NotificationOperationCode",
                type: "nvarchar(max)",
                maxLength: 10000,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 10000,
                oldNullable: true,
                oldComment: "The Arabic EMail Subject for noification ");

            migrationBuilder.AlterColumn<bool>(
                name: "DefaultSMS",
                schema: "LookUps",
                table: "NotificationOperationCode",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldComment: "Define if the reciever role will recieve SMS by default  or not");

            migrationBuilder.AlterColumn<bool>(
                name: "DefaultEmail",
                schema: "LookUps",
                table: "NotificationOperationCode",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldComment: "Define if the reciever role will recieve Email by default  or not");

            migrationBuilder.AlterColumn<bool>(
                name: "CanEditSMS",
                schema: "LookUps",
                table: "NotificationOperationCode",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldComment: "Define if the reciever role can change Default setting for recieving SMS or not");

            migrationBuilder.AlterColumn<bool>(
                name: "CanEditEmail",
                schema: "LookUps",
                table: "NotificationOperationCode",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldComment: "Define if the reciever role can change Default setting for recieving Email or not");

            migrationBuilder.AlterColumn<string>(
                name: "ArabicName",
                schema: "LookUps",
                table: "NotificationOperationCode",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 2000,
                oldNullable: true,
                oldComment: "Define notification template arabic Name");

            migrationBuilder.AlterColumn<int>(
                name: "NotificationOperationCodeId",
                schema: "LookUps",
                table: "NotificationOperationCode",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define Unique identifer Of Notification Operation Code");

            migrationBuilder.AlterColumn<string>(
                name: "EnglishName",
                schema: "LookUps",
                table: "NotificationCategory",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1024,
                oldNullable: true,
                oldComment: "Define the English Name Of Notification Category");

            migrationBuilder.AlterColumn<string>(
                name: "ArabicName",
                schema: "LookUps",
                table: "NotificationCategory",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1024,
                oldNullable: true,
                oldComment: "Define the arabic Name Of Notification Category");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                schema: "LookUps",
                table: "NotificationCategory",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define Unique identifer Of  Notification Category");

            migrationBuilder.AlterColumn<int>(
                name: "NotifacationStatusEntityId",
                schema: "LookUps",
                table: "NotifacationStatusEntity",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define Unique identifer Of Notification Status lookup [مرسل,لم يتم الارسال , فشل فى الارسال , مقروءه , غير مقروءه]");

            migrationBuilder.AlterColumn<string>(
                name: "NameEn",
                schema: "LookUps",
                table: "AgreementType",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 500,
                oldNullable: true,
                oldComment: "Define the arabic name of agreement type");

            migrationBuilder.AlterColumn<string>(
                name: "NameAr",
                schema: "LookUps",
                table: "AgreementType",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 500,
                oldNullable: true,
                oldComment: "Define the arabic name of agreement type");

            migrationBuilder.AlterColumn<int>(
                name: "AgreementTypeId",
                schema: "LookUps",
                table: "AgreementType",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define the identity of agreement type");

            migrationBuilder.AlterColumn<string>(
                name: "NameEn",
                schema: "Lookups",
                table: "YearQuarter",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true,
                oldComment: "define english name of year quarter");

            migrationBuilder.AlterColumn<string>(
                name: "NameAr",
                schema: "Lookups",
                table: "YearQuarter",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true,
                oldComment: "define arabic name of year quarter");

            migrationBuilder.AlterColumn<int>(
                name: "YearQuarterId",
                schema: "Lookups",
                table: "YearQuarter",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "define identity of year quarter");

            migrationBuilder.AlterColumn<string>(
                name: "NameEnglish",
                schema: "Lookups",
                table: "TenderFeesType",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                comment: "Define english name of tender fees type",
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true,
                oldComment: "Define english name of Tender fees type");

            migrationBuilder.AlterColumn<string>(
                name: "NameArabic",
                schema: "Lookups",
                table: "TenderFeesType",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                comment: "Define arabic name of tender fees type",
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true,
                oldComment: "Define arabic name of Tender fees type");

            migrationBuilder.AlterColumn<int>(
                name: "TenderFeesTypeId",
                schema: "Lookups",
                table: "TenderFeesType",
                type: "int",
                nullable: false,
                comment: "Define identity of tender fees type",
                oldClrType: typeof(int),
                oldComment: "Define identity of Tender fees type");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "Lookups",
                table: "OfferPresentationWay",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: true,
                comment: "Define the Name Of Offer Presentation Way lookup",
                oldClrType: typeof(string),
                oldMaxLength: 1024,
                oldNullable: true,
                oldComment: "Define the Name Of Notification Status");

            migrationBuilder.AlterColumn<string>(
                name: "NameEnglish",
                schema: "Lookups",
                table: "City",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true,
                oldComment: "Define English name of City");

            migrationBuilder.AlterColumn<string>(
                name: "NameArabic",
                schema: "Lookups",
                table: "City",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true,
                oldComment: "Define arabic name of City");

            migrationBuilder.AlterColumn<int>(
                name: "CityID",
                schema: "Lookups",
                table: "City",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define a unique identifer of City");

            migrationBuilder.AlterColumn<int>(
                name: "TenderId",
                schema: "Invitation",
                table: "UnRegisteredSuppliersInvitation",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define the related Tender for the invitations");

            migrationBuilder.AlterColumn<string>(
                name: "MobileNo",
                schema: "Invitation",
                table: "UnRegisteredSuppliersInvitation",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true,
                oldComment: "Define the supplier Mobile Number");

            migrationBuilder.AlterColumn<int>(
                name: "InvitationTypeId",
                schema: "Invitation",
                table: "UnRegisteredSuppliersInvitation",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define the invitaion Type if it was by email or mobile ETC...");

            migrationBuilder.AlterColumn<int>(
                name: "InvitationStatusId",
                schema: "Invitation",
                table: "UnRegisteredSuppliersInvitation",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true,
                oldComment: "Define the status of invitation id sent aor not and if accepted by supplier or Not");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                schema: "Invitation",
                table: "UnRegisteredSuppliersInvitation",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1024,
                oldNullable: true,
                oldComment: "Define the supplier email that he will recieve the invitaion on it ");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "Invitation",
                table: "UnRegisteredSuppliersInvitation",
                type: "nvarchar(2056)",
                maxLength: 2056,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 2056,
                oldNullable: true,
                oldComment: "Define the description written with the invitaion  ");

            migrationBuilder.AlterColumn<string>(
                name: "CrNumber",
                schema: "Invitation",
                table: "UnRegisteredSuppliersInvitation",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "Define the supplier Commercial Registeration Number");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                schema: "Invitation",
                table: "UnRegisteredSuppliersInvitation",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define Unique identifer Of UnRegistered Suppliers Invitation")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<DateTime>(
                name: "WithdrawalDate",
                schema: "Invitation",
                table: "Invitation",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldComment: "define withdrawal date of invitation");

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                schema: "Invitation",
                table: "Invitation",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 256,
                oldNullable: true,
                oldComment: "Determine who updated invitation");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                schema: "Invitation",
                table: "Invitation",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldComment: "Define updated date of invitation");

            migrationBuilder.AlterColumn<int>(
                name: "TenderId",
                schema: "Invitation",
                table: "Invitation",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "it's a foreign key described Tender that related to  invitation");

            migrationBuilder.AlterColumn<int>(
                name: "SupplierType",
                schema: "Invitation",
                table: "Invitation",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true,
                oldComment: "define supplier type  of invitation");

            migrationBuilder.AlterColumn<DateTime>(
                name: "SubmissionDate",
                schema: "Invitation",
                table: "Invitation",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldComment: "define submission date of invitation");

            migrationBuilder.AlterColumn<int>(
                name: "StatusId",
                schema: "Invitation",
                table: "Invitation",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "it's a foreign  key that described status of invitation");

            migrationBuilder.AlterColumn<DateTime>(
                name: "SendingDate",
                schema: "Invitation",
                table: "Invitation",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldComment: "define sending date of invitation");

            migrationBuilder.AlterColumn<string>(
                name: "RejectionReason",
                schema: "Invitation",
                table: "Invitation",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true,
                oldComment: "define reject reason of invitation");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                schema: "Invitation",
                table: "Invitation",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldNullable: true,
                oldComment: "Define invitation is active or not");

            migrationBuilder.AlterColumn<bool>(
                name: "InvitedByQualification",
                schema: "Invitation",
                table: "Invitation",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldNullable: true,
                oldComment: "define supplier invited by qualification or not");

            migrationBuilder.AlterColumn<int>(
                name: "InvitationTypeId",
                schema: "Invitation",
                table: "Invitation",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "it's a foreign  key that described type of invitation");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                schema: "Invitation",
                table: "Invitation",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 256,
                oldNullable: true,
                oldComment: "Determine who cretead invitation");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "Invitation",
                table: "Invitation",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldComment: "Define created date of invitation");

            migrationBuilder.AlterColumn<string>(
                name: "CommericalRegisterNo",
                schema: "Invitation",
                table: "Invitation",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "define supplier commerical register number");

            migrationBuilder.AlterColumn<int>(
                name: "InvitationId",
                schema: "Invitation",
                table: "Invitation",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "define identity of invitation")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                schema: "IDM",
                table: "UserProfile",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "define user name of user profile");

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                schema: "IDM",
                table: "UserProfile",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 256,
                oldNullable: true,
                oldComment: "Determine who updated user profile");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                schema: "IDM",
                table: "UserProfile",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldComment: "Define updated date of user profile");

            migrationBuilder.AlterColumn<byte[]>(
                name: "RowVersion",
                schema: "IDM",
                table: "UserProfile",
                type: "varbinary(max)",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldNullable: true,
                oldComment: "define row version of user profile");

            migrationBuilder.AlterColumn<string>(
                name: "Mobile",
                schema: "IDM",
                table: "UserProfile",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "define mobile of user profile");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                schema: "IDM",
                table: "UserProfile",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldNullable: true,
                oldComment: "Define user profile is active or not");

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                schema: "IDM",
                table: "UserProfile",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 200,
                oldNullable: true,
                oldComment: "define full name of user profile");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                schema: "IDM",
                table: "UserProfile",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true,
                oldComment: "define email of user profile");

            migrationBuilder.AlterColumn<string>(
                name: "DefaultUserRole",
                schema: "IDM",
                table: "UserProfile",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 200,
                oldNullable: true,
                oldComment: "define default user role of user profile");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                schema: "IDM",
                table: "UserProfile",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 256,
                oldNullable: true,
                oldComment: "Determine who cretead user profile");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "IDM",
                table: "UserProfile",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldComment: "Define created date of user profile");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                schema: "IDM",
                table: "UserProfile",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "define identity of user profile");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                schema: "dbo",
                table: "_UserAudit",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true,
                oldComment: "Define User Name of the user who take the action ");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                schema: "dbo",
                table: "_UserAudit",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true,
                oldComment: "Define the user id who making the action");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                schema: "dbo",
                table: "_UserAudit",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldComment: "Define time that action was done on ");

            migrationBuilder.AlterColumn<string>(
                name: "ProcessId",
                schema: "dbo",
                table: "_UserAudit",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true,
                oldComment: "Unique Number to the process");

            migrationBuilder.AlterColumn<string>(
                name: "Process",
                schema: "dbo",
                table: "_UserAudit",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true,
                oldComment: "Define the Process status Success or Fail");

            migrationBuilder.AlterColumn<string>(
                name: "IpAddress",
                schema: "dbo",
                table: "_UserAudit",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true,
                oldComment: "Define ip Address of Device that the user using ");

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                schema: "dbo",
                table: "_UserAudit",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true,
                oldComment: "Define User Full Name of the user who take the action");

            migrationBuilder.AlterColumn<string>(
                name: "AuditEvent",
                schema: "dbo",
                table: "_UserAudit",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true,
                oldComment: "Define the type of Action if Edit or Add or Delete");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                schema: "dbo",
                table: "_UserAudit",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define Unique identifer Of  User Action Auditting")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "refNegotiationSecondStage",
                schema: "CommunicationRequest",
                table: "NegotiationSupplierQuantityTable",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Refere to the related Negotioation Request ");

            migrationBuilder.AlterColumn<long>(
                name: "SupplierQuantityTableId",
                schema: "CommunicationRequest",
                table: "NegotiationSupplierQuantityTable",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldComment: "Refer to the Original Supplier Quantity Table that Filled by supplier");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "CommunicationRequest",
                table: "NegotiationSupplierQuantityTable",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldComment: "Define the Name Of Quantity Table ");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                schema: "CommunicationRequest",
                table: "NegotiationSupplierQuantityTable",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldComment: "Define Unique identifer Of Quantity Table ")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "SupplierExtendOfferValidityStatusId",
                schema: "CommunicationRequest",
                table: "ExtendOffersValiditySupplier",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true,
                oldComment: "Define the status of extend offers validity supplier based on supplier action on request");

            migrationBuilder.AlterColumn<string>(
                name: "SupplierCR",
                schema: "CommunicationRequest",
                table: "ExtendOffersValiditySupplier",
                type: "nvarchar(20)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true,
                oldComment: "Define the supplier cr that extend offers validity sent to");

            migrationBuilder.AlterColumn<DateTime>(
                name: "PeriodStartDateTime",
                schema: "CommunicationRequest",
                table: "ExtendOffersValiditySupplier",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldComment: "Define the the start date of extend offers validity period");

            migrationBuilder.AlterColumn<int>(
                name: "OfferId",
                schema: "CommunicationRequest",
                table: "ExtendOffersValiditySupplier",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define the related  supplier offer for extend offers validity");

            migrationBuilder.AlterColumn<bool>(
                name: "IsReported",
                schema: "CommunicationRequest",
                table: "ExtendOffersValiditySupplier",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldComment: "Define if the request is reported or not");

            migrationBuilder.AlterColumn<int>(
                name: "ExtendOffersValidityId",
                schema: "CommunicationRequest",
                table: "ExtendOffersValiditySupplier",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define the parent extend offers validity for extend offers validity supplier request");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                schema: "CommunicationRequest",
                table: "ExtendOffersValiditySupplier",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define a unique identifer of extend offers validity supplier")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "ReplyReceivingDurationTime",
                schema: "CommunicationRequest",
                table: "ExtendOffersValidity",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true,
                oldComment: "Define the time to reply the extend offers validity request");

            migrationBuilder.AlterColumn<int>(
                name: "ReplyReceivingDurationDays",
                schema: "CommunicationRequest",
                table: "ExtendOffersValidity",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define number of days to allow suppliers to reply the extend offers validity request");

            migrationBuilder.AlterColumn<int>(
                name: "OffersDuration",
                schema: "CommunicationRequest",
                table: "ExtendOffersValidity",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define the duration in days to end expire the extend offers validity request");

            migrationBuilder.AlterColumn<DateTime>(
                name: "NewOffersExpiryDate",
                schema: "CommunicationRequest",
                table: "ExtendOffersValidity",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldComment: "Define the expiration date of extend offers validity request");

            migrationBuilder.AlterColumn<string>(
                name: "ExtendOffersReason",
                schema: "CommunicationRequest",
                table: "ExtendOffersValidity",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true,
                oldComment: "Define the reason of extend offers validity request");

            migrationBuilder.AlterColumn<int>(
                name: "AgencyCommunicationRequestId",
                schema: "CommunicationRequest",
                table: "ExtendOffersValidity",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define the parent commmunication request for extend offers validity request");

            migrationBuilder.AlterColumn<int>(
                name: "ExtendOffersValidityId",
                schema: "CommunicationRequest",
                table: "ExtendOffersValidity",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define a unique identifer of extend offers validity request")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "TenderId",
                schema: "CommunicationRequest",
                table: "AgencyCommunicationRequest",
                type: "int",
                nullable: false,
                comment: "Define the related tender of agency communication request",
                oldClrType: typeof(int),
                oldComment: "Define the related Tender of agency communication request");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "CommunicationAgency",
                table: "ExtendOffersValidityAttachment",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 200,
                oldComment: "Define the name of file attached");

            migrationBuilder.AlterColumn<string>(
                name: "FileNetReferenceId",
                schema: "CommunicationAgency",
                table: "ExtendOffersValidityAttachment",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1024,
                oldNullable: true,
                oldComment: "Define The reference number from file Net");

            migrationBuilder.AlterColumn<int>(
                name: "ExtendOffersValiditySupplierId",
                schema: "CommunicationAgency",
                table: "ExtendOffersValidityAttachment",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define the parent extend offers validity for extend offers validity supplier request");

            migrationBuilder.AlterColumn<int>(
                name: "AttachmentTypeId",
                schema: "CommunicationAgency",
                table: "ExtendOffersValidityAttachment",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "the type of attachment file like [supplier attachment or agency attachment]");

            migrationBuilder.AlterColumn<int>(
                name: "AttachmentId",
                schema: "CommunicationAgency",
                table: "ExtendOffersValidityAttachment",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define the parent extend offers validity for extend offers validity supplier request")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "UserRoleId",
                schema: "Branch",
                table: "BranchUser",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define user role inside the branch");

            migrationBuilder.AlterColumn<int>(
                name: "UserProfileId",
                schema: "Branch",
                table: "BranchUser",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "reference the user full profile");

            migrationBuilder.AlterColumn<string>(
                name: "RelatedAgencyCode",
                schema: "Branch",
                table: "BranchUser",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true,
                oldComment: "Define the agency which the branch is related to");

            migrationBuilder.AlterColumn<decimal>(
                name: "EstimatedValueTo",
                schema: "Branch",
                table: "BranchUser",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldNullable: true,
                oldComment: "Define the biggest estimated value");

            migrationBuilder.AlterColumn<decimal>(
                name: "EstimatedValueFrom",
                schema: "Branch",
                table: "BranchUser",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldNullable: true,
                oldComment: "Define the smallest estimated value");

            migrationBuilder.AlterColumn<int>(
                name: "BranchId",
                schema: "Branch",
                table: "BranchUser",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Refere to the Branch");

            migrationBuilder.AlterColumn<int>(
                name: "BranchUserId",
                schema: "Branch",
                table: "BranchUser",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define a unique identifer of Branch User")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "CommitteeId",
                schema: "Branch",
                table: "BranchCommittee",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Refere to the Committie");

            migrationBuilder.AlterColumn<int>(
                name: "BranchId",
                schema: "Branch",
                table: "BranchCommittee",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Refere to the Branch");

            migrationBuilder.AlterColumn<int>(
                name: "BranchCommitteeId",
                schema: "Branch",
                table: "BranchCommittee",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define a unique identifer of Branch Committee")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "ZipCode",
                schema: "Branch",
                table: "BranchAddresse",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1024,
                oldNullable: true,
                oldComment: "Define Zip Code of the address");

            migrationBuilder.AlterColumn<string>(
                name: "PostalCode",
                schema: "Branch",
                table: "BranchAddresse",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1024,
                oldNullable: true,
                oldComment: "Define Postal Code of the address");

            migrationBuilder.AlterColumn<string>(
                name: "Position",
                schema: "Branch",
                table: "BranchAddresse",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1024,
                oldNullable: true,
                oldComment: "Define the Position of the address");

            migrationBuilder.AlterColumn<string>(
                name: "Phone2",
                schema: "Branch",
                table: "BranchAddresse",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true,
                oldComment: "Define second phone number of the address");

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                schema: "Branch",
                table: "BranchAddresse",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true,
                oldComment: "Define phone number of the address");

            migrationBuilder.AlterColumn<string>(
                name: "Fax2",
                schema: "Branch",
                table: "BranchAddresse",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true,
                oldComment: "Define second FAX number of the address");

            migrationBuilder.AlterColumn<string>(
                name: "Fax",
                schema: "Branch",
                table: "BranchAddresse",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true,
                oldComment: "Define FAX number of the address");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "Branch",
                table: "BranchAddresse",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1024,
                oldNullable: true,
                oldComment: "Define Description of the address");

            migrationBuilder.AlterColumn<string>(
                name: "CityCode",
                schema: "Branch",
                table: "BranchAddresse",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1024,
                oldNullable: true,
                oldComment: "Define City Code of the address");

            migrationBuilder.AlterColumn<int>(
                name: "AddressTypeId",
                schema: "Branch",
                table: "BranchAddresse",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define the type of address");

            migrationBuilder.AlterColumn<string>(
                name: "AddressName",
                schema: "Branch",
                table: "BranchAddresse",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1024,
                oldNullable: true,
                oldComment: "Define the branch Address name");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                schema: "Branch",
                table: "BranchAddresse",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1024,
                oldNullable: true,
                oldComment: "Define The detailed loction  of the Branch Address");

            migrationBuilder.AlterColumn<int>(
                name: "BranchAddressId",
                schema: "Branch",
                table: "BranchAddresse",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define a unique identifer of Branch Address")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "BranchName",
                schema: "Branch",
                table: "Branch",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 1024,
                oldComment: "Define the branch name");

            migrationBuilder.AlterColumn<string>(
                name: "AgencyCode",
                schema: "Branch",
                table: "Branch",
                type: "nvarchar(20)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true,
                oldComment: "Define the related Agency");

            migrationBuilder.AlterColumn<int>(
                name: "BranchId",
                schema: "Branch",
                table: "Branch",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define a unique identifer of Branch")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "TenderTypesId",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSupplierTemplate",
                type: "nvarchar(max)",
                nullable: true,
                comment: "Define type of tender",
                oldClrType: typeof(string),
                oldNullable: true,
                oldComment: "Define type of Tender");

        }
    }
}
