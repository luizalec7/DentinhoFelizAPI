# Dentinho Feliz API

![GitHub repo size](https://img.shields.io/github/repo-size/luizalec7/DentinhoFelizAPI)
![GitHub contributors](https://img.shields.io/github/contributors/luizalec7/DentinhoFelizAPI)
![GitHub last commit](https://img.shields.io/github/last-commit/luizalec7/DentinhoFelizAPI)

## 📌 Descrição
A **Dentinho Feliz API** é a camada backend do aplicativo **Dentinho Feliz**, desenvolvido para auxiliar crianças na conscientização da higiene bucal. A API fornece funcionalidades essenciais para autenticação de usuários, gerenciamento de quizzes, alarmes e dúvidas.

## 👥 Integrantes do Grupo
- **Luiz Alecsander Viana** - RM553034
- **Guilherme Augusto de Oliveira** - RM554176
- **Lucas Martinez Lopes** - RM553816

## 🏗 Arquitetura do Projeto
A API segue uma **arquitetura monolítica** organizada em **Camadas** (Clean Architecture), utilizando **.NET 7** com **Entity Framework Core** para manipulação do banco de dados Oracle.

### 🔍 Camadas da Aplicação
📁 **DentinhoFeliz.API** → Responsável pelos endpoints HTTP e configuração da API.
📁 **DentinhoFeliz.Application** → Contém os serviços de aplicação e regras de negócio.
📁 **DentinhoFeliz.Domain** → Define as entidades e interfaces da aplicação.
📁 **DentinhoFeliz.Infrastructure** → Implementa a persistência de dados e interações com o banco de dados Oracle.

## 📌 Design Patterns Utilizados
- **Repository Pattern** → Para abstrair a persistência de dados.
- **Unit of Work** → Para garantir transações atômicas.
- **Dependency Injection** → Para facilitar a inversão de dependências.
- **Factory Pattern** → Para a criação de instâncias de serviços.
- **DTO (Data Transfer Object)** → Para evitar exposição direta das entidades do banco.

## ⚙️ Tecnologias Utilizadas
- **.NET 7**
- **Entity Framework Core**
- **Oracle Database (FIAP Cloud)**
- **Swagger para documentação**
- **Docker para ambiente isolado (Opcional)**

## 🚀 Como Rodar a API
### 📌 Pré-requisitos
1. **Instale o .NET 7 SDK**
2. **Configure a string de conexão com Oracle FIAP** no `appsettings.json`:
   ```json
   "ConnectionStrings": {
      "DefaultConnection": "User Id=rm554176;Password=180505;Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=oracle.fiap.com.br)(PORT=1521)))(CONNECT_DATA=(SERVICE_NAME=freepdb1)))"
   }
   ```
3. **Instale o EF Core Tools**:
   ```sh
   dotnet tool install --global dotnet-ef
   ```

### 🔧 Passo a Passo para rodar
1️⃣ **Clone o repositório**:
```sh
   git clone https://github.com/luizalec7/DentinhoFelizAPI.git
   cd DentinhoFelizAPI
```

2️⃣ **Restaure as dependências**:
```sh
   dotnet restore
```

3️⃣ **Aplique as migrações do banco de dados**:
```sh
   dotnet ef database update --project ./DentinhoFeliz.Infrastructure --startup-project ./DentinhoFeliz.API
```

4️⃣ **Execute a API**:
```sh
   dotnet run --project ./DentinhoFeliz.API
```

A API estará disponível em `http://localhost:5043/swagger`

---

## 📌 Endpoints Principais
### 🔹 **Autenticação**
- `POST /api/auth/login` → Login de usuários.
- `POST /api/auth/register` → Cadastro de novos usuários.

### 🔹 **Usuários**
- `GET /api/usuario` → Lista todos os usuários cadastrados.
- `GET /api/usuario/{id}` → Retorna detalhes de um usuário específico.

### 🔹 **Quizzes**
- `GET /api/quiz` → Retorna todos os quizzes disponíveis.
- `POST /api/quiz` → Adiciona um novo quiz.

### 🔹 **Dúvidas**
- `GET /api/duvidas` → Lista todas as dúvidas registradas.
- `POST /api/duvidas` → Registra uma nova dúvida.

### 🔹 **Alarmes**
- `GET /api/alarmes` → Retorna todos os alarmes cadastrados.
- `POST /api/alarmes` → Cria um novo alarme.

## ✅ Exemplos de Testes
A API possui testes unitários utilizando **xUnit**. Para rodá-los, execute:
```sh
   dotnet test
```

## 📜 Licença
Este projeto é de código aberto e está licenciado sob a **MIT License**.

---

📌 **Repositório Oficial:** [Dentinho Feliz API](https://github.com/luizalec7/DentinhoFelizAPI)

🚀 Desenvolvido por Luiz Alecsander, Guilherme Augusto e Lucas Martinez.