CREATE TABLE [PrePlanning].[PrePlanningCountry] (
    [CreatedAt]     DATETIME2 (7)  NOT NULL,
    [CreatedBy]     NVARCHAR (256) NULL,
    [UpdatedAt]     DATETIME2 (7)  NULL,
    [UpdatedBy]     NVARCHAR (256) NULL,
    [IsActive]      BIT            NULL,
    [Id]            INT            IDENTITY (1, 1) NOT NULL,
    [CountryId]     INT            NOT NULL,
    [PrePlanningId] INT            NOT NULL,
    CONSTRAINT [PK_PrePlanningCountry] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PrePlanningCountry_Country_CountryId] FOREIGN KEY ([CountryId]) REFERENCES [LookUps].[Country] ([CountryId]),
    CONSTRAINT [FK_PrePlanningCountry_PrePlanning_PrePlanningId] FOREIGN KEY ([PrePlanningId]) REFERENCES [PrePlanning].[PrePlanning] ([PrePlanningId])
);


GO
CREATE NONCLUSTERED INDEX [IX_PrePlanningCountry_CountryId]
    ON [PrePlanning].[PrePlanningCountry]([CountryId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_PrePlanningCountry_PrePlanningId]
    ON [PrePlanning].[PrePlanningCountry]([PrePlanningId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define created date of pre-planning country', @level0type = N'SCHEMA', @level0name = N'PrePlanning', @level1type = N'TABLE', @level1name = N'PrePlanningCountry', @level2type = N'COLUMN', @level2name = N'CreatedAt';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Determine who cretead pre-planning country', @level0type = N'SCHEMA', @level0name = N'PrePlanning', @level1type = N'TABLE', @level1name = N'PrePlanningCountry', @level2type = N'COLUMN', @level2name = N'CreatedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define updated date of pre-planning country', @level0type = N'SCHEMA', @level0name = N'PrePlanning', @level1type = N'TABLE', @level1name = N'PrePlanningCountry', @level2type = N'COLUMN', @level2name = N'UpdatedAt';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Determine who updated pre-planning country', @level0type = N'SCHEMA', @level0name = N'PrePlanning', @level1type = N'TABLE', @level1name = N'PrePlanningCountry', @level2type = N'COLUMN', @level2name = N'UpdatedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define pre-planning country is active or not', @level0type = N'SCHEMA', @level0name = N'PrePlanning', @level1type = N'TABLE', @level1name = N'PrePlanningCountry', @level2type = N'COLUMN', @level2name = N'IsActive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'define identity of pre-planning country', @level0type = N'SCHEMA', @level0name = N'PrePlanning', @level1type = N'TABLE', @level1name = N'PrePlanningCountry', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'it''s a foreign  key that described country related to pre-planning', @level0type = N'SCHEMA', @level0name = N'PrePlanning', @level1type = N'TABLE', @level1name = N'PrePlanningCountry', @level2type = N'COLUMN', @level2name = N'CountryId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'it''s a foreign  key that described pre-planning ', @level0type = N'SCHEMA', @level0name = N'PrePlanning', @level1type = N'TABLE', @level1name = N'PrePlanningCountry', @level2type = N'COLUMN', @level2name = N'PrePlanningId';

