CREATE TABLE [LookUps].[City] (
    [CityID]      INT            NOT NULL,
    [NameEnglish] NVARCHAR (100) NULL,
    [NameArabic]  NVARCHAR (100) NULL,
    CONSTRAINT [PK_City] PRIMARY KEY CLUSTERED ([CityID] ASC)
);

