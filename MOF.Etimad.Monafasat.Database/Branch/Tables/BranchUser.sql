CREATE TABLE [Branch].[BranchUser] (
    [CreatedAt]          DATETIME2 (7)   NOT NULL,
    [CreatedBy]          NVARCHAR (256)  NULL,
    [UpdatedAt]          DATETIME2 (7)   NULL,
    [UpdatedBy]          NVARCHAR (256)  NULL,
    [IsActive]           BIT             NULL,
    [BranchUserId]       INT             IDENTITY (1, 1) NOT NULL,
    [UserProfileId]      INT             NOT NULL,
    [BranchId]           INT             NOT NULL,
    [UserRoleId]         INT             NOT NULL,
    [EstimatedValueFrom] DECIMAL (18, 2) NULL,
    [EstimatedValueTo]   DECIMAL (18, 2) NULL,
    [RelatedAgencyCode]  NVARCHAR (MAX)  NULL,
    CONSTRAINT [PK_BranchUser] PRIMARY KEY CLUSTERED ([BranchUserId] ASC),
    CONSTRAINT [FK_BranchUser_Branch_BranchId] FOREIGN KEY ([BranchId]) REFERENCES [Branch].[Branch] ([BranchId]),
    CONSTRAINT [FK_BranchUser_UserProfile_UserProfileId] FOREIGN KEY ([UserProfileId]) REFERENCES [IDM].[UserProfile] ([Id]),
    CONSTRAINT [FK_BranchUser_UserRole_UserRoleId] FOREIGN KEY ([UserRoleId]) REFERENCES [LookUps].[UserRole] ([UserRoleId])
);


GO
CREATE NONCLUSTERED INDEX [IX_BranchUser_BranchId]
    ON [Branch].[BranchUser]([BranchId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_BranchUser_UserProfileId]
    ON [Branch].[BranchUser]([UserProfileId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_BranchUser_UserRoleId]
    ON [Branch].[BranchUser]([UserRoleId] ASC);

