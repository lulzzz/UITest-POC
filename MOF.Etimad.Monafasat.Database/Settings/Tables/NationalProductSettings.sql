CREATE TABLE [Settings].[NationalProductSettings] (
    [Id]        INT             IDENTITY (1, 1) NOT NULL,
    [CreatedAt] DATETIME2 (7)   NOT NULL,
    [CreatedBy] NVARCHAR (256)  NULL,
    [UpdatedAt] DATETIME2 (7)   NULL,
    [UpdatedBy] NVARCHAR (256)  NULL,
    [IsActive]  BIT             NULL,
    [Rating]    DECIMAL (18, 2) NOT NULL,
    CONSTRAINT [PK_NationalProductSettings] PRIMARY KEY CLUSTERED ([Id] ASC)
);

