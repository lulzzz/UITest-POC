CREATE TABLE [Qualification].[QualificationItem] (
    [ID]                      INT            IDENTITY (1, 1) NOT NULL,
    [SubCategoryId]           INT            NOT NULL,
    [QualificationItemTypeId] INT            NOT NULL,
    [Name]                    NVARCHAR (MAX) NULL,
    [NameEn]                  NVARCHAR (MAX) NULL,
    [IsDeleted]               BIT            NOT NULL,
    [Code]                    INT            NOT NULL,
    [IsConfigure]             BIT            NOT NULL,
    CONSTRAINT [PK_QualificationItem] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_QualificationItem_QualificationItemType_QualificationItemTypeId] FOREIGN KEY ([QualificationItemTypeId]) REFERENCES [Qualification].[QualificationItemType] ([ID]),
    CONSTRAINT [FK_QualificationItem_QualificationSubCategory_SubCategoryId] FOREIGN KEY ([SubCategoryId]) REFERENCES [Qualification].[QualificationSubCategory] ([ID])
);


GO
CREATE NONCLUSTERED INDEX [IX_QualificationItem_QualificationItemTypeId]
    ON [Qualification].[QualificationItem]([QualificationItemTypeId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_QualificationItem_SubCategoryId]
    ON [Qualification].[QualificationItem]([SubCategoryId] ASC);

