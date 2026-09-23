using MySqlConnector;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Data;

public sealed class MySqlStudentRepository : IStudentRepository
{
    private readonly string _connectionString;

    public MySqlStudentRepository(string connectionString) => _connectionString = connectionString;

    public async Task<int> AddAsync(Student student, CancellationToken cancellationToken = default)
    {
        const string sql = """
            INSERT INTO students (enrollment_number, full_name, email, course, semester, date_of_birth)
            VALUES (@enrollment, @name, @email, @course, @semester, @dob);
            SELECT LAST_INSERT_ID();
            """;
        await using var connection = new MySqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);
        await using var command = new MySqlCommand(sql, connection);
        AddParameters(command, student);
        var result = await command.ExecuteScalarAsync(cancellationToken);
        return Convert.ToInt32(result);
    }

    public async Task<IReadOnlyList<Student>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        const string sql = "SELECT id, enrollment_number, full_name, email, course, semester, date_of_birth, created_at, updated_at FROM students ORDER BY id;";
        return await QueryAsync(sql, null, cancellationToken);
    }

    public async Task<Student?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        const string sql = "SELECT id, enrollment_number, full_name, email, course, semester, date_of_birth, created_at, updated_at FROM students WHERE id = @id;";
        var parameters = new Dictionary<string, object?> { ["@id"] = id };
        return (await QueryAsync(sql, parameters, cancellationToken)).FirstOrDefault();
    }

    public async Task<Student?> GetByEnrollmentNumberAsync(string enrollmentNumber, CancellationToken cancellationToken = default)
    {
        const string sql = "SELECT id, enrollment_number, full_name, email, course, semester, date_of_birth, created_at, updated_at FROM students WHERE enrollment_number = @enrollment;";
        var parameters = new Dictionary<string, object?> { ["@enrollment"] = enrollmentNumber };
        return (await QueryAsync(sql, parameters, cancellationToken)).FirstOrDefault();
    }

    public async Task<IReadOnlyList<Student>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default)
    {
        const string sql = """
            SELECT id, enrollment_number, full_name, email, course, semester, date_of_birth, created_at, updated_at
            FROM students
            WHERE full_name LIKE @term OR enrollment_number LIKE @term OR email LIKE @term OR course LIKE @term
            ORDER BY full_name;
            """;
        var parameters = new Dictionary<string, object?> { ["@term"] = $"%{searchTerm}%" };
        return await QueryAsync(sql, parameters, cancellationToken);
    }

    public async Task<bool> UpdateAsync(Student student, CancellationToken cancellationToken = default)
    {
        const string sql = """
            UPDATE students
            SET enrollment_number = @enrollment, full_name = @name, email = @email,
                course = @course, semester = @semester, date_of_birth = @dob
            WHERE id = @id;
            """;
        await using var connection = new MySqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);
        await using var command = new MySqlCommand(sql, connection);
        AddParameters(command, student);
        command.Parameters.AddWithValue("@id", student.Id);
        return await command.ExecuteNonQueryAsync(cancellationToken) > 0;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        const string sql = "DELETE FROM students WHERE id = @id;";
        await using var connection = new MySqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);
        await using var command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@id", id);
        return await command.ExecuteNonQueryAsync(cancellationToken) > 0;
    }

    private async Task<IReadOnlyList<Student>> QueryAsync(string sql, Dictionary<string, object?>? parameters, CancellationToken cancellationToken)
    {
        var students = new List<Student>();
        await using var connection = new MySqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);
        await using var command = new MySqlCommand(sql, connection);
        if (parameters is not null)
        {
            foreach (var parameter in parameters)
                command.Parameters.AddWithValue(parameter.Key, parameter.Value ?? DBNull.Value);
        }
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            students.Add(new Student
            {
                Id = reader.GetInt32("id"),
                EnrollmentNumber = reader.GetString("enrollment_number"),
                FullName = reader.GetString("full_name"),
                Email = reader.GetString("email"),
                Course = reader.GetString("course"),
                Semester = reader.GetByte("semester"),
                DateOfBirth = reader.GetDateTime("date_of_birth"),
                CreatedAt = reader.GetDateTime("created_at"),
                UpdatedAt = reader.GetDateTime("updated_at")
            });
        }
        return students;
    }

    private static void AddParameters(MySqlCommand command, Student student)
    {
        command.Parameters.AddWithValue("@enrollment", student.EnrollmentNumber);
        command.Parameters.AddWithValue("@name", student.FullName);
        command.Parameters.AddWithValue("@email", student.Email);
        command.Parameters.AddWithValue("@course", student.Course);
        command.Parameters.AddWithValue("@semester", student.Semester);
        command.Parameters.AddWithValue("@dob", student.DateOfBirth.Date);
    }
}
