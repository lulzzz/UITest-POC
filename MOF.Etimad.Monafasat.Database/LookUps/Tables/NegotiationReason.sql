CREATE TABLE [LookUps].[NegotiationReason] (
    [NegotiationReasonId] INT            NOT NULL,
    [Name]                NVARCHAR (100) NULL,
    CONSTRAINT [PK_NegotiationReason] PRIMARY KEY CLUSTERED ([NegotiationReasonId] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Describe the negotiation reason', @level0type = N'SCHEMA', @level0name = N'LookUps', @level1type = N'TABLE', @level1name = N'NegotiationReason';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define the unique  identifier for negotiation reason', @level0type = N'SCHEMA', @level0name = N'LookUps', @level1type = N'TABLE', @level1name = N'NegotiationReason', @level2type = N'COLUMN', @level2name = N'NegotiationReasonId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define The name of Negotiation reason Name', @level0type = N'SCHEMA', @level0name = N'LookUps', @level1type = N'TABLE', @level1name = N'NegotiationReason', @level2type = N'COLUMN', @level2name = N'Name';

