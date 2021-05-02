CREATE TABLE [Tender].[Tender] (
    [CreatedAt]                                  DATETIME2 (7)   NOT NULL,
    [CreatedBy]                                  NVARCHAR (256)  NULL,
    [UpdatedAt]                                  DATETIME2 (7)   NULL,
    [UpdatedBy]                                  NVARCHAR (256)  NULL,
    [IsActive]                                   BIT             NULL,
    [TenderId]                                   INT             IDENTITY (1, 1) NOT NULL,
    [TenderName]                                 NVARCHAR (1024) NULL,
    [ReferenceNumber]                            NVARCHAR (100)  NULL,
    [TenderNumber]                               NVARCHAR (100)  NULL,
    [Purpose]                                    NVARCHAR (1024) NULL,
    [ConditionsBookletPrice]                     DECIMAL (18, 2) NULL,
    [EstimatedValue]                             DECIMAL (18, 2) NULL,
    [SupplierNeedSample]                         BIT             NULL,
    [SamplesDeliveryAddress]                     NVARCHAR (2048) NULL,
    [InitialGuaranteePercentage]                 DECIMAL (18, 2) NULL,
    [LastEnqueriesDate]                          DATETIME2 (7)   NULL,
    [LastOfferPresentationDate]                  DATETIME2 (7)   NULL,
    [OffersOpeningDate]                          DATETIME2 (7)   NULL,
    [InsideKSA]                                  BIT             NULL,
    [Details]                                    NVARCHAR (MAX)  NULL,
    [SubmitionDate]                              DATETIME2 (7)   NULL,
    [IsUnitSecreteryAccepted]                    BIT             NULL,
    [ActivityDescription]                        NVARCHAR (2000) NULL,
    [TenderAwardingType]                         BIT             NULL,
    [OffersCheckingDate]                         DATETIME2 (7)   NULL,
    [AuditorAgree]                               BIT             NULL,
    [CompetitionIsBudgeted]                      BIT             NULL,
    [TenderTypeOtherSelectionReason]             NVARCHAR (1024) NULL,
    [InitialGuaranteeAddress]                    NVARCHAR (1024) NULL,
    [AcceptedAnnouncementDate]                   DATETIME2 (7)   NULL,
    [AwardingStoppingPeriod]                     INT             NULL,
    [AwardingReportFileName]                     NVARCHAR (500)  NULL,
    [AwardingReportFileNameId]                   NVARCHAR (500)  NULL,
    [AgreementMonthes]                           INT             NULL,
    [AgreementDays]                              INT             NULL,
    [AgreementYears]                             INT             NULL,
    [NumberOfWinners]                            INT             NULL,
    [BonusValue]                                 DECIMAL (18, 2) NULL,
    [AwardingDiscountPercentage]                 INT             NULL,
    [AwardingMonths]                             INT             NULL,
    [HasGuarantee]                               BIT             NULL,
    [HasAlternativeOffer]                        BIT             NULL,
    [TenderPointsToPass]                         DECIMAL (18, 2) NOT NULL,
    [TechnicalAdministrativeCapacity]            DECIMAL (18, 2) NOT NULL,
    [FinancialCapacity]                          DECIMAL (18, 2) NOT NULL,
    [TemplateYears]                              INT             NULL,
    [BuildingName]                               NVARCHAR (100)  NULL,
    [FloarNumber]                                NVARCHAR (100)  NULL,
    [DepartmentName]                             NVARCHAR (100)  NULL,
    [DeliveryDate]                               DATETIME2 (7)   NULL,
    [CheckingDateSet]                            BIT             NULL,
    [OpeningNotificationSent]                    BIT             NULL,
    [UnitSpacialistWouldLikeToAttendTheCommitte] BIT             NULL,
    [QualificationTypeId]                        INT             NULL,
    [CreatedByTypeId]                            INT             NULL,
    [TenderStatusId]                             INT             NOT NULL,
    [TenderTypeId]                               INT             NOT NULL,
    [ConditionTemplateStageStatusId]             INT             NULL,
    [InvitationTypeId]                           INT             NULL,
    [TechnicalOrganizationId]                    INT             NULL,
    [OffersOpeningCommitteeId]                   INT             NULL,
    [OffersCheckingCommitteeId]                  INT             NULL,
    [DirectPurchaseCommitteeId]                  INT             NULL,
    [VROCommitteeId]                             INT             NULL,
    [PreQualificationCommitteeId]                INT             NULL,
    [SpendingCategoryId]                         INT             NULL,
    [OfferPresentationWayId]                     INT             NULL,
    [ReasonForPurchaseTenderTypeId]              INT             NULL,
    [PreQualificationId]                         INT             NULL,
    [AgreementTypeId]                            INT             NULL,
    [ReasonForLimitedTenderTypeId]               INT             NULL,
    [PreviousFramWorkId]                         INT             NULL,
    [PostQualificationTenderId]                  INT             NULL,
    [TenderFirstStageId]                         INT             NULL,
    [OffersOpeningAddressId]                     INT             NULL,
    [TenderConditionsTemplateId]                 INT             NULL,
    [ConditionsBookletDeliveryAddressId]         INT             NULL,
    [AgencyCode]                                 NVARCHAR (20)   NULL,
    [BranchId]                                   INT             NOT NULL,
    [VRORelatedBranchId]                         INT             NULL,
    [TenderUnitStatusId]                         INT             NULL,
    [FinalGuaranteeDeliveryAddress]              NVARCHAR (500)  NULL,
    [QuantityTableVersionId]                     INT             NULL,
    [NationalProductPercentage]                  DECIMAL (18, 2) NULL,
    [IsSendToEmarketPlace]                       BIT             NULL,
    [CheckingNotificationSent]                   BIT             NULL,
    [IsNotificationSentForStoppingPeriod]        BIT             NULL,
    [PreAnnouncementId]                          INT             NULL,
    [FinancialCheckingDateSet]                   BIT             NULL,
    [DirectPurchaseMemberId]                     INT             NULL,
    [IsLowBudgetDirectPurchase]                  BIT             NULL,
    [AnnouncementTemplateId]                     INT             NULL,
    CONSTRAINT [PK_Tender] PRIMARY KEY CLUSTERED ([TenderId] ASC),
    CONSTRAINT [FK_Tender_Address_ConditionsBookletDeliveryAddressId] FOREIGN KEY ([ConditionsBookletDeliveryAddressId]) REFERENCES [Tender].[Address] ([AddressId]),
    CONSTRAINT [FK_Tender_Address_OffersOpeningAddressId] FOREIGN KEY ([OffersOpeningAddressId]) REFERENCES [Tender].[Address] ([AddressId]),
    CONSTRAINT [FK_Tender_AgreementType_AgreementTypeId] FOREIGN KEY ([AgreementTypeId]) REFERENCES [LookUps].[AgreementType] ([AgreementTypeId]),
    CONSTRAINT [FK_Tender_Announcement_PreAnnouncementId] FOREIGN KEY ([PreAnnouncementId]) REFERENCES [Announcement].[Announcement] ([AnnouncementId]),
    CONSTRAINT [FK_Tender_AnnouncementSupplierTemplate_AnnouncementTemplateId] FOREIGN KEY ([AnnouncementTemplateId]) REFERENCES [AnnouncementTemplate].[AnnouncementSupplierTemplate] ([AnnouncementId]),
    CONSTRAINT [FK_Tender_Branch_BranchId] FOREIGN KEY ([BranchId]) REFERENCES [Branch].[Branch] ([BranchId]),
    CONSTRAINT [FK_Tender_Branch_VRORelatedBranchId] FOREIGN KEY ([VRORelatedBranchId]) REFERENCES [Branch].[Branch] ([BranchId]),
    CONSTRAINT [FK_Tender_Committee_DirectPurchaseCommitteeId] FOREIGN KEY ([DirectPurchaseCommitteeId]) REFERENCES [Committee].[Committee] ([CommitteeId]),
    CONSTRAINT [FK_Tender_Committee_OffersCheckingCommitteeId] FOREIGN KEY ([OffersCheckingCommitteeId]) REFERENCES [Committee].[Committee] ([CommitteeId]),
    CONSTRAINT [FK_Tender_Committee_OffersOpeningCommitteeId] FOREIGN KEY ([OffersOpeningCommitteeId]) REFERENCES [Committee].[Committee] ([CommitteeId]),
    CONSTRAINT [FK_Tender_Committee_PreQualificationCommitteeId] FOREIGN KEY ([PreQualificationCommitteeId]) REFERENCES [Committee].[Committee] ([CommitteeId]),
    CONSTRAINT [FK_Tender_Committee_TechnicalOrganizationId] FOREIGN KEY ([TechnicalOrganizationId]) REFERENCES [Committee].[Committee] ([CommitteeId]),
    CONSTRAINT [FK_Tender_Committee_VROCommitteeId] FOREIGN KEY ([VROCommitteeId]) REFERENCES [Committee].[Committee] ([CommitteeId]),
    CONSTRAINT [FK_Tender_GovAgency_AgencyCode] FOREIGN KEY ([AgencyCode]) REFERENCES [IDM].[GovAgency] ([AgencyCode]),
    CONSTRAINT [FK_Tender_InvitationType_InvitationTypeId] FOREIGN KEY ([InvitationTypeId]) REFERENCES [LookUps].[InvitationType] ([InvitationTypeId]),
    CONSTRAINT [FK_Tender_OfferPresentationWay_OfferPresentationWayId] FOREIGN KEY ([OfferPresentationWayId]) REFERENCES [LookUps].[OfferPresentationWay] ([Id]),
    CONSTRAINT [FK_Tender_QualificationType_QualificationTypeId] FOREIGN KEY ([QualificationTypeId]) REFERENCES [Qualification].[QualificationType] ([ID]),
    CONSTRAINT [FK_Tender_ReasonForLimitedTenderType_ReasonForLimitedTenderTypeId] FOREIGN KEY ([ReasonForLimitedTenderTypeId]) REFERENCES [LookUps].[ReasonForLimitedTenderType] ([Id]),
    CONSTRAINT [FK_Tender_ReasonForPurchaseTenderType_ReasonForPurchaseTenderTypeId] FOREIGN KEY ([ReasonForPurchaseTenderTypeId]) REFERENCES [LookUps].[ReasonForPurchaseTenderType] ([Id]),
    CONSTRAINT [FK_Tender_SpendingCategory_SpendingCategoryId] FOREIGN KEY ([SpendingCategoryId]) REFERENCES [LookUps].[SpendingCategory] ([SpendingCategoryId]),
    CONSTRAINT [FK_Tender_Tender_PostQualificationTenderId] FOREIGN KEY ([PostQualificationTenderId]) REFERENCES [Tender].[Tender] ([TenderId]),
    CONSTRAINT [FK_Tender_Tender_PreQualificationId] FOREIGN KEY ([PreQualificationId]) REFERENCES [Tender].[Tender] ([TenderId]),
    CONSTRAINT [FK_Tender_Tender_PreviousFramWorkId] FOREIGN KEY ([PreviousFramWorkId]) REFERENCES [Tender].[Tender] ([TenderId]),
    CONSTRAINT [FK_Tender_Tender_TenderFirstStageId] FOREIGN KEY ([TenderFirstStageId]) REFERENCES [Tender].[Tender] ([TenderId]),
    CONSTRAINT [FK_Tender_TenderConditionsTemplate_TenderConditionsTemplateId] FOREIGN KEY ([TenderConditionsTemplateId]) REFERENCES [Tender].[TenderConditionsTemplate] ([TenderConditionsTemplateId]),
    CONSTRAINT [FK_Tender_TenderConditoinsStatus_ConditionTemplateStageStatusId] FOREIGN KEY ([ConditionTemplateStageStatusId]) REFERENCES [LookUps].[TenderConditoinsStatus] ([TenderConditoinsStatusId]),
    CONSTRAINT [FK_Tender_TenderCreatedByType_CreatedByTypeId] FOREIGN KEY ([CreatedByTypeId]) REFERENCES [LookUps].[TenderCreatedByType] ([TenderCreatedByTypeId]),
    CONSTRAINT [FK_Tender_TenderStatus_TenderStatusId] FOREIGN KEY ([TenderStatusId]) REFERENCES [LookUps].[TenderStatus] ([TenderStatusId]),
    CONSTRAINT [FK_Tender_TenderType_TenderTypeId] FOREIGN KEY ([TenderTypeId]) REFERENCES [LookUps].[TenderType] ([TenderTypeId]),
    CONSTRAINT [FK_Tender_TenderUnitStatus_TenderUnitStatusId] FOREIGN KEY ([TenderUnitStatusId]) REFERENCES [LookUps].[TenderUnitStatus] ([TenderUnitStatusId]),
    CONSTRAINT [FK_Tender_UserProfile_DirectPurchaseMemberId] FOREIGN KEY ([DirectPurchaseMemberId]) REFERENCES [IDM].[UserProfile] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Tender_AgencyCode]
    ON [Tender].[Tender]([AgencyCode] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Tender_AgreementTypeId]
    ON [Tender].[Tender]([AgreementTypeId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Tender_BranchId]
    ON [Tender].[Tender]([BranchId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Tender_ConditionTemplateStageStatusId]
    ON [Tender].[Tender]([ConditionTemplateStageStatusId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Tender_ConditionsBookletDeliveryAddressId]
    ON [Tender].[Tender]([ConditionsBookletDeliveryAddressId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Tender_CreatedByTypeId]
    ON [Tender].[Tender]([CreatedByTypeId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Tender_DirectPurchaseCommitteeId]
    ON [Tender].[Tender]([DirectPurchaseCommitteeId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Tender_InvitationTypeId]
    ON [Tender].[Tender]([InvitationTypeId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Tender_OfferPresentationWayId]
    ON [Tender].[Tender]([OfferPresentationWayId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Tender_OffersCheckingCommitteeId]
    ON [Tender].[Tender]([OffersCheckingCommitteeId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Tender_OffersOpeningAddressId]
    ON [Tender].[Tender]([OffersOpeningAddressId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Tender_OffersOpeningCommitteeId]
    ON [Tender].[Tender]([OffersOpeningCommitteeId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Tender_PostQualificationTenderId]
    ON [Tender].[Tender]([PostQualificationTenderId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Tender_PreQualificationCommitteeId]
    ON [Tender].[Tender]([PreQualificationCommitteeId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Tender_PreQualificationId]
    ON [Tender].[Tender]([PreQualificationId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Tender_PreviousFramWorkId]
    ON [Tender].[Tender]([PreviousFramWorkId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Tender_QualificationTypeId]
    ON [Tender].[Tender]([QualificationTypeId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Tender_ReasonForLimitedTenderTypeId]
    ON [Tender].[Tender]([ReasonForLimitedTenderTypeId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Tender_ReasonForPurchaseTenderTypeId]
    ON [Tender].[Tender]([ReasonForPurchaseTenderTypeId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Tender_SpendingCategoryId]
    ON [Tender].[Tender]([SpendingCategoryId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Tender_TechnicalOrganizationId]
    ON [Tender].[Tender]([TechnicalOrganizationId] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Tender_TenderConditionsTemplateId]
    ON [Tender].[Tender]([TenderConditionsTemplateId] ASC) WHERE ([TenderConditionsTemplateId] IS NOT NULL);


GO
CREATE NONCLUSTERED INDEX [IX_Tender_TenderFirstStageId]
    ON [Tender].[Tender]([TenderFirstStageId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Tender_TenderStatusId]
    ON [Tender].[Tender]([TenderStatusId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Tender_TenderTypeId]
    ON [Tender].[Tender]([TenderTypeId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Tender_TenderUnitStatusId]
    ON [Tender].[Tender]([TenderUnitStatusId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Tender_VROCommitteeId]
    ON [Tender].[Tender]([VROCommitteeId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Tender_VRORelatedBranchId]
    ON [Tender].[Tender]([VRORelatedBranchId] ASC);


GO
CREATE NONCLUSTERED INDEX [Mahmoud]
    ON [Tender].[Tender]([IsActive] ASC, [TenderStatusId] ASC, [TenderTypeId] ASC);


GO
CREATE NONCLUSTERED INDEX [Mahmoud2]
    ON [Tender].[Tender]([IsActive] ASC, [TenderStatusId] ASC, [TenderTypeId] ASC)
    INCLUDE([CreatedAt], [TenderName], [ReferenceNumber], [TenderNumber], [ConditionsBookletPrice], [LastEnqueriesDate], [LastOfferPresentationDate], [OffersOpeningDate], [InsideKSA], [SubmitionDate], [OffersCheckingDate], [InvitationTypeId], [TechnicalOrganizationId], [OfferPresentationWayId], [PreQualificationId], [AgencyCode]);


GO
CREATE NONCLUSTERED INDEX [IX_Tender_PreAnnouncementId]
    ON [Tender].[Tender]([PreAnnouncementId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Tender_DirectPurchaseMemberId]
    ON [Tender].[Tender]([DirectPurchaseMemberId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Tender_AnnouncementTemplateId]
    ON [Tender].[Tender]([AnnouncementTemplateId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define Identity Of Tender', @level0type = N'SCHEMA', @level0name = N'Tender', @level1type = N'TABLE', @level1name = N'Tender', @level2type = N'COLUMN', @level2name = N'TenderId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define Name Of Tender', @level0type = N'SCHEMA', @level0name = N'Tender', @level1type = N'TABLE', @level1name = N'Tender', @level2type = N'COLUMN', @level2name = N'TenderName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define Reference Number Of Tender, its a unique Identifier Like Tender Identity', @level0type = N'SCHEMA', @level0name = N'Tender', @level1type = N'TABLE', @level1name = N'Tender', @level2type = N'COLUMN', @level2name = N'ReferenceNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define Number Of Tender', @level0type = N'SCHEMA', @level0name = N'Tender', @level1type = N'TABLE', @level1name = N'Tender', @level2type = N'COLUMN', @level2name = N'TenderNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define Purpose Of Tender', @level0type = N'SCHEMA', @level0name = N'Tender', @level1type = N'TABLE', @level1name = N'Tender', @level2type = N'COLUMN', @level2name = N'Purpose';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define the price of conditions booklet Of Tender, it is determined by the government agency and supplier can buy it', @level0type = N'SCHEMA', @level0name = N'Tender', @level1type = N'TABLE', @level1name = N'Tender', @level2type = N'COLUMN', @level2name = N'ConditionsBookletPrice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define the estimation value of tender', @level0type = N'SCHEMA', @level0name = N'Tender', @level1type = N'TABLE', @level1name = N'Tender', @level2type = N'COLUMN', @level2name = N'EstimatedValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Boolean detrmine tf the supplier need samples of the tender', @level0type = N'SCHEMA', @level0name = N'Tender', @level1type = N'TABLE', @level1name = N'Tender', @level2type = N'COLUMN', @level2name = N'SupplierNeedSample';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define the address of samples deleviry of the tender', @level0type = N'SCHEMA', @level0name = N'Tender', @level1type = N'TABLE', @level1name = N'Tender', @level2type = N'COLUMN', @level2name = N'SamplesDeliveryAddress';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define the percentage of initial gurantee for suppliers if agency require it', @level0type = N'SCHEMA', @level0name = N'Tender', @level1type = N'TABLE', @level1name = N'Tender', @level2type = N'COLUMN', @level2name = N'InitialGuaranteePercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define the last date that supplier can enquiry or ask questions for tender', @level0type = N'SCHEMA', @level0name = N'Tender', @level1type = N'TABLE', @level1name = N'Tender', @level2type = N'COLUMN', @level2name = N'LastEnqueriesDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define the last date that supplier can apply offers on Tender', @level0type = N'SCHEMA', @level0name = N'Tender', @level1type = N'TABLE', @level1name = N'Tender', @level2type = N'COLUMN', @level2name = N'LastOfferPresentationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define the opening date for opening the suppliers offers', @level0type = N'SCHEMA', @level0name = N'Tender', @level1type = N'TABLE', @level1name = N'Tender', @level2type = N'COLUMN', @level2name = N'OffersOpeningDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define the location of tender', @level0type = N'SCHEMA', @level0name = N'Tender', @level1type = N'TABLE', @level1name = N'Tender', @level2type = N'COLUMN', @level2name = N'InsideKSA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define more information about tender', @level0type = N'SCHEMA', @level0name = N'Tender', @level1type = N'TABLE', @level1name = N'Tender', @level2type = N'COLUMN', @level2name = N'Details';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define the publish/approval date of tender', @level0type = N'SCHEMA', @level0name = N'Tender', @level1type = N'TABLE', @level1name = N'Tender', @level2type = N'COLUMN', @level2name = N'SubmitionDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define the description of tender activiites', @level0type = N'SCHEMA', @level0name = N'Tender', @level1type = N'TABLE', @level1name = N'Tender', @level2type = N'COLUMN', @level2name = N'ActivityDescription';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define the awarding type of tender partial or total awarding', @level0type = N'SCHEMA', @level0name = N'Tender', @level1type = N'TABLE', @level1name = N'Tender', @level2type = N'COLUMN', @level2name = N'TenderAwardingType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define the checking date for checking the suppliers offers', @level0type = N'SCHEMA', @level0name = N'Tender', @level1type = N'TABLE', @level1name = N'Tender', @level2type = N'COLUMN', @level2name = N'OffersCheckingDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define the reason of selecting direct purchase or limited tender that not exist in reasons list', @level0type = N'SCHEMA', @level0name = N'Tender', @level1type = N'TABLE', @level1name = N'Tender', @level2type = N'COLUMN', @level2name = N'TenderTypeOtherSelectionReason';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define the address of initial gurantee that suppliers apply it', @level0type = N'SCHEMA', @level0name = N'Tender', @level1type = N'TABLE', @level1name = N'Tender', @level2type = N'COLUMN', @level2name = N'InitialGuaranteeAddress';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define the period that suppliers can add plaint on tender after awarding between 5 and 10 days', @level0type = N'SCHEMA', @level0name = N'Tender', @level1type = N'TABLE', @level1name = N'Tender', @level2type = N'COLUMN', @level2name = N'AwardingStoppingPeriod';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define the name of awarding report that entered while entering awarding data in awarding stage', @level0type = N'SCHEMA', @level0name = N'Tender', @level1type = N'TABLE', @level1name = N'Tender', @level2type = N'COLUMN', @level2name = N'AwardingReportFileName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define the identity of awarding report that entered while entering awarding data in awarding stage', @level0type = N'SCHEMA', @level0name = N'Tender', @level1type = N'TABLE', @level1name = N'Tender', @level2type = N'COLUMN', @level2name = N'AwardingReportFileNameId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Number of months for framework agreement', @level0type = N'SCHEMA', @level0name = N'Tender', @level1type = N'TABLE', @level1name = N'Tender', @level2type = N'COLUMN', @level2name = N'AgreementMonthes';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Number of days for framework agreement', @level0type = N'SCHEMA', @level0name = N'Tender', @level1type = N'TABLE', @level1name = N'Tender', @level2type = N'COLUMN', @level2name = N'AgreementDays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Number of years for framework agreement', @level0type = N'SCHEMA', @level0name = N'Tender', @level1type = N'TABLE', @level1name = N'Tender', @level2type = N'COLUMN', @level2name = N'AgreementYears';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Number of winners in the competition', @level0type = N'SCHEMA', @level0name = N'Tender', @level1type = N'TABLE', @level1name = N'Tender', @level2type = N'COLUMN', @level2name = N'NumberOfWinners';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bonus value of competition', @level0type = N'SCHEMA', @level0name = N'Tender', @level1type = N'TABLE', @level1name = N'Tender', @level2type = N'COLUMN', @level2name = N'BonusValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Value of the final guarantee', @level0type = N'SCHEMA', @level0name = N'Tender', @level1type = N'TABLE', @level1name = N'Tender', @level2type = N'COLUMN', @level2name = N'AwardingDiscountPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Final guarantee duration in months', @level0type = N'SCHEMA', @level0name = N'Tender', @level1type = N'TABLE', @level1name = N'Tender', @level2type = N'COLUMN', @level2name = N'AwardingMonths';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Boolean determine if the tender requires a final guarantee or not', @level0type = N'SCHEMA', @level0name = N'Tender', @level1type = N'TABLE', @level1name = N'Tender', @level2type = N'COLUMN', @level2name = N'HasGuarantee';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'determine if the supliers can apply an alternative offers', @level0type = N'SCHEMA', @level0name = N'Tender', @level1type = N'TABLE', @level1name = N'Tender', @level2type = N'COLUMN', @level2name = N'HasAlternativeOffer';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The points needed to pass a prequalification', @level0type = N'SCHEMA', @level0name = N'Tender', @level1type = N'TABLE', @level1name = N'Tender', @level2type = N'COLUMN', @level2name = N'TenderPointsToPass';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Technical administrative capacity', @level0type = N'SCHEMA', @level0name = N'Tender', @level1type = N'TABLE', @level1name = N'Tender', @level2type = N'COLUMN', @level2name = N'TechnicalAdministrativeCapacity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Financial Capacity', @level0type = N'SCHEMA', @level0name = N'Tender', @level1type = N'TABLE', @level1name = N'Tender', @level2type = N'COLUMN', @level2name = N'FinancialCapacity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'number of years in quantities tables', @level0type = N'SCHEMA', @level0name = N'Tender', @level1type = N'TABLE', @level1name = N'Tender', @level2type = N'COLUMN', @level2name = N'TemplateYears';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The location of building name for samples delivery', @level0type = N'SCHEMA', @level0name = N'Tender', @level1type = N'TABLE', @level1name = N'Tender', @level2type = N'COLUMN', @level2name = N'BuildingName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Floar number at buliding for samples delivery', @level0type = N'SCHEMA', @level0name = N'Tender', @level1type = N'TABLE', @level1name = N'Tender', @level2type = N'COLUMN', @level2name = N'FloarNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Department name at buliding for samples delivery', @level0type = N'SCHEMA', @level0name = N'Tender', @level1type = N'TABLE', @level1name = N'Tender', @level2type = N'COLUMN', @level2name = N'DepartmentName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The date of samples delivey to supplier', @level0type = N'SCHEMA', @level0name = N'Tender', @level1type = N'TABLE', @level1name = N'Tender', @level2type = N'COLUMN', @level2name = N'DeliveryDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Is a date set when starting the checking stage', @level0type = N'SCHEMA', @level0name = N'Tender', @level1type = N'TABLE', @level1name = N'Tender', @level2type = N'COLUMN', @level2name = N'CheckingDateSet';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Flag that states that a notification is sent to the opening committee when the opening date is passed', @level0type = N'SCHEMA', @level0name = N'Tender', @level1type = N'TABLE', @level1name = N'Tender', @level2type = N'COLUMN', @level2name = N'OpeningNotificationSent';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Determine if the unit manger want to attend th committee', @level0type = N'SCHEMA', @level0name = N'Tender', @level1type = N'TABLE', @level1name = N'Tender', @level2type = N'COLUMN', @level2name = N'UnitSpacialistWouldLikeToAttendTheCommitte';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'determine the type of qualification post or prequalification', @level0type = N'SCHEMA', @level0name = N'Tender', @level1type = N'TABLE', @level1name = N'Tender', @level2type = N'COLUMN', @level2name = N'QualificationTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'determine the tender created by vro or agency or agency related by vro', @level0type = N'SCHEMA', @level0name = N'Tender', @level1type = N'TABLE', @level1name = N'Tender', @level2type = N'COLUMN', @level2name = N'CreatedByTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Status of tender', @level0type = N'SCHEMA', @level0name = N'Tender', @level1type = N'TABLE', @level1name = N'Tender', @level2type = N'COLUMN', @level2name = N'TenderStatusId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Type of tender', @level0type = N'SCHEMA', @level0name = N'Tender', @level1type = N'TABLE', @level1name = N'Tender', @level2type = N'COLUMN', @level2name = N'TenderTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The Status of condition template stage', @level0type = N'SCHEMA', @level0name = N'Tender', @level1type = N'TABLE', @level1name = N'Tender', @level2type = N'COLUMN', @level2name = N'ConditionTemplateStageStatusId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Type of invitation on tender public or private', @level0type = N'SCHEMA', @level0name = N'Tender', @level1type = N'TABLE', @level1name = N'Tender', @level2type = N'COLUMN', @level2name = N'InvitationTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The technical committee for reply on suppliers enquireis', @level0type = N'SCHEMA', @level0name = N'Tender', @level1type = N'TABLE', @level1name = N'Tender', @level2type = N'COLUMN', @level2name = N'TechnicalOrganizationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The opening committee for opening offers', @level0type = N'SCHEMA', @level0name = N'Tender', @level1type = N'TABLE', @level1name = N'Tender', @level2type = N'COLUMN', @level2name = N'OffersOpeningCommitteeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The Checking committee for checking and awarding tender', @level0type = N'SCHEMA', @level0name = N'Tender', @level1type = N'TABLE', @level1name = N'Tender', @level2type = N'COLUMN', @level2name = N'OffersCheckingCommitteeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The direct purchase committee for checking and awarding tender from type direct purchase', @level0type = N'SCHEMA', @level0name = N'Tender', @level1type = N'TABLE', @level1name = N'Tender', @level2type = N'COLUMN', @level2name = N'DirectPurchaseCommitteeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The committe of VRO for opening, checking and awarding tender', @level0type = N'SCHEMA', @level0name = N'Tender', @level1type = N'TABLE', @level1name = N'Tender', @level2type = N'COLUMN', @level2name = N'VROCommitteeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define PreQualification Committee', @level0type = N'SCHEMA', @level0name = N'Tender', @level1type = N'TABLE', @level1name = N'Tender', @level2type = N'COLUMN', @level2name = N'PreQualificationCommitteeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define the spending Category of the tender', @level0type = N'SCHEMA', @level0name = N'Tender', @level1type = N'TABLE', @level1name = N'Tender', @level2type = N'COLUMN', @level2name = N'SpendingCategoryId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'method of apply offers by suppliers one file or two files', @level0type = N'SCHEMA', @level0name = N'Tender', @level1type = N'TABLE', @level1name = N'Tender', @level2type = N'COLUMN', @level2name = N'OfferPresentationWayId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The reason of selecting direct purchase tender', @level0type = N'SCHEMA', @level0name = N'Tender', @level1type = N'TABLE', @level1name = N'Tender', @level2type = N'COLUMN', @level2name = N'ReasonForPurchaseTenderTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The identity that identify pre qualification on the tender', @level0type = N'SCHEMA', @level0name = N'Tender', @level1type = N'TABLE', @level1name = N'Tender', @level2type = N'COLUMN', @level2name = N'PreQualificationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The identity that identify the type framework agreament opened or closed', @level0type = N'SCHEMA', @level0name = N'Tender', @level1type = N'TABLE', @level1name = N'Tender', @level2type = N'COLUMN', @level2name = N'AgreementTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The reason of selecting limited tender', @level0type = N'SCHEMA', @level0name = N'Tender', @level1type = N'TABLE', @level1name = N'Tender', @level2type = N'COLUMN', @level2name = N'ReasonForLimitedTenderTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The identity that identify the previous framework agreament', @level0type = N'SCHEMA', @level0name = N'Tender', @level1type = N'TABLE', @level1name = N'Tender', @level2type = N'COLUMN', @level2name = N'PreviousFramWorkId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The identity that identify post qualification on the tender', @level0type = N'SCHEMA', @level0name = N'Tender', @level1type = N'TABLE', @level1name = N'Tender', @level2type = N'COLUMN', @level2name = N'PostQualificationTenderId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The identity of tender of type first stage', @level0type = N'SCHEMA', @level0name = N'Tender', @level1type = N'TABLE', @level1name = N'Tender', @level2type = N'COLUMN', @level2name = N'TenderFirstStageId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The Identity that identify address of open tender offers', @level0type = N'SCHEMA', @level0name = N'Tender', @level1type = N'TABLE', @level1name = N'Tender', @level2type = N'COLUMN', @level2name = N'OffersOpeningAddressId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The Agency code of agency that create a tender', @level0type = N'SCHEMA', @level0name = N'Tender', @level1type = N'TABLE', @level1name = N'Tender', @level2type = N'COLUMN', @level2name = N'AgencyCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The branch that create a tender', @level0type = N'SCHEMA', @level0name = N'Tender', @level1type = N'TABLE', @level1name = N'Tender', @level2type = N'COLUMN', @level2name = N'BranchId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Branch assigned to vro', @level0type = N'SCHEMA', @level0name = N'Tender', @level1type = N'TABLE', @level1name = N'Tender', @level2type = N'COLUMN', @level2name = N'VRORelatedBranchId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The status of tender at unit review', @level0type = N'SCHEMA', @level0name = N'Tender', @level1type = N'TABLE', @level1name = N'Tender', @level2type = N'COLUMN', @level2name = N'TenderUnitStatusId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The address of deleviry the final guarantee', @level0type = N'SCHEMA', @level0name = N'Tender', @level1type = N'TABLE', @level1name = N'Tender', @level2type = N'COLUMN', @level2name = N'FinalGuaranteeDeliveryAddress';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The version of quantity table', @level0type = N'SCHEMA', @level0name = N'Tender', @level1type = N'TABLE', @level1name = N'Tender', @level2type = N'COLUMN', @level2name = N'QuantityTableVersionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The percentage of National Product', @level0type = N'SCHEMA', @level0name = N'Tender', @level1type = N'TABLE', @level1name = N'Tender', @level2type = N'COLUMN', @level2name = N'NationalProductPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Flag determine if the awarded tender is sent to e-market place or not', @level0type = N'SCHEMA', @level0name = N'Tender', @level1type = N'TABLE', @level1name = N'Tender', @level2type = N'COLUMN', @level2name = N'IsSendToEmarketPlace';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Flag that states that a notification is sent to the direct purchase committee when the checking date is tomorrow', @level0type = N'SCHEMA', @level0name = N'Tender', @level1type = N'TABLE', @level1name = N'Tender', @level2type = N'COLUMN', @level2name = N'CheckingNotificationSent';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Flag determine if a notification sent after finishing the stoping period of tender', @level0type = N'SCHEMA', @level0name = N'Tender', @level1type = N'TABLE', @level1name = N'Tender', @level2type = N'COLUMN', @level2name = N'IsNotificationSentForStoppingPeriod';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tha pre-announcement related to tender', @level0type = N'SCHEMA', @level0name = N'Tender', @level1type = N'TABLE', @level1name = N'Tender', @level2type = N'COLUMN', @level2name = N'PreAnnouncementId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Date set by the checking secretary at the start of the financial checking', @level0type = N'SCHEMA', @level0name = N'Tender', @level1type = N'TABLE', @level1name = N'Tender', @level2type = N'COLUMN', @level2name = N'FinancialCheckingDateSet';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The user responsible for low budget direct purchase', @level0type = N'SCHEMA', @level0name = N'Tender', @level1type = N'TABLE', @level1name = N'Tender', @level2type = N'COLUMN', @level2name = N'DirectPurchaseMemberId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'determine if the tender is low budget or not if the estimatinn value less than 30000 sar', @level0type = N'SCHEMA', @level0name = N'Tender', @level1type = N'TABLE', @level1name = N'Tender', @level2type = N'COLUMN', @level2name = N'IsLowBudgetDirectPurchase';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tha announcement list of suppliers related to tender', @level0type = N'SCHEMA', @level0name = N'Tender', @level1type = N'TABLE', @level1name = N'Tender', @level2type = N'COLUMN', @level2name = N'AnnouncementTemplateId';

