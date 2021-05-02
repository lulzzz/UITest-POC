CREATE TABLE [PrePlanning].[PrePlanning] (
    [CreatedAt]             DATETIME2 (7)  NOT NULL,
    [CreatedBy]             NVARCHAR (256) NULL,
    [UpdatedAt]             DATETIME2 (7)  NULL,
    [UpdatedBy]             NVARCHAR (256) NULL,
    [IsActive]              BIT            NULL,
    [PrePlanningId]         INT            IDENTITY (1, 1) NOT NULL,
    [AgencyCode]            NVARCHAR (20)  NULL,
    [BranchId]              INT            NOT NULL,
    [InsideKSA]             BIT            NULL,
    [StatusId]              INT            NULL,
    [ProjectName]           NVARCHAR (200) NULL,
    [ProjectNature]         NVARCHAR (500) NULL,
    [ProjectDescription]    NVARCHAR (500) NULL,
    [RejectionReason]       NVARCHAR (500) NULL,
    [Duration]              NVARCHAR (100) NULL,
    [DurationInDays]        INT            NULL,
    [DurationInMonths]      INT            NULL,
    [DurationInYears]       INT            NULL,
    [DeleteRejectionReason] NVARCHAR (500) NULL,
    [IsDeleteRequested]     BIT            NOT NULL,
    [YearQuarterId]         INT            NOT NULL,
    CONSTRAINT [PK_PrePlanning] PRIMARY KEY CLUSTERED ([PrePlanningId] ASC),
    CONSTRAINT [FK_PrePlanning_GovAgency_AgencyCode] FOREIGN KEY ([AgencyCode]) REFERENCES [IDM].[GovAgency] ([AgencyCode]),
    CONSTRAINT [FK_PrePlanning_PrePlanningStatus_StatusId] FOREIGN KEY ([StatusId]) REFERENCES [LookUps].[PrePlanningStatus] ([StatusId]),
    CONSTRAINT [FK_PrePlanning_YearQuarter_YearQuarterId] FOREIGN KEY ([YearQuarterId]) REFERENCES [LookUps].[YearQuarter] ([YearQuarterId])
);


GO
CREATE NONCLUSTERED INDEX [IX_PrePlanning_AgencyCode]
    ON [PrePlanning].[PrePlanning]([AgencyCode] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_PrePlanning_StatusId]
    ON [PrePlanning].[PrePlanning]([StatusId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_PrePlanning_YearQuarterId]
    ON [PrePlanning].[PrePlanning]([YearQuarterId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define created date of pre-planning', @level0type = N'SCHEMA', @level0name = N'PrePlanning', @level1type = N'TABLE', @level1name = N'PrePlanning', @level2type = N'COLUMN', @level2name = N'CreatedAt';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Determine who cretead pre-planning', @level0type = N'SCHEMA', @level0name = N'PrePlanning', @level1type = N'TABLE', @level1name = N'PrePlanning', @level2type = N'COLUMN', @level2name = N'CreatedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define updated date of pre-planning', @level0type = N'SCHEMA', @level0name = N'PrePlanning', @level1type = N'TABLE', @level1name = N'PrePlanning', @level2type = N'COLUMN', @level2name = N'UpdatedAt';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Determine who updated pre-planning', @level0type = N'SCHEMA', @level0name = N'PrePlanning', @level1type = N'TABLE', @level1name = N'PrePlanning', @level2type = N'COLUMN', @level2name = N'UpdatedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define pre-planning is active or not', @level0type = N'SCHEMA', @level0name = N'PrePlanning', @level1type = N'TABLE', @level1name = N'PrePlanning', @level2type = N'COLUMN', @level2name = N'IsActive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'define identity of pre-planning', @level0type = N'SCHEMA', @level0name = N'PrePlanning', @level1type = N'TABLE', @level1name = N'PrePlanning', @level2type = N'COLUMN', @level2name = N'PrePlanningId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'describe of the pre-planning government agency', @level0type = N'SCHEMA', @level0name = N'PrePlanning', @level1type = N'TABLE', @level1name = N'PrePlanning', @level2type = N'COLUMN', @level2name = N'AgencyCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'it''s a foreign  key that describe of the pre-planning branch', @level0type = N'SCHEMA', @level0name = N'PrePlanning', @level1type = N'TABLE', @level1name = N'PrePlanning', @level2type = N'COLUMN', @level2name = N'BranchId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'define pre-planning project is inside ksa or not', @level0type = N'SCHEMA', @level0name = N'PrePlanning', @level1type = N'TABLE', @level1name = N'PrePlanning', @level2type = N'COLUMN', @level2name = N'InsideKSA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'define pre-planning status', @level0type = N'SCHEMA', @level0name = N'PrePlanning', @level1type = N'TABLE', @level1name = N'PrePlanning', @level2type = N'COLUMN', @level2name = N'StatusId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'define project name of pre-planning', @level0type = N'SCHEMA', @level0name = N'PrePlanning', @level1type = N'TABLE', @level1name = N'PrePlanning', @level2type = N'COLUMN', @level2name = N'ProjectName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'define project nature of pre-planning', @level0type = N'SCHEMA', @level0name = N'PrePlanning', @level1type = N'TABLE', @level1name = N'PrePlanning', @level2type = N'COLUMN', @level2name = N'ProjectNature';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'define project description of pre-planning', @level0type = N'SCHEMA', @level0name = N'PrePlanning', @level1type = N'TABLE', @level1name = N'PrePlanning', @level2type = N'COLUMN', @level2name = N'ProjectDescription';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'define reject reason of pre-planning', @level0type = N'SCHEMA', @level0name = N'PrePlanning', @level1type = N'TABLE', @level1name = N'PrePlanning', @level2type = N'COLUMN', @level2name = N'RejectionReason';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'define pre-planning duration', @level0type = N'SCHEMA', @level0name = N'PrePlanning', @level1type = N'TABLE', @level1name = N'PrePlanning', @level2type = N'COLUMN', @level2name = N'Duration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'define pre-planning duration in days', @level0type = N'SCHEMA', @level0name = N'PrePlanning', @level1type = N'TABLE', @level1name = N'PrePlanning', @level2type = N'COLUMN', @level2name = N'DurationInDays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'define pre-planning duration in month', @level0type = N'SCHEMA', @level0name = N'PrePlanning', @level1type = N'TABLE', @level1name = N'PrePlanning', @level2type = N'COLUMN', @level2name = N'DurationInMonths';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'define pre-planning duration in years', @level0type = N'SCHEMA', @level0name = N'PrePlanning', @level1type = N'TABLE', @level1name = N'PrePlanning', @level2type = N'COLUMN', @level2name = N'DurationInYears';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'describe of the reason for denying the deletion request of pre-planning', @level0type = N'SCHEMA', @level0name = N'PrePlanning', @level1type = N'TABLE', @level1name = N'PrePlanning', @level2type = N'COLUMN', @level2name = N'DeleteRejectionReason';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'define pre-planning has deleted request or not', @level0type = N'SCHEMA', @level0name = N'PrePlanning', @level1type = N'TABLE', @level1name = N'PrePlanning', @level2type = N'COLUMN', @level2name = N'IsDeleteRequested';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'define pre-planning year quarter', @level0type = N'SCHEMA', @level0name = N'PrePlanning', @level1type = N'TABLE', @level1name = N'PrePlanning', @level2type = N'COLUMN', @level2name = N'YearQuarterId';

