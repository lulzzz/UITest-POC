CREATE TABLE [CommunicationRequest].[SupplierExtendOfferDatesRequest] (
    [CreatedAt]                         DATETIME2 (7)  NOT NULL,
    [CreatedBy]                         NVARCHAR (256) NULL,
    [UpdatedAt]                         DATETIME2 (7)  NULL,
    [UpdatedBy]                         NVARCHAR (256) NULL,
    [IsActive]                          BIT            NULL,
    [SupplierExtendOfferDatesRequestId] INT            IDENTITY (1, 1) NOT NULL,
    [ExtendOfferDatesRequestReason]     NVARCHAR (MAX) NULL,
    [ExtendOfferDatesRequestedDate]     DATETIME2 (7)  NULL,
    [CR]                                NVARCHAR (MAX) NULL,
    [AgencyCommunicationRequestId]      INT            NOT NULL,
    CONSTRAINT [PK_SupplierExtendOfferDatesRequest] PRIMARY KEY CLUSTERED ([SupplierExtendOfferDatesRequestId] ASC),
    CONSTRAINT [FK_SupplierExtendOfferDatesRequest_AgencyCommunicationRequest_AgencyCommunicationRequestId] FOREIGN KEY ([AgencyCommunicationRequestId]) REFERENCES [CommunicationRequest].[AgencyCommunicationRequest] ([AgencyRequestId])
);


GO
CREATE NONCLUSTERED INDEX [IX_SupplierExtendOfferDatesRequest_AgencyCommunicationRequestId]
    ON [CommunicationRequest].[SupplierExtendOfferDatesRequest]([AgencyCommunicationRequestId] ASC);

