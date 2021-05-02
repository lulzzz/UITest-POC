CREATE TABLE [CommunicationRequest].[ExtendOffersValidity] (
    [CreatedAt]                    DATETIME2 (7)  NOT NULL,
    [CreatedBy]                    NVARCHAR (256) NULL,
    [UpdatedAt]                    DATETIME2 (7)  NULL,
    [UpdatedBy]                    NVARCHAR (256) NULL,
    [IsActive]                     BIT            NULL,
    [ExtendOffersValidityId]       INT            IDENTITY (1, 1) NOT NULL,
    [AgencyCommunicationRequestId] INT            NOT NULL,
    [OffersDuration]               INT            NOT NULL,
    [NewOffersExpiryDate]          DATETIME2 (7)  NOT NULL,
    [ExtendOffersReason]           NVARCHAR (MAX) NULL,
    [ReplyReceivingDurationDays]   INT            NOT NULL,
    [ReplyReceivingDurationTime]   NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_ExtendOffersValidity] PRIMARY KEY CLUSTERED ([ExtendOffersValidityId] ASC),
    CONSTRAINT [FK_ExtendOffersValidity_AgencyCommunicationRequest_AgencyCommunicationRequestId] FOREIGN KEY ([AgencyCommunicationRequestId]) REFERENCES [CommunicationRequest].[AgencyCommunicationRequest] ([AgencyRequestId])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_ExtendOffersValidity_AgencyCommunicationRequestId]
    ON [CommunicationRequest].[ExtendOffersValidity]([AgencyCommunicationRequestId] ASC);

