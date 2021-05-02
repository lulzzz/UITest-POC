CREATE TABLE [MandatoryList].[MandatoryListProductChangeRequest] (
    [Id]                           INT            IDENTITY (1, 1) NOT NULL,
    [CreatedAt]                    DATETIME2 (7)  NOT NULL,
    [CreatedBy]                    NVARCHAR (256) NULL,
    [UpdatedAt]                    DATETIME2 (7)  NULL,
    [UpdatedBy]                    NVARCHAR (256) NULL,
    [IsActive]                     BIT            NULL,
    [CSICode]                      NVARCHAR (400) NULL,
    [NameAr]                       NVARCHAR (400) NULL,
    [NameEn]                       NVARCHAR (400) NULL,
    [DescriptionAr]                NVARCHAR (MAX) NULL,
    [DescriptionEn]                NVARCHAR (MAX) NULL,
    [PriceCelling]                 FLOAT (53)     NOT NULL,
    [MandatoryListChangeRequestId] INT            NULL,
    CONSTRAINT [PK_MandatoryListProductChangeRequest] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_MandatoryListProductChangeRequest_MandatoryListChangeRequest_MandatoryListChangeRequestId] FOREIGN KEY ([MandatoryListChangeRequestId]) REFERENCES [MandatoryList].[MandatoryListChangeRequest] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_MandatoryListProductChangeRequest_MandatoryListChangeRequestId]
    ON [MandatoryList].[MandatoryListProductChangeRequest]([MandatoryListChangeRequestId] ASC);

