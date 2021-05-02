CREATE TABLE [dbo].[SupplierBankGuaranteeDetail] (
    [CreatedAt]            DATETIME2 (7)   NOT NULL,
    [CreatedBy]            NVARCHAR (256)  NULL,
    [UpdatedAt]            DATETIME2 (7)   NULL,
    [UpdatedBy]            NVARCHAR (256)  NULL,
    [IsActive]             BIT             NULL,
    [BankGuaranteeId]      INT             IDENTITY (1, 1) NOT NULL,
    [IsBankGuaranteeValid] BIT             NULL,
    [GuaranteeNumber]      NVARCHAR (500)  NOT NULL,
    [BankId]               INT             NOT NULL,
    [Amount]               DECIMAL (18, 2) NOT NULL,
    [GuaranteeStartDate]   DATETIME2 (7)   NULL,
    [GuaranteeEndDate]     DATETIME2 (7)   NULL,
    [GuaranteeDays]        INT             NULL,
    [OfferId]              INT             NULL,
    CONSTRAINT [PK_SupplierBankGuaranteeDetail] PRIMARY KEY CLUSTERED ([BankGuaranteeId] ASC),
    CONSTRAINT [FK_SupplierBankGuaranteeDetail_Bank_BankId] FOREIGN KEY ([BankId]) REFERENCES [LookUps].[Bank] ([BankId]),
    CONSTRAINT [FK_SupplierBankGuaranteeDetail_Offer_OfferId] FOREIGN KEY ([OfferId]) REFERENCES [Offer].[Offer] ([OfferId])
);


GO
CREATE NONCLUSTERED INDEX [IX_SupplierBankGuaranteeDetail_BankId]
    ON [dbo].[SupplierBankGuaranteeDetail]([BankId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_SupplierBankGuaranteeDetail_OfferId]
    ON [dbo].[SupplierBankGuaranteeDetail]([OfferId] ASC);

