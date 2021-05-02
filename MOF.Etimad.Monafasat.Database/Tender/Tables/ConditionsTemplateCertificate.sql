CREATE TABLE [Tender].[ConditionsTemplateCertificate] (
    [CreatedAt]            DATETIME2 (7)  NOT NULL,
    [CreatedBy]            NVARCHAR (256) NULL,
    [UpdatedAt]            DATETIME2 (7)  NULL,
    [UpdatedBy]            NVARCHAR (256) NULL,
    [IsActive]             BIT            NULL,
    [Id]                   INT            IDENTITY (1, 1) NOT NULL,
    [CerificateId]         INT            NOT NULL,
    [ConditionsTemplateId] INT            NOT NULL,
    CONSTRAINT [PK_ConditionsTemplateCertificate] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ConditionsTemplateCertificate_TenderConditionsTemplate_ConditionsTemplateId] FOREIGN KEY ([ConditionsTemplateId]) REFERENCES [Tender].[TenderConditionsTemplate] ([TenderConditionsTemplateId]),
    CONSTRAINT [FK_ConditionsTemplateCertificate_VendorCertificate_CerificateId] FOREIGN KEY ([CerificateId]) REFERENCES [LookUps].[VendorCertificate] ([VendorCertificateId])
);


GO
CREATE NONCLUSTERED INDEX [IX_ConditionsTemplateCertificate_CerificateId]
    ON [Tender].[ConditionsTemplateCertificate]([CerificateId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ConditionsTemplateCertificate_ConditionsTemplateId]
    ON [Tender].[ConditionsTemplateCertificate]([ConditionsTemplateId] ASC);

