CREATE TABLE [Tender].[TenderUnitAssign] (
    [CreatedAt]           DATETIME2 (7)  NOT NULL,
    [CreatedBy]           NVARCHAR (256) NULL,
    [UpdatedAt]           DATETIME2 (7)  NULL,
    [UpdatedBy]           NVARCHAR (256) NULL,
    [IsActive]            BIT            NULL,
    [TenderUnitAssignId]  INT            IDENTITY (1, 1) NOT NULL,
    [UserProfileId]       INT            NOT NULL,
    [TenderId]            INT            NOT NULL,
    [IsCurrentlyAssigned] BIT            NOT NULL,
    [UnitSpecialistLevel] INT            NOT NULL,
    CONSTRAINT [PK_TenderUnitAssign] PRIMARY KEY CLUSTERED ([TenderUnitAssignId] ASC),
    CONSTRAINT [FK_TenderUnitAssign_Tender_TenderId] FOREIGN KEY ([TenderId]) REFERENCES [Tender].[Tender] ([TenderId]),
    CONSTRAINT [FK_TenderUnitAssign_UserProfile_UserProfileId] FOREIGN KEY ([UserProfileId]) REFERENCES [IDM].[UserProfile] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_TenderUnitAssign_TenderId]
    ON [Tender].[TenderUnitAssign]([TenderId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_TenderUnitAssign_UserProfileId]
    ON [Tender].[TenderUnitAssign]([UserProfileId] ASC);

