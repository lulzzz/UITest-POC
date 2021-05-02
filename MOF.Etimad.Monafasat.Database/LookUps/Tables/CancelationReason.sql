CREATE TABLE [LookUps].[CancelationReason] (
    [CancelationReasonId] INT            NOT NULL,
    [NameAr]              NVARCHAR (500) NULL,
    [NameEn]              NVARCHAR (500) NULL,
    CONSTRAINT [PK_CancelationReason] PRIMARY KEY CLUSTERED ([CancelationReasonId] ASC)
);

