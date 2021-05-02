CREATE TABLE [LookUps].[Area] (
    [AreaId] INT             NOT NULL,
    [NameEn] NVARCHAR (1024) NULL,
    [NameAr] NVARCHAR (1024) NULL,
    CONSTRAINT [PK_Area] PRIMARY KEY CLUSTERED ([AreaId] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define identity of area', @level0type = N'SCHEMA', @level0name = N'LookUps', @level1type = N'TABLE', @level1name = N'Area', @level2type = N'COLUMN', @level2name = N'AreaId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define english name of Area', @level0type = N'SCHEMA', @level0name = N'LookUps', @level1type = N'TABLE', @level1name = N'Area', @level2type = N'COLUMN', @level2name = N'NameEn';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define arabic name of Area', @level0type = N'SCHEMA', @level0name = N'LookUps', @level1type = N'TABLE', @level1name = N'Area', @level2type = N'COLUMN', @level2name = N'NameAr';

