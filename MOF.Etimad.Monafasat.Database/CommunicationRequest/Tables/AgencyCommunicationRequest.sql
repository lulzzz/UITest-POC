CREATE TABLE [CommunicationRequest].[AgencyCommunicationRequest] (
    [CreatedAt]                         DATETIME2 (7)   NOT NULL,
    [CreatedBy]                         NVARCHAR (256)  NULL,
    [UpdatedAt]                         DATETIME2 (7)   NULL,
    [UpdatedBy]                         NVARCHAR (256)  NULL,
    [IsActive]                          BIT             NULL,
    [AgencyRequestId]                   INT             IDENTITY (1, 1) NOT NULL,
    [TenderId]                          INT             NOT NULL,
    [AgencyRequestTypeId]               INT             NOT NULL,
    [RejectionReason]                   NVARCHAR (1000) NULL,
    [EscalationRejectionReason]         NVARCHAR (1000) NULL,
    [Details]                           NVARCHAR (MAX)  NULL,
    [IsReported]                        BIT             NOT NULL,
    [TenderPlaintRequestProcedureId]    INT             NULL,
    [StatusId]                          INT             NOT NULL,
    [EscalationStatusId]                INT             NULL,
    [EscalationAcceptanceStatusId]      INT             NULL,
    [PlaintAcceptanceStatusId]          INT             NULL,
    [RequestedByRoleName]               NVARCHAR (MAX)  NULL,
    [SupplierExtendOfferDatesRequestId] INT             NULL,
    CONSTRAINT [PK_AgencyCommunicationRequest] PRIMARY KEY CLUSTERED ([AgencyRequestId] ASC),
    CONSTRAINT [FK_AgencyCommunicationRequest_AgencyCommunicationPlaintStatus_EscalationAcceptanceStatusId] FOREIGN KEY ([EscalationAcceptanceStatusId]) REFERENCES [LookUps].[AgencyCommunicationPlaintStatus] ([Id]),
    CONSTRAINT [FK_AgencyCommunicationRequest_AgencyCommunicationPlaintStatus_PlaintAcceptanceStatusId] FOREIGN KEY ([PlaintAcceptanceStatusId]) REFERENCES [LookUps].[AgencyCommunicationPlaintStatus] ([Id]),
    CONSTRAINT [FK_AgencyCommunicationRequest_AgencyCommunicationRequestStatus_EscalationStatusId] FOREIGN KEY ([EscalationStatusId]) REFERENCES [LookUps].[AgencyCommunicationRequestStatus] ([Id]),
    CONSTRAINT [FK_AgencyCommunicationRequest_AgencyCommunicationRequestStatus_StatusId] FOREIGN KEY ([StatusId]) REFERENCES [LookUps].[AgencyCommunicationRequestStatus] ([Id]),
    CONSTRAINT [FK_AgencyCommunicationRequest_AgencyCommunicationRequestType_AgencyRequestTypeId] FOREIGN KEY ([AgencyRequestTypeId]) REFERENCES [LookUps].[AgencyCommunicationRequestType] ([Id]),
    CONSTRAINT [FK_AgencyCommunicationRequest_SupplierExtendOfferDatesRequest_SupplierExtendOfferDatesRequestId] FOREIGN KEY ([SupplierExtendOfferDatesRequestId]) REFERENCES [CommunicationRequest].[SupplierExtendOfferDatesRequest] ([SupplierExtendOfferDatesRequestId]),
    CONSTRAINT [FK_AgencyCommunicationRequest_Tender_TenderId] FOREIGN KEY ([TenderId]) REFERENCES [Tender].[Tender] ([TenderId]),
    CONSTRAINT [FK_AgencyCommunicationRequest_TenderPlaintRequestProcedure_TenderPlaintRequestProcedureId] FOREIGN KEY ([TenderPlaintRequestProcedureId]) REFERENCES [LookUps].[TenderPlaintRequestProcedure] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_AgencyCommunicationRequest_AgencyRequestTypeId]
    ON [CommunicationRequest].[AgencyCommunicationRequest]([AgencyRequestTypeId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_AgencyCommunicationRequest_EscalationAcceptanceStatusId]
    ON [CommunicationRequest].[AgencyCommunicationRequest]([EscalationAcceptanceStatusId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_AgencyCommunicationRequest_EscalationStatusId]
    ON [CommunicationRequest].[AgencyCommunicationRequest]([EscalationStatusId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_AgencyCommunicationRequest_PlaintAcceptanceStatusId]
    ON [CommunicationRequest].[AgencyCommunicationRequest]([PlaintAcceptanceStatusId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_AgencyCommunicationRequest_StatusId]
    ON [CommunicationRequest].[AgencyCommunicationRequest]([StatusId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_AgencyCommunicationRequest_SupplierExtendOfferDatesRequestId]
    ON [CommunicationRequest].[AgencyCommunicationRequest]([SupplierExtendOfferDatesRequestId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_AgencyCommunicationRequest_TenderId]
    ON [CommunicationRequest].[AgencyCommunicationRequest]([TenderId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_AgencyCommunicationRequest_TenderPlaintRequestProcedureId]
    ON [CommunicationRequest].[AgencyCommunicationRequest]([TenderPlaintRequestProcedureId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define a unique identifer of agency communication request', @level0type = N'SCHEMA', @level0name = N'CommunicationRequest', @level1type = N'TABLE', @level1name = N'AgencyCommunicationRequest', @level2type = N'COLUMN', @level2name = N'AgencyRequestId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define the related tender of agency communication request', @level0type = N'SCHEMA', @level0name = N'CommunicationRequest', @level1type = N'TABLE', @level1name = N'AgencyCommunicationRequest', @level2type = N'COLUMN', @level2name = N'TenderId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define the Id of agency communication request type', @level0type = N'SCHEMA', @level0name = N'CommunicationRequest', @level1type = N'TABLE', @level1name = N'AgencyCommunicationRequest', @level2type = N'COLUMN', @level2name = N'AgencyRequestTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define the rejection reason for any request', @level0type = N'SCHEMA', @level0name = N'CommunicationRequest', @level1type = N'TABLE', @level1name = N'AgencyCommunicationRequest', @level2type = N'COLUMN', @level2name = N'RejectionReason';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define the rejection reason for the esclation request', @level0type = N'SCHEMA', @level0name = N'CommunicationRequest', @level1type = N'TABLE', @level1name = N'AgencyCommunicationRequest', @level2type = N'COLUMN', @level2name = N'EscalationRejectionReason';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define if the requeste is reported or not', @level0type = N'SCHEMA', @level0name = N'CommunicationRequest', @level1type = N'TABLE', @level1name = N'AgencyCommunicationRequest', @level2type = N'COLUMN', @level2name = N'IsReported';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define the action caused by accepting plaint or esclation', @level0type = N'SCHEMA', @level0name = N'CommunicationRequest', @level1type = N'TABLE', @level1name = N'AgencyCommunicationRequest', @level2type = N'COLUMN', @level2name = N'TenderPlaintRequestProcedureId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define the status of agency communication request', @level0type = N'SCHEMA', @level0name = N'CommunicationRequest', @level1type = N'TABLE', @level1name = N'AgencyCommunicationRequest', @level2type = N'COLUMN', @level2name = N'StatusId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define the esclation status set by manager of esclation request', @level0type = N'SCHEMA', @level0name = N'CommunicationRequest', @level1type = N'TABLE', @level1name = N'AgencyCommunicationRequest', @level2type = N'COLUMN', @level2name = N'EscalationStatusId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define the esclation status set by secretary of esclation request', @level0type = N'SCHEMA', @level0name = N'CommunicationRequest', @level1type = N'TABLE', @level1name = N'AgencyCommunicationRequest', @level2type = N'COLUMN', @level2name = N'EscalationAcceptanceStatusId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define the plaint status set by secretary of plaint request', @level0type = N'SCHEMA', @level0name = N'CommunicationRequest', @level1type = N'TABLE', @level1name = N'AgencyCommunicationRequest', @level2type = N'COLUMN', @level2name = N'PlaintAcceptanceStatusId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define the role who create the agency communication request', @level0type = N'SCHEMA', @level0name = N'CommunicationRequest', @level1type = N'TABLE', @level1name = N'AgencyCommunicationRequest', @level2type = N'COLUMN', @level2name = N'RequestedByRoleName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define the Supplier extend offer dates request Id of agency communication request', @level0type = N'SCHEMA', @level0name = N'CommunicationRequest', @level1type = N'TABLE', @level1name = N'AgencyCommunicationRequest', @level2type = N'COLUMN', @level2name = N'SupplierExtendOfferDatesRequestId';

