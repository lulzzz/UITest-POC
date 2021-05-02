CREATE TABLE [LookUps].[NegotiationType] (
    [NegotiationTypeId] INT            NOT NULL,
    [Name]              NVARCHAR (100) NULL,
    CONSTRAINT [PK_NegotiationType] PRIMARY KEY CLUSTERED ([NegotiationTypeId] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Describe the negotiation types', @level0type = N'SCHEMA', @level0name = N'LookUps', @level1type = N'TABLE', @level1name = N'NegotiationType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define the unique  identifier for negotiation types', @level0type = N'SCHEMA', @level0name = N'LookUps', @level1type = N'TABLE', @level1name = N'NegotiationType', @level2type = N'COLUMN', @level2name = N'NegotiationTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define The name of Negotiation Type Name', @level0type = N'SCHEMA', @level0name = N'LookUps', @level1type = N'TABLE', @level1name = N'NegotiationType', @level2type = N'COLUMN', @level2name = N'Name';

