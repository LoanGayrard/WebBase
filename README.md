# WebBase

WebBase is a **Docker-first fullstack template** designed to bootstrap modern web applications quickly.

It provides:
- a .NET 8 Web API (ASP.NET Core)
- PostgreSQL
- JWT authentication (ASP.NET Identity)
- EF Core with migrations
- Docker Compose for local development + production
- A simple React/Vite frontend
- a clean, reusable project structure

The goal is to offer a **ready-to-use base**, easy to fork and adapt.

---

## 🚀 Development setup

### Requirements
- Docker
- Docker Compose v2

All commands must be run from the **root directory** of the project (`WebBase`).

---

## ▶️ First start

```bash
git clone https://github.com/sonixray/WebBase.git
cd WebBase
cp .env.example .env
docker compose -f docker-compose.dev.yml up -d --build
```
Then open http://localhost:5173 🎉

This will:
* start PostgreSQL
* start the API
* apply pending EF Core migrations automatically (development only)
* start the frontend

### 🔄 Restart the project

```bash
docker compose -f docker-compose.dev.yml down
docker compose -f docker-compose.dev.yml up -d
```

### 🧹 Clean restart (reset database)

```bash
docker compose -f docker-compose.dev.yml down -v
docker compose -f docker-compose.dev.yml up -d --build
```

> ⚠️ **Warning:** This will delete the database volume and all local data.

### 🛑 Stop the project

```bash
docker compose -f docker-compose.dev.yml down
```

---

## 🌐 Access

**API (Swagger):**
[http://localhost:8080/swagger](http://localhost:8080/swagger)

**PostgreSQL (from host machine):**
* **Host:** `localhost`
* **Port:** `${POSTGRES_PORT}` (default: `5432`)
* **Database:** `${POSTGRES_DB}`
* **User:** `${POSTGRES_USER}`

**Frontend page**
[http://localhost:5173/](http://localhost:5173/)

---

## 🧱 Project structure

```text
backend/
  Api.sln
  Api/
    Api.csproj
    Program.cs
    Controllers/
    Dtos/
  Api.Data/
    Api.Data.csproj
    AppDbContext.cs
    Entities/
    Migrations/
    DesignTimeDbContextFactory.cs
```

---

## 🔐 Authentication

Authentication is handled using:
* **ASP.NET Core Identity**
* **JWT Bearer tokens**

Available endpoints:
* `POST /api/auth/register`
* `POST /api/auth/login`
* `GET /api/auth/me`

---

## 🗄️ Database & migrations

In **development**, pending EF Core migrations are applied automatically at API startup.

In **production**, automatic migrations at startup are disabled by default.

Recommended production approaches:
* run migrations during deployment (CI/CD)
* or use a dedicated migration job/container

---

## 🛠️ EF Core tooling (local)

`dotnet ef` does not load `.env` files.
A `DesignTimeDbContextFactory` is provided in `Api.Data` to support local tooling.

```bash
dotnet ef migrations add MyMigration \
  --project backend/Api.Data/Api.Data.csproj \
  --startup-project backend/Api/Api.csproj

dotnet ef database update \
  --project backend/Api.Data/Api.Data.csproj \
  --startup-project backend/Api/Api.csproj
```

---

## ⚙️ Environment variables

Example `.env` file:

```env
POSTGRES_DB=app
POSTGRES_USER=postgres
POSTGRES_PASSWORD=root
POSTGRES_PORT=5432

API_PORT=8080
ASPNETCORE_ENVIRONMENT=Development

JWT_ISSUER=Api
JWT_AUDIENCE=Api
JWT_SIGNING_KEY=CHANGE_ME_TO_A_LONG_RANDOM_SECRET
JWT_EXPIRES_MINUTES=120
```

> **Note:** `.env` files are not versioned. Use `.env.example` as a reference.

---

## ❓ Troubleshooting

**If PostgreSQL credentials were changed:**
```bash
docker compose -f docker-compose.dev.yml down -v
```

**If ports are already in use:**
1. update the `.env` file
2. restart the stack

---

## 📄 License

MIT

# 🤝 Contributing

Contributions are welcome!
Fork the repository, create a feature branch, and submit a pull request to improve or extend the base.
