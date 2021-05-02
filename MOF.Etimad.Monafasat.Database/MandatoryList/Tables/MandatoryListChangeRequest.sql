CREATE TABLE [MandatoryList].[MandatoryListChangeRequest] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [CreatedAt]        DATETIME2 (7)  NOT NULL,
    [CreatedBy]        NVARCHAR (256) NULL,
    [UpdatedAt]        DATETIME2 (7)  NULL,
    [UpdatedBy]        NVARCHAR (256) NULL,
    [IsActive]         BIT            NULL,
    [DivisionNameAr]   NVARCHAR (400) NULL,
    [DivisionNameEn]   NVARCHAR (400) NULL,
    [DivisionCode]     NVARCHAR (400) NULL,
    [RejectionReason]  NVARCHAR (MAX) NULL,
    [StatusId]         INT            NOT NULL,
    [OriginalEntityId] INT            DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_MandatoryListChangeRequest] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_MandatoryListChangeRequest_MandatoryList_OriginalEntityId] FOREIGN KEY ([OriginalEntityId]) REFERENCES [MandatoryList].[MandatoryList] ([Id]),
    CONSTRAINT [FK_MandatoryListChangeRequest_MandatoryListChangeRequestStatus_StatusId] FOREIGN KEY ([StatusId]) REFERENCES [LookUps].[MandatoryListChangeRequestStatus] ([StatusId])
);


GO
CREATE NONCLUSTERED INDEX [IX_MandatoryListChangeRequest_StatusId]
    ON [MandatoryList].[MandatoryListChangeRequest]([StatusId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_MandatoryListChangeRequest_OriginalEntityId]
    ON [MandatoryList].[MandatoryListChangeRequest]([OriginalEntityId] ASC);

