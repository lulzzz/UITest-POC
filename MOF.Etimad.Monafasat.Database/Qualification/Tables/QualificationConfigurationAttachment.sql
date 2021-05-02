CREATE TABLE [Qualification].[QualificationConfigurationAttachment] (
    [CreatedAt]                   DATETIME2 (7)  NOT NULL,
    [CreatedBy]                   NVARCHAR (256) NULL,
    [UpdatedAt]                   DATETIME2 (7)  NULL,
    [UpdatedBy]                   NVARCHAR (256) NULL,
    [IsActive]                    BIT            NULL,
    [ID]                          INT            IDENTITY (1, 1) NOT NULL,
    [FileName]                    NVARCHAR (MAX) NULL,
    [QualificationSupplierDataId] INT            NOT NULL,
    [FileReferenceId]             NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_QualificationConfigurationAttachment] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_QualificationConfigurationAttachment_QualificationSupplierData_QualificationSupplierDataId] FOREIGN KEY ([QualificationSupplierDataId]) REFERENCES [Qualification].[QualificationSupplierData] ([ID])
);


GO
CREATE NONCLUSTERED INDEX [IX_QualificationConfigurationAttachment_QualificationSupplierDataId]
    ON [Qualification].[QualificationConfigurationAttachment]([QualificationSupplierDataId] ASC);

