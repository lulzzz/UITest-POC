CREATE TABLE [Tender].[ConditionsBooklet] (
    [CreatedAt]            DATETIME2 (7)  NOT NULL,
    [CreatedBy]            NVARCHAR (256) NULL,
    [UpdatedAt]            DATETIME2 (7)  NULL,
    [UpdatedBy]            NVARCHAR (256) NULL,
    [IsActive]             BIT            NULL,
    [ConditionsBookletId]  INT            IDENTITY (1, 1) NOT NULL,
    [CommericalRegisterNo] NVARCHAR (20)  NULL,
    [TenderId]             INT            NOT NULL,
    CONSTRAINT [PK_ConditionsBooklet] PRIMARY KEY CLUSTERED ([ConditionsBookletId] ASC),
    CONSTRAINT [FK_ConditionsBooklet_Supplier_CommericalRegisterNo] FOREIGN KEY ([CommericalRegisterNo]) REFERENCES [IDM].[Supplier] ([SelectedCr]),
    CONSTRAINT [FK_ConditionsBooklet_Tender_TenderId] FOREIGN KEY ([TenderId]) REFERENCES [Tender].[Tender] ([TenderId])
);


GO
CREATE NONCLUSTERED INDEX [IX_ConditionsBooklet_CommericalRegisterNo]
    ON [Tender].[ConditionsBooklet]([CommericalRegisterNo] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ConditionsBooklet_TenderId]
    ON [Tender].[ConditionsBooklet]([TenderId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define created date of purchased the condition booklet', @level0type = N'SCHEMA', @level0name = N'Tender', @level1type = N'TABLE', @level1name = N'ConditionsBooklet', @level2type = N'COLUMN', @level2name = N'CreatedAt';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Determine who cretead request for purchased the condition booklet', @level0type = N'SCHEMA', @level0name = N'Tender', @level1type = N'TABLE', @level1name = N'ConditionsBooklet', @level2type = N'COLUMN', @level2name = N'CreatedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define updated date of purchased the condition booklet', @level0type = N'SCHEMA', @level0name = N'Tender', @level1type = N'TABLE', @level1name = N'ConditionsBooklet', @level2type = N'COLUMN', @level2name = N'UpdatedAt';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Determine who updated request for purchased the condition booklet', @level0type = N'SCHEMA', @level0name = N'Tender', @level1type = N'TABLE', @level1name = N'ConditionsBooklet', @level2type = N'COLUMN', @level2name = N'UpdatedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define condition booklet user is active or not', @level0type = N'SCHEMA', @level0name = N'Tender', @level1type = N'TABLE', @level1name = N'ConditionsBooklet', @level2type = N'COLUMN', @level2name = N'IsActive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define identity of conditions booklet', @level0type = N'SCHEMA', @level0name = N'Tender', @level1type = N'TABLE', @level1name = N'ConditionsBooklet', @level2type = N'COLUMN', @level2name = N'ConditionsBookletId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'it''s described supplier commerical number that who purchased the condition booklet', @level0type = N'SCHEMA', @level0name = N'Tender', @level1type = N'TABLE', @level1name = N'ConditionsBooklet', @level2type = N'COLUMN', @level2name = N'CommericalRegisterNo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'it''s a forigne key that described tender related to condtion booklet', @level0type = N'SCHEMA', @level0name = N'Tender', @level1type = N'TABLE', @level1name = N'ConditionsBooklet', @level2type = N'COLUMN', @level2name = N'TenderId';

