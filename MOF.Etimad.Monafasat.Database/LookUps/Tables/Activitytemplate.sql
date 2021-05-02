CREATE TABLE [LookUps].[Activitytemplate] (
    [ActivitytemplatId] INT             NOT NULL,
    [NameAr]            NVARCHAR (1024) NULL,
    [NameEn]            NVARCHAR (1024) NULL,
    [HasYears]          BIT             NULL,
    CONSTRAINT [PK_Activitytemplate] PRIMARY KEY CLUSTERED ([ActivitytemplatId] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define identity of activity template', @level0type = N'SCHEMA', @level0name = N'LookUps', @level1type = N'TABLE', @level1name = N'Activitytemplate', @level2type = N'COLUMN', @level2name = N'ActivitytemplatId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define arabic name of activity template', @level0type = N'SCHEMA', @level0name = N'LookUps', @level1type = N'TABLE', @level1name = N'Activitytemplate', @level2type = N'COLUMN', @level2name = N'NameAr';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define english name of activity template', @level0type = N'SCHEMA', @level0name = N'LookUps', @level1type = N'TABLE', @level1name = N'Activitytemplate', @level2type = N'COLUMN', @level2name = N'NameEn';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define activity template has years or not', @level0type = N'SCHEMA', @level0name = N'LookUps', @level1type = N'TABLE', @level1name = N'Activitytemplate', @level2type = N'COLUMN', @level2name = N'HasYears';

