CREATE TABLE [LookUps].[AgreementType] (
    [AgreementTypeId] INT            NOT NULL,
    [NameAr]          NVARCHAR (500) NULL,
    [NameEn]          NVARCHAR (500) NULL,
    CONSTRAINT [PK_AgreementType] PRIMARY KEY CLUSTERED ([AgreementTypeId] ASC)
);

