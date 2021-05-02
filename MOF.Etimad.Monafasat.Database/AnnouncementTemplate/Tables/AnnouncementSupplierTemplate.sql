CREATE TABLE [AnnouncementTemplate].[AnnouncementSupplierTemplate] (
    [AnnouncementId]                          INT             IDENTITY (1, 1) NOT NULL,
    [CreatedAt]                               DATETIME2 (7)   NOT NULL,
    [CreatedBy]                               NVARCHAR (256)  NULL,
    [UpdatedAt]                               DATETIME2 (7)   NULL,
    [UpdatedBy]                               NVARCHAR (256)  NULL,
    [IsActive]                                BIT             NULL,
    [AnnouncementName]                        NVARCHAR (1024) NULL,
    [IntroAboutAnnouncementTemplate]          NVARCHAR (MAX)  NULL,
    [InsidKsa]                                BIT             NOT NULL,
    [Descriptions]                            NVARCHAR (MAX)  NULL,
    [ActivityDescription]                     NVARCHAR (2000) NULL,
    [PublishedDate]                           DATETIME2 (7)   NULL,
    [AgencyCode]                              NVARCHAR (20)   NULL,
    [BranchId]                                INT             NULL,
    [ApprovedBy]                              NVARCHAR (200)  NULL,
    [ReferenceNumber]                         NVARCHAR (100)  NULL,
    [IsEffectiveList]                         BIT             NULL,
    [EffectiveListDate]                       DATETIME2 (7)   NULL,
    [StatusId]                                INT             NOT NULL,
    [RequirementConditionsToJoinList]         NVARCHAR (MAX)  NULL,
    [AgencyAddress]                           NVARCHAR (MAX)  NULL,
    [AgencyPhone]                             NVARCHAR (MAX)  NULL,
    [AgencyFax]                               NVARCHAR (MAX)  NULL,
    [AgencyEmail]                             NVARCHAR (MAX)  NULL,
    [IsRequiredAttachmentToJoinList]          BIT             NOT NULL,
    [RequiredAttachment]                      NVARCHAR (MAX)  NULL,
    [AnnouncementTemplateSuppliersListTypeId] INT             NULL,
    [Details]                                 NVARCHAR (MAX)  NULL,
    [CreatedById]                             INT             NULL,
    [TenderTypesId]                           NVARCHAR (MAX)  NULL,
    [ReadyForApproval]                        INT             NULL,
    [CancelationReason]                       NVARCHAR (MAX)  NULL,
    [TemplateExtendMechanism]                 NVARCHAR (MAX)  NULL,
    CONSTRAINT [PK_AnnouncementSupplierTemplate] PRIMARY KEY CLUSTERED ([AnnouncementId] ASC),
    CONSTRAINT [FK_AnnouncementSupplierTemplate_AnnouncementStatusSupplierTemplate_StatusId] FOREIGN KEY ([StatusId]) REFERENCES [AnnouncementTemplate].[AnnouncementStatusSupplierTemplate] ([Id]),
    CONSTRAINT [FK_AnnouncementSupplierTemplate_AnnouncementTemplateListType_AnnouncementTemplateSuppliersListTypeId] FOREIGN KEY ([AnnouncementTemplateSuppliersListTypeId]) REFERENCES [LookUps].[AnnouncementTemplateListType] ([AnnouncementTemplateSuppliersListTypeId]),
    CONSTRAINT [FK_AnnouncementSupplierTemplate_Branch_BranchId] FOREIGN KEY ([BranchId]) REFERENCES [Branch].[Branch] ([BranchId]),
    CONSTRAINT [FK_AnnouncementSupplierTemplate_GovAgency_AgencyCode] FOREIGN KEY ([AgencyCode]) REFERENCES [IDM].[GovAgency] ([AgencyCode])
);


GO
CREATE NONCLUSTERED INDEX [IX_AnnouncementSupplierTemplate_AgencyCode]
    ON [AnnouncementTemplate].[AnnouncementSupplierTemplate]([AgencyCode] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_AnnouncementSupplierTemplate_BranchId]
    ON [AnnouncementTemplate].[AnnouncementSupplierTemplate]([BranchId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_AnnouncementSupplierTemplate_StatusId]
    ON [AnnouncementTemplate].[AnnouncementSupplierTemplate]([StatusId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_AnnouncementSupplierTemplate_AnnouncementTemplateSuppliersListTypeId]
    ON [AnnouncementTemplate].[AnnouncementSupplierTemplate]([AnnouncementTemplateSuppliersListTypeId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define Identity Of Announcement Supplier Template', @level0type = N'SCHEMA', @level0name = N'AnnouncementTemplate', @level1type = N'TABLE', @level1name = N'AnnouncementSupplierTemplate', @level2type = N'COLUMN', @level2name = N'AnnouncementId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define created date for announcement list', @level0type = N'SCHEMA', @level0name = N'AnnouncementTemplate', @level1type = N'TABLE', @level1name = N'AnnouncementSupplierTemplate', @level2type = N'COLUMN', @level2name = N'CreatedAt';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Determine who cretead announcement supplier template', @level0type = N'SCHEMA', @level0name = N'AnnouncementTemplate', @level1type = N'TABLE', @level1name = N'AnnouncementSupplierTemplate', @level2type = N'COLUMN', @level2name = N'CreatedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define updated date for announcement list', @level0type = N'SCHEMA', @level0name = N'AnnouncementTemplate', @level1type = N'TABLE', @level1name = N'AnnouncementSupplierTemplate', @level2type = N'COLUMN', @level2name = N'UpdatedAt';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Determine who updated announcement supplier templat', @level0type = N'SCHEMA', @level0name = N'AnnouncementTemplate', @level1type = N'TABLE', @level1name = N'AnnouncementSupplierTemplate', @level2type = N'COLUMN', @level2name = N'UpdatedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define announcement template list is active or not ', @level0type = N'SCHEMA', @level0name = N'AnnouncementTemplate', @level1type = N'TABLE', @level1name = N'AnnouncementSupplierTemplate', @level2type = N'COLUMN', @level2name = N'IsActive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define Name Of Announcement Supplier Template', @level0type = N'SCHEMA', @level0name = N'AnnouncementTemplate', @level1type = N'TABLE', @level1name = N'AnnouncementSupplierTemplate', @level2type = N'COLUMN', @level2name = N'AnnouncementName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define Intro about  Announcement Supplier Template', @level0type = N'SCHEMA', @level0name = N'AnnouncementTemplate', @level1type = N'TABLE', @level1name = N'AnnouncementSupplierTemplate', @level2type = N'COLUMN', @level2name = N'IntroAboutAnnouncementTemplate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define excution place  of  Announcement Supplier Template', @level0type = N'SCHEMA', @level0name = N'AnnouncementTemplate', @level1type = N'TABLE', @level1name = N'AnnouncementSupplierTemplate', @level2type = N'COLUMN', @level2name = N'InsidKsa';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define descriptions of  Announcement Supplier Template', @level0type = N'SCHEMA', @level0name = N'AnnouncementTemplate', @level1type = N'TABLE', @level1name = N'AnnouncementSupplierTemplate', @level2type = N'COLUMN', @level2name = N'Descriptions';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define  description of activity for announcement supplier template', @level0type = N'SCHEMA', @level0name = N'AnnouncementTemplate', @level1type = N'TABLE', @level1name = N'AnnouncementSupplierTemplate', @level2type = N'COLUMN', @level2name = N'ActivityDescription';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define published date for announcement supplier template', @level0type = N'SCHEMA', @level0name = N'AnnouncementTemplate', @level1type = N'TABLE', @level1name = N'AnnouncementSupplierTemplate', @level2type = N'COLUMN', @level2name = N'PublishedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define agency code for announcement supplier template', @level0type = N'SCHEMA', @level0name = N'AnnouncementTemplate', @level1type = N'TABLE', @level1name = N'AnnouncementSupplierTemplate', @level2type = N'COLUMN', @level2name = N'AgencyCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define branch of announcement supplier template', @level0type = N'SCHEMA', @level0name = N'AnnouncementTemplate', @level1type = N'TABLE', @level1name = N'AnnouncementSupplierTemplate', @level2type = N'COLUMN', @level2name = N'BranchId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Determine who approved announcement supplier template', @level0type = N'SCHEMA', @level0name = N'AnnouncementTemplate', @level1type = N'TABLE', @level1name = N'AnnouncementSupplierTemplate', @level2type = N'COLUMN', @level2name = N'ApprovedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define reference number of announcement supplier template, its a unique identifier like announcement identity', @level0type = N'SCHEMA', @level0name = N'AnnouncementTemplate', @level1type = N'TABLE', @level1name = N'AnnouncementSupplierTemplate', @level2type = N'COLUMN', @level2name = N'ReferenceNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define effective date of announcement list  is required or not', @level0type = N'SCHEMA', @level0name = N'AnnouncementTemplate', @level1type = N'TABLE', @level1name = N'AnnouncementSupplierTemplate', @level2type = N'COLUMN', @level2name = N'IsEffectiveList';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define Effective date for announcement list', @level0type = N'SCHEMA', @level0name = N'AnnouncementTemplate', @level1type = N'TABLE', @level1name = N'AnnouncementSupplierTemplate', @level2type = N'COLUMN', @level2name = N'EffectiveListDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define status of announcement supplier template', @level0type = N'SCHEMA', @level0name = N'AnnouncementTemplate', @level1type = N'TABLE', @level1name = N'AnnouncementSupplierTemplate', @level2type = N'COLUMN', @level2name = N'StatusId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define conditions necessary to join the list', @level0type = N'SCHEMA', @level0name = N'AnnouncementTemplate', @level1type = N'TABLE', @level1name = N'AnnouncementSupplierTemplate', @level2type = N'COLUMN', @level2name = N'RequirementConditionsToJoinList';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define agency address  for announcement supplier template', @level0type = N'SCHEMA', @level0name = N'AnnouncementTemplate', @level1type = N'TABLE', @level1name = N'AnnouncementSupplierTemplate', @level2type = N'COLUMN', @level2name = N'AgencyAddress';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define agency phone  for announcement supplier template', @level0type = N'SCHEMA', @level0name = N'AnnouncementTemplate', @level1type = N'TABLE', @level1name = N'AnnouncementSupplierTemplate', @level2type = N'COLUMN', @level2name = N'AgencyPhone';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define agency fax  for announcement supplier template', @level0type = N'SCHEMA', @level0name = N'AnnouncementTemplate', @level1type = N'TABLE', @level1name = N'AnnouncementSupplierTemplate', @level2type = N'COLUMN', @level2name = N'AgencyFax';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define agency email  for announcement supplier template', @level0type = N'SCHEMA', @level0name = N'AnnouncementTemplate', @level1type = N'TABLE', @level1name = N'AnnouncementSupplierTemplate', @level2type = N'COLUMN', @level2name = N'AgencyEmail';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define required attachment of announcement list  is required or not', @level0type = N'SCHEMA', @level0name = N'AnnouncementTemplate', @level1type = N'TABLE', @level1name = N'AnnouncementSupplierTemplate', @level2type = N'COLUMN', @level2name = N'IsRequiredAttachmentToJoinList';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define required attachment of announcement list', @level0type = N'SCHEMA', @level0name = N'AnnouncementTemplate', @level1type = N'TABLE', @level1name = N'AnnouncementSupplierTemplate', @level2type = N'COLUMN', @level2name = N'RequiredAttachment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define the  type of announcement supplier template list that public or private ', @level0type = N'SCHEMA', @level0name = N'AnnouncementTemplate', @level1type = N'TABLE', @level1name = N'AnnouncementSupplierTemplate', @level2type = N'COLUMN', @level2name = N'AnnouncementTemplateSuppliersListTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define details of  Announcement Supplier Template', @level0type = N'SCHEMA', @level0name = N'AnnouncementTemplate', @level1type = N'TABLE', @level1name = N'AnnouncementSupplierTemplate', @level2type = N'COLUMN', @level2name = N'Details';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Determine who cretead announcement supplier template', @level0type = N'SCHEMA', @level0name = N'AnnouncementTemplate', @level1type = N'TABLE', @level1name = N'AnnouncementSupplierTemplate', @level2type = N'COLUMN', @level2name = N'CreatedById';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define type of tender', @level0type = N'SCHEMA', @level0name = N'AnnouncementTemplate', @level1type = N'TABLE', @level1name = N'AnnouncementSupplierTemplate', @level2type = N'COLUMN', @level2name = N'TenderTypesId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define status for announcement list is ready for approval or not ', @level0type = N'SCHEMA', @level0name = N'AnnouncementTemplate', @level1type = N'TABLE', @level1name = N'AnnouncementSupplierTemplate', @level2type = N'COLUMN', @level2name = N'ReadyForApproval';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define cancelation reason  of announcement list', @level0type = N'SCHEMA', @level0name = N'AnnouncementTemplate', @level1type = N'TABLE', @level1name = N'AnnouncementSupplierTemplate', @level2type = N'COLUMN', @level2name = N'CancelationReason';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define mechanism to extend the list', @level0type = N'SCHEMA', @level0name = N'AnnouncementTemplate', @level1type = N'TABLE', @level1name = N'AnnouncementSupplierTemplate', @level2type = N'COLUMN', @level2name = N'TemplateExtendMechanism';

