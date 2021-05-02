CREATE TABLE [LookUps].[AgencyCommunicationRequestStatus] (
    [Id]   INT             NOT NULL,
    [Name] NVARCHAR (1024) NULL,
    CONSTRAINT [PK_AgencyCommunicationRequestStatus] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define a unique identifer of agency communication request status', @level0type = N'SCHEMA', @level0name = N'LookUps', @level1type = N'TABLE', @level1name = N'AgencyCommunicationRequestStatus', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define status name of agency communication request', @level0type = N'SCHEMA', @level0name = N'LookUps', @level1type = N'TABLE', @level1name = N'AgencyCommunicationRequestStatus', @level2type = N'COLUMN', @level2name = N'Name';

