# Complete Run/Test/GitHub Runbook

## 1. Install software

Install .NET 10 SDK, MySQL 8.x, Git, and optionally Visual Studio 2026 or VS Code.

Check:

```powershell
dotnet --version
mysql --version
git --version
```

The .NET SDK must report a 10.0.x version.

## 2. Create the database

Open MySQL Workbench or MySQL CLI and run `database/schema.sql`.

Or run:

```sql
CREATE DATABASE student_management CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
```

The application will create the table automatically when it starts.

## 3. Configure credentials in PowerShell

```powershell
$env:SMS_DB_SERVER="127.0.0.1"
$env:SMS_DB_PORT="3306"
$env:SMS_DB_NAME="student_management"
$env:SMS_DB_USER="root"
$env:SMS_DB_PASSWORD="YOUR_MYSQL_PASSWORD"
```

These environment variables last only for the current PowerShell session.

## 4. Restore and build

From the repository root:

```powershell
dotnet restore StudentManagementSystem.sln
dotnet build StudentManagementSystem.sln --configuration Release
```

A successful build should end with `Build succeeded`.

## 5. Run unit tests

```powershell
dotnet test StudentManagementSystem.sln --configuration Release
```

The included tests cover duplicate enrollment, invalid semester, future DOB, and duplicate enrollment during update.

## 6. Run the application

```powershell
dotnet run --project src/StudentManagementSystem
```

Test this sequence manually:

1. Add a student.
2. List all students.
3. Search by name.
4. Search by enrollment number.
5. Update the student.
6. List again and confirm the update.
7. Delete the student.
8. List again and confirm deletion.
9. Try an invalid email.
10. Try semester 0 or 13.
11. Try a duplicate enrollment number.
12. Stop MySQL and confirm the application reports a startup/database error.

## 7. Verify directly in MySQL

```sql
USE student_management;
SELECT * FROM students;
```

## 8. Publish

Framework-dependent Windows release:

```powershell
dotnet publish src/StudentManagementSystem -c Release -r win-x64 --self-contained false -o publish
```

The `publish` directory contains the executable and required files. The target machine still needs a compatible .NET runtime.

## 9. GitHub repository

Create an empty GitHub repository, then:

```powershell
git init
git add .
git commit -m "Initial Student Management System"
git branch -M main
git remote add origin https://github.com/YOUR_USERNAME/student-management-system.git
git push -u origin main
```

## 10. Verify CI

Open the repository's Actions tab. The `.github/workflows/ci.yml` workflow should run automatically. It performs restore, Release build, and tests.

## 11. What to show an interviewer

- Repository tree
- README
- `Student.cs`
- `StudentService.cs`
- `MySqlStudentRepository.cs`
- `schema.sql`
- unit tests
- GitHub Actions workflow
- terminal showing the application
- MySQL table containing the records

## 12. Important limitation

GitHub is not the runtime host for this console program. GitHub is used here for source control, collaboration, CI, and optional release artifacts. To run the program for users, publish it and place it on a machine/environment with the .NET runtime and MySQL connectivity, or convert it into a web application/API and deploy it to an appropriate hosting platform.
