using Xunit;
using StudentManagementSystem.Data;
using StudentManagementSystem.Models;
using StudentManagementSystem.Services;

namespace StudentManagementSystem.Tests;

public sealed class StudentServiceTests
{
    [Fact]
    public async Task AddAsync_RejectsDuplicateEnrollmentNumber()
    {
        var repo = new FakeStudentRepository { ExistingByEnrollment = new Student { Id = 1, EnrollmentNumber = "STU001" } };
        var service = new StudentService(repo);
        var student = ValidStudent();
        student.EnrollmentNumber = "STU001";

        var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => service.AddAsync(student));
        Assert.Equal("Enrollment number already exists.", exception.Message);
    }

    [Fact]
    public async Task AddAsync_RejectsInvalidSemester()
    {
        var repo = new FakeStudentRepository();
        var service = new StudentService(repo);
        var student = ValidStudent();
        student.Semester = 0;

        await Assert.ThrowsAsync<ArgumentException>(() => service.AddAsync(student));
    }

    [Fact]
    public async Task AddAsync_RejectsFutureDateOfBirth()
    {
        var repo = new FakeStudentRepository();
        var service = new StudentService(repo);
        var student = ValidStudent();
        student.DateOfBirth = DateTime.Today.AddDays(1);

        await Assert.ThrowsAsync<ArgumentException>(() => service.AddAsync(student));
    }

    [Fact]
    public async Task UpdateAsync_RejectsEnrollmentOwnedByAnotherStudent()
    {
        var repo = new FakeStudentRepository { ExistingByEnrollment = new Student { Id = 99, EnrollmentNumber = "STU002" } };
        var service = new StudentService(repo);
        var student = ValidStudent();
        student.Id = 1;
        student.EnrollmentNumber = "STU002";

        var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => service.UpdateAsync(student));
        Assert.Equal("Enrollment number belongs to another student.", exception.Message);
    }

    private static Student ValidStudent() => new()
    {
        EnrollmentNumber = "STU001",
        FullName = "Aarav Sharma",
        Email = "aarav@example.com",
        Course = "MCA",
        Semester = 3,
        DateOfBirth = new DateTime(2002, 5, 20)
    };

    private sealed class FakeStudentRepository : IStudentRepository
    {
        public Student? ExistingByEnrollment { get; init; }
        public Task<int> AddAsync(Student student, CancellationToken cancellationToken = default) => Task.FromResult(1);
        public Task<IReadOnlyList<Student>> GetAllAsync(CancellationToken cancellationToken = default) => Task.FromResult<IReadOnlyList<Student>>(Array.Empty<Student>());
        public Task<Student?> GetByIdAsync(int id, CancellationToken cancellationToken = default) => Task.FromResult<Student?>(null);
        public Task<Student?> GetByEnrollmentNumberAsync(string enrollmentNumber, CancellationToken cancellationToken = default) => Task.FromResult(ExistingByEnrollment);
        public Task<IReadOnlyList<Student>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default) => Task.FromResult<IReadOnlyList<Student>>(Array.Empty<Student>());
        public Task<bool> UpdateAsync(Student student, CancellationToken cancellationToken = default) => Task.FromResult(true);
        public Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default) => Task.FromResult(true);
    }
}
