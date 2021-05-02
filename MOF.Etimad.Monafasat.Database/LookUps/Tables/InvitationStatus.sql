CREATE TABLE [LookUps].[InvitationStatus] (
    [InvitationStatusId] INT            NOT NULL,
    [NameAr]             NVARCHAR (100) NULL,
    [NameEn]             NVARCHAR (100) NULL,
    CONSTRAINT [PK_InvitationStatus] PRIMARY KEY CLUSTERED ([InvitationStatusId] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'define identity of invitation status', @level0type = N'SCHEMA', @level0name = N'LookUps', @level1type = N'TABLE', @level1name = N'InvitationStatus', @level2type = N'COLUMN', @level2name = N'InvitationStatusId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'define arabic name of invitation status', @level0type = N'SCHEMA', @level0name = N'LookUps', @level1type = N'TABLE', @level1name = N'InvitationStatus', @level2type = N'COLUMN', @level2name = N'NameAr';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'define english name of invitation status', @level0type = N'SCHEMA', @level0name = N'LookUps', @level1type = N'TABLE', @level1name = N'InvitationStatus', @level2type = N'COLUMN', @level2name = N'NameEn';

