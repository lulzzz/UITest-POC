CREATE TABLE [Committee].[Committee] (
    [CreatedAt]       DATETIME2 (7)   NOT NULL,
    [CreatedBy]       NVARCHAR (256)  NULL,
    [UpdatedAt]       DATETIME2 (7)   NULL,
    [UpdatedBy]       NVARCHAR (256)  NULL,
    [IsActive]        BIT             NULL,
    [CommitteeId]     INT             IDENTITY (1, 1) NOT NULL,
    [AgencyCode]      NVARCHAR (20)   NULL,
    [CommitteeTypeId] INT             NOT NULL,
    [CommitteeName]   NVARCHAR (1024) NULL,
    [Address]         NVARCHAR (1024) NULL,
    [Phone]           NVARCHAR (100)  NULL,
    [Fax]             NVARCHAR (100)  NULL,
    [Email]           NVARCHAR (1024) NULL,
    [PostalCode]      NVARCHAR (1024) NULL,
    [ZipCode]         NVARCHAR (1024) NULL,
    CONSTRAINT [PK_Committee] PRIMARY KEY CLUSTERED ([CommitteeId] ASC),
    CONSTRAINT [FK_Committee_CommitteeType_CommitteeTypeId] FOREIGN KEY ([CommitteeTypeId]) REFERENCES [LookUps].[CommitteeType] ([CommitteeTypeId]),
    CONSTRAINT [FK_Committee_GovAgency_AgencyCode] FOREIGN KEY ([AgencyCode]) REFERENCES [IDM].[GovAgency] ([AgencyCode])
);


GO
CREATE NONCLUSTERED INDEX [IX_Committee_AgencyCode]
    ON [Committee].[Committee]([AgencyCode] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Committee_CommitteeTypeId]
    ON [Committee].[Committee]([CommitteeTypeId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define created date of committee', @level0type = N'SCHEMA', @level0name = N'Committee', @level1type = N'TABLE', @level1name = N'Committee', @level2type = N'COLUMN', @level2name = N'CreatedAt';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Determine who cretead committee', @level0type = N'SCHEMA', @level0name = N'Committee', @level1type = N'TABLE', @level1name = N'Committee', @level2type = N'COLUMN', @level2name = N'CreatedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define updated date of committee', @level0type = N'SCHEMA', @level0name = N'Committee', @level1type = N'TABLE', @level1name = N'Committee', @level2type = N'COLUMN', @level2name = N'UpdatedAt';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Determine who updated committee', @level0type = N'SCHEMA', @level0name = N'Committee', @level1type = N'TABLE', @level1name = N'Committee', @level2type = N'COLUMN', @level2name = N'UpdatedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define committee is active or not', @level0type = N'SCHEMA', @level0name = N'Committee', @level1type = N'TABLE', @level1name = N'Committee', @level2type = N'COLUMN', @level2name = N'IsActive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define identity of committee', @level0type = N'SCHEMA', @level0name = N'Committee', @level1type = N'TABLE', @level1name = N'Committee', @level2type = N'COLUMN', @level2name = N'CommitteeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define agency code of committee', @level0type = N'SCHEMA', @level0name = N'Committee', @level1type = N'TABLE', @level1name = N'Committee', @level2type = N'COLUMN', @level2name = N'AgencyCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'it''s a forigne key  that describe committe type', @level0type = N'SCHEMA', @level0name = N'Committee', @level1type = N'TABLE', @level1name = N'Committee', @level2type = N'COLUMN', @level2name = N'CommitteeTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define  name of committee', @level0type = N'SCHEMA', @level0name = N'Committee', @level1type = N'TABLE', @level1name = N'Committee', @level2type = N'COLUMN', @level2name = N'CommitteeName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define address of committee', @level0type = N'SCHEMA', @level0name = N'Committee', @level1type = N'TABLE', @level1name = N'Committee', @level2type = N'COLUMN', @level2name = N'Address';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define phone of committee', @level0type = N'SCHEMA', @level0name = N'Committee', @level1type = N'TABLE', @level1name = N'Committee', @level2type = N'COLUMN', @level2name = N'Phone';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define fax of committee', @level0type = N'SCHEMA', @level0name = N'Committee', @level1type = N'TABLE', @level1name = N'Committee', @level2type = N'COLUMN', @level2name = N'Fax';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define e-mail of committee', @level0type = N'SCHEMA', @level0name = N'Committee', @level1type = N'TABLE', @level1name = N'Committee', @level2type = N'COLUMN', @level2name = N'Email';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define postal code of committee', @level0type = N'SCHEMA', @level0name = N'Committee', @level1type = N'TABLE', @level1name = N'Committee', @level2type = N'COLUMN', @level2name = N'PostalCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define zip code of committee', @level0type = N'SCHEMA', @level0name = N'Committee', @level1type = N'TABLE', @level1name = N'Committee', @level2type = N'COLUMN', @level2name = N'ZipCode';

