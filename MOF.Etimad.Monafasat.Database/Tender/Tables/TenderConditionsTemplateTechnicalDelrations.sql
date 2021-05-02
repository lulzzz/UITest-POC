CREATE TABLE [Tender].[TenderConditionsTemplateTechnicalDelrations] (
    [CreatedAt]                                     DATETIME2 (7)   NOT NULL,
    [CreatedBy]                                     NVARCHAR (256)  NULL,
    [UpdatedAt]                                     DATETIME2 (7)   NULL,
    [UpdatedBy]                                     NVARCHAR (256)  NULL,
    [IsActive]                                      BIT             NULL,
    [TenderConditionsTemplateTechnicalDeclrationId] INT             IDENTITY (1, 1) NOT NULL,
    [Term]                                          NVARCHAR (2000) NOT NULL,
    [Decleration]                                   NVARCHAR (2000) NOT NULL,
    [ConditionsTemplateId]                          INT             NOT NULL,
    CONSTRAINT [PK_TenderConditionsTemplateTechnicalDelrations] PRIMARY KEY CLUSTERED ([TenderConditionsTemplateTechnicalDeclrationId] ASC),
    CONSTRAINT [FK_TenderConditionsTemplateTechnicalDelrations_TenderConditionsTemplate_ConditionsTemplateId] FOREIGN KEY ([ConditionsTemplateId]) REFERENCES [Tender].[TenderConditionsTemplate] ([TenderConditionsTemplateId])
);


GO
CREATE NONCLUSTERED INDEX [IX_TenderConditionsTemplateTechnicalDelrations_ConditionsTemplateId]
    ON [Tender].[TenderConditionsTemplateTechnicalDelrations]([ConditionsTemplateId] ASC);

