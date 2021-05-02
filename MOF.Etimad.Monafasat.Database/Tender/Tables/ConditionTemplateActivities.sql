CREATE TABLE [Tender].[ConditionTemplateActivities] (
    [CreatedAt]                   DATETIME2 (7)  NOT NULL,
    [CreatedBy]                   NVARCHAR (256) NULL,
    [UpdatedAt]                   DATETIME2 (7)  NULL,
    [UpdatedBy]                   NVARCHAR (256) NULL,
    [IsActive]                    BIT            NULL,
    [Id]                          INT            IDENTITY (1, 1) NOT NULL,
    [ActivityTemplateId]          INT            NOT NULL,
    [ConditionsTemplateSectionId] INT            NOT NULL,
    CONSTRAINT [PK_ConditionTemplateActivities] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ConditionTemplateActivities_Activitytemplate_ActivityTemplateId] FOREIGN KEY ([ActivityTemplateId]) REFERENCES [LookUps].[Activitytemplate] ([ActivitytemplatId]),
    CONSTRAINT [FK_ConditionTemplateActivities_ConditionsTemplateSections_ConditionsTemplateSectionId] FOREIGN KEY ([ConditionsTemplateSectionId]) REFERENCES [LookUps].[ConditionsTemplateSections] ([ConditionsTemplateSectionId])
);


GO
CREATE NONCLUSTERED INDEX [IX_ConditionTemplateActivities_ActivityTemplateId]
    ON [Tender].[ConditionTemplateActivities]([ActivityTemplateId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ConditionTemplateActivities_ConditionsTemplateSectionId]
    ON [Tender].[ConditionTemplateActivities]([ConditionsTemplateSectionId] ASC);

