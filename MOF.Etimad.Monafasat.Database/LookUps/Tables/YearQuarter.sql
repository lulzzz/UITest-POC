CREATE TABLE [LookUps].[YearQuarter] (
    [YearQuarterId] INT            NOT NULL,
    [NameAr]        NVARCHAR (100) NULL,
    [NameEn]        NVARCHAR (100) NULL,
    CONSTRAINT [PK_YearQuarter] PRIMARY KEY CLUSTERED ([YearQuarterId] ASC)
);

