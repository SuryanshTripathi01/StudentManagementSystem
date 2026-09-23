=Student Management System=
A simple console-based Student Management System built with C#, .NET 10, and MySQL.
I created this project to practice building a small backend application with a proper structure instead of putting everything in one file. It includes student CRUD operations, validation, MySQL database access, unit tests, and GitHub Actions.

Features
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

  
#-#-#-Technologies-#-#-#
- C#
- .NET 10
- MySQL
- MySqlConnector
- xUnit
- Git
- GitHub Actions

  
---Project Structure---

The project is divided into a few simple layers:
Console UI
    ↓
Student Service
    ↓
Student Repository
    ↓
MySQL


Console UI
Handles the menu, user input, and displaying results.
Student Service
Contains the main business logic and validation.
Repository
Handles SQL queries and communication with MySQL.
MySQL
Stores the student records.
Database
The application uses a MySQL database named:
student_management
The main table is:
students
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
database/schema.sql


---Requirements---
You need the following installed:
- .NET 10 SDK
- MySQL 8.x or compatible MySQL server
- Git
Check your .NET version with:
dotnet --version

Setup
1. Clone the repository
git clone https://github.com/SuryanshTripathi01/StudentManagementSystem.git
cd StudentManagementSystem
2. Create the database
Create the database in MySQL:
CREATE DATABASE student_management
CHARACTER SET utf8mb4
COLLATE utf8mb4_unicode_ci;
You can also run the SQL file:
database/schema.sql
using MySQL Workbench.
3. Configure the database
The application uses environment variables for the MySQL connection.
On Windows PowerShell:
$env:SMS_DB_SERVER="127.0.0.1"
$env:SMS_DB_PORT="3306"
$env:SMS_DB_NAME="student_management"
$env:SMS_DB_USER="root"
$env:SMS_DB_PASSWORD="your_password"

Replace your_password with your own MySQL password.
Do not put your real password in the source code or commit it to GitHub.

Run the Project-
Restore the project dependencies:
dotnet restore
Build the project:
dotnet build
Run the application:
dotnet run --project src/StudentManagementSystem
After the application starts, use the console menu to add, view, search, update, or delete students.
Run Tests
The project uses xUnit for unit testing.
Run:
dotnet test
The service tests use a fake repository, so the business logic can be tested without connecting to MySQL.

GitHub Actions
The project contains a GitHub Actions workflow:
.github/workflows/ci.yml
The workflow automatically:
1. Restores the project
2. Builds the solution
3. Runs the tests
It runs when code is pushed to main and when a pull request targets main.

Why I Used a Service and Repository Layer
I wanted to keep the application organized instead of putting database code and business logic directly inside the console program.
The flow is:
ConsoleUi
   ↓
StudentService
   ↓
IStudentRepository
   ↓
MySqlStudentRepository
   ↓
MySQL
This makes each part easier to understand and test.
For example, the service layer can be tested with a fake repository without needing a real database connection.
Some Important Technical Points

Parameterized SQL
The application uses parameterized queries instead of directly putting user input into SQL statements.
This helps protect against SQL injection and safely handles database values.
Validation
Student information is checked before it is saved.
The application also handles duplicate enrollment numbers and email addresses.
Database Constraints
The database has unique constraints for important fields such as enrollment number and email.
This gives the application an additional layer of protection.
Async Database Operations
Database calls use asynchronous methods because database access is an I/O operation.

Testing Done
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

  
---What I Learned---

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
===Future Improvements===
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
- 
For now, I kept it as a console application so I could focus on understanding the backend structure, database operations, testing, and Git workflow.

For any help/Query Reach out on my GitHub Repository-
https://github.com/SuryanshTripathi01/StudentManagementSystem

License
See the LICENSE file in this repository.
