CREATE TYPE [dbo].[OfferQuantityItems] AS TABLE (
    [Id]                 BIGINT         NULL,
    [ColumnId]           BIGINT         NULL,
    [TenderFormHeaderId] BIGINT         NULL,
    [ActivityTemplateId] INT            NULL,
    [ItemNumber]         BIGINT         NULL,
    [Value]              NVARCHAR (MAX) NULL,
    [AlternativeValue]   NVARCHAR (MAX) NULL,
    [IsDefault]          BIT            NULL);

