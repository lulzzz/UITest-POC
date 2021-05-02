CREATE TABLE [LookUps].[PrePlanningStatus] (
    [StatusId] INT            NOT NULL,
    [NameAr]   NVARCHAR (100) NULL,
    [NameEn]   NVARCHAR (100) NULL,
    CONSTRAINT [PK_PrePlanningStatus] PRIMARY KEY CLUSTERED ([StatusId] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'define identity of preplanning status', @level0type = N'SCHEMA', @level0name = N'LookUps', @level1type = N'TABLE', @level1name = N'PrePlanningStatus', @level2type = N'COLUMN', @level2name = N'StatusId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'define arabic name of preplanning status', @level0type = N'SCHEMA', @level0name = N'LookUps', @level1type = N'TABLE', @level1name = N'PrePlanningStatus', @level2type = N'COLUMN', @level2name = N'NameAr';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'define english name of preplanning status', @level0type = N'SCHEMA', @level0name = N'LookUps', @level1type = N'TABLE', @level1name = N'PrePlanningStatus', @level2type = N'COLUMN', @level2name = N'NameEn';

