namespace StudentManagementSystem.Configuration;

public sealed class DatabaseOptions
{
    public string Server { get; init; } = "127.0.0.1";
    public uint Port { get; init; } = 3306;
    public string Database { get; init; } = "student_management";
    public string User { get; init; } = "root";
    public string Password { get; init; } = string.Empty;

    public string ConnectionString =>
        $"Server={Server};Port={Port};Database={Database};User ID={User};Password={Password};";
}
