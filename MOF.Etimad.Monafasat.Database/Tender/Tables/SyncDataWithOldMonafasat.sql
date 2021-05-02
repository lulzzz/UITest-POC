CREATE TABLE [Tender].[SyncDataWithOldMonafasat] (
    [SyncDataWithOldMonafasatId] INT            IDENTITY (1, 1) NOT NULL,
    [TenderId]                   INT            NOT NULL,
    [RequestInformation]         NVARCHAR (MAX) NULL,
    [IsSendSuccessfully]         BIT            NOT NULL,
    CONSTRAINT [PK_SyncDataWithOldMonafasat] PRIMARY KEY CLUSTERED ([SyncDataWithOldMonafasatId] ASC)
);

