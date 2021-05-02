CREATE TABLE [MandatoryList].[MandatoryListProduct] (
    [Id]              INT            IDENTITY (1, 1) NOT NULL,
    [CSICode]         NVARCHAR (400) NULL,
    [NameAr]          NVARCHAR (400) NULL,
    [NameEn]          NVARCHAR (400) NULL,
    [DescriptionAr]   NVARCHAR (MAX) NULL,
    [DescriptionEn]   NVARCHAR (MAX) NULL,
    [PriceCelling]    FLOAT (53)     NOT NULL,
    [MandatoryListId] INT            NULL,
    [CreatedAt]       DATETIME2 (7)  DEFAULT ('0001-01-01T00:00:00.0000000') NOT NULL,
    [CreatedBy]       NVARCHAR (256) NULL,
    [IsActive]        BIT            NULL,
    [UpdatedAt]       DATETIME2 (7)  NULL,
    [UpdatedBy]       NVARCHAR (256) NULL,
    CONSTRAINT [PK_MandatoryListProduct] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_MandatoryListProduct_MandatoryList_MandatoryListId] FOREIGN KEY ([MandatoryListId]) REFERENCES [MandatoryList].[MandatoryList] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_MandatoryListProduct_MandatoryListId]
    ON [MandatoryList].[MandatoryListProduct]([MandatoryListId] ASC);

