CREATE TABLE [LookUps].[ConstructionWork] (
    [ConstructionWorkId] INT             NOT NULL,
    [NameAr]             NVARCHAR (1024) NOT NULL,
    [NameEn]             NVARCHAR (1024) NULL,
    [ParentID]           INT             NULL,
    CONSTRAINT [PK_ConstructionWork] PRIMARY KEY CLUSTERED ([ConstructionWorkId] ASC),
    CONSTRAINT [FK_ConstructionWork_ConstructionWork_ParentID] FOREIGN KEY ([ParentID]) REFERENCES [LookUps].[ConstructionWork] ([ConstructionWorkId])
);


GO
CREATE NONCLUSTERED INDEX [IX_ConstructionWork_ParentID]
    ON [LookUps].[ConstructionWork]([ParentID] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'define identity of construction work', @level0type = N'SCHEMA', @level0name = N'LookUps', @level1type = N'TABLE', @level1name = N'ConstructionWork', @level2type = N'COLUMN', @level2name = N'ConstructionWorkId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'define arabic name of  construction work', @level0type = N'SCHEMA', @level0name = N'LookUps', @level1type = N'TABLE', @level1name = N'ConstructionWork', @level2type = N'COLUMN', @level2name = N'NameAr';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'define english name of  construction work', @level0type = N'SCHEMA', @level0name = N'LookUps', @level1type = N'TABLE', @level1name = N'ConstructionWork', @level2type = N'COLUMN', @level2name = N'NameEn';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'define parent id of construction work', @level0type = N'SCHEMA', @level0name = N'LookUps', @level1type = N'TABLE', @level1name = N'ConstructionWork', @level2type = N'COLUMN', @level2name = N'ParentID';

