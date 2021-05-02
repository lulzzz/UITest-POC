CREATE TABLE [MandatoryList].[MandatoryList] (
    [CreatedAt]       DATETIME2 (7)  NOT NULL,
    [CreatedBy]       NVARCHAR (256) NULL,
    [UpdatedAt]       DATETIME2 (7)  NULL,
    [UpdatedBy]       NVARCHAR (256) NULL,
    [IsActive]        BIT            NULL,
    [Id]              INT            IDENTITY (1, 1) NOT NULL,
    [DivisionNameAr]  NVARCHAR (400) NULL,
    [DivisionNameEn]  NVARCHAR (400) NULL,
    [DivisionCode]    NVARCHAR (400) NULL,
    [StatusId]        INT            NOT NULL,
    [RejectionReason] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_MandatoryList] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_MandatoryList_MandatoryListStatus_StatusId] FOREIGN KEY ([StatusId]) REFERENCES [LookUps].[MandatoryListStatus] ([StatusId])
);


GO
CREATE NONCLUSTERED INDEX [IX_MandatoryList_StatusId]
    ON [MandatoryList].[MandatoryList]([StatusId] ASC);

