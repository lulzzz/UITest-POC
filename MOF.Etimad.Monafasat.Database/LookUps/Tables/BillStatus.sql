CREATE TABLE [LookUps].[BillStatus] (
    [BillStatusId]     INT             NOT NULL,
    [BillStatusNameEn] NVARCHAR (1024) NULL,
    [BillStatusNameAr] NVARCHAR (1024) NULL,
    CONSTRAINT [PK_BillStatus] PRIMARY KEY CLUSTERED ([BillStatusId] ASC)
);

