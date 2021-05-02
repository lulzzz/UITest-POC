CREATE TABLE [LookUps].[AgencyCommunicationRequestType] (
    [Id]   INT             NOT NULL,
    [Name] NVARCHAR (1024) NULL,
    CONSTRAINT [PK_AgencyCommunicationRequestType] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define a unique identifer of agency communication request type', @level0type = N'SCHEMA', @level0name = N'LookUps', @level1type = N'TABLE', @level1name = N'AgencyCommunicationRequestType', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define type name of agency communication request', @level0type = N'SCHEMA', @level0name = N'LookUps', @level1type = N'TABLE', @level1name = N'AgencyCommunicationRequestType', @level2type = N'COLUMN', @level2name = N'Name';

