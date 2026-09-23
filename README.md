# Student Management System

A simple console-based Student Management System built with **C#, .NET 10, and MySQL**.

I created this project to practice building a small backend application with a proper structure instead of putting everything in one file. It includes student CRUD operations, validation, MySQL database access, unit tests, and GitHub Actions.

## Features

- Add a student
- View all students
- Search for a student
- Update student details
- Delete a student
- MySQL database storage
- Input and business validation
- Duplicate enrollment and email protection
- Parameterized SQL queries
- Asynchronous database operations
- Unit tests using xUnit
- GitHub Actions for build and test
- Database configuration through environment variables

## Technologies

- C#
- .NET 10
- MySQL
- MySqlConnector
- xUnit
- Git
- GitHub Actions

## Project Structure

The project is divided into a few simple layers:

```text
Console UI
    ↓
Student Service
    ↓
Student Repository
    ↓
MySQL
```

### Console UI

Handles the menu, user input, and displaying results.

### Student Service

Contains the main business logic and validation.

### Repository

Handles SQL queries and communication with MySQL.

### MySQL

Stores the student records.

## Database

The application uses a MySQL database named:

```text
student_management
```

The main table is:

```text
students
```

The table contains information such as:

- ID
- Enrollment number
- Full name
- Email
- Course
- Semester
- Date of birth
- Created and updated timestamps

The SQL setup is available in:

```text
database/schema.sql
```

## Requirements

You need the following installed:

- .NET 10 SDK
- MySQL 8.x or compatible MySQL server
- Git

Check your .NET version with:

```powershell
dotnet --version
```

## Setup

### 1. Clone the repository

```bash
git clone https://github.com/SuryanshTripathi01/StudentManagementSystem.git
cd StudentManagementSystem
```

### 2. Create the database

Create the database in MySQL:

```sql
CREATE DATABASE student_management
CHARACTER SET utf8mb4
COLLATE utf8mb4_unicode_ci;
```

You can also run the SQL file:

```text
database/schema.sql
```

using MySQL Workbench.

### 3. Configure the database

The application uses environment variables for the MySQL connection.

On Windows PowerShell:

```powershell
$env:SMS_DB_SERVER="127.0.0.1"
$env:SMS_DB_PORT="3306"
$env:SMS_DB_NAME="student_management"
$env:SMS_DB_USER="root"
$env:SMS_DB_PASSWORD="your_password"
```

Replace `your_password` with your own MySQL password.

Do not put your real password in the source code or commit it to GitHub.

## Run the Project

Restore the project dependencies:

```bash
dotnet restore
```

Build the project:

```bash
dotnet build
```

Run the application:

```bash
dotnet run --project src/StudentManagementSystem
```

After the application starts, use the console menu to add, view, search, update, or delete students.

## Run Tests

The project uses xUnit for unit testing.

Run:

```bash
dotnet test
```

The service tests use a fake repository, so the business logic can be tested without connecting to MySQL.

## GitHub Actions

The project contains a GitHub Actions workflow:

```text
.github/workflows/ci.yml
```

The workflow automatically:

1. Restores the project
2. Builds the solution
3. Runs the tests

It runs when code is pushed to `main` and when a pull request targets `main`.

## Why I Used a Service and Repository Layer

I wanted to keep the application organized instead of putting database code and business logic directly inside the console program.

The flow is:

```text
ConsoleUi
   ↓
StudentService
   ↓
IStudentRepository
   ↓
MySqlStudentRepository
   ↓
MySQL
```

This makes each part easier to understand and test.

For example, the service layer can be tested with a fake repository without needing a real database connection.

## Some Important Technical Points

### Parameterized SQL

The application uses parameterized queries instead of directly putting user input into SQL statements.

This helps protect against SQL injection and safely handles database values.

### Validation

Student information is checked before it is saved.

The application also handles duplicate enrollment numbers and email addresses.

### Database Constraints

The database has unique constraints for important fields such as enrollment number and email.

This gives the application an additional layer of protection.

### Async Database Operations

Database calls use asynchronous methods because database access is an I/O operation.

## Project Structure

```text
StudentManagementSystem/
│
├── .github/
│   └── workflows/
│       └── ci.yml
│
├── database/
│   └── schema.sql
│
├── src/
│   └── StudentManagementSystem/
│       ├── Configuration/
│       ├── Data/
│       ├── Models/
│       ├── Services/
│       ├── UI/
│       ├── Program.cs
│       └── StudentManagementSystem.csproj
│
├── tests/
│   └── StudentManagementSystem.Tests/
│       ├── StudentServiceTests.cs
│       └── StudentManagementSystem.Tests.csproj
│
├── .env.example
├── .gitignore
├── LICENSE
├── README.md
├── RUNBOOK.md
└── StudentManagementSystem.sln
```

## Testing Done

While developing the project, I tested:

- Project build
- Unit tests
- MySQL connection
- Database creation
- Adding students
- Listing students
- Searching students
- Updating students
- Deleting students
- Data persistence in MySQL

The project was also pushed to GitHub with GitHub Actions configured for continuous integration.

## Publish the Application

To create a release build for Windows:

```bash
dotnet publish src/StudentManagementSystem -c Release -r win-x64 --self-contained false -o publish
```

The published files will be placed in the `publish` folder.

The MySQL environment variables still need to be configured on the machine where the application is run.

## Git Workflow

For future changes, I use the normal Git workflow:

```bash
git status
git add .
git commit -m "Describe the change"
git push
```

GitHub Actions then checks the project automatically.

## What I Learned

This project helped me practice:

- C# and .NET
- Object-oriented programming
- CRUD operations
- MySQL
- SQL queries
- ADO.NET
- Repository pattern
- Service layer
- Interfaces
- Validation
- Async/Await
- Unit testing with xUnit
- Environment variables
- Git and GitHub
- GitHub Actions and basic CI

## Future Improvements

If I continue developing this project, I would like to add:

- ASP.NET Core Web API
- Web-based frontend
- Authentication and authorization
- Role-based access
- Logging
- Database migrations
- Pagination
- Integration tests
- Swagger/OpenAPI
- Docker support

For now, I kept it as a console application so I could focus on understanding the backend structure, database operations, testing, and Git workflow.

## GitHub Repository

https://github.com/SuryanshTripathi01/StudentManagementSystem

## License

See the `LICENSE` file in this repository.
