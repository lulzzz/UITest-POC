CREATE TABLE [Branch].[BranchCommittee] (
    [CreatedAt]         DATETIME2 (7)  NOT NULL,
    [CreatedBy]         NVARCHAR (256) NULL,
    [UpdatedAt]         DATETIME2 (7)  NULL,
    [UpdatedBy]         NVARCHAR (256) NULL,
    [IsActive]          BIT            NULL,
    [BranchCommitteeId] INT            IDENTITY (1, 1) NOT NULL,
    [BranchId]          INT            NOT NULL,
    [CommitteeId]       INT            NOT NULL,
    CONSTRAINT [PK_BranchCommittee] PRIMARY KEY CLUSTERED ([BranchCommitteeId] ASC),
    CONSTRAINT [FK_BranchCommittee_Branch_BranchId] FOREIGN KEY ([BranchId]) REFERENCES [Branch].[Branch] ([BranchId]),
    CONSTRAINT [FK_BranchCommittee_Committee_CommitteeId] FOREIGN KEY ([CommitteeId]) REFERENCES [Committee].[Committee] ([CommitteeId])
);


GO
CREATE NONCLUSTERED INDEX [IX_BranchCommittee_BranchId]
    ON [Branch].[BranchCommittee]([BranchId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_BranchCommittee_CommitteeId]
    ON [Branch].[BranchCommittee]([CommitteeId] ASC);

