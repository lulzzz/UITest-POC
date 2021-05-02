CREATE TABLE [Sadad].[BillPaymentDetails] (
    [CreatedAt]         DATETIME2 (7)   NOT NULL,
    [CreatedBy]         NVARCHAR (256)  NULL,
    [UpdatedAt]         DATETIME2 (7)   NULL,
    [UpdatedBy]         NVARCHAR (256)  NULL,
    [IsActive]          BIT             NULL,
    [Id]                INT             IDENTITY (1, 1) NOT NULL,
    [AmountDue]         NUMERIC (15, 2) NOT NULL,
    [BillInvoiceNumber] NVARCHAR (50)   NULL,
    [GFSCode]           NVARCHAR (100)  NULL,
    [FeesTypeId]        INT             NOT NULL,
    [BillInfoId]        INT             NULL,
    CONSTRAINT [PK_BillPaymentDetails] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_BillPaymentDetails_BillInfo_BillInfoId] FOREIGN KEY ([BillInfoId]) REFERENCES [Sadad].[BillInfo] ([Id]),
    CONSTRAINT [FK_BillPaymentDetails_TenderFeesType_FeesTypeId] FOREIGN KEY ([FeesTypeId]) REFERENCES [LookUps].[TenderFeesType] ([TenderFeesTypeId])
);


GO
CREATE NONCLUSTERED INDEX [IX_BillPaymentDetails_BillInfoId]
    ON [Sadad].[BillPaymentDetails]([BillInfoId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_BillPaymentDetails_FeesTypeId]
    ON [Sadad].[BillPaymentDetails]([FeesTypeId] ASC);

