CREATE TABLE [Tender].[TenderConditionsTemplateTechnicalOutput] (
    [CreatedAt]                                 DATETIME2 (7)   NOT NULL,
    [CreatedBy]                                 NVARCHAR (256)  NULL,
    [UpdatedAt]                                 DATETIME2 (7)   NULL,
    [UpdatedBy]                                 NVARCHAR (256)  NULL,
    [IsActive]                                  BIT             NULL,
    [TenderConditionsTemplateTechnicalOutputId] INT             IDENTITY (1, 1) NOT NULL,
    [OutputStage]                               NVARCHAR (1000) NOT NULL,
    [OutputName]                                NVARCHAR (1000) NOT NULL,
    [OutputDescriptions]                        NVARCHAR (2000) NOT NULL,
    [OutputDeliveryTime]                        NVARCHAR (100)  NOT NULL,
    [ConditionsTemplateId]                      INT             NOT NULL,
    CONSTRAINT [PK_TenderConditionsTemplateTechnicalOutput] PRIMARY KEY CLUSTERED ([TenderConditionsTemplateTechnicalOutputId] ASC),
    CONSTRAINT [FK_TenderConditionsTemplateTechnicalOutput_TenderConditionsTemplate_ConditionsTemplateId] FOREIGN KEY ([ConditionsTemplateId]) REFERENCES [Tender].[TenderConditionsTemplate] ([TenderConditionsTemplateId])
);


GO
CREATE NONCLUSTERED INDEX [IX_TenderConditionsTemplateTechnicalOutput_ConditionsTemplateId]
    ON [Tender].[TenderConditionsTemplateTechnicalOutput]([ConditionsTemplateId] ASC);

