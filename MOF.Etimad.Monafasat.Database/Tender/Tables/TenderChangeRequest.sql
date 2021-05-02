CREATE TABLE [Tender].[TenderChangeRequest] (
    [CreatedAt]                    DATETIME2 (7)   NOT NULL,
    [CreatedBy]                    NVARCHAR (256)  NULL,
    [UpdatedAt]                    DATETIME2 (7)   NULL,
    [UpdatedBy]                    NVARCHAR (256)  NULL,
    [IsActive]                     BIT             NULL,
    [TenderChangeRequestId]        INT             IDENTITY (1, 1) NOT NULL,
    [TenderId]                     INT             NOT NULL,
    [ChangeRequestStatusId]        INT             NOT NULL,
    [ChangeRequestTypeId]          INT             NOT NULL,
    [RequestedByRoleName]          NVARCHAR (MAX)  NULL,
    [RejectionReason]              NVARCHAR (MAX)  NULL,
    [HasAlternativeOffer]          BIT             NULL,
    [CancelationReasonDescription] NVARCHAR (1000) NULL,
    [CancelationReasonId]          INT             NULL,
    CONSTRAINT [PK_TenderChangeRequest] PRIMARY KEY CLUSTERED ([TenderChangeRequestId] ASC),
    CONSTRAINT [FK_TenderChangeRequest_CancelationReason_CancelationReasonId] FOREIGN KEY ([CancelationReasonId]) REFERENCES [LookUps].[CancelationReason] ([CancelationReasonId]),
    CONSTRAINT [FK_TenderChangeRequest_ChangeRequestStatus_ChangeRequestStatusId] FOREIGN KEY ([ChangeRequestStatusId]) REFERENCES [LookUps].[ChangeRequestStatus] ([Id]),
    CONSTRAINT [FK_TenderChangeRequest_ChangeRequestType_ChangeRequestTypeId] FOREIGN KEY ([ChangeRequestTypeId]) REFERENCES [LookUps].[ChangeRequestType] ([Id]),
    CONSTRAINT [FK_TenderChangeRequest_Tender_TenderId] FOREIGN KEY ([TenderId]) REFERENCES [Tender].[Tender] ([TenderId])
);


GO
CREATE NONCLUSTERED INDEX [IX_TenderChangeRequest_CancelationReasonId]
    ON [Tender].[TenderChangeRequest]([CancelationReasonId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_TenderChangeRequest_ChangeRequestStatusId]
    ON [Tender].[TenderChangeRequest]([ChangeRequestStatusId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_TenderChangeRequest_ChangeRequestTypeId]
    ON [Tender].[TenderChangeRequest]([ChangeRequestTypeId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_TenderChangeRequest_TenderId]
    ON [Tender].[TenderChangeRequest]([TenderId] ASC);

