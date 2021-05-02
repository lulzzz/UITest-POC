CREATE TABLE [PrePlanning].[PrePlanningArea] (
    [CreatedAt]     DATETIME2 (7)  NOT NULL,
    [CreatedBy]     NVARCHAR (256) NULL,
    [UpdatedAt]     DATETIME2 (7)  NULL,
    [UpdatedBy]     NVARCHAR (256) NULL,
    [IsActive]      BIT            NULL,
    [Id]            INT            IDENTITY (1, 1) NOT NULL,
    [AreaId]        INT            NOT NULL,
    [PrePlanningId] INT            NOT NULL,
    CONSTRAINT [PK_PrePlanningArea] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PrePlanningArea_Area_AreaId] FOREIGN KEY ([AreaId]) REFERENCES [LookUps].[Area] ([AreaId]),
    CONSTRAINT [FK_PrePlanningArea_PrePlanning_PrePlanningId] FOREIGN KEY ([PrePlanningId]) REFERENCES [PrePlanning].[PrePlanning] ([PrePlanningId])
);


GO
CREATE NONCLUSTERED INDEX [IX_PrePlanningArea_AreaId]
    ON [PrePlanning].[PrePlanningArea]([AreaId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_PrePlanningArea_PrePlanningId]
    ON [PrePlanning].[PrePlanningArea]([PrePlanningId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define created date of pre-planning area', @level0type = N'SCHEMA', @level0name = N'PrePlanning', @level1type = N'TABLE', @level1name = N'PrePlanningArea', @level2type = N'COLUMN', @level2name = N'CreatedAt';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Determine who cretead pre-planning area', @level0type = N'SCHEMA', @level0name = N'PrePlanning', @level1type = N'TABLE', @level1name = N'PrePlanningArea', @level2type = N'COLUMN', @level2name = N'CreatedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define updated date of pre-planning area', @level0type = N'SCHEMA', @level0name = N'PrePlanning', @level1type = N'TABLE', @level1name = N'PrePlanningArea', @level2type = N'COLUMN', @level2name = N'UpdatedAt';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Determine who updated pre-planning area', @level0type = N'SCHEMA', @level0name = N'PrePlanning', @level1type = N'TABLE', @level1name = N'PrePlanningArea', @level2type = N'COLUMN', @level2name = N'UpdatedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define pre-planning area is active or not', @level0type = N'SCHEMA', @level0name = N'PrePlanning', @level1type = N'TABLE', @level1name = N'PrePlanningArea', @level2type = N'COLUMN', @level2name = N'IsActive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'define identity of pre-planning area', @level0type = N'SCHEMA', @level0name = N'PrePlanning', @level1type = N'TABLE', @level1name = N'PrePlanningArea', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'it''s a foreign  key that described area related to pre-planning', @level0type = N'SCHEMA', @level0name = N'PrePlanning', @level1type = N'TABLE', @level1name = N'PrePlanningArea', @level2type = N'COLUMN', @level2name = N'AreaId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'it''s a foreign  key that described  pre-planning ', @level0type = N'SCHEMA', @level0name = N'PrePlanning', @level1type = N'TABLE', @level1name = N'PrePlanningArea', @level2type = N'COLUMN', @level2name = N'PrePlanningId';

