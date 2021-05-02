CREATE TABLE [CommunicationRequest].[PlaintRequestNotification] (
    [CreatedAt]                   DATETIME2 (7)  NOT NULL,
    [CreatedBy]                   NVARCHAR (256) NULL,
    [UpdatedAt]                   DATETIME2 (7)  NULL,
    [UpdatedBy]                   NVARCHAR (256) NULL,
    [IsActive]                    BIT            NULL,
    [PlaintRequestNotificationId] INT            IDENTITY (1, 1) NOT NULL,
    [IsSent]                      BIT            NOT NULL,
    [CommunicationRequestId]      INT            NOT NULL,
    [ApprovalDate]                DATETIME2 (7)  NULL,
    CONSTRAINT [PK_PlaintRequestNotification] PRIMARY KEY CLUSTERED ([PlaintRequestNotificationId] ASC),
    CONSTRAINT [FK_PlaintRequestNotification_AgencyCommunicationRequest_CommunicationRequestId] FOREIGN KEY ([CommunicationRequestId]) REFERENCES [CommunicationRequest].[AgencyCommunicationRequest] ([AgencyRequestId])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_PlaintRequestNotification_CommunicationRequestId]
    ON [CommunicationRequest].[PlaintRequestNotification]([CommunicationRequestId] ASC);

