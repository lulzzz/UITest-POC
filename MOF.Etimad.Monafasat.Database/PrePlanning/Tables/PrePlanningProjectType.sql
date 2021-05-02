CREATE TABLE [PrePlanning].[PrePlanningProjectType] (
    [CreatedAt]     DATETIME2 (7)  NOT NULL,
    [CreatedBy]     NVARCHAR (256) NULL,
    [UpdatedAt]     DATETIME2 (7)  NULL,
    [UpdatedBy]     NVARCHAR (256) NULL,
    [IsActive]      BIT            NULL,
    [Id]            INT            IDENTITY (1, 1) NOT NULL,
    [ActivityId]    INT            NOT NULL,
    [PrePlanningId] INT            NOT NULL,
    CONSTRAINT [PK_PrePlanningProjectType] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PrePlanningProjectType_Activity_ActivityId] FOREIGN KEY ([ActivityId]) REFERENCES [LookUps].[Activity] ([ActivityId]),
    CONSTRAINT [FK_PrePlanningProjectType_PrePlanning_PrePlanningId] FOREIGN KEY ([PrePlanningId]) REFERENCES [PrePlanning].[PrePlanning] ([PrePlanningId])
);


GO
CREATE NONCLUSTERED INDEX [IX_PrePlanningProjectType_ActivityId]
    ON [PrePlanning].[PrePlanningProjectType]([ActivityId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_PrePlanningProjectType_PrePlanningId]
    ON [PrePlanning].[PrePlanningProjectType]([PrePlanningId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define created date of pre-planning project type', @level0type = N'SCHEMA', @level0name = N'PrePlanning', @level1type = N'TABLE', @level1name = N'PrePlanningProjectType', @level2type = N'COLUMN', @level2name = N'CreatedAt';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Determine who cretead pre-planning project type', @level0type = N'SCHEMA', @level0name = N'PrePlanning', @level1type = N'TABLE', @level1name = N'PrePlanningProjectType', @level2type = N'COLUMN', @level2name = N'CreatedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define updated date of pre-planning project type', @level0type = N'SCHEMA', @level0name = N'PrePlanning', @level1type = N'TABLE', @level1name = N'PrePlanningProjectType', @level2type = N'COLUMN', @level2name = N'UpdatedAt';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Determine who updated pre-planning project type', @level0type = N'SCHEMA', @level0name = N'PrePlanning', @level1type = N'TABLE', @level1name = N'PrePlanningProjectType', @level2type = N'COLUMN', @level2name = N'UpdatedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define pre-planning project type is active or not', @level0type = N'SCHEMA', @level0name = N'PrePlanning', @level1type = N'TABLE', @level1name = N'PrePlanningProjectType', @level2type = N'COLUMN', @level2name = N'IsActive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'define identity of pre-planning project type', @level0type = N'SCHEMA', @level0name = N'PrePlanning', @level1type = N'TABLE', @level1name = N'PrePlanningProjectType', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'it''s a foreign  key that described activity of pre-planning project type', @level0type = N'SCHEMA', @level0name = N'PrePlanning', @level1type = N'TABLE', @level1name = N'PrePlanningProjectType', @level2type = N'COLUMN', @level2name = N'ActivityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'it''s a foreign  key that described pre-planning ', @level0type = N'SCHEMA', @level0name = N'PrePlanning', @level1type = N'TABLE', @level1name = N'PrePlanningProjectType', @level2type = N'COLUMN', @level2name = N'PrePlanningId';

