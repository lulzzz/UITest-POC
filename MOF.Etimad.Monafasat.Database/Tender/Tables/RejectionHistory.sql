CREATE TABLE [Tender].[RejectionHistory] (
    [CreatedAt]                                 DATETIME2 (7)   NOT NULL,
    [CreatedBy]                                 NVARCHAR (256)  NULL,
    [UpdatedAt]                                 DATETIME2 (7)   NULL,
    [UpdatedBy]                                 NVARCHAR (256)  NULL,
    [IsActive]                                  BIT             NULL,
    [RejectionHistoryId]                        INT             IDENTITY (1, 1) NOT NULL,
    [ReuestId]                                  INT             NOT NULL,
    [RequestsRejectionTypeId]                   INT             NOT NULL,
    [RejectionReason]                           NVARCHAR (2000) NULL,
    [StatusId]                                  INT             NULL,
    [AgencyCommunicationRequestAgencyRequestId] INT             NULL,
    CONSTRAINT [PK_RejectionHistory] PRIMARY KEY CLUSTERED ([RejectionHistoryId] ASC),
    CONSTRAINT [FK_RejectionHistory_AgencyCommunicationRequest_AgencyCommunicationRequestAgencyRequestId] FOREIGN KEY ([AgencyCommunicationRequestAgencyRequestId]) REFERENCES [CommunicationRequest].[AgencyCommunicationRequest] ([AgencyRequestId]),
    CONSTRAINT [FK_RejectionHistory_RequestsRejectionType_RequestsRejectionTypeId] FOREIGN KEY ([RequestsRejectionTypeId]) REFERENCES [LookUps].[RequestsRejectionType] ([RequestTypeId])
);


GO
CREATE NONCLUSTERED INDEX [IX_RejectionHistory_AgencyCommunicationRequestAgencyRequestId]
    ON [Tender].[RejectionHistory]([AgencyCommunicationRequestAgencyRequestId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_RejectionHistory_RequestsRejectionTypeId]
    ON [Tender].[RejectionHistory]([RequestsRejectionTypeId] ASC);

