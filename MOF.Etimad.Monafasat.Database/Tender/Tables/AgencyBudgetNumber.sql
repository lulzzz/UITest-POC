CREATE TABLE [Tender].[AgencyBudgetNumber] (
    [AgencyBudgetNumberId] INT             IDENTITY (1, 1) NOT NULL,
    [TenderId]             INT             NULL,
    [ProjectNumber]        NVARCHAR (MAX)  NULL,
    [ProjectName]          NVARCHAR (MAX)  NULL,
    [Cache]                DECIMAL (18, 2) NOT NULL,
    [Cost]                 DECIMAL (18, 2) NOT NULL,
    [IsProject]            BIT             NULL,
    CONSTRAINT [PK_AgencyBudgetNumber] PRIMARY KEY CLUSTERED ([AgencyBudgetNumberId] ASC),
    CONSTRAINT [FK_AgencyBudgetNumber_Tender_TenderId] FOREIGN KEY ([TenderId]) REFERENCES [Tender].[Tender] ([TenderId])
);


GO
CREATE NONCLUSTERED INDEX [IX_AgencyBudgetNumber_TenderId]
    ON [Tender].[AgencyBudgetNumber]([TenderId] ASC);

