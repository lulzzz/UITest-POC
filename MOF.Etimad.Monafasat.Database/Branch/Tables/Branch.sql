CREATE TABLE [Branch].[Branch] (
    [CreatedAt]  DATETIME2 (7)   NOT NULL,
    [CreatedBy]  NVARCHAR (256)  NULL,
    [UpdatedAt]  DATETIME2 (7)   NULL,
    [UpdatedBy]  NVARCHAR (256)  NULL,
    [IsActive]   BIT             NULL,
    [BranchId]   INT             IDENTITY (1, 1) NOT NULL,
    [BranchName] NVARCHAR (1024) NOT NULL,
    [AgencyCode] NVARCHAR (20)   NULL,
    CONSTRAINT [PK_Branch] PRIMARY KEY CLUSTERED ([BranchId] ASC),
    CONSTRAINT [FK_Branch_GovAgency_AgencyCode] FOREIGN KEY ([AgencyCode]) REFERENCES [IDM].[GovAgency] ([AgencyCode])
);


GO
CREATE NONCLUSTERED INDEX [IX_Branch_AgencyCode]
    ON [Branch].[Branch]([AgencyCode] ASC);

