CREATE TABLE [LookUps].[UserRole] (
    [UserRoleId]     INT             NOT NULL,
    [Name]           NVARCHAR (1024) NULL,
    [DisplayNameAr]  NVARCHAR (1024) NULL,
    [DisplayNameEn]  NVARCHAR (1024) NULL,
    [IsCombinedRole] BIT             DEFAULT (CONVERT([bit],(0))) NOT NULL,
    CONSTRAINT [PK_UserRole] PRIMARY KEY CLUSTERED ([UserRoleId] ASC)
);

