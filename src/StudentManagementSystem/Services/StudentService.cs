using System.Net.Mail;
using StudentManagementSystem.Data;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Services;

public sealed class StudentService
{
    private readonly IStudentRepository _repository;

    public StudentService(IStudentRepository repository) => _repository = repository;

    public async Task<int> AddAsync(Student student, CancellationToken cancellationToken = default)
    {
        Validate(student);
        if (await _repository.GetByEnrollmentNumberAsync(student.EnrollmentNumber, cancellationToken) is not null)
            throw new InvalidOperationException("Enrollment number already exists.");
        return await _repository.AddAsync(student, cancellationToken);
    }

    public Task<IReadOnlyList<Student>> GetAllAsync(CancellationToken cancellationToken = default) => _repository.GetAllAsync(cancellationToken);
    public Task<Student?> GetByIdAsync(int id, CancellationToken cancellationToken = default) => _repository.GetByIdAsync(id, cancellationToken);
    public Task<IReadOnlyList<Student>> SearchAsync(string term, CancellationToken cancellationToken = default) => _repository.SearchAsync(term.Trim(), cancellationToken);

    public async Task UpdateAsync(Student student, CancellationToken cancellationToken = default)
    {
        Validate(student);
        var existing = await _repository.GetByEnrollmentNumberAsync(student.EnrollmentNumber, cancellationToken);
        if (existing is not null && existing.Id != student.Id)
            throw new InvalidOperationException("Enrollment number belongs to another student.");
        if (!await _repository.UpdateAsync(student, cancellationToken))
            throw new KeyNotFoundException("Student was not found.");
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (!await _repository.DeleteAsync(id, cancellationToken))
            throw new KeyNotFoundException("Student was not found.");
    }

    private static void Validate(Student student)
    {
        if (string.IsNullOrWhiteSpace(student.EnrollmentNumber) || student.EnrollmentNumber.Length > 30)
            throw new ArgumentException("Enrollment number is required and must be 30 characters or fewer.");
        if (string.IsNullOrWhiteSpace(student.FullName) || student.FullName.Length > 100)
            throw new ArgumentException("Full name is required and must be 100 characters or fewer.");
        if (!IsValidEmail(student.Email))
            throw new ArgumentException("A valid email address is required.");
        if (string.IsNullOrWhiteSpace(student.Course) || student.Course.Length > 100)
            throw new ArgumentException("Course is required and must be 100 characters or fewer.");
        if (student.Semester is < 1 or > 12)
            throw new ArgumentException("Semester must be between 1 and 12.");
        if (student.DateOfBirth.Date >= DateTime.Today)
            throw new ArgumentException("Date of birth must be in the past.");
    }

    private static bool IsValidEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email) || email.Length > 150) return false;
        try { return new MailAddress(email).Address.Equals(email, StringComparison.OrdinalIgnoreCase); }
        catch (FormatException) { return false; }
    }
}
