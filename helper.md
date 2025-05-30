// rodas ef
dotnet ef migrations add Intial -p Fcg.Infra -s Fcg.WebApi
dotnet ef database update -p Fcg.Infra -s Fcg.WebApi


// tabelas
CREATE TABLE Usuarios (
    Id UUID PRIMARY KEY,
    Nome VARCHAR(100) NOT NULL,
    Email VARCHAR(100) NOT NULL UNIQUE,
    SenhaHash VARCHAR(255) NOT NULL,
    Role VARCHAR(20) NOT NULL DEFAULT 'Usuario'
);

CREATE TABLE Jogos (
    Id UUID PRIMARY KEY,
    Nome VARCHAR(100) NOT NULL,
    Descricao VARCHAR(500),
    Preco DECIMAL(10,2) NOT NULL,
    Ativo SMALLINT NOT NULL DEFAULT 1
);

CREATE TABLE JogosAdquiridos (
    Id UUID PRIMARY KEY,
    UsuarioId UUID NOT NULL,
    JogoId UUID NOT NULL,
    DataAquisicao TIMESTAMP NOT NULL,
    PrecoPago DECIMAL(10,2) NOT NULL,

    FOREIGN KEY (UsuarioId) REFERENCES Usuarios(Id),
    FOREIGN KEY (JogoId) REFERENCES Jogos(Id)
);

CREATE TABLE Promocoes (
    Id UUID PRIMARY KEY,
    JogoId UUID NOT NULL,
    PrecoPromocional DECIMAL(10,2) NOT NULL,
    DataInicio TIMESTAMP NOT NULL,
    DataFim TIMESTAMP NOT NULL,

    FOREIGN KEY (JogoId) REFERENCES Jogos(Id)
);