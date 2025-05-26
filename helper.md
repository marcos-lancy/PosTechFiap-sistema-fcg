// rodas ef
dotnet ef migrations add Intial -p Fcg.Infra -s Fcg.Api
dotnet ef database update -p Fcg.Infra -s Fcg.Api

ou
dotnet ef migrations add Initial -p Fcg.Infra -s Fcg.Api -- --connection "Host=postgres;Port=5432;Database=fcgDb;Username=postgres;Password=postgres"


// Precisa ser feito: 
 - Regras de negocio de login em service
 - Tratamento geral de erro (ao lancar uma exception em appservice ou servie, um middleware detecta e conforme a exception retorna o HTTP correspondente no loadout)
 - Documentação swagger

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