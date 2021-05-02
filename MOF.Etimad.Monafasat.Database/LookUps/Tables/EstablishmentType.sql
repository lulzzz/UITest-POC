CREATE TABLE [LookUps].[EstablishmentType] (
    [EstablishmentTypeId]      INT            IDENTITY (1, 1) NOT NULL,
    [Name]                     NVARCHAR (500) NULL,
    [CommercialRegistrationNo] NVARCHAR (20)  NULL,
    [Size]                     NVARCHAR (20)  NULL,
    CONSTRAINT [PK_EstablishmentType] PRIMARY KEY CLUSTERED ([EstablishmentTypeId] ASC)
);

