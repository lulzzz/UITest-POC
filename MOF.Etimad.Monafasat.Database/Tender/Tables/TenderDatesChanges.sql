CREATE TABLE [Tender].[TenderDatesChanges] (
    [CreatedAt]                 DATETIME2 (7)  NOT NULL,
    [CreatedBy]                 NVARCHAR (256) NULL,
    [UpdatedAt]                 DATETIME2 (7)  NULL,
    [UpdatedBy]                 NVARCHAR (256) NULL,
    [IsActive]                  BIT            NULL,
    [RevisionDateId]            INT            IDENTITY (1, 1) NOT NULL,
    [StatusId]                  INT            NOT NULL,
    [RejectionReason]           NVARCHAR (MAX) NULL,
    [LastEnqueriesDate]         DATETIME2 (7)  NULL,
    [LastOfferPresentationDate] DATETIME2 (7)  NULL,
    [LastOfferPresentationTime] NVARCHAR (MAX) NULL,
    [OffersOpeningDate]         DATETIME2 (7)  NULL,
    [OffersOpeningTime]         NVARCHAR (MAX) NULL,
    [OffersCheckingDate]        DATETIME2 (7)  NULL,
    [OffersCheckingTime]        NVARCHAR (MAX) NULL,
    [TenderChangeRequestId]     INT            NOT NULL,
    CONSTRAINT [PK_TenderDatesChanges] PRIMARY KEY CLUSTERED ([RevisionDateId] ASC),
    CONSTRAINT [FK_TenderDatesChanges_TenderChangeRequest_TenderChangeRequestId] FOREIGN KEY ([TenderChangeRequestId]) REFERENCES [Tender].[TenderChangeRequest] ([TenderChangeRequestId]),
    CONSTRAINT [FK_TenderDatesChanges_TenderStatus_StatusId] FOREIGN KEY ([StatusId]) REFERENCES [LookUps].[TenderStatus] ([TenderStatusId])
);


GO
CREATE NONCLUSTERED INDEX [IX_TenderDatesChanges_StatusId]
    ON [Tender].[TenderDatesChanges]([StatusId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_TenderDatesChanges_TenderChangeRequestId]
    ON [Tender].[TenderDatesChanges]([TenderChangeRequestId] ASC);

