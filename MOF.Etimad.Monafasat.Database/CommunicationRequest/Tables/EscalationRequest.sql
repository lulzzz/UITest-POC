CREATE TABLE [CommunicationRequest].[EscalationRequest] (
    [CreatedAt]           DATETIME2 (7)   NOT NULL,
    [CreatedBy]           NVARCHAR (256)  NULL,
    [UpdatedAt]           DATETIME2 (7)   NULL,
    [UpdatedBy]           NVARCHAR (256)  NULL,
    [IsActive]            BIT             NULL,
    [EscalationRequestId] INT             IDENTITY (1, 1) NOT NULL,
    [PlaintRequestId]     INT             NOT NULL,
    [EscalationNotes]     NVARCHAR (1000) NULL,
    CONSTRAINT [PK_EscalationRequest] PRIMARY KEY CLUSTERED ([EscalationRequestId] ASC),
    CONSTRAINT [FK_EscalationRequest_PlaintRequest_PlaintRequestId] FOREIGN KEY ([PlaintRequestId]) REFERENCES [CommunicationRequest].[PlaintRequest] ([PlainRequestId])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_EscalationRequest_PlaintRequestId]
    ON [CommunicationRequest].[EscalationRequest]([PlaintRequestId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define a unique identifer of escalation request', @level0type = N'SCHEMA', @level0name = N'CommunicationRequest', @level1type = N'TABLE', @level1name = N'EscalationRequest', @level2type = N'COLUMN', @level2name = N'EscalationRequestId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define the related plaint request for the escalation request', @level0type = N'SCHEMA', @level0name = N'CommunicationRequest', @level1type = N'TABLE', @level1name = N'EscalationRequest', @level2type = N'COLUMN', @level2name = N'PlaintRequestId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define notes for more details about the escalation request', @level0type = N'SCHEMA', @level0name = N'CommunicationRequest', @level1type = N'TABLE', @level1name = N'EscalationRequest', @level2type = N'COLUMN', @level2name = N'EscalationNotes';

