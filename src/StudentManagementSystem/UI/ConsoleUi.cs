using StudentManagementSystem.Models;
using StudentManagementSystem.Services;

namespace StudentManagementSystem.UI;

public sealed class ConsoleUi
{
    private readonly StudentService _service;

    public ConsoleUi(StudentService service) => _service = service;

    public async Task RunAsync(CancellationToken cancellationToken = default)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            PrintMenu();
            Console.Write("Select an option: ");
            var choice = Console.ReadLine()?.Trim();
            Console.WriteLine();
            try
            {
                switch (choice)
                {
                    case "1": await AddAsync(cancellationToken); break;
                    case "2": await ListAsync(cancellationToken); break;
                    case "3": await SearchAsync(cancellationToken); break;
                    case "4": await UpdateAsync(cancellationToken); break;
                    case "5": await DeleteAsync(cancellationToken); break;
                    case "6": return;
                    default: Console.WriteLine("Invalid option."); break;
                }
            }
            catch (Exception ex) when (ex is ArgumentException or InvalidOperationException or KeyNotFoundException)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
                Console.WriteLine("Check the database configuration and application logs/terminal output.");
            }
            Pause();
        }
    }

    private async Task AddAsync(CancellationToken ct)
    {
        var student = ReadStudent();
        var id = await _service.AddAsync(student, ct);
        Console.WriteLine($"Student added successfully. ID: {id}");
    }

    private async Task ListAsync(CancellationToken ct) => PrintStudents(await _service.GetAllAsync(ct));

    private async Task SearchAsync(CancellationToken ct)
    {
        var term = ReadRequired("Search name, enrollment, email, or course: ");
        PrintStudents(await _service.SearchAsync(term, ct));
    }

    private async Task UpdateAsync(CancellationToken ct)
    {
        var id = ReadInt("Student ID: ");
        var existing = await _service.GetByIdAsync(id, ct);
        if (existing is null) { Console.WriteLine("Student not found."); return; }
        Console.WriteLine("Press Enter to keep the existing value.");
        var updated = ReadStudent(existing);
        updated.Id = id;
        await _service.UpdateAsync(updated, ct);
        Console.WriteLine("Student updated successfully.");
    }

    private async Task DeleteAsync(CancellationToken ct)
    {
        var id = ReadInt("Student ID: ");
        var existing = await _service.GetByIdAsync(id, ct);
        if (existing is null) { Console.WriteLine("Student not found."); return; }
        Console.Write($"Delete {existing.FullName} ({existing.EnrollmentNumber})? [y/N]: ");
        if (!string.Equals(Console.ReadLine()?.Trim(), "y", StringComparison.OrdinalIgnoreCase)) { Console.WriteLine("Delete cancelled."); return; }
        await _service.DeleteAsync(id, ct);
        Console.WriteLine("Student deleted successfully.");
    }

    private static Student ReadStudent(Student? existing = null)
    {
        return new Student
        {
            EnrollmentNumber = ReadWithDefault("Enrollment number", existing?.EnrollmentNumber),
            FullName = ReadWithDefault("Full name", existing?.FullName),
            Email = ReadWithDefault("Email", existing?.Email),
            Course = ReadWithDefault("Course", existing?.Course),
            Semester = ReadIntWithDefault("Semester", existing?.Semester),
            DateOfBirth = ReadDateWithDefault("Date of birth (yyyy-MM-dd)", existing?.DateOfBirth)
        };
    }

    private static string ReadWithDefault(string label, string? current)
    {
        while (true)
        {
            Console.Write($"{label}{(current is null ? "" : $" [{current}]")}: ");
            var value = Console.ReadLine()?.Trim();
            if (string.IsNullOrWhiteSpace(value) && current is not null) return current;
            if (!string.IsNullOrWhiteSpace(value)) return value;
            Console.WriteLine("Value is required.");
        }
    }

    private static int ReadInt(string label)
    {
        while (true)
        {
            Console.Write(label);
            if (int.TryParse(Console.ReadLine(), out var value)) return value;
            Console.WriteLine("Enter a valid integer.");
        }
    }

    private static int ReadIntWithDefault(string label, int? current)
    {
        while (true)
        {
            Console.Write($"{label}{(current.HasValue ? $" [{current}]" : "")}: ");
            var input = Console.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(input) && current.HasValue) return current.Value;
            if (int.TryParse(input, out var value)) return value;
            Console.WriteLine("Enter a valid integer.");
        }
    }

    private static DateTime ReadDateWithDefault(string label, DateTime? current)
    {
        while (true)
        {
            Console.Write($"{label}{(current.HasValue ? $" [{current.Value:yyyy-MM-dd}]" : "")}: ");
            var input = Console.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(input) && current.HasValue) return current.Value;
            if (DateTime.TryParse(input, out var value)) return value;
            Console.WriteLine("Enter a valid date.");
        }
    }

    private static string ReadRequired(string label) => ReadWithDefault(label, null);

    private static void PrintStudents(IReadOnlyList<Student> students)
    {
        if (students.Count == 0) { Console.WriteLine("No students found."); return; }
        Console.WriteLine($"Found {students.Count} student(s):\n");
        Console.WriteLine("ID | Enrollment | Name | Email | Course | Sem | DOB");
        Console.WriteLine(new string('-', 110));
        foreach (var s in students)
            Console.WriteLine($"{s.Id} | {s.EnrollmentNumber} | {s.FullName} | {s.Email} | {s.Course} | {s.Semester} | {s.DateOfBirth:yyyy-MM-dd}");
    }

    private static void PrintMenu()
    {
        Console.Clear();
        Console.WriteLine("=========================================");
        Console.WriteLine("       STUDENT MANAGEMENT SYSTEM");
        Console.WriteLine("=========================================");
        Console.WriteLine("1. Add student");
        Console.WriteLine("2. List all students");
        Console.WriteLine("3. Search students");
        Console.WriteLine("4. Update student");
        Console.WriteLine("5. Delete student");
        Console.WriteLine("6. Exit");
        Console.WriteLine("=========================================");
    }

    private static void Pause()
    {
        Console.WriteLine("\nPress Enter to continue...");
        Console.ReadLine();
    }
}
