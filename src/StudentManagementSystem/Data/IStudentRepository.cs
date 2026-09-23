using StudentManagementSystem.Models;

namespace StudentManagementSystem.Data;

public interface IStudentRepository
{
    Task<int> AddAsync(Student student, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Student>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Student?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Student?> GetByEnrollmentNumberAsync(string enrollmentNumber, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Student>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(Student student, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
