CREATE TABLE [LookUps].[Activity] (
    [ActivityId]         INT             NOT NULL,
    [NameAr]             NVARCHAR (1024) NULL,
    [NameEn]             NVARCHAR (1024) NULL,
    [ParentID]           INT             NULL,
    [ActivitytemplateID] INT             NULL,
    CONSTRAINT [PK_Activity] PRIMARY KEY CLUSTERED ([ActivityId] ASC),
    CONSTRAINT [FK_Activity_Activity_ParentID] FOREIGN KEY ([ParentID]) REFERENCES [LookUps].[Activity] ([ActivityId]),
    CONSTRAINT [FK_Activity_Activitytemplate_ActivitytemplateID] FOREIGN KEY ([ActivitytemplateID]) REFERENCES [LookUps].[Activitytemplate] ([ActivitytemplatId])
);


GO
CREATE NONCLUSTERED INDEX [IX_Activity_ActivitytemplateID]
    ON [LookUps].[Activity]([ActivitytemplateID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Activity_ParentID]
    ON [LookUps].[Activity]([ParentID] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'define identity of activity', @level0type = N'SCHEMA', @level0name = N'LookUps', @level1type = N'TABLE', @level1name = N'Activity', @level2type = N'COLUMN', @level2name = N'ActivityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'define arabic name of activity', @level0type = N'SCHEMA', @level0name = N'LookUps', @level1type = N'TABLE', @level1name = N'Activity', @level2type = N'COLUMN', @level2name = N'NameAr';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'define english name of activity', @level0type = N'SCHEMA', @level0name = N'LookUps', @level1type = N'TABLE', @level1name = N'Activity', @level2type = N'COLUMN', @level2name = N'NameEn';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'define parent id of activity', @level0type = N'SCHEMA', @level0name = N'LookUps', @level1type = N'TABLE', @level1name = N'Activity', @level2type = N'COLUMN', @level2name = N'ParentID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'it''s a foreign  key that described activity template of activity', @level0type = N'SCHEMA', @level0name = N'LookUps', @level1type = N'TABLE', @level1name = N'Activity', @level2type = N'COLUMN', @level2name = N'ActivitytemplateID';

