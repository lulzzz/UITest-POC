CREATE TABLE [IDM].[UserAgencyRole] (
    [CreatedAt]        DATETIME2 (7)  NOT NULL,
    [CreatedBy]        NVARCHAR (256) NULL,
    [UpdatedAt]        DATETIME2 (7)  NULL,
    [UpdatedBy]        NVARCHAR (256) NULL,
    [IsActive]         BIT            NULL,
    [UserAgencyRoleID] INT            IDENTITY (1, 1) NOT NULL,
    [AgencyCode]       NVARCHAR (20)  NULL,
    [BranchId]         INT            NULL,
    [UserProfileId]    INT            NOT NULL,
    [RoleName]         NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_UserAgencyRole] PRIMARY KEY CLUSTERED ([UserAgencyRoleID] ASC),
    CONSTRAINT [FK_UserAgencyRole_Branch_BranchId] FOREIGN KEY ([BranchId]) REFERENCES [Branch].[Branch] ([BranchId]),
    CONSTRAINT [FK_UserAgencyRole_GovAgency_AgencyCode] FOREIGN KEY ([AgencyCode]) REFERENCES [IDM].[GovAgency] ([AgencyCode]),
    CONSTRAINT [FK_UserAgencyRole_UserProfile_UserProfileId] FOREIGN KEY ([UserProfileId]) REFERENCES [IDM].[UserProfile] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_UserAgencyRole_AgencyCode]
    ON [IDM].[UserAgencyRole]([AgencyCode] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_UserAgencyRole_BranchId]
    ON [IDM].[UserAgencyRole]([BranchId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_UserAgencyRole_UserProfileId]
    ON [IDM].[UserAgencyRole]([UserProfileId] ASC);

