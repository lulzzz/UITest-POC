CREATE TABLE [LookUps].[ConditionsTemplateSections] (
    [ConditionsTemplateSectionId] INT             NOT NULL,
    [NameAr]                      NVARCHAR (1024) NULL,
    [NameEn]                      NVARCHAR (1024) NULL,
    CONSTRAINT [PK_ConditionsTemplateSections] PRIMARY KEY CLUSTERED ([ConditionsTemplateSectionId] ASC)
);

