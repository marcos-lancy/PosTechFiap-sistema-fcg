// rodas ef
dotnet ef migrations add Intial -p Fcg.Infra -s Fcg.Api
dotnet ef database update -p Fcg.Infra -s Fcg.Api

ou
dotnet ef migrations add Initial -p Fcg.Infra -s Fcg.Api -- --connection "Host=localhost;Port=5432;Database=fcgDb;Username=postgres;Password=postgres"


// Precisa ser feito: 
 - Regras de negocio de login em service
 - Tratamento geral de erro (ao lancar uma exception em appservice ou servie, um middleware detecta e conforme a exception retorna o HTTP correspondente no loadout)
 - Documentação swagger
- Atualização de senha
- Endpoints "me"
- Ao atualizar jogo, verificar se nao esta criando dois com o mesmo nome
// login
{
  "email": "jhonnatan.jp@gmail.com",
  "senha": "Senha@123"
},
{
  "email": "admin@gmail.com",
  "senha": "Senha@1234"
}

# Autenticação
POST   /api/auth/login
POST   /api/auth/register

# Jogos (público para leitura, admins podem criar/editar/excluir)
GET    /api/jogos
GET    /api/jogos/{id}
POST   /api/jogos                    # Requer role: Admin
PUT    /api/jogos/{id}              # Requer role: Admin
DELETE /api/jogos/{id}              # Requer role: Admin

# Jogar (usuário logado)
POST   /api/jogos/{id}/jogar

# Conta do usuário autenticado
GET    /api/conta/me
PUT    /api/conta/me

# Usuários (restrito a admins)
GET    /api/usuarios                 # Requer role: Admin
GET    /api/usuarios/{id}           # Requer role: Admin
PUT    /api/usuarios/{id}           # Requer role: Admin
PUT    /api/usuarios/{id}/status    # Requer role: Admin
DELETE /api/usuarios/{id}           # Requer role: Admin


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