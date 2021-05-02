CREATE TABLE [Committee].[CommitteeUser] (
    [CreatedAt]         DATETIME2 (7)  NOT NULL,
    [CreatedBy]         NVARCHAR (256) NULL,
    [UpdatedAt]         DATETIME2 (7)  NULL,
    [UpdatedBy]         NVARCHAR (256) NULL,
    [IsActive]          BIT            NULL,
    [CommitteeUserId]   INT            IDENTITY (1, 1) NOT NULL,
    [UserRoleId]        INT            NOT NULL,
    [UserProfileId]     INT            NOT NULL,
    [CommitteeId]       INT            NOT NULL,
    [RelatedAgencyCode] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_CommitteeUser] PRIMARY KEY CLUSTERED ([CommitteeUserId] ASC),
    CONSTRAINT [FK_CommitteeUser_Committee_CommitteeId] FOREIGN KEY ([CommitteeId]) REFERENCES [Committee].[Committee] ([CommitteeId]),
    CONSTRAINT [FK_CommitteeUser_UserProfile_UserProfileId] FOREIGN KEY ([UserProfileId]) REFERENCES [IDM].[UserProfile] ([Id]),
    CONSTRAINT [FK_CommitteeUser_UserRole_UserRoleId] FOREIGN KEY ([UserRoleId]) REFERENCES [LookUps].[UserRole] ([UserRoleId])
);


GO
CREATE NONCLUSTERED INDEX [IX_CommitteeUser_CommitteeId]
    ON [Committee].[CommitteeUser]([CommitteeId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_CommitteeUser_UserProfileId]
    ON [Committee].[CommitteeUser]([UserProfileId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_CommitteeUser_UserRoleId]
    ON [Committee].[CommitteeUser]([UserRoleId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define created date of committee user', @level0type = N'SCHEMA', @level0name = N'Committee', @level1type = N'TABLE', @level1name = N'CommitteeUser', @level2type = N'COLUMN', @level2name = N'CreatedAt';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Determine who cretead committee user', @level0type = N'SCHEMA', @level0name = N'Committee', @level1type = N'TABLE', @level1name = N'CommitteeUser', @level2type = N'COLUMN', @level2name = N'CreatedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define updated date of committee user', @level0type = N'SCHEMA', @level0name = N'Committee', @level1type = N'TABLE', @level1name = N'CommitteeUser', @level2type = N'COLUMN', @level2name = N'UpdatedAt';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Determine who updated committee user', @level0type = N'SCHEMA', @level0name = N'Committee', @level1type = N'TABLE', @level1name = N'CommitteeUser', @level2type = N'COLUMN', @level2name = N'UpdatedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define committee user is active or not', @level0type = N'SCHEMA', @level0name = N'Committee', @level1type = N'TABLE', @level1name = N'CommitteeUser', @level2type = N'COLUMN', @level2name = N'IsActive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define identity of committee user', @level0type = N'SCHEMA', @level0name = N'Committee', @level1type = N'TABLE', @level1name = N'CommitteeUser', @level2type = N'COLUMN', @level2name = N'CommitteeUserId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'it''s a forigne key that described user role', @level0type = N'SCHEMA', @level0name = N'Committee', @level1type = N'TABLE', @level1name = N'CommitteeUser', @level2type = N'COLUMN', @level2name = N'UserRoleId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'it''s a forigne key that described user profile', @level0type = N'SCHEMA', @level0name = N'Committee', @level1type = N'TABLE', @level1name = N'CommitteeUser', @level2type = N'COLUMN', @level2name = N'UserProfileId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'it''s a forigne key that described related committe', @level0type = N'SCHEMA', @level0name = N'Committee', @level1type = N'TABLE', @level1name = N'CommitteeUser', @level2type = N'COLUMN', @level2name = N'CommitteeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define identity related agencycode', @level0type = N'SCHEMA', @level0name = N'Committee', @level1type = N'TABLE', @level1name = N'CommitteeUser', @level2type = N'COLUMN', @level2name = N'RelatedAgencyCode';

