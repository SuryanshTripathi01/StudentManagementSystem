# Student Management System — C# / .NET 10 / MySQL

A console-based student record management system built with C#, .NET 10, and MySQL. It demonstrates CRUD operations, layered design, validation, parameterized SQL, asynchronous ADO.NET access, unit testing, and GitHub Actions CI.

## Features

- Add, list, search, update, and delete students
- MySQL persistence
- Automatic table initialization at startup
- Parameterized SQL queries
- Input and business validation
- Duplicate enrollment protection
- Async database operations
- Unit tests for service/business rules
- GitHub Actions build + test pipeline
- Secrets kept outside source control through environment variables

## Architecture

`ConsoleUi` → `StudentService` → `IStudentRepository` → `MySqlStudentRepository` → MySQL

The service layer contains business validation; the repository layer owns database access; the UI handles console input/output.

## Requirements

- .NET 10 SDK
- MySQL 8.x (or a compatible MySQL server)
- Git

.NET 10 is an LTS release supported by Microsoft through November 14, 2028.

## Database setup

Option A: manually run `database/schema.sql` in MySQL Workbench or the MySQL CLI.

Option B: create the database, then let the application create the `students` table automatically.

Example:

```sql
CREATE DATABASE student_management CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
```

## Configuration

The application reads:

- `SMS_DB_SERVER` (default `127.0.0.1`)
- `SMS_DB_PORT` (default `3306`)
- `SMS_DB_NAME` (default `student_management`)
- `SMS_DB_USER` (default `root`)
- `SMS_DB_PASSWORD` (default empty)

### Windows PowerShell

```powershell
$env:SMS_DB_SERVER="127.0.0.1"
$env:SMS_DB_PORT="3306"
$env:SMS_DB_NAME="student_management"
$env:SMS_DB_USER="root"
$env:SMS_DB_PASSWORD="your_password"
```

Do not commit real passwords to GitHub.

## Run locally

```bash
dotnet restore
dotnet build
dotnet run --project src/StudentManagementSystem
```

## Test

```bash
dotnet test
```

## Publish a release build

```bash
dotnet publish src/StudentManagementSystem -c Release -r win-x64 --self-contained false -o publish
```

Run the published executable from the `publish` directory after setting the database environment variables.

## GitHub

```bash
git init
git add .
git commit -m "Initial Student Management System"
git branch -M main
git remote add origin https://github.com/YOUR_USERNAME/student-management-system.git
git push -u origin main
```

GitHub Actions runs restore, build, and tests on every push to `main` and every pull request targeting `main`.

## Interview talking points

- CRUD means Create, Read, Update, Delete.
- Repository pattern keeps SQL/database code away from the UI.
- Service layer enforces business rules before persistence.
- Parameterized queries protect against SQL injection and handle values safely.
- Async database APIs avoid blocking the calling thread while I/O is in progress.
- Unique database constraints provide a second line of defense for enrollment number and email uniqueness.
- Unit tests use a fake repository so business rules can be tested without requiring a live MySQL server.

## Scope and production improvements

For a larger production system, add authentication/authorization, structured logging, centralized configuration, migrations, integration tests against a disposable database, pagination, audit logging, and an ASP.NET Core Web API/UI instead of a console interface.
