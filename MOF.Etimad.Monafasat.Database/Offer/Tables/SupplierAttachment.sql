CREATE TABLE [Offer].[SupplierAttachment] (
    [CreatedAt]                DATETIME2 (7)   NOT NULL,
    [CreatedBy]                NVARCHAR (256)  NULL,
    [UpdatedAt]                DATETIME2 (7)   NULL,
    [UpdatedBy]                NVARCHAR (256)  NULL,
    [IsActive]                 BIT             NULL,
    [AttachmentId]             INT             IDENTITY (1, 1) NOT NULL,
    [FileName]                 NVARCHAR (1024) NULL,
    [FileNetReferenceId]       NVARCHAR (1024) NULL,
    [OfferId]                  INT             NOT NULL,
    [AttachType]               INT             NOT NULL,
    [IsBankGuaranteeValid]     BIT             NULL,
    [GuaranteeNumber]          NVARCHAR (500)  NULL,
    [BankId]                   INT             NULL,
    [Amount]                   DECIMAL (18, 2) NULL,
    [GuaranteeStartDate]       DATETIME2 (7)   NULL,
    [GuaranteeEndDate]         DATETIME2 (7)   NULL,
    [GuaranteeDays]            INT             NULL,
    [IsForApplier]             BIT             NULL,
    [Degree]                   INT             NULL,
    [ConstructionWorkId]       INT             NULL,
    [MaintenanceRunningWorkId] INT             NULL,
    CONSTRAINT [PK_SupplierAttachment] PRIMARY KEY CLUSTERED ([AttachmentId] ASC),
    CONSTRAINT [FK_SupplierAttachment_Bank_BankId] FOREIGN KEY ([BankId]) REFERENCES [LookUps].[Bank] ([BankId]),
    CONSTRAINT [FK_SupplierAttachment_ConstructionWork_ConstructionWorkId] FOREIGN KEY ([ConstructionWorkId]) REFERENCES [LookUps].[ConstructionWork] ([ConstructionWorkId]),
    CONSTRAINT [FK_SupplierAttachment_MaintenanceRunningWork_MaintenanceRunningWorkId] FOREIGN KEY ([MaintenanceRunningWorkId]) REFERENCES [LookUps].[MaintenanceRunningWork] ([MaintenanceRunningWorkId]),
    CONSTRAINT [FK_SupplierAttachment_Offer_OfferId] FOREIGN KEY ([OfferId]) REFERENCES [Offer].[Offer] ([OfferId])
);


GO
CREATE NONCLUSTERED INDEX [IX_SupplierAttachment_OfferId]
    ON [Offer].[SupplierAttachment]([OfferId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_SupplierAttachment_BankId]
    ON [Offer].[SupplierAttachment]([BankId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_SupplierAttachment_ConstructionWorkId]
    ON [Offer].[SupplierAttachment]([ConstructionWorkId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_SupplierAttachment_MaintenanceRunningWorkId]
    ON [Offer].[SupplierAttachment]([MaintenanceRunningWorkId] ASC);

