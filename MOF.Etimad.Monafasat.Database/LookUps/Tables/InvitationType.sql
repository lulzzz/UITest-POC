CREATE TABLE [LookUps].[InvitationType] (
    [InvitationTypeId] INT            NOT NULL,
    [NameAr]           NVARCHAR (100) NULL,
    [NameEn]           NVARCHAR (100) NULL,
    CONSTRAINT [PK_InvitationType] PRIMARY KEY CLUSTERED ([InvitationTypeId] ASC)
);

