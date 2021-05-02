CREATE TABLE [Tender].[TenderAwardingHistory] (
    [CreatedAt]                     DATETIME2 (7)   NOT NULL,
    [CreatedBy]                     NVARCHAR (256)  NULL,
    [UpdatedAt]                     DATETIME2 (7)   NULL,
    [UpdatedBy]                     NVARCHAR (256)  NULL,
    [IsActive]                      BIT             NULL,
    [TenderAwardingHistoryId]       INT             IDENTITY (1, 1) NOT NULL,
    [CommercialRegisterationNumber] NVARCHAR (20)   NULL,
    [TenderId]                      INT             NOT NULL,
    [TenderAwardingType]            BIT             NULL,
    [AwardingValue]                 DECIMAL (18, 2) NULL,
    [AwardingIndex]                 INT             NOT NULL,
    CONSTRAINT [PK_TenderAwardingHistory] PRIMARY KEY CLUSTERED ([TenderAwardingHistoryId] ASC),
    CONSTRAINT [FK_TenderAwardingHistory_Supplier_CommercialRegisterationNumber] FOREIGN KEY ([CommercialRegisterationNumber]) REFERENCES [IDM].[Supplier] ([SelectedCr]),
    CONSTRAINT [FK_TenderAwardingHistory_Tender_TenderId] FOREIGN KEY ([TenderId]) REFERENCES [Tender].[Tender] ([TenderId])
);


GO
CREATE NONCLUSTERED INDEX [IX_TenderAwardingHistory_CommercialRegisterationNumber]
    ON [Tender].[TenderAwardingHistory]([CommercialRegisterationNumber] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_TenderAwardingHistory_TenderId]
    ON [Tender].[TenderAwardingHistory]([TenderId] ASC);

