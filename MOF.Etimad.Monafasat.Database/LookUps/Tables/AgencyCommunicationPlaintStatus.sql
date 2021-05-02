CREATE TABLE [LookUps].[AgencyCommunicationPlaintStatus] (
    [Id]   INT             NOT NULL,
    [Name] NVARCHAR (1024) NULL,
    CONSTRAINT [PK_AgencyCommunicationPlaintStatus] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define a unique identifer of plaint request status', @level0type = N'SCHEMA', @level0name = N'LookUps', @level1type = N'TABLE', @level1name = N'AgencyCommunicationPlaintStatus', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define the plaint status name of plaint request', @level0type = N'SCHEMA', @level0name = N'LookUps', @level1type = N'TABLE', @level1name = N'AgencyCommunicationPlaintStatus', @level2type = N'COLUMN', @level2name = N'Name';

