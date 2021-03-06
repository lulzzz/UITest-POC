CREATE TABLE [Tender].[TenderConditionsTemplate] (
    [CreatedAt]                                             DATETIME2 (7)   NOT NULL,
    [CreatedBy]                                             NVARCHAR (256)  NULL,
    [UpdatedAt]                                             DATETIME2 (7)   NULL,
    [UpdatedBy]                                             NVARCHAR (256)  NULL,
    [IsActive]                                              BIT             NULL,
    [TenderConditionsTemplateId]                            INT             IDENTITY (1, 1) NOT NULL,
    [AgencyDecalration]                                     NVARCHAR (2000) NULL,
    [AgentPhone]                                            NVARCHAR (20)   NULL,
    [AgentFax]                                              NVARCHAR (20)   NULL,
    [AgentJob]                                              NVARCHAR (1000) NULL,
    [AgentEmail]                                            NVARCHAR (500)  NULL,
    [Description]                                           NVARCHAR (MAX)  NULL,
    [AgentName]                                             NVARCHAR (2000) NULL,
    [TenderFragmentation]                                   NVARCHAR (MAX)  NULL,
    [HideTenderFragmentation]                               BIT             NOT NULL,
    [HideCerificatesIDs]                                    BIT             NOT NULL,
    [FinancialOfferDocuments]                               NVARCHAR (2000) NULL,
    [HideTechnicalDocumentSections]                         BIT             NOT NULL,
    [TechnicalOfferDocuments]                               NVARCHAR (2000) NULL,
    [MaxTimeToAnswerQuestions]                              INT             NULL,
    [AlternativeEmailForCommuncation]                       NVARCHAR (500)  NULL,
    [DocumentStyle]                                         NVARCHAR (MAX)  NULL,
    [WritePrice]                                            NVARCHAR (MAX)  NULL,
    [ConfirimJoiningTheTender]                              NVARCHAR (MAX)  NULL,
    [RemoveHowToApplyTechnicalAndFinancialOffersSectionWay] BIT             NOT NULL,
    [ApplyOffersTemplateId]                                 NVARCHAR (MAX)  NULL,
    [AllowancePeriodToAddOffersIfNotAddedBeofre]            INT             NULL,
    [AllowedOfferSiningDays]                                INT             NULL,
    [ProjectsScope]                                         NVARCHAR (MAX)  NULL,
    [WorksProgram]                                          NVARCHAR (MAX)  NULL,
    [WorkLocationDetails]                                   NVARCHAR (MAX)  NULL,
    [WorkforceSpecifications]                               NVARCHAR (MAX)  NULL,
    [SpecialConditions]                                     NVARCHAR (MAX)  NULL,
    [Attachments]                                           NVARCHAR (MAX)  NULL,
    [MaterialsSpecifications]                               NVARCHAR (MAX)  NULL,
    [EquipmentsSpecifications]                              NVARCHAR (MAX)  NULL,
    [ContractBasedOnPerformanceDetails]                     NVARCHAR (1000) NULL,
    [ServicesAndWorkImplementationsMethod]                  NVARCHAR (MAX)  NULL,
    [QualitySpecifications]                                 NVARCHAR (MAX)  NULL,
    [SafetySpecifications]                                  NVARCHAR (MAX)  NULL,
    [TenderConditionsTemplateMaterialInofmrationId]         INT             NULL,
    CONSTRAINT [PK_TenderConditionsTemplate] PRIMARY KEY CLUSTERED ([TenderConditionsTemplateId] ASC),
    CONSTRAINT [FK_TenderConditionsTemplate_TenderConditionsTemplateMaterialsInofmration_TenderConditionsTemplateMaterialInofmrationId] FOREIGN KEY ([TenderConditionsTemplateMaterialInofmrationId]) REFERENCES [Tender].[TenderConditionsTemplateMaterialsInofmration] ([TenderConditionsTemplateMaterialInofmrationId])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_TenderConditionsTemplate_TenderConditionsTemplateMaterialInofmrationId]
    ON [Tender].[TenderConditionsTemplate]([TenderConditionsTemplateMaterialInofmrationId] ASC) WHERE ([TenderConditionsTemplateMaterialInofmrationId] IS NOT NULL);

