CREATE TABLE [dbo].[Livros] (
    [Id]     INT           IDENTITY (1, 1) NOT NULL,
    [titulo] NVARCHAR (50) NOT NULL,
    [isbn]   NVARCHAR (50) NOT NULL,
    [ano]    INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[Autores] (
    [Id]         INT           IDENTITY (1, 1) NOT NULL,
    [Nome]       NVARCHAR (50) NOT NULL,
    [Sobrenome]  NVARCHAR (50) NOT NULL,
    [Email]      NVARCHAR (50) NOT NULL,
    [Nascimento] DATE          NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[Autor_Livro] (
    [Id]       INT IDENTITY (1, 1) NOT NULL,
    [Id_Livro] INT NOT NULL,
    [Id_Autor] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Autor_Livro_Livro] FOREIGN KEY ([Id_Livro]) REFERENCES [dbo].[Livros] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_Autor_Livro_Autor] FOREIGN KEY ([Id_Autor]) REFERENCES [dbo].[Autores] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
);