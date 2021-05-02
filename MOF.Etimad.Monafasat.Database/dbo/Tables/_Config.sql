CREATE TABLE [dbo].[_Config] (
    [Id]    INT            IDENTITY (1, 1) NOT NULL,
    [Key]   NVARCHAR (MAX) NULL,
    [Value] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK__Config] PRIMARY KEY CLUSTERED ([Id] ASC)
);

