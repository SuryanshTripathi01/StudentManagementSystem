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

The application provides the basic operations needed to manage student records:

- Add a new student
- View all students
- Search for a student
- Update student information
- Delete a student
- Validate user input
- Prevent duplicate enrollment numbers and email addresses
- Store student data in MySQL

The application also performs database operations asynchronously and uses parameterized SQL queries.

---

## Technologies Used

- **C#**
- **.NET 10**
- **MySQL**
- **ADO.NET / MySqlConnector**
- **xUnit**
- **Git & GitHub**
- **GitHub Actions**

---

## Project Structure

The project follows a simple layered architecture:

```text
The application provides the basic operations needed to manage student records:

- Add a new student
- View all students
- Search for a student
- Update student information
- Delete a student
- Validate user input
- Prevent duplicate enrollment numbers and email addresses
- Store student data in MySQL

The application also performs database operations asynchronously and uses parameterized SQL queries.

---

## Technologies Used

- **C#**
- **.NET 10**
- **MySQL**
- **ADO.NET / MySqlConnector**
- **xUnit**
- **Git & GitHub**
- **GitHub Actions**

---

## Project Structure

The project follows a simple layered architecture:

```text
Console UI
    ↓
Student Service
    ↓
Student Repository
    ↓
MySQL Database

Each part has a specific responsibility.
Console UI
The UI is responsible for interacting with the user. It displays menus, reads input from the console, and shows the results.
Student Service
The service layer contains the main business rules and validation logic.
For example, it checks whether the student information is valid before sending it to the database.
Repository
The repository layer handles database operations.
This keeps SQL code separate from the user interface and business logic.
MySQL
MySQL is used to permanently store the student records.
Database
The project uses a MySQL database called:
student_management

The main table is:
students

It stores information such as:
- Student ID
- Enrollment number
- Full name
- Email
- Course
- Semester
- Date of birth
- Created/updated timestamps
The database schema is available here:
database/schema.sql

You can either run the SQL file manually using MySQL Workbench or allow the application to create the required table when it starts.
Getting Started
Requirements
Before running the project, make sure you have:
- .NET 10 SDK
- MySQL 8.x or a compatible MySQL server
- Git
You can check your .NET installation with:
dotnet --version

Setting up the database
First, create the database in MySQL:
CREATE DATABASE student_management
CHARACTER SET utf8mb4
COLLATE utf8mb4_unicode_ci;

You can also run:
database/schema.sql

from MySQL Workbench.
Database Configuration
The application uses environment variables for the database connection instead of storing the password directly in the source code.
The variables are:
SMS_DB_SERVER
SMS_DB_PORT
SMS_DB_NAME
SMS_DB_USER
SMS_DB_PASSWORD

For example, on Windows PowerShell:
$env:SMS_DB_SERVER="127.0.0.1"
$env:SMS_DB_PORT="3306"
$env:SMS_DB_NAME="student_management"
$env:SMS_DB_USER="root"
$env:SMS_DB_PASSWORD="your_password"

Replace your_password with your local MySQL password.
Do not commit your real password to GitHub.
The project contains an .env.example file only as a reference. The actual .env file is ignored by Git.
Running the Project
Clone the repository:
git clone https://github.com/SuryanshTripathi01/StudentManagementSystem.git

Move into the project directory:
cd StudentManagementSystem

Restore the dependencies:
dotnet restore

Build the project:
dotnet build

Run the application:
dotnet run --project src/StudentManagementSystem

After starting the application, a menu will appear in the console where you can perform the different student management operations.
Running the Tests
The project contains unit tests written using xUnit.
Run them with:
dotnet test

The tests focus mainly on the business/service layer, so they can run without requiring a live MySQL connection.
The current test suite covers important validation and business rules.
GitHub Actions
The project also includes a GitHub Actions workflow:
.github/workflows/ci.yml

Whenever changes are pushed to the main branch or a pull request is opened against main, GitHub Actions runs the project's restore, build, and test steps.
This helps catch build or test problems before changes are considered complete.
Why I used a Repository and Service Layer
I didn't want all of the application logic sitting inside Program.cs.
Instead, I separated the responsibilities:
ConsoleUi
   ↓
StudentService
   ↓
IStudentRepository
   ↓
MySqlStudentRepository
   ↓
MySQL

This makes the code easier to understand and also makes the business logic easier to test.
For example, the service can be tested using a fake repository without needing to connect to a real MySQL database.
A few things I focused on
While building the project, I specifically wanted to practice some concepts that are useful in real backend development.
Parameterized SQL
The database queries use parameters instead of directly concatenating user input into SQL statements.
This helps prevent SQL injection and makes handling database values safer.
Validation
Student information is validated before it reaches the database.
The application also handles duplicate enrollment numbers and email addresses.
Async Database Operations
Database operations use asynchronous APIs so that the application does not unnecessarily block while waiting for database I/O.
Database Constraints
The database itself also has unique constraints for fields such as enrollment number and email.
This means the application has validation at the business level as well as protection at the database level.
Project Structure
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

Publishing the Application
A release build can be created with:
dotnet publish src/StudentManagementSystem -c Release -r win-x64 --self-contained false -o publish

The generated application will be placed inside the publish directory.
The required database environment variables still need to be configured before running the published application.
What I Learned From This Project
This project helped me understand how the different parts of a backend application fit together.
Some of the main things I practiced were:
- Building a C# application with .NET
- Working with MySQL from C#
- Writing CRUD operations
- Separating business logic from database code
- Using interfaces and the repository pattern
- Writing asynchronous database code
- Handling validation and database constraints
- Writing unit tests with xUnit
- Managing configuration through environment variables
- Using Git and GitHub
- Setting up a basic CI pipeline with GitHub Actions
Possible Improvements
There are several things I would add if this project were expanded into a larger application:
- User authentication and authorization
- ASP.NET Core Web API
- A web-based frontend
- Structured logging
- Database migrations
- Pagination
- Better centralized configuration
- Integration tests using a disposable database
- Audit logging
- Role-based access control
For now, I kept the project as a console application so I could focus on understanding the backend structure and the core functionality.
