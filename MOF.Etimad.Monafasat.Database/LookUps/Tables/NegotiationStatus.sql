CREATE TABLE [LookUps].[NegotiationStatus] (
    [NegotiationStatusId] INT            NOT NULL,
    [Name]                NVARCHAR (100) NULL,
    CONSTRAINT [PK_NegotiationStatus] PRIMARY KEY CLUSTERED ([NegotiationStatusId] ASC)
);

