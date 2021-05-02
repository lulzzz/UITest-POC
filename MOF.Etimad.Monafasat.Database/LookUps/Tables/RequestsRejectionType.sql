CREATE TABLE [LookUps].[RequestsRejectionType] (
    [RequestTypeId] INT            NOT NULL,
    [NameAr]        NVARCHAR (500) NULL,
    [NameEn]        NVARCHAR (500) NULL,
    CONSTRAINT [PK_RequestsRejectionType] PRIMARY KEY CLUSTERED ([RequestTypeId] ASC)
);

