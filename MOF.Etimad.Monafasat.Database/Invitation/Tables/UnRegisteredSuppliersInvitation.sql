CREATE TABLE [Invitation].[UnRegisteredSuppliersInvitation] (
    [CreatedAt]          DATETIME2 (7)   NOT NULL,
    [CreatedBy]          NVARCHAR (256)  NULL,
    [UpdatedAt]          DATETIME2 (7)   NULL,
    [UpdatedBy]          NVARCHAR (256)  NULL,
    [IsActive]           BIT             NULL,
    [Id]                 INT             IDENTITY (1, 1) NOT NULL,
    [TenderId]           INT             NOT NULL,
    [Email]              NVARCHAR (1024) NULL,
    [MobileNo]           NVARCHAR (MAX)  NULL,
    [InvitationStatusId] INT             NULL,
    [CrNumber]           NVARCHAR (50)   NULL,
    [InvitationTypeId]   INT             NOT NULL,
    [Description]        NVARCHAR (2056) NULL,
    CONSTRAINT [PK_UnRegisteredSuppliersInvitation] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_UnRegisteredSuppliersInvitation_InvitationStatus_InvitationStatusId] FOREIGN KEY ([InvitationStatusId]) REFERENCES [LookUps].[InvitationStatus] ([InvitationStatusId]),
    CONSTRAINT [FK_UnRegisteredSuppliersInvitation_Tender_TenderId] FOREIGN KEY ([TenderId]) REFERENCES [Tender].[Tender] ([TenderId])
);


GO
CREATE NONCLUSTERED INDEX [IX_UnRegisteredSuppliersInvitation_InvitationStatusId]
    ON [Invitation].[UnRegisteredSuppliersInvitation]([InvitationStatusId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_UnRegisteredSuppliersInvitation_TenderId]
    ON [Invitation].[UnRegisteredSuppliersInvitation]([TenderId] ASC);

